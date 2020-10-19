
Partial Class VSA_max
    Inherits PageSetIdMode
    Dim lValoreCorrente As Long
    Dim sAnnoIsee As String
    Dim sAnnoCanone As String
    Dim par As New CM.Global
    Dim bEseguito As Boolean = False
    Dim scriptblock As String

    Function AggiustaOggetti()
        'Response.Write("<script></script>")
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsPostBack = False And bEseguito = False Then
            txtTab.Value = "1"
            Response.Expires = 0
            txtDataPG.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            bMemorizzato = False
            lIdDichiarazione = Request.QueryString("ID")
            Fl_Integrazione = Request.QueryString("INT")
            Fl_US = Request.QueryString("US")
            If Fl_Integrazione = "1" Then
                H1.Value = "1"
                H2.Value = "1"
                Image3.Visible = True
                Label5.Visible = True
            End If
            txtTab.Value = "1"
            If lIdDichiarazione = -1 Then
                lNuovaDichiarazione = 1
                CType(Dic_Dichiarazione1.FindControl("Label20"), Label).Text = lNuovaDichiarazione
                imgStampa.ImageUrl = "../NuoveImm/Img_No_Stampa.png"
                CType(Dic_Dichiarazione1.FindControl("txtbinserito"), TextBox).Text = "0"
                NuovaDichiarazione()
                CType(Dic_Nucleo1.FindControl("ListBox1"), ListBox).Items.Add("")
            Else
                lNuovaDichiarazione = 0
                CType(Dic_Dichiarazione1.FindControl("Label20"), Label).Text = lNuovaDichiarazione
                imgStampa.ImageUrl = "../NuoveImm/Img_No_Stampa.png"
                VisualizzaDichiarazione()
                If Session.Item("ANAGRAFE") = "1" Then
                    imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('../Anagrafe.aspx?ID=" & par.criptamolto(lIdDichiarazione) & "&T=2','Anagrafe','top=0,left=0,width=600,height=400');")
                End If
            End If
            bEseguito = True
            AggiustaOggetti()
        End If

        scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "function CalcolaReddito() {window.open('RedditoConv.aspx?ID=" & lIdDichiarazione & "',null,'');}" _
            & "</script>"
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript3000")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript3000", scriptblock)
        End If

        If IsPostBack = True And HiddenField1.Value = "1" Then
            CaricaComponenti()
            HiddenField1.Value = "0"
        End If

        H1.Value = H2.Value
        Dic_Dichiarazione1.FindControl("chTitolare").Visible = False

        CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).Visible = True
        CType(Dic_Patrimonio1.FindControl("Label15"), Label).Visible = True
        CType(Dic_Patrimonio1.FindControl("chUbicazione"), CheckBox).Visible = True


        CType(Dic_Dichiarazione1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "VSA/Help_Dichiarazione.htm"
        CType(Dic_Patrimonio1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "VSA/Help_Dichiarazione.htm"
        CType(Dic_Reddito1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "VSA/Help_Dichiarazione.htm"
        CType(Dic_Sottoscrittore1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "VSA/Help_Dichiarazione.htm"
        CType(Dic_Integrazione1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "VSA/Help_Dichiarazione.htm"

    End Sub

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

    Function VisualizzaDichiarazione()
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



        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

            If Session.Item("ANAGRAFE") = "0" Then
                imgAnagrafe.Visible = False
                imgAnagrafe.Attributes.Clear()
            Else
                imgAnagrafe.Visible = True
            End If


            par.cmd.CommandText = "SELECT BANDI_VSA.TIPO_BANDO,BANDI_VSA.DATA_INIZIO,BANDI_VSA.STATO FROM BANDI_VSA,DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID=" & lIdDichiarazione & " AND DICHIARAZIONI_VSA.ID_BANDO=BANDI_VSA.ID"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader("STATO") <> 1 And Fl_Integrazione <> "1" And UCase(Mid(Session.Item("OPERATORE"), 1, 6)) <> "COMUNE" Then
                    btnSalva.Visible = False
                    'Label5.Visible = False
                    imgStampa.Visible = False
                    'Label6.Visible = False

                    Dic_Dichiarazione1.DisattivaTutto()
                    Dic_Integrazione1.DisattivaTutto()
                    Dic_Note1.DisattivaTutto()
                    Dic_Nucleo1.DisattivaTutto()
                    Dic_Patrimonio1.DisattivaTutto()
                    Dic_Reddito1.DisattivaTutto()

                    Dic_Reddito_Conv1.DisattivaTutto()

                    cmbStato.Enabled = False
                    If Fl_US <> "1" Then
                        Response.Write("<script>alert('Non è possibile apportare modifiche! Il bando a cui appartiene la domande è CHIUSO.');</script>")
                    End If
                Else
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
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT DICHIARAZIONI_VSA.ID_CAF,DOMANDE_BANDO_VSA.N_DISTINTA,CAF_WEB.COD_CAF,DOMANDE_BANDO_VSA.ID_STATO FROM DOMANDE_BANDO_VSA,DICHIARAZIONI_VSA,CAF_WEB WHERE DICHIARAZIONI_VSA.ID_CAF=CAF_WEB.ID AND DICHIARAZIONI_VSA.ID=" & lIdDichiarazione & " AND DOMANDE_BANDO_VSA.ID_DICHIARAZIONE=DICHIARAZIONI_VSA.ID"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                If Session.Item("ID_CAF") = "6" Then
                    If Session.Item("ID_CAF") = "6" Then
                        LBLENTE.Visible = True
                        LBLENTE.Text = "Ente Ins.: " & par.IfNull(myReader("COD_CAF"), "")
                        If par.IfNull(myReader("ID_STATO"), -1) <> "2" Then
                            btnSalva.Visible = False
                            imgStampa.Visible = False
                            Dic_Dichiarazione1.DisattivaTutto()
                            Dic_Integrazione1.DisattivaTutto()
                            Dic_Note1.DisattivaTutto()
                            Dic_Nucleo1.DisattivaTutto()
                            Dic_Patrimonio1.DisattivaTutto()
                            Dic_Reddito1.DisattivaTutto()
                            Dic_Reddito_Conv1.DisattivaTutto()
                            cmbStato.Enabled = False
                        End If


                        If par.IfNull(myReader("N_DISTINTA"), -1) = 0 And myReader("ID_CAF") <> Session.Item("ID_CAF") Then
                            btnSalva.Visible = False
                            'Label5.Visible = False
                            imgStampa.Visible = False
                            'Label6.Visible = False

                            Dic_Dichiarazione1.DisattivaTutto()
                            Dic_Integrazione1.DisattivaTutto()
                            Dic_Note1.DisattivaTutto()
                            Dic_Nucleo1.DisattivaTutto()
                            Dic_Patrimonio1.DisattivaTutto()
                            Dic_Reddito1.DisattivaTutto()
                            Dic_Reddito_Conv1.DisattivaTutto()
                            cmbStato.Enabled = False
                            If Fl_US <> "1" Then
                                Response.Write("<script>alert('Non è possibile modificare. La domanda è ancora in carico presso un ente esterno!');</script>")
                            End If
                        End If
                    Else
                        If par.IfNull(myReader("N_DISTINTA"), 0) > 0 And Fl_Integrazione <> "1" Then
                            btnSalva.Visible = False
                            'Label5.Visible = False
                            imgStampa.Visible = False
                            'Label6.Visible = False
                            Dic_Dichiarazione1.DisattivaTutto()
                            Dic_Integrazione1.DisattivaTutto()
                            Dic_Note1.DisattivaTutto()
                            Dic_Nucleo1.DisattivaTutto()
                            Dic_Patrimonio1.DisattivaTutto()
                            Dic_Reddito1.DisattivaTutto()
                            Dic_Reddito_Conv1.DisattivaTutto()
                            cmbStato.Enabled = False
                            If Fl_US <> "1" Then
                                Response.Write("<script>alert('Non è possibile modificare. La domanda è stata scaricata nella distinta N. " & par.IfNull(myReader("N_DISTINTA"), "") & " ! USARE LE FUNZIONI DI INTEGRAZIONE');</script>")
                            End If
                        End If
                    End If
                Else
                    If par.IfNull(myReader("N_DISTINTA"), 0) > 0 And Fl_Integrazione <> "1" Then
                        btnSalva.Visible = False
                        'Label5.Visible = False
                        imgStampa.Visible = False
                        'Label6.Visible = False

                        Dic_Dichiarazione1.DisattivaTutto()
                        Dic_Integrazione1.DisattivaTutto()
                        Dic_Note1.DisattivaTutto()
                        Dic_Nucleo1.DisattivaTutto()
                        Dic_Patrimonio1.DisattivaTutto()
                        Dic_Reddito1.DisattivaTutto()
                        Dic_Reddito_Conv1.DisattivaTutto()
                        cmbStato.Enabled = False
                        If Fl_US <> "1" Then
                            Response.Write("<script>alert('Non è possibile modificare. La domanda è stata scaricata nella distinta N. " & par.IfNull(myReader("N_DISTINTA"), "") & " ! USARE LE FUNZIONI DI INTEGRAZIONE');</script>")
                        End If
                    End If
                End If
            Else
                If Session.Item("ID_CAF") = "6" Then
                    'If Session.Item("ID_CAF") = "6" Then

                    '    btnSalva.Visible = False
                    '    'Label5.Visible = False
                    '    imgStampa.Visible = False
                    '    'Label6.Visible = False

                    '    Dic_Dichiarazione1.DisattivaTutto()
                    '    Dic_Integrazione1.DisattivaTutto()
                    '    Dic_Note1.DisattivaTutto()
                    '    Dic_Nucleo1.DisattivaTutto()
                    '    Dic_Patrimonio1.DisattivaTutto()
                    '    Dic_Reddito1.DisattivaTutto()
                    '    Dic_Reddito_Conv1.DisattivaTutto()
                    '    cmbStato.Enabled = False
                    '    If Fl_US <> "1" Then
                    '        Response.Write("<script>alert('Non è possibile modificare. La domanda NON è stata ancora creata!');</script>")
                    '    End If
                    'End If
                End If
            End If
            myReader.Close()

            txtDataPG.Text = Format(Now, "dd/MM/yyyy")
            Dim lsiFrutto As New ListItem("DA COMPLETARE", "0")
            cmbStato.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("COMPLETA", "1")
            cmbStato.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("DA CANCELLARE", "2")
            cmbStato.Items.Add(lsiFrutto)

            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbNazioneNas", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbNazioneRes", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbTipoIRes", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")

            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbNazioneNas", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbNazioneRes", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbTipoIRes", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            par.RiempiDList(Dic_Patrimonio1, par.OracleConn, "cmbTipoImm", "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")

            par.cmd.CommandText = "SELECT COUNT(ID_PRATICA) FROM EVENTI_DICHIARAZIONI_VSA WHERE COD_EVENTO='F132' AND ID_PRATICA=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If myReader(0) > 0 Then
                    Session.Item("STAMPATO") = "1"
                Else
                    Session.Item("STAMPATO") = "0"
                End If
            Else
                Session.Item("STAMPATO") = "0"
            End If

            myReader.Close()
            If Fl_US <> "1" Then
                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            Else
                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione
            End If
            myReader = par.cmd.ExecuteReader()
            lblPG.ToolTip = lIdDichiarazione
            If myReader.Read() Then
                lIndice_Bando = myReader("ID_BANDO")
                lblPG.Text = par.IfNull(myReader("pg"), "")
                txtDataPG.Text = par.FormattaData(par.IfNull(myReader("data_pg"), ""))
                CType(Dic_Dichiarazione1.FindControl("txtId"), TextBox).Text = lIdDichiarazione
                CType(Dic_Dichiarazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data"), ""))
                CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")

                CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = Val(par.IfNull(myReader("ANNO_SIT_ECONOMICA"), ""))
                CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")

                CType(Dic_Note1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader("NOTE"), "")

                CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_INT_ERP"), ""))
                CType(Dic_Sottoscrittore1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_S"), ""))

                CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).SelectedIndex = -1
                CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Items.FindByValue(par.IfNull(myReader("ID_TIPO_CAT_AB"), "0")).Selected = True

                CType(Dic_Reddito_Conv1.FindControl("txtMinori"), TextBox).Text = par.IfNull(myReader("minori_carico"), "0")

                CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).SelectedIndex = -1
                CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).Items.FindByValue(par.IfNull(myReader("POSSESSO_UI"), "-1")).Selected = True

                If par.IfNull(myReader("FL_UBICAZIONE"), "0") = "0" Then
                    CType(Dic_Patrimonio1.FindControl("ChUbicazione"), CheckBox).Checked = False
                Else
                    CType(Dic_Patrimonio1.FindControl("ChUbicazione"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("POSSESSO_UI"), "-1") <> "-1" Then
                    CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).Enabled = True
                    CType(Dic_Patrimonio1.FindControl("chUbicazione"), CheckBox).Enabled = True
                Else
                    CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).Enabled = False
                    CType(Dic_Patrimonio1.FindControl("chUbicazione"), CheckBox).Enabled = False
                End If


                If par.IfNull(myReader("ID_TIPO_CAT_AB"), "-1") <> "-1" Then
                    CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = True
                End If

                cT = Dic_Dichiarazione1.FindControl("txtCAPRes")
                cT.Text = par.IfNull(myReader("CAP_RES_DNTE"), "")

                cT = Dic_Dichiarazione1.FindControl("txtIndRes")
                cT.Text = par.IfNull(myReader("IND_RES_DNTE"), "")

                cT = Dic_Dichiarazione1.FindControl("txtCivicoRes")
                cT.Text = par.IfNull(myReader("CIVICO_RES_DNTE"), "")

                cT = Dic_Dichiarazione1.FindControl("txtTelRes")
                cT.Text = par.IfNull(myReader("TELEFONO_DNTE"), "")

                lIndiceAppoggio_0 = myReader("ID_LUOGO_NAS_DNTE")
                lIndiceAppoggio_1 = myReader("ID_LUOGO_RES_DNTE")
                lIndiceAppoggio_2 = myReader("ID_TIPO_IND_RES_DNTE")

                If par.IfNull(myReader("FL_GIA_TITOLARE"), "0") = "0" Then
                    CType(Dic_Dichiarazione1.FindControl("chTitolare"), CheckBox).Checked = False
                Else
                    CType(Dic_Dichiarazione1.FindControl("chTitolare"), CheckBox).Checked = True
                End If


                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    CT1 = Dic_Dichiarazione1.FindControl("cmbNazioneNas")
                    CT1.SelectedIndex = -1
                    If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                        CT1 = Dic_Dichiarazione1.FindControl("cmbPrNas")
                        CT1.Visible = False
                        CT1 = Dic_Dichiarazione1.FindControl("cmbComuneNas")
                        CT1.Visible = False
                        CTT = Dic_Dichiarazione1.FindControl("label6")
                        CTT.Visible = False
                        CTT = Dic_Dichiarazione1.FindControl("label7")
                        CTT.Visible = False
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = Dic_Dichiarazione1.FindControl("cmbPrNas")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                        par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = Dic_Dichiarazione1.FindControl("cmbComuneNas")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                    End If
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    CT1 = Dic_Dichiarazione1.FindControl("cmbTipoIRes")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader1("DESCRIZIONE")).Selected = True
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    CT1 = Dic_Dichiarazione1.FindControl("cmbNazioneRes")
                    CT1.SelectedIndex = -1
                    If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = Dic_Dichiarazione1.FindControl("cmbPrRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                        par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = Dic_Dichiarazione1.FindControl("cmbComuneRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True

                        'cT = Dic_Dichiarazione1.FindControl("txtCAPRes")
                        'cT.Text = myReader1("CAP")
                    End If
                End If
                myReader1.Close()
            End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    lblDomAssociata.Text = par.IfNull(myReader("PG"), "")
                End If
                myReader.Close()
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA where PROGR=0 AND id_DICHIARAZIONE=" & lIdDichiarazione
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    CType(Dic_Dichiarazione1.FindControl("txtCognome"), TextBox).Text = par.IfNull(myReader("COGNOME"), "")
                    CType(Dic_Dichiarazione1.FindControl("txtNome"), TextBox).Text = par.IfNull(myReader("NOME"), "")
                    CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text = par.IfNull(myReader("COD_FISCALE"), "")
                    CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), ""))
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM SOTTOSCRITTORI_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then

                    CType(Dic_Sottoscrittore1.FindControl("txtS"), TextBox).Text = "1"

                    CType(Dic_Sottoscrittore1.FindControl("txtCognome"), TextBox).Text = par.IfNull(myReader("COGNOME"), "")
                    CType(Dic_Sottoscrittore1.FindControl("txtNome"), TextBox).Text = par.IfNull(myReader("NOME"), "")
                    CType(Dic_Sottoscrittore1.FindControl("txtDataNascita"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_NAS"), ""))

                    cT = Dic_Sottoscrittore1.FindControl("txtIndRes")
                    cT.Text = par.IfNull(myReader("IND"), "")

                    cT = Dic_Sottoscrittore1.FindControl("txtCivicoRes")
                    cT.Text = par.IfNull(myReader("CIVICO"), "")

                    cT = Dic_Sottoscrittore1.FindControl("txtTelRes")
                    cT.Text = par.IfNull(myReader("TELEFONO"), "")

                    cT = Dic_Sottoscrittore1.FindControl("txtCAPRes")
                    cT.Text = par.IfNull(myReader("CAP_RES"), "")

                    lIndiceAppoggio_0 = myReader("ID_LUOGO_NAS")
                    lIndiceAppoggio_1 = myReader("ID_LUOGO_RES")
                    lIndiceAppoggio_2 = myReader("ID_TIPO_IND")
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    CT1 = Dic_Sottoscrittore1.FindControl("cmbNazioneNas")
                    CT1.SelectedIndex = -1
                    If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader("NOME")).Selected = True

                        CT1 = Dic_Sottoscrittore1.FindControl("cmbPrNas")
                        CT1.Visible = False
                        CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneNas")
                        CT1.Visible = False
                        CTT = Dic_Sottoscrittore1.FindControl("label6")
                        CTT.Visible = False
                        CTT = Dic_Sottoscrittore1.FindControl("label7")
                        CTT.Visible = False
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = Dic_Sottoscrittore1.FindControl("cmbPrNas")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                        par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneNas")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader("NOME")).Selected = True
                    End If
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    CT1 = Dic_Sottoscrittore1.FindControl("cmbTipoIRes")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("DESCRIZIONE")).Selected = True
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    CT1 = Dic_Sottoscrittore1.FindControl("cmbNazioneRes")
                    CT1.SelectedIndex = -1
                    If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader("NOME")).Selected = True

                        CT1 = Dic_Sottoscrittore1.FindControl("cmbPrRes")
                        CT1.Visible = False
                        CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneRes")
                        CT1.Visible = False
                        CTT = Dic_Sottoscrittore1.FindControl("label10")
                        CTT.Visible = False
                        CTT = Dic_Sottoscrittore1.FindControl("label11")
                        CTT.Visible = False

                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = Dic_Sottoscrittore1.FindControl("cmbPrRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                        par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader("NOME")).Selected = True

                        'cT = Dic_Sottoscrittore1.FindControl("txtCAPRes")
                        'cT.Text = myReader("CAP")
                    End If
                End If
                myReader.Close()

                Dim MIAS As String
                Dim INDENNITA As String

                txtbinserito.Value = "1"
                par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.*,T_TIPO_PARENTELA.DESCRIZIONE FROM COMP_NUCLEO_VSA,T_TIPO_PARENTELA where COMP_NUCLEO_VSA.id_DICHIARAZIONE=" & lIdDichiarazione & " AND COMP_NUCLEO_VSA.GRADO_PARENTELA=T_TIPO_PARENTELA.COD ORDER BY PROGR ASC"
                myReader = par.cmd.ExecuteReader()
                While myReader.Read
                    If par.IfNull(myReader("INDENNITA_ACC"), "0") = "1" Then
                        INDENNITA = "SI"
                    Else
                        INDENNITA = "NO"
                    End If
                    MIAS = ""
                    MIAS = par.MiaFormat(par.IfNull(myReader("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 2)
                    CType(Dic_Nucleo1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(MIAS, myReader("PROGR")))
                    If par.IfNull(myReader("PERC_INVAL"), "0") = "100" And INDENNITA = "SI" Then
                        CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " ", myReader("PROGR")))
                    End If
                CType(Dic_Nucleo1.FindControl("txtProgr"), TextBox).Text = myReader("PROGR") + 1

                SOMMA = 0
                par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE_VSA WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader2.Read
                    SOMMA = SOMMA + Val(myReader2("IMPORTO"))
                    DESCRIZIONE = par.IfNull(myReader2("DESCRIZIONE"), "")
                    MIOPROGR = myReader("PROGR")
                End While
                myReader2.Close()
                If SOMMA <> 0 Then
                    For i = 0 To CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items.Count - 1
                        If MIOPROGR = CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items(i).Value Then
                            CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items(i).Text = par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " " & par.MiaFormat(SOMMA, 6) & ",00   " & par.MiaFormat(DESCRIZIONE, 17)
                            'CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " " & par.MiaFormat(myReader2("IMPORTO"), 6) & ",00   " & par.MiaFormat(myReader2("DESCRIZIONE"), 17), myReader("PROGR")))
                        End If
                    Next
                End If


                par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB_VSA WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    CType(Dic_Patrimonio1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("COGNOME"), "") & "," & par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(myReader2("COD_INTERMEDIARIO"), 27) & " " & par.MiaFormat(myReader2("INTERMEDIARIO"), 16) & " " & par.MiaFormat(myReader2("IMPORTO"), 8) & ",00 ", myReader("PROGR")))
                End While
                myReader2.Close()

                par.cmd.CommandText = "SELECT COMP_PATR_IMMOB_VSA.*,T_TIPO_PATR_IMMOB.descrizione FROM T_TIPO_PATR_IMMOB,COMP_PATR_IMMOB_VSA WHERE COMP_PATR_IMMOB_VSA.ID_COMPONENTE=" & myReader("ID") & " and COMP_PATR_IMMOB_VSA.ID_TIPO=T_TIPO_PATR_IMMOB.cod (+) ORDER BY COMP_PATR_IMMOB_VSA.ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    RESIDENZA = " "
                    If myReader2("F_RESIDENZA") = "0" Then
                        RESIDENZA = "NO"
                    Else
                        RESIDENZA = "SI"
                    End If
                    CType(Dic_Patrimonio1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 25) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 20) & " " & par.MiaFormat(myReader2("PERC_PATR_IMMOBILIARE"), 6) & " " & par.MiaFormat(Val(myReader2("VALORE")), 8) & ",00 " & par.MiaFormat(Val(myReader2("MUTUO")), 8) & ",00 " & RESIDENZA, myReader("PROGR")))
                End While
                myReader2.Close()

                par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    CType(Dic_Reddito_Conv1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("COGNOME"), "") & "," & par.IfNull(myReader("NOME"), ""), 35) & " " & par.MiaFormat(myReader2("CONDIZIONE"), 5) & " " & par.MiaFormat(myReader2("PROFESSIONE"), 5) & Chr(160) & Chr(160) & " " & par.MiaFormat(myReader2("DIPENDENTE"), 7) & ",00 " & par.MiaFormat(myReader2("PENSIONE"), 7) & ",00 " & par.MiaFormat(myReader2("AUTONOMO"), 7) & ",00 " & par.MiaFormat(myReader2("NON_IMPONIBILI"), 7) & ",00 " & par.MiaFormat(myReader2("occasionali"), 7) & ",00 " & par.MiaFormat(myReader2("DOM_AG_FAB"), 7) & ",00 " & par.MiaFormat(myReader2("ONERI"), 7) & ",00 ", myReader("PROGR")))
                End While
                myReader2.Close()


                par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    CType(Dic_Reddito1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 35) & " " & par.MiaFormat(myReader2("REDDITO_IRPEF"), 15) & ",00 " & par.MiaFormat(myReader2("PROV_AGRARI"), 15) & ",00", myReader("PROGR")))
                End While
                myReader2.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    CType(Dic_Integrazione1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 50) & " " & par.MiaFormat(Val(myReader2("IMPORTO")), 8) & ",00 ", myReader("PROGR")))
                End While
                myReader2.Close()

                par.cmd.CommandText = "SELECT COMP_DETRAZIONI_VSA.*,T_TIPO_DETRAZIONI.descrizione FROM T_TIPO_DETRAZIONI,COMP_DETRAZIONI_VSA WHERE COMP_DETRAZIONI_VSA.ID_COMPONENTE=" & myReader("id") & " and COMP_DETRAZIONI_VSA.ID_TIPO=T_TIPO_DETRAZIONI.cod (+) order by comp_detrazioni_VSA.id_componente asc"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    CType(Dic_Integrazione1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 30) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 35) & " " & par.MiaFormat(myReader2("importo"), 8) & ",00 ", myReader("PROGR")))
                End While
                myReader2.Close()

            End While
            myReader.Close()

            If Fl_US <> "1" Then
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
            End If
            Session.Add("LAVORAZIONE", "1")

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Pratica aperta da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If
                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione
                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader3.Read() Then
                    lIndice_Bando = myReader3("ID_BANDO")
                    lblPG.Text = par.IfNull(myReader3("pg"), "")
                    txtDataPG.Text = par.FormattaData(par.IfNull(myReader3("data_pg"), ""))
                    CType(Dic_Dichiarazione1.FindControl("txtId"), TextBox).Text = lIdDichiarazione
                    CType(Dic_Dichiarazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("data"), ""))
                    CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = par.IfNull(myReader3("ANNO_SIT_ECONOMICA"), "")
                    CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader3("ANNO_SIT_ECONOMICA"), "")
                    CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = Val(par.IfNull(myReader3("ANNO_SIT_ECONOMICA"), ""))
                    CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader3("ANNO_SIT_ECONOMICA"), "")

                    CType(Dic_Note1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader3("NOTE"), "")

                    CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("DATA_INT_ERP"), ""))
                    CType(Dic_Sottoscrittore1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("DATA_S"), ""))

                    CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).SelectedIndex = -1
                    CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Items.FindByValue(par.IfNull(myReader3("ID_TIPO_CAT_AB"), "0")).Selected = True

                    If par.IfNull(myReader3("ID_TIPO_CAT_AB"), "-1") <> "-1" Then
                        CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = True
                    End If

                    cT = Dic_Dichiarazione1.FindControl("txtCAPRes")
                    cT.Text = par.IfNull(myReader3("CAP_RES_DNTE"), "")

                    cT = Dic_Dichiarazione1.FindControl("txtIndRes")
                    cT.Text = par.IfNull(myReader3("IND_RES_DNTE"), "")

                    cT = Dic_Dichiarazione1.FindControl("txtCivicoRes")
                    cT.Text = par.IfNull(myReader3("CIVICO_RES_DNTE"), "")

                    cT = Dic_Dichiarazione1.FindControl("txtTelRes")
                    cT.Text = par.IfNull(myReader3("TELEFONO_DNTE"), "")

                    lIndiceAppoggio_0 = myReader3("ID_LUOGO_NAS_DNTE")
                    lIndiceAppoggio_1 = myReader3("ID_LUOGO_RES_DNTE")
                    lIndiceAppoggio_2 = myReader3("ID_TIPO_IND_RES_DNTE")

                    If par.IfNull(myReader3("FL_GIA_TITOLARE"), "0") = "0" Then
                        CType(Dic_Dichiarazione1.FindControl("chTitolare"), CheckBox).Checked = False
                    Else
                        CType(Dic_Dichiarazione1.FindControl("chTitolare"), CheckBox).Checked = True
                    End If

                    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        CT1 = Dic_Dichiarazione1.FindControl("cmbNazioneNas")
                        CT1.SelectedIndex = -1
                        If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                            CT1.Items.FindByText(myReader1("NOME")).Selected = True

                            CT1 = Dic_Dichiarazione1.FindControl("cmbPrNas")
                            CT1.Visible = False
                            CT1 = Dic_Dichiarazione1.FindControl("cmbComuneNas")
                            CT1.Visible = False
                            CTT = Dic_Dichiarazione1.FindControl("label6")
                            CTT.Visible = False
                            CTT = Dic_Dichiarazione1.FindControl("label7")
                            CTT.Visible = False

                        Else
                            CT1.Items.FindByText("ITALIA").Selected = True
                            CT1 = Dic_Dichiarazione1.FindControl("cmbPrNas")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                            CT1 = Dic_Dichiarazione1.FindControl("cmbComuneNas")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader1("NOME")).Selected = True
                        End If
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        CT1 = Dic_Dichiarazione1.FindControl("cmbTipoIRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("DESCRIZIONE")).Selected = True
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        CT1 = Dic_Dichiarazione1.FindControl("cmbNazioneRes")
                        CT1.SelectedIndex = -1
                        If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                            CT1.Items.FindByText(myReader1("NOME")).Selected = True
                        Else
                            CT1.Items.FindByText("ITALIA").Selected = True
                            CT1 = Dic_Dichiarazione1.FindControl("cmbPrRes")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                            CT1 = Dic_Dichiarazione1.FindControl("cmbComuneRes")
                            CT1.SelectedIndex = -1
                            CT1.Items.FindByText(myReader1("NOME")).Selected = True

                            'cT = Dic_Dichiarazione1.FindControl("txtCAPRes")
                            'cT.Text = myReader1("CAP")
                        End If
                    End If
                    myReader1.Close()
                End If
                myReader3.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA where PROGR=0 AND id_DICHIARAZIONE=" & lIdDichiarazione
                myReader3 = par.cmd.ExecuteReader()
                If myReader3.Read() Then
                    CType(Dic_Dichiarazione1.FindControl("txtCognome"), TextBox).Text = par.IfNull(myReader3("COGNOME"), "")
                    CType(Dic_Dichiarazione1.FindControl("txtNome"), TextBox).Text = par.IfNull(myReader3("NOME"), "")
                    CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text = par.IfNull(myReader3("COD_FISCALE"), "")
                    CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("DATA_NASCITA"), ""))
                End If
                myReader3.Close()

                par.cmd.CommandText = "SELECT * FROM SOTTOSCRITTORI_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
                myReader3 = par.cmd.ExecuteReader()
                If myReader3.Read() Then

                    CType(Dic_Sottoscrittore1.FindControl("txtS"), TextBox).Text = "1"

                    CType(Dic_Sottoscrittore1.FindControl("txtCognome"), TextBox).Text = par.IfNull(myReader3("COGNOME"), "")
                    CType(Dic_Sottoscrittore1.FindControl("txtNome"), TextBox).Text = par.IfNull(myReader3("NOME"), "")
                    CType(Dic_Sottoscrittore1.FindControl("txtDataNascita"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("DATA_NAS"), ""))

                    cT = Dic_Sottoscrittore1.FindControl("txtIndRes")
                    cT.Text = par.IfNull(myReader3("IND"), "")

                    cT = Dic_Sottoscrittore1.FindControl("txtCivicoRes")
                    cT.Text = par.IfNull(myReader3("CIVICO"), "")

                    cT = Dic_Sottoscrittore1.FindControl("txtTelRes")
                    cT.Text = par.IfNull(myReader3("TELEFONO"), "")

                    lIndiceAppoggio_0 = myReader3("ID_LUOGO_NAS")
                    lIndiceAppoggio_1 = myReader3("ID_LUOGO_RES")
                    lIndiceAppoggio_2 = myReader3("ID_TIPO_IND")
                End If
                myReader3.Close()

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
                myReader3 = par.cmd.ExecuteReader()
                If myReader3.Read() Then
                    CT1 = Dic_Sottoscrittore1.FindControl("cmbNazioneNas")
                    CT1.SelectedIndex = -1
                    If myReader3("SIGLA") = "E" Or myReader3("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader3("NOME")).Selected = True
                        CT1 = Dic_Sottoscrittore1.FindControl("cmbPrNas")
                        CT1.Visible = False
                        CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneNas")
                        CT1.Visible = False
                        CTT = Dic_Sottoscrittore1.FindControl("label6")
                        CTT.Visible = False
                        CTT = Dic_Sottoscrittore1.FindControl("label7")
                        CTT.Visible = False
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = Dic_Sottoscrittore1.FindControl("cmbPrNas")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader3("SIGLA")).Selected = True
                        par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader3("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneNas")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader3("NOME")).Selected = True
                    End If
                End If
                myReader3.Close()

                par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
                myReader3 = par.cmd.ExecuteReader()
                If myReader3.Read() Then
                    CT1 = Dic_Sottoscrittore1.FindControl("cmbTipoIRes")
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader3("DESCRIZIONE")).Selected = True
                End If
                myReader3.Close()

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
                myReader3 = par.cmd.ExecuteReader()
                If myReader3.Read() Then
                    CT1 = Dic_Sottoscrittore1.FindControl("cmbNazioneRes")
                    CT1.SelectedIndex = -1
                    If myReader3("SIGLA") = "E" Or myReader3("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader3("NOME")).Selected = True
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = Dic_Sottoscrittore1.FindControl("cmbPrRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader3("SIGLA")).Selected = True
                        par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader3("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneRes")
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader3("NOME")).Selected = True

                        cT = Dic_Sottoscrittore1.FindControl("txtCAPRes")
                        cT.Text = myReader3("CAP")
                    End If
                End If
                myReader3.Close()

                Dim MIAS As String
                Dim INDENNITA As String

                txtbinserito.Value = "1"
                par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.*,T_TIPO_PARENTELA.DESCRIZIONE FROM COMP_NUCLEO_VSA,T_TIPO_PARENTELA where COMP_NUCLEO_VSA.id_DICHIARAZIONE=" & lIdDichiarazione & " AND COMP_NUCLEO_VSA.GRADO_PARENTELA=T_TIPO_PARENTELA.COD ORDER BY PROGR ASC"
                myReader3 = par.cmd.ExecuteReader()
                While myReader3.Read
                    If par.IfNull(myReader3("INDENNITA_ACC"), "0") = "1" Then
                        INDENNITA = "SI"
                    Else
                        INDENNITA = "NO"
                    End If
                    MIAS = ""
                    MIAS = par.MiaFormat(par.IfNull(myReader3("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader3("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader3("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader3("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader3("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader3("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader3("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 2)
                    CType(Dic_Nucleo1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(MIAS, myReader3("PROGR")))
                    If par.IfNull(myReader3("PERC_INVAL"), "0") = "100" And INDENNITA = "SI" Then
                        CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 52) & " ", myReader3("PROGR")))
                    End If
                    CType(Dic_Nucleo1.FindControl("txtProgr"), TextBox).Text = myReader3("PROGR") + 1

                    SOMMA = 0
                    par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE_VSA WHERE ID_COMPONENTE=" & myReader3("ID") & " ORDER BY ID_COMPONENTE ASC"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader2.Read
                        SOMMA = SOMMA + Val(myReader2("IMPORTO"))
                        DESCRIZIONE = par.IfNull(myReader2("DESCRIZIONE"), "")
                        MIOPROGR = myReader3("PROGR")
                    End While
                    myReader2.Close()
                    If SOMMA <> 0 Then
                        For i = 0 To CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items.Count - 1
                            If MIOPROGR = CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items(i).Value Then
                                CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items(i).Text = par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 52) & " " & par.MiaFormat(SOMMA, 6) & ",00   " & par.MiaFormat(DESCRIZIONE, 17)
                                'CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " " & par.MiaFormat(myReader2("IMPORTO"), 6) & ",00   " & par.MiaFormat(myReader2("DESCRIZIONE"), 17), myReader("PROGR")))
                            End If
                        Next
                    End If


                    par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB_VSA WHERE ID_COMPONENTE=" & myReader3("ID") & " ORDER BY ID_COMPONENTE ASC"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        CType(Dic_Patrimonio1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader3("COGNOME"), "") & "," & par.IfNull(myReader3("NOME"), ""), 25) & " " & par.MiaFormat(myReader2("COD_INTERMEDIARIO"), 13) & " " & par.MiaFormat(myReader2("INTERMEDIARIO"), 30) & " " & par.MiaFormat(myReader2("IMPORTO"), 8) & ",00 ", myReader3("PROGR")))
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT COMP_PATR_IMMOB_VSA.*,T_TIPO_PATR_IMMOB.descrizione FROM T_TIPO_PATR_IMMOB,COMP_PATR_IMMOB_VSA WHERE COMP_PATR_IMMOB_VSA.ID_COMPONENTE=" & myReader3("ID") & " and COMP_PATR_IMMOB_VSA.ID_TIPO=T_TIPO_PATR_IMMOB.cod (+) ORDER BY COMP_PATR_IMMOB_VSA.ID_COMPONENTE ASC"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        RESIDENZA = " "
                        If myReader2("F_RESIDENZA") = "0" Then
                            RESIDENZA = "NO"
                        Else
                            RESIDENZA = "SI"
                        End If
                        CType(Dic_Patrimonio1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 25) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 20) & " " & par.MiaFormat(myReader2("PERC_PATR_IMMOBILIARE"), 6) & " " & par.MiaFormat(Val(myReader2("VALORE")), 8) & ",00 " & par.MiaFormat(Val(myReader2("MUTUO")), 8) & ",00 " & RESIDENZA, myReader3("PROGR")))
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & myReader3("ID") & " ORDER BY ID_COMPONENTE ASC"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        CType(Dic_Reddito1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 35) & " " & par.MiaFormat(myReader2("REDDITO_IRPEF"), 15) & ",00 " & par.MiaFormat(myReader2("PROV_AGRARI"), 15) & ",00", myReader3("PROGR")))
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE=" & myReader3("ID") & " ORDER BY ID_COMPONENTE ASC"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        CType(Dic_Integrazione1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 50) & " " & par.MiaFormat(Val(myReader2("IMPORTO")), 8) & ",00 ", myReader3("PROGR")))
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT COMP_DETRAZIONI_VSA.*,T_TIPO_DETRAZIONI.descrizione FROM T_TIPO_DETRAZIONI,COMP_DETRAZIONI_VSA WHERE COMP_DETRAZIONI_VSA.ID_COMPONENTE=" & myReader3("id") & " and COMP_DETRAZIONI_VSA.ID_TIPO=T_TIPO_DETRAZIONI.cod (+) order by comp_detrazioni_VSA.id_componente asc"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        CType(Dic_Integrazione1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 30) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 35) & " " & par.MiaFormat(myReader2("importo"), 8) & ",00 ", myReader3("PROGR")))
                    End While
                    myReader2.Close()

                End While
                myReader3.Close()


                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            Else
                Label4.Text = EX1.Message

            End If
            imgStampa.Enabled = False
            imgStampa.Visible = False
            btnSalva.Enabled = False
            btnSalva.Visible = False
            'Label5.Visible = False
            'Label6.Visible = False
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("TRANSAZIONE")
            HttpContext.Current.Session.Remove("CONNESSIONE")
            Session.Item("LAVORAZIONE") = "0"
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            imgStampa.Enabled = False
            btnSalva.Enabled = False
            Label4.Text = ex.Message
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("TRANSAZIONE")
            HttpContext.Current.Session.Remove("CONNESSIONE")
            Session.Item("LAVORAZIONE") = "0"
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Function


    Private Function CaricaComponenti()
        Dim MIAS As String
        Dim INDENNITA As String

        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        txtbinserito.Value = "1"
        par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.*,T_TIPO_PARENTELA.DESCRIZIONE FROM COMP_NUCLEO_VSA,T_TIPO_PARENTELA where COMP_NUCLEO_VSA.id_DICHIARAZIONE=" & lIdDichiarazione & " AND COMP_NUCLEO_VSA.GRADO_PARENTELA=T_TIPO_PARENTELA.COD ORDER BY PROGR ASC"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader.Read
            If par.IfNull(myReader("INDENNITA_ACC"), "0") = "1" Then
                INDENNITA = "SI"
            Else
                INDENNITA = "NO"
            End If
            MIAS = ""
            MIAS = par.MiaFormat(par.IfNull(myReader("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 2)
            CType(Dic_Nucleo1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(MIAS, myReader("PROGR")))
            If par.IfNull(myReader("PERC_INVAL"), "0") = "100" And INDENNITA = "SI" Then
                CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " ", myReader("PROGR")))
            End If
            CType(Dic_Nucleo1.FindControl("txtProgr"), TextBox).Text = myReader("PROGR") + 1

        End While
        myReader.Close()
    End Function

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

    Public Property iTab() As Integer
        Get
            If Not (ViewState("par_itab1") Is Nothing) Then
                Return CInt(ViewState("par_itab1"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_itab1") = value
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

    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        Try
            If Session.Item("LAVORAZIONE") = "1" Then
                If Fl_Integrazione = "1" Then
                    H1.Value = "1"
                End If
                If Session.Item("STAMPATO") = "0" And cmbStato.Text = "1" Then
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "alert('ATTENZIONE, La Dichiarazione deve essere stampata!');" _
                                & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript31")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript31", scriptblock)
                    End If
                    Exit Sub
                End If
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.OracleConn.Close()
                End If

                If Fl_US <> "1" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans
                    par.myTrans.Rollback()
                End If
                'par.myTrans.Dispose()
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                HttpContext.Current.Session.Remove("TRANSAZIONE")
                HttpContext.Current.Session.Remove("CONNESSIONE")
                Session.Item("LAVORAZIONE") = "0"
                Session.Remove("STAMPATO")
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
                If Fl_Integrazione = "1" Or Fl_US = "1" Then
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

    Private Function VerificaDati(ByRef S As String) As Boolean
        VerificaDati = True
        If CType(Dic_Dichiarazione1.FindControl("txtData1"), TextBox).Text = "" Then
            Response.Write("<script>alert('Attenzione...Campo <MILANO, Lì> deve essere valorizzato!')</script>")
            VerificaDati = False
            Exit Function
        End If
        If CType(Dic_Dichiarazione1.FindControl("txtCognome"), TextBox).Text = "" Then
            Response.Write("<script>alert('Attenzione...Campo <COGNOME> deve essere valorizzato!')</script>")
            VerificaDati = False
            Exit Function
        End If
        If CType(Dic_Dichiarazione1.FindControl("txtNome"), TextBox).Text = "" Then
            Response.Write("<script>alert('Attenzione...Campo <NOME> deve essere valorizzato!')</script>")
            VerificaDati = False
            Exit Function
        End If
        If CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text = "" Then
            Response.Write("<script>alert('Attenzione...Campo <CODICE FISCALE> deve essere valorizzato!')</script>")
            VerificaDati = False
            Exit Function
        End If

        If CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text = "" Then
            Response.Write("<script>alert('Attenzione...Campo <DATA DI NASCITA> deve essere valorizzato!')</script>")
            VerificaDati = False
            Exit Function
        End If

        If par.ControllaValiditaCF(CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text, CType(Dic_Dichiarazione1.FindControl("txtCognome"), TextBox).Text, CType(Dic_Dichiarazione1.FindControl("txtNome"), TextBox).Text, CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text) = False Then
            Response.Write("<script>alert('Attenzione...Il codice fiscale non è corretto!')</script>")
            VerificaDati = False
            Exit Function
        End If


    End Function

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Dim idComponenti(15) As Long
        Dim S As String = ""
        Dim i As Integer
        Dim j As Integer
        'Dim progr As Integer
        Dim NUM_PARENTI As Integer
        Dim totconv As Double
        Dim TotISEE As Double



        Try

            bMemorizzato = False

            NUM_PARENTI = 0

            N_INV_100_ACC = 0
            N_INV_100_NO_ACC = 0
            N_INV_100_66 = 0



            If VerificaDati(S) = False Then
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "../NuoveImm/Img_No_Stampa.png"
                Exit Try
            End If

            If DateDiff("m", DateSerial(Mid(CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text, 7, 4), Mid(CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text, 4, 2), Mid(CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text, 1, 2)), Now) / 12 < 18 Then
                Response.Write("<script>alert('Attenzione...Il richiedente deve essere maggiorenne! Memorizzazione non effettuata.')</script>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "../NuoveImm/Img_No_Stampa.png"
                Exit Try
            End If

            If Len(CType(Dic_Dichiarazione1.FindControl("txtCapRes"), TextBox).Text) < 5 Then
                Response.Write("<script>alert('Attenzione...Il CAP di Residenza deve essere 5 caratteri! Memorizzazione non effettuata.')</script>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "../NuoveImm/Img_No_Stampa.png"
                Exit Try
            End If

            If CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).SelectedItem.Text <> "SI" And CType(Dic_Patrimonio1.FindControl("ChUbicazione"), CheckBox).Checked = True Then
                Response.Write("<script>alert('ATTENZIONE...è possibile specificare la dislocazione dell\'immobile solo se la U.i. posseduta è adeguata al nucleo o di valore')</script>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "..\ImmMaschere\blu_stampa_no.jpg"
                Exit Try
            End If

            S = ""

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            'End If

            Dim sStringaSql As String


            par.cmd.CommandText = "SELECT comp_nucleo_VSA.* FROM comp_nucleo_VSA,DOMANDE_BANDO_VSA WHERE COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.PROGR_COMPONENTE=0 AND  comp_nucleo_VSA.progr=0 and comp_nucleo_VSA.id_dichiarazione<>" & lIdDichiarazione & " and comp_nucleo_VSA.cod_fiscale='" & CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text & "'"
            Dim myReader22 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader22.Read() Then
                Response.Write("<script>alert('Attenzione...Il codice fiscale del dichiarante risulta essere intestatario di precedente domanda!! Memorizzazione non effettuata.')</script>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "ImmMaschere\blu_stampa_no.jpg"
                Session.Item("STAMPATO") = "1"
                Exit Try
            End If

            par.cmd.CommandText = "SELECT comp_nucleo_VSA.* FROM comp_nucleo_VSA,DOMANDE_BANDO_VSA WHERE COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND comp_nucleo_VSA.id_dichiarazione<>" & lIdDichiarazione & " and comp_nucleo_VSA.cod_fiscale='" & CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text & "'"
            Dim myReader23 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader23.Read()

                par.cmd.CommandText = "SELECT comp_nucleo_VSA.* FROM comp_nucleo_VSA,DOMANDE_BANDO_VSA WHERE DOMANDE_BANDO_VSA.PROGR_COMPONENTE=COMP_NUCLEO_VSA.PROGR AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE=" & myReader23("ID_DICHIARAZIONE")
                Dim myReader24 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader24.Read Then
                    For i = 0 To CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items.Count - 1
                        If par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) = myReader24("COD_FISCALE") Then
                            Response.Write("<script>alert('Attenzione...Il codice fiscale del dichiarante è presente nel nucleo di precedente domanda e intestatario di questa, è presente in questo nucleo!! Memorizzazione non effettuata.')</script>")
                            imgStampa.Enabled = False
                            imgStampa.ImageUrl = "ImmMaschere\blu_stampa_no.jpg"
                            Session.Item("STAMPATO") = "1"
                            Exit Try
                        End If
                    Next


                End If
            End While

            Dim COD_PARENTE As Long
            Dim INDENNITA As String

            COD_PARENTE = 1
            INDENNITA = ""

            par.cmd.CommandText = "SELECT COD FROM T_TIPO_PARENTELA WHERE DESCRIZIONE='" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(0).Text, 81, 25) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                COD_PARENTE = myReader("COD")
            End If

            If Dic_Nucleo1.ProgrDaCancellare <> "" Then
                sStringaSql = "DELETE FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & Dic_Nucleo1.ProgrDaCancellare & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            End If
            Dic_Nucleo1.ProgrDaCancellare = ""

            If par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(0).Text, 120, 2) = "SI" Then
                INDENNITA = 1
            Else
                INDENNITA = 0
            End If

            If INDENNITA = 1 Then
                N_INV_100_ACC = N_INV_100_ACC + 1
            Else
                If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(0).Text, 107, 6)) = 100 Then
                    N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
                Else
                    If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(0).Text, 107, 6)) > 66 And Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(0).Text, 107, 6)) < 100 Then
                        N_INV_100_66 = N_INV_100_66 + 1
                    End If
                End If
            End If

            For i = 0 To 14
                idComponenti(i) = -1
            Next

            NUM_PARENTI = 0
            For i = 0 To CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items.Count - 1
                par.cmd.CommandText = "SELECT COD FROM T_TIPO_PARENTELA WHERE DESCRIZIONE='" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 81, 25) & "'"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    COD_PARENTE = myReader("COD")
                End If
                If par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 120, 2) = "SI" Then
                    INDENNITA = 1
                Else
                    INDENNITA = 0
                End If

                par.cmd.CommandText = "SELECT ID FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND COD_FISCALE='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "'"
                Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader4.Read = False Then
                    sStringaSql = "INSERT INTO COMP_NUCLEO_VSA (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL,DATA_NASCITA,USL,INDENNITA_ACC,SESSO" _
                                & ") VALUES (SEQ_COMP_NUCLEO_VSA.NEXTVAL," & lIdDichiarazione & "," & i & ",'" _
                                & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 25)) & "','" _
                                & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 25)) & "'," _
                                & COD_PARENTE & ",'" _
                                & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "','" _
                                & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6)) & "','" _
                                & par.PulisciStrSql(par.AggiustaData(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 53, 10))) & "','" _
                                & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 114, 5)) & "','" _
                                & INDENNITA & "','" & par.RicavaSesso(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "')"
                    par.cmd.CommandText = sStringaSql
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_VSA.CURRVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idComponenti(i) = myReader(0)
                    End If
                    myReader.Close()
                Else
                    idComponenti(i) = myReader4(0)
                    sStringaSql = "UPDATE COMP_NUCLEO_VSA set PROGR=" & i & ",COGNOME='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 25)) & "'," _
                                & "NOME='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 25)) & "'," _
                                & "GRADO_PARENTELA=" & COD_PARENTE & "," _
                                & "COD_FISCALE='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "'," _
                                & "PERC_INVAL='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6)) & "'," _
                                & "DATA_NASCITA='" & par.PulisciStrSql(par.AggiustaData(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 53, 10))) & "'," _
                                & "USL='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 114, 5)) & "'," _
                                & "INDENNITA_ACC='" & INDENNITA & "'," _
                                & "SESSO='" & par.RicavaSesso(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "' " _
                                & "WHERE ID=" & idComponenti(i)
                    par.cmd.CommandText = sStringaSql
                    par.cmd.ExecuteNonQuery()
                End If

                myReader4.Close()

                par.cmd.CommandText = "DELETE FROM COMP_ELENCO_SPESE_VSA WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_PATR_MOB_VSA WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_PATR_IMMOB_VSA WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_DETRAZIONI_VSA WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM DOMANDE_REDDITI_VSA WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                'If Dic_Patrimonio1.idCOMPMOB <> "" Then
                '    sStringaSql = "DELETE FROM COMP_PATR_MOB WHERE id_componente=" & idComponenti(i) & " and " & Dic_Patrimonio1.idCOMPMOB & ")"
                '    par.cmd.CommandText = sStringaSql
                '    par.cmd.ExecuteNonQuery()
                'End If
                'Dic_Patrimonio1.idCOMPMOB = ""


                NUM_PARENTI = NUM_PARENTI + 1
                If INDENNITA = 1 Then
                    If i <> 0 Then
                        N_INV_100_ACC = N_INV_100_ACC + 1
                    End If
                Else
                    If i <> 0 Then
                        If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6)) = 100 Then
                            N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
                        Else
                            If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6)) > 66 And Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6)) < 100 Then
                                N_INV_100_66 = N_INV_100_66 + 1
                            End If
                        End If
                    End If
                End If
            Next i

            Dim cat_Imm As String
            Dim AU As String = ""

            If CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = False Then
                cat_Imm = ""
            Else
                cat_Imm = ",ID_TIPO_CAT_AB=" & CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).SelectedItem.Value
            End If

            If CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).Enabled = False Then
                AU = ",POSSESSO_UI=NULL"
            Else
                AU = ",POSSESSO_UI=" & CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).SelectedItem.Value
            End If

            'FL_UBICAZIONE='" & Valore01(CType(Dic_Patrimonio1.FindControl("CHUBICAZIONE"), CheckBox).Checked) _

            sStringaSql = ""
            sStringaSql = "UPDATE DICHIARAZIONI_VSA SET " _
                      & "PG='" & lblPG.Text & "'" & AU _
                      & ",FL_UBICAZIONE='" & Valore01(CType(Dic_Patrimonio1.FindControl("CHUBICAZIONE"), CheckBox).Checked) _
                      & "', DATA_PG='" _
                      & par.AggiustaData(txtDataPG.Text) & "',LUOGO='Milano',DATA='" _
                      & par.AggiustaData(CType(Dic_Dichiarazione1.FindControl("txtData1"), TextBox).Text) _
                      & "',NOTE='" & par.PulisciStrSql(CType(Dic_Note1.FindControl("txtNote"), TextBox).Text) _
                      & "',ID_STATO=" & cmbStato.SelectedItem.Value _
                      & ",N_COMP_NUCLEO=" & NUM_PARENTI & ",N_INV_100_CON=" & N_INV_100_ACC _
                      & ",N_INV_100_SENZA=" & N_INV_100_NO_ACC _
                      & ",N_INV_100_66=" & N_INV_100_66 _
                      & cat_Imm & ",ANNO_SIT_ECONOMICA=" & Val(CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text) _
                      & ",LUOGO_S='Milano',DATA_S='" & par.AggiustaData(CType(Dic_Sottoscrittore1.FindControl("txtData1"), TextBox).Text) _
                      & "',LUOGO_INT_ERP='Milano',DATA_INT_ERP='" & par.AggiustaData(CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text) _
                      & "',FL_GIA_TITOLARE='" & Valore01(CType(Dic_Dichiarazione1.FindControl("chTitolare"), CheckBox).Checked) & "' " _
                      & ",MINORI_CARICO='" & Val(CType(Dic_Reddito_Conv1.FindControl("txtMinori"), TextBox).Text) & "' "

            If CType(Dic_Dichiarazione1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text = "ITALIA" Then
                sStringaSql = sStringaSql & ",ID_LUOGO_RES_DNTE=" & CType(Dic_Dichiarazione1.FindControl("cmbComuneRes"), DropDownList).SelectedItem.Value _
                              & ",ID_TIPO_IND_RES_DNTE=" & CType(Dic_Dichiarazione1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value _
                              & ",IND_RES_DNTE='" & par.PulisciStrSql(CType(Dic_Dichiarazione1.FindControl("txtIndRes"), TextBox).Text) _
                              & "',CIVICO_RES_DNTE='" & par.PulisciStrSql(CType(Dic_Dichiarazione1.FindControl("txtCivicoRes"), TextBox).Text) _
                              & "',TELEFONO_DNTE='" & par.PulisciStrSql(CType(Dic_Dichiarazione1.FindControl("txtTelRes"), TextBox).Text) & "' "
            Else
                sStringaSql = sStringaSql & ",ID_LUOGO_RES_DNTE=" & CType(Dic_Dichiarazione1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Value _
                              & ",ID_TIPO_IND_RES_DNTE=" & CType(Dic_Dichiarazione1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value _
                              & ",IND_RES_DNTE='" & par.PulisciStrSql(CType(Dic_Dichiarazione1.FindControl("txtIndRes"), TextBox).Text) _
                              & "',CIVICO_RES_DNTE='" & par.PulisciStrSql(CType(Dic_Dichiarazione1.FindControl("txtCivicoRes"), TextBox).Text) _
                              & "',TELEFONO_DNTE='" & par.PulisciStrSql(CType(Dic_Dichiarazione1.FindControl("txtTelRes"), TextBox).Text) & "' "
            End If

            If CType(Dic_Dichiarazione1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text = "ITALIA" Then
                sStringaSql = sStringaSql & ",CAP_RES_DNTE='" & par.PulisciStrSql(CType(Dic_Dichiarazione1.FindControl("txtCAPRes"), TextBox).Text) & "',ID_LUOGO_NAS_DNTE=" & CType(Dic_Dichiarazione1.FindControl("cmbComuneNas"), DropDownList).SelectedItem.Value
            Else
                sStringaSql = sStringaSql & ",CAP_RES_DNTE='" & par.PulisciStrSql(CType(Dic_Dichiarazione1.FindControl("txtCAPRes"), TextBox).Text) & "',ID_LUOGO_NAS_DNTE=" & CType(Dic_Dichiarazione1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Value
            End If


            par.cmd.CommandText = sStringaSql & " WHERE ID=" & lIdDichiarazione
            par.cmd.ExecuteNonQuery()

            Dim INDICE As Integer



            For i = 0 To CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items.Count - 1
                If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 54, 6)) > 0 Then

                    For j = 0 To cmbComp.Items.Count - 1
                        If cmbComp.Items(j).Value = CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Value Then
                            INDICE = j
                            Exit For
                        End If
                    Next

                    sStringaSql = "INSERT INTO COMP_ELENCO_SPESE_VSA (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) VALUES (SEQ_COMP_ELENCO_SPESE_VSA.NEXTVAL," & idComponenti(INDICE) & "," _
                               & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 54, 6) & ",'" _
                               & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 66, 17)) & "')"
                    par.cmd.CommandText = sStringaSql
                    par.cmd.ExecuteNonQuery()

                End If
            Next i




            For i = 0 To CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items.Count - 1
                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next

                ' par.cmd.CommandText = "SELECT ID FROM COMP_PATR_MOB WHERE ID_COMPONENTE=" & idComponenti(INDICE)
                ' Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                'If myReader5.Read = False Then
                sStringaSql = "INSERT INTO COMP_PATR_MOB_VSA (ID,ID_COMPONENTE,COD_INTERMEDIARIO,INTERMEDIARIO,IMPORTO) VALUES (SEQ_COMP_PATR_MOB_VSA.NEXTVAL," & idComponenti(INDICE) & "," _
                           & "'" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 27)) & "','" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 55, 16)) & "'," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 72, 8)) & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
                'Else
                'sStringaSql = "UPDATE COMP_PATR_MOB SET COD_INTERMEDIARIO='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 13)) & "'," _
                '            & "INTERMEDIARIO='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 41, 30)) & "',IMPORTO=" & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 72, 8)) _
                '            & " WHERE ID=" & myReader5(0)
                'par.cmd.CommandText = sStringaSql
                'par.cmd.ExecuteNonQuery()
                'End If
            Next i

            Dim ID_TIPO As Integer
            Dim RESIDENZA As String


            For i = 0 To CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items.Count - 1
                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next

                Select Case par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 27, 20)
                    Case "FABBRICATI"
                        ID_TIPO = 0
                    Case "TERRENI AGRICOLI"
                        ID_TIPO = 1
                    Case "TERRENI EDIFICABILI"
                        ID_TIPO = 2
                End Select

                Select Case par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 79, 2)
                    Case "SI"
                        RESIDENZA = "1"
                    Case Else
                        RESIDENZA = "0"
                End Select

                sStringaSql = "INSERT INTO COMP_PATR_IMMOB_VSA (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
                            & " (SEQ_COMP_PATR_IMMOB_VSA.NEXTVAL," & idComponenti(INDICE) & "," & ID_TIPO _
                            & "," & par.VirgoleInPunti(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 48, 6)) _
                            & "," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 55, 8)) _
                            & "," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 67, 8)) _
                            & ",'" & RESIDENZA & "')"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            Next i

            For i = 0 To CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items.Count - 1
                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next
                sStringaSql = "INSERT INTO COMP_REDDITO_VSA (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," _
                           & idComponenti(INDICE) & "," _
                           & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 37, 15) _
                           & "," & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 56, 15) & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()

            Next i

            sStringaSql = "DELETE FROM SOTTOSCRITTORI_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            par.cmd.CommandText = sStringaSql
            par.cmd.ExecuteNonQuery()



            If CType(Dic_Sottoscrittore1.FindControl("txtS"), TextBox).Text = "1" Then
                sStringaSql = "INSERT INTO SOTTOSCRITTORI_VSA (ID_DICHIARAZIONE) VALUES (" & lIdDichiarazione & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()

                If CType(Dic_Sottoscrittore1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text = "ITALIA" Then
                    sStringaSql = "UPDATE SOTTOSCRITTORI_VSA SET " _
                    & "ID_LUOGO_RES=" & CType(Dic_Sottoscrittore1.FindControl("cmbComuneRes"), DropDownList).SelectedItem.Value _
                    & ",ID_TIPO_IND=" & CType(Dic_Sottoscrittore1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value _
                    & ",IND='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtIndRes"), TextBox).Text) _
                    & "',CIVICO='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtCivicoRes"), TextBox).Text) _
                    & "',TELEFONO='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtTelRes"), TextBox).Text) _
                    & "',COGNOME='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtCognome"), TextBox).Text) _
                    & "',NOME='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtNome"), TextBox).Text) _
                    & "',DATA_NAS='" & par.AggiustaData(CType(Dic_Sottoscrittore1.FindControl("txtDataNascita"), TextBox).Text) & "' " _
                    & ",CAP_RES='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtCAPRes"), TextBox).Text) & "' "
                Else
                    sStringaSql = "UPDATE SOTTOSCRITTORI_VSA SET " _
                    & "ID_LUOGO_RES=" & CType(Dic_Sottoscrittore1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Value _
                    & ",ID_TIPO_IND=" & CType(Dic_Sottoscrittore1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value _
                    & ",IND='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtIndRes"), TextBox).Text) _
                    & "',CIVICO='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtCivicoRes"), TextBox).Text) _
                    & "',TELEFONO='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtTelRes"), TextBox).Text) _
                    & "',COGNOME='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtCognome"), TextBox).Text) _
                    & "',NOME='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtNome"), TextBox).Text) _
                    & "',DATA_NAS='" & par.AggiustaData(CType(Dic_Sottoscrittore1.FindControl("txtDataNascita"), TextBox).Text) & "' " _
                    & ",CAP_RES='" & par.PulisciStrSql(CType(Dic_Sottoscrittore1.FindControl("txtCAPRes"), TextBox).Text) & "' "
                End If

                If CType(Dic_Sottoscrittore1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text = "ITALIA" Then
                    sStringaSql = sStringaSql & ",ID_LUOGO_NAS=" & CType(Dic_Sottoscrittore1.FindControl("cmbComuneNas"), DropDownList).SelectedItem.Value
                Else
                    sStringaSql = sStringaSql & ",ID_LUOGO_NAS=" & CType(Dic_Sottoscrittore1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Value
                End If

                par.cmd.CommandText = sStringaSql & " WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
                par.cmd.ExecuteNonQuery()

            End If


            For i = 0 To CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items.Count - 1
                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next

                sStringaSql = "INSERT INTO COMP_ALTRI_REDDITI_VSA (ID,ID_COMPONENTE,IMPORTO) VALUES  " _
                            & " (SEQ_COMP_ALTRI_REDDITI_VSA.NEXTVAL," & idComponenti(INDICE) & "," _
                            & "" & Val(par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 52, 8)) _
                            & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            Next i


            For i = 0 To CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items.Count - 1
                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next

                Select Case par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 32, 35)
                    Case "IRPEF"
                        ID_TIPO = 0
                    Case "Spese Sanitarie"
                        ID_TIPO = 1
                    Case "Ricovero in strut. sociosanitarie"
                        ID_TIPO = 2
                End Select

                sStringaSql = "INSERT INTO COMP_DETRAZIONI_VSA (ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES " _
                            & " (SEQ_COMP_DETRAZIONI_VSA.NEXTVAL," & idComponenti(INDICE) & "," & ID_TIPO _
                            & "," & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 68, 8) _
                            & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            Next i

            For i = 0 To CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items.Count - 1

                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next

                sStringaSql = "INSERT INTO DOMANDE_REDDITI_VSA (ID,ID_DOMANDA,ID_COMPONENTE,CONDIZIONE,PROFESSIONE,DIPENDENTE,PENSIONE,AUTONOMO,NON_IMPONIBILI,occasionali,dom_ag_fab,ONERI) VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & lIdDichiarazione & "," & idComponenti(INDICE) & ",'" _
                           & Val(par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 37, 5)) & "','" _
                           & Val(par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 44, 5)) & "','" _
                           & par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 51, 7) & "','" _
                           & par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 62, 7) & "','" _
                           & par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 73, 7) & "','" _
                           & par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 84, 7) & "','" _
                           & par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 95, 7) & "','" _
                           & par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 106, 7) & "','" _
                           & par.RicavaTesto(CType(Dic_Reddito_Conv1.FindControl("listbox1"), ListBox).Items(i).Text, 117, 7) & "')"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            Next i


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

            par.cmd.CommandText = "SELECT ID,PG FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                sStringaSql = "UPDATE DOMANDE_BANDO_VSA SET FL_RINNOVO='1',ISBAR=0,ISBARC=0,ISBARC_R=0,DISAGIO_F=0,DISAGIO_E=0,DISAGIO_A=0,REDDITO_ISEE=0,ID_STATO='2',fl_completa='1',FL_ESAMINATA='0',FL_ISTRUTTORIA_COMPLETA='0' WHERE ID=" & myReader("ID")
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
                If cmbStato.Text <> "2" Then
                    If Fl_Integrazione = "1" Then
                        Session.Item("CONFERMATO") = "1"
                    End If
                    Label9.Text = "DOMANDA DA RIELABORARE!!"
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "alert('ATTENZIONE, La domanda " & myReader("PG") & " deve essere nuovamente elaborata e stampata!');" _
                                & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript29")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript29", scriptblock)
                    End If
                Else
                    Label9.Text = ""
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "alert('ATTENZIONE, Questa dichiarazione  e la domanda " & myReader("PG") & " saranno cancellate!');" _
                                & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript300")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript300", scriptblock)
                    End If

                End If

            End If

            par.cmd.CommandText = "SELECT * FROM DOMANDE_redditi_VSA WHERE ID_DOMANDA=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            While myReader.Read
                totconv = totconv + CDbl(par.IfNull(myReader("DIPENDENTE"), 0)) + CDbl(par.IfNull(myReader("PENSIONE"), 0)) + CDbl(par.IfNull(myReader("OCCASIONALI"), 0)) + CDbl(par.IfNull(myReader("AUTONOMO"), 0)) + CDbl(par.IfNull(myReader("NON_IMPONIBILI"), 0)) + CDbl(par.IfNull(myReader("DOM_AG_FAB"), 0))
            End While
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & myReader1("ID")
                myReader = par.cmd.ExecuteReader()
                While myReader.Read
                    TotISEE = TotISEE + CDbl(par.IfNull(myReader("REDDITO_IRPEF"), 0)) + CDbl(par.IfNull(myReader("PROV_AGRARI"), 0))
                End While
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE=" & myReader1("ID")
                myReader = par.cmd.ExecuteReader()
                While myReader.Read
                    TotISEE = TotISEE + CDbl(par.IfNull(myReader("IMPORTO"), 0))
                End While
                myReader.Close()

            End While
            myReader1.Close()



            If totconv <> TotISEE Then
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                                    & "alert('ATTENZIONE, ci sono delle incongruenze nella dichiarazione dei redditi convenzionali e Isee. In questi casi il calcolo dell ISEE non è attendibile!');" _
                                    & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript30023")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript30023", scriptblock)
                End If
            End If


            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            myReader = par.cmd.ExecuteReader()
            myReader.Close()

            imgStampa.Enabled = True
            imgStampa.ImageUrl = "../NuoveImm/Img_Stampa.png"
            bMemorizzato = True

            If Session.Item("ANAGRAFE") = "1" Then
                'imgAnagrafe.Attributes.Clear()
                imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('../Anagrafe.aspx?ID=" & par.criptamolto(lIdDichiarazione) & "&T=2','Anagrafe','top=0,left=0,width=600,height=400');")
            End If

        Catch EX As Exception
            Label4.Text = EX.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
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
                        Response.Write("<script>alert('NESSUN BANDO CAMBI IN EMERGENZA APERTO. Non è possibile inserire nuove dichiarazioni!');location.replace('pagina_home.aspx');</script>")

                    Else
                        par.OracleConn.Close()
                        Session.Item("LAVORAZIONE") = "0"
                        Response.Write("<script>alert('Il Bando a cui appartiene questa dichiarazione è CHIUSO. Per apportare modifiche utilizzare le funzioni di INTEGRAZIONE!');</script>")
                    End If
                    Exit Function
                Else
                    sAnnoIsee = myReader(0)
                    sAnnoCanone = myReader(1)
                    CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = sAnnoIsee
                    CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = Val(sAnnoIsee)
                    CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = sAnnoIsee
                    CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = sAnnoIsee

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
                    par.OracleConn.Close()
                    Session.Item("LAVORAZIONE") = "0"
                    Response.Write("<script>alert('NESSUN BANDO CAMBI IN EMERGENZA APERTO. Non è possibile inserire nuove dichiarazioni!');location.replace('pagina_home.aspx');</script>")

                Else
                    Response.Write("<script>alert('Il Bando a cui appartiene questa dichiarazione è CHIUSO. Per apportare modifiche utilizzare le funzioni di INTEGRAZIONE!');</script>")
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



            CType(Dic_Dichiarazione1.FindControl("txtData1"), TextBox).Text = Format(Now, "dd/MM/yyyy")
            CType(Dic_Sottoscrittore1.FindControl("txtData1"), TextBox).Text = Format(Now, "dd/MM/yyyy")
            CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text = Format(Now, "dd/MM/yyyy")

            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbNazioneNas", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbNazioneRes", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbTipoIRes", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")

            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbNazioneNas", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbNazioneRes", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbTipoIRes", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            par.RiempiDList(Dic_Patrimonio1, par.OracleConn, "cmbTipoImm", "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=4415"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then

                CT1 = Dic_Dichiarazione1.FindControl("cmbNazioneNas")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText("ITALIA").Selected = True
                CT1 = Dic_Dichiarazione1.FindControl("cmbPrNas")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                CT1 = Dic_Dichiarazione1.FindControl("cmbComuneNas")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("NOME")).Selected = True

                CT1 = Dic_Dichiarazione1.FindControl("cmbNazioneRes")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText("ITALIA").Selected = True
                CT1 = Dic_Dichiarazione1.FindControl("cmbPrRes")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                CT1 = Dic_Dichiarazione1.FindControl("cmbComuneRes")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("NOME")).Selected = True
                CType(Dic_Dichiarazione1.FindControl("txtCAPRes"), TextBox).Text = par.IfNull(myReader("CAP"), "")


                CT1 = Dic_Sottoscrittore1.FindControl("cmbNazioneNas")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText("ITALIA").Selected = True
                CT1 = Dic_Sottoscrittore1.FindControl("cmbPrNas")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneNas")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("NOME")).Selected = True

                CT1 = Dic_Sottoscrittore1.FindControl("cmbNazioneRes")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText("ITALIA").Selected = True
                CT1 = Dic_Sottoscrittore1.FindControl("cmbPrRes")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                CT1 = Dic_Sottoscrittore1.FindControl("cmbComuneRes")
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("NOME")).Selected = True
                CType(Dic_Sottoscrittore1.FindControl("txtCAPRes"), TextBox).Text = par.IfNull(myReader("CAP"), "")
            End If
            myReader.Close()

            CType(Dic_Dichiarazione1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text = "VIA"
            CType(Dic_Dichiarazione1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value = "6"
            CType(Dic_Dichiarazione1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text = "VIA"
            CType(Dic_Dichiarazione1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Value = "6"

            par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lValoreCorrente = par.IfNull(myReader(0), 0) + 1
            End If
            myReader.Close()
            par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrente & ")"
            par.cmd.ExecuteNonQuery()
            lblPG.Text = Format(lValoreCorrente, "0000000000")




            par.cmd.CommandText = "INSERT INTO Dichiarazioni_VSA (ID,ID_CAF,ID_BANDO) VALUES (SEQ_DICHIARAZIONI_VSA.NEXTVAL," & Session.Item("ID_CAF") & "," & lIndice_Bando & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT SEQ_DICHIARAZIONI_VSA.CURRVAL FROM DUAL"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lIdDichiarazione = myReader(0)
                'par.cmd.CommandText = "INSERT INTO COMP_NUCLEO (ID,ID_DICHIARAZIONE,PROGR) VALUES (SEQ_COMP_NUCLEO.NEXTVAL," & lIdDichiarazione & ",0)"
                'par.cmd.ExecuteNonQuery()
                CType(Dic_Dichiarazione1.FindControl("txtId"), TextBox).Text = lIdDichiarazione
            End If
            myReader.Close()
            lblPG.ToolTip = lIdDichiarazione
            Session.Add("LAVORAZIONE", "1")
            Session.Add("STAMPATO", "0")

            HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        Catch ex1 As Oracle.DataAccess.Client.OracleException
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('Errore : " & ex1.Message & "');" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript40")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript40", scriptblock)
            End If
            Label4.Text = ex1.Message
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
        Catch ex As Exception
            scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('Errore : " & ex.Message & "');" _
                        & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript40")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript40", scriptblock)
            End If
            Label4.Text = ex.Message
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
        Finally

        End Try
    End Function

    Protected Sub imgStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStampa.Click
        Call btnSalva_Click(sender, e)
        If bMemorizzato = True Then
            If cmbStato.SelectedItem.Text <> "COMPLETA" Then
                Response.Write("<script>alert('Attenzione...Lo stato della dichiarazione deve essere COMPLETA!')</script>")
                Exit Sub
            End If
            CalcolaStampa()
            Session.Item("STAMPATO") = "1"
        Else
            imgStampa.Enabled = False
            imgStampa.ImageUrl = "../NuoveImm/Img_No_Stampa.png"
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
            'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            'par.SettaCommand(par)
            'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            '‘‘par.cmd.Transaction = par.myTrans

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            If CType(Dic_Dichiarazione1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text = "ITALIA" Then
                DATI_ANAGRAFICI = "<BR>   COGNOME:   <I>" & CType(Dic_Dichiarazione1.FindControl("txtCognome"), TextBox).Text & "</I>   " _
                                & ", NOME:   <I>" & CType(Dic_Dichiarazione1.FindControl("txtNome"), TextBox).Text & "</I><BR>" _
                                & "NATO A:   <I>" & CType(Dic_Dichiarazione1.FindControl("cmbComuneNas"), DropDownList).SelectedItem.Text & "</I>   , " _
                                & "PROVINCIA:   <I>" & CType(Dic_Dichiarazione1.FindControl("cmbPrNas"), DropDownList).SelectedItem.Text & "</I>" _
                                & "<BR>" _
                                & "DATA DI NASCITA:   <I>" & CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text & "</I>   , " _
                                & "pref. e n. telefonico (facoltativo):   <I>" & CType(Dic_Dichiarazione1.FindControl("txtTelRes"), TextBox).Text & "</I><BR>"

            Else
                DATI_ANAGRAFICI = "<BR>   COGNOME:   <I>" & CType(Dic_Dichiarazione1.FindControl("txtCognome"), TextBox).Text & "</I>   " _
                                & ", NOME:   <I>" & CType(Dic_Dichiarazione1.FindControl("txtNome"), TextBox).Text & "</I><BR>" _
                                & "STATO ESTERO:   <I>" & CType(Dic_Dichiarazione1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text & "</I>" _
                                & "<BR>" _
                                & "DATA DI NASCITA:   <I>" & CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text & "</I>   , " _
                                & "pref. e n. telefonico (facoltativo):   <I>" & CType(Dic_Dichiarazione1.FindControl("txtTelRes"), TextBox).Text & "</I><BR>"


            End If

            If CType(Dic_Dichiarazione1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text = "ITALIA" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "RESIDENTE A:   <I>" & CType(Dic_Dichiarazione1.FindControl("cmbComuneRes"), DropDownList).SelectedItem.Text & "</I>   , " _
                & "PROVINCIA:   <I>" & CType(Dic_Dichiarazione1.FindControl("cmbPrRes"), DropDownList).SelectedItem.Text & "</I><BR>" _
                & "INDIRIZZO:   <I>" & CType(Dic_Dichiarazione1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text & " " & CType(Dic_Dichiarazione1.FindControl("txtIndRes"), TextBox).Text & "</I>   ," _
                & "N. CIVICO:   <I>" & CType(Dic_Dichiarazione1.FindControl("txtCivicoRes"), TextBox).Text & "</I>   , CAP:   <I>" & CType(Dic_Dichiarazione1.FindControl("txtCAPRes"), TextBox).Text & "</I>"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "RESIDENTE A:   <I>" & CType(Dic_Dichiarazione1.FindControl("cmbComuneRes"), DropDownList).SelectedItem.Text & "</I>   , " _
                & "STATO ESTERO:   <I>" & CType(Dic_Dichiarazione1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text & "</I><BR>" _
                & "INDIRIZZO:   <I>" & CType(Dic_Dichiarazione1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text & " " & CType(Dic_Dichiarazione1.FindControl("txtIndRes"), TextBox).Text & "</I>   ," _
                & "N. CIVICO:   <I>" & CType(Dic_Dichiarazione1.FindControl("txtCivicoRes"), TextBox).Text & "</I>   , CAP:   <I>" & CType(Dic_Dichiarazione1.FindControl("txtCAPRes"), TextBox).Text & "</I>"
            End If


            If CType(Dic_Dichiarazione1.FindControl("chTitolare"), CheckBox).Checked = True Then
                GIA_TITOLARI = "ESISTONO"
            Else
                GIA_TITOLARI = "NON ESISTONO"
            End If
            DATI_NUCLEO = ""

            For i = 0 To CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items.Count - 1
                DATI_NUCLEO = DATI_NUCLEO & "<tr>" _
                            & "<td width=5%><small><small>    <center>" & i & "</center></small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 25) & "</I>   </small></small></td>" _
                            & "<td width=20%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 25) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 53, 10) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 81, 25) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 120, 2) & "</I>   </small></small></td>" _
                            & "<td width=15%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 114, 5) & "</I>   </small></small></td>" _
                            & "</tr>"
            Next

            SPESE_SOSTENUTE = ""
            For i = 0 To CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items.Count - 1
                If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 54, 6)) > 0 Then

                    SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                                    & "<td width=50%><small><small><CENTER>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 1, 52) & "</CENTER></small></small></td>" _
                                    & "<td align=right width=50%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 54, 6) & ",00" & "</I></small></small></td>" _
                                    & "</tr>"
                End If
            Next i

            ANNO_SIT_ECONOMICA = CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text

            PATRIMONIO_MOB = ""
            For i = 0 To CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items.Count - 1
                PATRIMONIO_MOB = PATRIMONIO_MOB _
                               & "<tr>" _
                               & "<td width=25%><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 25) & "</I>   </center></small></small></td>" _
                               & "<TD  width=25%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 27) & "</I>   </small></small></td>" _
                               & "<td width=50%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 55, 16) & "</I>   </small></small></td>" _
                               & "<TD  align=right  width=50%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 72, 8) & ",00</I></small></small></td>" _
                               & "</tr>"
            Next i

            PATRIMONIO_IMMOB = ""
            For i = 0 To CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items.Count - 1
                PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                                   & "<tr>" _
                                   & "<td><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 1, 25) & "</I>   </center></small></small></td>" _
                                   & "<td><small><small>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 27, 20) & "</I>   </small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 48, 6) & "</I>   %   </p></small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 55, 8) & ",00</I>   </p></small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 67, 8) & ",00</I>   </p></small></small></td>" _
                                   & "<td><small><small>   <I></I><center>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 79, 2) & "</center><I></I>   </small></small></td>" _
                                   & "</tr>"
            Next i
            If CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = True Then
                CAT_CATASTALE = CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).SelectedItem.Text
            Else
                CAT_CATASTALE = ""
            End If

            REDDITO_NUCLEO = ""
            For i = 0 To CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items.Count - 1
                REDDITO_NUCLEO = REDDITO_NUCLEO _
                & "<tr>" _
                & "<td><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 35) & "</I>   </center></small></small></td>" _
                & "<td><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 37, 15) & ",00</I>   </small></small></p></td>" _
                & "<td><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 56, 15) & ",00</I>   </small></small></p></td>" _
                & "</tr>"
            Next i

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
            SDATA = CType(Dic_Sottoscrittore1.FindControl("txtdata1"), TextBox).Text


            If CType(Dic_Sottoscrittore1.FindControl("txtS"), TextBox).Text = "1" Then
                dichiarante = "<img src=block_checked.gif width=10 height=10 border=1>La presente dichiarazione &egrave; resa dal dichiarante in nome e per conto del richiedente incapace <BR>   <B>DATI ANAGRAFICI DEL DICHIARANTE"
            Else
                dichiarante = " "
            End If

            If CType(Dic_Sottoscrittore1.FindControl("txtS"), TextBox).Text = "1" Then

                If CType(Dic_Sottoscrittore1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text = "ITALIA" Then
                    DATI_DICHIARANTE = "<BR>   COGNOME:   <I>" & CType(Dic_Sottoscrittore1.FindControl("txtCognome"), TextBox).Text & "</I>   " _
                    & ", NOME:   <I>" & CType(Dic_Sottoscrittore1.FindControl("txtNome"), TextBox).Text & "</I><BR>" _
                    & "NATO A:   <I>" & CType(Dic_Sottoscrittore1.FindControl("cmbComuneNas"), DropDownList).SelectedItem.Text & "</I>   , " _
                    & "PROVINCIA:   <I>" & CType(Dic_Sottoscrittore1.FindControl("cmbPrNas"), DropDownList).SelectedItem.Text & "</I>" _
                    & "<BR>" _
                    & "DATA DI NASCITA:   <I>" & CType(Dic_Sottoscrittore1.FindControl("txtDataNascita"), TextBox).Text & "</I>   , " _
                    & "pref. e n. telefonico (facoltativo):   <I>" & CType(Dic_Sottoscrittore1.FindControl("txtTelRes"), TextBox).Text & "</I><BR>"
                Else
                    DATI_DICHIARANTE = "<BR>   COGNOME:   <I>" & CType(Dic_Sottoscrittore1.FindControl("txtCognome"), TextBox).Text & "</I>   " _
                                    & ", NOME:   <I>" & CType(Dic_Sottoscrittore1.FindControl("txtNome"), TextBox).Text & "</I><BR>" _
                                    & "STATO ESTERO:   <I>" & CType(Dic_Sottoscrittore1.FindControl("cmbNazioneNas"), DropDownList).SelectedItem.Text & "</I>" _
                                    & "<BR>" _
                                    & "DATA DI NASCITA:   <I>" & CType(Dic_Sottoscrittore1.FindControl("txtDataNascita"), TextBox).Text & "</I>   , " _
                                    & "pref. e n. telefonico (facoltativo):   <I>" & CType(Dic_Sottoscrittore1.FindControl("txtTelRes"), TextBox).Text & "</I><BR>"

                End If
                If CType(Dic_Sottoscrittore1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text = "ITALIA" Then
                    DATI_DICHIARANTE = DATI_DICHIARANTE & "RESIDENTE A:   <I>" & CType(Dic_Sottoscrittore1.FindControl("cmbComuneRes"), DropDownList).SelectedItem.Text & "</I>   , " _
                    & "PROVINCIA:   <I>" & CType(Dic_Sottoscrittore1.FindControl("cmbPrRes"), DropDownList).SelectedItem.Text & "</I><BR>" _
                    & "INDIRIZZO:   <I>" & CType(Dic_Sottoscrittore1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text & " " & CType(Dic_Sottoscrittore1.FindControl("txtIndRes"), TextBox).Text & "</I>   ," _
                    & "N. CIVICO:   <I>" & CType(Dic_Sottoscrittore1.FindControl("txtCivicoRes"), TextBox).Text & "</I>   , CAP:   <I>" & CType(Dic_Sottoscrittore1.FindControl("txtCAPRes"), TextBox).Text & "</I>"
                Else
                    DATI_DICHIARANTE = DATI_DICHIARANTE & ", " _
                    & "RESIDENTE IN:   <I>" & CType(Dic_Sottoscrittore1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text & "</I><BR>" _
                    & "INDIRIZZO:   <I>" & CType(Dic_Sottoscrittore1.FindControl("cmbTipoIRes"), DropDownList).SelectedItem.Text & " " & CType(Dic_Sottoscrittore1.FindControl("txtIndRes"), TextBox).Text & "</I>   ," _
                    & "N. CIVICO:   <I>" & CType(Dic_Sottoscrittore1.FindControl("txtCivicoRes"), TextBox).Text & "</I>   , CAP:   <I>" & CType(Dic_Sottoscrittore1.FindControl("txtCAPRes"), TextBox).Text & "</I>"
                End If
            Else
                DATI_DICHIARANTE = "<BR></BR>"
            End If

            REDDITO_IRPEF = ""
            For i = 0 To CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items.Count - 1
                REDDITO_IRPEF = REDDITO_IRPEF _
                & "<tr>" _
                & "<td width=40%><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 50) & "</I>   </center></small></small></td>" _
                & "<TD  width=505%><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 52, 8) & ",00</I>   </p></small></small></td>" _
                & "</tr>"
            Next i

            REDDITO_DETRAZIONI = ""
            For i = 0 To CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items.Count - 1
                REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                & "<tr>" _
                & "<td width=25%><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 1, 30) & "</I>   </center></small></small></td>" _
                & "<TD  width=25%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 32, 35) & "</I>   </center></small></small></td>" _
                & "<TD  width=25%><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 68, 8) & ",00</I>   </p></small></small></td>" _
                & "</tr>"
            Next i

            LUOGO_REDDITO = "Milano"

            DATA_REDDITO = CType(Dic_Integrazione1.FindControl("txtdata1"), TextBox).Text

            numero = lblSPG.Text & "-" & lblPG.Text & "-" & Label7.Text & " del " & Format(Now, "dd/MM/yyyy")


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
            sStringasql = sStringasql & "<center><table cellspacing=0 border=1 width=95%><tr><td><small><br>   <B>ANNO DI RIFERIMENTO DELLA SITUAZIONE ECONOMICA</B>   :   <I>" & ANNO_SIT_ECONOMICA & "</I>   </small><br><br></td></tr></table></center><br><center><table cellspacing=0 border=1 width=95%><tr><td><small><BR>"
            sStringasql = sStringasql & "<B>QUADRO C: SITUAZIONE PATRIMONIALE DEL NUCLEO FAMILIARE</B>   <B><UL><BR>CONSISTENZA DEL PATRIMONIO MOBILIARE</B>   <BR>posseduto alla data del 31 dicembre " & ANNO_SIT_ECONOMICA & "&nbsp;<BR>   <BR>   DATI SUI SOGGETTI CHE GESTISCONO IL PATRIMONIO MOBILIARE</UL>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td></tr>   <tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>CODICE INTERMEDIARIO O GESTORE</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>INTERMEDIARIO O GESTORE (indicare se &egrave;  Banca"
            sStringasql = sStringasql & " Posta"
            sStringasql = sStringasql & " SIM"
            sStringasql = sStringasql & " SGR"
            sStringasql = sStringasql & " Impresa di investimento comunitaria o extracomunitaria"
            sStringasql = sStringasql & " Agente di cambio"
            sStringasql = sStringasql & " ecc.)</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>VALORE INVESTIMENTO</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & PATRIMONIO_MOB & "<BR>"
            sStringasql = sStringasql & "</table></center>   <B><BR><UL>CONSISTENZA DEL PATRIMONIO IMMOBILIARE</B>   <BR>posseduto alla data del 31"
            sStringasql = sStringasql & " Dicembre " & ANNO_SIT_ECONOMICA
            sStringasql = sStringasql & " <br></UL>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td><td><center>E</center></td><td><center>F</center></td>   <tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>TIPO DI PATRIMONIO  IMMOBILIARE</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>QUOTA POSSEDUTA (percentuale)</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>VALORE AI FINI ICI (valore della quota posseduta dell'immobile"
            sStringasql = sStringasql & " come definita ai fini ICI)</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>QUOTA CAPITALE RESIDUA DEL MUTUO (valore della quota posseduta)   </small></small></center></td><td  width=10% bgcolor=#C0C0C0><center><small><small>AD USO ABITATIVO DEL NUCLEO</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & PATRIMONIO_IMMOB & "<BR>"
            sStringasql = sStringasql & "</table></center><br><ul>   </ul><NOBR></NOBR><center><table cellspacing=0 border=0  width=90%><tr><td width=80%><p align=right><small>   Categoria catastale dell'immobile ad uso abitativo del nucleo   </small></p></td><td width=10% style='border: thin solid rgb(0"
            sStringasql = sStringasql & " 0"
            sStringasql = sStringasql & " 0)'><small><p align=center>   <I>" & CAT_CATASTALE & "</I>   </p></small></td></tr></table></center>   <br><br></small></td></tr></table></center><p style='page-break-before: always'>&nbsp;</p><center><table  cellspacing=0 border=1 width=95%><tr><td><small>   <B>QUADRO D: REDDITO DEI COMPONENTI DEL NUCLEO FAMILIARE</B>   <BR><BR>"
            sStringasql = sStringasql & "<center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>REDDITO COMPLESSIVO DICHIARATO AI FINI IRPEF (1)</small></small><center></td><td bgcolor=#C0C0C0><center><small><small>PROVENTI AGRARI DA DICHIARAZIONE IRAP (per i soli impreditori agricolil)</small></small><center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_NUCLEO & "<BR>"
            sStringasql = sStringasql & "</table></center><br>(1) al netto dei redditi agrari dell'imprenditore agricolo; compresi i redditi da lavoro prestato nelle zone di frontiera"
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
            sStringasql = sStringasql & "<B><BR><UL>ALTRI REDDITI</ul></B>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>IMPORTO REDDITO</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_IRPEF & "</UL></UL><BR>"
            sStringasql = sStringasql & "</table></center>   <B><BR><UL>DETRAZIONI</ul></B>   <NOBR></NOBR><center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>TIPO DETRAZIONE</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>IMPORTO DETRAZIONE</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_DETRAZIONI & "</UL></UL><BR>"
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
            Response.Write("<script>window.open('StampaDichiarazione.aspx','Dichiarazione','');</script>")



            sStringasql = "INSERT INTO EVENTI_DICHIARAZIONI_VSA (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
            & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F132','','I')"

            par.cmd.CommandText = sStringasql
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader.Close()

        Catch ex As Exception
            Label4.Text = "err " & ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        Finally

        End Try
    End Function

End Class
