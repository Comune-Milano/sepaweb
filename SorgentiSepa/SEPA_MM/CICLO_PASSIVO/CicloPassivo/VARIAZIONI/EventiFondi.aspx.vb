Partial Class EventiFondi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or (Session.Item("BP_VARIAZIONI_SL") <> 1 And Session.Item("BP_VARIAZIONI") <> 1) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        ''CONTROLLO BP_VARIAZIONI O BP_GENERALE
        'If Session.Item("BP_VARIAZIONI") <> 1 And Session.Item("BP_VARIAZIONI_SL") <> 1 Then
        '    Response.Write("<script>alert('Operatore non abilitato per questa funzione!');self.close();</script>")
        '    Exit Sub
        'End If
        ''#########################

        If Not IsPostBack Then
            caricaEserciziFinanziari()
            caricaEventi()
        End If

    End Sub

    Protected Sub caricaEventi()
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
            End If
            Dim STRINGASQL As String = "SELECT DISTINCT SEPA.OPERATORI.OPERATORE,TO_DATE(SISCOM_MI.EVENTI_TRASF_FONDI.DATA_ORA_EVENTO,'yyyyMMddHH24MISS') AS DATA_ORA_EVENTO," _
                                       & "(SELECT SISCOM_MI.TAB_EVENTI.DESCRIZIONE " _
                                       & "FROM SISCOM_MI.TAB_EVENTI " _
                                       & "WHERE SISCOM_MI.TAB_EVENTI.COD=SISCOM_MI.EVENTI_TRASF_FONDI.COD_EVENTO) AS EVENTO," _
                                       & "SISCOM_MI.EVENTI_TRASF_FONDI.MOTIVAZIONE," _
                                       & "(SELECT SISCOM_MI.PF_VOCI.CODICE || ' - ' || SISCOM_MI.PF_VOCI.DESCRIZIONE " _
                                       & "FROM SISCOM_MI.PF_VOCI WHERE SISCOM_MI.PF_VOCI.ID=SISCOM_MI.EVENTI_TRASF_FONDI.ID_VOCE) AS VOCE," _
                                       & "TAB_FILIALI.NOME AS STRUTTURA_DA," _
                                       & "T2.NOME AS STRUTTURA_A," _
                                       & "TRIM(TO_CHAR(SISCOM_MI.EVENTI_TRASF_FONDI.IMPORTO,'9G999G999G990D99')) AS IMPORTO " _
                                       & "FROM SISCOM_MI.EVENTI_TRASF_FONDI,SEPA.OPERATORI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_VOCI A,SISCOM_MI.TAB_FILIALI,SISCOM_MI.TAB_FILIALI T2 " _
                                       & "WHERE SISCOM_MI.EVENTI_TRASF_FONDI.ID_OPERATORE=SEPA.OPERATORI.ID " _
                                       & "AND TAB_FILIALI.ID=EVENTI_TRASF_FONDI.ID_STRUTTURA_DA " _
                                       & "AND T2.ID=EVENTI_TRASF_FONDI.ID_STRUTTURA_A " _
                                       & "AND SISCOM_MI.PF_VOCI.ID_PIANO_FINANZIARIO=SISCOM_MI.PF_MAIN.ID AND SISCOM_MI.PF_MAIN.ID_STATO>='5' " _
                                       & "AND A.ID_PIANO_FINANZIARIO='" & ddlEsercizio.SelectedValue & "' AND A.ID=EVENTI_TRASF_FONDI.ID_VOCE " _
                                       & "ORDER BY DATA_ORA_EVENTO DESC"
            '****************************************************

            '*******CARICO LA GRIGLIA*******
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(STRINGASQL, par.OracleConn)
            Dim ds As New Data.DataTable
            da.Fill(ds)
            lblTotale.Text = "TOTALE EVENTI VARIAZIONI TROVATI PER L'ESERCIZIO FINANZIARIO SELEZIONATO: " & ds.Rows.Count
            lblTitolo.Text = "EVENTI VARIAZIONI"
            If ds.Rows.Count > 0 Then
                DataGrid1.Visible = True
                DataGrid1.DataSource = ds
                DataGrid1.DataBind()
            Else
                DataGrid1.Visible = False
            End If
            ds.Dispose()
            '*******************************


            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '***************************************************************

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                '***************************************************************
            End If

        End Try

    End Sub

    Protected Sub caricaEserciziFinanziari()
        Try
            'APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            '########## CONDIZIONE DI RICERCA DEI PIANI FINANZIARI ##########
            'SELEZIONO I PIANI FINANZIARI APPROVATI
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                    & "WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
                    & "AND ID_STATO>='5'"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While myReader1.Read
                Dim ANNOINIZIO As String = par.IfNull(myReader1("INIZIO"), "")
                Dim ANNOFINE As String = par.IfNull(myReader1("FINE"), "")
                If Len(ANNOINIZIO) = 8 And Len(ANNOFINE) = 8 Then
                    ANNOINIZIO = ConvertiData(ANNOINIZIO)
                    ANNOFINE = ConvertiData(ANNOFINE)
                    ddlEsercizio.Items.Add(New ListItem(ANNOINIZIO & " - " & ANNOFINE, par.IfNull(myReader1("ID"), 0)))
                    If par.IfNull(myReader1("ID_STATO"), 0) = 5 Then
                        ddlEsercizio.SelectedValue = par.IfNull(myReader1("ID"), 0)
                    End If
                Else
                    'ERRORE USCIRE DALLA PAGINA
                    Response.Write("<script>alert('Si è verificato un errore durante il caricamento degli esercizi finanziari.');location.replace('../../pagina_home.aspx');</script>")
                End If

            End While

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub

    Private Function ConvertiData(ByVal dataIn As String) As String
        Dim dataOut As String = ""
        If Len(dataIn) = 8 Then
            Return Right(dataIn, 2) & "/" & Mid(dataIn, 5, 2) & "/" & Left(dataIn, 4)
        Else
            Return ""
        End If
    End Function

    Protected Sub ddlEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEsercizio.SelectedIndexChanged
        caricaEventi()
    End Sub
End Class