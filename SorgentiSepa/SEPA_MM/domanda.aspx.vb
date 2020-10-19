Imports System.IO
Imports ExpertPdf.HtmlToPdf

Namespace CM

    Partial Class domanda
        Inherits PageSetIdMode



        Dim lValoreCorrente As Long
        Dim sAnnoIsee As String
        Dim sAnnoCanone As String

        Dim par As New [Global]()
        Dim bEseguito As Boolean = False
        Dim scriptblock As String




#Region " Codice generato da Progettazione Web Form "

        'Chiamata richiesta da Progettazione Web Form.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: questa chiamata al metodo è richiesta da Progettazione Web Form.
            'Non modificarla nell'editor del codice.
            InitializeComponent()

        End Sub

#End Region


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
            If IsPostBack = False And bEseguito = False Then
                txtDataPG.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
                bMemorizzato = False
                lIdDomanda = Request.QueryString("ID")
                lIdDichiarazione = Request.QueryString("ID1")
                lProgr = Request.QueryString("PROGR")
                Fl_Integrazione = Request.QueryString("INT")
                Fl_DerogaInCorso = Request.QueryString("DER")
                Fl_VerificaReq = Request.QueryString("VER")
                Fl_US = Request.QueryString("US")
                Fl_inAssegnazione = Request.QueryString("INASS")



                If Fl_Integrazione = "1" Then
                    H1.Value = "1"
                    H2.Value = "1"
                End If


                txtTab.Value = "1"

                par.caricaCheckBoxList("SELECT COD, DESCRIZIONE FROM T_TIPO_ESCLUSIONE_DOMANDE", CType(Dom_Requisiti1.FindControl("chkListRequisiti"), CheckBoxList), "cod", "descrizione")

                If lIdDomanda = -1 Then
                    'iTab = 0
                    lNuovaDomanda = 1
                    imgStampa.ImageUrl = "NuoveImm\Img_No_stampa.png"
                    imgAttendi.Visible = True
                    NuovaDomanda()
                    imgAttendi.Visible = False
                    FL_VECCHIO_BANDO = "1"
                Else
                    'iTab = 0
                    lNuovaDomanda = 0
                    imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                    imgAttendi.Visible = True
                    VisualizzaDomanda()
                    imgAttendi.Visible = False
                    If Session.Item("ANAGRAFE") = "1" Then
                        imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('Anagrafe.aspx?ID=" & par.criptamolto(lIdDichiarazione) & "&T=0','Anagrafe','top=0,left=0,width=600,height=400');")
                    End If


                End If

                bEseguito = True
            End If
            H1.Value = H2.Value
            If Session.Item("MOD_ERP_REQUISITI") = "0" Then
                CType(Dom_Requisiti1.FindControl("chkListRequisiti"), CheckBoxList).Enabled = False
            Else
                CType(Dom_Requisiti1.FindControl("chkListRequisiti"), CheckBoxList).Enabled = True
            End If
            If Session.Item("ID_CAF") = "2" Or Session.Item("ID_CAF") = "6" Or Session.Item("ID_CAF") = "61" Then
                imgElencostampe.Attributes.Add("onclick", "javascript:ElencoStampe(" & lIdDichiarazione & "," & numDichiarazione & ");")
            Else
                imgElencostampe.Visible = False
            End If

            CType(Dom_Richiedente1.FindControl("HyperLink1"), HyperLink).Target = "_blank"
            CType(Dom_Richiedente1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "Help_Domanda_ERP.pdf"

            CType(Dom_Dichiara1.FindControl("HyperLink1"), HyperLink).Target = "_blank"
            CType(Dom_Dichiara1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "Help_Domanda_ERP.pdf"

            CType(Dom_Requisiti1.FindControl("HyperLink1"), HyperLink).Target = "_blank"
            CType(Dom_Requisiti1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "Help_Domanda_ERP.pdf"

            CType(Dom_Abitative_1_1.FindControl("HyperLink2"), HyperLink).Target = "_blank"
            CType(Dom_Abitative_1_1.FindControl("HyperLink2"), HyperLink).NavigateUrl = "Help_Domanda_ERP.pdf"

            CType(Dom_Abitative_2_1.FindControl("HyperLink2"), HyperLink).Target = "_blank"
            CType(Dom_Abitative_2_1.FindControl("HyperLink2"), HyperLink).NavigateUrl = "Help_Domanda_ERP.pdf"

            CType(Dom_Familiari1.FindControl("HyperLink1"), HyperLink).Target = "_blank"
            CType(Dom_Familiari1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "Help_Domanda_ERP.pdf"

        End Sub

        Public Property numDichiarazione() As String
            Get
                If Not (ViewState("par_numDichiarazione") Is Nothing) Then
                    Return CStr(ViewState("par_numDichiarazione"))
                Else
                    Return "0"
                End If
            End Get

            Set(ByVal value As String)
                ViewState("par_numDichiarazione") = value
            End Set

        End Property

        Private Function VisualizzaDomanda()
            Dim CT1 As DropDownList
            Dim cT As TextBox
            Dim lIndiceAppoggio_0 As Long
            Dim lIndiceAppoggio_1 As Long
            Dim lIndiceAppoggio_2 As Long
            Dim LB1 As Label
            Dim VALORE_R As Double

            Try
                par.OracleConn.Open()
                par.SettaCommand(par)
                HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)


                If Request.QueryString("SS") = "1" Then
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_RINNOVO='1' WHERE ID=" & lIdDomanda
                    par.cmd.ExecuteNonQuery()
                End If


                If Session.Item("ANAGRAFE") = "0" Then
                    imgAnagrafe.Visible = False
                    imgAnagrafe.Attributes.Clear()
                Else
                    imgAnagrafe.Visible = True
                End If


                par.cmd.CommandText = "select anno_isee from bandi where stato=1 order by id desc"
                Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader11.Read Then
                    AnnoIsee = myReader11("anno_isee")
                End If
                myReader11.Close()


                par.cmd.CommandText = "SELECT BANDI.STATO,DOMANDE_BANDO.FL_RINNOVO FROM BANDI,DOMANDE_BANDO WHERE DOMANDE_BANDO.ID=" & lIdDomanda & " AND DOMANDE_BANDO.ID_BANDO=BANDI.ID"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    FL_RINNOVO = par.IfNull(myReader("FL_RINNOVO"), "")
                    FL_VECCHIO_BANDO = CStr(myReader("STATO"))
                    'If myReader("STATO") <> 1 And myReader("FL_RINNOVO") <> "1" Then
                    If myReader("STATO") <> 1 And Fl_Integrazione <> "1" Then
                        btnSalva.Visible = False
                        'Label5.Visible = False
                        imgStampa.Visible = False
                        'Label6.Visible = False

                        Dom_Richiedente1.DisattivaTutto()
                        Dom_Abitative_1_1.DisattivaTutto()
                        Dom_Abitative_2_1.DisattivaTutto()
                        Dom_Dichiara1.DisattivaTutto()
                        Dom_Familiari1.DisattivaTutto()
                        Dom_Requisiti1.DisattivaTutto()
                        If Fl_US <> "1" Then
                            Response.Write("<script>alert('Il Bando a cui appartiene questa domanda è CHIUSO. Per apportare modifiche usare le funzioni di INTEGRAZIONE!');</script>")
                        End If
                    End If
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT DICHIARAZIONI.ID_STATO  FROM DICHIARAZIONI,DOMANDE_BANDO WHERE DOMANDE_BANDO.ID=" & lIdDomanda & " AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    If myReader("ID_STATO") <> 1 Then
                        btnSalva.Visible = False
                        'Label5.Visible = False
                        imgStampa.Visible = False
                        'Label6.Visible = False

                        Dom_Richiedente1.DisattivaTutto()
                        Dom_Abitative_1_1.DisattivaTutto()
                        Dom_Abitative_2_1.DisattivaTutto()
                        Dom_Dichiara1.DisattivaTutto()
                        Dom_Familiari1.DisattivaTutto()
                        Dom_Requisiti1.DisattivaTutto()
                        If Fl_US <> "1" Then
                            Response.Write("<script>alert('LA DICHIARAZIONE NON è COMPLETA. Non è possibile apportare modifiche!');</script>")
                        End If
                    End If
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT DOMANDE_BANDO.N_DISTINTA,DICHIARAZIONI.ID_CAF,CAF_WEB.COD_CAF,DOMANDE_BANDO.FL_RINNOVO FROM DOMANDE_BANDO,DICHIARAZIONI,CAF_WEB WHERE DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DICHIARAZIONI.ID_CAF=CAF_WEB.ID AND DOMANDE_BANDO.ID=" & lIdDomanda
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    If Session.Item("ID_CAF") = "6" Then
                        If Session.Item("ID_CAF") = "6" Then
                            LBLENTE.Visible = True
                            LBLENTE.Text = "Ente Ins.: " & par.IfNull(myReader("COD_CAF"), "")
                            If par.IfNull(myReader("N_DISTINTA"), -1) = 0 And myReader("ID_CAF") <> Session.Item("ID_CAF") Then

                                btnSalva.Visible = False
                                'Label5.Visible = False
                                imgStampa.Visible = False
                                'Label6.Visible = False

                                Dom_Richiedente1.DisattivaTutto()
                                Dom_Abitative_2_1.DisattivaTutto()
                                Dom_Abitative_1_1.DisattivaTutto()
                                Dom_Dichiara1.DisattivaTutto()
                                Dom_Familiari1.DisattivaTutto()
                                Dom_Requisiti1.DisattivaTutto()
                                If Fl_US <> "1" Then
                                    Response.Write("<script>alert('Non è possibile modificare. La domanda è ancora in carico presso un ente esterno!');</script>")
                                End If
                            End If
                        Else
                            'If myReader("N_DISTINTA") > 0 And myReader("FL_RINNOVO") <> "1" Then
                            If par.IfNull(myReader("N_DISTINTA"), -1) > 0 And Fl_Integrazione <> "1" Then
                                btnSalva.Visible = False
                                'Label5.Visible = False
                                imgStampa.Visible = False
                                'Label6.Visible = False

                                Dom_Richiedente1.DisattivaTutto()
                                Dom_Abitative_2_1.DisattivaTutto()
                                Dom_Abitative_1_1.DisattivaTutto()
                                Dom_Dichiara1.DisattivaTutto()
                                Dom_Familiari1.DisattivaTutto()
                                Dom_Requisiti1.DisattivaTutto()
                                If Fl_US <> "1" Then
                                    Response.Write("<script>alert('Non è possibile modificare. La domanda è stata scaricata nella distinta N. " & par.IfNull(myReader("N_DISTINTA"), "") & " ! USARE LE FUNZIONI DI INTEGRAZIONE');</script>")
                                End If
                            End If
                        End If
                    Else
                        '            If myReader("N_DISTINTA") > 0 And myReader("FL_RINNOVO") <> "1" Then
                        If par.IfNull(myReader("N_DISTINTA"), -2) > 0 And Fl_Integrazione <> "1" Then
                            btnSalva.Visible = False
                            'Label5.Visible = False
                            imgStampa.Visible = False
                            'Label6.Visible = False

                            Dom_Richiedente1.DisattivaTutto()
                            Dom_Abitative_2_1.DisattivaTutto()
                            Dom_Abitative_1_1.DisattivaTutto()
                            Dom_Dichiara1.DisattivaTutto()
                            Dom_Familiari1.DisattivaTutto()
                            Dom_Requisiti1.DisattivaTutto()
                            If Fl_US <> "1" Then
                                Response.Write("<script>alert('Non è possibile modificare. La domanda è stata scaricata nella distinta N. " & par.IfNull(myReader("N_DISTINTA"), "") & " ! USARE LE FUNZIONI DI INTEGRAZIONE');</script>")
                            End If
                        End If
                    End If
                Else
                    If Session.Item("ID_CAF") = "6" Then
                        If Session.Item("ID_CAF") = "6" Then
                            btnSalva.Visible = False
                            'Label5.Visible = False
                            imgStampa.Visible = False
                            'Label6.Visible = False

                            Dom_Richiedente1.DisattivaTutto()
                            Dom_Abitative_2_1.DisattivaTutto()
                            Dom_Abitative_1_1.DisattivaTutto()
                            Dom_Dichiara1.DisattivaTutto()
                            Dom_Familiari1.DisattivaTutto()
                            Dom_Requisiti1.DisattivaTutto()
                            If Fl_US <> "1" Then
                                Response.Write("<script>alert('Non è possibile modificare. La domanda NON è stata ancora creata!');</script>")
                            End If
                        End If
                    End If
                    End If
                    myReader.Close()

                    par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbNazioneNas", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
                    par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbNazioneRes", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
                    par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
                    par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
                    par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbProvRec", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
                    par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbTipoIRes", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
                    par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbTipoIRec", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
                    par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbResidenza", "SELECT DESCRIZIONE,ID FROM T_RESIDENZA_LOMBARDIA ORDER BY ID ASC", "DESCRIZIONE", "ID")

                    par.RiempiDList(Dom_Dichiara1, par.OracleConn, "cmbPresentaD", "SELECT DESCRIZIONE,COD FROM T_CAUSALI_DOMANDA ORDER BY COD ASC", "DESCRIZIONE", "COD")

                If Fl_US <> "1" Then
                    par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
                Else
                    par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO WHERE ID=" & lIdDomanda
                End If
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then

                    lIndice_Bando = myReader("ID_BANDO")
                    lIdDichiarazione = myReader("ID_DICHIARAZIONE")
                    lProgr = myReader("PROGR_COMPONENTE")
                    sStato = myReader("id_stato")
                    'lblISBAR.Text = par.Converti3(Format(par.IfNull(myReader("isbarc_r"), 0), "##,##0.000"))
                    lblISBAR.Text = par.Tronca(par.IfNull(myReader("isbarc_r"), 0))
                    lblPG.Text = par.IfNull(myReader("pg"), "")
                    txtDataPG.Text = par.FormattaData(par.IfNull(myReader("data_pg"), ""))
                    lblPG.ToolTip = lIdDomanda
                    If Session.Item("ID_CAF") = "2" Or Session.Item("ID_CAF") = "6" Or Session.Item("ID_CAF") = "61" Then
                        imgEventi.Attributes.Add("onclick", "javascript:window.open('EventiDomanda.aspx?ID=" & lIdDomanda & "&PG=" & lblPG.Text & "','Eventi','height=620,top=0,left=0,width=800,scrollbars=yes');return false;")
                    Else
                        imgEventi.Visible = False
                    End If

                    If Fl_US <> "1" Then
                        IMGPREFERENZE.Visible = True
                        IMGPREFERENZE.Attributes.Add("onclick", "javascript:window.open('GestionePreferenze.aspx?T=BANDOERP&ID=" & lIdDomanda & "&PG=" & lblPG.Text & "','Gestione','height=620,top=0,left=0,width=800,scrollbars=yes');")
                    End If

                    cT = Dom_Richiedente1.FindControl("txtPresso")
                    cT.Text = par.IfNull(myReader("PRESSO_REC_DNTE"), "")

                    cT = Dom_Richiedente1.FindControl("txtIndirizzoRec")
                    cT.Text = par.IfNull(myReader("IND_REC_DNTE"), "")

                    cT = Dom_Richiedente1.FindControl("txtCivicoRec")
                    cT.Text = par.IfNull(myReader("CIVICO_REC_DNTE"), "")
                    If cT.Text = "" Then

                    End If

                    cT = Dom_Richiedente1.FindControl("txtTelRec")
                    cT.Text = par.IfNull(myReader("TELEFONO_REC_DNTE"), "")

                    cT = Dom_Richiedente1.FindControl("txtCAPRec")
                    cT.Text = par.IfNull(myReader("CAP_REC_DNTE"), "")

                    CT1 = CType(Dom_Richiedente1.FindControl("cmbResidenza"), DropDownList)
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("PERIODO_RES"), 0)).Selected = True


                    CT1 = CType(Dom_Dichiara1.FindControl("cmbPresentaD"), DropDownList)
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_CAUSALE_DOMANDA"), 0)).Selected = True

                    If par.IfNull(myReader("FL_FAM_1"), "0") = "0" Then
                        CType(Dom_Dichiara1.FindControl("cf1"), CheckBox).Checked = False
                    Else
                        CType(Dom_Dichiara1.FindControl("cf1"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("FL_FAM_2"), "0") = "0" Then
                        CType(Dom_Dichiara1.FindControl("cf2"), CheckBox).Checked = False
                    Else
                        CType(Dom_Dichiara1.FindControl("cf2"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("FL_FAM_3"), "0") = "0" Then
                        CType(Dom_Dichiara1.FindControl("cf3"), CheckBox).Checked = False
                    Else
                        CType(Dom_Dichiara1.FindControl("cf3"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("FL_FAM_4"), "0") = "0" Then
                        CType(Dom_Dichiara1.FindControl("cf4"), CheckBox).Checked = False
                    Else
                        CType(Dom_Dichiara1.FindControl("cf4"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("FL_PROFUGO"), "0") = "0" Then
                        CType(Dom_Dichiara1.FindControl("cfProfugo"), CheckBox).Checked = False
                    Else
                        CType(Dom_Dichiara1.FindControl("cfProfugo"), CheckBox).Checked = True
                    End If


                    If par.IfNull(myReader("FL_MOROSITA_G"), "0") = "0" Then
                        CType(Dom_Abitative_1_1.FindControl("chMG"), CheckBox).Checked = False
                    Else
                        CType(Dom_Abitative_1_1.FindControl("chMG"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("FL_AFF_ONEROSO"), "0") = "0" Then
                        CType(Dom_Abitative_2_1.FindControl("chAO"), CheckBox).Checked = False
                    Else
                        CType(Dom_Abitative_2_1.FindControl("chAO"), CheckBox).Checked = True
                    End If

                    cT = Dom_Abitative_2_1.FindControl("txtAnnoCanone")
                    cT.Text = par.IfNull(myReader("ANNO_RIF_CANONE"), "")

                    cT = Dom_Abitative_2_1.FindControl("txtSpese")
                    cT.Text = par.IfNull(myReader("IMPORTO_CANONE"), "0")

                    cT = Dom_Abitative_2_1.FindControl("txtAnnoAcc")
                    cT.Text = par.IfNull(myReader("ANNO_RIF_SPESE_ACC"), "")

                    cT = Dom_Abitative_2_1.FindControl("txtSpeseAcc")
                    cT.Text = par.IfNull(myReader("IMPORTO_SPESE_ACC"), "0")

                    If Session.Item("ID_CAF") = "2" Or Session.Item("ID_CAF") = "6" Or Session.Item("ID_CAF") = "61" Then
                        cT = Note1.FindControl("txtNote")
                        cT.Text = par.IfNull(myReader("NOTE_WEB"), "")

                        cT = Note1.FindControl("txtNoteClient")
                        cT.Text = par.IfNull(myReader("NOTE"), "")
                    Else
                        cT = Note1.FindControl("txtNote")
                        cT.Text = ""
                        cT.Enabled = False
                        cT = Note1.FindControl("txtNoteClient")
                        cT.Text = ""
                        cT.Enabled = False
                    End If


                    If par.IfNull(myReader("REQUISITO1"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("REQUISITO1"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("REQUISITO2"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR2"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR2"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("REQUISITO3"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR3"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR3"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("REQUISITO4"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR4"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR4"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("REQUISITO5"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR5"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR5"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("REQUISITO7"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR6"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR6"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("REQUISITO8"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR7"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR7"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("REQUISITO9"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR8"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR8"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("FL_MOROSITA"), "0") = "0" Then
                        CType(Dom_Abitative_1_1.FindControl("chMorosita"), CheckBox).Checked = False
                    Else
                        CType(Dom_Abitative_1_1.FindControl("chMorosita"), CheckBox).Checked = True
                    End If


                    par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF1", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=0 order by id asc", "DESCRIZIONE", "ID")
                    par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF2", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=1 order by id asc", "DESCRIZIONE", "ID")
                    par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF3", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=2 order by id asc", "DESCRIZIONE", "ID")
                    par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF4", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=3 order by id asc", "DESCRIZIONE", "ID")
                    par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF5", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=4 order by id asc", "DESCRIZIONE", "ID")
                    par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF6", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=5 order by id asc", "DESCRIZIONE", "ID")
                    par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF7", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=6 order by id asc", "DESCRIZIONE", "ID")


                    par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA1", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=7 order by id asc", "DESCRIZIONE", "ID")
                    par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA2", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=8 order by id asc", "DESCRIZIONE", "ID")
                    par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA3", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=9 order by id asc", "DESCRIZIONE", "ID")
                    par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA4", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=10 order by id asc", "DESCRIZIONE", "ID")
                    par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA5", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=11 order by id asc", "DESCRIZIONE", "ID")

                    par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA6", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=12 order by id asc", "DESCRIZIONE", "ID")
                    par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA7", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=13 order by id asc", "DESCRIZIONE", "ID")
                    par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA8", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=14 order by id asc", "DESCRIZIONE", "ID")
                    par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA9", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=15 order by id asc", "DESCRIZIONE", "ID")

                    CT1 = Dom_Familiari1.FindControl("cmbF1")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_0"), 0)).Selected = True

                    CT1 = Dom_Familiari1.FindControl("cmbF2")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_1"), 0)).Selected = True

                    CT1 = Dom_Familiari1.FindControl("cmbF3")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_2"), 0)).Selected = True

                    CT1 = Dom_Familiari1.FindControl("cmbF4")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_3"), 0)).Selected = True

                    CT1 = Dom_Familiari1.FindControl("cmbF5")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_4"), 0)).Selected = True

                    CT1 = Dom_Familiari1.FindControl("cmbF6")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_5"), 0)).Selected = True

                    CT1 = Dom_Familiari1.FindControl("cmbF7")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_6"), 0)).Selected = True

                    CT1 = Dom_Abitative_1_1.FindControl("cmbA1")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_7"), 0)).Selected = True

                    CT1 = Dom_Abitative_1_1.FindControl("cmbA2")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_8"), 0)).Selected = True

                    CT1 = Dom_Abitative_1_1.FindControl("cmbA3")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_9"), 0)).Selected = True

                    CT1 = Dom_Abitative_1_1.FindControl("cmbA4")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_10"), 0)).Selected = True

                    CT1 = Dom_Abitative_1_1.FindControl("cmbA5")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_11"), 0)).Selected = True

                    CT1 = Dom_Abitative_2_1.FindControl("cmbA6")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_12"), 0)).Selected = True

                    CT1 = Dom_Abitative_2_1.FindControl("cmbA7")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_13"), 0)).Selected = True

                    CT1 = Dom_Abitative_2_1.FindControl("cmbA8")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_14"), 0)).Selected = True

                    CT1 = Dom_Abitative_2_1.FindControl("cmbA9")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_PARA_15"), 0)).Selected = True

                    Dim item As ListItem

                    par.cmd.CommandText = "select * from comuni_nazioni where id=" & par.IfNull(myReader("ID_LUOGO_REC_DNTE"), 0)
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    If myReader1.Read Then
                        par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbProvRec", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")

                        CT1 = Dom_Richiedente1.FindControl("cmbProvRec")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader1("SIGLA"), 0)).Selected = True
                        item = CT1.SelectedItem

                        par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneRec", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")

                        CT1 = Dom_Richiedente1.FindControl("cmbComuneRec")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(par.IfNull(myReader1("NOME"), "")).Selected = True
                        item = CT1.SelectedItem

                        'cT = Dom_Richiedente1.FindControl("txtCAPRec")
                        'cT.Text = par.IfNull(myReader1("CAP"), "")

                    End If
                    myReader1.Close()

                    CT1 = Dom_Richiedente1.FindControl("cmbTipoIRec")
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_TIPO_IND_REC_DNTE"), "")).Selected = True

                    Select Case par.IfNull(myReader("PERIODO_RES"), 0)
                        Case 0
                            VALORE_R = 0
                        Case 1
                            VALORE_R = 0
                        Case 2
                            VALORE_R = 40
                        Case 3
                            VALORE_R = 85
                        Case 4
                            VALORE_R = 0
                    End Select
                    VALORE_R = (VALORE_R / 100) * 0.3

                    txtIndici.Text = "V1=" & (par.IfNull(myReader("isbar"), 0)) _
                               & "&V2=" & (par.IfNull(myReader("isbarc"), 0)) _
                               & "&V3=" & (par.IfNull(myReader("isbarc_r"), 0)) _
                               & "&V4=" & Format(par.IfNull(myReader("disagio_a"), 0), "##,##0.000000000000000") _
                               & "&V5=" & Format(par.IfNull(myReader("disagio_e"), 0), "##,##0.000000000000000") _
                               & "&V6=" & Format(par.IfNull(myReader("disagio_f"), 0), "##,##0.000000000000000") _
                               & "&V7=" & par.Converti(par.IfNull(myReader("reddito_isee"), 0)) _
                               & "&V8=" & par.Converti(par.IfNull(myReader("ISR_ERP"), 0)) _
                               & "&V9=" & par.Converti(par.IfNull(myReader("ISP_ERP"), 0)) _
                               & "&V10=" & par.Converti(par.IfNull(myReader("ISe_ERP"), 0)) _
                               & "&V11=" & Format(VALORE_R, "##,##0.000000000000000") _
                               & "&V12=" & par.Converti(par.IfNull(myReader("PSE"), 0)) _
                               & "&V13=" & par.Converti(par.IfNull(myReader("VSE"), 0)) _
                               & "&PG=" & lblPG.Text & "&UJ=3"

                End If
                myReader.Close()

                For Each Items As ListItem In CType(Dom_Requisiti1.FindControl("chkListRequisiti"), CheckBoxList).Items
                    par.cmd.CommandText = "SELECT ID_DOMANDA, ID_TIPO_ESCLUSIONE FROM DOMANDE_ESCLUSIONI where ID_TIPO_ESCLUSIONE=" & Items.Value & " and id_domanda=" & lIdDomanda
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Items.Selected = True
                    Else
                        Items.Selected = False
                    End If
                    myReader.Close()
                Next

                par.cmd.CommandText = "SELECT TIPO_BANDO,DATA_INIZIO FROM BANDI WHERE ID=" & lIndice_Bando
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    lBando = par.IfNull(myReader("TIPO_BANDO"), "-1")
                    Select Case lBando
                        Case 0
                            lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-1"
                        Case 1
                            lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-2"
                        Case 2
                            lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-3"
                    End Select
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND PROGR=" & lProgr
                myReader = par.cmd.ExecuteReader()
                Dim sPresso As String
                sPresso = ""

                If myReader.Read() Then
                    cT = Dom_Richiedente1.FindControl("txtCognome")
                    cT.Text = par.IfNull(myReader("COGNOME"), "")

                    cT = Dom_Richiedente1.FindControl("txtNome")
                    cT.Text = par.IfNull(myReader("NOME"), "")

                    cT = Dom_Richiedente1.FindControl("txtDataNascita")
                    cT.Text = Mid(par.IfNull(myReader("DATA_NASCITA"), ""), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_NASCITA"), ""), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_NASCITA"), ""), 1, 4)

                    cT = Dom_Richiedente1.FindControl("txtCF")
                    cT.Text = par.IfNull(myReader("COD_FISCALE"), "")

                    CT1 = Dom_Richiedente1.FindControl("cmbSesso")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(par.IfNull(myReader("SESSO"), "M")).Selected = True

                    lIndice_Componente = myReader("ID")

                    If lProgr <> 0 Then
                        par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD='" & Mid(cT.Text, 12, 4) & "'"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read() Then
                            CT1 = Dom_Richiedente1.FindControl("cmbNazioneNas")
                            If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                                CT1.SelectedIndex = -1
                                CT1.Items.FindByText(myReader1("NOME")).Selected = True
                                LB1 = Dom_Richiedente1.FindControl("Label6")
                                LB1.Visible = False
                                LB1 = Dom_Richiedente1.FindControl("Label7")
                                LB1.Visible = False
                                CT1 = Dom_Richiedente1.FindControl("cmbPrNas")
                                CT1.Visible = False
                                CT1 = Dom_Richiedente1.FindControl("cmbComuneNas")
                                CT1.Visible = False
                            Else
                                CT1.SelectedIndex = -1
                                CT1.Items.FindByText("ITALIA").Selected = True
                                CT1 = Dom_Richiedente1.FindControl("cmbPrNas")
                                CT1.SelectedIndex = -1
                                CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                                par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                                CT1 = Dom_Richiedente1.FindControl("cmbComuneNas")
                                CT1.SelectedIndex = -1
                                CT1.Items.FindByText(myReader1("NOME")).Selected = True
                            End If
                        End If
                        myReader1.Close()
                    End If

                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI WHERE ID=" & lIdDichiarazione
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    lblPGDic.Text = lblSPG.Text & "-" & myReader("PG") & "-F205"
                    iNumComponenti = myReader("N_COMP_NUCLEO") - 1
                    numDichiarazione = myReader("PG")


                    If lProgr = 0 Then
                        cT = Dom_Richiedente1.FindControl("txtIndRes")
                        cT.Text = par.IfNull(myReader("IND_RES_DNTE"), "")

                        cT = Dom_Richiedente1.FindControl("txtCivicoRes")
                        cT.Text = par.IfNull(myReader("CIVICO_RES_DNTE"), "")

                        cT = Dom_Richiedente1.FindControl("txtTelRes")
                        cT.Text = par.IfNull(myReader("TELEFONO_DNTE"), "")

                        cT = Dom_Richiedente1.FindControl("txtCAPRes")
                        cT.Text = par.IfNull(myReader("CAP_RES_DNTE"), "")

                        lIndiceAppoggio_0 = myReader("ID_LUOGO_NAS_DNTE")
                        lIndiceAppoggio_1 = myReader("ID_LUOGO_RES_DNTE")
                        lIndiceAppoggio_2 = myReader("ID_TIPO_IND_RES_DNTE")

                    Else
                        par.cmd.CommandText = "SELECT * FROM COMP_NAS_RES WHERE ID_COMPONENTE=" & lIndice_Componente
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then


                            lIndiceAppoggio_1 = myReader2("ID_LUOGO_RES_DNTE")
                            lIndiceAppoggio_2 = myReader2("ID_TIPO_IND_RES_DNTE")

                            CType(Dom_Richiedente1.FindControl("txtIndRes"), TextBox).Text = par.IfNull(myReader2("IND_RES_DNTE"), "")
                            CType(Dom_Richiedente1.FindControl("txtCivicoRes"), TextBox).Text = par.IfNull(myReader2("CIVICO_RES_DNTE"), "")

                            CType(Dom_Richiedente1.FindControl("txtTelRes"), TextBox).Text = par.IfNull(myReader2("TELEFONO_DNTE"), "")

                            cT = Dom_Richiedente1.FindControl("txtCAPRes")
                            cT.Text = par.IfNull(myReader2("CAP_RES"), "")

                            CT1 = Dom_Richiedente1.FindControl("cmbTipoIRes")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByValue(par.IfNull(myReader2("ID_TIPO_IND_RES_DNTE"), 6)).Selected = True

                        End If
                        myReader2.Close()
                    End If
                End If
                myReader.Close()
                If lProgr = 0 Then
                    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        CT1 = Dom_Richiedente1.FindControl("cmbNazioneNas")
                        CT1.SelectedIndex = -1
                        If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
                            CT1.Items.FindByText(myReader("NOME")).Selected = True
                            LB1 = Dom_Richiedente1.FindControl("Label6")
                            LB1.Visible = False
                            LB1 = Dom_Richiedente1.FindControl("Label7")
                            LB1.Visible = False
                            CT1 = Dom_Richiedente1.FindControl("cmbPrNas")
                            CT1.Visible = False
                            CT1 = Dom_Richiedente1.FindControl("cmbComuneNas")
                            CT1.Visible = False
                        Else
                            CT1.Items.FindByText("ITALIA").Selected = True
                            CT1 = Dom_Richiedente1.FindControl("cmbPrNas")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                            par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                            CT1 = Dom_Richiedente1.FindControl("cmbComuneNas")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader("NOME")).Selected = True
                        End If
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT DESCRIZIONE FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2

                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        CT1 = Dom_Richiedente1.FindControl("cmbTipoIRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader("DESCRIZIONE")).Selected = True
                    End If
                    myReader.Close()
                End If

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    CT1 = Dom_Richiedente1.FindControl("cmbNazioneRes")
                    CT1.SelectedIndex = -1
                    If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader("NOME")).Selected = True
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = Dom_Richiedente1.FindControl("cmbPrRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                        par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = Dom_Richiedente1.FindControl("cmbComuneRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader("NOME")).Selected = True

                        'cT = Dom_Richiedente1.FindControl("txtCAPRes")
                        'cT.Text = myReader("CAP")

                    End If
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT F_RESIDENZA FROM COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & lIndice_Componente
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    Fl_Residenza = myReader("F_RESIDENZA")
                End If
                myReader.Close()
                If Fl_Residenza = "1" Then
                    CType(Dom_Abitative_2_1.FindControl("alert2"), Image).Visible = True
                    CType(Dom_Abitative_2_1.FindControl("alert3"), Image).Visible = True
                End If

                '09/11/2018
                par.cmd.CommandText = "select bandi_graduatoria_def.*,(Select upper(descrizione) from tab_stati where cod=domande_bando.id_stato) as stato_dom from bandi_graduatoria_def," _
                        & " domande_bando where bandi_graduatoria_def.id_domanda(+)=domande_bando.id and " _
                        & " domande_bando.id=" & lIdDomanda
                Dim myrec2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myrec2.Read Then
                    ' lblStatoDomanda.Text = par.IfNull(myrec2("stato_dom"), "")
                    lblPunteggio.Text = "Posizione n. " & myrec2("posizione")
                End If
                myrec2.Close()


                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("SELECT TO_CHAR(TO_DATE(DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_NASCITA"",PERC_INVAL,INDENNITA_ACC FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " ORDER BY PROGR ASC", par.OracleConn)

                Dim ds As New Data.DataSet()
                da.Fill(ds, "COMP_NUCLEO")
                DataGrid1.DataSource = ds
                DataGrid1.DataBind()
                da.Dispose()
                ds.Dispose()

                Dom_Richiedente1.DisattivaRichiedente()
                If lProgr = 0 Then
                    Dom_Richiedente1.DisattivaIndirizzo()
                End If

                Dim S As String


                S = ""
                If VerificaDati(S) = False Then
                    Response.Write("<script>alert('Attenzione...Sono state riscontrate anomalie nella domanda. Risolvere il/i problema/i prima di salvare e stampare la domanda!')</script>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                Else
                    imgStampa.Enabled = True
                    imgStampa.ImageUrl = "NuoveImm\Img_Stampa.png"
                End If

                If Fl_Integrazione = "1" And par.PulisciStrSql(CType(Dom_Abitative_2_1.FindControl("txtAnnoCanone"), TextBox).Text) <> AnnoIsee And par.PulisciStrSql(CType(Dom_Abitative_2_1.FindControl("txtAnnoCanone"), TextBox).Text) <> "" Then
                    'Response.Write("<script>alert('Attenzione...Verificare che il canone di locazione e le spese accessorie della scheda <ABITATIVE 2> siano riferiti al " & AnnoIsee & "!');</script>")
                    Label10.Text = "Aggiornare, se necessario, il Canone e le Spese accessorie della scheda ABITATIVE 2 per l'anno " & AnnoIsee
                End If


                If sStato <> "2" And sStato <> "1" Then
                    imgAnagrafe.Visible = False
                    imgAnagrafe.Attributes.Clear()
                End If

                If Fl_US <> "1" Then
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                Else
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                Session.Add("LAVORAZIONE", "1")

            Catch EX1 As Oracle.DataAccess.Client.OracleException
                If EX1.Number = 54 Then
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Pratica aperta da un altro utente. Non è possibile effettuare modifiche!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                    par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO WHERE ID=" & lIdDomanda
                    Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader5.Read() Then

                        lIndice_Bando = myReader5("ID_BANDO")
                        lIdDichiarazione = myReader5("ID_DICHIARAZIONE")
                        lProgr = myReader5("PROGR_COMPONENTE")

                        'lblISBAR.Text = par.Converti3(Format(par.IfNull(myReader5("isbarc_r"), 0), "##,##0.000"))
                        lblISBAR.Text = par.Tronca(par.IfNull(myReader5("isbarc_r"), 0))
                        lblPG.Text = par.IfNull(myReader5("pg"), "")
                        txtDataPG.Text = par.FormattaData(par.IfNull(myReader5("data_pg"), ""))

                        cT = Dom_Richiedente1.FindControl("txtPresso")
                        cT.Text = par.IfNull(myReader5("PRESSO_REC_DNTE"), "")

                        cT = Dom_Richiedente1.FindControl("txtIndirizzoRec")
                        cT.Text = par.IfNull(myReader5("IND_REC_DNTE"), "")

                        cT = Dom_Richiedente1.FindControl("txtCivicoRec")
                        cT.Text = par.IfNull(myReader5("CIVICO_REC_DNTE"), "")

                        cT = Dom_Richiedente1.FindControl("txtTelRec")
                        cT.Text = par.IfNull(myReader5("TELEFONO_REC_DNTE"), "")

                        cT = Dom_Richiedente1.FindControl("txtCAPRec")
                        cT.Text = par.IfNull(myReader5("CAP_REC_DNTE"), "")

                        CT1 = CType(Dom_Richiedente1.FindControl("cmbResidenza"), DropDownList)
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("PERIODO_RES"), 0)).Selected = True


                        CT1 = CType(Dom_Dichiara1.FindControl("cmbPresentaD"), DropDownList)
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_CAUSALE_DOMANDA"), 0)).Selected = True

                        If par.IfNull(myReader5("FL_FAM_1"), "0") = "0" Then
                            CType(Dom_Dichiara1.FindControl("cf1"), CheckBox).Checked = False
                        Else
                            CType(Dom_Dichiara1.FindControl("cf1"), CheckBox).Checked = True
                        End If

                        If par.IfNull(myReader5("FL_FAM_2"), "0") = "0" Then
                            CType(Dom_Dichiara1.FindControl("cf2"), CheckBox).Checked = False
                        Else
                            CType(Dom_Dichiara1.FindControl("cf2"), CheckBox).Checked = True
                        End If

                        If par.IfNull(myReader5("FL_FAM_3"), "0") = "0" Then
                            CType(Dom_Dichiara1.FindControl("cf3"), CheckBox).Checked = False
                        Else
                            CType(Dom_Dichiara1.FindControl("cf3"), CheckBox).Checked = True
                        End If

                        If par.IfNull(myReader5("FL_FAM_4"), "0") = "0" Then
                            CType(Dom_Dichiara1.FindControl("cf4"), CheckBox).Checked = False
                        Else
                            CType(Dom_Dichiara1.FindControl("cf4"), CheckBox).Checked = True
                        End If

                        If par.IfNull(myReader5("FL_PROFUGO"), "0") = "0" Then
                            CType(Dom_Dichiara1.FindControl("cfProfugo"), CheckBox).Checked = False
                        Else
                            CType(Dom_Dichiara1.FindControl("cfProfugo"), CheckBox).Checked = True
                        End If


                        If par.IfNull(myReader5("FL_MOROSITA_G"), "0") = "0" Then
                            CType(Dom_Abitative_1_1.FindControl("chMG"), CheckBox).Checked = False
                        Else
                            CType(Dom_Abitative_1_1.FindControl("chMG"), CheckBox).Checked = True
                        End If

                        If par.IfNull(myReader5("FL_AFF_ONEROSO"), "0") = "0" Then
                            CType(Dom_Abitative_2_1.FindControl("chAO"), CheckBox).Checked = False
                        Else
                            CType(Dom_Abitative_2_1.FindControl("chAO"), CheckBox).Checked = True
                        End If

                        cT = Dom_Abitative_2_1.FindControl("txtAnnoCanone")
                        cT.Text = par.IfNull(myReader5("ANNO_RIF_CANONE"), "")

                        cT = Dom_Abitative_2_1.FindControl("txtSpese")
                        cT.Text = par.IfNull(myReader5("IMPORTO_CANONE"), "0")

                        cT = Dom_Abitative_2_1.FindControl("txtAnnoAcc")
                        cT.Text = par.IfNull(myReader5("ANNO_RIF_SPESE_ACC"), "")

                        cT = Dom_Abitative_2_1.FindControl("txtSpeseAcc")
                        cT.Text = par.IfNull(myReader5("IMPORTO_SPESE_ACC"), "0")


                        If Session.Item("ID_CAF") = "2" Or Session.Item("ID_CAF") = "6" Or Session.Item("ID_CAF") = "61" Then
                            cT = Note1.FindControl("txtNote")
                            cT.Text = par.IfNull(myReader5("NOTE_WEB"), "")

                            cT = Note1.FindControl("txtNoteClient")
                            cT.Text = par.IfNull(myReader5("NOTE"), "")
                        Else
                            cT = Note1.FindControl("txtNote")
                            cT.Text = ""
                            cT.Enabled = False
                            cT = Note1.FindControl("txtNoteClient")
                            cT.Text = ""
                            cT.Enabled = False
                        End If


                        If par.IfNull(myReader5("REQUISITO1"), "0") = "0" Then
                            CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked = False
                        Else
                            CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked = True
                        End If

                        If par.IfNull(myReader5("REQUISITO1"), "0") = "0" Then
                            CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked = False
                        Else
                            CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked = True
                        End If

                        If par.IfNull(myReader5("REQUISITO2"), "0") = "0" Then
                            CType(Dom_Requisiti1.FindControl("chR2"), CheckBox).Checked = False
                        Else
                            CType(Dom_Requisiti1.FindControl("chR2"), CheckBox).Checked = True
                        End If

                        If par.IfNull(myReader5("REQUISITO3"), "0") = "0" Then
                            CType(Dom_Requisiti1.FindControl("chR3"), CheckBox).Checked = False
                        Else
                            CType(Dom_Requisiti1.FindControl("chR3"), CheckBox).Checked = True
                        End If

                        If par.IfNull(myReader5("REQUISITO4"), "0") = "0" Then
                            CType(Dom_Requisiti1.FindControl("chR4"), CheckBox).Checked = False
                        Else
                            CType(Dom_Requisiti1.FindControl("chR4"), CheckBox).Checked = True
                        End If

                        If par.IfNull(myReader5("REQUISITO5"), "0") = "0" Then
                            CType(Dom_Requisiti1.FindControl("chR5"), CheckBox).Checked = False
                        Else
                            CType(Dom_Requisiti1.FindControl("chR5"), CheckBox).Checked = True
                        End If

                        If par.IfNull(myReader5("REQUISITO7"), "0") = "0" Then
                            CType(Dom_Requisiti1.FindControl("chR6"), CheckBox).Checked = False
                        Else
                            CType(Dom_Requisiti1.FindControl("chR6"), CheckBox).Checked = True
                        End If

                        If par.IfNull(myReader5("REQUISITO8"), "0") = "0" Then
                            CType(Dom_Requisiti1.FindControl("chR7"), CheckBox).Checked = False
                        Else
                            CType(Dom_Requisiti1.FindControl("chR7"), CheckBox).Checked = True
                        End If

                        If par.IfNull(myReader5("REQUISITO9"), "0") = "0" Then
                            CType(Dom_Requisiti1.FindControl("chR8"), CheckBox).Checked = False
                        Else
                            CType(Dom_Requisiti1.FindControl("chR8"), CheckBox).Checked = True
                        End If

                        If par.IfNull(myReader5("FL_MOROSITA"), "0") = "0" Then
                            CType(Dom_Abitative_1_1.FindControl("chMorosita"), CheckBox).Checked = False
                        Else
                            CType(Dom_Abitative_1_1.FindControl("chMorosita"), CheckBox).Checked = True
                        End If


                        par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF1", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=0 order by id asc", "DESCRIZIONE", "ID")
                        par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF2", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=1 order by id asc", "DESCRIZIONE", "ID")
                        par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF3", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=2 order by id asc", "DESCRIZIONE", "ID")
                        par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF4", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=3 order by id asc", "DESCRIZIONE", "ID")
                        par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF5", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=4 order by id asc", "DESCRIZIONE", "ID")
                        par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF6", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=5 order by id asc", "DESCRIZIONE", "ID")
                        par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF7", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=6 order by id asc", "DESCRIZIONE", "ID")


                        par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA1", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=7 order by id asc", "DESCRIZIONE", "ID")
                        par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA2", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=8 order by id asc", "DESCRIZIONE", "ID")
                        par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA3", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=9 order by id asc", "DESCRIZIONE", "ID")
                        par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA4", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=10 order by id asc", "DESCRIZIONE", "ID")
                        par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA5", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=11 order by id asc", "DESCRIZIONE", "ID")

                        par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA6", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=12 order by id asc", "DESCRIZIONE", "ID")
                        par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA7", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=13 order by id asc", "DESCRIZIONE", "ID")
                        par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA8", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=14 order by id asc", "DESCRIZIONE", "ID")
                        par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA9", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=15 order by id asc", "DESCRIZIONE", "ID")

                        CT1 = Dom_Familiari1.FindControl("cmbF1")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_0"), 0)).Selected = True
                        CT1 = Dom_Familiari1.FindControl("cmbF2")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_1"), 0)).Selected = True
                        CT1 = Dom_Familiari1.FindControl("cmbF3")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_2"), 0)).Selected = True
                        CT1 = Dom_Familiari1.FindControl("cmbF4")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_3"), 0)).Selected = True
                        CT1 = Dom_Familiari1.FindControl("cmbF5")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_4"), 0)).Selected = True
                        CT1 = Dom_Familiari1.FindControl("cmbF6")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_5"), 0)).Selected = True
                        CT1 = Dom_Familiari1.FindControl("cmbF7")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_6"), 0)).Selected = True
                        CT1 = Dom_Abitative_1_1.FindControl("cmbA1")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_7"), 0)).Selected = True
                        CT1 = Dom_Abitative_1_1.FindControl("cmbA2")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_8"), 0)).Selected = True
                        CT1 = Dom_Abitative_1_1.FindControl("cmbA3")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_9"), 0)).Selected = True
                        CT1 = Dom_Abitative_1_1.FindControl("cmbA4")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_10"), 0)).Selected = True
                        CT1 = Dom_Abitative_1_1.FindControl("cmbA5")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_11"), 0)).Selected = True
                        CT1.SelectedIndex = -1
                        CT1 = Dom_Abitative_2_1.FindControl("cmbA6")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_12"), 0)).Selected = True
                        CT1 = Dom_Abitative_2_1.FindControl("cmbA7")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_13"), 0)).Selected = True
                        CT1 = Dom_Abitative_2_1.FindControl("cmbA8")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_14"), 0)).Selected = True
                        CT1 = Dom_Abitative_2_1.FindControl("cmbA9")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_PARA_15"), 0)).Selected = True

                        Dim item As ListItem

                        par.cmd.CommandText = "select * from comuni_nazioni where id=" & par.IfNull(myReader5("ID_LUOGO_REC_DNTE"), 0)
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                        If myReader1.Read Then
                            par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbProvRec", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")

                            CT1 = Dom_Richiedente1.FindControl("cmbProvRec")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByValue(par.IfNull(myReader1("SIGLA"), 0)).Selected = True
                            item = CT1.SelectedItem

                            par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneRec", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")

                            CT1 = Dom_Richiedente1.FindControl("cmbComuneRec")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(par.IfNull(myReader1("NOME"), "")).Selected = True
                            item = CT1.SelectedItem

                            'cT = Dom_Richiedente1.FindControl("txtCAPRec")
                            'cT.Text = par.IfNull(myReader1("CAP"), "")

                        End If
                        myReader1.Close()

                        CT1 = Dom_Richiedente1.FindControl("cmbTipoIRec")
                        CT1.Items.FindByValue(par.IfNull(myReader5("ID_TIPO_IND_REC_DNTE"), "")).Selected = True

                        Select Case par.IfNull(myReader5("PERIODO_RES"), 0)
                            Case 0
                                VALORE_R = 0
                            Case 1
                                VALORE_R = 0
                            Case 2
                                VALORE_R = 40
                            Case 3
                                VALORE_R = 85
                            Case 4
                                VALORE_R = 0
                        End Select
                        VALORE_R = (VALORE_R / 100) * 0.3


                        txtIndici.Text = "V1=" & par.IfNull(myReader5("isbar"), 0) _
                                   & "&V2=" & par.IfNull(myReader5("isbarc"), 0) _
                                   & "&V3=" & par.IfNull(myReader5("isbarc_r"), 0) _
                                   & "&V4=" & Format(par.IfNull(myReader5("disagio_a"), 0), "##,##0.000000000000000") _
                                   & "&V5=" & Format(par.IfNull(myReader5("disagio_e"), 0), "##,##0.000000000000000") _
                                   & "&V6=" & Format(par.IfNull(myReader5("disagio_f"), 0), "##,##0.000000000000000") _
                                   & "&V7=" & par.Converti(par.IfNull(myReader5("reddito_isee"), 0)) _
                                   & "&V8=" & par.Converti(par.IfNull(myReader5("ISR_ERP"), 0)) _
                                   & "&V9=" & par.Converti(par.IfNull(myReader5("ISP_ERP"), 0)) _
                                   & "&V10=" & par.Converti(par.IfNull(myReader5("ISe_ERP"), 0)) _
                                   & "&V11=" & Format(VALORE_R, "##,##0.000000000000000") _
                                   & "&V12=" & par.Converti(par.IfNull(myReader5("PSE"), 0)) _
                                   & "&V13=" & par.Converti(par.IfNull(myReader5("VSE"), 0)) _
                                   & "&PG=" & lblPG.Text & "&UJ=3"

                    End If
                    myReader5.Close()

                    par.cmd.CommandText = "SELECT TIPO_BANDO,DATA_INIZIO FROM BANDI WHERE ID=" & lIndice_Bando
                    myReader5 = par.cmd.ExecuteReader()
                    If myReader5.Read() Then
                        lBando = par.IfNull(myReader5("TIPO_BANDO"), "-1")
                        Select Case lBando
                            Case 0
                                lblSPG.Text = Mid(myReader5("DATA_INIZIO"), 3, 2) & "-1"
                            Case 1
                                lblSPG.Text = Mid(myReader5("DATA_INIZIO"), 3, 2) & "-2"
                            Case 2
                                lblSPG.Text = Mid(myReader5("DATA_INIZIO"), 3, 2) & "-3"
                        End Select
                    End If
                    myReader5.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND PROGR=" & lProgr
                    myReader5 = par.cmd.ExecuteReader()
                    Dim sPresso As String
                    sPresso = ""

                    If myReader5.Read() Then
                        cT = Dom_Richiedente1.FindControl("txtCognome")
                        cT.Text = par.IfNull(myReader5("COGNOME"), "")

                        cT = Dom_Richiedente1.FindControl("txtNome")
                        cT.Text = par.IfNull(myReader5("NOME"), "")

                        cT = Dom_Richiedente1.FindControl("txtDataNascita")
                        cT.Text = Mid(par.IfNull(myReader5("DATA_NASCITA"), ""), 7, 2) & "/" & Mid(par.IfNull(myReader5("DATA_NASCITA"), ""), 5, 2) & "/" & Mid(par.IfNull(myReader5("DATA_NASCITA"), ""), 1, 4)

                        cT = Dom_Richiedente1.FindControl("txtCF")
                        cT.Text = par.IfNull(myReader5("COD_FISCALE"), "")

                        CT1 = Dom_Richiedente1.FindControl("cmbSesso")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(par.IfNull(myReader5("SESSO"), "M")).Selected = True

                        lIndice_Componente = myReader5("ID")

                        If lProgr <> 0 Then
                            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD='" & Mid(cT.Text, 12, 4) & "'"
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read() Then
                                CT1 = Dom_Richiedente1.FindControl("cmbNazioneNas")
                                If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                                    CT1.SelectedIndex = -1
                                    CT1.Items.FindByText(myReader1("NOME")).Selected = True
                                Else
                                    CT1.SelectedIndex = -1
                                    CT1.Items.FindByText("ITALIA").Selected = True
                                    CT1 = Dom_Richiedente1.FindControl("cmbPrNas")
                                    CT1.SelectedIndex = -1
                                    CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                                    par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                                    CT1 = Dom_Richiedente1.FindControl("cmbComuneNas")
                                    CT1.SelectedIndex = -1
                                    CT1.Items.FindByText(myReader1("NOME")).Selected = True
                                End If
                            End If
                            myReader1.Close()
                        End If

                    End If
                    myReader5.Close()

                    par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI WHERE ID=" & lIdDichiarazione
                    myReader5 = par.cmd.ExecuteReader()
                    If myReader5.Read() Then
                        lblPGDic.Text = lblSPG.Text & "-" & myReader5("PG") & "-F205"
                        iNumComponenti = myReader5("N_COMP_NUCLEO") - 1



                        If lProgr = 0 Then
                            cT = Dom_Richiedente1.FindControl("txtIndRes")
                            cT.Text = par.IfNull(myReader5("IND_RES_DNTE"), "")

                            cT = Dom_Richiedente1.FindControl("txtCivicoRes")
                            cT.Text = par.IfNull(myReader5("CIVICO_RES_DNTE"), "")

                            cT = Dom_Richiedente1.FindControl("txtTelRes")
                            cT.Text = par.IfNull(myReader5("TELEFONO_DNTE"), "")

                            cT = Dom_Richiedente1.FindControl("txtCAPRes")
                            cT.Text = par.IfNull(myReader5("CAP_RES_DNTE"), "")

                            lIndiceAppoggio_0 = myReader5("ID_LUOGO_NAS_DNTE")
                            lIndiceAppoggio_1 = myReader5("ID_LUOGO_RES_DNTE")
                            lIndiceAppoggio_2 = myReader5("ID_TIPO_IND_RES_DNTE")

                        Else
                            par.cmd.CommandText = "SELECT * FROM COMP_NAS_RES WHERE ID_COMPONENTE=" & lIndice_Componente
                            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader2.Read Then


                                lIndiceAppoggio_1 = myReader2("ID_LUOGO_RES_DNTE")
                                lIndiceAppoggio_2 = myReader2("ID_TIPO_IND_RES_DNTE")

                                CType(Dom_Richiedente1.FindControl("txtIndRes"), TextBox).Text = par.IfNull(myReader2("IND_RES_DNTE"), "")
                                CType(Dom_Richiedente1.FindControl("txtCivicoRes"), TextBox).Text = par.IfNull(myReader2("CIVICO_RES_DNTE"), "")

                                CType(Dom_Richiedente1.FindControl("txtTelRes"), TextBox).Text = par.IfNull(myReader2("TELEFONO_DNTE"), "")

                                cT = Dom_Richiedente1.FindControl("txtCAPRes")
                                cT.Text = par.IfNull(myReader2("CAP_RES"), "")

                                CT1 = Dom_Richiedente1.FindControl("cmbTipoIRes")
                                CT1.SelectedIndex = -1
                                CT1.Items.FindByValue(par.IfNull(myReader2("ID_TIPO_IND_RES_DNTE"), 6)).Selected = True

                            End If
                            myReader2.Close()
                        End If
                    End If
                    myReader5.Close()
                    If lProgr = 0 Then
                        par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
                        myReader5 = par.cmd.ExecuteReader()
                        If myReader5.Read() Then
                            CT1 = Dom_Richiedente1.FindControl("cmbNazioneNas")
                            CT1.SelectedIndex = -1
                            If myReader5("SIGLA") = "E" Or myReader5("SIGLA") = "C" Then
                                CT1.Items.FindByText(myReader5("NOME")).Selected = True
                            Else
                                CT1.Items.FindByText("ITALIA").Selected = True
                                CT1 = Dom_Richiedente1.FindControl("cmbPrNas")
                                CT1.SelectedIndex = -1
                                CT1.Items.FindByText(myReader5("SIGLA")).Selected = True
                                par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader5("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                                CT1 = Dom_Richiedente1.FindControl("cmbComuneNas")
                                CT1.SelectedIndex = -1
                                CT1.Items.FindByText(myReader5("NOME")).Selected = True
                            End If
                        End If
                        myReader5.Close()

                        par.cmd.CommandText = "SELECT DESCRIZIONE FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2

                        myReader5 = par.cmd.ExecuteReader()
                        If myReader5.Read() Then
                            CT1 = Dom_Richiedente1.FindControl("cmbTipoIRes")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader5("DESCRIZIONE")).Selected = True
                        End If
                        myReader5.Close()
                    End If

                    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
                    myReader5 = par.cmd.ExecuteReader()
                    If myReader5.Read() Then
                        CT1 = Dom_Richiedente1.FindControl("cmbNazioneRes")
                        CT1.SelectedIndex = -1
                        If myReader5("SIGLA") = "E" Or myReader5("SIGLA") = "C" Then
                            CT1.Items.FindByText(myReader5("NOME")).Selected = True
                        Else
                            CT1.Items.FindByText("ITALIA").Selected = True
                            CT1 = Dom_Richiedente1.FindControl("cmbPrRes")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader5("SIGLA")).Selected = True
                            par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader5("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                            CT1 = Dom_Richiedente1.FindControl("cmbComuneRes")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader5("NOME")).Selected = True

                            'cT = Dom_Richiedente1.FindControl("txtCAPRes")
                            'cT.Text = myReader("CAP")

                        End If
                    End If
                    myReader5.Close()

                    par.cmd.CommandText = "SELECT F_RESIDENZA FROM COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & lIndice_Componente
                    myReader5 = par.cmd.ExecuteReader()
                    If myReader5.Read() Then
                        Fl_Residenza = myReader5("F_RESIDENZA")
                    End If
                    myReader5.Close()
                    If Fl_Residenza = "1" Then
                        CType(Dom_Abitative_2_1.FindControl("alert2"), Image).Visible = True
                        CType(Dom_Abitative_2_1.FindControl("alert3"), Image).Visible = True
                    End If

                    '09/11/2018
                    par.cmd.CommandText = "select bandi_graduatoria_def.*,(Select upper(descrizione) from tab_stati where cod=domande_bando.id_stato) as stato_dom from bandi_graduatoria_def," _
                        & " domande_bando where bandi_graduatoria_def.id_domanda(+)=domande_bando.id and " _
                        & " domande_bando.id=" & lIdDomanda
                    Dim myrec2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myrec2.Read Then
                        'lblStatoDomanda.Text = par.IfNull(myrec2("stato_dom"), "")
                        lblPunteggio.Text = "Posizione n. " & myrec2("posizione")
                    End If
                    myrec2.Close()


                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("SELECT TO_CHAR(TO_DATE(DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_NASCITA"",PERC_INVAL,INDENNITA_ACC FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " ORDER BY PROGR ASC", par.OracleConn)

                    Dim ds As New Data.DataSet()
                    da.Fill(ds, "COMP_NUCLEO")
                    DataGrid1.DataSource = ds
                    DataGrid1.DataBind()
                    da.Dispose()
                    ds.Dispose()

                    Dom_Richiedente1.DisattivaRichiedente()
                    If lProgr = 0 Then
                        Dom_Richiedente1.DisattivaIndirizzo()
                    End If

                    Dim S As String


                    S = ""
                    If VerificaDati(S) = False Then
                        'Response.Write("<script>alert('Attenzione...Sono state riscontrate anomalie nella domanda. Risolvere il/i problema/i prima di salvare e stampare la domanda!')</script>")
                        'imgStampa.Enabled = False
                        'imgStampa.ImageUrl = "IMG\elaborastampaNO.gif"
                    Else
                        'imgStampa.Enabled = True
                        'imgStampa.ImageUrl = "IMG\elaborastampa.gif"
                    End If
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                    imgStampa.Enabled = False
                    imgStampa.Visible = False
                    btnSalva.Enabled = False
                    btnSalva.Visible = False
                    'Label5.Visible = False
                    'Label6.Visible = False
                Else
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

            Catch ex As Exception
                Label10.Text = ex.ToString
                par.cmd.Dispose()
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            End Try
        End Function


        Public Property AnnoIsee() As String
            Get
                If Not (ViewState("par_AnnoIsee") Is Nothing) Then
                    Return CStr(ViewState("par_AnnoIsee"))
                Else
                    Return ""
                End If
            End Get

            Set(ByVal value As String)
                ViewState("par_AnnoIsee") = value
            End Set

        End Property


        Private Function NuovaDomanda()
            Dim lIndiceAppoggio_0 As Long
            Dim lIndiceAppoggio_1 As Long
            Dim lIndiceAppoggio_2 As Long
            Dim CT1 As DropDownList
            Dim LB1 As Label
            Dim cT As TextBox



            Try
                If Session.Item("ANAGRAFE") = "0" Then
                    imgAnagrafe.Visible = False
                    imgAnagrafe.Attributes.Clear()
                Else
                    imgAnagrafe.Visible = True
                End If


                par.OracleConn.Open()
                par.SettaCommand(par)
                HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
                par.cmd.CommandText = "SELECT ANNO_ISEE,ANNO_CANONE,ID,TIPO_BANDO,DATA_INIZIO FROM BANDI WHERE STATO=1 order by id desc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    If myReader.HasRows = False Then
                        If lIdDichiarazione = -1 Then
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Session.Item("LAVORAZIONE") = "0"
                            Response.Write("<script>alert('NESSUN BANDO APERTO. Non è possibile inserire nuove domande!');history.go(-1);</script>")
                        Else
                            myReader.Close()
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Response.Write("<script>alert('Il Bando a cui appartiene questa domanda è CHIUSO. Per apportare modifiche utilizzare le funzioni di INTEGRAZIONE!');</script>")
                        End If
                        Exit Function
                    Else
                        sAnnoIsee = myReader(0)
                        sAnnoCanone = myReader(1)
                        lIndice_Bando = myReader(2)
                        lBando = par.IfNull(myReader("TIPO_BANDO"), "-1")
                        Select Case lBando
                            Case 0
                                lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-1"
                            Case 1
                                lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-2"
                            Case 2
                                lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-3"
                        End Select
                    End If
                Else
                    If lIdDichiarazione = -1 Then
                        myReader.Close()
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Session.Item("LAVORAZIONE") = "0"
                        Response.Write("<script>alert('NESSUN BANDO APERTO. Non è possibile inserire nuove domande!');history.go(-1);</script>")
                    Else
                        myReader.Close()
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Write("<script>alert('Il Bando a cui appartiene questa domanda è CHIUSO. Per apportare modifiche utilizzare le funzioni di INTEGRAZIONE!');</script>")
                    End If
                    Exit Function
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT MAX(COD) FROM T_TIPO_PRATICHE WHERE ID_SEZIONE=10"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    lBando = par.IfNull(myReader(0), "-1")
                End If
                myReader.Close()

                cT = Dom_Abitative_2_1.FindControl("txtAnnoCanone")
                cT.Text = par.IfNull(sAnnoCanone, "")

                cT = Dom_Abitative_2_1.FindControl("txtAnnoAcc")
                cT.Text = par.IfNull(sAnnoCanone, "")

                cT = Dom_Abitative_2_1.FindControl("txtSpeseAcc")
                cT.Text = "0"

                cT = Dom_Abitative_2_1.FindControl("txtSpese")
                cT.Text = "0"


                par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbNazioneNas", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
                par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbNazioneRes", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
                par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
                par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
                par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbProvRec", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
                par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbTipoIRes", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
                par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbTipoIRec", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
                par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbResidenza", "SELECT DESCRIZIONE,ID FROM T_RESIDENZA_LOMBARDIA ORDER BY ID ASC", "DESCRIZIONE", "ID")
                Dim lsiFrutto As New ListItem(" ", "-1")

                CT1 = Dom_Richiedente1.FindControl("cmbResidenza")
                CT1.Items.Add(lsiFrutto)
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(-1).Selected = True

                par.RiempiDList(Dom_Dichiara1, par.OracleConn, "cmbPresentaD", "SELECT DESCRIZIONE,COD FROM T_CAUSALI_DOMANDA ORDER BY COD ASC", "DESCRIZIONE", "COD")

                par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF1", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=0 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF2", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=1 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF3", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=2 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF4", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=3 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF5", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=4 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF6", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=5 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Familiari1, par.OracleConn, "cmbF7", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=6 order by id asc", "DESCRIZIONE", "ID")


                par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA1", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=7 order by id asc", "DESCRIZIONE", "ID")

                par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA2", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=8 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA3", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=9 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA4", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=10 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Abitative_1_1, par.OracleConn, "cmbA5", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=11 order by id asc", "DESCRIZIONE", "ID")

                par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA6", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=12 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA7", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=13 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA8", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=14 order by id asc", "DESCRIZIONE", "ID")
                par.RiempiDListC(Dom_Abitative_2_1, par.OracleConn, "cmbA9", "select DESCRIZIONE,ID from parametri_bando where id_bando=" & lIndice_Bando & " and tipo_parametro=15 order by id asc", "DESCRIZIONE", "ID")

                Dim item As ListItem

                CT1 = Dom_Richiedente1.FindControl("cmbProvRec")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText("MI").Selected = True
                item = CT1.SelectedItem
                par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneRec", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")

                CT1 = Dom_Richiedente1.FindControl("cmbComuneRec")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText("MILANO").Selected = True
                item = CT1.SelectedItem


                par.cmd.CommandText = "SELECT CAP FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(item.Text) & "'"
                cT = Dom_Richiedente1.FindControl("txtCAPRec")
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    cT.Text = myReader(0)
                End If
                myReader.Close()

                CT1 = Dom_Richiedente1.FindControl("cmbTipoIRec")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText("VIA").Selected = True

                txtDataPG.Text = Format(Now(), "dd/MM/yyyy")
                lblISBAR.Text = "0,000"

                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI WHERE ID=" & lIdDichiarazione
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    lblPGDic.Text = lblSPG.Text & "-" & myReader("PG") & "-F205"
                    iNumComponenti = myReader("N_COMP_NUCLEO") - 1

                    If lProgr = 0 Then

                        cT = Dom_Richiedente1.FindControl("txtIndRes")
                        cT.Text = par.IfNull(myReader("IND_RES_DNTE"), "")

                        cT = Dom_Richiedente1.FindControl("txtCivicoRes")
                        cT.Text = par.IfNull(myReader("CIVICO_RES_DNTE"), "")

                        cT = Dom_Richiedente1.FindControl("txtTelRes")
                        cT.Text = par.IfNull(myReader("TELEFONO_DNTE"), "")

                        cT = Dom_Richiedente1.FindControl("txtCAPRes")
                        cT.Text = par.IfNull(myReader("CAP_RES_DNTE"), "")


                        cT = Dom_Richiedente1.FindControl("txtIndirizzoRec")
                        cT.Text = par.IfNull(myReader("IND_RES_DNTE"), "")

                        cT = Dom_Richiedente1.FindControl("txtCivicoRec")
                        cT.Text = par.IfNull(myReader("CIVICO_RES_DNTE"), "")

                        cT = Dom_Richiedente1.FindControl("txtTelRec")
                        cT.Text = par.IfNull(myReader("TELEFONO_DNTE"), "")

                        cT = Dom_Richiedente1.FindControl("txtCAPRec")
                        cT.Text = par.IfNull(myReader("CAP_RES_DNTE"), "")

                        lIndiceAppoggio_0 = myReader("ID_LUOGO_NAS_DNTE")
                        lIndiceAppoggio_1 = myReader("ID_LUOGO_RES_DNTE")
                        lIndiceAppoggio_2 = myReader("ID_TIPO_IND_RES_DNTE")
                    Else

                        cT = Dom_Richiedente1.FindControl("txtIndRes")
                        cT.Text = ""

                        cT = Dom_Richiedente1.FindControl("txtCivicoRes")
                        cT.Text = ""

                        cT = Dom_Richiedente1.FindControl("txtTelRes")
                        cT.Text = ""

                        lIndiceAppoggio_1 = 4415 'MILANO
                        lIndiceAppoggio_2 = 6 'VIA
                    End If
                End If
                myReader.Close()

                If lProgr = 0 Then
                    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        CT1 = Dom_Richiedente1.FindControl("cmbNazioneNas")
                        If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader("NOME")).Selected = True
                            LB1 = Dom_Richiedente1.FindControl("Label6")
                            LB1.Visible = False
                            LB1 = Dom_Richiedente1.FindControl("Label7")
                            LB1.Visible = False
                            CT1 = Dom_Richiedente1.FindControl("cmbPrNas")
                            CT1.Visible = False
                            CT1 = Dom_Richiedente1.FindControl("cmbComuneNas")
                            CT1.Visible = False
                        Else
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText("ITALIA").Selected = True
                            CT1 = Dom_Richiedente1.FindControl("cmbPrNas")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                            par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                            CT1 = Dom_Richiedente1.FindControl("cmbComuneNas")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader("NOME")).Selected = True
                        End If
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        CT1 = Dom_Richiedente1.FindControl("cmbTipoIRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader("DESCRIZIONE")).Selected = True
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        CT1 = Dom_Richiedente1.FindControl("cmbTipoIRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader("DESCRIZIONE")).Selected = True
                    End If
                    myReader.Close()

                End If
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    CT1 = Dom_Richiedente1.FindControl("cmbNazioneRes")
                    CT1.SelectedIndex = -1
                    If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader("NOME")).Selected = True
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = Dom_Richiedente1.FindControl("cmbPrRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                        par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = Dom_Richiedente1.FindControl("cmbComuneRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader("NOME")).Selected = True

                        'cT = Dom_Richiedente1.FindControl("txtCAPRes")
                        'cT.Text = myReader("CAP")
                    End If
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT DESCRIZIONE FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2

                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    CT1 = Dom_Richiedente1.FindControl("cmbTipoIRes")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("DESCRIZIONE")).Selected = True
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND PROGR=" & lProgr
                myReader = par.cmd.ExecuteReader()
                Dim sPresso As String
                sPresso = ""

                If myReader.Read() Then
                    cT = Dom_Richiedente1.FindControl("txtCognome")
                    cT.Text = par.IfNull(myReader("COGNOME"), "")
                    sPresso = cT.Text
                    cT = Dom_Richiedente1.FindControl("txtNome")
                    cT.Text = par.IfNull(myReader("NOME"), "")
                    sPresso = sPresso & " " & cT.Text
                    cT = Dom_Richiedente1.FindControl("txtDataNascita")
                    cT.Text = Mid(par.IfNull(myReader("DATA_NASCITA"), ""), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_NASCITA"), ""), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_NASCITA"), ""), 1, 4)

                    cT = Dom_Richiedente1.FindControl("txtCF")
                    cT.Text = par.IfNull(myReader("COD_FISCALE"), "")

                    If lProgr <> 0 Then

                        par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD='" & Mid(cT.Text, 12, 4) & "'"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read() Then
                            CT1 = Dom_Richiedente1.FindControl("cmbNazioneNas")
                            If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                                CT1.SelectedIndex = -1
                                CT1.Items.FindByText(myReader1("NOME")).Selected = True
                                CT1 = Dom_Richiedente1.FindControl("cmbPrNas")
                                CT1.Visible = False
                                CT1 = Dom_Richiedente1.FindControl("cmbComuneNas")
                                CT1.Visible = False
                                LB1 = Dom_Richiedente1.FindControl("Label6")
                                LB1.Visible = False
                                LB1 = Dom_Richiedente1.FindControl("Label7")
                                LB1.Visible = False
                            Else
                                CT1.SelectedIndex = -1
                                CT1.Items.FindByText("ITALIA").Selected = True
                                CT1 = Dom_Richiedente1.FindControl("cmbPrNas")
                                CT1.SelectedIndex = -1
                                CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                                par.RiempiDList(Dom_Richiedente1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                                CT1 = Dom_Richiedente1.FindControl("cmbComuneNas")
                                CT1.SelectedIndex = -1
                                CT1.Items.FindByText(myReader1("NOME")).Selected = True
                            End If
                        End If
                        myReader1.Close()
                    End If

                    CT1 = Dom_Richiedente1.FindControl("cmbSesso")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("SESSO")).Selected = True




                    lIndice_Componente = myReader("ID")

                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT F_RESIDENZA FROM COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & lIndice_Componente
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    Fl_Residenza = myReader("F_RESIDENZA")
                End If
                myReader.Close()
                If Fl_Residenza = "1" Then
                    CType(Dom_Abitative_2_1.FindControl("alert2"), Image).Visible = True
                    CType(Dom_Abitative_2_1.FindControl("alert3"), Image).Visible = True
                End If

                cT = Dom_Richiedente1.FindControl("txtPresso")
                cT.Text = sPresso

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("SELECT TO_CHAR(TO_DATE(DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_NASCITA"",PERC_INVAL,INDENNITA_ACC FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " ORDER BY PROGR ASC", par.OracleConn)

                Dim ds As New Data.DataSet()
                da.Fill(ds, "COMP_NUCLEO")
                DataGrid1.DataSource = ds
                DataGrid1.DataBind()
                da.Dispose()
                ds.Dispose()

                par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    lValoreCorrente = myReader(0) + 1
                End If
                myReader.Close()
                par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI VALUES (" & lValoreCorrente & ")"
                par.cmd.ExecuteNonQuery()
                lblPG.Text = Format(lValoreCorrente, "0000000000")

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)


                par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO (ID,FL_PRATICA_CHIUSA,ID_STATO,N_DISTINTA,FL_CONFERMA_SCARICO,TIPO_PRATICA) VALUES (SEQ_DOMANDE_BANDO.NEXTVAL,'0','1',0,'0'," & lBando & ")"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "SELECT SEQ_DOMANDE_BANDO.CURRVAL FROM DUAL"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    lIdDomanda = myReader(0)
                End If
                myReader.Close()
                lblPG.ToolTip = lIdDomanda
                If lProgr <> 0 Then
                    par.cmd.CommandText = "INSERT INTO COMP_NAS_RES (ID_COMPONENTE) VALUES (" & lIndice_Componente & ")"
                    par.cmd.ExecuteNonQuery()
                End If

                Dom_Richiedente1.DisattivaRichiedente()
                If lProgr = 0 Then
                    Dom_Richiedente1.DisattivaIndirizzo()
                End If
                Session.Add("LAVORAZIONE", "1")

                If Session.Item("ANAGRAFE") = "1" Then
                    'imgAnagrafe.Attributes.Clear()
                    imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('Anagrafe.aspx?ID=" & par.criptamolto(lIdDichiarazione) & "&T=0','Anagrafe','top=0,left=0,width=600,height=400');")
                End If

            Catch EX As Exception
                par.SegnalaErrore(EX.ToString)

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Item("LAVORAZIONE") = "0"
                'Label5.Text = EX.ToString
            Finally
                If Not par.OracleConn Is Nothing Then
                    'par.OracleConn.Close()
                    'par.OracleConn.Dispose()
                End If
            End Try
        End Function


        Private Sub txtDataPG_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDataPG.TextChanged

            'txtDataPG.Text = Trim(Left(txtDataPG.Text, 10))
            'lbltxtdata.Text = ""
            'If Len(txtDataPG.Text) = 0 Then
            '    lbltxtdata.Text = "*"
            'ElseIf Not par.ControllaData(txtDataPG) Then
            '    lbltxtdata.Text = "formato: GG/MM/AAAA oppure GGMMAAAA"
            'Else
            '    par.SetFocusControl(Page, "Dom_Richiedente1_cmbResidenza")
            'End If

        End Sub



        Private Sub btnDichiara_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDichiara.Click

            'Dom_Dichiara1.Visible = True
            'btnDichiara.ImageUrl = "p_menu\DICH_1.gif"

            'Select Case iTab
            '    Case 0
            '        Dom_Richiedente1.Visible = False
            '        btnRichiedente.ImageUrl = "p_menu\RICH_0.gif"
            '    Case 1

            '    Case 2
            '        Dom_Familiari1.Visible = False
            '        btnFamiliari.ImageUrl = "p_menu\FAM_0.gif"
            '    Case 3
            '        Dom_Abitative_1_1.Visible = False
            '        btnAbitative1.ImageUrl = "p_menu\ABIT1_0.gif"
            '    Case 4
            '        Dom_Abitative_2_1.Visible = False
            '        btnAbitative2.ImageUrl = "p_menu\ABIT2_0.gif"
            '    Case 5
            '        Dom_Requisiti1.Visible = False
            '        btnRequisiti.ImageUrl = "p_menu\REC_0.gif"
            '    Case 6
            '        Note1.Visible = False
            '        btnNote.ImageUrl = "p_menu\NOTE_0.gif"
            'End Select

            'iTab = 1

        End Sub

        Private Sub btnRichiedente_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRichiedente.Click

            'Dom_Richiedente1.Visible = True
            'btnRichiedente.ImageUrl = "p_menu\RICH_1.gif"

            'Select Case iTab
            '    Case 0

            '    Case 1
            '        Dom_Dichiara1.Visible = False
            '        btnDichiara.ImageUrl = "p_menu\DICH_0.gif"
            '    Case 2
            '        Dom_Familiari1.Visible = False
            '        btnFamiliari.ImageUrl = "p_menu\FAM_0.gif"
            '    Case 3
            '        Dom_Abitative_1_1.Visible = False
            '        btnAbitative1.ImageUrl = "p_menu\ABIT1_0.gif"
            '    Case 4
            '        Dom_Abitative_2_1.Visible = False
            '        btnAbitative2.ImageUrl = "p_menu\ABIT2_0.gif"
            '    Case 5
            '        Dom_Requisiti1.Visible = False
            '        btnRequisiti.ImageUrl = "p_menu\REC_0.gif"
            '    Case 6
            '        Note1.Visible = False
            '        btnNote.ImageUrl = "p_menu\NOTE_0.gif"
            'End Select

            'iTab = 0

        End Sub

        Private Sub btnFamiliari_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnFamiliari.Click
            'Dom_Familiari1.Visible = True
            'btnFamiliari.ImageUrl = "p_menu\FAM_1.gif"

            'Select Case iTab
            '    Case 0
            '        Dom_Richiedente1.Visible = False
            '        btnRichiedente.ImageUrl = "p_menu\RICH_0.gif"
            '    Case 1
            '        Dom_Dichiara1.Visible = False
            '        btnDichiara.ImageUrl = "p_menu\DICH_0.gif"
            '    Case 2

            '    Case 3
            '        Dom_Abitative_1_1.Visible = False
            '        btnAbitative1.ImageUrl = "p_menu\ABIT1_0.gif"
            '    Case 4
            '        Dom_Abitative_2_1.Visible = False
            '        btnAbitative2.ImageUrl = "p_menu\ABIT2_0.gif"
            '    Case 5
            '        Dom_Requisiti1.Visible = False
            '        btnRequisiti.ImageUrl = "p_menu\REC_0.gif"
            '    Case 6
            '        Note1.Visible = False
            '        btnNote.ImageUrl = "p_menu\NOTE_0.gif"
            'End Select

            'iTab = 2

        End Sub

        Private Sub btnAbitative1_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAbitative1.Click

            'Dom_Abitative_1_1.Visible = True
            'btnAbitative1.ImageUrl = "p_menu\ABIT1_1.gif"

            'Select Case iTab
            '    Case 0
            '        Dom_Richiedente1.Visible = False
            '        btnRichiedente.ImageUrl = "p_menu\RICH_0.gif"
            '    Case 1
            '        Dom_Dichiara1.Visible = False
            '        btnDichiara.ImageUrl = "p_menu\DICH_0.gif"
            '    Case 2
            '        Dom_Familiari1.Visible = False
            '        btnFamiliari.ImageUrl = "p_menu\FAM_0.gif"
            '    Case 3

            '    Case 4
            '        Dom_Abitative_2_1.Visible = False
            '        btnAbitative2.ImageUrl = "p_menu\ABIT2_0.gif"
            '    Case 5
            '        Dom_Requisiti1.Visible = False
            '        btnRequisiti.ImageUrl = "p_menu\REC_0.gif"
            '    Case 6
            '        Note1.Visible = False
            '        btnNote.ImageUrl = "p_menu\NOTE_0.gif"
            'End Select

            'iTab = 3

        End Sub

        Private Sub btnAbitative2_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAbitative2.Click

            'Dom_Abitative_2_1.Visible = True
            'btnAbitative2.ImageUrl = "p_menu\ABIT2_1.gif"

            'Select Case iTab
            '    Case 0
            '        Dom_Richiedente1.Visible = False
            '        btnRichiedente.ImageUrl = "p_menu\RICH_0.gif"
            '    Case 1
            '        Dom_Dichiara1.Visible = False
            '        btnDichiara.ImageUrl = "p_menu\DICH_0.gif"
            '    Case 2
            '        Dom_Familiari1.Visible = False
            '        btnFamiliari.ImageUrl = "p_menu\FAM_0.gif"
            '    Case 3
            '        Dom_Abitative_1_1.Visible = False
            '        btnAbitative1.ImageUrl = "p_menu\ABIT1_0.gif"
            '    Case 4

            '    Case 5
            '        Dom_Requisiti1.Visible = False
            '        btnRequisiti.ImageUrl = "p_menu\REC_0.gif"
            '    Case 6
            '        Note1.Visible = False
            '        btnNote.ImageUrl = "p_menu\NOTE_0.gif"
            'End Select

            'iTab = 4


        End Sub

        Private Sub btnNote_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNote.Click

            'Note1.Visible = True
            'btnNote.ImageUrl = "p_menu\NOTE_1.gif"

            'Select Case iTab
            '    Case 0
            '        Dom_Richiedente1.Visible = False
            '        btnRichiedente.ImageUrl = "p_menu\RICH_0.gif"
            '    Case 1
            '        Dom_Dichiara1.Visible = False
            '        btnDichiara.ImageUrl = "p_menu\DICH_0.gif"
            '    Case 2
            '        Dom_Familiari1.Visible = False
            '        btnFamiliari.ImageUrl = "p_menu\FAM_0.gif"
            '    Case 3
            '        Dom_Abitative_1_1.Visible = False
            '        btnAbitative1.ImageUrl = "p_menu\ABIT1_0.gif"
            '    Case 4
            '        Dom_Abitative_2_1.Visible = False
            '        btnAbitative2.ImageUrl = "p_menu\ABIT2_0.gif"
            '    Case 5
            '        Dom_Requisiti1.Visible = False
            '        btnRequisiti.ImageUrl = "p_menu\REC_0.gif"
            '    Case 6

            'End Select

            'iTab = 6



        End Sub

        Public Property FL_VECCHIO_BANDO() As String
            Get
                If Not (ViewState("par_FL_VECCHIO_BANDO") Is Nothing) Then
                    Return CStr(ViewState("par_FL_VECCHIO_BANDO"))
                Else
                    Return "0"
                End If
            End Get

            Set(ByVal value As String)
                ViewState("par_FL_VECCHIO_BANDO") = value
            End Set

        End Property

        Public Property FL_RINNOVO() As String
            Get
                If Not (ViewState("par_FL_RINNOVO") Is Nothing) Then
                    Return CStr(ViewState("par_FL_RINNOVO"))
                Else
                    Return "0"
                End If
            End Get

            Set(ByVal value As String)
                ViewState("par_FL_RINNOVO") = value
            End Set

        End Property

        Public Property Fl_inAssegnazione() As String
            Get
                If Not (ViewState("par_Fl_inAssegnazione") Is Nothing) Then
                    Return CStr(ViewState("par_Fl_inAssegnazione"))
                Else
                    Return ""
                End If
            End Get

            Set(ByVal value As String)
                ViewState("par_Fl_inAssegnazione") = value
            End Set

        End Property

        Public Property Fl_VerificaReq() As String
            Get
                If Not (ViewState("par_Fl_VerificaReq") Is Nothing) Then
                    Return CStr(ViewState("par_Fl_VerificaReq"))
                Else
                    Return ""
                End If
            End Get

            Set(ByVal value As String)
                ViewState("par_Fl_VerificaReq") = value
            End Set

        End Property

        Public Property Fl_US() As String
            Get
                If Not (ViewState("par_US") Is Nothing) Then
                    Return CStr(ViewState("par_US"))
                Else
                    Return ""
                End If
            End Get

            Set(ByVal value As String)
                ViewState("par_US") = value
            End Set

        End Property

        Public Property Fl_DerogaInCorso() As String
            Get
                If Not (ViewState("par_Fl_DerogaInCorso") Is Nothing) Then
                    Return CStr(ViewState("par_Fl_DerogaInCorso"))
                Else
                    Return ""
                End If
            End Get

            Set(ByVal value As String)
                ViewState("par_Fl_DerogaInCorso") = value
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


        Public Property Fl_Residenza() As String
            Get
                If Not (ViewState("par_Residenza") Is Nothing) Then
                    Return CStr(ViewState("par_Residenza"))
                Else
                    Return "0"
                End If
            End Get

            Set(ByVal value As String)
                ViewState("par_Residenza") = value
            End Set

        End Property


        Public Property DeviSalvare() As Boolean
            Get
                If Not (ViewState("par_DeviSalvare") Is Nothing) Then
                    Return CLng(ViewState("par_DeviSalvare"))
                Else
                    Return False
                End If
            End Get

            Set(ByVal value As Boolean)
                ViewState("par_DeviSalvare") = value
            End Set

        End Property


        Public Property bMemorizzato() As Boolean
            Get
                If Not (ViewState("par_bMemorizzato") Is Nothing) Then
                    Return CLng(ViewState("par_bMemorizzato"))
                Else
                    Return False
                End If
            End Get

            Set(ByVal value As Boolean)
                ViewState("par_bMemorizzato") = value
            End Set

        End Property


        Public Property lBando() As Long
            Get
                If Not (ViewState("par_lBando") Is Nothing) Then
                    Return CLng(ViewState("par_lBando"))
                Else
                    Return 0
                End If
            End Get

            Set(ByVal value As Long)
                ViewState("par_lBando") = value
            End Set

        End Property


        Public Property iNumComponenti() As Integer
            Get
                If Not (ViewState("par_componenti") Is Nothing) Then
                    Return CInt(ViewState("par_componenti"))
                Else
                    Return 0
                End If
            End Get

            Set(ByVal value As Integer)
                ViewState("par_componenti") = value
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

        Public Property lProgr() As Integer
            Get
                If Not (ViewState("par_lProgr") Is Nothing) Then
                    Return CInt(ViewState("par_lProgr"))
                Else
                    Return 0
                End If
            End Get

            Set(ByVal value As Integer)
                ViewState("par_lProgr") = value
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

        Public Property lNuovaDomanda() As Long
            Get
                If Not (ViewState("par_lNuovaDomanda") Is Nothing) Then
                    Return CLng(ViewState("par_lNuovaDomanda"))
                Else
                    Return 0
                End If
            End Get

            Set(ByVal value As Long)
                ViewState("par_lNuovaDomanda") = value
            End Set

        End Property


        Public Property lIndice_Componente() As Long
            Get
                If Not (ViewState("par_lIndice_Componente") Is Nothing) Then
                    Return CLng(ViewState("par_lIndice_Componente"))
                Else
                    Return 0
                End If
            End Get

            Set(ByVal value As Long)
                ViewState("par_lIndice_Componente") = value
            End Set

        End Property

        Public Property iTab() As Integer
            Get
                If Not (ViewState("par_itab") Is Nothing) Then
                    Return CInt(ViewState("par_itab"))
                Else
                    Return 0
                End If
            End Get

            Set(ByVal value As Integer)
                ViewState("par_itab") = value
            End Set

        End Property


        Public Property lIndice_Bando() As Long
            Get
                If Not (ViewState("par_lIndice_Bando") Is Nothing) Then
                    Return CLng(ViewState("par_lIndice_Bando"))
                Else
                    Return 0
                End If
            End Get

            Set(ByVal value As Long)
                ViewState("par_lIndice_Bando") = value
            End Set

        End Property

        Private Function VerificaDati(ByRef S As String) As Boolean
            Dim i As Integer
            Dim adulti As Integer
            Dim TRENTENNI As Integer
            Dim MINORI As Integer

            Dim VerificaDati1 As Boolean
            Dim VerificaDati2 As Boolean
            Dim VerificaDati3 As Boolean
            Dim VerificaDati4 As Boolean
            Dim VerificaDati5 As Boolean
            Dim VerificaDati6 As Boolean
            Dim VerificaDati7 As Boolean
            Dim VerificaDati8 As Boolean
            Dim VerificaDati9 As Boolean
            Dim VerificaDati01 As Boolean
            Dim VerificaDati02 As Boolean

            Dim VerificaDati10 As Boolean

            VerificaDati1 = True
            VerificaDati01 = True
            VerificaDati02 = True
            VerificaDati2 = True
            VerificaDati3 = True
            VerificaDati4 = True
            VerificaDati5 = True
            VerificaDati6 = True
            VerificaDati7 = True
            VerificaDati8 = True
            VerificaDati9 = True
            VerificaDati10 = True
            VerificaDati = True

            If lProgr <> 0 Then
                If CType(Dom_Richiedente1.FindControl("txtIndRes"), TextBox).Text = "" Then
                    VerificaDati10 = False
                End If
                If CType(Dom_Richiedente1.FindControl("txtCivicoRes"), TextBox).Text = "" Then
                    VerificaDati10 = False
                End If
            End If

            'If CType(Dom_Dichiara1.FindControl("cf1"), CheckBox).Checked = True And iNumComponenti = 1 Then
            '    CType(Dom_Dichiara1.FindControl("alert1"), Image).Visible = True
            '    VerificaDati01 = False
            '    S = "TAB DICHIARA - VOCE 1 non consentita!"
            'Else
            '    CType(Dom_Dichiara1.FindControl("alert1"), Image).Visible = False
            'End If

            If Fl_Residenza = "1" Then
                If CType(Dom_Abitative_2_1.FindControl("txtSpese"), TextBox).Text <> "0" And CType(Dom_Abitative_2_1.FindControl("txtSpese"), TextBox).Text <> "" Then
                    VerificaDati02 = False
                    S = S & " TAB DICHIARA - Abita in abitazione propria e non è possibile inserire canone di locazione!"
                End If
            Else
                If IsNumeric(CType(Dom_Abitative_2_1.FindControl("txtSpese"), TextBox).Text) = False Then
                    VerificaDati02 = False
                    S = S & " TAB DICHIARA - Il canone di locazione deve essere un valore numerico!"
                End If
                If IsNumeric(CType(Dom_Abitative_2_1.FindControl("txtSpeseAcc"), TextBox).Text) = False Then
                    VerificaDati02 = False
                    S = S & " TAB DICHIARA - Il valore delle spese accessorie deve essere numerico!"
                End If
            End If

            If CType(Dom_Familiari1.FindControl("cmbF1"), DropDownList).SelectedIndex = 0 Then
                If iNumComponenti > 1 Then
                    CType(Dom_Familiari1.FindControl("alert4"), Image).Visible = True
                    VerificaDati1 = False
                    S = S & " TAB FAMILIARI - VOCE 1 Massimo due componenti consentiti nel nucleo!"
                Else
                    If iNumComponenti = 0 Then
                        CType(Dom_Familiari1.FindControl("alert4"), Image).Visible = True
                        VerificaDati1 = False
                        S = S & " TAB FAMILIARI - VOCE 1 Massimo due componenti consentiti nel nucleo!"
                    Else
                        If DateDiff("m", DateSerial(Mid(DataGrid1.Items(0).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(0).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(0).Cells(0).Text, 1, 2)), Now) / 12 >= 65 Then
                            If DateDiff("m", DateSerial(Mid(DataGrid1.Items(1).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(1).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(1).Cells(0).Text, 1, 2)), Now) / 12 >= 75 Or DataGrid1.Items(1).Cells(1).Text = "100" Then
                                CType(Dom_Familiari1.FindControl("alert4"), Image).Visible = False
                            Else
                                VerificaDati1 = False
                                CType(Dom_Familiari1.FindControl("alert4"), Image).Visible = True
                                S = S & " TAB FAMILIARI - VOCE 1 Valore non congruo con quanto specificato nel nucleo familiare!"
                            End If
                        Else
                            If DateDiff("m", DateSerial(Mid(DataGrid1.Items(1).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(1).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(1).Cells(0).Text, 1, 2)), Now) / 12 >= 75 And DataGrid1.Items(0).Cells(1).Text = "100" Then
                                CType(Dom_Familiari1.FindControl("alert4"), Image).Visible = False
                            Else
                                VerificaDati1 = False
                                CType(Dom_Familiari1.FindControl("alert4"), Image).Visible = True
                                S = S & " TAB FAMILIARI - VOCE 1 Valore non congruo con quanto specificato nel nucleo familiare!"
                            End If
                        End If
                    End If
                End If
            End If

            If CType(Dom_Familiari1.FindControl("cmbF1"), DropDownList).SelectedIndex = 1 Then
                If iNumComponenti > 1 Then
                    CType(Dom_Familiari1.FindControl("alert4"), Image).Visible = True
                    VerificaDati1 = False
                    S = S & " TAB FAMILIARI - VOCE 1 Massimo due componenti consentiti nel nucleo!"
                Else
                    If DateDiff("m", DateSerial(Mid(DataGrid1.Items(0).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(0).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(0).Cells(0).Text, 1, 2)), Now) / 12 < 65 Then
                        CType(Dom_Familiari1.FindControl("alert4"), Image).Visible = True
                        VerificaDati1 = False
                        S = S & " TAB FAMILIARI - VOCE 1 Valore non congruo con quanto specificato nel nucleo familiare!"
                    Else
                        If iNumComponenti = 1 Then
                            If DateDiff("m", DateSerial(Mid(DataGrid1.Items(1).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(1).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(1).Cells(0).Text, 1, 2)), Now) / 12 < 65 Then
                                CType(Dom_Familiari1.FindControl("alert4"), Image).Visible = True
                                VerificaDati1 = False
                                S = S & " TAB FAMILIARI - VOCE 1 Valore non congruo con quanto specificato nel nucleo familiare!"
                            Else
                                CType(Dom_Familiari1.FindControl("alert4"), Image).Visible = False
                            End If
                        Else
                            If VerificaDati1 = True Then
                                CType(Dom_Familiari1.FindControl("alert4"), Image).Visible = False
                            Else
                                CType(Dom_Familiari1.FindControl("alert4"), Image).Visible = True
                                VerificaDati1 = False
                                S = S & " TAB FAMILIARI - VOCE 1 Valore non congruo con quanto specificato nel nucleo familiare!"
                            End If
                        End If
                    End If
                End If
            End If
            If CType(Dom_Familiari1.FindControl("cmbF1"), DropDownList).SelectedIndex = 2 Then
                CType(Dom_Familiari1.FindControl("alert4"), Image).Visible = False
                VerificaDati1 = True
            End If

            Select Case CType(Dom_Familiari1.FindControl("cmbF2"), DropDownList).SelectedIndex
                Case 0
                    For i = 0 To DataGrid1.Items.Count - 1
                        If DataGrid1.Items(i).Cells(1).Text = "100" And DataGrid1.Items(i).Cells(2).Text = "1" Then
                            CType(Dom_Familiari1.FindControl("alert5"), Image).Visible = False
                            VerificaDati2 = True
                            Exit For
                        Else
                            VerificaDati2 = False
                            CType(Dom_Familiari1.FindControl("alert5"), Image).Visible = True
                            S = S & " TAB FAMILIARI - VOCE 2 Valore non congruo con quanto specificato nel nucleo familiare!"
                        End If
                    Next i
                Case 1
                    For i = 0 To DataGrid1.Items.Count - 1
                        If DataGrid1.Items(i).Cells(1).Text = "100" Then
                            CType(Dom_Familiari1.FindControl("alert5"), Image).Visible = False
                            VerificaDati2 = True
                            Exit For
                        Else
                            VerificaDati2 = False
                            CType(Dom_Familiari1.FindControl("alert5"), Image).Visible = True
                            S = S & " TAB FAMILIARI - VOCE 2 Valore non congruo con quanto specificato nel nucleo familiare!"
                        End If
                    Next i
                Case 2
                    For i = 0 To DataGrid1.Items.Count - 1
                        If Val(DataGrid1.Items(i).Cells(1).Text) >= 66 And Val(DataGrid1.Items(i).Cells(1).Text) < 100 Then
                            CType(Dom_Familiari1.FindControl("alert5"), Image).Visible = False
                            VerificaDati2 = True
                            Exit For
                        Else
                            VerificaDati2 = False
                            CType(Dom_Familiari1.FindControl("alert5"), Image).Visible = True
                            S = S & " TAB FAMILIARI - VOCE 2 Valore non congruo con quanto specificato nel nucleo familiare!"
                        End If
                    Next i
                Case 3
                    CType(Dom_Familiari1.FindControl("alert5"), Image).Visible = False
                    VerificaDati2 = True
            End Select

            Select Case CType(Dom_Familiari1.FindControl("cmbF3"), DropDownList).SelectedIndex
                Case 0
                    If iNumComponenti < 1 Then
                        VerificaDati3 = False
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 3 Valore non congruo con quanto specificato nel nucleo familiare!"
                        Exit Select
                    Else
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = False
                    End If

                    MINORI = 0
                    For i = 0 To DataGrid1.Items.Count - 1
                        If (DateDiff("m", DateSerial(Mid(DataGrid1.Items(i).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(i).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(i).Cells(0).Text, 1, 2)), Now) / 12) < 18 Then
                            CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = False
                            VerificaDati3 = True
                            MINORI = MINORI + 1
                            'Exit Select
                        Else
                            VerificaDati3 = False
                            CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = True
                            S = S & " TAB FAMILIARI - VOCE 3 Valore non congruo con quanto specificato nel nucleo familiare!"
                        End If
                    Next i
                    If MINORI = 0 Then
                        VerificaDati3 = False
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 3 Valore non congruo con quanto specificato nel nucleo familiare!"
                        Exit Select
                    End If

                    TRENTENNI = 0
                    For i = 0 To DataGrid1.Items.Count - 1
                        If (DateDiff("m", DateSerial(Mid(DataGrid1.Items(i).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(i).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(i).Cells(0).Text, 1, 2)), Now) / 12) >= 18 And (DateDiff("m", DateSerial(Mid(DataGrid1.Items(i).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(i).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(i).Cells(0).Text, 1, 2)), Now) / 12) < 30 Then
                            TRENTENNI = TRENTENNI + 1
                        End If
                    Next i

                    If TRENTENNI >= 1 Then
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = False
                        VerificaDati3 = True
                        'Exit Select
                    Else
                        VerificaDati3 = False
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 3 Valore non congruo con quanto specificato nel nucleo familiare!"
                        Exit Select
                    End If


                Case 1
                    If iNumComponenti < 1 Then
                        VerificaDati3 = False
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 3 Valore non congruo con quanto specificato nel nucleo familiare!"
                        Exit Select
                    Else
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = False
                    End If
                    MINORI = 0
                    For i = 0 To DataGrid1.Items.Count - 1
                        If (DateDiff("m", DateSerial(Mid(DataGrid1.Items(i).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(i).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(i).Cells(0).Text, 1, 2)), Now) / 12) < 18 Then
                            'CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = False
                            'VerificaDati3 = True
                            MINORI = MINORI + 1
                        Else
                            'VerificaDati3 = False
                            'CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = True
                            'S = S & " TAB FAMILIARI - VOCE 3 Valore non congruo con quanto specificato nel nucleo familiare!"
                        End If
                    Next i

                    If MINORI >= 1 Then
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = False
                        VerificaDati3 = True
                    Else
                        VerificaDati3 = False
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 3 Valore non congruo con quanto specificato nel nucleo familiare!"
                    End If

                Case 2
                    If iNumComponenti < 1 Then
                        VerificaDati3 = False
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 3 Valore non congruo con quanto specificato nel nucleo familiare!"
                        Exit Select
                    Else
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = False
                    End If

                    For i = 0 To DataGrid1.Items.Count - 1
                        If (DateDiff("m", DateSerial(Mid(DataGrid1.Items(i).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(i).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(i).Cells(0).Text, 1, 2)), Now) / 12) < 18 Then
                            CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = True
                            VerificaDati3 = False
                            S = S & " TAB FAMILIARI - VOCE 3 Valore non congruo con quanto specificato nel nucleo familiare!"
                            Exit Select
                        Else
                            VerificaDati3 = True
                            CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = False
                        End If
                    Next i

                    TRENTENNI = 0
                    For i = 0 To DataGrid1.Items.Count - 1
                        If (DateDiff("m", DateSerial(Mid(DataGrid1.Items(i).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(i).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(i).Cells(0).Text, 1, 2)), Now) / 12) >= 18 And (DateDiff("m", DateSerial(Mid(DataGrid1.Items(i).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(i).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(i).Cells(0).Text, 1, 2)), Now) / 12) < 30 Then
                            TRENTENNI = TRENTENNI + 1
                        End If
                    Next i

                    If TRENTENNI >= 1 Then
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = False
                        VerificaDati3 = True
                        'Exit Select
                    Else
                        VerificaDati3 = False
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 3 Valore non congruo con quanto specificato nel nucleo familiare!"
                        Exit Select
                    End If

                Case 3
                    If iNumComponenti <> 1 Then
                        VerificaDati3 = False
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 3 Valore non congruo con quanto specificato nel nucleo familiare!"
                        Exit Select
                    Else
                        CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = False
                    End If

                    For i = 0 To DataGrid1.Items.Count - 1
                        If (DateDiff("m", DateSerial(Mid(DataGrid1.Items(i).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(i).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(i).Cells(0).Text, 1, 2)), Now) / 12) < 18 Then
                            VerificaDati3 = False
                            CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = True
                            S = S & " TAB FAMILIARI - VOCE 3 Valore non congruo con quanto specificato nel nucleo familiare!"
                            Exit Select
                        Else
                            CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = False
                            VerificaDati3 = True
                            'Exit Select
                        End If
                    Next i



                Case 4
                    CType(Dom_Familiari1.FindControl("alert6"), Image).Visible = False
                    VerificaDati3 = True
            End Select

            Select Case CType(Dom_Familiari1.FindControl("cmbF4"), DropDownList).SelectedIndex
                Case 0
                    If iNumComponenti = 0 Then
                        VerificaDati4 = False
                        CType(Dom_Familiari1.FindControl("alert7"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 4 Valore non congruo con quanto specificato nel nucleo familiare!"
                        Exit Select
                    End If

                    Dim GRANDI As Integer

                    GRANDI = 0
                    For i = 0 To DataGrid1.Items.Count - 1

                        If (DateDiff("m", DateSerial(Mid(DataGrid1.Items(i).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(i).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(i).Cells(0).Text, 1, 2)), Now) / 12) < 18 Then


                        Else


                            GRANDI = GRANDI + 1
                        End If
                    Next i

                    If GRANDI > 1 Then
                        VerificaDati4 = False
                        CType(Dom_Familiari1.FindControl("alert7"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 4 Valore non congruo con quanto specificato nel nucleo familiare!"
                        Exit Select
                    Else
                        CType(Dom_Familiari1.FindControl("alert7"), Image).Visible = False
                        VerificaDati4 = True
                        'Exit Select
                    End If
                Case 1
                    If iNumComponenti <> 0 Then
                        VerificaDati4 = False
                        CType(Dom_Familiari1.FindControl("alert7"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 4 Valore non congruo con quanto specificato nel nucleo familiare!"
                        Exit Select
                    End If
                Case 2
                    CType(Dom_Familiari1.FindControl("alert7"), Image).Visible = False
                    VerificaDati4 = True
            End Select

            Select Case CType(Dom_Familiari1.FindControl("cmbF5"), DropDownList).SelectedIndex
                Case 0
                    If iNumComponenti = 0 Then
                        VerificaDati5 = False
                        CType(Dom_Familiari1.FindControl("alert8"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 5 Valore non congruo con quanto specificato nel nucleo familiare!"
                        Exit Select
                    Else
                        CType(Dom_Familiari1.FindControl("alert8"), Image).Visible = False
                        VerificaDati5 = True
                    End If
                Case 1
                    For i = 0 To DataGrid1.Items.Count - 1
                        If (DateDiff("m", DateSerial(Mid(DataGrid1.Items(i).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(i).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(i).Cells(0).Text, 1, 2)), Now) / 12) >= 45 Then
                            CType(Dom_Familiari1.FindControl("alert8"), Image).Visible = False
                            VerificaDati5 = True
                            Exit Select
                        Else
                            VerificaDati5 = False
                            CType(Dom_Familiari1.FindControl("alert8"), Image).Visible = True
                            S = S & " TAB FAMILIARI - VOCE 5 Valore non congruo con quanto specificato nel nucleo familiare!"
                            'Exit Select
                        End If
                    Next i
                Case 2
                    For i = 0 To DataGrid1.Items.Count - 1
                        If (DateDiff("m", DateSerial(Mid(DataGrid1.Items(i).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(i).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(i).Cells(0).Text, 1, 2)), Now) / 12) <= 45 Then
                            CType(Dom_Familiari1.FindControl("alert8"), Image).Visible = False
                            VerificaDati5 = True
                            Exit Select
                        Else
                            VerificaDati5 = False
                            CType(Dom_Familiari1.FindControl("alert8"), Image).Visible = True
                            S = S & " TAB FAMILIARI - VOCE 5 Valore non congruo con quanto specificato nel nucleo familiare!"
                            'Exit Select
                        End If
                    Next i
                Case 3
                    CType(Dom_Familiari1.FindControl("alert8"), Image).Visible = False
                    VerificaDati5 = True
            End Select

            Select Case CType(Dom_Familiari1.FindControl("cmbF6"), DropDownList).SelectedIndex
                Case 0
                    For i = 0 To DataGrid1.Items.Count - 1
                        If Val(DataGrid1.Items(i).Cells(1).Text) >= 74 And Val(DataGrid1.Items(i).Cells(1).Text) <= 100 Then
                            CType(Dom_Familiari1.FindControl("alert9"), Image).Visible = False
                            VerificaDati6 = True
                            'Exit Select
                        Else
                            VerificaDati6 = False
                            CType(Dom_Familiari1.FindControl("alert9"), Image).Visible = True
                            S = S & " TAB FAMILIARI - VOCE 6 Valore non congruo con quanto specificato nel nucleo familiare!"
                            Exit Select
                        End If
                    Next i
                Case 1
                    CType(Dom_Familiari1.FindControl("alert9"), Image).Visible = False
                    VerificaDati6 = True
                Case 2
                    CType(Dom_Familiari1.FindControl("alert9"), Image).Visible = False
                    VerificaDati6 = True
            End Select

            Select Case CType(Dom_Familiari1.FindControl("cmbF7"), DropDownList).SelectedIndex
                Case 0
                    adulti = 0
                    For i = 0 To DataGrid1.Items.Count - 1

                        If (DateDiff("m", DateSerial(Mid(DataGrid1.Items(i).Cells(0).Text, 7, 4), Mid(DataGrid1.Items(i).Cells(0).Text, 4, 2), Mid(DataGrid1.Items(i).Cells(0).Text, 1, 2)), Now) / 12) < 18 Then
                        Else
                            adulti = adulti + 1
                        End If
                    Next i
                    If adulti = 1 Then
                        CType(Dom_Familiari1.FindControl("alert10"), Image).Visible = False
                        VerificaDati7 = True
                        Exit Select
                    Else
                        VerificaDati7 = False
                        CType(Dom_Familiari1.FindControl("alert10"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 7 Valore non congruo con quanto specificato nel nucleo familiare!"
                        Exit Select
                    End If

                    For i = 0 To DataGrid1.Items.Count - 1
                        If Val(DataGrid1.Items(i).Cells(1).Text) >= 74 And Val(DataGrid1.Items(i).Cells(1).Text) <= 100 Then
                            CType(Dom_Familiari1.FindControl("alert9"), Image).Visible = False
                            VerificaDati7 = True
                            'Exit Select
                        Else
                            VerificaDati7 = False
                            CType(Dom_Familiari1.FindControl("alert9"), Image).Visible = True
                            S = S & " TAB FAMILIARI - VOCE 6 Valore non congruo con quanto specificato nel nucleo familiare!"
                            Exit Select
                        End If
                    Next i
                Case 1
                    CType(Dom_Familiari1.FindControl("alert10"), Image).Visible = False
                    VerificaDati7 = True
            End Select

            Select Case CType(Dom_Abitative_2_1.FindControl("cmbA7"), DropDownList).SelectedIndex
                Case 0
                    If CType(Dom_Familiari1.FindControl("alert4"), Image).Visible = True And CType(Dom_Familiari1.FindControl("alert5"), Image).Visible = True Then
                        VerificaDati8 = False
                        CType(Dom_Abitative_2_1.FindControl("alert19"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 6 Valore non congruo con quanto specificato nel nucleo familiare!"
                        Exit Select
                    Else
                        If CType(Dom_Familiari1.FindControl("cmbF2"), DropDownList).SelectedItem.Value = -1 And CType(Dom_Familiari1.FindControl("cmbF1"), DropDownList).SelectedItem.Value = -1 Then
                            VerificaDati8 = False
                            CType(Dom_Abitative_2_1.FindControl("alert19"), Image).Visible = True
                            S = S & " TAB FAMILIARI - VOCE 6 Valore non congruo con quanto specificato nel nucleo familiare!"
                            Exit Select
                        Else
                            CType(Dom_Abitative_2_1.FindControl("alert19"), Image).Visible = False
                            VerificaDati8 = True
                        End If
                    End If
                Case 1
                    CType(Dom_Abitative_2_1.FindControl("alert19"), Image).Visible = False
                    VerificaDati8 = True
            End Select

            Select Case CType(Dom_Abitative_2_1.FindControl("cmbA6"), DropDownList).SelectedIndex
                Case 0
                    If CType(Dom_Familiari1.FindControl("alert5"), Image).Visible = True Then
                        VerificaDati9 = False
                        CType(Dom_Abitative_2_1.FindControl("alert18"), Image).Visible = True
                        S = S & " TAB FAMILIARI - VOCE 6 Valore non congruo con quanto specificato nel nucleo familiare!"
                        Exit Select
                    Else

                        If CType(Dom_Familiari1.FindControl("cmbF2"), DropDownList).SelectedItem.Value = -1 Then
                            VerificaDati9 = False
                            CType(Dom_Abitative_2_1.FindControl("alert18"), Image).Visible = True
                            S = S & " TAB FAMILIARI - VOCE 6 Valore non congruo con quanto specificato nel nucleo familiare!"
                            Exit Select
                        Else
                            VerificaDati9 = True
                            CType(Dom_Abitative_2_1.FindControl("alert18"), Image).Visible = False
                            S = S & " TAB FAMILIARI - VOCE 6 Valore non congruo con quanto specificato nel nucleo familiare!"
                        End If
                    End If
                Case 1
                    CType(Dom_Abitative_2_1.FindControl("alert18"), Image).Visible = False
                    VerificaDati9 = True
            End Select

            If VerificaDati10 = False Or VerificaDati9 = False Or VerificaDati8 = False Or VerificaDati01 = False Or VerificaDati02 = False Or VerificaDati1 = False Or VerificaDati2 = False Or VerificaDati3 = False Or VerificaDati4 = False Or VerificaDati5 = False Or VerificaDati6 = False Or VerificaDati7 = False Then
                VerificaDati = False
            Else
                VerificaDati = True
            End If
        End Function


        Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
            Dim S As String

            Try
                bMemorizzato = False

                If CType(Dom_Richiedente1.FindControl("cmbResidenza"), DropDownList).SelectedItem.Value = -1 Then
                    Response.Write("<script>alert('Attenzione...Verificare il campo <Periodo di residenza in Lombardia>. Risolvere il problema prima di salvare e stampare la domanda!')</script>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                    Exit Try
                End If

                If CType(Dom_Dichiara1.FindControl("cmbPresentaD"), DropDownList).SelectedItem.Value = 5 And CType(Dom_Richiedente1.FindControl("cmbResidenza"), DropDownList).SelectedItem.Value <> 4 Then
                    Response.Write("<script>alert('Attenzione...Verificare il campo <Periodo di residenza in Lombardia> e <Motivo Presentazione Domanda>. Se italiano emigrato non può essere residente in Italia! Risolvere il problema prima di salvare e stampare la domanda.')</script>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                    Exit Try
                End If

                'If Valore01(CType(Dom_Abitative_2_1.FindControl("chAO"), CheckBox).Checked) = "1" And CType(Dom_Abitative_1_1.FindControl("cmbA1"), DropDownList).SelectedItem.Value <> -1 Then
                '    Response.Write("<script>alert('Attenzione...Verificare il campo <Rilascio Alloggio> e <Affitto oneroso nei 2 anni pregressi>. Situazione incompatibile. Risolvere il problema prima di salvare e stampare la domanda!')</script>")
                '    imgStampa.Enabled = False
                '    imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                '    Exit Try
                'End If



                If InStr(CType(Dom_Abitative_2_1.FindControl("txtSpese"), TextBox).Text, ".") = 0 Then
                    If InStr(CType(Dom_Abitative_2_1.FindControl("txtSpese"), TextBox).Text, ",") = 0 Then
                    Else
                        Response.Write("<script>alert('Attenzione...Verificare il campo <Canone di Locazione>. Sono ammessi solo valori interi!')</script>")
                        imgStampa.Enabled = False
                        imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                        Exit Try
                    End If
                Else
                    Response.Write("<script>alert('Attenzione...Verificare il campo <Canone di Locazione>. Sono ammessi solo valori interi!')</script>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                    Exit Try
                End If

                If InStr(CType(Dom_Abitative_2_1.FindControl("txtSpeseAcc"), TextBox).Text, ".") = 0 Then
                    If InStr(CType(Dom_Abitative_2_1.FindControl("txtSpeseAcc"), TextBox).Text, ",") = 0 Then
                    Else
                        Response.Write("<script>alert('Attenzione...Verificare il campo <Spese Accessorie>. Sono ammessi solo valori interi!')</script>")
                        imgStampa.Enabled = False
                        imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                        Exit Try
                    End If
                Else
                    Response.Write("<script>alert('Attenzione...Verificare il campo <Spese Accessorie>. Sono ammessi solo valori interi!')</script>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                    Exit Try
                End If

                If Len(CType(Dom_Abitative_2_1.FindControl("txtAnnoCanone"), TextBox).Text) < 4 Then
                    Response.Write("<script>alert('Attenzione...Verificare il campo <Anno canone di locazione> sia valido (yyyy). Anche se il canone è 0, valorizzare comunque!')</script>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                    Exit Try
                End If

                If Len(CType(Dom_Abitative_2_1.FindControl("txtAnnoAcc"), TextBox).Text) < 4 Then
                    Response.Write("<script>alert('Attenzione...Verificare il campo <Anno Spese Accessorie> sia valido (yyyy). Anche se la spesa è 0, valorizzare comunque!')</script>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                    Exit Try
                End If


                If Len(CType(Dom_Richiedente1.FindControl("txtCapRes"), TextBox).Text) < 5 Then
                    Response.Write("<script>alert('Attenzione...Il CAP di Residenza deve essere 5 caratteri! Memorizzazione non effettuata.')</script>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                    Exit Try
                End If

                If Len(CType(Dom_Richiedente1.FindControl("txtCapRec"), TextBox).Text) < 5 Then
                    Response.Write("<script>alert('Attenzione...Il CAP di Recapito deve essere 5 caratteri! Memorizzazione non effettuata.')</script>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                    Exit Try
                End If

                S = ""
                If VerificaDati(S) = False Then
                    Response.Write("<script>alert('Attenzione...Sono state riscontrate anomalie nella domanda. La domanda potra essere comunque salvata e stampata!')</script>")
                    imgStampa.Enabled = True
                    imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                End If

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                End If

                Dim sStringaSql As String


                Dim sNote As String = ""
                If Session.Item("ID_CAF") = "2" Or Session.Item("ID_CAF") = "6" Or Session.Item("ID_CAF") = "61" Then
                    sNote = ",NOTE_WEB='" & par.PulisciStrSql(CType(Note1.FindControl("txtNote"), TextBox).Text) & "'"
                End If
                sStringaSql = "UPDATE DOMANDE_BANDO Set ID_BANDO=" & lIndice_Bando _
                          & ",ID_DICHIARAZIONE=" & lIdDichiarazione & "," _
                          & " PROGR_COMPONENTE=" & lProgr _
                          & ",PRESSO_REC_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtPresso"), TextBox).Text) _
                          & "',ID_LUOGO_REC_DNTE=" & CType(Dom_Richiedente1.FindControl("cmbComuneRec"), DropDownList).SelectedItem.Value _
                          & ",ID_TIPO_IND_REC_DNTE=" & CType(Dom_Richiedente1.FindControl("cmbTipoIRec"), DropDownList).SelectedItem.Value _
                          & ",IND_REC_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtIndirizzoRec"), TextBox).Text) _
                          & "',CAP_REC_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtCAPRec"), TextBox).Text) & "',CIVICO_REC_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtCivicoRec"), TextBox).Text) _
                          & "',TELEFONO_REC_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtTelRec"), TextBox).Text) _
                          & "',PERIODO_RES=" & CType(Dom_Richiedente1.FindControl("cmbResidenza"), DropDownList).SelectedItem.Value _
                          & ",PG='" & lblPG.Text
                sStringaSql = sStringaSql _
                           & "',DATA_PG='" & par.AggiustaData(txtDataPG.Text) _
                           & "',FL_FAM_1='" & Valore01(CType(Dom_Dichiara1.FindControl("cf1"), CheckBox).Checked) _
                           & "',FL_FAM_2='" & Valore01(CType(Dom_Dichiara1.FindControl("cf2"), CheckBox).Checked) _
                           & "',FL_FAM_3='" & Valore01(CType(Dom_Dichiara1.FindControl("cf3"), CheckBox).Checked) _
                           & "',FL_FAM_4='" & Valore01(CType(Dom_Dichiara1.FindControl("cf4"), CheckBox).Checked)
                sStringaSql = sStringaSql _
                           & "',ID_CAUSALE_DOMANDA=" & CType(Dom_Dichiara1.FindControl("cmbPresentaD"), DropDownList).SelectedItem.Value _
                           & ",FL_PROFUGO='" & Valore01(CType(Dom_Dichiara1.FindControl("cfProfugo"), CheckBox).Checked) _
                           & "',ANNO_RIF_CANONE='" & par.PulisciStrSql(CType(Dom_Abitative_2_1.FindControl("txtAnnoCanone"), TextBox).Text) _
                           & "',IMPORTO_CANONE=" & par.PulisciStrSql(CType(Dom_Abitative_2_1.FindControl("txtSpese"), TextBox).Text)
                sStringaSql = sStringaSql _
                           & ",ANNO_RIF_SPESE_ACC='" & par.PulisciStrSql(CType(Dom_Abitative_2_1.FindControl("txtAnnoAcc"), TextBox).Text) _
                           & "',IMPORTO_SPESE_ACC=" & par.PulisciStrSql(CType(Dom_Abitative_2_1.FindControl("txtSpeseAcc"), TextBox).Text) _
                           & ",FL_MOROSITA='" & Valore01(CType(Dom_Abitative_1_1.FindControl("chMorosita"), CheckBox).Checked) _
                           & "'" & sNote & ","
                sStringaSql = sStringaSql _
                           & "REQUISITO1='" & Valore01(CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked) _
                           & "',REQUISITO2='" & Valore01(CType(Dom_Requisiti1.FindControl("chR2"), CheckBox).Checked) _
                           & "',REQUISITO3='" & Valore01(CType(Dom_Requisiti1.FindControl("chR3"), CheckBox).Checked) _
                           & "',REQUISITO4='" & Valore01(CType(Dom_Requisiti1.FindControl("chR4"), CheckBox).Checked) _
                           & "',REQUISITO5='" & Valore01(CType(Dom_Requisiti1.FindControl("chR5"), CheckBox).Checked) _
                           & "',REQUISITO7='" & Valore01(CType(Dom_Requisiti1.FindControl("chR6"), CheckBox).Checked) _
                           & "',REQUISITO8='" & Valore01(CType(Dom_Requisiti1.FindControl("chR7"), CheckBox).Checked) _
                           & "',REQUISITO9='" & Valore01(CType(Dom_Requisiti1.FindControl("chR8"), CheckBox).Checked)
                sStringaSql = sStringaSql _
                           & "',ID_PARA_0=" & CType(Dom_Familiari1.FindControl("cmbF1"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_1=" & CType(Dom_Familiari1.FindControl("cmbF2"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_2=" & CType(Dom_Familiari1.FindControl("cmbF3"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_3=" & CType(Dom_Familiari1.FindControl("cmbF4"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_4=" & CType(Dom_Familiari1.FindControl("cmbF5"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_5=" & CType(Dom_Familiari1.FindControl("cmbF6"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_6=" & CType(Dom_Familiari1.FindControl("cmbF7"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_7=" & CType(Dom_Abitative_1_1.FindControl("cmbA1"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_8=" & CType(Dom_Abitative_1_1.FindControl("cmbA2"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_9=" & CType(Dom_Abitative_1_1.FindControl("cmbA3"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_10=" & CType(Dom_Abitative_1_1.FindControl("cmbA4"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_11=" & CType(Dom_Abitative_1_1.FindControl("cmbA5"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_12=" & CType(Dom_Abitative_2_1.FindControl("cmbA6"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_13=" & CType(Dom_Abitative_2_1.FindControl("cmbA7"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_14=" & CType(Dom_Abitative_2_1.FindControl("cmbA8"), DropDownList).SelectedItem.Value & "," _
                           & "ID_PARA_15=" & CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).SelectedItem.Value & " " _
                           & ",FL_MOROSITA_G='" & Valore01(CType(Dom_Abitative_1_1.FindControl("chMG"), CheckBox).Checked) & "'," _
                           & "FL_AFF_ONEROSO='" & Valore01(CType(Dom_Abitative_2_1.FindControl("chAO"), CheckBox).Checked) _
                           & "' WHERE ID = " & lIdDomanda
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()

                If lProgr <> 0 Then
                    If CType(Dom_Richiedente1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text = "ITALIA" Then
                        sStringaSql = "UPDATE COMP_NAS_RES SET CAP_RES='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtCAPRes"), TextBox).Text) & "',ID_LUOGO_RES_DNTE=" & CType(Dom_Richiedente1.FindControl("cmbComuneRes"), DropDownList).SelectedItem.Value & ",ID_TIPO_IND_RES_DNTE=" & CType(Dom_Richiedente1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value & ",IND_RES_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtIndRes"), TextBox).Text) & "',CIVICO_RES_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtCivicoRes"), TextBox).Text) & "',TELEFONO_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtTelRes"), TextBox).Text) & "' WHERE ID_COMPONENTE=" & lIndice_Componente
                        par.cmd.CommandText = sStringaSql
                        par.cmd.ExecuteNonQuery()
                    Else
                        sStringaSql = "UPDATE COMP_NAS_RES SET CAP_RES='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtCAPRes"), TextBox).Text) & "',ID_LUOGO_RES_DNTE=" & CType(Dom_Richiedente1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Value & ",ID_TIPO_IND_RES_DNTE=" & CType(Dom_Richiedente1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value & ",IND_RES_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtIndRes"), TextBox).Text) & "',CIVICO_RES_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtCivicoRes"), TextBox).Text) & "',TELEFONO_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtTelRes"), TextBox).Text) & "' WHERE ID_COMPONENTE=" & lIndice_Componente
                        par.cmd.CommandText = sStringaSql
                        par.cmd.ExecuteNonQuery()
                    End If
                End If

                For Each Items As ListItem In CType(Dom_Requisiti1.FindControl("chkListRequisiti"), CheckBoxList).Items
                    If Items.Selected = True Then
                        par.cmd.CommandText = "SELECT ID_DOMANDA, ID_TIPO_ESCLUSIONE FROM DOMANDE_ESCLUSIONI where ID_TIPO_ESCLUSIONE=" & Items.Value & " and id_domanda=" & lIdDomanda
                        Dim myReaderEsc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If Not myReaderEsc.Read Then
                            par.cmd.CommandText = "INSERT INTO DOMANDE_ESCLUSIONI (ID_TIPO_ESCLUSIONE,ID_DOMANDA) " _
                                & " values (" & Items.Value & "," & lIdDomanda & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        myReaderEsc.Close()
                    Else
                        par.cmd.CommandText = "SELECT ID_DOMANDA, ID_TIPO_ESCLUSIONE FROM DOMANDE_ESCLUSIONI where ID_TIPO_ESCLUSIONE=" & Items.Value & " and id_domanda=" & lIdDomanda
                        Dim myReaderEsc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If myReaderEsc.Read Then
                            par.cmd.CommandText = "DELETE FROM DOMANDE_ESCLUSIONI WHERE ID_DOMANDA=" & lIdDomanda & " AND ID_TIPO_ESCLUSIONE=" & Items.Value
                            par.cmd.ExecuteNonQuery()
                        End If
                        myReaderEsc.Close()
                    End If
                Next

                If lNuovaDomanda = 1 Then
                    sStringaSql = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','1" _
                          & "','F01','','I')"
                    par.cmd.CommandText = sStringaSql
                    par.cmd.ExecuteNonQuery()
                    Session.Item("ID_NUOVA_DIC") = ""
                    lNuovaDomanda = 0
                End If

                If Session.Item("ANAGRAFE") = "1" Then
                    'imgAnagrafe.Attributes.Clear()
                    imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('Anagrafe.aspx?ID=" & par.criptamolto(lIdDichiarazione) & "&T=0','Anagrafe','top=0,left=0,width=600,height=400');")
                End If


                par.myTrans.Commit()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                myReader.Close()

                imgStampa.Enabled = True
                imgStampa.ImageUrl = "NuoveImm\Img_Stampa.png"
                bMemorizzato = True

            Catch EX As Exception
                Label10.Text = EX.Message
            Finally
            End Try
        End Sub

        Private Function Valore01(ByVal valore As Boolean) As String
            If valore = True Then
                Valore01 = "1"
            Else
                Valore01 = "0"
            End If
        End Function


        Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
            Try
                If Session.Item("LAVORAZIONE") = "1" Then
                    If Fl_Integrazione = "1" Then
                        If Session.Item("CONFERMATO") <> "0" And imgStampa.Visible = True Then
                            H1.Value = "1"
                            Response.Write("<script>alert('Attenzione...Hai visualizzato la domanda ma non hai elaborato. Assicurarsi di salvare ed elaborare la domanda!');</script>")
                            Exit Sub
                        End If
                    End If
                    If par.OracleConn.State = Data.ConnectionState.Open Then

                        par.OracleConn.Close()
                    End If

                    If Fl_US <> "1" Then
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                        ‘‘par.cmd.Transaction = par.myTrans
                        par.myTrans.Rollback()
                    End If
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                    HttpContext.Current.Session.Remove("TRANSAZIONE")
                    HttpContext.Current.Session.Remove("CONNESSIONE")
                    Session.Item("LAVORAZIONE") = "0"
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Page.Dispose()
                    If Fl_Integrazione = "1" Or Fl_US = "1" Then
                        Session.Item("LAVORAZIONE") = "1"
                        Response.Write("<script>window.close();</script>")
                    Else
                        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                    End If
                Else
                    Session.Item("LAVORAZIONE") = "0"
                    Page.Dispose()
                    If Fl_Integrazione = "1" Then
                        Session.Item("LAVORAZIONE") = "1"
                        Response.Write("<script>window.close();</script>")
                    Else
                        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                    End If
                End If
            Catch EX As Exception
                Session.Item("LAVORAZIONE") = "0"
                Page.Dispose()
                Response.Write("<script>document.location.href=""ErrorPage.aspx""</script>")
            Finally

            End Try
        End Sub

        Protected Sub btnRequisiti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRequisiti.Click

            'Dom_Requisiti1.Visible = True
            'btnRequisiti.ImageUrl = "p_menu\REC_1.gif"

            'Select Case iTab
            '    Case 0
            '        Dom_Richiedente1.Visible = False
            '        btnRichiedente.ImageUrl = "p_menu\RICH_0.gif"
            '    Case 1
            '        Dom_Dichiara1.Visible = False
            '        btnDichiara.ImageUrl = "p_menu\DICH_0.gif"
            '    Case 2
            '        Dom_Familiari1.Visible = False
            '        btnFamiliari.ImageUrl = "p_menu\FAM_0.gif"
            '    Case 3
            '        Dom_Abitative_1_1.Visible = False
            '        btnAbitative1.ImageUrl = "p_menu\ABIT1_0.gif"
            '    Case 4
            '        Dom_Abitative_2_1.Visible = False
            '        btnAbitative2.ImageUrl = "p_menu\ABIT2_0.gif"
            '    Case 5

            '    Case 6
            '        Note1.Visible = False
            '        btnNote.ImageUrl = "p_menu\NOTE_0.gif"
            'End Select

            'iTab = 5


        End Sub


        Protected Sub imgStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStampa.Click

            Call btnSalva_Click(sender, e)
            If bMemorizzato = True Then
                imgAttendi.Visible = True
                DeviSalvare = False
                CalcolaStampa()
                imgAttendi.Visible = False
                If Fl_Integrazione = "1" Then
                    Session.Item("CONFERMATO") = "0"
                End If
                If DeviSalvare = True Then
                    Call btnSalva_Click(sender, e)
                End If
            Else
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
            End If

        End Sub

        Private Function CalcolaStampa()

            

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
            Dim limite_isee As Integer = 0

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


                DescrizioneBandoAggiornamento = ""

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                glIndice_Bando_Origine = lIndice_Bando
                par.cmd.CommandText = "select fl_rinnovo from domande_bando where id=" & lIdDomanda
                Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader11.Read Then
                    If par.IfNull(myReader11("fl_rinnovo"), "0") = "1" Then
                        par.cmd.CommandText = "select * from bandi where stato=1 order by id desc"
                        Dim myReader12 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader12.Read Then
                            'lIndice_Bando = par.IfNull(myReader12("id"), "-1")
                            DescrizioneBandoAggiornamento = par.IfNull(myReader12("descrizione"), "")
                        End If
                        myReader12.Close()
                    End If
                End If
                myReader11.Close()

                If Fl_Integrazione = "1" Then
                    par.cmd.CommandText = "select * from bandi where stato=1 order by id desc"
                    Dim myReader14 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader14.Read Then
                        'lIndice_Bando = par.IfNull(myReader12("id"), "-1")
                        DescrizioneBandoAggiornamento = par.IfNull(myReader14("descrizione"), "")
                    End If
                    myReader14.Close()

                End If


                par.cmd.CommandText = "SELECT TASSO_RENDIMENTO FROM BANDI WHERE ID=" & lIndice_Bando
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader.Read() Then
                    TASSO_RENDIMENTO = par.IfNull(myReader("TASSO_RENDIMENTO"), 0)
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT DICHIARAZIONI.ANNO_SIT_ECONOMICA FROM DICHIARAZIONI,DOMANDE_BANDO WHERE DOMANDE_BANDO.ID=" & lIdDomanda & " AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID"
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

                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
                myReader = par.cmd.ExecuteReader()

                'If myReader.Read() Then

                TOT_COMPONENTI = 0

                Do While myReader.Read()

                    If par.RicavaEta(myReader("DATA_NASCITA")) >= 15 Then
                        If par.RicavaEta(myReader("DATA_NASCITA")) >= 18 Then
                            adulti = adulti + 1
                        End If
                    Else
                        MINORI = MINORI + 1
                    End If

                    par.cmd.CommandText = "SELECT * FROM COMP_DETRAZIONI WHERE id_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader1.Read
                        DETRAZIONI = DETRAZIONI + par.IfNull(myReader1("IMPORTO"), 0)
                    End While
                    myReader1.Close()



                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    myReader1 = par.cmd.ExecuteReader()
                    While myReader1.Read
                        REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("REDDITO_IRPEF"), 0) + par.IfNull(myReader1("PROV_AGRARI"), 0)
                    End While
                    myReader1.Close()



                    par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    myReader1 = par.cmd.ExecuteReader()
                    While myReader1.Read
                        REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("IMPORTO"), 0)
                    End While
                    myReader1.Close()



                    DETRAZIONI_FRAGILE = 0
                    par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
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

                    Else
                        If par.IfNull(myReader("indennita_acc"), 0) = "1" Then
                            DETRAZIONI_FR = DETRAZIONI_FR + 10000
                            TOT_SPESE = TOT_SPESE + 10000
                        End If

                    End If
                    myReader1.Close()


                    par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                    myReader1 = par.cmd.ExecuteReader()
                    While myReader1.Read
                        MOBILI = MOBILI + par.IfNull(myReader1("IMPORTO"), 0)
                    End While
                    myReader1.Close()



                    par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
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



                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI WHERE ID=" & lIdDichiarazione
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    INV_100_CON = par.IfNull(myReader("N_INV_100_CON"), 0)
                    INV_100_NO = par.IfNull(myReader("N_INV_100_SENZA"), 0)
                    INV_66_99 = par.IfNull(myReader("N_INV_100_66"), 0)
                End If
                myReader.Close()

                DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)



                If REDDITO_COMPLESSIVO <> 0 Then
                    STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                                 & "<td width='75%' height='23'> REDDITO COMPLESSIVO DEL NUCLEO FAMILIARE" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & par.Converti(Format(REDDITO_COMPLESSIVO, "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                Else
                    STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                                 & "<td width='75%' height='23'> Entrate nessun reddito indicato" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & par.Converti(Format(REDDITO_COMPLESSIVO, "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If

                FIGURATIVO_MOBILI = (MOBILI \ 5165) * 5165

                If FIGURATIVO_MOBILI > 0 Then
                    STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                                 & "<td width='75%' height='23'> FIGURATIVO REDDITO MOBILIARE" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & par.Converti(Format(((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100), "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If

                If DETRAZIONI <> 0 Then
                    STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                                 & "<td width='75%' height='23'> DETRAZIONI DAL REDDITO LORDO" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>- " & par.Converti(Format(DETRAZIONI, "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                Else
                    STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                                 & "<td width='75%' height='23'> Non ci sono detrazioni" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>0 " _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If



                If INV_100_CON > 0 Then
                    STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                                 & "<td width='75%' height='23'> INVALIDI 100% CON INDENNITA'" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & INV_100_CON _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If

                If TOT_SPESE > 0 Then
                    STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                                 & "<td width='75%' height='23'> SPESE SOSTENUTE PER ASSISTENZA" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & par.Converti(Format(TOT_SPESE, "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If

                If INV_100_NO > 0 Then
                    STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                                 & "<td width='75%' height='23'> INVALIDI 100% SENZA INDENNITA'" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & INV_100_NO _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If

                If INV_66_99 > 0 Then
                    STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                                 & "<td width='75%' height='23'> INVALIDI 66%-99%" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & INV_66_99 _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If

                If DETRAZIONI_FR > 0 Then
                    STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                                 & "<td width='75%' height='23'> Detrazioni per nucleo familiare affetto da fragilità (" & INV_100_CON + INV_100_NO + INV_66_99 & " invalidi)" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>-" & par.Converti(Format(DETRAZIONI_FR, "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If



                ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
                If ISEE_ERP < 0 Then
                    ISEE_ERP = 0
                End If

                ISR_ERP = ISEE_ERP

                ISEE_ERP = 0
                STRINGA_STAMPA = STRINGA_STAMPA & "<tr>" _
                             & "<td width='75%' height='23'> TOTALE DEL REDDITO DA CONSIDERARE AI FINI ISEE-erp" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(ISR_ERP, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"


                If FIGURATIVO_MOBILI > 0 Then
                    STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                                 & "<td width='75%' height='23'>CONSISTENZA DEL PATRIMONIO MOBILIARE" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & par.Converti(Format(FIGURATIVO_MOBILI, "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If


                If IMMOBILI > 0 Then
                    STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                                 & "<td width='75%' height='23'>CONSISTENZA DEL PATRIMONIO IMMOBILIARE" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & par.Converti(Format(IMMOBILI, "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If

                If MUTUI > 0 Then
                    STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                                 & "<td width='75%' height='23'>DETRAZIONI PER MUTUI CONTRATTI" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>-" & par.Converti(Format(MUTUI, "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If

                STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                             & "<td width='75%' height='23'>TOTALE CONSISTENZA DEL PATRIMONIO IMMOBILIARE" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(IMMOBILI - MUTUI, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

                If IMMOBILI_RESIDENZA > 0 Then
                    STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                                 & "<td width='75%' height='23'>VALORE DELLA RESIDENZA DI PROPRIETA'" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & par.Converti(Format(IMMOBILI_RESIDENZA, "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If

                If MUTUI_RESIDENZA > 0 Then
                    STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                                 & "<td width='75%' height='23'>MUTUO RESIDUO PER LA RESIDENZA" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & par.Converti(Format(MUTUI_RESIDENZA, "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If

                If IMMOBILI_RESIDENZA > 0 Then
                    STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                                 & "<td width='75%' height='23'>VALORE NETTO DELLA CASA DI RESIDENZA" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & par.Converti(Format(IMMOBILI_RESIDENZA - MUTUI_RESIDENZA, "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If

                TOTALE_IMMOBILI = (IMMOBILI - MUTUI) + (IMMOBILI_RESIDENZA - MUTUI_RESIDENZA)

                STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                             & "<td width='75%' height='23'>TOTALE COMPLESSIVO DEL PATRIMONIO DA CONSIDERARE AI FINI ISEE-erp" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(TOTALE_IMMOBILI, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"


                STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                             & "<td width='75%' height='23'>Coefficiente della valutazione del patrimonio immobiliare" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>0,20" _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"


                TOTALE_ISEE_ERP = (TOTALE_IMMOBILI) * 0.20000000000000001

                ISP_ERP = TOTALE_ISEE_ERP

                STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                             & "<td width='75%' height='23'>VALUTAZIONE DEL PATRIMONIO AI FINI ISEE-erp" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(TOTALE_ISEE_ERP, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"


                TOTALE_PATRIMONIO_ISEE_ERP = (TOTALE_IMMOBILI + FIGURATIVO_MOBILI)
                STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                             & "<td width='75%' height='23'>PATRIMONIO COMPLESSIVO AI FINI ERP" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(TOTALE_PATRIMONIO_ISEE_ERP, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

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

                If TOTALE_PATRIMONIO_ISEE_ERP > LIMITE_PATRIMONIO Then
                    STRINGA_STAMPA_1 = STRINGA_STAMPA_1 & "<tr>" _
                                 & "<td width='75%' height='23'>Limite Patrimonio Familiare" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & par.Converti(Format(LIMITE_PATRIMONIO, "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"

                End If


                STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                             & "<td width='75%' height='23'>Numero di componenti del nucleo familiare" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & TOT_COMPONENTI _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"



                STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                             & "<td width='75%' height='23'>Parametro corrispondente alla composizione del nucleo familiare" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(PARAMETRO, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

                'If MINORI > 0 Then
                STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                             & "<td width='75%' height='23'>Numero di minori di 15 anni nel nucleo familiare" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & MINORI _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                'Else
                'STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                '             & "<td width='75%' height='23'>Non ci sono minori di 15 anni nel nucleo familiare" _
                '             & "</td>" _
                '             & "<td width='13%' height='23'>" _
                '             & "</td>" _
                '             & "<td width='13%' height='23'>" _
                '             & "<table border='1' width='100%'>" _
                '             & "<tr>" _
                '             & "<td width='100%' align='right'>0" _
                '             & "</td>" _
                '             & "</tr>" _
                '             & "</table>      </td>   </tr>"
                'End If

                If PARAMETRO_MINORI > 0 Then
                    STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                                 & "<td width='75%' height='23'>- Parametro per minori" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>-" & par.Converti(Format(PARAMETRO_MINORI, "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If


                If adulti >= 1 Then
                    STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                                 & "<td width='75%' height='23'>Numero di adulti nel nucleo familiare" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & adulti _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                Else
                    STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                                 & "<td width='75%' height='23'>Non ci sono adulti nel nucleo familiare" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>0" _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                End If

                STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                             & "<td width='75%' height='23'>REDDITO DA CONSIDERARE AI FINI ISEE-erp" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(ISR_ERP, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

                STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                             & "<td width='75%' height='23'>VALUTAZIONE DEL PATRIMONIO AI FINI ISEE-erp" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(ISP_ERP, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

                ISE_ERP = ISR_ERP + ISP_ERP

                STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                             & "<td width='75%' height='23'>ISE-erp: indicatore della situazione economica (erp)" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(ISE_ERP, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"


                STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                             & "<td width='75%' height='23'>VSE: Valore della scala di equivalenza" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Converti(Format(VSE, "##,##0.00")) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"



                ISEE_ERP = ISE_ERP / VSE

                If ISEE_ERP <= 16000 Then
                    TIPO_ALLOGGIO = 0
                    limite_isee = 0
                Else
                    If ISEE_ERP > 16000 And ISE_ERP <= 17000 Then
                        TIPO_ALLOGGIO = 0
                        limite_isee = 0
                    Else
                        'If ISEE_ERP <= 23000 And ISE_ERP > 17000 Then
                        If ISEE_ERP <= 40000 And ISE_ERP > 17000 Then
                            TIPO_ALLOGGIO = 1
                            limite_isee = 1
                        Else
                            ESCLUSIONE = "<li>ISEE superiore al limite ERP</li>"
                            par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET REQUISITO6='0' WHERE ID=" & lIdDomanda
                            par.cmd.ExecuteNonQuery()
                            '***CasReq6.Text = "0"
                        End If
                    End If
                End If

                If TOTALE_PATRIMONIO_ISEE_ERP > LIMITE_PATRIMONIO Then
                    ESCLUSIONE = ESCLUSIONE & "<li>Limite Patrimoniale superato</li>"
                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET REQUISITO6='0' WHERE ID=" & lIdDomanda
                    par.cmd.ExecuteNonQuery()
                    '***CasReq6.Text = "0"
                End If

                If CType(Dom_Dichiara1.FindControl("cmbPresentaD"), DropDownList).SelectedItem.Value <> 5 Then
                    If CType(Dom_Richiedente1.FindControl("cmbResidenza"), DropDownList).SelectedItem.Value = 4 Then
                        ESCLUSIONE = ESCLUSIONE & "<li>" & CType(Dom_Richiedente1.FindControl("cmbResidenza"), DropDownList).SelectedItem.Text & "</li>"
                    End If
                Else
                    CType(Dom_Richiedente1.FindControl("cmbResidenza"), DropDownList).SelectedIndex = -1
                    CType(Dom_Richiedente1.FindControl("cmbResidenza"), DropDownList).Items.FindByValue("4").Selected = True
                End If

                Dim req1 As String
                Dim req2 As String
                Dim req3 As String
                Dim req4 As String
                Dim req5 As String
                Dim req6 As String
                Dim req7 As String

                If Valore01(CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked) = "0" Then
                    ESCLUSIONE = ESCLUSIONE & "<li>Mancanza della cittadinanza o del permesso di soggiorno</li>"
                    req1 = "<img src=block.gif width=10 height=10 border=1>"
                Else
                    req1 = "<img src=block_checked.gif width=10 height=10 border=1>"
                End If
                If Valore01(CType(Dom_Requisiti1.FindControl("chR2"), CheckBox).Checked) = "0" Then
                    ESCLUSIONE = ESCLUSIONE & "<li>Mancanza della residenza anagrafica o attività lavorativa nel comune</li>"
                End If
                If Valore01(CType(Dom_Requisiti1.FindControl("chR3"), CheckBox).Checked) = "0" Then
                    ESCLUSIONE = ESCLUSIONE & "<li>Predecente Assegnazione in proprietà</li>"
                    req2 = "<img src=block.gif width=10 height=10 border=1>"
                Else
                    req2 = "<img src=block_checked.gif width=10 height=10 border=1>"
                End If
                If Valore01(CType(Dom_Requisiti1.FindControl("chR4"), CheckBox).Checked) = "0" Then
                    ESCLUSIONE = ESCLUSIONE & "<li>Decadenza</li>"
                    req3 = "<img src=block.gif width=10 height=10 border=1>"
                Else
                    req3 = "<img src=block_checked.gif width=10 height=10 border=1>"
                End If
                If Valore01(CType(Dom_Requisiti1.FindControl("chR5"), CheckBox).Checked) = "0" Then
                    ESCLUSIONE = ESCLUSIONE & "<li>Cessione</li>"
                    req4 = "<img src=block.gif width=10 height=10 border=1>"
                Else
                    req4 = "<img src=block_checked.gif width=10 height=10 border=1>"
                End If
                If Valore01(CType(Dom_Requisiti1.FindControl("chR6"), CheckBox).Checked) = "0" Then
                    ESCLUSIONE = ESCLUSIONE & "<li>Proprietà o Godimento di alloggio adeguato</li>"
                    req5 = "<img src=block.gif width=10 height=10 border=1>"
                Else
                    req5 = "<img src=block_checked.gif width=10 height=10 border=1>"
                End If
                If Valore01(CType(Dom_Requisiti1.FindControl("chR7"), CheckBox).Checked) = "0" Then
                    ESCLUSIONE = ESCLUSIONE & "<li>Morosità da alloggio ERP negli ultimi 5 anni</li>"
                    req6 = "<img src=block.gif width=10 height=10 border=1>"
                Else
                    req6 = "<img src=block_checked.gif width=10 height=10 border=1>"
                End If
                If Valore01(CType(Dom_Requisiti1.FindControl("chR8"), CheckBox).Checked) = "0" Then
                    ESCLUSIONE = ESCLUSIONE & "<li>Occupazione abusiva negli ultimi 5 anni</li>"
                    req7 = "<img src=block.gif width=10 height=10 border=1>"
                Else
                    req7 = "<img src=block_checked.gif width=10 height=10 border=1>"
                End If

                '09_11_2018
                For Each Items As ListItem In CType(Dom_Requisiti1.FindControl("chkListRequisiti"), CheckBoxList).Items
                    If Items.Selected = True Then
                        ESCLUSIONE = ESCLUSIONE & "<li>" & Items.Text & "</li>"
                    End If
                Next

                If ESCLUSIONE <> "" Then
                    Testo_Da_Scrivere = "<b>DOMANDA NON IDONEA</b>"
                End If

                STRINGA_STAMPA_2 = STRINGA_STAMPA_2 & "<tr>" _
                             & "<td width='75%' height='23'><b>ISEE-erp:</b> Indicatore della Situazione Economica Equivalente (erp)" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'><b>" & par.Converti(Format(ISEE_ERP, "##,##0.00")) _
                             & "</b></td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"



                If TIPO_ALLOGGIO = 0 Then
                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>ISEE intermedio per canone sociale" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>16.000,00" _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"

                End If

                If TIPO_ALLOGGIO = 1 Then
                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>ISEE intermedio per canone sociale" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>17.000,00" _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

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

                Valore = CType(Dom_Familiari1.FindControl("cmbF1"), DropDownList).SelectedItem.Value
                If Valore > 0 Then
                    par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Valore = par.IfNull(myReader("regionale"), "0")
                        Valore1 = par.IfNull(myReader("comunale"), "0")
                    End If
                    myReader.Close()
                    F1 = F1 + Valore
                    F10 = F10 + Valore1
                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>Valore attribuito per il criterio anziani" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & Valore _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                Valore = CType(Dom_Familiari1.FindControl("cmbF2"), DropDownList).SelectedItem.Value
                If Valore > 0 Then
                    par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Valore = par.IfNull(myReader("regionale"), "0")
                        Valore1 = par.IfNull(myReader("comunale"), "0")
                    End If
                    myReader.Close()

                    F1 = F1 + Valore
                    F2 = F2 + Valore
                    F3 = F3 + Valore

                    F10 = F10 + Valore1
                    F20 = F20 + Valore1
                    F30 = F30 + Valore1


                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>Valore attribuito per il criterio disabilità" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & Valore _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                Valore = CType(Dom_Familiari1.FindControl("cmbF3"), DropDownList).SelectedItem.Value
                If Valore > 0 Then
                    par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Valore = par.IfNull(myReader("regionale"), "0")
                        Valore1 = par.IfNull(myReader("comunale"), "0")
                    End If
                    myReader.Close()

                    F2 = F2 + Valore
                    F20 = F20 + Valore1
                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>Valore attribuito per il criterio giovane coppia" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & Valore _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                Valore = CType(Dom_Familiari1.FindControl("cmbF4"), DropDownList).SelectedItem.Value
                If Valore > 0 Then
                    par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Valore = par.IfNull(myReader("regionale"), "0")
                        Valore1 = par.IfNull(myReader("comunale"), "0")
                    End If
                    myReader.Close()
                    F1 = F1 + Valore
                    F3 = F3 + Valore

                    F10 = F10 + Valore1
                    F30 = F30 + Valore1


                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>Valore attribuito per il criterio persona sola" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & Valore _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                Valore = CType(Dom_Familiari1.FindControl("cmbF5"), DropDownList).SelectedItem.Value
                If Valore > 0 Then
                    par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Valore = par.IfNull(myReader("regionale"), "0")
                        Valore1 = par.IfNull(myReader("comunale"), "0")
                    End If
                    myReader.Close()

                    F2 = F2 + Valore
                    F3 = F3 + Valore
                    F20 = F20 + Valore
                    F30 = F30 + Valore

                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>Valore attribuito per il criterio disoccupazione" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & Valore _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                Valore = CType(Dom_Familiari1.FindControl("cmbF6"), DropDownList).SelectedItem.Value
                If Valore > 0 Then
                    par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Valore = par.IfNull(myReader("regionale"), "0")
                        Valore1 = par.IfNull(myReader("comunale"), "0")
                    End If
                    myReader.Close()
                    F1 = F1 + Valore
                    F2 = F2 + Valore
                    F3 = F3 + Valore

                    F10 = F10 + Valore
                    F20 = F20 + Valore
                    F30 = F30 + Valore


                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>Valore attribuito per il criterio ricongiunzione" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & Valore _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                Valore = CType(Dom_Familiari1.FindControl("cmbF7"), DropDownList).SelectedItem.Value
                If Valore > 0 Then
                    par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Valore = par.IfNull(myReader("regionale"), "0")
                        Valore1 = par.IfNull(myReader("comunale"), "0")
                    End If
                    myReader.Close()
                    F1 = F1 + Valore
                    F2 = F2 + Valore
                    F3 = F3 + Valore

                    F10 = F10 + Valore
                    F20 = F20 + Valore
                    F30 = F30 + Valore


                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>Valore attribuito per il criterio condizioni particolari" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & Valore _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If



                Dim a1 As Double
                Dim A2 As Double
                Dim A3 As Double
                Dim A4 As Double

                Dim A10 As Double
                Dim A20 As Double
                Dim A30 As Double
                Dim A40 As Double

                a1 = 0
                A2 = 0
                A3 = 0
                A4 = 0

                A10 = 0
                A20 = 0
                A30 = 0
                A40 = 0


                Valore = CType(Dom_Abitative_1_1.FindControl("cmbA2"), DropDownList).SelectedItem.Value
                If Valore > 0 Then
                    par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Valore = par.IfNull(myReader("regionale"), "0")
                        Valore1 = par.IfNull(myReader("comunale"), "0")
                    End If
                    myReader.Close()
                    A2 = A2 + Valore
                    A20 = A20 + Valore1

                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>Valore attribuito per il criterio alloggio improprio" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & Valore _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                Valore = CType(Dom_Abitative_1_1.FindControl("cmbA3"), DropDownList).SelectedItem.Value
                If Valore > 0 Then
                    par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Valore = par.IfNull(myReader("regionale"), "0")
                        Valore1 = par.IfNull(myReader("comunale"), "0")
                    End If
                    myReader.Close()
                    A3 = A3 + Valore
                    A4 = A4 + Valore

                    A30 = A30 + Valore1
                    A40 = A40 + Valore1


                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>Valore attribuito per il criterio coabitazione" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & Valore _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                Valore = CType(Dom_Abitative_1_1.FindControl("cmbA4"), DropDownList).SelectedItem.Value
                If Valore > 0 Then
                    par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Valore = par.IfNull(myReader("regionale"), "0")
                        Valore1 = par.IfNull(myReader("comunale"), "0")
                    End If
                    myReader.Close()
                    A3 = A3 + Valore
                    A4 = A4 + Valore

                    A30 = A30 + Valore1
                    A40 = A40 + Valore1


                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>Valore attribuito per il criterio sovraffollamento" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & Valore _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                Valore = CType(Dom_Abitative_1_1.FindControl("cmbA5"), DropDownList).SelectedItem.Value
                If Valore > 0 Then
                    par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Valore = par.IfNull(myReader("regionale"), "0")
                        Valore1 = par.IfNull(myReader("comunale"), "0")
                    End If
                    myReader.Close()
                    A3 = A3 + Valore
                    A30 = A30 + Valore1

                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>Valore attribuito per il criterio condizione dell'alloggio" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & Valore _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                Valore = CType(Dom_Abitative_2_1.FindControl("cmbA6"), DropDownList).SelectedItem.Value
                If Valore > 0 Then
                    par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Valore = par.IfNull(myReader("regionale"), "0")
                        Valore1 = par.IfNull(myReader("comunale"), "0")
                    End If
                    myReader.Close()
                    A4 = A4 + Valore
                    A40 = A40 + Valore1

                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>Valore attribuito per il criterio barriere architettoniche" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & Valore _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                Valore = CType(Dom_Abitative_2_1.FindControl("cmbA7"), DropDownList).SelectedItem.Value
                If Valore > 0 Then
                    par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Valore = par.IfNull(myReader("regionale"), "0")
                        Valore1 = par.IfNull(myReader("comunale"), "0")
                    End If
                    myReader.Close()
                    A3 = A3 + Valore
                    A30 = A30 + Valore1

                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>Valore attribuito per il criterio di accessibilità" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & Valore _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                Valore = CType(Dom_Abitative_2_1.FindControl("cmbA8"), DropDownList).SelectedItem.Value
                If Valore > 0 Then
                    par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Valore = par.IfNull(myReader("regionale"), "0")
                        Valore1 = par.IfNull(myReader("comunale"), "0")
                    End If
                    myReader.Close()
                    A3 = A3 + Valore
                    A4 = A4 + Valore

                    A30 = A30 + Valore1
                    A40 = A40 + Valore1


                    STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                             & "<td width='75%' height='23'>Valore attribuito per il criterio lontananza dalla sede di lavoro" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & Valore _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                'If Valore01(CType(Dom_Abitative_1_1.FindControl("chMorosita"), CheckBox).Checked) = "0" And Valore01(CType(Dom_Abitative_1_1.FindControl("chMG"), CheckBox).Checked) = "1" Then
                If (Valore01(CType(Dom_Abitative_1_1.FindControl("chMorosita"), CheckBox).Checked) = "1") Or (Valore01(CType(Dom_Abitative_1_1.FindControl("chMorosita"), CheckBox).Checked) = "0" And Valore01(CType(Dom_Abitative_1_1.FindControl("chMG"), CheckBox).Checked) = "1") Then
                    Valore = CType(Dom_Abitative_1_1.FindControl("cmbA1"), DropDownList).SelectedItem.Value
                    If Valore > 0 Then
                        par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                        myReader = par.cmd.ExecuteReader()
                        If myReader.Read() Then
                            Valore = par.IfNull(myReader("regionale"), "0")
                            Valore1 = par.IfNull(myReader("comunale"), "0")
                        End If
                        myReader.Close()
                        a1 = a1 + Valore
                        A10 = A10 + Valore1

                        STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                                 & "<td width='75%' height='23'>Valore attribuito per il criterio rilascio" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & Valore _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"
                    End If
                Else
                    'If Valore01(CType(Dom_Abitative_1_1.FindControl("chMG"), CheckBox).Checked) = "1" Then
                    '    Valore = CType(Dom_Abitative_1_1.FindControl("cmbA1"), DropDownList).SelectedItem.Value
                    '    If Valore > 0 Then
                    '        par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                    '        myReader = par.cmd.ExecuteReader()
                    '        If myReader.Read() Then
                    '            Valore = par.IfNull(myReader("regionale"), "0")
                    '            Valore1 = par.IfNull(myReader("comunale"), "0")
                    '        End If
                    '        a1 = a1 + Valore
                    '        A10 = A10 + Valore1

                    '        STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                    '                 & "<td width='75%' height='23'>Valore attribuito per il criterio rilascio" _
                    '                 & "</td>" _
                    '                 & "<td width='13%' height='23'>" _
                    '                 & "</td>" _
                    '                 & "<td width='13%' height='23'>" _
                    '                 & "<table border='1' width='100%'>" _
                    '                 & "<tr>" _
                    '                 & "<td width='100%' align='right'>" & Valore _
                    '                 & "</td>" _
                    '                 & "</tr>" _
                    '                 & "</table>      </td>   </tr>"
                    '    End If
                    'End If
                End If

                Dim canone_int As Double

                canone_int = 0
                If CType(Dom_Abitative_2_1.FindControl("txtSpese"), TextBox).Text <> "0" Then
                    canone_int = CDbl(Val(CType(Dom_Abitative_2_1.FindControl("txtSpese"), TextBox).Text))
                End If
                If Val(CType(Dom_Abitative_2_1.FindControl("txtSpeseAcc"), TextBox).Text) <> "0" Then
                    If Int(CType(Dom_Abitative_2_1.FindControl("txtSpeseAcc"), TextBox).Text) > 516 Then
                        canone_int = canone_int + 516
                    Else
                        canone_int = canone_int + Int(CType(Dom_Abitative_2_1.FindControl("txtSpeseAcc"), TextBox).Text)
                    End If
                End If

                Dim AffittoOn As Boolean

                AffittoOn = False
                If ESCLUSIONE = "" Then
                    If canone_int <> 0 Then
                        STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                                 & "<td width='75%' height='23'>Canone Integrato" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & par.Converti(Format(canone_int, "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"

                        Dim C1 As Double
                        Dim C2 As Double
                        Dim LIMITE_CANONE As Double

                        C1 = canone_int + ((canone_int / 100) * 5)

                        C2 = ISE_ERP / VSE 'PARAMETRO

                        Select Case C2
                            Case 0 To 4000
                                LIMITE_CANONE = (ISE_ERP / 100) * 8
                            Case (400001 / 100) To 4500
                                LIMITE_CANONE = (ISE_ERP / 100) * 9
                            Case (450001 / 100) To 5000
                                LIMITE_CANONE = (ISE_ERP / 100) * 10
                            Case (500001 / 100) To 5500
                                LIMITE_CANONE = (ISE_ERP / 100) * 11
                            Case (550001 / 100) To 6000
                                LIMITE_CANONE = (ISE_ERP / 100) * 12
                            Case (600001 / 100) To 6500
                                LIMITE_CANONE = (ISE_ERP / 100) * 13
                            Case (650001 / 100) To 7000
                                LIMITE_CANONE = (ISE_ERP / 100) * 14
                            Case (700001 / 100) To 7500
                                LIMITE_CANONE = (ISE_ERP / 100) * 15
                            Case (750001 / 100) To 8000
                                LIMITE_CANONE = (ISE_ERP / 100) * 16
                            Case (800001 / 100) To 8500
                                LIMITE_CANONE = (ISE_ERP / 100) * 17
                            Case (850001 / 100) To 9000
                                LIMITE_CANONE = (ISE_ERP / 100) * 18
                            Case (900001 / 100) To 9500
                                LIMITE_CANONE = (ISE_ERP / 100) * 19
                            Case (950001 / 100) To 10000
                                LIMITE_CANONE = (ISE_ERP / 100) * 20
                            Case (1000001 / 100) To 10500
                                LIMITE_CANONE = (ISE_ERP / 100) * 21
                            Case (1050001 / 100) To 11000
                                LIMITE_CANONE = (ISE_ERP / 100) * 22
                            Case (1100001 / 100) To 11500
                                LIMITE_CANONE = (ISE_ERP / 100) * 23
                            Case (1150001 / 100) To 12000
                                LIMITE_CANONE = (ISE_ERP / 100) * 24
                            Case (1200001 / 100) To 12500
                                LIMITE_CANONE = (ISE_ERP / 100) * (245 / 10)
                            Case (1250001 / 100) To 13000
                                LIMITE_CANONE = (ISE_ERP / 100) * 25
                            Case (1300001 / 100) To 13500
                                LIMITE_CANONE = (ISE_ERP / 100) * (255 / 10)
                            Case (1350001 / 100) To 14000
                                LIMITE_CANONE = (ISE_ERP / 100) * 26
                            Case (1400001 / 100) To 14500
                                LIMITE_CANONE = (ISE_ERP / 100) * (265 / 10)
                            Case (1450001 / 100) To 15000
                                LIMITE_CANONE = (ISE_ERP / 100) * 27
                            Case (1500001 / 100) To 15500
                                LIMITE_CANONE = (ISE_ERP / 100) * (275 / 10)
                            Case (1550001 / 100) To 16000
                                LIMITE_CANONE = (ISE_ERP / 100) * 28
                            Case (1600001 / 100) To 16500
                                LIMITE_CANONE = (ISE_ERP / 100) * (285 / 10)
                            Case (1650001 / 100) To 999999
                                LIMITE_CANONE = (ISE_ERP / 100) * 29
                        End Select

                        STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                                 & "<td width='75%' height='23'>Limite Affitto oneroso (con rivalutazione del 5%)" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "</td>" _
                                 & "<td width='13%' height='23'>" _
                                 & "<table border='1' width='100%'>" _
                                 & "<tr>" _
                                 & "<td width='100%' align='right'>" & par.Converti(Format(LIMITE_CANONE + ((LIMITE_CANONE * 5) / 100), "##,##0.00")) _
                                 & "</td>" _
                                 & "</tr>" _
                                 & "</table>      </td>   </tr>"



                        If canone_int > LIMITE_CANONE + ((LIMITE_CANONE * 5) / 100) And Valore01(CType(Dom_Abitative_2_1.FindControl("chAO"), CheckBox).Checked) = "1" Then
                            STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                                     & "<td width='75%' height='23'><b>SUSSISTE</b> la condizione di Affitto oneroso" _
                                     & "</td>" _
                                     & "<td width='13%' height='23'>" _
                                     & "</td>" _
                                     & "<td width='13%' height='23'>" _
                                     & "<table border='1' width='100%'>" _
                                     & "<tr>" _
                                     & "<td width='100%' align='right'>SI" _
                                     & "</td>" _
                                     & "</tr>" _
                                     & "</table>      </td>   </tr>"
                            CType(Dom_Abitative_2_1.FindControl("alert19"), Image).Visible = False
                            AffittoOn = True

                            Valore = CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).SelectedItem.Value
                            If Valore = -1 Then
                                CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).SelectedIndex = -1
                                CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).Items.FindByText("a) La condizione sussiste").Selected = True
                                Valore = CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).SelectedItem.Value
                                CType(Dom_Abitative_2_1.FindControl("alert21"), Image).Visible = True
                                DeviSalvare = True
                            Else
                                CType(Dom_Abitative_2_1.FindControl("alert21"), Image).Visible = False
                            End If

                            par.cmd.CommandText = "select comunale,regionale from  parametri_bando where id=" & Valore
                            myReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                Valore = par.IfNull(myReader("regionale"), "0")
                                Valore1 = par.IfNull(myReader("comunale"), "0")
                            End If
                            myReader.Close()
                            A3 = A3 + Valore
                            A4 = A4 + Valore

                            A30 = A30 + Valore1
                            A40 = A40 + Valore1


                            STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                                     & "<td width='75%' height='23'>Valore attribuito per il criterio affitto oneroso" _
                                     & "</td>" _
                                     & "<td width='13%' height='23'>" _
                                     & "</td>" _
                                     & "<td width='13%' height='23'>" _
                                     & "<table border='1' width='100%'>" _
                                     & "<tr>" _
                                     & "<td width='100%' align='right'>" & Valore _
                                     & "</td>" _
                                     & "</tr>" _
                                     & "</table>      </td>   </tr>"

                        Else
                            STRINGA_STAMPA_3 = STRINGA_STAMPA_3 & "<tr>" _
                                     & "<td width='75%' height='23'>SUSSISTE la condizione di Affitto oneroso" _
                                     & "</td>" _
                                     & "<td width='13%' height='23'>" _
                                     & "</td>" _
                                     & "<td width='13%' height='23'>" _
                                     & "<table border='1' width='100%'>" _
                                     & "<tr>" _
                                     & "<td width='100%' align='right'>NO" _
                                     & "</td>" _
                                     & "</tr>" _
                                     & "</table>      </td>   </tr>"
                            AffittoOn = False
                            Valore = CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).SelectedItem.Value
                            If Valore > 0 Then
                                'CType(Dom_Abitative_2_1.FindControl("alert21"), Image).Visible = True
                                CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).SelectedIndex = -1
                                CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).Items.FindByText("Non sussiste la condizione").Selected = True
                                Valore = CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).SelectedItem.Value
                            End If
                        End If
                    Else
                        Valore = CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).SelectedItem.Value
                        If Valore = -1 Then
                            CType(Dom_Abitative_2_1.FindControl("alert21"), Image).Visible = False
                        Else
                            CType(Dom_Abitative_2_1.FindControl("alert21"), Image).Visible = True
                            'Response.Write("<script>alert('Attenzione...Eliminare voce Affitto Oneroso e ristampare la domanda!');</script>")
                            'Exit Function
                            CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).SelectedIndex = -1
                            CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).Items.FindByText("a) La condizione sussiste").Selected = True
                            Valore = CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).SelectedItem.Value
                        End If
                    End If
                End If

                If Valore01(CType(Dom_Abitative_2_1.FindControl("chAO"), CheckBox).Checked) = "1" And CType(Dom_Abitative_1_1.FindControl("cmbA1"), DropDownList).SelectedItem.Value <> -1 And CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).SelectedItem.Value <> -1 Then
                    Response.Write("<script>alert('Attenzione...Verificare il campo <Rilascio Alloggio> e <Affitto oneroso>. Situazione incompatibile. Risolvere il problema prima di salvare e stampare la domanda!')</script>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
                    Exit Try
                End If

                Dim DISAGIO_F As Double
                Dim DISAGIO_A As Double
                Dim DISAGIO_E As Double

                Dim DISAGIO_F0 As Double
                Dim DISAGIO_A0 As Double
                Dim DISAGIO_E0 As Double


                DISAGIO_F = 0
                DISAGIO_A = 0
                DISAGIO_E = 0

                DISAGIO_F0 = 0
                DISAGIO_A0 = 0
                DISAGIO_E0 = 0

                If F1 > F2 Then
                    If F1 > F3 Then
                        DISAGIO_F = F1
                    Else
                        DISAGIO_F = F3
                    End If
                Else
                    If F2 > F3 Then
                        DISAGIO_F = F2
                    Else
                        DISAGIO_F = F3
                    End If
                End If


                If F10 > F20 Then
                    If F10 > F30 Then
                        DISAGIO_F0 = F10
                    Else
                        DISAGIO_F0 = F30
                    End If
                Else
                    If F20 > F30 Then
                        DISAGIO_F0 = F20
                    Else
                        DISAGIO_F0 = F30
                    End If
                End If


                If A10 > A20 Then
                    If A10 > A30 Then
                        If A10 > A40 Then
                            DISAGIO_A0 = A10
                        Else
                            DISAGIO_A0 = A40
                        End If
                    Else
                        If A30 > A40 Then
                            DISAGIO_A0 = A30
                        Else
                            DISAGIO_A0 = A40
                        End If
                    End If
                Else
                    If A20 > A30 Then
                        If A20 > A40 Then
                            DISAGIO_A0 = A20
                        Else
                            DISAGIO_A0 = A40
                        End If
                    Else
                        If A30 > A40 Then
                            DISAGIO_A0 = A30
                        Else
                            DISAGIO_A0 = A40
                        End If
                    End If
                End If

                If a1 > A2 Then
                    If a1 > A3 Then
                        If a1 > A4 Then
                            DISAGIO_A = a1
                        Else
                            DISAGIO_A = A4
                        End If
                    Else
                        If A3 > A4 Then
                            DISAGIO_A = A3
                        Else
                            DISAGIO_A = A4
                        End If
                    End If
                Else
                    If A2 > A3 Then
                        If A2 > A4 Then
                            DISAGIO_A = A2
                        Else
                            DISAGIO_A = A4
                        End If
                    Else
                        If A3 > A4 Then
                            DISAGIO_A = A3
                        Else
                            DISAGIO_A = A4
                        End If
                    End If
                End If

                If DISAGIO_A0 > 0 Then
                    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
                             & "<td width='75%' height='23'>Valutazione Intermedia a)" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & DISAGIO_A0 _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                If DISAGIO_F0 > 0 Then
                    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
                             & "<td width='75%' height='23'>Valutazione Intermedia f)" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & DISAGIO_F0 _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                End If

                Dim ISBARC As Double
                Dim ISBARC_R As Double
                Dim ISBAR As Double
                Dim VALORE_R As Double
                Dim T As Double
                Dim T1 As Double
                Dim r As Double

                Select Case CType(Dom_Richiedente1.FindControl("cmbResidenza"), DropDownList).SelectedItem.Value
                    Case 0
                        VALORE_R = 0
                    Case 1
                        VALORE_R = 0
                    Case 2
                        VALORE_R = 40
                    Case 3
                        VALORE_R = 85
                    Case 4
                        VALORE_R = 0
                End Select
                r = (VALORE_R / 100) * (3 / 10)


                STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
                & "<td width='75%' height='23'>Valutazione Intermedia r)" _
                & "</td>" _
                & "<td width='13%' height='23'>" _
                & "</td>" _
                & "<td width='13%' height='23'>" _
                & "<table border='1' width='100%'>" _
                & "<tr>" _
                & "<td width='100%' align='right'>" & VALORE_R _
                & "</td>" _
                & "</tr>" _
                & "</table>      </td>   </tr>"



                DISAGIO_A = (DISAGIO_A / 100) * (8 / 10)
                DISAGIO_F = (DISAGIO_F / 100) * (5 / 10)

                If limite_isee = 0 Then
                    DISAGIO_E = ((17000 - ISEE_ERP) / 17000) * (3 / 10)
                Else
                    'DISAGIO_E = ((23000 - ISEE_ERP) / 23000) * (3 / 10)
                    DISAGIO_E = ((40000 - ISEE_ERP) / 40000) * (3 / 10)
                End If

                DISAGIO_A0 = (DISAGIO_A0 / 100) * (8 / 10)
                DISAGIO_F0 = (DISAGIO_F0 / 100) * (5 / 10)
                If limite_isee = 0 Then
                    DISAGIO_E0 = ((17000 - ISEE_ERP) / 17000) * (3 / 10)
                Else
                    'DISAGIO_E0 = ((23000 - ISEE_ERP) / 23000) * (3 / 10)
                    DISAGIO_E0 = ((40000 - ISEE_ERP) / 40000) * (3 / 10)
                End If


                If DISAGIO_A0 > 0 Then
                    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
                             & "<td width='75%' height='23'>a) Indicatore disagio Abitativo" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Tronca(DISAGIO_A0) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                Else
                    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
                             & "<td width='75%' height='23'>a) Indicatore disagio Abitativo Nessun disagio riportato" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>0,000" _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                    DISAGIO_A = 0
                End If

                If DISAGIO_E0 > 0 Then
                    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
                             & "<td width='75%' height='23'>e) Indicatore disagio economico" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Tronca(DISAGIO_E0) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                Else
                    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
                             & "<td width='75%' height='23'>e) Indicatore disagio economico Nessun disagio Riportato" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>0,000" _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                    DISAGIO_E = 0

                End If

                If DISAGIO_F0 > 0 Then
                    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
                             & "<td width='75%' height='23'>a) Indicatore disagio familiare" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>" & par.Tronca(DISAGIO_F0) _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                Else
                    STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
                             & "<td width='75%' height='23'>f) Indicatore disagio familiare Nessun disagio riportato" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "</td>" _
                             & "<td width='13%' height='23'>" _
                             & "<table border='1' width='100%'>" _
                             & "<tr>" _
                             & "<td width='100%' align='right'>0,000" _
                             & "</td>" _
                             & "</tr>" _
                             & "</table>      </td>   </tr>"
                    DISAGIO_F = 0
                End If



                STRINGA_STAMPA_4 = STRINGA_STAMPA_4 & "<tr>" _
                         & "<td width='75%' height='23'>r) Indicatore disagio Residenza" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "</td>" _
                         & "<td width='13%' height='23'>" _
                         & "<table border='1' width='100%'>" _
                         & "<tr>" _
                         & "<td width='100%' align='right'>" & par.Tronca(r) _
                         & "</td>" _
                         & "</tr>" _
                         & "</table>      </td>   </tr>"

                If ESCLUSIONE = "" Then

                    T = (1 - ((1 - DISAGIO_A0) * (1 - DISAGIO_F0) * (1 - DISAGIO_E0)))
                    ISBARC = T * 10000
                    T1 = (1 - ((1 - DISAGIO_A) * (1 - DISAGIO_F) * (1 - DISAGIO_E)))
                    ISBAR = T1 * 10000


                    ISBARC_R = (1 - ((1 - T) * (1 - r))) * 10000

                    STRINGA_STAMPA_6 = par.Tronca(ISBARC)
                    STRINGA_STAMPA_5 = par.Tronca(ISBAR)
                    STRINGA_STAMPA_66 = par.Tronca(ISBARC_R)
                    lblISBAR.Text = par.Tronca(ISBARC_R)
                Else
                    ISBARC = 0
                    ISBAR = 0
                    ISBARC_R = 0

                    STRINGA_STAMPA_6 = "0,000"
                    STRINGA_STAMPA_5 = "0,000"
                    STRINGA_STAMPA_66 = "0,000"

                    lblISBAR.Text = par.Tronca(ISBARC_R)
                End If

                If ESCLUSIONE <> "" Then
                    STRINGA_STAMPA_7 = "<p><b>Motivo:</b></p>" _
                                     & "<ul>" _
                                     & ESCLUSIONE _
                                     & "</ul>"
                End If


                '***INIZIO STAMPA

                Dim DATI_ANAGRAFICI As String
                Dim sa1 As String
                Dim sA2 As String
                Dim sA3 As String
                Dim sA4 As String
                Dim k1 As String
                Dim i1 As String
                Dim i2 As String
                Dim m1 As String
                Dim m2 As String
                Dim sc1 As String = ""
                Dim sc2 As String = ""
                Dim sc3 As String = ""
                Dim sc4 As String = ""
                Dim sc5 As String = ""
                Dim sc6 As String = ""
                Dim i1a As String = ""
                Dim i1b As String = ""
                Dim i1c As String = ""
                Dim i2a As String = ""
                Dim i2b As String = ""
                Dim i2c As String = ""
                Dim i2d As String = ""
                Dim i3a As String = ""
                Dim i3b As String = ""
                Dim i3c As String = ""
                Dim i3d As String = ""
                Dim i3e As String = ""
                Dim i4a As String = ""
                Dim i4b As String = ""
                Dim i4c As String = ""
                Dim i5a As String = ""
                Dim i5b As String = ""
                Dim i5c As String = ""
                Dim i5d As String = ""
                Dim i6a As String = ""
                Dim i6b As String = ""
                Dim i6c As String = ""
                Dim i7a As String = ""
                Dim i7b As String = ""
                Dim i7c As String = ""
                Dim i8a1 As String = ""
                Dim i8a2 As String = ""
                Dim i8b As String = ""
                Dim i8c As String = ""
                Dim i8d As String = ""

                Dim LOCAZIONE As String
                Dim ACCESSORIE As String

                Dim i8e As String = ""
                Dim i8f As String = ""
                Dim i9a As String = ""
                Dim i9b As String = ""
                Dim i9c As String = ""
                Dim i9d As String = ""
                Dim i10a As String = ""
                Dim i10b As String = ""
                Dim i10c As String = ""
                Dim i11a As String = ""
                Dim i11b As String = ""
                Dim i11c As String = ""
                Dim i12a As String = ""
                Dim i12b As String = ""
                Dim i12c As String = ""
                Dim i13a As String = ""
                Dim i13b As String = ""
                Dim i14a As String = ""
                Dim i14b As String = ""
                Dim i15a As String = ""
                Dim i15b As String = ""
                Dim i16a As String = ""
                Dim i16b As String = ""

                Dim protocollo As String

                Dim SITUAZIONE_REDDITUALE As String
                Dim SITUAZIONE_PATRIMONIALE As String

                Dim sISEE_ERP As String
                Dim RISULTATI_INTERMEDI As String
                Dim sISBARC_R As String
                Dim sISBARC As String
                Dim testo As String
                Dim Motivo As String

                Dim pg_dichiarazione As String

                Dim DATA_PRESENTA_DICH As String = ""
                Dim LUOGO_PRESENTA_DICH As String = ""
                Dim DATA_STAMPA As String = ""
                Dim ID_DOMANDA As String
                Dim DATA_STAMPA_DOMANDA As String = ""
                Dim PUNTEGGI_INTERMEDI As String = ""
                Dim sISBAR As String


                DATI_ANAGRAFICI = ""

                If CType(Dom_Richiedente1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text = "ITALIA" Then
                    DATI_ANAGRAFICI = "Il/La Sottoscritto/a " & CType(Dom_Richiedente1.FindControl("txtCognome"), TextBox).Text & " " _
                                    & "" & CType(Dom_Richiedente1.FindControl("txtNome"), TextBox).Text & "<BR>" _
                                    & "sesso " & CType(Dom_Richiedente1.FindControl("cmbSesso"), DropDownList).SelectedItem.Text & ", codice fiscale " & CType(Dom_Richiedente1.FindControl("txtCF"), TextBox).Text _
                                    & " , nato/a il " & CType(Dom_Richiedente1.FindControl("txtDataNascita"), TextBox).Text & "" _
                                    & "<BR>" _
                                    & "comune di " & CType(Dom_Richiedente1.FindControl("cmbComuneNas"), DropDownList).SelectedItem.Text & " , provincia di " & CType(Dom_Richiedente1.FindControl("cmbPrNas"), DropDownList).SelectedItem.Text _
                                    & "<BR>"

                Else
                    DATI_ANAGRAFICI = "Il/La Sottoscritto/a " & CType(Dom_Richiedente1.FindControl("txtCognome"), TextBox).Text & " " _
                                    & "" & CType(Dom_Richiedente1.FindControl("txtNome"), TextBox).Text & "<BR>" _
                                    & "sesso " & CType(Dom_Richiedente1.FindControl("cmbSesso"), DropDownList).SelectedItem.Text & ", codice fiscale " & CType(Dom_Richiedente1.FindControl("txtCF"), TextBox).Text _
                                    & " , nato/a il " & CType(Dom_Richiedente1.FindControl("txtDataNascita"), TextBox).Text & "" _
                                    & "<BR>" _
                                    & "Stato Estero " & CType(Dom_Richiedente1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text _
                                    & "<BR>"

                End If

                If CType(Dom_Richiedente1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text = "ITALIA" Then
                    DATI_ANAGRAFICI = DATI_ANAGRAFICI _
                    & "e residente nel comune di " & CType(Dom_Richiedente1.FindControl("cmbComuneRes"), DropDownList).SelectedItem.Text & " , provincia di " & CType(Dom_Richiedente1.FindControl("cmbPrRes"), DropDownList).SelectedItem.Text & " , cap " & CType(Dom_Richiedente1.FindControl("txtCAPRes"), TextBox).Text & "<BR>" _
                    & "indirizzo " & CType(Dom_Richiedente1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text & " " & CType(Dom_Richiedente1.FindControl("txtIndRes"), TextBox).Text & " , n. civico " & CType(Dom_Richiedente1.FindControl("txtCivicoRes"), TextBox).Text & " , n. telefono " & CType(Dom_Richiedente1.FindControl("txtTelRes"), TextBox).Text & "<BR>"
                Else
                    DATI_ANAGRAFICI = DATI_ANAGRAFICI _
                    & "e residente nello stato " & CType(Dom_Richiedente1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text & " , cap " & CType(Dom_Richiedente1.FindControl("txtCAPRes"), TextBox).Text & "<BR>" _
                    & "indirizzo " & CType(Dom_Richiedente1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text & " " & CType(Dom_Richiedente1.FindControl("txtIndRes"), TextBox).Text & " , n. civico " & CType(Dom_Richiedente1.FindControl("txtCivicoRes"), TextBox).Text & " , n. telefono " & CType(Dom_Richiedente1.FindControl("txtTelRes"), TextBox).Text & "<BR>"
                End If

                protocollo = lblSPG.Text & "-" & lblPG.Text & "-" & Label7.Text
                pg_dichiarazione = lblPGDic.Text

                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI WHERE id='" & lIdDichiarazione & "'"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    LUOGO_PRESENTA_DICH = par.IfNull(myReader("LUOGO"), "")
                    DATA_PRESENTA_DICH = par.FormattaData(par.IfNull(myReader("DATA"), ""))

                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM EVENTI_DICHIARAZIONI WHERE ID_PRATICA=" & lIdDichiarazione & " AND COD_EVENTO='F132'"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    DATA_STAMPA = par.FormattaData(Mid(par.IfNull(myReader("DATA_ORA"), ""), 1, 8))

                End If
                myReader.Close()

                Dim DATI_BANDO As String = ""
                par.cmd.CommandText = "SELECT * FROM BANDI WHERE ID=" & glIndice_Bando_Origine
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    DATI_BANDO = par.IfNull(myReader("DESCRIZIONE"), "")
                    'Select Case par.IfNull(myReader("TIPO_BANDO"), "")
                    '    Case 0
                    '        DATI_BANDO = DATI_BANDO & " I° Semestre"
                    '    Case 1
                    '        DATI_BANDO = DATI_BANDO & " II° Semestre"
                    '    Case 2
                    '        DATI_BANDO = DATI_BANDO & " Annuale"
                    'End Select
                    'DATI_BANDO = DATI_BANDO & " " & Mid(par.IfNull(myReader("DATA_INIZIO"), ""), 1, 4)
                End If
                myReader.Close()

                'If glIndice_Bando_Origine <> lIndice_Bando Then
                '    DATI_BANDO = DATI_BANDO & " / <b>Aggiornamento del " & Format(Now, "dd/MM/yyyy") & "  - " & DescrizioneBandoAggiornamento & "</b>"
                'End If
                If Fl_Integrazione = "1" Then
                    DATI_BANDO = DATI_BANDO & " / <b>Aggiornamento del " & Format(Now, "dd/MM/yyyy") & "  - " & DescrizioneBandoAggiornamento & "</b>"
                End If

                ID_DOMANDA = protocollo & " del " & txtDataPG.Text
                DATA_STAMPA_DOMANDA = Format(Now, "dd/MM/yyyy")

                If Valore01(CType(Dom_Dichiara1.FindControl("CF1"), CheckBox).Checked) = "0" Then
                    sa1 = "<img src=block.gif width=10 height=10 border=1>"
                Else
                    sa1 = "<img src=block_checked.gif width=10 height=10 border=1>"
                End If

                If Valore01(CType(Dom_Dichiara1.FindControl("CF2"), CheckBox).Checked) = "0" Then
                    sA2 = "<img src=block.gif width=10 height=10 border=1>"
                Else
                    sA2 = "<img src=block_checked.gif width=10 height=10 border=1>"
                End If

                If Valore01(CType(Dom_Dichiara1.FindControl("CF3"), CheckBox).Checked) = "0" Then
                    sA3 = "<img src=block.gif width=10 height=10 border=1>"
                Else
                    sA3 = "<img src=block_checked.gif width=10 height=10 border=1>"
                End If

                If Valore01(CType(Dom_Dichiara1.FindControl("CF4"), CheckBox).Checked) = "0" Then
                    sA4 = "<img src=block.gif width=10 height=10 border=1>"
                Else
                    sA4 = "<img src=block_checked.gif width=10 height=10 border=1>"
                End If

                SITUAZIONE_REDDITUALE = STRINGA_STAMPA
                SITUAZIONE_PATRIMONIALE = STRINGA_STAMPA_1
                sISEE_ERP = STRINGA_STAMPA_2
                RISULTATI_INTERMEDI = STRINGA_STAMPA_3
                PUNTEGGI_INTERMEDI = STRINGA_STAMPA_4
                sISBAR = STRINGA_STAMPA_5
                sISBARC = STRINGA_STAMPA_6
                sISBARC_R = STRINGA_STAMPA_66
                Motivo = STRINGA_STAMPA_7

                If TIPO_ALLOGGIO = 0 Then
                    If STRINGA_STAMPA_7 = "" Then
                        If ISEE_ERP > 16000 And ISE_ERP <= 17000 Then
                            testo = "<b>DOMANDA IDONEA per assegnazione di alloggi di cui all'articolo 1, comma 3, lettera a e lettera b).</b>"
                        Else
                            testo = "<b>DOMANDA IDONEA per assegnazione di alloggi di cui all'articolo 1, comma 3, lettera a ).</b>"
                        End If
                    Else
                        testo = "<b>DOMANDA NON IDONEA</b>"
                    End If
                Else
                    If STRINGA_STAMPA_7 = "" Then
                        testo = "<b>DOMANDA IDONEA per assegnazione di alloggi di cui all'articolo 1, comma 3, lettera b).</b>"
                    Else
                        testo = "<b>DOMANDA NON IDONEA</b>"
                    End If
                End If


                Select Case CType(Dom_Dichiara1.FindControl("cmbPresentaD"), DropDownList).SelectedIndex

                    Case 0
                        sc1 = "<img src=block_checked.gif width=10 height=10 border=1>"
                        sc2 = "<img src=block.gif width=10 height=10 border=1>"
                        sc3 = "<img src=block.gif width=10 height=10 border=1>"
                        sc4 = "<img src=block.gif width=10 height=10 border=1>"
                        sc5 = "<img src=block.gif width=10 height=10 border=1>"
                        sc6 = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        sc2 = "<img src=block_checked.gif width=10 height=10 border=1>"
                        sc1 = "<img src=block.gif width=10 height=10 border=1>"
                        sc3 = "<img src=block.gif width=10 height=10 border=1>"
                        sc4 = "<img src=block.gif width=10 height=10 border=1>"
                        sc5 = "<img src=block.gif width=10 height=10 border=1>"
                        sc6 = "<img src=block.gif width=10 height=10 border=1>"
                    Case 2
                        sc3 = "<img src=block_checked.gif width=10 height=10 border=1>"
                        sc2 = "<img src=block.gif width=10 height=10 border=1>"
                        sc1 = "<img src=block.gif width=10 height=10 border=1>"
                        sc4 = "<img src=block.gif width=10 height=10 border=1>"
                        sc5 = "<img src=block.gif width=10 height=10 border=1>"
                        sc6 = "<img src=block.gif width=10 height=10 border=1>"
                    Case 3
                        sc4 = "<img src=block_checked.gif width=10 height=10 border=1>"
                        sc2 = "<img src=block.gif width=10 height=10 border=1>"
                        sc3 = "<img src=block.gif width=10 height=10 border=1>"
                        sc1 = "<img src=block.gif width=10 height=10 border=1>"
                        sc5 = "<img src=block.gif width=10 height=10 border=1>"
                        sc6 = "<img src=block.gif width=10 height=10 border=1>"
                    Case 4
                        sc5 = "<img src=block_checked.gif width=10 height=10 border=1>"
                        sc2 = "<img src=block.gif width=10 height=10 border=1>"
                        sc3 = "<img src=block.gif width=10 height=10 border=1>"
                        sc4 = "<img src=block.gif width=10 height=10 border=1>"
                        sc1 = "<img src=block.gif width=10 height=10 border=1>"
                        sc6 = "<img src=block.gif width=10 height=10 border=1>"
                    Case 5
                        sc6 = "<img src=block_checked.gif width=10 height=10 border=1>"
                        sc2 = "<img src=block.gif width=10 height=10 border=1>"
                        sc3 = "<img src=block.gif width=10 height=10 border=1>"
                        sc4 = "<img src=block.gif width=10 height=10 border=1>"
                        sc5 = "<img src=block.gif width=10 height=10 border=1>"
                        sc1 = "<img src=block.gif width=10 height=10 border=1>"
                End Select

                If Valore01(CType(Dom_Dichiara1.FindControl("cfProfugo"), CheckBox).Checked) = "0" Then
                    k1 = "<img src=block.gif width=10 height=10 border=1>"
                Else
                    k1 = "<img src=block_checked.gif width=10 height=10 border=1>"
                End If


                i1 = CType(Dom_Abitative_2_1.FindControl("txtAnnoCanone"), TextBox).Text
                i2 = CType(Dom_Abitative_2_1.FindControl("txtSpese"), TextBox).Text


                m1 = CType(Dom_Abitative_2_1.FindControl("txtAnnoAcc"), TextBox).Text

                m2 = CType(Dom_Abitative_2_1.FindControl("txtSpeseAcc"), TextBox).Text



                'Dim Ia As String

                Select Case CType(Dom_Familiari1.FindControl("cmbF1"), DropDownList).SelectedIndex
                    Case 0
                        i1a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i1b = "<img src=block.gif width=10 height=10 border=1>"
                        i1c = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        i1b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i1a = "<img src=block.gif width=10 height=10 border=1>"
                        i1c = "<img src=block.gif width=10 height=10 border=1>"
                    Case 2
                        i1c = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i1b = "<img src=block.gif width=10 height=10 border=1>"
                        i1a = "<img src=block.gif width=10 height=10 border=1>"
                End Select


                Select Case CType(Dom_Familiari1.FindControl("cmbF2"), DropDownList).SelectedIndex
                    Case 0
                        i2a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i2b = "<img src=block.gif width=10 height=10 border=1>"
                        i2c = "<img src=block.gif width=10 height=10 border=1>"
                        i2d = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        i2b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i2a = "<img src=block.gif width=10 height=10 border=1>"
                        i2c = "<img src=block.gif width=10 height=10 border=1>"
                        i2d = "<img src=block.gif width=10 height=10 border=1>"
                    Case 2
                        i2c = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i2b = "<img src=block.gif width=10 height=10 border=1>"
                        i2a = "<img src=block.gif width=10 height=10 border=1>"
                        i2d = "<img src=block.gif width=10 height=10 border=1>"
                    Case 3
                        i2d = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i2b = "<img src=block.gif width=10 height=10 border=1>"
                        i2c = "<img src=block.gif width=10 height=10 border=1>"
                        i2a = "<img src=block.gif width=10 height=10 border=1>"
                End Select


                Select Case CType(Dom_Familiari1.FindControl("cmbF3"), DropDownList).SelectedIndex
                    Case 0
                        i3a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i3b = "<img src=block.gif width=10 height=10 border=1>"
                        i3c = "<img src=block.gif width=10 height=10 border=1>"
                        i3d = "<img src=block.gif width=10 height=10 border=1>"
                        i3e = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        i3b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i3a = "<img src=block.gif width=10 height=10 border=1>"
                        i3c = "<img src=block.gif width=10 height=10 border=1>"
                        i3d = "<img src=block.gif width=10 height=10 border=1>"
                        i3e = "<img src=block.gif width=10 height=10 border=1>"
                    Case 2
                        i3c = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i3b = "<img src=block.gif width=10 height=10 border=1>"
                        i3a = "<img src=block.gif width=10 height=10 border=1>"
                        i3d = "<img src=block.gif width=10 height=10 border=1>"
                        i3e = "<img src=block.gif width=10 height=10 border=1>"
                    Case 3
                        i3d = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i3b = "<img src=block.gif width=10 height=10 border=1>"
                        i3c = "<img src=block.gif width=10 height=10 border=1>"
                        i3a = "<img src=block.gif width=10 height=10 border=1>"
                        i3e = "<img src=block.gif width=10 height=10 border=1>"
                    Case 4
                        i3e = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i3b = "<img src=block.gif width=10 height=10 border=1>"
                        i3c = "<img src=block.gif width=10 height=10 border=1>"
                        i3d = "<img src=block.gif width=10 height=10 border=1>"
                        i3a = "<img src=block.gif width=10 height=10 border=1>"
                End Select

                Select Case CType(Dom_Familiari1.FindControl("cmbF4"), DropDownList).SelectedIndex
                    Case 0
                        i4a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i4b = "<img src=block.gif width=10 height=10 border=1>"
                        i4c = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        i4b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i4a = "<img src=block.gif width=10 height=10 border=1>"
                        i4c = "<img src=block.gif width=10 height=10 border=1>"
                    Case 2
                        i4c = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i4b = "<img src=block.gif width=10 height=10 border=1>"
                        i4a = "<img src=block.gif width=10 height=10 border=1>"
                End Select


                Select Case CType(Dom_Familiari1.FindControl("cmbF5"), DropDownList).SelectedIndex
                    Case 0
                        i5a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i5b = "<img src=block.gif width=10 height=10 border=1>"
                        i5c = "<img src=block.gif width=10 height=10 border=1>"
                        i5d = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        i5b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i5a = "<img src=block.gif width=10 height=10 border=1>"
                        i5c = "<img src=block.gif width=10 height=10 border=1>"
                        i5d = "<img src=block.gif width=10 height=10 border=1>"
                    Case 2
                        i5c = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i5b = "<img src=block.gif width=10 height=10 border=1>"
                        i5a = "<img src=block.gif width=10 height=10 border=1>"
                        i5d = "<img src=block.gif width=10 height=10 border=1>"
                    Case 3
                        i5d = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i5b = "<img src=block.gif width=10 height=10 border=1>"
                        i5c = "<img src=block.gif width=10 height=10 border=1>"
                        i5a = "<img src=block.gif width=10 height=10 border=1>"
                End Select

                Select Case CType(Dom_Familiari1.FindControl("cmbF6"), DropDownList).SelectedIndex
                    Case 0
                        i6a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i6b = "<img src=block.gif width=10 height=10 border=1>"
                        i6c = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        i6b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i6a = "<img src=block.gif width=10 height=10 border=1>"
                        i6c = "<img src=block.gif width=10 height=10 border=1>"
                    Case 2
                        i6c = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i6b = "<img src=block.gif width=10 height=10 border=1>"
                        i6a = "<img src=block.gif width=10 height=10 border=1>"
                End Select

                Select Case CType(Dom_Familiari1.FindControl("cmbF7"), DropDownList).SelectedIndex
                    Case 0
                        i7a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i7b = "<img src=block.gif width=10 height=10 border=1>"
                        i7c = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        i7b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i7a = "<img src=block.gif width=10 height=10 border=1>"
                        i7c = "<img src=block.gif width=10 height=10 border=1>"
                    Case 2
                        i7c = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i7b = "<img src=block.gif width=10 height=10 border=1>"
                        i7a = "<img src=block.gif width=10 height=10 border=1>"
                End Select



                Select Case CType(Dom_Abitative_1_1.FindControl("cmbA1"), DropDownList).SelectedIndex
                    Case 0
                        i8a1 = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i8a2 = "<img src=block.gif width=10 height=10 border=1>"
                        i8b = "<img src=block.gif width=10 height=10 border=1>"
                        i8c = "<img src=block.gif width=10 height=10 border=1>"
                        i8e = "<img src=block.gif width=10 height=10 border=1>"
                        i8f = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        i8a2 = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i8a1 = "<img src=block.gif width=10 height=10 border=1>"
                        i8b = "<img src=block.gif width=10 height=10 border=1>"
                        i8c = "<img src=block.gif width=10 height=10 border=1>"
                        i8e = "<img src=block.gif width=10 height=10 border=1>"
                        i8f = "<img src=block.gif width=10 height=10 border=1>"
                    Case 2
                        i8b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i8a2 = "<img src=block.gif width=10 height=10 border=1>"
                        i8a1 = "<img src=block.gif width=10 height=10 border=1>"
                        i8c = "<img src=block.gif width=10 height=10 border=1>"
                        i8e = "<img src=block.gif width=10 height=10 border=1>"
                        i8f = "<img src=block.gif width=10 height=10 border=1>"
                    Case 3
                        i8c = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i8a2 = "<img src=block.gif width=10 height=10 border=1>"
                        i8b = "<img src=block.gif width=10 height=10 border=1>"
                        i8a1 = "<img src=block.gif width=10 height=10 border=1>"
                        i8e = "<img src=block.gif width=10 height=10 border=1>"
                        i8f = "<img src=block.gif width=10 height=10 border=1>"
                    Case 4
                        i8e = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i8a2 = "<img src=block.gif width=10 height=10 border=1>"
                        i8b = "<img src=block.gif width=10 height=10 border=1>"
                        i8c = "<img src=block.gif width=10 height=10 border=1>"
                        i8a1 = "<img src=block.gif width=10 height=10 border=1>"
                        i8f = "<img src=block.gif width=10 height=10 border=1>"
                    Case Else
                        i8f = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i8a2 = "<img src=block.gif width=10 height=10 border=1>"
                        i8b = "<img src=block.gif width=10 height=10 border=1>"
                        i8c = "<img src=block.gif width=10 height=10 border=1>"
                        i8a1 = "<img src=block.gif width=10 height=10 border=1>"
                        i8e = "<img src=block.gif width=10 height=10 border=1>"
                End Select


                If Valore01(CType(Dom_Abitative_1_1.FindControl("chMorosita"), CheckBox).Checked) = "0" And Valore01(CType(Dom_Abitative_1_1.FindControl("chMG"), CheckBox).Checked) = "1" Then
                    i8d = "<img src=block_checked.gif width=10 height=10 border=1>"

                    LOCAZIONE = ""
                    ACCESSORIE = ""

                Else
                    If Valore01(CType(Dom_Abitative_1_1.FindControl("chMorosita"), CheckBox).Checked) = "0" And Valore01(CType(Dom_Abitative_1_1.FindControl("chMG"), CheckBox).Checked) = "0" Then
                        i8d = "<img src=block_checked.gif width=10 height=10 border=1>"

                        LOCAZIONE = ""
                        ACCESSORIE = ""
                    Else
                        i8d = "<img src=block.gif width=10 height=10 border=1>"

                        LOCAZIONE = "" 'CType(Dom_Dichiara1.FindControl("txtSpese"), TextBox).Text & ",00"
                        ACCESSORIE = "" 'CType(Dom_Dichiara1.FindControl("txtSpeseAcc"), TextBox).Text & ",00"
                    End If
                End If

                Select Case CType(Dom_Abitative_1_1.FindControl("cmbA2"), DropDownList).SelectedIndex
                    Case 0
                        i9a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i9b = "<img src=block.gif width=10 height=10 border=1>"
                        i9c = "<img src=block.gif width=10 height=10 border=1>"
                        i9d = "<img src=block.gif width=10 height=10 border=1>"

                    Case 1
                        i9b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i9a = "<img src=block.gif width=10 height=10 border=1>"
                        i9c = "<img src=block.gif width=10 height=10 border=1>"
                        i9d = "<img src=block.gif width=10 height=10 border=1>"
                    Case 2
                        i9c = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i9b = "<img src=block.gif width=10 height=10 border=1>"
                        i9a = "<img src=block.gif width=10 height=10 border=1>"
                        i9d = "<img src=block.gif width=10 height=10 border=1>"
                    Case 3
                        i9d = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i9b = "<img src=block.gif width=10 height=10 border=1>"
                        i9c = "<img src=block.gif width=10 height=10 border=1>"
                        i9a = "<img src=block.gif width=10 height=10 border=1>"
                End Select

                Select Case CType(Dom_Abitative_1_1.FindControl("cmbA3"), DropDownList).SelectedIndex
                    Case 0
                        i10a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i10b = "<img src=block.gif width=10 height=10 border=1>"
                        i10c = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        i10b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i10a = "<img src=block.gif width=10 height=10 border=1>"
                        i10c = "<img src=block.gif width=10 height=10 border=1>"
                    Case 2
                        i10c = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i10b = "<img src=block.gif width=10 height=10 border=1>"
                        i10a = "<img src=block.gif width=10 height=10 border=1>"
                End Select


                Select Case CType(Dom_Abitative_1_1.FindControl("cmbA4"), DropDownList).SelectedIndex
                    Case 0
                        i11a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i11b = "<img src=block.gif width=10 height=10 border=1>"
                        i11c = "<img src=block.gif width=10 height=10 border=1>"

                    Case 1
                        i11b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i11a = "<img src=block.gif width=10 height=10 border=1>"
                        i11c = "<img src=block.gif width=10 height=10 border=1>"
                    Case 2
                        i11c = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i11b = "<img src=block.gif width=10 height=10 border=1>"
                        i11a = "<img src=block.gif width=10 height=10 border=1>"
                End Select

                Select Case CType(Dom_Abitative_1_1.FindControl("cmbA5"), DropDownList).SelectedIndex
                    Case 0
                        i12a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i12b = "<img src=block.gif width=10 height=10 border=1>"
                        i12c = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        i12b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i12a = "<img src=block.gif width=10 height=10 border=1>"
                        i12c = "<img src=block.gif width=10 height=10 border=1>"
                    Case 2
                        i12c = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i12b = "<img src=block.gif width=10 height=10 border=1>"
                        i12a = "<img src=block.gif width=10 height=10 border=1>"
                End Select

                Select Case CType(Dom_Abitative_2_1.FindControl("cmbA6"), DropDownList).SelectedIndex
                    Case 0
                        i13a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i13b = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        i13b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i13a = "<img src=block.gif width=10 height=10 border=1>"
                End Select

                Select Case CType(Dom_Abitative_2_1.FindControl("cmbA7"), DropDownList).SelectedIndex
                    Case 0
                        i14a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i14b = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        i14b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i14a = "<img src=block.gif width=10 height=10 border=1>"
                End Select


                Select Case CType(Dom_Abitative_2_1.FindControl("cmbA8"), DropDownList).SelectedIndex
                    Case 0
                        i15a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i15b = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        i15b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i15a = "<img src=block.gif width=10 height=10 border=1>"
                End Select

                Select Case CType(Dom_Abitative_2_1.FindControl("cmbA9"), DropDownList).SelectedIndex
                    Case 0
                        i16a = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i16b = "<img src=block.gif width=10 height=10 border=1>"
                    Case 1
                        i16b = "<img src=block_checked.gif width=10 height=10 border=1>"
                        i16a = "<img src=block.gif width=10 height=10 border=1>"
                End Select

                Dim CANONE_LOCAZIONE As String

                If Val(i2) > 0 Then
                    CANONE_LOCAZIONE = " < TR ><td><font face='Arial' size='2'>3)</font></td>" _
                                     & "<TD colspan='3'><font face='Arial' size='2'>che per l'abitazione occupata in locazione" _
                                     & " come residenza principale al momento di presentazione" _
                                     & " della domanda il canone di locazione per" _
                                     & " l'anno &lt;" & i1 & "&gt; è di euro " & i2 & ";</font></td>" _
                                     & "</tr>" _
                                     & "<tr>" _
                                     & "<td><font face='Arial' size='2'>4)</font></td>" _
                                     & "<TD colspan='3'><font face='Arial' size='2'>che per l'abitazione di cui al comma precedente" _
                                     & " le spese accessorie di competenza per l'anno" _
                                     & "&lt;" & m1 & "&gt; sono di euro " & m2 & ";</font></td>" _
                                     & "</tr>"
                Else
                    CANONE_LOCAZIONE = ""
                End If


                Dim SstringaSql As String
                Dim CODICEANAGRAFICO As String = ""

                par.cmd.CommandText = "SELECT operatori.*,caf_web.cod_caf as ENTE from operatori,caf_web where operatori.id_caf=caf_web.id and operatori.ID=" & Session.Item("ID_OPERATORE")
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    CODICEANAGRAFICO = par.IfNull(myReader("ENTE"), "") & " - " & par.IfNull(myReader("COD_ANA"), "")
                End If
                myReader.Close()

                'Session.Item("OPERATORE")


                SstringaSql = ""

                SstringaSql = SstringaSql & "<html xmlns='http://www.w3.org/1999/xhtml'>" & vbCrLf
                SstringaSql = SstringaSql & "<head>" & vbCrLf
                SstringaSql = SstringaSql & "<meta http-equiv='Content-Style-Type' content='text/css'>" & vbCrLf
                SstringaSql = SstringaSql & "<title>Stampa ERP Comune di Milano</title>" & vbCrLf
                SstringaSql = SstringaSql & "<title>SEPA@Web</title></head>" & vbCrLf
                SstringaSql = SstringaSql & "<BODY>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<table border='0' cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='50%'><img border='0' src='logo.gif' width='166' height='104'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='50%'>" & vbCrLf
                SstringaSql = SstringaSql & "<p align='center'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & vbCrLf
                SstringaSql = SstringaSql & "<img border='0' src='marca_bollo.gif' width='65' height='81'></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='50%' valign='top'><img border='0' src='Settore.gif' width='254' height='30'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='50%'>" & vbCrLf
                SstringaSql = SstringaSql & "<p align='center'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<div align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<center>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<TABLE width='100%' cellspacing='0'>" & vbCrLf
                SstringaSql = SstringaSql & "<TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<TD valign='top' width='100%'>" & vbCrLf
                SstringaSql = SstringaSql & "<p class='titolo' align='center'>&nbsp;</p>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='4'>MILANO" & vbCrLf
                SstringaSql = SstringaSql & "li " & Format(Now, "dd/MM/yyyy") & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "<p class='titolo' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<b><font face='Arial' size='4'>" & vbCrLf
                SstringaSql = SstringaSql & "DOMANDA DI ASSEGNAZIONE ALLOGGI ERP<BR>" & vbCrLf
                SstringaSql = SstringaSql & "(RR 10 febbraio 2004 n 1)</font></b></p>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "&nbsp;" & vbCrLf
                SstringaSql = SstringaSql & "<p>" & DATI_BANDO & "&nbsp;</P><P>" & protocollo & " / " & CODICEANAGRAFICO & "</p>" & vbCrLf
                SstringaSql = SstringaSql & "<p>&nbsp;<font face='Arial' size='4'>" & DATI_ANAGRAFICI & "<BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</p>" & vbCrLf
                SstringaSql = SstringaSql & "<p class='titolo' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<b><font face='Arial' size='4'>" & vbCrLf
                SstringaSql = SstringaSql & "CHIEDE</font></b>" & vbCrLf
                SstringaSql = SstringaSql & "</p>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & vbCrLf
                SstringaSql = SstringaSql & "l'assegnazione di un alloggio di edilizia" & vbCrLf
                SstringaSql = SstringaSql & "residenziale pubblica a" & vbCrLf
                SstringaSql = SstringaSql & "canone sociale" & vbCrLf
                SstringaSql = SstringaSql & "ai sensi" & vbCrLf
                SstringaSql = SstringaSql & "del RR 10 febbraio 2004 n.1" & vbCrLf
                SstringaSql = SstringaSql & "<BR>" & vbCrLf
                SstringaSql = SstringaSql & "A tal fine" & vbCrLf
                SstringaSql = SstringaSql & "ai sensi del DPR 28 dicembre" & vbCrLf
                SstringaSql = SstringaSql & "2000" & vbCrLf
                SstringaSql = SstringaSql & "n. 445" & vbCrLf
                SstringaSql = SstringaSql & "sotto la propria responsabilità" & vbCrLf
                SstringaSql = SstringaSql & "e nella consapevolezza delle conseguenze" & vbCrLf
                SstringaSql = SstringaSql & "penali in caso di dichiarazione mendace" & vbCrLf
                SstringaSql = SstringaSql & "<BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "<p class='titolo' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<b><font face='Arial' size='4'>" & vbCrLf
                SstringaSql = SstringaSql & "DICHIARA</font></b>" & vbCrLf
                SstringaSql = SstringaSql & "</p>" & vbCrLf
                SstringaSql = SstringaSql & "<TABLE width='100%' cellpadding='2' cellspacing='0'>" & vbCrLf
                SstringaSql = SstringaSql & "<TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><font face='Arial' size='3'>1)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='3' width='1117'><font face='Arial' size='3'>che il proprio nucleo familiare è composto" & vbCrLf
                SstringaSql = SstringaSql & "così come indicato nella dichiarazione sostituiva" & vbCrLf
                SstringaSql = SstringaSql & "allegata</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & sa1 & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='3'>che trattasi di nucleo familiare di nuova" & vbCrLf
                SstringaSql = SstringaSql & "formazione costituito con atto di matrimonio" & vbCrLf
                SstringaSql = SstringaSql & "o per convivenza more uxorio entro due anni" & vbCrLf
                SstringaSql = SstringaSql & "dalla data della domanda con/senza figli" & vbCrLf
                SstringaSql = SstringaSql & "minorenni o minori anche legalmente separati.</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & sA2 & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='3'>che trattasi di nucleo familiare composto" & vbCrLf
                SstringaSql = SstringaSql & "da uno o da entrambi i coniugi e dai figli" & vbCrLf
                SstringaSql = SstringaSql & "legittimi" & vbCrLf
                SstringaSql = SstringaSql & "naturali riconosciuti o adottivi" & vbCrLf
                SstringaSql = SstringaSql & "con loro conviventi" & vbCrLf
                SstringaSql = SstringaSql & "ovvero costituita da" & vbCrLf
                SstringaSql = SstringaSql & "una persona sola.</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='45' valign='top'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & sA3 & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1005'><font face='Arial' size='3'>Che nel nucleo familiare sono presenti conviventi" & vbCrLf
                SstringaSql = SstringaSql & "(ascendenti" & vbCrLf
                SstringaSql = SstringaSql & "discendenti" & vbCrLf
                SstringaSql = SstringaSql & "collaterali fino" & vbCrLf
                SstringaSql = SstringaSql & "al terzo grado" & vbCrLf
                SstringaSql = SstringaSql & "affini fino al secondo grado)" & vbCrLf
                SstringaSql = SstringaSql & "anche more uxorio" & vbCrLf
                SstringaSql = SstringaSql & "la convivenza dei quali" & vbCrLf
                SstringaSql = SstringaSql & "dura da almeno un anno oppure sono presenti" & vbCrLf
                SstringaSql = SstringaSql & "soggetti affidati con provvedimento dell'Autorità" & vbCrLf
                SstringaSql = SstringaSql & "giudiziaria e di ultrasessantacinquenni o" & vbCrLf
                SstringaSql = SstringaSql & "invalidi o disabili affetti da minorazioni" & vbCrLf
                SstringaSql = SstringaSql & "o malattie invalidanti" & vbCrLf
                SstringaSql = SstringaSql & "che comportino una" & vbCrLf
                SstringaSql = SstringaSql & "percentuale di invalidità permanente pari" & vbCrLf
                SstringaSql = SstringaSql & "o superiore al 66%" & vbCrLf
                SstringaSql = SstringaSql & "riconosciuta ai sensi" & vbCrLf
                SstringaSql = SstringaSql & "delle vigenti normative" & vbCrLf
                SstringaSql = SstringaSql & "ovvero con grave" & vbCrLf
                SstringaSql = SstringaSql & "handicap o patologia con prognosi ad esito" & vbCrLf
                SstringaSql = SstringaSql & "infausto.</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='45' valign='top'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & sA4 & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1005'><font face='Arial' size='3'>Che nel nucleo familiare sono presenti altri" & vbCrLf
                SstringaSql = SstringaSql & "conviventi non legati da vincoli di parentela" & vbCrLf
                SstringaSql = SstringaSql & "o affinità" & vbCrLf
                SstringaSql = SstringaSql & "la convivenza dei quali è finalizzata" & vbCrLf
                SstringaSql = SstringaSql & "alla reciproca assistenza morale e materiale" & vbCrLf
                SstringaSql = SstringaSql & "e dura da almeno due anni.<BR>" & vbCrLf
                SstringaSql = SstringaSql & "<BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'><font face='Arial' size='3'>2)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='3' width='1117'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & k1 & " di essere nella condizione di profugo rimpatriato" & vbCrLf
                SstringaSql = SstringaSql & "da non oltre un quinquennio;</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='3' width='1117'><font face='Arial' size='3'>&nbsp;&nbsp;&nbsp;</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='3' width='1117'>" & vbCrLf
                SstringaSql = SstringaSql & "<p style='page-break-after: always' class='mini'><font face='Arial' size='3'>&nbsp;</font></p>" & vbCrLf
                SstringaSql = SstringaSql & "<p align='center'><b><font face='Arial' size='3'>REQUISITI" & vbCrLf
                SstringaSql = SstringaSql & "AMMISSIBILITA'</font></b></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='3' width='1117'><font face='Arial' size='3'>&nbsp;&nbsp;&nbsp;</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'><font face='Arial' size='3'>a)</font></td><td width='55' valign='top'><font face='Arial' size='3'>" & req1 & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='3'>che tutti i componenti" & vbCrLf
                SstringaSql = SstringaSql & "il nucleo familiare sono in possesso della cittadinanza di uno" & vbCrLf
                SstringaSql = SstringaSql & "Stato dell'unione europea oppure sono in possesso della carta di" & vbCrLf
                SstringaSql = SstringaSql & "soggiorno o permesso di soggiorno validi in corso di rinnovo;</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='3' width='1117'><font face='Arial' size='3'>di presentare domanda in quanto:</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top'><font face='Arial' size='3'>" & sc1 & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='3'>residente nel comune;</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top'><font face='Arial' size='3'>" & sc2 & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='3'>presta la propria attività lavorativa nel" & vbCrLf
                SstringaSql = SstringaSql & "comune;</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top'><font face='Arial' size='3'>" & sc3 & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='3'>il comune di residenza e quello in cui presta" & vbCrLf
                SstringaSql = SstringaSql & "la propria attività lavorativa" & vbCrLf
                SstringaSql = SstringaSql & "non hanno" & vbCrLf
                SstringaSql = SstringaSql & "indetto un bando per l'assegnazione" & vbCrLf
                SstringaSql = SstringaSql & "degli" & vbCrLf
                SstringaSql = SstringaSql & "alloggi di erp per due semestri" & vbCrLf
                SstringaSql = SstringaSql & "consecutivi;</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top'><font face='Arial' size='3'>" & sc4 & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='3'>svolgerà una nuova attività lavorativa nel" & vbCrLf
                SstringaSql = SstringaSql & "comune" & vbCrLf
                SstringaSql = SstringaSql & "a seguito della perdita" & vbCrLf
                SstringaSql = SstringaSql & "della precedente" & vbCrLf
                SstringaSql = SstringaSql & "attività lavorativa esclusiva" & vbCrLf
                SstringaSql = SstringaSql & "o principale" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "a causa di ristrutturazione industriale" & vbCrLf
                SstringaSql = SstringaSql & "o" & vbCrLf
                SstringaSql = SstringaSql & "di eventi non a lui imputabili" & vbCrLf
                SstringaSql = SstringaSql & "ovvero svolgerà" & vbCrLf
                SstringaSql = SstringaSql & "la propria attività lavorativa" & vbCrLf
                SstringaSql = SstringaSql & "nel comune" & vbCrLf
                SstringaSql = SstringaSql & "presso nuovi insediamenti o attività" & vbCrLf
                SstringaSql = SstringaSql & "produttive;</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top'><font face='Arial' size='3'>" & sc5 & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='3'>sarà assunto nel comune in base ad accordi" & vbCrLf
                SstringaSql = SstringaSql & "con le organizzazioni sindacali" & vbCrLf
                SstringaSql = SstringaSql & "di settore" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "a seguito di piani di sviluppo" & vbCrLf
                SstringaSql = SstringaSql & "occupazionale" & vbCrLf
                SstringaSql = SstringaSql & "nel comune medesimo;</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top'><font face='Arial' size='3'>" & sc6 & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='2' width='1056'><font face='Arial' size='3'>emigrato italiano all'estero." & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='16'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55'>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='2' width='1056'>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "</center>" & vbCrLf
                SstringaSql = SstringaSql & "</div>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<div align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<center>" & vbCrLf
                SstringaSql = SstringaSql & "<TABLE width='100%' cellpadding='0' cellspacing='0'>" & vbCrLf
                SstringaSql = SstringaSql & "<TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<TD valign='top'>" & vbCrLf
                SstringaSql = SstringaSql & "<TABLE width='100%' cellpadding='2' cellspacing='0'>" & vbCrLf
                SstringaSql = SstringaSql & "<TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top'><font face='Arial' size='3'>c)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='56' valign='top'><font face='Arial' size='3'>" & req2 & "" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1061' valign='top'><font face='Arial' size='3'>che nessun componente del nucleo familiare" & vbCrLf
                SstringaSql = SstringaSql & "indicato nella dichiarazione" & vbCrLf
                SstringaSql = SstringaSql & "sostitutiva" & vbCrLf
                SstringaSql = SstringaSql & "allegata alla data di presentazione" & vbCrLf
                SstringaSql = SstringaSql & "della" & vbCrLf
                SstringaSql = SstringaSql & "domanda ha ottenuto l'assegnazione" & vbCrLf
                SstringaSql = SstringaSql & "in proprietà" & vbCrLf
                SstringaSql = SstringaSql & "immediata o futura di alloggio" & vbCrLf
                SstringaSql = SstringaSql & "realizzato" & vbCrLf
                SstringaSql = SstringaSql & "con contributi pubblici o ha" & vbCrLf
                SstringaSql = SstringaSql & "usufruito di" & vbCrLf
                SstringaSql = SstringaSql & "finanziamenti agevolati in qualunque" & vbCrLf
                SstringaSql = SstringaSql & "forma" & vbCrLf
                SstringaSql = SstringaSql & "concessi dallo Stato e da enti" & vbCrLf
                SstringaSql = SstringaSql & "pubblici (Art.8" & vbCrLf
                SstringaSql = SstringaSql & "comma 1" & vbCrLf
                SstringaSql = SstringaSql & "lett. c RR 1/2004);" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='12' valign='top'><font face='Arial' size='3'>d)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='56' valign='top'><font face='Arial' size='3'>" & req3 & "" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1123' valign='top'><font face='Arial' size='3'>che nessun componente del nucleo familiare" & vbCrLf
                SstringaSql = SstringaSql & "è risultato in precedenza assegnatario" & vbCrLf
                SstringaSql = SstringaSql & "di" & vbCrLf
                SstringaSql = SstringaSql & "alloggio ERP revocato con provvedimento" & vbCrLf
                SstringaSql = SstringaSql & "amministrativo" & vbCrLf
                SstringaSql = SstringaSql & "di decadenza per aver destinato" & vbCrLf
                SstringaSql = SstringaSql & "l'alloggio" & vbCrLf
                SstringaSql = SstringaSql & "o le relative pertinenze ad attività" & vbCrLf
                SstringaSql = SstringaSql & "illecite" & vbCrLf
                SstringaSql = SstringaSql & "(Art.8 comma 1" & vbCrLf
                SstringaSql = SstringaSql & "lett. d RR 1/2004);" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='12' valign='top'><font face='Arial' size='3'>e)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='56' valign='top'><font face='Arial' size='3'>" & req4 & "" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1123' valign='top'><font face='Arial' size='3'>che nessun componente del nucleo familiare" & vbCrLf
                SstringaSql = SstringaSql & "è risultato in precedenza assegnatario" & vbCrLf
                SstringaSql = SstringaSql & "in" & vbCrLf
                SstringaSql = SstringaSql & "locazione semplice di alloggio" & vbCrLf
                SstringaSql = SstringaSql & "ERP ceduto" & vbCrLf
                SstringaSql = SstringaSql & "in tutto o in parte al di fuori" & vbCrLf
                SstringaSql = SstringaSql & "dei casi" & vbCrLf
                SstringaSql = SstringaSql & "previsti dalla legge (Art.8 comma 1" & vbCrLf
                SstringaSql = SstringaSql & "lett. e RR 1/2004);" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='12' valign='top'><font face='Arial' size='3'>g)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='56' valign='top'><font face='Arial' size='3'>" & req5 & "" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1123' valign='top'><font face='Arial' size='3'>che nessun componente del nucleo familiare" & vbCrLf
                SstringaSql = SstringaSql & "indicato nella dichiarazione" & vbCrLf
                SstringaSql = SstringaSql & "sostitutiva" & vbCrLf
                SstringaSql = SstringaSql & "allegata alla data di presentazione" & vbCrLf
                SstringaSql = SstringaSql & "della" & vbCrLf
                SstringaSql = SstringaSql & "domanda è titolare del diritto" & vbCrLf
                SstringaSql = SstringaSql & "di proprietà" & vbCrLf
                SstringaSql = SstringaSql & "o altri diritti reali di godimento" & vbCrLf
                SstringaSql = SstringaSql & "su alloggio" & vbCrLf
                SstringaSql = SstringaSql & "adeguato alle esigenze del nucleo" & vbCrLf
                SstringaSql = SstringaSql & "familiare" & vbCrLf
                SstringaSql = SstringaSql & "nell'ambito del territorio nazionale e all'estero (Art.8" & vbCrLf
                SstringaSql = SstringaSql & "comma 1" & vbCrLf
                SstringaSql = SstringaSql & "lett. g RR 1/2004);" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='12' valign='top'><font face='Arial' size='3'>h)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='56' valign='top'><font face='Arial' size='3'>" & req6 & "" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1123' valign='top'><font face='Arial' size='3'>che nessun componente del nucleo familiare" & vbCrLf
                SstringaSql = SstringaSql & "indicato nella dichiarazione" & vbCrLf
                SstringaSql = SstringaSql & "sostitutiva" & vbCrLf
                SstringaSql = SstringaSql & "allegata è stato sfrattato per" & vbCrLf
                SstringaSql = SstringaSql & "morosità da" & vbCrLf
                SstringaSql = SstringaSql & "alloggi ERP negli ultimi 5 anni" & vbCrLf
                SstringaSql = SstringaSql & "e abbia pagato" & vbCrLf
                SstringaSql = SstringaSql & "le somme dovute all'ente gestore;" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='12' valign='top'><font face='Arial' size='3'>i)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='56' valign='top'><font face='Arial' size='3'>" & req7 & "" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1123' valign='top'><font face='Arial' size='3'>che nessun componente del nucleo familiare indicato nella dichiarazione sostitutiva allegata ha occupato senza titolo alloggi ERP negli ultimi 5 anni;</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='12' valign='top'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1123' colspan='2'><font face='Arial' size='3'><b>Periodo di residenza/lavoro in Lombardia</b></font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='12' valign='top'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1123' colspan='2'><font face='Arial' size='3'>" & CType(Dom_Richiedente1.FindControl("cmbResidenza"), DropDownList).SelectedItem.Text & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='12' valign='top'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1123' colspan='2'>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "<p align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'><b>Dichiara inoltre le condizioni familiari" & vbCrLf
                SstringaSql = SstringaSql & "e abitative di seguito indicate</b>" & vbCrLf
                SstringaSql = SstringaSql & "<BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</p>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "</center>" & vbCrLf
                SstringaSql = SstringaSql & "</div>" & vbCrLf
                SstringaSql = SstringaSql & "<p style='page-break-after: always' class='mini'><font face='Arial' size='3'>&nbsp;</font></p>" & vbCrLf
                SstringaSql = SstringaSql & "<div align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<center>" & vbCrLf
                SstringaSql = SstringaSql & "<TABLE width='100%' cellpadding='0' cellspacing='0'>" & vbCrLf
                SstringaSql = SstringaSql & "<TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td>" & vbCrLf
                SstringaSql = SstringaSql & "<TABLE width='100%' cellpadding='2' cellspacing='0'>" & vbCrLf
                SstringaSql = SstringaSql & "<TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='4' class='titolo'>" & vbCrLf
                SstringaSql = SstringaSql & "<p align='center'><U><b><font face='Arial' size='4'>CONDIZIONI" & vbCrLf
                SstringaSql = SstringaSql & "FAMILIARI</font></b></U><font face='Arial' size='3'><BR><BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font></p>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font face='Arial' size='4'>1)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='4'>ANZIANI</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><font face='Arial' size='3'>Nuclei familiari di non più di due componenti" & vbCrLf
                SstringaSql = SstringaSql & "o persone singole che" & vbCrLf
                SstringaSql = SstringaSql & "alla data di presentazione" & vbCrLf
                SstringaSql = SstringaSql & "della domanda" & vbCrLf
                SstringaSql = SstringaSql & "abbiano superato 65 anni" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "ovvero quando uno dei due componenti" & vbCrLf
                SstringaSql = SstringaSql & "pur" & vbCrLf
                SstringaSql = SstringaSql & "non avendo tale età" & vbCrLf
                SstringaSql = SstringaSql & "sia totalmente inabile" & vbCrLf
                SstringaSql = SstringaSql & "al lavoro" & vbCrLf
                SstringaSql = SstringaSql & "ai sensi delle lett. a) e b) del" & vbCrLf
                SstringaSql = SstringaSql & "punto 6.2 del Bando" & vbCrLf
                SstringaSql = SstringaSql & "o abbia un'età superiore" & vbCrLf
                SstringaSql = SstringaSql & "a 75 anni; tali persone singole o nuclei" & vbCrLf
                SstringaSql = SstringaSql & "familiari possono avere minori a carico.</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i1a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>un componente con età maggiore di 65 anni" & vbCrLf
                SstringaSql = SstringaSql & "e l'altro totalmente inabile al lavoro o" & vbCrLf
                SstringaSql = SstringaSql & "con età maggiore di 75 anni</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i1b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>tutti con età maggiore di 65 anni</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i1c & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>c)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>non sussiste la condizione<BR><BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font face='Arial' size='4'>2)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='4'>DISABILI</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><font face='Arial' size='3'>Nuclei familiari nei quali uno o più componenti" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "anche se anagraficamente non conviventi" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "ma presenti nella domanda" & vbCrLf
                SstringaSql = SstringaSql & "siano affetti" & vbCrLf
                SstringaSql = SstringaSql & "da minorazioni o malattie invalidanti che" & vbCrLf
                SstringaSql = SstringaSql & "comportino un handicap grave (art. 3" & vbCrLf
                SstringaSql = SstringaSql & "comma" & vbCrLf
                SstringaSql = SstringaSql & "3" & vbCrLf
                SstringaSql = SstringaSql & "legge 5 febbraio 1992 n. 104)" & vbCrLf
                SstringaSql = SstringaSql & "ovvero" & vbCrLf
                SstringaSql = SstringaSql & "una percentuale di invalidità certificata" & vbCrLf
                SstringaSql = SstringaSql & "ai sensi della legislazione vigente o dai" & vbCrLf
                SstringaSql = SstringaSql & "competenti organi sanitari regionali. Il" & vbCrLf
                SstringaSql = SstringaSql & "disabile non anagraficamente convivente è" & vbCrLf
                SstringaSql = SstringaSql & "riconosciuto come componente del nucleo familiare" & vbCrLf
                SstringaSql = SstringaSql & "solo in presenza di una richiesta di ricongiungimento" & vbCrLf
                SstringaSql = SstringaSql & "al nucleo familiare del richiedente stesso" & vbCrLf
                SstringaSql = SstringaSql & "che comprenda lo stesso disabile nel nucleo" & vbCrLf
                SstringaSql = SstringaSql & "assegnatario.</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i2a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' align='center' valign='top'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>disabilità al 100% o handicap grave con accompagnamento</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i2b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' align='center' valign='top'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>disabilità al 100% o handicap grave</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i2c & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' align='center' valign='top'><font face='Arial' size='3'>c)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>disabilità dal 66% al 99%</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i2d & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' align='center' valign='top'><font face='Arial' size='3'>d)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>non sussiste la condizione<BR><BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font face='Arial' size='4'>3)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='4'>FAMIGLIA DI NUOVA FORMAZIONE</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><font face='Arial' size='3'>Nuclei familiari" & vbCrLf
                SstringaSql = SstringaSql & "come definiti" & vbCrLf
                SstringaSql = SstringaSql & "al punto 4.1 lett. b del Bando" & vbCrLf
                SstringaSql = SstringaSql & "da costituirsi prima della consegna" & vbCrLf
                SstringaSql = SstringaSql & "dell'alloggio" & vbCrLf
                SstringaSql = SstringaSql & "ovvero costituitisi entro" & vbCrLf
                SstringaSql = SstringaSql & "i due anni precedenti alla data della domanda;" & vbCrLf
                SstringaSql = SstringaSql & "in tali nuclei familiari possono essere presenti" & vbCrLf
                SstringaSql = SstringaSql & "figli minorenni o minori affidati.</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i3a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>giovane coppia con almeno un componente di" & vbCrLf
                SstringaSql = SstringaSql & "età non superiore al trentesimo anno alla" & vbCrLf
                SstringaSql = SstringaSql & "data della domanda e con minori</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i3b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>famiglia di nuova formazione con minori</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i3c & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>c)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>giovane coppia con almeno un componente di" & vbCrLf
                SstringaSql = SstringaSql & "età non superiore al trentesimo anno alla" & vbCrLf
                SstringaSql = SstringaSql & "data della domanda" & vbCrLf
                SstringaSql = SstringaSql & "senza minori</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i3d & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>d)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>famiglia di nuova formazione senza minori</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i3e & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>e)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>non sussiste la condizione<BR><BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font face='Arial' size='4'>4)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='4'>PERSONE SOLE" & vbCrLf
                SstringaSql = SstringaSql & "CON EVENTUALI MINORI A CARICO</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><font face='Arial' size='3'>Nuclei di un componente" & vbCrLf
                SstringaSql = SstringaSql & "con un eventuale" & vbCrLf
                SstringaSql = SstringaSql & "minore o più a carico.</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i4a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>persone sole con uno o più o minori" & vbCrLf
                SstringaSql = SstringaSql & "tutti" & vbCrLf
                SstringaSql = SstringaSql & "a carico</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i4b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>persona sola</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i4c & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>c)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>non sussiste la condizione<BR><BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "</center>" & vbCrLf
                SstringaSql = SstringaSql & "</div>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<div align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<center>" & vbCrLf
                SstringaSql = SstringaSql & "<TABLE width='100%' cellpadding='0' cellspacing='0'>" & vbCrLf
                SstringaSql = SstringaSql & "<TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<TD valign='top'>" & vbCrLf
                SstringaSql = SstringaSql & "<TABLE width='100%' cellpadding='2' cellspacing='0'>" & vbCrLf
                SstringaSql = SstringaSql & "<TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font size='4' face='Arial'>5)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><B><font size='4' face='Arial'>STATO DI DISOCCUPAZIONE</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><font face='Arial' size='3'>Stato di disoccupazione" & vbCrLf
                SstringaSql = SstringaSql & "sopravvenuto successivamente" & vbCrLf
                SstringaSql = SstringaSql & "all'anno di riferimento del reddito e che" & vbCrLf
                SstringaSql = SstringaSql & "perduri all'atto di presentazione della domanda" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "determinando una caduta del reddito complessivo" & vbCrLf
                SstringaSql = SstringaSql & "del nucleo familiare superiore al 50%:</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i5a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>richiedente e altro componente</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i5b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>richiedente o altro componente con età maggiore" & vbCrLf
                SstringaSql = SstringaSql & "di 45 anni</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i5c & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>c)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>richiedente o altro componente con età minore" & vbCrLf
                SstringaSql = SstringaSql & "di 45 anni</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i5d & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>d)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>non sussiste la condizione<BR><BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font face='Arial' size='4'>6)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='4'>RICONGIUNZIONE</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><font face='Arial' size='3'>Nucleo familiare che necessiti di alloggio" & vbCrLf
                SstringaSql = SstringaSql & "idoneo per accogliervi parente disabile</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i6a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>ricongiunzione del concorrente disabile(*)" & vbCrLf
                SstringaSql = SstringaSql & "(dal 74% al 100%) con ascendenti o discendenti" & vbCrLf
                SstringaSql = SstringaSql & "diretti o collaterali di primo grado presenti" & vbCrLf
                SstringaSql = SstringaSql & "nella domanda;</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i6b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>ricongiunzione del concorrente ascendente" & vbCrLf
                SstringaSql = SstringaSql & "o discendente diretto o collaterale di primo" & vbCrLf
                SstringaSql = SstringaSql & "grado con disabile(*) (dal 74% al 100%)" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "residente nel Comune in cui è stata presentata" & vbCrLf
                SstringaSql = SstringaSql & "la domanda;</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i6c & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>c)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>non sussiste la condizione</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='100%' colspan='4' class='piccolo'><font face='Arial' size='3'>(*) Per disabile si considera una persona" & vbCrLf
                SstringaSql = SstringaSql & "con una grave patologia medica" & vbCrLf
                SstringaSql = SstringaSql & "(psico-fisica)" & vbCrLf
                SstringaSql = SstringaSql & "o con grave handicap" & vbCrLf
                SstringaSql = SstringaSql & "attestati" & vbCrLf
                SstringaSql = SstringaSql & "dagli organi" & vbCrLf
                SstringaSql = SstringaSql & "sanitari regionali" & vbCrLf
                SstringaSql = SstringaSql & "continuativi" & vbCrLf
                SstringaSql = SstringaSql & "nel tempo" & vbCrLf
                SstringaSql = SstringaSql & "o con prognosi infausta.<BR><BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font face='Arial' size='4'>7)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='95%' colspan='3'><B><font face='Arial' size='4'>CASI PARTICOLARI</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i7a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>nucleo di un componente" & vbCrLf
                SstringaSql = SstringaSql & "con eventualmente" & vbCrLf
                SstringaSql = SstringaSql & "un minore o più a carico" & vbCrLf
                SstringaSql = SstringaSql & "domiciliato o proveniente" & vbCrLf
                SstringaSql = SstringaSql & "da luoghi di detenzione o comunità terapeutiche</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i7b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>nucleo familiare di emigrato che necessiti" & vbCrLf
                SstringaSql = SstringaSql & "rientrare in Italia</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i7c & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>c)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>non sussiste la condizione</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='4' class='piccolo' width='100%'><font face='Arial' size='3'>(*) E' volontariato il servizio reso in modo" & vbCrLf
                SstringaSql = SstringaSql & "continuativo" & vbCrLf
                SstringaSql = SstringaSql & "senza fini di lucro" & vbCrLf
                SstringaSql = SstringaSql & "attraverso" & vbCrLf
                SstringaSql = SstringaSql & "prestazioni personali" & vbCrLf
                SstringaSql = SstringaSql & "volontarie e gratuite" & vbCrLf
                SstringaSql = SstringaSql & "(LR 24.07.1993" & vbCrLf
                SstringaSql = SstringaSql & "n. 22) anche presso cooperativa" & vbCrLf
                SstringaSql = SstringaSql & "sociale (LR 1.06.1993" & vbCrLf
                SstringaSql = SstringaSql & "n. 16) almeno da tre" & vbCrLf
                SstringaSql = SstringaSql & "anni precedenti alla data di apertura del" & vbCrLf
                SstringaSql = SstringaSql & "bando.<BR>" & vbCrLf
                SstringaSql = SstringaSql & "<BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</tbody>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "</center>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<p style='page-break-after: always' class='mini'><font face='Arial' size='3'>&nbsp;</font></p>" & vbCrLf
                SstringaSql = SstringaSql & "<div align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<center>" & vbCrLf
                SstringaSql = SstringaSql & "<table width='100%' cellpadding='2' cellspacing='0'>" & vbCrLf
                SstringaSql = SstringaSql & "<tbody>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='4' class='titolo' width='100%'>" & vbCrLf
                SstringaSql = SstringaSql & "<p align='center'><U><B><font face='Arial' size='4'>CONDIZIONI ABITATIVE</font></B></U><font face='Arial' size='3'><BR><BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font></p>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font face='Arial' size='4'>8)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='3' width='95%'><B><font face='Arial' size='4'>RILASCIO ALLOGGIO</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='3' width='95%'><font face='Arial' size='3'>Concorrenti che debbano rilasciare l'alloggio" & vbCrLf
                SstringaSql = SstringaSql & "a seguito di ordinanza" & vbCrLf
                SstringaSql = SstringaSql & "sentenza esecutiva" & vbCrLf
                SstringaSql = SstringaSql & "o verbale di conciliazione" & vbCrLf
                SstringaSql = SstringaSql & "ovvero a seguito" & vbCrLf
                SstringaSql = SstringaSql & "di altro provvedimento giudiziario o amministrativo:</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD valign='top' align='center' width='51'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i8a1 & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD valign='top' align='center' width='5%'><font face='Arial' size='3'>a1)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>è stato eseguito il provvedimento di rilascio" & vbCrLf
                SstringaSql = SstringaSql & "da meno di un anno dalla presentazione della" & vbCrLf
                SstringaSql = SstringaSql & "domanda e il nucleo familiare si trova nelle" & vbCrLf
                SstringaSql = SstringaSql & "condizioni di cui al punto 6.9 del Bando per il quale" & vbCrLf
                SstringaSql = SstringaSql & "non si deve considerare il periodo temporale" & vbCrLf
                SstringaSql = SstringaSql & "previsto;</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD valign='top' align='center' width='5%'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i8a2 & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD valign='top' align='center' width='5%'><font face='Arial' size='3'>a2)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1001'><font face='Arial' size='3'>è stato eseguito il provvedimento di rilascio" & vbCrLf
                SstringaSql = SstringaSql & "da meno di un anno dalla presentazione della" & vbCrLf
                SstringaSql = SstringaSql & "domanda e il concorrente ha stipulato un" & vbCrLf
                SstringaSql = SstringaSql & "nuovo contratto di locazione per un alloggio" & vbCrLf
                SstringaSql = SstringaSql & "non avente i requisiti minimi per l'assegnazione" & vbCrLf
                SstringaSql = SstringaSql & "di un alloggio ERP nella Regione" & vbCrLf
                SstringaSql = SstringaSql & "di cui" & vbCrLf
                SstringaSql = SstringaSql & "all'Art.13" & vbCrLf
                SstringaSql = SstringaSql & "comma 9 del RR n. 1/2004;</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i8b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1001'><font face='Arial' size='3'>è decorso" & vbCrLf
                SstringaSql = SstringaSql & "al momento della presentazione" & vbCrLf
                SstringaSql = SstringaSql & "della domanda" & vbCrLf
                SstringaSql = SstringaSql & "il termine fissato" & vbCrLf
                SstringaSql = SstringaSql & "per il" & vbCrLf
                SstringaSql = SstringaSql & "rilascio" & vbCrLf
                SstringaSql = SstringaSql & "ovvero è già stato" & vbCrLf
                SstringaSql = SstringaSql & "notificato l'atto" & vbCrLf
                SstringaSql = SstringaSql & "di precetto ai fini dell'esecuzione</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i8c & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>c)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1001'><font face='Arial' size='3'>è in possesso di titolo esecutivo di sfratto" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "ma non è decorso al momento di" & vbCrLf
                SstringaSql = SstringaSql & "presentazione" & vbCrLf
                SstringaSql = SstringaSql & "della domanda il termine fissato" & vbCrLf
                SstringaSql = SstringaSql & "per il rilascio</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i8d & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>d)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1001'><font face='Arial' size='3'>il provvedimento di rilascio è stato motivato" & vbCrLf
                SstringaSql = SstringaSql & "da morosità." & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i8e & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>e)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='95%'><font face='Arial' size='3'>Rilascio alloggio da parte di coniugi legalmente separati o divorziati</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i8f & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>f)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='95%'><font face='Arial' size='3'>non sussiste la condizione</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='110%' colspan='4'><font face='Arial' size='3'>(In" & vbCrLf
                SstringaSql = SstringaSql & "caso di rilascio per morosità il punteggio è attribuito solo" & vbCrLf
                SstringaSql = SstringaSql & "quando il canone di locazione da corrispondere" & vbCrLf
                SstringaSql = SstringaSql & "integrato delle" & vbCrLf
                SstringaSql = SstringaSql & "spese accessorie" & vbCrLf
                SstringaSql = SstringaSql & "sia stato superiore" & vbCrLf
                SstringaSql = SstringaSql & "di oltre il 5%" & vbCrLf
                SstringaSql = SstringaSql & "all'importo" & vbCrLf
                SstringaSql = SstringaSql & "del canone sopportabile come definito al punto 8 del Bando.&nbsp;" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "<p>&nbsp;</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font face='Arial' size='4'>9)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</center>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<td width='105%' valign='top' align='center' colspan='3'>" & vbCrLf
                SstringaSql = SstringaSql & "<p align='left'><B><font face='Arial' size='4'>CONDIZIONE ABITATIVA IMPROPRIA</font></B>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<center>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i9a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='95%'><font face='Arial' size='3'>richiedenti che dimorino da almeno tre anni" & vbCrLf
                SstringaSql = SstringaSql & "presso strutture di assistenza" & vbCrLf
                SstringaSql = SstringaSql & "o beneficenza" & vbCrLf
                SstringaSql = SstringaSql & "legalmente riconosciute</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i9b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='95%'><font face='Arial' size='3'>richiedenti che dimorino in strutture di" & vbCrLf
                SstringaSql = SstringaSql & "tipo alberghiero a carico di" & vbCrLf
                SstringaSql = SstringaSql & "amministrazioni" & vbCrLf
                SstringaSql = SstringaSql & "pubbliche</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i9c & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>c)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='95%'><font face='Arial' size='3'>richiedenti che dimorino da almeno tre anni" & vbCrLf
                SstringaSql = SstringaSql & "in locali non originariamente" & vbCrLf
                SstringaSql = SstringaSql & "destinati alla" & vbCrLf
                SstringaSql = SstringaSql & "residenza abitativa" & vbCrLf
                SstringaSql = SstringaSql & "anche di" & vbCrLf
                SstringaSql = SstringaSql & "tipo rurale" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "ovvero in locali inabitabili" & vbCrLf
                SstringaSql = SstringaSql & "ai sensi del" & vbCrLf
                SstringaSql = SstringaSql & "regolamento d'igiene del comune" & vbCrLf
                SstringaSql = SstringaSql & "o in altro" & vbCrLf
                SstringaSql = SstringaSql & "ricovero procurato a titolo precario</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i9d & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>d)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='95%'><font face='Arial' size='3'>non sussiste la condizione" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "<p>&nbsp;</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font face='Arial' size='4'>10)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</center>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<td width='105%' valign='top' align='center' colspan='3'>" & vbCrLf
                SstringaSql = SstringaSql & "<p align='left'><B><font face='Arial' size='4'>COABITAZIONE</font></B>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<center>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "</center>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<td width='105%' valign='top' align='center' colspan='3'>" & vbCrLf
                SstringaSql = SstringaSql & "<p align='left'><font face='Arial' size='3'>Richiedenti che abitino da almeno tre anni" & vbCrLf
                SstringaSql = SstringaSql & "con il proprio nucleo familiare in uno stesso" & vbCrLf
                SstringaSql = SstringaSql & "alloggio con altro o più nuclei familiari:</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<center>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i10a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='95%'><font face='Arial' size='3'>nuclei non legati da vincoli di parentela" & vbCrLf
                SstringaSql = SstringaSql & "o di affinità</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i10b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='95%'><font face='Arial' size='3'>nuclei legati da vincoli di parentela o di" & vbCrLf
                SstringaSql = SstringaSql & "affinità entro il quarto grado</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i10c & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>c)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='95%'><font face='Arial' size='3'>non sussiste la condizione" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "<p>&nbsp;</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "</TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "</div>" & vbCrLf
                SstringaSql = SstringaSql & "</center>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<TABLE width='100%' cellpadding='0' cellspacing='0'>" & vbCrLf
                SstringaSql = SstringaSql & "<TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<div align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<center>" & vbCrLf
                SstringaSql = SstringaSql & "<table width='1147' cellpadding='2' cellspacing='0'>" & vbCrLf
                SstringaSql = SstringaSql & "<tbody>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='49'><B><font face='Arial' size='4'>11)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1086' colspan='5'><B><font face='Arial' size='4'>SOVRAFFOLLAMENTO</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='49'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='1086' colspan='5'><font face='Arial' size='3'>Richiedenti che abitino da almeno tre anni" & vbCrLf
                SstringaSql = SstringaSql & "con il proprio nucleo familiare:</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='49'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i11a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='45' valign='top' align='center'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='974' colspan='3'><font face='Arial' size='3'>in alloggio che presenta forte sovraffollamento" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "vale a dire</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='49'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='36'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='304'><font face='Arial' size='3'>- 3 o più persone in 1 vano abitabile</font> </td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='622'><font face='Arial' size='3'>= 14 mq + 20%</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='49'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='36'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='304'><font face='Arial' size='3'>- 4 o 5 persone in 2 vani abitabili</font> </td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='622'><font face='Arial' size='3'>= 28 mq + 20%</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='49'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='36'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='304'><font face='Arial' size='3'>- 6 persone in 3 o meno vani abitabili</font> </td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='622'><font face='Arial' size='3'>= 42 mq + 20%</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='49'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='36'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='304'><font face='Arial' size='3'>- 7 o più persone in 4 o meno vani abitabili</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='622'><font face='Arial' size='3'>= 56 mq + 20%</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='49'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i11b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='45' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='974' colspan='3'><font face='Arial' size='3'>in alloggio che presenta sovraffollamento" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "vale a dire:</font> </td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='49'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='36'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='304'><font face='Arial' size='3'>- 1 o 2 persone in 1 vano abitabile</font> </td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='622'><font face='Arial' size='3'>= 14 mq + 20%</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='49'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='36'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='304'><font face='Arial' size='3'>- 3 persone in 2 vani abitabili</font> </td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='622'><font face='Arial' size='3'>= 28 mq + 20%</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='49'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='36'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='304'><font face='Arial' size='3'>- 4 o 5 persone in 3 vani abitabili</font> </td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='622'><font face='Arial' size='3'>= 42 mq + 20%</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='49'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='36'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='304'><font face='Arial' size='3'>- 6 persone in 4 vani abitabili</font> </td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='622'><font face='Arial' size='3'>= 56 mq + 20%</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='49'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='45' valign='top' align='center'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='36'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='304'><font face='Arial' size='3'>- 7 o più persone in 5 vani abitabili</font> </td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='622'><font face='Arial' size='3'>= 70 mq + 20%</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='49'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='55' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i11c & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='45' valign='top' align='center'><font face='Arial' size='3'>c)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='974' colspan='3'><font face='Arial' size='3'>non sussiste la condizione</font>" & vbCrLf
                SstringaSql = SstringaSql & "<p>&nbsp;</p>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "</div>" & vbCrLf
                SstringaSql = SstringaSql & "</center>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<TABLE width='100%' cellpadding='0' cellspacing='0'>" & vbCrLf
                SstringaSql = SstringaSql & "<TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "<TR valign='top'>" & vbCrLf
                SstringaSql = SstringaSql & "<td>" & vbCrLf
                SstringaSql = SstringaSql & "<TABLE width='100%' cellpadding='2' cellspacing='0'>" & vbCrLf
                SstringaSql = SstringaSql & "<TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font face='Arial' size='4'>12)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='4'>CONDIZIONI DELL'ALLOGGIO</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><font face='Arial' size='3'>Richiedenti che abitino da almeno tre anni" & vbCrLf
                SstringaSql = SstringaSql & "con il proprio nucleo familiare:</font> </td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i12a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>in alloggio privo di servizi igienici interni" & vbCrLf
                SstringaSql = SstringaSql & "o con servizi igienici interni non regolamentari" & vbCrLf
                SstringaSql = SstringaSql & "(vale a dire: lavello" & vbCrLf
                SstringaSql = SstringaSql & "tazza e doccia o vasca)" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "ovvero privi di servizi a rete (acqua o elettricità" & vbCrLf
                SstringaSql = SstringaSql & "o gas)" & vbCrLf
                SstringaSql = SstringaSql & "ovvero in alloggi per i quali sia" & vbCrLf
                SstringaSql = SstringaSql & "stata accertata dall'ASL la condizione di" & vbCrLf
                SstringaSql = SstringaSql & "antigienicità ineliminabile con normali interventi" & vbCrLf
                SstringaSql = SstringaSql & "manutentivi</font> </td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i12b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>in alloggio privo di impianto di riscaldamento" & vbCrLf
                SstringaSql = SstringaSql & "(centralizzato o con caldaia autonoma)" & vbCrLf
                SstringaSql = SstringaSql & "ovvero" & vbCrLf
                SstringaSql = SstringaSql & "con servizi igienici interni privi di areazione" & vbCrLf
                SstringaSql = SstringaSql & "naturale o meccanica" & vbCrLf
                SstringaSql = SstringaSql & "ovvero in alloggi per" & vbCrLf
                SstringaSql = SstringaSql & "i quali sia stata accertata dall'ASL la condizione" & vbCrLf
                SstringaSql = SstringaSql & "di antigienicità eliminabile con normali" & vbCrLf
                SstringaSql = SstringaSql & "interventi manutentivi</font> </td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i12c & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>c)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>non sussiste la condizione<BR><BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr><p style='page-break-after: always' class='mini'><font face='Arial' size='3'>&nbsp;</font></p>" & vbCrLf
                SstringaSql = SstringaSql & "<tr><td>&nbsp;</td><td>&nbsp;</td></tr><tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font face='Arial' size='4'>13)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='4'>BARRIERE ARCHITETTONICHE</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><font face='Arial' size='3'>Richiedenti" & vbCrLf
                SstringaSql = SstringaSql & "di cui al precedente punto 2)" & vbCrLf
                SstringaSql = SstringaSql & "che abitino con il proprio nucleo familiare" & vbCrLf
                SstringaSql = SstringaSql & "in alloggio che" & vbCrLf
                SstringaSql = SstringaSql & "per accessibilità o per" & vbCrLf
                SstringaSql = SstringaSql & "tipologia" & vbCrLf
                SstringaSql = SstringaSql & "non consenta una normale condizione" & vbCrLf
                SstringaSql = SstringaSql & "abitativa (barriere architettoniche" & vbCrLf
                SstringaSql = SstringaSql & "mancanza" & vbCrLf
                SstringaSql = SstringaSql & "di servizi igienici adeguati o di un locale" & vbCrLf
                SstringaSql = SstringaSql & "separato per la patologia presente)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i13a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>sussiste la condizione</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i13b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>non sussiste la condizione<BR><BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font face='Arial' size='4'>14)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='4'>CONDIZIONI DI ACCESSIBILITÀ</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><font face='Arial' size='3'>Richiedenti" & vbCrLf
                SstringaSql = SstringaSql & "di cui ai precedenti punti 1)" & vbCrLf
                SstringaSql = SstringaSql & "e 2)" & vbCrLf
                SstringaSql = SstringaSql & "che abitino con il proprio nucleo familiare" & vbCrLf
                SstringaSql = SstringaSql & "in alloggio che non è servito da ascensore" & vbCrLf
                SstringaSql = SstringaSql & "ed è situato superiormente al primo piano</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i14a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>sussiste la condizione</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i14b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>non sussiste la condizione<BR>" & vbCrLf
                SstringaSql = SstringaSql & "<BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<table>" & vbCrLf
                SstringaSql = SstringaSql & "<TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font face='Arial' size='4'>15)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='4'>LONTANANZA DALLA SEDE DI LAVORO</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><font face='Arial' size='3'>Richiedente che risieda da almeno tre anni" & vbCrLf
                SstringaSql = SstringaSql & "in un alloggio situato in località diversa" & vbCrLf
                SstringaSql = SstringaSql & "dal Comune in cui presta la propria attività" & vbCrLf
                SstringaSql = SstringaSql & "lavorativa esclusiva o principale" & vbCrLf
                SstringaSql = SstringaSql & "ovvero" & vbCrLf
                SstringaSql = SstringaSql & "sia destinato all'atto del bando a prestare" & vbCrLf
                SstringaSql = SstringaSql & "servizio presso nuovi insediamenti o attività" & vbCrLf
                SstringaSql = SstringaSql & "produttive in Comune diverso da quello di" & vbCrLf
                SstringaSql = SstringaSql & "residenza; la distanza del luogo di residenza" & vbCrLf
                SstringaSql = SstringaSql & "dal Comune sede di lavoro deve essere superiore" & vbCrLf
                SstringaSql = SstringaSql & "a 90 minuti di percorrenza con gli ordinari" & vbCrLf
                SstringaSql = SstringaSql & "mezzi di trasporto pubblico</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i15a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>sussiste la condizione</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i15b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%' valign='top'><font face='Arial' size='3'>non sussiste la condizione<BR><BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "<p>&nbsp;</p>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'><B><font face='Arial' size='4'>16)</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' colspan='3'><B><font face='Arial' size='4'>AFFITTO ONEROSO</font></B></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td></td>" & vbCrLf
                SstringaSql = SstringaSql & "<TD colspan='3'><font face='Arial' size='3'>Richiedenti titolari da almeno tre anni di" & vbCrLf
                SstringaSql = SstringaSql & "un contratto di locazione relativo all'abitazione" & vbCrLf
                SstringaSql = SstringaSql & "principale il cui &quot;canone integrato&quot;" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "all'atto del bando" & vbCrLf
                SstringaSql = SstringaSql & "sia superiore di oltre" & vbCrLf
                SstringaSql = SstringaSql & "il 5% al &quot;canone sopportabile&quot;.</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i16a & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>a)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%'><font face='Arial' size='3'>sussiste la condizione</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%'></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & i16b & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='5%' valign='top' align='center'><font face='Arial' size='3'>b)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='85%' valign='top'><font face='Arial' size='3'>non sussiste la condizione" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "</table>&nbsp;" & vbCrLf
                SstringaSql = SstringaSql & "<p>&nbsp;" & vbCrLf
                SstringaSql = SstringaSql & "</p>" & vbCrLf
                SstringaSql = SstringaSql & "<p><font face='Arial' size='3'><BR>" & vbCrLf
                SstringaSql = SstringaSql & "Il sottoscritto dichiara infine di aver preso" & vbCrLf
                SstringaSql = SstringaSql & "conoscenza di tutte le norme contenute nel" & vbCrLf
                SstringaSql = SstringaSql & "bando di assegnazione degli immobili ERP" & vbCrLf
                SstringaSql = SstringaSql & "e di possedere tutti i requisiti di partecipazione" & vbCrLf
                SstringaSql = SstringaSql & "in esso indicati e di autorizzare" & vbCrLf
                SstringaSql = SstringaSql & "ai sensi" & vbCrLf
                SstringaSql = SstringaSql & "dell'art. 10 della legge 31.12.1996" & vbCrLf
                SstringaSql = SstringaSql & "n. 675" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "il trattamento dei dati da parte della Regione" & vbCrLf
                SstringaSql = SstringaSql & "Lombardia" & vbCrLf
                SstringaSql = SstringaSql & "per l'esclusiva finalità prevista" & vbCrLf
                SstringaSql = SstringaSql & "dal bando ed in quanto obbligatori per la" & vbCrLf
                SstringaSql = SstringaSql & "stessa" & vbCrLf
                SstringaSql = SstringaSql & "nonchè l'elaborazione in forma anonima" & vbCrLf
                SstringaSql = SstringaSql & "per eventuali elaborazioni statistiche.<BR>" & vbCrLf
                SstringaSql = SstringaSql & "<BR>" & vbCrLf
                SstringaSql = SstringaSql & "Per ogni ulteriore trattamento dei" & vbCrLf
                SstringaSql = SstringaSql & "dati verrà" & vbCrLf
                SstringaSql = SstringaSql & "richiesta esplicita autorizzazione" & vbCrLf
                SstringaSql = SstringaSql & "e sono" & vbCrLf
                SstringaSql = SstringaSql & "fatte salve le facoltà del sottoscritto" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "di cui all'art. 13 della legge 675/1996<BR>" & vbCrLf
                SstringaSql = SstringaSql & "<BR>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</p>" & vbCrLf
                SstringaSql = SstringaSql & "<TABLE width='100%' cellpadding='2' cellspacing='0'>" & vbCrLf
                SstringaSql = SstringaSql & "<TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='50%' valign='top'><font face='Arial' size='3'>Data ____/____/____</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='50%' align='center'><font face='Arial' size='3'>IL RICHIEDENTE<BR>" & vbCrLf
                SstringaSql = SstringaSql & "<BR>" & vbCrLf
                SstringaSql = SstringaSql & "_____________________________________<BR>" & vbCrLf
                SstringaSql = SstringaSql & "(firma leggibile)(*)</font></td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "</table>"
                SstringaSql = SstringaSql & "<p>&nbsp;</p>"
                SstringaSql = SstringaSql & "<table border='1' cellpadding='0' cellspacing='0' width='100%'>"
                SstringaSql = SstringaSql & "<tr>"
                SstringaSql = SstringaSql & "<td width='100%'><font face='Arial' size='1'>DICHIARAZIONE RESA E SOTTOSCRITTA IN NOME E PER CONTO DEL RICHIEDENTE DA<BR>"
                SstringaSql = SstringaSql & "(COGNOME)___________________________________(NOME)___________________________________<BR>"
                SstringaSql = SstringaSql & "(DOC. DIRICONOSCIMENTO, N°.)________________________<BR>"
                SstringaSql = SstringaSql & "IN QUALITA' DI (GRADO PARENTELA)_________________________, COMPONENENTE MAGGIORENNE IL NUCLEO FAMILIARE<br>"
                SstringaSql = SstringaSql & "RICHIEDENTE L'ALLOGGIO, MUNITO DI DELEGA ALLEGATA AGLIA ATTI.<br>"
                SstringaSql = SstringaSql & ""
                SstringaSql = SstringaSql & "L'OPERATORE______________</font></td>"
                SstringaSql = SstringaSql & "</tr>"
                SstringaSql = SstringaSql & "</table>"
                SstringaSql = SstringaSql & "<font face='Arial' size='3'><BR>" & vbCrLf
                SstringaSql = SstringaSql & "( ) Annotazione estremi documento di identità" & vbCrLf
                SstringaSql = SstringaSql & "__________________________ <BR>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "Firma apposta dal dichiarante in presenza" & vbCrLf
                SstringaSql = SstringaSql & "di _________________________ <BR>" & vbCrLf
                SstringaSql = SstringaSql & "<BR>" & vbCrLf
                SstringaSql = SstringaSql & "(*) ai sensi dell'art. 5 comma 3 della legge" & vbCrLf
                SstringaSql = SstringaSql & "15.5.1997 n. 127 la firma" & vbCrLf
                SstringaSql = SstringaSql & "apposta in calce" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "non deve essere autenticata" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</TBODY>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "<p align='left'>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & vbCrLf
                SstringaSql = SstringaSql & "</font>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<p style='page-break-after: always' class='mini'><font face='Arial' size='3'>&nbsp;</font></p>" & vbCrLf
                SstringaSql = SstringaSql & "<CENTER><H2><b><font face='Arial' size='5'>COMUNE DI MILANO ACCESSO ERP</font></b></H2></CENTER>" & vbCrLf
                SstringaSql = SstringaSql & "<table border='0' cellpadding='0' cellspacing='5' width='100%' height='66'>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='75%' height='23'><b><font face='Arial' size='3'>SITUAZIONE REDDITUALE DEL NUCLEO FAMILIARE</font></b>" & SITUAZIONE_REDDITUALE & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "" & vbCrLf
                SstringaSql = SstringaSql & "<table border='0' cellpadding='0' cellspacing='5' width='100%' height='66'>" & vbCrLf
                SstringaSql = SstringaSql & "<tr><td width='75%' height='23'><b><font face='Arial' size='3'>SITUAZIONE PATRIMONIALE DEL NUCLEO FAMILIARE</font></b>" & SITUAZIONE_PATRIMONIALE & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "<table border='0' cellpadding='0' cellspacing='5' width='100%' height='66'><tr><td width='75%' height='23'><b><font face='Arial' size='3'>DETERMINAZIONE" & vbCrLf
                SstringaSql = SstringaSql & "DELL'ISEE-erp </font></b>" & sISEE_ERP & "</td>   </table><table border='0' cellpadding='0' cellspacing='5' width='100%' height='66'><tr><td width='75%' height='23'><b><font face='Arial' size='3'>Risultati intermedi" & vbCrLf
                SstringaSql = SstringaSql & "</font></b>" & RISULTATI_INTERMEDI & "</td>   </table><table border='0' cellpadding='0' cellspacing='5' width='100%' height='66'><tr><td width='75%' height='23'><b><font face='Arial' size='3'>Punteggi intermedi" & vbCrLf
                SstringaSql = SstringaSql & "</font></b>" & PUNTEGGI_INTERMEDI & "</td>   </table><p style='page-break-after: always' class='mini'><font face='Arial' size='3'>&nbsp;</font></p><table border='0' cellpadding='0' cellspacing='5' width='100%' height='66'><tr><td width='75%' height='23'><b><font face='Arial' size='3'>Indicatori</font></b></td><tr><td width='75%' height='23'><font face='Arial' size='3'>ISBAR: Indicatore dello Stato di Bisogno Abitativo Regionale</font>  </td>      <td width='13%' height='23'></td>      <td width='13%' height='23'>         <table border='1' width='100%'>            <tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='100%' align='right'>  <font face='Arial' size='3'>" & sISBAR & "</font>  </td>            </tr>         </table>      </td>   </tr><tr><td width='75%' height='23'><b><font face='Arial' size='3'>ISBARC: Indicatore dello Stato di Bisogno Abitativo Regionale e Comunale</font></b>  </td>      <td width='13%' height='23'></td>      <td width='13%' height='23'>" & vbCrLf
                SstringaSql = SstringaSql & "<table border='1' width='100%'>" & vbCrLf
                SstringaSql = SstringaSql & "<tr>" & vbCrLf
                SstringaSql = SstringaSql & "<td width='100%'>" & vbCrLf
                SstringaSql = SstringaSql & "<p align='right'> <b><font face='Arial' size='3'>" & sISBARC & "</font></b></p>" & vbCrLf
                SstringaSql = SstringaSql & "</td>" & vbCrLf
                SstringaSql = SstringaSql & "</tr>" & vbCrLf
                SstringaSql = SstringaSql & "</table>" & vbCrLf
                SstringaSql = SstringaSql & "</td>   </tr>   <tr><td width='75%' height='18'><b><font face='Arial' size='3'>ISBARC/R</font></b>  </td>      <td width='13%' height='23'></td>      <td width='13%' height='23'>         <table border='1' width='100%'>            <tr>               <td width='100%' align='right'> <b><font face='Arial' size='3'>" & sISBARC_R & "</font></b></td>            </tr>         </table>      </td>   </tr>   </table>   <P>" & testo & " <P>" & Motivo & "<p><font face='Arial' size='3'>La presente domanda è stata valutata sulla base della dichiarazione sostitutiva unica con numero protocollo" & vbCrLf
                SstringaSql = SstringaSql & "<b>" & pg_dichiarazione & "</b>" & vbCrLf
                SstringaSql = SstringaSql & "presentata in data" & vbCrLf
                SstringaSql = SstringaSql & "<b>" & DATA_PRESENTA_DICH & "</b> a <b>" & LUOGO_PRESENTA_DICH & "</b> presso l'ente <b>COMUNE DI MILANO</b>  ed elaborata in data" & vbCrLf
                SstringaSql = SstringaSql & "<b>" & DATA_STAMPA & "</b></font></p><table align='center' border=1 width=80% height=57><tr><td align='center' width=16% height=29%><strong><font size='3' face='Arial'>Pg/data" & vbCrLf
                SstringaSql = SstringaSql & "domanda</font></strong></td><td align='center' width=16% height=29%><strong><font size='3' face='Arial'>ente erogatore</font></strong></td><td align='center' width=16% height=29%><strong><font size='3' face='Arial'>data elaborazione</font></strong></td><td align='center' width=16% height=29%><strong><font size='3' face='Arial'>ente inseritore</font></strong></td><td align='center' width=16% height=29%><strong><font size='3' face='Arial'>stato</font></strong></td></tr><tr><td align='center' width=16% height=29%><font face='Arial' size='3'>" & ID_DOMANDA & "</font></td><td align='center' width=16% height=29%>" & vbCrLf
                SstringaSql = SstringaSql & "<font size='3' face='Arial'>" & Session.Item("CAAF") & "</font></td><td align='center' width=16% height=29%>" & vbCrLf
                SstringaSql = SstringaSql & "<font face='Arial' size='3'>" & DATA_STAMPA_DOMANDA & "</font></td><td align='center' width=16% height=29%>" & vbCrLf
                SstringaSql = SstringaSql & "<font size='3' face='Arial'>COMUNE DI MILANO</font></td><td align='center' width=16% height=29%>" & vbCrLf
                SstringaSql = SstringaSql & "<font size='3' face='Arial'>Completa</font></td></tr></table></BODY>" & vbCrLf


                SstringaSql = SstringaSql & "<p style='page-break-before: always'>&nbsp;</p><table><tr><td style='text-align: left; font-family: arial; font-size: 12pt;'><span class='style1'>" _
                    & " INFORMATIVA SULL'USO DEI DATI PERSONALI E SUI DIRITTI DEL DICHIARANTE ART. 13 DEL REGOLAMENTO (UE) 2016/679" _
                    & " <br /></span><br />" _
                    & " I dati personali, forniti dal concorrente con le dichiarazioni sostitutive per sè e per i  componenti del nucleo familiare, ai sensi dell'art. 13 del Regolamento (Ue) 2016/679, nonchè dei contenuti di cui agli artt. 7 e 9 dello stesso, inclusi quelli 'sensibili' di cui all’art. 4 del cit. Regolamento," _
                    & " saranno raccolti soltanto per lo svolgimento delle funzioni istituzionali da parte del Comune e degli Enti che hanno stipulato apposita convenzione con l’Amministrazione comunale, che si avvarranno del sistema informatico all’uopo predisposto, in accordo con il Settore Informatica e Telecomunicazioni del Comune e la Regione Lombardia, per la trasmissione e l’elaborazione dei dati forniti." _
                    & " <br /><br />Tali dati saranno trattati unicamente per le finalità connesse al bando e all’assegnazione degli alloggi e ciascuna Amministrazione sarà responsabile del trattamento dei dati ad essa pervenuti o con supporto cartaceo o informatico." _
                    & " Potranno essere comunicati ad altra Pubblica Amministrazione limitatamente alle informazioni relative a stati, fatti e qualità personali previste dalla legge e strettamente necessarie per il perseguimento delle suddette finalità.<br />" _
                    & " Il concorrente, ai sensi dell'art. 13 del suddetto regolamento, ha diritto di accedere ai dati che lo riguardano e di rettificare, aggiornare, cancellare i dati erronei, incompleti o raccolti in termini non conformi alla legge, nonché il diritto di opporsi al loro trattamento per motivi legittimi.<br />" _
                    & " Per quanto non espressamente richiamato nel presente bando, si rimanda alla normativa nazionale e regionale vigente in materia, in particolare al Regolamento Regionale n. 1/2004 e s.m.i. ...</td></tr></table>”


                If Session.Item("ID_CAF") <> "2" And Session.Item("ID_CAF") <> "6" Then
                    SstringaSql = SstringaSql & "<p style='page-break-before: always'>&nbsp;</p><table style='width:100%;'><tr><td style='font-family: ARIAL; font-size: 14pt; font-weight: bold'>ALL.1</td></tr><tr><td>&nbsp;&nbsp;</td></tr><tr><td style='font-family: ARIAL; font-size: 12pt; font-weight: bold'>" _
                                & Session.Item("DESCRIZIONE_CAF") & "</td></tr><tr><td align='right'><table style='width: 30%;'><tr><td style='font-family: ARIAL; font-size: 12pt; font-weight: bold; text-align: center;'>ISEE-ERP</td></tr><tr><td style='font-family: ARIAL; font-size: 12pt; font-weight: bold; text-align: center;'>" _
                                & "Indicatore della Situazione<br />Economica Equivalente<br />ai fini ERP (R.R. 1/2004) </td></tr><tr><td>&nbsp;</td></tr></table></td></tr><tr><td style='font-family: ARIAL; font-size: 12pt; font-weight: bold'>" _
                                & "Dichiarazione N. " & pg_dichiarazione & " del " & DATA_PRESENTA_DICH & ", Domanda n. " & ID_DOMANDA & "</td></tr><tr><td style='font-family: ARIAL; font-size: 12pt; font-weight: bold' class='style1'>Dichiarante " _
                                & CType(Dom_Richiedente1.FindControl("txtCognome"), TextBox).Text _
                                & " " & CType(Dom_Richiedente1.FindControl("txtNome"), TextBox).Text & "</td></tr><tr><td style='font-family: ARIAL; " _
                                & "font-size: 12pt; font-weight: bold'></td></tr><tr><td style='font-family: ARIAL; font-size: 12pt; font-weight: bold'>Il sottoscritto " & CType(Dom_Richiedente1.FindControl("txtCognome"), TextBox).Text & " " & CType(Dom_Richiedente1.FindControl("txtNome"), TextBox).Text & " dichiara che all&#39;atto della presentazione della domanda " _
                                & "sopra indicata, ha presentato al " & Session.Item("DESCRIZIONE_CAF") & ", la documentazione di seguito indicata:<br /><br />n. __ certificato d&#39;invalidità<br />n. __ CUD<br />n. __ UNICO<br />n. __ 730" _
                                & "<br />n. __ Atti di Acquisizione<br />n. __ Dichiarazione ICI<br />n. __ Volture Catastali<br />n. __ Saldo Conto Corrente<br />n. __ Saldo Deposito Titoli/Azioni<br />n. __ Saldo Partecipazioni<br />n. __ Doc. Istuto di credito per mutui contratti<br />" _
                                & "n. __ Documentazione Contributi/Sussidi<br />altro..............................................<br />/td></tr><tr><td style='font-family: ARIAL; font-size: 12pt; font-weight: bold'>&nbsp; &nbsp;</td></tr><tr><td style='font-family: ARIAL; font-size: 12pt; font-weight: bold'>" _
                                & "Data " & Format(Now, "dd/MM/yyyy") & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" _
                                & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" _
                                & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " _
                                & "Firma_______________________________<br /><br />Il " & Session.Item("DESCRIZIONE_CAF") & ", in relazione al nucleo familiare del dichiarante ed alle relative condizioni economiche, come risultanti dalla domanda, e in base ai " _
                                & "documenti sopra indicati DICHIARA che l’indicatore della situazione economica equivalente ai fini ERP (ISEE-ERP) è pari a:" _
                                & "</td></tr><tr><td style='font-family: ARIAL; font-size: 12pt; font-weight: bold; text-align: center;'>" & par.Converti(Format(ISEE_ERP, "##,##0.00")) & "</td></tr><tr><td style='font-family: ARIAL; font-size: 12pt; font-weight: " _
                                & "bold; text-align: left;'>Data " & Format(Now, "dd/MM/yyyy") & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" _
                                & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" _
                                & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & Session.Item("DESCRIZIONE_CAF") & "</td></tr></table>"
                End If

                'HttpContext.Current.Session.Add("DOMANDA", SstringaSql)
                'Response.Write("<script>window.open('StampaDomanda.aspx','Domanda','');</script>")


                Dim url As String = Server.MapPath("ALLEGATI\BANDI_ERP\STAMPE\")

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
                pdfConverter1.PdfDocumentOptions.BottomMargin = 1
                pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                'pdfConverter1.PdfFooterOptions.FooterText = ("Domanda N. " & lblPG.Text)
                pdfConverter1.PdfFooterOptions.FooterText = ("Domanda N. " & protocollo & " / " & CODICEANAGRAFICO)
                'pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Black
                pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
                pdfConverter1.PdfFooterOptions.ShowPageNumber = True

                Dim nomefile As String = "01_" & lIdDichiarazione & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"

                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(SstringaSql, url & nomefile, Server.MapPath("IMG\"))
                pdfConverter1.PdfFooterOptions.ShowPageNumber = False


                Dim ix As Integer = 0
                For ix = 0 To 1000

                Next
                Response.Write("<script>window.open('ALLEGATI/BANDI_ERP/STAMPE/" & nomefile & "','Domanda','');</script>")




                lIndice_Bando = glIndice_Bando_Origine
                If Fl_DerogaInCorso = "" Then
                    If ESCLUSIONE = "" Then
                        If FL_RINNOVO = "0" Then
                            par.cmd.CommandText = "update domande_bando set FL_COMPLETA='1',FL_RINNOVO='0'," _
                                                & "ID_VECCHIO_STATO='',ID_TIPO_CONTENZIOSO=NULL," _
                                                & "FL_ISTRUTTORIA_COMPLETA='1',ID_STATO='7a'," _
                                                & "FL_ESAMINATA='1',ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
                                                & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) & ",ISP_ERP=" _
                                                & par.VirgoleInPunti(ISP_ERP) & ",REDDITO_ISEE=" _
                                                & par.VirgoleInPunti(ISEE_ERP) & ", TIPO_ALLOGGIO=" _
                                                & TIPO_ALLOGGIO & ",ISBARC=" & par.VirgoleInPunti(ISBARC) _
                                                & ",ISBAR=" & par.VirgoleInPunti(ISBAR) & ",ISBARC_R=" _
                                                & par.VirgoleInPunti(ISBARC_R) & ",DISAGIO_A=" _
                                                & par.VirgoleInPunti(DISAGIO_A) & ",DISAGIO_F=" _
                                                & par.VirgoleInPunti(DISAGIO_F) & ",DISAGIO_E=" _
                                                & par.VirgoleInPunti(DISAGIO_E) & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "' WHERE ID=" & lIdDomanda
                        Else
                            If FL_VECCHIO_BANDO = "1" Then
                                par.cmd.CommandText = "update domande_bando set FL_COMPLETA='1',FL_RINNOVO='0'," _
                                                    & "ID_VECCHIO_STATO='',ID_TIPO_CONTENZIOSO=NULL," _
                                                    & "FL_ISTRUTTORIA_COMPLETA='1',ID_STATO='7a'," _
                                                    & "FL_ESAMINATA='1',ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
                                                    & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) & ",ISP_ERP=" _
                                                    & par.VirgoleInPunti(ISP_ERP) & ",REDDITO_ISEE=" _
                                                    & par.VirgoleInPunti(ISEE_ERP) & ", TIPO_ALLOGGIO=" _
                                                    & TIPO_ALLOGGIO & ",ISBARC=" & par.VirgoleInPunti(ISBARC) _
                                                    & ",ISBAR=" & par.VirgoleInPunti(ISBAR) & ",ISBARC_R=" _
                                                    & par.VirgoleInPunti(ISBARC_R) & ",DISAGIO_A=" _
                                                    & par.VirgoleInPunti(DISAGIO_A) & ",DISAGIO_F=" _
                                                    & par.VirgoleInPunti(DISAGIO_F) & ",DISAGIO_E=" _
                                                    & par.VirgoleInPunti(DISAGIO_E) & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "' WHERE ID=" & lIdDomanda


                            Else

                                par.cmd.CommandText = "update domande_bando set FL_COMPLETA='1',FL_RINNOVO='0'," _
                                                    & "ID_VECCHIO_STATO='',ID_TIPO_CONTENZIOSO=NULL," _
                                                    & "FL_ISTRUTTORIA_COMPLETA='1',ID_STATO='8'," _
                                                    & "FL_ESAMINATA='1',ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
                                                    & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) & ",ISP_ERP=" _
                                                    & par.VirgoleInPunti(ISP_ERP) & ",REDDITO_ISEE=" _
                                                    & par.VirgoleInPunti(ISEE_ERP) & ", TIPO_ALLOGGIO=" _
                                                    & TIPO_ALLOGGIO & ",ISBARC=" & par.VirgoleInPunti(ISBARC) _
                                                     & ",ISBAR=" & par.VirgoleInPunti(ISBAR) & ",ISBARC_R=" _
                                                    & par.VirgoleInPunti(ISBARC_R) & ",DISAGIO_A=" _
                                                    & par.VirgoleInPunti(DISAGIO_A) & ",DISAGIO_F=" _
                                                    & par.VirgoleInPunti(DISAGIO_F) & ",DISAGIO_E=" _
                                                    & par.VirgoleInPunti(DISAGIO_E) & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "' WHERE ID=" & lIdDomanda
                            End If
                        End If
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "update domande_bando set FL_COMPLETA='1',FL_RINNOVO='0'," _
                                               & "ID_VECCHIO_STATO='',ID_TIPO_CONTENZIOSO=NULL," _
                                               & "FL_ISTRUTTORIA_COMPLETA='1',ID_STATO='4'," _
                                               & "FL_ESAMINATA='1',ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
                                               & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) _
                                               & ",ISP_ERP=" & par.VirgoleInPunti(ISP_ERP) _
                                               & ",REDDITO_ISEE=" & par.VirgoleInPunti(ISEE_ERP) _
                                               & ",TIPO_ALLOGGIO=" & TIPO_ALLOGGIO _
                                               & ",ISBARC=" & par.VirgoleInPunti(ISBARC) _
                                               & ",ISBAR=" & par.VirgoleInPunti(ISBAR) _
                                               & ",ISBARC_R=" & par.VirgoleInPunti(ISBARC_R) _
                                               & ",DISAGIO_A=" & par.VirgoleInPunti(DISAGIO_A) _
                                               & ",DISAGIO_F=" & par.VirgoleInPunti(DISAGIO_F) _
                                               & ",DISAGIO_E=" & par.VirgoleInPunti(DISAGIO_E) _
                                               & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "' WHERE ID=" & lIdDomanda
                        par.cmd.ExecuteNonQuery()
                    End If
                Else
                    ''in caso di deroga in corso
                    If ESCLUSIONE = "" Then
                        If FL_RINNOVO = "0" Then
                            par.cmd.CommandText = "update domande_bando set FL_COMPLETA='1',FL_RINNOVO='0'," _
                                                & "ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
                                                & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) & ",ISP_ERP=" _
                                                & par.VirgoleInPunti(ISP_ERP) & ",REDDITO_ISEE=" _
                                                & par.VirgoleInPunti(ISEE_ERP) & ", TIPO_ALLOGGIO=" _
                                                & TIPO_ALLOGGIO & ",ISBARC=" & par.VirgoleInPunti(ISBARC) _
                                                & ",ISBAR=" & par.VirgoleInPunti(ISBAR) & ",ISBARC_R=" _
                                                & par.VirgoleInPunti(ISBARC_R) & ",DISAGIO_A=" _
                                                & par.VirgoleInPunti(DISAGIO_A) & ",DISAGIO_F=" _
                                                & par.VirgoleInPunti(DISAGIO_F) & ",DISAGIO_E=" _
                                                & par.VirgoleInPunti(DISAGIO_E) & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "' WHERE ID=" & lIdDomanda
                        Else
                            par.cmd.CommandText = "update domande_bando set FL_COMPLETA='1',FL_RINNOVO='0'," _
                                                & "ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
                                                & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) & ",ISP_ERP=" _
                                                & par.VirgoleInPunti(ISP_ERP) & ",REDDITO_ISEE=" _
                                                & par.VirgoleInPunti(ISEE_ERP) & ", TIPO_ALLOGGIO=" _
                                                & TIPO_ALLOGGIO & ",ISBARC=" & par.VirgoleInPunti(ISBARC) _
                                                 & ",ISBAR=" & par.VirgoleInPunti(ISBAR) & ",ISBARC_R=" _
                                                & par.VirgoleInPunti(ISBARC_R) & ",DISAGIO_A=" _
                                                & par.VirgoleInPunti(DISAGIO_A) & ",DISAGIO_F=" _
                                                & par.VirgoleInPunti(DISAGIO_F) & ",DISAGIO_E=" _
                                                & par.VirgoleInPunti(DISAGIO_E) & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "' WHERE ID=" & lIdDomanda
                        End If
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "update domande_bando set FL_COMPLETA='1',FL_RINNOVO='0'," _
                                               & "ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
                                               & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) _
                                               & ",ISP_ERP=" & par.VirgoleInPunti(ISP_ERP) _
                                               & ",REDDITO_ISEE=" & par.VirgoleInPunti(ISEE_ERP) _
                                               & ",TIPO_ALLOGGIO=" & TIPO_ALLOGGIO _
                                               & ",ISBARC=" & par.VirgoleInPunti(ISBARC) _
                                               & ",ISBAR=" & par.VirgoleInPunti(ISBAR) _
                                               & ",ISBARC_R=" & par.VirgoleInPunti(ISBARC_R) _
                                               & ",DISAGIO_A=" & par.VirgoleInPunti(DISAGIO_A) _
                                               & ",DISAGIO_F=" & par.VirgoleInPunti(DISAGIO_F) _
                                               & ",DISAGIO_E=" & par.VirgoleInPunti(DISAGIO_E) _
                                               & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "' WHERE ID=" & lIdDomanda
                        par.cmd.ExecuteNonQuery()
                    End If

                End If




                txtIndici.Text = "V1=" & ISBAR _
                           & "&V2=" & ISBARC _
                           & "&V3=" & (ISBARC_R) _
                           & "&V4=" & (DISAGIO_A) _
                           & "&V5=" & (DISAGIO_E) _
                           & "&V6=" & (DISAGIO_F) _
                           & "&V7=" & par.Converti(ISEE_ERP) _
                           & "&V8=" & par.Converti(ISR_ERP) _
                           & "&V9=" & par.Converti(ISP_ERP) _
                           & "&V10=" & par.Converti(ISE_ERP) _
                           & "&V11=" & r _
                           & "&V12=" & PARAMETRO _
                           & "&V13=" & VSE _
                           & "&PG=" & lblPG.Text & "&UJ=3"

                SstringaSql = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
                      & "','F133','','I')"
                par.cmd.CommandText = SstringaSql
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "SELECT ID_DOMANDA, ID_TIPO_ESCLUSIONE FROM DOMANDE_ESCLUSIONI where id_domanda=" & lIdDomanda
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    ESCLUSIONE = "1"
                End If
                myReader.Close()

                If ESCLUSIONE = "" Then
                    SstringaSql = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
                          & "','F02','','I')"
                    par.cmd.CommandText = SstringaSql
                    par.cmd.ExecuteNonQuery()

                    If Fl_DerogaInCorso = "" Then
                        SstringaSql = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
                              & "','F37','','I')"
                        par.cmd.CommandText = SstringaSql
                        par.cmd.ExecuteNonQuery()


                        par.cmd.CommandText = "DELETE FROM BANDI_GRADUATORIA WHERE ID_DOMANDA=" & lIdDomanda
                        par.cmd.ExecuteNonQuery()

                        SstringaSql = "INSERT INTO BANDI_GRADUATORIA (ID,ID_BANDO,ID_DOMANDA," _
                                    & "ISBARC_R,TIPO,DISAGIO_A,DISAGIO_E,DISAGIO_F,ISE,ISEE,ISBAR) " _
                                    & "VALUES (SEQ_BANDI_GRADUATORIA.NEXTVAL," & lIndice_Bando _
                                    & "," & lIdDomanda & "," & par.VirgoleInPunti(par.IfNull(ISBARC_R, 0)) _
                                    & ",0," & par.VirgoleInPunti(par.IfNull(DISAGIO_A, 0)) & "," _
                                    & par.VirgoleInPunti(par.IfNull(DISAGIO_E, 0)) & "," _
                                    & par.VirgoleInPunti(par.IfNull(DISAGIO_F, 0)) & "," _
                                    & par.VirgoleInPunti(par.IfNull(ISE_ERP, 0)) & "," _
                                    & par.VirgoleInPunti(par.IfNull(ISEE_ERP, 0)) _
                                    & "," & par.VirgoleInPunti(par.IfNull(ISBAR, 0)) & ")"
                        par.cmd.CommandText = SstringaSql
                        par.cmd.ExecuteNonQuery()
                    End If

                    If Fl_VerificaReq = "1" And Fl_inAssegnazione = "1" Then
                        par.cmd.CommandText = "update domande_bando set ID_STATO='9',FL_CONTROLLA_REQUISITI='2' where id= " & lIdDomanda
                        par.cmd.ExecuteNonQuery()

                        SstringaSql = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
                                    & "','F39','','I')"
                        par.cmd.CommandText = SstringaSql
                        par.cmd.ExecuteNonQuery()
                    End If
                Else
                    SstringaSql = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
                            & "','F57','','I')"
                    par.cmd.CommandText = SstringaSql
                    par.cmd.ExecuteNonQuery()
                    If Fl_DerogaInCorso = "" Then
                        SstringaSql = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
                                & "','F37','','I')"
                        par.cmd.CommandText = SstringaSql
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "DELETE FROM BANDI_GRADUATORIA WHERE ID_DOMANDA=" & lIdDomanda
                        par.cmd.ExecuteNonQuery()
                    End If
                End If

                lIndice_Bando = glIndice_Bando_Origine

                '09/11/2018
                par.cmd.CommandText = "select bandi_graduatoria_def.*,(Select upper(descrizione) from tab_stati where cod=domande_bando.id_stato) as stato_dom from bandi_graduatoria_def," _
                        & " domande_bando where bandi_graduatoria_def.id_domanda(+)=domande_bando.id and " _
                        & " domande_bando.id=" & lIdDomanda
                Dim myrec2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myrec2.Read Then
                    lblStatoDomanda.Visible = True
                    lblStatoDomanda.Text = par.IfNull(myrec2("stato_dom"), "")
                    lblPunteggio.Text = "Posizione n. " & myrec2("posizione")
                End If
                myrec2.Close()

                par.myTrans.Commit()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
                myReader = par.cmd.ExecuteReader()
                myReader.Close()


            Catch ex As Exception
                Label10.Text = "err " & ex.ToString
                par.myTrans.Rollback()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
            Finally

            End Try

        End Function


        'Protected Sub btnIndici_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnIndici.Click
        '    Dim Stringa As String


        '    If Val(lblISBAR.Text) > 0 Then
        '        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        '        par.SettaCommand(par)
        '        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        '        ‘‘par.cmd.Transaction = par.myTrans
        '        par.cmd.CommandText = "select ISE_ERP,isr_erp,isp_erp,REDDITO_ISEE,isbar,isbarc,isbarc_r,disagio_a,disagio_e,disagio_f from domande_bando where id=" & lIdDomanda
        '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '        If myReader.Read() Then
        '            Stringa = "V1=" & Format(par.IfNull(myReader("isbar"), 0), "##,##0.000") _
        '                   & "&V2=" & Format(par.IfNull(myReader("isbarc"), 0), "##,##0.000") _
        '                   & "&V3=" & Format(par.IfNull(myReader("isbarc_r"), 0), "##,##0.000") _
        '                   & "&V4=" & Format(par.IfNull(myReader("disagio_a"), 0), "##,##0.000") _
        '                   & "&V5=" & Format(par.IfNull(myReader("disagio_e"), 0), "##,##0.000") _
        '                   & "&V6=" & Format(par.IfNull(myReader("disagio_f"), 0), "##,##0.000") _
        '                   & "&V7=" & Format(par.IfNull(myReader("reddito_isee"), 0), "##,##0.00") _
        '                   & "&V8=" & Format(par.IfNull(myReader("ISR_ERP"), 0), "##,##0.00") _
        '                   & "&V9=" & Format(par.IfNull(myReader("ISP_ERP"), 0), "##,##0.00") _
        '                   & "&V10=" & Format(par.IfNull(myReader("ISe_ERP"), 0), "##,##0.00") _
        '                   & "&PG=" & lblPG.Text
        '        End If
        '        myReader.Close()
        '        par.myTrans.Commit()
        '        par.myTrans = par.OracleConn.BeginTransaction()
        '        HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        '        Response.Write("<script>window.open('indici.aspx?" & Stringa & "','Indici','top=0,left=0,width=300,height=400');</script>")
        '    End If

        'End Sub


        Protected Sub Dom_Richiedente1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dom_Richiedente1.Load

        End Sub
    End Class



End Namespace