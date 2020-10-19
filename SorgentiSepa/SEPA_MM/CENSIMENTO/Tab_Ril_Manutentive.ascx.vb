
Partial Class CENSIMENTO_Tab_Ril_Manutentive
    Inherits UserControlSetIdMode
    Dim sStringaSql As String = ""
    Dim par As New CM.Global

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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            vId = CType(Me.Page, Object).vId
            CaricaDati()
            If Session("PED2_SOLOLETTURA") = "1" Then
                FrmSolaLettura()
            End If
            If Session("SLE") = "1" Then
                FrmSolaLettura()
            End If

        End If
        '**********SERVE PER RECUPERARE ID SUBITO DOPO NUOVO INSERIMENTO IMMOBILE*****************
        If vId = 0 Then
            vId = CType(Me.Page, Object).vId
        End If
    End Sub

    Private Sub FrmSolaLettura()
        Try
           
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Public Sub CaricaDati()
        Try
            If Session("SLE") = "1" Then
                par.OracleConn.Open()
                par.SettaCommand(par)

            Else
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            End If
            'sStringaSql = "select 'VERBALE SLOGGIO' AS ORIGINE,SL_STATO_SLOGGIO.DESCRIZIONE AS STATO,to_char(SL_SLOGGIO.STIMA_COSTI,'999999990D99') AS COSTO_INTERVENTO,(CASE WHEN SL_SLOGGIO.ID_STATO=2 THEN to_char(to_date(SL_SLOGGIO.DATA_SLOGGIO,'yyyymmdd'),'dd/mm/yyyy') ELSE to_char(to_date(SL_SLOGGIO.DATA_PRE_SLOGGIO,'yyyymmdd'),'dd/mm/yyyy') END) AS DATA_OPERAZIONE,'' AS COLLEGAMENTO from siscom_mi.sl_sloggio,SISCOM_MI.SL_STATO_SLOGGIO WHERE SL_STATO_SLOGGIO.ID=SL_SLOGGIO.ID_STATO AND SL_SLOGGIO.ID_STATO<>0 AND ID_UNITA_IMMOBILIARE= " & vId

            sStringaSql = "select (CASE WHEN SL_SLOGGIO.ID_STATO=2 THEN nvl(SL_SLOGGIO.DATA_SLOGGIO,'--') ELSE nvl(SL_SLOGGIO.DATA_PRE_SLOGGIO,'--') END) AS DATA_OPERAZIONE_ORD," _
            & "'VERBALE SLOGGIO' AS ORIGINE,SL_STATO_SLOGGIO.DESCRIZIONE AS STATO,to_char(SL_SLOGGIO.STIMA_COSTI,'999999990D99') AS COSTO_INTERVENTO,(CASE WHEN SL_SLOGGIO.ID_STATO=2 THEN to_char(to_date(SL_SLOGGIO.DATA_SLOGGIO,'yyyymmdd'),'dd/mm/yyyy') ELSE to_char(to_date(SL_SLOGGIO.DATA_PRE_SLOGGIO,'yyyymmdd'),'dd/mm/yyyy') END) AS DATA_OPERAZIONE," _
            & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/StampaVerbaleSL.aspx?ID='||SL_SLOGGIO.ID_UNITA_IMMOBILIARE||'&IDSTATO='||SL_SLOGGIO.ID_STATO||'&IDSLOGGIO='||SL_SLOGGIO.ID||''','''','''');£>Visualizza</a>','$','&'),'£','" & Chr(34) & "') AS COLLEGAMENTO " _
            & "from siscom_mi.sl_sloggio,SISCOM_MI.SL_STATO_SLOGGIO WHERE SL_STATO_SLOGGIO.ID=SL_SLOGGIO.ID_STATO  AND ID_UNITA_IMMOBILIARE= " & vId _
            & " UNION " _
            & "SELECT nvl(RILIEVO_ALLOGGIO_SFITTO.DATA_CARICAMENTO,'--') AS DATA_OPERAZIONE_ORD,'RILIEVO' AS ORIGINE,'CARICATO' AS STATO," _
            & "(" _
            & "CASE WHEN RILIEVO_ALLOGGIO_SFITTO.LIVELLO IS NULL THEN 'N.D.' ELSE " _
            & "to_char(NVL((SELECT RILIEVO_VAL_MONETARI.VALORE FROM SISCOM_MI.RILIEVO_VAL_MONETARI,SISCOM_MI.TAB_VALORI_MONETARI WHERE RILIEVO_VAL_MONETARI.ID_TIPO=TAB_VALORI_MONETARI.ID AND RILIEVO_VAL_MONETARI.ID_RILIEVO=RILIEVO_ALLOGGIO_SFITTO.ID_RILIEVO AND TAB_VALORI_MONETARI.DESCRIZIONE=RILIEVO_ALLOGGIO_SFITTO.LIVELLO),0)*NVL(UNITA_IMMOBILIARI.S_NETTA,0),'999999990D99') END " _
            & ") AS COSTO_INTERVENTO, " _
            & "to_char(to_date(RILIEVO_ALLOGGIO_SFITTO.DATA_CARICAMENTO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_OPERAZIONE," _
            & "replace(replace('<a href=£../ALLEGATI/RILEVAZIONI/SCHEDA_'||RILIEVO_ALLOGGIO_SFITTO.ID||'.PDF£ target=£_blank£>Visualizza</a>','$','&'),'£','" & Chr(34) & "') AS COLLEGAMENTO " _
            & "FROM " _
            & "SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.RILIEVO_ALLOGGIO_SFITTO " _
            & "WHERE RILIEVO_ALLOGGIO_SFITTO.LIVELLO IS NOT NULL AND " _
            & "RILIEVO_ALLOGGIO_SFITTO.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID " _
            & "AND UNITA_IMMOBILIARI.ID=" & vId & " ORDER BY DATA_OPERAZIONE_ORD DESC"


            par.cmd.CommandText = sStringaSql
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "RILEVAZIONI")
            dataGridRilevazioni.DataSource = ds
            dataGridRilevazioni.DataBind()
            If Session("SLE") = "1" Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Protected Sub dataGridRilevazioni_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dataGridRilevazioni.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
        End If
    End Sub
End Class
