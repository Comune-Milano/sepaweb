Imports ExpertPdf.HtmlToPdf
Imports System.IO

Partial Class VSA_max
    Inherits PageSetIdMode
    Dim lValoreCorrente As Long
    Dim sAnnoIsee As String
    Dim sAnnoCanone As String
    Dim par As New CM.Global
    Dim bEseguito As Boolean = False
    Dim scriptblock As String
    Dim docMancanti As Boolean = False
    'Dim annoRedd As String


    Function AggiustaOggetti()
        'Response.Write("<script></script>")
    End Function

    Private Sub RiempiCmbAnniRedd(ByVal annoredd As Integer)
        cmbAnnoReddituale.Items.Clear()
        For i As Integer = annoredd To Year(Now)
            cmbAnnoReddituale.Items.Add(New ListItem(i))
        Next
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsPostBack = False And bEseguito = False Then
            txtTab.Value = "1"
            Response.Expires = 0
            txtDataPG.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            cmbAnnoReddituale.Attributes.Add("onclick", "javascript:Uscita=1;")
            H1.Value = "1"

            '25/06/2012 - Stringa di connessione!!
            lIdConnessDICH = Format(Now, "yyyyMMddHHmmss")

            bMemorizzato = False
            lIdDichiarazione = Request.QueryString("ID")
            'id_contr.Value = Request.QueryString("IDcont")
            Fl_Integrazione = Request.QueryString("INT")
            Fl_US = Request.QueryString("US")
            SoloLettura = Request.QueryString("LE")
            'annoRedd = Request.QueryString("ANNI")
            If Fl_Integrazione = "1" Then
                H1.Value = "1"
                H2.Value = "1"
                Image3.Visible = True
                Label5.Visible = True
            End If
            txtTab.Value = "1"
            If Request.QueryString("GLocat") <> "" Then
                MenuStampe.Visible = False
                btnApplica.Visible = False
                IMGCanone.Visible = False
            End If
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
                'imgStampa.ImageUrl = "../NuoveImm/Img_No_Stampa.png"
                imgStampa.Enabled = True
                
                VisualizzaDichiarazione()
                If Session.Item("ANAGRAFE") = "1" Then
                    imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('../Anagrafe.aspx?ID=" & par.criptamolto(lIdDichiarazione) & "&T=4','Anagrafe','top=0,left=0,width=600,height=400');")
                End If
            End If
            bEseguito = True
            AggiustaOggetti()
            Dim CTRL As Control
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                ElseIf TypeOf CTRL Is Button Then
                    DirectCast(CTRL, Button).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")

                End If
            Next


        End If

        If Request.QueryString("CH") = 1 Then
            'Me.imgUscita.Style.Value = "visibility = 'hidden';"
            'senzaEsci.Value = "1"
            Me.imgUscita.Visible = True
        ElseIf Request.QueryString("CH") = 2 Then
            Me.imgUscita.Visible = True
            Me.imgVaiDomanda.Visible = False

        End If

        If par.IfNull(tipoCausale.Value, "") = "30" And par.IfNull(Request.QueryString("CH"), "") = "1" Then
            imgVaiDomanda.Visible = True
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

        H1.Value = "1"
        'H1.Value = H2.Value
        Dic_Dichiarazione1.FindControl("chTitolare").Visible = False


        CType(Dic_Dichiarazione1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "VSA/Help_Dichiarazione.htm"
        CType(Dic_Patrimonio1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "VSA/Help_Dichiarazione.htm"
        CType(Dic_Reddito1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "VSA/Help_Dichiarazione.htm"
        CType(Dic_Sottoscrittore1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "VSA/Help_Dichiarazione.htm"
        CType(Dic_Integrazione1.FindControl("HyperLink1"), HyperLink).NavigateUrl = "VSA/Help_Dichiarazione.htm"


    End Sub

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

    Public Property lIdConnessDICH() As String
        Get
            If Not (ViewState("par_lIdConnessDICH") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessDICH"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessDICH") = value
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

#Region "MENU ITEMS"
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
            Response.Write("<script>document.location.href=""../ErrorPage.aspx""</script>")
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


            If CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 1 Then
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

            If CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 1 Then
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

        If CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 1 Then
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

    Private Sub MenuCambioCons()
        Dim item As MenuItem

        If modPresent = "0" Then
            item = New MenuItem("Richiesta Cambio Consensuale", "RichCAMB", "", "javascript:RichCAMB();")
            MenuStampe.Items(0).ChildItems.Add(item)
        End If

        item = New MenuItem("Dichiarazione Permanenza Requisiti ERP", "DichPermanenza", "", "javascript:DichPermanenzaReq();")
        MenuStampe.Items(0).ChildItems.Add(item)


        If CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 1 Then
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

#End Region
    


    Private Sub VisualizzaDichiarazione()
        Dim CT1 As DropDownList
        Dim cT As TextBox
        Dim CTT As Label
        Dim lIndiceAppoggio_0 As Long
        Dim lIndiceAppoggio_1 As Long
        Dim lIndiceAppoggio_2 As Long

        Dim RESIDENZA As String = ""
        Dim SOMMA As Long
        Dim DESCRIZIONE As String = ""
        Dim i As Integer
        Dim MIOPROGR As Integer
        Dim scriptblock As String = ""

        Dim presDocManca As Integer
        Dim dataPresDocM As String = ""
        Dim fl_autorizza As Integer = 0



        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

            '25/06/2012 - Stringa di connessione in sessione
            HttpContext.Current.Session.Add(lIdConnessDICH, par.OracleConn)
            Session.Add("lIdConnessDICH", lIdConnessDICH)


            If Session.Item("ANAGRAFE") = "0" Then
                imgAnagrafe.Visible = False
                imgAnagrafe.Attributes.Clear()
            Else
                imgAnagrafe.Visible = True
            End If

            'If Not IsNothing(annoRedd) Then
            '    cmbAnnoReddituale.Items.Clear()
            '    If annoRedd.Length = 8 Then
            '        cmbAnnoReddituale.Items.Add(New ListItem(Right(annoRedd, 4)))
            '        cmbAnnoReddituale.Items.Add(New ListItem(Left(annoRedd, 4)))
            '    ElseIf annoRedd.Length > 8 Then
            '        cmbAnnoReddituale.Items.Add(New ListItem(Right(annoRedd, 4)))
            '        cmbAnnoReddituale.Items.Add(New ListItem(Mid(annoRedd, 5, 4)))
            '        cmbAnnoReddituale.Items.Add(New ListItem(Left(annoRedd, 4)))
            '    Else
            '        cmbAnnoReddituale.Items.Add(New ListItem(annoRedd))
            '    End If
            'End If


            par.cmd.CommandText = "SELECT BANDI_VSA.TIPO_BANDO,BANDI_VSA.DATA_INIZIO,BANDI_VSA.STATO FROM BANDI_VSA,DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID=" & lIdDichiarazione & " AND DICHIARAZIONI_VSA.ID_BANDO=BANDI_VSA.ID"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then




                If myReader("STATO") <> 1 And Fl_Integrazione <> "1" And Session.Item("ID_CAF") <> "6" Then
                    btnSalva.Visible = False
                    cmbAnnoReddituale.Enabled = False
                    'Label5.Visible = False
                    imgStampa.Visible = False
                    btnApplica.Visible = False
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
                    'Select Case lBando
                    '    Case 0
                    '        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-1"
                    '    Case 1
                    '        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-2"
                    '    Case 2
                    '        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-3"
                    'End Select

                End If
            End If
            myReader.Close()

            If SoloLettura = "1" Then
                btnSalva.Visible = False
                'Label5.Visible = False
                imgStampa.Visible = False
                btnApplica.Visible = False
                'Label6.Visible = False

                Dic_Dichiarazione1.DisattivaTutto()
                Dic_Integrazione1.DisattivaTutto()
                Dic_Note1.DisattivaTutto()
                Dic_Nucleo1.DisattivaTutto()
                Dic_Patrimonio1.DisattivaTutto()
                Dic_Reddito1.DisattivaTutto()

                Dic_Reddito_Conv1.DisattivaTutto()
                ChFO.Enabled = False
                cmbStato.Enabled = False

                cmbAnnoReddituale.Enabled = False

            End If


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_ASSEGNATE WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND PROVENIENZA='Z' AND GENERATO_CONTRATTO=1"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                btnSalva.Visible = False
                'Label5.Visible = False
                imgStampa.Visible = False
                btnApplica.Visible = False
                'Label6.Visible = False

                Dic_Dichiarazione1.DisattivaTutto()
                Dic_Integrazione1.DisattivaTutto()
                Dic_Note1.DisattivaTutto()
                Dic_Nucleo1.DisattivaTutto()
                Dic_Patrimonio1.DisattivaTutto()
                Dic_Reddito1.DisattivaTutto()

                Dic_Reddito_Conv1.DisattivaTutto()
                ChFO.Enabled = False
                cmbStato.Enabled = False
                cmbAnnoReddituale.Enabled = False

                Response.Write("<script>alert('Non è possibile apportare modifiche! Il contratto è stato già inserito per questa dichiarazione. Se il contratto è ancora in BOZZA, eliminarlo e poi effettuare le modifiche sulla dichiarazione!');</script>")
            End If
            myReader.Close()

            Dim statto_domanda As String = ""


            '*********peppe modify 20/09/2011
            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID,DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE,T_CAUSALI_DOMANDA_VSA.COD AS IDCAUS,DICHIARAZIONI_VSA.MOD_PRESENTAZIONE,DICHIARAZIONI_VSA.ID_CAF,DOMANDE_BANDO_VSA.N_DISTINTA,CAF_WEB.COD_CAF,DOMANDE_BANDO_VSA.ID_STATO,DOMANDE_BANDO_VSA.FL_PRESENT_DOC_MANC,DOMANDE_BANDO_VSA.DATA_PRESENT_DOC_MANC,DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE,DICHIARAZIONI_VSA.PG_COLLEGATO FROM DOMANDE_BANDO_VSA,DICHIARAZIONI_VSA,CAF_WEB,T_CAUSALI_DOMANDA_VSA WHERE DICHIARAZIONI_VSA.ID_CAF=CAF_WEB.ID AND DICHIARAZIONI_VSA.ID=" & lIdDichiarazione & " AND DOMANDE_BANDO_VSA.ID_DICHIARAZIONE=DICHIARAZIONI_VSA.ID and DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA=T_CAUSALI_DOMANDA_VSA.COD(+)"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
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
                    'Me.MenuStampe.Visible = False
                End If

                LBLENTE.Visible = True
                LBLENTE.Text = "Ente Ins.: " & par.IfNull(myReader("COD_CAF"), "")

                statto_domanda = par.IfNull(myReader("ID_STATO"), -1)
                If par.IfNull(myReader("ID_STATO"), -1) = "9" Or par.IfNull(myReader("ID_STATO"), -1) = "10" Then
                    btnSalva.Visible = False
                    imgStampa.Visible = False
                    btnApplica.Visible = False
                    Dic_Dichiarazione1.DisattivaTutto()
                    Dic_Integrazione1.DisattivaTutto()
                    Dic_Note1.DisattivaTutto()
                    Dic_Nucleo1.DisattivaTutto()
                    Dic_Patrimonio1.DisattivaTutto()
                    Dic_Reddito1.DisattivaTutto()
                    Dic_Reddito_Conv1.DisattivaTutto()
                    cmbStato.Enabled = False

                    cmbAnnoReddituale.Enabled = False

                    Response.Write("<script>alert('Non è possibile modificare. La domanda è IN ASSEGNAZIONE!');</script>")
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



                'If par.IfNull(myReader("N_DISTINTA"), -1) = 0 And myReader("ID_CAF") <> Session.Item("ID_CAF") Then
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
                '        Response.Write("<script>alert('Non è possibile modificare. La domanda è ancora in carico presso un ente esterno!');</script>")
                '    End If
                'End If
                'Else
                '    If par.IfNull(myReader("N_DISTINTA"), 0) > 0 And Fl_Integrazione <> "1" Then
                '        btnSalva.Visible = False
                '        'Label5.Visible = False
                '        imgStampa.Visible = False
                '        'Label6.Visible = False
                '        Dic_Dichiarazione1.DisattivaTutto()
                '        Dic_Integrazione1.DisattivaTutto()
                '        Dic_Note1.DisattivaTutto()
                '        Dic_Nucleo1.DisattivaTutto()
                '        Dic_Patrimonio1.DisattivaTutto()
                '        Dic_Reddito1.DisattivaTutto()
                '        Dic_Reddito_Conv1.DisattivaTutto()
                '        cmbStato.Enabled = False
                '        If Fl_US <> "1" Then
                '            Response.Write("<script>alert('Non è possibile modificare. La domanda è stata scaricata nella distinta N. " & par.IfNull(myReader("N_DISTINTA"), "") & " ! USARE LE FUNZIONI DI INTEGRAZIONE');</script>")
                '        End If
                '    End If
            End If
            'Else
            '    If par.IfNull(myReader("N_DISTINTA"), 0) > 0 And Fl_Integrazione <> "1" Then
            '        btnSalva.Visible = False
            '        'Label5.Visible = False
            '        imgStampa.Visible = False
            '        'Label6.Visible = False

            '        Dic_Dichiarazione1.DisattivaTutto()
            '        Dic_Integrazione1.DisattivaTutto()
            '        Dic_Note1.DisattivaTutto()
            '        Dic_Nucleo1.DisattivaTutto()
            '        Dic_Patrimonio1.DisattivaTutto()
            '        Dic_Reddito1.DisattivaTutto()
            '        Dic_Reddito_Conv1.DisattivaTutto()
            '        cmbStato.Enabled = False
            '        If Fl_US <> "1" Then
            '            Response.Write("<script>alert('Non è possibile modificare. La domanda è stata scaricata nella distinta N. " & par.IfNull(myReader("N_DISTINTA"), "") & " ! USARE LE FUNZIONI DI INTEGRAZIONE');</script>")
            '        End If
            '    End If
            'End If
            'Else
            'If Session.Item("ID_CAF") = "6" Then
            '    'If Session.Item("ID_CAF") = "6" Then

            '    '    btnSalva.Visible = False
            '    '    'Label5.Visible = False
            '    '    imgStampa.Visible = False
            '    '    'Label6.Visible = False

            '    '    Dic_Dichiarazione1.DisattivaTutto()
            '    '    Dic_Integrazione1.DisattivaTutto()
            '    '    Dic_Note1.DisattivaTutto()
            '    '    Dic_Nucleo1.DisattivaTutto()
            '    '    Dic_Patrimonio1.DisattivaTutto()
            '    '    Dic_Reddito1.DisattivaTutto()
            '    '    Dic_Reddito_Conv1.DisattivaTutto()
            '    '    cmbStato.Enabled = False
            '    '    If Fl_US <> "1" Then
            '    '        Response.Write("<script>alert('Non è possibile modificare. La domanda NON è stata ancora creata!');</script>")
            '    '    End If
            '    'End If
            'End If
            'End If
            myReader.Close()


            'RICAVO L'ID DELLA DICHIARAZIONE ASSOCIATA
            If tipoRichiesta.Value = "5" Then
                par.cmd.CommandText = "SELECT * from DICHIARAZIONI_VSA WHERE PG=" & PGcollegato
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    lIdDichCollegata = par.IfNull(myReader("ID"), 0)
                End If
                myReader.Close()
                CType(Dic_Dichiarazione1.FindControl("lblNumDich"), Label).Visible = True
                CType(Dic_Dichiarazione1.FindControl("lblNumDich"), Label).Text = "<< Num. dich. associata <a href='javascript:void(0)' onclick=" & Chr(34) & "today = new Date();document.getElementById('H1').value='0';window.open('max.aspx?CH=2&ID=" & lIdDichCollegata & "','DichCollegata'+ today.getMinutes() + today.getSeconds(),'top=250,left=650,width=670,height=550,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">" & PGcollegato & " </a> >>"
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

            ''par.cmd.CommandText = "SELECT COUNT(ID_PRATICA) FROM EVENTI_DICHIARAZIONI_VSA WHERE COD_EVENTO='F132' AND ID_PRATICA=" & lIdDichiarazione
            ''myReader = par.cmd.ExecuteReader()
            ''If myReader.Read Then
            ''    If myReader(0) > 0 Then
            ''        Session.Item("STAMPATO") = "1"
            ''    Else
            ''        Session.Item("STAMPATO") = "0"
            ''    End If
            ''Else
            ''    Session.Item("STAMPATO") = "0"
            ''End If
            ''myReader.Close()

            Session.Item("STAMPATO") = "0"

            If Fl_US <> "1" Then
                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            Else
                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione
            End If
            myReader = par.cmd.ExecuteReader()
            lblPG.ToolTip = lIdDichiarazione
            If myReader.Read() Then
                'If annoRedd = "" Then
                RiempiCmbAnniRedd(par.IfNull(myReader("ANNO_SIT_ECONOMICA"), ""))
                CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                'Else
                'cmbAnnoReddituale.Items.Add(New ListItem(annoRedd))
                'CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = annoRedd
                'End If
                lIndice_Bando = myReader("ID_BANDO")
                lblPG.Text = par.IfNull(myReader("pg"), "")

                'cmbStato.Items.FindByValue(par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "0")).Selected = True


                txtDataPG.Text = par.FormattaData(par.IfNull(myReader("data_pg"), ""))
                CType(Dic_Dichiarazione1.FindControl("txtId"), TextBox).Text = lIdDichiarazione
                CType(Dic_Dichiarazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data"), ""))
                'CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")


                cmbAnnoReddituale.SelectedIndex = -1
                cmbAnnoReddituale.Items.FindByValue(CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text).Selected = True
                annoRedd = cmbAnnoReddituale.SelectedItem.Value

                CType(Dic_Reddito_Conv1.FindControl("label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA " & CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text
                CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
                CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
                CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
                CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = cmbAnnoReddituale.SelectedItem.Value





                CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = Val(par.IfNull(myReader("ANNO_SIT_ECONOMICA"), ""))
                CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")

                CType(Dic_Note1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader("NOTE"), "")

                CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_INT_ERP"), ""))
                CType(Dic_Sottoscrittore1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_S"), ""))


                CType(Dic_Reddito_Conv1.FindControl("txtMinori"), TextBox).Text = par.IfNull(myReader("minori_carico"), "0")

                If par.IfNull(myReader("TIPO"), "0") = "1" Then
                    ChFO.Checked = True
                    'Label10.Visible = False
                    'lblDomAssociata.Visible = False
                    Label10.Text = "ISEE"
                    lblDomAssociata.Text = Format(CDbl(par.IfNull(myReader("ISEE"), "0")), "##,##0.00")
                Else
                    ChFO.Checked = False
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

            'PRENDO LA DICHIARAZIONE ASSOCIATA PER IL CAMBIO
            'If tipoRichiesta.Value = "5" Then
            '    par.cmd.CommandText = "SELECT DICHIARAZIONI_VSA.PG AS PGDICH,DICHIARAZIONI_VSA.ID AS IDDICH FROM DOMANDE_BANDO_VSA,DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND CONTRATTO_NUM IN (SELECT COD_CONTRATTO_SCAMBIO FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & ")"
            '    myReader = par.cmd.ExecuteReader
            '    If myReader.Read Then
            '        CType(Dic_Dichiarazione1.FindControl("lblDichScambio"), Label).Visible = True
            '        CType(Dic_Dichiarazione1.FindControl("lblDichScambio"), Label).Text = "<<&nbsp &nbsp Num. dich. <a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('max.aspx?CH=2&ID=" & par.IfNull(myReader("IDDICH"), -1) & "','Dichiarazione2','height=550,top=200,left=550,width=670,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">" & par.IfNull(myReader("PGDICH"), "") & "</a>&nbsp &nbsp>>"
            '    End If
            '    myReader.Close()
            'End If


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

                '********** Aggiungere i valori di NUOVO COMPONENTE (considerando la nuova tabella) ********** 
                par.cmd.CommandText = "SELECT * FROM NUOVI_COMP_NUCLEO_VSA WHERE ID_COMPONENTE = " & myReader("ID")
                Dim myReaderNewComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderNewComp.Read Then
                    MIAS = par.MiaFormat(par.IfNull(myReader("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 10) & " " & _
                        par.MiaFormat("SI", 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReaderNewComp("DATA_INGRESSO_NUCLEO"), "")), 10)
                    newComp = 1
                Else
                    MIAS = par.MiaFormat(par.IfNull(myReader("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 10) & " " & par.MiaFormat("NO", 12)
                End If
                myReaderNewComp.Close()

                'MIAS = par.MiaFormat(par.IfNull(myReader("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")), 10) & " " & par.MiaFormat(par.IfNull(myReader("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 2)
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
                    CType(Dic_Patrimonio1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("COGNOME"), "") & "," & par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader2("COD_INTERMEDIARIO"), ""), 27) & " " & par.MiaFormat(myReader2("INTERMEDIARIO"), 16) & " " & par.MiaFormat(myReader2("IMPORTO"), 8) & ",00 ", myReader("PROGR")))
                End While
                myReader2.Close()

                'par.cmd.CommandText = "SELECT COMP_PATR_IMMOB_VSA.*,T_TIPO_PATR_IMMOB.descrizione FROM T_TIPO_PATR_IMMOB,COMP_PATR_IMMOB_VSA WHERE COMP_PATR_IMMOB_VSA.ID_COMPONENTE=" & myReader("ID") & " and COMP_PATR_IMMOB_VSA.ID_TIPO=T_TIPO_PATR_IMMOB.cod (+) ORDER BY COMP_PATR_IMMOB_VSA.ID_COMPONENTE ASC"
                'myReader2 = par.cmd.ExecuteReader()
                'While myReader2.Read
                '    RESIDENZA = " "
                '    If myReader2("F_RESIDENZA") = "0" Then
                '        RESIDENZA = "NO"
                '    Else
                '        RESIDENZA = "SI"
                '    End If
                '    CType(Dic_Patrimonio1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 25) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 20) & " " & par.MiaFormat(myReader2("PERC_PATR_IMMOBILIARE"), 6) & " " & par.MiaFormat(Val(myReader2("VALORE")), 8) & ",00 " & par.MiaFormat(Val(myReader2("MUTUO")), 8) & ",00 " & RESIDENZA, myReader("PROGR")))
                'End While
                'myReader2.Close()

                'MODIFICA 27/01/2012 DA MASSIMILIANO
                Dim PienaP As String = ""

                par.cmd.CommandText = "SELECT COMP_PATR_IMMOB_VSA.*,T_TIPO_PATR_IMMOB.descrizione FROM T_TIPO_PATR_IMMOB,COMP_PATR_IMMOB_VSA WHERE COMP_PATR_IMMOB_VSA.ID_COMPONENTE=" & myReader("ID") & " and COMP_PATR_IMMOB_VSA.ID_TIPO=T_TIPO_PATR_IMMOB.cod (+) ORDER BY COMP_PATR_IMMOB_VSA.ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    PienaP = ""
                    If par.IfNull(myReader2("PIENA_PROPRIETA"), "0") = "1" Then
                        PienaP = Chr(160) & Chr(160) & Chr(160) & "SI"
                    Else
                        PienaP = Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160)
                    End If
                    'CType(Dic_Patrimonio1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 25) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 20) & " " & par.MiaFormat(myReader2("PERC_PATR_IMMOBILIARE"), 6) & " " & par.MiaFormat(Val(myReader2("VALORE")), 8) & ",00 " & par.MiaFormat(Val(myReader2("MUTUO")), 8) & ",00 " & par.MiaFormat(RESIDENZA, 2) & " " & par.MiaFormat(par.IfNull(myReader2("cat_catastale"), "A01"), 3) & " " & par.MiaFormat(par.IfNull(myReader2("comune"), "MILANO"), 30) & " " & par.MiaFormat(par.IfNull(myReader2("N_VANI"), "0"), 2) & " " & par.MiaFormat(par.IfNull(myReader2("SUP_UTILE"), "0"), 7) & " " & PienaP, myReader("PROGR")))
                    CType(Dic_Patrimonio1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 25) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 20) & " " & par.MiaFormat(myReader2("PERC_PATR_IMMOBILIARE"), 7) & " " & par.MiaFormat(par.IfNull(myReader2("cat_catastale"), "A01"), 3) & " " & par.MiaFormat(Val(par.IfNull(myReader2("REND_CATAST_DOMINICALE"), 0)), 8) & ",00 " & " " & par.MiaFormat(Val(par.IfNull(myReader2("VALORE"), 0)), 8) & ",00 " & par.MiaFormat(Val(par.IfNull(myReader2("MUTUO"), 0)), 12) & ",00 " & " " & par.MiaFormat(par.IfNull(myReader2("comune"), "MILANO"), 20) & " " & par.MiaFormat(par.IfNull(myReader2("N_VANI"), "0"), 2) & " " & par.MiaFormat(par.IfNull(myReader2("SUP_UTILE"), "0"), 7) & " " & PienaP, myReader("PROGR")))

                End While
                myReader2.Close()
                'FINE MODIFICA 27/01/2012 DA MASSIMILIANO


                par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_COMPONENTE=" & myReader("ID") & " ORDER BY ID_COMPONENTE ASC"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    'CType(Dic_Reddito_Conv1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("COGNOME"), "") & "," & par.IfNull(myReader("NOME"), ""), 35) & " " & par.MiaFormat(myReader2("CONDIZIONE"), 5) & " " & par.MiaFormat(myReader2("PROFESSIONE"), 5) & Chr(160) & Chr(160) & " " & par.MiaFormat(myReader2("DIPENDENTE"), 7) & ",00 " & par.MiaFormat(myReader2("PENSIONE"), 7) & ",00 " & par.MiaFormat(myReader2("AUTONOMO"), 7) & ",00 " & par.MiaFormat(myReader2("NON_IMPONIBILI"), 7) & ",00 " & par.MiaFormat(myReader2("occasionali"), 7) & ",00 " & par.MiaFormat(myReader2("DOM_AG_FAB"), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("ONERI"), ""), 7) & ",00 ", myReader("PROGR")))
                    CType(Dic_Reddito_Conv1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("COGNOME"), "") & "," & par.IfNull(myReader("NOME"), ""), 35) & " " & par.MiaFormat("", 5) & " " & par.MiaFormat("", 5) & Chr(160) & Chr(160) & " " & par.MiaFormat(par.IfNull(myReader2("DIPENDENTE"), ""), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("PENSIONE"), ""), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("AUTONOMO"), ""), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("NON_IMPONIBILI"), ""), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("occasionali"), ""), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("DOM_AG_FAB"), ""), 7) & ",00 " & par.MiaFormat(par.IfNull(myReader2("ONERI"), 0), 7) & "    ", myReader("PROGR")))
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

            'par.cmd.CommandText = "SELECT * FROM UTENZA_DOC_MANCANTE WHERE ID_DICHIARAZIONE in (SELECT UTENZA_DICHIARAZIONI.ID FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI WHERE UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO AND RAPPORTO IN (SELECT CONTRATTO_NUM FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & ")) order by descrizione asc"
            'myReader = par.cmd.ExecuteReader()
            'While myReader.Read
            '    CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 1
            '    CType(Dic_Note1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 250), myReader("ID_DOC")))
            'End While
            'myReader.Close()

            par.cmd.CommandText = "SELECT * FROM VSA_DOC_MANCANTI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " ORDER BY DESCRIZIONE ASC"
            myReader = par.cmd.ExecuteReader()
            While myReader.Read
                CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 1
                CType(Dic_Note1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 250), myReader("ID_DOC")))
            End While
            myReader.Close()

            If CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 1 Then
                CType(Dic_Note1.FindControl("chkDocManc"), CheckBox).Enabled = True
                CType(Dic_Note1.FindControl("txtDataDocManc"), TextBox).Enabled = True
            End If

            If presDocManca = 1 Then
                CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 0
                CType(Dic_Note1.FindControl("chkDocManc"), CheckBox).Checked = True
                CType(Dic_Note1.FindControl("txtDataDocManc"), TextBox).Text = par.FormattaData(dataPresDocM)
                'CType(Dic_Note1.FindControl("ListBox1"), ListBox).Enabled = False
                CType(Dic_Note1.FindControl("Button1"), Button).Enabled = False
                CType(Dic_Note1.FindControl("Button2"), Button).Enabled = False
                CType(Dic_Note1.FindControl("chkDocManc"), CheckBox).Enabled = True
                CType(Dic_Note1.FindControl("txtDataDocManc"), TextBox).Enabled = True
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
           
            End If
            If tipoRichiesta.Value = "4" Then
                MenuStampe.Visible = False
            End If



            If Fl_US <> "1" Then
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessDICH, par.myTrans)
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
                '**********************24/10/2011***************MT MODIFY...DOMANDA PG
                par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
                Dim myreader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myreader0.Read Then
                    lblDomAssociata.Text = par.IfNull(myreader0("PG"), "")
                End If
                myreader0.Close()
                '**********************24/10/2011***************END MT MODIFY...DOMANDA PG

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
                    CType(Dic_Reddito_Conv1.FindControl("Label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA " & par.IfNull(myReader3("ANNO_SIT_ECONOMICA"), "")


                    CType(Dic_Note1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader3("NOTE"), "")

                    CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("DATA_INT_ERP"), ""))
                    CType(Dic_Sottoscrittore1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader3("DATA_S"), ""))



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


                    '********** Aggiungere i valori di NUOVO COMPONENTE (considerando la nuova tabella) ********** 
                    par.cmd.CommandText = "SELECT * FROM NUOVI_COMP_NUCLEO_VSA WHERE ID_COMPONENTE = " & myReader3("ID")
                    Dim myReaderNewComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderNewComp.Read Then
                        MIAS = par.MiaFormat(par.IfNull(myReader3("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader3("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader3("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader3("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader3("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader3("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader3("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 10) & " " _
                            & par.MiaFormat("SI", 12) & " " & par.MiaFormat(par.FormattaData(myReaderNewComp("DATA_INGRESSO_NUCLEO")), 10)
                        newComp = 1
                    Else
                        MIAS = par.MiaFormat(par.IfNull(myReader3("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader3("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader3("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader3("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader3("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader3("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader3("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 10) & " " & par.MiaFormat("NO", 12)
                    End If
                    myReaderNewComp.Close()

                    'MIAS = par.MiaFormat(par.IfNull(myReader3("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader3("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader3("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader3("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader3("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader3("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader3("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 2)
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
                        CType(Dic_Patrimonio1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader3("COGNOME"), "") & "," & par.IfNull(myReader3("NOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader2("COD_INTERMEDIARIO"), ""), 13) & " " & par.MiaFormat(myReader2("INTERMEDIARIO"), 30) & " " & par.MiaFormat(myReader2("IMPORTO"), 8) & ",00 ", myReader3("PROGR")))
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
                        'CType(Dic_Patrimonio1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 25) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 20) & " " & par.MiaFormat(myReader2("PERC_PATR_IMMOBILIARE"), 6) & " " & par.MiaFormat(Val(myReader2("VALORE")), 8) & ",00 " & par.MiaFormat(Val(myReader2("MUTUO")), 8) & ",00 " & RESIDENZA, myReader3("PROGR")))
                        CType(Dic_Patrimonio1.FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader3("COGNOME") & "," & myReader3("NOME"), 25) & " " & par.MiaFormat(myReader2("DESCRIZIONE"), 20) & " " & par.MiaFormat(myReader2("PERC_PATR_IMMOBILIARE"), 7) & " " & par.MiaFormat(par.IfNull(myReader2("cat_catastale"), "A01"), 3) & " " & par.MiaFormat(Val(par.IfNull(myReader2("REND_CATAST_DOMINICALE"), 0)), 8) & ",00 " & " " & par.MiaFormat(Val(par.IfNull(myReader2("VALORE"), 0)), 8) & ",00 " & par.MiaFormat(Val(par.IfNull(myReader2("MUTUO"), 0)), 12) & ",00 " & " " & par.MiaFormat(par.IfNull(myReader2("comune"), "MILANO"), 20) & " " & par.MiaFormat(par.IfNull(myReader2("N_VANI"), "0"), 2) & " " & par.MiaFormat(par.IfNull(myReader2("SUP_UTILE"), "0"), 7) & " " & RESIDENZA, myReader3("PROGR")))

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
                par.cmd.CommandText = "SELECT * FROM VSA_DOC_MANCANTI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " ORDER BY DESCRIZIONE ASC"
                myReader3 = par.cmd.ExecuteReader()
                While myReader3.Read
                    CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 1
                    CType(Dic_Note1.FindControl("ListBox1"), ListBox).Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader3("DESCRIZIONE"), ""), 250), myReader3("ID_DOC")))
                End While
                myReader3.Close()

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessDICH, par.myTrans)

            Else
                'Label4.Text = EX1.Message
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End If
            imgStampa.Enabled = True
            'imgStampa.Visible = False
            btnApplica.Enabled = False
            btnApplica.Visible = False
            btnSalva.Enabled = False
            btnSalva.Visible = False
            'Label5.Visible = False
            'Label6.Visible = False
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessDICH)
            HttpContext.Current.Session.Remove(lIdConnessDICH)
            Session.Item("LAVORAZIONE") = "0"
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            imgStampa.Enabled = False
            btnSalva.Enabled = False
            'Label4.Text = ex.Message
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessDICH)
            HttpContext.Current.Session.Remove(lIdConnessDICH)
            Session.Item("LAVORAZIONE") = "0"
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Private Function CaricaComponenti()
        Dim MIAS As String
        Dim INDENNITA As String

        par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessDICH), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessDICH), Oracle.DataAccess.Client.OracleTransaction)
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

            '********** Aggiungere i valori di NUOVO COMPONENTE (considerando la nuova tabella) ********** 
            par.cmd.CommandText = "SELECT * FROM NUOVI_COMP_NUCLEO_VSA WHERE ID_COMPONENTE = " & myReader("ID")
            Dim myReaderNewComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderNewComp.Read Then
                MIAS = par.MiaFormat(par.IfNull(myReader("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 10) _
                    & par.MiaFormat("SI", 12) & par.MiaFormat(par.FormattaData(par.IfNull(myReaderNewComp("DATA_INGRESSO_NUCLEO"), "")), 10)
                newComp = 1
            Else
                MIAS = par.MiaFormat(par.IfNull(myReader("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 11) & par.MiaFormat("NO", 12) _
                    & par.MiaFormat("", 10)
            End If

            'MIAS = par.MiaFormat(par.IfNull(myReader("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), "")), 10) & " " & par.MiaFormat(par.IfNull(myReader("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 2)
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

    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        Try
            If txtModificato.Value <> "111" Then
                If Session.Item("LAVORAZIONE") = "1" Then

                    If Fl_Integrazione = "1" Then
                        H1.Value = "1"
                    End If
                    If Session.Item("STAMPATO") = "0" And cmbStato.Text = "1" Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                                    & "alert('ATTENZIONE, La Dichiarazione deve essere elaborata!');" _
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
                        par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessDICH), Oracle.DataAccess.Client.OracleConnection)
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessDICH), Oracle.DataAccess.Client.OracleTransaction)

                        ‘‘par.cmd.Transaction = par.myTrans
                        par.myTrans.Rollback()
                    End If
                    'par.myTrans.Dispose()
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessDICH)
                    HttpContext.Current.Session.Remove(lIdConnessDICH)
                    Session.Item("LAVORAZIONE") = "0"
                    Session.Remove("STAMPATO")
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Page.Dispose()
                    If Fl_Integrazione = "1" Or Fl_US = "1" Or Request.QueryString("CH") <> "" Then
                        'Session.Item("LAVORAZIONE") = "1"
                        Response.Write("<script>window.close();</script>")
                    Else
                        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                    End If
                Else
                    '********* AGGIUNTA chiusura connessione 23/12/2011 *********
                    par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessDICH), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    If par.OracleConn.State = Data.ConnectionState.Open Then
                        par.OracleConn.Close()
                        par.OracleConn.Dispose()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If
                    '************** FINE AGGIUNTA chiusura connessione 23/12/2011 *********

                    Session.Item("LAVORAZIONE") = "0"
                    Page.Dispose()
                    If Fl_Integrazione = "1" Or Fl_US = "1" Or Request.QueryString("CH") <> "" Then
                        'Session.Item("LAVORAZIONE") = "1"
                        Response.Write("<script>window.close();</script>")
                    Else
                        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                    End If
                End If
            Else
                Me.txtModificato.Value = "1"
            End If

        Catch EX As Exception
            Session.Item("LAVORAZIONE") = "0"
            Page.Dispose()
            Response.Write("<script>document.location.href=""../ErrorPage.aspx""</script>")
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
        Dim idDomanda As Long



        Try

            bMemorizzato = False

            NUM_PARENTI = 0

            N_INV_100_ACC = 0
            N_INV_100_NO_ACC = 0
            N_INV_100_66 = 0



            If VerificaDati(S) = False Then
                btnApplica.Visible = False
                imgStampa.Enabled = False
                'imgStampa.ImageUrl = "../NuoveImm/Img_No_Stampa.png"
                Exit Try
            End If

            'If CType(Dic_Nucleo1.FindControl("NuovoCompon"), HiddenField).Value <> "1" Then
            '    Response.Write("<script>alert('Attenzione...Nucleo familiare non aggiornato!')</script>")
            '    imgStampa.Enabled = False
            '    imgStampa.ImageUrl = "../NuoveImm/Img_No_Stampa.png"
            '    Exit Try
            'End If
            If cmbStato.SelectedValue = "1" Then
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

                'If CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).SelectedItem.Text <> "SI" And CType(Dic_Patrimonio1.FindControl("ChUbicazione"), CheckBox).Checked = True Then
                '    Response.Write("<script>alert('ATTENZIONE...è possibile specificare la dislocazione dell\'immobile solo se la U.i. posseduta è adeguata al nucleo o di valore')</script>")
                '    imgStampa.Enabled = False
                '    imgStampa.ImageUrl = "../NuoveImm/Img_No_Stampa.png"
                '    Exit Try
                'End If



                '17/02/2012 CONTROLLO CHE L'ANNO REDDITO SIA SUPERIORE DIVERSO DAL 2006
                If tipoRichiesta.Value = "3" And CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text <= "2006" Then
                    Response.Write("<script>alert('Attenzione...si prega di inserire il corretto anno di riferimento reddituale!')</script>")
                    Exit Try
                End If

                S = ""

                par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessDICH), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessDICH), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                'If par.OracleConn.State = Data.ConnectionState.Closed Then
                '    par.OracleConn.Open()
                'End If


                Dim contaNuovComp As Integer
                'If cmbStato.SelectedValue = "1" Then
                If tipoRichiesta.Value = "2" Then
                    contaNuovComp = 0
                    For i = 0 To CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items.Count - 1
                        If par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 131, 2) = "SI" Then
                            contaNuovComp = contaNuovComp + 1
                        End If
                    Next
                End If
                'End If

                '***** 23/11/11 CONTROLLO SE MORE UXORIO OPPURE PER ASSISTENZA SOLO 1 NUOVO COMPONENTE *****
                Dim tipoAmpl As Integer
                If tipoRichiesta.Value = "2" Then
                    par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If lettore.Read Then
                        tipoAmpl = par.IfNull(lettore("ID_CAUSALE_DOMANDA"), "-1")
                    End If
                    lettore.Close()

                    If contaNuovComp = 0 Then
                        Response.Write("<script>alert('ATTENZIONE...Aggiungere il nuovo componente nell\'apposita sezione. Memorizzazione non effettuata!');</script>")
                        Exit Try
                    End If

                    'If tipoAmpl = "4" Or tipoAmpl = "5" Or tipoAmpl = "7" Then
                    '    contaNuovComp = 0
                    '    For i = 0 To CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items.Count - 1
                    '        If par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 131, 2) = "SI" Then
                    '            contaNuovComp = contaNuovComp + 1
                    '        End If
                    '    Next
                    '    If contaNuovComp > 1 Then
                    '        Response.Write("<script>alert('ATTENZIONE...per la tipologia di ampliamento richiesta non è possibile aggiungere più di un componente. Memorizzazione non effettuata!');</script>")
                    '        Exit Try
                    '    End If
                    'End If
                End If
                '***** FINE CONTROLLO SE MORE UXORIO OPPURE PER ASSISTENZA SOLO 1 NUOVO COMPONENTE *****
            End If

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessDICH), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessDICH), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            End If

            Dim sStringaSql As String


            par.cmd.CommandText = "DELETE FROM DOMANDE_REDDITI_VSA WHERE ID_DOMANDA=" & lIdDichiarazione
            par.cmd.ExecuteNonQuery()


            'par.cmd.CommandText = "SELECT comp_nucleo_VSA.* FROM comp_nucleo_VSA,DOMANDE_BANDO_VSA WHERE COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.PROGR_COMPONENTE=0 AND  comp_nucleo_VSA.progr=0 and comp_nucleo_VSA.id_dichiarazione<>" & lIdDichiarazione & " and comp_nucleo_VSA.cod_fiscale='" & CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text & "'"
            'Dim myReader22 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader22.Read() Then
            '    myReader22.Close()
            '    Response.Write("<script>alert('Attenzione...Il codice fiscale del dichiarante risulta essere intestatario di precedente domanda!! Memorizzazione non effettuata.')</script>")
            '    imgStampa.Enabled = False
            '    imgStampa.ImageUrl = "../NuoveImm/Img_No_Stampa.png"

            '    Session.Item("STAMPATO") = "1"
            '    Exit Try
            'End If
            'myReader22.Close()

            'par.cmd.CommandText = "SELECT comp_nucleo_VSA.* FROM comp_nucleo_VSA,DOMANDE_BANDO_VSA WHERE COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND comp_nucleo_VSA.id_dichiarazione<>" & lIdDichiarazione & " and comp_nucleo_VSA.cod_fiscale='" & CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text & "'"
            'Dim myReader23 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader23.Read()
            '    par.cmd.CommandText = "SELECT comp_nucleo_VSA.* FROM comp_nucleo_VSA,DOMANDE_BANDO_VSA WHERE DOMANDE_BANDO_VSA.PROGR_COMPONENTE=COMP_NUCLEO_VSA.PROGR AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE=" & myReader23("ID_DICHIARAZIONE")
            '    Dim myReader24 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    If myReader24.Read Then
            '        For i = 0 To CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items.Count - 1
            '            If par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) = myReader24("COD_FISCALE") Then
            '                Response.Write("<script>alert('Attenzione...Il codice fiscale del dichiarante è presente nel nucleo di precedente domanda e intestatario di questa, è presente in questo nucleo!! Memorizzazione non effettuata.')</script>")
            '                imgStampa.Enabled = False
            '                imgStampa.ImageUrl = "../NuoveImm/Img_No_Stampa.png"
            '                myReader24.Close()
            '                myReader23.Close()
            '                Session.Item("STAMPATO") = "1"
            '                Exit Try
            '            End If
            '        Next
            '    End If
            '    myReader24.Close()
            'End While
            'myReader23.Close()

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

            Dim codContratto As String = ""
            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID=" & lIdDomanda
            Dim myReaderContr As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderContr.Read Then
                codContratto = par.IfNull(myReaderContr("CONTRATTO_NUM"), "")
            End If
            myReader.Close()

            If Dic_Nucleo1.ProgrDaCancellare <> "" Then
                '****** 14/03/2012 INSERIMENTO COMPONENTE DA CANCELLARE *******
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & Dic_Nucleo1.ProgrDaCancellare & ")"
                Dim myReaderCanc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReaderCanc.Read
                    'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND " _
                    '    & " COD_FISCALE='" & par.IfNull(myReaderCanc("COD_FISCALE"), "") & "' AND ID_CONTRATTO = (SELECT ID FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & codContratto & "')"
                    'Dim myReaderIntest As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReaderIntest.Read Then
                    '    If myReaderIntest("COD_TIPOLOGIA_OCCUPANTE") = "INTE" Then
                    '        Response.Write("<script>alert('Impossibile procedere! Il componente che si desidera eliminare risulta intestatario del contatto!')</script>")
                    '        Exit Try
                    '    End If
                    'End If
                    'myReaderIntest.Close()

                    par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_CANCELL (ID_DICHIARAZIONE,NOME,COGNOME,DATA_NASCITA,COD_FISCALE,ID_MOTIVO,DATA_USCITA) VALUES " _
                    & "(" & lIdDichiarazione & ",'" & par.PulisciStrSql(par.IfNull(myReaderCanc("NOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(myReaderCanc("COGNOME"), "")) & "'," _
                    & "'" & par.IfNull(myReaderCanc("DATA_NASCITA"), "") & "','" & par.IfNull(myReaderCanc("COD_FISCALE"), "") & "','" _
                    & CType(Dic_Nucleo1.FindControl("MotivoUscita"), HiddenField).Value & "','" & par.AggiustaData(CType(Dic_Nucleo1.FindControl("DataUscita"), HiddenField).Value) & "')"
                    par.cmd.ExecuteNonQuery()
                End While
                myReaderCanc.Close()
                '****** 14/03/2012 FINE INSERIMENTO COMPONENTE DA CANCELLARE *******

                par.cmd.CommandText = "SELECT * FROM NUOVI_COMP_NUCLEO_VSA,COMP_NUCLEO_VSA WHERE COMP_NUCLEO_VSA.ID = " _
                    & "NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE and ID_DICHIARAZIONE=" & lIdDichiarazione & Dic_Nucleo1.ProgrDaCancellare & ")"
                Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderN.Read Then
                    par.cmd.CommandText = "DELETE FROM NUOVI_COMP_NUCLEO_VSA WHERE ID_COMPONENTE=" & myReaderN("ID_COMPONENTE")
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderN.Close()

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
            Dim MIAS As String = ""
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

                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND (PROGR=" & i & " OR COD_FISCALE='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "')"
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

                    If par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 131, 2) <> "NO" Then
                        'sStringaSql = "INSERT INTO NUOVI_COMP_NUCLEO_VSA (ID_COMPONENTE,DATA_INGRESSO_NUCLEO) VALUES " _
                        '& "(" & idComponenti(i) & ", '" & par.PulisciStrSql(par.AggiustaData(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 144, 10))) & "')"
                        'par.cmd.CommandText = sStringaSql
                        par.cmd.CommandText = "INSERT INTO NUOVI_COMP_NUCLEO_VSA (ID_COMPONENTE,DATA_INGRESSO_NUCLEO,ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,COMUNE_RES_DNTE,CAP_RES_DNTE,CARTA_I,CARTA_I_DATA,CARTA_I_RILASCIATA,PERMESSO_SOGG_N,PERMESSO_SOGG_DATA,FL_REFERENTE) VALUES " _
                        & "(" & idComponenti(i) & ", '" & par.PulisciStrSql(par.AggiustaData(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 144, 10))) & "'," _
                        & "'" & CType(Dic_Nucleo1.FindControl("txtidTipoVIA"), HiddenField).Value & "','" & par.PulisciStrSql(CType(Dic_Nucleo1.FindControl("txtVIA"), HiddenField).Value.ToUpper) & "'," _
                        & "'" & CType(Dic_Nucleo1.FindControl("txtCIVICO"), HiddenField).Value.ToUpper & "','" & par.PulisciStrSql(CType(Dic_Nucleo1.FindControl("txtCOMUNE"), HiddenField).Value.ToUpper) & "'," _
                        & "'" & CType(Dic_Nucleo1.FindControl("txtCAP"), HiddenField).Value & "','" & CType(Dic_Nucleo1.FindControl("txtDOCIDENT"), HiddenField).Value.ToUpper & "'," _
                        & "'" & par.AggiustaData(CType(Dic_Nucleo1.FindControl("txtDATADOC"), HiddenField).Value) & "','" & CType(Dic_Nucleo1.FindControl("txtRILASCIO"), HiddenField).Value.ToUpper & "'," _
                        & "'" & CType(Dic_Nucleo1.FindControl("txtSOGGIORNO"), HiddenField).Value.ToUpper & "','" & par.AggiustaData(CType(Dic_Nucleo1.FindControl("txtDATASogg"), HiddenField).Value) & "'," _
                        & "'" & CType(Dic_Nucleo1.FindControl("txtREFERENTE"), HiddenField).Value & "')"
                        par.cmd.ExecuteNonQuery()

                        newComp = 1
                    End If

                Else

                    If par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 131, 2) = "NO" Then
                        par.cmd.CommandText = "SELECT * FROM NUOVI_COMP_NUCLEO_VSA WHERE ID_COMPONENTE=" & myReader4("ID")
                        Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderN.Read Then
                            par.cmd.CommandText = "DELETE FROM NUOVI_COMP_NUCLEO_VSA WHERE ID_COMPONENTE=" & myReaderN("ID_COMPONENTE")
                            par.cmd.ExecuteNonQuery()
                        End If
                        myReaderN.Close()

                        idComponenti(i) = myReader4(0)
                        If i = 0 And (CType(Dic_Dichiarazione1.FindControl("txtCognome"), TextBox).Text <> par.IfNull(myReader4("COGNOME"), "") Or CType(Dic_Dichiarazione1.FindControl("txtNome"), TextBox).Text <> par.IfNull(myReader4("NOME"), "") Or CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text <> par.IfNull(myReader4("COD_FISCALE"), "") Or CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text <> par.FormattaData(par.IfNull(myReader4("DATA_NASCITA"), ""))) Then
                            sStringaSql = "UPDATE COMP_NUCLEO_VSA set PROGR=" & i & ",COGNOME='" & par.PulisciStrSql(CType(Dic_Dichiarazione1.FindControl("txtCognome"), TextBox).Text) & "'," _
                                        & "NOME='" & par.PulisciStrSql(CType(Dic_Dichiarazione1.FindControl("txtNome"), TextBox).Text) & "'," _
                                        & "GRADO_PARENTELA=" & COD_PARENTE & "," _
                                        & "COD_FISCALE='" & par.PulisciStrSql(CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text) & "'," _
                                        & "PERC_INVAL='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 107, 6)) & "'," _
                                        & "DATA_NASCITA='" & par.PulisciStrSql(par.AggiustaData(CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text)) & "'," _
                                        & "USL='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 114, 5)) & "'," _
                                        & "INDENNITA_ACC='" & INDENNITA & "'," _
                                        & "SESSO='" & par.RicavaSesso(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 64, 16)) & "' " _
                                        & "WHERE ID=" & idComponenti(i)
                        Else
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
                        End If
                        par.cmd.CommandText = sStringaSql
                        par.cmd.ExecuteNonQuery()
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

                        par.cmd.CommandText = "SELECT * FROM NUOVI_COMP_NUCLEO_VSA WHERE ID_COMPONENTE=" & idComponenti(i)
                        Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderN.Read = False Then
                            'par.cmd.CommandText = "INSERT INTO NUOVI_COMP_NUCLEO_VSA (ID_COMPONENTE,DATA_INGRESSO_NUCLEO,ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,COMUNE_RES_DNTE,CAP_RES_DNTE,CARTA_I,CARTA_I_DATA,CARTA_I_RILASCIATA,PERMESSO_SOGG_N,PERMESSO_SOGG_DATA,FL_REFERENTE) VALUES " _
                            '& "(" & idComponenti(i) & ", '" & par.PulisciStrSql(par.AggiustaData(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 144, 10))) & "'," _
                            '& "'" & CType(Dic_Nucleo1.FindControl("txtidTipoVIA"), HiddenField).Value & "','" & par.PulisciStrSql(CType(Dic_Nucleo1.FindControl("txtVIA"), HiddenField).Value.ToUpper) & "'," _
                            '& "'" & CType(Dic_Nucleo1.FindControl("txtCIVICO"), HiddenField).Value.ToUpper & "','" & par.PulisciStrSql(CType(Dic_Nucleo1.FindControl("txtCOMUNE"), HiddenField).Value.ToUpper) & "'," _
                            '& "'" & CType(Dic_Nucleo1.FindControl("txtCAP"), HiddenField).Value & "','" & CType(Dic_Nucleo1.FindControl("txtDOCIDENT"), HiddenField).Value.ToUpper & "'," _
                            '& "'" & par.AggiustaData(CType(Dic_Nucleo1.FindControl("txtDATADOC"), HiddenField).Value) & "','" & CType(Dic_Nucleo1.FindControl("txtRILASCIO"), HiddenField).Value.ToUpper & "'," _
                            '& "'" & CType(Dic_Nucleo1.FindControl("txtSOGGIORNO"), HiddenField).Value.ToUpper & "','" & par.AggiustaData(CType(Dic_Nucleo1.FindControl("txtDATASogg"), HiddenField).Value) & "'," _
                            '& "'" & CType(Dic_Nucleo1.FindControl("txtREFERENTE"), HiddenField).Value & "')"
                            'par.cmd.ExecuteNonQuery()

                            'newComp = 1
                        Else
                            If par.IfEmpty(CType(Dic_Nucleo1.FindControl("txtID"), HiddenField).Value, -1) = idComponenti(i) Then
                                sStringaSql = "UPDATE NUOVI_COMP_NUCLEO_VSA set " _
                                & "DATA_INGRESSO_NUCLEO='" & par.PulisciStrSql(par.AggiustaData(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox1"), ListBox).Items(i).Text, 144, 10))) & "'," _
                                & "ID_TIPO_IND_RES_DNTE=" & CType(Dic_Nucleo1.FindControl("txtidTipoVIA"), HiddenField).Value & "," _
                                & "IND_RES_DNTE='" & par.PulisciStrSql(CType(Dic_Nucleo1.FindControl("txtVIA"), HiddenField).Value.ToUpper) & "'," _
                                & "CIVICO_RES_DNTE='" & CType(Dic_Nucleo1.FindControl("txtCIVICO"), HiddenField).Value & "'," _
                                & "COMUNE_RES_DNTE='" & par.PulisciStrSql(CType(Dic_Nucleo1.FindControl("txtCOMUNE"), HiddenField).Value.ToUpper) & "'," _
                                & "CAP_RES_DNTE='" & CType(Dic_Nucleo1.FindControl("txtCAP"), HiddenField).Value & "'," _
                                & "CARTA_I='" & CType(Dic_Nucleo1.FindControl("txtDOCIDENT"), HiddenField).Value.ToUpper & "'," _
                                & "CARTA_I_DATA='" & par.AggiustaData(CType(Dic_Nucleo1.FindControl("txtDATADOC"), HiddenField).Value) & "', " _
                                & "CARTA_I_RILASCIATA='" & CType(Dic_Nucleo1.FindControl("txtRILASCIO"), HiddenField).Value.ToUpper & "', " _
                                & "PERMESSO_SOGG_N='" & CType(Dic_Nucleo1.FindControl("txtSOGGIORNO"), HiddenField).Value.ToUpper & "', " _
                                & "PERMESSO_SOGG_DATA='" & par.AggiustaData(CType(Dic_Nucleo1.FindControl("txtDATASogg"), HiddenField).Value) & "', " _
                                & "FL_REFERENTE='" & CType(Dic_Nucleo1.FindControl("txtREFERENTE"), HiddenField).Value & "' " _
                                & "WHERE ID_COMPONENTE=" & idComponenti(i)
                                par.cmd.CommandText = sStringaSql
                                par.cmd.ExecuteNonQuery()
                            End If
                        End If
                        myReaderN.Close()

                    End If

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


            par.cmd.CommandText = "SELECT COUNT(ID_COMPONENTE) AS NUM_REFE FROM NUOVI_COMP_NUCLEO_VSA,COMP_NUCLEO_VSA WHERE NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE = COMP_NUCLEO_VSA.ID AND " _
                & "COMP_NUCLEO_VSA.ID_DICHIARAZIONE =" & lIdDichiarazione & " AND FL_REFERENTE = 1 GROUP BY (ID_COMPONENTE)"
            Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderR.Read Then
                If myReaderR("NUM_REFE") > 1 Then
                    Response.Write("<script>alert('Attenzione...il nuovo nucleo non può avere più di un referente!')</script>")
                    Exit Try
                End If
            End If
            myReaderR.Close()

            'FL_UBICAZIONE='" & Valore01(CType(Dic_Patrimonio1.FindControl("CHUBICAZIONE"), CheckBox).Checked) _

            sStringaSql = ""
            sStringaSql = "UPDATE DICHIARAZIONI_VSA SET TIPO=" & Valore01(ChFO.Checked) & "," _
                      & "PG='" & lblPG.Text & "'" _
                      & ",DATA_PG='" _
                      & par.AggiustaData(txtDataPG.Text) & "',LUOGO='Milano',DATA='" _
                      & par.AggiustaData(CType(Dic_Dichiarazione1.FindControl("txtData1"), TextBox).Text) _
                      & "',NOTE='" & par.PulisciStrSql(CType(Dic_Note1.FindControl("txtNote"), TextBox).Text) _
                      & "',ID_STATO=" & cmbStato.SelectedItem.Value _
                      & ",N_COMP_NUCLEO=" & NUM_PARENTI & ",N_INV_100_CON=" & N_INV_100_ACC _
                      & ",N_INV_100_SENZA=" & N_INV_100_NO_ACC _
                      & ",N_INV_100_66=" & N_INV_100_66 _
                      & ",ANNO_SIT_ECONOMICA=" & Val(CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text) _
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



            'Dim ID_TIPO As Integer
            'Dim RESIDENZA As String


            'For i = 0 To CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items.Count - 1
            '    For j = 0 To cmbComp.Items.Count - 1
            '        If cmbComp.Items(j).Value = CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Value Then
            '            INDICE = j
            '            Exit For
            '        End If
            '    Next

            '    Select Case par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 27, 20)
            '        Case "FABBRICATI"
            '            ID_TIPO = 0
            '        Case "TERRENI AGRICOLI"
            '            ID_TIPO = 1
            '        Case "TERRENI EDIFICABILI"
            '            ID_TIPO = 2
            '    End Select

            '    Select Case par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 79, 2)
            '        Case "SI"
            '            RESIDENZA = "1"
            '        Case Else
            '            RESIDENZA = "0"
            '    End Select

            '    sStringaSql = "INSERT INTO COMP_PATR_IMMOB_VSA (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA) VALUES " _
            '                & " (SEQ_COMP_PATR_IMMOB_VSA.NEXTVAL," & idComponenti(INDICE) & "," & ID_TIPO _
            '                & "," & par.VirgoleInPunti(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 48, 6)) _
            '                & "," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 55, 8)) _
            '                & "," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 67, 8)) _
            '                & ",'" & RESIDENZA & "')"
            '    par.cmd.CommandText = sStringaSql
            '    par.cmd.ExecuteNonQuery()
            'Next i

            'MODIFICA 27/01/2012 DA MASSIMILIANO
            Dim ID_TIPO As Integer
            Dim RESIDENZA As String
            Dim FL_70KM As String = "0"
            Dim PienaP As Integer = 0

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

                If par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 138, 2) = "SI" Then
                    PienaP = 1
                Else
                    PienaP = 0
                End If

                RESIDENZA = "0"

                '    Select Case par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 79, 2)
                '        Case "SI"
                '            RESIDENZA = "1"
                '        Case Else
                '            RESIDENZA = "0"
                'End Select

                FL_70KM = "0"
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 102, 20)) & "'"
                Dim myReaderCC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCC.Read() Then
                    If myReaderCC("ENTRO_70KM") = "1" Then
                        FL_70KM = "1"
                    End If
                End If
                myReaderCC.Close()

                'par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 86, 30))
                sStringaSql = "INSERT INTO COMP_PATR_IMMOB_VSA (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA,CAT_CATASTALE,COMUNE,N_VANI,SUP_UTILE,FL_70KM,PIENA_PROPRIETA,REND_CATAST_DOMINICALE) VALUES " _
                                & " (SEQ_UTENZA_COMP_PATR_IMMOB.NEXTVAL," & idComponenti(INDICE) & "," & ID_TIPO _
                                & "," & par.VirgoleInPunti(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 48, 7)) _
                                & "," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 73, 15)) _
                                & "," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 84, 19)) _
                                & ",'" & RESIDENZA _
                                & "','" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 56, 3) _
                                & "','" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 102, 20)) _
                                & "','" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 123, 2) _
                                & "','" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 126, 7) _
                                & "','" & FL_70KM & "'," & PienaP & "," & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 60, 8) & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            Next i
            'FINE MODIFICA 27/01/2012 DA MASSIMILIANO


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

                If CType(Dic_Sottoscrittore1.FindControl("txtCognome"), TextBox).Text = "" Or CType(Dic_Sottoscrittore1.FindControl("txtNome"), TextBox).Text = "" Then
                    Response.Write("<script>alert('Attenzione...Mancanza di dati nel Tab Sottoscrittore!')</script>")
                    Exit Try
                End If

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

            '-------------- 23/03/2012 BLOCCO SPOSTATO SOTTO
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


            '****** 09/03/2012 INSERIMENTO DOC ALLEGATI *******
            Dim docAllegati As Boolean = False
            For i = 0 To CType(Dic_Note1.FindControl("chkListDocumenti"), CheckBoxList).Items.Count - 1
                If CType(Dic_Note1.FindControl("chkListDocumenti"), CheckBoxList).Items(i).Selected = True Then
                    CType(Dic_Note1.FindControl("documAlleg"), HiddenField).Value = 1
                    par.cmd.CommandText = "SELECT * FROM VSA_DOC_ALLEGATI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND ID_DOC=" & CType(Dic_Note1.FindControl("chkListDocumenti"), CheckBoxList).Items(i).Value & ""
                    Dim lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lett.Read = False Then
                        par.cmd.CommandText = "INSERT INTO VSA_DOC_ALLEGATI (ID_DICHIARAZIONE,ID_DOC) VALUES (" & lIdDichiarazione & "," & CType(Dic_Note1.FindControl("chkListDocumenti"), CheckBoxList).Items(i).Value & ")"
                        par.cmd.ExecuteNonQuery()
                        docAllegati = True
                    End If
                    lett.Close()
                Else
                    par.cmd.CommandText = "SELECT * FROM VSA_DOC_ALLEGATI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND ID_DOC=" & CType(Dic_Note1.FindControl("chkListDocumenti"), CheckBoxList).Items(i).Value & ""
                    Dim lett2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lett2.Read Then
                        par.cmd.CommandText = "DELETE FROM VSA_DOC_ALLEGATI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND ID_DOC=" & CType(Dic_Note1.FindControl("chkListDocumenti"), CheckBoxList).Items(i).Value & ""
                        par.cmd.ExecuteNonQuery()
                        CType(Dic_Note1.FindControl("documAlleg"), HiddenField).Value = 0

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



            'par.cmd.CommandText = "DELETE FROM VSA_DOC_MANCANTi WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            'par.cmd.ExecuteNonQuery()

            'For m As Integer = 0 To CType(Dic_Note1.FindControl("listbox1"), ListBox).Items.Count - 1
            '    CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 1
            '    par.cmd.CommandText = "INSERT INTO VSA_DOC_MANCANTi (ID_DICHIARAZIONE,ID_DOC,DESCRIZIONE) VALUES (" & lIdDichiarazione & "," & CType(Dic_Note1.FindControl("listbox1"), ListBox).Items(m).Value & ",'" & par.PulisciStrSql(CType(Dic_Note1.FindControl("listbox1"), ListBox).Items(m).Text) & "')"
            '    par.cmd.ExecuteNonQuery()
            'Next

            'If CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 1 Then
            '    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_vsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
            '        & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
            '        & "','F193','','')"
            '    par.cmd.ExecuteNonQuery()
            'End If

            Dim dt As New Data.DataTable

            If Dic_Note1.idCOMPMOB <> "" Then
                par.cmd.CommandText = "SELECT * FROM VSA_DOC_MANCANTI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & Dic_Note1.idCOMPMOB & ")"
                Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReaderN.Read
                    par.cmd.CommandText = "DELETE FROM VSA_DOC_MANCANTI WHERE ID_DOC=" & myReaderN("ID_DOC")
                    par.cmd.ExecuteNonQuery()
                    CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 0
                End While
                myReaderN.Close()
            End If

            For i = 0 To CType(Dic_Note1.FindControl("listbox1"), ListBox).Items.Count - 1
                par.cmd.CommandText = "SELECT * FROM VSA_DOC_MANCANTI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND ID_DOC=" & CType(Dic_Note1.FindControl("listbox1"), ListBox).Items(i).Value
                Dim lettDocManc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettDocManc.Read = False Then
                    par.cmd.CommandText = "INSERT INTO VSA_DOC_MANCANTI (ID_DICHIARAZIONE,ID_DOC,DESCRIZIONE) VALUES (" & lIdDichiarazione & "," & CType(Dic_Note1.FindControl("listbox1"), ListBox).Items(i).Value & ",'" & par.PulisciStrSql(CType(Dic_Note1.FindControl("listbox1"), ListBox).Items(i).Text) & "')"
                    par.cmd.ExecuteNonQuery()
                    docMancanti = True
                    CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 1
                Else
                    CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 1
                End If
                lettDocManc.Close()
            Next
            If docMancanti = True Then
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

            If CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 1 Then
                CType(Dic_Note1.FindControl("chkDocManc"), CheckBox).Enabled = True
                CType(Dic_Note1.FindControl("txtDataDocManc"), TextBox).Enabled = True

            Else
                CType(Dic_Note1.FindControl("chkDocManc"), CheckBox).Enabled = False
                CType(Dic_Note1.FindControl("txtDataDocManc"), TextBox).Enabled = False
            End If

            If CType(Dic_Note1.FindControl("txtDataDocManc"), TextBox).Text <> "" And CType(Dic_Note1.FindControl("chkDocManc"), CheckBox).Checked = False Then
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert(''ATTENZIONE, impossibile procedere! Valorizzare il flag relativo alla presentazione della documentaz. mancante, altrimenti cancellare la data!');" _
                & "</script>"
                Response.Write(scriptblock)
                Exit Try
            End If

            If CType(Dic_Note1.FindControl("chkDocManc"), CheckBox).Checked = True Then
                If CType(Dic_Note1.FindControl("txtDataDocManc"), TextBox).Text <> "" Then
                    If Not par.ControllaData(CType(Dic_Note1.FindControl("txtDataDocManc"), TextBox)) Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('ATTENZIONE, Inserire una data valida!');" _
                        & "</script>"
                        Response.Write(scriptblock)
                        Exit Try
                    Else
                        If DateDiff(DateInterval.Day, CDate(par.FormattaData(dataDocManc)), CDate(par.FormattaData(Format(Now, "yyyyMMdd")))) <= 30 Then
                            par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET FL_PRESENT_DOC_MANC = 1,DATA_PRESENT_DOC_MANC='" & par.AggiustaData(CType(Dic_Note1.FindControl("txtDataDocManc"), TextBox).Text) & "'" _
                                & " WHERE ID=" & lIdDomanda
                            par.cmd.ExecuteNonQuery()
                            CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 0
                            'CType(Dic_Note1.FindControl("ListBox1"), ListBox).Enabled = False
                            CType(Dic_Note1.FindControl("Button1"), Button).Enabled = False
                            CType(Dic_Note1.FindControl("Button2"), Button).Enabled = False
                            'CType(Dic_Note1.FindControl("chkDocManc"), CheckBox).Enabled = False
                            'CType(Dic_Note1.FindControl("txtDataDocManc"), TextBox).ReadOnly = True
                        Else
                            scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "alert('ATTENZIONE, impossibile procedere! Documentazione mancante non integrata entro il termine di 30 gg!');" _
                            & "</script>"
                            Response.Write(scriptblock)
                            Exit Try
                        End If
                    End If
                Else
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('ATTENZIONE, indicare la data in cui è stata presentata la documentazione mancante, altrimenti togliere il flag relativo!');" _
                    & "</script>"
                    Response.Write(scriptblock)
                    Exit Try
                End If
            Else
                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET FL_PRESENT_DOC_MANC = 0,DATA_PRESENT_DOC_MANC=''" _
                & " WHERE ID=" & lIdDomanda
                par.cmd.ExecuteNonQuery()
                CType(Dic_Note1.FindControl("Button1"), Button).Enabled = True
                CType(Dic_Note1.FindControl("Button2"), Button).Enabled = True
            End If

            'controllo che non siano trascorsi più di 30 gg per la presentaz. dei documenti


            '--------- 23/03/2012 Alert per Documentazione Allegata!! -----------
            If cmbStato.SelectedValue = "1" Then
                If par.IfNull(tipoCausale.Value, "") <> "30" Then
                    If CType(Dic_Note1.FindControl("documAlleg"), HiddenField).Value = 0 Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "alert('ATTENZIONE, indicare la documentazione da allegare alla richiesta prima di procedere!');" _
                            & "</script>"
                        Response.Write(scriptblock)
                        Exit Try
                    End If
                End If

                If totconv <> TotISEE Then
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                                        & "alert('ATTENZIONE, ci sono delle incongruenze nella dichiarazione dei redditi convenzionali e Isee. In questi casi il calcolo dell ISEE non è attendibile!');" _
                                        & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript30023")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript30023", scriptblock)
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
                        lblElaborare.Visible = True
                        lblElaborare.Text = "ELABORARE TRAMITE IL PULSANTE " & Chr(34) & "Elabora" & Chr(34) & ""
                    End If
                    'scriptblock = "<script language='javascript' type='text/javascript'>" _
                    '            & "alert('ATTENZIONE, La domanda " & myReader("PG") & " deve essere nuovamente elaborata e stampata!');" _
                    '            & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript29")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript29", scriptblock)
                    End If
                Else
                    lblElaborare.Text = ""
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "alert('ATTENZIONE, Questa dichiarazione e la domanda " & myReader("PG") & " saranno cancellate!');" _
                                & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript300")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript300", scriptblock)
                    End If

                End If

            End If
            myReader.Close()

            For Each Menu As MenuItem In MenuStampe.Items
                Menu.ChildItems.Clear()
            Next
            If Request.QueryString("GLocat") = "" Then
                SetMenuStampe(tipoRichiesta.Value)
            End If




            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessDICH, par.myTrans)

            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            myReader = par.cmd.ExecuteReader()
            myReader.Close()

            'Dic_Note1.CaricaLista()

            If Request.QueryString("GLocat") <> "" Then
                imgStampa.Enabled = True
                imgStampa.ImageUrl = "../NuoveImm/Img_Stampa.png"
                bMemorizzato = True
            Else
                imgStampa.Enabled = True
                imgStampa.ImageUrl = "../NuoveImm/Img_Stampa.png"
                If tipoRichiesta.Value = "3" Then
                    btnApplica.Visible = True
                    btnApplica.Enabled = True
                End If
                bMemorizzato = True
            End If

            If Session.Item("ANAGRAFE") = "1" Then
                'imgAnagrafe.Attributes.Clear()
                imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('../Anagrafe.aspx?ID=" & par.criptamolto(lIdDichiarazione) & "&T=4','Anagrafe','top=0,left=0,width=600,height=400');")
            End If


            If tipoRichiesta.Value <> "3" Then
                If bMemorizzato = True Then
                    If cmbStato.SelectedValue <> "2" Then
                        If cmbStato.SelectedItem.Text <> "COMPLETA" Then
                            Response.Write("<script>alert('Attenzione...Lo stato della dichiarazione deve essere COMPLETA!')</script>")
                            Exit Sub
                        End If
                        Session.Item("STAMPATO") = "1"
                        If Request.QueryString("GLocat") = "" Then
                            CalcolaISEEDomanda()
                        End If
                        Response.Write("<script>alert('Elaborazione effettuata!')</script>")
                    End If
                End If
            End If


            Me.txtModificato.Value = 0

        Catch EX As Exception
            'Label4.Text = EX.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessDICH, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

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
                    RiempiCmbAnniRedd(sAnnoIsee)

                    CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = sAnnoIsee
                    CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = Val(sAnnoIsee)
                    CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = sAnnoIsee
                    CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = sAnnoIsee
                    CType(Dic_Reddito_Conv1.FindControl("Label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA " & sAnnoIsee

                    lIndice_Bando = myReader(2)
                    lBando = par.IfNull(myReader("TIPO_BANDO"), "-1")
                    'Select Case lBando
                    '    Case 0
                    '        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-1"
                    '    Case 1
                    '        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-2"
                    '    Case 2
                    '        lblSPG.Text = Mid(myReader("DATA_INIZIO"), 3, 2) & "-3"
                    'End Select
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

            lblAvviso.Visible = False
            imgAlert.Visible = False

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

            'ANTONELLO MODIFY 30/08/2013 - TRANSAZIONE SPOSTATA X EVITARE CASO RECORD NULL DB
            HttpContext.Current.Session.Add(lIdConnessDICH, par.OracleConn)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessDICH, par.myTrans)

            cmbAnnoReddituale.SelectedIndex = -1
            cmbAnnoReddituale.Items.FindByValue(sAnnoIsee).Selected = True

            CType(Dic_Reddito_Conv1.FindControl("label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA " & cmbAnnoReddituale.SelectedItem.Value
            CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
            CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
            CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
            CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = cmbAnnoReddituale.SelectedItem.Value

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

        Catch ex1 As Oracle.DataAccess.Client.OracleException
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('Errore : " & ex1.Message & "');" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript40")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript40", scriptblock)
            End If
            'Label4.Text = ex1.Message
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex1.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        Catch ex As Exception
            scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('Errore : " & ex.Message & "');" _
                        & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript40")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript40", scriptblock)
            End If
            'Label4.Text = ex.Message
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        Finally

        End Try
    End Function

    Protected Sub imgStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStampa.Click

        If Request.QueryString("GLocat") <> "" Then
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
                Me.lblElaborare.Visible = False
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
            'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            'par.SettaCommand(par)
            'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            '‘‘par.cmd.Transaction = par.myTrans

            par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessDICH), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessDICH), Oracle.DataAccess.Client.OracleTransaction)
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
            Dim pienapropr As String = ""
            For i = 0 To CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items.Count - 1
                pienapropr = par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 138, 2)
                If pienapropr <> "" Then
                    pienapropr = Replace(pienapropr, pienapropr, "X")
                End If
                PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                                   & "<tr>" _
                                   & "<td><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 1, 25) & "</I>   </center></small></small></td>" _
                                   & "<td><small><small>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 27, 20) & "</I>   </small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 48, 7) & "</I>   %   </p></small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 60, 8) & ",00</I>    </p></small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 73, 15) & ",00</I>   </p></small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 84, 19) & ",00</I>   </p></small></small></td>" _
                                   & "<td><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 56, 3) & "</I>   </small></small></td>" _
                                   & "<td><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 102, 20) & "</I>   </small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 123, 2) & "</I>   </small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 126, 7) & "</I>   </small></small></td>" _
                                   & "<td><small><small><center>   <I>" & par.IfEmpty(pienapropr, "&nbsp") & "</center><I></I>   </small></small></td>" _
                                   & "</tr>"
            Next i

            '& "<td><small><small>   <I></I><center>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 79, 2) & "</center><I></I>   </small></small></td>" _


            CAT_CATASTALE = ""
            'If CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = True Then
            '    CAT_CATASTALE = "Categoria catastale dell'immobile ad uso abitativo del nucleo : " & CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).SelectedItem.Text & "<br/>"
            'Else
            '    CAT_CATASTALE = "Categoria catastale dell'immobile ad uso abitativo del nucleo : ---<br/>"
            'End If


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

            'numero = lblSPG.Text & "-" & lblPG.Text & "-" & Label7.Text & " del " & Format(Now, "dd/MM/yyyy")
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
                & "<td  width=10% bgcolor=#C0C0C0><center><small><small>COMUNE</small></small></center></td><td width=10% bgcolor=#C0C0C0><center><small><small>NUM.VANI</small></small></center></td><td width=10% bgcolor=#C0C0C0><center><small><small>SUP.UTILE</small></small></center></td><td width=10% bgcolor=#C0C0C0><center><small><small>PIENA PROPRIETA'</small></small></center></td></tr>" _
                                      & "<UL><UL>   <NOBR></NOBR>" & PATRIMONIO_IMMOB & "<BR>"

            sStringasql = sStringasql & "</table></center>" _
                                      & "<br><ul>   </ul><NOBR></NOBR><center><table cellspacing=0 border=0  width=100%><tr><td width=100%><small><p align=left><I>" & CAT_CATASTALE & "</I></p></small></td></tr></table></center>   <br><br></small></td></tr></table></center><p style='page-break-before: always'>&nbsp;</p><center><table  cellspacing=0 border=1 width=95%><tr><td><small>   <B>QUADRO D: REDDITO DEI COMPONENTI DEL NUCLEO FAMILIARE</B>   <BR><BR>"
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
            Response.Write("<script>window.open('StampaDichiarazione.aspx','StmpDichiarazione','');</script>")


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
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessDICH, par.myTrans)

            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader.Close()

        Catch ex As Exception
            'Label4.Text = "err " & ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessDICH, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        Finally

        End Try
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

            par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessDICH), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessDICH), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans



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


            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessDICH, par.myTrans)




        Catch ex As Exception
            Label10.Visible = True
            Label10.Text = "err " & ex.ToString
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessDICH, par.myTrans)
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
        Dim Da_Calcolare36 As Boolean

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

        Label10.Visible = True
        lblDomAssociata.Visible = True

        Label10.Text = "ISEE"
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

    Protected Sub cmbAnnoReddituale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAnnoReddituale.SelectedIndexChanged
        Response.Write("<script>Uscita=1;</script>")

        imgStampa.Attributes.Add("onclick", "javascript:Uscita=1;")
        CType(Dic_Reddito_Conv1.FindControl("label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA " & cmbAnnoReddituale.SelectedItem.Value
        CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
        CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
        CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
        CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = cmbAnnoReddituale.SelectedItem.Value

    End Sub

    Protected Sub imgVaiDomanda_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgVaiDomanda.Click
        Try
            If Session.Item("LAVORAZIONE") = "1" Then
                If Fl_Integrazione = "1" Then
                    H1.Value = "1"
                End If
                If Session.Item("STAMPATO") = "0" And cmbStato.Text = "0" Then
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "alert('ATTENZIONE, La Dichiarazione deve essere completa!');" _
                                & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript31")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript31", scriptblock)
                    End If
                    Exit Sub
                End If
                If Session.Item("STAMPATO") = "0" And cmbStato.Text = "1" Then
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "alert('ATTENZIONE, La Dichiarazione deve essere elaborata!');" _
                                & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript31")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript31", scriptblock)
                    End If
                    Exit Sub
                End If
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.OracleConn.Close()
                End If
                Dim idDomanda As String = ""
                If Fl_US <> "1" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessDICH), Oracle.DataAccess.Client.OracleConnection)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessDICH), Oracle.DataAccess.Client.OracleTransaction)
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
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessDICH)
                HttpContext.Current.Session.Remove(lIdConnessDICH)
                Session.Item("LAVORAZIONE") = "0"
                Session.Remove("STAMPATO")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Page.Dispose()
                If Fl_Integrazione = "1" Or Fl_US = "1" Then
                    Session.Item("LAVORAZIONE") = "1"
                    Response.Write("<script>window.close();</script>")
                Else
                    'apro domanda
                    If idDomanda <> "" Then
                        Response.Write("<script>location.replace('domanda.aspx?ID=" & idDomanda & "&ID1=" & lIdDichiarazione & "&PROGR=-1&INT=0&CH=" & Request.QueryString("CH") & "','','');</script>")
                    Else
                        Response.Write("<script>alert('Impossibile aprire la domanda!')</script>")
                    End If
                End If
            Else
                Session.Item("LAVORAZIONE") = "0"
                Page.Dispose()
                If Fl_Integrazione = "1" Or Fl_US = "1" Then
                    Session.Item("LAVORAZIONE") = "1"
                    Response.Write("<script>window.close();</script>")
                Else
                    '
                    Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                End If
            End If

        Catch EX As Exception
            Session.Item("LAVORAZIONE") = "0"
            Page.Dispose()
            Response.Write("<script>document.location.href=""../ErrorPage.aspx""</script>")
        Finally
        End Try
    End Sub

    Protected Sub MenuStampe_MenuItemClick(sender As Object, e As System.Web.UI.WebControls.MenuEventArgs) Handles MenuStampe.MenuItemClick

    End Sub


    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete

    End Sub

    Protected Sub btnApplica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnApplica.Click
        Call btnSalva_Click(sender, e)
        If bMemorizzato = True Then
            If cmbStato.SelectedItem.Text <> "COMPLETA" Then
                Response.Write("<script>alert('Attenzione...Lo stato della dichiarazione deve essere COMPLETA!')</script>")
                Exit Sub
            End If

            Session.Item("STAMPATO") = "1"
            If Request.QueryString("GLocat") = "" Then
                CalcolaISEEDomanda()
            End If

            lblElaborare.Visible = False
            Response.Write("<script>alert('Elaborazione effettuata!')</script>")
            btnApplica.Visible = False
            'imgStampa.Enabled = True
            'imgStampa.ImageUrl = "../NuoveImm/Img_Stampa.png"
        Else
            btnApplica.Enabled = False
            btnApplica.Visible = False
        End If
        
    End Sub

    Protected Sub btnVisualizzaDich_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizzaDich.Click
        CType(Dic_Note1.FindControl("chkListDocumenti"), CheckBoxList).Items.Clear()
        Dic_Note1.CaricaLista()
    End Sub

End Class
