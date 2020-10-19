
Partial Class Contratti_SelezionaUnita
    Inherits PageSetIdMode
    Dim PAR As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        'Response.Expires = 0
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If IsPostBack = False Then
            Response.Flush()

            HIDDENidDom.Value = Request.QueryString("IDDOM")
            HIDDENtipo.Value = Request.QueryString("TIPO")

            ValorizzaInfoDomanda()
            ValorizzazioneVariabili()
            BindGrid()
        End If
    End Sub

    Private Sub ValorizzaInfoDomanda()
        Try
            PAR.OracleConn.Open()
            par.SettaCommand(par)

            Select Case HIDDENtipo.Value
                Case 1
                    PAR.cmd.CommandText = "SELECT DOMANDE_BANDO.DATA_PG,DOMANDE_BANDO.id_bando,DOMANDE_BANDO.PG,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,domande_bando.FL_ASS_ESTERNA,domande_bando.id_dichiarazione,domande_bando.tipo_alloggio FROM DOMANDE_BANDO,COMP_NUCLEO WHERE DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND COMP_NUCLEO.PROGR=DOMANDE_BANDO.PROGR_COMPONENTE AND DOMANDE_BANDO.ID=" & HIDDENidDom.Value
                Case 2
                    PAR.cmd.CommandText = "SELECT DOMANDE_BANDO_CAMBI.DATA_PG,DOMANDE_BANDO_CAMBI.id_bando,DOMANDE_BANDO_CAMBI.PG,COMP_NUCLEO_CAMBI.COGNOME,COMP_NUCLEO_CAMBI.NOME,domande_bando_CAMBI.FL_ASS_ESTERNA,domande_bando_cambi.id_dichiarazione FROM DOMANDE_BANDO_CAMBI,COMP_NUCLEO_CAMBI WHERE DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_CAMBI.PROGR=DOMANDE_BANDO_CAMBI.PROGR_COMPONENTE AND DOMANDE_BANDO_CAMBI.ID=" & HIDDENidDom.Value
                Case 3
                    PAR.cmd.CommandText = "SELECT DOMANDE_BANDO_vsa.DATA_PG,DOMANDE_BANDO_vsa.id_bando,DOMANDE_BANDO_vsa.PG,COMP_NUCLEO_vsa.COGNOME,COMP_NUCLEO_vsa.NOME,domande_bando_vsa.FL_ASS_ESTERNA,domande_bando_vsa.id_dichiarazione FROM DOMANDE_BANDO_vsa,COMP_NUCLEO_vsa WHERE DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=COMP_NUCLEO_vsa.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_vsa.PROGR=DOMANDE_BANDO_vsa.PROGR_COMPONENTE AND DOMANDE_BANDO_vsa.ID=" & HIDDENidDom.Value
            End Select

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader1.Read() Then
                HIDDENdataPG.Value = PAR.IfNull(myReader1("DATA_PG"), "")
                HIDDENnumPG.Value = PAR.IfNull(myReader1("PG"), "")
                HIDDENnome.Value = Mid(PAR.IfNull(myReader1("COGNOME"), "") & " " & PAR.IfNull(myReader1("NOME"), ""), 1, 27)
                HIDDENidBando.Value = PAR.IfNull(myReader1("ID_BANDO"), "0")
            End If
            myReader1.Close()

            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label9.Text = ex.Message
        End Try
    End Sub

    Public Property idQuartiere() As Long
        Get
            If Not (ViewState("par_idQuartiere") Is Nothing) Then
                Return CLng(ViewState("par_idQuartiere"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idQuartiere") = value
        End Set

    End Property

    Public Property tipo() As Integer
        Get
            If Not (ViewState("par_tipo") Is Nothing) Then
                Return CInt(ViewState("par_tipo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_tipo") = value
        End Set

    End Property

    Public Property pertinenza() As String
        Get
            If Not (ViewState("par_pertinenza") Is Nothing) Then
                Return CStr(ViewState("par_pertinenza"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_pertinenza") = value
        End Set

    End Property

    Public Property piano() As Integer
        Get
            If Not (ViewState("par_piano") Is Nothing) Then
                Return CInt(ViewState("par_piano"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_piano") = value
        End Set

    End Property

    Public Property zona() As String
        Get
            If Not (ViewState("par_zona") Is Nothing) Then
                Return CStr(ViewState("par_zona"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_zona") = value
        End Set

    End Property

    Public Property ascensore() As Integer
        Get
            If Not (ViewState("par_ascensore") Is Nothing) Then
                Return CInt(ViewState("par_ascensore"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_ascensore") = value
        End Set

    End Property

    Public Property handicap() As Integer
        Get
            If Not (ViewState("par_handicap") Is Nothing) Then
                Return CInt(ViewState("par_handicap"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_handicap") = value
        End Set

    End Property

    Public Property postoauto() As Integer
        Get
            If Not (ViewState("par_postoauto") Is Nothing) Then
                Return CInt(ViewState("par_postoauto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_postoauto") = value
        End Set

    End Property

    Private Sub ValorizzazioneVariabili()
        idQuartiere = Request.QueryString("Q")
        tipo = Request.QueryString("T")
        pertinenza = Request.QueryString("PRT")
        piano = Request.QueryString("P")
        ascensore = Request.QueryString("A")
        handicap = Request.QueryString("H")
        zona = Request.QueryString("Z")
        postoauto = Request.QueryString("PA")
    End Sub

    Private Sub BindGrid()
        Try

            PAR.OracleConn.Open()
            par.SettaCommand(par)

            Dim sStringaSQL1 As String = ""
            Dim filtri As String = ""

            If idQuartiere <> "-1" Then
                filtri = filtri & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE=" & idQuartiere
            End If
            If tipo <> "-1" Then
                filtri = filtri & " AND T_TIPO_ALL_ERP.COD =" & tipo
            End If
            If ascensore = 0 Then
                filtri = filtri & " AND (SISCOM_MI.EDIFICI.NUM_ASCENSORI = 0 OR SISCOM_MI.EDIFICI.NUM_ASCENSORI IS NULL) "
            Else
                filtri = filtri & " AND SISCOM_MI.EDIFICI.NUM_ASCENSORI > 0 "
            End If
            If pertinenza <> "- - -" Then
                filtri = filtri & " AND UNITA_IMMOBILIARI.cod_tipologia ='" & pertinenza & "' AND UNITA_IMMOBILIARI.id_unita_principale is not null "
            End If
            If zona <> "- - -" Then
                filtri = filtri & " AND ALLOGGI.ZONA ='" & zona & "'"
            End If
            If piano <> "-1" Then
                filtri = filtri & " AND TIPO_LIVELLO_PIANO.cod=" & piano
            End If
            If handicap = 0 Then
                filtri = filtri & " AND ALLOGGI.H_MOTORIO=0 "
            Else
                filtri = filtri & " AND ALLOGGI.H_MOTORIO=1 "
            End If
            If postoauto = 1 Then
                filtri = filtri & " AND unita_immobiliari.id in (select id_unita_principale from siscom_mi.unita_immobiliari where cod_tipologia in ('H','I') and id_unita_principale is not null) "
            End If

            sStringaSQL1 = "SELECT (SELECT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS NETTA,(SELECT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_CONV' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS CONV,DECODE(ALLOGGI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE as TIPO_ALL," _
                       & "T_TIPO_INDIRIZZO.DESCRIZIONE AS ""TIPO_VIA"",TAB_QUARTIERI.NOME AS NOME_QUARTIERE,ALLOGGI.ZONA,(CASE WHEN SISCOM_MI.EDIFICI.NUM_ASCENSORI > 0 THEN 'SI' else 'NO' END) as ""ELEVATORE"",(CASE WHEN ALLOGGI.H_MOTORIO = 1 THEN 'SI' else 'NO' END) as ""HANDICAP"", " _
                       & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,ALLOGGI," _
                       & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.TAB_QUARTIERI WHERE ALLOGGI.COD_ALLOGGIO=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE (+) AND FL_POR='0' AND EQCANONE='0' AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                       & " (unita_immobiliari.id_destinazione_uso=1 or unita_immobiliari.id_destinazione_uso=2) AND ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                       & " ASSEGNATO='0' AND PRENOTATO='0' AND alloggi.STATO=5 AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = 'LIBE' AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND TAB_QUARTIERI.ID =COMPLESSI_IMMOBILIARI.ID_QUARTIERE AND edifici.id = unita_immobiliari.id_Edificio AND fl_piano_vendita = 0 " _
                       & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) " & filtri & " ORDER BY ALLOGGI.TIPO_INDIRIZZO ASC, ALLOGGI.INDIRIZZO ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, PAR.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()
            If DataGrid1.Items.Count = 0 Then
                lblNumRisultati.Text = "Risultati 0 Unità Libere per Abbinamento"
                Response.Write("<script>alert('I filtri di ricerca inseriti non hanno prodotto risultati!');</script>")
            Else
                lblNumRisultati.Text = "Risultati " & DataGrid1.Items.Count & " Unità Libere per Abbinamento"
            End If

            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label9.Text = ex.Message
        End Try

    End Sub

    Protected Sub DataGrid1_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        
        Dim scriptblock As String = ""
        scriptblock = "<script language='javascript' type='text/javascript'>" _
        & "popupWindow=window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & e.Item.Cells(1).Text & "','" & e.Item.Cells(1).Text & "','height=580,top=0,left=0,width=780');" _
        & "</script>"
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
        End If

    End Sub

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        LBLID.Text = e.Item.Cells(0).Text
        Label9.Text = "Hai selezionato Unità Cod.: " & e.Item.Cells(1).Text

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Try
            If Session.Item("LAVORAZIONE") = "1" Then
                PAR.OracleConn.Close()
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE123"), Oracle.DataAccess.Client.OracleConnection)
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE123"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                PAR.myTrans.Rollback()

                PAR.OracleConn.Close()
                PAR.OracleConn.Dispose()
                HttpContext.Current.Session.Remove("TRANSAZIONE123")
                HttpContext.Current.Session.Remove("CONNESSIONE123")
                Session.Item("LAVORAZIONE") = "0"
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Page.Dispose()
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            Else
                Session.Item("LAVORAZIONE") = "0"
                Page.Dispose()
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            End If
        Catch EX As Exception
            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
            Page.Dispose()
            Response.Write("<script>document.location.href=""../ErrorPage.aspx""</script>")
        Finally

        End Try
    End Sub

    Protected Sub btnAnnulla0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNuovaRicerca.Click
        Response.Write("<script>location.replace('AbbinaDomandaNUOVO.aspx?ID=" & HIDDENidDom.Value & "&TIPO=" & HIDDENtipo.Value & "');</script>")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        If LBLID.Text = "-1" Or LBLID.Text = "" Or LBLID.Text = "Label" Then
            Response.Write("<script>alert('Nessun Alloggio Selezionato!')</script>")
        Else
            If Len(txtData.Text) <> 10 Or IsDate(txtData.Text) = False Or PAR.AggiustaData(txtData.Text) < Format(Now, "yyyyMMdd") Then
                Response.Write("<script>alert('Data Scadenza non valida!')</script>")
            Else
                Select Case HIDDENtipo.Value
                    Case "1"
                        Abbina()
                    Case "2"
                        AbbinaCambi()
                    Case "3"
                        AbbinaEmergenze()
                End Select
            End If
        End If
    End Sub

    Private Sub Abbina()
        Dim scriptblock As String
        Dim DATASCADENZA As String
        Dim N_ABBINAMENTO As String

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE123", PAR.OracleConn)

            PAR.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE123", PAR.myTrans)


            PAR.cmd.CommandText = "SELECT STATO FROM ALLOGGI WHERE ID=" & LBLID.Text & " for update nowait"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader("STATO") = "5" Then

                    DATASCADENZA = Mid(txtData.Text, 7, 4) & Mid(txtData.Text, 4, 2) & Mid(txtData.Text, 1, 2)

                    PAR.cmd.CommandText = "INSERT INTO DOMANDE_OFFERTE_SCAD (ID,ID_DOMANDA,DATA_SCADENZA,FL_VALIDA) VALUES (SEQ_ABBINAMENTI.NEXTVAL," & HIDDENidDom.Value & ",'" & DATASCADENZA & "','1')"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "SELECT SEQ_ABBINAMENTI.CURRVAL FROM DUAL"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    N_ABBINAMENTO = ""
                    If myReader2.Read() Then
                        N_ABBINAMENTO = myReader2(0)
                    End If
                    myReader2.Close()

                    PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_PROPOSTA='1' WHERE ID=" & HIDDENidDom.Value
                    PAR.cmd.ExecuteNonQuery()


                    PAR.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET ULTIMO=0 WHERE ID_PRATICA=" & HIDDENidDom.Value
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,PROPOSTA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & HIDDENidDom.Value & "," & LBLID.Text & ",'" & Format(Now, "yyyyMMdd") & "',1," & N_ABBINAMENTO & ")"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "UPDATE ALLOGGI SET STATO=7,PRENOTATO='1',ID_PRATICA=" & HIDDENidDom.Value & ",DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & LBLID.Text
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                        & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                        & "2,7," _
                                        & LBLID.Text & "," _
                                        & HIDDENidDom.Value & ",'')"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    If myReader1.Read() = False Then
                        PAR.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                        PAR.cmd.ExecuteNonQuery()
                    End If
                    myReader1.Close()

                    PAR.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI+1,DISPONIBILI=DISPONIBILI-1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & HIDDENidDom.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                & "','F10','','I')"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                        & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,18,'" & HIDDENnumPG.Value & "','" & HIDDENdataPG.Value & "',10," & HIDDENidBando.Value & ",10)"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.myTrans.Commit()
                    PAR.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE123", PAR.myTrans)


                    btnReport.Visible = True
                    Button1.Visible = False

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Alloggio Abbinato con successo! Ora sarà visualizzato il report di abbinamento!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                    Session.Add("NOMINATIVO", HIDDENnome.Value)

                    btnReport.Attributes.Add("OnClick", "javascript:window.open('ReportAbbinamento.aspx?ABB=" & N_ABBINAMENTO & "&IDALL=" & LBLID.Text & "&DATAS=" & txtData.Text & "','Report');")

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "window.open('ReportAbbinamento.aspx?ABB=" & N_ABBINAMENTO & "&IDALL=" & LBLID.Text & "&DATAS=" & txtData.Text & "','Report');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
                    End If
                Else
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('ATTENZIONE!! Questo alloggio non è più disponibile! Verificare che non sia stato assegnato o prenotato da un altro utente di SEPA!!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                End If
            End If
            myReader.Close()

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('ATTENZIONE!! Questo alloggio non è più disponibile! Verificare che non sia stato assegnato o prenotato da un altro utente di SEPA!!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            Else
                Button1.Visible = False
                Label9.Visible = False
                Label11.Visible = True
                Label11.Text = EX1.Message
            End If

        Catch ex As Exception
            Button1.Visible = False
            Label9.Visible = False
            Label11.Visible = True
            Label11.Text = ex.Message
        End Try

    End Sub

    Private Sub AbbinaCambi()
        Dim scriptblock As String
        Dim DATASCADENZA As String
        Dim N_ABBINAMENTO As String

        Try

            PAR.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE123", PAR.OracleConn)

            PAR.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE123", PAR.myTrans)


            PAR.cmd.CommandText = "SELECT STATO FROM ALLOGGI WHERE ID=" & LBLID.Text & " for update nowait"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader("STATO") = "5" Then

                    DATASCADENZA = Mid(txtData.Text, 7, 4) & Mid(txtData.Text, 4, 2) & Mid(txtData.Text, 1, 2)

                    PAR.cmd.CommandText = "INSERT INTO DOMANDE_OFFERTE_SCAD (ID,ID_DOMANDA,DATA_SCADENZA,FL_VALIDA) VALUES (SEQ_ABBINAMENTI.NEXTVAL," & HIDDENidDom.Value & ",'" & DATASCADENZA & "','1')"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "SELECT SEQ_ABBINAMENTI.CURRVAL FROM DUAL"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    N_ABBINAMENTO = ""
                    If myReader2.Read() Then
                        N_ABBINAMENTO = myReader2(0)
                    End If
                    myReader2.Close()

                    PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET FL_PROPOSTA='1' WHERE ID=" & HIDDENidDom.Value
                    PAR.cmd.ExecuteNonQuery()


                    PAR.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET ULTIMO=0 WHERE ID_PRATICA=" & HIDDENidDom.Value
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,PROPOSTA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & HIDDENidDom.Value & "," & LBLID.Text & ",'" & Format(Now, "yyyyMMdd") & "',1," & N_ABBINAMENTO & ")"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "UPDATE ALLOGGI SET STATO=7,PRENOTATO='1',ID_PRATICA=" & HIDDENidDom.Value & ",DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & LBLID.Text
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                        & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                        & "2,7," _
                                        & LBLID.Text & "," _
                                        & HIDDENidDom.Value & ",'')"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    If myReader1.Read() = False Then
                        PAR.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                        PAR.cmd.ExecuteNonQuery()
                    End If
                    myReader1.Close()

                    PAR.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI+1,DISPONIBILI=DISPONIBILI-1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & HIDDENidDom.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                & "','F10','','I')"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                        & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,18,'" & HIDDENnumPG.Value & "','" & HIDDENdataPG.Value & "',10," & HIDDENidBando.Value & ",10)"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.myTrans.Commit()

                    PAR.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE123", PAR.myTrans)


                    btnReport.Visible = True
                    Button1.Visible = False

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Alloggio Abbinato con successo! Ora sarà visualizzato il report di abbinamento!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                    Session.Add("NOMINATIVO", HIDDENnome.Value)

                    btnReport.Attributes.Add("OnClick", "javascript:window.open('ReportAbbinamento.aspx?ABB=" & N_ABBINAMENTO & "&IDALL=" & LBLID.Text & "&DATAS=" & txtData.Text & "','Report');")

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "window.open('ReportAbbinamento.aspx?ABB=" & N_ABBINAMENTO & "&IDALL=" & LBLID.Text & "&DATAS=" & txtData.Text & "','Report');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
                    End If
                Else
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('ATTENZIONE!! Questo alloggio non è più disponibile! Verificare che non sia stato assegnato o prenotato da un altro utente di SEPA!!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                End If
            End If
            myReader.Close()

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('ATTENZIONE!! Questo alloggio non è più disponibile! Verificare che non sia stato assegnato o prenotato da un altro utente di SEPA!!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            Else
                Button1.Visible = False
                Label9.Visible = False
                Label11.Visible = True
                Label11.Text = EX1.Message
            End If

        Catch ex As Exception
            Button1.Visible = False
            Label9.Visible = False
            Label11.Visible = True
            Label11.Text = ex.Message
        End Try

    End Sub

    Private Sub AbbinaEmergenze()
        Dim scriptblock As String
        Dim DATASCADENZA As String
        Dim N_ABBINAMENTO As String

        Try

            PAR.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE123", PAR.OracleConn)

            PAR.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE123", PAR.myTrans)


            PAR.cmd.CommandText = "SELECT STATO FROM ALLOGGI WHERE ID=" & LBLID.Text & " for update nowait"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader("STATO") = "5" Then

                    DATASCADENZA = Mid(txtData.Text, 7, 4) & Mid(txtData.Text, 4, 2) & Mid(txtData.Text, 1, 2)

                    PAR.cmd.CommandText = "INSERT INTO DOMANDE_OFFERTE_SCAD (ID,ID_DOMANDA,DATA_SCADENZA,FL_VALIDA) VALUES (SEQ_ABBINAMENTI.NEXTVAL," & HIDDENidDom.Value & ",'" & DATASCADENZA & "','1')"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "SELECT SEQ_ABBINAMENTI.CURRVAL FROM DUAL"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    N_ABBINAMENTO = ""
                    If myReader2.Read() Then
                        N_ABBINAMENTO = myReader2(0)
                    End If
                    myReader2.Close()

                    PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO_vsa SET FL_PROPOSTA='1' WHERE ID=" & HIDDENidDom.Value
                    PAR.cmd.ExecuteNonQuery()


                    PAR.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET ULTIMO=0 WHERE ID_PRATICA=" & HIDDENidDom.Value
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,PROPOSTA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & HIDDENidDom.Value & "," & LBLID.Text & ",'" & Format(Now, "yyyyMMdd") & "',1," & N_ABBINAMENTO & ")"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "UPDATE ALLOGGI SET STATO=7,PRENOTATO='1',ID_PRATICA=" & HIDDENidDom.Value & ",DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & LBLID.Text
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                        & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                        & "2,7," _
                                        & LBLID.Text & "," _
                                        & HIDDENidDom.Value & ",'')"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    If myReader1.Read() = False Then
                        PAR.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                        PAR.cmd.ExecuteNonQuery()
                    End If
                    myReader1.Close()

                    PAR.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI+1,DISPONIBILI=DISPONIBILI-1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO EVENTI_BANDI_vsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & HIDDENidDom.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                & "','F10','','I')"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                        & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,18,'" & HIDDENnumPG.Value & "','" & HIDDENdataPG.Value & "',10," & HIDDENidBando.Value & ",10)"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.myTrans.Commit()

                    PAR.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE123", PAR.myTrans)


                    btnReport.Visible = True
                    Button1.Visible = False

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Alloggio Abbinato con successo! Ora sarà visualizzato il report di abbinamento!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                    Session.Add("NOMINATIVO", HIDDENnome.Value)

                    btnReport.Attributes.Add("OnClick", "javascript:window.open('ReportAbbinamento.aspx?ABB=" & N_ABBINAMENTO & "&IDALL=" & LBLID.Text & "&DATAS=" & txtData.Text & "','Report');")

                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "window.open('ReportAbbinamento.aspx?ABB=" & N_ABBINAMENTO & "&IDALL=" & LBLID.Text & "&DATAS=" & txtData.Text & "','Report');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
                    End If


                Else
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('ATTENZIONE!! Questo alloggio non è più disponibile! Verificare che non sia stato assegnato o prenotato da un altro utente di SEPA!!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                End If
            End If
            myReader.Close()

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('ATTENZIONE!! Questo alloggio non è più disponibile! Verificare che non sia stato assegnato o prenotato da un altro utente di SEPA!!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            Else
                Button1.Visible = False
                Label9.Visible = False
                Label11.Visible = True
                Label11.Text = EX1.Message
            End If

        Catch ex As Exception
            Button1.Visible = False
            Label9.Visible = False
            Label11.Visible = True
            Label11.Text = ex.Message
        End Try

    End Sub

    Protected Sub DataGrid1_UpdateCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
        Dim CANONE As Double = 0
        Dim S As String = ""
        Dim id_unita As Long = 0
        If e.Item.Cells(2).Text = "0" Then
            Try
                PAR.OracleConn.Open()
                par.SettaCommand(par)

                PAR.cmd.CommandText = "SELECT id from siscom_MI.unita_immobiliari where cod_unita_immobiliare='" & e.Item.Cells(1).Text & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader.Read() Then
                    id_unita = PAR.IfNull(myReader(0), -1)
                End If
                myReader.Close()
                PAR.cmd.Dispose()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try

            Dim VAL_LOCATIVO_UNITA As String = ""
            Dim comunicazioni As String = ""

            Select Case HIDDENtipo.Value
                Case "1"
                    S = PAR.CalcolaCanone27(HIDDENidDom.Value, 1, id_unita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZona, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)
                Case "2"
                    S = PAR.CalcolaCanone27(HIDDENidDom.Value, 2, id_unita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZona, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)
                Case "3"
                    S = PAR.CalcolaCanone27(HIDDENidDom.Value, 3, id_unita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZona, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)
            End Select
            If comunicazioni <> "" Then
                Response.Write("<script>alert('" & comunicazioni & "');</script>")
            End If

            Session.Add("canone", S)
            Response.Write("<script>popupWindow=window.open('Canone.aspx','Canone','');popupWindow.focus();</script>")
        Else
            Response.Write("<script>alert('Non è possibile calcolare il canone su questo alloggio perchè non di proprietà comunale');</script>")
        End If
    End Sub

    Public Property AreaEconomica() As Integer
        Get
            If Not (ViewState("par_AreaEconomica") Is Nothing) Then
                Return CInt(ViewState("par_AreaEconomica"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_AreaEconomica") = value
        End Set

    End Property


    Public Property sCanone() As String
        Get
            If Not (ViewState("par_sCanone") Is Nothing) Then
                Return CStr(ViewState("par_sCanone"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCanone") = value
        End Set
    End Property

    Public Property sISEE() As String
        Get
            If Not (ViewState("par_sISEE") Is Nothing) Then
                Return CStr(ViewState("par_sISEE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISEE") = value
        End Set

    End Property


    Public Property sISE() As String
        Get
            If Not (ViewState("par_sISE") Is Nothing) Then
                Return CStr(ViewState("par_sISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE") = value
        End Set
    End Property

    Public Property sVSE() As String
        Get
            If Not (ViewState("par_sVSE") Is Nothing) Then
                Return CStr(ViewState("par_sVSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVSE") = value
        End Set
    End Property


    Public Property sPSE() As String
        Get
            If Not (ViewState("par_sPSE") Is Nothing) Then
                Return CStr(ViewState("par_sPSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPSE") = value
        End Set
    End Property

    Public Property sISP() As String
        Get
            If Not (ViewState("par_sISP") Is Nothing) Then
                Return CStr(ViewState("par_sISP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISP") = value
        End Set
    End Property


    Public Property sISR() As String
        Get
            If Not (ViewState("par_sISR") Is Nothing) Then
                Return CStr(ViewState("par_sISR"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISR") = value
        End Set
    End Property

    Public Property sREDD_DIP() As String
        Get
            If Not (ViewState("par_sREDD_DIP") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_DIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_DIP") = value
        End Set
    End Property

    Public Property sREDD_ALT() As String
        Get
            If Not (ViewState("par_sREDD_ALT") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_ALT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_ALT") = value
        End Set
    End Property

    Public Property sLimitePensione() As String
        Get
            If Not (ViewState("par_sLimitePensione") Is Nothing) Then
                Return CStr(ViewState("par_sLimitePensione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLimitePensione") = value
        End Set
    End Property

    Public Property sPER_VAL_LOC() As String
        Get
            If Not (ViewState("par_sPER_VAL_LOC") Is Nothing) Then
                Return CStr(ViewState("par_sPER_VAL_LOC"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPER_VAL_LOC") = value
        End Set
    End Property


    Public Property sPERC_INC_MAX_ISE_ERP() As String
        Get
            If Not (ViewState("par_sPERC_INC_MAX_ISE_ERP") Is Nothing) Then
                Return CStr(ViewState("par_sPERC_INC_MAX_ISE_ERP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPERC_INC_MAX_ISE_ERP") = value
        End Set
    End Property

    Public Property sCANONE_MIN() As String
        Get
            If Not (ViewState("par_sCANONE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sCANONE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONE_MIN") = value
        End Set
    End Property

    Public Property sISE_MIN() As String
        Get
            If Not (ViewState("par_sISE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sISE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE_MIN") = value
        End Set
    End Property

    Public Property sNOTE() As String
        Get
            If Not (ViewState("par_sNOTE") Is Nothing) Then
                Return CStr(ViewState("par_sNOTE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNOTE") = value
        End Set
    End Property

    Public Property sDEM() As String
        Get
            If Not (ViewState("par_sDEM") Is Nothing) Then
                Return CStr(ViewState("par_sDEM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDEM") = value
        End Set
    End Property

    Public Property sSUPCONVENZIONALE() As String
        Get
            If Not (ViewState("par_sSUPCONVENZIONALE") Is Nothing) Then
                Return CStr(ViewState("par_sSUPCONVENZIONALE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPCONVENZIONALE") = value
        End Set
    End Property

    Public Property sCOSTOBASE() As String
        Get
            If Not (ViewState("par_sCOSTOBASE") Is Nothing) Then
                Return CStr(ViewState("par_sCOSTOBASE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOSTOBASE") = value
        End Set
    End Property

    Public Property sZONA() As String
        Get
            If Not (ViewState("par_sZONA") Is Nothing) Then
                Return CStr(ViewState("par_sZONA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sZONA") = value
        End Set
    End Property

    Public Property sPIANO() As String
        Get
            If Not (ViewState("par_sPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPIANO") = value
        End Set
    End Property

    Public Property sCONSERVAZIONE() As String
        Get
            If Not (ViewState("par_sCONSERVAZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sCONSERVAZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCONSERVAZIONE") = value
        End Set
    End Property

    Public Property sVETUSTA() As String
        Get
            If Not (ViewState("par_sVETUSTA") Is Nothing) Then
                Return CStr(ViewState("par_sVETUSTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVETUSTA") = value
        End Set
    End Property

    Public Property sINCIDENZAISE() As String
        Get
            If Not (ViewState("par_sINCIDENZAISE") Is Nothing) Then
                Return CStr(ViewState("par_sINCIDENZAISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sINCIDENZAISE") = value
        End Set
    End Property


    Public Property sCOEFFFAM() As String
        Get
            If Not (ViewState("par_sCOEFFFAM") Is Nothing) Then
                Return CStr(ViewState("par_sCOEFFFAM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOEFFFAM") = value
        End Set
    End Property

    Public Property sSOTTOAREA() As String
        Get
            If Not (ViewState("par_sSOTTOAREA") Is Nothing) Then
                Return CStr(ViewState("par_sSOTTOAREA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSOTTOAREA") = value
        End Set
    End Property

    Public Property sMOTIVODECADENZA() As String
        Get
            If Not (ViewState("par_sMOTIVODECADENZA") Is Nothing) Then
                Return CStr(ViewState("par_sMOTIVODECADENZA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOTIVODECADENZA") = value
        End Set
    End Property

    Public Property sNUMCOMP() As String
        Get
            If Not (ViewState("par_sNUMCOMP") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP") = value
        End Set
    End Property

    Public Property sNUMCOMP66() As String
        Get
            If Not (ViewState("par_sNUMCOMP66") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP66"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP66") = value
        End Set
    End Property

    Public Property sNUMCOMP100() As String
        Get
            If Not (ViewState("par_sNUMCOMP100") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100") = value
        End Set
    End Property

    Public Property sNUMCOMP100C() As String
        Get
            If Not (ViewState("par_sNUMCOMP100C") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100C"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100C") = value
        End Set
    End Property

    Public Property sPREVDIP() As String
        Get
            If Not (ViewState("par_sPREVDIP") Is Nothing) Then
                Return CStr(ViewState("par_sPREVDIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPREVDIP") = value
        End Set
    End Property

    Public Property sDETRAZIONI() As String
        Get
            If Not (ViewState("par_sDETRAZIONI") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONI") = value
        End Set
    End Property

    Public Property sMOBILIARI() As String
        Get
            If Not (ViewState("par_sMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOBILIARI") = value
        End Set
    End Property


    Public Property sIMMOBILIARI() As String
        Get
            If Not (ViewState("par_sIMMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sIMMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sIMMOBILIARI") = value
        End Set
    End Property

    Public Property sCOMPLESSIVO() As String
        Get
            If Not (ViewState("par_sCOMPLESSIVO") Is Nothing) Then
                Return CStr(ViewState("par_sCOMPLESSIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOMPLESSIVO") = value
        End Set
    End Property

    Public Property sDETRAZIONEF() As String
        Get
            If Not (ViewState("par_sDETRAZIONEF") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONEF"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONEF") = value
        End Set
    End Property

    Public Property sANNOCOSTRUZIONE() As String
        Get
            If Not (ViewState("par_sANNOCOSTRUZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sANNOCOSTRUZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOCOSTRUZIONE") = value
        End Set
    End Property

    Public Property sLOCALITA() As String
        Get
            If Not (ViewState("par_sLOCALITA") Is Nothing) Then
                Return CStr(ViewState("par_sLOCALITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLOCALITA") = value
        End Set
    End Property

    Public Property sASCENSORE() As String
        Get
            If Not (ViewState("par_sASCENSORE") Is Nothing) Then
                Return CStr(ViewState("par_sASCENSORE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sASCENSORE") = value
        End Set
    End Property

    Public Property sDESCRIZIONEPIANO() As String
        Get
            If Not (ViewState("par_sDESCRIZIONEPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sDESCRIZIONEPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDESCRIZIONEPIANO") = value
        End Set
    End Property

    Public Property sSUPNETTA() As String
        Get
            If Not (ViewState("par_sSUPNETTA") Is Nothing) Then
                Return CStr(ViewState("par_sSUPNETTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPNETTA") = value
        End Set
    End Property

    Public Property sALTRESUP() As String
        Get
            If Not (ViewState("par_sALTRESUP") Is Nothing) Then
                Return CStr(ViewState("par_sALTRESUP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALTRESUP") = value
        End Set
    End Property

    Public Property sMINORI15() As String
        Get
            If Not (ViewState("par_sMINORI15") Is Nothing) Then
                Return CStr(ViewState("par_sMINORI15"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMINORI15") = value
        End Set
    End Property


    Public Property sMAGGIORI65() As String
        Get
            If Not (ViewState("par_sMAGGIORI65") Is Nothing) Then
                Return CStr(ViewState("par_sMAGGIORI65"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMAGGIORI65") = value
        End Set
    End Property

    Public Property sSUPACCESSORI() As String
        Get
            If Not (ViewState("par_sSUPACCESSORI") Is Nothing) Then
                Return CStr(ViewState("par_sSUPACCESSORI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPACCESSORI") = value
        End Set
    End Property

    Public Property sVALORELOCATIVO() As String
        Get
            If Not (ViewState("par_sVALORELOCATIVO") Is Nothing) Then
                Return CStr(ViewState("par_sVALORELOCATIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALORELOCATIVO") = value
        End Set
    End Property

    Public Property sCANONEMINIMO() As String
        Get
            If Not (ViewState("par_sCANONEMINIMO") Is Nothing) Then
                Return CStr(ViewState("par_sCANONEMINIMO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONEMINIMO") = value
        End Set
    End Property

    Public Property sCANONECLASSE() As String
        Get
            If Not (ViewState("par_sCANONECLASSE") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSE") = value
        End Set
    End Property

    Public Property sCANONESOPP() As String
        Get
            If Not (ViewState("par_sCANONESOPP") Is Nothing) Then
                Return CStr(ViewState("par_sCANONESOPP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONESOPP") = value
        End Set
    End Property


    Public Property sVALOCIICI() As String
        Get
            If Not (ViewState("par_sVALOCIICI") Is Nothing) Then
                Return CStr(ViewState("par_sVALOCIICI"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALOCIICI") = value
        End Set
    End Property

    Public Property sALLOGGIOIDONEO() As String
        Get
            If Not (ViewState("par_sALLOGGIOIDONEO") Is Nothing) Then
                Return CStr(ViewState("par_sALLOGGIOIDONEO"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALLOGGIOIDONEO") = value
        End Set
    End Property

    Public Property sISTAT() As String
        Get
            If Not (ViewState("par_sISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISTAT") = value
        End Set
    End Property

    Public Property sCANONECLASSEISTAT() As String
        Get
            If Not (ViewState("par_sCANONECLASSEISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSEISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSEISTAT") = value
        End Set
    End Property

    Public Property VAL_LOCATIVO_UNITA() As String
        Get
            If Not (ViewState("par_VAL_LOCATIVO_UNITA") Is Nothing) Then
                Return CStr(ViewState("par_VAL_LOCATIVO_UNITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_VAL_LOCATIVO_UNITA") = value
        End Set

    End Property



    Public Property CanoneCorrente() As Double
        Get
            If Not (ViewState("par_CanoneCorrente") Is Nothing) Then
                Return CDbl(ViewState("par_CanoneCorrente"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Double)
            ViewState("par_CanoneCorrente") = value
        End Set

    End Property


    Public Property sANNOINIZIOVAL() As String
        Get
            If Not (ViewState("par_sANNOINIZIOVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOINIZIOVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOINIZIOVAL") = value
        End Set
    End Property

    Public Property sANNOFINEVAL() As String
        Get
            If Not (ViewState("par_sANNOFINEVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOFINEVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOFINEVAL") = value
        End Set
    End Property


    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub
End Class
