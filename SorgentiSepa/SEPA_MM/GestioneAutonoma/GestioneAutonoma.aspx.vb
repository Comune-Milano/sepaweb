
Partial Class GestioneAutonoma_GestioneAutonoma
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public tabvisibility As String = ""
    Public tabdefault1 As String = ""
    Public tabdefault2 As String = ""
    Public tabdefault3 As String = ""

    Public dtEdifici As Data.DataTable

    Public Property vIdGestAutonoma() As String
        Get
            If Not (ViewState("par_vIdGestAutonoma") Is Nothing) Then
                Return CStr(ViewState("par_vIdGestAutonoma"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdGestAutonoma") = value
        End Set

    End Property

    Public Property vIdEsercizio() As String
        Get
            If Not (ViewState("par_vIdEsercizio") Is Nothing) Then
                Return CStr(ViewState("par_vIdEsercizio"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdEsercizio") = value
        End Set

    End Property
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
    Public Property vSelezionati() As String
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
    Public Property vNumComitato() As Integer

        Get
            If Not (ViewState("par_vNumComitato") Is Nothing) Then
                Return CInt(ViewState("par_vNumComitato"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_vNumComitato") = value
        End Set

    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        If Not IsPostBack Then

            vIdConnessione = Format(Now, "yyyyMMddHHmmss")
            vIdGestAutonoma = Request.QueryString("IdAutogest")
            Session.Item("LAVORAZIONE") = 1
            Session.Add("ListaSelezionati", New ListItemCollection)
            Me.txtDataCostituzione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            'Controllo modifica campi nel form
            Dim CTRL As Control
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                End If
            Next

            'Apro la connessione che resterà valida per tutti i metodi delle sottofinestre e del salva
            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNGA" & vIdConnessione, par.OracleConn)

            RiempiCampi()

            If vIdGestAutonoma <> "" Then
                ApriRicerca(Request.QueryString("IDESERC"))
            Else
                'ESEGUO UN INSERIMENTO FITTIZZIO PER PERMETTERE IL SALVATAGGIO DI TUTTI GLI ALTRI DATI (COMITATO E RAPPRESENTANTE)
                Salva()
            End If

            If Session.Item("GA_L") = 1 Then
                FrmSoloLettura()

            End If

        End If
    End Sub


    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Try


            If Me.txtModificato.Value <> "111" Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                If SoloLettura.Value = "0" Then
                    'eseguo la rollback se sono in primo inserimento serve a eliminare i dati fittizzi
                    par.myTrans.Rollback()

                End If
                'chiudo la connessione
                par.OracleConn.Close()
                'cancello le variabili contenenti le info per le transazioni e le connessioni
                HttpContext.Current.Session.Remove("TRANSGA")
                HttpContext.Current.Session.Remove("CONNGA")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Item("LAVORAZIONE") = 0
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            Else
                txtModificato.Value = "1"

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "btnEsci_Click - " & ex.Message
        End Try

    End Sub
    Private Sub RiempiCampi()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            Dim gest As Integer = 0
            Me.cmbComplessi.Items.Clear()
            cmbComplessi.Items.Add(New ListItem(" ", -1))
            par.cmd.CommandText = "SELECT distinct COMPLESSI_IMMOBILIARI.id,COMPLESSI_IMMOBILIARI.COD_COMPLESSO,COMPLESSI_IMMOBILIARI.DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID <> 1 ORDER BY DENOMINAZIONE ASC"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbComplessi.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_COMPLESSO"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()

            Dim condizione As String = "(SISCOM_MI.PF_MAIN.ID_STATO=1 OR SISCOM_MI.PF_MAIN.ID_STATO = 0) AND"
            condizione = ""


            par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'YYYY') AS FINE FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN WHERE " & condizione & " SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO"
            myReader1 = par.cmd.ExecuteReader
            'cmbAnno.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                Me.cmbAnno.Items.Add(New ListItem(par.IfNull(myReader1("INIZIO"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            CaricaEdifici()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "RiempiCampi - " & ex.Message
        End Try
    End Sub
    Private Sub ApriRicerca(ByVal Esercizio As String, Optional ByVal UnderTransact As Boolean = False)

        Dim scriptblock As String

        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            If UnderTransact = True Then
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            End If

            vIdEsercizio = Esercizio

            'BLOCCO AUTOGESTIONE APERTA IN FOR UPDATE NOWAIT

            par.cmd.CommandText = "SELECT AUTOGESTIONI.*, AUTOGESTIONI_ESERCIZI.ID_ESERCIZIO, BOLLETTATO,PAGATO,MOR_PERCENTUALE,ABUS_PERCENTUALE FROM SISCOM_MI.AUTOGESTIONI, SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI.ID =  " & vIdGestAutonoma & " AND AUTOGESTIONI_ESERCIZI.ID = " & vIdEsercizio & " AND AUTOGESTIONI.ID = AUTOGESTIONI_ESERCIZI.ID_AUTOGESTIONE FOR UPDATE NOWAIT"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.txtDenominazione.Text = par.IfNull(myReader("DENOMINAZIONE"), "")
                Me.txtContoCorrente.Text = par.IfNull(myReader("CC_POSTALE"), "")
                Me.txtDataCostituzione.Text = par.FormattaData(par.IfNull(myReader("DATA_COSTITUZIONE"), ""))
                Me.lblBollettato.Text = Format(CDbl(par.PuntiInVirgole(par.IfNull(myReader("BOLLETTATO"), 0))), "##,##0.00")
                Me.lblPagato.Text = Format(CDbl(par.PuntiInVirgole(par.IfNull(myReader("PAGATO"), 0))), "##,##0.00")
                Me.lblPercentuale.Text = Format(CDbl(par.PuntiInVirgole(par.IfNull(myReader("MOR_PERCENTUALE"), 0))), "##,##0.00") & "%"
                Me.lblPercAbusiv.Text = Format(CDbl(par.PuntiInVirgole(par.IfNull(myReader("ABUS_PERCENTUALE"), 0))), "##,##0.00") & "%"
                DirectCast(Note1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader("NOTE"), "")
                'ricavo la descrizione dell'E.F
                par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'YYYY') as ESERCIZIO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID IN (SELECT ID_ESERCIZIO FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE ID_AUTOGESTIONE = " & vIdGestAutonoma & ")"
                Dim annoReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                cmbAnno.Items.Clear()
                While annoReader.Read
                    Me.cmbAnno.Items.Add(New ListItem(par.IfNull(annoReader("ESERCIZIO"), " "), par.IfNull(annoReader("ID"), -1)))
                End While
                annoReader.Close()
                Me.cmbAnno.SelectedValue = myReader("ID_ESERCIZIO")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT ID_EDIFICIO FROM SISCOM_MI.AUTOGESTIONI_EDIFICI WHERE ID_AUTOGESTIONE = " & vIdGestAutonoma
            Dim myReadEdifici As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReadEdifici.Read
                Me.ListEdifici.Items.FindByValue(myReadEdifici("ID_EDIFICIO")).Selected = True
            End While
            myReadEdifici.Close()
            addSelEdifici()

            Me.txtCodice.Text = Format(CDbl(vIdGestAutonoma), "00000")
            Me.btnNuovoEsercizio.Visible = True

            If UnderTransact = False Then
                'Apro una nuova transazione
                Session.Item("LAVORAZIONE") = "1"
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSGA" & vIdConnessione, par.myTrans)
            End If

            DirectCast(Me.Tab_Servizi1.FindControl("btnDelete"), ImageButton).Visible = False

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Autogestione aperta da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                ApriFrmWithDBLock()
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If
            Else
                Me.lblErrore.Visible = True
                lblErrore.Text = "ApriRicerca - " & EX1.Message
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "ApriRicerca - " & ex.Message
        End Try
    End Sub
    Private Sub ApriFrmWithDBLock()
        '*******************RICHIAMO LA CONNESSIONE*********************
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        '*******************RICHIAMO LA TRANSAZIONE*********************
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        'BLOCCO AUTOGESTIONE APERTA IN FOR UPDATE NOWAIT

        par.cmd.CommandText = "SELECT AUTOGESTIONI.*, AUTOGESTIONI_ESERCIZI.ID_ESERCIZIO, BOLLETTATO,PAGATO,MOR_PERCENTUALE,ABUS_PERCENTUALE FROM SISCOM_MI.AUTOGESTIONI, SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI.ID =  " & vIdGestAutonoma & " AND AUTOGESTIONI_ESERCIZI.ID = " & vIdEsercizio & " AND AUTOGESTIONI.ID = AUTOGESTIONI_ESERCIZI.ID_AUTOGESTIONE "
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            Me.txtDenominazione.Text = par.IfNull(myReader("DENOMINAZIONE"), "")
            Me.txtContoCorrente.Text = par.IfNull(myReader("CC_POSTALE"), "")
            Me.txtDataCostituzione.Text = par.FormattaData(par.IfNull(myReader("DATA_COSTITUZIONE"), ""))
            Me.lblBollettato.Text = Format(CDbl(par.PuntiInVirgole(par.IfNull(myReader("BOLLETTATO"), 0))), "##,##0.00")
            Me.lblPagato.Text = Format(CDbl(par.PuntiInVirgole(par.IfNull(myReader("PAGATO"), 0))), "##,##0.00")
            Me.lblPercentuale.Text = Format(CDbl(par.PuntiInVirgole(par.IfNull(myReader("MOR_PERCENTUALE"), 0))), "##,##0.00") & "%"
            Me.lblPercAbusiv.Text = Format(CDbl(par.PuntiInVirgole(par.IfNull(myReader("ABUS_PERCENTUALE"), 0))), "##,##0.00") & "%"
            DirectCast(Note1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader("NOTE"), "")
            'ricavo la descrizione dell'E.F
            par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'YYYY') as ESERCIZIO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID IN (SELECT ID_ESERCIZIO FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE ID_AUTOGESTIONE = " & vIdGestAutonoma & ")"
            Dim annoReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            cmbAnno.Items.Clear()
            While annoReader.Read
                Me.cmbAnno.Items.Add(New ListItem(par.IfNull(annoReader("ESERCIZIO"), " "), par.IfNull(annoReader("ID"), -1)))
            End While
            annoReader.Close()
            Me.cmbAnno.SelectedValue = myReader("ID_ESERCIZIO")
        End If
        myReader.Close()

        par.cmd.CommandText = "SELECT ID_EDIFICIO FROM SISCOM_MI.AUTOGESTIONI_EDIFICI WHERE ID_AUTOGESTIONE = " & vIdGestAutonoma
        Dim myReadEdifici As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReadEdifici.Read
            Me.ListEdifici.Items.FindByValue(myReadEdifici("ID_EDIFICIO")).Selected = True
        End While
        myReadEdifici.Close()
        addSelEdifici()

        Me.txtCodice.Text = Format(CDbl(vIdGestAutonoma), "00000")
        Me.btnNuovoEsercizio.Visible = False

        Me.btnSalva.Visible = False
        Me.btnNuovoEsercizio.Visible = False
        Me.cmbAnno.Enabled = False
        Me.txtDenominazione.Enabled = False
        Me.txtContoCorrente.Enabled = False
        Me.SoloLettura.Value = 1
        DirectCast(Me.Tab_Servizi1.FindControl("btnDelete"), ImageButton).Visible = False
        FrmSoloLettura()

    End Sub
    Private Sub CaricaEdifici()
        Try


            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            If Me.cmbComplessi.SelectedValue = "-1" Then
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID and COMPLESSI_IMMOBILIARI.ID <> 1 order by denominazione asc"
            Else
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione, EDIFICI.ID_COMPLESSO FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID and COMPLESSI_IMMOBILIARI.ID = " & Me.cmbComplessi.SelectedValue.ToString & " order by denominazione asc"

            End If
            Me.ListEdifici.Items.Clear()
            dtEdifici = New Data.DataTable
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dtEdifici)
            For Each row As Data.DataRow In dtEdifici.Rows
                ListEdifici.Items.Add(New ListItem(par.IfNull(row.Item("denominazione"), " ") & "- -" & "cod." & par.IfNull(row.Item("COD_EDIFICIO"), " "), par.IfNull(row.Item("id"), -1)))
            Next


            For Each i As ListItem In DirectCast(Session.Item("ListaSelezionati"), ListItemCollection)
                If Not IsNothing(ListEdifici.Items.FindByValue(i.Value)) Then
                    ListEdifici.Items.FindByValue(i.Value).Selected = True
                End If
            Next


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CaricaEdifici - " & ex.Message
        End Try
    End Sub


    Private Function CalcolaMorosità() As Boolean

        CalcolaMorosità = False
        Try
            If vSelezionati <> "" Then

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                'Me.txtDataDecorrenza.Text = ""
                Dim s As String = ""

                If vSelezionati <> "" Then
                    par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO)AS BOLLETTATO FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE " _
                        & " WHERE BOL_BOLLETTE.ID_EDIFICIO IN  (" & vSelezionati & ") AND (SISCOM_MI.GETSTATOCONTRATTO(BOL_BOLLETTE.ID_CONTRATTO) = 'IN CORSO' OR SISCOM_MI.GETSTATOCONTRATTO(ID_CONTRATTO)='IN CORSO (S.T.)')  AND BOL_BOLLETTE.FL_ANNULLATA = 0 AND BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA"
                End If


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.lblBollettato.Text = Format(par.IfNull(myReader1(0), 0), "##,##0.00")
                End If
                myReader1.Close()
                If vSelezionati <> "" Then
                    par.cmd.CommandText = "SELECT SUM(IMPORTO_PAGATO)AS PAGATO FROM SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE.ID_EDIFICIO IN  (" & vSelezionati & ") AND FL_ANNULLATA = 0 AND (SISCOM_MI.GETSTATOCONTRATTO(ID_CONTRATTO) = 'IN CORSO' OR SISCOM_MI.GETSTATOCONTRATTO(ID_CONTRATTO)='IN CORSO (S.T.)') "
                End If
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.lblPagato.Text = Format(par.IfNull(myReader1(0), 0), "##,##0.00")
                End If
                myReader1.Close()

                If par.IfEmpty(Me.lblBollettato.Text, 0) <> 0 Then
                    Me.lblPercentuale.Text = Format(((lblBollettato.Text - lblPagato.Text) * 100) / Me.lblBollettato.Text, "##,##0.00") & "%"
                Else
                    Me.lblPercentuale.Text = "0,00%"
                End If

                ' '' ''PERCENTUALE(ABUSIVISMO)
                If vSelezionati <> "" Then
                    par.cmd.CommandText = "SELECT COUNT(UNITA_IMMOBILIARI.ID) AS NUM_ALLOGGI FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND EDIFICI.ID IN  (" & vSelezionati & ")"
                End If
                myReader1 = par.cmd.ExecuteReader()
                Dim TotUnitaImmob As Double
                If myReader1.Read Then
                    TotUnitaImmob = myReader1(0)
                End If
                myReader1.Close()
                If vSelezionati <> "" Then
                    par.cmd.CommandText = "SELECT COUNT(ID_UNITA) AS ABUSIVI FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI, SISCOM_MI.RAPPORTI_UTENZA WHERE EDIFICI.ID= UNITA_CONTRATTUALE.ID_EDIFICIO AND RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC = 'NONE' AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID IN  (" & vSelezionati & ")"
                End If
                myReader1 = par.cmd.ExecuteReader()
                Dim OccAbus As Double
                If myReader1.Read Then
                    OccAbus = myReader1(0)
                End If
                myReader1.Close()
                If TotUnitaImmob > 0 Then
                    Me.lblPercAbusiv.Text = Format((OccAbus * 100) / TotUnitaImmob, "##,##0.00") & "%"
                Else
                    Me.lblPercAbusiv.Text = "0,00%"
                End If

                'If vSelezionati <> "" Then
                '    par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE WHERE (SISCOM_MI.GETSTATOCONTRATTO(SISCOM_MI.RAPPORTI_UTENZA.ID)='IN CORSO' OR SISCOM_MI.GETSTATOCONTRATTO(SISCOM_MI.RAPPORTI_UTENZA.ID)='IN CORSO (S.T.)') AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID IN  (" & vSelezionati & ")"
                'End If
                'myReader1 = par.cmd.ExecuteReader()
                'If myReader1.Read Then
                '    DirectCast(Me.TabModelliBC1.FindControl("txtInquilini"), TextBox).Text = par.IfEmpty(myReader1(0), 0)
                'End If
                'myReader1.Close()


                If Me.lblPercentuale.Text.Replace("%", "") > 15 OrElse Me.lblPercAbusiv.Text.Replace("%", "") > 10 Then
                    Response.Write("<script>alert('ATTENZIONE!La morosità e/o l\'abusivismo percentuale dell\'immobile  non consentono la costituzione dell\'Autogestione');</script>")
                    CalcolaMorosità = True
                End If
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "Function CalcolaMorosità - " & ex.Message
        End Try

    End Function


    Private Sub addSelEdifici()
        Try
            Dim COLORE As String = "#E6E6E6"
            lblEdifici.Text = ""
            Dim testoTabella As String
            'Me.cmbEdifScelti.Items.Clear()
            Dim ListaSelezionati As New ListItemCollection

            If Selezionati(ListEdifici) = True Then
                testoTabella = ""
                testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf
                Dim I As Integer
                For I = 0 To Me.ListEdifici.Items.Count() - 1
                    If Me.ListEdifici.Items(I).Selected = True Then
                        testoTabella = testoTabella _
                            & "<tr bgcolor = '" & COLORE & "'>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & ListEdifici.Items(I).Text & "</a></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "</td>" _
                            & "</tr>"

                        ListaSelezionati.Add(ListEdifici.Items(I).Value.ToString)

                        If COLORE = "#FFFFFF" Then
                            COLORE = "#E6E6E6"
                        Else
                            COLORE = "#FFFFFF"
                        End If

                    End If
                Next
                lblEdifici.Text = testoTabella & "</table>"
                Session("ListaSelezionati") = ListaSelezionati

            End If

            Me.ScegEdifVis.Value = 1

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "addSelEdifici - " & ex.Message
        End Try

    End Sub

    Public Function Selezionati(ByVal lista As CheckBoxList) As Boolean
        Selezionati = False
        Try
            Selezionati = False
            vSelezionati = ""
            For Each I As ListItem In lista.Items
                If I.Selected = True Then
                    Selezionati = True
                    vSelezionati = vSelezionati & I.Value & ","
                End If
            Next
            If vSelezionati <> "" Then
                vSelezionati = vSelezionati.Substring(0, vSelezionati.LastIndexOf(","))

            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function

    Protected Sub imgAggiornaEdifici_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAggiornaEdifici.Click
        addSelEdifici()
        CalcolaMorosità()
        Me.Tab_Dettaglio1.CaricaElencoInquilini()
    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        'Response.Write("<script>Il modulo è in fase di sviluppo, non è ancora possibile eseguire questa operazione!</script>")
        If vIdGestAutonoma = "" Then
            Salva()
        Else
            Update()
        End If
        txtModificato.Value = 0
    End Sub

    Protected Sub cmbComplessi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplessi.SelectedIndexChanged
        CaricaEdifici()
        Me.ScegEdifVis.Value = 2
    End Sub

    Private Sub Salva()
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'Apro una nuova transazione
            Session.Item("LAVORAZIONE") = "1"
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSGA" & vIdConnessione, par.myTrans)
            ‘‘par.cmd.Transaction = par.myTrans
            If Me.cmbAnno.Items.Count > 0 Then

                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_AUTOGESTIONI.NEXTVAL FROM DUAL"
                Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If Lettore.Read Then
                    vIdGestAutonoma = Lettore(0)
                End If
                Lettore.Close()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI ( ID, DATA_COSTITUZIONE, DENOMINAZIONE,CC_POSTALE ) VALUES " _
                                    & "(" & vIdGestAutonoma & ", '" & par.AggiustaData(Me.txtDataCostituzione.Text) & "','" & Me.txtDenominazione.Text & "', '" & Me.txtContoCorrente.Text & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_AUTOGESTIONI_ESERCIZI.NEXTVAL FROM DUAL"
                Lettore = par.cmd.ExecuteReader()
                If Lettore.Read Then
                    vIdEsercizio = Lettore(0)
                End If
                Lettore.Close()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_ESERCIZI ( ID, ID_AUTOGESTIONE, ID_ESERCIZIO) VALUES " _
                    & "(" & vIdEsercizio & "," & vIdGestAutonoma & " ," & Me.cmbAnno.SelectedValue & ")"
                par.cmd.ExecuteNonQuery()

                '**********CHIAMO LA SCRITTURA DELL'EVENTO 
                WriteEvent("F55", "NUOVA AUTOGESTIONE")
            Else
                Response.Write("<script>alert('Non esiste un esercizio finanziario al quale associare la Gestione Autonoma!');</script>")
                Me.btnSalva.Visible = False
                Exit Sub
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "Sub Salva - " & ex.Message
        End Try
    End Sub

    Private Sub Update()
        Try
            If Me.txtConferma.Value = 1 Then

                If Me.vSelezionati <> "" Then
                    If vNumComitato < 3 Then
                        Response.Write("<script>alert('Il comitato deve essere composto da almeno tre persone!');</script>")
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(par.PulisciStrSql(DirectCast(Tab_Dettaglio1.FindControl("txtCognome"), TextBox).Text)) = True Or _
                        String.IsNullOrEmpty(par.PulisciStrSql(DirectCast(Tab_Dettaglio1.FindControl("txtnome"), TextBox).Text)) = True Or _
                        String.IsNullOrEmpty(par.PulisciStrSql(DirectCast(Tab_Dettaglio1.FindControl("txtrecapito"), TextBox).Text)) = True Then
                        Response.Write("<script>alert('Definire i dati del rappresentante!Operazione interrotta!');</script>")
                        Exit Sub

                    End If

                    If DirectCast(Tab_Servizi1.FindControl("DataGridServizi"), DataGrid).Items.Count <= 0 Then
                        Response.Write("<script>alert('Definire i SERVIZI approvati per la gestione autonoma dell\'anno " & Me.cmbAnno.SelectedItem.Text & "!Operazione interrotta!');</script>")
                        Exit Sub
                    End If

                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)

                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "UPDATE SISCOM_MI.AUTOGESTIONI SET DATA_COSTITUZIONE= '" & par.AggiustaData(par.IfNull(Me.txtDataCostituzione.Text, "")) & "'," _
                        & "DENOMINAZIONE='" & par.PulisciStrSql(Me.txtDenominazione.Text.ToUpper) & "', CC_POSTALE='" & par.PulisciStrSql(Me.txtContoCorrente.Text.ToUpper) & "',NOTE='" & par.PulisciStrSql(DirectCast(Note1.FindControl("txtNote"), TextBox).Text.ToUpper) & "' WHERE ID = " & vIdGestAutonoma
                    par.cmd.ExecuteNonQuery()


                    'AGGIORNO I DATI DI AUTOGESTIONI_ESERCIZI

                    par.cmd.CommandText = "UPDATE SISCOM_MI.AUTOGESTIONI_ESERCIZI SET ID_ESERCIZIO = '" & Me.cmbAnno.SelectedValue.ToString & "'," _
                                            & " RAPP_COGNOME ='" & par.PulisciStrSql(DirectCast(Tab_Dettaglio1.FindControl("txtCognome"), TextBox).Text.ToUpper) & "'," _
                                            & " RAPP_NOME= '" & par.PulisciStrSql(DirectCast(Tab_Dettaglio1.FindControl("txtnome"), TextBox).Text.ToUpper) & "'," _
                                            & " RAPP_CF= '" & par.PulisciStrSql(DirectCast(Tab_Dettaglio1.FindControl("txtcf"), TextBox).Text.ToUpper) & "'," _
                                            & " RAPP_RECAPITO ='" & par.PulisciStrSql(DirectCast(Tab_Dettaglio1.FindControl("txtrecapito"), TextBox).Text.ToUpper) & "'," _
                                            & " BOLLETTATO = " & par.VirgoleInPunti(Me.lblBollettato.Text) & ", PAGATO = " & par.VirgoleInPunti(Me.lblPagato.Text.Replace("%", "")) & "," _
                                            & " MOR_PERCENTUALE= " & par.VirgoleInPunti(Me.lblPercentuale.Text.Replace("%", "")) & ",ABUS_PERCENTUALE= " & par.VirgoleInPunti(Me.lblPercAbusiv.Text.Replace("%", "")) _
                                            & " WHERE ID_AUTOGESTIONE = " & vIdGestAutonoma & " AND ID = " & vIdEsercizio
                    par.cmd.ExecuteNonQuery()



                    'CANCELLO I VECCHI EDIFICI, E PROCEDO CON L'INSERMENTO DEI NUOVI
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.AUTOGESTIONI_EDIFICI WHERE ID_AUTOGESTIONE = " & vIdGestAutonoma
                    par.cmd.ExecuteNonQuery()
                    'CICLO CHE ESEGUE LO SPLIT SULLA STRINGA CONTENENTE (IDEDIFICIO1,IDEDIFICIO2,IDEDIFICIO3...IDEDIFCION)
                    Dim voci() As String
                    voci = vSelezionati.Split(New Char() {","})
                    For Each stringa As String In voci
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_EDIFICI (ID_AUTOGESTIONE, ID_EDIFICIO) VALUES " _
                            & "(" & vIdGestAutonoma & ", " & stringa & ")"
                        par.cmd.ExecuteNonQuery()
                    Next

                    '**********CHIAMO LA SCRITTURA DELL'EVENTO 
                    WriteEvent("F02")

                    par.myTrans.Commit()

                    DirectCast(Me.Tab_Servizi1.FindControl("btnDelete"), ImageButton).Visible = False

                    Me.txtCodice.Text = Format(CDbl(vIdGestAutonoma), "00000")
                    Me.btnNuovoEsercizio.Visible = True
                    Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")


                    'Apro una nuova transazione
                    Session.Item("LAVORAZIONE") = "1"
                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSGA" & vIdConnessione, par.myTrans)

                    Me.txtNuovoEs.Value = 0

                Else
                    Response.Write("<script>alert('Selezionare almeno un edificio per salvare!');</script>")
                End If

            End If


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "Sub Update - " & ex.Message
        End Try
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        tabdefault1 = ""
        tabdefault2 = ""
        tabdefault3 = ""
            Select txttab.Value
            Case "1"
                tabdefault1 = "tabbertabdefault"
            Case "2"
                tabdefault2 = "tabbertabdefault"
            Case "3"
                tabdefault3 = "tabbertabdefault"
        End Select

    End Sub

    Protected Sub btnIndietro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnIndietro.Click
        Try


            If Me.txtModificato.Value <> "111" Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                'eseguo la rollback se sono in primo inserimento serve a eliminare i dati fittizzi
                par.myTrans.Rollback()
                'chiudo la connessione
                par.OracleConn.Close()
                'cancello le variabili contenenti le info per le transazioni e le connessioni
                HttpContext.Current.Session.Remove("TRANSGA")
                HttpContext.Current.Session.Remove("CONNGA")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Item("LAVORAZIONE") = 0
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            Else
                txtModificato.Value = "1"

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "btnEsci_Click - " & ex.Message
        End Try

    End Sub

    Protected Sub cmbAnno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAnno.SelectedIndexChanged

        If Me.cmbAnno.SelectedValue <> vIdEsercizio AndAlso par.IfEmpty(Me.txtCodice.Text, "") <> "" Then
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            ''eseguo la rollback se sono in primo inserimento serve a eliminare i dati fittizzi
            'par.myTrans.Rollback()

            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE ID_ESERCIZIO = " & Me.cmbAnno.SelectedValue & " AND ID_AUTOGESTIONE = " & vIdGestAutonoma
            Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If Lettore.Read Then
                vIdEsercizio = Lettore(0)
            End If
            Lettore.Close()

            ApriRicerca(vIdEsercizio, True)
            Tab_Dettaglio1.CaricaDati()
            Tab_Servizi1.CaricaServizi()
            Tab_Servizi1.CaricaDati()
            Me.txtNuovoEs.Value = 0

        Else
            CalcolaMorosità()
        End If

    End Sub

    Protected Sub btnNuovoEsercizio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNuovoEsercizio.Click

        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            'salvo le eventuali modifiche apportate all'autogestione prima di ricopiarla per un nuovo esercizio
            par.myTrans.Commit()


            'Apro una nuova transazione
            Session.Item("LAVORAZIONE") = "1"
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSGA" & vIdConnessione, par.myTrans)
            ‘‘par.cmd.Transaction = par.myTrans

            If ControllaSeEsiste() = True Then
                Response.Write("<script>alert('Esiste già la stessa autogestione per l\'esercizio successivo a quello selezionato!!');</script>")
                Exit Sub
            End If


            Me.txtNuovoEs.Value = 1

            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_AUTOGESTIONI_ESERCIZI.NEXTVAL FROM DUAL"
            Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If Lettore.Read Then
                vIdEsercizio = Lettore(0)
            End If
            Lettore.Close()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_ESERCIZI ( ID, ID_AUTOGESTIONE, ID_ESERCIZIO) VALUES " _
                & "(" & vIdEsercizio & "," & vIdGestAutonoma & " ," & Me.cmbAnno.SelectedValue + 1 & ")"
            par.cmd.ExecuteNonQuery()

            '**********CHIAMO LA SCRITTURA DELL'EVENTO DI MODIFICA
            WriteEvent("F02", "FUNZIONE NUOVO ESERCIZIO")

            DirectCast(Me.Tab_Servizi1.FindControl("btnDelete"), ImageButton).Visible = True

            ApriRicerca(vIdEsercizio, True)
            Tab_Dettaglio1.CaricaElencoInquilini()
            Tab_Dettaglio1.CaricaDati()
            Tab_Servizi1.CaricaServizi()
            Tab_Servizi1.CaricaDati()
            CalcolaMorosità()
            Me.btnNuovoEsercizio.Visible = False
            txtModificato.Value = 1
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "btnNuovoEsercizio - " & ex.Message
        End Try

    End Sub
    Private Function ControllaSeEsiste() As Boolean
        ControllaSeEsiste = False
        '*******************RICHIAMO LA CONNESSIONE*********************
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        '*******************RICHIAMO LA TRANSAZIONE*********************
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE ID_ESERCIZIO = " & Me.cmbAnno.SelectedValue + 1 & " AND ID_AUTOGESTIONE = " & Me.vIdGestAutonoma
        Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If Lettore.Read Then
            ControllaSeEsiste = True
        End If


    End Function
    Public Sub WriteEvent(ByVal CodEvento As String, Optional ByVal Motivazione As String = "")
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_AUTOGESTIONE (ID_AUTOGESTIONE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vIdGestAutonoma & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', '" & CodEvento & "','" & Motivazione & "')"
            par.cmd.ExecuteNonQuery()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "WriteEvent - " & ex.Message
        End Try

    End Sub
    Private Sub FrmSoloLettura()
        Me.btnNuovoEsercizio.Visible = False
        Me.btnSalva.Visible = False
        Me.btnNuovoEsercizio.Visible = False
        Me.cmbAnno.Enabled = False
        Me.txtDenominazione.Enabled = False
        Me.txtContoCorrente.Enabled = False
        Me.txtDataCostituzione.Enabled = False
        Me.SoloLettura.Value = 1
        DirectCast(Me.Note1.FindControl("txtNote"), TextBox).Enabled = False
        'DirectCast(Tab_Dettaglio1.FindControl("txtCognome"), TextBox).Visible = False
        'DirectCast(Tab_Dettaglio1.FindControl("txtnome"), TextBox).Visible = False
        'DirectCast(Tab_Dettaglio1.FindControl("txtcf"), TextBox).Visible = False
        'DirectCast(Tab_Dettaglio1.FindControl("txtrecapito"), TextBox).Visible = False
    End Sub

    
End Class
