
Partial Class Contratti_Sposta_Annulla
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            codContratto = Request.QueryString("COD")
            tipoUI = Request.QueryString("TIPO")
            scelta = Request.QueryString("SCELTA")

            If Request.QueryString("SCELTA") = "1" Then
                personaGiuridica = CheckGiuridica()
                Select Case tipoUI
                    Case "USD"
                        lblTipoContratto.Text = "Usi Diversi"
                        CaricaUIUSD()
                    Case "EQC392"
                        lblTipoContratto.Text = "Equo Canone 392/78"
                        CaricaUIEQC392()
                    Case "L43198"
                        lblTipoContratto.Text = "Legge 431/98"
                        CaricaUIL43198()
                    Case "L43198_ART15"
                        lblTipoContratto.Text = "Art.15 R.R. 1/2004"
                        CaricaUIART15()
                    Case "NONE"
                        lblTipoContratto.Text = "Nessuna Tipologia (O.A.)"
                        CaricaUINONE()
                    Case "10"
                        lblTipoContratto.Text = "Forze Dell'Ordine"
                        CaricaUIFFOO()
                    Case "12"
                        lblTipoContratto.Text = "Canone Convenzionato"
                        CaricaUICanConv()
                End Select
            Else
                personaGiuridica = CheckGiuridica()
                If personaGiuridica = True Then
                    Response.Write("<script>document.location.href='RiepilogoAssegnazioneG.aspx?SCELTA=" & scelta & "&COD=" & codContratto & "&CODUI=" & LBLCODUI.Value & "&IDUI=" & LBLIDUI.Value & "&TIPOUI=ERP';</script>")
                Else
                    Response.Write("<script>document.location.href='RiepilogoAssegnazione.aspx?SCELTA=" & scelta & "&COD=" & codContratto & "&CODUI=" & LBLCODUI.Value & "&IDUI=" & LBLIDUI.Value & "&TIPOUI=ERP';</script>")
                End If
            End If
        End If

    End Sub

    Public Property scelta() As String
        Get
            If Not (ViewState("par_scelta") Is Nothing) Then
                Return CStr(ViewState("par_scelta"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_scelta") = value
        End Set

    End Property

    Private Function CheckGiuridica() As Boolean
        Dim giuridico As Boolean = False
        Dim idContratto As Long = 0
        Dim idAnagrafica As Long = 0
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * from siscom_mi.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContratto & "'"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idContratto = par.IfNull(myReader0("ID"), "0")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT * from siscom_mi.SOGGETTI_CONTRATTUALI where ID_CONTRATTO=" & idContratto & " AND COD_TIPOLOGIA_OCCUPANTE ='INTE'"
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idAnagrafica = par.IfNull(myReader0("ID_ANAGRAFICA"), "0")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT * from siscom_mi.ANAGRAFICA where ID=" & idAnagrafica
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                If par.IfNull(myReader0("RAGIONE_SOCIALE"), "") <> "" Or par.IfNull(myReader0("PARTITA_IVA"), "") <> "" Then
                    giuridico = True
                Else
                    giuridico = False
                End If
            End If
            myReader0.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

        Return giuridico

    End Function

    Public Property personaGiuridica() As Boolean
        Get
            If Not (ViewState("par_personaGiuridica") Is Nothing) Then
                Return CLng(ViewState("par_personaGiuridica"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_personaGiuridica") = value
        End Set

    End Property

    Public Property codContratto() As String
        Get
            If Not (ViewState("par_codContratto") Is Nothing) Then
                Return CStr(ViewState("par_codContratto"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_codContratto") = value
        End Set

    End Property

    Public Property tipoUI() As String
        Get
            If Not (ViewState("par_tipoUI") Is Nothing) Then
                Return CStr(ViewState("par_tipoUI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_tipoUI") = value
        End Set

    End Property

    Private Sub CaricaUIUSD()
        Try
            Dim strSQL As String = ""
            Dim conta As Integer = 0

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim dtUSD As New Data.DataTable

            strSQL = "SELECT TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS ""TIPOLOGIA"",DECODE(UI_USI_DIVERSI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",UI_USI_DIVERSI.*,T_TIPO_ALL_ERP.DESCRIZIONE," _
                & "T_TIPO_INDIRIZZO.DESCRIZIONE ||' '|| UI_USI_DIVERSI.indirizzo AS ""TIPO_VIA"", " _
                & "TO_CHAR(TO_DATE(UI_USI_DIVERSI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", UI_USI_DIVERSI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,SISCOM_MI.UI_USI_DIVERSI," _
                & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE=UI_USI_DIVERSI.COD_ALLOGGIO AND TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA AND TIPO_LIVELLO_PIANO.COD=UI_USI_DIVERSI.PIANO AND  " _
                & " UI_USI_DIVERSI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND EQCANONE='0' AND FL_OA='0' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA<>'SO' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA<>'C' AND " _
                & " ASSEGNATO='0' AND PRENOTATO='0' AND UI_USI_DIVERSI.STATO=5 " _
                & " AND UI_USI_DIVERSI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND UI_USI_DIVERSI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) ORDER BY UI_USI_DIVERSI.TIPO_INDIRIZZO ASC, UI_USI_DIVERSI.INDIRIZZO ASC, UI_USI_DIVERSI.ZONA ASC,UI_USI_DIVERSI.NUM_LOCALI ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(strSQL, par.OracleConn)
            da.Fill(dtUSD)
            DataGrid1.DataSource = dtUSD
            conta = dtUSD.Rows.Count
            DataGrid1.DataBind()

            lblTipoContratto.Text &= "  - Totale:" & conta

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaUIEQC392()
        Try
            Dim strSQL As String = ""
            Dim conta As Integer = 0

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim dtEQC392 As New Data.DataTable

            strSQL = "SELECT TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS ""TIPOLOGIA"",DECODE(ALLOGGI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE," _
                       & "T_TIPO_INDIRIZZO.DESCRIZIONE ||' '|| ALLOGGI.indirizzo AS ""TIPO_VIA"", " _
                       & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,ALLOGGI,siscom_mi.unita_immobiliari,siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI," _
                       & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                       & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND EQCANONE='1' AND FL_OA='0' AND " _
                       & " ASSEGNATO='0' AND TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA AND unita_immobiliari.cod_unita_immobiliare=alloggi.cod_alloggio AND PRENOTATO='0' AND ALLOGGI.STATO=5 " _
                       & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) ORDER BY ALLOGGI.TIPO_INDIRIZZO ASC, ALLOGGI.INDIRIZZO ASC,ALLOGGI.ZONA ASC,ALLOGGI.NUM_LOCALI ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(strSQL, par.OracleConn)
            da.Fill(dtEQC392)
            DataGrid1.DataSource = dtEQC392
            conta = dtEQC392.Rows.Count
            DataGrid1.DataBind()

            lblTipoContratto.Text &= "  - Totale:" & conta

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaUIL43198()
        Try
            Dim strSQL As String = ""
            Dim conta As Integer = 0

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim dtL43198 As New Data.DataTable

            strSQL = "SELECT TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS ""TIPOLOGIA"",DECODE(ALLOGGI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE," _
                       & "T_TIPO_INDIRIZZO.DESCRIZIONE ||' '|| ALLOGGI.indirizzo AS ""TIPO_VIA"", " _
                       & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,ALLOGGI,siscom_mi.unita_immobiliari,siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI," _
                       & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                       & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND EQCANONE='0' AND FL_POR='1' AND FL_OA='0' AND " _
                       & " ASSEGNATO='0' AND TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA AND unita_immobiliari.cod_unita_immobiliare=alloggi.cod_alloggio AND PRENOTATO='0' AND ALLOGGI.STATO=5 " _
                       & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) ORDER BY ALLOGGI.TIPO_INDIRIZZO ASC, ALLOGGI.INDIRIZZO ASC,ALLOGGI.ZONA ASC,ALLOGGI.NUM_LOCALI ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(strSQL, par.OracleConn)

            da.Fill(dtL43198)
            DataGrid1.DataSource = dtL43198

            conta = dtL43198.Rows.Count
            DataGrid1.DataBind()

            lblTipoContratto.Text &= "  - Totale:" & conta

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaUIART15()
        Try
            Dim strSQL As String = ""
            Dim conta As Integer = 0

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim dtART15 As New Data.DataTable

            strSQL = "SELECT TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS ""TIPOLOGIA"",DECODE(ALLOGGI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE," _
                       & "T_TIPO_INDIRIZZO.DESCRIZIONE ||' '|| ALLOGGI.indirizzo AS ""TIPO_VIA"", " _
                       & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM siscom_mi.unita_immobiliari,T_TIPO_PROPRIETA,ALLOGGI,siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI," _
                       & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE unita_immobiliari.id_destinazione_uso<>6 and unita_immobiliari.cod_unita_immobiliare=alloggi.cod_alloggio and TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                       & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND (EQCANONE='0' OR FL_POR='1') AND FL_OA='0' AND " _
                       & " ASSEGNATO='0' AND TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA AND PRENOTATO='0' AND ALLOGGI.STATO=5 " _
                       & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) ORDER BY ALLOGGI.TIPO_INDIRIZZO ASC, ALLOGGI.INDIRIZZO ASC,ALLOGGI.ZONA ASC,ALLOGGI.NUM_LOCALI ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(strSQL, par.OracleConn)
            da.Fill(dtART15)
            DataGrid1.DataSource = dtART15
            conta = dtART15.Rows.Count
            DataGrid1.DataBind()

            lblTipoContratto.Text &= "  - Totale:" & conta

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaUINONE()
        Try
            Dim strSQL As String = ""
            Dim conta As Integer = 0

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim dtNONE As New Data.DataTable

            strSQL = "SELECT TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS ""TIPOLOGIA"",DECODE(ALLOGGI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE," _
                       & "T_TIPO_INDIRIZZO.DESCRIZIONE ||' '|| ALLOGGI.indirizzo AS ""TIPO_VIA"", " _
                       & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,ALLOGGI,siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI,siscom_mi.unita_immobiliari," _
                       & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                       & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                       & " ASSEGNATO='0' AND TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA AND unita_immobiliari.cod_unita_immobiliare=alloggi.cod_alloggio AND PRENOTATO='0' AND ALLOGGI.STATO=5 " _
                       & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) ORDER BY ALLOGGI.TIPO_INDIRIZZO ASC,ALLOGGI.INDIRIZZO ASC,ALLOGGI.ZONA ASC,ALLOGGI.NUM_LOCALI ASC "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(strSQL, par.OracleConn)
            da.Fill(dtNONE)
            DataGrid1.DataSource = dtNONE
            conta = dtNONE.Rows.Count
            DataGrid1.DataBind()

            lblTipoContratto.Text &= "  - Totale:" & conta

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaUIFFOO()
        Try
            Dim strSQL As String = ""
            Dim conta As Integer = 0

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim dtFFOO As New Data.DataTable

            strSQL = "SELECT TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS ""TIPOLOGIA"",DECODE(ALLOGGI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE," _
                       & "T_TIPO_INDIRIZZO.DESCRIZIONE ||' '|| ALLOGGI.indirizzo AS ""TIPO_VIA"", " _
                       & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM siscom_mi.unita_immobiliari,T_TIPO_PROPRIETA,ALLOGGI,siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI," _
                       & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE unita_immobiliari.id_destinazione_uso=6 and unita_immobiliari.cod_unita_immobiliare=alloggi.cod_alloggio and TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                       & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND (EQCANONE='0' OR FL_POR='1') AND FL_OA='0' AND " _
                       & " ASSEGNATO='0' AND TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA AND PRENOTATO='0' AND ALLOGGI.STATO=5 " _
                       & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) ORDER BY ALLOGGI.TIPO_INDIRIZZO ASC, ALLOGGI.INDIRIZZO ASC,ALLOGGI.ZONA ASC,ALLOGGI.NUM_LOCALI ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(strSQL, par.OracleConn)
            da.Fill(dtFFOO)
            DataGrid1.DataSource = dtFFOO
            conta = dtFFOO.Rows.Count
            DataGrid1.DataBind()

            lblTipoContratto.Text &= "  - Totale:" & conta

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaUICanConv()
        Try
            Dim strSQL As String = ""
            Dim conta As Integer = 0

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim dtCanConv As New Data.DataTable

            strSQL = "SELECT TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS ""TIPOLOGIA"",DECODE(ALLOGGI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE," _
                      & "T_TIPO_INDIRIZZO.DESCRIZIONE ||' '|| ALLOGGI.indirizzo AS ""TIPO_VIA"", " _
                      & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM siscom_mi.unita_immobiliari,T_TIPO_PROPRIETA,ALLOGGI,siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI," _
                      & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE unita_immobiliari.id_destinazione_uso=7 and unita_immobiliari.cod_unita_immobiliare=alloggi.cod_alloggio and TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                      & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND (EQCANONE='0' OR FL_POR='1') AND FL_OA='0' AND " _
                      & " ASSEGNATO='0' AND TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA and PRENOTATO='0' AND ALLOGGI.STATO=5 " _
                      & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) ORDER BY ALLOGGI.TIPO_INDIRIZZO ASC, ALLOGGI.INDIRIZZO ASC,ALLOGGI.ZONA ASC,ALLOGGI.NUM_LOCALI ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(strSQL, par.OracleConn)
            da.Fill(dtCanConv)
            DataGrid1.DataSource = dtCanConv
            conta = dtCanConv.Rows.Count
            DataGrid1.DataBind()

            lblTipoContratto.Text &= "  - Totale:" & conta

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub DataGrid1_CancelCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        Dim scriptblock As String = ""
        scriptblock = "<script language='javascript' type='text/javascript'>" _
        & "popupWindow=window.open('../../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & e.Item.Cells(1).Text & "','" & e.Item.Cells(1).Text & "','height=580,top=150,left=250,width=780');" _
        & "</script>"
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
        End If
    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='gainsboro';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtSelezione').value='Hai selezionato Unità Cod.: " & e.Item.Cells(1).Text & "';document.getElementById('LBLCODUI').value='" & e.Item.Cells(1).Text & "';document.getElementById('LBLIDUI').value='" & e.Item.Cells(0).Text & "';")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            Select Case Request.QueryString("TIPO")
                Case "USD"
                    CaricaUIUSD()
                Case "EQC392"
                    CaricaUIEQC392()
                Case "L4138"
                    CaricaUIL43198()
                Case "NONE"
                    CaricaUINONE()
                Case "10"
                    CaricaUIFFOO()
                Case "12"
                    CaricaUICanConv()
            End Select
        End If
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        If LBLIDUI.Value <> "0" Then
            If personaGiuridica = True Then
                Response.Write("<script>document.location.href='RiepilogoAssegnazioneG.aspx?SCELTA=" & scelta & "&COD=" & codContratto & "&CODUI=" & LBLCODUI.Value & "&IDUI=" & LBLIDUI.Value & "&TIPOUI=" & tipoUI & "';</script>")
            Else
                Response.Write("<script>document.location.href='RiepilogoAssegnazione.aspx?SCELTA=" & scelta & "&COD=" & codContratto & "&CODUI=" & LBLCODUI.Value & "&IDUI=" & LBLIDUI.Value & "&TIPOUI=" & tipoUI & "';</script>")
            End If
        End If
    End Sub

    
End Class
