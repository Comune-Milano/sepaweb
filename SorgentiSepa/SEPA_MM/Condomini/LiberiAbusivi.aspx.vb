
Partial Class Condomini_LiberiAbusivi
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public Property vIdConnModale() As String
        Get
            If Not (ViewState("par_vIdConnModale") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnModale"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnModale") = value
        End Set

    End Property
    Public Property vIdGestione() As String
        Get
            If Not (ViewState("par_vIdGestione") Is Nothing) Then
                Return CStr(ViewState("par_vIdGestione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdGestione") = value
        End Set

    End Property
    Public Property vChiama() As String
        Get
            If Not (ViewState("par_vChiama") Is Nothing) Then
                Return CStr(ViewState("par_vChiama"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vChiama") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
            Response.Cache.SetLastModified(DateTime.Now)
            Response.Cache.SetAllowResponseInBrowserHistory(False)

            If Not IsPostBack Then
                If Session.Item("OPERATORE") = "" Then
                    Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
                End If
                If Request.QueryString("IDCON") <> "" Then
                    vIdConnModale = Request.QueryString("IDCON")
                End If
                If Request.QueryString("IDGEST") <> "" Then
                    vIdGestione = Request.QueryString("IDGEST")
                End If

                'Me.Session.Add("MODIFYMODAL", 0)

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "SELECT GESTIONE_INIZIO, GESTIONE_FINE, DENOMINAZIONE FROM SISCOM_MI.CONDOMINI WHERE ID = " & Request.QueryString("IDCONDOMINIO")
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.lblTitolo.Text = "Liberi Condominio: " & myReader1("DENOMINAZIONE")
                End If
                myReader1.Close()



                'CREO SUL DATABASE UN PALETTO PER IL ROLBACK FINO A QUI
                par.cmd.CommandText = "SAVEPOINT LIBABUS"
                par.cmd.ExecuteNonQuery()

                vChiama = Request.QueryString("CHIAMA")

                Select Case Request.QueryString("CHIAMA")
                    Case "P"
                        CercaDaPreventivi()
                    Case "C"
                        CercaDaConsuntivi()
                End Select
                ElencoInquilini()
                'AddJavascriptFunction()
                ''''''''******Se il form principale è in sola lettura anche i form chiamati vengono resi ReadOnly
                If Request.QueryString("IDVISUAL") = "0" And Not String.IsNullOrEmpty(Request.QueryString("IDVISUAL")) Then
                    Me.btnSalvaCambioAmm.Visible = False
                    Me.btnDelete.Enabled = False
                    SettaFrmReadOnly()
                End If
                ''''''''******FINE******
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub SettaFrmReadOnly()
        Try

            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridLiberiAbus.Items.Count - 1
                di = Me.DataGridLiberiAbus.Items(i)
                DirectCast(di.Cells(1).FindControl("txtTOT"), TextBox).Enabled = False

            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub CercaDaPreventivi()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            par.cmd.CommandText = "SELECT ID_GESTIONE, POSIZIONE_BILANCIO,ID_UI, STATO, trim(TO_CHAR(IMPORTO,'9G999G999G990D99')) AS IMPORTO FROM SISCOM_MI.COND_UI_GESTIONE_P WHERE ID_GESTIONE =" & vIdGestione
            '            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID_EDIFICIO,ID_CONTRATTO, UNITA_IMMOBILIARI.ID AS ID_UI, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,COND_UI.POSIZIONE_BILANCIO,UNITA_CONTRATTUALE.ID_CONTRATTO,(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO'  ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO,siscom_mi.Getintestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,1) AS INTESTATARIO,(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO ,COND_UI_GESTIONE_P.IMPORTO FROM SISCOM_MI.COND_UI_GESTIONE_P,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.ANAGRAFICA, SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID AND SCALE_EDIFICI.ID= UNITA_IMMOBILIARI.ID_SCALA AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND COND_UI.ID_UI= COND_UI_GESTIONE_P.ID_UI AND SISCOM_MI.COND_UI_GESTIONE_P.ID_GESTIONE = " & vIdGestione & " AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) AND COND_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID AND COD_TIPO_DISPONIBILITA <> 'VEND' AND (SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)='IN CORSO ABUSIVO' OR UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL ) AND COND_UI.ID_INTESTARIO=ANAGRAFICA.ID(+) AND (cond_ui.id_condominio= " & Request.QueryString("IDCONDOMINIO") & ") AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO =  " & Request.QueryString("IDCONDOMINIO") & ") ORDER BY POSIZIONE_BILANCIO ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()

            da.Fill(dt)
            DataGridLiberiAbus.DataSource = dt
            DataGridLiberiAbus.DataBind()
            AddJavascriptFunction()
            'SommaRighe()
            SommaColonne()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub CercaDaConsuntivi()
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            par.cmd.CommandText = "SELECT ID_GESTIONE, POSIZIONE_BILANCIO,ID_UI, STATO, trim(TO_CHAR(IMPORTO,'9G999G999G990D99')) AS IMPORTO FROM SISCOM_MI.COND_UI_GESTIONE_C WHERE ID_GESTIONE =" & vIdGestione
            '            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID_EDIFICIO,ID_CONTRATTO, UNITA_IMMOBILIARI.ID AS ID_UI, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,COND_UI.POSIZIONE_BILANCIO,UNITA_CONTRATTUALE.ID_CONTRATTO,(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO'  ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO,siscom_mi.Getintestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,1) AS INTESTATARIO,(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO ,COND_UI_GESTIONE_P.IMPORTO FROM SISCOM_MI.COND_UI_GESTIONE_P,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.ANAGRAFICA, SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID AND SCALE_EDIFICI.ID= UNITA_IMMOBILIARI.ID_SCALA AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND COND_UI.ID_UI= COND_UI_GESTIONE_P.ID_UI AND SISCOM_MI.COND_UI_GESTIONE_P.ID_GESTIONE = " & vIdGestione & " AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) AND COND_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID AND COD_TIPO_DISPONIBILITA <> 'VEND' AND (SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)='IN CORSO ABUSIVO' OR UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL ) AND COND_UI.ID_INTESTARIO=ANAGRAFICA.ID(+) AND (cond_ui.id_condominio= " & Request.QueryString("IDCONDOMINIO") & ") AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO =  " & Request.QueryString("IDCONDOMINIO") & ") ORDER BY POSIZIONE_BILANCIO ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()

            da.Fill(dt)
            DataGridLiberiAbus.DataSource = dt
            DataGridLiberiAbus.DataBind()
            AddJavascriptFunction()
            'SommaRighe()
            SommaColonne()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub


    Protected Sub btnSalvaCambioAmm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaCambioAmm.Click
        Try
            Dim i As Integer = 0
            Dim di As DataGridItem
            Dim Importo As String = ""

            '*CONTROLLO NON VI SIANO IMPORTI VUOTI, IN TAL CASO NON ESEGUO IL SALVATAGGIO
            For i = 0 To Me.DataGridLiberiAbus.Items.Count - 1
                di = Me.DataGridLiberiAbus.Items(i)
                Importo = DirectCast(di.Cells(1).FindControl("txtTOT"), TextBox).Text
                If String.IsNullOrEmpty(Importo) Then
                    Response.Write("<script>alert('L\'importo è obbligatorio per tutti gli immobili scelti!\nUno o più importi risultano vuoti!\Avvalorare i campi per completare l\'operazione!');</script>")
                    Exit Sub
                End If
            Next
            i = 0
            Select Case Request.QueryString("CHIAMA")
                Case "P"
                    SalvaPreventivi()
                Case "C"
                    SalvaConsuntivi()
            End Select
            SommaColonne()
            Session.Item("MODIFYMODAL") = 1
            Me.txtSalvato.value = 1
            'SommaRighe()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub SalvaPreventivi()
        Try
            If vIdGestione <> "" Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                'par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_UI_GESTIONE_P WHERE ID_GESTIONE = " & vIdGestione
                'par.cmd.ExecuteNonQuery()

                Dim i As Integer = 0
                Dim di As DataGridItem
                Dim Importo As String = ""
                For i = 0 To Me.DataGridLiberiAbus.Items.Count - 1
                    di = Me.DataGridLiberiAbus.Items(i)
                    Importo = DirectCast(di.Cells(1).FindControl("txtTOT"), TextBox).Text
                    If Not String.IsNullOrEmpty(Importo) Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_UI_GESTIONE_P SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Importo.Replace(".", "")), "Null") & " WHERE ID_GESTIONE = " & vIdGestione & " AND ID_UI = " & Me.DataGridLiberiAbus.Items(i).Cells(0).Text
                        par.cmd.ExecuteNonQuery()
                    Else
                        Response.Write("<script>alert('L\'importo è obbligatorio per tutti gli immobili scelti!\nUno o più importi risultano vuoti!\Avvalorare i campi per completare l\'operazione!');</script>")
                        Exit Sub
                    End If
                Next
            End If
            '****************MYEVENT*****************
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','MODIFICA ELENCO LIBERI/ABUSIVI PREVENTIVO SUL CONDOMINIO')"
            par.cmd.ExecuteNonQuery()

            Session.Item("MODIFYMODAL") = 1

            Response.Write("<script>alert('Operazone eseguita correttamente!');</script>")
            'Response.Write("<script>window.close();</script>")
            Me.txtModificato.Value = 0

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub SalvaConsuntivi()
        Try

            If vIdGestione <> "" Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                'par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_UI_GESTIONE_P WHERE ID_GESTIONE = " & vIdGestione
                'par.cmd.ExecuteNonQuery()

                Dim i As Integer = 0
                Dim di As DataGridItem
                Dim Importo As String = ""

                For i = 0 To Me.DataGridLiberiAbus.Items.Count - 1
                    di = Me.DataGridLiberiAbus.Items(i)
                    Importo = DirectCast(di.Cells(1).FindControl("txtTOT"), TextBox).Text
                    If Not String.IsNullOrEmpty(Importo) Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_UI_GESTIONE_C SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Importo.Replace(".", "")), "Null") & " WHERE ID_GESTIONE = " & vIdGestione & " AND ID_UI = " & Me.DataGridLiberiAbus.Items(i).Cells(0).Text
                        par.cmd.ExecuteNonQuery()
                    Else
                        Response.Write("<script>alert('L\'importo è obbligatorio per tutti gli immobili scelti!\nUno o più importi risultano vuoti!\Avvalorare i campi per completare l\'operazione!');</script>")
                        Exit Sub
                    End If
                Next
            End If
            '****************MYEVENT*****************
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','MODIFICA ELENCO LIBERI/ABUSIVI CONSUNTIVI SUL CONDOMINIO')"
            par.cmd.ExecuteNonQuery()

            Session.Item("MODIFYMODAL") = 1

            Response.Write("<script>alert('Operazone eseguita correttamente!');</script>")
            'Response.Write("<script>window.close();</script>")
            Me.txtModificato.Value = 0

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function
    Private Sub ElencoInquilini()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            If Request.QueryString("CHIAMA") = "P" Then
                'par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID_EDIFICIO,ID_CONTRATTO, UNITA_IMMOBILIARI.ID AS ID_UI, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,COND_UI.POSIZIONE_BILANCIO,UNITA_CONTRATTUALE.ID_CONTRATTO,TIPO_DISPONIBILITA.DESCRIZIONE AS STATO,siscom_mi.Getintestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,1) AS INTESTATARIO,(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.ANAGRAFICA, SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPO_DISPONIBILITA  WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID AND SCALE_EDIFICI.ID= UNITA_IMMOBILIARI.ID_SCALA AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) AND COND_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID AND COD_TIPO_DISPONIBILITA <> 'VEND' AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD AND TIPO_DISPONIBILITA.COD<>'OCCU' AND COND_UI.ID_INTESTARIO=ANAGRAFICA.ID(+) AND (cond_ui.id_condominio= " & Request.QueryString("IDCONDOMINIO") & ") AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO =  " & Request.QueryString("IDCONDOMINIO") & ") AND ID_UI NOT IN (SELECT ID_UI FROM SISCOM_MI.COND_UI_GESTIONE_P WHERE ID_GESTIONE = " & vIdGestione & " ) ORDER BY POSIZIONE_BILANCIO ASC"
                par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.DATA_DECORRENZA, UNITA_IMMOBILIARI.ID_EDIFICIO,ID_CONTRATTO, " _
                                    & "UNITA_IMMOBILIARI.ID AS ID_UI,RAPPORTI_UTENZA.COD_CONTRATTO, " _
                                    & "UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE , " _
                                    & "TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA, " _
                                    & "POSIZIONE_BILANCIO, " _
                                    & "TIPO_DISPONIBILITA.DESCRIZIONE AS STATO_UI," _
                                    & "(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN ''  " _
                                    & "ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO,(CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) END) AS stato_dt_select," _
                                    & "siscom_mi.Getintestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,0) AS INTESTATARIO," _
                                    & "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  " _
                                    & "	  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO," _
                                    & "RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC,RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR " _
                                    & "FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & "SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE," _
                                    & "SISCOM_MI.ANAGRAFICA, SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI," _
                                    & "SISCOM_MI.TIPO_DISPONIBILITA, SISCOM_MI.RAPPORTI_UTENZA " _
                                    & "WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD " _
                                    & "AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID " _
                                    & "AND SCALE_EDIFICI.ID(+)= UNITA_IMMOBILIARI.ID_SCALA " _
                                    & "AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & "AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) " _
                                    & "AND COND_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID " _
                                    & "AND COD_TIPO_DISPONIBILITA <> 'VEND' " _
                                    & "AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD " _
                                    & "AND COND_UI.ID_INTESTARIO=ANAGRAFICA.ID(+) " _
                                    & "AND (cond_ui.id_condominio=  " & Request.QueryString("IDCONDOMINIO") & ") AND EDIFICI.ID " _
                                    & "IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO =   " & Request.QueryString("IDCONDOMINIO") & ") " _
                                    & "AND ID_UI NOT IN (SELECT ID_UI FROM SISCOM_MI.COND_UI_GESTIONE_P WHERE ID_GESTIONE = " & vIdGestione & ") " _
                                    & "AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID(+)  " _
                                    & "ORDER BY POSIZIONE_BILANCIO  ASC,id_ui ASC,DATA_DECORRENZA DESC"
                ''☺♫☼ Puccia 08/01/2013 AND COD_TIPO_DISPONIBILITA = 'LIBE' 
            Else
                'par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID_EDIFICIO,ID_CONTRATTO, UNITA_IMMOBILIARI.ID AS ID_UI, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,COND_UI.POSIZIONE_BILANCIO,UNITA_CONTRATTUALE.ID_CONTRATTO,TIPO_DISPONIBILITA.DESCRIZIONE AS STATO,siscom_mi.Getintestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,1) AS INTESTATARIO,(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.ANAGRAFICA, SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPO_DISPONIBILITA  WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID AND SCALE_EDIFICI.ID= UNITA_IMMOBILIARI.ID_SCALA AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) AND COND_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID AND COD_TIPO_DISPONIBILITA <> 'VEND' AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD AND TIPO_DISPONIBILITA.COD<>'OCCU' AND COND_UI.ID_INTESTARIO=ANAGRAFICA.ID(+) AND (cond_ui.id_condominio= " & Request.QueryString("IDCONDOMINIO") & ") AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO =  " & Request.QueryString("IDCONDOMINIO") & ") AND ID_UI NOT IN (SELECT ID_UI FROM SISCOM_MI.COND_UI_GESTIONE_C WHERE ID_GESTIONE = " & vIdGestione & " ) ORDER BY POSIZIONE_BILANCIO ASC"
                par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.DATA_DECORRENZA, UNITA_IMMOBILIARI.ID_EDIFICIO,ID_CONTRATTO, " _
                                    & "UNITA_IMMOBILIARI.ID AS ID_UI,RAPPORTI_UTENZA.COD_CONTRATTO, " _
                                    & "UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE , " _
                                    & "TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA, " _
                                    & "POSIZIONE_BILANCIO, " _
                                    & "TIPO_DISPONIBILITA.DESCRIZIONE AS STATO_UI," _
                                    & "(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN ''  " _
                                    & "ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO,(CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) END) AS stato_dt_select," _
                                    & "siscom_mi.Getintestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,0) AS INTESTATARIO," _
                                    & "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  " _
                                    & "	  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO," _
                                    & "RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC,RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR " _
                                    & "FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & "SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE," _
                                    & "SISCOM_MI.ANAGRAFICA, SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI," _
                                    & "SISCOM_MI.TIPO_DISPONIBILITA, SISCOM_MI.RAPPORTI_UTENZA " _
                                    & "WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD " _
                                    & "AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID " _
                                    & "AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.ID_SCALA " _
                                    & "AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & "AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) " _
                                    & "AND COND_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID " _
                                    & "AND COD_TIPO_DISPONIBILITA <> 'VEND' " _
                                    & "AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD " _
                                    & "AND COND_UI.ID_INTESTARIO=ANAGRAFICA.ID(+) " _
                                    & "AND (cond_ui.id_condominio=  " & Request.QueryString("IDCONDOMINIO") & ") AND EDIFICI.ID " _
                                    & "IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO =   " & Request.QueryString("IDCONDOMINIO") & ") " _
                                    & "AND ID_UI NOT IN (SELECT ID_UI FROM SISCOM_MI.COND_UI_GESTIONE_C WHERE ID_GESTIONE = " & vIdGestione & " ) " _
                                    & "AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID(+) " _
                                    & "ORDER BY POSIZIONE_BILANCIO  ASC,id_ui ASC,DATA_DECORRENZA DESC"
                ''☺♫☼ Puccia 08/01/2013 AND COD_TIPO_DISPONIBILITA = 'LIBE' 

            End If

            'par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID_EDIFICIO,ID_CONTRATTO, UNITA_IMMOBILIARI.ID AS ID_UI, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,COND_UI.POSIZIONE_BILANCIO,UNITA_CONTRATTUALE.ID_CONTRATTO,(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO'  ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO,siscom_mi.Getintestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,1) AS INTESTATARIO,(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.ANAGRAFICA, SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID AND SCALE_EDIFICI.ID= UNITA_IMMOBILIARI.ID_SCALA AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) AND COND_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID AND COD_TIPO_DISPONIBILITA <> 'VEND' AND (SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)='IN CORSO ABUSIVO' OR UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL ) AND COND_UI.ID_INTESTARIO=ANAGRAFICA.ID(+) AND (cond_ui.id_condominio= " & Request.QueryString("IDCONDOMINIO") & ") AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO =  " & Request.QueryString("IDCONDOMINIO") & ") AND ID_UI NOT IN (SELECT ID_UI FROM SISCOM_MI.COND_UI_GESTIONE_P WHERE ID_GESTIONE = " & vIdGestione & " ) ORDER BY POSIZIONE_BILANCIO ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)

            DataGridElencoUiLibereAbusive.DataSource = FiltraSoloAbusLiberi(FiltraContrattiVeri(dt))
            DataGridElencoUiLibereAbusive.DataBind()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Function FiltraContrattiVeri(ByVal Table As Data.DataTable) As Data.DataTable
        FiltraContrattiVeri = Table.Clone()
        Dim idUi As Integer = 0
        Try
            Dim rSelect As Data.DataRow()

            For i As Integer = 0 To Table.Rows.Count - 1
                If par.IfNull(Table.Rows(i).Item("COD_CONTRATTO"), "") <> "" Then
                    If Table.Rows(i).Item("ID_UI") <> idUi Then
                        rSelect = Table.Select("ID_UI = " & Table.Rows(i).Item("ID_UI") & " AND STATO_DT_SELECT LIKE '%IN CORSO%'")
                        If rSelect.Length > 0 Then
                            FiltraContrattiVeri.Rows.Add(rSelect(0).ItemArray)
                            idUi = rSelect(0).Item("ID_UI")
                        Else
                            FiltraContrattiVeri.Rows.Add(Table.Rows(i).ItemArray)
                            idUi = Table.Rows(i).Item("ID_UI")
                        End If
                    End If
                Else
                    FiltraContrattiVeri.Rows.Add(Table.Rows(i).ItemArray)
                End If
            Next

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " FiltraContrattiVeri"
        End Try
        Return FiltraContrattiVeri
    End Function

    Private Function FiltraSoloAbusLiberi(ByVal Table As Data.DataTable) As Data.DataTable

        FiltraSoloAbusLiberi = Table.Clone()
        Try

            Dim idUi As Integer = 0

            For i As Integer = 0 To Table.Rows.Count - 1
                If par.IfNull(Table.Rows(i).Item("COD_CONTRATTO"), "") <> "" Then
                    If ((Table.Rows(i).Item("COD_TIPOLOGIA_CONTR_LOC") = "NONE" And Table.Rows(i).Item("COD_TIPOLOGIA_RAPP_CONTR") = "ILLEG") Or Table.Rows(i).Item("STATO") = "CHIUSO") And Table.Rows(i).Item("ID_UI") <> idUi Then
                        FiltraSoloAbusLiberi.Rows.Add(Table.Rows(i).ItemArray)
                        idUi = Table.Rows(i).Item("ID_UI")
                    End If
                Else
                    FiltraSoloAbusLiberi.Rows.Add(Table.Rows(i).ItemArray)
                End If
            Next
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        Return FiltraSoloAbusLiberi

    End Function
    Protected Sub Aggiungi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Aggiungi.Click
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            Dim i As Integer = 0
            Dim di As DataGridItem
            Dim posizioneBilancio As String = ""
            For i = 0 To Me.DataGridElencoUiLibereAbusive.Items.Count - 1
                di = Me.DataGridElencoUiLibereAbusive.Items(i)
                posizioneBilancio = Me.DataGridElencoUiLibereAbusive.Items(i).Cells(2).Text
                If DirectCast(di.Cells(1).FindControl("ChkSeleziona"), CheckBox).Checked = True Then
                    If Request.QueryString("CHIAMA") = "P" Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_UI_GESTIONE_P(ID_GESTIONE,ID_UI, POSIZIONE_BILANCIO,STATO) VALUES" _
                        & " (" & vIdGestione & ", " & Me.DataGridElencoUiLibereAbusive.Items(i).Cells(0).Text & ", '" & posizioneBilancio & "', '" & par.PulisciStrSql(Me.DataGridElencoUiLibereAbusive.Items(i).Cells(5).Text) & "')"
                    ElseIf Request.QueryString("CHIAMA") = "C" Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_UI_GESTIONE_C(ID_GESTIONE,ID_UI, POSIZIONE_BILANCIO,STATO) VALUES" _
                        & " (" & vIdGestione & ", " & Me.DataGridElencoUiLibereAbusive.Items(i).Cells(0).Text & ", " & par.IfEmpty(Me.DataGridElencoUiLibereAbusive.Items(i).Cells(2).Text, "Null") & ", '" & par.PulisciStrSql(Me.DataGridElencoUiLibereAbusive.Items(i).Cells(5).Text) & "')"
                    End If
                    par.cmd.ExecuteNonQuery()
                    Session.Item("MODIFYMODAL") = 1
                End If
            Next
            If Request.QueryString("CHIAMA") = "P" Then
                CercaDaPreventivi()
            ElseIf Request.QueryString("CHIAMA") = "C" Then
                CercaDaConsuntivi()
            End If
            ElencoInquilini()
            Me.txtModificato.Value = 1
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub AddJavascriptFunction()
        Try

            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridLiberiAbus.Items.Count - 1

                di = Me.DataGridLiberiAbus.Items(i)
                DirectCast(di.Cells(1).FindControl("txtTOT"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                DirectCast(di.Cells(1).FindControl("txtTOT"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")

            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub


    Protected Sub DataGridLiberiAbus_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridLiberiAbus.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'unità con posizione di bilancio:" & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtIdUi').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'unità con posizione di bilancio:" & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtIdUi').value='" & e.Item.Cells(0).Text & "'")
        End If

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDelete.Click
        Try

            If Me.txtIdUi.Value <> 0 Then
                If txtConfElimina.Value = 1 Then

                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    If Request.QueryString("CHIAMA") = "P" Then

                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_UI_GESTIONE_P WHERE ID_UI = " & Me.txtIdUi.Value & " AND ID_GESTIONE = " & vIdGestione
                        par.cmd.ExecuteNonQuery()

                        CercaDaPreventivi()

                    ElseIf Request.QueryString("CHIAMA") = "C" Then

                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_UI_GESTIONE_C WHERE ID_UI = " & Me.txtIdUi.Value & " AND ID_GESTIONE = " & vIdGestione
                        par.cmd.ExecuteNonQuery()

                        CercaDaPreventivi()

                    End If
                    txtIdUi.Value = 0
                    txtConfElimina.Value = 0
                    ElencoInquilini()
                Else
                    txtConfElimina.Value = 0
                    txtIdUi.Value = 0

                End If

            Else
                Response.Write("<script>alert('Selezionare un elemento da eliminare!');</script>")
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Try

            If Request.QueryString("IDVISUAL") = "0" And Not String.IsNullOrEmpty(Request.QueryString("IDVISUAL")) Then
                Response.Write("<script>window.close();</script>")
            Else
                If txtesci.Value = 1 Then


                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    'par.cmd.CommandText = "ROLLBACK TO SAVEPOINT LIBABUS"
                    'par.cmd.ExecuteNonQuery()
                    'If Me.txtModificato.Value = 1 Then
                    Session.Item("MODIFYMODAL") = 1
                    'End If
                    If txtsalvato.Value = 0 Then
                        Response.Write("<script>window.close();</script>")
                    Else
                        Response.Write("<script>window.close();window.returnValue = '1';</script>")

                    End If
                End If

            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    'Private Sub SommaRighe()
    '    Try
    '        Dim i As Integer = 0
    '        Dim di As DataGridItem

    '        For i = 0 To Me.DataGridLiberiAbus.Items.Count - 1
    '            di = Me.DataGridLiberiAbus.Items(i)
    '            DirectCast(di.Cells(1).FindControl("txtTOT"), TextBox).Text = Format(CDbl(par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Text, 0.0)) + CDbl(par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtImportoAsc"), TextBox).Text, 0.0)) + CDbl(par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtImportoRisc"), TextBox).Text, 0.0)), "##,##0.00")
    '        Next

    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message
    '    End Try
    'End Sub

    Protected Sub btnSommatoria_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSommatoria.Click
        SommaColonne()
    End Sub
    Private Sub SommaColonne()
        Try

            Dim Totale As Decimal = 0
            For Each di As DataGridItem In DataGridLiberiAbus.Items
                Totale = Totale + par.IfEmpty(DirectCast(di.FindControl("txtTOT"), TextBox).Text.Replace(".", ""), 0)
            Next

            Me.txtSommaTot.Text = Format(Totale, "##,##0.00")
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try
    End Sub

End Class
