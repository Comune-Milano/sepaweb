
Imports Telerik.Web.UI

Partial Class CENSIMENTO_InserimentoEdifici
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public classetab As String = ""
    Public tabdefault1 As String = ""
    Public tabdefault2 As String = ""
    Public tabdefault3 As String = ""
    Public tabdefault4 As String = ""
    Public tabdefault5 As String = ""
    Public tabdefault6 As String = ""
    Public tabdefault7 As String = ""
    Public tabdefault8 As String = ""
    Public tabdefault9 As String = ""
    Public tabdefault10 As String = ""
    Public tabdefault11 As String = ""
    Public tabdefault12 As String = ""
    Public tabdefault13 As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If vId <> 0 Then
                classetab = "tabbertab"
            Else
                classetab = "tabbertabhide"
            End If
            Dim Str As String

            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='IMMCENSIMENTO/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"

            Response.Write(Str)

            'vIdEdificio = Request.QueryString("ID")
            'If vIdEdificio <> 0 Then

            '    Me.Riempicampi()
            '    ApriRicerca()
            'End If
            If Session.Item("OPERATORE") = "" Then
                Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            End If

            If Not IsPostBack Then
                Response.Flush()
                'PEPPE MODIFY PER BLOCCARE INSERIMENTO CARATTERI SPECIALI
                Me.TxtScala.Attributes.Add("onBlur", "javascript:valid(this,'special');valid(this,'quotes');")

                vId = Request.QueryString("ID")
                If vId <> 0 Then
                    classetab = "tabbertab"
                Else
                    classetab = "tabbertabhide"
                End If
                TxtDenEdificio.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtNote.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                TxtDescrInd.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                TxtLocalità.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtpCarrabili.Attributes.Add("onkeyUp", "javascript:valid(this,'notnumbers');")
                txtScCostoBase.Attributes.Add("onkeyUp", "javascript:valid(this,'onlynumbers');")
                txtUnitaNP.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")

                Session.Add("MODIFICASOTTOFORM", 0)


                If vId <> 0 Then
                    Me.Riempicampi()
                    ApriRicerca()
                    CaricaGridEdifici()
                Else
                    Me.Riempicampi()
                    'Apro la connessione che resterà valida per tutti i metodi delle sottofinestre e del salva
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
                End If
                'Controllo modifica campi nel form
                Dim CTRL As Control
                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        If DirectCast(CTRL, DropDownList).ID <> "DrlSchede" And DirectCast(CTRL, DropDownList).ID <> "cmbPeriodo" And DirectCast(CTRL, DropDownList).ID <> "cmbScale" Then
                            DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                        End If
                    End If
                Next
                'Me.TxtDenComplesso.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                CaricaAttributi()

                'FINE DEL CICLO
                TxtDataCostr.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
                TxtDataRistrut.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
                ' ''********************************************************************************************
                TxtDataCostr.Attributes.Add("onfocus", "javascript:selectText(this);")
                TxtDataRistrut.Attributes.Add("onfocus", "javascript:selectText(this);")
                '*********************************************************************************************
                TxtDataCostr.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                TxtDataRistrut.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


                If Request.QueryString("SLE") = 1 Or Session.Item("SLE") = 1 Then
                    FrmSolaLettura()
                    Me.btnIndietro.Visible = False
                    'CType(Me.Tab_ComEdifici1.FindControl("btnNewEdi"), ImageButton).Visible = False
                    'CType(Me.Tab_UnComuni1.FindControl("btnNuovoUC"), ImageButton).Visible = False
                    Session.Add("SLE", 1)
                End If


                If Session("PED2_SOLOLETTURA") = "1" Then
                    FrmSolaLettura()
                    If par.IfNull(Session.Item("MOD_CENS_MANUT"), 0) > 0 Then
                        Me.DrlSchede.Enabled = True
                        'Me.btnrilievo.Visible = True
                        Me.cmbPeriodo.Enabled = True
                    End If
                    If Session.Item("CENS_MANUT_SL") > 0 Then
                        Me.CENS_MANUT_SL.Value = 1
                    End If
                End If
                If Session.Item("SLE") = "1" And IsNothing(Request.QueryString("SLE")) = True Then
                    FrmSolaLettura()
                    Me.btnIndietro.Visible = True
                End If



                If Session("ID_CAF") <> "6" Then
                    FrmSolaLettura()
                    Session.Add("SLE", 1)
                    Session("PED2_SOLOLETTURA") = "1"
                    If par.IfNull(Session.Item("MOD_CENS_MANUT"), 0) > 0 Then
                        Me.DrlSchede.Enabled = True
                        Me.cmbPeriodo.Enabled = True
                    End If
                    If Session.Item("CENS_MANUT_SL") > 0 Then
                        Me.CENS_MANUT_SL.Value = 1
                    End If
                End If

                If dbLock.Value = 0 And Session.Item("ID_CAF") = 2 _
                        And Request.QueryString("SLE") <> "1" Then
                    Me.btnSalva.Visible = True
                    Me.cmbBuildingManager.Enabled = True
                End If

            End If
            'Me.txtindietro.Text = txtindietro.Text - 1

            NumeroScale()

            VerificaModificheSottoform()
            'txtindietro.Text = txtindietro.Text - 1
            'max 20/04/2018
            VeririfcaOSMI()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()
        End Try



    End Sub
    'max 20/04/2018
    Private Sub VeririfcaOSMI()
        Dim APERTORA As Boolean = False
        Try
            If Not Session.Item("TRANSAZIONE") Is Nothing Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
            Else
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        APERTORA = True
                    End If
                    par.SettaCommand(par)
                End If
            End If
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "select descrizione from SISCOM_MI.TAB_ZONA_OSMI where id=(select id_osmi from siscom_mi.edifici where id=" & Me.vId & ")"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                lblOSMI.Visible = True
                lblOSMI.Text = "Z. OSMI: " & par.IfNull(myReader(0), "")
            Else
                lblOSMI.Visible = False
                lblOSMI.Text = ""
            End If
            myReader.Close()

            If APERTORA = True Then
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Public Sub VerificaModificheSottoform()
        'Se vengono effettuate modifiche nei sotto-form questo manda il messaggio in casa di uscita senza salvataggio
        If Session.Item("MODIFICASOTTOFORM") = 1 Then
            Me.txtModificato.Value = 1
            Session.Item("MODIFICASOTTOFORM") = 0
        End If

    End Sub

    Private Sub NumeroScale()
        Try


            Dim APERTORA As Boolean = False

            '***PER AVERE IL NUMERO SCALE AGGIORNATO SEMPRE!****
            'Richiamo la connessione
            If vId <> 0 Then

                If Not Session.Item("TRANSAZIONE") Is Nothing Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    
                Else
                    If par.OracleConn.State = Data.ConnectionState.Closed Then

                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                        If par.OracleConn.State = Data.ConnectionState.Closed Then
                            par.OracleConn.Open()
                            APERTORA = True
                        End If
                        par.SettaCommand(par)
                    End If

                End If

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "select count(*) from SISCOM_MI.scale_edifici where id_edificio = " & Me.vId
                myReader = par.cmd.ExecuteReader
                If myReader.Read AndAlso myReader(0) <> 0 Then
                    Me.TxtNumScale.Text = myReader(0)
                End If
                myReader.Close()
                If APERTORA = True Then
                    par.OracleConn.Close()
                    par.cmd.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub FrmSolaLettura()
        Try
            Me.btnSalva.Visible = False
            DirectCast(Me.Tab_CPI1.FindControl("Attivita"), CheckBoxList).Enabled = False
            Me.txtVisibility.Value = 1
            BtnADD.Visible = False
            btnAddProgrInt.Visible = False
           

            Dim CTRL As Control = Nothing

            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadComboBox Then
                    DirectCast(CTRL, RadComboBox).Enabled = False
                ElseIf TypeOf CTRL Is RadNumericTextBox Then
                    DirectCast(CTRL, RadNumericTextBox).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
               
                End If
            Next
            dgvScaleEdifici.Enabled = False
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub NascondiCampi()

        Me.Label30.Visible = True
        Me.TxtGimi.Visible = True
        'Me.TxtCodComun.ReadOnly = True

    End Sub

    Private Sub Riempicampi()
        '**************************************************************
        Try
            LoadDrlSchede()
            'Apro la CONNESSIONE  con il DB PER RIEMPIRE I CAMPI (Combo, textbox...ecc...)
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If Request.QueryString("SK") <> "0" Then
                SelezionaSkeda(Request.QueryString("SK"))
            End If
            'Controllo sull'utenza
            If Session("PED2_ESTERNA") = "1" Then
                'par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.ID, COMPLESSI_IMMOBILIARI.DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID order by DENOMINAZIONE asc"
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where lotto > 3 and complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "
            Else
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where  complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            DdLComplesso.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                'DdLComplesso.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                DdLComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_complesso"), " "), par.IfNull(myReader1("id"), -1)))

            End While
            myReader1.Close()

            par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.PROGRAMMAZIONE_INTERVENTI WHERE ID IN (SELECT ID_PROGRAMMAZIONE_INTERVENTI FROM SISCOM_MI.RIATTI_APPALTI)", cmbProgrInt, "ID", "DESCRIZIONE", True)
            Dim stringa As String = "select ID, 'Appalto: ' || nome_appalto || ' - Commessa: ' || commessa || ' - DL: ' || direttore_lavori || ' - Ditta: ' || ditta_esecutrice || ' - Data: ' || getdata(data_aggiudicazione) as descrizione from SISCOM_MI.riatti_appalti WHERE ID_PROGRAMMAZIONE_INTERVENTI = " & cmbProgrInt.SelectedValue
            par.caricaComboTelerik(stringa, cmbAppalto, "ID", "DESCRIZIONE", True)
            Dim vIdCOMP As String = Request.QueryString("IDC")
            If vIdCOMP > 0 Then
                Me.DdLComplesso.SelectedValue = vIdCOMP
                Me.DdLComplesso.Enabled = False
            End If

            'Riempio l'oggetto da con quello che restituisce la select
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_EDIFICIO"

            myReader1 = par.cmd.ExecuteReader()
            DrLTipoEdificio.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLTipoEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()

            '**************************************************************
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UTILIZZO_PRINCIPALE_EDIFICIO"
            myReader1 = par.cmd.ExecuteReader()
            DrLUtilizzo.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLUtilizzo.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_COSTRUTTIVA"
            myReader1 = par.cmd.ExecuteReader()
            DrLTipCostrut.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLTipCostrut.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.LIVELLO_POSSESSO"

            myReader1 = par.cmd.ExecuteReader()
            DrLLivelloPoss.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLLivelloPoss.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_IMP_RISCALDAMENTO"

            myReader1 = par.cmd.ExecuteReader()
            DrLImpRiscald.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLImpRiscald.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()

            par.cmd.CommandText = "SELECT COMU_COD, COMU_DESCR FROM sepa.COMUNI order by comu_descr asc"
            myReader1 = par.cmd.ExecuteReader()
            DrLComune.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLComune.Items.Add(New ListItem(par.IfNull(myReader1("COMU_DESCR"), " "), par.IfNull(myReader1("COMU_COD"), -1)))
            End While
            Me.DrLComune.SelectedValue = "F205"
            myReader1.Close()

            If vIdCOMP > 0 Then
                par.cmd.CommandText = "SELECT COD_COMUNE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID = " & vIdCOMP & " AND INDIRIZZI.ID = COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.DrLComune.SelectedValue = par.IfNull(myReader1("COD_COMUNE"), "-1")
                    Me.DrLComune.Enabled = False
                    'Me.TxtCodComun.Text = Me.DrLComune.SelectedValue.ToString
                End If
                myReader1.Close()

            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_INDIRIZZO "

            DrLTipoInd.Items.Add(New ListItem(" ", -1))
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                DrLTipoInd.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()

            par.cmd.CommandText = "select cod, abs(livello) AS LIVELLO from siscom_mi.tipo_livello_piano where descrizione like ('Interrato%') order by livello asc"

            CmbEntroTerra.Items.Add(New ListItem(" ", -1))
            CmbEntroTerra.Items.Add(New ListItem("0", 0))
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                CmbEntroTerra.Items.Add(New ListItem(par.IfNull(myReader1("livello"), " "), par.IfNull(myReader1("livello"), -1)))
            End While
            myReader1.Close()

            par.cmd.CommandText = "select cod, abs(livello) AS LIVELLO from siscom_mi.tipo_livello_piano where descrizione like ('Fuori%') order by livello asc"

            CmbFuoriTerra.Items.Add(New ListItem(" ", -1))
            CmbFuoriTerra.Items.Add(New ListItem("0", 0))
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                CmbFuoriTerra.Items.Add(New ListItem(par.IfNull(myReader1("livello"), " "), par.IfNull(myReader1("livello"), -1)))
            End While
            myReader1.Close()
            '**********CAMPI AGGIUNTI PER FUSIONE MANUTENZIONI
            par.cmd.CommandText = "Select * from SISCOM_MI.tipologie_strutturali"
            myReader1 = par.cmd.ExecuteReader()
            '24/09/2010 TOLTO PERCHe' RIDONDANTE...RICHIESTA DI MARCO
            'cmbTipolStrutt.Items.Add(New ListItem(" ", -1))
            'While myReader1.Read
            '    cmbTipolStrutt.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
            'End While
            'myReader1.Close()

            par.cmd.CommandText = "Select * from SISCOM_MI.stato_ce"
            myReader1 = par.cmd.ExecuteReader()
            cmbStatoFisico.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbStatoFisico.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()

            par.cmd.CommandText = "Select * from SISCOM_MI.TIPOLOGIA_EDILE_1"

            myReader1 = par.cmd.ExecuteReader()
            Me.cmbTipoEdil1.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbTipoEdil1.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()


            par.cmd.CommandText = "Select * from SISCOM_MI.TIPOLOGIA_EDILE_2"

            myReader1 = par.cmd.ExecuteReader()
            Me.cmbTipoEdil2.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbTipoEdil2.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_MICROZONE"
            myReader1 = par.cmd.ExecuteReader
            Me.cmbMicrozona.Items.Add(New ListItem(" ", -1))

            While myReader1.Read
                cmbMicrozona.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()


            par.cmd.CommandText = "SELECT * FROM ZONA_ALER order by zona asc"
            myReader1 = par.cmd.ExecuteReader
            Me.cmbZona.Items.Add(New ListItem(" ", -1))

            While myReader1.Read
                cmbZona.Items.Add(New ListItem(par.IfNull(myReader1("zona"), " "), par.IfNull(myReader1("cod"), -1)))
            End While
            myReader1.Close()


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_RIFERIMENTI_LEG order by descrizione"
            myReader1 = par.cmd.ExecuteReader
            Me.cmbRifLeg.Items.Add(New ListItem(" ", -1))

            While myReader1.Read
                cmbRifLeg.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("cod"), -1)))
            End While
            myReader1.Close()

            'Me.cmbTipoEdil2.Items.Add("  ")
            'Me.cmbTipoEdil2.Items.Add("TORRE")
            'Me.cmbTipoEdil2.Items.Add("LINEA")
            'Me.cmbTipoEdil2.Items.Add("BLOCCO")
            'Me.cmbTipoEdil2.Items.Add("CASA ISOLATA SU LOTTO")
            'Me.cmbTipoEdil2.Items.Add("CASA A SCHIERA")
            'Me.cmbTipoEdil2.Items.Add("CORTE")
            ''*********PEPPE MODIFY 09/09/2010
            'Me.cmbTipoEdil2.Items.Add("BALLATOIO")
            'Me.cmbTipoEdil2.Items.Add("PALAZZINA")

            par.cmd.CommandText = "Select * from SISCOM_MI.TIPOLOGIA_EDILE_3"
            myReader1 = par.cmd.ExecuteReader()
            Me.CmbTipoEdil3.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                CmbTipoEdil3.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()
            'Me.CmbTipoEdil3.Items.Add("  ")
            'Me.CmbTipoEdil3.Items.Add("BIFAMILIARE")
            'Me.CmbTipoEdil3.Items.Add("UNIFAMILIARE")
            ''*********PEPPE MODIFY 22/04/2009
            'Me.CmbTipoEdil3.Items.Add("PLURIFAMILIARE")


            par.cmd.CommandText = "SELECT id, (to_char(to_date(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy')) as DATA_INIZIO,(to_char(to_date(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')) as DATA_FINE  FROM SISCOM_MI.CENSIMENTI_STATO_MANU order BY DATA_INIZIO desc"
            myReader1 = par.cmd.ExecuteReader()
            cmbPeriodo.Items.Add(New ListItem(" ", ""))
            Dim i As Integer = 1
            While myReader1.Read
                If par.IfNull(myReader1("DATA_INIZIO"), "") <> "" Then
                    cmbPeriodo.Items.Add(New ListItem(myReader1("DATA_INIZIO"), i))
                    i = i + 1
                End If
                If par.IfNull(myReader1("DATA_FINE"), "") <> "" Then
                    cmbPeriodo.Items.Add(New ListItem(par.IfNull(myReader1("DATA_FINE"), ""), i))
                    i = i + 1
                End If
            End While
            myReader1.Close()

            If Request.QueryString("DSK") <> "" Then
                Me.cmbPeriodo.Items.FindByText(par.FormattaData(Request.QueryString("DSK"))).Selected = True
            End If

            '***CHIUDO LA CONNESSIONE perchè dopo aprirò quella unica che serve al corretto funzionamento del form sotto transazione!
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        Try
            If Session.Item("SLE") = 0 Or IsNothing(Session.Item("SLE")) Then
                If Me.txtModificato.Value <> "111" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    If Not IsNothing(par.OracleConn) Then
                        If par.OracleConn.State = Data.ConnectionState.Open Then
                            par.OracleConn.Close()
                        End If
                    End If


                    HttpContext.Current.Session.Remove("TRANSAZIONE")
                    HttpContext.Current.Session.Remove("CONNESSIONE")
                    HttpContext.Current.Session.Remove("DT_SCALE_EDIFICI")
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Session.Item("LAVORAZIONE") = 0
                    Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

                Else
                    Me.txtModificato.Value = "1"
                    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
                End If


            Else
                If Not IsNothing(CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)) Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.OracleConn.Close()

                    HttpContext.Current.Session.Remove("TRANSAZIONE")
                    HttpContext.Current.Session.Remove("CONNESSIONE")
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Session.Item("LAVORAZIONE") = 0
                End If
                Response.Write("<script language='javascript'> { self.close() }</script>")
                Session.Remove("SLE")
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub ApriRicerca()

        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim dt As New Data.DataTable
        Dim dt2 As New Data.DataTable
        Dim appoggioDate As String
        Dim scriptblock As String

        If vId <> -1 Then
            Try
                'SE SI PROVIENE DA UNA RICERCA DOPO AVER RIEMPITO I CAMPI SETTO LA MIA CONNESSIONE CHE RESTERà VALIDA PER TUTTA LA DURATA DI APERTURA DEL FORM
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn) 'MEMORIZZAZIONE CONNESSIONE IN VARIABILE DI SESSIONE
                End If


                par.cmd.CommandText = "SELECT EDIFICI.*,(select id_filiale from siscom_mi.complessi_immobiliari ci where ci.id=edifici.id_complesso) as idfiliale FROM SISCOM_MI.EDIFICI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Me.TxtGimi.Text = par.IfNull((dt.Rows(0).Item("COD_EDIFICIO_GIMI")), "- -")
                    Me.DdLComplesso.SelectedValue = (dt.Rows(0).Item("ID_COMPLESSO"))
                    Me.DdLComplesso.Enabled = False
                    appoggioDate = par.IfNull(dt.Rows(0).Item("DATA_COSTRUZIONE"), "dd/mm/YYYY")
                    If appoggioDate <> "dd/mm/YYYY" Then
                        Me.TxtDataCostr.Text = par.FormattaData(appoggioDate)
                    Else
                        Me.TxtDataCostr.Text = ""
                    End If
                    appoggioDate = par.IfNull(dt.Rows(0).Item("DATA_RISTRUTTURAZIONE"), "dd/mm/YYYY")

                    If appoggioDate <> "dd/mm/YYYY" Then
                        Me.TxtDataRistrut.Text = par.FormattaData(appoggioDate)
                    Else
                        Me.TxtDataRistrut.Text = ""
                    End If


                    Me.DrLTipoEdificio.SelectedValue = (par.IfNull(dt.Rows(0).Item("COD_TIPOLOGIA_EDIFICIO"), "-1"))
                    Me.DrLUtilizzo.SelectedValue = (par.IfNull(dt.Rows(0).Item("COD_UTILIZZO_PRINCIPALE"), "-1"))

                    Me.DrLTipCostrut.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_TIPOLOGIA_COSTRUTTIVA"), "-1")

                    Me.DrLLivelloPoss.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_LIVELLO_POSSESSO"), "-1")

                    Me.DrLImpRiscald.SelectedValue = (dt.Rows(0).Item("COD_TIPOLOGIA_IMP_RISCALD"))
                    Me.txtCodEdificio.Text = par.IfNull((dt.Rows(0).Item("COD_EDIFICIO")), "ND")
                    Me.txtpCarrabili.Text = par.IfNull(dt.Rows(0).Item("NUM_PASSI_CARRABILI"), "")
                    Me.cmbMicrozona.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_MICROZONA"), "-1")
                    txtUnitaNP.Text = par.IfNull(dt.Rows(0).Item("UNITA_NON_PROPRIETA").ToString, 0)
                    '*******************PEPPE MODIFY 10/09/2010 NUOVI CAMPI CENSIMENTO STATO MANUTENTIVO

                    Me.cmbZona.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_ZONA"), "-1")


                    'Me.cmbTipolStrutt.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_TIPOLOGIA_STRUTTURA"), "-1")
                    Me.cmbTipoEdil1.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_TIPOLOGIA_EDILE_1"), "-1")
                    Me.cmbTipoEdil2.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_TIPOLOGIA_EDILE_2"), "-1")
                    Me.CmbTipoEdil3.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_TIPOLOGIA_EDILE_3"), "-1")
                    Me.cmbStatoFisico.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_MANCATO_RILIEVO"), "-1")

                    '****MAX 
                    Me.cmbRifLeg.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_RIF_LEG"), "-1")
                    CType(Me.Tab_CPI1.FindControl("TxtDataScadenza"), TextBox).Text = par.FormattaData(par.IfNull(dt.Rows(0).Item("DATA_SCADENZA_CPI"), ""))
                    CType(Me.Tab_CPI1.FindControl("TxtDataRilascio"), TextBox).Text = par.FormattaData(par.IfNull(dt.Rows(0).Item("DATA_RILASCIO_CPI"), ""))

                    If par.IfNull(dt.Rows(0).Item("CONDOMINIO").ToString, "null") <> "null" Then
                        Me.CmbCondominio.SelectedValue = dt.Rows(0).Item("CONDOMINIO").ToString
                    End If
                    If par.IfNull(dt.Rows(0).Item("PIANO_TERRA").ToString, "null") <> "null" Then
                        Me.CmbPTerra.SelectedValue = dt.Rows(0).Item("PIANO_TERRA").ToString
                    End If
                    If par.IfNull(dt.Rows(0).Item("SEMINTERRATO").ToString, "null") <> "null" Then
                        Me.CmbSeminterrato.SelectedValue = dt.Rows(0).Item("SEMINTERRATO").ToString
                    End If
                    '*******PEPPE MODIFY AGGIUNTA E GESTIONE CAMPO GESTIONE DIRETTA RISCALDAMENTO**********
                    If par.IfNull(dt.Rows(0).Item("GEST_RISC_DIR").ToString, "null") <> "null" Then
                        Me.cmbGestDirRisc.SelectedValue = dt.Rows(0).Item("GEST_RISC_DIR").ToString
                    End If

                    If par.IfNull(dt.Rows(0).Item("SOTTOTETTO").ToString, "null") <> "null" Then
                        Me.CmbSottotetto.SelectedValue = dt.Rows(0).Item("SOTTOTETTO").ToString
                    End If
                    If par.IfNull(dt.Rows(0).Item("ATTICO").ToString, "null") <> "null" Then
                        Me.CmbAttico.SelectedValue = dt.Rows(0).Item("ATTICO").ToString
                    End If
                    If par.IfNull(dt.Rows(0).Item("SUPERATTICO").ToString, "null") <> "null" Then
                        Me.CmbSuperAttico.SelectedValue = dt.Rows(0).Item("SUPERATTICO").ToString
                    End If

                    If Me.CmbCondominio.SelectedValue = 1 Then
                        Me.cmbGestDirRisc.Enabled = True
                    Else
                        Me.cmbGestDirRisc.Enabled = False
                    End If

                    Me.TxtDenEdificio.Text = dt.Rows(0).Item("DENOMINAZIONE").ToString
                    Me.CmbFuoriTerra.SelectedValue = par.IfNull(dt.Rows(0).Item("NUM_PIANI_FUORI").ToString, "-1")
                    Me.CmbEntroTerra.SelectedValue = par.IfNull(dt.Rows(0).Item("NUM_PIANI_ENTRO").ToString, "-1")
                    Me.txtNumMezzanini.Text = par.IfNull(dt.Rows(0).Item("NUM_MEZZANINI").ToString, 0)
                    '30/11/2011 PUCCETTONE MODIFY PER NUMERO MEZZANINI
                    'If par.IfNull(dt.Rows(0).Item("NUM_MEZZANINI").ToString, "null") <> "null" Then
                    '    Me.DrLMezzanini.SelectedValue = dt.Rows(0).Item("NUM_MEZZANINI").ToString
                    'End If
                    Me.txtNote.Text = dt.Rows(0).Item("SINTESI_EDIFICIO").ToString
                    Me.txtNoteMan.Text = dt.Rows(0).Item("NOTE").ToString
                    Me.txtScCostoBase.Text = par.IfNull(dt.Rows(0).Item("SCONTO_COSTO_BASE").ToString, 0)
                    'MODIFICA DATI CARICATI IN SINTESI EDIFICIO 14/09/2010

                    Me.TxtNumScale.Text = dt.Rows(0).Item("NUM_SCALE").ToString
                    'Me.TxtSezione.Text = dt.Rows(0).Item("SEZIONE").ToString
                    'Me.TxtFoglio.Text = dt.Rows(0).Item("FOGLIO").ToString
                    'Me.TxtNumero.Text = dt.Rows(0).Item("NUMERO").ToString
                    Me.txtNumAscensori.Text = dt.Rows(0).Item("NUM_ASCENSORI").ToString
                    Me.CmbPianoVendita.SelectedValue = dt.Rows(0).Item("FL_PIANO_VENDITA").ToString
                    vIdIndirizzo = dt.Rows(0).Item("ID_INDIRIZZO_PRINCIPALE")

                    caricaBM(dt.Rows(0).Item("IDfiliale"))
                    cmbBuildingManager.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_BM"), -1)
                    '*****SELEZIONE DEL'INDIRIZZO A PARTIRE DALL'ID DELL'ID INDIRIZZO PRINCIPALE PESCATO DALL'ID INDIRIZZO
                    da = Nothing
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID = " & dt.Rows(0).Item("ID_INDIRIZZO_PRINCIPALE")
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    da.Fill(dt2)


                    Me.TxtLocalità.Text = dt2.Rows(0).Item("LOCALITA").ToString
                    Me.TxtDescrInd.Text = RicavaDescVia(dt2.Rows(0).Item("DESCRIZIONE").ToString)
                    Me.TxtCAP.Text = dt2.Rows(0).Item("CAP").ToString
                    Me.TxtCivicoKilo.Text = dt2.Rows(0).Item("CIVICO").ToString



                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "SELECT COD FROM SISCOM_MI.TIPOLOGIA_INDIRIZZO WHERE DESCRIZIONE =  '" & RicavaVial(dt2.Rows(0).Item("DESCRIZIONE").ToString) & "'"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        Me.DrLTipoInd.SelectedValue = myReader(0)
                    End If
                    myReader.Close()
                    'Me.DrLComune.SelectedValue = -1
                    Me.DrLComune.SelectedValue = (dt2.Rows(0).Item("COD_COMUNE"))
                    'Me.TxtCodComun.Text = dt2.Rows(0).Item("COD_COMUNE").ToString
                    Me.DrLComune.Enabled = False
                    '**************SCHEDE DI IMPUTAZIONE**************'
                    '**************GIANCARLO  28/03/2018**************'
                    txtMqEsterni.Text = par.IfEmpty(dt.Rows(0).Item("MQ_ESTERNI").ToString.Replace(",", "."), "0")
                    txtMqPiloty.Text = par.IfEmpty(dt.Rows(0).Item("MQ_PILOTY").ToString.Replace(",", "."), "0")
                    txtNumBidoniCarta.Text = dt.Rows(0).Item("NUMERO_BIDONI_CARTA").ToString
                    txtNumBidoniVetro.Text = dt.Rows(0).Item("NUMERO_BIDONI_VETRO").ToString
                    txtNumBidoniUmido.Text = dt.Rows(0).Item("NUMERO_BIDONI_UMIDO").ToString
                    If dt.Rows(0).Item("FL_SPAZI_ESTERNI").ToString = "1" Then
                        chkSpazioEsterno.Checked = True
                    Else
                        chkSpazioEsterno.Checked = False
                    End If



                    '*************************************************'


                    dt = New Data.DataTable
                    da = Nothing
                    If Not Session.Item("LAVORAZIONE") = 1 Then
                        'Apro una nuova transazione
                        Session.Item("LAVORAZIONE") = "1"
                        par.myTrans = par.OracleConn.BeginTransaction()
                        HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                    End If

                    '*ABILITO I BOTTONI E I CAMPI AGGIUNTIVI DOPO APERTURA EDIFICIO ESISTENTE DA RICERCA
                    If Me.vId <> 0 Then
                        Me.attivabottoni()
                    End If
                End If
                BindGridScale()



                '*********************PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                Try
                    If My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/ED/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodEdificio.Text) & "*.*").Count > 0 Or My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/ED/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodEdificio.Text) & "*.*").Count > 0 Then
                        Me.btnFoto.Visible = True
                        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
                    Else
                        Me.btnFoto.Visible = True
                        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan2.png"
                    End If
                Catch ex As Exception
                    Me.LblErrore.Visible = True
                    LblErrore.Text = "ATTENZIONE!Verificare il percorso delle foto e delle planimetrie!"
                End Try
                '*********************FINE CONTROLLO PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********



                If par.IfNull(Session.Item("LIVELLO"), 0) <> 1 Then
                    If par.IfNull(Session.Item("MOD_CENS_MANUT"), 0) > 0 Then
                        Me.DrlSchede.Enabled = True
                        Me.cmbPeriodo.Enabled = True
                    End If

                    If Session.Item("CENS_MANUT_SL") > 0 Then
                        Me.CENS_MANUT_SL.Value = 1

                    End If
                Else
                    Me.DrlSchede.Enabled = True
                    Me.cmbPeriodo.Enabled = True
                    Me.CENS_MANUT_SL.Value = 0
                End If
            Catch EX1 As Oracle.DataAccess.Client.OracleException
                If EX1.Number = 54 Then
                    par.OracleConn.Close()
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Edificio aperto da un altro utente. Non è possibile effettuare modifiche!');" _
                    & "</script>"
                    ApriFrmWithDBLock()
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                    End If
                Else
                    'par.myTrans.Rollback()

                    par.OracleConn.Close()

                    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                    Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                End If



            Catch ex As Exception
                par.OracleConn.Close()
                Me.btnSalva.Visible = False
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
            End Try
        End If
    End Sub


    Private Sub save()
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        Dim seqedif As String = ""
        Dim lotto As String = ""
        Dim idCompTronc As String = ""
        Dim codEdificio As String = ""
        Dim IDINDIRIZZO As String = ""
        Try
            If TxtDataRistrut.Text <> "" AndAlso par.IfNull(TxtDataRistrut.Text, "Null") <> "Null" Then

                If par.AggiustaData(Me.TxtDataCostr.Text) > Format(Now, "yyyyMMddHHmm") Or par.AggiustaData(Me.TxtDataRistrut.Text) < par.AggiustaData(Me.TxtDataCostr.Text) Or par.AggiustaData(Me.TxtDataRistrut.Text) > Format(Now, "yyyyMMddHHmm") Then
                    Response.Write("<SCRIPT>alert('ATTENZIONE!Verificare la coerenza dei dati inseriti in DATA COSTRUZIONE e DATA RISTRUTTURAZIONE!\n                                         L\'operazione non può essere completata!');</SCRIPT>")
                    Exit Sub
                End If
            End If

            If Me.TxtDataRistrut.Text = "dd/Mm/YYYY" Then
                Me.TxtDataRistrut.Text = ""
            End If
            If Me.TxtDataCostr.Text = "dd/Mm/YYYY" Then
                Me.TxtDataCostr.Text = ""
            End If
            'Vecchio controllo con foglio e particella obbligatori
            'If Me.DdLComplesso.SelectedValue <> "-1" AndAlso Me.TxtDenEdificio.Text <> "" AndAlso Me.CmbFuoriTerra.SelectedValue <> "-1" AndAlso Me.CmbEntroTerra.SelectedValue <> "-1" AndAlso Me.TxtDataCostr.Text <> "" AndAlso Me.DrLTipoEdificio.SelectedValue <> "-1" AndAlso Me.DrLUtilizzo.SelectedValue <> "-1" AndAlso Me.DrLTipCostrut.SelectedValue <> "-1" AndAlso Me.DrLLivelloPoss.SelectedValue <> "-1" AndAlso Me.DrLImpRiscald.SelectedValue <> "-1" AndAlso par.IfEmpty(Me.TxtFoglio.Text, "null") <> "null" AndAlso par.IfEmpty(Me.TxtNumero.Text, "null") <> "null" Then
            '******PEPPE MODIFY 09/12/2010 
            'If Me.DdLComplesso.SelectedValue <> "-1" AndAlso Me.TxtDenEdificio.Text <> "" AndAlso Me.CmbFuoriTerra.SelectedValue <> "-1" AndAlso Me.CmbEntroTerra.SelectedValue <> "-1" AndAlso Me.TxtDataCostr.Text <> "" AndAlso Me.DrLTipoEdificio.SelectedValue <> "-1" AndAlso Me.DrLUtilizzo.SelectedValue <> "-1" AndAlso Me.DrLTipCostrut.SelectedValue <> "-1" AndAlso Me.DrLLivelloPoss.SelectedValue <> "-1" AndAlso Me.DrLImpRiscald.SelectedValue <> "-1" Then

            If Me.DdLComplesso.SelectedValue <> "-1" AndAlso Me.TxtDenEdificio.Text <> "" AndAlso Me.CmbFuoriTerra.SelectedValue <> "-1" AndAlso Me.CmbEntroTerra.SelectedValue <> "-1" AndAlso Me.TxtDataCostr.Text <> "" AndAlso Me.DrLLivelloPoss.SelectedValue <> "-1" AndAlso Me.DrLImpRiscald.SelectedValue <> "-1" Then
                If par.AggiustaData(Me.TxtDataCostr.Text) > Format(Now, "yyyyMMddHHmm") Then
                    Response.Write("<SCRIPT>alert('ATTENZIONE!Verificare la coerenza dei dati inseriti in DATA COSTRUZIONE\n                               L\'operazione non può essere completata!');</SCRIPT>")
                    Exit Sub
                End If

                'Richiamo la connessione
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)


                '***************++31/08/2009 VERIFICA DENOMINAZIONE COMPLESSO UNIVOCA *************************
                par.cmd.CommandText = "select * FROM SISCOM_MI.EDIFICI WHERE EDIFICI.DENOMINAZIONE ='" & par.PulisciStrSql(Me.TxtDenEdificio.Text.ToUpper) & "'"
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    Response.Write("<SCRIPT>alert('       Impossibile completare!\r\nQuesto edificio è già stato inserito!');</SCRIPT>")
                    Exit Sub
                End If
                myReader.Close()
                '***************++31/08/2009 VERIFICA DENOMINAZIONE COMPLESSO UNIVOCA *************************


                idCompTronc = Mid(DdLComplesso.SelectedValue.ToString, 5)
                'SELEZIONO SEQUENZIALE COMPLESSO
                'par.cmd.CommandText = "select count(*) from SISCOM_MI.edifici where id_complesso = " & Me.DdLComplesso.SelectedValue.ToString
                par.cmd.CommandText = "SELECT nvl(MAX(substr(cod_edificio,8,2)),0) as UltimoEdif FROM siscom_mi.EDIFICI WHERE id_complesso = " & Me.DdLComplesso.SelectedValue.ToString

                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    seqedif = par.IfNull(myReader(0), 0) + 1
                    If seqedif.Length = 1 Then
                        seqedif = 0 & seqedif
                    End If
                End If
                myReader.Close()
                par.cmd.CommandText = ""
                'SELEZIONO LOTTO COMPLESSI

                par.cmd.CommandText = "SELECT LOTTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID =" & DdLComplesso.SelectedValue.ToString
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    lotto = myReader(0)
                End If
                myReader.Close()
                par.cmd.CommandText = ""
                codEdificio = lotto & idCompTronc & seqedif

                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_INDIRIZZI.NEXTVAL FROM DUAL"
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    'IDINDIRIZZO = Mid(DdLComplesso.SelectedValue.ToString, 1, 1) & myReader(0)
                    IDINDIRIZZO = myReader(0)

                End If

                myReader.Close()
                par.cmd.CommandText = ""

                If Me.TxtDescrInd.Text <> "" And Me.TxtCAP.Text <> "" And Me.TxtCivicoKilo.Text <> "" Then
                    'Apro la Transazione
                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.INDIRIZZI (ID,DESCRIZIONE,CIVICO,CAP,LOCALITA,COD_COMUNE) VALUES (" & IDINDIRIZZO & ", '" & DrLTipoInd.SelectedItem.Text.ToUpper & " " & par.PulisciStrSql(TxtDescrInd.Text.ToUpper) & "','" & par.PulisciStrSql(TxtCivicoKilo.Text.ToUpper) & "', '" & par.PulisciStrSql(TxtCAP.Text) & "', '" & par.PulisciStrSql(Me.TxtLocalità.Text.ToUpper) & "', '" & Me.DrLComune.SelectedValue.ToString & "')"
                    par.cmd.ExecuteNonQuery()
                    vIdIndirizzo = IDINDIRIZZO
                    par.cmd.CommandText = ""

                Else
                    Response.Write("<SCRIPT>alert('Impossibile completare!\r\nNon sono stati inseriti i dati relativi all\'indirizzo!');</SCRIPT>")
                    Exit Sub

                End If

                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_EDIFICI.NEXTVAL FROM DUAL"
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    vId = Mid(DdLComplesso.SelectedValue.ToString, 1, 1) & myReader(0)
                End If
                myReader.Close()
                'Dim Mezzanini As Integer
                'If Me.ChkMezzanin.Checked = True Then
                '    Mezzanini = 1
                'Else
                '    Mezzanini = 0
                'End If
                Dim spaziEsterni As Integer = 0
                If chkSpazioEsterno.Checked Then
                    spaziEsterni = 1
                End If

                'INSERT DELL'EDIFICIO APPENA CREATO
                par.cmd.CommandText = " INSERT INTO SISCOM_MI.EDIFICI (ID, COD_EDIFICIO, COD_EDIFICIO_GIMI, DENOMINAZIONE, ID_COMPLESSO, " _
                & "NUM_PIANI_ENTRO, NUM_PIANI_FUORI, NUM_SCALE, ID_INDIRIZZO_PRINCIPALE, DATA_COSTRUZIONE, DATA_RISTRUTTURAZIONE, COD_TIPOLOGIA_EDIFICIO," _
                & " COD_UTILIZZO_PRINCIPALE, COD_TIPOLOGIA_COSTRUTTIVA, COD_LIVELLO_POSSESSO, CONDOMINIO, COD_TIPOLOGIA_IMP_RISCALD, SINTESI_EDIFICIO, PIANO_TERRA," _
                & " SEMINTERRATO, SOTTOTETTO, ATTICO, SUPERATTICO, NUM_MEZZANINI, ID_OPERATORE_INSERIMENTO,COD_COMUNE,NUM_ASCENSORI,FL_PIANO_VENDITA, GEST_RISC_DIR " _
                & ", ID_TIPOLOGIA_EDILE_1,ID_TIPOLOGIA_EDILE_2,ID_TIPOLOGIA_EDILE_3,ID_MANCATO_RILIEVO,NUM_PASSI_CARRABILI,SCONTO_COSTO_BASE,ID_MICROZONA,ID_ZONA,COD_RIF_LEG,ID_BM, UNITA_NON_PROPRIETA, " _
                & "MQ_ESTERNI,MQ_PILOTY, NUMERO_BIDONI_CARTA,NUMERO_BIDONI_VETRO,NUMERO_BIDONI_UMIDO, FL_SPAZI_ESTERNI  ) VALUES " _
                & "(" & vId & ", '" & codEdificio & "', '" & par.PulisciStrSql(Me.TxtGimi.Text) & "', '" & par.PulisciStrSql(Me.TxtDenEdificio.Text.ToUpper) & "', " & Me.DdLComplesso.SelectedValue _
                & ", " & par.IfEmpty(par.VirgoleInPunti(Me.CmbEntroTerra.SelectedItem.Text.ToString), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.CmbFuoriTerra.SelectedItem.Text), "Null") & ", " & Me.TxtNumScale.Text & ", " & IDINDIRIZZO & ", '" & par.AggiustaData(Me.TxtDataCostr.Text) & "', '" & par.AggiustaData(Me.TxtDataRistrut.Text) & "', " & RitornaNullSeMenoUno(Me.DrLTipoEdificio.SelectedValue) _
                & ", " & RitornaNullSeMenoUno(Me.DrLUtilizzo.SelectedValue) & ", " & RitornaNullSeMenoUno(Me.DrLTipCostrut.SelectedValue) & ", " & RitornaNullSeMenoUno(Me.DrLLivelloPoss.SelectedValue) & " ," & Me.CmbCondominio.SelectedValue.ToString & ", " & RitornaNullSeMenoUno(Me.DrLImpRiscald.SelectedValue) & " , '" & par.PulisciStrSql(Me.txtNote.Text) _
                & "', " & (Me.CmbPTerra.SelectedValue.ToString) & ", " & (Me.CmbSeminterrato.SelectedValue.ToString) & ", " & (Me.CmbSottotetto.SelectedValue.ToString) & ", " & (Me.CmbAttico.SelectedValue.ToString) & ", " & (Me.CmbSuperAttico.SelectedValue.ToString) _
                & ", " & par.IfEmpty(Me.txtNumMezzanini.Text, 0) & ", '" & Session("ID_OPERATORE") & "', '" & par.PulisciStrSql(Me.DrLComune.SelectedValue.ToString) & "'," & par.IfEmpty(Me.txtNumAscensori.Text, "Null") & "," & Me.CmbPianoVendita.SelectedValue.ToString & "," & Me.cmbGestDirRisc.SelectedValue.ToString _
                & ", " & RitornaNullSeMenoUno(par.PulisciStrSql(Me.cmbTipoEdil1.SelectedValue.ToString)) & ", " & RitornaNullSeMenoUno(par.PulisciStrSql(Me.cmbTipoEdil2.SelectedValue.ToString)) & ", " & RitornaNullSeMenoUno(par.PulisciStrSql(Me.CmbTipoEdil3.SelectedValue.ToString)) & ", " & RitornaNullSeMenoUno(Me.cmbStatoFisico.SelectedValue.ToString) & "," & par.IfEmpty(Me.txtpCarrabili.Text, "NULL") & "," & par.IfEmpty(Me.txtScCostoBase.Text, "null") & "," & RitornaNullSeMenoUno(Me.cmbMicrozona.SelectedValue.ToString) & "," & RitornaNullSeMenoUno(Me.cmbZona.SelectedValue.ToString) & "," & RitornaNullSeMenoUno(Me.cmbRifLeg.SelectedValue.ToString) & "," & par.insDbValue(cmbBuildingManager.SelectedValue, False, , True) & "," & par.IfEmpty(txtUnitaNP.Text, 0) _
                & ", " & par.IfEmpty(txtMqEsterni.Text, "Null") & ", " & par.IfEmpty(txtMqPiloty.Text, "Null") & ", " & par.IfEmpty(txtNumBidoniCarta.Text, "Null") & ", " & par.IfEmpty(txtNumBidoniVetro.Text, "Null") & ", " & par.IfEmpty(txtNumBidoniUmido.Text, "Null") & "," & spaziEsterni _
                & ")"
                par.cmd.ExecuteNonQuery()

                'VECCHIA QUERY: SONO STATI ELIMINATI I CAMPI SEZIONE, FOGLIO E NUMERO CHE ERANO NASCOSTI
                'par.cmd.CommandText = " INSERT INTO SISCOM_MI.EDIFICI (ID, COD_EDIFICIO, COD_EDIFICIO_GIMI, DENOMINAZIONE, ID_COMPLESSO, " _
                '& "NUM_PIANI_ENTRO, NUM_PIANI_FUORI, NUM_SCALE, ID_INDIRIZZO_PRINCIPALE, DATA_COSTRUZIONE, DATA_RISTRUTTURAZIONE, COD_TIPOLOGIA_EDIFICIO," _
                '& " COD_UTILIZZO_PRINCIPALE, COD_TIPOLOGIA_COSTRUTTIVA, COD_LIVELLO_POSSESSO, CONDOMINIO, COD_TIPOLOGIA_IMP_RISCALD, SINTESI_EDIFICIO, PIANO_TERRA," _
                '& " SEMINTERRATO, SOTTOTETTO, ATTICO, SUPERATTICO, NUM_MEZZANINI, ID_OPERATORE_INSERIMENTO,SEZIONE,FOGLIO,NUMERO,COD_COMUNE,NUM_ASCENSORI,FL_PIANO_VENDITA, GEST_RISC_DIR) VALUES " _
                '& "(" & vId & ", '" & codEdificio & "', '" & par.PulisciStrSql(Me.TxtGimi.Text) & "', '" & par.PulisciStrSql(Me.TxtDenEdificio.Text.ToUpper) & "', " & Me.DdLComplesso.SelectedValue _
                '& ", " & par.IfEmpty(par.VirgoleInPunti(Me.CmbEntroTerra.SelectedItem.Text.ToString), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.CmbFuoriTerra.SelectedItem.Text), "Null") & ", " & Me.TxtNumScale.Text & ", " & IDINDIRIZZO & ", '" & par.AggiustaData(Me.TxtDataCostr.Text) & "', '" & par.AggiustaData(Me.TxtDataRistrut.Text) & "', " & RitornaNullSeMenoUno(Me.DrLTipoEdificio.SelectedValue) _
                '& ", " & RitornaNullSeMenoUno(Me.DrLUtilizzo.SelectedValue) & ", " & RitornaNullSeMenoUno(Me.DrLTipCostrut.SelectedValue) & ", " & RitornaNullSeMenoUno(Me.DrLLivelloPoss.SelectedValue) & " ," & Me.CmbCondominio.SelectedValue.ToString & ", " & RitornaNullSeMenoUno(Me.DrLImpRiscald.SelectedValue) & " , '" & par.PulisciStrSql(Me.TxtSintesi.Text) _
                '& "', " & (Me.CmbPTerra.SelectedValue.ToString) & ", " & (Me.CmbSeminterrato.SelectedValue.ToString) & ", " & (Me.CmbSottotetto.SelectedValue.ToString) & ", " & (Me.CmbAttico.SelectedValue.ToString) & ", " & (Me.CmbSuperAttico.SelectedValue.ToString) _
                '& ", " & Me.DrLMezzanini.SelectedValue.ToString & ", '" & Session("ID_OPERATORE") & "', '" & par.PulisciStrSql(Me.TxtSezione.Text) & "', '" & par.PulisciStrSql(Me.TxtFoglio.Text) & "','" & par.PulisciStrSql(Me.TxtNumero.Text) & "', '" & par.PulisciStrSql(Me.TxtCodComun.Text) & "'," & par.IfEmpty(Me.txtNumAscensori.Text, "Null") & "," & Me.CmbPianoVendita.SelectedValue.ToString & "," & Me.cmbGestDirRisc.SelectedValue.ToString & " )"
                'par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = ""
                Me.txtCodEdificio.Text = codEdificio
                Me.attivabottoni()

                classetab = "tabbertab"
                '****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI (ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','')"
                par.cmd.ExecuteNonQuery()
                '*******************************FINE****************************************
                '***************************************************************************
                MemorizzaAttributi()
                CaricaAttributi()

                'COMMIT GENERALE
                par.myTrans.Commit()
                '*********************PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                If My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/ED/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodEdificio.Text) & "*.*").Count > 0 Or My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/ED/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodEdificio.Text) & "*.*").Count > 0 Then
                    Me.btnFoto.Visible = True
                    Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
                Else
                    Me.btnFoto.Visible = True
                    Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan2.png"
                End If
                'Riapro una nuova transazione
                Session.Item("LAVORAZIONE") = "1"
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                'Blocco il complesso per eventuali modifiche da altri utenti
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EDIFICI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                myReader = par.cmd.ExecuteReader
                myReader.Close()


                Response.Write("<SCRIPT>alert('Operazione eseguita correttamente!');</SCRIPT>")
                If par.IfNull(Session.Item("LIVELLO"), 0) <> 1 Then
                    If par.IfNull(Session.Item("MOD_CENS_MANUT"), 0) > 0 Then
                        Me.DrlSchede.Enabled = True
                        Me.cmbPeriodo.Enabled = True
                    End If

                    If Session.Item("CENS_MANUT_SL") > 0 Then
                        Me.CENS_MANUT_SL.Value = 1

                    End If
                Else
                    Me.DrlSchede.Enabled = True
                    Me.cmbPeriodo.Enabled = True
                    Me.CENS_MANUT_SL.Value = 0
                End If
            Else
                Response.Write("<SCRIPT>alert('       Impossibile completare!\r\nAvvalorare tutti i campi obbligatori!');</SCRIPT>")
                'par.myTrans = par.OracleConn.BeginTransaction()
                'HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        End Try

    End Sub
    Private Sub attivabottoni()
        If vId <> 0 Then
            '*ABILITO I BOTTONI E I CAMPI AGGIUNTIVI DOPO IL SAVE
            'Me.ImgBtnScale.Visible = True
            'Me.ImgButDimens.Visible = True
            'Me.ImageButton2.Visible = True
            'Me.ImageButton3.Visible = True
            'Me.ImgBtnMillesimali.Visible = True
            Me.btnUniImmob.Visible = True
            Me.btnUniCom.Visible = True
            Me.imgStampa.Visible = True
            Me.DdLComplesso.Enabled = False
            Me.btnFoto.Visible = True

            'Me.btnUtenzaMill.Visible = True
        End If


    End Sub

    Private Function RitornaNumDaSiNo(ByVal valoredapassare As String) As String
        Try
            Dim a As String
            If valoredapassare = "SI" Then
                a = 1
            ElseIf valoredapassare = "NO" Then
                a = 0
            Else
                a = "Null"
            End If

            Return a
        Catch ex As Exception
        End Try
    End Function

    Private Function RitornaSiNoDaNum(ByVal valoredapassare As String) As String

        Dim a As String
        If valoredapassare = 1 And valoredapassare <> "" Then
            a = "SI"
        Else
            a = "NO"
        End If

        Return a

    End Function

    Public Sub PROVA()
        Beep()
    End Sub
    Public Property vId() As Long
        Get
            If Not (ViewState("par_idEdificio") Is Nothing) Then
                Return CLng(ViewState("par_idEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idEdificio") = value
        End Set

    End Property
    Public Property vIdIndirizzo() As Long
        Get
            If Not (ViewState("par_vIdIndirizzo") Is Nothing) Then
                Return CLng(ViewState("par_vIdIndirizzo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_vIdIndirizzo") = value
        End Set

    End Property



    Private Sub Update()
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        Dim numscale As Integer


        Try
            If vId <> 0 Then

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                



                If TxtDataRistrut.Text <> "" AndAlso par.IfNull(Me.TxtDataRistrut.Text, "Null") <> "Null" Then

                    If (par.AggiustaData(Me.TxtDataCostr.Text) > Format(Now, "yyyyMMddHHmm") Or par.AggiustaData(Me.TxtDataRistrut.Text) < par.AggiustaData(Me.TxtDataCostr.Text) Or par.AggiustaData(Me.TxtDataRistrut.Text) > Format(Now, "yyyyMMddHHmm")) Then
                        Response.Write("<SCRIPT>alert('ATTENZIONE!Verificare la coerenza dei dati inseriti in DATA COSTRUZIONE e DATA RISTRUTTURAZIONE!\n                                         L\'operazione non può essere completata!');</SCRIPT>")
                        Exit Sub
                    End If
                End If
                '***************++31/08/2009 VERIFICA DENOMINAZIONE EDIFCIO UNIVOCA *************************
                par.cmd.CommandText = "select * FROM SISCOM_MI.EDIFICI WHERE EDIFICI.DENOMINAZIONE ='" & par.PulisciStrSql(Me.TxtDenEdificio.Text.ToUpper) & "' AND EDIFICI.ID <> " & vId
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    Response.Write("<SCRIPT>alert('       Impossibile completare!\r\nQuesto edificio è già stato inserito!');</SCRIPT>")
                    Exit Sub
                End If
                myReader.Close()
                '***************++31/08/2009 VERIFICA DENOMINAZIONE EDIFCIO UNIVOCA *************************
                'Vecchio controllo con foglio e particella obbligatori
                'If Me.DdLComplesso.SelectedValue <> "-1" AndAlso Me.TxtDenEdificio.Text <> "" AndAlso par.IfEmpty(Me.CmbFuoriTerra.SelectedItem.Text, "null") <> "null" AndAlso par.IfEmpty(Me.CmbEntroTerra.SelectedItem.Text, "null") <> "null" AndAlso Me.TxtDataCostr.Text <> "" AndAlso Me.DrLTipoEdificio.SelectedValue <> "-1" AndAlso Me.DrLUtilizzo.SelectedValue <> "-1" AndAlso Me.DrLTipCostrut.SelectedValue <> "-1" AndAlso Me.DrLLivelloPoss.SelectedValue <> "-1" AndAlso Me.DrLImpRiscald.SelectedValue <> "-1" AndAlso par.IfEmpty(Me.TxtFoglio.Text, "null") <> "null" AndAlso par.IfEmpty(Me.TxtNumero.Text, "null") <> "null" Then
                '*************PEPPE MODIFY 09/12/2010
                'If Me.DdLComplesso.SelectedValue <> "-1" AndAlso Me.TxtDenEdificio.Text <> "" AndAlso par.IfEmpty(Me.CmbFuoriTerra.SelectedItem.Text, "null") <> "null" AndAlso par.IfEmpty(Me.CmbEntroTerra.SelectedItem.Text, "null") <> "null" AndAlso Me.TxtDataCostr.Text <> "" AndAlso Me.DrLTipoEdificio.SelectedValue <> "-1" AndAlso Me.DrLUtilizzo.SelectedValue <> "-1" AndAlso Me.DrLTipCostrut.SelectedValue <> "-1" AndAlso Me.DrLLivelloPoss.SelectedValue <> "-1" AndAlso Me.DrLImpRiscald.SelectedValue <> "-1" AndAlso par.IfEmpty(Me.TxtDescrInd.Text, "Null") <> "Null" Then
                If Me.DdLComplesso.SelectedValue <> "-1" AndAlso Me.TxtDenEdificio.Text <> "" AndAlso Me.CmbFuoriTerra.SelectedValue <> "-1" AndAlso Me.CmbEntroTerra.SelectedValue <> "-1" AndAlso Me.TxtDataCostr.Text <> "" AndAlso Me.DrLLivelloPoss.SelectedValue <> "-1" AndAlso Me.DrLImpRiscald.SelectedValue <> "-1" AndAlso par.IfEmpty(Me.TxtDescrInd.Text, "Null") <> "Null" Then

                    par.cmd.CommandText = "UPDATE SISCOM_MI.INDIRIZZI SET DESCRIZIONE = '" & DrLTipoInd.SelectedItem.Text.ToUpper & " " & par.PulisciStrSql(TxtDescrInd.Text.ToUpper) & "', CIVICO = '" & par.PulisciStrSql(Me.TxtCivicoKilo.Text.ToUpper) & "', CAP = '" & Me.TxtCAP.Text & "', LOCALITA = '" & par.PulisciStrSql(Me.TxtLocalità.Text.ToUpper) & "', COD_COMUNE = '" & Me.DrLComune.SelectedValue.ToString & "'  WHERE ID IN (SELECT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI WHERE ID =" & vId & ")"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""


                    par.cmd.CommandText = "select count(*) from SISCOM_MI.scale_edifici where id_edificio = " & vId
                    myReader = par.cmd.ExecuteReader

                    '* le due update diverse servono per gli edifici dei quali sono gestite le scale e deve essere aggiornato il numero scale con quelle caricate *
                    If myReader.Read Then
                        numscale = myReader(0)
                        myReader.Close()
                    Else
                        myReader.Close()
                    End If
                    'Dim Mezzanini As Integer
                    'If Me.ChkMezzanin.Checked = True Then
                    '    Mezzanini = 1
                    'Else
                    '    Mezzanini = 0
                    'End If
                    Dim spaziEsterni As Integer = 0
                    If chkSpazioEsterno.Checked Then
                        spaziEsterni = 1
                    End If
                    If numscale > 0 Then
                        'MODIFICATO CON I NUOVI CAMPI DEL CENSIMENTO MANUTENTIVO
                        par.cmd.CommandText = ""
                        par.cmd.CommandText = "UPDATE SISCOM_MI.EDIFICI " _
                                            & "SET COD_EDIFICIO_GIMI = '" & par.PulisciStrSql(Me.TxtGimi.Text) & "', " _
                                            & " DENOMINAZIONE= '" & par.PulisciStrSql(Me.TxtDenEdificio.Text) & "', " _
                                            & " NUM_PIANI_ENTRO = " & par.IfEmpty(par.VirgoleInPunti(Me.CmbEntroTerra.SelectedItem.Text), "Null") & ", " _
                                            & " NUM_PIANI_FUORI= " & par.IfEmpty(par.VirgoleInPunti(Me.CmbFuoriTerra.SelectedItem.Text), "Null") & ", " _
                                            & "NUM_SCALE = " & numscale _
                                            & ", DATA_COSTRUZIONE = '" & par.AggiustaData(Me.TxtDataCostr.Text) & "', " _
                                            & " DATA_RISTRUTTURAZIONE = '" & par.AggiustaData(Me.TxtDataRistrut.Text) & "', " _
                                            & " COD_TIPOLOGIA_EDIFICIO = " & RitornaNullSeMenoUno(Me.DrLTipoEdificio.SelectedValue) & ", " _
                                            & " COD_UTILIZZO_PRINCIPALE = " & RitornaNullSeMenoUno(Me.DrLUtilizzo.SelectedValue) _
                                            & ", COD_TIPOLOGIA_COSTRUTTIVA = " & RitornaNullSeMenoUno(Me.DrLTipCostrut.SelectedValue) & ", " _
                                            & " COD_LIVELLO_POSSESSO = '" & Me.DrLLivelloPoss.SelectedValue & "', " _
                                            & " CONDOMINIO = " & (Me.CmbCondominio.SelectedValue.ToString) & ", " _
                                            & " COD_TIPOLOGIA_IMP_RISCALD = '" & Me.DrLImpRiscald.SelectedValue _
                                            & "', SINTESI_EDIFICIO = '" & par.PulisciStrSql(Me.txtNote.Text) & "', " _
                                            & " NOTE = '" & par.PulisciStrSql(Me.txtNoteMan.Text) & "', " _
                                            & " PIANO_TERRA = " & (Me.CmbPTerra.SelectedValue.ToString) & "," _
                                            & " SEMINTERRATO = " & (Me.CmbSeminterrato.SelectedValue.ToString) & "," _
                                            & " SOTTOTETTO = " & (Me.CmbSottotetto.SelectedValue.ToString) & ", " _
                                            & " ATTICO = " & (Me.CmbAttico.SelectedValue.ToString) _
                                            & ", SUPERATTICO = " & (Me.CmbSuperAttico.SelectedValue.ToString) & ", " _
                                            & " NUM_MEZZANINI = " & par.IfEmpty(Me.txtNumMezzanini.Text, 0) & ", " _
                                            & " ID_OPERATORE_AGGIORNAMENTO = '" & Session("ID_OPERATORE") _
                                            & "' , NUM_ASCENSORI = " & par.IfEmpty(Me.txtNumAscensori.Text, "Null") _
                                            & ", FL_PIANO_VENDITA= " & Me.CmbPianoVendita.SelectedValue.ToString _
                                            & ", GEST_RISC_DIR = " & Me.cmbGestDirRisc.SelectedValue.ToString _
                                            & ", ID_TIPOLOGIA_EDILE_1=" & RitornaNullSeMenoUno(Me.cmbTipoEdil1.SelectedValue.ToString) _
                                            & ", ID_TIPOLOGIA_EDILE_2=" & RitornaNullSeMenoUno(Me.cmbTipoEdil2.SelectedValue.ToString) _
                                            & ", ID_TIPOLOGIA_EDILE_3=" & RitornaNullSeMenoUno(Me.CmbTipoEdil3.SelectedValue.ToString) _
                                            & ",ID_MANCATO_RILIEVO=" & RitornaNullSeMenoUno(Me.cmbStatoFisico.SelectedValue) & ", " _
                                            & "NUM_PASSI_CARRABILI=" & par.IfEmpty(Me.txtpCarrabili.Text, "NULL") _
                                            & ",DATA_RILASCIO_CPI='" & par.AggiustaData(CType(Me.Tab_CPI1.FindControl("TxtDataRilascio"), TextBox).Text) _
                                            & "',DATA_SCADENZA_CPI='" & par.AggiustaData(CType(Me.Tab_CPI1.FindControl("TxtDataScadenza"), TextBox).Text) _
                                            & "', SCONTO_COSTO_BASE = " & par.IfEmpty(Me.txtScCostoBase.Text, "null") & ", id_microzona = " _
                                            & RitornaNullSeMenoUno(Me.cmbMicrozona.SelectedValue.ToString) _
                                            & ",id_zona = " & RitornaNullSeMenoUno(Me.cmbZona.SelectedValue.ToString) _
                                            & ",COD_RIF_LEG = " & RitornaNullSeMenoUno(Me.cmbRifLeg.SelectedValue.ToString) _
                                            & ",id_bm=" & par.insDbValue(cmbBuildingManager.SelectedValue, False, , True) _
                                            & ", UNITA_NON_PROPRIETA = " & par.IfEmpty(txtUnitaNP.Text, 0) _
                                            & ", MQ_ESTERNI = " & par.IfEmpty(txtMqEsterni.Text, "Null") _
                                            & ", MQ_PILOTY = " & par.IfEmpty(txtMqPiloty.Text, "Null") _
                                            & ", NUMERO_BIDONI_CARTA = " & par.IfEmpty(txtNumBidoniCarta.Text, "Null") _
                                            & ", NUMERO_BIDONI_VETRO = " & par.IfEmpty(txtNumBidoniVetro.Text, "Null") _
                                            & ", NUMERO_BIDONI_UMIDO = " & par.IfEmpty(txtNumBidoniUmido.Text, "Null") _
                                            & ", FL_SPAZI_ESTERNI = " & spaziEsterni _
                                            & " WHERE ID = " & vId
                        par.cmd.ExecuteNonQuery()

                    Else
                        'MODIFICATO CON I NUOVI CAMPI DEL CENSIMENTO MANUTENTIVO
                        par.cmd.CommandText = ""
                        par.cmd.CommandText = "UPDATE SISCOM_MI.EDIFICI " _
                                            & "SET COD_EDIFICIO_GIMI = '" & par.PulisciStrSql(Me.TxtGimi.Text) _
                                            & "', DENOMINAZIONE= '" & par.PulisciStrSql(Me.TxtDenEdificio.Text) _
                                            & "', NUM_PIANI_ENTRO = " & Me.CmbEntroTerra.SelectedItem.Text _
                                            & ", NUM_PIANI_FUORI= " & Me.CmbFuoriTerra.SelectedItem.Text _
                                            & ", NUM_SCALE = " & Me.TxtNumScale.Text _
                                            & ", DATA_COSTRUZIONE = '" & par.AggiustaData(Me.TxtDataCostr.Text) _
                                            & "', DATA_RISTRUTTURAZIONE = '" & par.AggiustaData(Me.TxtDataRistrut.Text) _
                                            & "', COD_TIPOLOGIA_EDIFICIO = " & RitornaNullSeMenoUno(Me.DrLTipoEdificio.SelectedValue) _
                                            & ", COD_UTILIZZO_PRINCIPALE = " & RitornaNullSeMenoUno(Me.DrLUtilizzo.SelectedValue) _
                                            & ", COD_TIPOLOGIA_COSTRUTTIVA = " & RitornaNullSeMenoUno(Me.DrLTipCostrut.SelectedValue) _
                                            & ", COD_LIVELLO_POSSESSO = '" & Me.DrLLivelloPoss.SelectedValue _
                                            & "', CONDOMINIO = " & (Me.CmbCondominio.SelectedValue.ToString) _
                                            & ", COD_TIPOLOGIA_IMP_RISCALD = '" & Me.DrLImpRiscald.SelectedValue _
                                            & "', SINTESI_EDIFICIO = '" & par.PulisciStrSql(Me.txtNote.Text) _
                                            & "', NOTE = '" & par.PulisciStrSql(Me.txtNoteMan.Text) _
                                            & "', PIANO_TERRA = " & (Me.CmbPTerra.SelectedValue.ToString) _
                                            & ",  SEMINTERRATO = " & (Me.CmbSeminterrato.SelectedValue.ToString) _
                                            & ", SOTTOTETTO = " & (Me.CmbSottotetto.SelectedValue.ToString) _
                                            & ", ATTICO = " & (Me.CmbAttico.SelectedValue.ToString) _
                                            & ", SUPERATTICO = " & (Me.CmbSuperAttico.SelectedValue.ToString) _
                                            & ", NUM_MEZZANINI = " & par.IfEmpty(Me.txtNumMezzanini.Text, 0) _
                                            & ", ID_OPERATORE_AGGIORNAMENTO ='" & Session("ID_OPERATORE") _
                                            & "' , NUM_ASCENSORI = " & par.IfEmpty(Me.txtNumAscensori.Text, "Null") _
                                            & ", GEST_RISC_DIR = " & Me.cmbGestDirRisc.SelectedValue.ToString _
                                            & ", ID_TIPOLOGIA_EDILE_1=" & RitornaNullSeMenoUno(Me.cmbTipoEdil1.SelectedValue.ToString) _
                                            & ", ID_TIPOLOGIA_EDILE_2=" & RitornaNullSeMenoUno(Me.cmbTipoEdil2.SelectedValue.ToString) _
                                            & ", ID_TIPOLOGIA_EDILE_3=" & RitornaNullSeMenoUno(Me.CmbTipoEdil3.SelectedValue.ToString) _
                                            & ",ID_MANCATO_RILIEVO=" & RitornaNullSeMenoUno(Me.cmbStatoFisico.SelectedValue) & ", " _
                                            & "NUM_PASSI_CARRABILI=" & par.IfEmpty(Me.txtpCarrabili.Text, "NULL") _
                                            & ",DATA_RILASCIO_CPI='" & par.FormattaData(CType(Me.Tab_CPI1.FindControl("TxtDataRilascio"), TextBox).Text) _
                                            & "',DATA_SCADENZA_CPI='" & par.FormattaData(CType(Me.Tab_CPI1.FindControl("TxtDataScadenza"), TextBox).Text) _
                                            & "', SCONTO_COSTO_BASE = " & par.IfEmpty(Me.txtScCostoBase.Text, "null") _
                                            & ", id_microzona = " & RitornaNullSeMenoUno(Me.cmbMicrozona.SelectedValue.ToString) _
                                            & ",id_zona = " & RitornaNullSeMenoUno(Me.cmbZona.SelectedValue.ToString) _
                                            & ",COD_RIF_LEG = " & RitornaNullSeMenoUno(Me.cmbRifLeg.SelectedValue.ToString) _
                                            & ",id_bm=" & par.insDbValue(cmbBuildingManager.SelectedValue, False, , True) _
                                            & ", UNITA_NON_PROPRIETA = " & par.IfEmpty(txtUnitaNP.Text, 0) _
                                            & ", MQ_ESTERNI = " & par.IfEmpty(par.VirgoleInPunti(txtMqEsterni.Text), "Null") _
                                            & ", MQ_PILOTY = " & par.IfEmpty(par.VirgoleInPunti(txtMqPiloty.Text), "Null") _
                                            & ", NUMERO_BIDONI_CARTA = " & par.IfEmpty(par.VirgoleInPunti(txtNumBidoniCarta.Text), "Null") _
                                            & ", NUMERO_BIDONI_VETRO = " & par.IfEmpty(par.VirgoleInPunti(txtNumBidoniVetro.Text), "Null") _
                                            & ", NUMERO_BIDONI_UMIDO = " & par.IfEmpty(par.VirgoleInPunti(txtNumBidoniUmido.Text), "Null") _
                                            & ", FL_SPAZI_ESTERNI = " & spaziEsterni _
                        & " WHERE ID = " & vId
                        par.cmd.ExecuteNonQuery()

                    End If
                    'EDIFICI_TMP UTILE PER LE SCHEDE DI IMPUTAZIONE
                    If numscale > 0 Then
                        'MODIFICATO CON I NUOVI CAMPI DEL CENSIMENTO MANUTENTIVO
                        par.cmd.CommandText = ""
                        par.cmd.CommandText = "UPDATE SISCOM_MI.EDIFICI_TMP " _
                                            & "SET COD_EDIFICIO_GIMI = '" & par.PulisciStrSql(Me.TxtGimi.Text) & "', " _
                                            & " DENOMINAZIONE= '" & par.PulisciStrSql(Me.TxtDenEdificio.Text) & "', " _
                                            & " NUM_PIANI_ENTRO = " & par.IfEmpty(par.VirgoleInPunti(Me.CmbEntroTerra.SelectedItem.Text), "Null") & ", " _
                                            & " NUM_PIANI_FUORI= " & par.IfEmpty(par.VirgoleInPunti(Me.CmbFuoriTerra.SelectedItem.Text), "Null") & ", " _
                                            & "NUM_SCALE = " & numscale _
                                            & ", DATA_COSTRUZIONE = '" & par.AggiustaData(Me.TxtDataCostr.Text) & "', " _
                                            & " DATA_RISTRUTTURAZIONE = '" & par.AggiustaData(Me.TxtDataRistrut.Text) & "', " _
                                            & " COD_TIPOLOGIA_EDIFICIO = " & RitornaNullSeMenoUno(Me.DrLTipoEdificio.SelectedValue) & ", " _
                                            & " COD_UTILIZZO_PRINCIPALE = " & RitornaNullSeMenoUno(Me.DrLUtilizzo.SelectedValue) _
                                            & ", COD_TIPOLOGIA_COSTRUTTIVA = " & RitornaNullSeMenoUno(Me.DrLTipCostrut.SelectedValue) & ", " _
                                            & " COD_LIVELLO_POSSESSO = '" & Me.DrLLivelloPoss.SelectedValue & "', " _
                                            & " CONDOMINIO = " & (Me.CmbCondominio.SelectedValue.ToString) & ", " _
                                            & " COD_TIPOLOGIA_IMP_RISCALD = '" & Me.DrLImpRiscald.SelectedValue _
                                            & "', SINTESI_EDIFICIO = '" & par.PulisciStrSql(Me.txtNote.Text) & "', " _
                                            & " NOTE = '" & par.PulisciStrSql(Me.txtNoteMan.Text) & "', " _
                                            & " PIANO_TERRA = " & (Me.CmbPTerra.SelectedValue.ToString) & "," _
                                            & " SEMINTERRATO = " & (Me.CmbSeminterrato.SelectedValue.ToString) & "," _
                                            & " SOTTOTETTO = " & (Me.CmbSottotetto.SelectedValue.ToString) & ", " _
                                            & " ATTICO = " & (Me.CmbAttico.SelectedValue.ToString) _
                                            & ", SUPERATTICO = " & (Me.CmbSuperAttico.SelectedValue.ToString) & ", " _
                                            & " NUM_MEZZANINI = " & par.IfEmpty(Me.txtNumMezzanini.Text, 0) & ", " _
                                            & " ID_OPERATORE_AGGIORNAMENTO = '" & Session("ID_OPERATORE") _
                                            & "' , NUM_ASCENSORI = " & par.IfEmpty(Me.txtNumAscensori.Text, "Null") _
                                            & ", FL_PIANO_VENDITA= " & Me.CmbPianoVendita.SelectedValue.ToString _
                                            & ", GEST_RISC_DIR = " & Me.cmbGestDirRisc.SelectedValue.ToString _
                                            & ", ID_TIPOLOGIA_EDILE_1=" & RitornaNullSeMenoUno(Me.cmbTipoEdil1.SelectedValue.ToString) _
                                            & ", ID_TIPOLOGIA_EDILE_2=" & RitornaNullSeMenoUno(Me.cmbTipoEdil2.SelectedValue.ToString) _
                                            & ", ID_TIPOLOGIA_EDILE_3=" & RitornaNullSeMenoUno(Me.CmbTipoEdil3.SelectedValue.ToString) _
                                            & ",ID_MANCATO_RILIEVO=" & RitornaNullSeMenoUno(Me.cmbStatoFisico.SelectedValue) & ", " _
                                            & "NUM_PASSI_CARRABILI=" & par.IfEmpty(Me.txtpCarrabili.Text, "NULL") _
                                            & ",DATA_RILASCIO_CPI='" & par.AggiustaData(CType(Me.Tab_CPI1.FindControl("TxtDataRilascio"), TextBox).Text) _
                                            & "',DATA_SCADENZA_CPI='" & par.AggiustaData(CType(Me.Tab_CPI1.FindControl("TxtDataScadenza"), TextBox).Text) _
                                            & "', SCONTO_COSTO_BASE = " & par.IfEmpty(Me.txtScCostoBase.Text, "null") & ", id_microzona = " _
                                            & RitornaNullSeMenoUno(Me.cmbMicrozona.SelectedValue.ToString) _
                                            & ",id_zona = " & RitornaNullSeMenoUno(Me.cmbZona.SelectedValue.ToString) _
                                            & ",COD_RIF_LEG = " & RitornaNullSeMenoUno(Me.cmbRifLeg.SelectedValue.ToString) _
                                            & ",id_bm=" & par.insDbValue(cmbBuildingManager.SelectedValue, False, , True) _
                                            & ", UNITA_NON_PROPRIETA = " & par.IfEmpty(txtUnitaNP.Text, 0) _
                                            & ", MQ_ESTERNI = " & par.IfEmpty(txtMqEsterni.Text, "Null") _
                                            & ", MQ_PILOTY = " & par.IfEmpty(txtMqPiloty.Text, "Null") _
                                            & ", NUMERO_BIDONI_CARTA = " & par.IfEmpty(txtNumBidoniCarta.Text, "Null") _
                                            & ", NUMERO_BIDONI_VETRO = " & par.IfEmpty(txtNumBidoniVetro.Text, "Null") _
                                            & ", NUMERO_BIDONI_UMIDO = " & par.IfEmpty(txtNumBidoniUmido.Text, "Null") _
                                            & ", FL_SPAZI_ESTERNI = " & spaziEsterni _
                                            & " WHERE ID = " & vId
                        par.cmd.ExecuteNonQuery()
                    Else
                        'MODIFICATO CON I NUOVI CAMPI DEL CENSIMENTO MANUTENTIVO
                        par.cmd.CommandText = ""
                        par.cmd.CommandText = "UPDATE SISCOM_MI.EDIFICI_TMP " _
                                            & "SET COD_EDIFICIO_GIMI = '" & par.PulisciStrSql(Me.TxtGimi.Text) _
                                            & "', DENOMINAZIONE= '" & par.PulisciStrSql(Me.TxtDenEdificio.Text) _
                                            & "', NUM_PIANI_ENTRO = " & Me.CmbEntroTerra.SelectedItem.Text _
                                            & ", NUM_PIANI_FUORI= " & Me.CmbFuoriTerra.SelectedItem.Text _
                                            & ", NUM_SCALE = " & Me.TxtNumScale.Text _
                                            & ", DATA_COSTRUZIONE = '" & par.AggiustaData(Me.TxtDataCostr.Text) _
                                            & "', DATA_RISTRUTTURAZIONE = '" & par.AggiustaData(Me.TxtDataRistrut.Text) _
                                            & "', COD_TIPOLOGIA_EDIFICIO = " & RitornaNullSeMenoUno(Me.DrLTipoEdificio.SelectedValue) _
                                            & ", COD_UTILIZZO_PRINCIPALE = " & RitornaNullSeMenoUno(Me.DrLUtilizzo.SelectedValue) _
                                            & ", COD_TIPOLOGIA_COSTRUTTIVA = " & RitornaNullSeMenoUno(Me.DrLTipCostrut.SelectedValue) _
                                            & ", COD_LIVELLO_POSSESSO = '" & Me.DrLLivelloPoss.SelectedValue _
                                            & "', CONDOMINIO = " & (Me.CmbCondominio.SelectedValue.ToString) _
                                            & ", COD_TIPOLOGIA_IMP_RISCALD = '" & Me.DrLImpRiscald.SelectedValue _
                                            & "', SINTESI_EDIFICIO = '" & par.PulisciStrSql(Me.txtNote.Text) _
                                            & "', NOTE = '" & par.PulisciStrSql(Me.txtNoteMan.Text) _
                                            & "', PIANO_TERRA = " & (Me.CmbPTerra.SelectedValue.ToString) _
                                            & ",  SEMINTERRATO = " & (Me.CmbSeminterrato.SelectedValue.ToString) _
                                            & ", SOTTOTETTO = " & (Me.CmbSottotetto.SelectedValue.ToString) _
                                            & ", ATTICO = " & (Me.CmbAttico.SelectedValue.ToString) _
                                            & ", SUPERATTICO = " & (Me.CmbSuperAttico.SelectedValue.ToString) _
                                            & ", NUM_MEZZANINI = " & par.IfEmpty(Me.txtNumMezzanini.Text, 0) _
                                            & ", ID_OPERATORE_AGGIORNAMENTO ='" & Session("ID_OPERATORE") _
                                            & "' , NUM_ASCENSORI = " & par.IfEmpty(Me.txtNumAscensori.Text, "Null") _
                                            & ", GEST_RISC_DIR = " & Me.cmbGestDirRisc.SelectedValue.ToString _
                                            & ", ID_TIPOLOGIA_EDILE_1=" & RitornaNullSeMenoUno(Me.cmbTipoEdil1.SelectedValue.ToString) _
                                            & ", ID_TIPOLOGIA_EDILE_2=" & RitornaNullSeMenoUno(Me.cmbTipoEdil2.SelectedValue.ToString) _
                                            & ", ID_TIPOLOGIA_EDILE_3=" & RitornaNullSeMenoUno(Me.CmbTipoEdil3.SelectedValue.ToString) _
                                            & ",ID_MANCATO_RILIEVO=" & RitornaNullSeMenoUno(Me.cmbStatoFisico.SelectedValue) & ", " _
                                            & "NUM_PASSI_CARRABILI=" & par.IfEmpty(Me.txtpCarrabili.Text, "NULL") _
                                            & ",DATA_RILASCIO_CPI='" & par.FormattaData(CType(Me.Tab_CPI1.FindControl("TxtDataRilascio"), TextBox).Text) _
                                            & "',DATA_SCADENZA_CPI='" & par.FormattaData(CType(Me.Tab_CPI1.FindControl("TxtDataScadenza"), TextBox).Text) _
                                            & "', SCONTO_COSTO_BASE = " & par.IfEmpty(Me.txtScCostoBase.Text, "null") _
                                            & ", id_microzona = " & RitornaNullSeMenoUno(Me.cmbMicrozona.SelectedValue.ToString) _
                                            & ",id_zona = " & RitornaNullSeMenoUno(Me.cmbZona.SelectedValue.ToString) _
                                            & ",COD_RIF_LEG = " & RitornaNullSeMenoUno(Me.cmbRifLeg.SelectedValue.ToString) _
                                            & ",id_bm=" & par.insDbValue(cmbBuildingManager.SelectedValue, False, , True) _
                                            & ", UNITA_NON_PROPRIETA = " & par.IfEmpty(txtUnitaNP.Text, 0) _
                                            & ", MQ_ESTERNI = " & par.IfEmpty(par.VirgoleInPunti(txtMqEsterni.Text), "Null") _
                                            & ", MQ_PILOTY = " & par.IfEmpty(par.VirgoleInPunti(txtMqPiloty.Text), "Null") _
                                            & ", NUMERO_BIDONI_CARTA = " & par.IfEmpty(par.VirgoleInPunti(txtNumBidoniCarta.Text), "Null") _
                                            & ", NUMERO_BIDONI_VETRO = " & par.IfEmpty(par.VirgoleInPunti(txtNumBidoniVetro.Text), "Null") _
                                            & ", NUMERO_BIDONI_UMIDO = " & par.IfEmpty(par.VirgoleInPunti(txtNumBidoniUmido.Text), "Null") _
                                            & ", FL_SPAZI_ESTERNI = " & spaziEsterni _
                                            & " WHERE ID = " & vId
                        par.cmd.ExecuteNonQuery()

                    End If


                    '************INSERIMENTO DEI DATI INSERITI NEL TAB C.P.I*************
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.CPI_EDIFICI WHERE ID_EDIFICIO = " & vId
                    par.cmd.ExecuteNonQuery()

                    If DirectCast(Me.Tab_CPI1.FindControl("Attivita"), CheckBoxList).Items.Count > 0 Then
                        For Each o As Object In DirectCast(Me.Tab_CPI1.FindControl("Attivita"), CheckBoxList).Items
                            Dim item As System.Web.UI.WebControls.ListItem
                            item = CType(o, System.Web.UI.WebControls.ListItem)
                            If item.Selected Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.CPI_EDIFICI (ID_EDIFICIO,ID_ATTIVITA_CPI ) VALUES (" & vId & "," & item.Value & ")"
                                par.cmd.ExecuteNonQuery()
                            End If
                        Next
                    End If
                    '************FINE DEI DATI INSERITI NEL TAB C.P.I*************

                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI (ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','')"
                    par.cmd.ExecuteNonQuery()
                    '*******************************FINE****************************************
                    '***************************************************************************

                    '***************SALVATAGGIO SCALE_EDIFICI***************'
                    SalvaScaleEdifici()
                    '*******************************************************'

                    MemorizzaAttributi()
                    CaricaAttributi()

                    par.myTrans.Commit()
                    '*********************PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                    If My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/ED/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodEdificio.Text) & "*.*").Count > 0 Or My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/ED/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodEdificio.Text) & "*.*").Count > 0 Then
                        Me.btnFoto.Visible = True
                        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
                    Else
                        Me.btnFoto.Visible = True
                        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan2.png"
                    End If

                    'Riapro una nuova transazione
                    Session.Item("LAVORAZIONE") = "1"
                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                    Me.attivabottoni()
                    Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
                Else
                    Response.Write("<SCRIPT>alert('       Impossibile completare!\r\nAvvalorare tutti i campi obbligatori!');</SCRIPT>")
                    Exit Sub
                End If
            End If
            '            Me.Riempicampi()
            Me.ApriRicerca()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        End Try


    End Sub
    Private Function RicavaVial(ByVal indirizzo As String) As String
        Dim pos As Integer
        Dim via As String
        Try

            pos = InStr(1, indirizzo, " ")
            If pos > 0 Then
                via = Mid(indirizzo, 1, pos - 1)
                Select Case via

                    Case "CORSO", "C.SO"
                        RicavaVial = "CORSO"
                    Case "PIAZZA", "PZ.", "P.ZZA"
                        RicavaVial = "PIAZZA"
                    Case "PIAZZALE", "P.LE"
                        RicavaVial = "PIAZZALE"
                    Case "P.T"
                        RicavaVial = "PORTA"
                    Case "S.T.R.", "STRADA"
                        RicavaVial = "STRADA"
                    Case "V.", "VIA"
                        RicavaVial = "VIA"
                    Case "VIALE", "V.LE"
                        RicavaVial = "VIALE"
                    Case "LARGO"
                        RicavaVial = "LARGO"
                    Case "VICO"
                        RicavaVial = "VICO"
                    Case "VICOLO"
                        RicavaVial = "VICOLO"
                    Case "ALTRO"
                        RicavaVial = "ALTRO"
                    Case "ALZAIA"
                        RicavaVial = "ALZAIA"
                    Case "RIPA"
                        RicavaVial = "RIPA"
                    Case "CALLE"
                        RicavaVial = "CALLE"
                    Case Else
                        RicavaVial = "VIA"
                End Select

            Else
                RicavaVial = ""
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Source
        End Try
    End Function
    Private Function RicavaDescVia(ByVal indirizzo As String) As String
        Try

            Dim pos As Integer
            Dim descrizione As String

            pos = InStr(1, indirizzo, " ")
            If pos > 0 Then
                descrizione = Mid(indirizzo, pos + 1)
                RicavaDescVia = descrizione
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Source
        End Try
    End Function
    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String

        Try

            Dim a As String
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If
            Return a
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Source
        End Try
    End Function


    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Dim ERRORE As String = ""
        If par.ControllaCAP(Me.DrLComune.SelectedValue.ToString, Me.TxtCAP.Text, ERRORE) = False Then
            Response.Write("<SCRIPT>alert('CAP errato!I possibili valori sono:\n" & ERRORE & "');</SCRIPT>")
            Exit Sub
        End If
        If Me.cmbGestDirRisc.SelectedValue = 1 Then
            If Me.DrLImpRiscald.SelectedValue <> "CENT" Then
                Response.Write("<SCRIPT>alert('Impianto di riscaldamento non coerente con Gestione Diretta Riscaldamento!');</SCRIPT>")
                Me.cmbGestDirRisc.SelectedValue = 0
                Exit Sub
            End If
        End If


        If vId <> 0 Then
            Me.Update()
        Else
            Me.save()
        End If
        'Me.txtindietro.Text = txtindietro.Text - 1
        Me.txtModificato.Value = "0"
        CaricaGridEdifici()

    End Sub

    'Protected Sub ImgBtnScale_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnScale.Click
    '    If vId > 0 Then
    '        Me.txtindietro.Text = txtindietro.Text - 1

    '        Response.Write("<script>popupWindow=window.open('Scale.aspx?ID=" & vId & "','VARIAZIONI', 'resizable=no, width=400, height=300');</script>")
    '    End If
    'End Sub

    Protected Sub DrLComune_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLComune.SelectedIndexChanged
        If Me.DrLComune.SelectedValue <> "" Then
            'Me.TxtCodComun.Text = Me.DrLComune.SelectedValue.ToString
        End If
    End Sub

    'Protected Sub ImgBtnMillesimali_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnMillesimali.Click
    '    Me.txtindietro.Text = txtindietro.Text - 1
    '    Response.Write("<script>window.open('InsTabMillesim.aspx?ID=" & vId & ",&Pas=ED','MILLESIMALI', 'resizable=no, width=630, height=280');</script>")

    'End Sub

    'Protected Sub btnUtenzaMill_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUtenzaMill.Click
    '    Me.txtindietro.Text = txtindietro.Text - 1
    '    If EsistonoMilles() Then
    '        Response.Write("<script>window.open('ListaUtenze.aspx?ID=" & vId & ",&Pas=ED','UTMILLESIMALI', 'resizable=no, width=630, height=280');</script>")

    '    Else

    '        Exit Sub
    '    End If
    'End Sub


    Protected Sub btnUniImmob_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUniImmob.Click

        If vId <> -1 Then
            If Me.txtModificato.Value <> "111" Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.OracleConn.Close()

                HttpContext.Current.Session.Remove("TRANSAZIONE")
                HttpContext.Current.Session.Remove("CONNESSIONE")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Item("LAVORAZIONE") = 0
                Response.Redirect("EdificiUI.aspx?X=" & Request.QueryString("X") & "&E=" & vId & "&COMPLESSO=" & Request.QueryString("COMPLESSO"))
            Else
                Me.txtModificato.Value = "1"
                CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            End If

        End If

    End Sub



    Protected Sub imgStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStampa.Click
        If vId <> -1 Then
            If Me.txtModificato.Value <> "111" Then
                Response.Write("<script>window.open('StampaE.aspx?ID=" & vId & "', '');</script>")
            Else
                Me.txtModificato.Value = "1"
                CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            End If
            'Me.txtindietro.Text = txtindietro.Text - 1

        End If
    End Sub


    Protected Sub btnIndietro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnIndietro.Click
        'Me.txtindietro.Text = txtindietro.Text - 1
        Try
            If Me.txtModificato.Value <> "111" Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.cmd.Dispose()
                par.OracleConn.Close()

                HttpContext.Current.Session.Remove("TRANSAZIONE")
                HttpContext.Current.Session.Remove("CONNESSIONE")
                HttpContext.Current.Session.Remove("DT_SCALE_EDIFICI")

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Item("LAVORAZIONE") = 0

                Select Case Request.QueryString("C")
                    Case "RisultatiEdifici"
                        Response.Redirect(Request.QueryString("C") & ".aspx?C=" & Request.QueryString("COMPLESSO") & "&E=" & Request.QueryString("EDIFICIO"))
                    Case "CompUC"
                        If Session.Item("SLE") = 1 Then
                            Response.Redirect("InserimentoComplessi.aspx?ID=" & Request.QueryString("COMPLESSO"))
                        Else

                            Response.Redirect("RisultatiEdifici.aspx?C=" & Request.QueryString("COMPLESSO") & "&E=" & vId)
                        End If

                    Case "EdificiUI"
                        If Session.Item("SLE") = 1 Then
                            Response.Redirect("InserimentoComplessi.aspx?ID=" & Request.QueryString("COMPLESSO"))
                        Else
                            Response.Redirect("RisultatiEdifici.aspx?C=" & Request.QueryString("COMPLESSO") & "&E=" & vId)
                        End If

                    Case "RisultSchede"
                        Response.Redirect("RisultSchede.aspx?SCHEDA=" & Request.QueryString("SK"))


                    Case Nothing
                        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

                    Case Else
                        Response.Redirect(Request.QueryString("C") & ".aspx?ID=" & Request.QueryString("COMPLESSO"))
                End Select
            Else
                Me.txtModificato.Value = "1"
                CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub


    Protected Sub btnUniCom_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnUniCom.Click
        If vId <> -1 Then
            If Me.txtModificato.Value <> "111" Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.OracleConn.Close()

                HttpContext.Current.Session.Remove("TRANSAZIONE")
                HttpContext.Current.Session.Remove("CONNESSIONE")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Item("LAVORAZIONE") = 0
                Response.Redirect("CompUC.aspx?X=" & Request.QueryString("X") & "&E=" & vId & "&PAS=ED&COMPLESSO=" & Request.QueryString("COMPLESSO"))
            Else
                Me.txtModificato.Value = "1"
                CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            End If

        End If

    End Sub

    Protected Sub DdLComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdLComplesso.SelectedIndexChanged
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

            par.cmd.CommandText = "SELECT COD_COMUNE,ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID = " & Me.DdLComplesso.SelectedValue.ToString & " AND INDIRIZZI.ID = COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.DrLComune.SelectedValue = par.IfNull(myReader1("COD_COMUNE"), "-1")
                Me.DrLComune.Enabled = False
                caricaBM(par.IfNull(myReader1("ID_FILIALE"), -1))
                'Me.TxtCodComun.Text = Me.DrLComune.SelectedValue.ToString
            Else
                Me.DrLComune.Enabled = True
            End If

            myReader1.Close()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Private Sub caricaBM(ByVal idStruttura As Integer)
        Dim condizioneIdS As String = ""
        If idStruttura <> 0 Then
            condizioneIdS = " where id_Struttura=" & idStruttura
        End If
        par.caricaComboBox("SELECT id,((CODICE )|| (CASE WHEN (SELECT OPERATORE FROM OPERATORI WHERE ID =(SELECT ID_OPERATORE FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE ID_BM = BUILDING_MANAGER.ID AND TIPO_OPERATORE = 1 AND NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD') AND NVL (inizio_validita, '29991231') <= TO_CHAR (SYSDATE, 'YYYYMMDD'))) IS NOT NULL THEN  ' - '||(SELECT operatori.cognome||' '||operatori.nome FROM OPERATORI WHERE ID =(SELECT ID_OPERATORE FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE ID_BM = BUILDING_MANAGER.ID AND TIPO_OPERATORE = 1 AND NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD') AND NVL (inizio_validita, '29991231') <= TO_CHAR (SYSDATE, 'YYYYMMDD'))) ELSE '' END)|| (CASE WHEN (SELECT OPERATORE FROM OPERATORI WHERE ID =(SELECT ID_OPERATORE FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE ID_BM = BUILDING_MANAGER.ID AND TIPO_OPERATORE = 2 AND NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD'))) IS NOT NULL THEN  ' - '||(SELECT operatori.cognome||' '||operatori.nome FROM OPERATORI WHERE ID =(SELECT ID_OPERATORE FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE ID_BM = BUILDING_MANAGER.ID AND TIPO_OPERATORE = 2 AND NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD'))) ELSE '' END) )AS codice FROM siscom_mi.BUILDING_MANAGER " & condizioneIdS & " order by CODICE ASC", cmbBuildingManager, "ID", "CODICE")
    End Sub
    Private Sub ApriFrmWithDBLock()
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim dt As New Data.DataTable
        Dim dt2 As New Data.DataTable
        Dim appoggioDate As String

        Try
            If vId <> -1 Then

                'SE SI PROVIENE DA UNA RICERCA DOPO AVER RIEMPITO I CAMPI SETTO LA MIA CONNESSIONE CHE RESTERà VALIDA PER TUTTA LA DURATA DI APERTURA DEL FORM
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "SELECT EDIFICI.*,(select id_filiale from siscom_mi.complessi_immobiliari ci where ci.id=edifici.id_complesso) as idfiliale FROM SISCOM_MI.EDIFICI WHERE ID = " & vId
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    Me.TxtGimi.Text = par.IfNull((dt.Rows(0).Item("COD_EDIFICIO_GIMI")), "- -")
                    Me.DdLComplesso.SelectedValue = (dt.Rows(0).Item("ID_COMPLESSO"))
                    Me.DdLComplesso.Enabled = False
                    appoggioDate = par.IfNull(dt.Rows(0).Item("DATA_COSTRUZIONE"), "dd/mm/YYYY")
                    If appoggioDate <> "dd/mm/YYYY" Then
                        Me.TxtDataCostr.Text = par.FormattaData(appoggioDate)
                    Else
                        Me.TxtDataCostr.Text = ""
                    End If
                    appoggioDate = par.IfNull(dt.Rows(0).Item("DATA_RISTRUTTURAZIONE"), "dd/mm/YYYY")

                    If appoggioDate <> "dd/mm/YYYY" Then
                        Me.TxtDataRistrut.Text = par.FormattaData(appoggioDate)
                    Else
                        Me.TxtDataRistrut.Text = ""
                    End If

                    Me.DrLTipoEdificio.SelectedValue = (par.IfNull(dt.Rows(0).Item("COD_TIPOLOGIA_EDIFICIO"), "-1"))
                    Me.DrLUtilizzo.SelectedValue = (par.IfNull(dt.Rows(0).Item("COD_UTILIZZO_PRINCIPALE"), "-1"))

                    Me.DrLTipCostrut.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_TIPOLOGIA_COSTRUTTIVA"), "-1")

                    Me.DrLLivelloPoss.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_LIVELLO_POSSESSO"), "-1")


                    Me.DrLImpRiscald.SelectedValue = (dt.Rows(0).Item("COD_TIPOLOGIA_IMP_RISCALD"))
                    Me.txtCodEdificio.Text = par.IfNull((dt.Rows(0).Item("COD_EDIFICIO")), "ND")
                    Me.txtpCarrabili.Text = par.IfNull(dt.Rows(0).Item("NUM_PASSI_CARRABILI"), "")

                    '*******************PEPPE MODIFY 10/09/2010 NUOVI CAMPI CENSIMENTO STATO MANUTENTIVO

                    'Me.cmbTipolStrutt.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_TIPOLOGIA_STRUTTURA"), "-1")
                    Me.cmbTipoEdil1.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_TIPOLOGIA_EDILE_1"), "-1")
                    Me.cmbTipoEdil2.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_TIPOLOGIA_EDILE_2"), "-1")
                    Me.CmbTipoEdil3.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_TIPOLOGIA_EDILE_3"), "-1")
                    Me.cmbStatoFisico.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_MANCATO_RILIEVO"), "-1")


                    If par.IfNull(dt.Rows(0).Item("CONDOMINIO").ToString, "null") <> "null" Then
                        Me.CmbCondominio.SelectedValue = dt.Rows(0).Item("CONDOMINIO").ToString
                    End If
                    If par.IfNull(dt.Rows(0).Item("PIANO_TERRA").ToString, "null") <> "null" Then
                        Me.CmbPTerra.SelectedValue = dt.Rows(0).Item("PIANO_TERRA").ToString
                    End If
                    If par.IfNull(dt.Rows(0).Item("SEMINTERRATO").ToString, "null") <> "null" Then
                        Me.CmbSeminterrato.SelectedValue = dt.Rows(0).Item("SEMINTERRATO").ToString
                    End If

                    If par.IfNull(dt.Rows(0).Item("SOTTOTETTO").ToString, "null") <> "null" Then
                        Me.CmbSottotetto.SelectedValue = dt.Rows(0).Item("SOTTOTETTO").ToString
                    End If
                    If par.IfNull(dt.Rows(0).Item("ATTICO").ToString, "null") <> "null" Then
                        Me.CmbAttico.SelectedValue = dt.Rows(0).Item("ATTICO").ToString
                    End If
                    If par.IfNull(dt.Rows(0).Item("SUPERATTICO").ToString, "null") <> "null" Then
                        Me.CmbSuperAttico.SelectedValue = dt.Rows(0).Item("SUPERATTICO").ToString
                    End If
                    '*******PEPPE MODIFY AGGIUNTA E GESTIONE CAMPO GESTIONE DIRETTA RISCALDAMENTO**********
                    If par.IfNull(dt.Rows(0).Item("GEST_RISC_DIR").ToString, "null") <> "null" Then
                        Me.cmbGestDirRisc.SelectedValue = dt.Rows(0).Item("GEST_RISC_DIR").ToString
                    End If

                    CType(Me.Tab_CPI1.FindControl("TxtDataScadenza"), TextBox).Text = par.FormattaData(par.IfNull(dt.Rows(0).Item("DATA_SCADENZA_CPI"), ""))
                    CType(Me.Tab_CPI1.FindControl("TxtDataRilascio"), TextBox).Text = par.FormattaData(par.IfNull(dt.Rows(0).Item("DATA_RILASCIO_CPI"), ""))

                    Me.TxtDenEdificio.Text = dt.Rows(0).Item("DENOMINAZIONE").ToString
                    Me.CmbFuoriTerra.SelectedValue = par.IfNull(dt.Rows(0).Item("NUM_PIANI_FUORI").ToString, "-1")
                    Me.CmbEntroTerra.SelectedValue = par.IfNull(dt.Rows(0).Item("NUM_PIANI_ENTRO").ToString, "-1")
                    '30/11/2011 PUCCETTONE MODFIFY il numero di mezzanini viene memorizzato, non è più una combo con flag 0/1
                    Me.txtNumMezzanini.Text = par.IfNull(dt.Rows(0).Item("NUM_MEZZANINI").ToString, 0)
                    'If par.IfNull(dt.Rows(0).Item("NUM_MEZZANINI").ToString, "null") <> "null" Then
                    '    Me.DrLMezzanini.SelectedValue = dt.Rows(0).Item("NUM_MEZZANINI").ToString
                    'End If
                    Me.txtNote.Text = dt.Rows(0).Item("SINTESI_EDIFICIO").ToString
                    Me.txtNoteMan.Text = dt.Rows(0).Item("NOTE").ToString

                    Me.TxtNumScale.Text = dt.Rows(0).Item("NUM_SCALE").ToString
                    'Me.TxtSezione.Text = dt.Rows(0).Item("SEZIONE").ToString
                    'Me.TxtFoglio.Text = dt.Rows(0).Item("FOGLIO").ToString
                    'Me.TxtNumero.Text = dt.Rows(0).Item("NUMERO").ToString
                    Me.txtNumAscensori.Text = dt.Rows(0).Item("NUM_ASCENSORI").ToString
                    vIdIndirizzo = dt.Rows(0).Item("ID_INDIRIZZO_PRINCIPALE")
                    caricaBM(dt.Rows(0).Item("IDfiliale"))
                    Me.cmbBuildingManager.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_BM"), "")
                    '*****SELEZIONE DEL'INDIRIZZO A PARTIRE DALL'ID DELL'ID INDIRIZZO PRINCIPALE PESCATO DALL'ID INDIRIZZO
                    da = Nothing
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID = " & dt.Rows(0).Item("ID_INDIRIZZO_PRINCIPALE")
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    da.Fill(dt2)


                    Me.TxtLocalità.Text = dt2.Rows(0).Item("LOCALITA").ToString
                    Me.TxtDescrInd.Text = RicavaDescVia(dt2.Rows(0).Item("DESCRIZIONE").ToString)
                    Me.TxtCAP.Text = dt2.Rows(0).Item("CAP").ToString
                    Me.TxtCivicoKilo.Text = dt2.Rows(0).Item("CIVICO").ToString
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "SELECT COD FROM SISCOM_MI.TIPOLOGIA_INDIRIZZO WHERE DESCRIZIONE =  '" & RicavaVial(dt2.Rows(0).Item("DESCRIZIONE").ToString) & "'"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        Me.DrLTipoInd.SelectedValue = myReader(0)
                    End If
                    myReader.Close()
                    'Me.DrLComune.SelectedValue = -1
                    Me.DrLComune.SelectedValue = (dt2.Rows(0).Item("COD_COMUNE"))
                    'Me.TxtCodComun.Text = dt2.Rows(0).Item("COD_COMUNE").ToString
                    Me.DrLComune.Enabled = False
                    dt = New Data.DataTable
                    da = Nothing

                    '************************scale*******************************
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID_EDIFICIO = " & vId & "ORDER BY DESCRIZIONE ASC"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        Me.cmbScale.SelectedValue = myReader(0)
                    End If
                    'myReader.Close()
                    'par.OracleConn.Close()

                    'da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    'Dim ds As New Data.DataSet()
                    'da.Fill(ds, "INTERV_ADEG_NORM")
                    'DataGridScale.DataSource = ds
                    'DataGridScale.DataBind()
                    '************************************************************

                    '*ABILITO I BOTTONI E I CAMPI AGGIUNTIVI DOPO APERTURA EDIFICIO ESISTENTE DA RICERCA
                    If Me.vId <> 0 Then
                        Me.attivabottoni()
                    End If
                End If
                dbLock.Value = 1
                FrmSolaLettura()

                CType(Tab_Millesimali1.FindControl("BtnADD"), ImageButton).Visible = False
                CType(Tab_Millesimali1.FindControl("btnModifica"), ImageButton).Visible = False
                CType(Tab_Millesimali1.FindControl("btnDelete"), ImageButton).Visible = False

                HyLinkPertinenze.Visible = False
                BtnElimina.Visible = False

                CType(Tab_AdDimens1.FindControl("BtnADD"), ImageButton).Visible = False
                CType(Tab_AdDimens1.FindControl("BtnElimina"), ImageButton).Visible = False

                CType(Tab_AdVarConf1.FindControl("BtnADD"), ImageButton).Visible = False
                CType(Tab_AdVarConf1.FindControl("BtnElimina"), ImageButton).Visible = False

                CType(Tab_AdNormativo1.FindControl("BtnADD"), ImageButton).Visible = False
                CType(Tab_AdNormativo1.FindControl("BtnElimina"), ImageButton).Visible = False

                CType(Tab_UtMillesimali1.FindControl("BtnADD"), ImageButton).Visible = False
                CType(Tab_UtMillesimali1.FindControl("BtnElimina"), ImageButton).Visible = False

                CType(Tab_CPI1.FindControl("TxtDataRilascio"), TextBox).Enabled = False
                CType(Tab_CPI1.FindControl("TxtDataScadenza"), TextBox).Enabled = False

                If par.IfNull(Session.Item("LIVELLO"), 0) <> 1 Then
                    If par.IfNull(Session.Item("MOD_CENS_MANUT"), 0) > 0 Then
                        Me.DrlSchede.Enabled = True
                        Me.cmbPeriodo.Enabled = True
                    End If

                    If Session.Item("CENS_MANUT_SL") > 0 Then
                        Me.CENS_MANUT_SL.Value = 1

                    End If
                Else
                    Me.DrlSchede.Enabled = True
                    Me.cmbPeriodo.Enabled = True
                    Me.CENS_MANUT_SL.Value = 0
                End If
                'Me.ImgBtnScale.Enabled = False
                'Me.ImgButDimens.Enabled = False
                'Me.ImageButton2.Enabled = False
                'Me.ImageButton3.Enabled = False
                'Me.btnUtenzaMill.Enabled = False
                '*********************PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                Try
                    If My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/ED/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodEdificio.Text) & "*.*").Count > 0 Or My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/ED/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.txtCodEdificio.Text) & "*.*").Count > 0 Then
                        Me.btnFoto.Visible = True
                        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
                    Else
                        Me.btnFoto.Visible = True
                        Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan2.png"
                    End If
                Catch ex As Exception
                    Me.LblErrore.Visible = True
                    LblErrore.Text = "ATTENZIONE!Verificare il percorso delle foto e delle planimetrie!"
                End Try
                '*********************FINE CONTROLLO PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Function EsistonoMilles() As Boolean
        'Richiamo la connessione
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        Dim trovatiMillesimali As Boolean = False
        'Richiamo la transazione
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        

        par.cmd.CommandText = "select * from siscom_mi.tabelle_millesimali where id_edificio = " & vId
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader
        If myReader.Read Then
            EsistonoMilles = True
        Else
            Response.Write("<script>alert('Non esistono TABELLE MILLESIMALI da associare ad una UTENZA MILLESIMALE!');</script>")

            EsistonoMilles = False
        End If
        Return EsistonoMilles
    End Function
    Private Sub BindGridScale()
        Try


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            
            Me.cmbScale.Items.Clear()
            cmbScale.Items.Add(New ListItem(" ", -1))
            par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID_EDIFICIO = " & vId & "ORDER BY DESCRIZIONE ASC"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While myReader.Read
                Me.cmbScale.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("id"), -1)))
            End While
            myReader.Close()
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim ds As New Data.DataSet()
            'da.Fill(ds, "INTERV_ADEG_NORM")
            'DataGridScale.DataSource = ds
            'DataGridScale.DataBind()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub



    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        tabdefault1 = ""
        tabdefault2 = ""
        tabdefault3 = ""
        tabdefault4 = ""
        tabdefault5 = ""
        tabdefault6 = ""
        tabdefault7 = ""
        tabdefault8 = ""
        tabdefault9 = ""
        tabdefault10 = ""
        tabdefault11 = "tabbertabhide"
        tabdefault12 = ""
        tabdefault13 = ""

        Select Case txttab.Value
            Case "1"
                tabdefault1 = "tabbertabdefault"
            Case "2"
                tabdefault2 = "tabbertabdefault"
            Case "3"
                tabdefault3 = "tabbertabdefault"
            Case "4"
                tabdefault4 = "tabbertabdefault"
            Case "5"
                tabdefault5 = "tabbertabdefault"
            Case "6"
                tabdefault6 = "tabbertabdefault"
            Case "7"
                tabdefault7 = "tabbertabdefault"
            Case "8"
                tabdefault8 = "tabbertabdefault"
            Case "9"
                tabdefault9 = "tabbertabdefault"
            Case "10"
                tabdefault10 = "tabbertabdefault"
            Case "11"
                tabdefault11 = "tabbertabdefault"
            Case "12"
                tabdefault12 = "tabbertabdefault"
            Case "13"
                tabdefault13 = "tabbertabdefault"

        End Select
    End Sub



    Protected Sub BtnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnElimina.Click
        Try
            If Me.cmbScale.SelectedValue <> "-1" Then

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                

                'par.cmd.CommandText = "DELETE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID = " & txtid.Text

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID = " & Me.cmbScale.SelectedValue.ToString
                par.cmd.ExecuteNonQuery()
                '****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI (ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F56','SCALE EDIFICIO')"
                par.cmd.ExecuteNonQuery()
                '*******************************FINE****************************************
                '***************************************************************************
                CaricaGridEdifici()

                BindGridScale()
                NumeroScale()

                Session.Item("MODIFICASOTTOFORM") = 1
                'Me.txtmia.Text = ""
                'Me.txtid.Text = ""
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 2292 Then
                Response.Write("<script>alert('Scala in uso!Impossibile Eliminare!');</script>")
                par.myTrans.Rollback()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try
    End Sub

    Protected Sub BtnOK_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnOK.Click
        Try
            If vId <> 0 Then

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                

                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID_EDIFICIO = " & vId

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                myReader1 = par.cmd.ExecuteReader()

                While myReader1.Read
                    If Me.TxtScala.Text.ToUpper = par.IfNull(myReader1("DESCRIZIONE"), " ").ToString.ToUpper Then
                        Response.Write("<SCRIPT>alert('Scala esistente per questo edificio!');</SCRIPT>")
                        par.cmd.CommandText = ""
                        Exit Sub
                    End If
                End While

                If Me.TxtScala.Text <> "" Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SCALE_EDIFICI(ID,ID_EDIFICIO,DESCRIZIONE) VALUES (SISCOM_MI.SEQ_SCALE_EDIFICI.NEXTVAL , " & vId & ", '" & par.PulisciStrSql(Me.TxtScala.Text.ToUpper) & "')"
                    par.cmd.ExecuteNonQuery()
                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI (ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','MODIFICA SCALE')"
                    par.cmd.ExecuteNonQuery()
                    '*******************************FINE****************************************
                    '***************************************************************************

                    Session.Item("MODIFICASOTTOFORM") = 1
                    Me.TxtScala.Text = ""
                End If
                CaricaGridEdifici()
                BindGridScale()
                NumeroScale()
            Else
                Response.Write("<script>alert('Salvare prima i dati dell\'edificio, e poi le scale!');</script>")

            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
        End Try
    End Sub



    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Protected Sub btnFoto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnFoto.Click
        Response.Write("<script>window.open('FotoImmobile.aspx?T=E&ID=" & vId & "&I=" & vIdIndirizzo & "', '');</script>")

    End Sub

    Protected Sub CmbCondominio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCondominio.SelectedIndexChanged
        Try

            If Me.CmbCondominio.SelectedValue = 1 Then
                Me.cmbGestDirRisc.Enabled = True
            Else
                Me.cmbGestDirRisc.Enabled = False
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Private Sub LoadDrlSchede()
        Me.DrlSchede.Items.Add(New ListItem("- - - - - - - - - - - - - - - - - - ", -1))
        Me.DrlSchede.Items.Add(New ListItem("Sc. A-RILIEVO STRUTTURE", 0))
        Me.DrlSchede.Items.Add(New ListItem("Sc. B-SCHEDA RILIEVO CHIUSURE", 1))
        Me.DrlSchede.Items.Add(New ListItem("Sc. C-SCHEDA RILIEVO PARTIZIONI INTERNE", 2))
        Me.DrlSchede.Items.Add(New ListItem("Sc. D-SCHEDA RILIEVO PAVIMENTAZIONI INTERNE", 3))
        Me.DrlSchede.Items.Add(New ListItem("Sc. E-SCHEDA RILIEVO PROTEZIONE E DELIMITAZIONI", 4))
        Me.DrlSchede.Items.Add(New ListItem("Sc. F-SCHEDA RILIEVO ATTREZZATURE E SPAZI INTERNI", 5))
        Me.DrlSchede.Items.Add(New ListItem("Sc. G-SCHEDA RILIEVO ATTREZZATURE ED ARREDI ESTERNI", 6))
        Me.DrlSchede.Items.Add(New ListItem("Sc. H-SCHEDA RILIEVO IMPIANTI FISSI DI TRASPORTO", 7))
        Me.DrlSchede.Items.Add(New ListItem("Sc. I-SCHEDA RILIEVO IMPIANTI RISCALDAMENTO E PRODUZIONE H2O CENTRALIZZATA", 8))
        Me.DrlSchede.Items.Add(New ListItem("Sc. L-SCHEDA RILIEVO IMPIANTI IDRICO SANITARI", 9))
        Me.DrlSchede.Items.Add(New ListItem("Sc. M-SCHEDA RILIEVO IMPIANTI ANTINCENDIO", 10))
        Me.DrlSchede.Items.Add(New ListItem("Sc. N-SCHEDA RILIEVO RETE SCARICO / FOGNARIA", 11))
        Me.DrlSchede.Items.Add(New ListItem("Sc. O-SCHEDA RILIEVO IMPIANTI SMALTIMENTO AERIFORMI ", 12))
        Me.DrlSchede.Items.Add(New ListItem("Sc. P-SCHEDA RILIEVO INPIANTO DI DISTRIBUZIONE GAS ", 13))
        Me.DrlSchede.Items.Add(New ListItem("Sc. Q-SCHEDA RILIEVO IMPIANTI ELETTRICI", 14))
        Me.DrlSchede.Items.Add(New ListItem("Sc. R-SCHEDA RILIEVO IMPIANTI TELEVISIVI", 15))
        Me.DrlSchede.Items.Add(New ListItem("Sc. S-SCHEDA RILIEVO IMPIANTI CITOFONI", 16))
        Me.DrlSchede.Items.Add(New ListItem("Sc. T-SCHEDA RILIEVO IMPIANTI DI TELECOMUNICAZIONE", 17))
        Me.DrlSchede.Enabled = False


    End Sub
    Private Sub SelezionaSkeda(ByVal scheda As String)
        Select Case scheda
            Case "A"
                Me.DrlSchede.SelectedValue = 0
            Case "B"
                Me.DrlSchede.SelectedValue = 1
            Case "C"
                Me.DrlSchede.SelectedValue = 2
            Case "D"
                Me.DrlSchede.SelectedValue = 3
            Case "E"
                Me.DrlSchede.SelectedValue = 4
            Case "F"
                Me.DrlSchede.SelectedValue = 5
            Case "G"
                Me.DrlSchede.SelectedValue = 6
            Case "H"
                Me.DrlSchede.SelectedValue = 7
            Case "I"
                Me.DrlSchede.SelectedValue = 8
            Case "L"
                Me.DrlSchede.SelectedValue = 9
            Case "M"
                Me.DrlSchede.SelectedValue = 10
            Case "N"
                Me.DrlSchede.SelectedValue = 11
            Case "O"
                Me.DrlSchede.SelectedValue = 12
            Case "P"
                Me.DrlSchede.SelectedValue = 13
            Case "Q"
                Me.DrlSchede.SelectedValue = 14
            Case "R"
                Me.DrlSchede.SelectedValue = 15
            Case "S"
                Me.DrlSchede.SelectedValue = 16
            Case "T"
                Me.DrlSchede.SelectedValue = 17
            Case "X"
                Me.DrlSchede.SelectedValue = 18
            Case Else
                Me.DrlSchede.SelectedValue = -1

        End Select
    End Sub
    Private Sub dgvProgrInt_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvProgrInt.NeedDataSource
        Try
            par.cmd.CommandText = "SELECT id,(SELECT descrizione FROM siscom_mi.programmazione_interventi WHERE id = id_programmazione_interventi) AS PROGRAMMA_INTERVENTO," _
                                & " (SELECT 'Appalto: ' || nome_appalto || ' - Commessa: ' || commessa || ' - DL: ' || direttore_lavori || ' - Ditta: ' || ditta_esecutrice || ' - Data: ' || getdata(data_aggiudicazione) " _
                                & " FROM siscom_mi.riatti_appalti WHERE id = id_riatti_appalti) AS APPALTO " _
                                & " FROM siscom_mi.EDIFICI_PROGR_INTERV where ID_EDIFICIO = " & vId
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt

        Catch ex As Exception
            par.OracleConn.Close()
            Me.LblErrore.Visible = True
            LblErrore.Text = "dgvProgrInt_NeedDataSource" & ex.Message
        End Try
    End Sub

    Private Sub cmbProgrInt_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbProgrInt.SelectedIndexChanged
        Try
            Dim stringa As String = "select ID, 'Appalto: ' || nome_appalto || ' - Commessa: ' || commessa || ' - DL: ' || direttore_lavori || ' - Ditta: ' || ditta_esecutrice || ' - Data: ' || getdata(data_aggiudicazione) as descrizione from SISCOM_MI.riatti_appalti WHERE ID_PROGRAMMAZIONE_INTERVENTI = " & cmbProgrInt.SelectedValue
            par.caricaComboTelerik(stringa, cmbAppalto, "ID", "DESCRIZIONE", True)
            Dim script As String = "function f(){var rad= $find(""" + RadWindowProgrIntervento.ClientID + """); rad.show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        Catch ex As Exception
            par.OracleConn.Close()
            Me.LblErrore.Visible = True
            LblErrore.Text = "cmbProgrInt_SelectedIndexChanged" & ex.Message
        End Try
    End Sub

    Private Sub btnSalvaProgrInt_Click(sender As Object, e As EventArgs) Handles btnSalvaProgrInt.Click
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_EDIFICI_PROGR_INTERV.NEXTVAL FROM DUAL"


            If HiddenIdProgrInterv.Value = "" Then
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_COMPLESSI_PROGR_INTERV.NEXTVAL FROM DUAL"
                Dim seqProgrInt As Integer = CInt(par.cmd.ExecuteScalar)
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EDIFICI_PROGR_INTERV ( " _
                              & "    ID, ID_EDIFICIO, ID_COMPLESSO, ID_PROGRAMMAZIONE_INTERVENTI,  " _
                              & "    ID_RIATTI_APPALTI)  " _
                              & " VALUES (" & seqProgrInt & " /* ID */, " _
                              & vId & "  /* ID_EDIFICIO */, " _
                              & " (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID = " & vId & ")," _
                              & cmbProgrInt.SelectedValue & "  /* ID_PROGRAMMAZIONE_INTERVENTI */, " _
                              & cmbAppalto.SelectedValue & "  /* ID_RIATTI_APPALTI */ ) "
                par.cmd.ExecuteNonQuery()
                'APPALTO E PROGRAMMAZIONE INTERVENTI IN CASCATA SU EDIFICI ED UNITA'
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.UNITA_PROGR_INTERV(ID, ID_EDIFICIO, ID_UNITA_IMMOBILIARI, ID_PROGRAMMAZIONE_INTERVENTI,ID_RIATTI_APPALTI, ID_STATO ) " _
                & "SELECT SISCOM_MI.SEQ_UNITA_PROGR_INTERV.NEXTVAL, ID_EDIFICIO,ID, " & cmbProgrInt.SelectedValue & "," _
                & cmbAppalto.SelectedValue & ", 0 FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & vId _
                & " AND ID NOT IN (SELECT ID_UNITA_IMMOBILIARI FROM SISCOM_MI.UNITA_PROGR_INTERV WHERE ID_EDIFICIO = " & vId & ")"
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "UPDATE SISCOM_MI.EDIFICI_PROGR_INTERV " _
                                    & " SET    ID_PROGRAMMAZIONE_INTERVENTI = " & cmbProgrInt.SelectedValue & ", " _
                                    & "        ID_RIATTI_APPALTI            = " & cmbAppalto.SelectedValue & " " _
                                    & " WHERE  ID                           = " & HiddenIdProgrInterv.Value
                par.cmd.ExecuteNonQuery()
                'par.cmd.CommandText = "UPDATE SISCOM_MI.UNITA_PROGR_INTERV " _
                '                    & " SET    ID_PROGRAMMAZIONE_INTERVENTI   = " & cmbProgrInt.SelectedValue & ", " _
                '                    & "        ID_RIATTI_APPALTI              = " & cmbAppalto.SelectedValue & " " _
                '                    & " WHERE  ID_EDIFICIO                    IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID_COMPLESSO FROM SISCOM_MI.COMPLESSI_PROGR_INTERV WHERE ID = " & HiddenIdProgrInterv.Value & ")) "
                'par.cmd.ExecuteNonQuery()
            End If
            dgvProgrInt.Rebind()
        Catch ex As Exception
            par.OracleConn.Close()
            Me.LblErrore.Visible = True
            LblErrore.Text = "btnSalvaProgrInt_Click" & ex.Message
        End Try
    End Sub

    Private Sub dgvProgrInt_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgvProgrInt.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('HiddenIdProgrInterv').value='" & dataItem("ID").Text & "';")
            e.Item.Attributes.Add("onDblClick", "document.getElementById('btnApriProgrInterv').click();")
        End If
    End Sub

    Private Sub btnAddProgrInt_Click(sender As Object, e As ImageClickEventArgs) Handles btnAddProgrInt.Click
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.EDIFICI_PROGR_INTERV WHERE ID_EDIFICIO IN " _
                                & " (SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_PROGR_INTERV WHERE ID_STATO = 0) AND ID_EDIFICIO = " & vId
            Dim numero As Integer = CInt(par.cmd.ExecuteScalar)
            If numero = 0 Then
                Dim script As String = "function f(){var rad= $find(""" + RadWindowProgrIntervento.ClientID + """); rad.show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "errore", "alert('Impossibile inserire un nuovo elemento!\nC\'è ancora un programma intervento in corso');", True)
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Me.LblErrore.Visible = True
            LblErrore.Text = "btnAddProgrInt_Click" & ex.Message
        End Try
    End Sub

    Protected Sub dgvProgettiSpeciali_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvProgettiSpeciali.NeedDataSource
        Try
            par.cmd.CommandText = "SELECT distinct ID, " _
                                & "        NOME_PROGETTO, " _
                                & "        GETDATA (DATA_INIZIO_PROGETTO) AS DATA_INIZIO_PROGETTO, " _
                                & "        GETDATA (DATA_FINE_PROGETTO) AS DATA_FINE_PROGETTO " _
                                & "   FROM SISCOM_MI.TAB_RIATTI_PROGETTI_SPECIALI, SISCOM_MI.RIATTI_PROGETTI_SPECIALI " _
                                & "  WHERE RIATTI_PROGETTI_SPECIALI.ID_TAB_RIATTI_PROGETTI_SPEC = " _
                                & "           TAB_RIATTI_PROGETTI_SPECIALI.ID " _
                                & " AND RIATTI_PROGETTI_SPECIALI.ID_EDIFICIO = " & vId
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
        Catch ex As Exception
            par.OracleConn.Close()
            Me.LblErrore.Visible = True
            LblErrore.Text = "dgvProgettiSpeciali_NeedDataSource" & ex.Message
        End Try
    End Sub

    Protected Sub dgvElencoFunSpeciali_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvElencoFunSpeciali.NeedDataSource
        Try
            'par.cmd.CommandText = "SELECT 'FALSE' AS CHECKED, " _
            '                    & "        ID, " _
            '                    & "        NOME_PROGETTO, " _
            '                    & "        GETDATA (DATA_INIZIO_PROGETTO) AS DATA_INIZIO_PROGETTO, " _
            '                    & "        GETDATA (DATA_FINE_PROGETTO) AS DATA_FINE_PROGETTO " _
            '                    & "   FROM SISCOM_MI.RIATTI_PROGETTI_SPECIALI " _
            '                    & "  WHERE ID NOT IN (SELECT UNITA_RIATTI_PROGETTI_SPECIALI.ID_TAB_RIATTI_PROGETTI_SPEC " _
            '                    & "                     FROM SISCOM_MI.UNITA_RIATTI_PROGETTI_SPECIALI " _
            '                    & "                    WHERE     UNITA_RIATTI_PROGETTI_SPECIALI.ID_TAB_RIATTI_PROGETTI_SPEC = " _
            '                    & "                                 RIATTI_PROGETTI_SPECIALI.ID " _
            '                    & "                          AND ID_UNITA_IMMOBILIARE = " & vId & ") "
            par.cmd.CommandText = "SELECT ( CASE WHEN(SELECT distinct RIATTI_PROGETTI_SPECIALI.ID_TAB_RIATTI_PROGETTI_SPEC " _
                                & "           FROM SISCOM_MI.RIATTI_PROGETTI_SPECIALI " _
                                & "          WHERE RIATTI_PROGETTI_SPECIALI.ID_TAB_RIATTI_PROGETTI_SPEC = " _
                                & "                   TAB_RIATTI_PROGETTI_SPECIALI.ID AND ID_EDIFICIO = " & vId & ") IS NOT NULL THEN 'TRUE' ELSE 'FALSE' END) " _
                                & "           AS CHECKED, " _
                                & "        ID, " _
                                & "        NOME_PROGETTO, " _
                                & "        GETDATA (DATA_INIZIO_PROGETTO) AS DATA_INIZIO_PROGETTO, " _
                                & "        GETDATA (DATA_FINE_PROGETTO) AS DATA_FINE_PROGETTO " _
                                & "   FROM SISCOM_MI.TAB_RIATTI_PROGETTI_SPECIALI "
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Protected Sub btn_SalvaProgSpeciali_Click(sender As Object, e As System.EventArgs) Handles btn_SalvaProgSpeciali.Click
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.RIATTI_PROGETTI_SPECIALI WHERE ID_EDIFICIO = " & vId
            par.cmd.ExecuteNonQuery()
            For i As Integer = 0 To dgvElencoFunSpeciali.Items.Count - 1

                If DirectCast(dgvElencoFunSpeciali.Items(i).FindControl("ChkSeleziona"), CheckBox).Checked = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RIATTI_PROGETTI_SPECIALI ( " _
                                        & "    ID_EDIFICIO, ID_TAB_RIATTI_PROGETTI_SPEC) " _
                                        & " VALUES ( " & vId & "/* ID_UNITA_IMMOBILIARE */," _
                                        & dgvElencoFunSpeciali.Items(i).Cells(2).Text & "  /* ID_TAB_RIATTI_PROGETTI_SPEC */ )"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RIATTI_PROGETTI_SPECIALI(ID_COMPLESSO,ID_EDIFICIO,ID_UNITA_IMMOBILIARE, ID_TAB_RIATTI_PROGETTI_SPEC) " _
                                    & " SELECT EDIFICI.ID_COMPLESSO, " _
                                    & "        EDIFICI.ID AS ID_EDIFICIO, " _
                                    & "        UNITA_IMMOBILIARI.ID AS ID_UNITA_IMMOBILIARE, " & dgvElencoFunSpeciali.Items(i).Cells(2).Text _
                                    & "   FROM SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI " _
                                    & "  WHERE     EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO(+) " _
                                    & " and edifici.id = " & vId
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI (ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & "VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                        & "','F291' ,'Inserimento valore ''" & dgvElencoFunSpeciali.Items(i).Cells(4).Text & "''')"
                    par.cmd.ExecuteNonQuery()
                End If

            Next
            dgvProgettiSpeciali.Rebind()

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Private Function MemorizzaAttributi() As Boolean
        Dim ELENCOERRORI As String = ""
        Try
            Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
            Dim CTRL As Control = Nothing
            'If txtProgrInt.Text.ToUpper <> txtProgrInt.Attributes("CORRENTE").ToUpper.ToString Then
            '    If ScriviLogOp(txtProgrInt.Attributes("NOME").ToUpper.ToString, txtProgrInt.Attributes("CORRENTE").ToUpper.ToString, txtProgrInt.Text.ToUpper, "F289", Tempo) = False Then
            '        ELENCOERRORI = ELENCOERRORI & txtProgrInt.Attributes("NOME").ToUpper.ToString & "<br/>"
            '    End If
            'End If
            If cmbProgrInt.SelectedItem.Text.ToUpper <> cmbProgrInt.Attributes("CORRENTE").ToUpper.ToString Then
                If ScriviLogOp(cmbProgrInt.Attributes("NOME").ToUpper.ToString, cmbProgrInt.Attributes("CORRENTE").ToUpper.ToString, cmbProgrInt.SelectedItem.Text.ToUpper, "F290", Tempo) = False Then
                    ELENCOERRORI = ELENCOERRORI & cmbProgrInt.Attributes("NOME").ToUpper.ToString & "<br/>"
                End If
            End If
            If cmbAppalto.SelectedItem.Text.ToUpper <> cmbAppalto.Attributes("CORRENTE").ToUpper.ToString Then
                If ScriviLogOp(cmbAppalto.Attributes("NOME").ToUpper.ToString, cmbAppalto.Attributes("CORRENTE").ToUpper.ToString, cmbAppalto.SelectedItem.Text.ToUpper, "F290", Tempo) = False Then
                    ELENCOERRORI = ELENCOERRORI & cmbAppalto.Attributes("NOME").ToUpper.ToString & "<br/>"
                End If
            End If
        Catch ex As Exception
            LblErrore.Visible = True
            LblErrore.Text = "ERRORE MEMORIZZAZIONE ATTRIBUTI - " & ELENCOERRORI & ex.Message
        End Try
    End Function

    Private Function ScriviLogOp(ByVal CAMPO As String, ByVal VAL_PRECEDENTE As String, ByVal VAL_IMPOSTATO As String, OPERAZIONE As String, tempo As String) As Boolean
        Try
            Dim aperto As Boolean = False

            If par.cmd.Connection.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
                aperto = True
            End If

            'Evento per programma intervento
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI(ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & par.PulisciStrSql(OPERAZIONE) & "' ,'Inserimento valore ''" & par.PulisciStrSql(CAMPO) & "'' da  " & par.IfEmpty(par.PulisciStrSql(VAL_PRECEDENTE), "- - -") & "  a  " & par.PulisciStrSql(VAL_IMPOSTATO) & "')"
            par.cmd.ExecuteNonQuery()

            If aperto = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
            End If
            ScriviLogOp = True
        Catch ex As Exception
            par.OracleConn.Close()
            ScriviLogOp = False
        End Try
    End Function

    Private Function CaricaAttributi()

        'VENGONO CARICATI GLI ATTRIBUTI "CORRENTE" (VALORE CORRENTE) E "NOME" (NOME DEL CAMPO)
        'MENTRE IL VALORE CORRENTE VIENE CARICATO AUTOMATCAMENTE (SOLO PER CHECKBOX, TEXTBOX E DROPDOWNLIST)
        'IL VALORE DELL'ATTRIBUTO "NOME" VIENE CARICATO MANUALMENTE, IN MODO DA INSERIRE DEL TESTO PIU' 
        'SIGNIFICATIVO E NON SEMPLICEMENTE LA PROPRIETA' TEXT
        For Each CTRL In Me.PanelProgrIntervento.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("CORRENTE", UCase(DirectCast(CTRL, TextBox).Text))
            End If
            If TypeOf CTRL Is RadComboBox Then
                DirectCast(CTRL, RadComboBox).Attributes.Add("CORRENTE", UCase(DirectCast(CTRL, RadComboBox).SelectedItem.Text))
            End If
        Next
        'attributi nome da memorizzare

        Me.cmbAppalto.Attributes.Add("NOME", "APPALTO")
        cmbProgrInt.Attributes.Add("NOME", "PROGRAMMAZIONE INTERVENTO")
    End Function

    Private Sub CaricaGridEdifici()
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        par.cmd.CommandText = "SELECT ID, " _
                            & " DESCRIZIONE AS SCALA, (CASE WHEN PULIZIA_SCALE = 1 THEN 'TRUE' ELSE 'FALSE' END) AS PULIZIA_SCALE, " _
                            & " (CASE WHEN ROTAZIONE_SACCHI = 1 THEN 'TRUE' ELSE 'FALSE' END) AS ROTAZIONE_SACCHI, " _
                            & " (CASE WHEN RESA_SACCHI = 1 THEN 'TRUE' ELSE 'FALSE' END) AS RESA_SACCHI " _
                            & " FROM SISCOM_MI.SCALE_EDIFICI WHERE ID_EDIFICIO = " & vId
        Dim DT As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Session.Item("DT_SCALE_EDIFICI") = DT
        dgvScaleEdifici.Rebind()
    End Sub

    Private Sub dgvScaleEdifici_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvScaleEdifici.NeedDataSource
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            Dim DT As Data.DataTable = Session.Item("DT_SCALE_EDIFICI")
            TryCast(sender, RadGrid).DataSource = DT
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Protected Sub headerChkScale_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim dtdett As Data.DataTable = Session.Item("DT_EDIFICI")
            hiddenSelTuttiScale.Value = CStr(Not CBool(hiddenSelTuttiScale.Value))
            If hiddenSelTuttiScale.Value.ToUpper = "TRUE" Then
                For Each r As Data.DataRow In dtdett.Rows
                    r.Item("PULIZIA_SCALE") = "TRUE"
                Next
            Else
                For Each r As Data.DataRow In dtdett.Rows
                    r.Item("PULIZIA_SCALE") = "FALSE"
                Next
            End If
            dgvScaleEdifici.Rebind()
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub
    Protected Sub headerChkRotSacchi_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim dtdett As Data.DataTable = Session.Item("DT_EDIFICI")
            hiddenSelTuttiRotSacchi.Value = CStr(Not CBool(hiddenSelTuttiRotSacchi.Value))
            If hiddenSelTuttiRotSacchi.Value.ToUpper = "TRUE" Then
                For Each r As Data.DataRow In dtdett.Rows
                    r.Item("ROTAZIONE_SACCHI") = "TRUE"
                Next
            Else
                For Each r As Data.DataRow In dtdett.Rows
                    r.Item("ROTAZIONE_SACCHI") = "FALSE"
                Next
            End If
            dgvScaleEdifici.Rebind()
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub
    Protected Sub headerChkResaSacchi_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim dtdett As Data.DataTable = Session.Item("DT_EDIFICI")
            hiddenSelTuttiResaSacchi.Value = CStr(Not CBool(hiddenSelTuttiResaSacchi.Value))
            If hiddenSelTuttiResaSacchi.Value.ToUpper = "TRUE" Then
                For Each r As Data.DataRow In dtdett.Rows
                    r.Item("RESA_SACCHI") = "TRUE"
                Next
            Else
                For Each r As Data.DataRow In dtdett.Rows
                    r.Item("RESA_SACCHI") = "FALSE"
                Next
            End If
            dgvScaleEdifici.Rebind()
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Private Sub AggiornaValoriEdifici()
        Try
            Dim dt As New Data.DataTable
            dt = CType(Session.Item("DT_SCALE_EDIFICI"), Data.DataTable)
            Dim row As Data.DataRow
            For Each item As GridDataItem In dgvScaleEdifici.Items
                Dim puliziaScale As String = CStr(CType(item.FindControl("chkPuliziaScale"), CheckBox).Checked).ToUpper
                Dim rotSacchi As String = CStr(CType(item.FindControl("chkRotSacchi"), CheckBox).Checked).ToUpper
                Dim resaSacchi As String = CStr(CType(item.FindControl("chkResaSacchi"), CheckBox).Checked).ToUpper
                row = dt.Select("id = " & item("ID").Text)(0)
                row.Item("PULIZIA_SCALE") = puliziaScale
                row.Item("ROTAZIONE_SACCHI") = rotSacchi
                row.Item("RESA_SACCHI") = resaSacchi
            Next
            Session.Item("DT_SCALE_EDIFICI") = dt
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Private Sub SalvaScaleEdifici()
        Try


            'Edifici
            Dim dtEdifici As Data.DataTable = Session.Item("DT_SCALE_EDIFICI")
            AggiornaValoriEdifici()
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            For Each riga As Data.DataRow In dtEdifici.Rows
                par.cmd.CommandText = "UPDATE SISCOM_MI.SCALE_EDIFICI " _
                                    & " SET    PULIZIA_SCALE        = " & Convert.ToInt32(CBool(par.IfNull(riga.Item("PULIZIA_SCALE"), 0))) & ", " _
                                    & "        ROTAZIONE_SACCHI        = " & Convert.ToInt32(CBool(par.IfNull(riga.Item("ROTAZIONE_SACCHI"), 0))) & ", " _
                                    & "        RESA_SACCHI        = " & Convert.ToInt32(CBool(par.IfNull(riga.Item("RESA_SACCHI"), 0))) _
                                    & " WHERE  ID                         = " & riga.Item("ID")
                par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "UPDATE SISCOM_MI.SCALE_EDIFICI_TMP " _
                '                   & " SET    PULIZIA_SCALE        = " & Convert.ToInt32(CBool(par.IfNull(riga.Item("PULIZIA_SCALE"), 0))) & ", " _
                '                   & "        ROTAZIONE_SACCHI        = " & Convert.ToInt32(CBool(par.IfNull(riga.Item("ROTAZIONE_SACCHI"), 0))) & ", " _
                '                   & "        RESA_SACCHI        = " & Convert.ToInt32(CBool(par.IfNull(riga.Item("RESA_SACCHI"), 0))) _
                '                   & " WHERE  ID                         = " & riga.Item("ID")
                'par.cmd.ExecuteNonQuery()
            Next

            CaricaGridEdifici()
            dgvScaleEdifici.Rebind()
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub


End Class
