Imports System.Collections
Imports System.Math
Imports Telerik.Web.UI

Partial Class Tab_Manu_Dettagli
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstInterventi As System.Collections.Generic.List(Of Epifani.Manutenzioni_Interventi)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        lstInterventi = CType(HttpContext.Current.Session.Item("LSTINTERVENTI"), System.Collections.Generic.List(Of Epifani.Manutenzioni_Interventi))

        Try
            If Not IsPostBack Then


                If Not IsNothing(lstInterventi) Then

                lstInterventi.Clear()
                End If

                ricaricaValori()

                ' CONNESSIONE DB
                IdConnessione = CType(Me.Page.FindControl("txtConnessione"), TextBox).Text
                Me.txtIdConnessione.Text = IdConnessione

                If PAR.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    'PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    '''par.cmd.Transaction = par.myTrans
                End If
                ''''''''''''''''''''''''''

                ''BindGrid_Interventi() richiamato da BindGrid_Consuntivi()
                BindGrid_Consuntivi()

                CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "0"


            End If


            vIdManutenzione = CType(Me.Page.FindControl("txtIdManutenzione"), TextBox).Text

            'vIdComplesso = CType(Me.Page.FindControl("txtIdComplesso"), TextBox).Text
            'vIdEdificio = CType(Me.Page.FindControl("txtIdEdificio"), TextBox).Text
            'vIdScala = CType(Me.Page.FindControl("txtIdScala"), TextBox).Text


            If CType(Me.Page.FindControl("txtIdComplesso"), TextBox).Text = "" Or CType(Me.Page.FindControl("txtIdComplesso"), TextBox).Text = "0" Then
                CType(Me.Page.FindControl("txtIdComplesso"), TextBox).Text = "-1"
                vIdComplesso = -1
            Else
                vIdComplesso = CType(Me.Page.FindControl("txtIdComplesso"), TextBox).Text
            End If
            If CType(Me.Page.FindControl("txtIdEdificio"), TextBox).Text = "" Or CType(Me.Page.FindControl("txtIdEdificio"), TextBox).Text = "0" Then
                CType(Me.Page.FindControl("txtIdEdificio"), TextBox).Text = "-1"
                vIdEdificio = -1
            Else
                vIdEdificio = CType(Me.Page.FindControl("txtIdEdificio"), TextBox).Text
            End If

            If CType(Me.Page.FindControl("txtIdScala"), TextBox).Text = "" Then
                vIdScala = -1
            Else
                vIdScala = CType(Me.Page.FindControl("txtIdScala"), TextBox).Text
            End If


            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub ricaricaValori()
        vIdManutenzione = CType(Me.Page.FindControl("txtIdManutenzione"), TextBox).Text
        txtIdManuPadre.Value = CType(Me.Page.FindControl("txtIdManutenzione"), TextBox).Text

        If CType(Me.Page.FindControl("txtIdComplesso"), TextBox).Text = "" Then
            vIdComplesso = -1
        Else
            vIdComplesso = CType(Me.Page.FindControl("txtIdComplesso"), TextBox).Text
        End If
        If CType(Me.Page.FindControl("txtIdEdificio"), TextBox).Text = "" Then
            vIdEdificio = -1
        Else
            vIdEdificio = CType(Me.Page.FindControl("txtIdEdificio"), TextBox).Text
        End If

        If CType(Me.Page.FindControl("txtIdScala"), TextBox).Text = "" Then
            vIdScala = -1
        Else
            vIdScala = CType(Me.Page.FindControl("txtIdScala"), TextBox).Text
        End If


        'vIdSegnalazione = PAR.IfEmpty(CType(Me.Page.FindControl("txtID_Segnalazioni"), HiddenField).Value, 0)
        'vIdUnita = PAR.IfEmpty(CType(Me.Page.FindControl("txtID_Unita"), HiddenField).Value, 0)
    End Sub

    Private Property vIdManutenzione() As Long
        Get
            If Not (ViewState("par_idManutenzione") Is Nothing) Then
                Return CLng(ViewState("par_idManutenzione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idManutenzione") = value
        End Set

    End Property

    Private Property vIdComplesso() As Long
        Get
            If Not (ViewState("par_idComplesso") Is Nothing) Then
                Return CLng(ViewState("par_idComplesso"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idComplesso") = value
        End Set

    End Property

    Private Property vIdEdificio() As Long
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


    Private Property vIdScala() As Long
        Get
            If Not (ViewState("par_idScala") Is Nothing) Then
                Return CLng(ViewState("par_idScala"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idScala") = value
        End Set

    End Property

    Private Property vIdSegnalazione() As Long
        Get
            If Not (ViewState("par_idSegnalazioni") Is Nothing) Then
                Return CLng(ViewState("par_idSegnalazioni"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idSegnalazioni") = value
        End Set

    End Property


    Public Property IdConnessione() As String
        Get
            If Not (ViewState("par_Connessione") Is Nothing) Then
                Return CStr(ViewState("par_Connessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Connessione") = value
        End Set

    End Property

    Private Property vIdUnita() As Long
        Get
            If Not (ViewState("par_idUnita") Is Nothing) Then
                Return CLng(ViewState("par_idUnita"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idUnita") = value
        End Set

    End Property

    'INTERVENTI GRID1
    Public Sub BindGrid_Interventi()
        Dim StringaSql As String

        If vIdManutenzione = -1 Then
            ricaricaValori()
        End If

        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        PAR.SettaCommand(PAR)

        If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
            PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
        End If

        Dim sSQL_DettaglioIMPIANTO As String
        sSQL_DettaglioIMPIANTO = "(CASE IMPIANTI.COD_TIPOLOGIA " _
                                    & " WHEN 'AN' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'CF' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'CI' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'EL' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'GA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'ID' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'ME' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'SO' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||" _
                                            & "(select  (CASE when SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO is null THEN " _
                                                            & " 'Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA " _
                                                    & " ELSE 'Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO END) " _
                                            & " from SISCOM_MI.I_SOLLEVAMENTO where ID=IMPIANTI.ID) " _
                                    & " WHEN 'TA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TE' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TR' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TU' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TV' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                & " ELSE '' " _
                                & " END) as DETTAGLIO "

        Dim aggiunta As String = ""

        Dim stato As String = CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value
        '*********** modifica marco 23/07/2015 ************
        'If stato = "0" Then
        '    aggiunta = " union select null as ID,null as TIPOLOGIA,null as ID_IMPIANTO,null as ID_COMPLESSO,null as ID_EDIFICIO,null as ID_UNITA_IMMOBILIARE,null as ID_UNITA_COMUNE," _
        '           & "      null as DETTAGLIO,'' AS IMPORTO_PRESUNTO," _
        '           & "      null as IMPORTO_CONSUNTIVO," _
        '           & "      null as IMPORTO_RIMBORSO," _
        '           & "      null as FL_BLOCCATO " _
        '           & " from dual  "
        'Else
        '    aggiunta = ""
        'End If
        '***************************************************


        StringaSql = " select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & sSQL_DettaglioIMPIANTO & ",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
				  & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
					& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
					& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
				   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.IMPIANTI" _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO is not null and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO=SISCOM_MI.IMPIANTI.ID (+) " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "       COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
				   & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
			& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
			& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
				   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='COMPLESSO' and  SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "        (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
				   & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
			& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
			& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
				   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='EDIFICIO' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "       (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||(select descrizione from siscom_mi.scale_edifici where siscom_mi.scale_edifici.id=unita_immobiliari.id_scala)||' - -Interno '||SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
				  & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
				   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INTESTATARI_UI " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA IMMOBILIARE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID (+) and	SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID  (+)  and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "      (SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
				   & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE  " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA COMUNE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE=SISCOM_MI.UNITA_COMUNI.ID (+) and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "      (SISCOM_MI.edifici.denominazione||' - '||siscom_mi.scale_edifici.descrizione) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
				   & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
				   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.SCALE_EDIFICI  " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='SCALA' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+) and SISCOM_MI.SCALE_EDIFICI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID " _
        & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,'' AS TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "      '' AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
				   & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
				   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
             & " from SISCOM_MI.MANUTENZIONI_INTERVENTI   " _
                  & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione & " and (SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA is null or tipologia='-1') "


        PAR.cmd.Parameters.Clear()
        PAR.cmd.CommandText = StringaSql
        Dim dat As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataTable 'Data.DataSet()

        da.Fill(ds)


        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        da.Dispose()
        ds.Dispose()

        '*** SOMMA IMPORTO
        Dim SommaTot As Decimal = 0
        Dim i As Integer


        Dim tipologia As String = ""
        Dim sStr1 As String = ""
        For Each elemento As GridDataItem In DataGrid1.Items
            'If String.IsNullOrEmpty(elemento.Cells(PAR.IndRDGC(DataGrid1, "TIPOLOGIA")).Text.ToString.Replace("&nbsp;", "")) Then
            '    CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue = "-1"
            '    tipologia = "-1"
            'Else

            '    CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue = elemento.Cells(PAR.IndRDGC(DataGrid1, "TIPOLOGIA")).Text.ToString.Replace("&nbsp;", "")
            '    tipologia = CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue
            'End If
            CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue = elemento.Cells(PAR.IndRDGC(DataGrid1, "TIPOLOGIA")).Text.ToString.Replace("&nbsp;", "")
            tipologia = CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue

            CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Items.Clear()
            CType(elemento.FindControl("TextBoxIvaRimborso"), DropDownList).SelectedValue = CInt(PAR.IfEmpty(elemento.Cells(PAR.IndRDGC(DataGrid1, "PERC_IVA_RIMBORSO")).Text.Replace("&nbsp;", ""), "0"))
            Select Case tipologia
                Case "SCALA"
                    If vIdEdificio <> -1 Then
                        sStr1 = "select ID,(select denominazione from siscom_mi.edifici where edifici.id=scale_edifici.id_Edificio)||'- Scala '||DESCRIZIONE as denominazione from SISCOM_MI.scale_edifici where ID_EDIFICIO=" & vIdEdificio & " order by denominazione asc"
                    ElseIf vIdComplesso <> -1 Then
                        sStr1 = "select ID,(select denominazione from siscom_mi.edifici where edifici.id=scale_edifici.id_Edificio)||'- Scala '||DESCRIZIONE as denominazione from SISCOM_MI.scale_edifici where ID_edificio IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO=" & vIdComplesso & ") order by denominazione asc"
                    End If
                Case "COMPLESSO"
                    If vIdComplesso <> -1 Then
                        sStr1 = "select ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=" & vIdComplesso & " order by DENOMINAZIONE asc"
                    ElseIf vIdEdificio <> -1 Then
                        sStr1 = "select ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID in (select id_complesso from siscom_mi.edifici where id=" & vIdEdificio & ") order by DENOMINAZIONE asc"
                    End If
                Case "EDIFICIO"
                    If vIdEdificio <> -1 Then
                        sStr1 = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID=" & vIdEdificio & " order by DENOMINAZIONE asc"
                    Else
                        If vIdComplesso <> -1 Then
                            sStr1 = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID_COMPLESSO=" & vIdComplesso & " order by DENOMINAZIONE asc"
                        Else
                            Dim idApp As String = CType(Me.Page.FindControl("txtIdAppalto"), HiddenField).Value
                            If IsNumeric(idApp) Then
                                sStr1 = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI,siscom_mi.appalti_lotti_patrimonio where appalti_lotti_patrimonio.id_edificio=edifici.id and id_appalto=" & idApp & " order by DENOMINAZIONE asc"
                            Else
                                sStr1 = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI order by DENOMINAZIONE asc"
                            End If
                        End If
                    End If
                Case "UNITA IMMOBILIARE"
                    If vIdEdificio <> -1 Then
                        sStr1 = "select SISCOM_MI.UNITA_IMMOBILIARI.ID," _
                                & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||" _
                                 & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE||' - -Piano '||" _
                                 & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE||' - -Interno '||" _
                                 & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) as DENOMINAZIONE  " _
                            & " from SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INTESTATARI_UI" _
                            & " where SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=" & vIdEdificio

                    Else
                        'ok
                        sStr1 = "select SISCOM_MI.UNITA_IMMOBILIARI.ID," _
                                & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||" _
                                 & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE||' - -Piano '||" _
                                 & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE||' - -Interno '||" _
                                 & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) as DENOMINAZIONE " _
                            & " from SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INTESTATARI_UI" _
                            & " where SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO in ( select ID from SISCOM_MI.EDIFICI " _
                                     & " where SISCOM_MI.EDIFICI.ID_COMPLESSO=" & vIdComplesso & ")"

                    End If

                    If vIdScala <> -1 Then
                        sStr1 = sStr1 & " and SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=" & vIdScala
                    End If

                    sStr1 = sStr1 & " and SISCOM_MI.EDIFICI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO  (+) " _
                                  & " and SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+) " _
                                  & " and SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO=SISCOM_MI.TIPO_LIVELLO_PIANO.COD (+) " _
                                  & " and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) " _
                    & " order by DENOMINAZIONE,SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE,SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE asc"
                Case "UNITA COMUNE"
                    If vIdEdificio <> -1 Then
                        'ok
                        sStr1 = "select SISCOM_MI.UNITA_COMUNI.ID," _
                                     & "(SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) as DENOMINAZIONE " _
                             & " from SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE " _
                             & " where SISCOM_MI.UNITA_COMUNI.ID_EDIFICIO=" & vIdEdificio _
                             & " and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
                             & " order by SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE,SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE asc "
                    Else
                        'ok
                        sStr1 = "select SISCOM_MI.UNITA_COMUNI.ID," _
                                     & "(SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) as DENOMINAZIONE " _
                             & " from  SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE " _
                             & " where SISCOM_MI.UNITA_COMUNI.ID_COMPLESSO=" & vIdComplesso _
                             & " and   SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
                             & " order by SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE,SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE asc "
                    End If
                Case Else
                    'ok
                    If tipologia = "SOLLEVAMENTO" Then
                        If vIdEdificio <> -1 Then
                            sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                            & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO||' - - Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO||' - - Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA) as DENOMINAZIONE " _
                                 & " from SISCOM_MI.IMPIANTI,SISCOM_MI.I_SOLLEVAMENTO " _
                                 & " where SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & vIdEdificio _
                                 & "   and SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "') " _
                                 & "   and SISCOM_MI.IMPIANTI.ID=SISCOM_MI.I_SOLLEVAMENTO.ID (+) "
                        Else
                            sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                            & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO||' - - Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO||' - - Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA) as DENOMINAZIONE " _
                                 & " from SISCOM_MI.IMPIANTI,SISCOM_MI.I_SOLLEVAMENTO " _
                                 & " where SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & vIdComplesso _
                                 & "   and SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "') " _
                                 & "   and SISCOM_MI.IMPIANTI.ID=SISCOM_MI.I_SOLLEVAMENTO.ID (+) "


                        End If
                    Else
                        If vIdEdificio <> -1 Then
                            sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                            & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO) as DENOMINAZIONE " _
                                 & " from SISCOM_MI.IMPIANTI " _
                                 & " where SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & vIdEdificio _
                                 & " and   SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "')"

                        Else
                            sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                            & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO) as DENOMINAZIONE " _
                                 & " from SISCOM_MI.IMPIANTI " _
                                 & " where SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & vIdComplesso _
                                 & " and   SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "')"
                        End If
                    End If
            End Select
            PAR.caricaComboBox(sStr1, CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList), "ID", "DENOMINAZIONE", True, "NULL", "- - -")

        Next

        For i = 0 To Me.DataGrid1.Items.Count - 1
            If Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text = "&nbsp;" Then Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text = ""
            SommaTot = SommaTot + PAR.IfEmpty(Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text, 0)
            CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "TIPOLOGIA")).Text
            Select Case DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "TIPOLOGIA")).Text
                Case "COMPLESSO"
                    CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "ID_COMPLESSO")).Text
                Case "EDIFICIO"
                    CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "ID_EDIFICIO")).Text
                Case "UNITA IMMOBILIARE"
                    CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "ID_UNITA_IMMOBILIARE")).Text
                Case "UNITA COMUNE"
                    CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "ID_UNITA_COMUNE")).Text
                Case "SCALA"
                    CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "ID_SCALA")).Text
                Case Else
                    CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "ID_IMPIANTO")).Text
            End Select

            If stato = "0" Then
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Enabled = False
            ElseIf stato = "1" Or stato = "2" Then
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Enabled = True
            Else
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Enabled = False
            End If
            CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).ToolTip = CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedItem.Text
            CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).ToolTip = CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedItem.Text

            If CType(Me.Page.FindControl("cmbIndirizzo"), RadComboBox).SelectedValue = "-1" Then
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue = "-1"
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = False
            End If

            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal(this,2);")
            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
			'CType(Me.DataGrid1.Items(i).FindControl("TextBoxIvaRimborso"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        Next i

        If Me.DataGrid1.Items.Count = 1 Then
            'Me.txtSel1.Text = PAR.IfEmpty(Me.DataGrid1.Items(0).Cells(PAR.IndrDGC(DataGrid1, "TIPOLOGIA")).Text, "")
            Me.txtIdComponente.Text = PAR.IfEmpty(Me.DataGrid1.Items(0).Cells(PAR.IndRDGC(DataGrid1, "ID")).Text, 0)
            Me.txt_FL_BLOCCATO.Value = PAR.IfEmpty(Me.DataGrid1.Items(0).Cells(PAR.IndRDGC(DataGrid1, "FL_BLOCCATO")).Text, 0)
        End If

        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImporto"), TextBox).Text = IsNumFormat(SommaTot, "", "##,##0.00")
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoControllo"), HiddenField).Value = IsNumFormat(SommaTot, "", "##,##0.00")

        CalcolaImporti(SommaTot, PAR.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))

        'Me.txtImportoTotale.Text = Format(SommaTot, "##,##0.00")
        '**********************
        SettaMisure()

        AbilitaDisabilita()

    End Sub

    Protected Sub DataGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGrid1.ItemCommand
        Try
            If e.CommandName = "Delete" Then
                ' EliminaDettaglio()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Property vIdAppalto() As Long
        Get
            If Not (ViewState("par_IdAppalto") Is Nothing) Then
                Return CLng(ViewState("par_IdAppalto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IdAppalto") = value
        End Set

    End Property

    Public Property vIdPF_VOCE() As Long
        Get
            If Not (ViewState("par_PF_VOCE") Is Nothing) Then
                Return CLng(ViewState("par_PF_VOCE"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_PF_VOCE") = value
        End Set

    End Property

    Sub SettaMisure()
        Dim myReaderTMP1 As Oracle.DataAccess.Client.OracleDataReader

        Try

            ' RIPRENDO LA CONNESSIONE
            PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            'CAMPI per il controllo del prezzo, non deve superare quello residuo
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select MANUTENZIONI.IVA_CONSUMO,APPALTI_VOCI_PF.PERC_ONERI_SIC_CON,APPALTI_VOCI_PF.SCONTO_CONSUMO,APPALTI.FL_RIT_LEGGE,MANUTENZIONI.ID_APPALTO,MANUTENZIONI.ID_PF_VOCE " _
                               & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI, SISCOM_MI.APPALTI_VOCI_PF " _
                               & " where MANUTENZIONI.ID=" & vIdManutenzione _
                               & "   and APPALTI.ID=MANUTENZIONI.ID_APPALTO" _
                               & "   and APPALTI_VOCI_PF.ID_APPALTO = MANUTENZIONI.ID_APPALTO  " _
                               & "   and APPALTI_VOCI_PF.ID_PF_VOCE=MANUTENZIONI.ID_PF_VOCE "

            myReaderTMP1 = par.cmd.ExecuteReader()

            If myReaderTMP1.Read Then
                CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value = PAR.IfNull(myReaderTMP1("IVA_CONSUMO"), -1)
                'Me.txtPercIVA_P.Value = PAR.IfNull(myReaderTMP1("IVA_CONSUMO"), -1)           'MANUTENZIONI

                CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value = PAR.IfNull(myReaderTMP1("PERC_ONERI_SIC_CON"), -1)
                'Me.txtPercOneri.Value = PAR.IfNull(myReaderTMP1("PERC_ONERI_SIC_CON"), 0)   'APPALTI_VOCI_PF

                CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value = PAR.IfNull(myReaderTMP1("SCONTO_CONSUMO"), -1)
                'Me.txtScontoConsumo.Value = par.IfNull(myReaderTMP1("SCONTO_CONSUMO"), 0)   'APPALTI_VOCI_PF

                CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value = PAR.IfNull(myReaderTMP1("FL_RIT_LEGGE"), -1)
                'Me.txtFL_RIT_LEGGE.Value = par.IfNull(myReaderTMP1("FL_RIT_LEGGE"), 0)  'APPALTI

                vIdAppalto = par.IfNull(myReaderTMP1("ID_APPALTO"), -1)
                vIdPF_VOCE = par.IfNull(myReaderTMP1("ID_PF_VOCE"), -1)

            End If
            myReaderTMP1.Close()
            '*************************************



        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            If vIdManutenzione <> -1 Then
                par.myTrans.Rollback()

                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, PAR.myTrans)

            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try


    End Sub

    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
    '                            & "document.getElementById('Tab_Manu_Dettagli_txtIdComponente').value='" & e.Item.Cells(PAR.IndrDGC(DataGrid1, "ID")).Text & "';document.getElementById('Tab_Manu_Dettagli_txt_FL_BLOCCATO').value='" & e.Item.Cells(PAR.IndrDGC(DataGrid1, "FL_BLOCCATO")).Text & "'")
    '    End If
    'End Sub
    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('Tab_Manu_Dettagli_txtIdComponente').value='" & e.Item.Cells(PAR.IndRDGC(DataGrid1, "ID")).Text & "';document.getElementById('Tab_Manu_Dettagli_txt_FL_BLOCCATO').value='" & e.Item.Cells(PAR.IndRDGC(DataGrid1, "FL_BLOCCATO")).Text & "'")
        End If
    End Sub
    Function ControlloCampiInterventi() As Boolean

        ControlloCampiInterventi = True


        'If PAR.IfEmpty(Me.cmbTipologia.Text, "Null") = "Null" Or Me.cmbTipologia.Text = "-1" Then
        '    Response.Write("<script>alert('Inserire la tipologia oggetto!');</script>")
        '    ControlloCampiInterventi = False
        '    cmbTipologia.Focus()
        '    Exit Function
        'End If


        'If PAR.IfEmpty(Me.cmbDettaglio.Text, "Null") = "Null" Or Me.cmbDettaglio.Text = "-1" Then
        '    Response.Write("<script>alert('Inserire il dettaglio tipologia oggetto!');</script>")
        '    ControlloCampiInterventi = False
        '    cmbDettaglio.Focus()
        '    Exit Function
        'End If

        If PAR.IfEmpty(Me.txtImporto.Text, "Null") = "Null" Then
            RadWindowManager1.RadAlert("Inserire l\'importo presunto!", 300, 150, "Attenzione", "", "null")
            ControlloCampiInterventi = False
            txtImporto.Focus()
            Exit Function
        End If

        'Controllo che la somma degli importi non superi quello RESIDUO TOTALE (APPALTI_LOTTI_SERVIZI.RESIDUO_CONSUMO)

        Dim SommaTot As Decimal = 0
        Dim SommaTot1 As Decimal = 0
        Dim i As Integer

        '[SOMMA DI TUTTO CIO' CHE HO PREVENTIVATO dalla GRIGLIA]
        For i = 0 To Me.DataGrid1.Items.Count - 1
            If Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text = "&nbsp;" Then Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text = ""
            SommaTot = SommaTot + PAR.IfEmpty(Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text, 0)
        Next i

        SommaTot = SommaTot - CDbl(PAR.IfEmpty(Me.txtImportoODL.Value, 0)) + CDbl(Me.txtImporto.Text)

        SommaTot1 = CalcolaImportiControllo(SommaTot, PAR.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))
        '*****************

        'SommaTot1                =[SOMMA DI TUTTO CIO' CHE HO PREVENTIVATO dalla GRIGLIA] 
        'Ricalcola_ImportoResiduo =[ (TOTALE DISPONIBILE x APPALTO - Tutto quello PREVENT e CONSUN e EMESSO SAL per APPALTO tranne la manutenzione in questione]
        'Esempio: 10000 totale;  4000 già impeganto da altri; 2000 mio (1000 già salvato 1000 messo ora) 
        '         quindi 2000 non deve superare (10000-4000)=6000

        If Math.Round(SommaTot1, 2) > Math.Round(Ricalcola_ImportoResiduo(), 2) Then
            'If SommaTot1 > CDbl(txtResiduoConsumo.Value) Then
            RadWindowManager1.RadAlert("L\'importo inserito è superiore all\'importo contrattuale residuo!", 300, 150, "Attenzione", "", "null")
            ControlloCampiInterventi = False
            txtImporto.Focus()
            Exit Function
        End If
    End Function

    Protected Sub btn_Inserisci1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Inserisci1.Click
        If ControlloCampiInterventi() = False Then
            txtAppare1.Text = "1"
            Exit Sub
        End If

        If Me.txtID1.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaInterventi()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateInterventi()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        'txtSel1.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_Chiudi1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Chiudi1.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        ' CType(Me.Page.FindControl("txtSommaInterventi"), HiddenField).Value = 0
        'txtSel1.Text = ""
        txtIdComponente.Text = ""
    End Sub


    Private Sub SalvaInterventi()
        Dim idC As Long
        Dim idE As Long
        Dim idUM As Long
        Dim idUC As Long
        Dim idI As Long
        Dim idS As Long

        Try

            idC = -1
            idE = -1
            idUM = -1
            idUC = -1
            idI = -1
            IdS = -1


            Select Case Me.cmbTipologia.SelectedValue
                Case "COMPLESSO"
                    idC = cmbDettaglio.SelectedValue

                Case "EDIFICIO"
                    idE = cmbDettaglio.SelectedValue

                Case "UNITA IMMOBILIARE"
                    idUM = cmbDettaglio.SelectedValue


                Case "UNITA COMUNE"
                    idUC = cmbDettaglio.SelectedValue
                Case "SCALA"
                    idS = cmbDettaglio.SelectedValue

                Case Else
                    idI = cmbDettaglio.SelectedValue
            End Select


            If vIdManutenzione = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.Manutenzioni_Interventi

                Dim tipologia As String = ""
                If cmbTipologia.SelectedValue <> "" And cmbTipologia.SelectedValue <> "-1" Then
                    tipologia = cmbTipologia.SelectedValue
                End If
                gen = New Epifani.Manutenzioni_Interventi(lstInterventi.Count, tipologia, idI, idC, idE, idUM, idUC, PAR.PulisciStrSql(cmbDettaglio.SelectedItem.Text), txtImporto.Text, "", "", 0, ids)

                DataGrid1.DataSource = Nothing
                lstInterventi.Add(gen)
                gen = Nothing

                DataGrid1.DataSource = lstInterventi
                DataGrid1.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                PAR.SettaCommand(PAR)

                'RIPRENDO LA TRANSAZIONE
                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans
                End If

                PAR.cmd.Parameters.Clear()
                PAR.cmd.CommandText = "insert into SISCOM_MI.MANUTENZIONI_INTERVENTI  " _
                                            & " (ID, ID_MANUTENZIONE,ID_IMPIANTO,ID_COMPLESSO,ID_EDIFICIO,ID_UNITA_IMMOBILIARE,ID_UNITA_COMUNE,TIPOLOGIA,IMPORTO_PRESUNTO,ID_sCALA) " _
                                    & "values (SISCOM_MI.SEQ_MANUTENZIONI_INTERVENTI.NEXTVAL,:id_manu,:id_impianto,:id_complesso,:id_edificio,:id_unita,:id_comune,:tipologia,:importo,:ID_sCALA) "

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_manu", vIdManutenzione))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", RitornaNullSeIntegerMenoUno(Convert.ToInt32(idI))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", RitornaNullSeIntegerMenoUno(Convert.ToInt32(idC))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(idE))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_unita", RitornaNullSeIntegerMenoUno(Convert.ToInt32(idUM))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_comune", RitornaNullSeIntegerMenoUno(Convert.ToInt32(idUC))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_sCALA", RitornaNullSeIntegerMenoUno(Convert.ToInt32(idS))))
                If CStr(cmbTipologia.SelectedValue) <> "-1" AndAlso CStr(cmbTipologia.SelectedValue) <> "" Then
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipologia", cmbTipologia.SelectedValue))
                Else
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipologia", DBNull.Value))
                End If
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtImporto.Text.Replace(".", ""))))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                BindGrid_Interventi()

                '*** EVENTI_MANUTENZIONE
                PAR.InserisciEventoManu(PAR.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLI_MANUTENZIONE, "Intervento di Manutenzione")

            End If

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

            '*** SOMMA IMPORTO
            Dim SommaTot As Decimal = 0
            Dim i As Integer

            For i = 0 To Me.DataGrid1.Items.Count - 1
                If Me.DataGrid1.Items(i).Cells(PAR.IndrDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text = "&nbsp;" Then Me.DataGrid1.Items(i).Cells(PAR.IndrDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text = ""
                SommaTot = SommaTot + PAR.IfEmpty(Me.DataGrid1.Items(i).Cells(PAR.IndrDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text, 0)
            Next i


            CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImporto"), TextBox).Text = IsNumFormat(SommaTot, "", "##,##0.00")
            CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoControllo"), HiddenField).Value = IsNumFormat(SommaTot, "", "##,##0.00")
            CalcolaImporti(SommaTot, PAR.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))
            '**********************

            AbilitaDisabilita()

            '' COMMIT
            'par.myTrans.Commit()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"


            If vIdManutenzione <> -1 Then
                PAR.myTrans.Rollback()

                'PAR.myTrans = par.OracleConn.BeginTransaction()
                '''par.cmd.Transaction = par.myTrans
                'HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, PAR.myTrans)
            End If

            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & IdConnessione)

            Page.Dispose()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub UpdateInterventi()
        Dim idC As Long
        Dim idE As Long
        Dim idUM As Long
        Dim idUC As Long
        Dim idI As Long
        Dim idS As Long

        Try

            idC = -1
            idE = -1
            idUM = -1
            idUC = -1
            idI = -1
            IdS = -1

            Select Case Me.cmbTipologia.SelectedValue
                Case "COMPLESSO"
                    idC = cmbDettaglio.SelectedValue
                Case "SCALA"
                    idS = cmbDettaglio.SelectedValue

                Case "EDIFICIO"
                    idE = cmbDettaglio.SelectedValue

                Case "UNITA IMMOBILIARE"
                    idUM = cmbDettaglio.SelectedValue


                Case "UNITA COMUNE"
                    idUC = cmbDettaglio.SelectedValue

                Case Else
                    idI = cmbDettaglio.SelectedValue
            End Select

            If vIdManutenzione = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA MANUTENZIONI

                Dim tipologia As String = ""
                If cmbTipologia.SelectedValue <> "" And cmbTipologia.SelectedValue <> "-1" Then
                    tipologia = cmbTipologia.SelectedValue
                End If
                lstInterventi(txtIdComponente.Text).TIPOLOGIA = tipologia
                'lstInterventi(txtIdComponente.Text).TIPOLOGIA = Me.cmbTipologia.SelectedValue

                lstInterventi(txtIdComponente.Text).ID_COMPLESSO = idC
                lstInterventi(txtIdComponente.Text).ID_EDIFICIO = idE
                lstInterventi(txtIdComponente.Text).ID_UNITA_IMMOBILIARE = idUM
                lstInterventi(txtIdComponente.Text).ID_UNITA_COMUNE = idUC
                lstInterventi(txtIdComponente.Text).ID_IMPIANTO = idI
                lstInterventi(txtIdComponente.Text).ID_SCALA = idS

                lstInterventi(txtIdComponente.Text).DETTAGLIO = PAR.PulisciStrSql(cmbDettaglio.SelectedItem.Text)
                lstInterventi(txtIdComponente.Text).IMPORTO_PRESUNTO = Me.txtImporto.Text
                lstInterventi(txtIdComponente.Text).IMPORTO_CONSUNTIVO = ""
                lstInterventi(txtIdComponente.Text).IMPORTO_RIMBORSO = ""
                lstInterventi(txtIdComponente.Text).FL_BLOCCATO = 0

                DataGrid1.DataSource = lstInterventi
                DataGrid1.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA MANUTENZIONI

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                PAR.SettaCommand(PAR)

                'RIPRENDO LA TRANSAZIONE
                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans
                End If

                PAR.cmd.Parameters.Clear()
                PAR.cmd.CommandText = "update  SISCOM_MI.MANUTENZIONI_INTERVENTI  set " _
                                            & "ID_IMPIANTO=:id_impianto,ID_COMPLESSO=:id_complesso," _
                                            & "ID_EDIFICIO=:id_edificio,ID_UNITA_IMMOBILIARE=:id_unita," _
                                            & "ID_UNITA_COMUNE=:id_comune,TIPOLOGIA=:tipologia,IMPORTO_PRESUNTO=:importo,ID_sCALA=:id_scala " _
                                    & " where ID=" & Me.txtID1.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", RitornaNullSeIntegerMenoUno(Convert.ToInt32(idI))))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", RitornaNullSeIntegerMenoUno(Convert.ToInt32(idC))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(idE))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_unita", RitornaNullSeIntegerMenoUno(Convert.ToInt32(idUM))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_comune", RitornaNullSeIntegerMenoUno(Convert.ToInt32(idUC))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", RitornaNullSeIntegerMenoUno(Convert.ToInt32(IdS))))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipologia", Me.cmbTipologia.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtImporto.Text.Replace(".", ""))))


                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                BindGrid_Interventi()

                '*** EVENTI_MANUTENZIONE
                PAR.InserisciEventoManu(PAR.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLI_MANUTENZIONE, "Intervento di Manutenzione")


            End If

            '*** SOMMA IMPORTO
            Dim SommaTot As Decimal = 0
            Dim i As Integer

            For i = 0 To Me.DataGrid1.Items.Count - 1
                If Me.DataGrid1.Items(i).Cells(PAR.IndrDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text = "&nbsp;" Then Me.DataGrid1.Items(i).Cells(PAR.IndrDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text = ""
                SommaTot = SommaTot + PAR.IfEmpty(Me.DataGrid1.Items(i).Cells(PAR.IndrDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text, 0)
            Next i

            CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImporto"), TextBox).Text = IsNumFormat(SommaTot, "", "##,##0.00")
            CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoControllo"), HiddenField).Value = IsNumFormat(SommaTot, "", "##,##0.00")
            CalcolaImporti(SommaTot, PAR.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))
            '**********************


            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

            AbilitaDisabilita()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"


            If vIdManutenzione <> -1 Then
                PAR.myTrans.Rollback()

                'PAR.myTrans = par.OracleConn.BeginTransaction()
                '''par.cmd.Transaction = par.myTrans
                'HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, PAR.myTrans)
            End If

            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & IdConnessione)

            Page.Dispose()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    'Protected Sub btnApri1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApri1.Click

    '    Try


    '        If txtIdComponente.Text = "" Then
    '            'Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
    '            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    '            txtAppare1.Text = "0"
    '        Else
    '            If vIdManutenzione = -1 Then
    '                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
    '                Me.txtID1.Text = lstInterventi(txtIdComponente.Text).ID

    '                'Me.cmbTipologia.Items.FindByValue(PAR.IfNull(lstInterventi(txtIdComponente.Text).TIPOLOGIA, "")).Selected = True
    '                'Me.cmbTipologia.SelectedValue = PAR.IfNull(lstInterventi(txtIdComponente.Text).TIPOLOGIA, "-1")
    '                If IsNumeric(PAR.IfNull(lstInterventi(txtIdComponente.Text).TIPOLOGIA, "-1")) And cmbTipologia.Items.Count > 0 Then
    '                    Me.cmbTipologia.SelectedValue = PAR.IfNull(lstInterventi(txtIdComponente.Text).TIPOLOGIA, "-1")
    '                End If

    '                Setta_Dettaglio()

    '                Select Case Me.cmbTipologia.SelectedValue
    '                    Case "COMPLESSO"
    '                        Me.cmbDettaglio.SelectedValue = PAR.IfNull(lstInterventi(txtIdComponente.Text).ID_COMPLESSO, -1)

    '                    Case "EDIFICIO"
    '                        Me.cmbDettaglio.SelectedValue = PAR.IfNull(lstInterventi(txtIdComponente.Text).ID_EDIFICIO, -1)

    '                    Case "UNITA IMMOBILIARE"
    '                        Me.cmbDettaglio.SelectedValue = PAR.IfNull(lstInterventi(txtIdComponente.Text).ID_UNITA_IMMOBILIARE, -1)

    '                    Case "UNITA COMUNE"
    '                        Me.cmbDettaglio.SelectedValue = PAR.IfNull(lstInterventi(txtIdComponente.Text).ID_UNITA_COMUNE, -1)

    '                    Case Else
    '                        Me.cmbDettaglio.SelectedValue = PAR.IfNull(lstInterventi(txtIdComponente.Text).ID_IMPIANTO, -1)
    '                End Select

    '                Me.txtImporto.Text = PAR.IfNull(lstInterventi(txtIdComponente.Text).IMPORTO_PRESUNTO, "")
    '                Me.txtImportoODL.Value = PAR.IfNull(lstInterventi(txtIdComponente.Text).IMPORTO_PRESUNTO, "")

    '            Else
    '                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA 

    '                If PAR.OracleConn.State = Data.ConnectionState.Open Then
    '                    Response.Write("IMPOSSIBILE VISUALIZZARE")
    '                    Exit Sub
    '                Else
    '                    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '                    PAR.SettaCommand(PAR)
    '                    PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
    '                    '‘par.cmd.Transaction = par.myTrans
    '                End If

    '                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

    '                PAR.cmd.Parameters.Clear()
    '                PAR.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI_INTERVENTI where ID=" & Me.txtIdComponente.Text

    '                myReader1 = PAR.cmd.ExecuteReader

    '                If myReader1.Read Then
    '                    Me.txtID1.Text = PAR.IfNull(myReader1("ID"), -1)

    '                    'Me.cmbTipologia.Items.FindByValue(PAR.IfNull(myReader1("TIPOLOGIA"), "")).Selected = True
    '                    Me.cmbTipologia.SelectedValue = PAR.IfNull(myReader1("TIPOLOGIA"), -1)

    '                    Setta_Dettaglio()

    '                    Select Case Me.cmbTipologia.SelectedValue
    '                        Case "COMPLESSO"
    '                            Me.cmbDettaglio.SelectedValue = PAR.IfNull(myReader1("ID_COMPLESSO"), -1)

    '                        Case "EDIFICIO"
    '                            Me.cmbDettaglio.SelectedValue = PAR.IfNull(myReader1("ID_EDIFICIO"), -1)

    '                        Case "UNITA IMMOBILIARE"
    '                            Me.cmbDettaglio.SelectedValue = PAR.IfNull(myReader1("ID_UNITA_IMMOBILIARE"), -1)

    '                        Case "UNITA COMUNE"
    '                            Me.cmbDettaglio.SelectedValue = PAR.IfNull(myReader1("ID_UNITA_COMUNE"), -1)

    '                        Case Else
    '                            Me.cmbDettaglio.SelectedValue = PAR.IfNull(myReader1("ID_IMPIANTO"), -1)
    '                    End Select

    '                    Me.txtImporto.Text = IsNumFormat(PAR.IfNull(myReader1("IMPORTO_PRESUNTO"), 0), "", "##,##0.00")
    '                    Me.txtImportoODL.Value = PAR.IfNull(myReader1("IMPORTO_PRESUNTO"), 0)

    '                End If
    '                myReader1.Close()

    '                If Me.txt_FL_BLOCCATO.Value = 1 Then
    '                    Response.Write("<SCRIPT>alert('Attenzione...Non è possibile modificare la voce perchè proveniente da un ordine emesso integrativo!');</SCRIPT>")
    '                    Me.btn_Inserisci1.Visible = False
    '                Else
    '                    Me.btn_Inserisci1.Visible = True
    '                End If


    '            End If
    '        End If

    '    Catch ex As Exception

    '        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    '        Session.Item("LAVORAZIONE") = "0"


    '        If vIdManutenzione <> -1 Then
    '            PAR.myTrans.Rollback()

    '            'PAR.myTrans = par.OracleConn.BeginTransaction()
    '            '''par.cmd.Transaction = par.myTrans
    '            'HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, PAR.myTrans)
    '        End If

    '        PAR.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '        HttpContext.Current.Session.Remove("CONNESSIONE" & IdConnessione)

    '        Page.Dispose()


    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '    End Try


    'End Sub

    Protected Sub btnElimina1_Click(sender As Object, e As System.EventArgs) Handles btnElimina1.Click
        EliminaDettaglio()
    End Sub

    'Protected Sub btnAgg1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgg1.Click

    '    Me.txtID1.Text = -1


    '    Me.cmbTipologia.SelectedValue = "-1"

    '    Me.cmbDettaglio.Items.Clear()
    '    Me.cmbDettaglio.Items.Add(New ListItem(" ", -1))


    '    Me.txtImporto.Text = ""
    '    Me.txtImportoODL.Value = 0
    '    Me.btn_Inserisci1.Visible = True

    'End Sub


    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function


    Private Sub FrmSolaLettura()

        'Me.btnAgg1.Visible = False
        Me.btnElimina1.Visible = False
        CType(Me.Page.FindControl("HiddenMostraPulsantiDett"), HiddenField).value = 0
        'Me.btnApri1.Visible = False

        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtPenale"), TextBox).ReadOnly = True

        CType(Me.Page.FindControl("txtVisualizza"), HiddenField).Value = 2  ' solo consultazione allegati

        'CType(Me.Page.FindControl("btnSalva"), ImageButton).Visible = False
        'CType(Me.Page.FindControl("btnElimina"), ImageButton).Visible = False

        '*************  08/05/2013 ******************
        'CType(Me.Page.FindControl("btnAnnulla"), ImageButton).Visible = False
        'btnAnnulla comandato da FrmSolaLettura della pagina principale
        '********************************************

        CType(Me.Page.FindControl("btnOrdineIntegrativo"), RadButton).Visible = False

        Dim CTRL As Control = Nothing
        For Each CTRL In Me.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).ReadOnly = False
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            ElseIf TypeOf CTRL Is RadComboBox Then
                DirectCast(CTRL, RadComboBox).Enabled = False
            End If
        Next

        CType(Me.Page.FindControl("cmbIndirizzo"), RadComboBox).Enabled = False
        CType(Me.Page.FindControl("cmbScala"), RadComboBox).Enabled = False

        '    CType(Me.Page.FindControl("cmbServizio"), RadComboBox).Enabled = False
        '    CType(Me.Page.FindControl("cmbServizioVoce"), RadComboBox).Enabled = False

    End Sub



    Protected Sub cmbTipologia_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipologia.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub

    Protected Sub cmbTipologia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipologia.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        Setta_Dettaglio()
    End Sub

    Private Sub Setta_Dettaglio()
        Dim FlagConnessione As Boolean
        Dim sStr1 As String

        Try

            Me.cmbDettaglio.Items.Clear()
            Me.cmbDettaglio.Items.Add(New ListItem(" ", -1))

            FlagConnessione = False
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                PAR.SettaCommand(PAR)

                FlagConnessione = True
            End If


            If Me.cmbTipologia.SelectedValue <> "-1" Then

                Select Case Me.cmbTipologia.SelectedValue
                    Case "SCALA"
                        If vIdEdificio <> -1 Then
                            sStr1 = "select ID,(select denominazione from siscom_mi.edifici where edifici.id=scale_edifici.id_Edificio)||'- Scala '||DESCRIZIONE as denominazione from SISCOM_MI.scale_edifici where ID_EDIFICIO=" & vIdEdificio & " order by denominazione asc"
                        ElseIf vIdComplesso <> -1 Then
                            sStr1 = "select ID,(select denominazione from siscom_mi.edifici where edifici.id=scale_edifici.id_Edificio)||'- Scala '||DESCRIZIONE as denominazione from SISCOM_MI.scale_edifici where ID_edificio IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO=" & vIdComplesso & ") order by denominazione asc"
                        End If
                    Case "COMPLESSO"
                        'ok
                        If vIdComplesso <> -1 Then
                            sStr1 = "select ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=" & vIdComplesso & " order by DENOMINAZIONE asc"
                        ElseIf vIdEdificio <> -1 Then
                            sStr1 = "select ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID in (select id_complesso from siscom_mi.edifici where id=" & vIdEdificio & ") order by DENOMINAZIONE asc"
                        End If

                    Case "EDIFICIO"
                        If vIdEdificio <> -1 Then
                            sStr1 = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID=" & vIdEdificio & " order by DENOMINAZIONE asc"
                        Else
                            'ok
                            sStr1 = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID_COMPLESSO=" & vIdComplesso & " order by DENOMINAZIONE asc"
                        End If

                    Case "UNITA IMMOBILIARE"

                        If vIdEdificio <> -1 Then
                            'ok

                            'ALTER TABLE UNITA_IMMOBILIARI ADD (
                            '                            FOREIGN(KEY(COD_TIPO_LIVELLO_PIANO))
                            ' REFERENCES TIPO_LIVELLO_PIANO (COD));
                            'e la scala non da SCALA ma tramite ID_SCALA che fa riferimento a SCALE_EDIFICI

                            '- -Cod.'||" "SISCOM_MI.EDIFICI.COD_EDIFICIO||' TOLTO 09/02/2011

                            sStr1 = "select SISCOM_MI.UNITA_IMMOBILIARI.ID," _
                                    & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||" _
                                     & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE||' - -Piano '||" _
                                     & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE||' - -Interno '||" _
                                     & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) as DENOMINAZIONE  " _
                                & " from SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INTESTATARI_UI" _
                                & " where SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=" & vIdEdificio

                        Else
                            'ok
                            sStr1 = "select SISCOM_MI.UNITA_IMMOBILIARI.ID," _
                                    & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||" _
                                     & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE||' - -Piano '||" _
                                     & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE||' - -Interno '||" _
                                     & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) as DENOMINAZIONE " _
                                & " from SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INTESTATARI_UI" _
                                & " where SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO in ( select ID from SISCOM_MI.EDIFICI " _
                                         & " where SISCOM_MI.EDIFICI.ID_COMPLESSO=" & vIdComplesso & ")"

                        End If

                        If vIdScala <> -1 Then
                            sStr1 = sStr1 & " and SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=" & vIdScala
                        End If

                        sStr1 = sStr1 & " and SISCOM_MI.EDIFICI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO  (+) " _
                                      & " and SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+) " _
                                      & " and SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO=SISCOM_MI.TIPO_LIVELLO_PIANO.COD (+) " _
                                      & " and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) " _
                        & " order by DENOMINAZIONE,SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE,SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE asc"



                    Case "UNITA COMUNE"

                        If vIdEdificio <> -1 Then
                            'ok
                            sStr1 = "select SISCOM_MI.UNITA_COMUNI.ID," _
                                         & "(SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) as DENOMINAZIONE " _
                                 & " from SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE " _
                                 & " where SISCOM_MI.UNITA_COMUNI.ID_EDIFICIO=" & vIdEdificio _
                                 & " and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
                                 & " order by SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE,SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE asc "
                        Else
                            'ok
                            sStr1 = "select SISCOM_MI.UNITA_COMUNI.ID," _
                                         & "(SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) as DENOMINAZIONE " _
                                 & " from  SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE " _
                                 & " where SISCOM_MI.UNITA_COMUNI.ID_COMPLESSO=" & vIdComplesso _
                                 & " and   SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
                                 & " order by SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE,SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE asc "
                        End If
                    Case Else
                        'ok
                        If cmbTipologia.SelectedValue = "SOLLEVAMENTO" Then
                            If vIdEdificio <> -1 Then
                                sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                                & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO||' - - Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO||' - - Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA) as DENOMINAZIONE " _
                                     & " from SISCOM_MI.IMPIANTI,SISCOM_MI.I_SOLLEVAMENTO " _
                                     & " where SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & vIdEdificio _
                                     & "   and SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & cmbTipologia.SelectedValue & "') " _
                                     & "   and SISCOM_MI.IMPIANTI.ID=SISCOM_MI.I_SOLLEVAMENTO.ID (+) "
                            Else
                                sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                                & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO||' - - Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO||' - - Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA) as DENOMINAZIONE " _
                                     & " from SISCOM_MI.IMPIANTI,SISCOM_MI.I_SOLLEVAMENTO " _
                                     & " where SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & vIdComplesso _
                                     & "   and SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & cmbTipologia.SelectedValue & "') " _
                                     & "   and SISCOM_MI.IMPIANTI.ID=SISCOM_MI.I_SOLLEVAMENTO.ID (+) "


                            End If
                        Else
                            If vIdEdificio <> -1 Then
                                sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                                & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO) as DENOMINAZIONE " _
                                     & " from SISCOM_MI.IMPIANTI " _
                                     & " where SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & vIdEdificio _
                                     & " and   SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & cmbTipologia.SelectedValue & "')"

                            Else
                                sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                                & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO) as DENOMINAZIONE " _
                                     & " from SISCOM_MI.IMPIANTI " _
                                     & " where SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & vIdComplesso _
                                     & " and   SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & cmbTipologia.SelectedValue & "')"
                            End If
                        End If

                End Select

                PAR.cmd.Parameters.Clear()
                PAR.cmd.CommandText = sStr1

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                While myReader1.Read
                    Me.cmbDettaglio.Items.Add(New ListItem(PAR.IfNull(myReader1("DENOMINAZIONE"), " "), PAR.IfNull(myReader1("ID"), -1)))
                End While
                myReader1.Close()
            End If

            Me.cmbDettaglio.SelectedValue = "-1"

            If FlagConnessione = True Then
                PAR.cmd.Dispose()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If



        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"


            If vIdManutenzione <> -1 Then
                PAR.myTrans.Rollback()

                'PAR.myTrans = par.OracleConn.BeginTransaction()
                '''par.cmd.Transaction = par.myTrans
                'HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, PAR.myTrans)

                'CType(Me.Page, Object).ChiudiTutto()
            End If

            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & IdConnessione)

            Page.Dispose()


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

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

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function


    Sub CalcolaImporti(ByVal importo, ByVal perc_oneri, ByVal perc_sconto, ByVal perc_iva, ByVal fl_rit_legge)

        Dim oneri, asta, iva, ritenuta, risultato1, risultato2, risultato3, risultato4, ritenutaIVATA, risultato5 As Decimal

        'A) Importo
        'V) perc_oneri
        'Y) perc_sconto
        'Z) perc_iva

        'B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
        oneri = PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriP_MANO"), HiddenField).Value, 0)

        If oneri = -1 Then
            'B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
        oneri = importo - ((importo * 100) / (100 + perc_oneri))
        End If
        oneri = Round(oneri, 2)

        'C) A-B LORDO senza ONERI= IMPORTO-oneri
        risultato1 = importo - oneri

        'D) RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
        asta = (risultato1 * perc_sconto) / 100
        asta = Round(asta, 2)

        'E) C-D NETTO senza ONERI= (LORDO senza oneri-asta)
        risultato2 = risultato1 - asta

        'AGGIUNTO
        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato3 = risultato2 + oneri

        'F) ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
        If fl_rit_legge = 1 Then
            ritenuta = (risultato3 * 0.5) / 100 '(risultato2 * 0.5) / 100
            ritenuta = Round(ritenuta, 2)
            ritenutaIVATA = Round((ritenuta + ((ritenuta * perc_iva) / 100)), 2)
            ritenutaIVATA = Round(ritenutaIVATA, 2)
        Else
            ritenuta = 0
            ritenutaIVATA = 0
        End If

        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato5 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri


        'H) (G*Z)/100 IVA= (NETTO con oneri*perc_iva)/100
        If perc_iva < 0 Then
            iva = 0
        Else
            iva = Math.Round((risultato5 * perc_iva) / 100, 2)
        End If
        iva = Round(iva, 2)

        'I) NETTO+ONERI+IVA
        risultato4 = risultato5 + iva

        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneri"), TextBox).Text = IsNumFormat(oneri, "", "##,##0.00")
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriImporto"), TextBox).Text = IsNumFormat(risultato1, "", "##,##0.00")
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtRibassoAsta"), TextBox).Text = IsNumFormat(asta, "", "##,##0.00")
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtNetto"), TextBox).Text = IsNumFormat(risultato2, "", "##,##0.00")

        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtRitenuta"), TextBox).Text = IsNumFormat(ritenutaIVATA, "", "##,##0.00") '6 campo

        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtNettoOneri"), TextBox).Text = IsNumFormat(risultato3, "", "##,##0.00")
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtIVA"), TextBox).Text = IsNumFormat(iva, "", "##,##0.00")
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtNettoOneriIVA"), TextBox).Text = IsNumFormat(risultato4, "", "##,##0.00")
        risultato4 = risultato4 + ritenutaIVATA
        'risultato4 = Math.Round(risultato4, 2) + Math.Round(ritenutaIVATA, 2) 'risultato4 + ritenutaIVATA
        CType(Me.Page.FindControl("txtNettoOneriIVA_TMP"), HiddenField).Value = IsNumFormat(risultato4, "", "##,##0.00")

    End Sub


    Sub CalcolaImportiC(ByVal importo As Decimal, ByVal rimborso As Decimal, ByVal perc_oneri As Decimal, ByVal perc_sconto As Decimal, ByVal perc_iva As Decimal, ByVal fl_rit_legge As Integer)

        Dim oneri, asta, iva, ritenuta, risultato1, risultato2, risultato3, risultato4, ritenutaIVATA, risultato5 As Decimal

        'A) Importo
        'V) perc_oneri
        'Y) perc_sconto
        'Z) perc_iva

        'B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
        oneri = PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriC_MANO"), HiddenField).Value, 0)

        If oneri = -1 Then
            oneri = PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriP_MANO"), HiddenField).Value, 0)
            CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriC_MANO"), HiddenField).Value = oneri
            If oneri = 0 Then
                oneri = -1
            End If

            If importo = 0 Then
                oneri = -1
            End If
        End If


        If oneri = -1 Then
            'B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
        oneri = importo - ((importo * 100) / (100 + perc_oneri))
        End If
        oneri = Round(oneri, 2)

        'C) A-B LORDO senza ONERI= IMPORTO-oneri
        risultato1 = importo - oneri

        'D) RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
        asta = (risultato1 * perc_sconto) / 100
        asta = Round(asta, 2)

        'E) C-D NETTO senza ONERI= (LORDO senza oneri-asta)
        risultato2 = risultato1 - asta

        'AGGIUNTO
        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato3 = risultato2 + oneri

        'F) ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
        If fl_rit_legge = 1 Then
            ritenuta = (risultato3 * 0.5) / 100 '(risultato2 * 0.5) / 100
            ritenuta = Round(ritenuta, 2)
            ritenutaIVATA = Round((ritenuta + ((ritenuta * perc_iva) / 100)), 2)
            ritenutaIVATA = Round(ritenutaIVATA, 2)
        Else
            ritenuta = 0
            ritenutaIVATA = 0
        End If

        'If penale > risultato2 Then
        '    Response.Write("<SCRIPT>alert('Attenzione...La penale è stata azzerata perchè non deve superare l\'importo netto escluso oneri!');</SCRIPT>")
        '    penale = 0
        '    CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtPenale"), TextBox).Text = IsNumFormat(penale, "", "##,##0.00")
        'End If
        '*******

        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato5 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri


        'H) (G*Z)/100 IVA= (NETTO con oneri*perc_iva)/100
        If perc_iva < 0 Then
            iva = 0
        Else
            iva = Math.Round((risultato5 * perc_iva) / 100, 2)
        End If
        iva = Round(iva, 2)

        'I) NETTO+ONERI+IVA
        risultato4 = risultato5 + iva + Round(rimborso, 2)

        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriC"), TextBox).Text = IsNumFormat(oneri, "", "##,##0.00")
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriImportoC"), TextBox).Text = IsNumFormat(risultato1, "", "##,##0.00")
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtRibassoAstaC"), TextBox).Text = IsNumFormat(asta, "", "##,##0.00")
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtNettoC"), TextBox).Text = IsNumFormat(risultato2, "", "##,##0.00")

        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtRitenutaC"), TextBox).Text = IsNumFormat(ritenutaIVATA, "", "##,##0.00") '6 campo

        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtNettoOneriC"), TextBox).Text = IsNumFormat(risultato3, "", "##,##0.00")
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtIVAC"), TextBox).Text = IsNumFormat(iva, "", "##,##0.00")
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtNettoOneriIVAC"), TextBox).Text = IsNumFormat(risultato4, "", "##,##0.00")

        risultato4 = risultato4 + ritenutaIVATA
        'risultato4 = Math.Round(risultato4, 2) + Math.Round(ritenutaIVATA, 2) 'risultato4 + ritenutaIVATA
        CType(Me.Page.FindControl("txtNettoOneriIVAC_TMP"), HiddenField).Value = IsNumFormat(risultato4, "", "##,##0.00")

    End Sub


    Function CalcolaImportiControllo(ByVal importo As Decimal, ByVal perc_oneri As Decimal, ByVal perc_sconto As Decimal, ByVal perc_iva As Decimal, ByVal fl_rit_legge As Integer) As Decimal

        Dim oneri, asta, iva, ritenuta, risultato1, risultato2, risultato3, ritenutaIVATA As Decimal

        'A) Importo
        'V) perc_oneri
        'Y) perc_sconto
        'Z) perc_iva


        'B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
        oneri = PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriP_MANO"), HiddenField).Value, 0)

        If oneri = -1 Then
            'B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
        oneri = importo - ((importo * 100) / (100 + perc_oneri))
        End If
        oneri = Round(oneri, 2)

        'C) A-B LORDO senza ONERI= IMPORTO-oneri
        risultato1 = importo - oneri

        'D) RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
        asta = (risultato1 * perc_sconto) / 100
        asta = Round(asta, 2)

        'E) C-D NETTO senza ONERI= (LORDO senza oneri-asta)
        risultato2 = risultato1 - asta

        'AGGIUNTO
        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato3 = risultato2 + oneri

        'F) ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
        If fl_rit_legge = 1 Then
            ritenuta = (risultato3 * 0.5) / 100 '(risultato2 * 0.5) / 100
            ritenuta = Round(ritenuta, 2)
            ritenutaIVATA = Round((ritenuta + ((ritenuta * perc_iva) / 100)), 2)
            ritenutaIVATA = Round(ritenutaIVATA, 2)
        Else
            ritenuta = 0
            ritenutaIVATA = 0
        End If

        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato3 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri

        'H) (G*Z)/100 IVA= (NETTO con oneri*perc_iva)/100
        If perc_iva < 0 Then
            iva = 0
        Else
            iva = Math.Round((risultato3 * perc_iva) / 100, 2)
        End If
        iva = Round(iva, 2)

        'I) NETTO+ONERI+IVA
        CalcolaImportiControllo = risultato3 + iva + ritenutaIVATA


    End Function


    'Protected Sub btnConsuntivo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConsuntivo.Click
    '    txtSel1.Text = ""
    '    txtIdComponente.Text = ""

    '    BindGrid_Consuntivi()
    'End Sub


    Private Sub BindGrid_Consuntivi()

        BindGrid_Interventi()

        '*** SOMMA IMPORTO
        Dim SommaTotC As Decimal = 0
        Dim SommaTotRimborsi As Decimal = 0

        Dim i As Integer

        For i = 0 To Me.DataGrid1.Items.Count - 1
            If Me.DataGrid1.Items(i).Cells(PAR.IndrDGC(DataGrid1, "IMPORTO_CONSUNTIVO")).Text = "&nbsp;" Then Me.DataGrid1.Items(i).Cells(PAR.IndrDGC(DataGrid1, "IMPORTO_CONSUNTIVO")).Text = ""
            If Me.DataGrid1.Items(i).Cells(PAR.IndrDGC(DataGrid1, "IMPORTO_RIMBORSO")).Text = "&nbsp;" Then Me.DataGrid1.Items(i).Cells(PAR.IndrDGC(DataGrid1, "IMPORTO_RIMBORSO")).Text = ""
            If Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "perc_iva_rimborso")).Text = "&nbsp;" Then Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "perc_iva_rimborso")).Text = ""

            SommaTotC = SommaTotC + PAR.IfEmpty(Me.DataGrid1.Items(i).Cells(PAR.IndrDGC(DataGrid1, "IMPORTO_CONSUNTIVO")).Text, 0)
            SommaTotRimborsi = SommaTotRimborsi + (PAR.IfEmpty(Me.DataGrid1.Items(i).Cells(PAR.IndrDGC(DataGrid1, "IMPORTO_RIMBORSO")).Text, 0) + (PAR.IfEmpty(Me.DataGrid1.Items(i).Cells(PAR.IndrDGC(DataGrid1, "IMPORTO_RIMBORSO")).Text, 0) * PAR.IfEmpty(Me.DataGrid1.Items(i).Cells(PAR.IndrDGC(DataGrid1, "perc_iva_rimborso")).Text, 0) / 100))
        Next i

        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoC"), TextBox).Text = IsNumFormat(SommaTotC, "", "##,##0.00")
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoControlloC"), HiddenField).Value = IsNumFormat(SommaTotC + SommaTotRimborsi, "", "##,##0.00")

        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtRimborsi"), TextBox).Text = IsNumFormat(SommaTotRimborsi, "", "##,##0.00")

        CalcolaImportiC(SommaTotC, PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtRimborsi"), TextBox).Text, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_C"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))

        'Me.txtImportoTotale.Text = Format(SommaTot, "##,##0.00")
        '**********************

        CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"
        AbilitaDisabilita()

    End Sub

    Private Sub AbilitaDisabilita()

        Select Case CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value
            Case 0  'BOZZA
                ' Posso inserire MANUTENZIONI_INTERVENTI + Descrizione
                ' Posso eliminare la manutenzione, se Integrativa, allora pulisco il campo MANUTENZIONI.ID_FIGLIO

                If Val(PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoControllo"), HiddenField).Value, 0)) > 0 Then
                    CType(Me.Page.FindControl("RBL1"), RadioButtonList).Enabled = False
                    CType(Me.Page.FindControl("cmbIndirizzo"), RadComboBox).Enabled = False
                    CType(Me.Page.FindControl("cmbScala"), RadComboBox).Enabled = False
                    ' CType(Me.Page.FindControl("cmbServizio"), RadComboBox).Enabled = False
                Else

                    CType(Me.Page.FindControl("RBL1"), RadioButtonList).Enabled = False
                    CType(Me.Page.FindControl("cmbIndirizzo"), RadComboBox).Enabled = False

                    If CType(Me.Page.FindControl("RBL1"), RadioButtonList).Items(1).Selected = True Then CType(Me.Page.FindControl("cmbScala"), RadComboBox).Enabled = True

                    CType(Me.Page.FindControl("cmbVocePF"), RadComboBox).Enabled = True

                End If

                If CType(Me.Page.FindControl("cmbVocePF"), RadComboBox).SelectedValue <> "-1" Then
                    'Me.btnAgg1.Visible = True
                    Me.btnElimina1.Visible = True
                    CType(Me.Page.FindControl("HiddenMostraPulsantiDett"), HiddenField).value = 1
                    'Me.btnApri1.Visible = True
                Else
                    'Me.btnAgg1.Visible = False
                    Me.btnElimina1.Visible = False
                    CType(Me.Page.FindControl("HiddenMostraPulsantiDett"), HiddenField).Value = 0
                    'Me.btnApri1.Visible = False
                End If

                'Me.btnConsuntivo.Visible = False
                'Me.btnVisualConsuntivo.Visible = False

                CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtPenale"), TextBox).ReadOnly = True

                If vIdManutenzione = "-1" Then
                    'PRIMO INSERIMENTO
                    CType(Me.Page.FindControl("btnElimina"), RadButton).Visible = False
                    CType(Me.Page.FindControl("txtVisualizza"), HiddenField).Value = 0 'NO ALLEGATI
                Else
                    CType(Me.Page.FindControl("btnElimina"), RadButton).Visible = True
                    CType(Me.Page.FindControl("txtVisualizza"), HiddenField).Value = 1 'possibilità di inserire ALLEGATO
                End If
                If Session.Item("FL_COMI") = 1 Or Session.Item("FL_ANNULLA_ODL") = "1" Then
                    CType(Me.Page.FindControl("btnAnnulla"), RadButton).Enabled = True
                Else
                    CType(Me.Page.FindControl("btnAnnulla"), RadButton).Enabled = False
                End If

                CType(Me.Page.FindControl("btnOrdineIntegrativo"), RadButton).Visible = False
                Me.txtDescrizione.ReadOnly = False

                If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then FrmSolaLettura()

            Case 1  'EMESSO ORDINE
                ' Disabilito l'inserimento di MANUTENZIONI_INTERVENTI
                ' abilito il bottone "Consuntivo"
                ' Posso inserire MANUTENZIONI_CONSUNTIVI anche a MANUTENZIONI_INTERVENTI.FL_BLOCCATO=1 (provenienti da integrativi)
                ' scalo la somma MANUTENZIONI_INTERVENTI da APPALTI_LOTTI_SERVIZIO.RESIDUO_CONSUMO solo di MANUTENZIONI_INTERVENTI.FL_BLOCCATO=0
                ' abilito il bottone "Ordine Integrativo"
                ' disabilto il bottone "Elimina" e abilito "Annulla Ordine" 
                ' NOTA: 
                '       su "Annulla Ordine" rimetto la somma MANUTENZIONI_INTERVENTI in APPALTI_LOTTI_SERVIZIO.RESIDUO_CONSUMO 
                '                           solo di MANUTENZIONI_INTERVENTI.FL_BLOCCATO=0 e blocca la MANUNETZIONE con MANUTENZIONE.FL_ORDINE_BLOCCATO=1

                If Val(PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoControllo"), HiddenField).Value, 0)) > 0 Then
                    CType(Me.Page.FindControl("RBL1"), RadioButtonList).Enabled = False
                    CType(Me.Page.FindControl("cmbIndirizzo"), RadComboBox).Enabled = False
                    CType(Me.Page.FindControl("cmbScala"), RadComboBox).Enabled = False
                    '    CType(Me.Page.FindControl("cmbServizio"), RadComboBox).Enabled = False

                    'Me.btnAgg1.Visible = False
                    Me.btnElimina1.Visible = False
                    CType(Me.Page.FindControl("HiddenMostraPulsantiDett"), HiddenField).value = 0
                    'Me.btnApri1.Visible = False

                    If Val(PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoControlloC"), HiddenField).Value, 0)) > 0 Then
                        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_P"), DropDownList).Enabled = False
                        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneri"), TextBox).ReadOnly = True
                    Else
                        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_P"), DropDownList).Enabled = True
                        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneri"), TextBox).ReadOnly = False
                    End If

                    'Me.btnConsuntivo.Visible = True
                    'Me.btnVisualConsuntivo.Visible = False

                    CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtPenale"), TextBox).ReadOnly = False
                    CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_C"), DropDownList).Enabled = True

                Else
                    ' se ho cancellato tutti gli INTERVENTI inseriti, posso cambiare l'indirizzo o servizio (NO perchè viene scelto prima)
                    CType(Me.Page.FindControl("RBL1"), RadioButtonList).Enabled = False
                    CType(Me.Page.FindControl("cmbIndirizzo"), RadComboBox).Enabled = False

                    If CType(Me.Page.FindControl("RBL1"), RadioButtonList).Items(1).Selected = True Then CType(Me.Page.FindControl("cmbScala"), RadComboBox).Enabled = True

                    CType(Me.Page.FindControl("cmbVocePF"), RadComboBox).Enabled = True


                    If CType(Me.Page.FindControl("cmbVocePF"), RadComboBox).SelectedValue <> "-1" Then
                        'Me.btnAgg1.Visible = True
                        Me.btnElimina1.Visible = True
                        CType(Me.Page.FindControl("HiddenMostraPulsantiDett"), HiddenField).value = 1
                        'Me.btnApri1.Visible = True

                        'Me.btnConsuntivo.Visible = False
                        'Me.btnVisualConsuntivo.Visible = False

                        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtPenale"), TextBox).ReadOnly = True

                        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_P"), DropDownList).Enabled = True
                        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_C"), DropDownList).Enabled = False
                    End If
                End If

                CType(Me.Page.FindControl("btnElimina"), RadButton).Enabled = False
                CType(Me.Page.FindControl("btnAnnulla"), RadButton).Enabled = True
                CType(Me.Page.FindControl("btnOrdineIntegrativo"), RadButton).Visible = True
                CType(Me.Page.FindControl("txtVisualizza"), HiddenField).Value = 1 'possibilità di inserire ALLEGATO


                Me.txtDescrizione.ReadOnly = True

                If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then FrmSolaLettura()
                Me.btnElimina1.Visible = True
                CType(Me.Page.FindControl("HiddenMostraPulsantiDett"), HiddenField).Value = 1
                ' CType(Me.Page.FindControl("cmbServizioVoce"), RadComboBox).Enabled = True

            Case 2  'CONSUNTIVO (FINE)
                'Disabilito tutti i campi
                'rendo invisibile il bottone "Salva", "Elimina", "Ordine Integrativo"

                If PAR.IfEmpty(CType(Me.Page.FindControl("txtID_Pagamento"), HiddenField).Value, -1) > 0 Then
                    'NOTA: se ho emessso il SAL allora non posso modificare l'IVA

                    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                    FrmSolaLettura()

                    CType(Me.Page.FindControl("txtVisualizza"), HiddenField).Value = 2  ' solo consultazione allegati
                    CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value = 2       ' solo RI stampa PAGAMENTO

                    'Me.btnConsuntivo.Visible = False
                    'Me.btnVisualConsuntivo.Visible = True

                    'CType(Me.Page.FindControl("btnSalva"), ImageButton).Visible = False
                    CType(Me.Page.FindControl("btnElimina"), RadButton).Enabled = False
                    If Session.Item("FL_COMI") = 1 Or Session.Item("FL_ANNULLA_ODL") = "1" Then
                        CType(Me.Page.FindControl("btnAnnulla"), RadButton).Enabled = True
                    Else
                        CType(Me.Page.FindControl("btnAnnulla"), RadButton).Enabled = False
                    End If

                    CType(Me.Page.FindControl("btnOrdineIntegrativo"), RadButton).Visible = False

                    Dim CTRL As Control = Nothing
                    For Each CTRL In Me.Page.FindControl("form1").Controls
                        If TypeOf CTRL Is TextBox Then
                            DirectCast(CTRL, TextBox).ReadOnly = True
                        ElseIf TypeOf CTRL Is DropDownList Then
                            DirectCast(CTRL, DropDownList).Enabled = False
                        ElseIf TypeOf CTRL Is RadComboBox Then
                            DirectCast(CTRL, RadComboBox).Enabled = False
                        ElseIf TypeOf CTRL Is RadioButtonList Then
                            DirectCast(CTRL, RadioButtonList).Enabled = False
                        End If
                    Next
                    Me.txtDescrizione.ReadOnly = True

                    CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_P"), DropDownList).Enabled = False
                    CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_C"), DropDownList).Enabled = False
                    CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriC"), TextBox).ReadOnly = True
                    CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneri"), TextBox).ReadOnly = True

                Else

                    'Me.btnAgg1.Visible = False
                    'Me.btnElimina1.Visible = False
                    CType(Me.Page.FindControl("HiddenMostraPulsantiDett"), HiddenField).Value = 1
                    Me.btnElimina1.Visible = True
                    'Me.btnApri1.Visible = False

                    CType(Me.Page.FindControl("txtVisualizza"), HiddenField).Value = 2  ' solo consultazione allegati
                    CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value = 2       ' solo RI stampa PAGAMENTO

                    'Me.btnConsuntivo.Visible = True
                    'Me.btnVisualConsuntivo.Visible = False


                    CType(Me.Page.FindControl("btnElimina"), RadButton).Enabled = False
                    CType(Me.Page.FindControl("btnAnnulla"), RadButton).Enabled = True

                    Me.txtDescrizione.ReadOnly = True

                    CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_P"), DropDownList).Enabled = False
                    CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_C"), DropDownList).Enabled = True
                    CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriC"), TextBox).ReadOnly = False
                    CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneri"), TextBox).ReadOnly = True
                End If

            Case 3 'INTEGRATO

                'se ha FIGLI INTEGRATIVI, la scheda viene BLOCCATO, solo gli allegati si possono consultare e RI Stampare l'ordine
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()

                CType(Me.Page.FindControl("BLOCCATO"), HiddenField).Value = "2"      ' BLOCCATO COME INTEGRATIVO
                CType(Me.Page.FindControl("txtVisualizza"), HiddenField).Value = 2   ' solo consultazione allegati
                CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value = 3        ' solo RI stampa ORDINE


                'Me.btnConsuntivo.Visible = False
                'Me.btnVisualConsuntivo.Visible = True

                Me.txtDescrizione.ReadOnly = True

                CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_P"), DropDownList).Enabled = False
                CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_C"), DropDownList).Enabled = False

            Case 4 'EMESSO PAGAMENTO
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()

                CType(Me.Page.FindControl("txtVisualizza"), HiddenField).Value = 2   ' solo consultazione allegati
                CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value = 4        ' solo RI stampa ORDINE
                Me.txtDescrizione.ReadOnly = True

                CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_P"), DropDownList).Enabled = False
                CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_C"), DropDownList).Enabled = False
                CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriC"), TextBox).ReadOnly = True
                CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneri"), TextBox).ReadOnly = True


            Case 5 ' ANNULLATO
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()

                'Me.btnAgg1.Visible = False
                Me.btnElimina1.Visible = False
                CType(Me.Page.FindControl("HiddenMostraPulsantiDett"), HiddenField).value = 0
                'Me.btnApri1.Visible = False

                'Me.btnConsuntivo.Visible = False
                'Me.btnVisualConsuntivo.Visible = True

                CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtPenale"), TextBox).ReadOnly = False

                Me.txtDescrizione.ReadOnly = True

                CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_P"), DropDownList).Enabled = False
                CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_C"), DropDownList).Enabled = False

                CType(Me.Page.FindControl("txtVisualizza"), HiddenField).Value = 2   ' solo consultazione allegati
                CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value = 5        ' NO stampa 


        End Select

        If vIdSegnalazione > 0 Then
            CType(Me.Page.FindControl("RBL1"), RadioButtonList).Enabled = False
            CType(Me.Page.FindControl("cmbIndirizzo"), RadComboBox).Enabled = False
            CType(Me.Page.FindControl("cmbScala"), RadComboBox).Enabled = False
        End If

        Dim stato As String = CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value
        For i = 0 To Me.DataGrid1.Items.Count - 1
            If stato = "0" Then
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxIvaRimborso"), DropDownList).Enabled = False
            ElseIf stato = "1" Then
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxIvaRimborso"), DropDownList).Enabled = True
            ElseIf stato = "2" Then
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = True
                If CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Text = "" Then
                    CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Text = "0"
                End If
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxIvaRimborso"), DropDownList).Enabled = True
            Else
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxIvaRimborso"), DropDownList).Enabled = False
            End If
            If CType(Me.Page.FindControl("cmbIndirizzo"), RadComboBox).SelectedValue = "-1" Then
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue = "-1"
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = False
            End If
        Next

        '*** 19/10/2017 ***
        'IVA BLOCCATA PER RICHIESTA SD 1855/2017
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_P"), DropDownList).Enabled = False
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("cmbIVA_C"), DropDownList).Enabled = False
        '*** 19/10/2017 ***

    End Sub


    Private Function Ricalcola_ImportoResiduo() As Decimal
        Dim FlagConnessione As Boolean
        Dim sStr1 As String

        Dim ris1, ris2 As Decimal

        Try

            FlagConnessione = False
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                PAR.SettaCommand(PAR)

                FlagConnessione = True
            End If

            Ricalcola_ImportoResiduo = 0

            'CALCOLO l'IMPORTO RESIDUO dato da:

            '1) la somma di eventuali variazioni all'importo residuo di APPALTI_VARIAZIONI_IMPORTI

            sStr1 = "select SUM(IMPORTO) " _
               & " from   SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI " _
               & " where  ID_PF_VOCE=" & CType(Me.Page.FindControl("cmbVocePF"), RadComboBox).SelectedValue _
                 & " and  ID_VARIAZIONE in (select ID from SISCOM_MI.APPALTI_VARIAZIONI " _
                                       & " where ID_APPALTO=" & CType(Me.Page.FindControl("txtIdAppalto"), HiddenField).Value & ")"

            PAR.cmd.Parameters.Clear()
            PAR.cmd.CommandText = sStr1
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

            If myReader1.Read Then
                Ricalcola_ImportoResiduo = PAR.IfNull(myReader1(0), 0)
            End If
            myReader1.Close()


            '2)la somma dell'importo calcolato (IMPORTO-CONSUMO+IVA) 

            sStr1 = "select * from SISCOM_MI.APPALTI_VOCI_PF " _
                 & " where ID_APPALTO=" & CType(Me.Page.FindControl("txtIdAppalto"), HiddenField).Value _
                 & "   and ID_PF_VOCE in (select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=(select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID=" & CType(Me.Page.FindControl("cmbVocePF"), RadComboBox).SelectedValue & ")) "

            '& "   and ID_PF_VOCE=" & Me.cmbVocePF.SelectedValue 'Prima cercavo solo la VOCE


            PAR.cmd.Parameters.Clear()
            PAR.cmd.CommandText = sStr1
            myReader1 = PAR.cmd.ExecuteReader()

            While myReader1.Read
                'IMPORTO a CONSUMO senza IVA=IMPORTO_CONSUMO-(IMPORTO_COSUMO*SCONTO_CONSUMO/100)
                'ris1 = par.IfNull(myReader1("IMPORTO_CONSUMO"), 0) - par.IfNull(myReader1("SCONTO_CONSUMO"), 0)
                ris1 = PAR.IfNull(myReader1("IMPORTO_CONSUMO"), 0) * PAR.IfNull(myReader1("SCONTO_CONSUMO"), 0) / 100
                ris1 = PAR.IfNull(myReader1("IMPORTO_CONSUMO"), 0) - ris1

                ris1 = ris1 + PAR.IfNull(myReader1("ONERI_SICUREZZA_CONSUMO"), 0) 'par.IfEmpty(Me.txtOneriSicurezza.Value, 0)

                If PAR.IfNull(myReader1("IVA_CONSUMO"), 0) > 0 Then
                    ris2 = ris1 * PAR.IfNull(myReader1("IVA_CONSUMO"), 0) / 100
                Else
                    ris2 = 0
                End If
                Ricalcola_ImportoResiduo = Ricalcola_ImportoResiduo + ris1 + ris2
            End While
            myReader1.Close()


            '3)la SommaResiduo va sottratto alla somma dell'IMPORTO PRENOTATO o CONSUNTIVATO o EMESSO PAGAMENTO (SAL) da MANUTENZIONI 
            sStr1 = "select SUM(IMPORTO_TOT) from SISCOM_MI.MANUTENZIONI " _
                 & " where ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & CType(Me.Page.FindControl("txtIdAppalto"), HiddenField).Value & ")) " _
                 & "   and ID_PF_VOCE_IMPORTO is null " _
                 & "   and ID_PF_VOCE in (select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=(select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID=" & CType(Me.Page.FindControl("cmbVocePF"), RadComboBox).SelectedValue & ")) " _
                 & "   and STATO in (1,2,4)"

            'TUTTO CIO' SPESO tranne quelle speso dalla manutenzioni in modifica
            If vIdManutenzione > 0 Then
                sStr1 = sStr1 & " and ID<>" & vIdManutenzione
            End If

            PAR.cmd.Parameters.Clear()
            PAR.cmd.CommandText = sStr1
            myReader1 = PAR.cmd.ExecuteReader()

            If myReader1.Read Then
                Ricalcola_ImportoResiduo = Ricalcola_ImportoResiduo - PAR.IfNull(myReader1(0), 0) '+ par.IfEmpty(Me.txtOneriSicurezza.Value, 0)
            End If
            myReader1.Close()


            If FlagConnessione = True Then
                PAR.cmd.Dispose()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            Ricalcola_ImportoResiduo = 0

            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & IdConnessione)
            Page.Dispose()



            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Function


    Protected Sub DropDownListTipologiaOggetto_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Dim FlagConnessione As Boolean
        Dim sStr1 As String
        Try
            FlagConnessione = False
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                PAR.SettaCommand(PAR)

                FlagConnessione = True
            End If

            For Each elemento As GridDataItem In DataGrid1.Items
                'If elemento.Cells(PAR.IndrDGC(DataGrid1, "ID")).Text.ToString.Replace("&nbsp;", "") = "" Then
                If elemento.Cells(PAR.IndRDGC(DataGrid1, "ID")).Text.ToString = txtIdComponente.Text.ToString Then
                    Dim tipologia As String = CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue
                    CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Items.Clear()
                    Select Case tipologia
                        Case "SCALA"
                            If vIdEdificio <> -1 Then
                                sStr1 = "select ID,(select denominazione from siscom_mi.edifici where edifici.id=scale_edifici.id_Edificio)||'- Scala '||DESCRIZIONE as denominazione from SISCOM_MI.scale_edifici where ID_EDIFICIO=" & vIdEdificio & " order by denominazione asc"
                            ElseIf vIdComplesso <> -1 Then
                                sStr1 = "select ID,(select denominazione from siscom_mi.edifici where edifici.id=scale_edifici.id_Edificio)||'- Scala '||DESCRIZIONE as denominazione from SISCOM_MI.scale_edifici where ID_edificio IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO=" & vIdComplesso & ") order by denominazione asc"
                            End If
                        Case "COMPLESSO"
                            If vIdComplesso <> -1 Then
                                sStr1 = "select ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=" & vIdComplesso & " order by DENOMINAZIONE asc"
                            ElseIf vIdEdificio <> -1 Then
                                sStr1 = "select ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID in (select id_complesso from siscom_mi.edifici where id=" & vIdEdificio & ") order by DENOMINAZIONE asc"
                            End If
                        Case "EDIFICIO"
                            If vIdEdificio <> -1 Then
                                sStr1 = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID=" & vIdEdificio & " order by DENOMINAZIONE asc"
                            Else
                                sStr1 = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID_COMPLESSO=" & vIdComplesso & " order by DENOMINAZIONE asc"
                            End If
                        Case "UNITA IMMOBILIARE"
                            If vIdEdificio <> -1 Then
                                'ok

                                'ALTER TABLE UNITA_IMMOBILIARI ADD (
                                '                            FOREIGN(KEY(COD_TIPO_LIVELLO_PIANO))
                                ' REFERENCES TIPO_LIVELLO_PIANO (COD));
                                'e la scala non da SCALA ma tramite ID_SCALA che fa riferimento a SCALE_EDIFICI

                                '- -Cod.'||" "SISCOM_MI.EDIFICI.COD_EDIFICIO||' TOLTO 09/02/2011

                                sStr1 = "select SISCOM_MI.UNITA_IMMOBILIARI.ID," _
                                        & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||" _
                                         & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE||' - -Piano '||" _
                                         & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE||' - -Interno '||" _
                                         & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) as DENOMINAZIONE  " _
                                    & " from SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INTESTATARI_UI" _
                                    & " where SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=" & vIdEdificio

                            Else
                                'ok
                                sStr1 = "select SISCOM_MI.UNITA_IMMOBILIARI.ID," _
                                        & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||" _
                                         & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE||' - -Piano '||" _
                                         & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE||' - -Interno '||" _
                                         & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) as DENOMINAZIONE " _
                                    & " from SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INTESTATARI_UI" _
                                    & " where SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO in ( select ID from SISCOM_MI.EDIFICI " _
                                             & " where SISCOM_MI.EDIFICI.ID_COMPLESSO=" & vIdComplesso & ")"

                            End If

                            If vIdScala <> -1 Then
                                sStr1 = sStr1 & " and SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=" & vIdScala
                            End If

                            sStr1 = sStr1 & " and SISCOM_MI.EDIFICI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO  (+) " _
                                          & " and SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+) " _
                                          & " and SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO=SISCOM_MI.TIPO_LIVELLO_PIANO.COD (+) " _
                                          & " and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) " _
                            & " order by DENOMINAZIONE,SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE,SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE asc"
                        Case "UNITA COMUNE"
                            If vIdEdificio <> -1 Then
                                'ok
                                sStr1 = "select SISCOM_MI.UNITA_COMUNI.ID," _
                                             & "(SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) as DENOMINAZIONE " _
                                     & " from SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE " _
                                     & " where SISCOM_MI.UNITA_COMUNI.ID_EDIFICIO=" & vIdEdificio _
                                     & " and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
                                     & " order by SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE,SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE asc "
                            Else
                                'ok
                                sStr1 = "select SISCOM_MI.UNITA_COMUNI.ID," _
                                             & "(SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) as DENOMINAZIONE " _
                                     & " from  SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE " _
                                     & " where SISCOM_MI.UNITA_COMUNI.ID_COMPLESSO=" & vIdComplesso _
                                     & " and   SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
                                     & " order by SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE,SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE asc "
                            End If
                        Case Else
                            'ok
                            If tipologia = "SOLLEVAMENTO" Then
                                If vIdEdificio <> -1 Then
                                    sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                                    & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO||' - - Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO||' - - Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA) as DENOMINAZIONE " _
                                         & " from SISCOM_MI.IMPIANTI,SISCOM_MI.I_SOLLEVAMENTO " _
                                         & " where SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & vIdEdificio _
                                         & "   and SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "') " _
                                         & "   and SISCOM_MI.IMPIANTI.ID=SISCOM_MI.I_SOLLEVAMENTO.ID (+) "
                                Else
                                    sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                                    & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO||' - - Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO||' - - Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA) as DENOMINAZIONE " _
                                         & " from SISCOM_MI.IMPIANTI,SISCOM_MI.I_SOLLEVAMENTO " _
                                         & " where SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & vIdComplesso _
                                         & "   and SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "') " _
                                         & "   and SISCOM_MI.IMPIANTI.ID=SISCOM_MI.I_SOLLEVAMENTO.ID (+) "


                                End If
                            Else
                                If vIdEdificio <> -1 Then
                                    sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                                    & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO) as DENOMINAZIONE " _
                                         & " from SISCOM_MI.IMPIANTI " _
                                         & " where SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & vIdEdificio _
                                         & " and   SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "')"

                                Else
                                    sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                                    & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO) as DENOMINAZIONE " _
                                         & " from SISCOM_MI.IMPIANTI " _
                                         & " where SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & vIdComplesso _
                                         & " and   SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "')"
                                End If
                            End If
                    End Select
                    PAR.caricaComboBox(sStr1, CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList), "ID", "DENOMINAZIONE", True, "NULL", "- - -")
                    'Try
                    '    Select Case CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedItem.Text
                    '        Case "COMPLESSO"
                    '            CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = elemento.Cells(PAR.IndrDGC(DataGrid1, "ID_COMPLESSO")).Text
                    '        Case "EDIFICIO"
                    '            CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = elemento.Cells(PAR.IndrDGC(DataGrid1, "ID_EDIFICIO")).Text
                    '        Case "UNITA IMMOBILIARE"
                    '            CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = elemento.Cells(PAR.IndrDGC(DataGrid1, "ID_UNITA_IMMOBILIARE")).Text
                    '        Case "UNITA COMUNE "
                    '            CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = elemento.Cells(PAR.IndrDGC(DataGrid1, "ID_UNITA_COMUNE")).Text
                    '        Case Else
                    '            CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = elemento.Cells(PAR.IndrDGC(DataGrid1, "ID_IMPIANTO")).Text
                    '    End Select
                    'Catch ex As Exception
                    'End Try
                End If
            Next

            Me.cmbDettaglio.SelectedValue = "-1"

            If FlagConnessione = True Then
                PAR.cmd.Dispose()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"
            If vIdManutenzione <> -1 Then
                PAR.myTrans.Rollback()

            End If

            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & IdConnessione)

            Page.Dispose()


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


    End Sub

    Protected Sub ImageButtonAggiorna_Click(sender As Object, e As System.EventArgs) Handles ImageButtonAggiorna.Click
        Try
            PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            PAR.SettaCommand(PAR)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            End If
            Dim dt As New Data.DataTable
            dt.Columns.Add("ID")
            dt.Columns.Add("TIPOLOGIA")
            dt.Columns.Add("DETTAGLIO")
            dt.Columns.Add("IMPORTO_PRESUNTO")
            dt.Columns.Add("IMPORTO_CONSUNTIVO")
            dt.Columns.Add("IMPORTO_RIMBORSO")
            dt.Columns.Add("ID_IMPIANTO")
            dt.Columns.Add("ID_COMPLESSO")
            dt.Columns.Add("ID_EDIFICIO")
            dt.Columns.Add("ID_SCALA")
            dt.Columns.Add("ID_UNITA_IMMOBILIARE")
            dt.Columns.Add("ID_UNITA_COMUNE")
            dt.Columns.Add("FL_BLOCCATO")
			dt.Columns.Add("PERC_IVA_RIMBORSO")
            Dim riga As Data.DataRow
            For Each elemento As GridDataItem In DataGrid1.Items
                riga = dt.NewRow
                riga.Item("ID") = elemento.Cells(PAR.IndrDGC(DataGrid1, "ID")).Text
                riga.Item("TIPOLOGIA") = CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue
                riga.Item("DETTAGLIO") = CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue
                riga.Item("IMPORTO_PRESUNTO") = CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text
                riga.Item("IMPORTO_CONSUNTIVO") = CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Text
                riga.Item("IMPORTO_RIMBORSO") = CType(elemento.FindControl("TextBoxImportoRimborso"), TextBox).Text
                riga.Item("ID_IMPIANTO") = elemento.Cells(PAR.IndrDGC(DataGrid1, "ID_IMPIANTO")).Text
                riga.Item("ID_COMPLESSO") = elemento.Cells(PAR.IndrDGC(DataGrid1, "ID_COMPLESSO")).Text
                riga.Item("ID_EDIFICIO") = elemento.Cells(PAR.IndrDGC(DataGrid1, "ID_EDIFICIO")).Text
                riga.Item("ID_SCALA") = elemento.Cells(PAR.IndrDGC(DataGrid1, "ID_SCALA")).Text
                riga.Item("ID_UNITA_IMMOBILIARE") = elemento.Cells(PAR.IndrDGC(DataGrid1, "ID_UNITA_IMMOBILIARE")).Text
                riga.Item("ID_UNITA_COMUNE") = elemento.Cells(PAR.IndrDGC(DataGrid1, "ID_UNITA_COMUNE")).Text
                riga.Item("FL_BLOCCATO") = elemento.Cells(PAR.IndrDGC(DataGrid1, "FL_BLOCCATO")).Text
		riga.Item("PERC_IVA_RIMBORSO") = CType(elemento.FindControl("TextBoxIvaRimborso"), DropDownList).SelectedValue
                dt.Rows.Add(riga)
            Next
            riga = dt.NewRow
            riga.Item("ID") = "-" & CDec(dt.Rows.Count + 1)
            riga.Item("TIPOLOGIA") = ""
            riga.Item("DETTAGLIO") = ""
            riga.Item("IMPORTO_PRESUNTO") = "0"
            riga.Item("IMPORTO_CONSUNTIVO") = ""
            riga.Item("IMPORTO_RIMBORSO") = ""
            riga.Item("ID_IMPIANTO") = ""
            riga.Item("ID_COMPLESSO") = ""
            riga.Item("ID_EDIFICIO") = ""
            riga.Item("ID_SCALA") = ""
            riga.Item("ID_UNITA_IMMOBILIARE") = ""
            riga.Item("ID_UNITA_COMUNE") = ""
            riga.Item("FL_BLOCCATO") = ""
			riga.Item("PERC_IVA_RIMBORSO") = ""
            dt.Rows.Add(riga)
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()
            For Each elemento As GridDataItem In DataGrid1.Items
                CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue = elemento.Cells(PAR.IndRDGC(DataGrid1, "TIPOLOGIA")).Text
                CType(elemento.FindControl("TextBoxIvaRimborso"), DropDownList).SelectedValue = elemento.Cells(PAR.IndRDGC(DataGrid1, "PERC_IVA_RIMBORSO")).Text
                Dim tipologia As String = CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue
                CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Items.Clear()
	'CType(elemento.FindControl("TextBoxIvaRimborso"), DropDownList).Items.Clear()
                Dim query As String = ""
                Select Case tipologia
                    Case "SCALA"
                        If vIdEdificio <> -1 Then
                            query = "select ID,(select denominazione from siscom_mi.edifici where edifici.id=scale_edifici.id_Edificio)||'- Scala '||DESCRIZIONE as denominazione from SISCOM_MI.scale_edifici where ID_EDIFICIO=" & vIdEdificio & " order by denominazione asc"
                        ElseIf vIdComplesso <> -1 Then
                            query = "select ID,(select denominazione from siscom_mi.edifici where edifici.id=scale_edifici.id_Edificio)||'- Scala '||DESCRIZIONE as denominazione from SISCOM_MI.scale_edifici where ID_edificio IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO=" & vIdComplesso & ") order by denominazione asc"
                        End If
                    Case "COMPLESSO"
                        If vIdComplesso <> -1 Then
                            query = "select ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=" & vIdComplesso & " order by DENOMINAZIONE asc"
                        ElseIf vIdEdificio <> -1 Then
                            query = "select ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID in (select id_complesso from siscom_mi.edifici where id=" & vIdEdificio & ") order by DENOMINAZIONE asc"
                        End If
                    Case "EDIFICIO"
                        If vIdEdificio <> -1 Then
                            query = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID=" & vIdEdificio & " order by DENOMINAZIONE asc"
                        Else
                            query = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID_COMPLESSO=" & vIdComplesso & " order by DENOMINAZIONE asc"
                        End If
                    Case "UNITA IMMOBILIARE"
                        If vIdEdificio <> -1 Then
                            query = "select SISCOM_MI.UNITA_IMMOBILIARI.ID," _
                                    & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||" _
                                     & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE||' - -Piano '||" _
                                     & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE||' - -Interno '||" _
                                     & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) as DENOMINAZIONE  " _
                                & " from SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INTESTATARI_UI" _
                                & " where SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=" & vIdEdificio
                        Else
                            query = "select SISCOM_MI.UNITA_IMMOBILIARI.ID," _
                                    & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||" _
                                     & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE||' - -Piano '||" _
                                     & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE||' - -Interno '||" _
                                     & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) as DENOMINAZIONE " _
                                & " from SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INTESTATARI_UI" _
                                & " where SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO in ( select ID from SISCOM_MI.EDIFICI " _
                                         & " where SISCOM_MI.EDIFICI.ID_COMPLESSO=" & vIdComplesso & ")"
                        End If
                        If vIdScala <> -1 Then
                            query = query & " and SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=" & vIdScala
                        End If
                        query = query & " and SISCOM_MI.EDIFICI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO  (+) " _
                                      & " and SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+) " _
                                      & " and SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO=SISCOM_MI.TIPO_LIVELLO_PIANO.COD (+) " _
                                      & " and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) " _
                        & " order by DENOMINAZIONE,SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE,SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE asc"
                    Case "UNITA COMUNE"
                        If vIdEdificio <> -1 Then
                            query = "select SISCOM_MI.UNITA_COMUNI.ID," _
                                         & "(SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) as DENOMINAZIONE " _
                                 & " from SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE " _
                                 & " where SISCOM_MI.UNITA_COMUNI.ID_EDIFICIO=" & vIdEdificio _
                                 & " and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
                                 & " order by SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE,SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE asc "
                        Else
                            query = "select SISCOM_MI.UNITA_COMUNI.ID," _
                                         & "(SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) as DENOMINAZIONE " _
                                 & " from  SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE " _
                                 & " where SISCOM_MI.UNITA_COMUNI.ID_COMPLESSO=" & vIdComplesso _
                                 & " and   SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
                                 & " order by SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE,SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE asc "
                        End If
                    Case Else
                        If tipologia = "SOLLEVAMENTO" Then
                            If vIdEdificio <> -1 Then
                                query = "select  SISCOM_MI.IMPIANTI.ID," _
                                                & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO||' - - Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO||' - - Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA) as DENOMINAZIONE " _
                                     & " from SISCOM_MI.IMPIANTI,SISCOM_MI.I_SOLLEVAMENTO " _
                                     & " where SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & vIdEdificio _
                                     & "   and SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "') " _
                                     & "   and SISCOM_MI.IMPIANTI.ID=SISCOM_MI.I_SOLLEVAMENTO.ID (+) "
                            Else
                                query = "select  SISCOM_MI.IMPIANTI.ID," _
                                                & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO||' - - Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO||' - - Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA) as DENOMINAZIONE " _
                                     & " from SISCOM_MI.IMPIANTI,SISCOM_MI.I_SOLLEVAMENTO " _
                                     & " where SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & vIdComplesso _
                                     & "   and SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "') " _
                                     & "   and SISCOM_MI.IMPIANTI.ID=SISCOM_MI.I_SOLLEVAMENTO.ID (+) "
                            End If
                        Else
                            If vIdEdificio <> -1 Then
                                query = "select  SISCOM_MI.IMPIANTI.ID," _
                                                & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO) as DENOMINAZIONE " _
                                     & " from SISCOM_MI.IMPIANTI " _
                                     & " where SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & vIdEdificio _
                                     & " and   SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "')"
                            Else
                                query = "select  SISCOM_MI.IMPIANTI.ID," _
                                                & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO) as DENOMINAZIONE " _
                                     & " from SISCOM_MI.IMPIANTI " _
                                     & " where SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & vIdComplesso _
                                     & " and   SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "')"
                            End If
                        End If
                End Select
                PAR.caricaComboBox(query, CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList), "ID", "DENOMINAZIONE", True, "NULL", "- - -")
                CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = elemento.Cells(PAR.IndRDGC(DataGrid1, "DETTAGLIO")).Text
                CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
                CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
                CType(elemento.FindControl("TextBoxImportoRimborso"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                CType(elemento.FindControl("TextBoxImportoRimborso"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
                If CType(Page.FindControl("cmbIndirizzo"), RadComboBox).SelectedValue = "-1" Then
                    CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue = "-1"
                    CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = False
                    CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = False
                End If
            Next
            AbilitaDisabilita()
        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            If vIdManutenzione <> -1 Then
                PAR.myTrans.Rollback()
            End If
            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            HttpContext.Current.Session.Remove("CONNESSIONE" & IdConnessione)
            Page.Dispose()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        'Try
        '    ' RIPRENDO LA CONNESSIONE
        '    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        '    PAR.SettaCommand(PAR)
        '    'RIPRENDO LA TRANSAZIONE
        '    If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
        '        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        '    End If
        '    Dim idC As Long = -1
        '    Dim idE As Long = -1
        '    Dim idUM As Long = -1
        '    Dim idUC As Long = -1
        '    Dim idI As Long = -1
        '    Select Case Me.cmbTipologia.SelectedValue
        '        Case "COMPLESSO"
        '            idC = cmbDettaglio.SelectedValue
        '        Case "EDIFICIO"
        '            idE = cmbDettaglio.SelectedValue
        '        Case "UNITA IMMOBILIARE"
        '            idUM = cmbDettaglio.SelectedValue
        '        Case "UNITA COMUNE"
        '            idUC = cmbDettaglio.SelectedValue
        '        Case Else
        '            idI = cmbDettaglio.SelectedValue
        '    End Select
        '    Select Case CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value
        '        Case "0"
        '            'bozza
        '            Dim TIPOLOGIA As String = ""
        '            Dim IMPORTO_PRESUNTO As String = ""
        '            Dim ID_UNITA_IMMOBILIARE As String = "NULL"
        '            Dim ID_UNITA_COMUNE As String = "NULL"
        '            Dim ID_MANUTENZIONE As String = ""
        '            Dim ID_IMPIANTO As String = "NULL"
        '            Dim ID_EDIFICIO As String = "NULL"
        '            Dim ID_COMPLESSO As String = "NULL"
        '            Dim ID As String = ""
        '            Dim FL_BLOCCATO As String = "0"
        '            For Each elementO As DataGridItem In DataGrid1.Items
        '                TIPOLOGIA = CType(elementO.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue.ToString
        '                IMPORTO_PRESUNTO = CDec(CType(elementO.FindControl("TextBoxImportoPresunto"), TextBox).Text.ToString)
        '                Select Case TIPOLOGIA
        '                    Case "COMPLESSO"
        '                        ID_COMPLESSO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
        '                    Case "EDIFICIO"
        '                        ID_EDIFICIO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
        '                    Case "UNITA IMMOBILIARE"
        '                        ID_UNITA_IMMOBILIARE = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
        '                    Case "UNITA COMUNE"
        '                        ID_UNITA_COMUNE = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
        '                    Case "CENTRALE TERMICA"
        '                        ID_IMPIANTO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
        '                    Case "SOLLEVAMENTO"
        '                        ID_IMPIANTO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
        '                    Case "TELERISCALDAMENTO"
        '                        ID_IMPIANTO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
        '                End Select
        '                ID_MANUTENZIONE = vIdManutenzione
        '                ID = PAR.IfNull(elementO.Cells(PAR.IndrDGC(DataGrid1, "ID")).Text.Replace("&nbsp;", ""), "0")
        '                FL_BLOCCATO = "0"
        '                If ID = "0" Or ID = "" Then
        '                    'inserimento
        '                    PAR.cmd.CommandText = " INSERT INTO SISCOM_MI.MANUTENZIONI_INTERVENTI ( " _
        '                        & " TIPOLOGIA, IMPORTO_PRESUNTO, ID_UNITA_IMMOBILIARE,  " _
        '                        & " ID_UNITA_COMUNE, ID_MANUTENZIONE, ID_IMPIANTO,  " _
        '                        & " ID_EDIFICIO, ID_COMPLESSO, ID,  " _
        '                        & " FL_BLOCCATO)  " _
        '                        & " VALUES ('" & TIPOLOGIA & "', " _
        '                        & " " & PAR.PuntiInVirgole(IMPORTO_PRESUNTO) & ", " _
        '                        & ID_UNITA_IMMOBILIARE & ", " _
        '                        & ID_UNITA_COMUNE & ", " _
        '                        & ID_MANUTENZIONE & ", " _
        '                        & ID_IMPIANTO & ", " _
        '                        & ID_EDIFICIO & ", " _
        '                        & ID_COMPLESSO & ", " _
        '                        & "SISCOM_MI.SEQ_MANUTENZIONI_INTERVENTI.NEXTVAL, " _
        '                        & "0)"
        '                    PAR.cmd.ExecuteNonQuery()
        '                Else
        '                    'update

        '                End If
        '            Next
        '    End Select
        'Catch ex As Exception
        '    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        '    Session.Item("LAVORAZIONE") = "0"
        '    If vIdManutenzione <> -1 Then
        '        PAR.myTrans.Rollback()
        '    End If
        '    PAR.OracleConn.Close()
        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '    HttpContext.Current.Session.Remove("CONNESSIONE" & IdConnessione)
        '    Page.Dispose()
        '    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
        '    Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        'End Try
    End Sub

    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
            Dim StringaSql As String

        If vIdManutenzione = -1 Then
            ricaricaValori()
        End If

        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        PAR.SettaCommand(PAR)

        If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
            PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
        End If

        Dim sSQL_DettaglioIMPIANTO As String
        sSQL_DettaglioIMPIANTO = "(CASE IMPIANTI.COD_TIPOLOGIA " _
                                    & " WHEN 'AN' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'CF' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'CI' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'EL' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'GA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'ID' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'ME' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'SO' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||" _
                                            & "(select  (CASE when SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO is null THEN " _
                                                            & " 'Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA " _
                                                    & " ELSE 'Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO END) " _
                                            & " from SISCOM_MI.I_SOLLEVAMENTO where ID=IMPIANTI.ID) " _
                                    & " WHEN 'TA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TE' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TR' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TU' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TV' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                & " ELSE '' " _
                                & " END) as DETTAGLIO "

        Dim aggiunta As String = ""

        Dim stato As String = CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value
        '*********** modifica marco 23/07/2015 ************
        'If stato = "0" Then
        '    aggiunta = " union select null as ID,null as TIPOLOGIA,null as ID_IMPIANTO,null as ID_COMPLESSO,null as ID_EDIFICIO,null as ID_UNITA_IMMOBILIARE,null as ID_UNITA_COMUNE," _
        '           & "      null as DETTAGLIO,'' AS IMPORTO_PRESUNTO," _
        '           & "      null as IMPORTO_CONSUNTIVO," _
        '           & "      null as IMPORTO_RIMBORSO," _
        '           & "      null as FL_BLOCCATO " _
        '           & " from dual  "
        'Else
        '    aggiunta = ""
        'End If
        '***************************************************


        StringaSql = " select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & sSQL_DettaglioIMPIANTO & ",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
				  & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
					& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
					& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
				   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.IMPIANTI" _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO is not null and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO=SISCOM_MI.IMPIANTI.ID (+) " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "       COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
				   & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
			& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
			& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
				   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='COMPLESSO' and  SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "        (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
				   & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
			& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
			& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
				   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='EDIFICIO' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "       (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||(select descrizione from siscom_mi.scale_edifici where siscom_mi.scale_edifici.id=unita_immobiliari.id_scala)||' - -Interno '||SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
				  & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
				   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INTESTATARI_UI " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA IMMOBILIARE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID (+) and	SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID  (+)  and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "      (SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
				   & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE  " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA COMUNE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE=SISCOM_MI.UNITA_COMUNI.ID (+) and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "      (SISCOM_MI.edifici.denominazione||' - '||siscom_mi.scale_edifici.descrizione) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
				   & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
				   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.SCALE_EDIFICI  " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='SCALA' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+) and SISCOM_MI.SCALE_EDIFICI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID " _
        & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,'' AS TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "      '' AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
				   & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
				   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
             & " from SISCOM_MI.MANUTENZIONI_INTERVENTI   " _
                  & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione & " and (SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA is null or tipologia='-1') "


        PAR.cmd.Parameters.Clear()
        PAR.cmd.CommandText = StringaSql
        Dim dat As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataTable 'Data.DataSet()

        da.Fill(ds)


        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        da.Dispose()
        ds.Dispose()

        '*** SOMMA IMPORTO
        Dim SommaTot As Decimal = 0
        Dim i As Integer



        Dim sStr1 As String = ""
        For Each elemento As DataGridItem In DataGrid1.Items
            CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue = elemento.Cells(PAR.IndRDGC(DataGrid1, "TIPOLOGIA")).Text.ToString.Replace("&nbsp;", "")
            CType(elemento.FindControl("TextBoxIvaRimborso"), DropDownList).SelectedValue = CInt(PAR.IfEmpty(elemento.Cells(PAR.IndRDGC(DataGrid1, "PERC_IVA_RIMBORSO")).Text.Replace("&nbsp;", ""), "0"))
            Dim tipologia As String = CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue
            CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Items.Clear()
            Select Case tipologia
                Case "SCALA"
                    If vIdEdificio <> -1 Then
                        sStr1 = "select ID,(select denominazione from siscom_mi.edifici where edifici.id=scale_edifici.id_Edificio)||'- Scala '||DESCRIZIONE as denominazione from SISCOM_MI.scale_edifici where ID_EDIFICIO=" & vIdEdificio & " order by denominazione asc"
                    ElseIf vIdComplesso <> -1 Then
                        sStr1 = "select ID,(select denominazione from siscom_mi.edifici where edifici.id=scale_edifici.id_Edificio)||'- Scala '||DESCRIZIONE as denominazione from SISCOM_MI.scale_edifici where ID_edificio IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO=" & vIdComplesso & ") order by denominazione asc"
                    End If
                Case "COMPLESSO"
                    If vIdComplesso <> -1 Then
                    sStr1 = "select ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=" & vIdComplesso & " order by DENOMINAZIONE asc"
                    ElseIf vIdEdificio <> -1 Then
                        sStr1 = "select ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID in (select id_complesso from siscom_mi.edifici where id=" & vIdEdificio & ") order by DENOMINAZIONE asc"
                    End If
                Case "EDIFICIO"
                    If vIdEdificio <> -1 Then
                        sStr1 = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID=" & vIdEdificio & " order by DENOMINAZIONE asc"
                    Else
                        If vIdComplesso <> -1 Then
                        sStr1 = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID_COMPLESSO=" & vIdComplesso & " order by DENOMINAZIONE asc"
                        Else
                            Dim idApp As String = CType(Me.Page.FindControl("txtIdAppalto"), HiddenField).Value
                            If IsNumeric(idApp) Then
                                sStr1 = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI,siscom_mi.appalti_lotti_patrimonio where appalti_lotti_patrimonio.id_edificio=edifici.id and id_appalto=" & idApp & " order by DENOMINAZIONE asc"
                            Else
                                sStr1 = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI order by DENOMINAZIONE asc"
                    End If
                        End If
                    End If
                Case "UNITA IMMOBILIARE"
                    If vIdEdificio <> -1 Then
                        sStr1 = "select SISCOM_MI.UNITA_IMMOBILIARI.ID," _
                                & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||" _
                                 & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE||' - -Piano '||" _
                                 & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE||' - -Interno '||" _
                                 & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) as DENOMINAZIONE  " _
                            & " from SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INTESTATARI_UI" _
                            & " where SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=" & vIdEdificio

                    Else
                        'ok
                        sStr1 = "select SISCOM_MI.UNITA_IMMOBILIARI.ID," _
                                & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||" _
                                 & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE||' - -Piano '||" _
                                 & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE||' - -Interno '||" _
                                 & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) as DENOMINAZIONE " _
                            & " from SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INTESTATARI_UI" _
                            & " where SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO in ( select ID from SISCOM_MI.EDIFICI " _
                                     & " where SISCOM_MI.EDIFICI.ID_COMPLESSO=" & vIdComplesso & ")"

                    End If

                    If vIdScala <> -1 Then
                        sStr1 = sStr1 & " and SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=" & vIdScala
                    End If

                    sStr1 = sStr1 & " and SISCOM_MI.EDIFICI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO  (+) " _
                                  & " and SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+) " _
                                  & " and SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO=SISCOM_MI.TIPO_LIVELLO_PIANO.COD (+) " _
                                  & " and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) " _
                    & " order by DENOMINAZIONE,SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE,SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE asc"
                Case "UNITA COMUNE"
                    If vIdEdificio <> -1 Then
                        'ok
                        sStr1 = "select SISCOM_MI.UNITA_COMUNI.ID," _
                                     & "(SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) as DENOMINAZIONE " _
                             & " from SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE " _
                             & " where SISCOM_MI.UNITA_COMUNI.ID_EDIFICIO=" & vIdEdificio _
                             & " and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
                             & " order by SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE,SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE asc "
                    Else
                        'ok
                        sStr1 = "select SISCOM_MI.UNITA_COMUNI.ID," _
                                     & "(SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) as DENOMINAZIONE " _
                             & " from  SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE " _
                             & " where SISCOM_MI.UNITA_COMUNI.ID_COMPLESSO=" & vIdComplesso _
                             & " and   SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
                             & " order by SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE,SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE asc "
                    End If
                Case Else
                    'ok
                    If tipologia = "SOLLEVAMENTO" Then
                        If vIdEdificio <> -1 Then
                            sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                            & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO||' - - Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO||' - - Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA) as DENOMINAZIONE " _
                                 & " from SISCOM_MI.IMPIANTI,SISCOM_MI.I_SOLLEVAMENTO " _
                                 & " where SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & vIdEdificio _
                                 & "   and SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "') " _
                                 & "   and SISCOM_MI.IMPIANTI.ID=SISCOM_MI.I_SOLLEVAMENTO.ID (+) "
                        Else
                            sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                            & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO||' - - Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO||' - - Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA) as DENOMINAZIONE " _
                                 & " from SISCOM_MI.IMPIANTI,SISCOM_MI.I_SOLLEVAMENTO " _
                                 & " where SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & vIdComplesso _
                                 & "   and SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "') " _
                                 & "   and SISCOM_MI.IMPIANTI.ID=SISCOM_MI.I_SOLLEVAMENTO.ID (+) "


                        End If
                    Else
                        If vIdEdificio <> -1 Then
                            sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                            & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO) as DENOMINAZIONE " _
                                 & " from SISCOM_MI.IMPIANTI " _
                                 & " where SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & vIdEdificio _
                                 & " and   SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "')"

                        Else
                            sStr1 = "select  SISCOM_MI.IMPIANTI.ID," _
                                            & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO) as DENOMINAZIONE " _
                                 & " from SISCOM_MI.IMPIANTI " _
                                 & " where SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & vIdComplesso _
                                 & " and   SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "')"
                        End If
                    End If
            End Select
            PAR.caricaComboBox(sStr1, CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList), "ID", "DENOMINAZIONE", True, "NULL", "- - -")

        Next

        For i = 0 To Me.DataGrid1.Items.Count - 1
            If Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text = "&nbsp;" Then Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text = ""
            SommaTot = SommaTot + PAR.IfEmpty(Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text, 0)
            CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "TIPOLOGIA")).Text

            Select Case DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "TIPOLOGIA")).Text
                Case "COMPLESSO"
                    CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "ID_COMPLESSO")).Text
                Case "EDIFICIO"
                    CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "ID_EDIFICIO")).Text
                Case "UNITA IMMOBILIARE"
                    CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "ID_UNITA_IMMOBILIARE")).Text
                Case "UNITA COMUNE"
                    CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "ID_UNITA_COMUNE")).Text
                Case "SCALA"
                    CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "ID_SCALA")).Text
                Case Else
                    CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "ID_IMPIANTO")).Text
            End Select

            If stato = "0" Then
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Enabled = False
            ElseIf stato = "1" Or stato = "2" Then
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Enabled = True
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Enabled = True
            Else
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Enabled = False
            End If
            CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).ToolTip = CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedItem.Text
            CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).ToolTip = CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedItem.Text

            If CType(Me.Page.FindControl("cmbIndirizzo"), DropDownList).SelectedValue = "-1" Then
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue = "-1"
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = False
                CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = False
            End If

            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
			'CType(Me.DataGrid1.Items(i).FindControl("TextBoxIvaRimborso"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        Next i


        If Me.DataGrid1.Items.Count = 1 Then
            'Me.txtSel1.Text = PAR.IfEmpty(Me.DataGrid1.Items(0).Cells(PAR.IndDGC(DataGrid1, "TIPOLOGIA")).Text, "")
            Me.txtIdComponente.Text = PAR.IfEmpty(Me.DataGrid1.Items(0).Cells(PAR.IndRDGC(DataGrid1, "ID")).Text, 0)
            Me.txt_FL_BLOCCATO.Value = PAR.IfEmpty(Me.DataGrid1.Items(0).Cells(PAR.IndRDGC(DataGrid1, "FL_BLOCCATO")).Text, 0)
        End If

        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImporto"), TextBox).Text = IsNumFormat(SommaTot, "", "##,##0.00")
        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoControllo"), HiddenField).Value = IsNumFormat(SommaTot, "", "##,##0.00")

        CalcolaImporti(SommaTot, PAR.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))

        'Me.txtImportoTotale.Text = Format(SommaTot, "##,##0.00")
        '**********************
        SettaMisure()

        AbilitaDisabilita()
    End Sub

  

    

    Private Sub EliminaDettaglio()
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        Try
            Dim SommaTot As Decimal = 0
            Dim i As Integer

            If txtIdComponente.Text = "" Then
                RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare1.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else
                If vIdManutenzione = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                    'lstInterventi.RemoveAt(txtIdComponente.Text)

                    'Dim indice As Integer = 0
                    'For Each griglia As Epifani.Manutenzioni_Interventi In lstInterventi
                    '    griglia.ID = indice
                    '    indice += 1
                    'Next

                    Dim dt As New Data.DataTable
                    dt.Columns.Add("ID")
                    dt.Columns.Add("TIPOLOGIA")
                    dt.Columns.Add("DETTAGLIO")
                    dt.Columns.Add("IMPORTO_PRESUNTO")
                    dt.Columns.Add("IMPORTO_CONSUNTIVO")
                    dt.Columns.Add("IMPORTO_RIMBORSO")
                    dt.Columns.Add("FL_BLOCCATO")
                    dt.Columns.Add("ID_COMPLESSO")
                    dt.Columns.Add("ID_EDIFICIO")
                    dt.Columns.Add("ID_UNITA_IMMOBILIARE")
                    dt.Columns.Add("ID_UNITA_COMUNE")
                    dt.Columns.Add("ID_IMPIANTO")
                    dt.Columns.Add("ID_SCALA")
                    Dim riga As Data.DataRow
                    Dim maggioreZero As Boolean = False
                    For Each elemento As GridDataItem In DataGrid1.Items
                        If elemento.Cells(PAR.IndRDGC(DataGrid1, "id")).Text <> txtIdComponente.Text Then
                            riga = dt.NewRow
                            riga.Item("ID") = elemento.Cells(PAR.IndRDGC(DataGrid1, "ID")).Text
                            riga.Item("TIPOLOGIA") = CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue
                            riga.Item("DETTAGLIO") = CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue
                            riga.Item("IMPORTO_PRESUNTO") = CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text
                            riga.Item("IMPORTO_CONSUNTIVO") = CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Text
                            riga.Item("IMPORTO_RIMBORSO") = CType(elemento.FindControl("TextBoxImportoRimborso"), TextBox).Text
                            riga.Item("FL_BLOCCATO") = elemento.Cells(PAR.IndRDGC(DataGrid1, "FL_BLOCCATO")).Text
                            riga.Item("ID_COMPLESSO") = ""
                            riga.Item("ID_EDIFICIO") = ""
                            riga.Item("ID_UNITA_IMMOBILIARE") = ""
                            riga.Item("ID_UNITA_COMUNE") = ""
                            riga.Item("ID_IMPIANTO") = ""
                            riga.Item("ID_SCALA") = ""
                            dt.Rows.Add(riga)
                        Else
                            If IsNumeric(CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text) AndAlso CDec(CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text) > 0 Then
                                riga = dt.NewRow
                                riga.Item("ID") = elemento.Cells(PAR.IndRDGC(DataGrid1, "ID")).Text
                                riga.Item("TIPOLOGIA") = CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue
                                riga.Item("DETTAGLIO") = CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue
                                riga.Item("IMPORTO_PRESUNTO") = CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text
                                riga.Item("IMPORTO_CONSUNTIVO") = CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Text
                                riga.Item("IMPORTO_RIMBORSO") = CType(elemento.FindControl("TextBoxImportoRimborso"), TextBox).Text
                                riga.Item("FL_BLOCCATO") = elemento.Cells(PAR.IndRDGC(DataGrid1, "FL_BLOCCATO")).Text
                                riga.Item("ID_COMPLESSO") = ""
                                riga.Item("ID_EDIFICIO") = ""
                                riga.Item("ID_UNITA_IMMOBILIARE") = ""
                                riga.Item("ID_UNITA_COMUNE") = ""
                                riga.Item("ID_IMPIANTO") = ""
                                riga.Item("ID_SCALA") = ""
                                dt.Rows.Add(riga)
                                maggioreZero = True
                            End If
                        End If
                    Next
                    DataGrid1.DataSource = dt
                    DataGrid1.DataBind()
                    For Each elemento As GridDataItem In DataGrid1.Items
                        CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue = elemento.Cells(PAR.IndRDGC(DataGrid1, "TIPOLOGIA")).Text
                        Dim tipologia As String = CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue
                        CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Items.Clear()
                        Dim query As String = ""
                        Select Case tipologia
                            Case "SCALA"
                                If vIdEdificio <> -1 Then
                                    query = "select ID,(select denominazione from siscom_mi.edifici where edifici.id=scale_edifici.id_Edificio)||'- Scala '||DESCRIZIONE as denominazione from SISCOM_MI.scale_edifici where ID_EDIFICIO=" & vIdEdificio & " order by denominazione asc"
                                ElseIf vIdComplesso <> -1 Then
                                    query = "select ID,(select denominazione from siscom_mi.edifici where edifici.id=scale_edifici.id_Edificio)||'- Scala '||DESCRIZIONE as denominazione from SISCOM_MI.scale_edifici where ID_edificio IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO=" & vIdComplesso & ") order by denominazione asc"
                                End If
                            Case "COMPLESSO"
                                If vIdComplesso <> -1 Then
                                    query = "select ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=" & vIdComplesso & " order by DENOMINAZIONE asc"
                                ElseIf vIdEdificio <> -1 Then
                                    query = "select ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID in (select id_complesso from siscom_mi.edifici where id=" & vIdEdificio & ") order by DENOMINAZIONE asc"
                                End If
                            Case "EDIFICIO"
                                If vIdEdificio <> -1 Then
                                    query = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID=" & vIdEdificio & " order by DENOMINAZIONE asc"
                                Else
                                    query = "select ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID_COMPLESSO=" & vIdComplesso & " order by DENOMINAZIONE asc"
                                End If
                            Case "UNITA IMMOBILIARE"
                                If vIdEdificio <> -1 Then
                                    query = "select SISCOM_MI.UNITA_IMMOBILIARI.ID," _
                                            & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||" _
                                             & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE||' - -Piano '||" _
                                             & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE||' - -Interno '||" _
                                             & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) as DENOMINAZIONE  " _
                                        & " from SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INTESTATARI_UI" _
                                        & " where SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=" & vIdEdificio
                                Else
                                    query = "select SISCOM_MI.UNITA_IMMOBILIARI.ID," _
                                            & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||" _
                                             & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE||' - -Piano '||" _
                                             & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE||' - -Interno '||" _
                                             & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) as DENOMINAZIONE " _
                                        & " from SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INTESTATARI_UI" _
                                        & " where SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO in ( select ID from SISCOM_MI.EDIFICI " _
                                                 & " where SISCOM_MI.EDIFICI.ID_COMPLESSO=" & vIdComplesso & ")"
                                End If
                                If vIdScala <> -1 Then
                                    query = query & " and SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=" & vIdScala
                                End If
                                query = query & " and SISCOM_MI.EDIFICI.ID=SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO  (+) " _
                                              & " and SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+) " _
                                              & " and SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO=SISCOM_MI.TIPO_LIVELLO_PIANO.COD (+) " _
                                              & " and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) " _
                                & " order by DENOMINAZIONE,SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE,SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE asc"
                            Case "UNITA COMUNE"
                                If vIdEdificio <> -1 Then
                                    query = "select SISCOM_MI.UNITA_COMUNI.ID," _
                                                 & "(SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) as DENOMINAZIONE " _
                                         & " from SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE " _
                                         & " where SISCOM_MI.UNITA_COMUNI.ID_EDIFICIO=" & vIdEdificio _
                                         & " and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
                                         & " order by SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE,SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE asc "
                                Else
                                    query = "select SISCOM_MI.UNITA_COMUNI.ID," _
                                                 & "(SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) as DENOMINAZIONE " _
                                         & " from  SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE " _
                                         & " where SISCOM_MI.UNITA_COMUNI.ID_COMPLESSO=" & vIdComplesso _
                                         & " and   SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
                                         & " order by SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE,SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE asc "
                                End If
                            Case Else
                                If tipologia = "SOLLEVAMENTO" Then
                                    If vIdEdificio <> -1 Then
                                        query = "select  SISCOM_MI.IMPIANTI.ID," _
                                                        & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO||' - - Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO||' - - Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA) as DENOMINAZIONE " _
                                             & " from SISCOM_MI.IMPIANTI,SISCOM_MI.I_SOLLEVAMENTO " _
                                             & " where SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & vIdEdificio _
                                             & "   and SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "') " _
                                             & "   and SISCOM_MI.IMPIANTI.ID=SISCOM_MI.I_SOLLEVAMENTO.ID (+) "
                                    Else
                                        query = "select  SISCOM_MI.IMPIANTI.ID," _
                                                        & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO||' - - Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO||' - - Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA) as DENOMINAZIONE " _
                                             & " from SISCOM_MI.IMPIANTI,SISCOM_MI.I_SOLLEVAMENTO " _
                                             & " where SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & vIdComplesso _
                                             & "   and SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "') " _
                                             & "   and SISCOM_MI.IMPIANTI.ID=SISCOM_MI.I_SOLLEVAMENTO.ID (+) "
                                    End If
                                Else
                                    If vIdEdificio <> -1 Then
                                        query = "select  SISCOM_MI.IMPIANTI.ID," _
                                                        & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO) as DENOMINAZIONE " _
                                             & " from SISCOM_MI.IMPIANTI " _
                                             & " where SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & vIdEdificio _
                                             & " and   SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "')"
                                    Else
                                        query = "select  SISCOM_MI.IMPIANTI.ID," _
                                                        & "(SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO) as DENOMINAZIONE " _
                                             & " from SISCOM_MI.IMPIANTI " _
                                             & " where SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & vIdComplesso _
                                             & " and   SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=(select COD from SISCOM_MI.TIPOLOGIA_IMPIANTI where DESCRIZIONE='" & tipologia & "')"
                                    End If
                                End If
                        End Select
                        PAR.caricaComboBox(query, CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList), "ID", "DENOMINAZIONE", True, "NULL", "- - -")
                        CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = elemento.Cells(PAR.IndRDGC(DataGrid1, "DETTAGLIO")).Text
                        CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                        CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
                        CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                        CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
                        CType(elemento.FindControl("TextBoxImportoRimborso"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                        CType(elemento.FindControl("TextBoxImportoRimborso"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
                    Next
                    AbilitaDisabilita()
                    Dim stato As String = CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value

                    SommaTot = 0
                    For i = 0 To Me.DataGrid1.Items.Count - 1
                        If Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text = "&nbsp;" Then Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text = ""
                        SommaTot = SommaTot + PAR.IfEmpty(Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text, 0)
                        CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "TIPOLOGIA")).Text
                        CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue = DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "DETTAGLIO")).Text
                        If stato = "0" Then
                            CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = True
                            CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = True
                            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Enabled = True
                            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Enabled = False
                            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Enabled = False
                        ElseIf stato = "1" Or stato = "2" Then
                            CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = False
                            CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = False
                            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Enabled = False
                            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Enabled = True
                            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Enabled = True
                        Else
                            CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).Enabled = False
                            CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).Enabled = False
                            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Enabled = False
                            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Enabled = False
                            CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Enabled = False
                        End If
                        CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).ToolTip = CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedItem.Text
                        CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).ToolTip = CType(Me.DataGrid1.Items(i).FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedItem.Text

                        CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                        CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoPresunto"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
                        CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                        CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoConsuntivo"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
                        CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                        CType(Me.DataGrid1.Items(i).FindControl("TextBoxImportoRimborso"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")
                    Next i


                    If Me.DataGrid1.Items.Count = 1 Then
                        'Me.txtSel1.Text = PAR.IfEmpty(Me.DataGrid1.Items(0).Cells(PAR.IndrDGC(DataGrid1, "TIPOLOGIA")).Text, "")
                        Me.txtIdComponente.Text = PAR.IfEmpty(Me.DataGrid1.Items(0).Cells(PAR.IndRDGC(DataGrid1, "ID")).Text, 0)
                        Me.txt_FL_BLOCCATO.Value = PAR.IfEmpty(Me.DataGrid1.Items(0).Cells(PAR.IndRDGC(DataGrid1, "FL_BLOCCATO")).Text, 0)
                    End If

                    CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImporto"), TextBox).Text = IsNumFormat(SommaTot, "", "##,##0.00")
                    CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoControllo"), HiddenField).Value = IsNumFormat(SommaTot, "", "##,##0.00")

                    CalcolaImporti(SommaTot, PAR.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))

                    'Me.txtImportoTotale.Text = Format(SommaTot, "##,##0.00")
                    '**********************

                    AbilitaDisabilita()

                    If maggioreZero = True Then
                        RadWindowManager1.RadAlert("Attenzione...Impossibile eliminare intervento con importo presunto diverso da 0!", 300, 150, "Attenzione", "", "null")
                    End If

                Else
                    If Me.txt_FL_BLOCCATO.Value = "1" Then
                        RadWindowManager1.RadAlert("Attenzione...Non è possibile eliminare la voce perchè proveniente da un ordine emesso integrativo!", 300, 150, "Attenzione", "", "null")
                        'txtSel1.Text = ""
                        txtIdComponente.Text = ""
                        Exit Sub
                    End If

                    '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO
                    If PAR.OracleConn.State = Data.ConnectionState.Open Then
                        Response.Write("IMPOSSIBILE VISUALIZZARE")
                        Exit Sub
                    Else
                        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        PAR.SettaCommand(PAR)
                        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        '‘par.cmd.Transaction = par.myTrans


                        If txtIdComponente.Text > 0 Then
                            PAR.cmd.Parameters.Clear()
                            Dim daEliminare As Boolean = False
                            PAR.cmd.CommandText = "select * from siscom_mi.manutenzioni_interventi where id = " & txtIdComponente.Text & " and importo_presunto=0"
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader
                            If lettore.HasRows Then
                                daEliminare = True
                            End If
                            If daEliminare Then
                                PAR.cmd.CommandText = "delete from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where ID_MANUTENZIONI_INTERVENTI = " & txtIdComponente.Text
                                PAR.cmd.ExecuteNonQuery()
                                PAR.cmd.CommandText = "delete from SISCOM_MI.MANUTENZIONI_INTERVENTI where ID = " & txtIdComponente.Text
                                PAR.cmd.ExecuteNonQuery()
                                PAR.cmd.CommandText = ""
                                PAR.cmd.Parameters.Clear()
                                BindGrid_Interventi()
                            Else
                                RadWindowManager1.RadAlert("Attenzione...Impossibile eliminare intervento con importo presunto diverso da 0!", 300, 150, "Attenzione", "", "null")
                            End If
                        ElseIf txtIdComponente.Text < 0 Then

                            Dim dt As New Data.DataTable
                            dt.Columns.Add("ID")
                            dt.Columns.Add("TIPOLOGIA")
                            dt.Columns.Add("DETTAGLIO")
                            dt.Columns.Add("IMPORTO_PRESUNTO")
                            dt.Columns.Add("IMPORTO_CONSUNTIVO")
                            dt.Columns.Add("IMPORTO_RIMBORSO")
                            dt.Columns.Add("ID_IMPIANTO")
                            dt.Columns.Add("ID_COMPLESSO")
                            dt.Columns.Add("ID_EDIFICIO")
                            dt.Columns.Add("ID_SCALA")
                            dt.Columns.Add("ID_UNITA_IMMOBILIARE")
                            dt.Columns.Add("ID_UNITA_COMUNE")
                            dt.Columns.Add("FL_BLOCCATO")
                            Dim riga As Data.DataRow
                            For Each elemento As GridDataItem In DataGrid1.Items
                                If elemento.Cells(PAR.IndRDGC(DataGrid1, "ID")).Text <> txtIdComponente.Text Then
                                    riga = dt.NewRow
                                    riga.Item("ID") = elemento.Cells(PAR.IndRDGC(DataGrid1, "ID")).Text
                                    riga.Item("TIPOLOGIA") = CType(elemento.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue
                                    riga.Item("DETTAGLIO") = CType(elemento.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue
                                    riga.Item("IMPORTO_PRESUNTO") = CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text
                                    riga.Item("IMPORTO_CONSUNTIVO") = CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Text
                                    riga.Item("IMPORTO_RIMBORSO") = CType(elemento.FindControl("TextBoxImportoRimborso"), TextBox).Text
                                    riga.Item("ID_IMPIANTO") = elemento.Cells(PAR.IndRDGC(DataGrid1, "ID_IMPIANTO")).Text
                                    riga.Item("ID_COMPLESSO") = elemento.Cells(PAR.IndRDGC(DataGrid1, "ID_COMPLESSO")).Text
                                    riga.Item("ID_EDIFICIO") = elemento.Cells(PAR.IndRDGC(DataGrid1, "ID_EDIFICIO")).Text
                                    riga.Item("ID_SCALA") = elemento.Cells(PAR.IndRDGC(DataGrid1, "ID_SCALA")).Text
                                    riga.Item("ID_UNITA_IMMOBILIARE") = elemento.Cells(PAR.IndRDGC(DataGrid1, "ID_UNITA_IMMOBILIARE")).Text
                                    riga.Item("ID_UNITA_COMUNE") = elemento.Cells(PAR.IndRDGC(DataGrid1, "ID_UNITA_COMUNE")).Text
                                    riga.Item("FL_BLOCCATO") = elemento.Cells(PAR.IndRDGC(DataGrid1, "FL_BLOCCATO")).Text
                                    dt.Rows.Add(riga)
                                End If
                            Next
                            DataGrid1.DataSource = dt
                            DataGrid1.DataBind()
                        End If

                        '*** EVENTI_MANUTENZIONE
                        PAR.InserisciEventoManu(PAR.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLI_MANUTENZIONE, "Intervento di Manutenzione")

                    End If
                End If
                'txtSel1.Text = ""
                txtIdComponente.Text = ""

            End If
            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

            '*** SOMMA IMPORTO
            SommaTot = 0


            For i = 0 To Me.DataGrid1.Items.Count - 1
                If Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text = "&nbsp;" Then Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text = ""
                SommaTot = SommaTot + PAR.IfEmpty(Me.DataGrid1.Items(i).Cells(PAR.IndRDGC(DataGrid1, "IMPORTO_PRESUNTO")).Text, 0)
            Next i

            CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImporto"), TextBox).Text = IsNumFormat(SommaTot, "", "##,##0.00")
            CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoControllo"), HiddenField).Value = IsNumFormat(SommaTot, "", "##,##0.00")
            CalcolaImporti(SommaTot, PAR.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value, 0), PAR.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))
            '**********************

            AbilitaDisabilita()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            If vIdManutenzione <> -1 Then
                PAR.myTrans.Rollback()

                'PAR.myTrans = par.OracleConn.BeginTransaction()
                '''par.cmd.Transaction = par.myTrans
                'HttpContext.Current.Session.Add("TRANSAZIONE" & IdConnessione, PAR.myTrans)
            End If

            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & IdConnessione)

            Page.Dispose()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub DataGrid1_PreRender(sender As Object, e As System.EventArgs) Handles DataGrid1.PreRender
        If HiddenAggiungi.Value = 0 Then
            If CType(Me.Page.FindControl("HiddenMostraPulsantiDett"), HiddenField).Value = 0 Then
                DataGrid1.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
                DataGrid1.Enabled = False
                DataGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
                DataGrid1.DataBind()
                'DataGrid1.Rebind()
                BindGrid_Consuntivi()
                'ElseIf CType(Me.Page.FindControl("HiddenMostraPulsantiDett"), HiddenField).Value = 1 Then
                '    DataGrid1.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = True
                '    DataGrid1.Enabled = True
                '    DataGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top
                '    DataGrid1.DataBind()
                '    ' DataGrid1.Rebind()
                '    BindGrid_Consuntivi()
            End If
        ElseIf HiddenAggiungi.Value = 1 Then
            HiddenAggiungi.Value = 0
        End If
    End Sub
    Protected Sub btnElimina2_Click(sender As Object, e As System.EventArgs)
        EliminaDettaglio()
    End Sub
End Class
