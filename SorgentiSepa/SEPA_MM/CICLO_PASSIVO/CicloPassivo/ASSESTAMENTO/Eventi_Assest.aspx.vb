
Partial Class CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_Eventi_Assest
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String
    Dim sWhere As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or (Session.Item("BP_GENERALE") <> 1 And Session.Item("MOD_ASS_CONV_COMU") <> 1) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        '##### CARICAMENTO PAGINA #####
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        If Not IsPostBack Then
            CaricaFiliali()
            CaricaEventiAssest()
            'CaricaEventiPagAnnullo()
        End If

    End Sub

    Private Sub CaricaFiliali()

        '*****************APERTURA CONNESSIONE***************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Me.cmbStruttura.Items.Clear()

        'If Session.Item("MOD_ASS_CONV_ALER") = 1 Or Session.Item("MOD_ASS_CONV_COMU") = 1 Then
        '    par.cmd.CommandText = "SELECT TAB_FILIALI.NOME, TAB_FILIALI.ID, (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP||' '||COMUNI_NAZIONI.NOME) AS INDIRIZZO FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI WHERE TAB_FILIALI.ID_INDIRIZZO = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = COMUNI_NAZIONI.COD ORDER BY NOME ASC"
        'Else
        '    Dim QUERY As String = ""
        '    QUERY = "SELECT TAB_FILIALI.NOME, TAB_FILIALI.ID, (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP||' '||COMUNI_NAZIONI.NOME) AS INDIRIZZO FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI WHERE TAB_FILIALI.ID_INDIRIZZO = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = COMUNI_NAZIONI.COD AND TAB_FILIALI.ID = " & par.IfEmpty(Session.Item("ID_STRUTTURA"), 0) & " ORDER BY NOME ASC"

        '    par.cmd.CommandText = QUERY
        'End If

        par.cmd.CommandText = "SELECT TAB_FILIALI.NOME, TAB_FILIALI.ID, (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP||' '||COMUNI_NAZIONI.NOME) AS INDIRIZZO FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI WHERE TAB_FILIALI.ID_INDIRIZZO = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = COMUNI_NAZIONI.COD ORDER BY NOME ASC"
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        cmbStruttura.Items.Clear()
        'If Session.Item("MOD_ASS_CONV_ALER") = 1 Or Session.Item("MOD_ASS_CONV_COMU") = 1 Then
        cmbStruttura.Items.Add(New ListItem("QUALSIASI", -1))
        'Else
        '    Me.cmbStruttura.Enabled = False
        'End If
        While lettore.Read
            Me.cmbStruttura.Items.Add(New ListItem(par.IfNull(lettore("NOME"), " ") & " - " & par.IfNull(lettore("INDIRIZZO"), " "), par.IfNull(lettore("ID"), -1)))
        End While
        If Session.Item("ID_STRUTTURA") > 0 Then
            Me.cmbStruttura.SelectedValue = Session.Item("ID_STRUTTURA")
        End If
        lettore.Close()
        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


    End Sub

    Private Sub CaricaEventiAssest()
        Try
            Dim CondStruttura As String = ""
            If Me.cmbStruttura.SelectedValue > 0 Then
                CondStruttura = "AND UFF.id = '" & Me.cmbStruttura.SelectedValue & "' "
            Else
                CondStruttura = " "
            End If

            sStringaSql = "SELECT TO_DATE(SISCOM_MI.EVENTI_ASSESTAMENTO.DATA_ORA,'yyyyMMddHH24MISS') AS DATA_ORA," _
                & "SISCOM_MI.TAB_EVENTI.DESCRIZIONE,COD_EVENTO,MOTIVAZIONE, " _
                & "SEPA.OPERATORI.OPERATORE,SISCOM_MI.EVENTI_ASSESTAMENTO.ID_OPERATORE, " _
                & "STRU.NOME AS FILIALE, " _
                & "TRIM(TO_CHAR(EVENTI_ASSESTAMENTO.IMPORTO,'9G999G999G990D99')) AS IMPORTO, " _
                & "PF_VOCI.CODICE||' '||PF_VOCI.DESCRIZIONE AS VOCE " _
                & "FROM SISCOM_MI.EVENTI_ASSESTAMENTO, " _
                & "SISCOM_MI.TAB_EVENTI, " _
                & "SEPA.OPERATORI, " _
                & "SISCOM_MI.TAB_FILIALI STRU, " _
                & "SISCOM_MI.TAB_FILIALI UFF, " _
                & "SISCOM_MI.PF_VOCI " _
                & "WHERE SISCOM_MI.EVENTI_ASSESTAMENTO.COD_EVENTO = SISCOM_MI.TAB_EVENTI.COD " _
                & "AND SISCOM_MI.EVENTI_ASSESTAMENTO.ID_OPERATORE=SEPA.OPERATORI.ID " _
                & "AND SEPA.OPERATORI.ID_UFFICIO=UFF.ID " _
                & "AND SISCOM_MI.EVENTI_ASSESTAMENTO.ID_STRUTTURA=STRU.ID(+) " _
                & CondStruttura _
                & "AND PF_VOCI.ID(+) = EVENTI_ASSESTAMENTO.ID_VOCE " _
                & "AND ID_ASSESTAMENTO = '" & Request.QueryString("IDASSEST") & "' " _
                & "ORDER BY EVENTI_ASSESTAMENTO.DATA_ORA " _
                & "DESC, EVENTI_ASSESTAMENTO.COD_EVENTO DESC"

            'sStringaSql = "SELECT TO_DATE(SISCOM_MI.EVENTI_ASSESTAMENTO.DATA_ORA,'yyyyMMddHH24MISS') AS ""DATA_ORA"", " _
            '            & "SISCOM_MI.TAB_EVENTI.DESCRIZIONE,COD_EVENTO,MOTIVAZIONE, " _
            '            & "SEPA.OPERATORI.OPERATORE,SISCOM_MI.EVENTI_ASSESTAMENTO.ID_OPERATORE, " _
            '            & "SEPA.CAF_WEB.COD_CAF,TRIM(TO_CHAR(EVENTI_ASSESTAMENTO.IMPORTO,'9G999G999G990D99')) AS IMPORTO, (PF_VOCI.CODICE||' '||PF_VOCI.DESCRIZIONE)AS VOCE " _
            '            & "FROM SEPA.CAF_WEB, SISCOM_MI.EVENTI_ASSESTAMENTO,SISCOM_MI.TAB_EVENTI,SEPA.OPERATORI,SISCOM_MI.PF_VOCI " _
            '            & "WHERE SISCOM_MI.EVENTI_ASSESTAMENTO.COD_EVENTO=SISCOM_MI.TAB_EVENTI.COD  " _
            '            & "AND SISCOM_MI.EVENTI_ASSESTAMENTO.ID_OPERATORE=SEPA.OPERATORI.ID " _
            '            & "AND SEPA.CAF_WEB.ID=SEPA.OPERATORI.ID_CAF " _
            '            & " " & CondStruttura _
            '            & " AND PF_VOCI.ID(+) = EVENTI_ASSESTAMENTO.ID_VOCE " _
            '            & " AND ID_ASSESTAMENTO = " & Request.QueryString("IDASSEST") _
            '            & " ORDER BY EVENTI_ASSESTAMENTO.DATA_ORA DESC, EVENTI_ASSESTAMENTO.COD_EVENTO DESC"


            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            'Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSql, par.OracleConn)
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

            'lblTotale.Text = "0"
            'Do While myReader.Read()
            '    lblTotale.Text = CInt(lblTotale.Text) + 1
            'Loop

            'lblTotale.Text = "TOTALE EVENTI PAGAMENTI PARZIALI TROVATI: " & lblTotale.Text
            'myReader.Close()

            '*** CARICO LA GRIGLIA
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)
            Dim ds As New Data.DataTable
            da.Fill(ds)
            lblTotale.Text = "TOTALE EVENTI ASSESTAMENTO TROVATI PER LA STRUTTURA DELL'OPERATORE: " & ds.Rows.Count
            lblTitolo.Text = "EVENTI ASSESTAMENTO ESERCIZIO FINANZIARIO IN CORSO"
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            ds.Dispose()
            '*******************************
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch EX1 As Oracle.DataAccess.Client.OracleException
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Protected Sub cmbStruttura_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStruttura.SelectedIndexChanged
        CaricaEventiAssest()
    End Sub
End Class
