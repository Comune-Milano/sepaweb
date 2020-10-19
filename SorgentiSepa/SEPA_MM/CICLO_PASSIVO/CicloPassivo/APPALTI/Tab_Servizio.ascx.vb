Imports System.Collections
Imports Telerik.Web.UI

Partial Class Tab_Servizio
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim lstservizi As System.Collections.Generic.List(Of Mario.VociServizi)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        lstservizi = CType(HttpContext.Current.Session.Item("LSTSERVIZI"), System.Collections.Generic.List(Of Mario.VociServizi))

        If CType(Page.FindControl("Tab_Appalto_generale").FindControl("canone"), HiddenField).Value <> "" Then
            CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtpercanone"), TextBox).Text = CType(Page.FindControl("Tab_Appalto_generale").FindControl("canone"), HiddenField).Value
        End If
        If CType(Page.FindControl("Tab_Appalto_generale").FindControl("consumo"), HiddenField).Value <> "" Then
            CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtperconsumo"), TextBox).Text = CType(Page.FindControl("Tab_Appalto_generale").FindControl("consumo"), HiddenField).Value
        End If

        Try
            If Not IsPostBack Then

                Me.txtimportocorpo.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                Me.txtimportocorpo.Attributes.Add("onBlur", "javascript:CalcolaPercentuale(document.getElementById('Tab_Servizio_RadWindowServizi_C_txtOnerCanone'),this.value,Tab_Servizio_RadWindowServizi_C_txtpercanone);AutoDecimal(this,2);return false;")

                Me.txtimportoconsumo.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                Me.txtimportoconsumo.Attributes.Add("onBlur", "javascript:CalcolaPercentuale(document.getElementById('Tab_Servizio_RadWindowServizi_C_txtOneriConsumo'),this.value,Tab_Servizio_RadWindowServizi_C_txtperconsumo);AutoDecimal2(this,2);return false;")

                Me.txtscontoconsumo.Attributes.Add("onBlur", "javascript:AutoDecimalPercentage(this);")
                Me.txtscontocorpo.Attributes.Add("onBlur", "javascript:AutoDecimalPercentage(this);")

                Me.txtOnerCanone.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                Me.txtOnerCanone.Attributes.Add("onBlur", "javascript:CalcolaPercentuale(this,document.getElementById('Tab_Servizio_RadWindowServizi_C_txtimportocorpo').value,Tab_Servizio_RadWindowServizi_C_txtpercanone);AutoDecimal2(this);return false;")

                Me.txtOneriConsumo.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                Me.txtOneriConsumo.Attributes.Add("onBlur", "javascript:CalcolaPercentuale(this,document.getElementById('Tab_Servizio_RadWindowServizi_C_txtimportoconsumo').value,Tab_Servizio_RadWindowServizi_C_txtperconsumo);AutoDecimal2(this);return false;")
                Me.cmbFreqPagamento.Attributes.Add("onchange", "javascript:visbilityImage();")
                Me.txtivaconsumo.Attributes.Add("onBlur", "javascript:AutoDecimalPercentage2(this);")
                Me.txtivacorpo.Attributes.Add("onBlur", "javascript:AutoDecimalPercentage2(this);")

                If Not IsNothing(lstservizi) Then
                    lstservizi.Clear()
                End If
                idLotti = CType(Me.Page.FindControl("txtidlotto"), HiddenField).Value

                ' CONNESSIONE DB
                IdConnessione = CType(Me.Page.FindControl("txtConnessione"), HiddenField).Value

                'peppe modify intervengo nel commentare le righe che scrivono impossibile visualizzare ma non ho ben capito
                ' a cosa servisse questo if....la connessione di solito si setta nel aspx principale...gli ascx la usano
                'oppure per gli ascx che leggono dati e basta se ne crea una e la si chiude immediatamente

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    'Response.Write("IMPOSSIBILE VISUALIZZARE")
                    'Exit Sub
                Else
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                End If
                ''''''''''''''''''''''''''
                'caricaFornitori()

                DataGrid3.Rebind()
                If CType(Me.Page, Object).vIdAppalti > 0 Then
                    CalcolaImpContrattuale()
                    CalcolaResiduo()

                End If
            End If

            idLotti = CType(Me.Page.FindControl("txtidlotto"), HiddenField).Value

            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Or DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "ATTIVO" Then
                FrmSolaLettura()
            End If
            'If Session.Item("STAPPALTO") = "ATTIVO" Then
            '    Me.btnApriAppalti.Visible = True
            '    Me.btn_ChiudiAppalti.Visible = True
            'End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
        End Try

    End Sub

    Private Property idLotti() As Long
        Get
            If Not (ViewState("par_idLotti") Is Nothing) Then
                Return CLng(ViewState("par_idLotti"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idLotti") = value
        End Set

    End Property
    Private Property DescPF() As String
        Get
            If Not (ViewState("par_DescPf") Is Nothing) Then
                Return CStr(ViewState("par_DescPf"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DescPf") = value
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

    'APPALTI GRID3
    Public Sub BindGrid_Servizi()
        Dim StringaSql As String

        Try

            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)


            ' I NOMI DEI CAMPI DEVONO CORRISPONDERE CON QUELLI NELLA DATAGRID

            StringaSql = " select rownum as ""ID"",SISCOM_MI.PF_VOCI_IMPORTO.ID_LOTTO," _
                             & " SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO,TAB_SERVIZI.DESCRIZIONE AS SERVIZIO,SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO," _
                             & " SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE,TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IMPORTO_CANONE,'9G999G999G999G999G990D99')) AS ""IMPORTO_CANONE""," _
                             & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.SCONTO_CANONE,'9G999G999G999G999G990D999'))AS ""SCONTO_CANONE"",TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IVA_CANONE,'999G990D99')) AS IVA_CANONE," _
                             & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IMPORTO_CONSUMO,'9G999G999G999G999G990D99')) AS ""IMPORTO_CONSUMO""," _
                             & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.SCONTO_CONSUMO,'9G999G999G999G999G990D999'))AS ""SCONTO_CONSUMO"",TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IVA_CONSUMO,'999G990D99')) AS IVA_CONSUMO,(PF_VOCI.CODICE ||' '|| PF_VOCI.DESCRIZIONE) AS DESC_PF ," _
                             & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.ONERI_SICUREZZA_CANONE,'9G999G999G999G999G990D99'))AS ""ONERI_SICUREZZA_CANONE"",TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.ONERI_SICUREZZA_CONSUMO,'9G999G999G999G999G990D99'))AS ""ONERI_SICUREZZA_CONSUMO""" _
                             & " , TRIM (TO_CHAR (nvl((select imponibile from siscom_mi.APPALTI_ANTICIPI_CONTRATTUALI where id_appalto = " & CType(Me.Page, Object).vIdAppalti & " AND APPALTI_ANTICIPI_CONTRATTUALI.ID_PF_VOCE_IMPORTO = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO),0),  '9G999G999G999G999G990D99')) as imponibile " _
                             & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.TAB_SERVIZI ,SISCOM_MI.PF_VOCI" _
                      & " where SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID " _
                      & "   and SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & " AND SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO = TAB_SERVIZI.ID  AND PF_VOCI_IMPORTO.ID_VOCE = PF_VOCI.ID"


            par.cmd.CommandText = StringaSql

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "APPALTI_LOTTI_SERVIZI")


            DataGrid3.DataSource = ds
            DataGrid3.DataBind()

            ds.Dispose()

            AggiornaVociServizi()
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
        End Try

    End Sub
    'APPALTI GRID3
    'Public Sub BindGrid_Servizi()
    '    Dim StringaSql As String

    '    Try

    '        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
    '        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '        par.SettaCommand(par)
    '        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)


    '        ' I NOMI DEI CAMPI DEVONO CORRISPONDERE CON QUELLI NELLA DATAGRID

    '        StringaSql = " select rownum as ""ID"",SISCOM_MI.PF_VOCI_IMPORTO.ID_LOTTO," _
    '                         & " SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO,TAB_SERVIZI.DESCRIZIONE AS SERVIZIO,SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO," _
    '                         & " SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE,TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IMPORTO_CANONE,'9G999G999G999G999G990D99')) AS ""IMPORTO_CANONE""," _
    '                         & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.SCONTO_CANONE,'9G999G999G999G999G990D999'))AS ""SCONTO_CANONE"",TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IVA_CANONE,'999G990D99')) AS IVA_CANONE," _
    '                         & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IMPORTO_CONSUMO,'9G999G999G999G999G990D99')) AS ""IMPORTO_CONSUMO""," _
    '                         & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.SCONTO_CONSUMO,'9G999G999G999G999G990D999'))AS ""SCONTO_CONSUMO"",TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IVA_CONSUMO,'999G990D99')) AS IVA_CONSUMO,(PF_VOCI.CODICE ||' '|| PF_VOCI.DESCRIZIONE) AS DESC_PF ," _
    '                         & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.ONERI_SICUREZZA_CANONE,'9G999G999G999G999G990D99'))AS ""ONERI_SICUREZZA_CANONE"",TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.ONERI_SICUREZZA_CONSUMO,'9G999G999G999G999G990D99'))AS ""ONERI_SICUREZZA_CONSUMO""" _
    '                         & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.TAB_SERVIZI ,SISCOM_MI.PF_VOCI" _
    '                  & " where SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID " _
    '                  & "   and SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & " AND SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO = TAB_SERVIZI.ID  AND PF_VOCI_IMPORTO.ID_VOCE = PF_VOCI.ID"


    '        par.cmd.CommandText = StringaSql

    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        Dim ds As New Data.DataSet()

    '        da.Fill(ds, "APPALTI_LOTTI_SERVIZI")


    '        DataGrid3.DataSource = ds
    '        DataGrid3.DataBind()

    '        ds.Dispose()

    '        AggiornaVociServizi()
    '    Catch ex As Exception
    '        CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
    '        CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
    '    End Try

    'End Sub



    Function ControlloCampiAppalti() As Boolean
        ControlloCampiAppalti = True
        If cmbservizio.SelectedValue = "-1" Then
            'Response.Write("<script>alert('Scegliere un servizio!');</script>")
            RadWindowManager1.RadAlert("Scegliere un servizio!", 300, 150, "Attenzione", "", "null")
            ControlloCampiAppalti = False
            Exit Function
        End If

        If cmbvoce.SelectedValue = "-1" Then
            Dim script As String = "function f(){$find(""" + RadWindowServizi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            RadWindowManager1.RadAlert("Impossibile salvare se non si seleziona almeno una voce di servizio o non ci sono voci disponibili!", 300, 150, "Attenzione", "", "null")
            'Response.Write("<script>alert('Impossibile salvare se non si seleziona almeno una voce di servizio o non ci sono voci disponibili!');</script>")
            ControlloCampiAppalti = False
            Exit Function
        End If

        If par.IfEmpty(Me.txtimportocorpo.Text, 0) > 0 And Me.cmbFreqPagamento.SelectedValue = 0 Then
            Dim script As String = "function f(){$find(""" + RadWindowServizi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            RadWindowManager1.RadAlert("Definire la frequenza di pagamento!", 300, 150, "Attenzione", "", "null")
            'Response.Write("<script>alert('Definire la frequenza di pagamento!')</script>")
            ControlloCampiAppalti = False
            Exit Function
        End If
        If par.IfEmpty(Me.txtimportocorpo.Text, 0) = 0 And Me.cmbFreqPagamento.SelectedValue > 0 Then
            Dim script As String = "function f(){$find(""" + RadWindowServizi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            RadWindowManager1.RadAlert("Definire l\importo a canone, altrimenti impostare la frequenza su Non Definito!", 300, 150, "Attenzione", "", "null")
            'Response.Write("<script>alert('Definire l\importo a canone, altrimenti impostare la frequenza su Non Definito!')</script>")
            ControlloCampiAppalti = False
            Exit Function
        End If
        'If Me.txtAnnoRealizzazioneP.Text = "dd/mm/YYYY" Then
        '    Me.txtAnnoRealizzazioneP.Text = ""
        'End If
    End Function

  
    Private Function ControlImporti() As Boolean
        ControlImporti = True
        Dim prenotato As Decimal
        Dim VOCE As String = 0
        If String.IsNullOrEmpty(Session.Item("VoceSadVecchia")) = True Then
            VOCE = Me.idvoce.Value
        Else
            VOCE = Session.Item("VoceSadVecchia")
        End If

        If CType(Me.Page, Object).vIdAppalti = 0 Then
            Dim lstscdenze As System.Collections.Generic.List(Of Mario.ScadenzeManuali)
            lstscdenze = CType(HttpContext.Current.Session.Item("LSTSCADENZE"), System.Collections.Generic.List(Of Mario.ScadenzeManuali))
            For Each r As Mario.ScadenzeManuali In lstscdenze
                If r.ID_PF_VOCE_IMPORTO = VOCE Then
                    prenotato = prenotato + CDec(r.IMPORTO.Replace(".", ","))

                End If
            Next

        Else

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)

            par.cmd.CommandText = "SELECT SUM(IMPORTO) AS PRENOTATO FROM SISCOM_MI.APPALTI_SCADENZE WHERE ID_APPALTO  =  " & CType(Me.Page, Object).vIdAppalti & " " _
                    & " AND ID_PF_VOCE_IMPORTO = " & VOCE
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                prenotato = par.IfNull(lettore("PRENOTATO"), 0)
            End If
            lettore.Close()



        End If
        '*************************controllo rate siano inferiori o uguali all'importo a canone della singola voce
        Dim TotSuVoceCanone As Decimal = CDec(par.IfEmpty(Me.txtimportocorpo.Text.Replace(".", ""), 0)) - (((CDec(par.IfEmpty(Me.txtimportocorpo.Text.Replace(".", ""), 0))) * CDec(par.IfEmpty(Me.txtscontocorpo.Text, 0))) / 100)
        TotSuVoceCanone = TotSuVoceCanone + CDec(par.IfEmpty(Me.txtOnerCanone.Text.Replace(".", ""), 0))
        TotSuVoceCanone = TotSuVoceCanone + ((TotSuVoceCanone * CDec(par.IfEmpty(Me.txtivacorpo.Text, 0))) / 100)
        TotSuVoceCanone = Math.Round(TotSuVoceCanone, 4)

        Dim TotSuVoceConsumo As Decimal = CDec(par.IfEmpty(Me.txtimportoconsumo.Text.Replace(".", ""), 0)) - (((CDec(par.IfEmpty(Me.txtimportoconsumo.Text.Replace(".", ""), 0))) * CDec(par.IfEmpty(Me.txtscontoconsumo.Text, 0))) / 100)
        TotSuVoceConsumo = TotSuVoceConsumo + CDec(par.IfEmpty(Me.txtOneriConsumo.Text.Replace(".", ""), 0))
        TotSuVoceConsumo = TotSuVoceConsumo + ((TotSuVoceConsumo * CDec(par.IfEmpty(Me.txtivaconsumo.Text, 0))) / 100)
        TotSuVoceConsumo = Math.Round(TotSuVoceConsumo, 4)

        If prenotato > 0 Then
            If prenotato > TotSuVoceCanone Then
                ControlImporti = False
                Dim script As String = "function f(){$find(""" + RadWindowServizi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                RadWindowManager1.RadAlert("Attenzione!Verificare gli importi delle scadenze manuali definite!Impossibile procedere", 300, 150, "Attenzione", "", "null")
                'Response.Write("<script>alert('Attenzione!Verificare gli importi delle scadenze manuali definite!Impossibile procedere')</script>")
            End If
        End If

        If TotSuVoceCanone < PagatoCanone.Value Then
            ControlImporti = False
            Dim script As String = "function f(){$find(""" + RadWindowServizi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            RadWindowManager1.RadAlert("Impossibile aggiornare la voce perchè l\'importo a canone definito è inferiore a quanto già pagato!", 300, 150, "Attenzione", "", "null")
            'Response.Write("<script>alert('Impossibile aggiornare la voce perchè l\'importo a canone definito è inferiore a quanto già pagato!')</script>")
        End If

        If TotSuVoceConsumo < PagatoConsumo.Value Then
            ControlImporti = False
            Dim script As String = "function f(){$find(""" + RadWindowServizi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            RadWindowManager1.RadAlert("Impossibile aggiornare la voce perchè l\'importo a consumo definito è inferiore a quanto già definito in manutenzione!", 300, 150, "Attenzione", "", "null")
            'Response.Write("<script>alert('Impossibile aggiornare la voce perchè l\'importo a consumo definito è inferiore a quanto già definito in manutenzione!')</script>")
        End If


    End Function
    Private Sub AggiornaVoceScadenza()
        Try
            Dim prenotato As Decimal
            If Not String.IsNullOrEmpty(Session.Item("VoceSadVecchia")) Then

                If Session.Item("VoceSadVecchia") <> idvoce.Value Then
                    If CType(Me.Page, Object).vIdAppalti = 0 Then

                        Dim lstscdenze As System.Collections.Generic.List(Of Mario.ScadenzeManuali)
                        lstscdenze = CType(HttpContext.Current.Session.Item("LSTSCADENZE"), System.Collections.Generic.List(Of Mario.ScadenzeManuali))
                        For Each r As Mario.ScadenzeManuali In lstscdenze
                            If r.ID_PF_VOCE_IMPORTO = Session.Item("VoceSadVecchia") Then
                                r.ID_PF_VOCE_IMPORTO = idvoce.Value
                                prenotato = prenotato + r.IMPORTO
                            End If
                        Next
                    Else
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)


                        par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_SCADENZE SET ID_PF_VOCE_IMPORTO = " & idvoce.Value _
                                            & " WHERE ID_APPALTO =" & CType(Me.Page, Object).vIdAppalti _
                                            & " AND ID_PF_VOCE_IMPORTO = " & Session.Item("VoceSadVecchia")

                        par.cmd.ExecuteNonQuery()

                    End If

                End If
            End If

            Session.Remove("VoceSadVecchia")





        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
        End Try
    End Sub

    Public Sub CalcolaImpContrattuale()
        Try
            Dim TotOnCanone As Decimal = 0
            Dim TotOnConsumo As Decimal = 0
            Dim VarCan As Decimal = 0
            Dim VarCons As Decimal = 0
            If CType(Me.Page, Object).vIdAppalti > 0 Then




                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)



                '***************14/02/2014 variazioni canone
                par.cmd.CommandText = "SELECT SUM(importo) as ""VAR_CANONE"" FROM SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI where id_variazione in (SELECT ID FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID_TIPOLOGIA = 5 AND  ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & ")"
                Dim myReaderlotto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReaderlotto.Read Then
                    DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtVarCan"), TextBox).Text = IsNumFormat(par.IfNull(myReaderlotto("VAR_CANONE"), 0), "", "##,##0.00")
                    VarCan = par.IfNull(myReaderlotto("VAR_CANONE"), 0)
                End If
                myReaderlotto.Close()
                '***************14/02/2014 variazioni CONSUMO
                par.cmd.CommandText = "SELECT SUM(importo) as ""VAR_CONSUMO"" FROM SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI where id_variazione in (SELECT ID FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID_TIPOLOGIA = 6 AND  ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & ")"
                myReaderlotto = par.cmd.ExecuteReader
                If myReaderlotto.Read Then
                    DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtVarCons"), TextBox).Text = IsNumFormat(par.IfNull(myReaderlotto("VAR_CONSUMO"), 0), "", "##,##0.00")
                    VarCons = par.IfNull(myReaderlotto("VAR_CONSUMO"), 0)

                End If
                myReaderlotto.Close()


                'l'iva è da aggiungere all'importo comprensivo di sconto e percentuale di oneri

                par.cmd.CommandText = "SELECT ROUND(SUM(SUM ((Importo_consumo - (Importo_consumo*(sconto_consumo/100))))),4) AS ImpContConsumo, " _
                                    & "ROUND(SUM(SUM ((Importo_canone - (Importo_canone*(sconto_canone/100))))),4) AS ImpContCanone " _
                                    & "FROM  siscom_mi.APPALTI_LOTTI_SERVIZI WHERE id_appalto =" & CType(Me.Page, Object).vIdAppalti & " GROUP BY appalti_lotti_servizi.id_appalto"
                Dim Myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Myreader.Read Then
                    DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpContCanone"), TextBox).Text = Format(par.IfNull(Myreader("ImpContCanone"), 0), "##,##0.00")
                    DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpContConsumo"), TextBox).Text = Format(par.IfNull(Myreader("ImpContConsumo"), 0), "##,##0.00")
                End If
                Myreader.Close()
                par.cmd.CommandText = "SELECT appalti_lotti_servizi.*,appalti_lotti_servizi.PERC_ONERI_SIC_CAN,appalti_lotti_servizi.PERC_ONERI_SIC_CON,PERC_REVERSIBILITA " _
                                    & "FROM siscom_mi.appalti_lotti_servizi,siscom_mi.appalti,SISCOM_MI.PF_VOCI_IMPORTO " _
                                    & "WHERE APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO = PF_VOCI_IMPORTO.ID AND Appalti.ID = appalti_lotti_servizi.id_appalto AND id_appalto = " & CType(Me.Page, Object).vIdAppalti
                Myreader = par.cmd.ExecuteReader

                '******CALCOLO PERCENTUALE ONERI IN BASE AL TESTO SCRITTO
                'Dim PercOneriCon As Decimal = par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtperconsumo"), TextBox).Text, 0)
                'Dim PercOneriCan As Decimal = par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtpercanone"), TextBox).Text, 0)
                Dim TOTNettoCan As Decimal = 0
                Dim TOTNettoCons As Decimal = 0
                Dim ToTReversCanone As Decimal = 0
                Dim ToTReversConsumo As Decimal = 0


                If Myreader.HasRows = True Then
                    Dim ImpContConsumo As Decimal = 0
                    Dim ImpContCanone As Decimal = 0
                    Dim Netto As Decimal = 0
                    Dim NettoPlusOneri As Decimal = 0
                    Dim appoggio As Decimal = 0
                    While Myreader.Read

                        'importo contrattuale CONSUMO
                        Netto = par.IfNull(Myreader("IMPORTO_CONSUMO"), 0) - ((par.IfNull(Myreader("IMPORTO_CONSUMO"), 0) * par.IfNull(Myreader("SCONTO_CONSUMO"), 0)) / 100)

                        NettoPlusOneri = Netto + (par.IfNull(Myreader("ONERI_SICUREZZA_CONSUMO"), 0))
                        appoggio = (NettoPlusOneri + ((NettoPlusOneri * par.IfNull(Myreader("IVA_CONSUMO"), 0)) / 100))
                        ImpContConsumo = ImpContConsumo + appoggio
                        ToTReversConsumo = ToTReversConsumo + (((appoggio) * par.IfNull(Myreader("PERC_REVERSIBILITA"), 0)) / 100)

                        appoggio = 0
                        'importo contrattuale CANONE
                        Netto = par.IfNull(Myreader("IMPORTO_CANONE"), 0) - ((par.IfNull(Myreader("IMPORTO_CANONE"), 0) * par.IfNull(Myreader("SCONTO_CANONE"), 0)) / 100)
                        NettoPlusOneri = Netto + (par.IfNull(Myreader("ONERI_SICUREZZA_CANONE"), 0))
                        appoggio = (NettoPlusOneri + ((NettoPlusOneri * par.IfNull(Myreader("IVA_CANONE"), 0)) / 100))

                        ImpContCanone = ImpContCanone + appoggio
                        ToTReversCanone = ToTReversCanone + (((appoggio) * par.IfNull(Myreader("PERC_REVERSIBILITA"), 0)) / 100)

                        TotOnConsumo = TotOnConsumo + (par.IfNull(Myreader("ONERI_SICUREZZA_CONSUMO"), 0))
                        TotOnCanone = TotOnCanone + (par.IfNull(Myreader("ONERI_SICUREZZA_CANONE"), 0))
                    End While
                    Myreader.Close()
                    DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpTotPlusOneriCons"), TextBox).Text = Format(ImpContConsumo + VarCons, "##,##0.00")
                    DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpTotPlusOneriCan"), TextBox).Text = Format(ImpContCanone + VarCan, "##,##0.00")

                    'GESTIONE REVERSIBILITA IMPORTI A CANONE E A CONSUMO
                    DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneRevers"), TextBox).Text = Format(ToTReversCanone, "##,##0.00")
                    DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsRevers"), TextBox).Text = Format(ToTReversConsumo, "##,##0.00")

                    DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneNotRevers"), TextBox).Text = Format(ImpContCanone - ToTReversCanone, "##,##0.00")
                    DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsNotRevers"), TextBox).Text = Format(ImpContConsumo - ToTReversConsumo, "##,##0.00")


                Else
                    DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpTotPlusOneriCons"), TextBox).Text = Format(0, "##,##0.00")
                    DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpTotPlusOneriCan"), TextBox).Text = Format(0, "##,##0.00")

                End If

                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsNotRevers"), TextBox).Text = Format((par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpTotPlusOneriCons"), TextBox).Text, 0) - par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsRevers"), TextBox).Text, 0)), "##,##0.00")
                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneNotRevers"), TextBox).Text = Format((par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpTotPlusOneriCan"), TextBox).Text, 0) - par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneRevers"), TextBox).Text, 0)), "##,##0.00")

            Else

                Dim ImpContConsumo As Double = 0
                Dim ImpContCanone As Double = 0
                Dim TOTNettoCons As Decimal = 0
                Dim NettoPlusOneri As Decimal = 0
                Dim TOTNettoCan As Decimal = 0
                Dim PercOneriCon As Decimal = 0
                Dim PercOneriCan As Decimal = 0
                Dim N As Decimal = 0
                Dim NCanone As Decimal = 0
                Dim NConsumo As Decimal = 0
                Dim TotRevCanone As Decimal = 0
                Dim TotRevConsumo As Decimal = 0

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                Dim lstservizi As System.Collections.Generic.List(Of Mario.VociServizi)
                lstservizi = CType(HttpContext.Current.Session.Item("LSTSERVIZI"), System.Collections.Generic.List(Of Mario.VociServizi))
                For Each gen As Mario.VociServizi In lstservizi
                    TOTNettoCons = TOTNettoCons + (gen.IMPORTO_CONSUMO - (gen.IMPORTO_CONSUMO * (gen.SCONTO_CONSUMO / 100)))
                    TOTNettoCan = TOTNettoCan + (gen.IMPORTO_CANONE - (gen.IMPORTO_CANONE * (gen.SCONTO_CANONE / 100)))
                Next

                'If par.IfEmpty(TOTNettoCan, 0) > 0 Then
                '    PercOneriCan = (par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtonericanone"), TextBox).Text, 0) * 100) / TOTNettoCan
                'End If

                'If par.IfEmpty(TOTNettoCons, 0) > 0 Then
                '    PercOneriCon = (par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtonericonsumo"), TextBox).Text, 0) * 100) / TOTNettoCons
                'End If

                '****SET ROUND PRECISIONE TO PRESERVE OVERFLOW EXCEPTION
                PercOneriCan = Math.Round(PercOneriCan, 4)
                PercOneriCon = Math.Round(PercOneriCon, 4)
                Dim appoggio As Decimal = 0
                Dim percRev As Decimal = 0
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader

                For Each gen As Mario.VociServizi In lstservizi

                    'percentuale reversibilita voce selezionata
                    par.cmd.CommandText = "SELECT PERC_REVERSIBILITA FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID = " & gen.ID_PF_VOCE_IMPORTO
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        percRev = par.IfNull(lettore("PERC_REVERSIBILITA"), 0)
                    End If
                    lettore.Close()


                    '******-*SEZIONE CANONE
                    N = (gen.IMPORTO_CANONE - (gen.IMPORTO_CANONE * (gen.SCONTO_CANONE / 100)))
                    NettoPlusOneri = N + par.IfEmpty(gen.ONERI_SICUREZZA_CANONE, 0)
                    ImpContCanone = ImpContCanone + NettoPlusOneri + ((NettoPlusOneri * gen.IVA_CANONE) / 100)
                    appoggio = NettoPlusOneri + ((NettoPlusOneri * gen.IVA_CANONE) / 100)
                    NCanone = NCanone + N
                    TotOnCanone = TotOnCanone + par.IfEmpty(gen.ONERI_SICUREZZA_CANONE, 0)
                    '**************reversibilita a canone
                    TotRevCanone = TotRevCanone + ((appoggio * percRev) / 100)


                    N = 0
                    NettoPlusOneri = 0
                    appoggio = 0
                    '*****-*SEZIONE CONSUMO
                    N = (gen.IMPORTO_CONSUMO - (gen.IMPORTO_CONSUMO * (gen.SCONTO_CONSUMO / 100)))
                    NettoPlusOneri = N + par.IfEmpty(gen.ONERI_SICUREZZA_CONSUMO, 0)
                    ImpContConsumo = ImpContConsumo + NettoPlusOneri + ((NettoPlusOneri * gen.IVA_CONSUMO) / 100)
                    appoggio = NettoPlusOneri + ((NettoPlusOneri * gen.IVA_CONSUMO) / 100)
                    NConsumo = NConsumo + N
                    TotOnConsumo = TotOnConsumo + par.IfEmpty(gen.ONERI_SICUREZZA_CONSUMO, 0)
                    'reversibilita a consumo
                    TotRevConsumo = TotRevConsumo + ((appoggio * percRev) / 100)


                Next
                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpContCanone"), TextBox).Text = Format(NCanone, "##,##0.00")
                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpContConsumo"), TextBox).Text = Format(NConsumo, "##,##0.00")

                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpTotPlusOneriCons"), TextBox).Text = Format(ImpContConsumo, "##,##0.00")
                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpTotPlusOneriCan"), TextBox).Text = Format(ImpContCanone, "##,##0.00")

                'GESTIONE REVERSIBILITA IMPORTI A CANONE E A CONSUMO
                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneRevers"), TextBox).Text = Format(TotRevCanone, "##,##0.00")
                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsRevers"), TextBox).Text = Format(TotRevConsumo, "##,##0.00")

                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneNotRevers"), TextBox).Text = Format(ImpContCanone - TotRevCanone, "##,##0.00")
                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsNotRevers"), TextBox).Text = Format(ImpContConsumo - TotRevConsumo, "##,##0.00")


            End If

            DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtonericanone"), TextBox).Text = Format(TotOnCanone, "##,##0.00")
            DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtOneriConsumo"), TextBox).Text = Format(TotOnConsumo, "##,##0.00")

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
        End Try
    End Sub

    Private Sub SalvaServizi()

        Try


            If CType(Me.Page, Object).vIdAppalti = 0 Then
                Dim gen As Mario.VociServizi
                Dim inizio As Integer = 0
                If Not String.IsNullOrEmpty(cmbvoce.SelectedItem.Text.IndexOf("-")) Then
                    inizio = cmbvoce.SelectedItem.Text.IndexOf("-")
                End If
                Dim inizioPF As Integer = 0
                If Not String.IsNullOrEmpty(DescPF.IndexOf("-")) Then
                    inizioPF = DescPF.IndexOf("-")
                End If
                gen = New Mario.VociServizi(lstservizi.Count, idLotti, Me.cmbservizio.SelectedValue, _
                                            par.PulisciStrSql(Me.cmbservizio.SelectedItem.Text), Me.cmbvoce.SelectedValue, _
                                            par.PulisciStrSql(Me.cmbvoce.SelectedItem.Text.Substring(inizio + 1)), par.IfEmpty(Me.txtimportocorpo.Text, 0), _
                                            par.IfEmpty(Me.txtOnerCanone.Text, 0), par.PuntiInVirgole(par.IfEmpty(Me.txtscontocorpo.Text, 0)), _
                                            par.PuntiInVirgole(par.IfEmpty(Me.txtivacorpo.Text, 0)), Me.cmbFreqPagamento.SelectedValue, _
                                            par.IfEmpty(Me.txtimportoconsumo.Text, 0), par.IfEmpty(Me.txtOneriConsumo.Text, 0), par.PuntiInVirgole(par.IfEmpty(Me.txtscontoconsumo.Text, 0)), _
                                            par.PuntiInVirgole(par.IfEmpty(Me.txtivaconsumo.Text, 0)), DescPF)



                DataGrid3.DataSource = Nothing
                lstservizi.Add(gen)
                gen = Nothing

                DataGrid3.DataSource = lstservizi
                DataGrid3.DataBind()


            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA FORNITORE

                '*********************************
                'CALCOLO: INPUT
                '       1) gen.IMPORTO_CONSUMO  txtimportoconsumo
                '       2) gen.SCONTO_CONSUMO   txtscontoconsumo
                '       3) gen.IVA_CONSUMO      txtivaconsumo
                '       4) oneri=PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtonericonsumo"), TextBox).Text, 0)

                'RESIDUO_CONSUMO= Importo al consumo -  %sconto al consumo  + % IVA Consumo + ( Oneri + % iva consumo)  

                'Dim Residuo As Decimal
                'Residuo = CalcolaResiduo(PAR.IfEmpty(Me.txtimportoconsumo.Text, 0), PAR.IfEmpty(Me.txtscontoconsumo.Text, 0), PAR.IfEmpty(Me.txtivaconsumo.Text, 0), PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtonericonsumo"), TextBox).Text, 0))
                '**************************************


                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)


                Dim PercOnSicConsumo As Decimal = 0
                If par.IfEmpty(txtimportoconsumo.Text, 0) > 0 Then
                    PercOnSicConsumo = (par.IfEmpty(par.VirgoleInPunti(Me.txtOneriConsumo.Text.Replace(".", "")), 0) / par.IfEmpty(par.VirgoleInPunti(Me.txtimportoconsumo.Text.Replace(".", "")), "0")) * 100
                    PercOnSicConsumo = Math.Round(PercOnSicConsumo, 4)
                End If

                Dim PercOneriSicCanone As Decimal = 0
                If par.IfEmpty(txtimportocorpo.Text, 0) > 0 Then
                    PercOneriSicCanone = (par.IfEmpty(par.VirgoleInPunti(Me.txtOnerCanone.Text.Replace(".", "")), 0) / par.IfEmpty(par.VirgoleInPunti(Me.txtimportocorpo.Text.Replace(".", "")), "0")) * 100
                    PercOneriSicCanone = Math.Round(PercOneriSicCanone, 4)
                End If
                par.cmd.CommandText = "insert into SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                            & " (ID_APPALTO,ID_PF_VOCE_IMPORTO," _
                            & "  IMPORTO_CANONE,SCONTO_CANONE,ONERI_SICUREZZA_CANONE,PERC_ONERI_SIC_CAN,IVA_CANONE,FREQUENZA_PAGAMENTO," _
                            & "  IMPORTO_CONSUMO,SCONTO_CONSUMO,ONERI_SICUREZZA_CONSUMO,PERC_ONERI_SIC_CON,IVA_CONSUMO)" _
                            & " values (" & CType(Me.Page, Object).vIdAppalti & "," & Me.cmbvoce.SelectedValue & "," _
                            & par.IfEmpty(par.VirgoleInPunti(Me.txtimportocorpo.Text.Replace(".", "")), "0") & "," & par.IfEmpty(par.VirgoleInPunti(Me.txtscontocorpo.Text), "0") & "," & par.IfEmpty(par.VirgoleInPunti(Me.txtOnerCanone.Text.Replace(".", "")), 0) & "," & par.VirgoleInPunti(PercOneriSicCanone) & "," & par.IfEmpty(par.VirgoleInPunti(Me.txtivacorpo.Text), "0") & "," & Me.cmbFreqPagamento.SelectedValue & "," _
                            & par.IfEmpty(par.VirgoleInPunti(Me.txtimportoconsumo.Text.Replace(".", "")), "0") & "," & par.IfEmpty(par.VirgoleInPunti(Me.txtscontoconsumo.Text), "0") & "," & par.IfEmpty(par.VirgoleInPunti(Me.txtOneriConsumo.Text.Replace(".", "")), 0) & "," & par.VirgoleInPunti(PercOnSicConsumo) & "," & par.IfEmpty(par.VirgoleInPunti(Me.txtivaconsumo.Text), "0") & ")"
                par.cmd.ExecuteNonQuery()

                'aggiorna gli appalti pluriennali

                AllineaPluriennali()


                '**** Ricavo ID VOCE SERVIZIO PER UTILIZZARLO IN SEGUITO
                'PAR.cmd.CommandText = " select SISCOM_MI.SEQ_APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO  from SISCOM_MI._APPALTI_LOTTI_SERVIZI where SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & cmbvoce.SelectedValue
                'Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                'If myReaderI.Read Then
                '    Me.txtIDS.Text = myReaderI("ID_VOCE_SERVIZIO")
                'End If
                'myReaderI.Close()
                'PAR.cmd.CommandText = ""
                '**********

                DataGrid3.Rebind()

                '*** EVENTI_APPALTI
                InserisciEvento(par.cmd, CType(Me.Page, Object).vIdAppalti, Session.Item("ID_OPERATORE"), 2, "Modifica appalti - Inserimento voce servizio")

            End If


            'CALCOLO SOMMA IMPORTI
            somma()

            'CALCOLA PERCENTUALE
            percentuale()

            'RESIDUO
            'CalcolaResiduo()
            CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"
            Me.cmbvoce.ClearSelection()


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"

            If CType(Me.Page, Object).vIdAppalti <> -1 Then par.myTrans.Rollback()
            par.OracleConn.Close()

            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"

        End Try

    End Sub
    ''' <summary>
    ''' Aggiusta tutti gli appalti creati
    ''' negli anni successivi a partire dal principale PLURIENNALE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AllineaPluriennali()
        '' modifica per inserire in appalti lotti servizi se appalto è pluriennale su tutte le copie create negli anni
        par.cmd.CommandText = "select id,id_lotto from siscom_mi.appalti where id_gruppo = " & CType(Me.Page, Object).vIdAppalti & " and id <> id_gruppo"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        For Each r As Data.DataRow In dt.Rows
            par.cmd.CommandText = "delete from siscom_mi.appalti_lotti_servizi where id_appalto = " & r.Item("id")
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_LOTTI_SERVIZI ( " _
                                & "ID_APPALTO, ID_PF_VOCE_IMPORTO, IMPORTO_CANONE, " _
                                & "SCONTO_CANONE, IVA_CANONE, IMPORTO_CONSUMO, " _
                                & "SCONTO_CONSUMO, IVA_CONSUMO, RESIDUO_CONSUMO, " _
                                & "ONERI_SICUREZZA_CANONE, ONERI_SICUREZZA_CONSUMO, PERC_ONERI_SIC_CAN, " _
                                & "PERC_ONERI_SIC_CON, FREQUENZA_PAGAMENTO) " _
                                & "SELECT " _
                                & "(SELECT ID FROM SISCOM_MI.APPALTI WHERE id_lotto=" & r.Item("id_lotto").ToString & " AND id_gruppo=b.id_appalto) AS id_appalto_2015, " _
                                & "(SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE id_lotto=" & r.Item("id_lotto").ToString & "  AND id_old=b.ID_PF_VOCE_IMPORTO) AS ID_PF_VOCE_IMPORTO_2015, " _
                                & "IMPORTO_CANONE, " _
                                & "SCONTO_CANONE, IVA_CANONE, IMPORTO_CONSUMO, " _
                                & "SCONTO_CONSUMO, IVA_CONSUMO, RESIDUO_CONSUMO, " _
                                & "ONERI_SICUREZZA_CANONE, ONERI_SICUREZZA_CONSUMO, PERC_ONERI_SIC_CAN, " _
                                & "PERC_ONERI_SIC_CON, FREQUENZA_PAGAMENTO " _
                                & "FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI b WHERE b.id_appalto IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE id_lotto=" & CType(Me.Page.FindControl("txtidlotto"), HiddenField).Value & "  AND ID = " & CType(Me.Page, Object).vIdAppalti & " )"
            par.cmd.ExecuteNonQuery()
        Next


    End Sub
    Private Sub UpdateAppalti()

        Try

            If CType(Me.Page, Object).vIdAppalti = 0 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA FORNITORE


                lstservizi(txtIdComponente0.Value).ID_LOTTO = idLotti
                lstservizi(txtIdComponente0.Value).ID_SERVIZIO = Me.cmbservizio.SelectedValue.ToString
                lstservizi(txtIdComponente0.Value).ID_PF_VOCE_IMPORTO = Me.cmbvoce.SelectedValue.ToString
                lstservizi(txtIdComponente0.Value).DESCRIZIONE = Me.cmbvoce.SelectedItem.Text
                lstservizi(txtIdComponente0.Value).IMPORTO_CANONE = par.IfEmpty(Me.txtimportocorpo.Text, 0)
                lstservizi(txtIdComponente0.Value).SCONTO_CANONE = par.PuntiInVirgole(par.IfEmpty(Me.txtscontocorpo.Text, 0))
                lstservizi(txtIdComponente0.Value).ONERI_SICUREZZA_CANONE = (par.IfEmpty(Me.txtOnerCanone.Text, 0))
                lstservizi(txtIdComponente0.Value).IVA_CANONE = par.PuntiInVirgole(par.IfEmpty(Me.txtivacorpo.Text, 0))
                lstservizi(txtIdComponente0.Value).IMPORTO_CONSUMO = Me.txtimportoconsumo.Text
                lstservizi(txtIdComponente0.Value).SCONTO_CONSUMO = par.PuntiInVirgole(par.IfEmpty(Me.txtscontoconsumo.Text, 0))
                lstservizi(txtIdComponente0.Value).ONERI_SICUREZZA_CONSUMO = (par.IfEmpty(Me.txtOneriConsumo.Text, 0))
                lstservizi(txtIdComponente0.Value).IVA_CONSUMO = par.PuntiInVirgole(par.IfEmpty(Me.txtivaconsumo.Text, 0))


                DataGrid3.DataSource = lstservizi
                DataGrid3.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA FORNITORE

                '*********************************
                'CALCOLO: INPUT
                '       1) gen.IMPORTO_CONSUMO  txtimportoconsumo
                '       2) gen.SCONTO_CONSUMO   txtscontoconsumo
                '       3) gen.IVA_CONSUMO      txtivaconsumo
                '       4) oneri=PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtonericonsumo"), TextBox).Text, 0)

                'RESIDUO_CONSUMO= Importo al consumo -  %sconto al consumo  + % IVA Consumo + ( Oneri + % iva consumo)  

                'Dim Residuo As Decimal
                'Residuo = CalcolaResiduo(PAR.IfEmpty(Me.txtimportoconsumo.Text, 0), PAR.IfEmpty(Me.txtscontoconsumo.Text, 0), PAR.IfEmpty(Me.txtivaconsumo.Text, 0), PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtonericonsumo"), TextBox).Text, 0))
                '**************************************


                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)






                par.cmd.CommandText = "update SISCOM_MI.APPALTI_LOTTI_SERVIZI set " _
                                            & "ID_PF_VOCE_IMPORTO=" & par.PulisciStrSql(Me.cmbvoce.SelectedValue) & "," _
                                            & "IMPORTO_CANONE=" & par.IfEmpty(par.VirgoleInPunti(Me.txtimportocorpo.Text.Replace(".", "")), "0") & "," _
                                            & "SCONTO_CANONE=" & par.IfEmpty(par.VirgoleInPunti(Me.txtscontocorpo.Text.Replace(".", "")), "0") & "," _
                                            & "IVA_CANONE=" & par.IfEmpty(par.VirgoleInPunti(Me.txtivacorpo.Text.Replace(".", "")), "0") & "," _
                                            & "ONERI_SICUREZZA_CANONE= " & par.IfEmpty(par.VirgoleInPunti(Me.txtOnerCanone.Text.Replace(".", "")), "0") & "," _
                                            & "PERC_ONERI_SIC_CAN= " & par.VirgoleInPunti(par.IfEmpty(percanone.Value, 0)) & "," _
                                            & "FREQUENZA_PAGAMENTO= " & Me.cmbFreqPagamento.SelectedValue & "," _
                                            & "IMPORTO_CONSUMO=" & par.IfEmpty(par.VirgoleInPunti(Me.txtimportoconsumo.Text.Replace(".", "")), "0") & "," _
                                            & "SCONTO_CONSUMO=" & par.IfEmpty(par.VirgoleInPunti(Me.txtscontoconsumo.Text.Replace(".", "")), "0") & "," _
                                            & "IVA_CONSUMO=" & par.IfEmpty(par.VirgoleInPunti(Me.txtivaconsumo.Text.Replace(".", "")), "0") & ", " _
                                            & "ONERI_SICUREZZA_CONSUMO= " & par.IfEmpty(par.VirgoleInPunti(Me.txtOneriConsumo.Text.Replace(".", "")), "0") & "," _
                                            & "PERC_ONERI_SIC_CON= " & par.VirgoleInPunti(par.IfEmpty(perconsumo.Value, 0)) & " " _
                                            & " WHERE ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & " and ID_PF_VOCE_IMPORTO=" & Me.txtIDS.Value

                par.cmd.ExecuteNonQuery()

                AllineaPluriennali()

                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                DataGrid3.Rebind()

                '*** EVENTI_FORNITORI
                InserisciEvento(par.cmd, CType(Me.Page, Object).vIdAppalti, Session.Item("ID_OPERATORE"), 2, "Modifica dati voci servizio appalto")

            End If



            'CALCOLO SOMMA IMPORTI
            somma()

            'CALCOLA PERCENTUALE
            percentuale()

            'RESIDUO

            'CalcolaResiduo()
            CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"


            'DEVO FARE UPDATE DELLE VOCI DI SERVIZI A CANONE E A CONSUMO SULLE QUALI é POSSIBILE INSERIRE UNA VARIAZIONE!
            'SE IL CONTRATTO è ATTIVO ED è POSSIBILE MODIFICARE I SERVIZI. SE QUESTO NON é POSSIBILE NON SERVE AGGIORNARE NULLA!

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"

            If CType(Me.Page, Object).vIdAppalti <> -1 Then par.myTrans.Rollback()
            par.OracleConn.Close()

            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"

        End Try

    End Sub

    ' APERTURA IN MODIFICA DELLA LISTA VOCI
    'Protected Sub btnApriAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriAppalti.Click



    'End Sub
    ''' <summary>
    ''' Imposta in un campo nascosto la somma degli importi già pagati a canone e a consumo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ImpostaLimiteMinimo(ByVal id_servizio As Integer, ByVal idvoce As Integer)

        par.cmd.CommandText = "SELECT SUM(NVL(importo_approvato,0)) AS totCanone " _
                            & "FROM siscom_mi.PRENOTAZIONI " _
                            & "WHERE TIPO_PAGAMENTO = 6 " _
                            & "AND id_pagamento IS NOT NULL " _
                            & "AND id_appalto IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE id_gruppo = " & CType(Me.Page, Object).vIdAppalti & ") " _
                            & "AND id_voce_pf_importo IN (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR PF_VOCI_IMPORTO.ID = PF_VOCI_IMPORTO.id_old START WITH PF_VOCI_IMPORTO.ID = " & idvoce & ")"

        Me.PagatoCanone.Value = par.IfNull(par.cmd.ExecuteScalar, 0)

        par.cmd.CommandText = "SELECT SUM(NVL(importo_approvato,0)) AS totConsumo " _
                    & "FROM siscom_mi.PRENOTAZIONI " _
                    & "WHERE TIPO_PAGAMENTO = 3 " _
                    & "AND id_pagamento IS NOT NULL " _
                    & "AND id_appalto IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE id_gruppo = " & CType(Me.Page, Object).vIdAppalti & ") " _
                    & "AND id_voce_pf_importo IN (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR PF_VOCI_IMPORTO.ID = PF_VOCI_IMPORTO.id_old START WITH PF_VOCI_IMPORTO.ID = " & idvoce & ")"

        Me.PagatoConsumo.Value = par.IfNull(par.cmd.ExecuteScalar, 0)



    End Sub
    ' FINE APERTURA MODIFICA LISTA VOCI

    'Protected Sub btnEliminaAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaAppalti.Click
    '    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
    '    Try

    '    Catch ex As Exception
    '        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
    '        Session.Item("LAVORAZIONE") = "0"
    '        If CType(Me.Page, Object).vIdAppalti <> -1 Then par.myTrans.Rollback()
    '        par.OracleConn.Close()
    '        CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
    '        CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
    '    End Try
    'End Sub

    'Protected Sub btnAggAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggAppalti.Click
    '    Try


    '        Me.txtIDS.Text = -1

    '        Me.txtimportocorpo.Text = ""
    '        Me.txtscontocorpo.Text = ""
    '        Me.txtivacorpo.Text = ""
    '        Me.txtimportoconsumo.Text = ""
    '        Me.txtscontoconsumo.Text = ""
    '        Me.txtivaconsumo.Text = ""
    '        Me.cmbservizio.SelectedValue = -1
    '        Me.cmbvoce.Items.Clear()

    '        txtIdComponente.Text = "" 'lo resetto
    '        txtIdComponente1.Text = ""

    '        Me.cmbservizio.Enabled = True
    '        Me.cmbvoce.Enabled = True

    '        Me.txtimportoconsumo.Enabled = True
    '        Me.txtscontoconsumo.Enabled = True
    '        Me.txtAppareP.Text = 1



    '        'CARICO SOLO I SERVIZI CHE NON SONO STATI ASSEGNATI
    '        'cmbvoce.Items.Add(New ListItem(" ", -1))

    '        'Dim Trovato As Boolean

    '        'PAR.cmd.CommandText = "select SISCOM_MI.PF_VOCI_IMPORTO.ID, SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE FROM SISCOM_MI.PF_VOCI_IMPORTO where SISCOM_MI.PF_VOCI_IMPORTO.ID not in (select SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI where SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & vIdAppalti & ")"
    '        'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
    '        'While myReader2.Read
    '        '    Trovato = False

    '        '    For Each genvoce As Mario.VociServizi In lstservizi

    '        '        If PAR.IfNull(myReader2("ID"), -1) = genvoce.ID_VOCE_SERVIZIO Then

    '        '            Trovato = True
    '        '            Exit For
    '        '        End If
    '        '    Next

    '        '    If Trovato = False Then
    '        '        cmbvoce.Items.Add(New ListItem(PAR.IfNull(myReader2("DESCRIZIONE"), " "), PAR.IfNull(myReader2("ID"), -1)))
    '        '    End If
    '        'End While
    '        'myReader2.Close()

    '        'cmbvoce.SelectedValue = -1

    '    Catch ex As Exception
    '        PAR.OracleConn.Close()

    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

    '    End Try
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
        Try
            DataGrid3.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
            DataGrid3.MasterTableView.Columns.FindByUniqueName("modificaServizio").Visible = False
            DataGrid3.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
            DataGrid3.Rebind()
            'Me.btnAggAppalti.Visible = False
            ' Me.btnEliminaAppalti.Visible = False
            ' Me.btnApriAppalti.Visible = False
            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
            For Each CTRL In Me.PanelServiziVoci.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is RadComboBox Then
                    DirectCast(CTRL, RadComboBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadButton Then
                    DirectCast(CTRL, RadButton).Enabled = False
                End If
            Next


            btn_ChiudiAppalti.Enabled = True

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub


    Protected Sub cmbservizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbservizio.SelectedIndexChanged
        AggiornaVociServizi()
        Dim script As String = "function f(){$find(""" + RadWindowServizi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
       
    End Sub
    Private Sub AggiornaVociServizi()
        '****************************
        '****NUOVA VERSIONE**********
        '**COMPLETAMENTE RINNOVATA***
        '****************************
        Try
            If Me.cmbservizio.SelectedValue <> "-1" Then

                '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)


                Me.cmbvoce.Items.Clear()
                ' cmbvoce.Items.Add(New ListItem(" ", -1))

                If CType(Me.Page, Object).vIdAppalti > 0 Then
                    '****************************
                    '****AGGIUNTA SERVIZI IN UPDATE**********

                    If cmbservizio.SelectedValue = Me.controllaservizio.Value And txtIdComponente.Value <> "" Then

                        par.cmd.CommandText = "select SISCOM_MI.PF_VOCI_IMPORTO.ID, (PF_VOCI.CODICE ||' - '||SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE) AS DESCRIZIONE " _
                                            & " from  SISCOM_MI.PF_VOCI_IMPORTO, siscom_mi.pf_voci  " _
                                            & " where" _
                                            & " (SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO = " & Me.cmbservizio.SelectedValue _
                                            & " or    SISCOM_MI.PF_VOCI_IMPORTO.ID = " & txtIdComponente.Value & ")" _
                                            & " AND pf_voci.ID = pf_voci_importo.id_voce order by DESCRIZIONE ASC"


                    Else
                        par.cmd.CommandText = "select SISCOM_MI.PF_VOCI_IMPORTO.ID, (PF_VOCI.CODICE ||' - '||SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE) AS DESCRIZIONE  " _
                                            & "  from SISCOM_MI.PF_VOCI_IMPORTO, siscom_mi.pf_voci  " _
                                            & " where SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO = " & Me.cmbservizio.SelectedValue _
                                            & " AND SISCOM_MI.PF_VOCI_IMPORTO.ID NOT IN (SELECT ID_PF_VOCE_IMPORTO FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & ") " _
                                            & " and SISCOM_MI.PF_VOCI_IMPORTO.id_lotto = " & CType(Me.Page.FindControl("txtidlotto"), HiddenField).Value _
                                            & " AND pf_voci.ID = pf_voci_importo.id_voce order by DESCRIZIONE ASC"

                    End If
                Else 'AGGIUNTA SERVIZI TOGLIENDO QUELLI GIA' CARICATI NELLA CLASSE
                    '****************************
                    '****AGGIUNTA PRIMA INSERT**********
                    Dim servizi As String
                    Dim Condizione As String = ""
                    servizi = ""
                    For Each gen As Mario.VociServizi In lstservizi

                        If servizi = "" Then
                            servizi = gen.ID_PF_VOCE_IMPORTO
                        Else
                            servizi = servizi & "," & gen.ID_PF_VOCE_IMPORTO
                        End If
                    Next

                    If servizi <> "" Then
                        Condizione = "SISCOM_MI.PF_VOCI_IMPORTO.ID not in " & "(" & servizi & ") AND "
                    Else
                        Condizione = ""
                    End If

                    Select Case CType(Me.Page.FindControl("idStatoPf"), HiddenField).Value
                        Case 6
                            If Session.Item("FL_COMI") <> 1 Then
                                Condizione = Condizione & "PF_VOCI.ID IN (SELECT ID FROM siscom_mi.PF_VOCI WHERE FL_CC = 1 AND id_piano_finanziario =" & CType(Me.Page.FindControl("txtIdPianoFinanziario"), HiddenField).Value & ") AND"
                            End If

                        Case 7
                            Condizione = Condizione & "PF_VOCI.ID IN (SELECT ID FROM siscom_mi.PF_VOCI WHERE FL_CC = 1 AND id_piano_finanziario =" & CType(Me.Page.FindControl("txtIdPianoFinanziario"), HiddenField).Value & ") AND"

                    End Select
                    par.cmd.CommandText = "select SISCOM_MI.PF_VOCI_IMPORTO.ID, (PF_VOCI.CODICE ||' - '||SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE) AS DESCRIZIONE  " _
                                        & " from SISCOM_MI.PF_VOCI_IMPORTO, siscom_mi.pf_voci  " _
                                        & " where " & Condizione _
                                        & " SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO=" & Me.cmbservizio.SelectedValue _
                                        & " and id_lotto = " & CType(Me.Page.FindControl("txtidlotto"), HiddenField).Value & "" _
                                        & " AND pf_voci.ID = pf_voci_importo.id_voce order by DESCRIZIONE ASC"




                End If

                'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                'myReader2 = par.cmd.ExecuteReader()
                'While myReader2.Read()
                '    cmbvoce.Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
                'End While
                'myReader2.Close()
                par.caricaComboTelerik(par.cmd.CommandText, cmbvoce, "ID", "DESCRIZIONE", True)

                
            Else
                Me.cmbvoce.Items.Clear()
                Dim item As New RadComboBoxItem
                item.Text = " "
                item.Value = "-1"
                cmbvoce.Items.Add(item)
                'cmbvoce.Items.Add(New ListItem(" ", -1))
            End If
           
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"

        End Try
    End Sub
    'Protected Sub DataGrid3_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid3.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then


    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Servizio_txtSelAppalti').value='Hai selezionato: " & Replace(Mid(e.Item.Cells(6).Text, 1, 80), "'", "\'") & "';document.getElementById('Tab_Servizio_txtIdComponente').value='" & e.Item.Cells(5).Text & "';document.getElementById('Tab_Servizio_txtIdComponente0').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_Servizio_txtIdComponente1').value='" & e.Item.Cells(2).Text & "';")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then


    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Servizio_txtSelAppalti').value='Hai selezionato: " & Replace(Mid(e.Item.Cells(6).Text, 1, 80), "'", "\'") & "';document.getElementById('Tab_Servizio_txtIdComponente').value='" & e.Item.Cells(5).Text & "';document.getElementById('Tab_Servizio_txtIdComponente0').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_Servizio_txtIdComponente1').value='" & e.Item.Cells(2).Text & "';")

    '    End If
    'End Sub

    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
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
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
        End Try
        Return a

    End Function
    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDec(v), Precision)
        End If
    End Function


    Function IsNumFormatClasse(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If v = "Null" Then
            IsNumFormatClasse = S
        Else
            IsNumFormatClasse = Format(CDbl(v), Precision)
        End If
    End Function

    Public Function InserisciEvento(ByVal MyPar As Oracle.DataAccess.Client.OracleCommand, ByVal vIdFornitore As Long, ByVal vIdOperatore As Long, ByVal Tab_Eventi As Integer, ByRef Motivazione As String) As Boolean

        Try

            InserisciEvento = False

            MyPar.Parameters.Clear()
            If InStr(Motivazione, "Modifica") Then
                MyPar.CommandText = "insert into SISCOM_MI.EVENTI_APPALTI(ID_APPALTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & CType(Me.Page, Object).vIdAppalti & "," & vIdOperatore & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F0" & Tab_Eventi & "','" & par.PulisciStrSql(Motivazione) & "')"
            Else
                MyPar.CommandText = "insert into SISCOM_MI.EVENTI_APPALTI(ID_APPALTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & CType(Me.Page, Object).vIdAppalti & "," & vIdOperatore & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F" & Tab_Eventi & "','" & par.PulisciStrSql(Motivazione) & "')"
            End If
            MyPar.ExecuteNonQuery()
            MyPar.CommandText = ""
            MyPar.Parameters.Clear()


        Catch ex As Exception
            InserisciEvento = False
            MyPar.Parameters.Clear()
        End Try

    End Function

    Protected Sub cmbvoce_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbvoce.SelectedIndexChanged
        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)


            par.cmd.CommandText = "SELECT PF_VOCI.CODICE,PF_VOCI.DESCRIZIONE," _
                                & "SISCOM_MI.PF_VOCI_IMPORTO.IVA_CANONE, SISCOM_MI.PF_VOCI_IMPORTO.IVA_CONSUMO, " _
                                & "tab_servizi_voci.iva_consumo as iva_cons_serv, tab_servizi_voci.iva_canone as iva_can_serv " _
                                & "FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.tab_servizi_voci WHERE SISCOM_MI.PF_VOCI.ID = SISCOM_MI.PF_VOCI_IMPORTO.ID_VOCE AND PF_VOCI_IMPORTO.ID_VOCE_SERVIZIO = TAB_SERVIZI_VOCI.ID(+) AND SISCOM_MI.PF_VOCI_IMPORTO.ID=" & cmbvoce.SelectedValue
            Dim myReaderiva As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderiva.Read Then
                Me.txtivacorpo.Text = par.IfNull(myReaderiva("iva_can_serv"), 0)
                Me.txtivaconsumo.Text = par.IfNull(myReaderiva("iva_cons_serv"), 0)
                Me.DescPF = par.IfNull(myReaderiva("CODICE"), "") & " - " & par.IfNull(myReaderiva("DESCRIZIONE"), "")
            End If
            myReaderiva.Close()

            Me.txtpercanone.Text = par.IfEmpty(percanone.Value, "0,0000")
            Me.txtperconsumo.Text = par.IfEmpty(perconsumo.Value, "0,0000")
            Me.idvoce.Value = Me.cmbvoce.SelectedValue
            Dim script As String = "function f(){$find(""" + RadWindowServizi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Sub somma()
        Try


            Dim sommacanone As Decimal = 0
            Dim sommaconsumo As Decimal = 0

            Dim i As Integer

            For i = 0 To Me.DataGrid3.Items.Count - 1
                Dim dataItem As GridDataItem = CType(DataGrid3.Items(i), GridDataItem)
                dataItem("IMPORTO_CANONE").Text = par.IfEmpty(Replace(dataItem("IMPORTO_CANONE").Text, "&nbsp;", ""), 0)
                sommacanone = sommacanone + dataItem("IMPORTO_CANONE").Text

                dataItem("IMPORTO_CONSUMO").Text = par.IfEmpty(Replace(dataItem("IMPORTO_CONSUMO").Text, "&nbsp;", ""), 0)
                sommaconsumo = sommaconsumo + dataItem("IMPORTO_CONSUMO").Text
            Next

            'ASSEGNO AL CAMPO DI DESTINAZIONE I RISULTATI FORMATTATI
            If sommacanone <> 0 Then
                CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtastacanone"), TextBox).Text = IsNumFormat(sommacanone, "", "##,##0.00")
            End If

            If sommaconsumo <> 0 Then
                CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtastaconsumo"), TextBox).Text = IsNumFormat(sommaconsumo, "", "##,##0.00")
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"

        End Try
    End Sub

    Sub percentuale()
        'Try


        '    'per evitare divisioni per zero in caso togliessi tutti gli importi
        '    If CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtastacanone"), TextBox).Text = "0,00" Then
        '        CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtpercanone"), TextBox).Text = ""
        '        CType(Page.FindControl("Tab_Appalto_generale").FindControl("canone"), HiddenField).Value = ""

        '    End If

        '    If CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtastaconsumo"), TextBox).Text = "0,00" Then
        '        CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtperconsumo"), TextBox).Text = ""
        '        CType(Page.FindControl("Tab_Appalto_generale").FindControl("consumo"), HiddenField).Value = ""

        '    End If

        '    Dim percentualecanone As Decimal = 0
        '    Dim percentualeconsumo As Decimal = 0
        '    'canone
        '    If Val(Replace(Replace((CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtastacanone"), TextBox).Text), ".", ""), ",", ".")) > 0 And Val(Replace(Replace((CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtonericanone"), TextBox).Text), ".", ""), ",", ".")) > 0 Then
        '        percentualecanone = (100 * Val(Replace(Replace((CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtonericanone"), TextBox).Text), ".", ""), ",", "."))) / Val(Replace(Replace((CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtastacanone"), TextBox).Text), ".", ""), ",", "."))
        '        CType(Page.FindControl("Tab_Appalto_generale").FindControl("canone"), HiddenField).Value = percentualecanone
        '        CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtpercanone"), TextBox).Text = percentualecanone
        '    End If
        '    'consumo
        '    If Val(Replace(Replace((CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtastaconsumo"), TextBox).Text), ".", ""), ",", ".")) > 0 And Val(Replace(Replace((CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtonericonsumo"), TextBox).Text), ".", ""), ",", ".")) > 0 Then
        '        percentualeconsumo = (100 * Val(Replace(Replace((CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtonericonsumo"), TextBox).Text), ".", ""), ",", "."))) / Val(Replace(Replace((CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtastaconsumo"), TextBox).Text), ".", ""), ",", "."))
        '        CType(Page.FindControl("Tab_Appalto_generale").FindControl("consumo"), HiddenField).Value = percentualeconsumo
        '        CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtperconsumo"), TextBox).Text = percentualeconsumo
        '    End If
        'Catch ex As Exception
        '    CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
        '    CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_Servizi - percentuale"

        'End Try
    End Sub
    Public Sub CalcolaResiduo()
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)


            If CType(Me.Page, Object).vIdAppalti > 0 Then
                Dim STANZIATO_CONUSMO As Decimal = 0
                Dim STANZIATO_CANONE As Decimal = 0
                Dim RESIDUO_CONSUMO As Decimal = 0
                Dim RESIDUO_CANONE As Decimal = 0

                STANZIATO_CONUSMO = DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpTotPlusOneriCons"), TextBox).Text
                STANZIATO_CANONE = DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpTotPlusOneriCan"), TextBox).Text
                'STANZIATO_CONUSMO = STANZIATO_CONUSMO + par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtVarCons"), TextBox).Text, 0)
                'STANZIATO_CANONE = STANZIATO_CANONE + par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtVarCan"), TextBox).Text, 0)


                ''******************CALCOLO RESIDUO A CONSUMO*************************
                'Dim ReversibilManut As Double = 0
                'par.cmd.CommandText = "SELECT SUM(IMPORTO_TOT) AS TOT_MANUTENZIONI FROM SISCOM_MI.MANUTENZIONI " _
                '                    & "WHERE (STATO = 1 OR STATO = 2 OR STATO = 4)AND MANUTENZIONI.id_appalto in  (" _
                '                    & " select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & "))"


                par.cmd.CommandText = "select sum(nvl(importo_prenotato,0)) as tot  from siscom_mi.prenotazioni where id_appalto in (SELECT ID FROM siscom_mi.APPALTI WHERE id_gruppo=(SELECT id_gruppo FROM siscom_mi.APPALTI WHERE ID= " & CType(Me.Page, Object).vIdAppalti & ")) and id_stato = 0 and tipo_pagamento = 3"
                Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                myreader = par.cmd.ExecuteReader
                'RESIDUO APPALTO
                If myreader.Read Then
                    RESIDUO_CONSUMO = STANZIATO_CONUSMO - par.IfNull(myreader("tot"), 0)
                    'erratoRESIDUO_CANONE = STANZIATO_CANONE - par.IfNull(myreader("TOT_MANUTENZIONI"), 0)
                End If
                myreader.Close()
                par.cmd.CommandText = "select sum(nvl(importo_approvato,0)) as tot  from siscom_mi.prenotazioni where id_appalto in (SELECT ID FROM siscom_mi.APPALTI WHERE id_gruppo=(SELECT id_gruppo FROM siscom_mi.APPALTI WHERE ID=  " & CType(Me.Page, Object).vIdAppalti & ")) and id_stato > 0 and tipo_pagamento = 3"
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    RESIDUO_CONSUMO = RESIDUO_CONSUMO - par.IfNull(myreader("tot"), 0)
                    'erratoRESIDUO_CANONE = STANZIATO_CANONE - par.IfNull(myreader("TOT_MANUTENZIONI"), 0)
                End If
                myreader.Close()



                'par.cmd.CommandText = "SELECT SUM(PAGAMENTI.IMPORTO_CONSUNTIVATO) AS CONSUNTIVATO " _
                '    & "FROM  SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PAGAMENTI,SISCOM_MI.PF_VOCI_IMPORTO " _
                '    & "WHERE " _
                '    & "PRENOTAZIONI.ID_PAGAMENTO = PAGAMENTI.ID " _
                '    & "AND PRENOTAZIONI.ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND " _
                '    & "PRENOTAZIONI.ID_VOCE_PF_IMPORTO = PF_VOCI_IMPORTO.ID"
                'MODIFYED BY EPIFANI

                '***modifica marco 13/06/2016***
                par.cmd.CommandText = "SELECT SUM((SELECT SUM((CASE WHEN IMPORTO_APPROVATO IS NOT NULL THEN NVL(IMPORTO_APPROVATO,0) ELSE NVL(IMPORTO_PRENOTATO,0) END)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO=PAGAMENTI.ID AND PRENOTAZIONI.ID_sTATO<>-3)) /*SUM(PAGAMENTI.IMPORTO_CONSUNTIVATO)*/ AS CONSUNTIVATO " _
                    & " FROM SISCOM_MI.PAGAMENTI WHERE PAGAMENTI.ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & " )) " _
                    & " AND PAGAMENTI.ID_STATO > 0 AND PAGAMENTI.TIPO_PAGAMENTO = 6"
                '***modifica marco 13/06/2016***

                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    'RESIDUO_CONSUMO = STANZIATO_CONUSMO - par.IfNull(myreader("TOT_MANUTENZIONI"), 0)
                    RESIDUO_CANONE = STANZIATO_CANONE - par.IfNull(myreader("CONSUNTIVATO"), 0)
                End If
                myreader.Close()


                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoCanone"), TextBox).Text = Format(RESIDUO_CANONE, "##,##0.00")
                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtresiduoConsumo"), TextBox).Text = Format(RESIDUO_CONSUMO, "##,##0.00")

                ''ESTRAZIONE DELLE QUOTE DI REVERSIBILITA PER TUTTO L'APPALTO A CNONE/CONSUMO
                Dim TOT_REV_CONSUMO As Double = 0
                Dim TOT_REV_CANONE As Double = 0

                par.cmd.CommandText = "select nvl(importo_prenotato,0) as tot_manutenzioni,PF_VOCI_IMPORTO.PERC_REVERSIBILITA  from siscom_mi.prenotazioni,SISCOM_MI.PF_VOCI_IMPORTO where PRENOTAZIONI.ID_VOCE_PF_IMPORTO = PF_VOCI_IMPORTO.ID and id_appalto in (select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & ")) and id_stato = 0 and tipo_pagamento = 3"
                'par.cmd.CommandText = "SELECT IMPORTO_TOT AS TOT_MANUTENZIONI , PERC_REVERSIBILITA FROM SISCOM_MI.MANUTENZIONI, SISCOM_MI.PF_VOCI_IMPORTO WHERE (STATO = 1 OR STATO = 2 OR STATO = 4) AND MANUTENZIONI.ID_PF_VOCE_IMPORTO = PF_VOCI_IMPORTO.ID AND MANUTENZIONI.ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & " )) "
                myreader = par.cmd.ExecuteReader
                'REVERSIBILITà DEL RESIDUO
                Dim ResiduoRevers As Decimal = 0
                While myreader.Read
                    ResiduoRevers = ResiduoRevers + ((par.IfNull(myreader("TOT_MANUTENZIONI"), 0) * par.IfNull(myreader("PERC_REVERSIBILITA"), 0)) / 100)
                End While
                myreader.Close()
                par.cmd.CommandText = "select nvl(importo_approvato,0) as tot_manutenzioni,PF_VOCI_IMPORTO.PERC_REVERSIBILITA  from siscom_mi.prenotazioni,SISCOM_MI.PF_VOCI_IMPORTO where PRENOTAZIONI.ID_VOCE_PF_IMPORTO = PF_VOCI_IMPORTO.ID and id_appalto in (select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & ")) and id_stato > 0  and tipo_pagamento = 3"
                myreader = par.cmd.ExecuteReader
                'REVERSIBILITà DEL RESIDUO
                While myreader.Read
                    ResiduoRevers = ResiduoRevers + ((par.IfNull(myreader("TOT_MANUTENZIONI"), 0) * par.IfNull(myreader("PERC_REVERSIBILITA"), 0)) / 100)
                End While
                myreader.Close()




                Dim ResRevConsumo As Decimal = par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsRevers"), TextBox).Text.Replace(".", ""), 0) - ResiduoRevers


                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoConsRevers"), TextBox).Text = Format(ResRevConsumo, "##,##0.00")
                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoConsNotRevers"), TextBox).Text = Format((RESIDUO_CONSUMO - ResRevConsumo), "##,##0.00")

                ResiduoRevers = 0
                par.cmd.CommandText = "SELECT  (NVL(IMPORTO_APPROVATO ,0)- NVL(RIT_LEGGE_IVATA,0)) AS IMPORTO_CONSUNTIVATO, PF_VOCI_IMPORTO.PERC_REVERSIBILITA " _
                                    & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI_IMPORTO " _
                                    & "WHERE " _
                                    & " PRENOTAZIONI.ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & " ))  AND " _
                                    & "PRENOTAZIONI.ID_VOCE_PF_IMPORTO = PF_VOCI_IMPORTO.ID " _
                                    & " AND ID_STATO >= 2 AND TIPO_PAGAMENTO = 6"
                myreader = par.cmd.ExecuteReader

                While myreader.Read
                    ResiduoRevers = ResiduoRevers + ((par.IfNull(myreader("IMPORTO_CONSUNTIVATO"), 0) * par.IfNull(myreader("PERC_REVERSIBILITA"), 0)) / 100)
                End While
                Dim ResRevCanone As Decimal = par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneRevers"), TextBox).Text.Replace(".", ""), 0) - ResiduoRevers


                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoRevers"), TextBox).Text = Format(ResRevCanone, "##,##0.00")
                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoNotRevers"), TextBox).Text = Format((RESIDUO_CANONE - ResRevCanone), "##,##0.00")

            Else
                Dim resCanone As Decimal = 0
                Dim resConsumo As Decimal = 0

                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoRevers"), TextBox).Text = Format(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneRevers"), TextBox).Text, 0), )
                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoNotRevers"), TextBox).Text = Format(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneNotRevers"), TextBox).Text, 0), )

                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoConsRevers"), TextBox).Text = Format(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsRevers"), TextBox).Text, 0), )
                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoConsNotRevers"), TextBox).Text = Format(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsNotRevers"), TextBox).Text, 0), )


                resCanone = CDec(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneRevers"), TextBox).Text.Replace(".", ""), 0)) + CDec(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneNotRevers"), TextBox).Text.Replace(".", ""), 0))
                resConsumo = CDec(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsRevers"), TextBox).Text.Replace(".", ""), 0)) + CDec(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsNotRevers"), TextBox).Text.Replace(".", ""), 0))

                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtresiduoConsumo"), TextBox).Text = Format(resConsumo, "##,##0.00")
                DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoCanone"), TextBox).Text = Format(resCanone, "##,##0.00")

            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Session.Add("ERRORE", "CalcolaResiduo:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    'Public Sub CalcolaResiduo()
    '    Try

    '        '*******************RICHIAMO LA CONNESSIONE*********************
    '        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '        par.SettaCommand(par)

    '        '*******************RICHIAMO LA TRANSAZIONE*********************
    '        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
    '        ‘‘par.cmd.Transaction = par.myTrans

    '        If CType(Me.Page, Object).vIdAppalti > 0 Then
    '            Dim STANZIATO_CONUSMO As Decimal = 0
    '            Dim STANZIATO_CANONE As Decimal = 0
    '            Dim RESIDUO_CONSUMO As Decimal = 0
    '            Dim RESIDUO_CANONE As Decimal = 0

    '            STANZIATO_CONUSMO = DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpTotPlusOneriCons"), TextBox).Text
    '            STANZIATO_CANONE = DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpTotPlusOneriCan"), TextBox).Text

    '            ''******************CALCOLO RESIDUO A CONSUMO*************************
    '            'Dim ReversibilManut As Double = 0
    '            par.cmd.CommandText = "SELECT SUM(IMPORTO_TOT) AS TOT_MANUTENZIONI FROM SISCOM_MI.MANUTENZIONI " _
    '                                & "WHERE (STATO = 1 OR STATO = 2 OR STATO = 4)AND MANUTENZIONI.id_appalto in  (" _
    '                                & " select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & "))"
    '            Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
    '            myreader = par.cmd.ExecuteReader
    '            'RESIDUO APPALTO
    '            If myreader.Read Then
    '                RESIDUO_CONSUMO = STANZIATO_CONUSMO - par.IfNull(myreader("TOT_MANUTENZIONI"), 0)
    '                'erratoRESIDUO_CANONE = STANZIATO_CANONE - par.IfNull(myreader("TOT_MANUTENZIONI"), 0)
    '            End If
    '            myreader.Close()

    '            'par.cmd.CommandText = "SELECT SUM(PAGAMENTI.IMPORTO_CONSUNTIVATO) AS CONSUNTIVATO " _
    '            '    & "FROM  SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PAGAMENTI,SISCOM_MI.PF_VOCI_IMPORTO " _
    '            '    & "WHERE " _
    '            '    & "PRENOTAZIONI.ID_PAGAMENTO = PAGAMENTI.ID " _
    '            '    & "AND PRENOTAZIONI.ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " AND " _
    '            '    & "PRENOTAZIONI.ID_VOCE_PF_IMPORTO = PF_VOCI_IMPORTO.ID"
    '            'MODIFYED BY EPIFANI
    '            par.cmd.CommandText = "SELECT SUM(PAGAMENTI.IMPORTO_CONSUNTIVATO) AS CONSUNTIVATO " _
    '                & " FROM SISCOM_MI.PAGAMENTI WHERE PAGAMENTI.ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & " )) " _
    '                & " AND PAGAMENTI.ID_STATO > 0 AND PAGAMENTI.TIPO_PAGAMENTO = 6"


    '            myreader = par.cmd.ExecuteReader
    '            If myreader.Read Then
    '                'RESIDUO_CONSUMO = STANZIATO_CONUSMO - par.IfNull(myreader("TOT_MANUTENZIONI"), 0)
    '                RESIDUO_CANONE = STANZIATO_CANONE - par.IfNull(myreader("CONSUNTIVATO"), 0)
    '            End If
    '            myreader.Close()


    '            DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoCanone"), TextBox).Text = Format(RESIDUO_CANONE, "##,##0.00")
    '            DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtresiduoConsumo"), TextBox).Text = Format(RESIDUO_CONSUMO, "##,##0.00")

    '            ''ESTRAZIONE DELLE QUOTE DI REVERSIBILITA PER TUTTO L'APPALTO A CNONE/CONSUMO
    '            Dim TOT_REV_CONSUMO As Double = 0
    '            Dim TOT_REV_CANONE As Double = 0
    '            par.cmd.CommandText = "SELECT IMPORTO_TOT AS TOT_MANUTENZIONI , PERC_REVERSIBILITA FROM SISCOM_MI.MANUTENZIONI, SISCOM_MI.PF_VOCI_IMPORTO WHERE (STATO = 1 OR STATO = 2 OR STATO = 4) AND MANUTENZIONI.ID_PF_VOCE_IMPORTO = PF_VOCI_IMPORTO.ID AND MANUTENZIONI.ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & " )) "
    '            myreader = par.cmd.ExecuteReader

    '            'REVERSIBILITà DEL RESIDUO
    '            Dim ResiduoRevers As Decimal = 0

    '            While myreader.Read
    '                ResiduoRevers = ResiduoRevers + ((par.IfNull(myreader("TOT_MANUTENZIONI"), 0) * par.IfNull(myreader("PERC_REVERSIBILITA"), 0)) / 100)
    '            End While
    '            myreader.Close()

    '            Dim ResRevConsumo As Decimal = par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsRevers"), TextBox).Text.Replace(".", ""), 0) - ResiduoRevers


    '            DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoConsRevers"), TextBox).Text = Format(ResRevConsumo, "##,##0.00")
    '            DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoConsNotRevers"), TextBox).Text = Format((RESIDUO_CONSUMO - ResRevConsumo), "##,##0.00")

    '            ResiduoRevers = 0
    '            par.cmd.CommandText = "SELECT DISTINCT PAGAMENTI.IMPORTO_CONSUNTIVATO,PF_VOCI_IMPORTO.PERC_REVERSIBILITA " _
    '                                & "FROM  SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PAGAMENTI,SISCOM_MI.PF_VOCI_IMPORTO " _
    '                                & "WHERE " _
    '                                & "PRENOTAZIONI.ID_PAGAMENTO = PAGAMENTI.ID " _
    '                                & "AND PRENOTAZIONI.ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & CType(Me.Page, Object).vIdAppalti & " ))  AND " _
    '                                & "PRENOTAZIONI.ID_VOCE_PF_IMPORTO = PF_VOCI_IMPORTO.ID " _
    '                                & "AND PAGAMENTI.ID_STATO > 0 AND PAGAMENTI.TIPO_PAGAMENTO = 6"
    '            myreader = par.cmd.ExecuteReader

    '            While myreader.Read
    '                ResiduoRevers = ResiduoRevers + ((par.IfNull(myreader("IMPORTO_CONSUNTIVATO"), 0) * par.IfNull(myreader("PERC_REVERSIBILITA"), 0)) / 100)
    '            End While
    '            Dim ResRevCanone As Decimal = par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneRevers"), TextBox).Text.Replace(".", ""), 0) - ResiduoRevers


    '            DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoRevers"), TextBox).Text = Format(ResRevCanone, "##,##0.00")
    '            DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoNotRevers"), TextBox).Text = Format((RESIDUO_CANONE - ResRevCanone), "##,##0.00")

    '        Else
    '            Dim resCanone As Decimal = 0
    '            Dim resConsumo As Decimal = 0

    '            DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoRevers"), TextBox).Text = Format(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneRevers"), TextBox).Text, 0), )
    '            DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoNotRevers"), TextBox).Text = Format(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneNotRevers"), TextBox).Text, 0), )

    '            DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoConsRevers"), TextBox).Text = Format(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsRevers"), TextBox).Text, 0), )
    '            DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoConsNotRevers"), TextBox).Text = Format(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsNotRevers"), TextBox).Text, 0), )


    '            resCanone = CDec(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneRevers"), TextBox).Text.Replace(".", ""), 0)) + CDec(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpCanoneNotRevers"), TextBox).Text.Replace(".", ""), 0))
    '            resConsumo = CDec(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsRevers"), TextBox).Text.Replace(".", ""), 0)) + CDec(par.IfEmpty(DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtImpConsNotRevers"), TextBox).Text.Replace(".", ""), 0))

    '            DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtresiduoConsumo"), TextBox).Text = Format(resConsumo, "##,##0.00")
    '            DirectCast(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtResiduoCanone"), TextBox).Text = Format(resCanone, "##,##0.00")

    '        End If

    '    Catch ex As Exception
    '        par.myTrans.Rollback()
    '        par.OracleConn.Close()
    '        par.OracleConn.Dispose()
    '        Session.Add("ERRORE", "CalcolaResiduo:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '    End Try
    'End Sub
    Private Sub impostaModalitaPagamento()
        'Try
        '    Dim idTipoModalitaPagamento As String = ""
        '    Dim idTipoPagamento As String = ""
        '    For Each elemento As DataGridItem In DataGridFornitori.Items
        '        If elemento.Cells(par.IndDGC(DataGridFornitori, "CAPOFILA")).Text = "C" Or DataGridFornitori.Items.Count = 1 Then
        '            If elemento.Cells(par.IndDGC(DataGridFornitori, "ID_TIPO_MODALITA_PAG")).Text.Replace("&nbsp;", "") = "" Then
        '                CType(CType(Me.Page, Object).FindControl("cmbModalitaPagamento"), DropDownList).SelectedValue = "-1"
        '            Else
        '                CType(CType(Me.Page, Object).FindControl("cmbModalitaPagamento"), DropDownList).SelectedValue = elemento.Cells(par.IndDGC(DataGridFornitori, "ID_TIPO_MODALITA_PAG")).Text
        '            End If
        '            If elemento.Cells(par.IndDGC(DataGridFornitori, "ID_TIPO_PAGAMENTO")).Text.Replace("&nbsp;", "") = "" Then
        '                CType(CType(Me.Page, Object).FindControl("cmbCondizionePagamento"), DropDownList).SelectedValue = "-1"
        '            Else
        '                CType(CType(Me.Page, Object).FindControl("cmbCondizionePagamento"), DropDownList).SelectedValue = elemento.Cells(par.IndDGC(DataGridFornitori, "ID_TIPO_PAGAMENTO")).Text
        '            End If
        '        End If
        '    Next
        'Catch ex As Exception

        'End Try
    End Sub

    Protected Sub btn_InserisciAppalti_Click(sender As Object, e As System.EventArgs) Handles btn_InserisciAppalti.Click

        If ControlloCampiAppalti() = False Then
            txtAppareP.Value = "1"
            'Dim script As String = "function f(){$find(""" + RadWindowServizi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            'RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            Exit Sub
        End If

        '*************PEPPE MODIFY 24/02/2015 ore 15:50
        If par.IfEmpty(Me.txtIDS.Value, "-1") = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            If ControlImporti() = False Then
                'Response.Write("<script>alert('Attenzione!Verificare gli importi delle scadenze manuali!Impossibile procedere')</script>")
                txtAppareP.Value = "1"
                Exit Sub
            End If

            Me.SalvaServizi()
            AggiornaVoceScadenza()
            txtAppareP.Value = "1"
            '*******cancello i campi
            'Me.cmbservizio.SelectedValue = "-1"
            AggiornaVociServizi()
            'Me.cmbvoce.SelectedValue = "-1"
            Me.txtimportocorpo.Text = ""
            Me.txtimportoconsumo.Text = ""
            Me.txtscontoconsumo.Text = ""
            Me.txtscontocorpo.Text = ""
            Me.txtivaconsumo.Text = ""
            Me.txtivacorpo.Text = ""
            Me.txtOnerCanone.Text = ""
            Me.txtOneriConsumo.Text = ""
            Me.percanone.Value = ""
            Me.perconsumo.Value = ""
            Me.cmbFreqPagamento.SelectedValue = 0

        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            If ControlImporti() = False Then
                'Response.Write("<script>alert('Attenzione!Verificare gli importi delle scadenze manuali!Impossibile procedere')</script>")
                txtAppareP.Value = "1"
                Exit Sub
            End If

            Me.UpdateAppalti()
            AggiornaVoceScadenza()

            AggiornaVociServizi()
            txtAppareP.Value = "0"
            Me.txtIDS.Value = ""
            '*******cancello i campi
            Me.cmbservizio.SelectedValue = "-1"
            'Me.cmbvoce.SelectedValue = "-1"
            Me.txtimportocorpo.Text = ""
            Me.txtimportoconsumo.Text = ""
            Me.txtscontoconsumo.Text = ""
            Me.txtscontocorpo.Text = ""
            Me.txtivaconsumo.Text = ""
            Me.txtivacorpo.Text = ""
            Me.txtOnerCanone.Text = ""
            Me.txtOneriConsumo.Text = ""
            Me.percanone.Value = ""
            Me.perconsumo.Value = ""
            Me.txtpercanone.Text = ""
            Me.txtperconsumo.Text = ""
            Me.cmbFreqPagamento.SelectedValue = 0
        End If

        CalcolaImpContrattuale()
        idvoce.Value = 0
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        txtSelAppalti.Text = ""
        txtIdComponente.Value = ""
        txtIdComponente1.Value = ""
        CalcolaResiduo()
    End Sub

    Protected Sub btn_ChiudiAppalti_Click(sender As Object, e As System.EventArgs) Handles btn_ChiudiAppalti.Click
        Try


            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"

            Me.txtIDS.Value = ""
            txtSelAppalti.Text = ""
            txtIdComponente.Value = ""
            txtIdComponente1.Value = ""
            '*******cancello i campi
            Me.cmbservizio.SelectedValue = "-1"
            Me.cmbvoce.ClearSelection()
            AggiornaVociServizi()

            Me.txtimportocorpo.Text = ""
            Me.txtimportoconsumo.Text = ""
            Me.txtscontoconsumo.Text = ""
            Me.txtscontocorpo.Text = ""
            Me.txtivaconsumo.Text = ""
            Me.txtivacorpo.Text = ""
            Me.txtOnerCanone.Text = ""
            Me.txtOneriConsumo.Text = ""
            Me.percanone.Value = ""
            Me.perconsumo.Value = ""
            Me.txtpercanone.Text = ""
            Me.txtperconsumo.Text = ""
            Me.cmbFreqPagamento.SelectedValue = 0

            'CalcolaImpContrattuale()
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
        End Try
    End Sub

    Protected Sub DataGrid3_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGrid3.ItemCommand
        Try
            Select Case e.CommandName
                Case "myEdit"
                    ApriServizio()
                Case "Delete"
                    Dim sottraicanone As Decimal
                    Dim sottraiconsumo As Decimal
                    If CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtastacanone"), TextBox).Text <> "" Then
                        sottraicanone = CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtastacanone"), TextBox).Text
                    End If

                    If CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtastaconsumo"), TextBox).Text <> "" Then
                        sottraiconsumo = CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtastaconsumo"), TextBox).Text
                    End If

                    If txtIdComponente.Value = "" Then
                        'Response.Write("<script>alert('Nessuna riga selezionata!')</script>") messaggio visibile in confermaannulloappalti del file .aspx
                        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
                        txtAppareP.Value = "0"
                    Else
                        If CType(Me.Page, Object).vIdAppalti = 0 Then
                            '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA APPALTO
                            sottraicanone = sottraicanone - Val((Replace(Replace(lstservizi(txtIdComponente0.Value).IMPORTO_CANONE, ".", ""), ",", ".")))
                            sottraiconsumo = sottraiconsumo - Val((Replace(Replace(lstservizi(txtIdComponente0.Value).IMPORTO_CONSUMO, ".", ""), ",", ".")))
                            lstservizi.RemoveAt(txtIdComponente0.Value)
                            Dim indice As Integer = 0
                            For Each griglia As Mario.VociServizi In lstservizi
                                griglia.ID = indice
                                indice += 1
                            Next
                            DataGrid3.DataSource = lstservizi
                            DataGrid3.DataBind()
                        Else
                            '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA FORNITORE
                            If par.OracleConn.State = Data.ConnectionState.Open Then
                                Response.Write("IMPOSSIBILE VISUALIZZARE")
                                Exit Sub
                            Else
                                '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                                par.SettaCommand(par)
                                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                                'controllo che gli importi a consumo non siano legati a delle manutenzioni
                                par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI " _
                                                    & " where ID_SERVIZIO=" & Me.txtIdComponente1.Value _
                                                    & "   and ID_PF_VOCE_IMPORTO in (  SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                                    & "CONNECT BY PRIOR PF_VOCI_IMPORTO.ID=PF_VOCI_IMPORTO.id_old " _
                                                    & "START WITH PF_VOCI_IMPORTO.ID=" & Me.txtIdComponente.Value & ")" _
                                                    & "   and ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo = " & CType(Me.Page, Object).vIdAppalti & ") " _
                                                    & " and manutenzioni.stato <=4"

                                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReader1.Read Then
                                    'Response.Write("<script>alert('Impossibile eliminare la voce perchè il servizio è già legato ad una manutenzione!');</script>")
                                    RadWindowManager1.RadAlert("Impossibile eliminare la voce perchè il servizio è già legato ad una manutenzione!", 300, 150, "Attenzione", "", "null")
                                    myReader1.Close()
                                    Exit Sub
                                End If
                                myReader1.Close()
                                'controllo che nessuna prenotazione a canone sia stata pagata
                                'AT
                                par.cmd.CommandText = "SELECT * FROM siscom_mi.PRENOTAZIONI WHERE tipo_pagamento = 6 and id_appalto IN(SELECT ID FROM siscom_mi.APPALTI WHERE id_gruppo =  " & CType(Me.Page, Object).vIdAppalti & ") " _
                                                    & "AND id_voce_pf_importo IN(  SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                                    & "CONNECT BY PRIOR PF_VOCI_IMPORTO.ID=PF_VOCI_IMPORTO.id_old " _
                                                    & "START WITH PF_VOCI_IMPORTO.ID=" & Me.txtIdComponente.Value & ") and id_pagamento is not null"
                                myReader1 = par.cmd.ExecuteReader
                                If myReader1.Read Then
                                    'Response.Write("<script>alert('Impossibile eliminare la voce perchè alcune prenotazioni a canone sono state pagate!');</script>")
                                    RadWindowManager1.RadAlert("Impossibile eliminare la voce perchè alcune prenotazioni a canone sono state pagate!", 300, 150, "Attenzione", "", "null")
                                    myReader1.Close()
                                    Exit Sub
                                End If
                                myReader1.Close()
                                'TROVA IMPORTO CORRISPONDENTE
                                par.cmd.CommandText = "select SISCOM_MI.APPALTI_LOTTI_SERVIZI.IMPORTO_CANONE, SISCOM_MI.APPALTI_LOTTI_SERVIZI.IMPORTO_CONSUMO from SISCOM_MI.APPALTI_LOTTI_SERVIZI where SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & " and SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & txtIdComponente.Value
                                Dim myReadersomma As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReadersomma.Read Then
                                    sottraicanone = sottraicanone - Val(par.VirgoleInPunti(myReadersomma("IMPORTO_CANONE")))
                                    sottraiconsumo = sottraiconsumo - Val(par.VirgoleInPunti(myReadersomma("IMPORTO_CONSUMO")))
                                End If
                                myReadersomma.Close()

                                'ELIMINA voce da appalti lotti servizi su tutti gli appalti anche quelli nati negli anni successivi se pluriennali 
                                par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE " _
                                                    & "ID_APPALTO IN (SELECT ID FROM siscom_mi.APPALTI WHERE id_gruppo =  " & CType(Me.Page, Object).vIdAppalti & ") " _
                                                    & "and ID_PF_VOCE_IMPORTO in (  SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                                    & "CONNECT BY PRIOR PF_VOCI_IMPORTO.ID=PF_VOCI_IMPORTO.id_old " _
                                                    & "START WITH PF_VOCI_IMPORTO.ID=" & Me.txtIdComponente.Value & ")"

                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                                par.cmd.Parameters.Clear()
                                DataGrid3.Rebind()
                                '*** EVENTI_FORNITORI
                                InserisciEvento(par.cmd, CType(Me.Page, Object).vIdAppalti, Session.Item("ID_OPERATORE"), 56, "Elimina voce servizio da appalto")
                            End If
                        End If
                        txtSelAppalti.Text = ""
                        txtIdComponente.Value = ""
                        txtIdComponente1.Value = ""
                    End If
                    CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtastacanone"), TextBox).Text = IsNumFormat(sottraicanone, "", "##,##0.00")
                    CType(Page.FindControl("Tab_Appalto_generale").FindControl("txtastaconsumo"), TextBox).Text = IsNumFormat(sottraiconsumo, "", "##,##0.00")
                    CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

                    'CALCOLA SOMMA
                    somma()
                    'CALCOLA PERCENTUALE
                    percentuale()
                    'Aggiornamento importo Contrattuale
                    CalcolaImpContrattuale()
            End Select
        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
        End Try
    End Sub

    Protected Sub DataGrid3_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid3.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('Tab_Servizio_txtIdComponente').value='" & dataItem("ID_PF_VOCE_IMPORTO").Text & "';document.getElementById('Tab_Servizio_txtIdComponente0').value='" & dataItem("ID").Text & "';document.getElementById('Tab_Servizio_txtIdComponente1').value='" & dataItem("ID_SERVIZIO").Text & "';")
            e.Item.Attributes.Add("onDblClick", "document.getElementById('Tab_Servizio_btnApriServizioAppalto').click();")
            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Or DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "ATTIVO" Then
                dataItem("DeleteColumn").Enabled = False
                dataItem("modificaServizio").Enabled = False
                DataGrid3.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
            End If
        End If
    End Sub

    Protected Sub DataGrid3_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid3.NeedDataSource
        Dim StringaSql As String

        Try
            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ' I NOMI DEI CAMPI DEVONO CORRISPONDERE CON QUELLI NELLA DATAGRID
            StringaSql = " select rownum as ""ID"",SISCOM_MI.PF_VOCI_IMPORTO.ID_LOTTO," _
                             & " SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO,TAB_SERVIZI.DESCRIZIONE AS SERVIZIO,SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO," _
                             & " SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE,TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IMPORTO_CANONE,'9G999G999G999G999G990D99')) AS ""IMPORTO_CANONE""," _
                             & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.SCONTO_CANONE,'9G999G999G999G999G990D999'))AS ""SCONTO_CANONE"",TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IVA_CANONE,'999G990D99')) AS IVA_CANONE," _
                             & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IMPORTO_CONSUMO,'9G999G999G999G999G990D99')) AS ""IMPORTO_CONSUMO""," _
                             & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.SCONTO_CONSUMO,'9G999G999G999G999G990D999'))AS ""SCONTO_CONSUMO"",TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IVA_CONSUMO,'999G990D99')) AS IVA_CONSUMO,(PF_VOCI.CODICE ||' '|| PF_VOCI.DESCRIZIONE) AS DESC_PF ," _
                             & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.ONERI_SICUREZZA_CANONE,'9G999G999G999G999G990D99'))AS ""ONERI_SICUREZZA_CANONE"",TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.ONERI_SICUREZZA_CONSUMO,'9G999G999G999G999G990D99'))AS ""ONERI_SICUREZZA_CONSUMO""" _
                             & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.TAB_SERVIZI ,SISCOM_MI.PF_VOCI" _
                             & " where SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID " _
                             & "   and SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & " AND SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO = TAB_SERVIZI.ID  AND PF_VOCI_IMPORTO.ID_VOCE = PF_VOCI.ID"
            par.cmd.CommandText = StringaSql
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "APPALTI_LOTTI_SERVIZI")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            'DataGrid3.DataSource = ds
            'DataGrid3.DataBind()
            ds.Dispose()
            AggiornaVociServizi()
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
        End Try
    End Sub

    Private Sub ApriServizio()
        Try
            If txtIdComponente.Value = "" Then
                'Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
                txtAppareP.Value = "0"

            Else

                Me.cmbservizio.Enabled = True
                Me.cmbvoce.Enabled = True

                Me.txtimportoconsumo.Enabled = True
                Me.txtscontoconsumo.Enabled = True

                If CType(Me.Page, Object).vIdAppalti = 0 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA APPALTO
                    Me.txtIDS.Value = lstservizi(txtIdComponente0.Value).ID_PF_VOCE_IMPORTO
                    Me.cmbservizio.SelectedValue = lstservizi(txtIdComponente0.Value).ID_SERVIZIO

                    ' RIPRENDO LA CONNESSIONE
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)

                    'RIPRENDO LA TRANSAZIONE
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)


                    cmbvoce.Items.Clear()

                    'par.cmd.CommandText = "select SISCOM_MI.PF_VOCI_IMPORTO.ID, SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE  FROM SISCOM_MI.PF_VOCI_IMPORTO where SISCOM_MI.PF_VOCI_IMPORTO.ID not in (select SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI where SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_LOTTO=" & idLotti & " and SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_SERVIZIO=" & Me.cmbservizio.SelectedValue & ") and SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO=" & Me.cmbservizio.SelectedValue
                    par.cmd.CommandText = "select SISCOM_MI.PF_VOCI_IMPORTO.ID, SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE  FROM SISCOM_MI.PF_VOCI_IMPORTO where SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO= " & Me.cmbservizio.SelectedValue & " and id_lotto = " & idLotti
                    par.caricaComboTelerik(par.cmd.CommandText, cmbvoce, "ID", "DESCRIZIONE", True)


                    Me.cmbvoce.SelectedValue = lstservizi(txtIdComponente0.Value).ID_PF_VOCE_IMPORTO
                    Me.txtimportocorpo.Text = IsNumFormatClasse(par.IfEmpty(lstservizi(txtIdComponente0.Value).IMPORTO_CANONE, 0), "", "##,##0.00")
                    Me.txtscontocorpo.Text = IsNumFormatClasse(par.IfEmpty(lstservizi(txtIdComponente0.Value).SCONTO_CANONE, 0), "", "##,##0.000") 'PAR.IfNull(lstservizi(txtIdComponente0.Text).SCONTO_CORPO, "")
                    Me.txtivacorpo.Text = par.IfNull(lstservizi(txtIdComponente0.Value).IVA_CANONE, "")
                    Me.txtimportoconsumo.Text = IsNumFormatClasse(par.IfEmpty(lstservizi(txtIdComponente0.Value).IMPORTO_CONSUMO, 0), "", "##,##0.00")
                    Me.txtscontoconsumo.Text = IsNumFormatClasse(par.IfEmpty(lstservizi(txtIdComponente0.Value).SCONTO_CONSUMO, 0), "", "##,##0.000") 'PAR.IfNull(lstservizi(txtIdComponente0.value).SCONTO_CONSUMO, "")
                    Me.txtivaconsumo.Text = par.IfNull(lstservizi(txtIdComponente0.Value).IVA_CONSUMO, "")
                    controllaservizio.Value = Me.cmbservizio.SelectedValue ' mi servirà per il controllo quando cambio servizio e dovrò visualizzare le voci corrispondenti
                    Me.cmbFreqPagamento.SelectedValue = lstservizi(txtIdComponente0.Value).FREQUENZA_CANONE
                    Me.txtOnerCanone.Text = IsNumFormatClasse(par.IfEmpty(lstservizi(txtIdComponente0.Value).ONERI_SICUREZZA_CANONE, 0), "", "##,##0.00")
                    Me.txtOneriConsumo.Text = IsNumFormatClasse(par.IfEmpty(lstservizi(txtIdComponente0.Value).ONERI_SICUREZZA_CONSUMO, 0), "", "##,##0.00")
                    Me.idvoce.Value = Me.cmbvoce.SelectedValue

                    If Me.txtimportoconsumo.Text > 0 Then
                        Me.txtperconsumo.Text = Format((txtOneriConsumo.Text.Replace(".", "") / txtimportoconsumo.Text.Replace(".", "")) * 100, "##,##0.0000")

                        perconsumo.Value = Me.txtperconsumo.Text
                    Else
                        txtperconsumo.Text = 0
                        perconsumo.Value = ""
                    End If

                    If Me.txtimportocorpo.Text > 0 Then
                        Me.txtpercanone.Text = Format((txtOnerCanone.Text.Replace(".", "") / txtimportocorpo.Text.Replace(".", "")) * 100, "##,##0.0000")
                        percanone.Value = Me.txtpercanone.Text
                    Else
                        txtpercanone.Text = 0
                        percanone.Value = ""
                    End If

                Else
                    '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA APPALTO 

                    'If par.OracleConn.State = Data.ConnectionState.Open Then
                    '    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    '    Exit Sub
                    'Else
                    '    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    '    par.SettaCommand(par)
                    '    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                    'End If



                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader


                    'LEGGO SERVIZIO

                    'PAR.cmd.CommandText = "SELECT SISCOM_MI.PF_VOCI_IMPORTO.ID, SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE " _
                    '& "FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE SISCOM_MI.PF_VOCI_IMPORTO.ID NOT IN " _
                    '& "(SELECT SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    '& "WHERE SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & ") OR SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO=" & txtIdComponente.Text

                    'cmbvoce.Items.Add(New ListItem(" ", -1))
                    'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    'While myReader2.Read
                    '    cmbvoce.Items.Add(New ListItem(PAR.IfNull(myReader2("DESCRIZIONE"), " "), PAR.IfNull(myReader2("ID"), -1)))
                    'End While
                    'myReader2.Close()

                    'cmbvoce.SelectedValue = -1


                    Dim Str1 As String

                    'CARICO LOTTI APPALTATI CON RELATIVO SERVIZIO

                    Str1 = " select APPALTI_LOTTI_SERVIZI.* , pf_voci_importo.id_servizio FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI,siscom_mi.pf_voci_importo  where pf_voci_importo.id = appalti_lotti_servizi.id_pf_voce_importo and SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & " and SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO='" & par.PulisciStrSql(txtIdComponente.Value) & "'"

                    par.cmd.CommandText = Str1

                    myReader1 = par.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.txtIDS.Value = par.IfNull(myReader1("ID_PF_VOCE_IMPORTO"), -1)
                        Me.txtimportocorpo.Text = IsNumFormat(par.IfNull(myReader1("IMPORTO_CANONE"), 0), "", "##,##0.00")
                        Me.txtscontocorpo.Text = IsNumFormat(par.IfNull(myReader1("SCONTO_CANONE"), 0), "", "0.000") 'PAR.IfNull(myReader1("SCONTO_CORPO"), "")


                        '******************IVA 21********************************
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA=(SELECT ID_ALIQUOTA FROM SISCOM_MI.IVA WHERE VALORE=" & par.IfNull(myReader1("IVA_CANONE"), 0) & ")"
                        Dim fasciaIVA = 0
                        Dim LettoreIVa As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If LettoreIVa.Read Then
                            fasciaIVA = par.IfNull(LettoreIVa("valore"), 0)
                        End If
                        LettoreIVa.Close()
                        Me.txtivacorpo.Text = fasciaIVA


                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA=(SELECT ID_ALIQUOTA FROM SISCOM_MI.IVA WHERE VALORE=" & par.IfNull(myReader1("IVA_CONSUMO"), 0) & ")"
                        Dim fasciaIVACONSUMO = 0
                        Dim LettoreIVaCONSUMO As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If LettoreIVaCONSUMO.Read Then
                            fasciaIVACONSUMO = par.IfNull(LettoreIVaCONSUMO("valore"), 0)
                        End If
                        LettoreIVaCONSUMO.Close()
                        Me.txtivaconsumo.Text = fasciaIVACONSUMO



                        Me.txtimportoconsumo.Text = IsNumFormat(par.IfNull(myReader1("IMPORTO_CONSUMO"), 0), "", "##,##0.00")
                        Me.txtscontoconsumo.Text = IsNumFormat(par.IfNull(myReader1("SCONTO_CONSUMO"), 0), "", "0.000") 'PAR.IfNull(myReader1("SCONTO_CONSUMO"), "")

                        Me.cmbFreqPagamento.SelectedValue = par.IfNull(myReader1("FREQUENZA_PAGAMENTO"), 0)
                        '  Me.cmbvoce.SelectedValue = PAR.IfNull(myReader1("ID_VOCE_SERVIZIO"), -1)
                        Me.cmbservizio.SelectedValue = par.IfNull(myReader1("ID_SERVIZIO"), -1)
                        controllaservizio.Value = Me.cmbservizio.SelectedValue ' mi servirà per il controllo quando cambio servizio e dovrò visualizzare le voci corrispondenti

                        Me.txtOnerCanone.Text = IsNumFormat(par.IfNull(myReader1("ONERI_SICUREZZA_CANONE"), 0), "", "##,##0.00")
                        Me.txtOneriConsumo.Text = IsNumFormat(par.IfNull(myReader1("ONERI_SICUREZZA_CONSUMO"), 0), "", "##,##0.00")

                        Me.txtpercanone.Text = IsNumFormat(par.IfNull(myReader1("PERC_ONERI_SIC_CAN"), 0), "", "##,##0.0000")
                        Me.txtperconsumo.Text = IsNumFormat(par.IfNull(myReader1("PERC_ONERI_SIC_CON"), 0), "", "##,##0.0000")
                        Me.percanone.Value = Me.txtpercanone.Text
                        Me.perconsumo.Value = txtperconsumo.Text
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = ""

                    'par.cmd.CommandText = "select SISCOM_MI.PF_VOCI_IMPORTO.ID, SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE  FROM SISCOM_MI.PF_VOCI_IMPORTO where SISCOM_MI.PF_VOCI_IMPORTO.ID not in " _
                    '& "(select SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI where SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_LOTTO=" & idLotti & " and SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_SERVIZIO=" & Me.cmbservizio.SelectedValue & ") and SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO=" & Me.cmbservizio.SelectedValue & " or SISCOM_MI.PF_VOCI_IMPORTO.ID=" & txtIdComponente.Value

                    par.cmd.CommandText = "SELECT SISCOM_MI.PF_VOCI_IMPORTO.ID, SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE  " _
                                        & "FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                        & "WHERE SISCOM_MI.PF_VOCI_IMPORTO.ID NOT IN (SELECT SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                        & "FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE id_appalto =" & CType(Me.Page, Object).vIdAppalti & " ) " _
                                        & "AND SISCOM_MI.PF_VOCI_IMPORTO.ID_SERVIZIO = " & Me.cmbservizio.SelectedValue & " AND id_lotto = " & idLotti & " OR SISCOM_MI.PF_VOCI_IMPORTO.ID= " & txtIdComponente.Value
                    cmbvoce.Items.Clear()
                    par.caricaComboTelerik(par.cmd.CommandText, cmbvoce, "ID", "DESCRIZIONE", True)

                    ' cmbvoce.SelectedValue = -1

                    par.cmd.CommandText = ""

                    par.cmd.CommandText = "select *  FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI where SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & " and SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & txtIdComponente.Value
                    myReader1 = par.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.cmbvoce.SelectedValue = par.IfNull(myReader1("ID_PF_VOCE_IMPORTO"), -1)
                        Me.idvoce.Value = Me.cmbvoce.SelectedValue
                    End If
                    myReader1.Close()


                    'DOVREBBE ESSERE ID_PF_VOCI_IMPORTI DEVO ASPETTARE CHE EPIFANI RINOMINI LA COLONNA SUL DB E TUTTE LE OCCORRENZE NEL PROGETTO
                    par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI " _
                                       & " where ID_SERVIZIO=" & Me.txtIdComponente1.Value _
                                       & "   and ID_PF_VOCE_IMPORTO=" & Me.txtIdComponente.Value _
                                       & "   and ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti _
                                       & "   and stato <=4"

                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then

                        Me.cmbservizio.Enabled = False
                        Me.cmbvoce.Enabled = False

                        Me.txtimportoconsumo.Enabled = False
                        Me.txtscontoconsumo.Enabled = False
                        'Response.Write("<script>alert('Importo a consumo bloccato perchè trovati \nordini sulla voce di servizio!')</script>")
                        RadWindowManager1.RadAlert("Importo a consumo bloccato perchè trovati \nordini sulla voce di servizio!", 300, 150, "Attenzione", "", "null")

                    End If
                    myReader1.Close()

                    ImpostaLimiteMinimo(Me.cmbservizio.SelectedValue, Me.cmbvoce.SelectedValue)

                End If
                Dim script As String = "function f(){$find(""" + RadWindowServizi.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
        End Try
    End Sub

    Protected Sub btnApriServizioAppalto_Click(sender As Object, e As System.EventArgs) Handles btnApriServizioAppalto.Click
        ApriServizio()
        If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Or DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "ATTIVO" Then
            FrmSolaLettura()
        End If
    End Sub


End Class
