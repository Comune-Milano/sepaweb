
Partial Class FSA_domanda
    Inherits PageSetIdMode
    Dim lValoreCorrente As Long
    Dim sAnnoIsee As String
    Dim sAnnoCanone As String

    Dim par As New CM.Global
    Dim bEseguito As Boolean = False
    Dim scriptblock As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
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

            If Fl_Integrazione = "1" Then
                H1.Value = "1"
                H2.Value = "1"
            End If

            txtTab.Value = "1"


            If Session.Item("ANAGRAFE") = "0" Then
                imgAnagrafe.Visible = False
                imgAnagrafe.Attributes.Clear()
            Else
                imgAnagrafe.Visible = True
            End If

            If lIdDomanda = -1 Then

                lNuovaDomanda = 1
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                imgStampa.Enabled = False

                imgAttendi.Visible = True
                'NuovaDomanda()
                imgAttendi.Visible = False
                FL_VECCHIO_BANDO = "1"
            Else

                lNuovaDomanda = 0
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_Stampa.png"
                imgStampa.Enabled = False

                imgAttendi.Visible = True
                VisualizzaDomanda()
                lblEventi.Attributes.Add("onclick", "javascript:window.open('Eventi.aspx?ID=" & lIdDomanda & "','Eventi','');")
                imgAttendi.Visible = False
                If Session.Item("ANAGRAFE") = "1" Then
                    imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('../Anagrafe.aspx?ID=" & par.criptamolto(lIdDichiarazione) & "&T=3','Anagrafe','top=0,left=0,width=600,height=400');")
                End If

                'If sStato <> "4" And (Replace(llISEE.Text, ".", "") < 3100 Or Replace(llISEE.Text, ".", "") - CANONE_INTEGRATO) < 2066 Then
                '    LBLDIFFICOLTA.Visible = True
                'End If
            End If
            imgRiassunto.Attributes.Add("onclick", "javascript:window.open('Riassunto.aspx?ID=" & lIdDomanda & "&CF=" & CType(Dom_Richiedente1.FindControl("txtCF"), TextBox).Text & "','Riassunto','');")

            ' CType(Dom_Abitative_1_1.FindControl("hpPED"), HyperLink).NavigateUrl = ""


            CType(Dom_Richiedente1.FindControl("cmbResidenza"), DropDownList).Visible = False
            CType(Dom_Richiedente1.FindControl("Label9"), Label).Visible = False
            bEseguito = True
        End If
        H1.Value = H2.Value

    End Sub

    Private Function VisualizzaDomanda()
        Dim CT1 As DropDownList
        Dim cT As TextBox
        Dim lIndiceAppoggio_0 As Long
        Dim lIndiceAppoggio_1 As Long
        Dim lIndiceAppoggio_2 As Long
        Dim LB1 As Label

        Dim ISE As Double
        Dim VSE As Double

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)


            par.cmd.CommandText = "select anno_isee from bandi_fsa where stato=1 order by id desc"
            Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader11.Read Then
                AnnoIsee = myReader11("anno_isee")
            End If
            myReader11.Close()


            par.cmd.CommandText = "SELECT BANDI_fsa.STATO,DOMANDE_BANDO_fsa.FL_RINNOVO FROM BANDI_fsa,DOMANDE_BANDO_fsa WHERE DOMANDE_BANDO_fsa.ID=" & lIdDomanda & " AND DOMANDE_BANDO_fsa.ID_BANDO=BANDI_fsa.ID"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                FL_RINNOVO = par.IfNull(myReader("FL_RINNOVO"), "")
                FL_VECCHIO_BANDO = CStr(myReader("STATO"))
                If myReader("STATO") <> 1 And Fl_Integrazione <> "1" Then
                    btnSalva.Visible = False
                    imgStampa.Visible = False

                    Dom_Richiedente1.DisattivaTutto()
                    Dom_Abitative_1_1.DisattivaTutto()
                    Dom_Abitative_2_1.DisattivaTutto()
                    Dom_Dichiara1.DisattivaTutto()
                    Dom_Contratto1.DisattivaTutto()
                    Dom_Requisiti1.DisattivaTutto()

                    Response.Write("<script>alert('Il Bando FSA a cui appartiene questa domanda è CHIUSO. Per apportare modifiche usare le funzioni di INTEGRAZIONE!');</script>")
                End If
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT DICHIARAZIONI_fsa.ID_STATO  FROM DICHIARAZIONI_fsa,DOMANDE_BANDO_fsa WHERE DOMANDE_BANDO_fsa.ID=" & lIdDomanda & " AND DOMANDE_BANDO_fsa.ID_DICHIARAZIONE=DICHIARAZIONI_fsa.ID"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader("ID_STATO") <> 1 Then
                    btnSalva.Visible = False
                    imgStampa.Visible = False
                    Dom_Richiedente1.DisattivaTutto()
                    Dom_Abitative_1_1.DisattivaTutto()
                    Dom_Abitative_2_1.DisattivaTutto()
                    Dom_Dichiara1.DisattivaTutto()
                    Dom_Contratto1.DisattivaTutto()
                    Dom_Requisiti1.DisattivaTutto()


                    Response.Write("<script>alert('LA DICHIARAZIONE NON è COMPLETA. Non è possibile apportare modifiche!');</script>")
                End If
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_fsa.N_DISTINTA,DICHIARAZIONI_fsa.ID_CAF,CAF_WEB.COD_CAF,DOMANDE_BANDO_fsa.FL_RINNOVO FROM DOMANDE_BANDO_fsa,DICHIARAZIONI_fsa,CAF_WEB WHERE DOMANDE_BANDO_fsa.ID_DICHIARAZIONE=DICHIARAZIONI_fsa.ID AND DICHIARAZIONI_FSA.ID_CAF=CAF_WEB.ID AND DOMANDE_BANDO_fsa.ID=" & lIdDomanda
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                If Session.Item("ID_CAF") = "6" Then
                    If Session.Item("ID_CAF") = "6" Then
                        LBLENTE.Visible = True
                        LBLENTE.Text = "Ente Ins.: " & par.IfNull(myReader("COD_CAF"), "")
                        If par.IfNull(myReader("N_DISTINTA"), -1) = 0 And myReader("ID_CAF") <> Session.Item("ID_CAF") Then
                            btnSalva.Visible = False
                            imgStampa.Visible = False
                            Dom_Richiedente1.DisattivaTutto()
                            Dom_Abitative_2_1.DisattivaTutto()
                            Dom_Abitative_1_1.DisattivaTutto()
                            Dom_Dichiara1.DisattivaTutto()
                            Dom_Contratto1.DisattivaTutto()
                            Dom_Requisiti1.DisattivaTutto()

                            Response.Write("<script>alert('Non è possibile modificare. La domanda è ancora in carico presso un ente esterno!');</script>")
                        End If
                    Else
                        If par.IfNull(myReader("N_DISTINTA"), -1) > 0 And Fl_Integrazione <> "1" Then
                            btnSalva.Visible = False
                            imgStampa.Visible = False
                            Dom_Richiedente1.DisattivaTutto()
                            Dom_Abitative_2_1.DisattivaTutto()
                            Dom_Abitative_1_1.DisattivaTutto()
                            Dom_Dichiara1.DisattivaTutto()
                            Dom_Contratto1.DisattivaTutto()
                            Dom_Requisiti1.DisattivaTutto()

                            Response.Write("<script>alert('Non è possibile modificare. La domanda è stata scaricata nella distinta N. " & par.IfNull(myReader("N_DISTINTA"), "") & " ! USARE LE FUNZIONI DI INTEGRAZIONE');</script>")
                        End If
                    End If
                Else
                    If par.IfNull(myReader("N_DISTINTA"), -2) > 0 And Fl_Integrazione <> "1" Then
                        btnSalva.Visible = False
                        imgStampa.Visible = False

                        Dom_Richiedente1.DisattivaTutto()
                        Dom_Abitative_2_1.DisattivaTutto()
                        Dom_Abitative_1_1.DisattivaTutto()
                        Dom_Dichiara1.DisattivaTutto()
                        Dom_Contratto1.DisattivaTutto()
                        Dom_Requisiti1.DisattivaTutto()

                        Response.Write("<script>alert('Non è possibile modificare. La domanda è stata scaricata nella distinta N. " & par.IfNull(myReader("N_DISTINTA"), "") & " ! USARE LE FUNZIONI DI INTEGRAZIONE');</script>")
                    End If
                End If
            Else
                If Session.Item("ID_CAF") = "6" Then
                    If Session.Item("ID_CAF") = "6" Then
                        btnSalva.Visible = False
                        imgStampa.Visible = False

                        Dom_Richiedente1.DisattivaTutto()
                        Dom_Abitative_2_1.DisattivaTutto()
                        Dom_Abitative_1_1.DisattivaTutto()
                        Dom_Dichiara1.DisattivaTutto()
                        Dom_Contratto1.DisattivaTutto()
                        Dom_Requisiti1.DisattivaTutto()

                        Response.Write("<script>alert('Non è possibile modificare. La domanda NON è stata ancora creata!');</script>")
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

            par.RiempiDList(Me, par.OracleConn, "cmbOperatore", "SELECT OPERATORE AS ""DESCRIZIONE"",ID FROM OPERATORI WHERE ID_CAF=" & Session.Item("ID_CAF") & " ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "ID")
            Dim lsiFrutto As New ListItem("---", "-1")
            cmbOperatore.Items.Add(lsiFrutto)

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_fsa WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then

                txtDetrazioniCanone.Text = par.IfNull(myReader("importodetrazione"), "0")

                stato_domanda = par.IfNull(myReader("ID_stato"), "-1")
                sStato = stato_domanda

                cmbOperatore.SelectedIndex = -1
                cmbOperatore.Items.FindByValue(par.IfNull(myReader("ID_OPERATORE_CARICO"), "-1")).Selected = True

                If par.IfNull(myReader("FL_DA_LIQUIDARE"), "0") = "1" Then
                    chDaLiquidare.Checked = True
                End If

                If par.IfNull(myReader("FL_MANDATO_EFF"), "0") = "1" Then
                    ChMandato.Checked = True
                End If

                dm_QuotaComunale = par.IfNull(myReader("quotacomunalepagata"), 0)
                dm_QuotaRegionale = par.IfNull(myReader("quotaregionalepagata"), 0)
                dm_QuotaTotale = par.IfNull(myReader("tot_importo_erogato"), 0)


                ISE = par.IfNull(myReader("ISE_ERP"), 0)
                VSE = CDbl(par.IfNull(myReader("VSE"), 0))

                lblComunale.Text = "C. Comunale: " & dm_QuotaComunale
                lblRegionale.Text = "C. Regionale: " & dm_QuotaRegionale
                lblTotale.Text = "Tot.: " & dm_QuotaTotale
                lblAbbattimento.Text = "% Abb.: " & Format(par.IfNull(myReader("perc_abbattimento"), 0), "0.00")


                CANONE_INTEGRATO = par.IfNull(myReader("CANONE_INT"), 0)

                lIndice_Bando = myReader("ID_BANDO")
                lIdDichiarazione = myReader("ID_DICHIARAZIONE")
                lProgr = myReader("PROGR_COMPONENTE")
                sStato = myReader("id_stato")
                llISEE.Text = par.Tronca(par.IfNull(myReader("reddito_isee"), 0))
                lblPG.Text = par.IfNull(myReader("pg"), "")
                txtDataPG.Text = par.FormattaData(par.IfNull(myReader("data_pg"), ""))
                lblPG.ToolTip = lIdDomanda

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


                CT1 = Note1.FindControl("cmbAnagrafica")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("anagrafica"), "A")).Selected = True

                CT1 = Note1.FindControl("cmbDichiara")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("Dichiara"), "A")).Selected = True

                CT1 = Note1.FindControl("cmbLocazione")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("Locazione"), "A")).Selected = True

                CT1 = Note1.FindControl("cmbNucleo")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("Nucleo"), "A")).Selected = True

                CT1 = Note1.FindControl("cmbSottoscrittore")
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("Sottoscrittore"), "A")).Selected = True


                CType(Dom_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text = par.FormattaData(par.IfNull(myReader("decorrenza_c"), ""))
                CType(Dom_Contratto1.FindControl("txtDataDisdetta"), TextBox).Text = par.FormattaData(par.IfNull(myReader("disdetta_c"), ""))
                CType(Dom_Contratto1.FindControl("txtDataScadenza"), TextBox).Text = par.FormattaData(par.IfNull(myReader("fine_c"), ""))
                CType(Dom_Contratto1.FindControl("txtDataReg"), TextBox).Text = par.FormattaData(par.IfNull(myReader("registrazione_c"), ""))
                CType(Dom_Contratto1.FindControl("txtDataStipula"), TextBox).Text = par.FormattaData(par.IfNull(myReader("stipula_c"), ""))
                CType(Dom_Contratto1.FindControl("txtEstremi"), TextBox).Text = par.IfNull(myReader("estremi_reg_c"), "")

                CType(Dom_Contratto1.FindControl("txtAffitto"), TextBox).Text = par.IfNull(myReader("spese_locazione"), "0")
                CType(Dom_Contratto1.FindControl("txtSpese"), TextBox).Text = par.IfNull(myReader("spese_condominiali"), "0")
                CType(Dom_Contratto1.FindControl("txtRiscaldamento"), TextBox).Text = par.IfNull(myReader("spese_riscaldamento"), "0")
                CType(Dom_Contratto1.FindControl("txtIdonei"), TextBox).Text = par.IfNull(myReader("num_contratti_reg"), "0")
                CType(Dom_Contratto1.FindControl("txtMesi"), TextBox).Text = par.IfNull(myReader("mesi_contratti_reg"), "0")

                CT1 = CType(Dom_Contratto1.FindControl("cmbStatoC"), DropDownList)
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("stato_c"), "REG")).Selected = True

                CT1 = CType(Dom_Contratto1.FindControl("cmbTipoContratto"), DropDownList)
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("tipo_c"), "1")).Selected = True



                CType(Dom_Dichiara1.FindControl("txtOccupanti"), TextBox).Text = par.IfNull(myReader("occupanti_alloggio"), "0")
                CType(Dom_Dichiara1.FindControl("txtCoabitanti"), TextBox).Text = par.IfNull(myReader("nuclei_coabitanti"), "0")
                CType(Dom_Dichiara1.FindControl("txtAutonomo"), TextBox).Text = par.IfNull(myReader("num_redd_autonomo"), "0")
                CType(Dom_Dichiara1.FindControl("txtDipendenti"), TextBox).Text = par.IfNull(myReader("num_redd_dipendente"), "0")
                CType(Dom_Dichiara1.FindControl("txtpensione"), TextBox).Text = par.IfNull(myReader("num_redd_pensione"), "0")
                CType(Dom_Dichiara1.FindControl("txtSubordinato"), TextBox).Text = par.IfNull(myReader("num_redd_Subordinato"), "0")

                If par.IfNull(myReader("fl_indigente"), "0") = "0" Then
                    CType(Dom_Dichiara1.FindControl("chDifficolta"), CheckBox).Checked = False
                Else
                    CType(Dom_Dichiara1.FindControl("chDifficolta"), CheckBox).Checked = True
                End If
                CType(Dom_Dichiara1.FindControl("txtNoteIndigente"), TextBox).Text = par.IfNull(myReader("NOTE_INDIGENTE"), "")

                CT1 = CType(Dom_Dichiara1.FindControl("cmbContributo"), DropDownList)
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("tipo_pagamento"), "RID")).Selected = True

                CType(Dom_Dichiara1.FindControl("txtIntestato"), TextBox).Text = par.IfNull(myReader("intestatario_contributo"), "")
                CType(Dom_Dichiara1.FindControl("txtIban"), TextBox).Text = par.IfNull(myReader("iban"), "")
                CType(Dom_Dichiara1.FindControl("txtBanca"), TextBox).Text = par.IfNull(myReader("banca"), "")
                CType(Dom_Dichiara1.FindControl("txtUbicazione"), TextBox).Text = par.IfNull(myReader("ubicazione_banca"), "")



                CType(Dom_Abitative_1_1.FindControl("txtAnno"), TextBox).Text = par.IfNull(myReader("anno_costruzione"), "")
                CType(Dom_Abitative_1_1.FindControl("txtFoglio"), TextBox).Text = par.IfNull(myReader("foglio"), "")
                CType(Dom_Abitative_1_1.FindControl("txtParticella"), TextBox).Text = par.IfNull(myReader("particella"), "")
                CType(Dom_Abitative_1_1.FindControl("txtSub"), TextBox).Text = par.IfNull(myReader("subalterno"), "")
                CType(Dom_Abitative_1_1.FindControl("txtNumLocali"), TextBox).Text = par.IfNull(myReader("numero_locali"), "0")
                CType(Dom_Abitative_1_1.FindControl("txtSuperficie"), TextBox).Text = par.IfNull(myReader("superficie"), "0")

                If par.IfNull(myReader("fl_auto"), "0") = "0" Then
                    CType(Dom_Abitative_1_1.FindControl("ChAuto"), CheckBox).Checked = False
                Else
                    CType(Dom_Abitative_1_1.FindControl("ChAuto"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("fl_box"), "0") = "0" Then
                    CType(Dom_Abitative_1_1.FindControl("ChBox"), CheckBox).Checked = False
                Else
                    CType(Dom_Abitative_1_1.FindControl("ChBox"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("fl_cucina"), "0") = "0" Then
                    CType(Dom_Abitative_1_1.FindControl("ChCucina"), CheckBox).Checked = False
                Else
                    CType(Dom_Abitative_1_1.FindControl("ChCucina"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("fl_degrado"), "0") = "0" Then
                    CType(Dom_Abitative_1_1.FindControl("ChDegrado"), CheckBox).Checked = False
                Else
                    CType(Dom_Abitative_1_1.FindControl("ChDegrado"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("fl_acqua"), "0") = "0" Then
                    CType(Dom_Abitative_1_1.FindControl("ChPotabile"), CheckBox).Checked = False
                Else
                    CType(Dom_Abitative_1_1.FindControl("ChPotabile"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("fl_improprio"), "0") = "0" Then
                    CType(Dom_Abitative_1_1.FindControl("ChImproprio"), CheckBox).Checked = False
                Else
                    CType(Dom_Abitative_1_1.FindControl("ChImproprio"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("fl_prop_indivisa"), "0") = "0" Then
                    CType(Dom_Abitative_1_1.FindControl("ChIndivisa"), CheckBox).Checked = False
                Else
                    CType(Dom_Abitative_1_1.FindControl("ChIndivisa"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("fl_riscaldamento"), "0") = "0" Then
                    CType(Dom_Abitative_1_1.FindControl("ChRiscaldamento"), CheckBox).Checked = False
                Else
                    CType(Dom_Abitative_1_1.FindControl("ChRiscaldamento"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("fl_servizi"), "0") = "0" Then
                    CType(Dom_Abitative_1_1.FindControl("ChServizi"), CheckBox).Checked = False
                Else
                    CType(Dom_Abitative_1_1.FindControl("ChServizi"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("fl_suss_requisiti"), "0") = "0" Then
                    CType(Dom_Abitative_1_1.FindControl("ChReq"), CheckBox).Checked = False
                Else
                    CType(Dom_Abitative_1_1.FindControl("ChReq"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("cartaceo_non_disp"), "0") = "0" Then
                    CType(Note1.FindControl("ChCartacea"), CheckBox).Checked = False
                Else
                    CType(Note1.FindControl("ChCartacea"), CheckBox).Checked = True
                End If


                If par.IfNull(myReader("provv_negativo"), "0") = "0" Then
                    CType(Note1.FindControl("ChNonIdoneo"), CheckBox).Checked = False
                Else
                    CType(Note1.FindControl("ChNonIdoneo"), CheckBox).Checked = True
                End If

                CT1 = CType(Dom_Abitative_1_1.FindControl("cmbCat"), DropDownList)
                CT1.SelectedIndex = -1
                CT1.Items.FindByValue(par.IfNull(myReader("CATEGORIA_CAT"), "A1")).Selected = True


                cT = Note1.FindControl("txtNote")
                cT.Text = par.IfNull(myReader("NOTE"), "")


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

                If par.IfNull(myReader("REQUISITO10"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR9"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR9"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("REQUISITO11"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR10"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR10"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("REQUISITO12"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR11"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR11"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("REQUISITO13"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR12"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR12"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("REQUISITO14"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR13"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR13"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("REQUISITO16"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR16"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR16"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("REQUISITO17"), "0") = "0" Then
                    CType(Dom_Requisiti1.FindControl("chR17"), CheckBox).Checked = False
                Else
                    CType(Dom_Requisiti1.FindControl("chR17"), CheckBox).Checked = True
                End If

                If VSE <> 0 Then
                    If sStato <> "4" And (Replace(llISEE.Text, ".", "") < 3100 Or ((ISE - CANONE_INTEGRATO) / VSE)) < 2066 Then
                        LBLDIFFICOLTA.Visible = True
                        LBLDIFFICOLTA.Text = "STATO DI GRAVE DIFFICOLTA'"
                    End If
                Else
                    If sStato <> "4" And (Replace(llISEE.Text, ".", "") < 3100) Then
                        LBLDIFFICOLTA.Visible = True
                        LBLDIFFICOLTA.Text = "STATO DI GRAVE DIFFICOLTA'"
                    End If
                End If


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


                End If
                myReader1.Close()

                CT1 = Dom_Richiedente1.FindControl("cmbTipoIRec")
                CT1.Items.FindByValue(par.IfNull(myReader("ID_TIPO_IND_REC_DNTE"), "")).Selected = True



                txtIndici.Text = "V1=" & dm_QuotaComunale _
                           & "&V2=" & dm_QuotaRegionale _
                           & "&V3=" & dm_QuotaTotale _
                           & "&V4=" & "" _
                           & "&V5=" & "" _
                           & "&V6=" & "" _
                           & "&V7=" & par.Converti(par.IfNull(myReader("reddito_isee"), 0)) _
                           & "&V8=" & par.Converti(par.IfNull(myReader("ISR_ERP"), 0)) _
                           & "&V9=" & par.Converti(par.IfNull(myReader("ISP_ERP"), 0)) _
                           & "&V10=" & par.Converti(par.IfNull(myReader("ISe_ERP"), 0)) _
                           & "&V11=" & "" _
                           & "&V12=" & par.Converti(par.IfNull(myReader("PSE"), 0)) _
                           & "&V13=" & par.Converti(par.IfNull(myReader("VSE"), 0)) _
                           & "&PG=" & lblPG.Text & "&UJ=3"

            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT TIPO_BANDO,DATA_INIZIO FROM BANDI_fsa WHERE ID=" & lIndice_Bando
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lBando = par.IfNull(myReader("TIPO_BANDO"), "-1")
                'Select Case lBando
                '    Case 0
                '        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-1"
                '    Case 1
                '        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-2"
                '    Case 2
                '        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-3"
                'End Select
                lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2)
            End If


            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_FSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND PROGR=" & lProgr
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

            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_FSA WHERE ID=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lblPGDic.Text = lblSPG.Text & "-" & myReader("PG") & "-F205"
                'iNumComponenti = myReader("N_COMP_NUCLEO") - 1
                iNumComponenti = myReader("N_COMP_NUCLEO")


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

            par.cmd.CommandText = "SELECT F_RESIDENZA FROM COMP_PATR_IMMOB_FSA WHERE ID_COMPONENTE=" & lIndice_Componente
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                Fl_Residenza = myReader("F_RESIDENZA")
            End If
            myReader.Close()
            If Fl_Residenza = "1" Then
                CType(Dom_Abitative_2_1.FindControl("alert2"), Image).Visible = True
                CType(Dom_Abitative_2_1.FindControl("alert3"), Image).Visible = True
            End If



            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("SELECT TO_CHAR(TO_DATE(DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_NASCITA"",PERC_INVAL,INDENNITA_ACC FROM COMP_NUCLEO_FSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " ORDER BY PROGR ASC", par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "COMP_NUCLEO_FSA")
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
            'If VerificaDati(S) = False Then
            '    Response.Write("<SCRIPT>alert('Attenzione...Sono state riscontrate anomalie nella domanda. Risolvere il/i problema/i prima di salvare e stampare la domanda!')</SCRIPT>")
            '    imgStampa.Enabled = False
            '    imgStampa.ImageUrl = "ImmMaschere\blu_stampa_no.jpg"
            'Else
            '    imgStampa.Enabled = True
            '    imgStampa.ImageUrl = "ImmMaschere\blu_stampa.jpg"
            'End If



            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            Session.Add("LAVORAZIONE", "1")

            If sStato <> "2" And sStato <> "1" Then
                imgAnagrafe.Visible = False
                imgAnagrafe.Attributes.Clear()
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Pratica aperta da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
                par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_fsa WHERE ID=" & lIdDomanda
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then

                    txtDetrazioniCanone.Text = par.IfNull(myReader("importodetrazione"), "0")
                    lIndice_Bando = myReader("ID_BANDO")
                    lIdDichiarazione = myReader("ID_DICHIARAZIONE")
                    lProgr = myReader("PROGR_COMPONENTE")
                    sStato = myReader("id_stato")
                    llISEE.Text = par.Tronca(par.IfNull(myReader("reddito_isee"), 0))
                    lblPG.Text = par.IfNull(myReader("pg"), "")
                    txtDataPG.Text = par.FormattaData(par.IfNull(myReader("data_pg"), ""))
                    lblPG.ToolTip = lIdDomanda

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


                    CT1 = Note1.FindControl("cmbAnagrafica")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("anagrafica"), "A")).Selected = True

                    CT1 = Note1.FindControl("cmbDichiara")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("Dichiara"), "A")).Selected = True

                    CT1 = Note1.FindControl("cmbLocazione")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("Locazione"), "A")).Selected = True

                    CT1 = Note1.FindControl("cmbNucleo")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("Nucleo"), "A")).Selected = True

                    CT1 = Note1.FindControl("cmbSottoscrittore")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("Sottoscrittore"), "A")).Selected = True


                    CType(Dom_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text = par.FormattaData(par.IfNull(myReader("decorrenza_c"), ""))
                    CType(Dom_Contratto1.FindControl("txtDataDisdetta"), TextBox).Text = par.FormattaData(par.IfNull(myReader("disdetta_c"), ""))
                    CType(Dom_Contratto1.FindControl("txtDataScadenza"), TextBox).Text = par.FormattaData(par.IfNull(myReader("fine_c"), ""))
                    CType(Dom_Contratto1.FindControl("txtDataReg"), TextBox).Text = par.FormattaData(par.IfNull(myReader("registrazione_c"), ""))
                    CType(Dom_Contratto1.FindControl("txtDataStipula"), TextBox).Text = par.FormattaData(par.IfNull(myReader("stipula_c"), ""))
                    CType(Dom_Contratto1.FindControl("txtEstremi"), TextBox).Text = par.IfNull(myReader("estremi_reg_c"), "")

                    CType(Dom_Contratto1.FindControl("txtAffitto"), TextBox).Text = par.IfNull(myReader("spese_locazione"), "0")
                    CType(Dom_Contratto1.FindControl("txtSpese"), TextBox).Text = par.IfNull(myReader("spese_condominiali"), "0")
                    CType(Dom_Contratto1.FindControl("txtRiscaldamento"), TextBox).Text = par.IfNull(myReader("spese_riscaldamento"), "0")
                    CType(Dom_Contratto1.FindControl("txtIdonei"), TextBox).Text = par.IfNull(myReader("num_contratti_reg"), "0")
                    CType(Dom_Contratto1.FindControl("txtMesi"), TextBox).Text = par.IfNull(myReader("mesi_contratti_reg"), "0")

                    CT1 = CType(Dom_Contratto1.FindControl("cmbStatoC"), DropDownList)
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("stato_c"), "REG")).Selected = True

                    CT1 = CType(Dom_Contratto1.FindControl("cmbTipoContratto"), DropDownList)
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("tipo_c"), "1")).Selected = True



                    CType(Dom_Dichiara1.FindControl("txtOccupanti"), TextBox).Text = par.IfNull(myReader("occupanti_alloggio"), "0")
                    CType(Dom_Dichiara1.FindControl("txtCoabitanti"), TextBox).Text = par.IfNull(myReader("nuclei_coabitanti"), "0")
                    CType(Dom_Dichiara1.FindControl("txtAutonomo"), TextBox).Text = par.IfNull(myReader("num_redd_autonomo"), "0")
                    CType(Dom_Dichiara1.FindControl("txtDipendenti"), TextBox).Text = par.IfNull(myReader("num_redd_dipendente"), "0")
                    CType(Dom_Dichiara1.FindControl("txtpensione"), TextBox).Text = par.IfNull(myReader("num_redd_pensione"), "0")
                    CType(Dom_Dichiara1.FindControl("txtSubordinato"), TextBox).Text = par.IfNull(myReader("num_redd_Subordinato"), "0")

                    If par.IfNull(myReader("fl_indigente"), "0") = "0" Then
                        CType(Dom_Dichiara1.FindControl("chDifficolta"), CheckBox).Checked = False
                    Else
                        CType(Dom_Dichiara1.FindControl("chDifficolta"), CheckBox).Checked = True
                    End If
                    CType(Dom_Dichiara1.FindControl("txtNoteIndigente"), TextBox).Text = par.IfNull(myReader("NOTE_INDIGENTE"), "")


                    CType(Dom_Dichiara1.FindControl("txtIntestato"), TextBox).Text = par.IfNull(myReader("intestatario_contributo"), "")
                    CType(Dom_Dichiara1.FindControl("txtIban"), TextBox).Text = par.IfNull(myReader("iban"), "")
                    CType(Dom_Dichiara1.FindControl("txtBanca"), TextBox).Text = par.IfNull(myReader("banca"), "")
                    CType(Dom_Dichiara1.FindControl("txtUbicazione"), TextBox).Text = par.IfNull(myReader("ubicazione_banca"), "")



                    CType(Dom_Abitative_1_1.FindControl("txtAnno"), TextBox).Text = par.IfNull(myReader("anno_costruzione"), "")
                    CType(Dom_Abitative_1_1.FindControl("txtFoglio"), TextBox).Text = par.IfNull(myReader("foglio"), "")
                    CType(Dom_Abitative_1_1.FindControl("txtParticella"), TextBox).Text = par.IfNull(myReader("particella"), "")
                    CType(Dom_Abitative_1_1.FindControl("txtSub"), TextBox).Text = par.IfNull(myReader("subalterno"), "")
                    CType(Dom_Abitative_1_1.FindControl("txtNumLocali"), TextBox).Text = par.IfNull(myReader("numero_locali"), "0")
                    CType(Dom_Abitative_1_1.FindControl("txtSuperficie"), TextBox).Text = par.IfNull(myReader("superficie"), "0")

                    If par.IfNull(myReader("fl_auto"), "0") = "0" Then
                        CType(Dom_Abitative_1_1.FindControl("ChAuto"), CheckBox).Checked = False
                    Else
                        CType(Dom_Abitative_1_1.FindControl("ChAuto"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("fl_box"), "0") = "0" Then
                        CType(Dom_Abitative_1_1.FindControl("ChBox"), CheckBox).Checked = False
                    Else
                        CType(Dom_Abitative_1_1.FindControl("ChBox"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("fl_cucina"), "0") = "0" Then
                        CType(Dom_Abitative_1_1.FindControl("ChCucina"), CheckBox).Checked = False
                    Else
                        CType(Dom_Abitative_1_1.FindControl("ChCucina"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("fl_degrado"), "0") = "0" Then
                        CType(Dom_Abitative_1_1.FindControl("ChDegrado"), CheckBox).Checked = False
                    Else
                        CType(Dom_Abitative_1_1.FindControl("ChDegrado"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("fl_acqua"), "0") = "0" Then
                        CType(Dom_Abitative_1_1.FindControl("ChPotabile"), CheckBox).Checked = False
                    Else
                        CType(Dom_Abitative_1_1.FindControl("ChPotabile"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("fl_improprio"), "0") = "0" Then
                        CType(Dom_Abitative_1_1.FindControl("ChImproprio"), CheckBox).Checked = False
                    Else
                        CType(Dom_Abitative_1_1.FindControl("ChImproprio"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("fl_prop_indivisa"), "0") = "0" Then
                        CType(Dom_Abitative_1_1.FindControl("ChIndivisa"), CheckBox).Checked = False
                    Else
                        CType(Dom_Abitative_1_1.FindControl("ChIndivisa"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("fl_riscaldamento"), "0") = "0" Then
                        CType(Dom_Abitative_1_1.FindControl("ChRiscaldamento"), CheckBox).Checked = False
                    Else
                        CType(Dom_Abitative_1_1.FindControl("ChRiscaldamento"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("fl_servizi"), "0") = "0" Then
                        CType(Dom_Abitative_1_1.FindControl("ChServizi"), CheckBox).Checked = False
                    Else
                        CType(Dom_Abitative_1_1.FindControl("ChServizi"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("fl_suss_requisiti"), "0") = "0" Then
                        CType(Dom_Abitative_1_1.FindControl("ChReq"), CheckBox).Checked = False
                    Else
                        CType(Dom_Abitative_1_1.FindControl("ChReq"), CheckBox).Checked = True
                    End If


                    CT1 = CType(Dom_Abitative_1_1.FindControl("cmbCat"), DropDownList)
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByValue(par.IfNull(myReader("CATEGORIA_CAT"), "A1")).Selected = True


                    cT = Note1.FindControl("txtNote")
                    cT.Text = par.IfNull(myReader("NOTE"), "")


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

                    If par.IfNull(myReader("REQUISITO10"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR9"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR9"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("REQUISITO11"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR10"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR10"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("REQUISITO12"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR11"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR11"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("REQUISITO13"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR12"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR12"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("REQUISITO14"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR13"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR13"), CheckBox).Checked = True
                    End If

                    If par.IfNull(myReader("REQUISITO17"), "0") = "0" Then
                        CType(Dom_Requisiti1.FindControl("chR17"), CheckBox).Checked = False
                    Else
                        CType(Dom_Requisiti1.FindControl("chR17"), CheckBox).Checked = True
                    End If


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


                    End If
                    myReader1.Close()

                    CT1 = Dom_Richiedente1.FindControl("cmbTipoIRec")
                    CT1.Items.FindByValue(par.IfNull(myReader("ID_TIPO_IND_REC_DNTE"), "")).Selected = True



                    txtIndici.Text = "V1=" & "" _
                               & "&V2=" & "" _
                               & "&V3=" & "" _
                               & "&V4=" & "" _
                               & "&V5=" & "" _
                               & "&V6=" & "" _
                               & "&V7=" & par.Converti(par.IfNull(myReader("reddito_isee"), 0)) _
                               & "&V8=" & par.Converti(par.IfNull(myReader("ISR_ERP"), 0)) _
                               & "&V9=" & par.Converti(par.IfNull(myReader("ISP_ERP"), 0)) _
                               & "&V10=" & par.Converti(par.IfNull(myReader("ISe_ERP"), 0)) _
                               & "&V11=" & "" _
                               & "&V12=" & par.Converti(par.IfNull(myReader("PSE"), 0)) _
                               & "&V13=" & par.Converti(par.IfNull(myReader("VSE"), 0)) _
                               & "&PG=" & lblPG.Text & "&UJ=3"

                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT TIPO_BANDO,DATA_INIZIO FROM BANDI_fsa WHERE ID=" & lIndice_Bando
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    lBando = par.IfNull(myReader("TIPO_BANDO"), "-1")
                    'Select Case lBando
                    '    Case 0
                    '        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-1"
                    '    Case 1
                    '        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-2"
                    '    Case 2
                    '        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-3"
                    'End Select
                    lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2)
                End If


                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_FSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND PROGR=" & lProgr
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

                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_FSA WHERE ID=" & lIdDichiarazione
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    lblPGDic.Text = lblSPG.Text & "-" & myReader("PG") & "-F205"
                    ' iNumComponenti = myReader("N_COMP_NUCLEO") - 1
                    iNumComponenti = myReader("N_COMP_NUCLEO")



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

                par.cmd.CommandText = "SELECT F_RESIDENZA FROM COMP_PATR_IMMOB_FSA WHERE ID_COMPONENTE=" & lIndice_Componente
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    Fl_Residenza = myReader("F_RESIDENZA")
                End If
                myReader.Close()
                If Fl_Residenza = "1" Then
                    CType(Dom_Abitative_2_1.FindControl("alert2"), Image).Visible = True
                    CType(Dom_Abitative_2_1.FindControl("alert3"), Image).Visible = True
                End If



                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("SELECT TO_CHAR(TO_DATE(DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_NASCITA"",PERC_INVAL,INDENNITA_ACC FROM COMP_NUCLEO_FSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " ORDER BY PROGR ASC", par.OracleConn)

                Dim ds As New Data.DataSet()
                da.Fill(ds, "COMP_NUCLEO_FSA")
                DataGrid1.DataSource = ds
                DataGrid1.DataBind()
                da.Dispose()
                ds.Dispose()

                Dom_Richiedente1.DisattivaRichiedente()
                If lProgr = 0 Then
                    Dom_Richiedente1.DisattivaIndirizzo()
                End If



                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)


            Else
                Label10.Text = EX1.ToString

            End If
            Dom_Abitative_1_1.DisattivaTutto()
            Dom_Abitative_2_1.DisattivaTutto()
            Dom_Contratto1.DisattivaTutto()
            Dom_Dichiara1.DisattivaTutto()
            Dom_Requisiti1.DisattivaTutto()
            Dom_Richiedente1.DisattivaTutto()
            Note1.DisattivaTutto()

            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("TRANSAZIONE")
            HttpContext.Current.Session.Remove("CONNESSIONE")
            Session.Item("LAVORAZIONE") = "0"
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Label10.Text = ex.ToString


            'par.myTrans.Rollback()
            par.OracleConn.Close()

            'par.OracleConn.Close()
            par.OracleConn.Dispose()

            'Session.Add("ERRORE", ex.Message)
            'Response.Write("<script>top.location.href='../AutoCompilazione/Errore.aspx';</script>")

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

    Public Property dm_QuotaComunale() As Double
        Get
            If Not (ViewState("par_dm_QuotaComunale") Is Nothing) Then
                Return CDbl(ViewState("par_dm_QuotaComunale"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Double)
            ViewState("par_dm_QuotaComunale") = value
        End Set

    End Property

    Public Property dm_QuotaRegionale() As Double
        Get
            If Not (ViewState("par_dm_QuotaComunale") Is Nothing) Then
                Return CDbl(ViewState("par_dm_QuotaRegionale"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Double)
            ViewState("par_dm_QuotaRegionale") = value
        End Set

    End Property

    Public Property dm_QuotaTotale() As Double
        Get
            If Not (ViewState("par_dm_QuotaTotale") Is Nothing) Then
                Return CDbl(ViewState("par_dm_QuotaTotale"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Double)
            ViewState("par_dm_QuotaTotale") = value
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


    Public Property stato_domanda() As String
        Get
            If Not (ViewState("par_stato_domanda") Is Nothing) Then
                Return CStr(ViewState("par_stato_domanda"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_stato_domanda") = value
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

    Public Property CANONE_INTEGRATO() As Double
        Get
            If Not (ViewState("par_CANONE_INTEGRATO") Is Nothing) Then
                Return CDbl(ViewState("par_CANONE_INTEGRATO"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Double)
            ViewState("par_CANONE_INTEGRATO") = value
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
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                par.myTrans.Rollback()

                par.OracleConn.Close()
                par.OracleConn.Dispose()
                HttpContext.Current.Session.Remove("TRANSAZIONE")
                HttpContext.Current.Session.Remove("CONNESSIONE")
                Session.Item("LAVORAZIONE") = "0"
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Page.Dispose()
                If Fl_Integrazione = "1" Then
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

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click


        Try
            bMemorizzato = False

            If IsNumeric(CType(Dom_Dichiara1.FindControl("txtOccupanti"), TextBox).Text) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di occupanti deve essere un valore numerico!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If


            If InStr(CType(Dom_Dichiara1.FindControl("txtOccupanti"), TextBox).Text, ".") <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di occupanti deve essere un valore numerico intero!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            Else
                If InStr(CType(Dom_Dichiara1.FindControl("txtOccupanti"), TextBox).Text, ",") <> 0 Then
                    Response.Write("<SCRIPT>alert('Attenzione...Il numero di occupanti deve essere un valore numerico intero!')</SCRIPT>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                    'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                    Exit Try
                End If
            End If

            If IsNumeric(CType(Dom_Dichiara1.FindControl("txtCoabitanti"), TextBox).Text) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di nuclei coabitanti deve essere un valore numerico!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If


            If InStr(CType(Dom_Dichiara1.FindControl("txtCoabitanti"), TextBox).Text, ".") <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di nuclei coabitanti deve essere un valore numerico intero!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            Else
                If InStr(CType(Dom_Dichiara1.FindControl("txtCoabitanti"), TextBox).Text, ",") <> 0 Then
                    Response.Write("<SCRIPT>alert('Attenzione...Il numero di nuclei coabitanti deve essere un valore numerico intero!')</SCRIPT>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                    'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                    Exit Try
                End If
            End If

            If IsNumeric(CType(Dom_Dichiara1.FindControl("txtAutonomo"), TextBox).Text) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con reddito autonomo deve essere un valore numerico!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If
            If InStr(CType(Dom_Dichiara1.FindControl("txtAutonomo"), TextBox).Text, ".") <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con reddito autonomo deve essere un valore numerico intero!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            Else
                If InStr(CType(Dom_Dichiara1.FindControl("txtAutonomo"), TextBox).Text, ",") <> 0 Then
                    Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con reddito autonomo deve essere un valore numerico intero!')</SCRIPT>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                    'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                    Exit Try
                End If
            End If


            If IsNumeric(CType(Dom_Dichiara1.FindControl("txtDipendenti"), TextBox).Text) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con reddito dipendente deve essere un valore numerico!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If
            If InStr(CType(Dom_Dichiara1.FindControl("txtDipendenti"), TextBox).Text, ".") <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con reddito dipendente deve essere un valore numerico intero!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            Else
                If InStr(CType(Dom_Dichiara1.FindControl("txtDipendenti"), TextBox).Text, ",") <> 0 Then
                    Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con reddito dipendente deve essere un valore numerico intero!')</SCRIPT>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                    'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                    Exit Try
                End If
            End If


            If IsNumeric(CType(Dom_Dichiara1.FindControl("txtpensione"), TextBox).Text) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con reddito da pensione deve essere un valore numerico!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If InStr(CType(Dom_Dichiara1.FindControl("txtpensione"), TextBox).Text, ".") <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con reddito da pensione deve essere un valore numerico intero!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            Else
                If InStr(CType(Dom_Dichiara1.FindControl("txtpensione"), TextBox).Text, ",") <> 0 Then
                    Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con reddito da pensione deve essere un valore numerico intero!')</SCRIPT>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                    'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                    Exit Try
                End If
            End If


            If IsNumeric(CType(Dom_Dichiara1.FindControl("txtSubordinato"), TextBox).Text) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con reddito subordinato deve essere un valore numerico!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If
            If InStr(CType(Dom_Dichiara1.FindControl("txtSubordinato"), TextBox).Text, ".") <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con reddito subordinato deve essere un valore numerico intero!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            Else
                If InStr(CType(Dom_Dichiara1.FindControl("txtSubordinato"), TextBox).Text, ",") <> 0 Then
                    Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con reddito subordinato deve essere un valore numerico intero!')</SCRIPT>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                    'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                    Exit Try
                End If
            End If

            If IsNumeric(CType(Dom_Abitative_1_1.FindControl("txtAnno"), TextBox).Text) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...Anno costruzione deve essere un valore numerico!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If
            If Len(CType(Dom_Abitative_1_1.FindControl("txtAnno"), TextBox).Text) <> 4 Then
                Response.Write("<SCRIPT>alert('Attenzione...Anno costruzione deve essere lungo 4!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If IsNumeric(CType(Dom_Abitative_1_1.FindControl("txtNumLocali"), TextBox).Text) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero locali con reddito subordinato deve essere un valore numerico!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If
            If InStr(CType(Dom_Abitative_1_1.FindControl("txtNumLocali"), TextBox).Text, ".") <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero locali con reddito subordinato deve essere un valore numerico intero!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            Else
                If InStr(CType(Dom_Abitative_1_1.FindControl("txtNumLocali"), TextBox).Text, ",") <> 0 Then
                    Response.Write("<SCRIPT>alert('Attenzione...Il numero locali con reddito subordinato deve essere un valore numerico intero!')</SCRIPT>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                    'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                    Exit Try
                End If
            End If

            If IsNumeric(par.VirgoleInPunti(CType(Dom_Abitative_1_1.FindControl("txtSuperficie"), TextBox).Text)) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...la superficie deve essere un valore numerico!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If IsNumeric(CType(Dom_Contratto1.FindControl("txtAffitto"), TextBox).Text) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...spese locazione deve essere un valore numerico!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If
            If InStr(CType(Dom_Contratto1.FindControl("txtAffitto"), TextBox).Text, ".") <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...spese locazione deve essere un valore numerico intero!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            Else
                If InStr(CType(Dom_Contratto1.FindControl("txtAffitto"), TextBox).Text, ",") <> 0 Then
                    Response.Write("<SCRIPT>alert('Attenzione...spese locazione deve essere un valore numerico intero!')</SCRIPT>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                    'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                    Exit Try
                End If
            End If

            If IsNumeric(CType(Dom_Contratto1.FindControl("txtSpese"), TextBox).Text) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...spese condominiali deve essere un valore numerico!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If
            If InStr(CType(Dom_Contratto1.FindControl("txtSpese"), TextBox).Text, ".") <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...spese condominiali deve essere un valore numerico intero!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            Else
                If InStr(CType(Dom_Contratto1.FindControl("txtSpese"), TextBox).Text, ",") <> 0 Then
                    Response.Write("<SCRIPT>alert('Attenzione...spese condominiali deve essere un valore numerico intero!')</SCRIPT>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                    'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                    Exit Try
                End If
            End If

            If IsNumeric(CType(Dom_Contratto1.FindControl("txtRiscaldamento"), TextBox).Text) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...spese riscaldamento deve essere un valore numerico!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If
            If InStr(CType(Dom_Contratto1.FindControl("txtRiscaldamento"), TextBox).Text, ".") <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...spese riscaldamento deve essere un valore numerico intero!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            Else
                If InStr(CType(Dom_Contratto1.FindControl("txtRiscaldamento"), TextBox).Text, ",") <> 0 Then
                    Response.Write("<SCRIPT>alert('Attenzione...spese riscaldamento deve essere un valore numerico intero!')</SCRIPT>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                    'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                    Exit Try
                End If
            End If

            If IsNumeric(CType(Dom_Contratto1.FindControl("txtIdonei"), TextBox).Text) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...numero contratti deve essere un valore numerico!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If
            If InStr(CType(Dom_Contratto1.FindControl("txtIdonei"), TextBox).Text, ".") <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...numero contratti deve essere un valore numerico intero!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            Else
                If InStr(CType(Dom_Contratto1.FindControl("txtIdonei"), TextBox).Text, ",") <> 0 Then
                    Response.Write("<SCRIPT>alert('Attenzione...numero contratti deve essere un valore numerico intero!')</SCRIPT>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                    'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                    Exit Try
                End If
            End If


            If IsNumeric(CType(Dom_Contratto1.FindControl("txtMesi"), TextBox).Text) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...numero mesi contratti deve essere un valore numerico!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If
            If InStr(CType(Dom_Contratto1.FindControl("txtMesi"), TextBox).Text, ".") <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...numero mesi contratti deve essere un valore numerico intero!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            Else
                If InStr(CType(Dom_Contratto1.FindControl("txtMesi"), TextBox).Text, ",") <> 0 Then
                    Response.Write("<SCRIPT>alert('Attenzione...numero mesi contratti deve essere un valore numerico intero!')</SCRIPT>")
                    imgStampa.Enabled = False
                    imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                    'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                    Exit Try
                End If
            End If

            If Len(CType(Dom_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text) <> 10 And Len(CType(Dom_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text) <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...data decorrenza non valida!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If Len(CType(Dom_Contratto1.FindControl("txtDataDisdetta"), TextBox).Text) <> 10 And Len(CType(Dom_Contratto1.FindControl("txtDataDisdetta"), TextBox).Text) <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...data disdetta non valida!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If Len(CType(Dom_Contratto1.FindControl("txtDataScadenza"), TextBox).Text) <> 10 And Len(CType(Dom_Contratto1.FindControl("txtDataScadenza"), TextBox).Text) <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...data scadenza non valida!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If stato_domanda <> "4" And Len(CType(Dom_Contratto1.FindControl("txtDataReg"), TextBox).Text) <> 10 And Len(CType(Dom_Contratto1.FindControl("txtDataReg"), TextBox).Text) <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...data registrazione non valida!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If Len(CType(Dom_Contratto1.FindControl("txtDataStipula"), TextBox).Text) <> 10 And Len(CType(Dom_Contratto1.FindControl("txtDataStipula"), TextBox).Text) <> 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...data stipula non valida!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If CType(Dom_Contratto1.FindControl("cmbStatoC"), DropDownList).SelectedItem.Value = "REG" And CType(Dom_Contratto1.FindControl("txtDataReg"), TextBox).Text = "" Then
                Response.Write("<SCRIPT>alert('Attenzione...Se il contratto è REGISTRATO la data di registrazione è obbligatoria!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If iNumComponenti < Val(CType(Dom_Dichiara1.FindControl("txtAutonomo"), TextBox).Text) Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con redditi autonomo deve essere inferiore al numero dei componenti inseriti nella dichiarazione!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If iNumComponenti < Val(CType(Dom_Dichiara1.FindControl("txtDipendenti"), TextBox).Text) Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con redditi dipendente deve essere inferiore al numero dei componenti inseriti nella dichiarazione!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If iNumComponenti < Val(CType(Dom_Dichiara1.FindControl("txtpensione"), TextBox).Text) Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con redditi pensione deve essere inferiore al numero dei componenti inseriti nella dichiarazione!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If iNumComponenti < Val(CType(Dom_Dichiara1.FindControl("txtSubordinato"), TextBox).Text) Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di componenti con redditi subordinato deve essere inferiore al numero dei componenti inseriti nella dichiarazione!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If Len(CType(Dom_Dichiara1.FindControl("txtIban"), TextBox).Text) <> 27 And CType(Dom_Dichiara1.FindControl("cmbContributo"), DropDownList).SelectedItem.Value = "ACE" Then
                Response.Write("<SCRIPT>alert('Attenzione...Pagamento tramite Bonifico. IBAN (27 caratteri) obbligatorio!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If Val(CType(Dom_Contratto1.FindControl("txtMesi"), TextBox).Text) < 1 Or Val(CType(Dom_Contratto1.FindControl("txtMesi"), TextBox).Text) > 12 Then
                Response.Write("<SCRIPT>alert('Attenzione...Il numero di mesi di locazione deve essere compreso tra 1 e 12!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            Dim Catastali As String = "SI"
            If (CType(Dom_Abitative_1_1.FindControl("txtFoglio"), TextBox).Text = "" Or CType(Dom_Abitative_1_1.FindControl("txtFoglio"), TextBox).Text = "0") Or (CType(Dom_Abitative_1_1.FindControl("txtParticella"), TextBox).Text = "" Or CType(Dom_Abitative_1_1.FindControl("txtParticella"), TextBox).Text = "0") Or (CType(Dom_Abitative_1_1.FindControl("txtSub"), TextBox).Text = "" Or CType(Dom_Abitative_1_1.FindControl("txtSub"), TextBox).Text = "0") Then
                Response.Write("<SCRIPT>alert('Attenzione...Non sono stati inseriti i dati catastali dell/unità (Foglio,Mappale,Sub), non sarà possibile verificare se è gia stato chiesto un contributo per questa unità!')</SCRIPT>")
                Catastali = "NO"
            End If

            If chDaLiquidare.Checked = True And dm_QuotaTotale = 0 Then
                Response.Write("<SCRIPT>alert('Attenzione...La domanda non si può liquidare!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If chDaLiquidare.Checked = True And CType(Note1.FindControl("ChCartacea"), CheckBox).Checked = True Then
                Response.Write("<SCRIPT>alert('Attenzione...La domanda non si può liquidare!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            If IsNumeric(txtDetrazioniCanone.Text) = False Then
                Response.Write("<SCRIPT>alert('Attenzione...Il valore della detrazione del canone deve essere un valore numerico intero!')</SCRIPT>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\NuoveImm\Img_No_stampa.png"
                'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            'If chDaLiquidare.Checked = True And CType(Note1.FindControl("ChNonIdoneo"), CheckBox).Checked = True Then
            '    Response.Write("<SCRIPT>alert('Attenzione...La domanda non si può liquidare, è presente provve!')</SCRIPT>")
            '    imgStampa.Enabled = False
            '    'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
            '    Exit Try
            'End If


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            If Catastali = "SI" Then
                Dim ALTREUNITA As String = ""

                par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_fsa WHERE id_bando=" & lIndice_Bando & " and ID<>" & lIdDomanda & " and FOGLIO='" & par.PulisciStrSql(CType(Dom_Abitative_1_1.FindControl("txtFoglio"), TextBox).Text) & "' and particella='" & par.PulisciStrSql(CType(Dom_Abitative_1_1.FindControl("txtParticella"), TextBox).Text) & "' and subalterno='" & par.PulisciStrSql(CType(Dom_Abitative_1_1.FindControl("txtSub"), TextBox).Text) & "'"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    ALTREUNITA = ALTREUNITA & par.IfNull(myReader1("PG"), "") & " "
                End While
                myReader1.Close()
                If ALTREUNITA <> "" Then
                    Response.Write("<SCRIPT>alert('Attenzione...è stato già chiesto il contributo per questa unità abitativa nelle domande (" & ALTREUNITA & "). In fase di elaborazione la domanda sarà esclusa.')</SCRIPT>")
                End If

            End If

            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            'End If

            Dim sStringaSql As String
            Dim Fdetrazione As String = "0"
            Dim impdetrazione As Long = 0

            impdetrazione = Val(txtDetrazioniCanone.Text)

            If Val(txtDetrazioniCanone.Text) > 0 Then
                Fdetrazione = "1"
            End If
            If Val(txtDetrazioniCanone.Text) < 0 Then
                Fdetrazione = "0"
                impdetrazione = 0
            End If

            sStringaSql = "UPDATE DOMANDE_BANDO_fsa SET importodetrazione=" & impdetrazione & ",flagdetrazione=" & Fdetrazione & ",ID_OPERATORE_CARICO=" & cmbOperatore.SelectedItem.Value & ",FL_DA_LIQUIDARE='" & Valore01(chDaLiquidare.Checked) & "',FL_MANDATO_EFF='" & Valore01(ChMandato.Checked) & "',ID_BANDO=" & lIndice_Bando _
                      & ",ID_DICHIARAZIONE=" & lIdDichiarazione & "," _
                      & " PROGR_COMPONENTE=" & lProgr _
                      & ",PRESSO_REC_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtPresso"), TextBox).Text) _
                      & "',ID_LUOGO_REC_DNTE=" & CType(Dom_Richiedente1.FindControl("cmbComuneRec"), DropDownList).SelectedItem.Value _
                      & ",ID_TIPO_IND_REC_DNTE=" & CType(Dom_Richiedente1.FindControl("cmbTipoIRec"), DropDownList).SelectedItem.Value _
                      & ",IND_REC_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtIndirizzoRec"), TextBox).Text) _
                      & "',CAP_REC_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtCAPRec"), TextBox).Text) & "',CIVICO_REC_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtCivicoRec"), TextBox).Text) _
                      & "',TELEFONO_REC_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtTelRec"), TextBox).Text) _
                      & "',PG='" & lblPG.Text _
                      & "',stato_c='" & CType(Dom_Contratto1.FindControl("cmbStatoC"), DropDownList).SelectedItem.Value _
                      & "',tipo_c='" & CType(Dom_Contratto1.FindControl("cmbTipoContratto"), DropDownList).SelectedItem.Value _
                      & "',occupanti_alloggio=" & CType(Dom_Dichiara1.FindControl("txtOccupanti"), TextBox).Text _
                      & ",nuclei_coabitanti=" & CType(Dom_Dichiara1.FindControl("txtCoabitanti"), TextBox).Text _
                      & ",num_redd_autonomo=" & CType(Dom_Dichiara1.FindControl("txtAutonomo"), TextBox).Text _
                      & ",num_redd_dipendente=" & CType(Dom_Dichiara1.FindControl("txtDipendenti"), TextBox).Text _
                      & ",num_redd_pensione=" & CType(Dom_Dichiara1.FindControl("txtpensione"), TextBox).Text _
                      & ",num_redd_Subordinato=" & CType(Dom_Dichiara1.FindControl("txtSubordinato"), TextBox).Text _
                      & ",fl_indigente='" & Valore01(CType(Dom_Dichiara1.FindControl("chDifficolta"), CheckBox).Checked) _
                      & "',NOTE_INDIGENTE='" & par.PulisciStrSql(CType(Dom_Dichiara1.FindControl("txtNoteIndigente"), TextBox).Text) _
                      & "',tipo_pagamento='" & CType(Dom_Dichiara1.FindControl("cmbContributo"), DropDownList).SelectedItem.Value _
                      & "',intestatario_contributo='" & par.PulisciStrSql(CType(Dom_Dichiara1.FindControl("txtIntestato"), TextBox).Text) _
                      & "',IBAN='" & par.PulisciStrSql(CType(Dom_Dichiara1.FindControl("txtIban"), TextBox).Text) _
                      & "',BANCA='" & par.PulisciStrSql(CType(Dom_Dichiara1.FindControl("txtBanca"), TextBox).Text) _
                      & "',ubicazione_banca='" & par.PulisciStrSql(CType(Dom_Dichiara1.FindControl("txtUbicazione"), TextBox).Text) _
                      & "',anno_costruzione='" & par.PulisciStrSql(CType(Dom_Abitative_1_1.FindControl("txtAnno"), TextBox).Text) _
                      & "',FOGLIO='" & par.PulisciStrSql(CType(Dom_Abitative_1_1.FindControl("txtFoglio"), TextBox).Text) _
                      & "',particella='" & par.PulisciStrSql(CType(Dom_Abitative_1_1.FindControl("txtParticella"), TextBox).Text) _
                      & "',subalterno='" & par.PulisciStrSql(CType(Dom_Abitative_1_1.FindControl("txtSub"), TextBox).Text) _
                      & "',numero_locali=" & CType(Dom_Abitative_1_1.FindControl("txtNumLocali"), TextBox).Text _
                      & ",superficie=" & par.VirgoleInPunti(CType(Dom_Abitative_1_1.FindControl("txtSuperficie"), TextBox).Text) _
                      & ",fl_auto='" & Valore01(CType(Dom_Abitative_1_1.FindControl("ChAuto"), CheckBox).Checked) _
                      & "',fl_box='" & Valore01(CType(Dom_Abitative_1_1.FindControl("ChBox"), CheckBox).Checked) _
                      & "',fl_cucina='" & Valore01(CType(Dom_Abitative_1_1.FindControl("ChCucina"), CheckBox).Checked) _
                      & "',fl_degrado='" & Valore01(CType(Dom_Abitative_1_1.FindControl("ChDegrado"), CheckBox).Checked) _
                      & "',fl_acqua='" & Valore01(CType(Dom_Abitative_1_1.FindControl("ChPotabile"), CheckBox).Checked) _
                      & "',fl_improprio='" & Valore01(CType(Dom_Abitative_1_1.FindControl("ChImproprio"), CheckBox).Checked) _
                      & "',fl_prop_indivisa='" & Valore01(CType(Dom_Abitative_1_1.FindControl("ChIndivisa"), CheckBox).Checked) _
                      & "',fl_riscaldamento='" & Valore01(CType(Dom_Abitative_1_1.FindControl("ChRiscaldamento"), CheckBox).Checked) _
                      & "',fl_servizi='" & Valore01(CType(Dom_Abitative_1_1.FindControl("ChServizi"), CheckBox).Checked) _
                      & "',fl_suss_requisiti='" & Valore01(CType(Dom_Abitative_1_1.FindControl("ChReq"), CheckBox).Checked) _
                      & "',CATEGORIA_CAT='" & CType(Dom_Abitative_1_1.FindControl("cmbCat"), DropDownList).SelectedItem.Value
            sStringaSql = sStringaSql _
                       & "',DATA_PG='" & par.AggiustaData(txtDataPG.Text) _
                       & "',NOTE='" & par.PulisciStrSql(CType(Note1.FindControl("txtNote"), TextBox).Text) _
                       & "',ANAGRAFICA='" & CType(Note1.FindControl("cmbAnagrafica"), DropDownList).SelectedItem.Value _
                       & "',DICHIARA='" & CType(Note1.FindControl("cmbDichiara"), DropDownList).SelectedItem.Value _
                       & "',LOCAZIONE='" & CType(Note1.FindControl("cmbLocazione"), DropDownList).SelectedItem.Value _
                       & "',NUCLEO='" & CType(Note1.FindControl("cmbNUCLEO"), DropDownList).SelectedItem.Value _
                       & "',SOTTOSCRITTORE='" & CType(Note1.FindControl("cmbSOTTOSCRITTORE"), DropDownList).SelectedItem.Value & "',"
            sStringaSql = sStringaSql _
                       & "decorrenza_c='" & par.AggiustaData(CType(Dom_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text) _
                       & "',disdetta_c='" & par.AggiustaData(CType(Dom_Contratto1.FindControl("txtDataDisdetta"), TextBox).Text) _
                       & "',fine_c='" & par.AggiustaData(CType(Dom_Contratto1.FindControl("txtDataScadenza"), TextBox).Text) _
                       & "',registrazione_c='" & par.AggiustaData(CType(Dom_Contratto1.FindControl("txtDataReg"), TextBox).Text) _
                       & "',stipula_c='" & par.AggiustaData(CType(Dom_Contratto1.FindControl("txtDataStipula"), TextBox).Text) _
                       & "',estremi_reg_c='" & par.PulisciStrSql(CType(Dom_Contratto1.FindControl("txtEstremi"), TextBox).Text) _
                       & "',spese_locazione=" & CType(Dom_Contratto1.FindControl("txtAffitto"), TextBox).Text _
                       & ",spese_condominiali=" & CType(Dom_Contratto1.FindControl("txtSpese"), TextBox).Text _
                       & ",spese_riscaldamento=" & CType(Dom_Contratto1.FindControl("txtRiscaldamento"), TextBox).Text _
                       & ",num_contratti_reg=" & CType(Dom_Contratto1.FindControl("txtIdonei"), TextBox).Text _
                       & ",mesi_contratti_reg=" & CType(Dom_Contratto1.FindControl("txtMesi"), TextBox).Text _
                       & ",REQUISITO1='" & Valore01(CType(Dom_Requisiti1.FindControl("chR1"), CheckBox).Checked) _
                       & "',REQUISITO2='" & Valore01(CType(Dom_Requisiti1.FindControl("chR2"), CheckBox).Checked) _
                       & "',REQUISITO3='" & Valore01(CType(Dom_Requisiti1.FindControl("chR3"), CheckBox).Checked) _
                       & "',REQUISITO4='" & Valore01(CType(Dom_Requisiti1.FindControl("chR4"), CheckBox).Checked) _
                       & "',REQUISITO5='" & Valore01(CType(Dom_Requisiti1.FindControl("chR5"), CheckBox).Checked) _
                       & "',REQUISITO7='" & Valore01(CType(Dom_Requisiti1.FindControl("chR6"), CheckBox).Checked) _
                       & "',REQUISITO8='" & Valore01(CType(Dom_Requisiti1.FindControl("chR7"), CheckBox).Checked) _
                       & "',REQUISITO9='" & Valore01(CType(Dom_Requisiti1.FindControl("chR8"), CheckBox).Checked) _
                       & "',REQUISITO10='" & Valore01(CType(Dom_Requisiti1.FindControl("chR9"), CheckBox).Checked) _
                       & "',REQUISITO11='" & Valore01(CType(Dom_Requisiti1.FindControl("chR10"), CheckBox).Checked) _
                       & "',REQUISITO12='" & Valore01(CType(Dom_Requisiti1.FindControl("chR11"), CheckBox).Checked) _
                       & "',REQUISITO13='" & Valore01(CType(Dom_Requisiti1.FindControl("chR12"), CheckBox).Checked) _
                       & "',REQUISITO14='" & Valore01(CType(Dom_Requisiti1.FindControl("chR13"), CheckBox).Checked) _
                       & "',REQUISITO16='" & Valore01(CType(Dom_Requisiti1.FindControl("chR16"), CheckBox).Checked) _
                       & "',REQUISITO17='" & Valore01(CType(Dom_Requisiti1.FindControl("chR17"), CheckBox).Checked) _
                       & "',fl_rinnovo='1',CARTACEO_NON_DISP='" & Valore01(CType(Note1.FindControl("ChCartacea"), CheckBox).Checked) _
                       & "',PROVV_NEGATIVO='" & Valore01(CType(Note1.FindControl("ChNonIdoneo"), CheckBox).Checked) _
                       & "' WHERE ID = " & lIdDomanda
            par.cmd.CommandText = sStringaSql
            par.cmd.ExecuteNonQuery()

            If lProgr <> 0 Then
                If CType(Dom_Richiedente1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text = "ITALIA" Then
                    sStringaSql = "UPDATE COMP_NAS_RES_fsa SET CAP_RES='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtCAPRes"), TextBox).Text) & "',ID_LUOGO_RES_DNTE=" & CType(Dom_Richiedente1.FindControl("cmbComuneRes"), DropDownList).SelectedItem.Value & ",ID_TIPO_IND_RES_DNTE=" & CType(Dom_Richiedente1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value & ",IND_RES_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtIndRes"), TextBox).Text) & "',CIVICO_RES_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtCivicoRes"), TextBox).Text) & "',TELEFONO_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtTelRes"), TextBox).Text) & "' WHERE ID_COMPONENTE=" & lIndice_Componente
                    par.cmd.CommandText = sStringaSql
                    par.cmd.ExecuteNonQuery()
                Else
                    sStringaSql = "UPDATE COMP_NAS_RES_fsa SET CAP_RES='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtCAPRes"), TextBox).Text) & "',ID_LUOGO_RES_DNTE=" & CType(Dom_Richiedente1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Value & ",ID_TIPO_IND_RES_DNTE=" & CType(Dom_Richiedente1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value & ",IND_RES_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtIndRes"), TextBox).Text) & "',CIVICO_RES_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtCivicoRes"), TextBox).Text) & "',TELEFONO_DNTE='" & par.PulisciStrSql(CType(Dom_Richiedente1.FindControl("txtTelRes"), TextBox).Text) & "' WHERE ID_COMPONENTE=" & lIndice_Componente
                    par.cmd.CommandText = sStringaSql
                    par.cmd.ExecuteNonQuery()
                End If
            End If

            If lNuovaDomanda = 1 Then
                sStringaSql = "INSERT INTO EVENTI_BANDI_fsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','1" _
                      & "','F01','','I')"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
                Session.Item("ID_NUOVA_DIC") = ""
                lNuovaDomanda = 0
            Else
                sStringaSql = "INSERT INTO EVENTI_BANDI_fsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & stato_domanda _
                      & "','F03','','I')"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()

            End If

            If Session.Item("ANAGRAFE") = "1" Then
                'imgAnagrafe.Attributes.Clear()
                imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('../Anagrafe.aspx?ID=" & par.criptamolto(lIdDichiarazione) & "&T=3','Anagrafe','top=0,left=0,width=600,height=400');")
            End If

            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)


            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_fsa WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader.Close()

            imgStampa.Enabled = True
            imgStampa.Visible = True
            imgStampa.ImageUrl = "..\NuoveImm\Img_stampa.png"
            'imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa.jpg"
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



    Protected Sub imgStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStampa.Click
        'fl_rinnovo='0'
        'METTERE EVENTO F057 SE ESClUSA
        Call btnSalva_Click(sender, e)
        If bMemorizzato = True Then
            imgAttendi.Visible = True
            DeviSalvare = False
            CalcolaStampa()
            imgAttendi.Visible = False
            'If Fl_Integrazione = "1" Then
            '    Session.Item("CONFERMATO") = "0"
            'End If
        Else
            imgStampa.Enabled = False
            imgStampa.ImageUrl = "ImmMaschere\blu_stampa_no.jpg"
        End If
    End Sub

    Private Function CalcolaStampa()
        Dim sStringaSql As String = ""
        Try
            Dim AnnoBando As String = ""
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
            Dim oltre65 As Boolean

            Dim detrazioni_oltre_65 As Double



            Dim MINORI As Integer
            Dim adulti As Integer
            Dim TASSO_RENDIMENTO As Double

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            Dim CODICEANAGRAFICO As String = ""
            par.cmd.CommandText = "SELECT operatori.*,caf_web.cod_caf as ENTE from operatori,caf_web where operatori.id_caf=caf_web.id and operatori.ID=" & Session.Item("ID_OPERATORE")
            Dim myReaderENTE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderENTE.Read() Then
                CODICEANAGRAFICO = par.IfNull(myReaderENTE("ENTE"), "") & " - " & par.IfNull(myReaderENTE("COD_ANA"), "")
            End If
            myReaderENTE.Close()

            par.cmd.CommandText = "select * from bandi_fsa where id=" & lIndice_Bando
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                AnnoBando = Mid(par.IfNull(myReader("data_inizio"), "    "), 1, 4)
                TASSO_RENDIMENTO = par.IfNull(myReader("tasso_rendimento"), "0")
            End If
            myReader.Close()


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



            Dim E1 As Double = 0
            Dim E2 As Double = 0
            Dim E3FSA As Double = 0
            Dim E4FSA As Double = 0


            Dim F12 As Double = 0
            Dim F13 As Double = 0
            Dim F16FSA As Double = 0
            Dim F18FSA As Double = 0
            Dim F24FSA As Double = 0

            Dim L1 As Double = 0
            Dim L2 As Double = 0
            Dim L3 As Double = 0
            Dim L4 As Double = 0
            Dim L5 As Double = 0
            Dim L6 As Double = 0
            Dim L7 As Double = 0
            Dim L8 As Double = 0
            Dim L9 As Double = 0
            Dim L10 As Double = 0
            Dim L11 As Double = 0
            Dim L12 As Double = 0
            Dim L13 As Double = 0
            Dim L14 As Double = 0

            Dim ENTRAMBI_GENITORI As String = ""
            Dim HPSICO As Integer
            Dim LAVORO_IMPRESA As String = ""

            Dim quotacomunale As Double = 0
            Dim quotaregionale As Double = 0
            Dim maxContributo As Double = 0

            Dim IDONEO_PRESUNTO As String = ""

            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_FSA WHERE ID=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                ENTRAMBI_GENITORI = par.IfNull(myReader("ENTRAMBI_GENITORI"), "0")
                HPSICO = par.IfNull(myReader("H_PSICO"), 0)
                LAVORO_IMPRESA = par.IfNull(myReader("LAVORO_IMPRESA"), "0")

                INV_100_CON = par.IfNull(myReader("N_INV_100_CON"), 0)
                INV_100_NO = par.IfNull(myReader("N_INV_100_SENZA"), 0)
                INV_66_99 = par.IfNull(myReader("N_INV_100_66"), 0)

            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_FSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader1.Close()
            detrazioni_oltre_65 = 0
            TOT_COMPONENTI = 0
            While myReader.Read
                TOT_COMPONENTI = TOT_COMPONENTI + 1
                oltre65 = False
                If par.RicavaEtaChiusura(par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")), "20111111") >= 18 Then
                    adulti = adulti + 1
                    If par.RicavaEtaChiusura(par.FormattaData(myReader("DATA_NASCITA")), "20111111") >= 65 Then
                        oltre65 = True
                    End If
                Else
                    MINORI = MINORI + 1
                End If

                par.cmd.CommandText = "SELECT * FROM COMP_DETRAZIONI_FSA WHERE id_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    If par.IfNull(myReader1("id_tipo"), 0) <> 3 Then
                        DETRAZIONI = DETRAZIONI + par.IfNull(myReader1("IMPORTO"), 0)
                    Else
                        If oltre65 = True Then
                            detrazioni_oltre_65 = detrazioni_oltre_65 + par.IfNull(myReader1("IMPORTO"), 0)
                        End If
                    End If

                End While
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_FSA WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("REDDITO_IRPEF"), 0) + par.IfNull(myReader1("PROV_AGRARI"), 0)
                End While
                myReader1.Close()


                par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_FSA WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("IMPORTO"), 0)

                End While
                myReader1.Close()

                DETRAZIONI_FRAGILE = 0


                par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB_FSA WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    MOBILI = MOBILI + par.IfNull(myReader1("IMPORTO"), 0)

                End While
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB_FSA WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
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

            End While
            myReader.Close()


            DETRAZIONI_FR = 0 'PER FSA NON VENGNO CALCOLATE DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)

            FIGURATIVO_MOBILI = (MOBILI \ 5165) * 5165

            If detrazioni_oltre_65 > 2582 Then detrazioni_oltre_65 = 2582

            DETRAZIONI = DETRAZIONI + detrazioni_oltre_65
            E1 = REDDITO_COMPLESSIVO 'Format(REDDITO_COMPLESSIVO, "##,##0.00")
            E2 = ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) 'Format(((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100), "##,##0.00")
            E3FSA = DETRAZIONI 'Format(DETRAZIONI, "##,##0.00")



            ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
            E4FSA = ISEE_ERP 'Format(ISEE_ERP, "##,##0.00")
            L1 = E4FSA


            F12 = FIGURATIVO_MOBILI 'Format(FIGURATIVO_MOBILI, "##,##0.00")


            If ISEE_ERP < 0 Then
                ISEE_ERP = 0
            End If

            ISR_ERP = ISEE_ERP

            ISEE_ERP = 0

            TOTALE_IMMOBILI = (IMMOBILI - MUTUI) + (IMMOBILI_RESIDENZA - MUTUI_RESIDENZA)
            F13 = TOTALE_IMMOBILI

            ''F16FSA = (((TOTALE_IMMOBILI + MOBILI) \ 5165) * 5165)
            ''TOTALE_ISEE_ERP = (((TOTALE_IMMOBILI + MOBILI) \ 5165) * 5165) * 0.05
            ''TOTALE_PATRIMONIO_ISEE_ERP = (TOTALE_IMMOBILI + MOBILI)


            F16FSA = (((TOTALE_IMMOBILI + FIGURATIVO_MOBILI) \ 5165) * 5165)
            TOTALE_ISEE_ERP = (((TOTALE_IMMOBILI + FIGURATIVO_MOBILI) \ 5165) * 5165) * 0.05
            TOTALE_PATRIMONIO_ISEE_ERP = (TOTALE_IMMOBILI + FIGURATIVO_MOBILI)


            F24FSA = TOTALE_ISEE_ERP


            ISP_ERP = TOTALE_ISEE_ERP

            F18FSA = 0.05


            L2 = F24FSA
            L3 = L1 + L2 'Format(CDbl(L1) + CDbl(L2), "##,##0.00")

            Dim PARAMETRO As Double

            Select Case TOT_COMPONENTI
                Case 1
                    PARAMETRO = 1
                Case 2
                    PARAMETRO = 1.57
                Case 3
                    PARAMETRO = 2.04
                Case 4
                    PARAMETRO = 2.46
                Case 5
                    PARAMETRO = 2.85
                Case Else
                    PARAMETRO = 2.85 + ((TOT_COMPONENTI - 5) * 0.35)
            End Select

            L4 = PARAMETRO

            If ENTRAMBI_GENITORI = "0" And MINORI > 0 Then
                PARAMETRO = PARAMETRO + 0.2
                L5 = 0.2
            End If

            PARAMETRO = PARAMETRO + ((HPSICO + INV_100_CON + INV_100_NO + INV_66_99) * 0.5)
            L6 = (HPSICO + INV_100_CON + INV_100_NO + INV_66_99) * 0.5 'Format((HPSICO + INV_100_CON + INV_100_NO + INV_66_99) * 0.5, "##,##0.00")

            If LAVORO_IMPRESA = "1" And MINORI > 0 Then
                PARAMETRO = PARAMETRO + 0.2
                L7 = 0.2
            End If

            L8 = L7 + L6 + L5 + L4 'Format(CDbl(L7) + CDbl(L6) + CDbl(L5) + CDbl(L4), "##,##0.00")

            VSE = PARAMETRO
            LIMITE_PATRIMONIO = 10330 + (5165 * VSE)
            ISE_ERP = ISR_ERP + ISP_ERP

            If CDbl(CType(Dom_Contratto1.FindControl("txtAffitto"), TextBox).Text) > 7000 Then
                If ISE_ERP <= 7000 Then
                    ISE_ERP = 7000
                End If
            Else
                If ISE_ERP <= CDbl(CType(Dom_Contratto1.FindControl("txtAffitto"), TextBox).Text) Then
                    ISE_ERP = CDbl(CType(Dom_Contratto1.FindControl("txtAffitto"), TextBox).Text)
                End If
            End If


            ISEE_ERP = ISE_ERP / VSE





            If ISEE_ERP >= 12911.43 Then
                ISEE_ERP = 0
                ESCLUSIONE = "LIMITE ISEE SUPERATO<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "1"
            End If

            If F16FSA > LIMITE_PATRIMONIO Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "LIMITE PATRIMONIALE SUPERATO<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "2"
            End If

            If CType(Dom_Abitative_1_1.FindControl("cmbCat"), DropDownList).SelectedItem.Value = "A1" Or CType(Dom_Abitative_1_1.FindControl("cmbCat"), DropDownList).SelectedItem.Value = "A8" Or CType(Dom_Abitative_1_1.FindControl("cmbCat"), DropDownList).SelectedItem.Value = "A9" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "UNITA' IMMOBILIARE DI CAT. " & CType(Dom_Abitative_1_1.FindControl("cmbCat"), DropDownList).SelectedItem.Value & "<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "5"
            End If

            If CType(Dom_Abitative_1_1.FindControl("txtSuperficie"), TextBox).Text > 110 Then
                If TOT_COMPONENTI > 4 Then
                    If CDbl(CType(Dom_Abitative_1_1.FindControl("txtSuperficie"), TextBox).Text) > 110 * (1 + (0.10000000000000001 * (TOT_COMPONENTI - 4))) Then
                        ISEE_ERP = 0
                        ESCLUSIONE = ESCLUSIONE & "UNITA' IMMOBILIARE CON SUP. UTILE NETTA SUPERIORE A " & 110 * (1 + (0.10000000000000001 * (TOT_COMPONENTI - 4))) & " mq<BR>"
                        IDONEO_PRESUNTO = IDONEO_PRESUNTO & "4"
                    End If
                Else
                    ISEE_ERP = 0
                    ESCLUSIONE = ESCLUSIONE & "UNITA' IMMOBILIARE CON SUP. UTILE NETTA SUPERIORE A 110 mq<BR>"
                    IDONEO_PRESUNTO = IDONEO_PRESUNTO & "4"
                End If
            End If

            llISEE.Text = par.Tronca(ISEE_ERP)

            If CType(Dom_Contratto1.FindControl("cmbStatoC"), DropDownList).SelectedItem.Value <> "REG" Then
                Response.Write("<script>alert('Attenzione, il contratto non è registrato. In fase di erogazione del contributo deve dimostrare di aver inoltrato richiesta di registrazione e aver versato la relativa imposta.');</script>")
            End If


            Dim Catastali As String = "SI"
            If (CType(Dom_Abitative_1_1.FindControl("txtFoglio"), TextBox).Text = "" Or CType(Dom_Abitative_1_1.FindControl("txtFoglio"), TextBox).Text = "0") Or (CType(Dom_Abitative_1_1.FindControl("txtParticella"), TextBox).Text = "" Or CType(Dom_Abitative_1_1.FindControl("txtParticella"), TextBox).Text = "0") Or (CType(Dom_Abitative_1_1.FindControl("txtSub"), TextBox).Text = "" Or CType(Dom_Abitative_1_1.FindControl("txtSub"), TextBox).Text = "0") Then
                'Response.Write("<SCRIPT>alert('Attenzione...Non sono stati inseriti i dati catastali dell/unità (Foglio,Mappale,Sub), non sarà possibile verificare se è gia stato chiesto un contributo per questa unità!')</SCRIPT>")
                Catastali = "NO"
            End If

            If Catastali = "SI" Then
                par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_fsa WHERE ID_BANDO=" & lIndice_Bando & " AND ID<>" & lIdDomanda & " and FOGLIO='" & par.PulisciStrSql(CType(Dom_Abitative_1_1.FindControl("txtFoglio"), TextBox).Text) & "' and particella='" & par.PulisciStrSql(CType(Dom_Abitative_1_1.FindControl("txtParticella"), TextBox).Text) & "' and subalterno='" & par.PulisciStrSql(CType(Dom_Abitative_1_1.FindControl("txtSub"), TextBox).Text) & "'"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    ISEE_ERP = 0
                    ESCLUSIONE = ESCLUSIONE & "CONTRIBUTO GIA RICHIESTO PER QUESTA UNITA ABITATIVA<BR>"
                    IDONEO_PRESUNTO = IDONEO_PRESUNTO & "8"
                End If
                myReader1.Close()
            End If

            If CType(Dom_Requisiti1.FindControl("ChR1"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "NON E' RESIDENTE NELL’ALLOGGIO OGGETTO DEL CONTRATTO<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "9"

            End If

            If CType(Dom_Requisiti1.FindControl("ChR2"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "ASSEGNATARIO ALLOGGIO ERP O POR<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "A"

            End If

            If CType(Dom_Requisiti1.FindControl("ChR3"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "MANCANZA DI CITTADINANZA O SOGGIORNO<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "B"

            End If

            If CType(Dom_Requisiti1.FindControl("ChR4"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "PROCEDURA ESECUTIVA DI SFRATTO<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "C"

            End If

            If CType(Dom_Requisiti1.FindControl("ChR5"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "PROPRIETA' DI ALLOGGIO ADEGUATO<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "D"

            End If

            If CType(Dom_Requisiti1.FindControl("ChR6"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "ASSEGNAZIONE IN GODIMENTO DI U.I. DA PARTE DI COOP EDILIZIE<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "E"

            End If

            If CType(Dom_Requisiti1.FindControl("ChR7"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "ASSEGNAZIONE DI U.I REALIZZATE CON CONTR. PUBBLICI<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "F"

            End If

            If CType(Dom_Requisiti1.FindControl("ChR8"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "MORTE<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "G"

            End If

            If CType(Dom_Requisiti1.FindControl("ChR9"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "IRREPERIBILITA'<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "H"

            End If

            If CType(Dom_Requisiti1.FindControl("ChR10"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "MANCATA PRESENTAZIONE DOPO DIFFIDA<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "I"

            End If

            If CType(Dom_Requisiti1.FindControl("ChR11"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "RINUNCIA<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "L"

            End If

            If CType(Dom_Requisiti1.FindControl("ChR12"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "PERMESSO DI SOGGIORNO INFERIORE AL LIMITE<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "M"

            End If

            If CType(Dom_Requisiti1.FindControl("ChR13"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "DICHIARAZIONE NON VERITIERA O DISCORDANTE<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "N"
            End If

            If CType(Dom_Requisiti1.FindControl("ChR16"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "RESIDENZA IN LOMBARDIA INFERIORE A 5 ANNI<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "Q"
            End If

            If CType(Dom_Requisiti1.FindControl("ChR17"), CheckBox).Checked = False Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "RESIDENZA SUL TERRITORIO NAZIONALE INFERIORE A 10 ANNI<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "R"
            End If




            L10 = 0
            L11 = 0
            L12 = 0
            L13 = 0
            L14 = 0

            If CDbl(CType(Dom_Contratto1.FindControl("txtAffitto"), TextBox).Text) > 7000 Then
                L11 = 7000
            Else
                L11 = CDbl(CType(Dom_Contratto1.FindControl("txtAffitto"), TextBox).Text)
            End If

            L9 = ISEE_ERP



            Dim Imax As Integer
            Dim contributoAFF As Double = 0
            Dim perc_abbattimento As Double = 0

            CANONE_INTEGRATO = 0

            If ESCLUSIONE = "" Then
                Select Case ISEE_ERP
                    Case Is <= 3100
                        Imax = 10
                        perc_abbattimento = 49.799999999999997

                    Case Is <= 3615.1999999999998
                        Imax = 11
                        perc_abbattimento = 50.799999999999997

                    Case Is <= 4131.6599999999999
                        Imax = 12
                        perc_abbattimento = 51.799999999999997

                    Case Is <= 4648.1099999999997
                        Imax = 13
                        perc_abbattimento = 52.799999999999997

                    Case Is <= 5164.5699999999997
                        Imax = 14
                        perc_abbattimento = 53.799999999999997

                    Case Is <= 5681.0299999999997
                        Imax = 15
                        perc_abbattimento = 54.799999999999997

                    Case Is <= 6197.4799999999996
                        Imax = 16
                        perc_abbattimento = 55.799999999999997

                    Case Is <= 6713.9399999999996
                        Imax = 17
                        perc_abbattimento = 56.799999999999997

                    Case Is <= 7230.3999999999996
                        Imax = 18
                        perc_abbattimento = 57.700000000000003

                    Case Is <= 7746.8500000000004
                        Imax = 19
                        perc_abbattimento = 58.700000000000003

                    Case Is <= 8263.3099999999995
                        Imax = 20
                        perc_abbattimento = 59.700000000000003

                    Case Is <= 8779.7700000000004
                        Imax = 21
                        perc_abbattimento = 60.600000000000001

                    Case Is <= 9296.2199999999993
                        Imax = 22
                        perc_abbattimento = 61.399999999999999

                    Case Is <= 9812.6800000000003
                        Imax = 23
                        perc_abbattimento = 62.399999999999999

                    Case Is <= 10329.139999999999
                        Imax = 24
                        perc_abbattimento = 63.399999999999999

                    Case Is <= 10845.59
                        Imax = 25
                        perc_abbattimento = 64.299999999999997

                    Case Is <= 11362.049999999999
                        Imax = 26
                        perc_abbattimento = 65.299999999999997

                    Case Is <= 11878.51
                        Imax = 27
                        perc_abbattimento = 66.200000000000003

                    Case Is <= 12911.42
                        Imax = 28
                        perc_abbattimento = 67.099999999999994

                End Select
                L10 = Format(Imax, "0.00")




                L12 = CDbl(CType(Dom_Contratto1.FindControl("txtSpese"), TextBox).Text) + CDbl(CType(Dom_Contratto1.FindControl("txtRiscaldamento"), TextBox).Text) 'Format(CDbl(CType(Dom_Contratto1.FindControl("txtSpese"), TextBox).Text) + CDbl(CType(Dom_Contratto1.FindControl("txtRiscaldamento"), TextBox).Text), "##,##0.00")
                If L12 > 516 Then
                    L12 = 516
                End If

                L13 = L11 '+ L12 

                L14 = (Imax * CDbl(L3)) / 100

                contributoAFF = ((L13 - L14) / 12) * Val(CType(Dom_Contratto1.FindControl("txtMesi"), TextBox).Text)




                'If ISEE_ERP < 3100 Or ((ISE_ERP - L13) / VSE) < 2066 Then
                '    maxContributo = 999999999
                'Else
                '    If TOT_COMPONENTI <= 2 Then
                '        maxContributo = 2300
                '    End If
                '    If TOT_COMPONENTI >= 3 Then
                '        maxContributo = 2300 + ((VSE - 1.5700000000000001) * 460)
                '    End If
                'End If

                If TOT_COMPONENTI <= 2 Then
                    maxContributo = 2300
                End If
                If TOT_COMPONENTI >= 3 Then
                    maxContributo = 2300 + ((VSE - 1.5700000000000001) * 460)
                End If


                If contributoAFF > maxContributo Then
                    contributoAFF = maxContributo
                End If

                contributoAFF = contributoAFF - ((perc_abbattimento * contributoAFF) / 100)
                contributoAFF = Format(contributoAFF, "0.00")

                If contributoAFF < 100 Then
                    ISEE_ERP = 0
                    ESCLUSIONE = ESCLUSIONE & "CONTRIBUTO INFERIORE A 100,00 Euro<BR>"
                    IDONEO_PRESUNTO = IDONEO_PRESUNTO & "6"
                End If


                If ISEE_ERP < 3100 Or ((ISE_ERP - L13) / VSE) < 2066 Then
                    quotacomunale = Format((20 * contributoAFF) / 100, "0.00")
                    quotaregionale = Format(contributoAFF - quotacomunale, "0.00")
                    LBLDIFFICOLTA.Visible = True
                    IDONEO_PRESUNTO = IDONEO_PRESUNTO & "3"
                Else
                    quotacomunale = Format((10 * contributoAFF) / 100, "0.00")
                    quotaregionale = Format(contributoAFF - quotacomunale, "0.00")
                    LBLDIFFICOLTA.Visible = False
                    IDONEO_PRESUNTO = "0"
                End If

                CANONE_INTEGRATO = L13

            End If


            sStringaSql = sStringaSql & "<head>"
            sStringaSql = sStringaSql & "    <title>Stampa Domanda FSA</title>"
            sStringaSql = sStringaSql & "</head>"
            sStringaSql = sStringaSql & "<body>"
            sStringaSql = sStringaSql & "    <table width='90%'>"
            sStringaSql = sStringaSql & "        <tr>"
            sStringaSql = sStringaSql & "            <td style='text-align: center'>"
            sStringaSql = sStringaSql & "                <span style='font-family: Arial; font-size: 10pt;'><strong>COMUNE DI MILANO</strong></span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr>"
            sStringaSql = sStringaSql & "            <td>"
            sStringaSql = sStringaSql & "                &nbsp;</td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr>"
            sStringaSql = sStringaSql & "            <td>"
            sStringaSql = sStringaSql & "                <span style='font-family: Arial'><span style='font-size: 9pt'>Comune di Milano, <em>" & Format(Now, "dd/MM/yyyy") & "</em></span></span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr>"
            sStringaSql = sStringaSql & "            <td style='width: 3px; height: 19px'>"
            sStringaSql = sStringaSql & "                &nbsp;</td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr>"
            sStringaSql = sStringaSql & "            <td style='text-align: center'>"
            sStringaSql = sStringaSql & "                <strong><span style='font-family: Arial; font-size: 10pt;'>SPORTELLO AFFITTO " & AnnoBando & " - DOMANDA DI CONTRIBUTO</span></strong></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr>"
            sStringaSql = sStringaSql & "            <td style='text-align: center'>"
            sStringaSql = sStringaSql & "                <strong><span style='font-size: 8pt; font-family: Arial'>(art.11, Legge 9 dicembre"
            sStringaSql = sStringaSql & "                    1998, n. 431)</span></strong></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='text-align: center'>"
            sStringaSql = sStringaSql & "                &nbsp;</td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-family: Arial'><span style='font-size: 9pt'>Io sottoscritto/a <em>" & CType(Dom_Richiedente1.FindControl("txtCognome"), TextBox).Text & " " & CType(Dom_Richiedente1.FindControl("txtnome"), TextBox).Text & "</em>&nbsp;<br />"
            sStringaSql = sStringaSql & "                    sesso <em>" & par.RicavaSesso(CType(Dom_Richiedente1.FindControl("txtCF"), TextBox).Text) & "</em>, codice fiscale <em>" & CType(Dom_Richiedente1.FindControl("txtCF"), TextBox).Text & "</em>, nato/a il <em>" & CType(Dom_Richiedente1.FindControl("txtDataNascita"), TextBox).Text & "</em>"
            sStringaSql = sStringaSql & "                    <br />"
            If CType(Dom_Richiedente1.FindControl("cmbComuneNas"), DropDownList).Visible = True Then
                sStringaSql = sStringaSql & "                    nel comune di <em>" & CType(Dom_Richiedente1.FindControl("cmbComuneNas"), DropDownList).SelectedItem.Text & " (" & CType(Dom_Richiedente1.FindControl("cmbPrNas"), DropDownList).SelectedItem.Text & ")</em>&nbsp;<br />"
            Else
                sStringaSql = sStringaSql & "                    nello stato estero di <em>" & CType(Dom_Richiedente1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text & "</em>&nbsp;<br />"
            End If

            sStringaSql = sStringaSql & "                    e residente nel Comune di <em>" & CType(Dom_Richiedente1.FindControl("cmbComuneRes"), DropDownList).SelectedItem.Text & "</em>"
            sStringaSql = sStringaSql & "                    <br />"
            sStringaSql = sStringaSql & "                    indirizzo: <em>" & CType(Dom_Richiedente1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text & " " & CType(Dom_Richiedente1.FindControl("txtIndRes"), TextBox).Text & "</em> , n. civico: <em>" & CType(Dom_Richiedente1.FindControl("txtCivicoRes"), TextBox).Text & "</em>, CAP: <em>" & CType(Dom_Richiedente1.FindControl("txtCapRes"), TextBox).Text & "</em>&nbsp;<br />"
            sStringaSql = sStringaSql & "                    n. telefono: <em>" & CType(Dom_Richiedente1.FindControl("txtTelRes"), TextBox).Text & "</em></span></span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='text-align: left'>"
            sStringaSql = sStringaSql & "                &nbsp;</td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='text-align: center'>"
            sStringaSql = sStringaSql & "                <strong><span style='font-size: 10pt; font-family: Arial'>CHIEDE</span></strong></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-size: 9pt; font-family: Arial'>un contributo al pagamento del canone"
            sStringaSql = sStringaSql & "                    di locazione relativo all'anno " & AnnoBando & ", previsto dalla legge regionale n. 2 del 14-1-2000"
            sStringaSql = sStringaSql & "                    in attuazione dell'art. 11 della Legge n. 431/1998.<br />"
            sStringaSql = sStringaSql & "                    <br />"
            sStringaSql = sStringaSql & "                    A tal fine, ai sensi del DPR 28 dicembre 2000, n. 445, sotto la propria responsabilità"
            sStringaSql = sStringaSql & "                    e nella consapevolezza delle conseguenze penali in caso di dichiarazione mendace,</span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: center'>"
            sStringaSql = sStringaSql & "                <strong><span style='font-size: 10pt; font-family: Arial'>DICHIARA</span></strong></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: center'>"
            sStringaSql = sStringaSql & "                <table width='100%'>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px' valign='top'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>-</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: left'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>di essere alla data di presentazione"
            sStringaSql = sStringaSql & "                                della domanda titolare di contratto di locazione con decorrenza in data " & CType(Dom_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text & " e"
            sStringaSql = sStringaSql & "                                con scadenza in data " & CType(Dom_Contratto1.FindControl("txtDataScadenza"), TextBox).Text & ";</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px' valign='top'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>-</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: left'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>DI AVER RICEVUTO DISDETTA PER IL GIORNO"
            sStringaSql = sStringaSql & "                                " & CType(Dom_Contratto1.FindControl("txtDataDisdetta"), TextBox).Text & ";</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px' valign='top'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>-</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: left'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>che il contratto di locazione non"
            sStringaSql = sStringaSql & "                                è stato risolto a seguito di procedura esecutiva di sfratto;</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px' valign='top'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>-</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: left'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>che il contratto è " & CType(Dom_Contratto1.FindControl("cmbStatoC"), DropDownList).SelectedItem.Text & ";</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px' valign='top'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>-</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: left'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>che il contratto è stato stipulato"
            sStringaSql = sStringaSql & "                                in data " & CType(Dom_Contratto1.FindControl("txtDataStipula"), TextBox).Text & " e registrato in data " & CType(Dom_Contratto1.FindControl("txtDataReg"), TextBox).Text & ", con estremi di registrazione " & CType(Dom_Contratto1.FindControl("txtEstremi"), TextBox).Text & " e del"
            sStringaSql = sStringaSql & "                                versamento dell'imposta ;</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px' valign='top'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>-</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: left'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>che tale contratto di locazione è"
            sStringaSql = sStringaSql & "                                relativo ad unità immobiliare ad uso residenziale, sita in Lombardia e occupata"
            sStringaSql = sStringaSql & "                                alla data di apertura del presente bando a titolo di residenza esclusiva o principale"
            sStringaSql = sStringaSql & "                                da parte del richiedente, del suo nucleo familiare anagrafico e dei soggetti a loro"
            sStringaSql = sStringaSql & "                                carico ai fini IRPEF;</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px' valign='top'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>-</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: left'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>di essere residente in questo Comune"
            sStringaSql = sStringaSql & "                                alla data di presentazione della domanda;</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px' valign='top'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>-</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: left'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>di aver per l'anno " & AnnoBando & " un periodo"
            sStringaSql = sStringaSql & "                                di vigenza contrattuale di n. " & CType(Dom_Contratto1.FindControl("txtMesi"), TextBox).Text & " mesi per contratti che hanno i requisiti previsti"
            sStringaSql = sStringaSql & "                                dal bando Sportello Affitto " & AnnoBando & ";</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px' valign='top'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>-</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: left'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>che nessun componente del nucleo familiare"
            sStringaSql = sStringaSql & "                                indicato nella dichiarazione sostitutiva allegata alla data di presentazione della"
            sStringaSql = sStringaSql & "                                domanda è titolare del diritto di proprietà o altri diritti reali di godimento su"
            sStringaSql = sStringaSql & "                                alloggio adeguato (art. 2, comma 2 L.r. 91-92/83 e succ. mod.) alle esigenze del"
            sStringaSql = sStringaSql & "                                nucleo familiare nell'ambito regionale;</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px' valign='top'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>-</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: left'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>che nessun componente del nucleo familiare"
            sStringaSql = sStringaSql & "                                indicato nella dichiarazione sostitutiva allegata alla data di presentazione della"
            sStringaSql = sStringaSql & "                                domanda ha ottenuto l'assegnazione in proprietà immediata o futura di alloggio realizzato"
            sStringaSql = sStringaSql & "                                con contributi pubblici o ha usufruito di finanziamenti agevolati in qualunque forma"
            sStringaSql = sStringaSql & "                                concessi dallo Stato e da enti pubblici;</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px' valign='top'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>-</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: left'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>che nessun componente del nucleo familiare"
            sStringaSql = sStringaSql & "                                indicato nella dichiarazione sostitutiva allegata ha già presentato altra istanza"
            sStringaSql = sStringaSql & "                                della domanda di contributo.</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                </table>"
            sStringaSql = sStringaSql & "                <p style='page-break-after: always' class='mini'><font face='Arial' size='2'>&nbsp;</font>&nbsp;</p>"
            sStringaSql = sStringaSql & "            </td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'><strong>Informazioni relative all'alloggio</strong></span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-size: 9pt; font-family: Arial'>Informazioni relative all'alloggio"
            sStringaSql = sStringaSql & "                    condotto in locazione sito in Lombardia non incluso nelle categorie catastali A/1,"
            sStringaSql = sStringaSql & "                    A/8, A/9 e con superficie utile interna non superiore a 110 mq (maggiorata del 10%"
            sStringaSql = sStringaSql & "                    per ogni componente il nucleo familiare dopo il quarto) ed occupato dalla famiglia"
            sStringaSql = sStringaSql & "                    del richiedente al momento della presentazione della domanda.</span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                &nbsp;</td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'>Superficie " & CType(Dom_Abitative_1_1.FindControl("txtSuperficie"), TextBox).Text & " mq, foglio " & CType(Dom_Abitative_1_1.FindControl("txtFoglio"), TextBox).Text & ", particella"
            sStringaSql = sStringaSql & "                    " & CType(Dom_Abitative_1_1.FindControl("txtParticella"), TextBox).Text & ", subalterno " & CType(Dom_Abitative_1_1.FindControl("txtSub"), TextBox).Text & ", anno costruzione " & CType(Dom_Abitative_1_1.FindControl("txtAnno"), TextBox).Text & ", categoria catastale " & CType(Dom_Abitative_1_1.FindControl("cmbCat"), DropDownList).SelectedItem.Text & ".</span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <table width='100%'>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>L'alloggio è in condizioni di degrado"
            sStringaSql = sStringaSql & "                                tali da pregiudicare l'incolumità degli occupanti</span></td>"
            sStringaSql = sStringaSql & "                        <td valign='top'>"
            If CType(Dom_Abitative_1_1.FindControl("chDegrado"), CheckBox).Checked = True Then
                sStringaSql = sStringaSql & "                            <img src='block_SI_checked.gif' /><img src='/block_no.gif' /></td>"
            Else
                sStringaSql = sStringaSql & "                            <img src='block_SI.gif' /><img src='block_no_checked.gif' /></td>"
            End If
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>L'alloggio dispone di acqua potabile</span></td>"
            sStringaSql = sStringaSql & "                        <td valign='top'>"

            If CType(Dom_Abitative_1_1.FindControl("chPotabile"), CheckBox).Checked = True Then
                sStringaSql = sStringaSql & "                            <img src='block_SI_checked.gif' /><img src='block_no.gif' /></td>"
            Else
                sStringaSql = sStringaSql & "                            <img src='block_SI.gif' /><img src='block_no_checked.gif' /></td>"
            End If
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>L'alloggio dispone di servizio cucina</span></td>"
            sStringaSql = sStringaSql & "                        <td valign='top'>"

            If CType(Dom_Abitative_1_1.FindControl("chCucina"), CheckBox).Checked = True Then
                sStringaSql = sStringaSql & "                            <img src='block_SI_checked.gif' /><img src='block_no.gif' /></td>"
            Else
                sStringaSql = sStringaSql & "                            <img src='block_SI.gif' /><img src='block_no_checked.gif' /></td>"
            End If
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px; height: 14px;' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td style='height: 14px;'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>L'alloggio è improprio (soffitto,"
            sStringaSql = sStringaSql & "                                seminterrato, rustico, box)</span></td>"
            sStringaSql = sStringaSql & "                        <td style='height: 14px' valign='top'>"
            If CType(Dom_Abitative_1_1.FindControl("chImproprio"), CheckBox).Checked = True Then
                sStringaSql = sStringaSql & "                            <img src='block_SI_checked.gif' /><img src='block_no.gif' /></td>"
            Else
                sStringaSql = sStringaSql & "                            <img src='block_SI.gif' /><img src='block_no_checked.gif' /></td>"
            End If
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>L'alloggio dispone di servizi igienici</span></td>"
            sStringaSql = sStringaSql & "                        <td valign='top'>"
            If CType(Dom_Abitative_1_1.FindControl("chServizi"), CheckBox).Checked = True Then
                sStringaSql = sStringaSql & "                            <img src='block_SI_checked.gif' /><img src='block_no.gif' /></td>"
            Else
                sStringaSql = sStringaSql & "                            <img src='block_SI.gif' /><img src='block_no_checked.gif' /></td>"
            End If
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>L'alloggio dispone di adeguati dispositivi"
            sStringaSql = sStringaSql & "                                per il riscaldamento</span></td>"
            sStringaSql = sStringaSql & "                        <td valign='top'>"
            If CType(Dom_Abitative_1_1.FindControl("chRiscaldamento"), CheckBox).Checked = True Then
                sStringaSql = sStringaSql & "                            <img src='block_SI_checked.gif' /><img src='block_no.gif' /></td>"
            Else
                sStringaSql = sStringaSql & "                            <img src='block_SI.gif' /><img src='block_no_checked.gif' /></td>"
            End If
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>Numero locali (esclusi locale cucina,"
            sStringaSql = sStringaSql & "                                servizi, soffitte, cantine e ripostiglio; soggiorno con angolo cottura va indicato"
            sStringaSql = sStringaSql & "                                come un solo locale)</span></td>"
            sStringaSql = sStringaSql & "                        <td>" & CType(Dom_Abitative_1_1.FindControl("txtNumLocali"), TextBox).Text
            sStringaSql = sStringaSql & "                        </td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>Presenza di box</span></td>"
            sStringaSql = sStringaSql & "                        <td valign='top'>"
            If CType(Dom_Abitative_1_1.FindControl("chBox"), CheckBox).Checked Then
                sStringaSql = sStringaSql & "                            <img src='block_SI_checked.gif' /><img src='block_no.gif' /></td>"
            Else
                sStringaSql = sStringaSql & "                            <img src='block_SI.gif' /><img src='block_no_checked.gif' /></td>"
            End If
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>Presenza di posto macchina</span></td>"
            sStringaSql = sStringaSql & "                        <td valign='top'>"
            If CType(Dom_Abitative_1_1.FindControl("chAuto"), CheckBox).Checked Then

                sStringaSql = sStringaSql & "                            <img src='block_SI_checked.gif' /><img src='block_no.gif' /></td>"
            Else
                sStringaSql = sStringaSql & "                            <img src='block_SI.gif' /><img src='block_no_checked.gif' /></td>"

            End If

            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>Assegnazione in godimento di unità"
            sStringaSql = sStringaSql & "                                immobiliare da parte di cooperativa edilizia a proprietà indivisa</span></td>"
            sStringaSql = sStringaSql & "                        <td valign='top'>"
            If CType(Dom_Abitative_1_1.FindControl("chIndivisa"), CheckBox).Checked Then
                sStringaSql = sStringaSql & "                            <img src='block_SI_checked.gif' /><img src='block_no.gif' /></td>"
            Else
                sStringaSql = sStringaSql & "                            <img src='block_SI.gif' /><img src='block_no_checked.gif' /></td>"

            End If

            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>Sussistenza di uno dei requisiti B,"
            sStringaSql = sStringaSql & "                                C, D, E, F, G dell'articolo 3, comma 2, dell'allegato 1 alla d.G.r.n. 5075 del 10"
            sStringaSql = sStringaSql & "                                luglio 2007</span></td>"
            sStringaSql = sStringaSql & "                        <td valign='top'>"
            If CType(Dom_Abitative_1_1.FindControl("chReq"), CheckBox).Checked Then
                sStringaSql = sStringaSql & "                            <img src='block_SI_checked.gif' /><img src='block_no.gif' /></td>"
            Else
                sStringaSql = sStringaSql & "                            <img src='block_SI.gif' /><img src='block_no_checked.gif' /></td>"

            End If
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>tipo contratto: " & CType(Dom_Contratto1.FindControl("cmbTipoContratto"), DropDownList).SelectedItem.Text & " ;</span></td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                        </td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>figura del proprietario: " & CType(Dom_Contratto1.FindControl("cmbTipoFigura"), DropDownList).SelectedItem.Text & ";</span></td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                        </td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>affitto annuo come risultante dal"
            sStringaSql = sStringaSql & "                                contratto vigente Euro " & CType(Dom_Contratto1.FindControl("txtAffitto"), TextBox).Text & ";</span></td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                        </td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>spese complessive accessorie come"
            sStringaSql = sStringaSql & "                                risultanti dal contratto vigente: condominiali Euro " & CType(Dom_Contratto1.FindControl("txtSpese"), TextBox).Text & ", riscaldamento Euro " & CType(Dom_Contratto1.FindControl("txtRiscaldamento"), TextBox).Text & ".</span></td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                        </td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>numero complessivo di contratti idonei"
            sStringaSql = sStringaSql & "                                e registrati per l'anno " & AnnoBando & ": " & CType(Dom_Contratto1.FindControl("txtIdonei"), TextBox).Text & ";</span></td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                        </td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                </table>"
            sStringaSql = sStringaSql & "                &nbsp;</td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'><strong>Tipologia dei redditi</strong></span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <table width='100%'>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>numero di componenti con redditi da"
            sStringaSql = sStringaSql & "                                lavoro dipendente o assimilati: " & CType(Dom_Dichiara1.FindControl("txtDipendenti"), TextBox).Text & ";</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px' valign='top'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>numero di componenti con redditi da"
            sStringaSql = sStringaSql & "                                pensione: " & CType(Dom_Dichiara1.FindControl("txtPensione"), TextBox).Text & ";</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>numero totale di componenti con redditi"
            sStringaSql = sStringaSql & "                                dal lavoro autonomo: " & CType(Dom_Dichiara1.FindControl("txtAutonomo"), TextBox).Text & ";</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>numero totale di componenti con altri"
            sStringaSql = sStringaSql & "                                tipi di reddito: " & CType(Dom_Dichiara1.FindControl("txtSubordinato"), TextBox).Text & ";</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                </table>"
            sStringaSql = sStringaSql & "                &nbsp;</td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'><strong>Convivenza di più nuclei familiari</strong></span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <table width='100%'>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>numero totale di nuclei familiari"
            sStringaSql = sStringaSql & "                                che occupano l'alloggio in locazione alla data di presentazione della domanda: " & CType(Dom_Dichiara1.FindControl("txtCoabitanti"), TextBox).Text & ";</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 11px'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>numero totale di persone che occupano"
            sStringaSql = sStringaSql & "                                l'alloggio in locazione alla data di presentazione della domanda: " & CType(Dom_Dichiara1.FindControl("txtOccupanti"), TextBox).Text & ";</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                </table>"
            sStringaSql = sStringaSql & "                &nbsp;</td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'><strong>Particolari difficoltà economiche</strong></span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <table width='100%'>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 12px'>"
            sStringaSql = sStringaSql & "                            -</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 951px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>Il nucleo familiare si trova in particolari"
            sStringaSql = sStringaSql & "                                difficoltà economiche, come risulta dalle attestazioni allegate</span></td>"
            sStringaSql = sStringaSql & "                        <td valign='top'>"

            If CType(Dom_Dichiara1.FindControl("chDifficolta"), CheckBox).Checked = True Then
                sStringaSql = sStringaSql & "                            <img src='block_SI_checked.gif' /><img src='block_no.gif' /></td>"
            Else
                sStringaSql = sStringaSql & "                            <img src='block_SI.gif' /><img src='block_no_checked.gif' /></td>"
            End If

            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                </table>"
            sStringaSql = sStringaSql & "                <p style='page-break-after: always' class='mini'><font face='Arial' size='2'></font>&nbsp;</p>"
            sStringaSql = sStringaSql & "            </td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'><strong>Certificazioni</strong></span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"


            Dim AutoC As String = ""
            Dim Cert As String = ""

            If CType(Note1.FindControl("cmbAnagrafica"), DropDownList).SelectedItem.Value = "C" Then
                Cert = "ANAGRAFICA, "
            Else
                AutoC = "ANAGRAFICA, "
            End If

            If CType(Note1.FindControl("cmbDichiara"), DropDownList).SelectedItem.Value = "C" Then
                Cert = Cert & "DICHIARA, "
            Else
                AutoC = AutoC & "DICHIARA, "
            End If

            If CType(Note1.FindControl("cmbLocazione"), DropDownList).SelectedItem.Value = "C" Then
                Cert = Cert & "LOCAZIONE, "
            Else
                AutoC = AutoC & "LOCAZIONE, "
            End If

            If CType(Note1.FindControl("cmbNucleo"), DropDownList).SelectedItem.Value = "C" Then
                Cert = Cert & "NUCLEO, "
            Else
                AutoC = AutoC & "NUCLEO, "
            End If

            'If CType(Note1.FindControl("cmbSottoscrittore"), DropDownList).SelectedItem.Value = "C" Then
            '    Cert = Cert & "SOTTOSCRITTORE, "
            'Else
            '    AutoC = AutoC & "SOTTOSCRITTORE, "
            'End If



            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'>L'ente certifica le seguenti schede:"
            sStringaSql = sStringaSql & "                    " & Cert & "</span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <table width='100%'>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 120px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>Data " & Format(Now, "dd/MM/yyyy") & "</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: center'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>IL DICHIARANTE</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 120px'>"
            sStringaSql = sStringaSql & "                        </td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: center'>"
            sStringaSql = sStringaSql & "                            ___________________________</td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                </table>"
            sStringaSql = sStringaSql & "                <br />"
            sStringaSql = sStringaSql & "            </td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'><strong>Modalità di pagamento</strong></span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'>Il sottoscritto chiede che l'eventuale"
            sStringaSql = sStringaSql & "                    contributo sia corrisposto mediante:<br />"
            Dim Contributo As String = ""

            Select Case CType(Dom_Dichiara1.FindControl("cmbContributo"), DropDownList).SelectedItem.Value
                Case "RID", "ASE"
                    Contributo = CType(Dom_Dichiara1.FindControl("cmbContributo"), DropDownList).SelectedItem.Text & " Intestata a " & CType(Dom_Dichiara1.FindControl("txtIntestato"), TextBox).Text
                Case "ACE"
                    Contributo = CType(Dom_Dichiara1.FindControl("cmbContributo"), DropDownList).SelectedItem.Text & " Intestato a " & CType(Dom_Dichiara1.FindControl("txtIntestato"), TextBox).Text & "<br>Banca: " & CType(Dom_Dichiara1.FindControl("txtBanca"), TextBox).Text & " - Ubicazione: " & CType(Dom_Dichiara1.FindControl("txtUbicazione"), TextBox).Text & "<br>IBAN: " & CType(Dom_Dichiara1.FindControl("txtiban"), TextBox).Text

            End Select

            sStringaSql = sStringaSql & "                    " & Contributo & "<br />"
            sStringaSql = sStringaSql & "                </span>"
            sStringaSql = sStringaSql & "            </td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'>Il sottoscritto dichiara infine di"
            sStringaSql = sStringaSql & "                    essere a conoscenza delle norme che istituiscono lo Sportello Affitto " & AnnoBando & " e di"
            sStringaSql = sStringaSql & "                    possedere tutti i requisiti di partecipazione in esso indicati nonché la propria"
            sStringaSql = sStringaSql & "                    disponibilità a fornire idonea documentazione atta a dimostrare la completezza e"
            sStringaSql = sStringaSql & "                    la veridicità dei dati dichiarati.<br />"
            sStringaSql = sStringaSql & "                </span>"
            sStringaSql = sStringaSql & "            </td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                    <table width='100%'>"
            sStringaSql = sStringaSql & "                        <tr>"
            sStringaSql = sStringaSql & "                            <td style='width: 120px'>"
            sStringaSql = sStringaSql & "                                <span style='font-size: 10pt; font-family: Arial'>Data " & Format(Now, "dd/MM/yyyy") & "</span></td>"
            sStringaSql = sStringaSql & "                            <td style='text-align: center'>"
            sStringaSql = sStringaSql & "                                <span style='font-size: 10pt; font-family: Arial'>IL DICHIARANTE</span></td>"
            sStringaSql = sStringaSql & "                        </tr>"
            sStringaSql = sStringaSql & "                        <tr>"
            sStringaSql = sStringaSql & "                            <td style='width: 120px'>"
            sStringaSql = sStringaSql & "                            </td>"
            sStringaSql = sStringaSql & "                            <td style='text-align: center'>"
            sStringaSql = sStringaSql & "                                ___________________________</td>"
            sStringaSql = sStringaSql & "                        </tr>"
            sStringaSql = sStringaSql & "                    </table>"
            sStringaSql = sStringaSql & "                </span>"
            sStringaSql = sStringaSql & "                <br />"
            sStringaSql = sStringaSql & "            </td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'><strong>DATI ANAGRAFICI DEL DICHIARANTE</strong></span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-family: Arial'><span style='font-size: 9pt'>Cognome e Nome: <em>" & CType(Dom_Richiedente1.FindControl("txtCognome"), TextBox).Text & " " & CType(Dom_Richiedente1.FindControl("txtnome"), TextBox).Text & "</em>&nbsp;<br />"
            sStringaSql = sStringaSql & "                    sesso <em>" & par.RicavaSesso(CType(Dom_Richiedente1.FindControl("txtCF"), TextBox).Text) & "</em>, codice fiscale <em>" & CType(Dom_Richiedente1.FindControl("txtCF"), TextBox).Text & "</em>, nato/a il <em>" & CType(Dom_Richiedente1.FindControl("txtDataNascita"), TextBox).Text & "</em>"
            sStringaSql = sStringaSql & "                    <br />"
            If CType(Dom_Richiedente1.FindControl("cmbComuneNas"), DropDownList).Visible = True Then
                sStringaSql = sStringaSql & "                    nel comune di <em>" & CType(Dom_Richiedente1.FindControl("cmbComuneNas"), DropDownList).SelectedItem.Text & " (" & CType(Dom_Richiedente1.FindControl("cmbPrNas"), DropDownList).SelectedItem.Text & ")</em>&nbsp;<br />"
            Else
                sStringaSql = sStringaSql & "                    nello stato estero di <em>" & CType(Dom_Richiedente1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text & "</em>&nbsp;<br />"
            End If

            sStringaSql = sStringaSql & "                    e residente nel Comune di <em>" & CType(Dom_Richiedente1.FindControl("cmbComuneRes"), DropDownList).SelectedItem.Text & "</em>"
            sStringaSql = sStringaSql & "                    <br />"
            sStringaSql = sStringaSql & "                    indirizzo: <em>" & CType(Dom_Richiedente1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text & " " & CType(Dom_Richiedente1.FindControl("txtIndRes"), TextBox).Text & "</em> , n. civico: <em>" & CType(Dom_Richiedente1.FindControl("txtCivicoRes"), TextBox).Text & "</em>, CAP: <em>" & CType(Dom_Richiedente1.FindControl("txtCapRes"), TextBox).Text & "</em>&nbsp;<br />"
            sStringaSql = sStringaSql & "                    n. telefono: <em>" & CType(Dom_Richiedente1.FindControl("txtTelRes"), TextBox).Text & "</em></span></span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            'sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            'sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            'sStringaSql = sStringaSql & "                <span style='font-size: 9pt'><span style='font-family: Arial'>Io sottoscritto/a <em>"
            'sStringaSql = sStringaSql & "                    COGNOME E NOME</em>&nbsp;<br />"
            'sStringaSql = sStringaSql & "                    sesso <em>X</em>, codice fiscale <em>XXXXXXXXX</em>, nato/a il <em>XXX</em>"
            'sStringaSql = sStringaSql & "                    <br />"
            'sStringaSql = sStringaSql & "                    nel comune o stato estero di <em>XXXXXX</em>&nbsp;<br />"
            'sStringaSql = sStringaSql & "                    e residente nel Comune di <em>XXXX</em>"
            'sStringaSql = sStringaSql & "                    <br />"
            'sStringaSql = sStringaSql & "                    indirizzo: <em>XXXX</em> , n. civico: <em>XX</em>, CAP: <em>XXXX</em>&nbsp;<br />"
            'sStringaSql = sStringaSql & "                    n. telefono: <em>XXXX</em></span></span></td>"
            'sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                &nbsp;</td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "            <td style='height: 17px; text-align: left'>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'>( ) Annotazione estremi documento"
            sStringaSql = sStringaSql & "                    di identità __________________________<br />"
            sStringaSql = sStringaSql & "                    <br />"
            sStringaSql = sStringaSql & "                    Firma apposta dal dichiarante in presenza di _________________________<br />"
            sStringaSql = sStringaSql & "                    <br />"
            sStringaSql = sStringaSql & "                    ( ) Presentata copia del documento di identità __________________________<br />"
            sStringaSql = sStringaSql & "                    <br />"
            sStringaSql = sStringaSql & "                    <br />"
            sStringaSql = sStringaSql & "                    <br />"
            sStringaSql = sStringaSql & "                    <span style='font-size: 12pt; font-family: Times New Roman'>DOMANDA N°: <strong><span"
            sStringaSql = sStringaSql & "                        style='font-size: 10pt; font-family: Arial'>" & lblSPG.Text & "-" & lblPG.Text & "-" & Label7.Text & " / " & CODICEANAGRAFICO & "</span></strong><BR>DICHIARAZIONE"
            sStringaSql = sStringaSql & "                        ASSOCIATA N°: <span style='font-size: 10pt; font-family: Arial'><strong>" & lblPGDic.Text & "</strong></span></span></span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        "
            sStringaSql = sStringaSql & "        "
            sStringaSql = sStringaSql & "    </table>"
            sStringaSql = sStringaSql & "    <br />"
            sStringaSql = sStringaSql & ""
            sStringaSql = sStringaSql & "    <br />"
            sStringaSql = sStringaSql & "    <p style='page-break-after: always' class='mini'><font face='Arial' size='2'></font>&nbsp;</p>"
            sStringaSql = sStringaSql & "    <table width='90%'>"
            sStringaSql = sStringaSql & "        <tr>"
            sStringaSql = sStringaSql & "            <td>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'>Richiedente:" & CType(Dom_Richiedente1.FindControl("txtCognome"), TextBox).Text & " " & CType(Dom_Richiedente1.FindControl("txtnome"), TextBox).Text & "</span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr>"
            sStringaSql = sStringaSql & "            <td>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'>FONDO REGIONALE PER IL SOSTEGNO ALL'ACCESSO"
            sStringaSql = sStringaSql & "                    ALLE ABITAZIONI IN LOCAZIONE</span></td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr>"
            sStringaSql = sStringaSql & "            <td>"
            sStringaSql = sStringaSql & "                &nbsp;&nbsp;"
            sStringaSql = sStringaSql & "            </td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "        <tr>"
            sStringaSql = sStringaSql & "            <td>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'><strong>QUADRO E-fsa: SITUAZIONE REDDITUALE"
            sStringaSql = sStringaSql & "                    DEL NUCLEO FAMILIARE<br />"
            sStringaSql = sStringaSql & "                </strong></span>"
            sStringaSql = sStringaSql & "                <br />"
            sStringaSql = sStringaSql & "                <table border='1' cellspacing='1' width='100%'>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            E1</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>REDDITO COMLPESSIVI DEL NUCLEO FAMILIARE<br />"
            sStringaSql = sStringaSql & "                                <span style='font-size: 8pt'>(=D1+D2+...D10, colonna B e C)</span></span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>" & Format(E1, "##,##0.00") & "</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            E2</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-family: Arial'><span style='font-size: 10pt'>REDDITO FIGURATIVO DEL"
            sStringaSql = sStringaSql & "                                PATRIMONIO MOBILIARE<br />"
            sStringaSql = sStringaSql & "                            </span><span style='font-size: 8pt'>(4,41% di C1, pari al rendimento medio ponderato"
            sStringaSql = sStringaSql & "                                dei titoli decennali del Tesoro anno 2007)</span></span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>" & Format(E2, "##,##0.00") & "</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            E3fsa</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>DETRAZIONI AL REDDITO LORDO</span>"
            sStringaSql = sStringaSql & "                            <br />"
            sStringaSql = sStringaSql & "                            <span style='font-size: 8pt; font-family: Arial'>(IRPEF e spese mediche)</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>" & Format(E3FSA, "##,##0.00") & "</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            E4fsa</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-family: Arial'><span style='font-size: 10pt'>TOTALE DEL REDDITO DA"
            sStringaSql = sStringaSql & "                                CONSIDERARE AI FINI ISEEfsa"
            sStringaSql = sStringaSql & "                                <br />"
            sStringaSql = sStringaSql & "                            </span><span style='font-size: 8pt'>(=E1 + E2 - E3fsa)</span></span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>" & Format(E4FSA, "##,##0.00") & "</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                </table>"
            sStringaSql = sStringaSql & "                <br />"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt'><span style='font-family: Arial'><strong>QUADRO F-fsa:"
            sStringaSql = sStringaSql & "                    SITUAZIONE PATRIMONIALE DEL NUCLEO FAMILIARE<br />"
            sStringaSql = sStringaSql & "                </strong></span>"
            sStringaSql = sStringaSql & "                    <br />"
            sStringaSql = sStringaSql & "                </span>"
            sStringaSql = sStringaSql & "                <table border='1' cellspacing='1' width='100%' style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            F12</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>CONSISTENZA DEL PATRIMONIO MOBILIARE<br />"
            sStringaSql = sStringaSql & "                                <span style='font-size: 8pt'>(=C1)</span></span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid; font-size: 12pt; font-family: Times New Roman;'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>" & Format(F12, "##,##0.00") & "</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            F13</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span><span style='font-family: Arial'><span style='font-size: 10pt'>CONSISTENZA DEL"
            sStringaSql = sStringaSql & "                                PATRIMONIO IMMOBILIARE (VALORE AI FINI ICI)<br />"
            sStringaSql = sStringaSql & "                            </span><span style='font-size: 8pt'>(=C12 + C13 +... C21, colonna D)</span></span></span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid; font-size: 12pt; font-family: Times New Roman;'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>" & Format(F13, "##,##0.00") & "</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            F16fsa</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>PATRIMONIO COMPLESSIVO</span>"
            sStringaSql = sStringaSql & "                            <br />"
            sStringaSql = sStringaSql & "                            <span style='font-size: 8pt; font-family: Arial'>(F12 + F13) multipli di 5165</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid; font-size: 12pt; font-family: Times New Roman;'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>" & Format(F16FSA, "##,##0.00") & "</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr style='font-size: 12pt; font-family: Times New Roman'>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            F18fsa</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-family: Arial'><span style='font-size: 10pt'>COEFFICIENTE PER IL PATRIMONIO"
            sStringaSql = sStringaSql & "                            </span></span>"
            sStringaSql = sStringaSql & "                        </td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>" & Format(F18FSA, "##,##0.00") & "</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            F24fsa</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-family: Arial'><span style='font-size: 10pt'>TOTALE COMPLESSIVO DEL"
            sStringaSql = sStringaSql & "                                PATRIMONIO DA CONSIDERARE AI FINI ISEEfsa<br />"
            sStringaSql = sStringaSql & "                            </span><span style='font-size: 8pt'>(=F16Fsa X F18fsa)</span></span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>" & Format(F24FSA, "##,##0.00") & "</span></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                </table>"
            sStringaSql = sStringaSql & "            </td>"
            sStringaSql = sStringaSql & "        </tr>"
            sStringaSql = sStringaSql & "    </table>"
            sStringaSql = sStringaSql & "    <p style='page-break-after: always' class='mini'><font face='Arial' size='2'></font>&nbsp;</p>"
            sStringaSql = sStringaSql & "    <table width='90%'>"
            sStringaSql = sStringaSql & "        <tr>"
            sStringaSql = sStringaSql & "            <td>"
            sStringaSql = sStringaSql & "                <span style='font-size: 10pt; font-family: Arial'><strong>QUADRO L: DETERMINAZIONE DELL'ISEE-fsa<br />"
            sStringaSql = sStringaSql & "                </strong></span>"
            sStringaSql = sStringaSql & "                <br />"
            sStringaSql = sStringaSql & "                <table border='1' cellspacing='1' width='100%'>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            L1</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>REDDITO DA CONSIDERARE AI FINI ISEE-fsa<br />"
            sStringaSql = sStringaSql & "                                <span style='font-size: 8pt'>(=E4fsa)</span></span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & Format(L1, "##,##0.00") & "</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            L2</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-family: Arial'><span style='font-size: 10pt'>PATRIMONIO DA CONSIDERARE"
            sStringaSql = sStringaSql & "                                AI FINI ISEE-fsa<br />"
            sStringaSql = sStringaSql & "                            </span><span style='font-size: 8pt'>(=F24fsa)</span></span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & Format(L2, "##,##0.00") & "</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            L3</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>TOTALE DA CONSIDERARE AI FINI ISEE-fsa</span>"
            sStringaSql = sStringaSql & "                            <br />"
            sStringaSql = sStringaSql & "                            <span style='font-size: 8pt; font-family: Arial'>(=L1 + L2)</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & Format(L3, "##,##0.00") & "</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'> "
            sStringaSql = sStringaSql & "                        </td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            Individuazione dei parametri correttivi:</td>"
            sStringaSql = sStringaSql & "                        <td style='border-top-width: 1px; border-left-width: 1px; border-left-color: black;"
            sStringaSql = sStringaSql & "                            border-bottom-width: 1px; border-bottom-color: black; border-top-color: black;"
            sStringaSql = sStringaSql & "                            border-right-width: 1px; border-right-color: black'> "
            sStringaSql = sStringaSql & "                        </td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            L4</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-family: Arial'><span style='font-size: 10pt'>Parametro corrispondente"
            sStringaSql = sStringaSql & "                                alla composizione del nucleo familiare<br />"
            sStringaSql = sStringaSql & "                            </span><span style='font-size: 8pt'>= 1 se il nucleo è formato da un solo componente"
            sStringaSql = sStringaSql & "                                <br />"
            sStringaSql = sStringaSql & "                                = 1,57 se il nucleo è formato da due componenti"
            sStringaSql = sStringaSql & "                                <br />"
            sStringaSql = sStringaSql & "                                = 2,04 se il nucleo è formato da tre componenti<br />"
            sStringaSql = sStringaSql & "                                = 2,46 se il nucleo è formato da quattro componenti"
            sStringaSql = sStringaSql & "                                <br />"
            sStringaSql = sStringaSql & "                                = 2,85 se il nucleo è formato da cinque componenti<br />"
            sStringaSql = sStringaSql & "                                maggiorazione di + 0,35 per ogni ulteriore componente</span></span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & Format(L4, "##,##0.00") & "</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'> "
            sStringaSql = sStringaSql & "                        </td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>Altri parametri correttivi</SPAN></td>"
            sStringaSql = sStringaSql & "                        <td style='border-top-width: 1px; border-left-width: 1px; border-left-color: black;"
            sStringaSql = sStringaSql & "                            border-bottom-width: 1px; border-bottom-color: black; border-top-color: black;"
            sStringaSql = sStringaSql & "                            border-right-width: 1px; border-right-color: black'> "
            sStringaSql = sStringaSql & "                        </td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            L5</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>- in caso di assenza di un genitore e presenza di figli minori</SPAN><br />"
            sStringaSql = sStringaSql & "                            <span style='font-size: 8pt'>(=0,2)</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & Format(L5, "##,##0.00") & "</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            L6</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>- per ogni componente con handicap o invalidità superiore al 66%</SPAN><br />"
            sStringaSql = sStringaSql & "                            <span style='font-size: 8pt'>(=0,5)</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & Format(L6, "##,##0.00") & "</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            L7</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>- per nuclei familiari con figli minori in cui entrambi i genitori svolgono attività"
            sStringaSql = sStringaSql & "                            di lavoro o di impresa</SPAN><br />"
            sStringaSql = sStringaSql & "                            <span style='font-size: 8pt'>(=0,2)</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & Format(L7, "##,##0.00") & "</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            L8</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>TOTALE PARAMETRI</SPAN><br />"
            sStringaSql = sStringaSql & "                            <span style='font-size: 8pt'>(sommare i valori di cui alle righe L4, L5, L6, L7)</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & Format(L8, "##,##0.00") & "</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            L9</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>INDICATORE ISEE-fsa</SPAN><br />"
            sStringaSql = sStringaSql & "                            <span style='font-size: 8pt'>(=L3 / L8)</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & Format(L9, "##,##0.00") & "</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            L10</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>INCIDENZA MASSIMA AMMISSIBILE DEL CANONE</SPAN><br />"
            sStringaSql = sStringaSql & "                            <span style='font-size: 8pt'>(Art. 5 del regolamento)</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & Format(L10, "##,##0.00") & "%</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            L11</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>CANONE ANNUO RISULTANTE DAL CONTRATTO</SPAN></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & Format(CDbl(CType(Dom_Contratto1.FindControl("txtAffitto"), TextBox).Text), "##,##0.00") & "</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            L12</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>SPESE ACCESSORIE</SPAN></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & Format(L12, "##,##0.00") & "</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            L13</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>CANONE INTEGRATO</SPAN></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & Format(L13, "##,##0.00") & "</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                            L14</td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>CANONE SOPPORTABILE</SPAN><br />"
            sStringaSql = sStringaSql & "                            <span style='font-size: 8pt'>(L3 * L10)</span></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid; font-size: 12pt;'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & Format(L14, "##,##0.00") & "</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                    <tr style='font-size: 12pt'>"
            sStringaSql = sStringaSql & "                        <td style='width: 43px'>"
            sStringaSql = sStringaSql & "                        </td>"
            sStringaSql = sStringaSql & "                        <td style='width: 651px'>"
            sStringaSql = sStringaSql & "                            <span style='font-size: 10pt; font-family: Arial'>MESI DI LOCAZIONE</SPAN></td>"
            sStringaSql = sStringaSql & "                        <td style='text-align: right;border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;"
            sStringaSql = sStringaSql & "                            border-bottom: black 1px solid'><span style='font-size: 10pt; font-family: Arial'>"
            sStringaSql = sStringaSql & "                            " & CDbl(CType(Dom_Contratto1.FindControl("txtMesi"), TextBox).Text) & "</SPAN></td>"
            sStringaSql = sStringaSql & "                    </tr>"
            sStringaSql = sStringaSql & "                </table>"
            sStringaSql = sStringaSql & "                <br />"
            sStringaSql = sStringaSql & "                <span><span></span><span style='font-family: Arial'>Questi risultati implicano:</span><br />"
            sStringaSql = sStringaSql & "                    <span style='font-family: Arial'>- La domanda è da considerarsi <strong>"


            dm_QuotaComunale = quotacomunale
            dm_QuotaRegionale = quotaregionale
            dm_QuotaTotale = contributoAFF

            If ESCLUSIONE <> "" Then
                sStringaSql = sStringaSql & "NON IDONEA </STRONG>per i seguenti motivi:<br>" & ESCLUSIONE & "<br />"
            Else
                sStringaSql = sStringaSql & "IDONEA<br />"
                sStringaSql = sStringaSql & "                    </strong><span style='font-family: Times New Roman'>- <span style='font-family: Arial'>"

                If LBLDIFFICOLTA.Visible = True Then
                    'sStringaSql = sStringaSql & "Il nucleo familiare è da considerarsi in grave difficoltà socioeconomiche e pertanto l'Amministrazione Comunale dovrà provvedere agli adempimenti di cui all'art. 7 comma 6 e 7 dell'allegato A1 e all'art. 6 lettere c) e d) dell'allegato A2 alla DGR VIII/2603 del 24 maggio 2006. <br>"
                End If
                If CType(Dom_Contratto1.FindControl("cmbStatoC"), DropDownList).SelectedItem.Value <> "REG" Then
                    sStringaSql = sStringaSql & "Il richiedente DEVE DIMOSTRARE all'atto dell'erogazione del contributo di aver inoltrato la richiesta di registrazione del contratto al competente ufficio e aver versato la relativa imposta.<br>"
                End If

                'sStringaSql = sStringaSql & "                        Il contributo spettante sarà determinato alla Chiusura dello Sportello Affitto " & AnnoBando
                'sStringaSql = sStringaSql & "                        in relazione alle richieste valide pervenute e alla disponibilità dei fondi statali"
                'sStringaSql = sStringaSql & "                        e regionali.</span></span></span></span>"
                sStringaSql = sStringaSql & "                         Contr. Comunale: " & dm_QuotaComunale & " Euro<br/>- Contr. Regionale: " & dm_QuotaRegionale & " Euro<br/>- Tot.: " & dm_QuotaTotale & " Euro</span></span></span></span>"
            End If

            If CType(Note1.FindControl("ChCartacea"), CheckBox).Checked = True Then
                sStringaSql = sStringaSql & "<br><BR>La Documentazione cartacea di questa domanda non è stata presentata."
            End If
            sStringaSql = sStringaSql & "<br><BR>Parametri del fondo sostegno Affitti utilizzati nel calcolo del contributo integrato:<br>"
            sStringaSql = sStringaSql & "1-Il Limite per le spese di locazione è di 7.000,00 Euro<br>"
            If maxContributo <> 999999999 Then
                sStringaSql = sStringaSql & "2-Il Limite del contributo base è di " & Format(maxContributo, "##,##0.00") & " Euro<br>"
            End If
            sStringaSql = sStringaSql & "3-La soglia limite del patrimonio è di " & Format(LIMITE_PATRIMONIO, "##,##0.00") & " Euro<br>"
            sStringaSql = sStringaSql & "4-Il Limite ISEE-Fsa per l'accesso al contributo è di 12.911,00 Euro<br>"
            sStringaSql = sStringaSql & "        </td></tr>"
            sStringaSql = sStringaSql & "    </table>"
            sStringaSql = sStringaSql & "</body>"
            sStringaSql = sStringaSql & "</html>"



            lblComunale.Text = "C. Comunale: " & dm_QuotaComunale
            lblRegionale.Text = "C. Regionale: " & dm_QuotaRegionale
            lblTotale.Text = "Tot.: " & dm_QuotaTotale & " Euro"
            lblAbbattimento.Text = "% Abb.:" & Format(perc_abbattimento, "0.00")


            Dim statod As String = ""

            If ESCLUSIONE <> "" Then
                statod = "ID_STATO='4', "
            Else
                statod = "ID_STATO='7a', "
            End If


            HttpContext.Current.Session.Add("DOMANDA", sStringaSql)


            par.cmd.CommandText = "UPDATE DOMANDE_BANDO_FSA SET " & statod & "IDONEO_PRESUNTO='" & IDONEO_PRESUNTO & "',fl_rinnovo='0',reddito_isee=" & par.VirgoleInPunti(ISEE_ERP) & ",isr_erp=" & par.VirgoleInPunti(ISR_ERP) & ",ise_erp=" & par.VirgoleInPunti(ISE_ERP) & ",isp_erp=" & par.VirgoleInPunti(ISP_ERP) & ",pse='" & VSE & "',vse='" & VSE & "',canone_int=" & par.VirgoleInPunti(CDbl(L13)) & ",canone_sup=" & par.VirgoleInPunti(CDbl(L14)) & ",tot_importo_erogato=" & par.VirgoleInPunti(Format(contributoAFF, "0")) & ",quotacomunalepagata=" & par.VirgoleInPunti(quotacomunale) & ",quotaregionalepagata=" & par.VirgoleInPunti(quotaregionale) & ",perc_abbattimento=" & par.VirgoleInPunti(Format(perc_abbattimento, "0.00")) & " where id=" & lIdDomanda
            par.cmd.ExecuteNonQuery()

            sStringaSql = "INSERT INTO EVENTI_BANDI_FSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
            & "','F141','','I')"
            par.cmd.CommandText = sStringaSql
            par.cmd.ExecuteNonQuery()


            sStringaSql = "INSERT INTO EVENTI_BANDI_FSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
            & "','F133','','I')"
            par.cmd.CommandText = sStringaSql
            par.cmd.ExecuteNonQuery()


            If ESCLUSIONE <> "" Then
                sStringaSql = "INSERT INTO EVENTI_BANDI_FSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
                & "','F57','" & par.PulisciStrSql(Replace(ESCLUSIONE, "<BR>", " - ")) & "','I')"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            Else
                sStringaSql = "INSERT INTO EVENTI_BANDI_FSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
                & "','F02','','I')"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            End If

            txtIndici.Text = "V1=" & dm_QuotaComunale _
           & "&V2=" & dm_QuotaRegionale _
           & "&V3=" & dm_QuotaTotale _
           & "&V4=" & "" _
           & "&V5=" & "" _
           & "&V6=" & "" _
           & "&V7=" & par.Converti(ISEE_ERP) _
           & "&V8=" & par.Converti(ISR_ERP) _
           & "&V9=" & par.Converti(ISP_ERP) _
           & "&V10=" & par.Converti(ISE_ERP) _
           & "&V11=" & "" _
           & "&V12=" & PARAMETRO _
           & "&V13=" & VSE _
           & "&PG=" & lblPG.Text & "&UJ=3"





            Response.Write("<script>window.open('../StampaDomanda.aspx','Domanda','');</script>")

            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_fsa WHERE ID=" & lIdDomanda & " FOR UPDATE NOWAIT"
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
End Class
