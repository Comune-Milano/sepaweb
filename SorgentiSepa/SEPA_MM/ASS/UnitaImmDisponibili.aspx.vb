
Partial Class ASS_UnitaImmDisponibili
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        txt_scadOff.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


        If IsPostBack = False Then
            Response.Flush()

            HIDDENidDom.Value = Request.QueryString("IDDOM")
            HIDDENtipo.Value = Request.QueryString("TIPO")
            provenienza.Value = Request.QueryString("PROV")

            If provenienza.Value = "1" Then

                btn_abbina.Visible = True
                ImgProcedi.Visible = False
                lbl_scadOff.Visible = True
                txt_scadOff.Visible = True

            Else

                btn_abbina.Visible = False
                lbl_scadOff.Visible = False
                txt_scadOff.Visible = False
                ImgProcedi.Visible = True

            End If




            ValorizzazioneVariabili()
            BindGrid()
        End If
    End Sub

    Private Sub ValorizzazioneVariabili()
        idQuartiere = Request.QueryString("Q")
        tipo = Request.QueryString("T")
        pertinenza = Request.QueryString("PRT")
        piano = Request.QueryString("P")
        ascensore = Request.QueryString("A")
        handicap = Request.QueryString("H")
        zona = Request.QueryString("Z")
        postoauto = Request.QueryString("PA")
        Indirizzo = Request.QueryString("IN")
        aler = Request.QueryString("ALER")
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

    Public Property Indirizzo() As String
        Get
            If Not (ViewState("par_Indirizzo") Is Nothing) Then
                Return CStr(ViewState("par_Indirizzo"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Indirizzo") = value
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

    Public Property aler() As Integer
        Get
            If Not (ViewState("par_aler") Is Nothing) Then
                Return CInt(ViewState("par_aler"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_aler") = value
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

    Private Sub BindGrid()
        Try

            PAR.OracleConn.Open()
            par.SettaCommand(par)

            Dim sStringaTot As String = ""
            Dim sStringaSQL1 As String = ""
            Dim sStringaSQL2 As String = ""
            Dim filtri As String = ""
            Dim filtriquery2 As String = ""

            If idQuartiere <> "-1" Then
                filtri = filtri & " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE=" & idQuartiere
            End If

            If PAR.DeCripta(Indirizzo) <> "- - -" Then
                filtri = filtri & " AND unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where descrizione like '%" & PAR.PulisciStrSql(PAR.DeCripta(Indirizzo)) & "%') "
                filtriquery2 = filtriquery2 & " AND alloggi.indirizzo like '%" & PAR.PulisciStrSql(PAR.DeCripta(Indirizzo)) & "%' "
            End If

            If tipo <> "-1" Then
                filtri = filtri & " AND T_TIPO_ALL_ERP.COD =" & tipo
                filtriquery2 = filtriquery2 & " AND T_TIPO_ALL_ERP.COD =" & tipo
            End If


            If ascensore = 0 Then
                'filtri = filtri & " AND (SISCOM_MI.EDIFICI.NUM_ASCENSORI = 0 OR SISCOM_MI.EDIFICI.NUM_ASCENSORI IS NULL) "
            Else
                filtri = filtri & " AND 'SI'=(CASE WHEN (SELECT DISTINCT UNITA_IMMOBILIARI.ID  FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.UNITA_IMMOBILIARI UI WHERE UI.ID=unita_immobiliari.ID AND IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI_SCALE.ID_SCALA=UI.ID_SCALA AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)>0 THEN 'SI' END)  "
                filtriquery2 = filtriquery2 & " AND 'SI'=(CASE WHEN ASCENSORE = '1' THEN 'SI' END)"
            End If

            If pertinenza <> "-1" Then

                filtri = filtri & " AND unita_immobiliari.id in (select id_unita_principale from siscom_mi.unita_immobiliari where cod_tipologia ='" & pertinenza & "' and id_unita_principale is not null) "
            End If

            If zona <> "- - -" Then
                filtri = filtri & " AND ALLOGGI.ZONA ='" & zona & "'"
                filtriquery2 = filtriquery2 & " AND ALLOGGI.ZONA ='" & zona & "'"
            End If
            If piano <> "-1" Then
                filtri = filtri & " AND TIPO_LIVELLO_PIANO.cod=" & piano
                filtriquery2 = filtriquery2 & " AND ALLOGGI.PIANO ='" & piano & "'"
            End If
            If handicap = 0 Then

            Else
                filtri = filtri & " AND ALLOGGI.H_MOTORIO=1 "
                filtriquery2 = filtriquery2 & " AND ALLOGGI.H_MOTORIO=1 "
            End If

            If postoauto = 1 Then
                filtri = filtri & " AND unita_immobiliari.id in (select id_unita_principale from siscom_mi.unita_immobiliari where cod_tipologia in ('H','I') and id_unita_principale is not null) "
            End If

            

            sStringaSQL1 = "SELECT (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS NETTA,(SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_CONV' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS CONV," _
                & " (CASE WHEN (SELECT DISTINCT UNITA_IMMOBILIARI.ID  FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.UNITA_IMMOBILIARI UI WHERE UI.ID=unita_immobiliari.ID AND IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI_SCALE.ID_SCALA=UI.ID_SCALA AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)>0 THEN 'SI' END) AS elevatore," _
                & "T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE as TIPO_ALL," _
                       & "T_TIPO_INDIRIZZO.DESCRIZIONE AS ""TIPO_VIA"",TAB_QUARTIERI.NOME AS NOME_QUARTIERE,ALLOGGI.ZONA,(CASE WHEN ALLOGGI.H_MOTORIO = 1 THEN 'SI' else 'NO' END) as ""HANDICAP"", " _
                       & "('<a href=""javascript:window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID='||UNITA_IMMOBILIARI.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');void(0);"">'||'<img alt=" & Chr(34) & "Dettagli Unità, Foto e Planimetrie" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Abbina_Foto.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " />'||'</a>') as Visualizza," _
                       & "('<a href=""javascript:window.open(''CalcolaCanone.aspx?P='||PROPRIETA||'&IdDomanda=" & HIDDENidDom.Value & "&Tipo=" & HIDDENtipo.Value & "&ID='||UNITA_IMMOBILIARI.ID||''',''Dettagli'',''height=500,top=0,left=0,width=600'');void(0);"">'||'<img alt=" & Chr(34) & "Visualizza Canone" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Abbina_Canone.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " />'||'</a>') as VisualizzaCanone," _
                       & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,ALLOGGI," _
                       & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.TAB_QUARTIERI WHERE " _
                       & " unita_immobiliari.ID IN (SELECT id_unita FROM SISCOM_MI.UNITA_STATO_MANUTENTIVO WHERE UNITA_STATO_MANUTENTIVO.RIASSEGNABILE=1 and (UNITA_STATO_MANUTENTIVO.TIPO_RIASSEGNABILE = '1' OR (UNITA_STATO_MANUTENTIVO.TIPO_RIASSEGNABILE = '0' AND UNITA_STATO_MANUTENTIVO.FINE_LAVORI = 1))) AND " _
                       & "ALLOGGI.COD_ALLOGGIO=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE (+) AND FL_POR='0' AND EQCANONE='0' AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                       & " (unita_immobiliari.id_destinazione_uso=1 or unita_immobiliari.id_destinazione_uso=2) AND ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                       & " ASSEGNATO='0' AND PRENOTATO='0' AND alloggi.STATO=5 AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = 'LIBE' AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND TAB_QUARTIERI.ID =COMPLESSI_IMMOBILIARI.ID_QUARTIERE AND edifici.id = unita_immobiliari.id_Edificio AND fl_piano_vendita = 0 " _
                       & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) " & filtri

            sStringaSQL2 = "SELECT to_number(alloggi.SUP) AS netta,null AS conv,(CASE WHEN ASCENSORE = 1 then 'SI' END) AS elevatore," _
                      & " t_tipo_proprieta.descrizione AS ""PROPRIETA1""," _
                      & " tipo_livello_piano.descrizione AS ""PIANO"", alloggi.*," _
                      & " t_tipo_all_erp.descrizione AS tipo_all," _
                      & " t_tipo_indirizzo.descrizione AS ""TIPO_VIA""," _
                      & " '' AS nome_quartiere, alloggi.zona," _
                      & " (CASE WHEN alloggi.h_motorio = 1 THEN 'SI'" _
                      & " ELSE 'NO' END) AS ""HANDICAP"",'' AS visualizza,'' AS visualizzacanone," _
                      & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1""," _
                      & " alloggi.num_alloggio AS ""N_ALL""" _
                      & " FROM t_tipo_proprieta,alloggi,t_tipo_all_erp,t_tipo_indirizzo,siscom_mi.tipo_livello_piano" _
                      & " WHERE fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano" _
                      & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0'" _
                      & " AND prenotato = '0' AND alloggi.stato = 5" _
                      & " AND PROPRIETA = 1 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+)" _
                      & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " & filtriquery2 & ""

            If aler <> -1 Then
                If aler = 1 Then
                    sStringaTot = sStringaSQL2 & " ORDER BY tipo_via ASC,9 asc,10 asc"
                Else
                    sStringaTot = sStringaSQL1 & " ORDER BY tipo_via ASC,9 asc,10 asc"
                End If
            Else
                sStringaTot = sStringaSQL1 & "UNION " & sStringaSQL2 & " ORDER BY tipo_via ASC,9 asc,10 asc"
            End If
            
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaTot, PAR.OracleConn)
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
            'Label9.Text = ex.Message
        End Try

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='silver'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato l\' unità Cod. " & e.Item.Cells(1).Text & " - " & Replace(e.Item.Cells(6).Text, "'", "\'") & " " & Replace(e.Item.Cells(7).Text, "'", "\'") & " " & Replace(e.Item.Cells(8).Text, "'", "\'") & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If

    End Sub


 
    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        If LBLID.Value <> "" Then
            Session.Add("UNITA", LBLID.Value)
            Dim scriptblock As String = ""
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('Scelta effettuata. I dati dell\'unità saranno riportati nella maschera principale! Per modificare premere nuovamente il pulsante di ricerca.');self.close();" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
            End If
        End If


    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Dim scriptblock As String = ""
        scriptblock = "<script language='javascript' type='text/javascript'>" _
        & "window.close();" _
        & "</script>"
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
        End If
    End Sub

    '********************************** MARIA **********************************


    Protected Sub btn_abbina_Click(sender As Object, e As System.EventArgs) Handles btn_abbina.Click

        Try
      

            Dim scriptblock As String = ""


            scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
            End If



            If txt_conferma.Value = "1" Then

                If LBLID.Value = "" Then
                    Response.Write("<script>alert('Nessun alloggio selezionato!')</script>")
                    Exit Sub
                End If


                If Len(txt_scadOff.Text) <> 10 Or IsDate(txt_scadOff.Text) = False Or PAR.AggiustaData(txt_scadOff.Text) < Format(Now, "yyyyMMdd") Then
                    Response.Write("<script>alert('Data Scadenza non valida!')</script>")
                    Exit Sub
                End If


                Select Case HIDDENtipo.Value
                    Case "1"
                        Abbina()
                    Case "2"
                        AbbinaCambi()
                    Case "3"
                        AbbinaEmergenze()

                End Select

                btn_abbina.Enabled = False
                '   Response.Write("<script>window.close();</script>")

            End If



        Catch ex As Exception

       
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub



    Protected Sub Abbina()
        Dim scriptblock As String
        Dim N_ABBINAMENTO As String
        Dim PG As String = ""
        Dim dataPG As String = ""
        Dim idBando As Integer = 0
        Dim tipoAlloggio As Integer = 0
        Dim nominativo As String = ""
        Dim tipoGraduatoria As String = ""

        Dim dataScadOff As String = ""
        Try

            PAR.OracleConn.Open()
            par.SettaCommand(par)
            PAR.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans



            PAR.cmd.CommandText = "SELECT * from DOMANDE_BANDO,COMP_NUCLEO,BANDI_GRADUATORIA_DEF,DICHIARAZIONI WHERE DICHIARAZIONI.ID = DOMANDE_BANDO.ID_DICHIARAZIONE AND COMP_NUCLEO.ID_DICHIARAZIONE = DICHIARAZIONI.ID AND DOMANDE_BANDO.progr_componente = COMP_NUCLEO.progr AND BANDI_GRADUATORIA_DEF.id_domanda = DOMANDE_BANDO.ID(+) AND DOMANDE_BANDO.ID =" & HIDDENidDom.Value
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader0.Read Then
                PG = PAR.IfNull(myReader0("PG"), "")
                dataPG = PAR.IfNull(myReader0("DATA_PG"), "")
                idBando = PAR.IfNull(myReader0("ID_BANDO"), "")
                tipoAlloggio = PAR.IfNull(myReader0("TIPO_ALLOGGIO"), 0)
                nominativo = PAR.IfNull(myReader0("COGNOME"), "") & " " & PAR.IfNull(myReader0("NOME"), "")
                Select Case PAR.IfNull(myReader0("TIPO"), "")
                    Case 0
                        tipoGraduatoria = "E.R.P"
                    Case 1
                        tipoGraduatoria = "ART.14"
                    Case 2
                        tipoGraduatoria = "ART.15"
                End Select
            End If
            myReader0.Close()





            PAR.cmd.CommandText = "SELECT STATO FROM ALLOGGI WHERE ID=" & LBLID.Value & " for update nowait"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader("STATO") = "5" Then

                    ' DATASCADENZA = Mid(txtData.Text, 7, 4) & Mid(txtData.Text, 4, 2) & Mid(txtData.Text, 1, 2)

                    PAR.cmd.CommandText = "INSERT INTO DOMANDE_OFFERTE_SCAD (ID,ID_DOMANDA,DATA_SCADENZA,FL_VALIDA) VALUES (SEQ_ABBINAMENTI.NEXTVAL," & HIDDENidDom.Value & ",'" & PAR.AggiustaData(txt_scadOff.Text) & "','1')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT SEQ_ABBINAMENTI.CURRVAL FROM DUAL"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    N_ABBINAMENTO = ""
                    If myReader2.Read() Then
                        N_ABBINAMENTO = myReader2(0)
                        ' dataScadOff = PAR.FormattaData(myReader2("DATA_SCADENZA"))
                    End If
                    myReader2.Close()



                    PAR.cmd.CommandText = "SELECT * from domande_offerte_scad where id=" & N_ABBINAMENTO
                    myReader2 = PAR.cmd.ExecuteReader()
                    If myReader2.Read() Then

                        dataScadOff = PAR.FormattaData(myReader2("DATA_SCADENZA"))
                    End If
                    myReader2.Close()



                    PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_PROPOSTA='1' WHERE ID=" & HIDDENidDom.Value
                    par.cmd.ExecuteNonQuery()


                    PAR.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET ULTIMO=0 WHERE ID_PRATICA=" & HIDDENidDom.Value
                    par.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,PROPOSTA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & HIDDENidDom.Value & "," & LBLID.Value & ",'" & Format(Now, "yyyyMMdd") & "',1," & N_ABBINAMENTO & ")"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "UPDATE ALLOGGI SET STATO=7,PRENOTATO='1',ID_PRATICA=" & HIDDENidDom.Value & ",DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & LBLID.Value
                    par.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                        & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                        & "2,7," _
                                        & LBLID.Value & "," _
                                        & HIDDENidDom.Value & ",'')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read() = False Then
                        par.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI+1,DISPONIBILI=DISPONIBILI-1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
                    par.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & HIDDENidDom.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                & "','F10','','I')"
                    par.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                        & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,18,'" & PG & "','" & dataPG & "',10," & idBando & ",10)"
                    par.cmd.ExecuteNonQuery()


                    PAR.myTrans.Commit()
                    PAR.OracleConn.Close()
                    PAR.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                    Response.Write("<script>alert('Operazione effettuata con successo!'); </script>")


                    '   Response.Write("<script>alert('Operazione effettuata con successo!'); window.opener.location.reload('ProcessoDecisionale.aspx?TIPO=" & HIDDENtipo.Value & "&ID=" & HIDDENidDom.Value & "&OF=" & N_ABBINAMENTO & "&SC=" & dataScadOff & "'); window.close(); </script>")

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


        Catch ex As Exception
            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()
            PAR.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Protected Sub AbbinaCambi()
        Dim scriptblock As String
        Dim N_ABBINAMENTO As String
        Dim PG As String = ""
        Dim dataPG As String = ""
        Dim idBando As Integer = 0
        Dim tipoAlloggio As Integer = 0
        Dim nominativo As String = ""
        Dim tipoGraduatoria As String = ""

        Dim dataScadOff As String = ""
        Try

            PAR.OracleConn.Open()
            par.SettaCommand(par)
            PAR.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans



            PAR.cmd.CommandText = "SELECT * from DOMANDE_BANDO_CAMBI, COMP_NUCLEO_CAMBI, BANDI_GRADUATORIA_DEF_CAMBI, DICHIARAZIONI_CAMBI WHERE DICHIARAZIONI_CAMBI.ID = DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE = DICHIARAZIONI_CAMBI.ID AND DOMANDE_BANDO_CAMBI.progr_componente = COMP_NUCLEO_CAMBI.progr AND BANDI_GRADUATORIA_DEF_CAMBI.id_domanda = DOMANDE_BANDO_CAMBI.ID(+) AND DOMANDE_BANDO_CAMBI.ID =" & HIDDENidDom.Value
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader0.Read Then
                PG = PAR.IfNull(myReader0("PG"), "")
                dataPG = PAR.IfNull(myReader0("DATA_PG"), "")
                idBando = PAR.IfNull(myReader0("ID_BANDO"), "")
                tipoAlloggio = PAR.IfNull(myReader0("TIPO_ALLOGGIO"), 0)
                nominativo = PAR.IfNull(myReader0("COGNOME"), "") & " " & PAR.IfNull(myReader0("NOME"), "")
                Select Case PAR.IfNull(myReader0("TIPO"), "")
                    Case 0
                        tipoGraduatoria = "E.R.P"
                    Case 1
                        tipoGraduatoria = "ART.14"
                    Case 2
                        tipoGraduatoria = "ART.15"
                End Select
            End If
            myReader0.Close()





            PAR.cmd.CommandText = "SELECT STATO FROM ALLOGGI WHERE ID=" & LBLID.Value & " for update nowait"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader("STATO") = "5" Then

                    ' DATASCADENZA = Mid(txtData.Text, 7, 4) & Mid(txtData.Text, 4, 2) & Mid(txtData.Text, 1, 2)

                    PAR.cmd.CommandText = "INSERT INTO DOMANDE_OFFERTE_SCAD (ID,ID_DOMANDA,DATA_SCADENZA,FL_VALIDA) VALUES (SEQ_ABBINAMENTI.NEXTVAL," & HIDDENidDom.Value & ",'" & PAR.AggiustaData(txt_scadOff.Text) & "','1')"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "SELECT SEQ_ABBINAMENTI.CURRVAL FROM DUAL"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    N_ABBINAMENTO = ""
                    If myReader2.Read() Then
                        N_ABBINAMENTO = myReader2(0)
                        ' dataScadOff = PAR.FormattaData(myReader2("DATA_SCADENZA"))
                    End If
                    myReader2.Close()



                    PAR.cmd.CommandText = "SELECT * from domande_offerte_scad where id=" & N_ABBINAMENTO
                    myReader2 = PAR.cmd.ExecuteReader()

                    If myReader2.Read() Then

                        dataScadOff = PAR.FormattaData(myReader2("DATA_SCADENZA"))
                    End If
                    myReader2.Close()



                    PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET FL_PROPOSTA='1' WHERE ID=" & HIDDENidDom.Value
                    PAR.cmd.ExecuteNonQuery()


                    PAR.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET ULTIMO=0 WHERE ID_PRATICA=" & HIDDENidDom.Value
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,PROPOSTA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & HIDDENidDom.Value & "," & LBLID.Value & ",'" & Format(Now, "yyyyMMdd") & "',1," & N_ABBINAMENTO & ")"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "UPDATE ALLOGGI SET STATO=7,PRENOTATO='1',ID_PRATICA=" & HIDDENidDom.Value & ",DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & LBLID.Value
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                        & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                        & "2,7," _
                                        & LBLID.Value & "," _
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
                                        & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,18,'" & PG & "','" & dataPG & "',10," & idBando & ",10)"
                    PAR.cmd.ExecuteNonQuery()


                    PAR.myTrans.Commit()
                    PAR.OracleConn.Close()
                    PAR.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                    Response.Write("<script>alert('Operazione effettuata con successo!');</script>")

                    '  ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "opener.location.href('VerificaSManutentivo.aspx?A=" & chiamante.Value & "&ID= " & idunita.Value & "'); window.focus();", True)

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


        Catch ex As Exception
            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()
            PAR.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub



    Protected Sub AbbinaEmergenze()
        Dim scriptblock As String
        Dim N_ABBINAMENTO As String
        Dim PG As String = ""
        Dim dataPG As String = ""
        Dim idBando As Integer = 0
        Dim tipoAlloggio As Integer = 0
        Dim nominativo As String = ""
        Dim tipoGraduatoria As String = ""

        Dim dataScadOff As String = ""
        Try

            PAR.OracleConn.Open()
            par.SettaCommand(par)
            PAR.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans



            PAR.cmd.CommandText = "SELECT * from DOMANDE_BANDO_VSA, COMP_NUCLEO_VSA, DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND DOMANDE_BANDO_VSA.progr_componente = COMP_NUCLEO_VSA.progr AND DOMANDE_BANDO_VSA.ID =" & HIDDENidDom.Value
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader0.Read Then
                PG = PAR.IfNull(myReader0("PG"), "")
                dataPG = PAR.IfNull(myReader0("DATA_PG"), "")
                idBando = PAR.IfNull(myReader0("ID_BANDO"), "")
                tipoAlloggio = PAR.IfNull(myReader0("TIPO_ALLOGGIO"), 0)
                nominativo = PAR.IfNull(myReader0("COGNOME"), "") & " " & PAR.IfNull(myReader0("NOME"), "")
                Select Case PAR.IfNull(myReader0("TIPO"), "")
                    Case 0
                        tipoGraduatoria = "E.R.P"
                    Case 1
                        tipoGraduatoria = "ART.14"
                    Case 2
                        tipoGraduatoria = "ART.15"
                End Select
            End If
            myReader0.Close()





            PAR.cmd.CommandText = "SELECT STATO FROM ALLOGGI WHERE ID=" & LBLID.Value & " for update nowait"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader("STATO") = "5" Then

                    ' DATASCADENZA = Mid(txtData.Text, 7, 4) & Mid(txtData.Text, 4, 2) & Mid(txtData.Text, 1, 2)

                    PAR.cmd.CommandText = "INSERT INTO DOMANDE_OFFERTE_SCAD (ID,ID_DOMANDA,DATA_SCADENZA,FL_VALIDA) VALUES (SEQ_ABBINAMENTI.NEXTVAL," & HIDDENidDom.Value & ",'" & PAR.AggiustaData(txt_scadOff.Text) & "','1')"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "SELECT SEQ_ABBINAMENTI.CURRVAL FROM DUAL"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    N_ABBINAMENTO = ""
                    If myReader2.Read() Then
                        N_ABBINAMENTO = myReader2(0)
                        ' dataScadOff = PAR.FormattaData(myReader2("DATA_SCADENZA"))
                    End If
                    myReader2.Close()



                    PAR.cmd.CommandText = "SELECT * from domande_offerte_scad where id=" & N_ABBINAMENTO
                    myReader2 = PAR.cmd.ExecuteReader()

                    If myReader2.Read() Then

                        dataScadOff = PAR.FormattaData(myReader2("DATA_SCADENZA"))
                    End If
                    myReader2.Close()



                    PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET FL_PROPOSTA='1' WHERE ID=" & HIDDENidDom.Value
                    PAR.cmd.ExecuteNonQuery()


                    PAR.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET ULTIMO=0 WHERE ID_PRATICA=" & HIDDENidDom.Value
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO REL_PRAT_ALL_CCAA_ERP (ID,ID_PRATICA,ID_ALLOGGIO,DATA_PROPOSTA,ULTIMO,PROPOSTA) VALUES (SEQ_REL_PRAT_ALL.NEXTVAL," & HIDDENidDom.Value & "," & LBLID.Value & ",'" & Format(Now, "yyyyMMdd") & "',1," & N_ABBINAMENTO & ")"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "UPDATE ALLOGGI SET STATO=7,PRENOTATO='1',ID_PRATICA=" & HIDDENidDom.Value & ",DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & LBLID.Value
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                        & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                        & "2,7," _
                                        & LBLID.Value & "," _
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

                    PAR.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                & "VALUES (" & HIDDENidDom.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                & "','F10','','I')"
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "INSERT INTO STATISTICHE (DATA_EVENTO,COD_EVENTO,ESITO_EVENTO,PG,DATA_PG,SEZIONE,TIPOLOGIA,GRAD) " _
                                        & "VALUES ('" & Format(Now, "yyyyMMdd") & "',5,18,'" & PG & "','" & dataPG & "',10," & idBando & ",10)"
                    PAR.cmd.ExecuteNonQuery()


                    PAR.myTrans.Commit()
                    PAR.OracleConn.Close()
                    PAR.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                    Response.Write("<script>alert('Operazione effettuata con successo!');</script>")



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


        Catch ex As Exception
            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()
            PAR.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    '********************************** MARIA fine **********************************
End Class
