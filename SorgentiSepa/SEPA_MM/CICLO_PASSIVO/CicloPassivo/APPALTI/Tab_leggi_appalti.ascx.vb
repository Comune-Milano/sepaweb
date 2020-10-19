' TAB LEGALE DEL FORNITORE FISICO

Partial Class Tab_leggi_appalti
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            BindGrid_Appalti()

            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If
        End If
        vIdFornitori = CType(Me.Page.FindControl("txtIdFornitore"), TextBox).Text
    End Sub

    Private Sub FrmSolaLettura()
        Try

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.lblErrore.Visible = True
            'lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub BindGrid_Appalti()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        ' I NOMI DEI CAMPI DEVONO CORRISPONDERE CON QUELLI NELLA DATAGRID
        StringaSql = "  select SISCOM_MI.APPALTI.ID, SISCOM_MI.APPALTI.ID_FORNITORE, SISCOM_MI.APPALTI.NUM_REPERTORIO, TO_CHAR(TO_DATE(SISCOM_MI.APPALTI.DATA_REPERTORIO,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_REPERTORIO"", (CASE (APPALTI.SAL) WHEN 0 THEN 'NO' ELSE 'SI' END) AS ""SAL"", SISCOM_MI.APPALTI.DESCRIZIONE, SISCOM_MI.APPALTI.DATA_INIZIO, SISCOM_MI.APPALTI.DURATA_MESI AS ""DURATA"", TRIM(TO_CHAR(SISCOM_MI.APPALTI.BASE_ASTA_CANONE,'9G999G990D99')) AS ""ASTA_CANONE"", TRIM(TO_CHAR(SISCOM_MI.APPALTI.BASE_ASTA_CONSUMO,'9G999G990D99')) AS ""ASTA_CONSUMO"", " _
        & "TRIM(TO_CHAR(SISCOM_MI.APPALTI.ONERI_SICUREZZA_CANONE,'9G999G990D99')) AS ""ONERI_CANONE"", TRIM(TO_CHAR(SISCOM_MI.APPALTI.ONERI_SICUREZZA_CONSUMO,'9G999G990D99')) AS ""ONERI_CONSUMO"", SISCOM_MI.APPALTI.PERC_ONERI_SIC_CAN, SISCOM_MI.APPALTI.PERC_ONERI_SIC_CON, SISCOM_MI.APPALTI.PENALI, SISCOM_MI.APPALTI.ANNO_RIF_INIZIO AS ""RIFINIZIO"", SISCOM_MI.APPALTI.ANNO_RIF_FINE AS ""RIFINE"", '' AS ""COSTO"", " _
        & "SISCOM_MI.LOTTI_SERVIZI.ID AS ""ID_LOTTO"", SISCOM_MI.LOTTI_SERVIZI.DESCRIZIONE AS ""DESCRIZIONE_LOTTO"", SISCOM_MI.TAB_SERVIZI.DESCRIZIONE AS ""SERVIZIO"", SISCOM_MI.APPALTI.PERC_SCONTO_CANONE AS ""SCONTO_CANONE"", SISCOM_MI.APPALTI.PERC_SCONTO_CONSUMO AS ""SCONTO_CONSUMO""" _
                    & " from SISCOM_MI.APPALTI, SISCOM_MI_FORNITORI" _
    & " where SISCOM_MI.APPALTI.ID_FORNITORE = " & vIdFornitori & " and " _
  & "SISCOM_MI.FORNITORI.ID=SISCOM_MI.APPALTI.ID_FORNITORE " _
 & "order by SISCOM_MI.APPALTI.NUM_REPERTORIO "




        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "APPALTI")


        DataGrid3.DataSource = ds
        DataGrid3.DataBind()

        ds.Dispose()
    End Sub

    Private Property vIdFornitori() As Long
        Get
            If Not (ViewState("par_idFornitori") Is Nothing) Then
                Return CLng(ViewState("par_idFornitori"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idFornitori") = value
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

End Class
