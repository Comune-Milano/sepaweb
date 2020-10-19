'*** TAB DI SOLA VISUALIZZAZIONE MANUTENZIONI

Partial Class Tab_Manutenzioni
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text

                ' CONNESSIONE DB
                IdConnessione = CType(Me.Page.FindControl("txtConnessione"), TextBox).Text

                If PAR.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    'PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    '‘‘par.cmd.Transaction = par.myTrans
                End If
                ''''''''''''''''''''''''''

                BindGrid_Manutenzioni()

            End If

            vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text

        Catch ex As Exception

            PAR.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Property vIdImpianto() As Long
        Get
            If Not (ViewState("par_idImpianto") Is Nothing) Then
                Return CLng(ViewState("par_idImpianto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idImpianto") = value
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


    'CONTROLLI GRID1
    Private Sub BindGrid_Manutenzioni()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans



        'CREATE TABLE INTERVENTI_MANUTENZIONE
        '(
        '  ID_TABELLA              NUMBER,
        '  ID_TABELLA_MILLESIMALE  NUMBER,
        ')

        'REVERSIBILE    =0=A carico della proprietà
        '               =1=Reversibile



        StringaSql = "select SISCOM_MI.INTERVENTI_MANUTENZIONE.ID,SISCOM_MI.TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS ""TIPO_SERVIZIO"" ," _
                        & " SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS ""TIPO_INTERVENTO"" ," _
                        & " SISCOM_MI.ARTICOLI_MANU.DESCRIZIONE AS ""ARTICOLO"" ," _
                        & " SISCOM_MI.INTERVENTI_MANUTENZIONE.DESCRIZIONE, " _
                        & " to_char(to_date(SISCOM_MI.INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy') as ""DATA_INIZIO_INTERVENTO""," _
                        & " to_char(to_date(SISCOM_MI.INTERVENTI_MANUTENZIONE.DATA_FINE_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy') as ""DATA_FINE_INTERVENTO""," _
                        & " to_char(to_date(SISCOM_MI.INTERVENTI_MANUTENZIONE.DATA_ORDINE,'yyyymmdd'),'dd/mm/yyyy') as ""DATA_ORDINE""," _
                        & " SISCOM_MI.INTERVENTI_MANUTENZIONE.NUM_DOCUMENTO,SISCOM_MI.INTERVENTI_MANUTENZIONE.NUM_FATTURA," _
                        & " SISCOM_MI.INTERVENTI_MANUTENZIONE.COSTO," _
                        & " decode(SISCOM_MI.INTERVENTI_MANUTENZIONE.REVERSIBILE,'0','A carico della proprietà',1,'Reversibile') as ""REVERSIBILE""," _
                        & " SISCOM_MI.INTERVENTI_MANUTENZIONE.COSTO_REVERSIBILE " _
              & " from SISCOM_MI.INTERVENTI_MANUTENZIONE,SISCOM_MI.TIPOLOGIA_SERVIZI_MANU,SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU,SISCOM_MI.ARTICOLI_MANU " _
              & " where SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_IMPIANTO = " & vIdImpianto _
              & " and SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO=SISCOM_MI.TIPOLOGIA_SERVIZI_MANU.ID (+) " _
              & " and SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO=SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU.ID (+) " _
              & " and SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_ARTICOLO=SISCOM_MI.ARTICOLI_MANU.ID (+) "
        '& " order by SISCOM_MI.RENDIMENTO_TERMICI.DATA_ESAME "


        PAR.cmd.CommandText = StringaSql

        'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, PAR.OracleConn)
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "INTERVENTI_MANUTENZIONE")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        ds.Dispose()

    End Sub

End Class
