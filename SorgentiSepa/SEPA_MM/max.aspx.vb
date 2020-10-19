Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class max
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
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsPostBack = False And bEseguito = False Then
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
                imgStampa.ImageUrl = "NuoveImm/Img_No_Stampa.png"
                CType(Dic_Dichiarazione1.FindControl("txtbinserito"), TextBox).Text = "0"
                NuovaDichiarazione()
                CType(Dic_Nucleo1.FindControl("ListBox1"), ListBox).Items.Add("")
            Else
                lNuovaDichiarazione = 0
                imgStampa.ImageUrl = "NuoveImm/Img_No_Stampa.png"
                VisualizzaDichiarazione()
                If Session.Item("ANAGRAFE") = "1" Then
                    imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('Anagrafe.aspx?ID=" & par.CriptaMolto(lIdDichiarazione) & "&T=0','Anagrafe','top=0,left=0,width=600,height=400');")
                End If

            End If
            bEseguito = True
            AggiustaOggetti()
        End If

        If Session.Item("ID_CAF") = "2" Or Session.Item("ID_CAF") = "6" Or Session.Item("ID_CAF") = "61" Then
            imgElencostampe.Attributes.Add("onclick", "javascript:ElencoStampe(" & lIdDichiarazione & "," & lblPG.Text & ");")
        Else
            imgElencostampe.Visible = False
        End If

        H1.Value = H2.Value

        CType(Dic_Dichiarazione1.FindControl("HyperLink1"), HyperLink).Target = "_blank"
        CType(Dic_Dichiarazione1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "Help_Dichiarazione_ERP.pdf"

        CType(Dic_Patrimonio1.FindControl("HyperLink1"), HyperLink).Target = "_blank"
        CType(Dic_Patrimonio1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "Help_Dichiarazione_ERP.pdf"

        CType(Dic_Reddito1.FindControl("HyperLink1"), HyperLink).Target = "_blank"
        CType(Dic_Reddito1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "Help_Dichiarazione_ERP.pdf"

        CType(Dic_Sottoscrittore1.FindControl("HyperLink1"), HyperLink).Target = "_blank"
        CType(Dic_Sottoscrittore1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "Help_Dichiarazione_ERP.pdf"

        CType(Dic_Integrazione1.FindControl("HyperLink1"), HyperLink).Target = "_blank"
        CType(Dic_Integrazione1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "Help_Dichiarazione_ERP.pdf"

        CType(Dic_Nucleo1.FindControl("HyperLink1111"), HyperLink).Target = "_blank"
        CType(Dic_Nucleo1.FindControl("HyperLink1111"), HyperLink).NavigateUrl = "Help_Dichiarazione_ERP.pdf"

        Dim CTRL As Control
        For Each CTRL In Me.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next

        'CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        'CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Attributes.Add("OnChange", "javascript:AttendiCF1();")

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


    Function VisualizzaDichiarazione()
        Dim CT1 As DropDownList
        Dim cT As TextBox
        Dim CTT As Label
        Dim lIndiceAppoggio_0 As Long
        Dim lIndiceAppoggio_1 As Long
        Dim lIndiceAppoggio_2 As Long

        Dim RESIDENZA As String
        Dim SOMMA As Long
        Dim DESCRIZIONE As String = ""
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

            par.cmd.CommandText = "select anno_isee from bandi where stato=1 order by id desc"
            Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader11.Read Then
                AnnoIsee = myReader11("anno_isee")
            End If
            myReader11.Close()
            Label5.Text = "Aggiornare i redditi al " & AnnoIsee & "!!"

            par.cmd.CommandText = "SELECT BANDI.TIPO_BANDO,BANDI.DATA_INIZIO,BANDI.STATO FROM BANDI,DICHIARAZIONI WHERE DICHIARAZIONI.ID=" & lIdDichiarazione & " AND DICHIARAZIONI.ID_BANDO=BANDI.ID"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader("STATO") <> 1 And Fl_Integrazione <> "1" Then
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

            par.cmd.CommandText = "SELECT DICHIARAZIONI.ID_CAF,DOMANDE_BANDO.N_DISTINTA,CAF_WEB.COD_CAF FROM DOMANDE_BANDO,DICHIARAZIONI,CAF_WEB WHERE DICHIARAZIONI.ID_CAF=CAF_WEB.ID AND DICHIARAZIONI.ID=" & lIdDichiarazione & " AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                If Session.Item("ID_CAF") = "6" Then
                    If Session.Item("ID_CAF") = "6" And Session.Item("LIVELLO") <> "1" Then
                        LBLENTE.Visible = True
                        LBLENTE.Text = "Ente Ins.: " & par.IfNull(myReader("COD_CAF"), "")

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
                        cmbStato.Enabled = False
                        If Fl_US <> "1" Then
                            Response.Write("<script>alert('Non è possibile modificare. La domanda è stata scaricata nella distinta N. " & par.IfNull(myReader("N_DISTINTA"), "") & " ! USARE LE FUNZIONI DI INTEGRAZIONE');</script>")
                        End If
                    End If
                End If
            Else
                If Session.Item("ID_CAF") = "6" Then
                    If Session.Item("ID_CAF") = "6" And Session.Item("LIVELLO") <> "1" Then

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
                        cmbStato.Enabled = False
                        If Fl_US <> "1" Then
                            Response.Write("<script>alert('Non è possibile modificare. La domanda NON è stata ancora creata!');</script>")
                        End If

                    End If
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
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI  WHERE SIGLA NOT IN ('I','E','C','00') ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI WHERE SIGLA NOT IN ('I','E','C','00') ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbTipoIRes", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")

            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbNazioneNas", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbNazioneRes", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI  WHERE SIGLA NOT IN ('I','E','C','00') ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI  WHERE SIGLA NOT IN ('I','E','C','00') ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbTipoIRes", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            par.RiempiDList(Dic_Patrimonio1, par.OracleConn, "cmbTipoImm", "select * from T_TIPO_CATEGORIE_IMMOBILE order by descrizione", "DESCRIZIONE", "COD")

            par.cmd.CommandText = "SELECT COUNT(ID_PRATICA) FROM EVENTI_DICHIARAZIONI WHERE COD_EVENTO='F132' AND ID_PRATICA=" & lIdDichiarazione
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
                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            Else
                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI WHERE ID=" & lIdDichiarazione
            End If
            myReader = par.cmd.ExecuteReader()
            lblPG.ToolTip = lIdDichiarazione
            If myReader.Read() Then
                lIndice_Bando = myReader("ID_BANDO")
                lblPG.Text = par.IfNull(myReader("pg"), "")
                txtDataPG.Text = par.FormattaData(par.IfNull(myReader("data_pg"), ""))
                If Session.Item("ID_CAF") = "2" Or Session.Item("ID_CAF") = "6" Or Session.Item("ID_CAF") = "61" Then
                    imgEventi.Attributes.Add("onclick", "javascript:window.open('EventiDichiarazione.aspx?ID=" & lIdDichiarazione & "&PG=" & lblPG.Text & "','Eventi','height=620,top=0,left=0,width=800,scrollbars=yes');return false;")
                Else
                    imgEventi.Visible = False
                End If

                CType(Dic_Dichiarazione1.FindControl("txtId"), TextBox).Text = lIdDichiarazione
                CType(Dic_Dichiarazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data"), ""))
                CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = Val(par.IfNull(myReader("ANNO_SIT_ECONOMICA"), ""))
                CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")

                If Session.Item("ID_CAF") = "2" Or Session.Item("ID_CAF") = "6" Or Session.Item("ID_CAF") = "61" Then
                    CType(Dic_Note1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader("NOTE"), "")
                Else
                    CType(Dic_Note1.FindControl("txtNote"), TextBox).Text = ""
                    CType(Dic_Note1.FindControl("txtNote"), TextBox).Enabled = False
                End If

                CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_INT_ERP"), ""))
                CType(Dic_Sottoscrittore1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_S"), ""))

                CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).SelectedIndex = -1
                CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Items.FindByValue(par.IfNull(myReader("ID_TIPO_CAT_AB"), "0")).Selected = True

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

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblDomAssociata.Text = par.IfNull(myReader("PG"), "")
            End If
            myReader.Close()
            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO where PROGR=0 AND id_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CType(Dic_Dichiarazione1.FindControl("txtCognome"), TextBox).Text = par.IfNull(myReader("COGNOME"), "")
                CType(Dic_Dichiarazione1.FindControl("txtNome"), TextBox).Text = par.IfNull(myReader("NOME"), "")
                CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text = par.IfNull(myReader("COD_FISCALE"), "")
                CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SOTTOSCRITTORI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
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
            Dim INVALIDITA As String = "NO"

            Dim TIPO As String = ""
            Dim SOTTOTIPO As String = ""

            Dim DOCIMPORTO As String = ""
            Dim DOCMUTUO As String = ""

            Dim TIPOREDDITO As String = "--"
            Dim TIPOAGRARI As String = "--"

            txtbinserito.Value = "1"
            par.cmd.CommandText = "SELECT COMP_NUCLEO.*,T_TIPO_PARENTELA.DESCRIZIONE FROM COMP_NUCLEO,T_TIPO_PARENTELA where COMP_NUCLEO.id_DICHIARAZIONE=" & lIdDichiarazione & " AND COMP_NUCLEO.GRADO_PARENTELA=T_TIPO_PARENTELA.COD ORDER BY PROGR ASC"
            myReader = par.cmd.ExecuteReader()
            While myReader.Read
                If par.IfNull(myReader("INDENNITA_ACC"), "0") = "1" Then
                    INDENNITA = "SI"
                Else
                    INDENNITA = "NO"
                End If
                INVALIDITA = "NO"
                If Val(par.IfNull(myReader("PERC_INVAL"), "0")) > 0 Then
                    INVALIDITA = "SI"
                End If


                MIAS = ""
                MIAS = par.MiaFormat(par.IfNull(myReader("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader("USL"), ""), 5) & " " & par.MiaFormat(INVALIDITA, 4) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader("DATA_CERTIFICATO"), "")), 10)
                CType(Dic_Nucleo1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(MIAS, myReader("PROGR")))
                If par.IfNull(myReader("PERC_INVAL"), "0") = "100" And INDENNITA = "SI" Then
                    CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " ", myReader("PROGR")))
                End If
                CType(Dic_Nucleo1.FindControl("txtProgr"), TextBox).Text = myReader("PROGR") + 1

                SOMMA = 0
                DESCRIZIONE = ""
                par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
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
                            CType(Dic_Nucleo1.FindControl("ListBox2"), ListBox).Items(i).Text = par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " " & par.MiaFormat(SOMMA, 6) & ",00   " & par.MiaFormat(DESCRIZIONE, 100)

                        End If
                    Next
                End If



                par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    TIPO = ""
                    SOTTOTIPO = ""

                    Select Case par.IfNull(myReader2("TIPO"), 0)
                        Case 0
                            TIPO = "SALDO AL 31/12"
                        Case 1
                            TIPO = "TITOLI AL 31/12-ASS. VITA"
                        Case 2
                            TIPO = "PARTECIPAZIONI AL 31/12"
                    End Select

                    Select Case par.IfNull(myReader2("SOTTOTIPO"), 0)
                        Case 0
                            SOTTOTIPO = "AUTOCERTIFICATO"
                        Case 1
                            SOTTOTIPO = "SALDO CONTO"
                        Case 2
                            SOTTOTIPO = "SALDO TITOLI"
                        Case 3
                            SOTTOTIPO = "SALDO PREMI VERSATI"
                        Case 4
                            SOTTOTIPO = "DOCUMENTAZIONE CCIAA"
                        Case 5
                            SOTTOTIPO = "730"
                        Case 6
                            SOTTOTIPO = "UNICO"
                        Case 7
                            SOTTOTIPO = "BILANCIO"
                    End Select

                    CType(Dic_Patrimonio1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("COGNOME"), "") & "," & par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(TIPO, 27) & " " & par.MiaFormat(SOTTOTIPO, 25) & " " & par.MiaFormat(myReader2("IMPORTO"), 10) & ",00" & " " & par.MiaFormat(par.IfNull(myReader2("IBAN"), ""), 27) & " " & par.MiaFormat(par.IfNull(myReader2("INTERMEDIARIO"), " "), 50), myReader("PROGR")))
                End While
                myReader2.Close()

                par.cmd.CommandText = "SELECT COMP_PATR_IMMOB.*,T_TIPO_PATR_IMMOB.descrizione FROM T_TIPO_PATR_IMMOB,COMP_PATR_IMMOB WHERE COMP_PATR_IMMOB.ID_COMPONENTE=" & myReader("ID") & " and COMP_PATR_IMMOB.ID_TIPO=T_TIPO_PATR_IMMOB.cod (+) ORDER BY COMP_PATR_IMMOB.ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    RESIDENZA = " "
                    If myReader2("F_RESIDENZA") = "0" Then
                        RESIDENZA = "NO"
                    Else
                        RESIDENZA = "SI"
                    End If

                    Select Case myReader2("TIPO_DOC_VALORE")
                        Case -1
                            DOCIMPORTO = "--"
                        Case 0
                            DOCIMPORTO = "AUTOCERTIFICATO"
                        Case 1
                            DOCIMPORTO = "730"
                        Case 2
                            DOCIMPORTO = "UNICO"
                        Case 3
                            DOCIMPORTO = "ATTO DI ACQUISIZIONE"
                        Case 4
                            DOCIMPORTO = "DICHIARAZIONE ICI"
                        Case 5
                            DOCIMPORTO = "VISURA CATASTO"
                    End Select

                    Select Case myReader2("TIPO_DOC_MUTUO")
                        Case -1
                            DOCMUTUO = "--"
                        Case 0
                            DOCMUTUO = "AUTOCERTIFICATO"
                        Case 1
                            DOCMUTUO = "DOC. ISTITUTO DI CREDITO"
                    End Select

                    CType(Dic_Patrimonio1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 25) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 20) & " " & par.MiaFormat(myReader2("PERC_PATR_IMMOBILIARE"), 6) & " " & par.MiaFormat(DOCIMPORTO, 25) & " " & par.MiaFormat(Val(myReader2("VALORE")), 8) & ",00 " & par.MiaFormat(DOCMUTUO, 25) & " " & par.MiaFormat(Val(myReader2("MUTUO")), 8) & ",00 " & RESIDENZA, myReader("PROGR")))
                End While
                myReader2.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_REDDITO WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read

                    Select Case par.IfNull(myReader2("TIPO_REDDITO"), "-1")
                        Case "-1"
                            TIPOREDDITO = "--"
                        Case "0"
                            TIPOREDDITO = "CUD"
                        Case "1"
                            TIPOREDDITO = "730"
                        Case "2"
                            TIPOREDDITO = "UNICO"
                        Case "3"
                            TIPOREDDITO = "AUTOCERTIFICATO"
                    End Select


                    Select Case par.IfNull(myReader2("TIPO_AGRARI"), "-1")
                        Case "-1"
                            TIPOAGRARI = "--"
                        Case "0"
                            TIPOAGRARI = "AUTOCERTIFICATO"
                        Case "1"
                            TIPOAGRARI = "UNICO"
                    End Select

                    CType(Dic_Reddito1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 35) & " " & par.MiaFormat(TIPOREDDITO, 25) & " " & par.MiaFormat(myReader2("REDDITO_IRPEF"), 8) & ",00" & " " & par.MiaFormat(TIPOAGRARI, 25) & " " & par.MiaFormat(myReader2("PROV_AGRARI"), 8) & ",00", myReader("PROGR")))
                End While
                myReader2.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read

                    TIPO = "--"
                    Select Case par.IfNull(myReader2("TIPO_REDDITO"), 0)
                        Case 1
                            TIPO = "AUTOCERTIFICATO"
                        Case 2
                            TIPO = "EROGAZIONE FSA"
                        Case 3
                            TIPO = "EROGAZIONE SUSSIDI"
                        Case 4
                            TIPO = "PROVVIDENZE INV. CIVILI"
                        Case 5
                            TIPO = "PENS./ASS. SOCIALE"
                        Case 6
                            TIPO = "ALTRO"
                    End Select

                    CType(Dic_Integrazione1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 45) & " " & par.MiaFormat(TIPO, 24) & " " & par.MiaFormat(Val(myReader2("IMPORTO")), 8) & ",00 ", myReader("PROGR")))
                End While
                myReader2.Close()

                Dim TIPO_RILEVAZIONE As String = ""

                par.cmd.CommandText = "SELECT COMP_DETRAZIONI.*,T_TIPO_DETRAZIONI.descrizione FROM T_TIPO_DETRAZIONI,COMP_DETRAZIONI WHERE COMP_DETRAZIONI.ID_COMPONENTE=" & myReader("id") & " and COMP_DETRAZIONI.ID_TIPO=T_TIPO_DETRAZIONI.cod (+) order by comp_detrazioni.id_componente asc"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    Select Case myReader2("TIPO_RILEVAZIONE")
                        Case "-1"
                            TIPO_RILEVAZIONE = "--"
                        Case "0"
                            TIPO_RILEVAZIONE = "CUD"
                        Case "1"
                            TIPO_RILEVAZIONE = "730"
                        Case "2"
                            TIPO_RILEVAZIONE = "UNICO"
                        Case "3"
                            TIPO_RILEVAZIONE = "AUTOCERTIFICATO"
                    End Select
                    CType(Dic_Integrazione1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 30) & " " & par.MiaFormat(TIPO_RILEVAZIONE, 15) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 35) & " " & par.MiaFormat(myReader2("importo"), 8) & ",00 ", myReader("PROGR")))
                End While
                myReader2.Close()

            End While
            myReader.Close()

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
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If
                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI WHERE ID=" & lIdDichiarazione
                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader3.Read() Then
                    lIndice_Bando = myReader3("ID_BANDO")
                    lblPG.Text = par.IfNull(myReader3("pg"), "")
                    txtDataPG.Text = par.FormattaData(par.IfNull(myReader3("data_pg"), ""))
                    If Session.Item("ID_CAF") = "2" Or Session.Item("ID_CAF") = "6" Or Session.Item("ID_CAF") = "61" Then
                        imgEventi.Attributes.Add("onclick", "javascript:window.open('EventiDichiarazione.aspx?ID=" & lIdDichiarazione & "&PG=" & lblPG.Text & "','Eventi','height=620,top=0,left=0,width=800,scrollbars=yes');return false;")
                    Else
                        imgEventi.Visible = False
                    End If
                    CType(Dic_Dichiarazione1.FindControl("txtId"), TextBox).Text = lIdDichiarazione
                    CType(Dic_Dichiarazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("data"), ""))
                    CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = par.IfNull(myReader3("ANNO_SIT_ECONOMICA"), "")
                    CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader3("ANNO_SIT_ECONOMICA"), "")
                    CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = Val(par.IfNull(myReader3("ANNO_SIT_ECONOMICA"), ""))
                    CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader3("ANNO_SIT_ECONOMICA"), "")

                    If Session.Item("ID_CAF") = "2" Or Session.Item("ID_CAF") = "6" Or Session.Item("ID_CAF") = "61" Then
                        CType(Dic_Note1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader3("NOTE"), "")
                    Else
                        CType(Dic_Note1.FindControl("txtNote"), TextBox).Text = ""
                        CType(Dic_Note1.FindControl("txtNote"), TextBox).Enabled = False
                    End If

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

                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO where PROGR=0 AND id_DICHIARAZIONE=" & lIdDichiarazione
                myReader3 = par.cmd.ExecuteReader()
                If myReader3.Read() Then
                    CType(Dic_Dichiarazione1.FindControl("txtCognome"), TextBox).Text = par.IfNull(myReader3("COGNOME"), "")
                    CType(Dic_Dichiarazione1.FindControl("txtNome"), TextBox).Text = par.IfNull(myReader3("NOME"), "")
                    CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text = par.IfNull(myReader3("COD_FISCALE"), "")
                    CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("DATA_NASCITA"), ""))
                End If
                myReader3.Close()

                par.cmd.CommandText = "SELECT * FROM SOTTOSCRITTORI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
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
                par.cmd.CommandText = "SELECT COMP_NUCLEO.*,T_TIPO_PARENTELA.DESCRIZIONE FROM COMP_NUCLEO,T_TIPO_PARENTELA where COMP_NUCLEO.id_DICHIARAZIONE=" & lIdDichiarazione & " AND COMP_NUCLEO.GRADO_PARENTELA=T_TIPO_PARENTELA.COD ORDER BY PROGR ASC"
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
                    par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & myReader3("ID") & " ORDER BY ID_COMPONENTE ASC"
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

                    Dim tipo As String = ""
                    Dim sottotipo As String = ""

                    par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB WHERE ID_COMPONENTE=" & myReader3("ID") & " ORDER BY ID_COMPONENTE ASC"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        tipo = ""
                        sottotipo = ""

                        Select Case par.IfNull(myReader2("TIPO"), 0)
                            Case 0
                                tipo = "SALDO AL 31/12"
                            Case 1
                                tipo = "TITOLI AL 31/12-ASS. VITA"
                            Case 2
                                tipo = "PARTECIPAZIONI AL 31/12"
                        End Select

                        Select Case par.IfNull(myReader2("SOTTOTIPO"), 0)
                            Case 0
                                sottotipo = "AUTOCERTIFICATO"
                            Case 1
                                sottotipo = "SALDO CONTO"
                            Case 2
                                sottotipo = "SALDO TITOLI"
                            Case 3
                                sottotipo = "SALDO PREMI VERSATI"
                            Case 4
                                sottotipo = "DOCUMENTAZIONE CCIAA"
                            Case 5
                                sottotipo = "730"
                            Case 6
                                sottotipo = "UNICO"
                            Case 7
                                sottotipo = "BILANCIO"
                        End Select

                        CType(Dic_Patrimonio1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader3("COGNOME"), "") & "," & par.IfNull(myReader3("NOME"), ""), 25) & " " & par.MiaFormat(tipo, 27) & " " & par.MiaFormat(sottotipo, 25) & " " & par.MiaFormat(myReader2("IMPORTO"), 10) & ",00" & " " & par.MiaFormat(par.IfNull(myReader2("IBAN"), " "), 27) & " " & par.MiaFormat(par.IfNull(myReader2("INTERMEDIARIO"), " "), 50), myReader3("PROGR")))
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT COMP_PATR_IMMOB.*,T_TIPO_PATR_IMMOB.descrizione FROM T_TIPO_PATR_IMMOB,COMP_PATR_IMMOB WHERE COMP_PATR_IMMOB.ID_COMPONENTE=" & myReader3("ID") & " and COMP_PATR_IMMOB.ID_TIPO=T_TIPO_PATR_IMMOB.cod (+) ORDER BY COMP_PATR_IMMOB.ID_COMPONENTE ASC"
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

                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO WHERE ID_COMPONENTE=" & myReader3("ID") & " ORDER BY ID_COMPONENTE ASC"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        CType(Dic_Reddito1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 35) & " " & par.MiaFormat(myReader2("REDDITO_IRPEF"), 15) & ",00 " & par.MiaFormat(myReader2("PROV_AGRARI"), 15) & ",00", myReader3("PROGR")))
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & myReader3("ID") & " ORDER BY ID_COMPONENTE ASC"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        CType(Dic_Integrazione1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 50) & " " & par.MiaFormat(Val(myReader2("IMPORTO")), 8) & ",00 ", myReader3("PROGR")))
                    End While
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT COMP_DETRAZIONI.*,T_TIPO_DETRAZIONI.descrizione FROM T_TIPO_DETRAZIONI,COMP_DETRAZIONI WHERE COMP_DETRAZIONI.ID_COMPONENTE=" & myReader3("id") & " and COMP_DETRAZIONI.ID_TIPO=T_TIPO_DETRAZIONI.cod (+) order by comp_detrazioni.id_componente asc"
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

        Dim INVALIDITA As Integer = 0

        Try

            bMemorizzato = False

            NUM_PARENTI = 0

            N_INV_100_ACC = 0
            N_INV_100_NO_ACC = 0
            N_INV_100_66 = 0



            If VerificaDati(S) = False Then
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "NuoveImm/Img_No_Stampa.png"
                Exit Try
            End If

            If DateDiff("m", DateSerial(Mid(CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text, 7, 4), Mid(CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text, 4, 2), Mid(CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text, 1, 2)), Now) / 12 < 18 Then
                Response.Write("<script>alert('Attenzione...Il richiedente deve essere maggiorenne! Memorizzazione non effettuata.')</script>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "NuoveImm/Img_No_Stampa.png"

                Exit Try
            End If

            If Len(CType(Dic_Dichiarazione1.FindControl("txtCapRes"), TextBox).Text) < 5 Then
                Response.Write("<script>alert('Attenzione...Il CAP di Residenza deve essere 5 caratteri! Memorizzazione non effettuata.')</script>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "NuoveImm/Img_No_Stampa.png"

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

            par.cmd.CommandText = "SELECT comp_nucleo.* FROM comp_nucleo,DOMANDE_BANDO WHERE DOMANDE_BANDO.ID NOT IN (SELECT ID_DOMANDA FROM DOMANDE_ESCLUSIONI WHERE ID_TIPO_ESCLUSIONE=11) AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND DOMANDE_BANDO.PROGR_COMPONENTE=0 AND  comp_nucleo.progr=0 and comp_nucleo.id_dichiarazione<>" & lIdDichiarazione & " and comp_nucleo.cod_fiscale='" & CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text & "'"
            Dim myReader22 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader22.Read() Then
                Response.Write("<script>alert('Attenzione...Il codice fiscale del dichiarante risulta essere intestatario di precedente domanda!! Memorizzazione non effettuata.')</script>")
                imgStampa.Enabled = False
                imgStampa.ImageUrl = "NuoveImm/Img_No_Stampa.png"
                Session.Item("STAMPATO") = "1"
                myReader22.Close()
                Exit Try
            End If
            myReader22.Close()

            par.cmd.CommandText = "SELECT comp_nucleo.* FROM comp_nucleo,DOMANDE_BANDO WHERE DOMANDE_BANDO.ID NOT IN (SELECT ID_DOMANDA FROM DOMANDE_ESCLUSIONI WHERE ID_TIPO_ESCLUSIONE=11)  AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND  comp_nucleo.id_dichiarazione<>" & lIdDichiarazione & " and comp_nucleo.cod_fiscale='" & CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text & "'"
            Dim myReader23 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader23.Read()
                par.cmd.CommandText = "SELECT comp_nucleo.* FROM comp_nucleo,DOMANDE_BANDO WHERE DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND COMP_NUCLEO.ID_DICHIARAZIONE=" & myReader23("ID_DICHIARAZIONE")
                Dim myReader24 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader24.Read Then
                    For i = 0 To CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items.Count - 1
                        If par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) = myReader24("COD_FISCALE") Then
                            Response.Write("<script>alert('Attenzione...Il codice fiscale del dichiarante è presente nel nucleo di precedente domanda e intestatario di questa, è presente in questo nucleo!! Memorizzazione non effettuata.')</script>")
                            imgStampa.Enabled = False
                            imgStampa.ImageUrl = "NuoveImm/Img_No_Stampa.png"
                            Session.Item("STAMPATO") = "1"
                            myReader24.Close()
                            myReader23.Close()
                            Exit Try
                        End If
                    Next
                End If
                myReader24.Close()
            End While
            myReader23.Close()

            Dim COD_PARENTE As Long
            Dim INDENNITA As String

            COD_PARENTE = 1
            INDENNITA = ""

            par.cmd.CommandText = "SELECT COD FROM T_TIPO_PARENTELA WHERE DESCRIZIONE='" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(0).Text, 81, 25) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                COD_PARENTE = myReader("COD")
            End If
            myReader.Close()

            If Dic_Nucleo1.ProgrDaCancellare <> "" Then
                sStringaSql = "DELETE FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & Dic_Nucleo1.ProgrDaCancellare & ")"
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
                myReader.Close()
                If par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 120, 2) = "SI" Then
                    INDENNITA = 1
                Else
                    INDENNITA = 0
                End If

                par.cmd.CommandText = "SELECT ID FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND COD_FISCALE='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "'"
                Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()


                If myReader4.Read = False Then
                    INVALIDITA = 0
                    If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6)) > 0 Then
                        INVALIDITA = 1
                    End If

                    sStringaSql = "INSERT INTO COMP_NUCLEO (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL,DATA_NASCITA,USL,INDENNITA_ACC,SESSO,INVALIDITA,DATA_CERTIFICATO" _
                                & ") VALUES (SEQ_COMP_NUCLEO.NEXTVAL," & lIdDichiarazione & "," & i & ",'" _
                                & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 25)) & "','" _
                                & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 25)) & "'," _
                                & COD_PARENTE & ",'" _
                                & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "','" _
                                & Val(par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6))) & "','" _
                                & par.PulisciStrSql(par.AggiustaData(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 53, 10))) & "','" _
                                & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 114, 5)) & "','" _
                                & INDENNITA & "','" & par.RicavaSesso(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "'," & INVALIDITA & ",'" & par.PulisciStrSql(par.AggiustaData(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 125, 10))) & "')"
                    par.cmd.CommandText = sStringaSql
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO.CURRVAL FROM DUAL"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idComponenti(i) = myReader(0)
                    End If
                    myReader.Close()
                Else
                    idComponenti(i) = myReader4(0)
                    sStringaSql = "UPDATE COMP_NUCLEO set PROGR=" & i & ",COGNOME='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 25)) & "'," _
                                & "NOME='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 25)) & "'," _
                                & "GRADO_PARENTELA=" & COD_PARENTE & "," _
                                & "COD_FISCALE='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "'," _
                                & "PERC_INVAL='" & Val(par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6))) & "'," _
                                & "DATA_NASCITA='" & par.PulisciStrSql(par.AggiustaData(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 53, 10))) & "'," _
                                & "USL='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 114, 5)) & "'," _
                                & "INDENNITA_ACC='" & INDENNITA & "'," _
                                & "SESSO='" & par.RicavaSesso(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "', " _
                                & "INVALIDITA=" & INVALIDITA & ",DATA_CERTIFICATO='" & par.PulisciStrSql(par.AggiustaData(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 125, 10))) & "' " _
                                & "WHERE ID=" & idComponenti(i)
                    par.cmd.CommandText = sStringaSql
                    par.cmd.ExecuteNonQuery()
                End If

                myReader4.Close()

                par.cmd.CommandText = "DELETE FROM COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_PATR_MOB WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_REDDITO WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & idComponenti(i)
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_DETRAZIONI WHERE ID_COMPONENTE=" & idComponenti(i)
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

            If CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = False Then
                cat_Imm = ""
            Else
                cat_Imm = ",ID_TIPO_CAT_AB=" & CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).SelectedItem.Value
            End If

            Dim sNote As String = ""
            If Session.Item("ID_CAF") = "2" Or Session.Item("ID_CAF") = "6" Or Session.Item("ID_CAF") = "61" Then
                sNote = ",NOTE='" & par.PulisciStrSql(CType(Dic_Note1.FindControl("txtNote"), TextBox).Text) & "'"
            End If


            sStringaSql = ""
            sStringaSql = "UPDATE DICHIARAZIONI SET " _
                      & "PG='" & lblPG.Text & "', DATA_PG='" _
                      & par.AggiustaData(txtDataPG.Text) & "',LUOGO='Milano',DATA='" _
                      & par.AggiustaData(CType(Dic_Dichiarazione1.FindControl("txtData1"), TextBox).Text) _
                      & "'" & sNote & ",ID_STATO=" & cmbStato.SelectedItem.Value _
                      & ",N_COMP_NUCLEO=" & NUM_PARENTI & ",N_INV_100_CON=" & N_INV_100_ACC _
                      & ",N_INV_100_SENZA=" & N_INV_100_NO_ACC _
                      & ",N_INV_100_66=" & N_INV_100_66 _
                      & cat_Imm & ",ANNO_SIT_ECONOMICA=" & Val(CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text) _
                      & ",LUOGO_S='Milano',DATA_S='" & par.AggiustaData(CType(Dic_Sottoscrittore1.FindControl("txtData1"), TextBox).Text) _
                      & "',LUOGO_INT_ERP='Milano',DATA_INT_ERP='" & par.AggiustaData(CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text) _
                      & "',FL_GIA_TITOLARE='" & Valore01(CType(Dic_Dichiarazione1.FindControl("chTitolare"), CheckBox).Checked) & "' "

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

            Dim TIPO As Integer = 0
            Dim SOTTOTIPO As Integer = 0

            For i = 0 To CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items.Count - 1
                If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 54, 6)) > 0 Then

                    For j = 0 To cmbComp.Items.Count - 1
                        If cmbComp.Items(j).Value = CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Value Then
                            INDICE = j
                            Exit For
                        End If
                    Next


                    sStringaSql = "INSERT INTO COMP_ELENCO_SPESE (ID,ID_COMPONENTE,IMPORTO,DESCRIZIONE) VALUES (SEQ_COMP_ELENCO_SPESE.NEXTVAL," & idComponenti(INDICE) & "," _
                               & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 54, 6) & ",'" _
                               & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 66, 100)) & "')"
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

                Select Case Trim(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 26, 27))
                    Case "SALDO AL 31/12"
                        TIPO = 0
                    Case "TITOLI AL 31/12-ASS. VITA"
                        TIPO = 1
                    Case "PARTECIPAZIONI AL 31/12"
                        TIPO = 2
                End Select
                Select Case Trim(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 54, 25))
                    Case "AUTOCERTIFICATO"
                        SOTTOTIPO = 0
                    Case "SALDO CONTO"
                        SOTTOTIPO = 1
                    Case "SALDO TITOLI"
                        SOTTOTIPO = 2
                    Case "SALDO PREMI VERSATI"
                        SOTTOTIPO = 3
                    Case "DOCUMENTAZIONE CCIAA"
                        SOTTOTIPO = 4
                    Case "730"
                        SOTTOTIPO = 5
                    Case "UNICO"
                        SOTTOTIPO = 6
                    Case "BILANCIO"
                        SOTTOTIPO = 7
                End Select


                sStringaSql = "INSERT INTO COMP_PATR_MOB (ID,ID_COMPONENTE,TIPO,SOTTOTIPO,IMPORTO,IBAN,INTERMEDIARIO) VALUES (SEQ_COMP_PATR_MOB.NEXTVAL," & idComponenti(INDICE) & "," _
                           & TIPO & "," & SOTTOTIPO & "," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 81, 13)) & ",'" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 95, 27) & "','" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 123, 50) & "')"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()



            Next i

            Dim ID_TIPO As Integer
            Dim RESIDENZA As String
            Dim TIPODOC As Integer = 0
            Dim TIPOMUTUO As Integer = 0


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

                Select Case par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 131, 2)
                    Case "SI"
                        RESIDENZA = "1"
                    Case Else
                        RESIDENZA = "0"
                End Select

                Select Case Trim(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 55, 25))
                    Case "--"
                        TIPODOC = -1
                    Case "AUTOCERTIFICATO"
                        TIPODOC = 0
                    Case "730"
                        TIPODOC = 1
                    Case "UNICO"
                        TIPODOC = 2
                    Case "ATTO DI ACQUISIZIONE"
                        TIPODOC = 3
                    Case "DICHIARAZIONE ICI"
                        TIPODOC = 4
                    Case "VISURA CATASTO"
                        TIPODOC = 5
                End Select

                Select Case Trim(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 93, 25))
                    Case "--"
                        TIPOMUTUO = -1
                    Case "AUTOCERTIFICATO"
                        TIPOMUTUO = 0
                    Case "DOC. ISTITUTO DI CREDITO"
                        TIPOMUTUO = 1
                End Select


                sStringaSql = "INSERT INTO COMP_PATR_IMMOB (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA,TIPO_DOC_VALORE,TIPO_DOC_MUTUO) VALUES " _
                            & " (SEQ_COMP_PATR_IMMOB.NEXTVAL," & idComponenti(INDICE) & "," & ID_TIPO _
                            & "," & par.VirgoleInPunti(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 48, 6)) _
                            & "," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 81, 8)) _
                            & "," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 119, 8)) _
                            & ",'" & RESIDENZA & "'," & TIPODOC & "," & TIPOMUTUO & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            Next i

            Dim TIPOREDDITO As Integer = -1
            Dim TIPOAGRARI As Integer = -1

            For i = 0 To CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items.Count - 1
                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next

                Select Case Trim(par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 37, 25))
                    Case "--"
                        TIPOREDDITO = -1
                    Case "CUD"
                        TIPOREDDITO = 0
                    Case "730"
                        TIPOREDDITO = 1
                    Case "UNICO"
                        TIPOREDDITO = 2
                    Case "AUTOCERTIFICATO"
                        TIPOREDDITO = 3
                End Select


                Select Case Trim(par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 75, 25))
                    Case "--"
                        TIPOAGRARI = -1
                    Case "AUTOCERTIFICATO"
                        TIPOAGRARI = 0
                    Case "UNICO"
                        TIPOAGRARI = 1
                End Select


                sStringaSql = "INSERT INTO COMP_REDDITO (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI,TIPO_REDDITO,TIPO_AGRARI) VALUES (SEQ_COMP_REDDITO.NEXTVAL," _
                           & idComponenti(INDICE) & "," _
                           & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 63, 8) _
                           & "," & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 101, 8) & "," & TIPOREDDITO & "," & TIPOAGRARI & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()

            Next i

            sStringaSql = "DELETE FROM SOTTOSCRITTORI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            par.cmd.CommandText = sStringaSql
            par.cmd.ExecuteNonQuery()



            If CType(Dic_Sottoscrittore1.FindControl("txtS"), TextBox).Text = "1" Then
                sStringaSql = "INSERT INTO SOTTOSCRITTORI (ID_DICHIARAZIONE) VALUES (" & lIdDichiarazione & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()

                If CType(Dic_Sottoscrittore1.FindControl("cmbNazioneRes"), DropDownList).SelectedItem.Text = "ITALIA" Then
                    sStringaSql = "UPDATE SOTTOSCRITTORI SET " _
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
                    sStringaSql = "UPDATE SOTTOSCRITTORI SET " _
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

            Dim TIPOCONT As Integer = -1

            For i = 0 To CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items.Count - 1
                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next


                TIPOCONT = -1
                Select Case Trim(par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 47, 24))
                    Case "AUTOCERTIFICATO"
                        TIPOCONT = 1
                    Case "EROGAZIONE FSA"
                        TIPOCONT = 2
                    Case "EROGAZIONE SUSSIDI"
                        TIPOCONT = 3
                    Case "PROVVIDENZE INV. CIVILI"
                        TIPOCONT = 4
                    Case "PENS./ASS. SOCIALE"
                        TIPOCONT = 5
                    Case "ALTRO"
                        TIPOCONT = 6
                End Select

                sStringaSql = "INSERT INTO COMP_ALTRI_REDDITI (ID,ID_COMPONENTE,IMPORTO,TIPO_REDDITO) VALUES  " _
                            & " (SEQ_COMP_ALTRI_REDDITI.NEXTVAL," & idComponenti(INDICE) & "," _
                            & "" & Val(par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 72, 8)) _
                            & "," & TIPOCONT & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            Next i

            Dim TIPO_RILEVAZIONE As String = "-1"

            For i = 0 To CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items.Count - 1
                For j = 0 To cmbComp.Items.Count - 1
                    If cmbComp.Items(j).Value = CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Value Then
                        INDICE = j
                        Exit For
                    End If
                Next

                'Select Case par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 48, 35)
                '    Case "IRPEF"
                '        ID_TIPO = 0
                '    Case "Spese Sanitarie"
                '        ID_TIPO = 1
                '    Case "Ricovero in strut. sociosanitarie"
                '        ID_TIPO = 2
                'End Select
                ID_TIPO = RicavaIndice(par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 48, 35))

                Select Case Trim(par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 32, 15))
                    Case "--"
                        TIPO_RILEVAZIONE = "-1"
                    Case "CUD"
                        TIPO_RILEVAZIONE = "0"
                    Case "730"
                        TIPO_RILEVAZIONE = "1"
                    Case "UNICO"
                        TIPO_RILEVAZIONE = "2"
                    Case "AUTOCERTIFICATO"
                        TIPO_RILEVAZIONE = "3"
                End Select

                sStringaSql = "INSERT INTO COMP_DETRAZIONI (ID,ID_COMPONENTE,ID_TIPO,IMPORTO,TIPO_RILEVAZIONE) VALUES " _
                            & " (SEQ_COMP_DETRAZIONI.NEXTVAL," & idComponenti(INDICE) & "," & ID_TIPO _
                            & "," & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 84, 8) _
                            & "," & TIPO_RILEVAZIONE & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            Next i



            If lNuovaDichiarazione = 1 Then
                sStringaSql = "INSERT INTO EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F130','','I')"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
                Session.Add("ID_NUOVA_DIC", lblPG.Text)
                lNuovaDichiarazione = 0
            Else
                sStringaSql = "INSERT INTO EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F131','','I')"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            End If

            par.cmd.CommandText = "SELECT ID,PG FROM DOMANDE_BANDO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                sStringaSql = "UPDATE DOMANDE_BANDO SET FL_RINNOVO='1',ISBAR=0,ISBARC=0,ISBARC_R=0,DISAGIO_F=0,DISAGIO_E=0,DISAGIO_A=0,REDDITO_ISEE=0,ID_STATO='2',fl_completa='1',FL_ESAMINATA='0',FL_ISTRUTTORIA_COMPLETA='0' WHERE ID=" & myReader("ID")
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
                If cmbStato.Text <> "2" Then
                    If Fl_Integrazione = "1" Then
                        Session.Item("CONFERMATO") = "1"
                    End If
                    Label9.Text = "LA DOMANDA DEVE ESSERE RIELABORATA!!"
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


            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            myReader = par.cmd.ExecuteReader()
            myReader.Close()

            imgStampa.Enabled = True
            imgStampa.ImageUrl = "NuoveImm/Img_Stampa.png"
            bMemorizzato = True

            txtModificato.Value = "0"

            If Session.Item("ANAGRAFE") = "1" Then
                'imgAnagrafe.Attributes.Clear()
                imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('Anagrafe.aspx?ID=" & par.CriptaMolto(lIdDichiarazione) & "&T=0','Anagrafe','top=0,left=0,width=600,height=400');")
            End If

        Catch EX As Exception
            Label4.Text = EX.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        Finally
        End Try
    End Sub

    Private Function RicavaIndice(ByVal Testo As String) As Integer
        Try
            RicavaIndice = -1
            par.cmd.CommandText = "select cod from t_tipo_detrazioni where upper(descrizione) like '" & UCase(Testo) & "%'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                RicavaIndice = myReader("cod")
            End If
            myReader.Close()

        Catch ex As Exception
            RicavaIndice = -1

        End Try
    End Function


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
            HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

            If Session.Item("ANAGRAFE") = "0" Then
                imgAnagrafe.Visible = False
                imgAnagrafe.Attributes.Clear()
            Else
                imgAnagrafe.Visible = True
            End If

            par.cmd.CommandText = "SELECT ANNO_ISEE,ANNO_CANONE,ID,TIPO_BANDO,DATA_INIZIO FROM BANDI WHERE STATO=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader.HasRows = False Then
                    If lIdDichiarazione = -1 Then
                        myReader.Close()
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Write("<script>alert('NESSUN BANDO APERTO. Non è possibile inserire nuove dichiarazioni!');history.go(-1);</script>")
                    Else
                        myReader.Close()
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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
                    myReader.Close()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Session.Item("LAVORAZIONE") = "0"
                    Response.Write("<script>alert('NESSUN BANDO APERTO. Non è possibile inserire nuove dichiarazioni!');history.go(-1);</script>")
                Else
                    myReader.Close()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI  WHERE SIGLA NOT IN ('I','E','C','00') ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI  WHERE SIGLA NOT IN ('I','E','C','00') ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Dichiarazione1, par.OracleConn, "cmbTipoIRes", "SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")

            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbNazioneNas", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbNazioneRes", "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", "NOME", "ID")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbPrNas", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI  WHERE SIGLA NOT IN ('I','E','C','00') ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
            par.RiempiDList(Dic_Sottoscrittore1, par.OracleConn, "cmbPrRes", "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI  WHERE SIGLA NOT IN ('I','E','C','00') ORDER BY SIGLA ASC", "SIGLA", "SIGLA")
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


            par.cmd.CommandText = "INSERT INTO Dichiarazioni (ID,ID_CAF,ID_BANDO) VALUES (SEQ_DICHIARAZIONI.NEXTVAL," & Session.Item("ID_CAF") & "," & lIndice_Bando & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT SEQ_DICHIARAZIONI.CURRVAL FROM DUAL"
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

        Catch ex1 As Oracle.DataAccess.Client.OracleException
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('Errore : " & ex1.Message & "');" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript40")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript40", scriptblock)
            End If
            Label4.Text = ex1.Message

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
        Catch ex As Exception
            scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('Errore : " & ex.Message & "');" _
                        & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript40")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript40", scriptblock)
            End If
            Label4.Text = ex.Message

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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
            imgStampa.ImageUrl = "NuoveImm\Img_No_Stampa.png"
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
                            & "<td width=5%><font size='3'><font size='3'>    <center>" & i & "</center></font></font></td>" _
                            & "<td width=15%><font size='3'><font size='3'>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16) & "</I>   </font></font></td>" _
                            & "<td width=20%><font size='3'><font size='3'>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 25) & "</I>   </font></font></td>" _
                            & "<td width=20%><font size='3'><font size='3'>   <I>" & par.IfEmpty(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 27, 25), "-") & "</I>   </font></font></td>" _
                            & "<td width=15%><font size='3'><font size='3'>   <I>" & par.IfEmpty(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 53, 10), "-") & "</I>   </font></font></td>" _
                            & "<td width=15%><font size='3'><font size='3'>   <I>" & par.IfEmpty(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 81, 25), "-") & "</I>   </font></font></td>" _
                            & "<td width=15%><font size='3'><font size='3'>   <I>" & par.IfEmpty(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6), "-") & "</I>   </font></font></td>" _
                            & "<td width=15%><font size='3'><font size='3'>   <I>" & par.IfEmpty(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 125, 10), "-") & "</I>   </font></font></td>" _
                            & "<td width=15%><font size='3'><font size='3'>   <I>" & par.IfEmpty(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 120, 2), "-") & "</I>   </font></font></td>" _
                            & "<td width=15%><font size='3'><font size='3'>   <I>" & par.IfEmpty(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 114, 5), "-") & "</I>   </font></font></td>" _
                            & "</tr>"
            Next

            SPESE_SOSTENUTE = ""
            For i = 0 To CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items.Count - 1
                If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 54, 6)) > 0 Then

                    SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                                    & "<td width=50%><font size='3'><font size='3'><CENTER>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 1, 52) & "</CENTER></font></font></td>" _
                                    & "<td align=right width=50%><font size='3'><font size='3'>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 54, 6) & ",00" & "</I></font></font></td>" _
                                    & "</tr>"
                End If
            Next i

            ANNO_SIT_ECONOMICA = CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text

            PATRIMONIO_MOB = ""
            For i = 0 To CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items.Count - 1
                PATRIMONIO_MOB = PATRIMONIO_MOB _
                               & "<tr>" _
                               & "<td width=25%><font size='3'><font size='3'><center>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 25) & "</I>   </center></font></font></td>" _
                               & "<TD  width=25%><font size='3'><font size='3'>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 26, 27) & "</I>   </font></font></td>" _
                               & "<td width=50%><font size='3'><font size='3'>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 54, 25) & "</I>   </font></font></td>" _
                               & "<TD  align=right  width=50%><font size='3'><font size='3'>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 81, 10) & ",00</I></font></font></td>" _
                               & "<TD  align=right  width=50%><font size='3'><font size='3'>   <I>" & par.IfEmpty(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 95, 27), "&nbsp;") & "</I></font></font></td>" _
                               & "<TD  align=right  width=50%><font size='3'><font size='3'>   <I>" & par.IfEmpty(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox1"), ListBox).Items(i).Text, 123, 50), "&nbsp;") & "</I></font></font></td>" _
                               & "</tr>"


            Next i

            PATRIMONIO_IMMOB = ""
            For i = 0 To CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items.Count - 1
                PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                                   & "<tr>" _
                                   & "<td><font size='3'><font size='3'><center>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 1, 25) & "</I>   </center></font></font></td>" _
                                   & "<td><font size='3'><font size='3'><I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 27, 20) & "</I>   </font></font></td>" _
                                   & "<td><font size='3'><font size='3'><p align=right><I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 48, 6) & "</I>%</p></font></font></td>" _
                                   & "<td><font size='3'><font size='3'><I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 55, 25) & "</I>   </font></font></td>" _
                                   & "<td><font size='3'><font size='3'><p align=right><I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 81, 8) & ",00</I></p></font></font></td>" _
                                   & "<td><font size='3'><font size='3'><I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 93, 25) & "</I>   </font></font></td>" _
                                   & "<td><font size='3'><font size='3'><p align=right><I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 119, 8) & ",00</I></p></font></font></td>" _
                                   & "<td><font size='3'><font size='3'>   <I></I><center>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 131, 2) & "</center><I></I>   </font></font></td>" _
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
                & "<td><font size='3'><font size='3'><center>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 35) & "</I>   </center></font></font></td>" _
                & "<td><font size='3'><font size='3'><p align=left>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 37, 25) & "</I>   </font></font></p></td>" _
                & "<td><font size='3'><font size='3'><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 63, 8) & ",00</I>   </font></font></p></td>" _
                & "<td><font size='3'><font size='3'><p align=left>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 75, 25) & "</I>   </font></font></p></td>" _
                & "<td><font size='3'><font size='3'><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 101, 8) & ",00</I>   </font></font></p></td>" _
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
                & "<td width=40%><font size='3'><font size='3'><center>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 45) & "</I>   </center></font></font></td>" _
                 & "<td width=40%><font size='3'><font size='3'><center>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 47, 24) & "</I>   </center></font></font></td>" _
                & "<TD  width=505%><font size='3'><font size='3'><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 72, 8) & ",00</I>   </p></font></font></td>" _
                & "</tr>"
            Next i

            REDDITO_DETRAZIONI = ""
            For i = 0 To CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items.Count - 1
                REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                & "<tr>" _
                & "<td width=25%><font size='3'><font size='3'><center>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 1, 30) & "</I>   </center></font></font></td>" _
                & "<TD  width=25%><font size='3'><font size='3'>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 32, 15) & "</I>   </center></font></font></td>" _
                & "<TD  width=25%><font size='3'><font size='3'>   <I>" & RicavaTestoCompleto(par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 48, 35)) & "</I>   </center></font></font></td>" _
                & "<TD  width=25%><font size='3'><font size='3'><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox2"), ListBox).Items(i).Text, 84, 8) & ",00</I>   </p></font></font></td>" _
                & "</tr>"
            Next i

            LUOGO_REDDITO = "Milano"

            DATA_REDDITO = CType(Dic_Integrazione1.FindControl("txtdata1"), TextBox).Text

            Dim CODICEANAGRAFICO As String = ""
            par.cmd.CommandText = "SELECT operatori.*,caf_web.cod_caf as ENTE from operatori,caf_web where operatori.id_caf=caf_web.id and operatori.ID=" & Session.Item("ID_OPERATORE")
            Dim myReaderENTE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderENTE.Read() Then
                CODICEANAGRAFICO = par.IfNull(myReaderENTE("ENTE"), "") & " - " & par.IfNull(myReaderENTE("COD_ANA"), "")
            End If
            myReaderENTE.Close()
            numero = lblSPG.Text & "-" & lblPG.Text & "-" & Label7.Text & " del " & Format(Now, "dd/MM/yyyy") & " / " & CODICEANAGRAFICO


            sStringasql = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title>Finestra di Stampa</title></head><BODY><UL><UL>   <NOBR></NOBR><basefont SIZE=2></UL></UL>"
            sStringasql = sStringasql & "<p align='center'><b><font size='5'>COMUNE DI MILANO</font></b><P><CENTER><font size='3'>DICHIARAZIONE SOSTITUTIVA DELLE CONDIZIONI ECONOMICHE DEL NUCLEO FAMILIARE PER LA RICHIESTA DI PRESTAZIONI SOCIALI AGEVOLATE</font></CENTER>   <BR>"
            sStringasql = sStringasql & "<CENTER><font size='3'>Io sottoscritto/a"
            sStringasql = sStringasql & "</CENTER>   <NOBR></NOBR>   <CENTER>ai sensi del DPR 28 dicembre 2000"
            sStringasql = sStringasql & " n. 445"
            sStringasql = sStringasql & " dichiaro quanto segue:</font></CENTER><BR>"
            sStringasql = sStringasql & "<center><table border=1 cellspacing=0 width=95%><tr><td><font size='3'>   <B><font size='4'>QUADRO A: DATI ANAGRAFICI DEL RICHIEDENTE</font></B><BR>"
            sStringasql = sStringasql & "<font size='3'>" & DATI_ANAGRAFICI & "</font><br><br></font></td></tr></table></center>"
            sStringasql = sStringasql & "<BR><UL>   </UL><NOBR></NOBR><center>"
            sStringasql = sStringasql & "<table border=1 cellspacing=0 width=95%><tr><td><br><font size='3'><font size='4'>QUADRO B: SOGGETTI COMPONENTI IL NUCLEO FAMILIARE: richiedente"
            sStringasql = sStringasql & " componenti la famiglia anagrafica e altri soggetti considerati a carico ai fini IRPEF</font>"
            sStringasql = sStringasql & "<BR>"
            sStringasql = sStringasql & ""
            sStringasql = sStringasql & ""
            sStringasql = sStringasql & "<center>"
            sStringasql = sStringasql & "</font>"
            sStringasql = sStringasql & "<table border=1 cellspacing=0 width=90%><tr><td>"
            sStringasql = sStringasql & "<p align='center'><font size='3'>A</font></p>"
            sStringasql = sStringasql & "</td><td>"
            sStringasql = sStringasql & "<p align='center'><font size='3'>B</font></p>"
            sStringasql = sStringasql & "</td><td colspan=2>"
            sStringasql = sStringasql & "<p align='center'><font size='3'>C</font></p>"
            sStringasql = sStringasql & "</td><td>"
            sStringasql = sStringasql & "<p align='center'><font size='3'>D</font></p>"
            sStringasql = sStringasql & "</td><td>"
            sStringasql = sStringasql & "<p align='center'><font size='3'>E</font></td><td>"
            sStringasql = sStringasql & "<p align='center'><font size='3'>F</font></td><td>"
            sStringasql = sStringasql & "<p align='center'><font size='3'>G</font></td><td>"
            sStringasql = sStringasql & "<p align='center'><font size='3'>H</font></p>"
            sStringasql = sStringasql & "</td><td><p align='center'><font size='3'>I</font></p></td></tr>   <font size='3'>   <tr><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>N.Progr.</font></font></center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>CODICE FISCALE</font></font><center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>COGNOME</font></font></center></td><td   bgcolor=#C0C0C0><center><font size='3'><font size='3'>NOME</font></font></center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>DATA DI NASCITA</font></font></center></td>"
            sStringasql = sStringasql & "</font>"
            sStringasql = sStringasql & "<td bgcolor=#C0C0C0>"
            sStringasql = sStringasql & "<p align='center'><font size='3'>GR. PARENTELA</font></td><td bgcolor=#C0C0C0>"
            sStringasql = sStringasql & "<p align='center'><font size='3'>&nbsp;% INVALIDITA'</font></td><td bgcolor=#C0C0C0>"
            sStringasql = sStringasql & "<p align='center'><font size='3'>&nbsp;DATA CERT.</font></td><td bgcolor=#C0C0C0>"
            sStringasql = sStringasql & "<p align='center'><font size='3'>INDENNITA' ACC.</font></td>   <td bgcolor=#C0C0C0><font size='3'>ASL&nbsp;</font></td></tr><UL><UL>   <NOBR></NOBR>"
            sStringasql = sStringasql & DATI_NUCLEO
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</table></center>"
            sStringasql = sStringasql & "<BR><UL>   <NOBR></NOBR><font size='3'>   <B>Altre informazioni sul nucleo familiare</B><BR></font>"
            sStringasql = sStringasql & "<p><font size='3'>Nel nucleo famigliare del richiedente <b>" & GIA_TITOLARI & "</b>"
            sStringasql = sStringasql & " titolari di un contratto di assegnazione di alloggio di edilizia residenziale pubblica<BR>"
            sStringasql = sStringasql & "</p>"
            sStringasql = sStringasql & "<table cellspacing=0 border=0 width=90%><tr><td height=18 width=35% ><font size='3'>   - nel nucleo familiare sono presenti n.   </font></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right><font size='3'>   <I>" & N_INV_100_ACC & "</I>   </p></td></tr></table></td><td width=50% ><font size='3'>   componenti con invalidit&agrave; al 100% (con indennit&agrave; di accompagnamento)   </font></td></tr><tr><td><font size='3'><CENTER>Spese effettivamente sostenute distinte per componente</font><table border=1 cellpadding=0 cellspacing=0 width=50%>   <tr><td width=50%><font size='3'><CENTER><b>A</b></CENTER></font size='3'></td><td align=right width=50%><font size='3'><CENTER><b>B</b></CENTER></font></td></tr>   <tr><td bgcolor=#C0C0C0 width=50%><CENTER><font size='3'><font size='3'>Nome</font></font></font></CENTER></td><font size='3'>   <td bgcolor=#C0C0C0 align=right width=50%><font size='3'><font size='3'><CENTER>SPESA</CENTER></font></font></td></tr><UL><UL>   <NOBR></NOBR>" & SPESE_SOSTENUTE & "</UL></UL>   <NOBR></NOBR></table></CENTER></td><td>&nbsp;</td><td>&nbsp;<BR>"
            sStringasql = sStringasql & "</font></td></tr><tr><td height=18 width=30% ><font size='3'>   - nel nucleo sono presenti n.   </font></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right>   <I><font size='3'>   <font size='3'>   " & N_INV_100_NO_ACC & "</font></font></I>   </p></td></tr></table></td><td width=55% ><font size='3'>   componenti con invalidit&agrave; al 100% senza indennit&agrave; di accompagnamento<BR>"
            sStringasql = sStringasql & "</font></td></tr><tr><td height=18 width=30% ><font size='3'>   - nel nucleo sono presenti n.   </font></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right>   <I><font size='3'>   <font size='3'>" & N_INV_100_66 & "</font></font></I>   </p></td></tr></table></td><td width=55% ><font size='3'>   componenti con invalidit&agrave; inferiore al 100% e superiore al 66%<BR>"
            sStringasql = sStringasql & "</font></td></tr></table>"
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</td></tr></table><p style='page-break-before: always'>&nbsp;</p>"
            sStringasql = sStringasql & "<center><table cellspacing=0 border=1 width=95%><tr><td><font size='3'><br>   <B>ANNO DI RIFERIMENTO DELLA SITUAZIONE ECONOMICA</B>   :   <I>" & ANNO_SIT_ECONOMICA & "</I>   </font><br><br></td></tr></table></center><br><center><table cellspacing=0 border=1 width=95%><tr><td><font size='3'><BR>"
            sStringasql = sStringasql & "<B>QUADRO C: SITUAZIONE PATRIMONIALE DEL NUCLEO FAMILIARE</B>   <B><UL><BR>CONSISTENZA DEL PATRIMONIO MOBILIARE</B>   <BR>posseduto alla data del 31 dicembre " & ANNO_SIT_ECONOMICA & "&nbsp;<BR>   <BR>   DATI SUI SOGGETTI CHE GESTISCONO IL PATRIMONIO MOBILIARE</UL>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td><td><center>E</center></td><td><center>F</center></td></tr></tr><tr><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>NOME</font></font></center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>TIPO</font></font></center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>RILEVATO DA</font></font></center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>IMPORTO</font></font></center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>IBAN</font></font></center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>INTERMEDIARIO</font></font></center></td></tr><UL><UL>   <NOBR></NOBR>" & PATRIMONIO_MOB & "<BR>"
            sStringasql = sStringasql & "</table></center>   <B><BR><UL>CONSISTENZA DEL PATRIMONIO IMMOBILIARE</B>   <BR>posseduto alla data del 31"
            sStringasql = sStringasql & " Dicembre " & ANNO_SIT_ECONOMICA
            sStringasql = sStringasql & " <br></UL>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td><td><center>E</center></td><td><center>F</center></td><td><center>G</center></td><td><center>H</center></td>   <tr><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>NOME</font></font></center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>TIPO DI PATRIMONIO  IMMOBILIARE</font></font></center></td><td bgcolor=#C0C0C0>   <center><font size='3'><font size='3'>QUOTA POSSEDUTA (percentuale)</font></font></center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>VALORE ICI RILEVATO DA</font></font></center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>VALORE AI FINI ICI (valore della quota posseduta dell'immobile"
            sStringasql = sStringasql & " come definita ai fini ICI)</font></font></center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>RILEVAZIONE QUOTA MUTUO DA</font></font></center></td>   <td bgcolor=#C0C0C0><center><font size='3'><font size='3'>QUOTA CAPITALE RESIDUA DEL MUTUO (valore della quota posseduta)   </font></font></center></td><td  width=10% bgcolor=#C0C0C0><center><font size='3'><font size='3'>AD USO ABITATIVO DEL NUCLEO</font></font></center></td></tr><UL><UL>   <NOBR></NOBR>" & PATRIMONIO_IMMOB & "<BR>"
            sStringasql = sStringasql & "</table></center><br><ul>   </ul><NOBR></NOBR><center><table cellspacing=0 border=0  width=90%><tr><td width=80%><p align=right><font size='3'>   Categoria catastale dell'immobile ad uso abitativo del nucleo   </font></p></td><td width=10% style='border: thin solid rgb(0"
            sStringasql = sStringasql & " 0"
            sStringasql = sStringasql & " 0)'><font size='3'><p align=center>   <I>" & CAT_CATASTALE & "</I>   </p></font></td></tr></table></center>   <br><br></font></td></tr></table></center><p style='page-break-before: always'>&nbsp;</p><center><table  cellspacing=0 border=1 width=95%><tr><td><font size='3'>   <B>QUADRO D: REDDITO DEI COMPONENTI DEL NUCLEO FAMILIARE</B>   <BR><BR>"
            sStringasql = sStringasql & "<center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td><td><center>E</center></td></tr><tr><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>NOME</font></font></center></td><td bgcolor=#C0C0C0>   <center><font size='3'><font size='3'>TIPOLOGIA REDDITO IRPEF</font></font><center></td><td bgcolor=#C0C0C0>   <center><font size='3'><font size='3'>REDDITO COMPLESSIVO DICHIARATO AI FINI IRPEF (1)</font></font><center></td><td bgcolor=#C0C0C0>   <center><font size='3'><font size='3'>TIPOLOGIA PROVENTI AGRARI</font></font><center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>PROVENTI AGRARI DA DICHIARAZIONE IRAP (per i soli impreditori agricolil)</font></font><center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_NUCLEO & "<BR>"
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
            sStringasql = sStringasql & " nel caso di erogazione di una prestazione sociale agevolata"
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
            sStringasql = sStringasql & "<CENTER><table cellspacing=0 border=0 width=80%> <tr> <td width=33%  height=20><font size='3'><center>   <I>" & LUOGO & "</I>   </center></font></td><td width=33%><font size='3'><center>"
            sStringasql = sStringasql & " li   <I>" & Format(Now, "dd/MM/yyyy") & "</I>   </center></font></td><td width=34%><center>   _______________________________   </center></td></tr><tr><td width=33% height=15><font size='3'><center>(luogo)</center></font></td><td width=33%><font size='3'><center>(data)</center></font></td><td width=34%><font size='3'><center>(firma)</center></font></td></tr></table></CENTER>"
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
            sStringasql = sStringasql & "</font></td></tr></table></center><BR>"
            sStringasql = sStringasql & "</center>"
            sStringasql = sStringasql & "<p><p style='page-break-before: always'>&nbsp;</p>"
            sStringasql = sStringasql & "<BR><center><table  cellspacing=0 border=1 width=95%><tr><td><font size='3'>   <B>INTEGRAZIONE ALLA SITUAZIONE REDDITUALE AI FINI ERP</B><BR>"
            sStringasql = sStringasql & "<B><BR><UL>ALTRI REDDITI</ul></B>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td></tr><tr><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>NOME</font></font></center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>CONTRIBUTI/SUSSIDI</font></font></center></td><td bgcolor=#C0C0C0>   <center><font size='3'><font size='3'>IMPORTO REDDITO</font></font></center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_IRPEF & "</UL></UL><BR>"
            sStringasql = sStringasql & "</table></center>   <B><BR><UL>DETRAZIONI</ul></B>   <NOBR></NOBR><center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td></tr><tr><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>NOME</font></font></center></td><td bgcolor=#C0C0C0>   <center><font size='3'><font size='3'>TIPO</font></font></center></td><td bgcolor=#C0C0C0>   <center><font size='3'><font size='3'>TIPO DETRAZIONE</font></font></center></td><td bgcolor=#C0C0C0><center><font size='3'><font size='3'>IMPORTO DETRAZIONE</font></font></center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_DETRAZIONI & "</UL></UL><BR>"
            sStringasql = sStringasql & "</table></center><BR>"
            sStringasql = sStringasql & "<BR><BR><CENTER><table cellspacing=0 border=0 width=80%> <tr> <td width=33%  height=20><font size='3'><center>   <I>" & LUOGO_REDDITO & "</I>   </center></font></td><td width=33%><font size='3'><center>"
            sStringasql = sStringasql & " li   <I>" & Format(Now, "dd/MM/yyyy") & "</I>   </center></font></td><td width=34%><center>   _______________________________   </center></td></tr><tr><td width=33% height=15><font size='3'><center>(luogo)</center></font></td><td width=33%><font size='3'><center>(data)</center></font></td><td width=34%><font size='3'><center>(firma)</center></font></td></tr></table></CENTER><BR>"
            sStringasql = sStringasql & "</font>"
            sStringasql = sStringasql & "</table>"
            sStringasql = sStringasql & "<p align='left'>"
            sStringasql = sStringasql & " &nbsp;"
            sStringasql = sStringasql & "<p align='left'>"
            sStringasql = sStringasql & " Dic.N. " & numero & "   <p><b><font face='Arial' size='2'>Data Elaborazione: " & Format(Now, "dd/MM/yyyy") & "</font></b></p>"
            sStringasql = sStringasql & "<BR><BR>"
            sStringasql = sStringasql & "</center>"
            sStringasql = sStringasql & "<p align='center'><font face='Arial'></font></p>"
            sStringasql = sStringasql & "</BODY></html>"

            'HttpContext.Current.Session.Add("DICHIARAZIONE", sStringasql)



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
            pdfConverter1.PdfFooterOptions.FooterText = ("Dichiarazione N. " & numero)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "00_" & lIdDichiarazione & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"

            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(sStringasql, url & nomefile, Server.MapPath("IMG\"))
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False

           
            Dim ix As Integer = 0
            For ix = 0 To 1000

            Next


            Response.Write("<script>window.open('ALLEGATI/BANDI_ERP/STAMPE/" & nomefile & "','Dichiarazione','');</script>")
            'Response.Write("<script>window.open('StampaDichiarazione.aspx','Dichiarazione','');</script>")



            sStringasql = "INSERT INTO EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
            & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F132','','I')"

            par.cmd.CommandText = sStringasql
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)






            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
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

    Private Function RicavaTestoCompleto(ByVal Testo As String) As String
        Try
            RicavaTestoCompleto = Testo
            par.cmd.CommandText = "select descrizione from t_tipo_detrazioni where upper(descrizione) = '" & UCase(Testo) & "'"
            Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderX.Read Then
                RicavaTestoCompleto = myReaderX("descrizione")
            End If
            myReaderX.Close()
            If RicavaTestoCompleto = Testo Then
                par.cmd.CommandText = "select descrizione from t_tipo_detrazioni where upper(descrizione) like '" & UCase(Testo) & "%' order by COD asc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    RicavaTestoCompleto = myReader("descrizione")
                End If
                myReader.Close()
            End If

        Catch ex As Exception
            par.OracleConn.Close()

        End Try
    End Function

   

End Class