Imports System.Collections

Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class MOROSITA_CreaLettere
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sValoreStrutturaAler As String
    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreIndirizzo As String
    Public sValoreCivico As String

    Public sValoreTI As String

    Public sValoreData_Dal As String
    Public sValoreData_Al As String

    Public sValoreDataRIF_Dal As String
    Public sValoreDataRIF_Al As String

    Public sValoreCodice As String
    Public sValoreCognome As String
    Public sValoreNome As String

    Public sValoreSDAL As String
    Public sValoreSAL As String

    Public sValoreImporto1 As String
    Public sValoreImporto2 As String

    Public sValoreBolScaduteDA As String
    Public sValoreBolScaduteA As String

    Public sTipoRapporto As String
    Public sTipoContr As String
    Public sStatoContratto As String

    Public sTipoRicerca As String
    Public sOrdinamento As String

    Public sStringaSqlDATA_RIF As String

    Public lstListaRapporti As System.Collections.Generic.List(Of Epifani.ListaGenerale)
    Private Function ControllaNumero() As Boolean
        ControllaNumero = False
        'Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim conta As Integer = 0
        'For Each item As DataGridItem In DataGrid1.Items
        '    chkExport = item.FindControl("CheckBox1")
        '    If chkExport.Checked = True Then
        '        conta += 1
        '    End If
        'Next
        conta = CInt(lblNumIntestatariSelezionati.Text)
        If conta <= 200 Then
            ControllaNumero = True
        End If
        Return ControllaNumero

    End Function




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If

        Response.Expires = 0

        lstListaRapporti = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE1"), System.Collections.Generic.List(Of Epifani.ListaGenerale))

        Try

            If Not IsPostBack Then

                sValoreStrutturaAler = Request.QueryString("FI")    'FILIALE (STRUTTURA)

                sValoreComplesso = Request.QueryString("CO")    'COMPLESSO
                sValoreEdificio = Request.QueryString("ED")     'EDIFICIO
                sValoreIndirizzo = Request.QueryString("IN")    'INDIRIZZO
                sValoreCivico = Request.QueryString("CI")       'CIVICO

                sValoreTI = UCase(Request.QueryString("TI"))    'TIPOLOGIA U.I.

                sValoreSDAL = Request.QueryString("DAL_S")      'DATA STIPULA DAL
                sValoreSAL = Request.QueryString("AL_S")        'DATA STIPULA AL

                sValoreDataRIF_Dal = Request.QueryString("DAL")      'DATA BOL_BOLLETTE.RIFERIMENTO_DA
                sValoreDataRIF_Al = Request.QueryString("AL")        'DATA BOL_BOLLETTE.RIFERIMENTO_A

                sValoreCodice = Request.QueryString("CD")       'CODICE RAPPORTO
                sValoreCognome = Request.QueryString("CG")      'COGNOME
                sValoreNome = Request.QueryString("NM")         'NOME


                sValoreImporto1 = Request.QueryString("IMP1")   'IMPORTO DA
                sValoreImporto2 = Request.QueryString("IMP2")   'IMPORTO A

                sValoreBolScaduteDA = Request.QueryString("BOLDA")  'BOLLETTE SCADUTE DA
                sValoreBolScaduteA = Request.QueryString("BOLA")  'BOLLETTE SCADUTE A

                sTipoRapporto = Request.QueryString("RAPP")
                sStatoContratto = Request.QueryString("ST")

                sTipoRicerca = Request.QueryString("MORA")      'TIPOLOGIA RICERCA MORA PRIMA o DOPO 30.09.2009
                sOrdinamento = Request.QueryString("ORD")       'ORDINAMENTO



                Me.txtDataProtocolloAler.Text = par.FormattaData(Format(Now, "yyyyMMdd"))

                vIdMorosita = -1

                SettaggioCampi()

                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                'CType(TabDettagli1.FindControl("txtConnessione"), TextBox).Text = CStr(lIdConnessione)
                Me.txtConnessione.Text = CStr(lIdConnessione)
                Me.txtDataScad.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                    ' HttpContext.Current.Session.Add("SESSION_MANUTENZIONI", par.OracleConn)
                End If

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "0"

                BindGrid()

                txtindietro.Text = 0


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


                btnStampa.Visible = False
                btnSalva.Visible = True

                'cmbStato.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")


                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Meteoriche.png';</script>")

                'If Session.Item("MOD_DEM_SL") = "1" Or Mid(par.IfNull(Session.Item("MOD_DEM_IMP"), "0000000000000"), 7, 1) = 0 Then
                '    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                '    FrmSolaLettura()
                'End If

            End If

        Catch ex As Exception

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


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

    Public Property vIdMorosita() As Long
        Get
            If Not (ViewState("par_vIdMorosita") Is Nothing) Then
                Return CLng(ViewState("par_vIdMorosita"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_vIdMorosita") = value
        End Set

    End Property


    Private Sub SettaggioCampi()

        Me.cmbTipoInvio.Items.Add(New ListItem("Racc. A.R.", "Racc. A.R."))
        Me.cmbTipoInvio.Items.Add(New ListItem("Racc. a mano", "Racc. mano"))
        Me.cmbTipoInvio.Items.Add(New ListItem("FAX", "FAX"))

    End Sub

    Private Sub BindGrid()
        Dim ElencoID_Rapporti As String = ""
        Dim sStringaSQL1 As String

        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        Dim ContaIntestatari As Integer = 0
        Dim TrovatiInquiliniMorosi As Integer = 0


        Try

            sOrdinamento = Request.QueryString("ORD")       'ORDINAMENTO

            Dim gen As Epifani.ListaGenerale

            For Each gen In lstListaRapporti
                If ElencoID_Rapporti <> "" Then
                    ElencoID_Rapporti = ElencoID_Rapporti & "," & gen.STR
                Else
                    ElencoID_Rapporti = gen.STR
                End If
            Next



            'DATA COMPETENZA BOLLETTA
            sStringaSqlDATA_RIF = ""
            '***********************************************************
            If Request.QueryString("FILTDATE") = 0 Then
                ' GRIGLIA ELENCO INQUILINI
                sStringaSQL1 = "select  UNITA_IMMOBILIARI.COD_TIPOLOGIA," _
                        & " INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO""," _
                        & " INDIRIZZI.CIVICO,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                        & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE ," _
                        & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../Contratti/Contratto.aspx?LT=1$ID='||RAPPORTI_UTENZA.ID||''',''Contratto'',''height=780,width=1160'');£>'||RAPPORTI_UTENZA.COD_CONTRATTO||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_CONTRATTO ," _
                        & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''ElencoAllegati.aspx?COD='||rapporti_utenza.cod_contratto||''',''Allegati'','''');£>Visualizza</a>','$','&'),'£','" & Chr(34) & "') as ALLEGATI_CONTRATTO, " _
                        & "RAPPORTI_UTENZA.ID,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC," _
                        & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CONTABILITA/DatiUtenza.aspx?C=RisUtenza&IDANA='||ANAGRAFICA.ID||'&IDCONT='||RAPPORTI_UTENZA.ID||''',''DatiUtente'' ,''height=580,top=0,left=0,width=780'');£>'||" _
                        & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                        & "     then  RAGIONE_SOCIALE " _
                        & "     else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                        & " ||'</a>','$','&'),'£','" & Chr(34) & "') as  INTESTATARIO ," _
                        & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI.SALDO,0) ,'9G999G999G999G999G990D99'))   as DEBITO2," _
                        & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI.SALDO_1,0) ,'9G999G999G999G999G990D99'))   as DEBITO_MG," _
                        & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI.SALDO_2,0) ,'9G999G999G999G999G990D99'))   as DEBITO_MA," _
                        & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                        & "NVL(SISCOM_MI.SALDI.SALDO,0)  as DEBITO, " _
                        & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                        & "     then  trim(RAGIONE_SOCIALE) " _
                        & "     else  RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                        & " from  " _
                        & " SISCOM_MI.RAPPORTI_UTENZA,  " _
                        & " SISCOM_MI.INDIRIZZI, " _
                        & " SISCOM_MI.UNITA_IMMOBILIARI,  " _
                        & " SISCOM_MI.UNITA_CONTRATTUALE,  " _
                        & " SISCOM_MI.ANAGRAFICA, " _
                        & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                        & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                        & " SISCOM_MI.SALDI " _
                        & " where  " _
                        & "      UNITA_IMMOBILIARI.ID_INDIRIZZO            =INDIRIZZI.ID (+) " _
                        & " and  UNITA_CONTRATTUALE.ID_UNITA               =UNITA_IMMOBILIARI.ID   " _
                        & " and  RAPPORTI_UTENZA.ID 					     =UNITA_CONTRATTUALE.ID_CONTRATTO " _
                        & " and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID " _
                        & " and  RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  =TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                        & " and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID  " _
                        & " and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                        & " and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE     is null   " _
                        & " and SALDI.ID_CONTRATTO=RAPPORTI_UTENZA.ID "
            Else


                sStringaSQL1 = "select  UNITA_IMMOBILIARI.COD_TIPOLOGIA," _
                               & " INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO""," _
                               & " INDIRIZZI.CIVICO,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                               & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE ," _
                               & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../Contratti/Contratto.aspx?LT=1$ID='||RAPPORTI_UTENZA.ID||''',''Contratto'',''height=780,width=1160'');£>'||RAPPORTI_UTENZA.COD_CONTRATTO||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_CONTRATTO ," _
                               & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''ElencoAllegati.aspx?COD='||rapporti_utenza.cod_contratto||''',''Allegati'','''');£>Visualizza</a>','$','&'),'£','" & Chr(34) & "') as ALLEGATI_CONTRATTO, " _
                               & "RAPPORTI_UTENZA.ID,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC," _
                               & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CONTABILITA/DatiUtenza.aspx?C=RisUtenza&IDANA='||ANAGRAFICA.ID||'&IDCONT='||RAPPORTI_UTENZA.ID||''',''DatiUtente'' ,''height=580,top=0,left=0,width=780'');£>'||" _
                               & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                               & "     then  RAGIONE_SOCIALE " _
                               & "     else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                               & " ||'</a>','$','&'),'£','" & Chr(34) & "') as  INTESTATARIO ," _
                               & " TRIM (TO_CHAR( NVL(siscom_mi.MOROSITACONTRATTO(RAPPORTI_UTENZA.ID,'" & Request.QueryString("FDAL") & "','" & Request.QueryString("FAL") & "',NULL),0) ,'9G999G999G999G999G990D99'))   as DEBITO2," _
                               & " TRIM (TO_CHAR( NVL(siscom_mi.MOROSITACONTRATTO(RAPPORTI_UTENZA.ID,'" & Request.QueryString("FDAL") & "','" & Request.QueryString("FAL") & "','G'),0) ,'9G999G999G999G999G990D99'))   as DEBITO_MG," _
                               & " TRIM (TO_CHAR( NVL(siscom_mi.MOROSITACONTRATTO(RAPPORTI_UTENZA.ID,'" & Request.QueryString("FDAL") & "','" & Request.QueryString("FAL") & "','A'),0) ,'9G999G999G999G999G990D99'))   as DEBITO_MA," _
                               & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                               & "NVL(siscom_mi.MOROSITACONTRATTO(RAPPORTI_UTENZA.ID,'" & Request.QueryString("FDAL") & "','" & Request.QueryString("FAL") & "',NULL),0)  as DEBITO, " _
                               & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                               & "     then  trim(RAGIONE_SOCIALE) " _
                               & "     else  RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                               & " from  " _
                               & " SISCOM_MI.RAPPORTI_UTENZA,  " _
                               & " SISCOM_MI.INDIRIZZI, " _
                               & " SISCOM_MI.UNITA_IMMOBILIARI,  " _
                               & " SISCOM_MI.UNITA_CONTRATTUALE,  " _
                               & " SISCOM_MI.ANAGRAFICA, " _
                               & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                               & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE " _
                               & " where  " _
                               & "      UNITA_IMMOBILIARI.ID_INDIRIZZO            =INDIRIZZI.ID (+) " _
                               & " and  UNITA_CONTRATTUALE.ID_UNITA               =UNITA_IMMOBILIARI.ID   " _
                               & " and  RAPPORTI_UTENZA.ID 					     =UNITA_CONTRATTUALE.ID_CONTRATTO " _
                               & " and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID " _
                               & " and  RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  =TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                               & " and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID  " _
                               & " and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                               & " and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE     is null   "

            End If


            If ElencoID_Rapporti <> "" Then
                sStringaSQL1 = sStringaSQL1 & "  and RAPPORTI_UTENZA.ID in (" & ElencoID_Rapporti & ")"
            Else
                sStringaSQL1 = sStringaSQL1 & "  and UNITA_IMMOBILIARI.ID in (select ID_UNITA from SISCOM_MI.BOL_BOLLETTE " _
                    & " where  (NVL(TO_DATE(BOL_BOLLETTE.DATA_SCADENZA,'yyyymmdd')- TO_DATE(SYSDATE),21) <= 30) " _
                    & "   and   FL_ANNULLATA=0) "
            End If

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1 & " ORDER BY " & sOrdinamento, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()

            For Each oDataGridItem In Me.DataGrid1.Items
                chkExport = oDataGridItem.FindControl("CheckBox1")
                chkExport.Checked = True
                ContaIntestatari = ContaIntestatari + 1
            Next

            Me.lblNumIntestatari.Text = ContaIntestatari
            Me.lblNumIntestatariSelezionati.Text = ContaIntestatari
            '############################################################################
            Dim somme As String = ""
            If Request.QueryString("FILTDATE") = 0 Then
                somme = "select  " _
                        & " sum(NVL(SISCOM_MI.SALDI.SALDO, 0)), " _
                        & " sum(NVL(SISCOM_MI.SALDI.SALDO_1, 0)), " _
                        & " sum(NVL(SISCOM_MI.SALDI.SALDO_2, 0)) " _
                        & " from  " _
                        & " SISCOM_MI.RAPPORTI_UTENZA,  " _
                        & " SISCOM_MI.INDIRIZZI, " _
                        & " SISCOM_MI.UNITA_IMMOBILIARI,  " _
                        & " SISCOM_MI.UNITA_CONTRATTUALE,  " _
                        & " SISCOM_MI.ANAGRAFICA, " _
                        & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                        & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                        & " SISCOM_MI.SALDI " _
                        & " where  " _
                        & " UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID (+) " _
                        & " and  UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID   " _
                        & " and  RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO " _
                        & " and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                        & " and  RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                        & " and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID  " _
                        & " and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                        & " and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE is null " _
                        & " and SALDI.ID_CONTRATTO=RAPPORTI_UTENZA.ID "
            Else
                somme = "select  " _
                        & " sum(NVL(siscom_mi.MOROSITACONTRATTO(RAPPORTI_UTENZA.ID,'" & Request.QueryString("FDAL") & "','" & Request.QueryString("FAL") & "',NULL), 0)), " _
                        & " sum(NVL(siscom_mi.MOROSITACONTRATTO(RAPPORTI_UTENZA.ID,'" & Request.QueryString("FDAL") & "','" & Request.QueryString("FAL") & "','G'), 0)), " _
                        & " sum(NVL(siscom_mi.MOROSITACONTRATTO(RAPPORTI_UTENZA.ID,'" & Request.QueryString("FDAL") & "','" & Request.QueryString("FAL") & "','A'), 0)) " _
                        & " from  " _
                        & " SISCOM_MI.RAPPORTI_UTENZA,  " _
                        & " SISCOM_MI.INDIRIZZI, " _
                        & " SISCOM_MI.UNITA_IMMOBILIARI,  " _
                        & " SISCOM_MI.UNITA_CONTRATTUALE,  " _
                        & " SISCOM_MI.ANAGRAFICA, " _
                        & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                        & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE " _
                        & " where  " _
                        & " UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID (+) " _
                        & " and  UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID   " _
                        & " and  RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO " _
                        & " and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                        & " and  RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                        & " and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID  " _
                        & " and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                        & " and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE is null "

            End If


            If ElencoID_Rapporti <> "" Then
                somme = somme & " and RAPPORTI_UTENZA.ID in (" & ElencoID_Rapporti & ") "
            Else
                somme = somme & " and UNITA_IMMOBILIARI.ID in (select ID_UNITA from SISCOM_MI.BOL_BOLLETTE " _
                    & " where (NVL(TO_DATE(BOL_BOLLETTE.DATA_SCADENZA,'yyyymmdd')- TO_DATE(SYSDATE),21) <= 30) " _
                    & " and FL_ANNULLATA=0) "
            End If

            par.cmd.CommandText = somme
            Dim lettoreSomme As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreSomme.Read Then
                txtImporto1.Text = Format(par.IfNull(lettoreSomme(0), 0), "#,##0.00")
                txtImporto2.Text = Format(par.IfNull(lettoreSomme(1), 0), "#,##0.00")
                txtImporto3.Text = Format(par.IfNull(lettoreSomme(2), 0), "#,##0.00")
                txtImportoSelezionato1.Text = Format(par.IfNull(lettoreSomme(0), 0), "#,##0.00")
                txtImportoSelezionato2.Text = Format(par.IfNull(lettoreSomme(1), 0), "#,##0.00")
                txtImportoSelezionato3.Text = Format(par.IfNull(lettoreSomme(2), 0), "#,##0.00")
            End If
            lettoreSomme.Close()
            '############################################################################
            Dim Trovati As Integer = 0
            'Trovati = InquiliniNoCF("DA_BINDGRID")
            If Trovati = 1 Then
                Me.imgExcel.Visible = True
                Response.Write("<SCRIPT>alert('Attenzione: Riscontrato un inquilino con C.F. errato, premere il bottone \'Elenco C.F. Errati\' per maggiori informazioni!');</SCRIPT>")
            ElseIf Trovati > 1 Then
                Me.imgExcel.Visible = True
                Response.Write("<SCRIPT>alert('Attenzione: Riscontrati " & Trovati & " inquilini con C.F. errato, premere il bottone \'Elenco C.F. Errati\' per maggiori informazioni!');</SCRIPT>")
            Else
                Me.imgExcel.Visible = False
            End If
            '*********************************************************
            Dim intestatariSelezionati As Integer = ContaIntestatari
            For Each oDataGridItem In Me.DataGrid1.Items

                chkExport = oDataGridItem.FindControl("CheckBox1")

                If chkExport.Checked Then

                    If Val(oDataGridItem.Cells(5).Text.Replace(".", "").Replace(",", ".")) < 0 Or Val(oDataGridItem.Cells(6).Text.Replace(".", "").Replace(",", ".")) < 0 Then
                        chkExport = oDataGridItem.FindControl("CheckBox1")
                        chkExport.Checked = False
                        ContaIntestatari = ContaIntestatari - 1
                        intestatariSelezionati = intestatariSelezionati - 1
                    End If
                Else
                    intestatariSelezionati = intestatariSelezionati - 1
                    txtImportoSelezionato1.Text = CDec(txtImportoSelezionato1.Text) - CDec(oDataGridItem.Cells(4).Text)
                    txtImportoSelezionato2.Text = CDec(txtImportoSelezionato2.Text) - CDec(oDataGridItem.Cells(5).Text)
                    txtImportoSelezionato3.Text = CDec(txtImportoSelezionato3.Text) - CDec(oDataGridItem.Cells(6).Text)
                End If
            Next

            Me.lblNumIntestatari.Text = ContaIntestatari
            Me.lblNumIntestatariSelezionati.Text = intestatariSelezionati
            txtImportoSelezionato1.Text = Format(CDec(txtImportoSelezionato1.Text), "#,##0.00")
            txtImportoSelezionato2.Text = Format(CDec(txtImportoSelezionato2.Text), "#,##0.00")
            txtImportoSelezionato3.Text = Format(CDec(txtImportoSelezionato3.Text), "#,##0.00")
            'CONTROLLO SE CI SONO INQUILINI CHE HANNO GIA AVUTO UNA MOROSITA
            par.cmd.CommandText = "select count(distinct id_contratto) from siscom_mi.morosita_lettere where id_contratto in (" & ElencoID_Rapporti & ") and cod_stato <> 'M98'"
            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderT.Read Then
                If par.IfNull(myReaderT(0), 0) > 0 Then
                    TrovatiInquiliniMorosi = par.IfNull(myReaderT(0), 0)
                End If
            End If
            myReaderT.Close()
            '*****************************


            If TrovatiInquiliniMorosi > 0 Then
                If TrovatiInquiliniMorosi = 1 Then
                    Response.Write("<SCRIPT>alert('Attenzione! \n Riscontrato un inquilino che ha già ricevuto la lettera di morosità, premere \'Elenco già Morosi\' per maggiori informazioni!');</SCRIPT>")
                Else
                    Response.Write("<SCRIPT>alert('Attenzione...\n Riscontrati " & TrovatiInquiliniMorosi & " inquilini che hanno già ricevuto la lettera di morosità, premere \'Elenco già Morosi\' per maggiori informazioni!');</SCRIPT>")
                End If
                Me.imgInquinili.Visible = True
            End If


            'DATA DI RIFERIMENTO
            'If sValoreDataRIF_Dal = "" Or sValoreDataRIF_Al = "" Then
            par.cmd.Parameters.Clear()
            '07/01/2014 AGGIUNTA MODIFICA IMPORTO_RUOLO = 0
            par.cmd.CommandText = "select MIN(RIFERIMENTO_DA) " _
                               & " from SISCOM_MI.BOL_BOLLETTE " _
                               & " where ID_CONTRATTO in (" & ElencoID_Rapporti & ")" _
                               & "   and FL_ANNULLATA=0 " _
                               & "   and ID_BOLLETTA_RIC is null  and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                               & "   and BOL_BOLLETTE.ID_TIPO not in (4,5,7)  " _
                               & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                               & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                'If sValoreDataRIF_Dal = "" Then
                If Not String.IsNullOrEmpty(Request.QueryString("DAL").ToString) Then
                    lbl_RIF_DA.Text = par.FormattaData(Request.QueryString("DAL").ToString)
                Else
                    lbl_RIF_DA.Text = par.FormattaData(par.IfNull(myReader1(0), ""))

                End If

                'End If
                If Not String.IsNullOrEmpty(Request.QueryString("FAL").ToString) Then
                    lbl_RIF_A.Text = par.FormattaData(Request.QueryString("FAL").ToString)
                Else
                    lbl_RIF_A.Text = par.FormattaData(Format(Now, "yyyyMMdd"))
                End If
                'If sValoreDataRIF_Al = "" Then
                'End If
            End If



            myReader1.Close()
            par.cmd.CommandText = "select valore from sepa.parameter where id = 120"
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                Me.txtProFis.Text = par.IfNull(myReader1(0), "BA0301/NUMI")
            Else
                Me.txtProFis.Text = "BA0301/NUMI"
            End If
            myReader1.Close()
            'Else
            'lbl_RIF_DA.Text = par.FormattaData(par.IfNull(sValoreDataRIF_Dal, ""))
            'lbl_RIF_A.Text = par.FormattaData(par.IfNull(sValoreDataRIF_Al, ""))
            'End If

            'par.OracleConn.Open()
            'par.SettaCommand(par)

            'par.cmd.CommandText = sStringaSQL2
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader.Read() Then
            '    Label4.Text = "(" & DataGrid1.Items.Count & " nella pagina - Totale :" & ds.Tables(0).Rows.Count & ") in " & myReader(0) & " Rapporti"
            'End If
            'myReader.Close()

            'par.cmd.Dispose()
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            da.Dispose()
            dt.Dispose()
            calcolaSomme()
        Catch ex As Exception
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub



    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        If txtModificato.Text <> "111" Then


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

            'Response.Write("<script>parent.funzioni.Form1.Image3.src='../../../NuoveImm/Titolo_IMPIANTI.png';</script>")
            Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
        Else
            txtModificato.Text = "1"

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub

    Protected Sub btnINDIETRO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnINDIETRO.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")

            sValoreStrutturaAler = Request.QueryString("FI")    'FILIALE (STRUTTURA)

            sValoreComplesso = Request.QueryString("CO")    'COMPLESSO
            sValoreEdificio = Request.QueryString("ED")     'EDIFICIO
            sValoreIndirizzo = Request.QueryString("IN")    'INDIRIZZO
            sValoreCivico = Request.QueryString("CI")       'CIVICO

            sValoreTI = UCase(Request.QueryString("TI"))    'TIPOLOGIA U.I.

            sValoreSDAL = Request.QueryString("DAL_S")      'DATA STIPULA DAL
            sValoreSAL = Request.QueryString("AL_S")        'DATA STIPULA AL

            sValoreDataRIF_Dal = Request.QueryString("DAL")      'DATA BOL_BOLLETTE.RIFERIMENTO_DA
            sValoreDataRIF_Al = Request.QueryString("AL")        'DATA BOL_BOLLETTE.RIFERIMENTO_A


            sValoreCodice = Request.QueryString("CD")       'CODICE RAPPORTO
            sValoreCognome = Request.QueryString("CG")      'COGNOME
            sValoreNome = Request.QueryString("NM")         'NOME


            sValoreImporto1 = Request.QueryString("IMP1")   'IMPORTO DA
            sValoreImporto2 = Request.QueryString("IMP2")   'IMPORTO A

            sValoreBolScaduteDA = Request.QueryString("BOLDA")  'BOLLETTE SCADUTE DA
            sValoreBolScaduteA = Request.QueryString("BOLA")  'BOLLETTE SCADUTE A

            sTipoRapporto = Request.QueryString("RAPP")
            sTipoContr = Request.QueryString("TIPOCONTR")
            sStatoContratto = Request.QueryString("ST")

            sTipoRicerca = Request.QueryString("MORA")      'TIPOLOGIA RICERCA MORA PRIMA o DOPO 30.09.2009
            sOrdinamento = Request.QueryString("ORD")       'ORDINAMENTO



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


            'Response.Write("<script>parent.funzioni.Form1.Image3.src='../../../NuoveImm/Titolo_IMPIANTI.png';</script>")

            If txtindietro.Text = 1 Then
                Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            Else
                Dim filtdate As String = ""

                If Request.QueryString("FILTDATE") = 0 Then
                    filtdate = "&FILTDATE=0"
                Else
                    filtdate = "&FILTDATE=1"

                End If

                Response.Write("<script>location.replace('RisultatiDebitori.aspx?FI=" & sValoreStrutturaAler _
                    & "&CO=" & sValoreComplesso _
                    & "&ED=" & sValoreEdificio _
                    & "&IN=" & par.VaroleDaPassare(sValoreIndirizzo) _
                    & "&CI=" & par.VaroleDaPassare(sValoreCivico) _
                    & "&CG=" & par.VaroleDaPassare(sValoreCognome) _
                    & "&NM=" & par.VaroleDaPassare(sValoreNome) _
                    & "&CD=" & par.VaroleDaPassare(sValoreCodice) _
                    & "&TI=" & sValoreTI _
                    & "&DAL_S=" & sValoreSDAL _
                    & "&AL_S=" & sValoreSAL _
                    & "&DAL=" & sValoreDataRIF_Dal _
                    & "&AL=" & sValoreDataRIF_Al _
                    & "&IMP1=" & sValoreImporto1 _
                    & "&IMP2=" & sValoreImporto2 _
                    & "&BOLDA=" & sValoreBolScaduteDA _
                    & "&BOLA=" & sValoreBolScaduteA _
                    & "&RAPP=" & sTipoRapporto _
                    & "&TIPOCONTR=" & sTipoContr _
                    & "&ST=" & sStatoContratto _
                    & "&MORA=" & sTipoRicerca _
                    & "&ORD=" & sOrdinamento & filtdate _
                    & "');</script>")
            End If

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub


    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If ControllaNumero() = False Then
            Response.Write("<script>alert('Selezionare al massimo 200 inquilini!')</script>")
            Exit Sub
        End If
        If String.IsNullOrEmpty(Me.txtDataScad.Text) Then
            Response.Write("<script>alert('Definire la data scadenza!')</script>")
            Exit Sub
        End If

        Dim sStr1 As String
        'Dim ElencoID_Rapporti As String = ""
        Dim ValSomma As Decimal = 0

        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

        Dim vIdMorositaLettera As Long
        Dim sDataDA As String = ""
        Dim FlagConnessione As Boolean

        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        Dim sListRapporti As String = ""

        Try

            FlagConnessione = False


            If ControlloCampi() = False Then
                Exit Sub
            End If


            If Me.txtAnnullo.Value = "1" Then

                Me.txtAnnullo.Value = "0"

                FrmSolaLettura()

                'sValoreDataRIF_Dal = Request.QueryString("DAL")      'DATA BOL_BOLLETTE.RIFERIMENTO_DA
                'sValoreDataRIF_Al = Request.QueryString("AL")        'DATA BOL_BOLLETTE.RIFERIMENTO_A


                'DATA COMPETENZA BOLLETTA
                sStringaSqlDATA_RIF = ""
                'If sValoreDataRIF_Dal <> "" Then
                '    If sValoreDataRIF_Al <> "" Then
                '        sStringaSqlDATA_RIF = " and ((BOL_BOLLETTE.RIFERIMENTO_DA>='" & sValoreDataRIF_Dal & "' and BOL_BOLLETTE.RIFERIMENTO_DA<='" & sValoreDataRIF_Al & "') " _
                '                             & " and (BOL_BOLLETTE.RIFERIMENTO_A>='" & sValoreDataRIF_Dal & "' and BOL_BOLLETTE.RIFERIMENTO_A<='" & sValoreDataRIF_Al & "'))"

                '    Else
                '        sStringaSqlDATA_RIF = " and (BOL_BOLLETTE.RIFERIMENTO_DA>='" & sValoreDataRIF_Dal & "' " _
                '                             & " and BOL_BOLLETTE.RIFERIMENTO_A>='" & sValoreDataRIF_Dal & "')"

                '    End If
                'ElseIf sValoreDataRIF_Al <> "" Then
                '    sStringaSqlDATA_RIF = " and (BOL_BOLLETTE.RIFERIMENTO_DA<='" & sValoreDataRIF_Al & "' " _
                '                         & " and BOL_BOLLETTE.RIFERIMENTO_A<='" & sValoreDataRIF_Al & "')"
                'End If
                '***********************************************************


                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                FlagConnessione = True


                '1) INSERT MOROSITA
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_MOROSITA.NEXTVAL FROM DUAL"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    vIdMorosita = myReader1(0)
                End If
                myReader1.Close()

                'TITOLO_DIRIGENTE, COGNOME_DIRIGENTE, NOME_DIRIGENTE, TELEFONO_DIRIGENTE, 
                'TITOLO_RESPONSABILE, COGNOME_RESPONSABILE, NOME_RESPONSABILE, TELEFONO_RESPONSABILE, 
                'TITOLO_TRATTATA, COGNOME_TRATTATA, NOME_TRATTATA, TELEFONO_TRATTATA

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " insert into SISCOM_MI.MOROSITA " _
                                             & " (ID,DATA_CREAZIONE,PROTOCOLLO_ALER,DATA_PROTOCOLLO,TIPO_INVIO,RIF_DA,RIF_A,NOTE)" _
                                    & " values (:id,:data_creazione,:protocollo,:data_protocollo,:tipo_invio,:rif_da,:rif_a,:note)"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdMorosita))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_creazione", Format(Now, "yyyyMMdd")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("protocollo", Me.txtProtocollo.Text))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_protocollo", par.IfEmpty(par.AggiustaData(Me.txtDataProtocolloAler.Text), "")))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_invio", Me.cmbTipoInvio.SelectedValue.ToString))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rif_da", par.AggiustaData(Me.lbl_RIF_DA.Text)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rif_a", par.AggiustaData(Me.lbl_RIF_A.Text)))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Me.txtNote.Text))

                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("titoloD", Me.cmbTitoloD.SelectedValue.ToString))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("CognomeD", Me.txtCognomeD.Text))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("NomeD", Me.txtNomeD.Text))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("TelD", Me.txtTelefonoD.Text))

                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("titoloR", Me.cmbTitoloR.SelectedValue.ToString))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("CognomeR", Me.txtCognomeR.Text))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("NomeR", Me.txtNomeR.Text))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("TelR", Me.txtTelefonoR.Text))

                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("titoloT", Me.cmbTitoloT.SelectedValue.ToString))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("CognomeT", Me.txtCognomeT.Text))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("NomeT", Me.txtNomeT.Text))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("TelT", Me.txtTelefonoT.Text))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '*****************************************


                'Dim gen As Epifani.ListaGenerale
                'For Each gen In lstListaRapporti
                '    If ElencoID_Rapporti <> "" Then
                '        ElencoID_Rapporti = ElencoID_Rapporti & "," & gen.STR
                '    Else
                '        ElencoID_Rapporti = gen.STR
                '    End If
                'Next

                For Each oDataGridItem In Me.DataGrid1.Items
                    chkExport = oDataGridItem.FindControl("CheckBox1")
                    If chkExport.Checked = True Then

                        If sListRapporti <> "" Then
                            sListRapporti = sListRapporti & "," & oDataGridItem.Cells(0).Text
                        Else
                            sListRapporti = oDataGridItem.Cells(0).Text
                        End If

                    End If
                Next


                '& "   and (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' or SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT')" _

                If Request.QueryString("FILTDATE") = 0 Then
                    sStr1 = "select ANAGRAFICA.ID as ID_ANAGRAFICA, RAPPORTI_UTENZA.ID as ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR,SALDI.SALDO_1,SALDI.SALDO_2 " _
                         & " from   SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.SALDI " _
                         & " where  RAPPORTI_UTENZA.ID in (" & sListRapporti & ")" _
                         & "   and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID " _
                         & "   and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID  " _
                         & "   and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                         & "   and  RAPPORTI_UTENZA.ID=SALDI.ID_CONTRATTO " _
                         & " order by ID_CONTRATTO	"
                Else
                    sStr1 = "select ANAGRAFICA.ID as ID_ANAGRAFICA, RAPPORTI_UTENZA.ID as ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR," _
                         & "siscom_mi.MOROSITACONTRATTO(RAPPORTI_UTENZA.ID,'" & Request.QueryString("FDAL") & "','" & Request.QueryString("FAL") & "','G') as SALDO_1,siscom_mi.MOROSITACONTRATTO(RAPPORTI_UTENZA.ID,'" & Request.QueryString("FDAL") & "','" & Request.QueryString("FAL") & "','A') AS SALDO_2 " _
                         & " from   SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI " _
                         & " where  RAPPORTI_UTENZA.ID in (" & sListRapporti & ")" _
                         & "   and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID " _
                         & "   and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID  " _
                         & "   and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                         & " order by ID_CONTRATTO	"

                End If


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1
                myReader1 = par.cmd.ExecuteReader()

                While myReader1.Read
                    'Per ogni riga di RAPPORTO_UTENZA ID_CONTRATTO:
                    '1) select BOL_BOLLETTE x ID_CONTRATTO + altre OPZIONI
                    '2) somma1 di tutte le BOL_BOLLETTE_VOCI.IMPORTO  x ogni BOLLETTA
                    '3) sommaTOT=somma1- somma2 di BOL_BOLLETTE.IMPORTO_PAGATO
                    '4) insert MOROSITA_DETT
                    '5) insert MOROSITA_LETTERE

                    '(1)
                    ValSomma = 0
                    '1) SOMMA IMPORTO DA DARE (dare - avere)
                    If par.IfNull(myReader1("SALDO_1"), 0) > 0 And par.IfNull(myReader1("SALDO_2"), 0) > 0 Then
                        'If par.IfNull(myReader1("N_BOL_SCADUTE_1"), 0) > 0 And par.IfNull(myReader1("N_BOL_SCADUTE_2"), 0) > 0 Then
                        'If par.IfNull(myReader1("FL_MOR"), 0) = 1 Then
                        '2 MAV
                        'LETTERA 1 PRIMA DEL 30.09.2009
                        '& "   and RIFERIMENTO_DA<=20090930 " 
                        'sStr1 = "select SUM( NVL(BOL_BOLLETTE_VOCI.IMPORTO, 0) - NVL(BOL_BOLLETTE_VOCI.IMP_PAGATO, 0) ) " _
                        '     & " from SISCOM_MI.BOL_BOLLETTE_VOCI " _
                        '     & " where ID_BOLLETTA in (select  ID " _
                        '                           & " from SISCOM_MI.BOL_BOLLETTE " _
                        '                           & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                        '                           & "   and ID<0 " _
                        '                           & "   and FL_ANNULLATA=0 " _
                        '                           & "   and  ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) )>0 " _
                        '                           & "   and ID_BOLLETTA_RIC is null  " _
                        '                           & "   and   (BOL_BOLLETTE.ID_TIPO<>4 or BOL_BOLLETTE.ID_TIPO is null) " _
                        '                           & "   and   NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0)>0 " _
                        '                           & "   and BOL_BOLLETTE.ID not in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SINDACATI) " & sStringaSqlDATA_RIF & ")" _
                        '    & " and ID_VOCE not in (select ID from SISCOM_MI.T_VOCI_BOLLETTA where COMPETENZA=3)"


                        '& "  Al posto  and DATA_PAGAMENTO is null " _
                        '& " and  ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) )>0 " _
                        '& "   and ID in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SOLLECITI) )"

                        '07/01/2014 AGGIUNTA MODIFICA NVL(IMPORTO_RUOLO,0) = 0
                        'sStr1 = " select SUM((NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND, 0)) - (NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND, 0))) "
                        If Request.QueryString("FILTDATE") = 0 Then
                            sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0)) ) " _
                                 & " from SISCOM_MI.BOL_BOLLETTE " _
                                 & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                 & "   and FL_ANNULLATA=0 " _
                                 & "   and ID_MOROSITA is Null  " _
                                 & "   and ID_BOLLETTA_RIC Is NULL " _
                                 & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                 & "   and ID<0 " _
                                 & "   and BOL_BOLLETTE.ID_TIPO not in (4,5,7)  " _
                                 & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                 & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                 & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "
                        Else
                            'sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0)) ) "
                            'cioò che esegue la funzione oracle select ((SUM(NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0)-NVL(QUOTA_SIND_B,0)) - SUM(NVL(BOL_BOLLETTE.IMPORTO_RICLASSIFICATO,0))) - (SUM(NVL(importo_pagato,0)-NVL(QUOTA_SIND_pagata_B,0)) - SUM(NVL(BOL_BOLLETTE.IMPORTO_RICLASSIFICATO_PAGATO,0)))) 
                            sStr1 = " select distinct NVL (siscom_mi.MOROSITACONTRATTO(" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") & ",'" & Request.QueryString("FDAL") & "','" & Request.QueryString("FAL") & "','G'), 0)" _
                             & " from SISCOM_MI.BOL_BOLLETTE " _
                             & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                             & "   and FL_ANNULLATA=0 " _
                             & "   and ID_MOROSITA is Null  " _
                             & "   and ID_BOLLETTA_RIC Is NULL " _
                             & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                             & "   and ID<0 "

                            If Not String.IsNullOrEmpty(Request.QueryString("FDAL")) Then
                                sStr1 &= " AND RIFERIMENTO_DA >='" & Request.QueryString("FDAL") & "'"
                            End If
                            If Not String.IsNullOrEmpty(Request.QueryString("FAL")) Then
                                sStr1 &= "AND RIFERIMENTO_A <='" & Request.QueryString("FAL") & "' "

                            End If

                            sStr1 &= "   and BOL_BOLLETTE.ID_TIPO not in (4,5,7)  " _
                                 & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                 & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                 & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "

                        End If

                        '& "   and ((NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND,0)) - (NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND,0))) >0"


                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = sStr1
                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            ValSomma = par.IfNull(myReader2(0), 0)
                        End If
                        myReader2.Close()
                        '***********************************

                        If ValSomma > 0 Then

                            'SUM(IMPORTO) 25/10/2011 (faccio tutto con la select di sopra
                            'IMPORTI NEGATIVI da SOTTRARRE
                            'sStr1 = "select SUM( NVL(BOL_BOLLETTE_VOCI.IMPORTO, 0) - NVL(BOL_BOLLETTE_VOCI.IMP_PAGATO, 0) ) " _
                            '    & " from SISCOM_MI.BOL_BOLLETTE_VOCI " _
                            '    & " where ID_BOLLETTA in (select  ID " _
                            '                          & " from SISCOM_MI.BOL_BOLLETTE " _
                            '                          & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                            '                          & "   and ID<0 " _
                            '                          & "   and FL_ANNULLATA=0 " _
                            '                          & "   and ID_BOLLETTA_RIC is null  and ID_RATEIZZAZIONE is null " _
                            '                          & "   and   (BOL_BOLLETTE.ID_TIPO<>4 or BOL_BOLLETTE.ID_TIPO is null) " _
                            '                          & "   and   NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0)<0 " _
                            '                          & "    " & sStringaSqlDATA_RIF & ")" _
                            '   & " and ID_VOCE not in (select ID from SISCOM_MI.T_VOCI_BOLLETTA where COMPETENZA=3)"

                            ''and BOL_BOLLETTE.ID not in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SINDACATI)
                            ''& "  Al posto  and DATA_PAGAMENTO is null " _
                            ''& " and  ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) )>0 " _
                            ''& "   and ID in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SOLLECITI) )"


                            'par.cmd.Parameters.Clear()
                            'par.cmd.CommandText = sStr1
                            'myReader2 = par.cmd.ExecuteReader()
                            'If myReader2.Read Then
                            '    ValSomma = ValSomma + par.IfNull(myReader2(0), 0)
                            'End If
                            'myReader2.Close()
                            ''***********************************


                            '1) SOMMA IMPORTO PAGATO
                            'sStr1 = "select SUM(IMPORTO_PAGATO)" _
                            '     & " from SISCOM_MI.BOL_BOLLETTE " _
                            '     & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                            '     & "   and ID<0 " _
                            '     & "   and  ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) )>0 " _
                            '     & "   and FL_ANNULLATA=0 " _
                            '     & "   and ID_BOLLETTA_RIC is null  " _
                            '     & "   and   (BOL_BOLLETTE.ID_TIPO<>4 or BOL_BOLLETTE.ID_TIPO is null) " _
                            '     & "   and BOL_BOLLETTE.ID not in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SINDACATI) " & sStringaSqlDATA_RIF

                            ''& " and ID_VOCE not in (select ID from SISCOM_MI.T_VOCI_BOLLETTA where COMPETENZA=3)"

                            ''& "   and ID in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SOLLECITI)"


                            'par.cmd.CommandText = sStr1
                            'myReader2 = par.cmd.ExecuteReader()
                            'If myReader2.Read Then
                            '    ValSomma = ValSomma - par.IfNull(myReader2(0), 0) 'DARE - DATO
                            'End If
                            'myReader2.Close()
                            'par.cmd.Parameters.Clear()
                            '************************************

                            'DATA RIFERIMENTO DA
                            'par.cmd.CommandText = " select MIN(RIFERIMENTO_DA) from SISCOM_MI.BOL_BOLLETTE " _
                            '                    & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                            '                    & "   and ID<0 " _
                            '                    & "   and  ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) )>0 " _
                            '                    & "   and FL_ANNULLATA=0 " _
                            '                    & "   and ID_BOLLETTA_RIC is null  " _
                            '                    & "   and   (BOL_BOLLETTE.ID_TIPO<>4 or BOL_BOLLETTE.ID_TIPO is null) " _
                            '                    & "   and   NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0)>0 " _
                            '                    & "   and BOL_BOLLETTE.ID not in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SINDACATI) " & sStringaSqlDATA_RIF

                            '& "   and ID in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SOLLECITI) "

                            '
                            '07/01/2014 AGGIUNTA MODIFICA IMPORTO_RUOLO = 0
                            par.cmd.Parameters.Clear()
                            If Request.QueryString("FILTDATE") = 0 Then
                                par.cmd.CommandText = " select MIN(RIFERIMENTO_DA) " _
                                                    & " from SISCOM_MI.BOL_BOLLETTE" _
                                                    & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                                    & "   and FL_ANNULLATA=0 " _
                                                    & "   and ID_MOROSITA is Null  " _
                                                    & "   and ID_BOLLETTA_RIC Is NULL " _
                                                    & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                                    & "   and ID<0 " _
                                                    & "   and BOL_BOLLETTE.ID_TIPO not in (4,5,7)  " _
                                                    & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                                    & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                                    & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "
                            Else
                                par.cmd.CommandText = " select MIN(RIFERIMENTO_DA) " _
                                                    & " from SISCOM_MI.BOL_BOLLETTE" _
                                                    & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                                    & "   and FL_ANNULLATA=0 " _
                                                    & "   and ID_MOROSITA is Null  " _
                                                    & "   and ID_BOLLETTA_RIC Is NULL " _
                                                    & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 "

                                If Not String.IsNullOrEmpty(Request.QueryString("FDAL")) Then
                                    par.cmd.CommandText &= " AND RIFERIMENTO_DA >='" & Request.QueryString("FDAL") & "'"
                                End If
                                If Not String.IsNullOrEmpty(Request.QueryString("FAL")) Then
                                    par.cmd.CommandText &= "AND RIFERIMENTO_A <='" & Request.QueryString("FAL") & "' "

                                End If
                                par.cmd.CommandText &= "   and ID<0 " _
                                                    & "   and BOL_BOLLETTE.ID_TIPO not in (4,5,7)  " _
                                                    & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                                    & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                                    & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "


                            End If

                            myReader2 = par.cmd.ExecuteReader()
                            If myReader2.Read Then
                                sDataDA = par.IfNull(myReader2(0), 0)
                            End If
                            myReader2.Close()
                            '******************************************


                            '(5) INSERT MOROSITA_LETTERE
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_MOROSITA_LETTERE.NEXTVAL FROM DUAL"
                            myReader2 = par.cmd.ExecuteReader()
                            If myReader2.Read Then
                                vIdMorositaLettera = myReader2(0)
                            End If
                            myReader2.Close()


                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_LETTERE " _
                                                          & " (ID,ID_MOROSITA,ID_ANAGRAFICA,ID_CONTRATTO,ID_OPERATORE," _
                                                          & " EMISSIONE,IMPORTO,INIZIO_PERIODO,FINE_PERIODO,COD_CONTRATTO,NUM_LETTERE,TIPO_LETTERA,IMPORTO_INIZIALE,COD_STATO) " _
                                                & "values (:id,:id_morosita,:id_anagrafica,:id_contratto,:id_operatore," _
                                                      & " :emissione,:importo,:inizio,:fine,:cod_contratto,:num_lettere,:tipo_lettera,:importo_iniziale,:cod_stato)"

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdMorositaLettera))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorosita))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_anagrafica", par.IfNull(myReader1("ID_ANAGRAFICA"), "-1")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_contratto", par.IfNull(myReader1("ID_CONTRATTO"), "-1")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("emissione", Format(Now, "yyyyMMdd")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(ValSomma)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("inizio", sDataDA))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fine", "20090930"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_contratto", par.IfNull(myReader1("COD_CONTRATTO"), "")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_lettere", 1))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_lettera", "AB"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_iniziale", strToNumber(ValSomma)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_stato", "M94"))

                            'NOTE:
                            '  BOLLETTINO        VARCHAR2(50 BYTE) 'DA INSERIRE DOPO AVER EMESSO IL MAV

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '************************************************
                            'UPDATE BOL_BOLLETTE PRIMA DEL 2009 (DARE e AVERE) in data 09/02/2012 invece di ID_BOLLETTA_RIC uso ID_MOROSITA_LETTERA
                            par.cmd.Parameters.Clear()
                            If Request.QueryString("FILTDATE") = 0 Then
                                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                                    & " set   ID_MOROSITA_LETTERA=" & vIdMorositaLettera & "," _
                                                    & "       ID_MOROSITA=" & vIdMorosita _
                                                    & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                                    & "   and FL_ANNULLATA=0 " _
                                                    & "   and ID_MOROSITA is Null  " _
                                                    & "   and ID_BOLLETTA_RIC Is NULL " _
                                                    & "   and ID_RATEIZZAZIONE is null " _
                                                    & "   and ID<0  AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                                    & "   and BOL_BOLLETTE.ID_TIPO not in (4,5,7)  " _
                                                    & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                                    & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                                    & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "



                            Else
                                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                                    & " set   ID_MOROSITA_LETTERA=" & vIdMorositaLettera & "," _
                                                    & "       ID_MOROSITA=" & vIdMorosita _
                                                    & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1")

                                If Not String.IsNullOrEmpty(Request.QueryString("FDAL")) Then
                                    par.cmd.CommandText &= " AND RIFERIMENTO_DA >='" & Request.QueryString("FDAL") & "'"
                                End If
                                If Not String.IsNullOrEmpty(Request.QueryString("FAL")) Then
                                    par.cmd.CommandText &= "AND RIFERIMENTO_A <='" & Request.QueryString("FAL") & "' "

                                End If
                                par.cmd.CommandText &= "  and FL_ANNULLATA=0 " _
                                                    & "   and ID_MOROSITA is Null  " _
                                                    & "   and ID_BOLLETTA_RIC Is NULL " _
                                                    & "   and ID_RATEIZZAZIONE is null " _
                                                    & "   and ID<0  AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                                    & "   and BOL_BOLLETTE.ID_TIPO not in (4,5,7)  " _
                                                    & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                                    & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                                    & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "



                            End If


                            par.cmd.ExecuteNonQuery()


                            ''****************MYEVENT**PRIMA del 2009 LETTERA 1 di 2***************

                            sStr1 = "Inviato il MAV e la lettera di messa in mora con " & Me.cmbTipoInvio.SelectedValue.ToString & " per il recupero crediti MOROSITA dal: " & Me.lbl_RIF_DA.Text & " al: 30/09/2009"

                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & Format(Int(Epifani.TabEventiMorosita.MESSA_IN_MORA), "00")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '************************************************


                            'LETTERA 2 DOPO IL 30.09.2009

                            'sStr1 = "select SUM( NVL(BOL_BOLLETTE_VOCI.IMPORTO, 0) - NVL(BOL_BOLLETTE_VOCI.IMP_PAGATO, 0) )" _
                            '     & " from SISCOM_MI.BOL_BOLLETTE_VOCI " _
                            '     & " where ID_BOLLETTA in (select  ID " _
                            '                           & " from SISCOM_MI.BOL_BOLLETTE " _
                            '                           & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                            '                           & "   and RIFERIMENTO_DA>20090930 " _
                            '                           & "   and  ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) )>0 " _
                            '                           & "   and FL_ANNULLATA=0 " _
                            '                           & "   and ID_BOLLETTA_RIC is null  " _
                            '                           & "   and   (BOL_BOLLETTE.ID_TIPO<>4 or BOL_BOLLETTE.ID_TIPO is null) " _
                            '                           & "   and   NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0)>0 " _
                            '                           & "   and ID in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SOLLECITI) " _
                            '                           & "   and BOL_BOLLETTE.ID not in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SINDACATI) " & sStringaSqlDATA_RIF & ")" _
                            '   & " and ID_VOCE not in (select ID from SISCOM_MI.T_VOCI_BOLLETTA where COMPETENZA=3)"

                            '07/01/2014 AGGIUNTA MODIFICA NVL(IMPORTO_RUOLO,0) = 0
                            ' sStr1 = " select SUM((NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND, 0)) - (NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND, 0))) "
                            If Request.QueryString("FILTDATE") = 0 Then
                                sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0) ) ) " _
                                      & " from SISCOM_MI.BOL_BOLLETTE " _
                                      & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                      & "   and ID>0 " _
                                      & "   and FL_ANNULLATA=0 " _
                                      & "   and ID_MOROSITA is Null  " _
                                      & "   and ID_BOLLETTA_RIC is null  AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                      & "   and ID_RATEIZZAZIONE is null " _
                                      & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                                      & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "
                            Else
                                'sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0) ) ) "
                                sStr1 = " select distinct NVL (siscom_mi.MOROSITACONTRATTO(" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") & ",'" & Request.QueryString("FDAL") & "','" & Request.QueryString("FAL") & "','A'), 0)" _
                                          & " from SISCOM_MI.BOL_BOLLETTE " _
                                          & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                          & "   and ID>0 "
                                If Not String.IsNullOrEmpty(Request.QueryString("FDAL")) Then
                                    sStr1 &= " AND RIFERIMENTO_DA >='" & Request.QueryString("FDAL") & "'"
                                End If
                                If Not String.IsNullOrEmpty(Request.QueryString("FAL")) Then
                                    sStr1 &= "AND RIFERIMENTO_A <='" & Request.QueryString("FAL") & "' "

                                End If
                                sStr1 &= "  and FL_ANNULLATA=0 " _
                                      & "   and ID_MOROSITA is Null  " _
                                      & "   and ID_BOLLETTA_RIC is null  AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                      & "   and ID_RATEIZZAZIONE is null " _
                                      & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                                      & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "


                            End If
                            '& "   and (NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND,0)) - (NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND,0))>0 "
                            ' & "   and RIFERIMENTO_DA>20090930 " 

                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = sStr1
                            myReader2 = par.cmd.ExecuteReader()
                            If myReader2.Read Then
                                ValSomma = par.IfNull(myReader2(0), 0)
                            End If
                            myReader2.Close()
                            '***********************************

                            '1) SOMMA IMPORTO PAGATO
                            'sStr1 = "select SUM(IMPORTO_PAGATO)" _
                            '     & " from SISCOM_MI.BOL_BOLLETTE " _
                            '     & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                            '     & "   and RIFERIMENTO_DA>20090930 " _
                            '     & "   and  ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) )>0 " _
                            '     & "   and FL_ANNULLATA=0 " _
                            '     & "   and ID_BOLLETTA_RIC is null  " _
                            '     & "   and   (BOL_BOLLETTE.ID_TIPO<>4 or BOL_BOLLETTE.ID_TIPO is null) " _
                            '     & "   and   NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0)>0 " _
                            '     & "   and ID in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SOLLECITI) " _
                            '     & "   and BOL_BOLLETTE.ID not in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SINDACATI) " & sStringaSqlDATA_RIF



                            'par.cmd.CommandText = sStr1
                            'myReader2 = par.cmd.ExecuteReader()
                            'If myReader2.Read Then
                            '    ValSomma = ValSomma - par.IfNull(myReader2(0), 0) 'DARE - DATO
                            'End If
                            'myReader2.Close()
                            'par.cmd.Parameters.Clear()
                            '***********************************
                            '(5) INSERT MOROSITA_LETTERE
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_MOROSITA_LETTERE.NEXTVAL FROM DUAL"
                            myReader2 = par.cmd.ExecuteReader()
                            If myReader2.Read Then
                                vIdMorositaLettera = myReader2(0)
                            End If
                            myReader2.Close()


                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_LETTERE " _
                                                          & " (ID,ID_MOROSITA,ID_ANAGRAFICA,ID_CONTRATTO,ID_OPERATORE," _
                                                          & " EMISSIONE,IMPORTO,INIZIO_PERIODO,FINE_PERIODO,COD_CONTRATTO,NUM_LETTERE,TIPO_LETTERA,IMPORTO_INIZIALE,COD_STATO) " _
                                                & "values (:id,:id_morosita,:id_anagrafica,:id_contratto,:id_operatore," _
                                                      & " :emissione,:importo,:inizio,:fine,:cod_contratto,:num_lettere,:tipo_lettera,:importo_iniziale,:cod_stato)"

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdMorositaLettera))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorosita))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_anagrafica", par.IfNull(myReader1("ID_ANAGRAFICA"), "-1")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_contratto", par.IfNull(myReader1("ID_CONTRATTO"), "-1")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("emissione", Format(Now, "yyyyMMdd")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(ValSomma)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("inizio", "20091001"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fine", par.AggiustaData(Me.lbl_RIF_A.Text)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_contratto", par.IfNull(myReader1("COD_CONTRATTO"), "")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_lettere", 2))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_lettera", "AB"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_iniziale", strToNumber(ValSomma)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_stato", "M94"))

                            'NOTE:
                            '  BOLLETTINO        VARCHAR2(50 BYTE) 'DA INSERIRE DOPO AVER EMESSO IL MAV

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '************************************************

                            'UPDATE BOL_BOLLETTE
                            par.cmd.Parameters.Clear()
                            If Request.QueryString("FILTDATE") = 0 Then
                                par.cmd.CommandText = " update SISCOM_MI.BOL_BOLLETTE " _
                                                  & " set   ID_MOROSITA_LETTERA=" & vIdMorositaLettera & "," _
                                                  & "       ID_MOROSITA=" & vIdMorosita _
                                                  & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                                  & "   and ID>0 " _
                                                  & "   and FL_ANNULLATA=0 " _
                                                  & "   and ID_MOROSITA is Null  " _
                                                  & "   and ID_BOLLETTA_RIC is null  " _
                                                  & "   and ID_RATEIZZAZIONE is null " _
                                                  & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                                                  & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "
                            Else
                                par.cmd.CommandText = " update SISCOM_MI.BOL_BOLLETTE " _
                                                      & " set   ID_MOROSITA_LETTERA=" & vIdMorositaLettera & "," _
                                                      & "       ID_MOROSITA=" & vIdMorosita _
                                                      & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                                      & "   and ID>0 "
                                If Not String.IsNullOrEmpty(Request.QueryString("FDAL")) Then
                                    par.cmd.CommandText &= " AND RIFERIMENTO_DA >='" & Request.QueryString("FDAL") & "'"
                                End If
                                If Not String.IsNullOrEmpty(Request.QueryString("FAL")) Then
                                    par.cmd.CommandText &= "AND RIFERIMENTO_A <='" & Request.QueryString("FAL") & "' "
                                End If

                                par.cmd.CommandText &= "  and FL_ANNULLATA=0 " _
                                                      & "   and ID_MOROSITA is Null  " _
                                                      & "   and ID_BOLLETTA_RIC is null  " _
                                                      & "   and ID_RATEIZZAZIONE is null " _
                                                      & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                                                      & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "

                            End If

                            par.cmd.ExecuteNonQuery()

                            'UPDARE BOL_BOLLETTE_VOCI fatto dopo la creazione del MAV (mod. 14/02/2012)
                            ''UPDATE BOL_BOLLETTE_VOCI (mod. 01/12/2011) tolto da BOL_BOLLETTE.IMPORTO_RICLASSIFICATO
                            'par.cmd.Parameters.Clear()
                            'par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI " _
                            '                   & " set  IMPORTO_RICLASSIFICATO = NVL(IMPORTO,0) - NVL(IMP_PAGATO,0) " _
                            '                   & " where ID_BOLLETTA in (select ID from SISCOM_MI.BOL_BOLLETTE " _
                            '                                        & "  where ID_MOROSITA_LETTERA=" & vIdMorositaLettera _
                            '                                        & "    and ID_MOROSITA=" & vIdMorosita _
                            '                                        & "    and FL_ANNULLATA=0 " _
                            '                                        & "    and ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") & ") " _
                            '                  & " and ID_VOCE NOT IN (SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE FL_NO_SALDO = 1)"

                            'par.cmd.ExecuteNonQuery()
                            'par.cmd.Parameters.Clear()
                            '**************************************

                            'par.cmd.Parameters.Clear()
                            'par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                            '                     & " set   ID_BOLLETTA_RIC=" & vIdMorositaLettera & "," _
                            '                     & "       ID_MOROSITA=" & vIdMorosita _
                            '                     & " where ID in (select ID from SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.BOL_FLUSSI " _
                            '                                  & " where BOL_BOLLETTE.ID = BOL_FLUSSI.id_bolletta " _
                            '                                  & "   and ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                            '                                   & "   and RIFERIMENTO_DA>20090930 " _
                            '                                   & "   and FL_ANNULLATA=0 " _
                            '                                   & "   and ID_BOLLETTA_RIC is null and ID_RATEIZZAZIONE is null  " _
                            '                                   & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                            '                                   & "   and (NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) -NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND,0)) - (NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND,0))>0 )"

                            'par.cmd.ExecuteNonQuery()


                            'par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                            '                    & " set   ID_BOLLETTA_RIC=" & vIdMorositaLettera & "," _
                            '                    & "       ID_MOROSITA=" & vIdMorosita _
                            '                    & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                            '                    & "   and RIFERIMENTO_DA>20090930 " _
                            '                    & "   and  ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) )>0 " _
                            '                    & "   and FL_ANNULLATA=0 " _
                            '                    & "   and ID_BOLLETTA_RIC is null  " _
                            '                    & "   and   (BOL_BOLLETTE.ID_TIPO<>4 or BOL_BOLLETTE.ID_TIPO is null) " _
                            '                    & "   and   NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0)>0 " _
                            '                    & "   and ID in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SOLLECITI) " _
                            '                    & "   and BOL_BOLLETTE.ID not in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SINDACATI) " & sStringaSqlDATA_RIF
                            '************************************


                            ''****************MYEVENT**DOPO del 2009 LETTERA 2 di 2***************

                            sStr1 = "Inviato il MAV e la lettera di messa in mora con " & Me.cmbTipoInvio.SelectedValue.ToString & " per il recupero crediti MOROSITA dal: 30/09/2009 al: " & Me.lbl_RIF_DA.Text

                            par.cmd.Parameters.Clear()

                            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & Format(Int(Epifani.TabEventiMorosita.MESSA_IN_MORA), "00")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '************************************************

                        End If

                    ElseIf par.IfNull(myReader1("SALDO_1"), 0) > 0 And par.IfNull(myReader1("SALDO_2"), 0) = 0 Then
                        'SOLO PRIMA DEL 2009
                        '07/01/2014 AGGIUNTA MODIFICA NVL(IMPORTO_RUOLO,0) = 0
                        If Request.QueryString("FILTDATE") = 0 Then
                            sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0)) ) " _
                                 & " from SISCOM_MI.BOL_BOLLETTE " _
                                 & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                 & "   and FL_ANNULLATA=0 " _
                                 & "   and ID_MOROSITA is Null  " _
                                 & "   and ID_BOLLETTA_RIC Is NULL " _
                                 & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                 & "   and ID<0 " _
                                 & "   and BOL_BOLLETTE.ID_TIPO not in (4,5,7)  " _
                                 & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                 & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                 & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "
                        Else
                            ' sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0)) ) "
                            sStr1 = " select distinct NVL (siscom_mi.MOROSITACONTRATTO(" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") & ",'" & Request.QueryString("FDAL") & "','" & Request.QueryString("FAL") & "','G'), 0)" _
                             & " from SISCOM_MI.BOL_BOLLETTE " _
                             & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                             & "   and FL_ANNULLATA=0 " _
                             & "   and ID_MOROSITA is Null  "
                            If Not String.IsNullOrEmpty(Request.QueryString("FDAL")) Then
                                sStr1 &= " AND RIFERIMENTO_DA >='" & Request.QueryString("FDAL") & "'"
                            End If
                            If Not String.IsNullOrEmpty(Request.QueryString("FAL")) Then
                                sStr1 &= "AND RIFERIMENTO_A <='" & Request.QueryString("FAL") & "' "
                            End If
                            sStr1 &= "  and ID_BOLLETTA_RIC Is NULL " _
                                 & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                 & "   and ID<0 " _
                                 & "   and BOL_BOLLETTE.ID_TIPO not in (4,5,7)  " _
                                 & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                 & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                 & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "

                        End If


                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = sStr1
                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            ValSomma = par.IfNull(myReader2(0), 0)
                        End If
                        myReader2.Close()
                        '***********************************

                        If ValSomma > 0 Then
                            '07/01/2014 AGGIUNTA MODIFICA NVL(IMPORTO_RUOLO,0) = 0
                            par.cmd.Parameters.Clear()
                            If Request.QueryString("FILTDATE") = 0 Then
                                par.cmd.CommandText = " select MIN(RIFERIMENTO_DA) " _
                                                & " from SISCOM_MI.BOL_BOLLETTE" _
                                                & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                                & "   and FL_ANNULLATA=0 " _
                                                & "   and ID_MOROSITA is Null  " _
                                                & "   and ID_BOLLETTA_RIC Is NULL " _
                                                & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                                & "   and ID<0 " _
                                                & "   and BOL_BOLLETTE.ID_TIPO not in (4,5,7)  " _
                                                & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                                & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                                & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "

                            Else
                                par.cmd.CommandText = " select MIN(RIFERIMENTO_DA) " _
                                                & " from SISCOM_MI.BOL_BOLLETTE" _
                                                & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                                & "   and FL_ANNULLATA=0 " _
                                                & "   and ID_MOROSITA is Null  " _
                                                & "   and ID_BOLLETTA_RIC Is NULL " _
                                                & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                                & "   and ID<0 " _
                                                & "   and BOL_BOLLETTE.ID_TIPO not in (4,5,7)  " _
                                                & "   and BOL_BOLLETTE.ID_TIPO is not null "
                                If Not String.IsNullOrEmpty(Request.QueryString("FDAL")) Then
                                    par.cmd.CommandText &= " AND RIFERIMENTO_DA >='" & Request.QueryString("FDAL") & "'"
                                End If
                                If Not String.IsNullOrEmpty(Request.QueryString("FAL")) Then
                                    par.cmd.CommandText &= "AND RIFERIMENTO_A <='" & Request.QueryString("FAL") & "' "
                                End If
                                par.cmd.CommandText &= "  and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                                & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "

                            End If

                            myReader2 = par.cmd.ExecuteReader()
                            If myReader2.Read Then
                                sDataDA = par.IfNull(myReader2(0), 0)
                            End If
                            myReader2.Close()
                            '******************************************


                            '(5) INSERT MOROSITA_LETTERE
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_MOROSITA_LETTERE.NEXTVAL FROM DUAL"
                            myReader2 = par.cmd.ExecuteReader()
                            If myReader2.Read Then
                                vIdMorositaLettera = myReader2(0)
                            End If
                            myReader2.Close()


                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_LETTERE " _
                                & " (ID,ID_MOROSITA,ID_ANAGRAFICA,ID_CONTRATTO,ID_OPERATORE," _
                                & " EMISSIONE,IMPORTO,INIZIO_PERIODO,FINE_PERIODO,COD_CONTRATTO,NUM_LETTERE,TIPO_LETTERA,IMPORTO_INIZIALE,COD_STATO) " _
                                & "values (:id,:id_morosita,:id_anagrafica,:id_contratto,:id_operatore," _
                                & " :emissione,:importo,:inizio,:fine,:cod_contratto,:num_lettere,:tipo_lettera,:importo_iniziale,:cod_stato)"
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdMorositaLettera))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorosita))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_anagrafica", par.IfNull(myReader1("ID_ANAGRAFICA"), "-1")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_contratto", par.IfNull(myReader1("ID_CONTRATTO"), "-1")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("emissione", Format(Now, "yyyyMMdd")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(ValSomma)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("inizio", sDataDA))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fine", "20090930"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_contratto", par.IfNull(myReader1("COD_CONTRATTO"), "")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_lettere", 1))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_lettera", "EF"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_iniziale", strToNumber(ValSomma)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_stato", "M94"))

                            'NOTE:
                            '  BOLLETTINO        VARCHAR2(50 BYTE) 'DA INSERIRE DOPO AVER EMESSO IL MAV

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '************************************************

                            'UPDATE BOL_BOLLETTE PRIMA DEL 2009 (DARE e AVERE)
                            par.cmd.Parameters.Clear()
                            If Request.QueryString("FILTDATE") = 0 Then
                                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                                    & " set   ID_MOROSITA_LETTERA=" & vIdMorositaLettera & "," _
                                                    & "       ID_MOROSITA=" & vIdMorosita _
                                                    & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                                    & "   and FL_ANNULLATA=0 " _
                                                    & "   and ID_MOROSITA is Null  " _
                                                    & "   and ID_BOLLETTA_RIC Is NULL " _
                                                    & "   and ID_RATEIZZAZIONE is null " _
                                                    & "   and ID<0  AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                                    & "   and BOL_BOLLETTE.ID_TIPO not in (4,5,7)  " _
                                                    & "   and BOL_BOLLETTE.ID_TIPO is not null " _
                                                    & "   and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                                    & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "
                            Else
                                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                                    & " set   ID_MOROSITA_LETTERA=" & vIdMorositaLettera & "," _
                                                    & "       ID_MOROSITA=" & vIdMorosita _
                                                    & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                                    & "   and FL_ANNULLATA=0 " _
                                                    & "   and ID_MOROSITA is Null  " _
                                                    & "   and ID_BOLLETTA_RIC Is NULL " _
                                                    & "   and ID_RATEIZZAZIONE is null " _
                                                    & "   and ID<0  AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                                    & "   and BOL_BOLLETTE.ID_TIPO not in (4,5,7)  " _
                                                    & "   and BOL_BOLLETTE.ID_TIPO is not null "
                                If Not String.IsNullOrEmpty(Request.QueryString("FDAL")) Then
                                    par.cmd.CommandText &= " AND RIFERIMENTO_DA >='" & Request.QueryString("FDAL") & "'"
                                End If
                                If Not String.IsNullOrEmpty(Request.QueryString("FAL")) Then
                                    par.cmd.CommandText &= "AND RIFERIMENTO_A <='" & Request.QueryString("FAL") & "' "
                                End If
                                par.cmd.CommandText &= "  and  ( ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) )<0   or " _
                                                    & "          ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )<>0 ) "

                            End If



                            par.cmd.ExecuteNonQuery()
                            par.cmd.Parameters.Clear()


                            'UPDARE BOL_BOLLETTE_VOCI fatto dopo la creazione del MAV (mod. 14/02/2012)
                            'UPDATE BOL_BOLLETTE_VOCI (mod. 01/12/2011) tolto da BOL_BOLLETTE.IMPORTO_RICLASSIFICATO
                            'par.cmd.Parameters.Clear()
                            'par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI " _
                            '                   & " set  IMPORTO_RICLASSIFICATO = NVL(IMPORTO,0) - NVL(IMP_PAGATO,0) " _
                            '                   & " where ID_BOLLETTA in (select ID from SISCOM_MI.BOL_BOLLETTE " _
                            '                                        & "  where ID_MOROSITA_LETTERA=" & vIdMorositaLettera _
                            '                                        & "    and ID_MOROSITA=" & vIdMorosita _
                            '                                        & "    and FL_ANNULLATA=0 " _
                            '                                        & "    and ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") & ") " _
                            '                  & " and ID_VOCE NOT IN (SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE FL_NO_SALDO = 1)"

                            'par.cmd.ExecuteNonQuery()
                            'par.cmd.Parameters.Clear()
                            '**************************************

                            ''****************MYEVENT**PRIMA del 2009 LETTERA 1 ***************

                            sStr1 = "Inviato il MAV e la lettera di messa in mora con " & Me.cmbTipoInvio.SelectedValue.ToString & " per il recupero crediti MOROSITA dal: " & Me.lbl_RIF_DA.Text & " al: 30/09/2009"

                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & Format(Int(Epifani.TabEventiMorosita.MESSA_IN_MORA), "00")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '************************************************

                        End If

                    Else
                        ' SOLO DOPO il 2009
                        '07/01/2014 AGGIUNTA MODIFICA NVL(IMPORTO_RUOLO,0) = 0
                        If Request.QueryString("FILTDATE") = 0 Then
                            sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0) ) ) " _
                                & " from SISCOM_MI.BOL_BOLLETTE " _
                                & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                & "   and ID>0 " _
                                & "   and FL_ANNULLATA=0 " _
                                & "   and ID_MOROSITA is Null  " _
                                & "   and ID_BOLLETTA_RIC is null  " _
                                & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                                & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "
                        Else
                            'sStr1 = " select SUM( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B, 0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B, 0) ) ) "
                            sStr1 = " select NVL (siscom_mi.MOROSITACONTRATTO(" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") & ",'" & Request.QueryString("FDAL") & "','" & Request.QueryString("FAL") & "','A'), 0) " _
                                & " from SISCOM_MI.BOL_BOLLETTE " _
                                & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                & "   and ID>0 " _
                                & "   and FL_ANNULLATA=0 " _
                                & "   and ID_MOROSITA is Null  " _
                                & "   and ID_BOLLETTA_RIC is null  " _
                                & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ "
                            If Not String.IsNullOrEmpty(Request.QueryString("FDAL")) Then
                                sStr1 &= " AND RIFERIMENTO_DA >='" & Request.QueryString("FDAL") & "'"
                            End If
                            If Not String.IsNullOrEmpty(Request.QueryString("FAL")) Then
                                sStr1 &= "AND RIFERIMENTO_A <='" & Request.QueryString("FAL") & "' "
                            End If
                            sStr1 &= " and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "

                        End If

                        'sStr1 = " select SUM((NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND, 0)) - (NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND, 0))) " _
                        '     & " from SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_FLUSSI " _
                        '     & " where BOL_BOLLETTE.ID = BOL_FLUSSI.id_bolletta " _
                        '     & "   and ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                        '     & "   and RIFERIMENTO_DA>20090930 " _
                        '     & "   and FL_ANNULLATA=0 " _
                        '     & "   and ID_BOLLETTA_RIC is null  and ID_RATEIZZAZIONE is null " _
                        '     & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                        '     & "   and (NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) -NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND,0)) - (NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_FLUSSI.BOLLETTATO_QUOTA_SIND,0))>0 "

                        ''& "   and ID in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SOLLECITI) " _


                        'sStr1 = "select SUM( NVL(BOL_BOLLETTE_VOCI.IMPORTO, 0) - NVL(BOL_BOLLETTE_VOCI.IMP_PAGATO, 0) ) " _
                        '       & " from SISCOM_MI.BOL_BOLLETTE_VOCI " _
                        '       & " where ID_BOLLETTA in (select  ID " _
                        '                             & " from SISCOM_MI.BOL_BOLLETTE " _
                        '                             & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                        '                             & "   and RIFERIMENTO_DA>20090930 " _
                        '                             & "   and  ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) )>0 " _
                        '                             & "   and FL_ANNULLATA=0 " _
                        '                             & "   and ID_BOLLETTA_RIC is null  " _
                        '                             & "   and   (BOL_BOLLETTE.ID_TIPO<>4 or BOL_BOLLETTE.ID_TIPO is null) " _
                        '                             & "   and   NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0)>0 " _
                        '                             & "   and ID in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SOLLECITI) " _
                        '                             & "   and BOL_BOLLETTE.ID not in (select ID_BOLLETTA from SISCOM_MI.BOL_BOLLETTE_SINDACATI) " & sStringaSqlDATA_RIF & " )" _
                        '        & " and ID_VOCE not in (select ID from SISCOM_MI.T_VOCI_BOLLETTA where COMPETENZA=3)"


                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = sStr1
                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            ValSomma = par.IfNull(myReader2(0), 0)
                        End If
                        myReader2.Close()
                        '***********************************

                        If ValSomma > 0 Then
                            '07/01/2014 AGGIUNTA MODIFICA NVL(IMPORTO_RUOLO,0) = 0
                            par.cmd.Parameters.Clear()
                            If Request.QueryString("FILTDATE") = 0 Then
                                par.cmd.CommandText = "   select MIN(RIFERIMENTO_DA)  " _
                                                    & " from SISCOM_MI.BOL_BOLLETTE " _
                                                    & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                                    & "   and ID>0 " _
                                                    & "   and FL_ANNULLATA=0 " _
                                                    & "   and ID_MOROSITA is Null  " _
                                                    & "   and ID_BOLLETTA_RIC is null  " _
                                                    & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                                    & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                                                    & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "
                            Else
                                par.cmd.CommandText = "   select MIN(RIFERIMENTO_DA)  " _
                                                    & " from SISCOM_MI.BOL_BOLLETTE " _
                                                    & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                                    & "   and ID>0 " _
                                                    & "   and FL_ANNULLATA=0 " _
                                                    & "   and ID_MOROSITA is Null  " _
                                                    & "   and ID_BOLLETTA_RIC is null  " _
                                                    & "   and ID_RATEIZZAZIONE is null AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                                    & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ "
                                If Not String.IsNullOrEmpty(Request.QueryString("FDAL")) Then
                                    par.cmd.CommandText &= " AND RIFERIMENTO_DA >='" & Request.QueryString("FDAL") & "'"
                                End If
                                If Not String.IsNullOrEmpty(Request.QueryString("FAL")) Then
                                    par.cmd.CommandText &= "AND RIFERIMENTO_A <='" & Request.QueryString("FAL") & "' "
                                End If
                                par.cmd.CommandText &= "and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "


                            End If
                            myReader2 = par.cmd.ExecuteReader()
                            If myReader2.Read Then
                                sDataDA = par.IfNull(myReader2(0), 0)
                            End If
                            myReader2.Close()
                            '*****************************************

                            '************************************************


                            '(5) INSERT MOROSITA_LETTERE

                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_MOROSITA_LETTERE.NEXTVAL FROM DUAL"
                            myReader2 = par.cmd.ExecuteReader()
                            If myReader2.Read Then
                                vIdMorositaLettera = myReader2(0)
                            End If
                            myReader2.Close()


                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_LETTERE " _
                                                          & " (ID,ID_MOROSITA,ID_ANAGRAFICA,ID_CONTRATTO,ID_OPERATORE," _
                                                          & " EMISSIONE,IMPORTO,INIZIO_PERIODO,FINE_PERIODO,COD_CONTRATTO,NUM_LETTERE,TIPO_LETTERA,IMPORTO_INIZIALE,COD_STATO) " _
                                                & "values (:id,:id_morosita,:id_anagrafica,:id_contratto,:id_operatore," _
                                                      & " :emissione,:importo,:inizio,:fine,:cod_contratto,:num_lettere,:tipo_lettera,:importo_iniziale,:cod_stato)"

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdMorositaLettera))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorosita))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_anagrafica", par.IfNull(myReader1("ID_ANAGRAFICA"), "-1")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_contratto", par.IfNull(myReader1("ID_CONTRATTO"), "-1")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("emissione", Format(Now, "yyyyMMdd")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(ValSomma)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("inizio", sDataDA))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fine", par.AggiustaData(Me.lbl_RIF_A.Text)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_contratto", par.IfNull(myReader1("COD_CONTRATTO"), "")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_lettere", 1))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_lettera", "CD"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_iniziale", strToNumber(ValSomma)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_stato", "M94"))

                            'NOTE:
                            '  BOLLETTINO        VARCHAR2(50 BYTE) 'DA INSERIRE DOPO AVER EMESSO IL MAV

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '************************************************


                            'UPDATE BOL_BOLLETTE
                            par.cmd.Parameters.Clear()
                            If Request.QueryString("FILTDATE") = 0 Then
                                par.cmd.CommandText = " update SISCOM_MI.BOL_BOLLETTE " _
                                                  & " set   ID_MOROSITA_LETTERA=" & vIdMorositaLettera & "," _
                                                  & "       ID_MOROSITA=" & vIdMorosita _
                                                  & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                                  & "   and ID>0  AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                                  & "   and FL_ANNULLATA=0 " _
                                                  & "   and ID_MOROSITA is Null  " _
                                                  & "   and ID_BOLLETTA_RIC is null  " _
                                                  & "   and ID_RATEIZZAZIONE is null " _
                                                  & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ " _
                                                  & "   and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "
                            Else
                                par.cmd.CommandText = " update SISCOM_MI.BOL_BOLLETTE " _
                                                  & " set   ID_MOROSITA_LETTERA=" & vIdMorositaLettera & "," _
                                                  & "       ID_MOROSITA=" & vIdMorosita _
                                                  & " where ID_CONTRATTO=" & par.IfNull(myReader1("ID_CONTRATTO"), "-1") _
                                                  & "   and ID>0  AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                                  & "   and FL_ANNULLATA=0 " _
                                                  & "   and ID_MOROSITA is Null  " _
                                                  & "   and ID_BOLLETTA_RIC is null  " _
                                                  & "   and ID_RATEIZZAZIONE is null " _
                                                  & "   /*and BOL_BOLLETTE.FL_SOLLECITO=1*/ "
                                If Not String.IsNullOrEmpty(Request.QueryString("FDAL")) Then
                                    par.cmd.CommandText &= " AND RIFERIMENTO_DA >='" & Request.QueryString("FDAL") & "'"
                                End If
                                If Not String.IsNullOrEmpty(Request.QueryString("FAL")) Then
                                    par.cmd.CommandText &= "AND RIFERIMENTO_A <='" & Request.QueryString("FAL") & "' "
                                End If
                                par.cmd.CommandText &= "  and ( NVL(BOL_BOLLETTE.IMPORTO_TOTALE, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_B,0) - ( NVL(BOL_BOLLETTE.IMPORTO_PAGATO, 0) - NVL(BOL_BOLLETTE.QUOTA_SIND_PAGATA_B,0)) )>0 "


                            End If

                            par.cmd.ExecuteNonQuery()



                            ''****************MYEVENT****DOPO il 2009 Lettera 1*************

                            sStr1 = "Inviato il MAV e la lettera di messa in mora con " & Me.cmbTipoInvio.SelectedValue.ToString & " per il recupero crediti MOROSITA dal: " & Me.lbl_RIF_DA.Text & " al: " & Me.lbl_RIF_A.Text

                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                & " (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL,:id_morosita,:id_operatore,:data,:cod_evento,:motivo)"
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", vIdMorositaLettera))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "M" & Format(Int(Epifani.TabEventiMorosita.MESSA_IN_MORA), "00")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()
                            '************************************************
                        End If
                    End If

                End While
                myReader1.Close()

                ' COMMIT
                par.myTrans.Commit()
                par.cmd.CommandText = ""
                FlagConnessione = False

                



                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtModificato.Text = "0"
                'Response.Redirect("CreaLettere2.aspx?IDMOR=" & vIdMorosita, False)
                'Response.Write("<script>window.open('CreaLettere2.aspx?IDMOR=" & vIdMorosita & "','Crea Lettera " & Format(Now, "hhss") & "','height=300,top=0,left=0,width=500');</script>")
                Response.Write("<script>window.open('CreaLettere2.aspx?IDMOR=" & vIdMorosita & "&SCADBOL=" & par.AggiustaData(Me.txtDataScad.Text) & "','Crea Lettera " & Format(Now, "hhss") & "','height=300,top=0,left=0,width=500');</script>")
                'Response.Write("<script>window.showModalDialog('CreaLettere2.aspx?IDMOR=" & vIdMorosita & "&SCADBOL=" & par.AggiustaData(Me.txtDataScad.Text) & "',window, 'status:no;dialogWidth:800px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');</script>")

                btnStampa.Visible = True
                btnSalva.Visible = False
				Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")

            Else
                Me.txtAnnullo.Value = "0"
            End If

        Catch ex As Exception

            If FlagConnessione = True Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    par.myTrans.Rollback()
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If


                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            End If

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            Page.Dispose()


            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub

    Public Function ControlloCampi() As Boolean
        Dim ElencoID_Rapporti As String = ""

        Dim trovato As Boolean
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        Dim Trovati As Integer = 0
        Dim TrovatiNegativi As Integer = 0

        ControlloCampi = True
        trovato = False

        If Strings.Len(Strings.Trim(Me.txtProtocollo.Text)) = 0 Then
            Response.Write("<script>alert('Attenzione: Inserire il protocollo!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If Strings.Len(Strings.Trim(Me.txtDataProtocolloAler.Text)) = 0 Then
            Response.Write("<script>alert('Attenzione: Inserire la data di protocollo!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        For Each oDataGridItem In Me.DataGrid1.Items

            chkExport = oDataGridItem.FindControl("CheckBox1")

            If chkExport.Checked Then
                If Val(oDataGridItem.Cells(5).Text.Replace(".", "").Replace(",", ".")) < 0 Or Val(oDataGridItem.Cells(6).Text.Replace(".", "").Replace(",", ".")) < 0 Then
                    chkExport = oDataGridItem.FindControl("CheckBox1")
                    chkExport.Checked = False
                    TrovatiNegativi = TrovatiNegativi + 1
                End If
            End If
        Next




        trovato = False
        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("CheckBox1")
            If chkExport.Checked = True Then
                trovato = True
                Exit For
            End If
        Next

        If trovato = False Then
            If TrovatiNegativi > 0 Then
                Response.Write("<SCRIPT>alert('Attenzione: Per gli importi negativi o uguali a zero non verrà emessa nessuna morosità!');</SCRIPT>")
                ControlloCampi = False
                Exit Function
            Else
                Response.Write("<SCRIPT>alert('Attenzione: Selezionare almeno un inquilino dalla lista!');</SCRIPT>")
                ControlloCampi = False
                Exit Function
            End If
        Else
            If TrovatiNegativi > 0 Then
                Response.Write("<SCRIPT>alert('Attenzione: Per gli importi negativi o uguali a zero non verrà emessa nessuna morosità!');</SCRIPT>")
            End If

        End If


        'Trovati = InquiliniNoCF("DA_SALVA")
        If Trovati = 1 Then
            ControlloCampi = False
            Me.imgExcel.Visible = True

            'Response.Write("<SCRIPT>alert('Attenzione: premendo il bottone \'Elenco CF Errati\' verrà visualizzato l\'elenco di inquilini con codice fiscale errato.');</SCRIPT>")
            Response.Write("<SCRIPT>alert('Attenzione: Riscontrato un inquilino con C.F. errato, premere il bottone \'Elenco C.F. Errati\' per maggiori informazioni!');</SCRIPT>")
            Exit Function
        ElseIf Trovati > 1 Then
            ControlloCampi = False
            Me.imgExcel.Visible = True
            Response.Write("<SCRIPT>alert('Attenzione: Riscontrati " & Trovati & " inquilini con C.F. errato, premere il bottone \'Elenco C.F. Errati\' per maggiori informazioni!');</SCRIPT>")
            Exit Function
        End If



        'Me.imgExcel.Visible = False

        'Dim gen As Epifani.ListaGenerale

        'For Each gen In lstListaRapporti
        '    If ElencoID_Rapporti <> "" Then
        '        ElencoID_Rapporti = ElencoID_Rapporti & "," & gen.STR
        '    Else
        '        ElencoID_Rapporti = gen.STR
        '    End If
        'Next

        'sStr1 = "select ANAGRAFICA.ID as ID_ANAGRAFICA,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.COD_FISCALE, RAPPORTI_UTENZA.ID as ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR" _
        ' & " from   SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI " _
        ' & " where  RAPPORTI_UTENZA.ID in (" & ElencoID_Rapporti & ")" _
        ' & "   and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID " _
        ' & "   and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID  " _
        ' & "   and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
        ' & " order by ID_CONTRATTO	"

        'Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
        'par.cmd.CommandText = sStr1
        'myReaderT = par.cmd.ExecuteReader

        'While myReaderT.Read
        '    If par.ControllaCFNomeCognome(UCase(par.IfNull(myReaderT("COD_FISCALE"), "")), par.IfNull(myReaderT("COGNOME"), ""), par.IfNull(myReaderT("NOME"), "")) = False Then


        '        'Dim sRisp As String

        '        'sRisp = "Attenzione...il codice fiscale di " & par.IfNull(myReaderT("COGNOME"), "") & " " & par.IfNull(myReaderT("NOME"), "") & " è errato, correggere prima di effettuare la morosità"

        '        Me.imgExcel.Visible = True
        '        Response.Write("<SCRIPT>alert('Attenzione: premendo il bottone \'Elenco CF Errati\' verrà visualizzato l\'elenco di inquilini con codice fiscale errato. correggere prima di effettuare la morosità');</SCRIPT>")

        '        myReaderT.Close()
        '        'ControllaCF()

        '        ControlloCampi = False
        '        Exit Function
        '    End If

        'End While
        'myReaderT.Close()

        'If Trim(Me.txtCognomeD.Text) = "" And Trim(Me.txtNomeD.Text) = "" Then
        '    Response.Write("<script>alert('Attenzione...Inserire il nominativo del dirigente!');</script>")
        '    ControlloCampi = False
        '    Exit Function
        'End If

        'If Trim(Me.txtCognomeR.Text) = "" And Trim(Me.txtNomeR.Text) = "" Then
        '    Response.Write("<script>alert('Attenzione...Inserire il nominativo del responsabile del procedimento!');</script>")
        '    ControlloCampi = False
        '    Exit Function
        'End If

        'If Trim(Me.txtCognomeT.Text) = "" And Trim(Me.txtNomeT.Text) = "" Then
        '    Response.Write("<script>alert('Attenzione...Inserire il nominativo di chi ha trattato la pratica!');</script>")
        '    ControlloCampi = False
        '    Exit Function
        'End If


    End Function


    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)
        Try


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



    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click

        'If Me.txtAnnullo.Value = "1" Then

        ' Me.txtAnnullo.Value = "0"

        Response.Write("<script>window.showModalDialog('VisualizzaFileZIP.aspx?IDMOR=" & vIdMorosita & "',window, 'status:no;dialogWidth:800px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');</script>")

        '  End If

    End Sub



    Public Sub ControllaCF()
        Dim ElencoID_Rapporti As String = ""
        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String

        Dim FlagConnessione As Boolean

        Try

            sNomeFile = "Export_CF_" & Format(Now, "yyyyMMddHHmmss")
            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\") & sNomeFile & ".xls")


                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "CODICE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INTESTATARIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "CODICE FISCALE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "TIPO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "POSIZIONE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "COD. UNITA'", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "TIPO UNITA'", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "INDIRIZZO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "CIVICO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "COMUNE", 0)


                .SetColumnWidth(1, 1, 25)   'CODICE 
                .SetColumnWidth(2, 2, 35)   'INTESTATARIO 
                .SetColumnWidth(3, 3, 25)   'CF
                .SetColumnWidth(4, 4, 10)   'TIPO
                .SetColumnWidth(5, 5, 20)   'POSIZIONE
                .SetColumnWidth(6, 6, 20)   'COD UNITA
                .SetColumnWidth(7, 7, 15)   'TIPO UNITA
                .SetColumnWidth(8, 8, 35)   'INDIRIZZO
                .SetColumnWidth(9, 10, 10)  'CIVICO e COMUNE

                K = 2


                Dim gen As Epifani.ListaGenerale

                For Each gen In lstListaRapporti
                    If ElencoID_Rapporti <> "" Then
                        ElencoID_Rapporti = ElencoID_Rapporti & "," & gen.STR
                    Else
                        ElencoID_Rapporti = gen.STR
                    End If
                Next

                ' APRO CONNESSIONE
                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Dim sStr1 As String

                sStr1 = "select  UNITA_IMMOBILIARI.COD_TIPOLOGIA," _
                        & " INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO""," _
                        & " INDIRIZZI.CIVICO,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                        & " (UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE) as  COD_UNITA_IMMOBILIARE ," _
                        & " (RAPPORTI_UTENZA.COD_CONTRATTO) as  COD_CONTRATTO ," _
                        & "RAPPORTI_UTENZA.ID,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC," _
                            & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                            & "     then  RAGIONE_SOCIALE " _
                            & "     else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end  as  INTESTATARIO ," _
                        & " ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.RAGIONE_SOCIALE,ANAGRAFICA.PARTITA_IVA," _
                        & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO"" " _
              & " from  " _
                 & " SISCOM_MI.RAPPORTI_UTENZA,  " _
                 & " SISCOM_MI.INDIRIZZI, " _
                 & " SISCOM_MI.UNITA_IMMOBILIARI,  " _
                 & " SISCOM_MI.UNITA_CONTRATTUALE,  " _
                 & " SISCOM_MI.ANAGRAFICA, " _
                 & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                 & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE " _
            & " where  " _
                  & "      UNITA_IMMOBILIARI.ID_INDIRIZZO            =INDIRIZZI.ID (+) " _
                  & " and  UNITA_CONTRATTUALE.ID_UNITA               =UNITA_IMMOBILIARI.ID   " _
                  & " and  RAPPORTI_UTENZA.ID 					     =UNITA_CONTRATTUALE.ID_CONTRATTO " _
                  & " and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID " _
                  & " and  RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  =TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                  & " and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID  " _
                  & " and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                  & " and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE     is null   "

                If ElencoID_Rapporti <> "" Then
                    sStr1 = sStr1 & "  and RAPPORTI_UTENZA.ID in (" & ElencoID_Rapporti & ")"
                Else
                    sStr1 = sStr1 & "  and UNITA_IMMOBILIARI.ID in (select ID_UNITA from SISCOM_MI.BOL_BOLLETTE " _
                                                                              & " where  (NVL(TO_DATE(BOL_BOLLETTE.DATA_SCADENZA,'yyyymmdd')- TO_DATE(SYSDATE),21) <= 30) " _
                                                                              & "   and   FL_ANNULLATA=0) "

                End If


                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1
                myReaderT = par.cmd.ExecuteReader

                Dim CF_Errato As Boolean

                While myReaderT.Read
                    If par.IfNull(myReaderT("RAGIONE_SOCIALE"), "") = "" Then

                        CF_Errato = False

                        If par.ControllaCF(UCase(par.IfNull(myReaderT("COD_FISCALE"), ""))) = False Then
                            CF_Errato = True
                        ElseIf par.ControllaCFNomeCognome(UCase(par.IfNull(myReaderT("COD_FISCALE"), "")), par.IfNull(myReaderT("COGNOME"), ""), par.IfNull(myReaderT("NOME"), "")) = False Then
                            CF_Errato = True
                        End If
                    Else

                        If Len(par.IfNull(myReaderT("PARTITA_IVA"), 0)) = 11 Or Len(par.IfNull(myReaderT("COD_FISCALE"), 0)) = 16 Then
                            CF_Errato = False
                        Else
                            CF_Errato = True
                        End If
                    End If

                    If CF_Errato = True Then


                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(myReaderT("COD_CONTRATTO"), "")), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(myReaderT("INTESTATARIO"), "")), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(myReaderT("COD_FISCALE"), "")), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(myReaderT("COD_TIPOLOGIA_CONTR_LOC"), "")), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(myReaderT("POSIZIONE_CONTRATTO"), "")), 0)

                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(myReaderT("COD_UNITA_IMMOBILIARE"), "")), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(myReaderT("COD_TIPOLOGIA"), "")), 0)

                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(myReaderT("INDIRIZZO"), "")), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(myReaderT("CIVICO"), "")), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(myReaderT("COMUNE_UNITA"), "")), 0)

                        i = i + 1
                        K = K + 1
                    End If
                End While
                myReaderT.Close()

                'par.cmd.Dispose()
                'par.OracleConn.Close()
                'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                .CloseFile()
            End With

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")

            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            '
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)

            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            File.Delete(strFile)


            Response.Redirect("..\FileTemp\" & sNomeFile & ".zip", False)



            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
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


    Protected Sub imgExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgExcel.Click
        ControllaCF()
    End Sub


    'Public Function InquiliniNoCF(ByVal TipoControllo As String) As Integer
    '    Dim oDataGridItem As DataGridItem
    '    Dim chkExport As System.Web.UI.WebControls.CheckBox

    '    Dim sStr1 As String
    '    Dim ElencoID_Rapporti As String = ""
    '    Dim gen As Epifani.ListaGenerale

    '    Dim FlagConnessione As Boolean

    '    InquiliniNoCF = 0

    '    Try

    '        ' APRO CONNESSIONE
    '        FlagConnessione = False
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)

    '            FlagConnessione = True
    '        End If

    '        If TipoControllo = "DA_SALVA" Then
    '            For Each oDataGridItem In Me.DataGrid1.Items
    '                chkExport = oDataGridItem.FindControl("CheckBox1")
    '                If chkExport.Checked = True Then

    '                    If ElencoID_Rapporti <> "" Then
    '                        ElencoID_Rapporti = ElencoID_Rapporti & "," & oDataGridItem.Cells(0).Text
    '                    Else
    '                        ElencoID_Rapporti = oDataGridItem.Cells(0).Text
    '                    End If

    '                End If
    '            Next
    '        Else

    '            For Each gen In lstListaRapporti
    '                If ElencoID_Rapporti <> "" Then
    '                    ElencoID_Rapporti = ElencoID_Rapporti & "," & gen.STR
    '                Else
    '                    ElencoID_Rapporti = gen.STR
    '                End If
    '            Next
    '        End If

    '        sStr1 = "select ANAGRAFICA.ID as ID_ANAGRAFICA,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.COD_FISCALE, ANAGRAFICA.RAGIONE_SOCIALE,ANAGRAFICA.PARTITA_IVA,RAPPORTI_UTENZA.ID as ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR" _
    '         & " from   SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI " _
    '         & " where  RAPPORTI_UTENZA.ID in (" & ElencoID_Rapporti & ")" _
    '         & "   and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID " _
    '         & "   and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID  " _
    '         & "   and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
    '         & " order by ID_CONTRATTO	"

    '        Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
    '        par.cmd.Parameters.Clear()
    '        par.cmd.CommandText = sStr1
    '        myReaderT = par.cmd.ExecuteReader

    '        Dim CF_Errato As Boolean

    '        While myReaderT.Read
    '            CF_Errato = False

    '            If par.IfNull(myReaderT("RAGIONE_SOCIALE"), "") = "" Then
    '                If par.ControllaCF(UCase(par.IfNull(myReaderT("COD_FISCALE"), ""))) = False Then
    '                    CF_Errato = True
    '                ElseIf par.ControllaCFNomeCognome(UCase(par.IfNull(myReaderT("COD_FISCALE"), "")), par.IfNull(myReaderT("COGNOME"), ""), par.IfNull(myReaderT("NOME"), "")) = False Then
    '                    CF_Errato = True
    '                End If

    '                If CF_Errato = True Then
    '                    For Each oDataGridItem In Me.DataGrid1.Items

    '                        If TipoControllo = "DA_SALVA" Then
    '                            InquiliniNoCF = InquiliniNoCF + 1
    '                            Exit For
    '                        Else
    '                            If par.IfNull(myReaderT("ID_CONTRATTO"), "") = oDataGridItem.Cells(0).Text Then

    '                                chkExport = oDataGridItem.FindControl("CheckBox1")
    '                                chkExport.Checked = False
    '                                InquiliniNoCF = InquiliniNoCF + 1
    '                                Exit For
    '                            End If
    '                        End If
    '                    Next
    '                End If
    '            Else
    '                If Len(par.IfNull(myReaderT("PARTITA_IVA"), 0)) = 11 Or Len(par.IfNull(myReaderT("COD_FISCALE"), 0)) = 16 Then
    '                Else
    '                    CF_Errato = True
    '                End If

    '                If CF_Errato = True Then
    '                    For Each oDataGridItem In Me.DataGrid1.Items

    '                        If TipoControllo = "DA_SALVA" Then
    '                            InquiliniNoCF = InquiliniNoCF + 1
    '                            Exit For
    '                        Else
    '                            If par.IfNull(myReaderT("ID_CONTRATTO"), "") = oDataGridItem.Cells(0).Text Then

    '                                chkExport = oDataGridItem.FindControl("CheckBox1")
    '                                chkExport.Checked = False
    '                                InquiliniNoCF = InquiliniNoCF + 1
    '                                Exit For
    '                            End If
    '                        End If
    '                    Next
    '                End If
    '            End If

    '        End While
    '        myReaderT.Close()


    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If


    '    Catch ex As Exception
    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If

    '        Session.Item("LAVORAZIONE") = "0"
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")

    '    End Try

    'End Function



    Protected Sub imgInquinili_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgInquinili.Click

        Dim ElencoID_Rapporti As String = ""
        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String

        Dim FlagConnessione As Boolean

        Try

            sNomeFile = "Export_gia_Morisi_" & Format(Now, "yyyyMMddHHmmss")
            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\") & sNomeFile & ".xls")


                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "CODICE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INTESTATARIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "CODICE FISCALE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "TIPO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "POSIZIONE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "COD. UNITA'", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "TIPO UNITA'", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "INDIRIZZO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "CIVICO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "COMUNE", 0)


                .SetColumnWidth(1, 1, 25)   'CODICE 
                .SetColumnWidth(2, 2, 35)   'INTESTATARIO 
                .SetColumnWidth(3, 3, 25)   'CF
                .SetColumnWidth(4, 4, 10)   'TIPO
                .SetColumnWidth(5, 5, 20)   'POSIZIONE
                .SetColumnWidth(6, 6, 20)   'COD UNITA
                .SetColumnWidth(7, 7, 15)   'TIPO UNITA
                .SetColumnWidth(8, 8, 35)   'INDIRIZZO
                .SetColumnWidth(9, 10, 10)  'CIVICO e COMUNE

                K = 2


                Dim gen As Epifani.ListaGenerale

                For Each gen In lstListaRapporti
                    If ElencoID_Rapporti <> "" Then
                        ElencoID_Rapporti = ElencoID_Rapporti & "," & gen.STR
                    Else
                        ElencoID_Rapporti = gen.STR
                    End If
                Next

                ' APRO CONNESSIONE
                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Dim sStr1 As String

                'sStr1 = "select * from SISCOM_MI.ANAGRAFICA where ID in (select ID_CONTRATTO from SISCOM_MI.morosita_lettere where ID_CONTRATTO in (" & ElencoID_Rapporti & ")) " _
                '    & " order by COGNOME, NOME"

                sStr1 = "select  UNITA_IMMOBILIARI.COD_TIPOLOGIA," _
                        & " INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO""," _
                        & " INDIRIZZI.CIVICO,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                        & " (UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE) as  COD_UNITA_IMMOBILIARE ," _
                        & " (RAPPORTI_UTENZA.COD_CONTRATTO) as  COD_CONTRATTO ," _
                        & "RAPPORTI_UTENZA.ID,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC," _
                            & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                            & "     then  RAGIONE_SOCIALE " _
                            & "     else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end  as  INTESTATARIO ," _
                        & " ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.RAGIONE_SOCIALE,ANAGRAFICA.PARTITA_IVA," _
                        & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO"" " _
              & " from  " _
                 & " SISCOM_MI.RAPPORTI_UTENZA,  " _
                 & " SISCOM_MI.INDIRIZZI, " _
                 & " SISCOM_MI.UNITA_IMMOBILIARI,  " _
                 & " SISCOM_MI.UNITA_CONTRATTUALE,  " _
                 & " SISCOM_MI.ANAGRAFICA, " _
                 & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                 & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE " _
            & " where  " _
                  & "      UNITA_IMMOBILIARI.ID_INDIRIZZO            =INDIRIZZI.ID (+) " _
                  & " and  UNITA_CONTRATTUALE.ID_UNITA               =UNITA_IMMOBILIARI.ID   " _
                  & " and  RAPPORTI_UTENZA.ID 					     =UNITA_CONTRATTUALE.ID_CONTRATTO " _
                  & " and  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA       =ANAGRAFICA.ID " _
                  & " and  RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  =TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                  & " and  SOGGETTI_CONTRATTUALI.ID_CONTRATTO        =RAPPORTI_UTENZA.ID  " _
                  & " and  SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                  & " and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE     is null   "

                sStr1 = sStr1 & "  and RAPPORTI_UTENZA.ID in (select ID_CONTRATTO from SISCOM_MI.morosita_lettere where ID_CONTRATTO in (" & ElencoID_Rapporti & ")) "

                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1
                myReaderT = par.cmd.ExecuteReader

                While myReaderT.Read

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(myReaderT("COD_CONTRATTO"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(myReaderT("INTESTATARIO"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(myReaderT("COD_FISCALE"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(myReaderT("COD_TIPOLOGIA_CONTR_LOC"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(myReaderT("POSIZIONE_CONTRATTO"), "")), 0)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(myReaderT("COD_UNITA_IMMOBILIARE"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(myReaderT("COD_TIPOLOGIA"), "")), 0)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(myReaderT("INDIRIZZO"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(myReaderT("CIVICO"), "")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(myReaderT("COMUNE_UNITA"), "")), 0)

                    i = i + 1
                    K = K + 1
                End While
                myReaderT.Close()

                'par.cmd.Dispose()
                'par.OracleConn.Close()
                'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                .CloseFile()
            End With

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")

            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            '
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)

            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            File.Delete(strFile)


            Response.Redirect("..\FileTemp\" & sNomeFile & ".zip", False)



            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
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

    Private Sub calcolaSomme()
        For Each di As DataGridItem In DataGrid1.Items
            DirectCast(di.Cells(2).FindControl("CheckBox1"), CheckBox).Attributes.Add("onclick", "javascript:Somma(" & par.VirgoleInPunti(di.Cells(4).Text.Replace("&nbsp;", "0").Replace(".", "")) & "," & par.VirgoleInPunti(di.Cells(5).Text.Replace("&nbsp;", "0").Replace(".", "")) & "," & par.VirgoleInPunti(di.Cells(6).Text.Replace("&nbsp;", "0").Replace(".", "")) & ",this);")
        Next
    End Sub


End Class
