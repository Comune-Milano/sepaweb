
Partial Class CENSIMENTO_UnitàComEdifici
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public classetab As String = ""
    Public tabdefault1 As String = ""
    Public tabdefault2 As String = ""
    Public tabdefault3 As String = ""
    Public tabdefault4 As String = ""
    Public tabdefault5 As String = ""

    'Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
    '    Response.Write("<script>window.open('AdDimens.aspx?ID=" & vId & ",&Pas=UC','DIMENSIONI', 'resizable=yes, width=520, height=220');</script>")
    '    'Me.txtindietro.Text = txtindietro.Text - 1
    'End Sub

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
                classetabED = "tabbertabhide"
            End If
            Dim Str As String

            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:500; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='IMMCENSIMENTO/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"

            Response.Write(Str)
            If Not IsPostBack Then
                Response.Flush()

                vId = Request.QueryString("ID")
                If vId <> 0 Then
                    classetab = "tabbertab"
                Else
                    classetab = "tabbertabhide"
                    classetabED = "tabbertabhide"
                End If
                If Request.QueryString("IDED") <> "" Then
                    INDICE = Request.QueryString("IDED")
                Else
                    INDICE = Request.QueryString("IDC")
                End If
                'Controllo modifica campi nel form
                Dim CTRL As Control

                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    End If
                Next
                'Me.TxtDenComplesso.Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")

                'FINE DEL CICLO
                TxtLocalUnita.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

                vId = Request.QueryString("ID")

                If vId <> 0 Then
                    Me.Riempicampi()

                    ApriRicerca()

                Else
                    Me.Riempicampi()
                    'Me.ImageButton1.Enabled = False
                    'Apro la connessione che resterà valida per tutti i metodi delle sottofinestre e del salva
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
                    'Me.salvainiziale()
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

                If Session.Item("SLE") = "1" Then
                    FrmSolaLettura()
                    Me.BTNiNDIETRO.Visible = True
                End If
            End If
            Me.txtindietro.Value = txtindietro.Value - 1

            VerificaModificheSottoform()

            If Session("ID_CAF") <> "6" Then
                NascondiCampi()
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
    Private Sub FrmSolaLettura()
        Try
            Me.ImageButton2.Visible = False
            DirectCast(Me.Tab_EdifAssociati1.FindControl("ListEdifci"), CheckBoxList).Enabled = False
            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub NascondiCampi()
        'Me.DrLEdificio.Enabled = False
        'Me.DLRComplessi.Enabled = False
        'Me.Label5.Visible = False
        'Me.Label9.Visible = False
        'Me.DLRComplessi.Visible = False
        'Me.DrLEdificio.Visible = False
        'Me.imgStampa.Visible = False
    End Sub
    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click
        Try
            If Session.Item("LE") = 0 Or IsNothing(Session.Item("LE")) Then

                If txtModificato.Value <> "111" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.OracleConn.Close()

                    HttpContext.Current.Session.Remove("TRANSAZIONE")
                    HttpContext.Current.Session.Remove("CONNESSIONE")
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Session.Item("LAVORAZIONE") = 0

                    If Request.QueryString("X") = "1" Then
                        Response.Write("<script language='javascript'> { self.close() }</script>")
                    Else
                        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                    End If

                Else
                    txtModificato.Value = "1"
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
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
                If Request.QueryString("X") = "1" Then
                    Response.Write("<script language='javascript'> { self.close() }</script>")
                Else
                    Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                End If
                Session.Remove("LE")
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Public Property vId() As String
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CStr(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property
    Public Property classetabED() As String
        Get
            If Not (ViewState("par_classetabED") Is Nothing) Then
                Return CStr(ViewState("par_classetabED"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_classetabED") = value
        End Set

    End Property
    Public Property INDICE() As String
        Get
            If Not (ViewState("par_lINDICE") Is Nothing) Then
                Return CStr(ViewState("par_lINDICE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lINDICE") = value
        End Set

    End Property
    Public Property vIdCompl() As String
        Get
            If Not (ViewState("par_lIdComplesso") Is Nothing) Then
                Return CStr(ViewState("par_lIdComplesso"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdComplesso") = value
        End Set

    End Property
    Public Property DaPassare() As String
        Get
            If Not (ViewState("par_valDaPassare") Is Nothing) Then
                Return CStr(ViewState("par_valDaPassare"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_valDaPassare") = value
        End Set

    End Property
    Public Property Selezionati() As String
        Get
            If Not (ViewState("par_Selezionati") Is Nothing) Then
                Return CStr(ViewState("par_Selezionati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Selezionati") = value
        End Set

    End Property


    Private Sub ApriRicerca()
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim dt As New Data.DataTable
        Dim scriptblock As String

        If vId <> -1 Then

            Try
                'SE SI PROVIENE DA UNA RICERCA DOPO AVER RIEMPITO I CAMPI SETTO LA MIA CONNESSIONE CHE RESTERà VALIDA PER TUTTA LA DURATA DI APERTURA DEL FORM
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn) 'MEMORIZZAZIONE CONNESSIONE IN VARIABILE DI SESSIONE

                End If

                If Session.Item("MOD_CENS_MANUT") = 1 Then
                    par.OracleConn.Close()
                    ApriFrmWithDBLock()
                    Exit Sub
                End If
                'par.cmd.CommandText = "SELECT UNITA_COMUNI.*, EDIFICI.ID_COMPLESSO AS COMP_OF_EDIF FROM SISCOM_MI.UNITA_COMUNI,SISCOM_MI.EDIFICI  WHERE UNITA_COMUNI.ID_EDIFICIO=EDIFICI.ID(+) AND UNITA_COMUNI.ID =" & vId & "  FOR UPDATE NOWAIT"
                par.cmd.CommandText = "SELECT UNITA_COMUNI.* FROM SISCOM_MI.UNITA_COMUNI WHERE UNITA_COMUNI.ID = " & vId & "  FOR UPDATE NOWAIT"
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)
                If dt.Rows.Count > 0 Then

                    If (dt.Rows(0).Item("ID_EDIFICIO").ToString) <> "" Then
                        par.cmd.CommandText = "SELECT EDIFICI.ID_COMPLESSO  FROM SISCOM_MI.EDIFICI WHERE ID = " & (dt.Rows(0).Item("ID_EDIFICIO").ToString)
                        Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                        Me.DLRComplessi.SelectedIndex = -1
                        If myreader.Read Then
                            Me.DLRComplessi.SelectedValue = (myreader("ID_COMPLESSO"))
                            'vIdCompl = dt.Rows(0).Item("ID_COMPLESSO").ToString
                        End If
                        myreader.Close()
                    Else
                        Me.DLRComplessi.SelectedValue = (dt.Rows(0).Item("ID_COMPLESSO").ToString)
                        vIdCompl = dt.Rows(0).Item("ID_COMPLESSO").ToString
                    End If


                    If (dt.Rows(0).Item("ID_EDIFICIO").ToString) <> "" Then
                        Me.DrLEdificio.SelectedIndex = -1
                        Me.DrLEdificio.SelectedValue = (dt.Rows(0).Item("ID_EDIFICIO").ToString)
                        vIdCompl = dt.Rows(0).Item("ID_EDIFICIO").ToString
                    End If

                    Me.DrLDispUnitComune.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_DISPONIBILITA"), -1)

                    Me.DrLTipoUnitComune.SelectedValue = 0
                    Me.DrLTipoUnitComune.Items.FindByValue(dt.Rows(0).Item("COD_TIPOLOGIA")).Selected = True
                    Me.DrLTipoLivPiano.SelectedValue = 0
                    Me.DrLTipoLivPiano.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_TIPO_LIVELLO_PIANO"), "-1")

                    Me.TxtLocalUnita.Text = par.IfNull(dt.Rows(0).Item("LOCALIZZAZIONE"), "")

                    Me.TxtNumPianiAsc.Text = par.IfNull(dt.Rows(0).Item("NUM_PIANI_ASCENSORE").ToString, "")

                    Me.TxtNumPianiScale.Text = dt.Rows(0).Item("NUM_PIANI_SCALE").ToString
                    Me.txtCodUnitCom.Text = dt.Rows(0).Item("COD_UNITA_COMUNE").ToString
                    Me.txtNote.Text = par.IfNull(dt.Rows(0).Item("NOTE").ToString, "")
                    'Nuovi campi 11/08/2010
                    Me.CmbDestUso.Text = par.IfNull(dt.Rows(0).Item("ID_DESTINAZIONE_USO"), "")
                    Me.CmbUbicazione.Text = par.IfNull(dt.Rows(0).Item("ID_UBICAZIONE"), "")
                    Me.CmbStFisico.Text = par.IfNull(dt.Rows(0).Item("ID_STATO_FISICO"), "")


                    Me.DrLEdificio.Enabled = False
                    Me.DLRComplessi.Enabled = False
                    'Me.ImgBtnMillesim.Visible = True
                    'Me.ImageButton1.Visible = True
                    Me.imgStampa.Visible = True
                    If (dt.Rows(0).Item("ID_COMPLESSO").ToString) <> "" Then
                        DaPassare = "UNCOM"
                    ElseIf (dt.Rows(0).Item("ID_EDIFICIO").ToString) <> "" Then
                        DaPassare = "UNCOMED"
                    End If

                    ApriUCcorrelateEdifici()
                    scala()

                    'Apro una nuova transazione
                    Session.Item("LAVORAZIONE") = "1"
                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                    If Me.DrlSc.Items.Count > 1 Then
                        Me.DrlSc.SelectedValue = (par.IfNull((dt.Rows(0).Item("id_scala").ToString), "-1"))
                        ' Me.DrlSc.Enabled = False
                    End If
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
                    If DrLEdificio.SelectedValue <> "-1" Then
                        classetabED = "tabbertabhide"
                    Else
                        classetabED = "tabbertab"
                    End If
                End If



            Catch EX1 As Oracle.DataAccess.Client.OracleException
                If EX1.Number = 54 Then
                    par.OracleConn.Close()
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Unità Comune aperta da un altro utente. Non è possibile effettuare modifiche!');" _
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
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
                par.OracleConn.Close()
                ImageButton2.Visible = False
            End Try
        End If

    End Sub
    Private Sub LoadDrlSchede()
        Try
            Me.DrlSchede.Items.Add(New ListItem("- - - - - - - - - - - - - - - - - - ", -1))
            Me.DrlSchede.Items.Add(New ListItem("Sc. A-RILIEVO STRUTTURE", 0))
            Me.DrlSchede.Items.Add(New ListItem("Sc. B-SCHEDA RILIEVO CHIUSURE", 1))
            Me.DrlSchede.Items.Add(New ListItem("Sc. C-SCHEDA RILIEVO PARTIZIONI INTERNE", 2))
            Me.DrlSchede.Items.Add(New ListItem("Sc. D-SCHEDA RILIEVO PAVIMENTAZIONI INTERNE", 3))
            Me.DrlSchede.Items.Add(New ListItem("Sc. E-SCHEDA RILIEVO PROTEZIONE E DELIMITAZIONI", 4))
            Me.DrlSchede.Items.Add(New ListItem("Sc. F-SCHEDA RILIEVO ATTREZZATURE E SPAZI INTERNI", 5))
            Me.DrlSchede.Items.Add(New ListItem("Sc. G-SCHEDA RILIEVO ATTREZZATURE ED ARREDI ESTERNI", 6))
            Me.DrlSchede.Items.Add(New ListItem("Sc. H-SCHEDA RILIEVO IMPIANTI FISSI DI TRASPORTO", 7))
            Me.DrlSchede.Items.Add(New ListItem("Sc. I-SCHEDA RILIEVO IMPIANTI  RISCALDAMENTO E PRODUZIONE H2O CENTRALIZZATA", 8))
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
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
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
            Case Else
                Me.DrlSchede.SelectedValue = -1

        End Select
    End Sub

    Private Sub Riempicampi()
        Dim ds As New Data.DataSet
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim gest As Integer = 3

        If vId <> -1 Then

            Try
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                End If
                LoadDrlSchede()

                If Request.QueryString("SK") <> "0" Then
                    SelezionaSkeda(Request.QueryString("SK"))
                End If

                '****CARICAMENTO LISTA COMPLESSI *****
                DLRComplessi.Items.Add(New ListItem(" ", -1))
                'Me.DLRComplessi.Items.Add(New ListItem("PROVA", 1))
                If Session("PED2_ESTERNA") = "1" Then
                    'par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.ID, COMPLESSI_IMMOBILIARI.DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID order by DENOMINAZIONE asc"
                    par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where lotto > 3 and complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "
                Else
                    par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where  complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "

                End If
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader2.Read
                    'DLRComplessi.Items.Add(New ListItem(par.IfNull(myReader2("DENOMINAZIONE"), " "), par.IfNull(myReader2("id"), -1)))
                    DLRComplessi.Items.Add(New ListItem(par.IfNull(myReader2("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader2("cod_complesso"), " "), par.IfNull(myReader2("id"), -1)))

                End While
                myReader2.Close()
                DLRComplessi.Text = "-1"
                '*******IN FASE DI APERTURA DA EDIFICIO O COMPLESSO PER SELEZIONARE IL DATO AUTOMATICAMENTE DALLA COMBO
                Dim VIDCOMPED As String = Request.QueryString("IDC")
                If VIDCOMPED > 0 Then
                    Me.DLRComplessi.SelectedValue = VIDCOMPED
                    Me.DLRComplessi.Enabled = False
                    Me.DrLEdificio.Enabled = False
                    Me.CaricaListBox()
                End If

                Me.CaricaEdifix()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPO_UNITA_COMUNE"
                myReader2 = par.cmd.ExecuteReader
                DrLTipoUnitComune.Items.Add(New ListItem(" ", -1))

                While (myReader2.Read)
                    DrLTipoUnitComune.Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("COD"), -1)))
                End While
                myReader2.Close()

                da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT * FROM SISCOM_MI.TIPO_DISPONIBILITA", par.OracleConn)
                da.Fill(ds)
                DrLDispUnitComune.DataSource = ds
                DrLDispUnitComune.DataTextField = "DESCRIZIONE"
                DrLDispUnitComune.DataValueField = "COD"
                DrLDispUnitComune.DataBind()
                ds = New Data.DataSet
                da = Nothing

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPO_LIVELLO_PIANO"
                myReader1 = par.cmd.ExecuteReader
                DrLTipoLivPiano.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    DrLTipoLivPiano.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("cod"), -1)))
                End While
                DrLTipoLivPiano.Text = "-1"

                myReader1.Close()

                par.cmd.CommandText = "Select * from SISCOM_MI.ubicazioni_uc"
                myReader1 = par.cmd.ExecuteReader()
                CmbUbicazione.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    CmbUbicazione.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()

                par.cmd.CommandText = "Select * from SISCOM_MI.destinazioni_uso_uc"
                myReader1 = par.cmd.ExecuteReader()
                CmbDestUso.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    CmbDestUso.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()

                par.cmd.CommandText = "Select * from SISCOM_MI.stato_uc"
                myReader1 = par.cmd.ExecuteReader()
                CmbStFisico.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    CmbStFisico.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()

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


                Dim IDED As String = Request.QueryString("IDED")
                If IDED > 0 Then
                    Me.DrLEdificio.SelectedValue = IDED
                    Me.DrLEdificio.Enabled = False
                    Me.DLRComplessi.Enabled = False
                    LivelloPiano()
                    scala()

                End If
                CaricaListBox()

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Catch ex As Exception
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
                par.OracleConn.Close()
            End Try
        End If

    End Sub
    Private Sub scala()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Me.DrlSc.Items.Clear()
            par.cmd.CommandText = "SELECT  ID, DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI where id_EDIFICIO =" & Me.DrLEdificio.SelectedValue.ToString
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Me.DrlSc.Items.Add(New ListItem("NON DEFINIBILE", -1))
            While myReader1.Read
                DrlSc.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Private Sub Salva()

        Dim NextVal As String = 0
        Dim idedif As String
        Dim CodUniCom As String
        Dim IdUniComu As String
        'Dim lotto As String
        Dim CodCompl As String
        Dim CodEdif As String

        Try

            Dim myreader As Oracle.DataAccess.Client.OracleDataReader


            If (Me.DLRComplessi.SelectedValue <> "-1" Or Me.DrLEdificio.SelectedValue <> "-1") AndAlso Me.DrLTipoUnitComune.SelectedValue.ToString <> "-1" AndAlso par.IfEmpty(Me.TxtLocalUnita.Text, "Null") <> "Null" AndAlso Me.DrLTipoLivPiano.SelectedValue <> "-1" Then
                'Richiamo la connessione
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'Apro la Transazione
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_UNITA_COMUNI.NEXTVAL FROM DUAL"
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    NextVal = myreader(0)
                End If

                myreader.Close()
                par.cmd.CommandText = ""

                If Me.DrLEdificio.SelectedValue <> "-1" Then
                    '*************UNITA COMUNE ASSOCIATA AD UN EDIFICIO ******************
                    IdUniComu = Mid(DrLEdificio.SelectedValue.ToString, 1, 1) & NextVal
                    vId = IdUniComu

                    par.cmd.CommandText = "SELECT SISCOM_MI.EDIFICI.COD_EDIFICIO FROM SISCOM_MI.EDIFICI where id = " & Me.DrLEdificio.SelectedValue.ToString
                    myreader = par.cmd.ExecuteReader
                    If myreader.Read Then
                        CodEdif = myreader(0)
                    End If
                    myreader.Close()
                    par.cmd.CommandText = ""
                    CodUniCom = CodEdif & Mid(NextVal, 6)
                    idedif = Me.DrLEdificio.SelectedValue.ToString
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.UNITA_COMUNI (ID, COD_UNITA_COMUNE,COD_TIPOLOGIA,VECCHIO_COD_UNITA_COMUNE_GIMI,ID_COMPLESSO,ID_EDIFICIO,LOCALIZZAZIONE,COD_DISPONIBILITA,NUM_PIANI_ASCENSORE,NUM_PIANI_SCALE,ID_OPERATORE_INSERIMENTO,ID_SCALA, COD_TIPO_LIVELLO_PIANO,NOTE,ID_DESTINAZIONE_USO,ID_UBICAZIONE,ID_STATO_FISICO) VALUES" _
                    & "(" & IdUniComu & ", '" & CodUniCom & "' , '" & Me.DrLTipoUnitComune.SelectedValue.ToString & "', '',NULL, " & idedif & ", '" & par.PulisciStrSql(Me.TxtLocalUnita.Text) & "', '" & Me.DrLDispUnitComune.SelectedValue.ToString & "', " & par.IfEmpty(par.VirgoleInPunti(Me.TxtNumPianiAsc.Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.TxtNumPianiScale.Text), "Null") & ", '" & Session("ID_OPERATORE") & "'," & RitornaNullSeMenoUno(Me.DrlSc.SelectedValue.ToString) & "," _
                    & "'" & par.IfNull(Me.DrLTipoLivPiano.SelectedValue.ToString, "Null") & "','" & par.PulisciStrSql(Me.txtNote.Text) & "'," & RitornaNullSeMenoUno(Me.CmbDestUso.SelectedValue) & ", " & RitornaNullSeMenoUno(Me.CmbUbicazione.SelectedValue) & ", " & RitornaNullSeMenoUno(Me.CmbStFisico.SelectedValue) & ")"

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    Me.txtCodUnitCom.Text = CodUniCom
                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UC (ID_UC,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','')"
                    par.cmd.ExecuteNonQuery()
                    '*******************************FINE****************************************
                    '***************************************************************************

                    'COMMIT GENERALE
                    par.myTrans.Commit()
                    classetab = "tabbertab"

                    'Riapro una nuova transazione
                    Session.Item("LAVORAZIONE") = "1"
                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                    Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
                    Me.DrLEdificio.Enabled = False
                    Me.DLRComplessi.Enabled = False
                    'Me.ImgBtnMillesim.Visible = True
                    'Me.ImageButton1.Visible = True
                    'Me.ImageButton1.Enabled = True
                    Me.imgStampa.Visible = True

                Else
                    '*************UNITA COMUNE ASSOCIATA AD UN COMPLESSO******************

                    IdUniComu = Mid(DLRComplessi.SelectedValue.ToString, 1, 1) & NextVal
                    vId = IdUniComu

                    'par.cmd.CommandText = "SELECT SISCOM_MI.COMPLESSI_IMMOBILIARI.LOTTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI where id = " & Me.DLRComplessi.SelectedValue.ToString
                    'myreader = par.cmd.ExecuteReader
                    'If myreader.Read Then
                    '    lotto = myreader(0)
                    'End If
                    'myreader.Close()
                    'par.cmd.CommandText = ""

                    par.cmd.CommandText = "SELECT SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI where id = " & Me.DLRComplessi.SelectedValue.ToString
                    myreader = par.cmd.ExecuteReader
                    If myreader.Read Then
                        CodCompl = myreader(0)
                    End If
                    myreader.Close()
                    par.cmd.CommandText = ""


                    CodUniCom = Mid(CodCompl, 1) & "00" & Mid(NextVal, 6)
                    idedif = "''"
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.UNITA_COMUNI (ID, COD_UNITA_COMUNE,COD_TIPOLOGIA,VECCHIO_COD_UNITA_COMUNE_GIMI,ID_COMPLESSO,ID_EDIFICIO,LOCALIZZAZIONE,COD_DISPONIBILITA,NUM_PIANI_ASCENSORE,NUM_PIANI_SCALE,ID_OPERATORE_INSERIMENTO,ID_SCALA,COD_TIPO_LIVELLO_PIANO) VALUES" _
                    & "(" & IdUniComu & ", '" & CodUniCom & "' , '" & Me.DrLTipoUnitComune.SelectedValue.ToString & "', ''," & Me.DLRComplessi.SelectedValue.ToString & ", " & idedif & ", '" & par.PulisciStrSql(Me.TxtLocalUnita.Text) & "', '" & Me.DrLDispUnitComune.SelectedValue.ToString & "', " & par.IfEmpty(par.VirgoleInPunti(Me.TxtNumPianiAsc.Text), "null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.TxtNumPianiScale.Text), "null") & ", '" & Session("ID_OPERATORE") & "'," & RitornaNullSeMenoUno(Me.DrlSc.SelectedValue.ToString) & ",'" & par.IfNull(Me.DrLTipoLivPiano.SelectedValue.ToString, "Null") & "')"
                    par.cmd.ExecuteNonQuery()

                    Me.txtCodUnitCom.Text = CodUniCom
                    par.cmd.CommandText = ""

                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UC (ID_UC,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','')"
                    par.cmd.ExecuteNonQuery()
                    '*******************************FINE****************************************
                    '***************************************************************************
                    par.cmd.CommandText = ""

                    '++++++++NUOVA CHECk+++++++++++++++
                    If DirectCast(Me.Tab_EdifAssociati1.FindControl("ListEdifci"), CheckBoxList).Items.Count > 0 Then

                        For Each o As Object In DirectCast(Me.Tab_EdifAssociati1.FindControl("ListEdifci"), CheckBoxList).Items
                            Dim item As System.Web.UI.WebControls.ListItem
                            item = CType(o, System.Web.UI.WebControls.ListItem)
                            If item.Selected Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.UC_EDIFICI (ID_UNITA_COMUNE,ID_EDIFICIO ) VALUES (" & IdUniComu & "," & item.Value & ")"
                                par.cmd.ExecuteNonQuery()
                            End If
                        Next
                    End If
                    '+++++++++fine nuova check


                    'COMMIT GENERALE
                    par.myTrans.Commit()
                    classetab = "tabbertab"
                    'Riapro una nuova transazione
                    Session.Item("LAVORAZIONE") = "1"
                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)


                    Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
                    'Me.ImageButton1.Enabled = True
                    Me.DrLEdificio.Enabled = False
                    Me.DLRComplessi.Enabled = False

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
                    End If                    'Me.ImgBtnMillesim.Visible = True
                    'Me.ImageButton1.Visible = True
                    Me.imgStampa.Visible = True

                End If

                vId = IdUniComu

                If Me.DrLEdificio.SelectedValue <> "-1" Or Me.DrLEdificio.SelectedValue <> "" Then
                    DaPassare = "UNCOMED"
                Else
                    DaPassare = "UNCOM"
                End If
            Else
                Response.Write("<SCRIPT>alert('       Impossibile completare!\r\nAvvalorare tutti i campi obbligatori!');</SCRIPT>")

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try
    End Sub
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
            LblErrore.Text = ex.Message
        End Try
    End Function

    Private Sub CaricaEdifix()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            'Me.DrLEdificio.Enabled = False

            '****CARICA LISTA EDIFICI
            Dim gest As Integer = 0
            If Me.DLRComplessi.SelectedValue = "-1" Then
                Me.DrLEdificio.Items.Clear()
                DrLEdificio.Items.Add(New ListItem(" ", -1))

                If Session("PED2_ESTERNA") = "1" Then
                    par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.LOTTO > 3 order by denominazione asc"
                Else
                    par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID order by denominazione asc"

                End If
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    'DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                    DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))

                End While
                myReader1.Close()
                'DrLEdificio.Text = "-1"
                DrLEdificio.Items.Add(New ListItem(" ", -1))

            Else

                DrLEdificio.Items.Clear()
                DrLEdificio.Items.Add(New ListItem(" ", -1))
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where id_complesso = " & Me.DLRComplessi.SelectedValue.ToString & " order by denominazione asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    'DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                    DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()
                DrLEdificio.Text = "-1"
                DrLEdificio.Items.Add(New ListItem(" ", -1))
                'Me.DrLEdificio.Enabled = True
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub DLRComplessi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLRComplessi.SelectedIndexChanged
        Me.CaricaEdifix()
        Me.DrlSc.Items.Add(New ListItem("NON DEFINIBILE", -1))
        Me.CaricaListBox()
    End Sub

    Protected Sub DrLEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLEdificio.SelectedIndexChanged
        If Me.DrLEdificio.SelectedValue <> "-1" Then
            scala()

            Me.DLRComplessi.Enabled = False
            Me.CaricaListBox()

        Else
            Me.CaricaListBox()

            Me.DLRComplessi.Enabled = True
        End If
        LivelloPiano()

    End Sub
    Private Sub Update()
        Try

            If (Me.DLRComplessi.SelectedValue <> "-1" Or Me.DrLEdificio.SelectedValue <> "-1") AndAlso Me.DrLTipoUnitComune.SelectedValue.ToString <> "-1" AndAlso par.IfEmpty(Me.TxtLocalUnita.Text, "Null") <> "Null" AndAlso Me.DrLTipoLivPiano.SelectedValue <> "-1" Then

                If vId > 0 Then

                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "UPDATE SISCOM_MI.UNITA_COMUNI SET COD_TIPOLOGIA = '" & Me.DrLTipoUnitComune.SelectedValue.ToString & "' , LOCALIZZAZIONE = '" & par.PulisciStrSql(Me.TxtLocalUnita.Text) & "', COD_DISPONIBILITA = '" & Me.DrLDispUnitComune.SelectedValue.ToString & "' , NUM_PIANI_ASCENSORE = " & par.IfEmpty(par.VirgoleInPunti(Me.TxtNumPianiAsc.Text), "NULL") & ", NUM_PIANI_SCALE = " & par.IfEmpty(par.VirgoleInPunti(Me.TxtNumPianiScale.Text), "NULL") & ", ID_OPERATORE_AGGIORNAMENTO = '" & Session("ID_OPERATORE") & "', ID_SCALA=" & RitornaNullSeMenoUno(Me.DrlSc.SelectedValue.ToString) & ", COD_TIPO_LIVELLO_PIANO='" & par.IfNull(Me.DrLTipoLivPiano.SelectedValue.ToString, "Null") & "',NOTE = '" & par.PulisciStrSql(Me.txtNote.Text) & "', ID_DESTINAZIONE_USO= " & RitornaNullSeMenoUno(Me.CmbDestUso.SelectedValue) & ",ID_UBICAZIONE=" & RitornaNullSeMenoUno(Me.CmbUbicazione.SelectedValue) & ",ID_STATO_FISICO=" & RitornaNullSeMenoUno(Me.CmbStFisico.SelectedValue) & " WHERE ID =" & vId
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.UC_EDIFICI WHERE ID_UNITA_COMUNE =" & vId
                    par.cmd.ExecuteNonQuery()
                    '++++++++NUOVA CHECk+++++++++++++++
                    If DirectCast(Me.Tab_EdifAssociati1.FindControl("ListEdifci"), CheckBoxList).Items.Count > 0 Then

                        For Each o As Object In DirectCast(Me.Tab_EdifAssociati1.FindControl("ListEdifci"), CheckBoxList).Items
                            Dim item As System.Web.UI.WebControls.ListItem
                            item = CType(o, System.Web.UI.WebControls.ListItem)
                            If item.Selected Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.UC_EDIFICI (ID_UNITA_COMUNE,ID_EDIFICIO ) VALUES (" & vId & "," & item.Value & ")"
                                par.cmd.ExecuteNonQuery()
                            End If
                        Next
                    End If
                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UC (ID_UC,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','')"
                    par.cmd.ExecuteNonQuery()
                    '*******************************FINE****************************************
                    '***************************************************************************

                    par.myTrans.Commit()

                    'Riapro una nuova transazione
                    Session.Item("LAVORAZIONE") = "1"
                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                    Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")

                    Me.DrLEdificio.Enabled = False
                    Me.DLRComplessi.Enabled = False
                    'Me.ImageButton1.Visible = True
                    'Me.ImgBtnMillesim.Visible = True
                    Me.imgStampa.Visible = True
                End If
            Else
                Response.Write("<SCRIPT>alert('       Impossibile completare!\r\nAvvalorare tutti i campi obbligatori!');</SCRIPT>")

            End If
            'par.OracleConn.Close()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        If vId <> 0 Then
            Me.Update()
        Else
            Me.Salva()

        End If
        txtModificato.Value = "0"

        'Me.txtindietro.Text = CInt(txtindietro.Text) - 1

    End Sub

    'Protected Sub ImgBtnMillesim_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnMillesim.Click
    '    Response.Write("<script>window.open('Millesimali.aspx?IDED=" & vIdCompl & ",&Pas=" & DaPassare & "&IDUNI=" & vId & "','MILLESIMALI', 'resizable=yes, width=520, height=250');</script>")
    '    'Me.txtindietro.Text = txtindietro.Text - 1

    'End Sub

    Protected Sub imgStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStampa.Click
        Try
            If vId <> -1 And vId <> 0 Then
                If txtModificato.Value <> "111" Then
                    'Me.txtindietro.Text = txtindietro.Text - 1
                    Response.Write("<script>window.open('StampaUC.aspx?ID=" & vId & "', '');</script>")
                Else
                    txtModificato.Value = "1"
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                End If
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub BTNiNDIETRO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BTNiNDIETRO.Click
        Try
            'If Request.QueryString("V") <> "1" Then
            '    Response.Redirect(Request.QueryString("C") & ".aspx?E=" & INDICE & "&PAS=" & Request.QueryString("RICPER"))
            'Else
            '    Response.Write("<script>history.go(" & txtindietro.Text & ");</script>")
            'End If
            If txtModificato.Value <> "111" Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.OracleConn.Close()

                HttpContext.Current.Session.Remove("TRANSAZIONE")
                HttpContext.Current.Session.Remove("CONNESSIONE")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Item("LAVORAZIONE") = 0
                Select Case Request.QueryString("C")
                    Case "RisultatiUC"
                        Response.Redirect(Request.QueryString("C") & ".aspx?C=" & Request.QueryString("COMPLESSO") & "&E=" & Request.QueryString("COMPEDI") & "&TIPOL=" & Request.QueryString("TIPOL") & "&PAS=" & Request.QueryString("RICPER"))
                    Case "InserimentoComplessi"
                        Response.Redirect(Request.QueryString("C") & ".aspx?ID=" & Request.QueryString("IDC"))
                    Case "RisultSchede"
                        Response.Redirect("RisultSchede.aspx?SCHEDA=" & Request.QueryString("SK"))

                    Case Nothing
                        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

                    Case Else
                        Response.Redirect(Request.QueryString("C") & ".aspx?E=" & Request.QueryString("COMPEDI") & "&PAS=" & Request.QueryString("RICPER"))
                End Select
            Else
                txtModificato.Value = "1"
                Me.USCITA.Value = 0
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub CaricaListBox()
        Try
            Dim apertaora As Boolean = False
            If Me.DLRComplessi.SelectedValue <> "-1" And (Me.DrLEdificio.SelectedValue = "-1" Or Me.DrLEdificio.SelectedValue = "") Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    apertaora = True
                End If
                DirectCast(Me.Tab_EdifAssociati1.FindControl("ListEdifci"), CheckBoxList).Items.Clear()
                par.cmd.CommandText = "select  edifici.id  ,('COD. '||edifici.cod_edificio ||' - - '||edifici.denominazione) as DESCRIZIONE from SISCOM_MI.edifici where edifici.id_complesso = " & Me.DLRComplessi.SelectedValue.ToString & " order by edifici.cod_edificio asc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    DirectCast(Me.Tab_EdifAssociati1.FindControl("ListEdifci"), CheckBoxList).Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
                End While
                myReader.Close()
                If DirectCast(Me.Tab_EdifAssociati1.FindControl("ListEdifci"), CheckBoxList).Items.Count > 0 Then
                    DirectCast(Me.Tab_EdifAssociati1.FindControl("btnSelezionaTutto"), Button).Visible = True
                Else
                    DirectCast(Me.Tab_EdifAssociati1.FindControl("btnSelezionaTutto"), Button).Visible = False
                End If
                myReader.Close()
                If apertaora = True Then
                    par.OracleConn.Close()

                End If
            Else
                DirectCast(Me.Tab_EdifAssociati1.FindControl("ListEdifci"), CheckBoxList).Items.Clear()
                DirectCast(Me.Tab_EdifAssociati1.FindControl("btnSelezionaTutto"), Button).Visible = False
            End If
            '300000046
            'par.OracleConn.Close()
            '& Me.DLRComplessi.SelectedValue.ToString & 
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Public Sub SelezionaTutto()
        Try
            If Selezionati = "" Then
                Selezionati = 1
            Else
                Selezionati = ""
            End If
            Dim a As Integer
            Dim i As Integer = 0
            If Selezionati <> "" Then
                a = DirectCast(Me.Tab_EdifAssociati1.FindControl("ListEdifci"), CheckBoxList).Items.Count.ToString
                While i < a
                    DirectCast(Me.Tab_EdifAssociati1.FindControl("ListEdifci"), CheckBoxList).Items(i).Selected = True
                    i = i + 1
                End While
            Else
                a = DirectCast(Me.Tab_EdifAssociati1.FindControl("ListEdifci"), CheckBoxList).Items.Count.ToString
                While i < a
                    DirectCast(Me.Tab_EdifAssociati1.FindControl("ListEdifci"), CheckBoxList).Items(i).Selected = False
                    i = i + 1
                End While
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub ApriUCcorrelateEdifici()

        CaricaListBox()
        Dim ApertaOra As Boolean = False
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ApertaOra = True
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UC_EDIFICI WHERE ID_UNITA_COMUNE =" & vId
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader.Read
                DirectCast(Me.Tab_EdifAssociati1.FindControl("ListEdifci"), CheckBoxList).Items.FindByValue(myReader.Item("ID_EDIFICIO")).Selected = True
                Selezionati = 1
            End While
            myReader.Close()
            If ApertaOra = True Then
                par.OracleConn.Close()
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub LivelloPiano()
        Try
            Dim ApertaAdesso As Boolean = False
            Me.DrLTipoLivPiano.Items.Clear()
            'Questo metodo vuole una connessione aperte dal chiamante in caso contrario ne apre una nuova e la chiude immediatamente
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ApertaAdesso = True
            End If
            Dim sStrSQL As String

            Dim Entro As Integer
            Dim Fuori As Integer
            Dim mezzanini As Integer
            Dim Attico As Integer
            Dim SuperAttico As Integer
            Dim Sottotetto As Integer
            Dim Seminter As Integer
            Dim PTerra As Integer
            Dim TROVATO As Boolean
            Me.DrLTipoLivPiano.Items.Clear()


            If Me.DrLEdificio.SelectedValue.ToString = "-1" Then
                par.cmd.CommandText = "SELECT COD, DESCRIZIONE, LIVELLO FROM SISCOM_MI.TIPO_LIVELLO_PIANO"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                DrLTipoLivPiano.Items.Add(New ListItem(" ", -1))
                While myReader2.Read
                    DrLTipoLivPiano.Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("COD"), -1)))
                End While
                par.cmd.CommandText = ""
                myReader2.Close()

            Else
                par.cmd.CommandText = "SELECT  NUM_PIANI_ENTRO , NUM_PIANI_FUORI,PIANO_TERRA,SEMINTERRATO,SOTTOTETTO,ATTICO,SUPERATTICO,NUM_MEZZANINI FROM SISCOM_MI.EDIFICI WHERE ID = " & Me.DrLEdificio.SelectedValue.ToString
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    Entro = par.IfNull(myReader("NUM_PIANI_ENTRO"), 0)
                    Fuori = par.IfNull(myReader("NUM_PIANI_FUORI"), 0)
                    mezzanini = par.IfNull(myReader("NUM_MEZZANINI"), 0)
                    Attico = par.IfNull(myReader("ATTICO"), 0)
                    SuperAttico = par.IfNull(myReader("SUPERATTICO"), 0)
                    Sottotetto = par.IfNull(myReader("SOTTOTETTO"), 0)
                    Seminter = par.IfNull(myReader("SEMINTERRATO"), 0)
                    PTerra = par.IfNull(myReader("PIANO_TERRA"), 0)
                End If
                myReader.Close()
                par.cmd.CommandText = ""
                sStrSQL = "select COD, DESCRIZIONE FROM SISCOM_MI.TIPO_LIVELLO_PIANO"

                If Fuori <> 0 Then
                    sStrSQL = sStrSQL & " WHERE (LIVELLO <= " & Fuori
                    TROVATO = True
                End If
                If TROVATO = True Then
                    sStrSQL = sStrSQL & " and "
                Else
                    sStrSQL = sStrSQL & " where( "
                    TROVATO = True
                End If
                sStrSQL = sStrSQL & " LIVELLO >=-" & Entro

                If TROVATO = True Then
                    sStrSQL = sStrSQL & " AND (ROUND(LIVELLO,0)=LIVELLO) "
                Else
                    sStrSQL = sStrSQL & " WHERE (ROUND(LIVELLO,0)=LIVELLO) "
                    TROVATO = True

                End If
                If PTerra = 1 Then
                    sStrSQL = sStrSQL & " )"

                Else
                    sStrSQL = sStrSQL & " AND LIVELLO<>0)"

                End If
                If mezzanini <> 0 Then
                    If TROVATO = True Then
                        sStrSQL = sStrSQL & "OR (LIVELLO<" & Fuori & " AND (ROUND(LIVELLO,0)<>LIVELLO)) "
                    End If
                End If

                If Attico <> 0 Then
                    sStrSQL = sStrSQL & " or COD = 74 "
                End If
                If SuperAttico <> 0 Then
                    sStrSQL = sStrSQL & " or COD = 75 "
                End If
                If Sottotetto <> 0 Then
                    sStrSQL = sStrSQL & " or COD = 73 "
                End If
                If Seminter <> 0 Then
                    sStrSQL = sStrSQL & " or COD = 72 "
                End If

                'sStrSQL = sStrSQL & " ) ORDER BY COD ASC"

                par.cmd.CommandText = sStrSQL

                myReader = par.cmd.ExecuteReader

                DrLTipoLivPiano.Items.Add(New ListItem(" ", -1))
                While myReader.Read
                    DrLTipoLivPiano.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("COD"), -1)))
                End While
                par.cmd.CommandText = ""
                myReader.Close()


            End If

            If ApertaAdesso = True Then
                par.OracleConn.Close()
            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub ApriFrmWithDBLock()
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim dt As New Data.DataTable

        If vId <> -1 Then

            Try
                'SE SI PROVIENE DA UNA RICERCA DOPO AVER RIEMPITO I CAMPI SETTO LA MIA CONNESSIONE CHE RESTERà VALIDA PER TUTTA LA DURATA DI APERTURA DEL FORM
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "SELECT UNITA_COMUNI.*, EDIFICI.ID_COMPLESSO AS COMP_OF_EDIF FROM SISCOM_MI.UNITA_COMUNI,SISCOM_MI.EDIFICI  WHERE UNITA_COMUNI.ID_EDIFICIO=EDIFICI.ID(+) AND UNITA_COMUNI.ID =" & vId
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    If (dt.Rows(0).Item("ID_COMPLESSO").ToString) <> "" Then
                        Me.DLRComplessi.SelectedIndex = -1
                        Me.DLRComplessi.SelectedValue = (dt.Rows(0).Item("ID_COMPLESSO").ToString)
                        vIdCompl = dt.Rows(0).Item("ID_COMPLESSO").ToString
                    Else
                        Me.DLRComplessi.SelectedValue = (dt.Rows(0).Item("COMP_OF_EDIF").ToString)
                    End If


                    If (dt.Rows(0).Item("ID_EDIFICIO").ToString) <> "" Then
                        Me.DrLEdificio.SelectedIndex = -1
                        Me.DrLEdificio.SelectedValue = (dt.Rows(0).Item("ID_EDIFICIO").ToString)
                        vIdCompl = dt.Rows(0).Item("ID_EDIFICIO").ToString
                    End If

                    Me.DrLDispUnitComune.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_DISPONIBILITA"), -1)

                    Me.DrLTipoUnitComune.SelectedValue = 0
                    Me.DrLTipoUnitComune.Items.FindByValue(dt.Rows(0).Item("COD_TIPOLOGIA")).Selected = True
                    Me.DrLTipoLivPiano.SelectedValue = 0
                    Me.DrLTipoLivPiano.SelectedValue = par.IfNull(dt.Rows(0).Item("COD_TIPO_LIVELLO_PIANO"), "-1")

                    Me.TxtLocalUnita.Text = dt.Rows(0).Item("LOCALIZZAZIONE").ToString

                    Me.TxtNumPianiAsc.Text = dt.Rows(0).Item("NUM_PIANI_ASCENSORE").ToString

                    Me.TxtNumPianiScale.Text = dt.Rows(0).Item("NUM_PIANI_SCALE").ToString
                    Me.txtCodUnitCom.Text = dt.Rows(0).Item("COD_UNITA_COMUNE").ToString
                    Me.txtNote.Text = par.IfNull(dt.Rows(0).Item("NOTE").ToString, "")

                    Me.CmbDestUso.Text = par.IfNull(dt.Rows(0).Item("ID_DESTINAZIONE_USO"), "")
                    Me.CmbUbicazione.Text = par.IfNull(dt.Rows(0).Item("ID_UBICAZIONE"), "")
                    Me.CmbStFisico.Text = par.IfNull(dt.Rows(0).Item("ID_STATO_FISICO"), "")



                    Me.DrLEdificio.Enabled = False
                    Me.DLRComplessi.Enabled = False
                    'Me.ImgBtnMillesim.Visible = True
                    'Me.ImageButton1.Visible = True
                    Me.imgStampa.Visible = True
                    If (dt.Rows(0).Item("ID_COMPLESSO").ToString) <> "" Then
                        DaPassare = "UNCOM"
                    ElseIf (dt.Rows(0).Item("ID_EDIFICIO").ToString) <> "" Then
                        DaPassare = "UNCOMED"
                    End If

                    ApriUCcorrelateEdifici()
                    scala()

                    If DrLEdificio.SelectedValue <> "-1" Then
                        classetabED = "tabbertabhide"
                    Else
                        classetabED = "tabbertab"
                    End If

                    If Me.DrlSc.Items.Count > 1 Then
                        Me.DrlSc.SelectedValue = (par.IfNull((dt.Rows(0).Item("id_scala").ToString), "-1"))
                        ' Me.DrlSc.Enabled = False
                    End If
                End If
                'par.OracleConn.Close()

                CType(Tab_AdDimens1.FindControl("BtnADD"), ImageButton).Visible = False
                CType(Tab_AdDimens1.FindControl("BtnElimina"), ImageButton).Visible = False

                FrmSolaLettura()

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

            Catch ex As Exception
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try

            tabdefault1 = ""
            tabdefault2 = ""
            tabdefault3 = ""
            tabdefault4 = ""
            tabdefault5 = ""

            'tabdefault7 = ""

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
            End Select

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
