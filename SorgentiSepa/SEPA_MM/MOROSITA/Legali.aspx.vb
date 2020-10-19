'GESTIONE LEGALI ESTERNI convenzionati con (accessibile solo da DIREZIONE CREDITI – TAB_FILIALI.ID=16) 
Imports System.Collections


Partial Class MOROSITA_Legali
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sValoreComune As String = ""
    Public sValoreIndirizzo As String = ""
    Public sValoreCognome As String = ""
    Public sValoreNome As String = ""
    Public sValoreCivico As String = ""

    Public sValoreTribunale As String = ""
    Public sOrdinamento As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If

        Response.Expires = 0


        If Not IsPostBack Then

            Try

                sValoreComune = Request.QueryString("CO")
                sValoreIndirizzo = Request.QueryString("IN")

                sValoreCognome = Request.QueryString("CG")
                sValoreNome = Request.QueryString("NM")
                sValoreCivico = Request.QueryString("CI")

                sValoreTribunale = Request.QueryString("TR")

                sOrdinamento = Request.QueryString("ORD")

                vIdLegale = 0
                vIdLegale = Session.Item("ID")


                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                Me.txtConnessione.Value = CStr(lIdConnessione)

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                End If


                CaricaTipoIndirizzi()
                CaricaComuni()
                CaricaTribunali()

                If vIdLegale <> 0 Then

                    VisualizzaDati()

                    Me.btnINDIETRO.Visible = True
                    Me.btnElimina.Visible = True

                    Me.txtindietro.Value = 0
                Else
                    Me.txtID_STRUTTURA.Value = Session.Item("ID_STRUTTURA")

                    Me.btnINDIETRO.Visible = False
                    Me.btnElimina.Visible = False

                    Me.txtindietro.Value = 1
                End If


                Dim CTRL As Control

                '*** FORM PRINCIPALE
                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                'Or Session.Item("BP_GENERALE") = "1"
                'If Session.Item("BP_OP_L") = "1" Or IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                '    Me.txtVisualizza.Value = 2 'SOLO LETTURA
                '    FrmSolaLettura()
                'End If

                '' SE l'operatore è BP_GENERALE=1 (può vedere tutte le strutture) perrò la sua struttura + diversa da quella selezionata allora la maschera è in SOLO LETTURA
                'If Session.Item("BP_GENERALE") = "1" And Me.txtID_STRUTTURA.Value <> Session.Item("ID_STRUTTURA") Then
                '    Me.txtVisualizza.Value = 2 'SOLO LETTURA
                '    FrmSolaLettura()
                'End If


                ' In solo LETTURA se :
                '   1) l'utente non è abilitato alla MOROSITA MOD_MOROSITA_SL=1
                '   2) oppure è Abilitato ma non è della struttuta 16 DIREZIONE CREDITI
                If Session.Item("MOD_MOROSITA_SL") = "1" Or Request.QueryString("X") = "1" Or (Session.Item("MOD_MOROSITA_SL") <> "1" And Session.Item("ID_STRUTTURA") <> 16 And Session.Item("BP_GENERALE") <> "1") Then
                    FrmSolaLettura()
                End If


            Catch ex As Exception

                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")

            End Try
        End If

    End Sub

    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property

    Public Property vIdLegale() As Long
        Get
            If Not (ViewState("par_IdLegale") Is Nothing) Then
                Return CLng(ViewState("par_IdLegale"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IdLegale") = value
        End Set

    End Property


    'CARICO COMBO TIPO INDIRIZZI
    Private Sub CaricaTipoIndirizzi()
        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Me.cmdTipoIndirizzo.Items.Clear()
            Me.cmdTipoIndirizzo.Items.Add(New ListItem(" ", -1))


            par.cmd.CommandText = "select ID,trim(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.TIPI_INDIRIZZO order by DESCRIZIONE ASC"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read
                Me.cmdTipoIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), ""), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    'CARICO COMBO COMUNI
    Private Sub CaricaComuni()

        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Me.cmbComune.Items.Clear()
            Me.cmbComune.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select COD,trim(NOME) as NOME from SEPA.COMUNI_NAZIONI where CAP IS NOT NULL order by NOME ASC"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbComune.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), ""), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    'CARICO COMBO TRIBUNALI DI COMPETENZA
    Private Sub CaricaTribunali()
        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Me.cmbTribunali.Items.Clear()
            Me.cmbTribunali.Items.Add(New ListItem(" ", -1))


            par.cmd.CommandText = "select TAB_TRIBUNALI_COMPETENTI.ID, T_COMUNI.NOME as COMUNE,TAB_TRIBUNALI_COMPETENTI.COMPETENZA  " _
                               & " from SISCOM_MI.TAB_TRIBUNALI_COMPETENTI,SEPA.T_COMUNI " _
                               & " where TAB_TRIBUNALI_COMPETENTI.COD_COMUNE=T_COMUNI.COD " _
                               & " order by COMUNE ASC"


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbTribunali.Items.Add(New ListItem(par.IfNull(myReader1("COMPETENZA"), "") & " - " & par.IfNull(myReader1("COMUNE"), ""), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Dim sStr1 As String

        Me.txtCognome.Text = par.IfNull(myReader1("COGNOME"), "")
        Me.txtNome.Text = par.IfNull(myReader1("NOME"), "")

        Me.cmdTipoIndirizzo.Items.FindByText(par.IfNull(myReader1("TIPO_INDIRIZZO"), "VIA")).Selected = True
        Me.txtIndirizzo.Text = par.IfNull(myReader1("INDIRIZZO"), "")
        Me.txtCivico.Text = par.IfNull(myReader1("CIVICO"), "")

        Me.cmbComune.Items.FindByValue(par.IfNull(myReader1("COD_COMUNE"), "-1")).Selected = True

        Me.txtCAP.Text = par.IfNull(myReader1("CAP"), "")
        Me.txtTelefono1.Text = par.IfNull(myReader1("TEL_1"), "")
        Me.txtTelefono2.Text = par.IfNull(myReader1("TEL_2"), "")
        Me.txtCell.Text = par.IfNull(myReader1("CELL"), "")
        Me.txtFax.Text = par.IfNull(myReader1("FAX"), "")
        Me.txtMail.Text = par.IfNull(myReader1("EMAIL"), "")

        Me.txtNote.Text = par.IfNull(myReader1("NOTE"), "")

        Me.cmbTribunali.SelectedValue = par.IfNull(myReader1("ID_TRIBUNALI_COMPETENTI"), "-1")


        Dim vIdMorosita As Long = 11

        'sStr1 = "select  UNITA_IMMOBILIARI.COD_TIPOLOGIA," _
        '            & " INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO""," _
        '            & " INDIRIZZI.CIVICO,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
        '            & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE ,"

        ''If par.IfEmpty(Request.QueryString("CHIAMANTE"), "") = "REPORT" Then
        'sStr1 = sStr1 & " '' as  MOROSITA ,"
        'Else
        ' sStr1 = sStr1 & " replace(replace('<a href=£javascript:void(0)£ onclick=£location.replace(''MorositaInquilino.aspx?ID=" & vIdMorosita & "$X=0" & "$CO=" & sValoreComplesso & "$ED=" & sValoreEdificio & "$IN=" & par.VaroleDaPassare(sValoreIndirizzo) & "$CI=" & par.VaroleDaPassare(sValoreCivico) & "$TI=" & sValoreTI & "$DAL=" & sValoreData_Dal & "$AL=" & sValoreData_Al & "$CD=" & par.VaroleDaPassare(sValoreCodice) & "$CG=" & par.VaroleDaPassare(sValoreCognome) & "$NM=" & par.VaroleDaPassare(sValoreNome) & "$ORD=" & sOrdinamento & "$CON='||RAPPORTI_UTENZA.ID||'" & "$ANA='||ANAGRAFICA.ID||''');£>Dettagli</a>','$','&'),'£','" & Chr(34) & "') as  MOROSITA ,"
        'End If

        'sStr1 = sStr1 & "RAPPORTI_UTENZA.ID,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC," _
        '            & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CONTABILITA/DatiUtenza.aspx?C=RisUtenza&IDANA='||ANAGRAFICA.ID||'&IDCONT='||RAPPORTI_UTENZA.ID||''',''DatiUtente'',''top=0,left=0'');£>'||" _
        '                & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
        '                                            & "     then  RAGIONE_SOCIALE " _
        '                                            & "     else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
        '            & " ||'</a>','$','&'),'£','" & Chr(34) & "') as  INTESTATARIO ," _
        '            & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
        '            & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
        '            & "     then  trim(RAGIONE_SOCIALE) " _
        '            & "     else  RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
        '  & " from  " _
        '        & " SISCOM_MI.COMPLESSI_IMMOBILIARI," _
        '        & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE," _
        '        & " SISCOM_MI.RAPPORTI_UTENZA, " _
        '        & " SISCOM_MI.ANAGRAFICA," _
        '        & " SISCOM_MI.INDIRIZZI," _
        '        & " SISCOM_MI.EDIFICI," _
        '        & " SISCOM_MI.UNITA_CONTRATTUALE," _
        '        & " SISCOM_MI.UNITA_IMMOBILIARI," _
        '        & " SISCOM_MI.SOGGETTI_CONTRATTUALI " _
        '& " where  " _
        '& "       EDIFICI.ID_COMPLESSO                      =COMPLESSI_IMMOBILIARI.ID " _
        '& "  and  UNITA_IMMOBILIARI.ID_EDIFICIO             =EDIFICI.ID" _
        '& "  and  UNITA_IMMOBILIARI.ID_INDIRIZZO            =INDIRIZZI.ID (+) " _
        '& "  and  UNITA_CONTRATTUALE.ID_UNITA               =UNITA_IMMOBILIARI.ID " _
        '& "  and  UNITA_CONTRATTUALE.ID_CONTRATTO           =RAPPORTI_UTENZA.ID " _
        '& "  and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID " _
        '& "  and  RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  =TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
        '& "  and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID" _
        '& "  and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'  " _
        '& "  and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE     is null  " _
        '& "  and RAPPORTI_UTENZA.ID in ( select ID_CONTRATTO " _
        '                             & " from SISCOM_MI.MOROSITA_LETTERE " _
        '                             & " where ID in (select distinct(ID_MOROSITA_LETTERA1) as ID " _
        '                                          & " from SISCOM_MI.MOROSITA_LEGALI_PRATICHE " _
        '                                          & " where ID_MOROSITA_LETTERA1 is not null " _
        '                                          & "  and ID_LEGALE=" & vIdLegale _
        '                                          & " union select distinct(ID_MOROSITA_LETTERA2) as ID " _
        '                                          & " from SISCOM_MI.MOROSITA_LEGALI_PRATICHE " _
        '                                          & " where ID_MOROSITA_LETTERA2 is not null " _
        '                                          & "  and ID_LEGALE=" & vIdLegale & ") )" _
        '& "  order by INTESTATARIO2"



        '***************
        sStr1 = "select distinct MOROSITA_LETTERE.ID_ANAGRAFICA," _
                & " MOROSITA_LETTERE.ID_MOROSITA,MOROSITA_LETTERE.ID_CONTRATTO," _
                & " MOROSITA_LEGALI_PRATICHE.ID_MOROSITA_LETTERA1,MOROSITA_LEGALI_PRATICHE.ID_MOROSITA_LETTERA2, " _
                & " MOROSITA_LEGALI_PRATICHE.ID_TIPO_AZIONE_LEGALE, " _
                & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../Contratti/Contratto.aspx?LT=1$ID='||RAPPORTI_UTENZA.ID||''',''Contratto'',''height=780,width=1160'');£>'||RAPPORTI_UTENZA.COD_CONTRATTO||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_CONTRATTO," _
                & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CONTABILITA/DatiUtenza.aspx?C=RisUtenza&IDANA='||ANAGRAFICA.ID||'&IDCONT='||RAPPORTI_UTENZA.ID||''',''DatiUtente'',''top=0,left=0'');£>'||" _
                & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                    & "     then  RAGIONE_SOCIALE " _
                    & "     else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                    & " ||'</a>','$','&'),'£','" & Chr(34) & "') as  INTESTATARIO ," _
                & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                    & "     then  trim(RAGIONE_SOCIALE) " _
                    & "     else  RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2, " _
                & " RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC," _
                & " INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO"",INDIRIZZI.CIVICO," _
                & " (SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                & " MOROSITA_LEGALI_PRATICHE.PROTOCOLLO, " _
                & " TO_CHAR(TO_DATE(MOROSITA_LEGALI_PRATICHE.DATA_APERTURA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_APERTURA"", " _
                & " TO_CHAR(TO_DATE(MOROSITA_LEGALI_PRATICHE.DATA_CHIUSURA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_CHIUSURA"", " _
                & " MOROSITA_LEGALI_PRATICHE.NOTE as NOTE_PRATICA, " _
                & " TAB_EVENTI_MOROSITA.DESCRIZIONE as ""EVENTO"" " _
        & " from SISCOM_MI.MOROSITA_LEGALI_PRATICHE," _
             & " SISCOM_MI.MOROSITA_LETTERE," _
             & " SISCOM_MI.RAPPORTI_UTENZA," _
             & " SISCOM_MI.ANAGRAFICA, " _
             & " SISCOM_MI.UNITA_CONTRATTUALE, " _
             & " SISCOM_MI.UNITA_IMMOBILIARI, " _
             & " SISCOM_MI.INDIRIZZI, " _
             & " SISCOM_MI.TAB_EVENTI_MOROSITA " _
        & " where  ((MOROSITA_LEGALI_PRATICHE.ID_MOROSITA_LETTERA1 is not null  and ID_LEGALE=" & vIdLegale & ") or " _
                & " (MOROSITA_LEGALI_PRATICHE.ID_MOROSITA_LETTERA2 is not null  and ID_LEGALE=" & vIdLegale & ") ) " _
          & " and (MOROSITA_LETTERE.ID=MOROSITA_LEGALI_PRATICHE.ID_MOROSITA_LETTERA1 or MOROSITA_LETTERE.ID=MOROSITA_LEGALI_PRATICHE.ID_MOROSITA_LETTERA2) " _
          & " and  RAPPORTI_UTENZA.ID            =MOROSITA_LETTERE.ID_CONTRATTO " _
          & " and  UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+) " _
          & " and  UNITA_CONTRATTUALE.ID_UNITA   =UNITA_IMMOBILIARI.ID " _
          & " and  UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
          & " and  UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE     is null  " _
          & " and  TAB_EVENTI_MOROSITA.COD  =MOROSITA_LETTERE.COD_STATO " _
          & " and  MOROSITA_LETTERE.ID_ANAGRAFICA=ANAGRAFICA.ID  " _
          & "  order by INTESTATARIO2"

        '******************************




        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStr1, par.OracleConn)
        Dim ds As New Data.DataTable()

        da.Fill(ds)

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        da.Dispose()
        ds.Dispose()


        '****** CONTEGGIO PRATICHE
        par.cmd.CommandText = "select count(*) from SISCOM_MI.MOROSITA_LEGALI_PRATICHE where ID_LEGALE=" & vIdLegale & " and DATA_CHIUSURA is  null"

        Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderT.Read Then
            Me.txtInCorso.Text = par.IfNull(myReaderT(0), 0)
        End If
        myReaderT.Close()

        par.cmd.CommandText = "select count(*) from SISCOM_MI.MOROSITA_LEGALI_PRATICHE where ID_LEGALE=" & vIdLegale & " and DATA_CHIUSURA is  not null"

        myReaderT = par.cmd.ExecuteReader()
        If myReaderT.Read Then
            Me.txtChiuse.Text = par.IfNull(myReaderT(0), 0)
        End If
        myReaderT.Close()


    End Sub


    Private Sub VisualizzaDati()
        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim ds As New Data.DataSet()

        Try

            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            If vIdLegale <> 0 Then
                ' LEGGO MOROSITA_LEGALI
                par.cmd.CommandText = "select * from SISCOM_MI.MOROSITA_LEGALI  where ID=" & vIdLegale & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                Session.Add("LAVORAZIONE", "1")
            End If


        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Scheda Legale aperto da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                ' LEGGO IL RECORD IN SOLO LETTURA

                par.cmd.CommandText = "select * from SISCOM_MI.MOROSITA_LEGALI where ID=" & vIdLegale
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read() Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                Me.txtVisualizza.Value = 2 'SOLO LETTURA
                FrmSolaLettura()

            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If ControlloCampi() = False Then
            Exit Sub
        End If

        If vIdLegale = 0 Then
            Me.Salva()
        Else
            Me.Update()
        End If

    End Sub

    Public Function ControlloCampi() As Boolean

        ControlloCampi = True

        If par.IfEmpty(Me.txtCognome.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire il Cognome!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If par.IfEmpty(Me.txtCognome.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire il Nome!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If par.IfEmpty(Me.cmbTribunali.Text, "-1") = "-1" Then
            Response.Write("<script>alert('Selezionare il Tribunale di Competenza!');</script>")
            ControlloCampi = False
            Exit Function
        End If

    End Function


    Private Sub Salva()

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


            ' '' Ricavo vIdLegale
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select SISCOM_MI.SEQ_MOROSITA_LEGALI.NEXTVAL FROM dual "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                vIdLegale = myReader1(0)
            End If
            myReader1.Close()
            par.cmd.CommandText = ""


            ' MOROSITA_LEGALI
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_LEGALI " _
                                        & " (ID,COGNOME,NOME,TIPO_INDIRIZZO,INDIRIZZO,CIVICO,COD_COMUNE,CAP,TEL_1,TEL_2,CELL,FAX,EMAIL,NOTE,ID_TRIBUNALI_COMPETENTI) " _
                                & "values (:id,:cognome,:nome,:tipo_indirizzo,:indirizzo,:civico,:cod_comune,:cap,:tel1,:tel2,:cell,:fax,:mail,:note,:id_trubunali)"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdLegale))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cognome", Strings.Left(Me.txtCognome.Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("nome", Strings.Left(Me.txtNome.Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_indirizzo", par.IfEmpty(Me.cmdTipoIndirizzo.SelectedItem.ToString, "VIA")))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("indirizzo", Strings.Left(Me.txtIndirizzo.Text, 100)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("civico", Strings.Left(Me.txtCivico.Text, 15)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_comune", Me.cmbComune.SelectedValue))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cap", Strings.Left(Me.txtCAP.Text, 5)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tel1", Strings.Left(Me.txtTelefono2.Text, 20)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tel2", Strings.Left(Me.txtTelefono2.Text, 20)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cell", Strings.Left(Me.txtCell.Text, 20)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fax", Strings.Left(Me.txtFax.Text, 20)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("mail", Strings.Left(Me.txtMail.Text, 50)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(Me.txtNote.Text, 500)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_trubunali", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbTribunali.SelectedValue))))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '**********************


            ''****Scrittura evento EVENTI_MOROSITA_LEGALI
            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_MOROSITA_LEGALI " _
                                        & " (ID_MOROSITA_LEGALI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                               & " values ( " & vIdLegale & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','')"
            par.cmd.ExecuteNonQuery()


            ' COMMIT
            par.myTrans.Commit()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()



            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select * from SISCOM_MI.MOROSITA_LEGALI  where ID = " & vIdLegale & " FOR UPDATE NOWAIT"
            myReader1 = par.cmd.ExecuteReader()
            myReader1.Close()



            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")

            Me.btnElimina.Visible = True

            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Me.txtModificato.Value = "0"




        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Private Sub Update()

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " update SISCOM_MI.MOROSITA_LEGALI " _
                               & " set COGNOME=:cognome,NOME=:nome,TIPO_INDIRIZZO=:tipo_indirizzo," _
                               & "     INDIRIZZO=:indirizzo,CIVICO=:civico,COD_COMUNE=:cod_comune," _
                               & "     CAP=:cap,TEL_1=:tel1,TEL_2=:tel2,CELL=:cell,FAX=:fax,EMAIL=:mail,NOTE=:note,ID_TRIBUNALI_COMPETENTI=:id_trubunali " _
                               & " where ID=" & vIdLegale


            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cognome", Strings.Left(Me.txtCognome.Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("nome", Strings.Left(Me.txtNome.Text, 50)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_indirizzo", par.IfEmpty(Me.cmdTipoIndirizzo.SelectedItem.ToString, "VIA")))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("indirizzo", Strings.Left(Me.txtIndirizzo.Text, 100)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("civico", Strings.Left(Me.txtCivico.Text, 15)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_comune", Me.cmbComune.SelectedValue))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cap", Strings.Left(Me.txtCAP.Text, 5)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tel1", Strings.Left(Me.txtTelefono2.Text, 20)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tel2", Strings.Left(Me.txtTelefono2.Text, 20)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cell", Strings.Left(Me.txtCell.Text, 20)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fax", Strings.Left(Me.txtFax.Text, 20)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("mail", Strings.Left(Me.txtMail.Text, 50)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(Me.txtNote.Text, 500)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_trubunali", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbTribunali.SelectedValue))))

            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()

            Me.txtStatoPagamento.Value = 1 'EMESSO non STAMPATO



            ''****Scrittura evento EVENTI_MOROSITA_LEGALI
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_MOROSITA_LEGALI " _
                                        & " (ID_MOROSITA_LEGALI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                               & " values ( " & vIdLegale & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','')"
            par.cmd.ExecuteNonQuery()



            par.myTrans.Commit() 'COMMIT
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()




            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")


            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Me.txtModificato.Value = "0"


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        If Me.txtModificato.Value <> "111" Then

            'CHIUSURA DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()
            '**************************


            If x.Value = "1" Then
                Response.Write("<script language='javascript'> { self.close(); }</script>")
            Else
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            End If
        Else
            Me.txtModificato.Value = "1"
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        End If

    End Sub

    Protected Sub btnINDIETRO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnINDIETRO.Click

        If Me.txtModificato.Value <> "111" Then

            sValoreComune = Request.QueryString("CO")
            sValoreIndirizzo = Request.QueryString("IN")

            sValoreCognome = Request.QueryString("CG")
            sValoreNome = Request.QueryString("NM")
            sValoreCivico = Request.QueryString("CI")

            sValoreTribunale = Request.QueryString("TR")

            sOrdinamento = Request.QueryString("ORD")


            'CHIUSURA DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()
            '**************************


            If Me.txtindietro.Value = 1 Then
                If x.Value = "1" Then
                    Response.Write("<script language='javascript'> { self.close(); }</script>")
                Else
                    Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                End If
            Else
                If x.Value = "1" Then
                    Response.Write("<script language='javascript'> { self.close(); }</script>")
                Else
                    Response.Write("<script>location.replace('RisultatiLegali.aspx?CO=" & sValoreComune _
                                                                              & "&IN=" & sValoreIndirizzo _
                                                                              & "&CG=" & sValoreCognome _
                                                                              & "&NM=" & sValoreNome _
                                                                              & "&CI=" & sValoreCivico _
                                                                              & "&TR=" & sValoreTribunale _
                                                                              & "&ORD=" & sOrdinamento & "');</script>")
                End If
            End If
        Else
            Me.txtModificato.Value = "1"
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        End If

    End Sub

    Protected Sub btnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click

        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"

        Try

            If Me.txtElimina.Value = "1" Then

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "delete from SISCOM_MI.MOROSITA_LEGALI where ID = " & vIdLegale
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""


                'LOG CANCELLAZIONE
                par.cmd.Parameters.Clear()

                par.cmd.CommandText = "insert into SISCOM_MI.LOG  " _
                                        & " (ID_OPERATORE,DATA_OPERAZIONE,TIPO_OPERAZIONE,TIPO_OGGETTO," _
                                        & " COD_OGGETTO,DESCR_OGGETTO,NOTE) " _
                                 & " values (:id_operatore,:data_operazione,:tipo_operazione,:tipo_oggetto," _
                                        & ":cod_oggetto,:descrizione,:note) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_operazione", Format(Now, "yyyyMMdd")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_operazione", "CANCELLAZIONE LEGALE MOROSITA"))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "LEGALE MOROSITA"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", par.IfEmpty(vIdLegale, "")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(par.IfEmpty(txtCognome.Text, ""), 50) & " - " & Strings.Left(par.IfEmpty(txtNome.Text, ""), 50)))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", par.IfEmpty(txtCognome.Text, "")))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '****************************************


                ' COMMIT
                par.myTrans.Commit()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                Response.Write("<SCRIPT>alert('Eliminazione completata correttamente!');</SCRIPT>")


                'DA RICERCA SELETTIVA
                sValoreComune = Request.QueryString("CO")
                sValoreIndirizzo = Request.QueryString("IN")

                sValoreCognome = Request.QueryString("CG")
                sValoreNome = Request.QueryString("NM")
                sValoreCivico = Request.QueryString("CI")

                sValoreTribunale = Request.QueryString("TR")

                sOrdinamento = Request.QueryString("ORD")


                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

                Session.Item("LAVORAZIONE") = "0"

                Page.Dispose()


                If Me.txtindietro.Value = 1 Then
                    If x.Value = "1" Then
                        Response.Write("<script language='javascript'> { self.close(); }</script>")
                    Else
                        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                    End If
                Else
                    If x.Value = "1" Then
                        Response.Write("<script language='javascript'> { self.close(); }</script>")
                    Else
                        Response.Write("<script>location.replace('RisultatiLegali.aspx?CO=" & sValoreComune _
                                                                                & "&IN=" & sValoreIndirizzo _
                                                                                & "&CG=" & sValoreCognome _
                                                                                & "&NM=" & sValoreNome _
                                                                                & "&CI=" & sValoreCivico _
                                                                                & "&TR=" & sValoreTribunale _
                                                                                & "&ORD=" & sOrdinamento & "');</script>")
                    End If
                End If

            Else
                CType(Me.Page.FindControl("txtElimina"), HiddenField).Value = "0"
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"

            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub



    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function

    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Dim a As String = "Null"

        Try
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If

        Catch ex As Exception
        End Try
        Return a

    End Function

    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function



    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)
        Try

            Me.btnSalva.Visible = False
            Me.btnElimina.Visible = False


            Dim CTRL As Control = Nothing
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Enabled = False
                End If
            Next


        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub cmbComune_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComune.SelectedIndexChanged
        Dim FlagConnessione As Boolean

        Try

            If Me.cmbComune.SelectedValue <> "-1" Then

                FlagConnessione = False
                'APERURA CONNESSIONE
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                par.cmd.CommandText = "select SIGLA from SEPA.COMUNI_NAZIONI where COD= '" & Me.cmbComune.SelectedValue.ToString & "'"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.txtProvincia.Text = myReader1(0)
                End If
                myReader1.Close()

                '*********************CHIUSURA CONNESSIONE**********************
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
            Else
                Me.txtProvincia.Text = ""
            End If

        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

End Class
