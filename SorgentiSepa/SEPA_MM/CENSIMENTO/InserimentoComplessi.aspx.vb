
Partial Class CENSIMENTO_InserimentoComplessi
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim gestorId As String
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



    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            '**************PER RENDERE VISIBILE O MENO I TAB+++++++++++
            If vId <> 0 Then
                classetab = "tabbertab"
            Else
                classetab = "tabbertabhide"
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
                End If

                If vId <> 0 Then
                    Riempicampi()
                    ApriDaRicerca()
                Else
                    Riempicampi()
                    'Apro la connessione che resterà valida per tutti i metodi delle sottofinestre e del salva
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
                End If
                If Session.Item("PED2_SOLOLETTURA") = "1" Then
                    FrmSolaLettura()
                    '*****PEPPE MODIFY 26/10/2009 CAMPI MODIFICABILI ANCHE DA UTENZE ABILITATE IN SOLA LETTURA*****
                    Me.CmbFiliali.Enabled = True
                    Me.cmbFilialiAmministrative.Enabled = True
                    Me.CmbCommissariati.Enabled = True
                    '*****PEPPE MODIFY 05/01/2011 PERMESSO AD OPERATORI IN SOLA LETTURA, DI MODIFICARE I QUARTIERI
                    Me.CmbQuartieri.Enabled = True
                    Me.btnSalva.Visible = True
                    CType(Me.Tab_ComEdifici1.FindControl("btnNewEdi"), ImageButton).Visible = False
                    CType(Me.Tab_UnComuni1.FindControl("btnNuovoUC"), ImageButton).Visible = False

                    If par.IfNull(Session.Item("MOD_CENS_MANUT"), 0) > 0 Then
                        Me.DrlSchede.Enabled = True
                        'Me.btnrilievo.Visible = True
                        Me.cmbPeriodo.Enabled = True
                    End If
                    If Session.Item("CENS_MANUT_SL") > 0 Then
                        Me.CENS_MANUT_SL.value = 1
                    End If
                End If

                If Request.QueryString("SLE") = 1 Or Session.Item("SLE") = 1 Then
                    FrmSolaLettura()
                    Me.ImageButton1.Visible = False
                    CType(Me.Tab_ComEdifici1.FindControl("btnNewEdi"), ImageButton).Visible = False
                    CType(Me.Tab_UnComuni1.FindControl("btnNuovoUC"), ImageButton).Visible = False
                    Session.Add("SLE", 1)
                End If

                If Session("ID_CAF") <> "6" Then
                    FrmSolaLettura()

                    Session("PED2_SOLOLETTURA") = "1"
                    Me.ImageButton1.Visible = False
                    CType(Me.Tab_ComEdifici1.FindControl("btnNewEdi"), ImageButton).Visible = False
                    CType(Me.Tab_UnComuni1.FindControl("btnNuovoUC"), ImageButton).Visible = False
                End If

                Dim CTRL As Control

                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        If DirectCast(CTRL, DropDownList).ID <> "DrlSchede" And DirectCast(CTRL, DropDownList).ID <> "cmbPeriodo" Then
                            DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                        End If

                    End If
                Next

                'FINE DEL CICLO
            End If
            TxtDenComplesso.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            TxtVia.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            TxtLocalita.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtpCarrabili.Attributes.Add("onkeyUp", "javascript:valid(this,'notnumbers');")
            'Controllo modifica campi nel form
            'TxtDataInizio.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            'TxtDataInizio.Attributes.Add("onfocus", "javascript:selectText(this);")
            TxtDataInizio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            'TxtDataFine.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            'TxtDataFine.Attributes.Add("onfocus", "javascript:selectText(this);")
            TxtDataFine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Me.txtindietro.Text = txtindietro.Text - 1
            'If Session("ID_CAF") <> "6" Then
            '    NascondiCampi()
            'End If
            VerificaModificheSottoform()

            VerificaFiliale()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()
        End Try

    End Sub
    Public Sub VerificaModificheSottoform()
        'Se vengono effettuate modifiche nei sotto-form questo manda il messaggio in casa di uscita senza salvataggio
        If Session.Item("MODIFICASOTTOFORM") = 1 Then
            Me.txtModificato.value = 1
            Session.Item("MODIFICASOTTOFORM") = 0
        End If

    End Sub
    Private Sub NascondiCampi()
        Me.DrLLotto.Enabled = False
        'Me.TxtCodComplesso.Visible = False
        Me.TxtCodGimi.ReadOnly = True
        Me.TxtDenComplesso.ReadOnly = True
        Me.DrLProvenienza.Enabled = False
        Me.DrLTipoInd.Enabled = False
        Me.DrLGestore.Enabled = False
        Me.TxtVia.ReadOnly = True
        Me.TxtCivico.ReadOnly = True
        Me.DrLComune.Enabled = False
        Me.TxtLocalita.ReadOnly = True
        Me.TxtCap.ReadOnly = True
        'Me.Lbl3.ReadOnly = True
        'Me.Label3.Visible = False
        'Me.Label4.Visible = False
        'Me.Label12.Visible = False
        'Me.Label5.Visible = False
        'Me.Label7.Visible = False
        'Me.Label29.Visible = False
        'Me.Label9.Visible = False
        'Me.Label11.Visible = False
        'Me.Label10.Visible = False
        'Me.Label13.Visible = False
        'Me.imgStampa.Visible = False
    End Sub
    Private Sub FrmSolaLettura()
        Try
            Me.btnSalva.Visible = False
            Me.txtVisibility.Value = 1
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
    'Private Sub FrmSolaLetturaTranneSkede()
    '    Try
    '        Me.btnSalva.Visible = False
    '        Me.txtVisibility.Value = 1
    '        Dim CTRL As Control = Nothing
    '        For Each CTRL In Me.form1.Controls
    '            If TypeOf CTRL Is TextBox Then
    '                DirectCast(CTRL, TextBox).Enabled = False
    '            ElseIf TypeOf CTRL Is DropDownList Then
    '                DirectCast(CTRL, DropDownList).Enabled = False
    '            End If
    '        Next
    '        Me.DrlSchede.Enabled = True
    '        Me.cmbPeriodo.Enabled = True
    '        CType(Tab_Millesimali1.FindControl("BtnADD"), ImageButton).Visible = False
    '        CType(Tab_Millesimali1.FindControl("btnModifica"), ImageButton).Visible = False
    '        CType(Tab_Millesimali1.FindControl("btnDelete"), ImageButton).Visible = False

    '        CType(Tab_UtMillesimali1.FindControl("BtnADD"), ImageButton).Visible = False
    '        CType(Tab_UtMillesimali1.FindControl("BtnElimina"), ImageButton).Visible = False

    '        CType(Tab_Servizi1.FindControl("BtnElimina"), ImageButton).Visible = False

    '        CType(Tab_ImpComuni1.FindControl("BtnElimina"), ImageButton).Visible = False

    '    Catch ex As Exception
    '        Me.LblErrore.Visible = True
    '        LblErrore.Text = ex.Message
    '    End Try
    'End Sub
    Private Sub Riempicampi()

        Dim ds As New Data.DataSet


        '**************************************************************
        Try
            'riempo una combo con tre valori fissi
            'Me.DrLGestore.Items.Add("GEFI")
            'Me.DrLGestore.Items.Add("PIRELLI")
            'Me.DrLGestore.Items.Add("ROMEO")
            'Me.DrLGestore.Items.Add("DIRETTA")
            'Me.DrLGestore.Items.Add("ALER")
            'Me.DrLGestore.Items.Add("MM") 
            'Apro la connsessione con il DB
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT DESCRIZIONE_PATRIMONIO FROM SISCOM_MI.TAB_GESTORI_ARCHIVIO ORDER BY DESCRIZIONE_PATRIMONIO ASC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                DrLGestore.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE_PATRIMONIO"), " "), par.IfNull(myReader1("DESCRIZIONE_PATRIMONIO"), -1)))
            End While
            myReader1.Close()
            'Me.DrLGestore.SelectedItem.Text = "ROMEO"
            If Session("PED2_ESTERNA") = "1" Then
                Me.DrLLotto.Items.Add("04")
                Me.DrLLotto.Items.Add("05")
            Else
                Me.DrLLotto.Items.Add("01")
                Me.DrLLotto.Items.Add("02")
                Me.DrLLotto.Items.Add("03")
                Me.DrLLotto.Items.Add("04")
                Me.DrLLotto.Items.Add("05")

            End If


            'Riempio l'oggetto da con quello che restituisce la select
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPO_COMPLESSO_IMMOBILIARE"
            myReader1 = par.cmd.ExecuteReader()
            DrLTipoComplesso.Items.Add(New ListItem(" ", -1))

            While myReader1.Read
                DrLTipoComplesso.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()
            ''''''DA ELIMINARE
            'DrLTipoComplesso.SelectedValue = "COMPLERES"
            'Passo il risultato della select alla DropDownList impostando Indice e Testo associato

            '**************************************************************

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.LIVELLO_POSSESSO"
            myReader1 = par.cmd.ExecuteReader()
            DdlLivelloPossesso.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DdlLivelloPossesso.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()
            ''''''DA ELIMINARE
            'DdlLivelloPossesso.SelectedValue = "COMPLESSO"
            '**************************************************************
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPO_UBICAZIONE_LG_392_78"

            myReader1 = par.cmd.ExecuteReader()
            DdLCodUbicaz.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DdLCodUbicaz.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()
            ''''''DA ELIMINARE
            'DdLCodUbicaz.SelectedValue = "ALTRO"
            '**************************************************************
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_PROVENIENZA"

            myReader1 = par.cmd.ExecuteReader()
            DrLProvenienza.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLProvenienza.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()
            ''''''DA ELIMINARE
            'DrLProvenienza.SelectedValue = "DEMA"
            '**************************************************************

            par.cmd.CommandText = "SELECT COMU_COD, COMU_DESCR FROM sepa.COMUNI order by comu_descr asc"
            myReader1 = par.cmd.ExecuteReader()
            DrLComune.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLComune.Items.Add(New ListItem(par.IfNull(myReader1("COMU_DESCR"), " "), par.IfNull(myReader1("COMU_COD"), -1)))
            End While
            DrLComune.SelectedValue = "F205"
            myReader1.Close()


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_INDIRIZZO "
            myReader1 = par.cmd.ExecuteReader()
            DrLTipoInd.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLTipoInd.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()

            '******PeppeModify 29/10/2009**********
            '******Aggiunta delle combo Commissariati e Filiali ******
            '**************COMMISSARIATI******************************

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_COMMISSARIATI "
            myReader1 = par.cmd.ExecuteReader()
            CmbCommissariati.Items.Add(New ListItem(" ", ""))
            While myReader1.Read
                CmbCommissariati.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()

            '*****************FILIALI*********************************

            par.cmd.CommandText = "SELECT TAB_FILIALI.NOME, TAB_FILIALI.ID, (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP||' '||COMUNI_NAZIONI.NOME) AS INDIRIZZO FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI  WHERE TAB_FILIALI.ID_INDIRIZZO = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = COMUNI_NAZIONI.COD AND (TAB_FILIALI.ID_TIPO_ST IS NULL OR TAB_FILIALI.ID_TIPO_ST = 0)"
            myReader1 = par.cmd.ExecuteReader()
            CmbFiliali.Items.Add(New ListItem(" ", ""))
            cmbFilialiAmministrative.Items.Add(New ListItem(" ", ""))
            While myReader1.Read
                CmbFiliali.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " ") & " - " & par.IfNull(myReader1("INDIRIZZO"), " "), par.IfNull(myReader1("ID"), -1)))
                cmbFilialiAmministrative.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " ") & " - " & par.IfNull(myReader1("INDIRIZZO"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()

            '*****************QUARTIERI*********************************

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_QUARTIERI"
            myReader1 = par.cmd.ExecuteReader()
            CmbQuartieri.Items.Add(New ListItem(" ", ""))
            While myReader1.Read
                CmbQuartieri.Items.Add(New ListItem(myReader1("NOME"), myReader1("ID")))
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

            '******End Mpdify********
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ''''' DA ELIMINARE
            'Me.DrLTipoInd.SelectedValue = "1"
            'Me.TxtLocalita.Text = "MILANO"
            'Me.TxtCap.Text = "20126"
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
        End Try

    End Sub

    Protected Sub imgUscita_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        Try
            If Session.Item("SLE") = 0 Or IsNothing(Session.Item("SLE")) Then

                If txtModificato.value <> "111" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.OracleConn.Close()

                    HttpContext.Current.Session.Remove("TRANSAZIONE")
                    HttpContext.Current.Session.Remove("CONNESSIONE")
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Session.Item("LAVORAZIONE") = 0

                    Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                Else
                    txtModificato.value = "1"
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

                Response.Write("<script language='javascript'> { self.close() }</script>")
                Session.Remove("SLE")
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Dim ERRORE As String = ""
        If par.ControllaCAP(Me.DrLComune.SelectedValue.ToString, Me.TxtCap.Text, ERRORE) = False Then
            Response.Write("<SCRIPT>alert('CAP errato!I possibili valori sono:\n" & ERRORE & "');</SCRIPT>")
            Exit Sub
        End If
        If vId <> 0 Then

            Me.update()
        Else
            Me.SaveInsert()
        End If
        txtModificato.value = "0"

        'Me.txtindietro.Text = txtindietro.Text - 1
    End Sub
    Private Sub SaveInsert()

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        Dim nextval As Integer
        Dim CodComplesso As String = ""
        Dim IdComplesso As Integer
        Dim nexindirizzo As String = ""
        Try
            Me.IdGestore()
            'Richiamo la connessione
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            '***************++31/08/2009 VERIFICA DENOMINAZIONE COMPLESSO UNIVOCA *************************
            par.cmd.CommandText = "select * FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.DENOMINAZIONE ='" & par.PulisciStrSql(Me.TxtDenComplesso.Text.ToUpper) & "'"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                Response.Write("<SCRIPT>alert('       Impossibile completare!\r\nEsiste già un complesso con la stessa denominazione!');</SCRIPT>")
                Exit Sub
            End If
            myReader.Close()
            '***************++31/08/2009 VERIFICA DENOMINAZIONE COMPLESSO UNIVOCA *************************


            'Seleziono nextval della per complessi immobiliari e lo memorizzo in una variabile
            par.cmd.CommandText = "select SISCOM_MI.SEQ_COMPLESSI_IMMOBILIARI.nextval from dual"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                nextval = myReader(0)
            End If
            myReader.Close()

            CodComplesso = DrLLotto.SelectedValue & nextval
            IdComplesso = gestorId & nextval

            Me.TxtCodComplesso.Text = CodComplesso

            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_INDIRIZZI.NEXTVAL FROM DUAL"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                nexindirizzo = myReader(0)
            End If
            myReader.Close()
            'nexindirizzo = gestorId.Substring(0, 1) & nexindirizzo

            'If Me.TxtDenComplesso.Text <> "" AndAlso Me.TxtVia.Text <> "" AndAlso Me.TxtCap.Text <> "" AndAlso Me.TxtCivico.Text <> "" Then

            If Me.TxtDenComplesso.Text <> "" AndAlso Me.DrLTipoComplesso.SelectedValue <> "-1" AndAlso Me.DdlLivelloPossesso.SelectedValue <> "-1" AndAlso Me.DdLCodUbicaz.SelectedValue <> "-1" AndAlso Me.DrLProvenienza.SelectedValue <> "-1" Then
                'Esecuzione insert nuovo indirizzo
                If Me.TxtVia.Text <> "" AndAlso Me.TxtCivico.Text <> "" AndAlso Me.DrLTipoInd.SelectedValue <> "-1" And Me.TxtCap.Text <> "" Then

                    'Apro la Transazione
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.INDIRIZZI (ID,DESCRIZIONE,CIVICO,CAP,LOCALITA,COD_COMUNE) VALUES (" & nexindirizzo & ", '" & DrLTipoInd.SelectedItem.Text & " " & par.PulisciStrSql(TxtVia.Text) & "','" & par.PulisciStrSql(TxtCivico.Text) & "', '" & par.PulisciStrSql(TxtCap.Text) & "', '" & par.PulisciStrSql(TxtLocalita.Text) & "', '" & Me.DrLComune.SelectedValue & "')"
                    par.cmd.ExecuteNonQuery()
                    Me.vIdIndirizzo = nexindirizzo
                    par.cmd.CommandText = ""
                Else
                    Response.Write("<SCRIPT>alert('Impossibile completare!\r\nNon sono stati inseriti i dati relativi all\'indirizzo!');</SCRIPT>")
                    Exit Sub
                End If
                '*******non mi serve più ho messo l'ID del nuovo indirizzo in nexindirizzo!
                'par.cmd.CommandText = "SELECT ID from SISCOM_MI.indirizzi order by id desc"
                'myReader = par.cmd.ExecuteReader
                'If myReader.Read Then
                '    NuovIndirizzo = myReader("ID")
                'End If
                'myReader.Close()
                '*******
                'Esecuzione insert nuovo complesso, con codice indirizzo appena creato!
                par.cmd.CommandText = ""
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.COMPLESSI_IMMOBILIARI (ID, COD_COMPLESSO,COD_COMPLESSO_GIMI,DENOMINAZIONE,COD_TIPO_COMPLESSO, COD_LIVELLO_POSSESSO,ID_INDIRIZZO_RIFERIMENTO, COD_TIPOLOGIA_PROVENIENZA,COD_TIPO_UBICAZIONE_LG_392_78,ID_OPERATORE_INSERIMENTO, LOTTO,ID_COMMISSARIATO, ID_FILIALE,ID_FILIALE_AMM, ID_QUARTIERE,NOTE,NUM_PASSI_CARRABILI,SUBFASCIA,DATA_INIZIO_GEST,DATA_FINE_GEST)" _
                & " VALUES(" & IdComplesso & ", '" & CodComplesso & "', '" & par.PulisciStrSql(Me.TxtCodGimi.Text) & "', '" & par.PulisciStrSql(Me.TxtDenComplesso.Text.ToUpper) & "', '" & Me.DrLTipoComplesso.SelectedValue & "', '" & Me.DdlLivelloPossesso.SelectedValue & "', " & nexindirizzo & ", '" & Me.DrLProvenienza.SelectedValue & "', '" & Me.DdLCodUbicaz.SelectedValue & "' ,'" & Session("ID_OPERATORE") & "','" & DrLLotto.SelectedValue & "','" & Me.CmbCommissariati.SelectedValue & "','" & Me.CmbFiliali.SelectedValue & "','" & Me.cmbFilialiAmministrative.SelectedValue & "', " & par.IfEmpty(Me.CmbQuartieri.SelectedValue, "Null") & ",'" & par.PulisciStrSql(Me.txtNote.Text) & "'," & par.IfEmpty(Me.txtPCarrabili.Text, "NULL") & ",'" & par.PulisciStrSql(Me.txtSubFascia.Text) & "','" & par.AggiustaData(TxtDataInizio.Text) & "','" & par.AggiustaData(TxtDataFine.Text) & "') "
                par.cmd.ExecuteNonQuery()
                vId = IdComplesso
                '****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_COMPLESSI (ID_COMPLESSO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','')"
                par.cmd.ExecuteNonQuery()
                '*******************************FINE****************************************
                '***************************************************************************

                par.myTrans.Commit()
                '*********************PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                If My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/CO/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.TxtCodComplesso.Text) & "*.*").Count > 0 Or My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/CO/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.TxtCodComplesso.Text) & "*.*").Count > 0 Then
                    Me.btnFoto.Visible = True
                    Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
                Else
                    Me.btnFoto.Visible = True
                    Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan2.png"
                End If

                'Blocco il complesso per eventuali modifiche da altri utenti
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                myReader = par.cmd.ExecuteReader
                myReader.Close()
                classetab = "tabbertab"

                'Riapro una nuova transazione
                Session.Item("LAVORAZIONE") = "1"
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                Response.Write("<SCRIPT>alert('Operazione eseguita correttamente!');</SCRIPT>")

                '27/09/2010 GESTIONE OPERATORI CENSIMENTO ST. MANUTENTIVO
                If par.IfNull(Session.Item("LIVELLO"), 0) <> 1 Then
                    If par.IfNull(Session.Item("MOD_CENS_MANUT"), 0) > 0 Then
                        Me.DrlSchede.Enabled = True
                        Me.cmbPeriodo.Enabled = True
                    End If

                    If Session.Item("CENS_MANUT_SL") > 0 Then
                        Me.CENS_MANUT_SL.value = 1

                    End If
                Else
                    Me.DrlSchede.Enabled = True
                    Me.cmbPeriodo.Enabled = True
                    Me.CENS_MANUT_SL.value = 0
                End If


                'Me.btnrilievo.Visible = True
                'Me.ImgBtnMillesimali.Visible = False
                'Me.ImgUtenza.Visible = False
                Me.DrLLotto.Enabled = False
                Me.DrLGestore.Enabled = False
                Me.imgStampa.Visible = True
            Else
                Response.Write("<SCRIPT>alert('       Impossibile completare!\r\nAvvalorare tutti i campi obbligatori!');</SCRIPT>")
                Exit Sub
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub update()
        Try

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            Dim idIndrizzo As String = ""


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            If Me.TxtDenComplesso.Text <> "" AndAlso Me.DrLTipoComplesso.SelectedValue <> "-1" AndAlso Me.DdlLivelloPossesso.SelectedValue <> "-1" AndAlso Me.DdLCodUbicaz.SelectedValue <> "-1" AndAlso Me.DrLProvenienza.SelectedValue <> "-1" AndAlso Me.DrLTipoInd.SelectedValue <> "-1" AndAlso par.IfEmpty(Me.TxtVia.Text, "Null") <> "Null" Then
                '***************++31/08/2009 VERIFICA DENOMINAZIONE COMPLESSO UNIVOCA *************************
                par.cmd.CommandText = "select * FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.DENOMINAZIONE ='" & par.PulisciStrSql(Me.TxtDenComplesso.Text.ToUpper) & "' AND COMPLESSI_IMMOBILIARI.ID <>" & vId
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    Response.Write("<SCRIPT>alert('       Impossibile completare!\r\nEsiste già un complesso con la stessa denominazione!');</SCRIPT>")
                    par.myTrans.Rollback()
                    'Apro la Transazione
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

                    Exit Sub
                End If
                myReader.Close()
                '***************++31/08/2009 VERIFICA DENOMINAZIONE COMPLESSO UNIVOCA *************************
                par.cmd.CommandText = "UPDATE SISCOM_MI.COMPLESSI_IMMOBILIARI SET COD_COMPLESSO_GIMI = '" & par.PulisciStrSql(Me.TxtCodGimi.Text) & "', DENOMINAZIONE = '" & par.PulisciStrSql(Me.TxtDenComplesso.Text) & "', COD_TIPO_COMPLESSO = '" & Me.DrLTipoComplesso.SelectedValue.ToString & "', COD_LIVELLO_POSSESSO = '" & Me.DdlLivelloPossesso.SelectedValue.ToString _
                    & "', COD_TIPOLOGIA_PROVENIENZA = '" & Me.DrLProvenienza.SelectedValue.ToString & "', COD_TIPO_UBICAZIONE_LG_392_78 = '" & Me.DdLCodUbicaz.SelectedValue.ToString & "', ID_OPERATORE_AGGIORNAMENTO = '" & Session("ID_OPERATORE") & "',ID_COMMISSARIATO = '" & Me.CmbCommissariati.SelectedValue & "',ID_FILIALE = '" & CmbFiliali.SelectedValue & "',ID_FILIALE_AMM = '" & cmbFilialiAmministrative.SelectedValue & "'," _
                    & " ID_QUARTIERE = " & par.IfEmpty(Me.CmbQuartieri.SelectedValue.ToString, "Null") & ", NOTE = '" & par.PulisciStrSql(Me.txtNote.Text) & "', NUM_PASSI_CARRABILI = " & par.IfEmpty(Me.txtPCarrabili.Text, "NULL") & ", SUBFASCIA='" & par.PulisciStrSql(Me.txtSubFascia.Text) & "',DATA_INIZIO_GEST='" & par.AggiustaData(TxtDataInizio.Text) & "',DATA_FINE_GEST='" & par.AggiustaData(TxtDataFine.Text) & "' WHERE ID = " & vId
                par.cmd.ExecuteNonQuery()

                '****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_COMPLESSI (ID_COMPLESSO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','')"
                par.cmd.ExecuteNonQuery()
                '*******************************FINE****************************************
                '***************************************************************************


                par.cmd.CommandText = "SELECT ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = " & vId
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    idIndrizzo = myReader(0)
                End If
                myReader.Close()

                par.cmd.CommandText = "UPDATE SISCOM_MI.INDIRIZZI SET DESCRIZIONE = '" & DrLTipoInd.SelectedItem.Text & " " & par.PulisciStrSql(TxtVia.Text) & "' , CIVICO = '" & par.PulisciStrSql(TxtCivico.Text) & "', CAP = '" & par.PulisciStrSql(TxtCap.Text) & "' , LOCALITA= '" & par.PulisciStrSql(TxtLocalita.Text) & "' , COD_COMUNE = '" & Me.DrLComune.SelectedValue.ToString & "' WHERE ID = " & idIndrizzo
                par.cmd.ExecuteNonQuery()
                par.myTrans.Commit()
                '*********************PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                If My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/CO/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.TxtCodComplesso.Text) & "*.*").Count > 0 Or My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/CO/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.TxtCodComplesso.Text) & "*.*").Count > 0 Then
                    Me.btnFoto.Visible = True
                    Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
                Else
                    Me.btnFoto.Visible = True
                    Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan2.png"
                End If

                Response.Write("<SCRIPT>alert('Operazione eseguita correttamente!');</SCRIPT>")
                'Me.ImgBtnMillesimali.Visible = True
                'Me.ImgUtenza.Visible = True
                Me.DrLLotto.Enabled = False
                Me.DrLGestore.Enabled = False
                Me.imgStampa.Visible = True

                'Riapro una nuova transazione
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            Else
                Response.Write("<SCRIPT>alert('       Impossibile completare!\r\nAvvalorare tutti i campi obbligatori!');</SCRIPT>")
                Exit Sub
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Function IdGestore()
        If Me.DrLGestore.SelectedItem.Text = "GEFI" Then
            gestorId = 1000
        ElseIf Me.DrLGestore.SelectedItem.Text = "PIRELLI" Then
            gestorId = 2000
        ElseIf Me.DrLGestore.SelectedItem.Text = "ROMEO" Then
            gestorId = 3000
        ElseIf Me.DrLGestore.SelectedItem.Text = "DIRETTA" Then
            gestorId = 4000
            'MODIFY 16/10/2009 Richiesta di Gobbi a Max per Inserimento complesso con gestore ...
        ElseIf Me.DrLGestore.SelectedItem.Text = "ALER" Then
            gestorId = 5000
            'Fine NUOVA MODIFY

            'MODIFY 19/11/2015 Richiesta di MARCO per Inserimento complesso con gestore MM ...
        ElseIf Me.DrLGestore.SelectedItem.Text = "MM" Then
            gestorId = 6000

        End If
    End Function
    Private Sub GestoreDaId()

        If CStr(Mid(vId, 1, 4)) = 1000 Then
            DrLGestore.SelectedValue = "GEFI"
            'DrLGestore.Items.FindByText("GEFI").Selected = True
        ElseIf CStr(Mid(vId, 1, 4)) = 2000 Then
            DrLGestore.SelectedValue = "PIRELLI"
            'DrLGestore.Items.FindByText("PIRELLI").Selected = True
        ElseIf CStr(Mid(vId, 1, 4)) = 3000 Then
            DrLGestore.SelectedValue = "ROMEO"
            'DrLGestore.Items.FindByText("ROMEO").Selected = True
        ElseIf CStr(Mid(vId, 1, 4)) = 4000 Then
            DrLGestore.SelectedValue = "DIRETTA"
            'DrLGestore.Items.FindByText("DIRETTA").Selected = True
        ElseIf CStr(Mid(vId, 1, 4)) = 5000 Then
            DrLGestore.SelectedValue = "ALER"
            'DrLGestore.Items.FindByText("ALER").Selected = True
        ElseIf CStr(Mid(vId, 1, 4)) = 6000 Then
            DrLGestore.SelectedValue = "MM"
            'DrLGestore.Items.FindByText("MM").Selected = True
        End If
    End Sub
    Private Function RicavaVial(ByVal indirizzo As String) As String
        Dim pos As Integer
        Dim via As String
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
    End Function

    Private Sub ApriDaRicerca()
        If vId <> -1 Then
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable
            Dim idindirizzorif As String = ""
            Dim scriptblock As String = ""

            Try

                GestoreDaId()
                'CONNESSIONE E MEMORIZZO LA CONNESSIONE
                par.OracleConn.Open()
                par.SettaCommand(par)
                HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
                '08/09/2010 CARICAMENTO DELLA LISTA DELLE SCHEDE DI RILIEVO
                LoadDrlSchede()

                If Request.QueryString("SK") <> "0" Then
                    SelezionaSkeda(Request.QueryString("SK"))
                End If


                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = " & vId & " FOR UPDATE NOWAIT"

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then


                    idindirizzorif = (dt.Rows(0).Item("ID_INDIRIZZO_RIFERIMENTO"))
                    Me.vIdIndirizzo = idindirizzorif
                    Me.TxtCodGimi.Text = par.IfNull(dt.Rows(0).Item("COD_COMPLESSO_GIMI"), " ")
                    Me.TxtCodComplesso.Text = dt.Rows(0).Item("COD_COMPLESSO")


                    Me.DrLTipoComplesso.SelectedValue = (dt.Rows(0).Item("COD_TIPO_COMPLESSO"))


                    'Me.DrLLotto.Items.FindByText("05").Selected = True
                    'Me.DrLLotto.Items.FindByText(dt.Rows(0).Item("LOTTO")).Selected = True
                    Me.DrLLotto.SelectedValue = (par.IfNull((dt.Rows(0).Item("LOTTO")), "04"))

                    Me.DdlLivelloPossesso.SelectedValue = (dt.Rows(0).Item("COD_LIVELLO_POSSESSO"))

                    Me.DdLCodUbicaz.SelectedValue = (dt.Rows(0).Item("COD_TIPO_UBICAZIONE_LG_392_78"))

                    Me.TxtDenComplesso.Text = dt.Rows(0).Item("DENOMINAZIONE")

                    Me.DrLProvenienza.SelectedValue = (dt.Rows(0).Item("COD_TIPOLOGIA_PROVENIENZA"))
                    Me.txtPCarrabili.Text = par.IfNull(dt.Rows(0).Item("NUM_PASSI_CARRABILI"), "")

                    Me.CmbCommissariati.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_COMMISSARIATO"), "")
                    Me.CmbFiliali.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_FILIALE"), "")
                    Me.cmbFilialiAmministrative.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_FILIALE_AMM"), "")
                    Me.CmbQuartieri.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_QUARTIERE"), "")
                    Me.txtNote.Text = par.IfNull(dt.Rows(0).Item("NOTE"), "")
                    Me.txtSubFascia.Text = par.IfNull(dt.Rows(0).Item("SUBFASCIA"), "")

                    TxtDataInizio.Text = par.FormattaData(par.IfNull(dt.Rows(0).Item("DATA_INIZIO_GEST"), ""))
                    TxtDataFine.Text = par.FormattaData(par.IfNull(dt.Rows(0).Item("DATA_FINE_GEST"), ""))

                End If
                da = Nothing

                dt = New Data.DataTable
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID = " & idindirizzorif & ""

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    'Me.DrLTipoInd.SelectedValue = -1
                    Me.DrLTipoInd.Items.FindByText((RicavaVial(dt.Rows(0).Item("DESCRIZIONE")))).Selected = True

                    Me.TxtVia.Text = RicavaDescVia(dt.Rows(0).Item("DESCRIZIONE"))
                    Me.TxtCivico.Text = dt.Rows(0).Item("CIVICO")
                    Me.TxtCap.Text = dt.Rows(0).Item("CAP")

                    DrLComune.Items.FindByValue("F205").Selected = False
                    'Me.DrLComune.SelectedValue = 0
                    Me.DrLComune.SelectedValue = (dt.Rows(0).Item("COD_COMUNE"))
                    Me.TxtLocalita.Text = par.IfNull(dt.Rows(0).Item("LOCALITA"), " ")

                    'Apro una nuova transazione
                    Session.Item("LAVORAZIONE") = "1"
                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                End If
                Me.DrLLotto.Enabled = False
                Me.DrLGestore.Enabled = False
                'Me.ImgBtnMillesimali.Visible = True
                'Me.ImgUtenza.Visible = True
                Me.imgStampa.Visible = True
                Me.btnFoto.Visible = True


                If par.IfNull(Session.Item("LIVELLO"), 0) <> 1 Then

                    If par.IfNull(Session.Item("MOD_CENS_MANUT"), 0) > 0 Then
                        Me.DrlSchede.Enabled = True
                        Me.cmbPeriodo.Enabled = True
                    End If

                    If Session.Item("CENS_MANUT_SL") > 0 Then
                        Me.CENS_MANUT_SL.value = 1

                    End If
                Else
                    Me.DrlSchede.Enabled = True
                    Me.cmbPeriodo.Enabled = True
                    Me.CENS_MANUT_SL.value = 0
                End If


                '*********************PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                Try
                    '******************28/09/2010 gestione dell'errore in caso di non coerenza del path delle foto e delle planimetrie!
                    If My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/CO/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.TxtCodComplesso.Text) & "*.*").Count > 0 Or My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/CO/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.TxtCodComplesso.Text) & "*.*").Count > 0 Then
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

            Catch EX1 As Oracle.DataAccess.Client.OracleException
                If EX1.Number = 54 Then
                    par.OracleConn.Close()
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Complesso aperto da un altro utente. Non è possibile effettuare modifiche!');" _
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
                btnSalva.Visible = False
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.OracleConn.Close()
                End If
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message

            End Try

        End If


    End Sub

    Private Function RicavaDescVia(ByVal indirizzo As String) As String
        Dim pos As Integer
        Dim descrizione As String

        pos = InStr(1, indirizzo, " ")
        If pos > 0 Then
            descrizione = Mid(indirizzo, pos + 1)
            RicavaDescVia = descrizione
        End If
    End Function
    Public Property vId() As Long
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

    'Protected Sub ImgBtnMillesimali_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnMillesimali.Click
    '    Me.txtindietro.Text = txtindietro.Text - 1
    '    Response.Write("<script>window.open('InsTabMillesim.aspx?ID=" & vId & ",&Pas=CO','FINESTRAESTERNA', 'resizable=no, width=630, height=280');</script>")
    'End Sub


    'Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgUtenza.Click
    '    If EsistonoMilles() = True Then
    '        Response.Write("<script>window.open('ListaUtenze.aspx?ID=" & vId & ",&Pas=COMP','FINESTRAESTERNA', 'resizable=no, width=630, height=280');</script>")
    '    Else
    '        Exit Sub
    '    End If
    'End Sub





    Protected Sub imgStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStampa.Click
        If vId <> -1 Then
            'Me.txtindietro.Text = txtindietro.Text - 1
            If txtModificato.value <> "111" Then
                Response.Write("<script>window.open('StampaC.aspx?ID=" & vId & "','MILLESIMALI','');</script>")
            Else
                txtModificato.value = "1"
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            End If
        End If

    End Sub

    Protected Sub ImageButton1_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        'Me.txtindietro.Text = txtindietro.Text - 1
        'Response.Write("<script>history.go(" & txtindietro.Text & ");</script>")

        If txtModificato.value <> "111" Then
            Session.Item("LAVORAZIONE") = 0
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            HttpContext.Current.Session.Remove("TRANSAZIONE")
            HttpContext.Current.Session.Remove("CONNESSIONE")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Select Case Request.QueryString("C")
                Case "RicercaComplessi"
                    Response.Redirect(Request.QueryString("C") & ".aspx")
                Case "ElencoComplessi"
                    Response.Redirect(Request.QueryString("C") & ".aspx")
                Case "RisultSchede"
                    Response.Redirect("RisultSchede.aspx?SCHEDA=" & Request.QueryString("SK"))

                Case Nothing
                    Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

                Case Else
                    Response.Write("<script>document.location.href=""RicercaComplessi.aspx""</script>")

                    '    Response.Redirect(Request.QueryString("C") & ".aspx?ID=" & Request.QueryString("IDC"))
            End Select
        Else
            txtModificato.value = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub

    Private Sub ApriFrmWithDBLock()
        If vId <> -1 Then
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable
            Dim idindirizzorif As String = ""

            Try
                GestoreDaId()
                'CONNESSIONE E MEMORIZZO LA CONNESSIONE
                par.OracleConn.Open()
                par.SettaCommand(par)


                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = " & vId

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then


                    idindirizzorif = (dt.Rows(0).Item("ID_INDIRIZZO_RIFERIMENTO"))
                    vIdIndirizzo = idindirizzorif
                    Me.TxtCodGimi.Text = par.IfNull(dt.Rows(0).Item("COD_COMPLESSO_GIMI"), " ")
                    Me.TxtCodComplesso.Text = dt.Rows(0).Item("COD_COMPLESSO")


                    Me.DrLTipoComplesso.SelectedValue = (dt.Rows(0).Item("COD_TIPO_COMPLESSO"))


                    'Me.DrLLotto.Items.FindByText("05").Selected = True
                    'Me.DrLLotto.Items.FindByText(dt.Rows(0).Item("LOTTO")).Selected = True
                    Me.DrLLotto.SelectedValue = (par.IfNull((dt.Rows(0).Item("LOTTO")), "04"))

                    Me.DdlLivelloPossesso.SelectedValue = (dt.Rows(0).Item("COD_LIVELLO_POSSESSO"))

                    Me.DdLCodUbicaz.SelectedValue = (dt.Rows(0).Item("COD_TIPO_UBICAZIONE_LG_392_78"))

                    Me.TxtDenComplesso.Text = dt.Rows(0).Item("DENOMINAZIONE")

                    Me.DrLProvenienza.SelectedValue = (dt.Rows(0).Item("COD_TIPOLOGIA_PROVENIENZA"))

                    Me.CmbCommissariati.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_COMMISSARIATO"), "")
                    Me.CmbFiliali.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_FILIALE"), "")
                    Me.cmbFilialiAmministrative.SelectedValue = par.IfNull(dt.Rows(0).Item("ID_FILIALE_AMM"), "")
                    Me.txtNote.Text = par.IfNull(dt.Rows(0).Item("NOTE"), "")
                    Me.txtSubFascia.Text = par.IfNull(dt.Rows(0).Item("SUBFASCIA"), "")

                    Me.TxtDataInizio.Text = par.FormattaData(par.IfNull(dt.Rows(0).Item("DATA_INIZIO_GEST"), ""))
                    Me.TxtDataFine.Text = par.FormattaData(par.IfNull(dt.Rows(0).Item("DATA_FINE_GEST"), ""))


                End If
                da = Nothing

                dt = New Data.DataTable
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID = " & idindirizzorif & ""

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    'Me.DrLTipoInd.SelectedValue = -1
                    Me.DrLTipoInd.Items.FindByText((RicavaVial(dt.Rows(0).Item("DESCRIZIONE")))).Selected = True

                    Me.TxtVia.Text = RicavaDescVia(dt.Rows(0).Item("DESCRIZIONE"))
                    Me.TxtCivico.Text = dt.Rows(0).Item("CIVICO")
                    Me.TxtCap.Text = dt.Rows(0).Item("CAP")

                    DrLComune.Items.FindByValue("F205").Selected = False
                    'Me.DrLComune.SelectedValue = 0
                    Me.DrLComune.SelectedValue = (dt.Rows(0).Item("COD_COMUNE"))
                    Me.TxtLocalita.Text = par.IfNull(dt.Rows(0).Item("LOCALITA"), " ")
                End If

                'par.OracleConn.Close()

                Me.DrLLotto.Enabled = False
                Me.DrLGestore.Enabled = False
                'Me.ImgBtnMillesimali.Visible = True
                'Me.ImgUtenza.Visible = True
                Me.imgStampa.Visible = True
                Me.btnFoto.Visible = True

                Me.FrmSolaLettura()

                CType(Tab_Millesimali1.FindControl("BtnADD"), ImageButton).Visible = False
                CType(Tab_Millesimali1.FindControl("btnModifica"), ImageButton).Visible = False
                CType(Tab_Millesimali1.FindControl("btnDelete"), ImageButton).Visible = False

                CType(Tab_UtMillesimali1.FindControl("BtnADD"), ImageButton).Visible = False
                CType(Tab_UtMillesimali1.FindControl("BtnElimina"), ImageButton).Visible = False

                CType(Tab_Servizi1.FindControl("BtnElimina"), ImageButton).Visible = False

                CType(Tab_ImpComuni1.FindControl("BtnElimina"), ImageButton).Visible = False

                If par.IfNull(Session.Item("LIVELLO"), 0) <> 1 Then

                    If par.IfNull(Session.Item("MOD_CENS_MANUT"), 0) > 0 Then
                        Me.DrlSchede.Enabled = True
                        Me.cmbPeriodo.Enabled = True
                    End If

                    If Session.Item("CENS_MANUT_SL") > 0 Then
                        Me.CENS_MANUT_SL.value = 1

                    End If
                Else
                    Me.DrlSchede.Enabled = True
                    Me.cmbPeriodo.Enabled = True
                    Me.CENS_MANUT_SL.value = 0
                End If

                '*********************PRESENZA DI FOTO E/O PLANIMETRIE PER L'IMMOBILE***********
                If My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/CO/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.TxtCodComplesso.Text) & "*.*").Count > 0 Or My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/CO/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Trim(Me.TxtCodComplesso.Text) & "*.*").Count > 0 Then
                    Me.btnFoto.Visible = True
                    Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
                Else
                    Me.btnFoto.Visible = True
                    Me.btnFoto.ImageUrl = "~/CENSIMENTO/IMMCENSIMENTO/FotoEplan2.png"
                End If

                'Me.ImgBtnMillesimali.Enabled = False
                'Me.ImgUtenza.Enabled = False

            Catch ex As Exception
                par.OracleConn.Close()
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
            End Try

        End If

    End Sub
    Private Function EsistonoMilles() As Boolean
        'Richiamo la connessione
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        Dim trovatiMillesimali As Boolean = False
        'Richiamo la transazione
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        par.cmd.CommandText = "select * from siscom_mi.tabelle_millesimali where ID_COMPLESSO = " & vId
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

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        tabdefault1 = ""
        tabdefault2 = ""
        tabdefault3 = ""
        tabdefault4 = ""
        tabdefault5 = ""
        tabdefault6 = ""

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

        End Select
        'If Session.Item("SLE") = 1 And par.OracleConn.State = Data.ConnectionState.Open Then
        '    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        '    par.OracleConn.Close()

        '    HttpContext.Current.Session.Remove("TRANSAZIONE")
        '    HttpContext.Current.Session.Remove("CONNESSIONE")
        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '    Session.Item("LAVORAZIONE") = 0

        'End If

    End Sub



    Protected Sub btnFoto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnFoto.Click
        Response.Write("<script>window.open('FotoImmobile.aspx?T=C&ID=" & vId & "&I=" & vIdIndirizzo & "', '');</script>")

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



    '☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺PEPPE MODIFY 06/10/2010☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺☺
    Private Sub VerificaFiliale()
        Try
            If vId > 0 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                Dim LastIdEsercizio As String = ""
                par.cmd.CommandText = "SELECT MAX(ID_ESERCIZIO_FINANZIARIO) FROM SISCOM_MI.PF_MAIN"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    LastIdEsercizio = myReader(0)
                End If

                myReader.Close()
                'par.cmd.CommandText = "select lotti.*from siscom_mi.lotti_patrimonio, siscom_mi.lotti,siscom_mi.tab_filiali where lotti.id_esercizio_finanziario = " & LastIdEsercizio & " and lotti_patrimonio.id_lotto = lotti.id and lotti_patrimonio.id_complesso = " & vId & " AND lotti.id_filiale = tab_filiali.ID AND tab_filiali.id_tipo_st <> 2"
                'myReader = par.cmd.ExecuteReader
                'If myReader.Read Then
                '    Me.CmbFiliali.Enabled = False
                '    Me.txtInfoVisible.Value = 1
                '    Me.txtLottoName.Value = par.IfNull(myReader("DESCRIZIONE"), "")
                'Else
                '    Me.CmbFiliali.Enabled = True
                '    Me.txtInfoVisible.Value = 0
                'End If
                'myReader.Close()
                'If apertoOra = True Then
                '    par.OracleConn.Close()
                '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                'End If
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Me.LblErrore.Visible = True
            LblErrore.Text = "VerificaFiliale" & ex.Message
        End Try
    End Sub
End Class
