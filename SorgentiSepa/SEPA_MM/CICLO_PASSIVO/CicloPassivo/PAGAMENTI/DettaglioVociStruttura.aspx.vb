Imports System.Math
Partial Class DettaglioVociStruttura
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public titolo As String = ""
    Public tableDettagli As String = ""
    Public informazioniData As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        '######## VOCE RICHIESTA ###########

        Dim idVoce As Integer = Request.QueryString("ID")
        Dim dataAl As Integer = Request.QueryString("dataAl")

        informazioniData = ControllaData(dataAl)

        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If


        'INIZIALIZZAZIONE DATATABLE
        Dim dt1 As New Data.DataTable
        Dim RIGA As Data.DataRow

        dt1.Columns.Clear()
        dt1.Columns.Add("STRUTTURA")
        dt1.Columns.Add("BUDGET_INIZIALE")
        dt1.Columns.Add("BUDGET_ASSESTATO_+_VAR.")
        dt1.Columns.Add("DISPONIBILITA_RESIDUA")
        dt1.Columns.Add("TOTALE_PRENOTATO")
        dt1.Columns.Add("TOTALE_CONSUNTIVATO")
        dt1.Columns.Add("TOTALE_RIT")
        dt1.Columns.Add("TOTALE_CERTIFICATO")
        dt1.Columns.Add("TOTALE_PAGATO")



        par.cmd.CommandText = "SELECT DISTINCT SUBSTR(INIZIO,1,4) FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
            & "WHERE PF_VOCI.ID_PIANO_FINANZIARIO = PF_MAIN.ID " _
            & "And PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID " _
            & "AND PF_VOCI.ID='" & idVoce & "'"
        Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        Dim ANNO As String = ""
        If LETTORE.Read Then
            ANNO = par.IfNull(LETTORE(0), "")
        End If
        LETTORE.Close()



        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE ID='" & idVoce & "'"
        Dim LettoreVoce As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If LettoreVoce.Read Then
            titolo = par.IfNull(LettoreVoce("CODICE"), "") & " - " & par.IfNull(LettoreVoce("DESCRIZIONE"), "")
        Else
            titolo = "Si è verificato un errore durante il caricamento dei dati!"
            Exit Sub
        End If
        LettoreVoce.Close()

        Dim totaleBudget As Decimal = 0
        Dim totaleBudgetAssestato As Decimal = 0
        Dim totaleprenotato As Decimal = 0
        Dim totaleCONSUNTIVATO As Decimal = 0
        Dim totaleCERTIFICATO As Decimal = 0
        Dim totaleRITENUTELEGGECONSUNTIVATE As Decimal = 0
        Dim totaleRITENUTELEGGECERTIFICATE As Decimal = 0
        Dim totaleRITENUTELEGGELIQUIDATE As Decimal = 0
        Dim totaleliquidato As Decimal = 0

        'ELENCO STRUTTURE

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME ASC"
        Dim LettoreStrutture As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        While LettoreStrutture.Read
            RIGA = dt1.NewRow

            'VALORE LORDO, ASSESTAMENTO E VARIAZIONI PER STRUTTURA
            par.cmd.CommandText = "SELECT " _
                        & "TRIM(TO_CHAR(SUM(NVL(VALORE_LORDO,0)),'999999990D9999')) AS VALORE_LORDO,TRIM(TO_CHAR(SUM(NVL(ASSESTAMENTO_VALORE_LORDO,0)),'999999990D9999')) AS ASSESTAMENTO_VALORE_LORDO," _
                        & "TRIM(TO_CHAR(SUM(NVL(VARIAZIONI, 0)),'999999990D9999')) AS VARIAZIONI " _
                        & "FROM SISCOM_MI.PF_VOCI_STRUTTURA " _
                        & "WHERE ID_STRUTTURA='" & par.IfNull(LettoreStrutture("ID"), "-1") & "' " _
                        & "AND ID_VOCE IN (" _
                        & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                        & "WHERE CONNECT_BY_ISLEAF = 1 " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                        & "START WITH ID='" & idVoce & "') "
            Dim LettoreBudget As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            Dim budget As Decimal = 0
            Dim assestamento As Decimal = 0
            Dim variazioni As Decimal = 0

            If LettoreBudget.Read Then
                budget = par.IfNull(LettoreBudget("VALORE_LORDO"), 0)
                assestamento = par.IfNull(LettoreBudget("ASSESTAMENTO_VALORE_LORDO"), 0)
                variazioni = par.IfNull(LettoreBudget("VARIAZIONI"), 0)
            End If
            LettoreBudget.Close()

            'IMPORTO APPROVATO IN ASSESTAMENTO DA DETRARRE
            par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO_APPROVATO,0)),'999999990D9999')) AS ASSESTAMENTO_IMPORTO_APPROVATO " _
                        & "FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI,SISCOM_MI.PF_ASSESTAMENTO " _
                        & "WHERE ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & idVoce & "') " _
                        & "AND PF_ASSESTAMENTO.ID=PF_ASSESTAMENTO_VOCI.ID_ASSESTAMENTO " _
                        & "AND ID_STATO>=5 " _
                        & "AND ID_STRUTTURA='" & par.IfNull(LettoreStrutture("ID"), "-1") & "' " _
                        & "AND DATA_APP_COMUNE>'" & dataAl & "'"

            Dim importoAssestatoApprovato As Decimal = 0
            Dim LettoreImportoAssestamentoApprovato As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LettoreImportoAssestamentoApprovato.Read Then
                importoAssestatoApprovato = par.IfNull(LettoreImportoAssestamentoApprovato("ASSESTAMENTO_IMPORTO_APPROVATO"), 0)
            End If
            LettoreImportoAssestamentoApprovato.Close()
            assestamento = assestamento - importoAssestatoApprovato


            totaleBudget = totaleBudget + budget
            totaleBudgetAssestato = totaleBudgetAssestato + budget + assestamento + variazioni
            RIGA.Item("STRUTTURA") = par.IfNull(LettoreStrutture("NOME"), "")
            RIGA.Item("BUDGET_INIZIALE") = Format(budget, "##,##0.00")
            RIGA.Item("BUDGET_ASSESTATO_+_VAR.") = Format(budget + assestamento + variazioni, "##,##0.00")

            '*********************************************************************
            'IMPORTO PRENOTATO PER STRUTTURA

            par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO_PRENOTATO,0)),'999999990D9999')) AS PRENOTATO FROM SISCOM_MI.PRENOTAZIONI " _
                        & "WHERE (ID_STATO=0 OR " _
                        & "(id_stato = 1 and data_consuntivazione >'" & dataAl & "') OR " _
                        & "(id_stato = 2 and data_certificazione >'" & dataAl & "' and data_consuntivazione > '" & dataAl & "'))" _
                        & "AND ANNO='" & ANNO & "' " _
                        & "AND ID_STRUTTURA='" & par.IfNull(LettoreStrutture("ID"), "-1") & "' " _
                        & "AND ID_VOCE_PF IN (" _
                        & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                        & "WHERE CONNECT_BY_ISLEAF = 1 " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                        & "START WITH ID='" & idVoce & "') "
            Dim prenotato As Decimal = 0
            Dim LettorePrenotato As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LettorePrenotato.Read Then
                prenotato = par.IfNull(LettorePrenotato("PRENOTATO"), 0)
            End If
            LettorePrenotato.Close()
            totaleprenotato = totaleprenotato + prenotato
            RIGA.Item("TOTALE_PRENOTATO") = Format(prenotato, "##,##0.00")
            '*********************************************************************

            '*********************************************************************
            'IMPORTO CONSUNTIVATO PER STRUTTURA

            par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO_APPROVATO,0)-NVL(RIT_LEGGE_IVATA,0)),'999999990D9999')) AS APPROVATO FROM SISCOM_MI.PRENOTAZIONI " _
                        & "WHERE ((ID_STATO=1) " _
                        & "or (id_stato=2 and data_certificazione >'" & dataAl & "'))" _
                        & "AND DATA_CONSUNTIVAZIONE<='" & dataAl & "' " _
                        & "AND ANNO='" & ANNO & "' " _
                        & "AND ID_STRUTTURA='" & par.IfNull(LettoreStrutture("ID"), "-1") & "' " _
                        & "AND ID_VOCE_PF IN (" _
                        & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                        & "WHERE CONNECT_BY_ISLEAF = 1 " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                        & "START WITH ID='" & idVoce & "') "
            Dim CONSUNTIVATO As Decimal = 0
            Dim LettoreAPPROVATO As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LettoreAPPROVATO.Read Then
                CONSUNTIVATO = par.IfNull(LettoreAPPROVATO("APPROVATO"), 0)
            End If
            LettoreAPPROVATO.Close()
            totaleCONSUNTIVATO = totaleCONSUNTIVATO + CONSUNTIVATO
            RIGA.Item("TOTALE_CONSUNTIVATO") = Format(CONSUNTIVATO, "##,##0.00")
            '*********************************************************************

            'IMPORTO LIQUIDATO PER STRUTTURA
            par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO,0)),'999999990D9999')) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE DATA_MANDATO<='" & dataAl & "' " _
                    & "AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2 AND ID_STRUTTURA='" & par.IfNull(LettoreStrutture("ID"), "-1") & "') " _
                    & "AND ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF=1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & idVoce & "'" _
                    & ") "
            '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
            '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
            '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
            'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO,0)),'999999990D9999')) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
            '            & "WHERE ID_PAGAMENTO IN (" _
            '            & "SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI " _
            '            & "WHERE " _
            '            & "DATA_PRENOTAZIONE<='" & dataAl & "' " _
            '            & "AND ID_STRUTTURA='" & par.IfNull(LettoreStrutture("ID"), "-1") & "' " _
            '            & "AND ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI " _
            '            & "WHERE CONNECT_BY_ISLEAF=1 " _
            '            & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
            '            & "START WITH ID='" & idVoce & "'" _
            '            & ") AND ID_STATO=2) " _
            '            & "AND DATA_MANDATO<='" & dataAl & "' " _
            '            & "AND id_voce_pf IN (SELECT ID " _
            '            & "FROM siscom_mi.pf_voci " _
            '            & "WHERE CONNECT_BY_ISLEAF = 1 " _
            '            & "CONNECT BY PRIOR pf_voci.ID = pf_voci.id_voce_madre " _
            '            & "START WITH ID = '" & idVoce & "')"

            Dim liquidato As Decimal = 0
            Dim LettoreLiquidato As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LettoreLiquidato.Read Then
                liquidato = par.IfNull(LettoreLiquidato("IMPORTO"), 0)
            End If
            LettoreLiquidato.Close()
            totaleliquidato = totaleliquidato + liquidato
            RIGA.Item("TOTALE_PAGATO") = Format(liquidato, "##,##0.00")
            '*********************************************************************
            'IMPORTO CERTIFICATO

            par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO_APPROVATO,0)-NVL(RIT_LEGGE_IVATA,0)),'999999990D9999')) AS APPROVATO FROM SISCOM_MI.PRENOTAZIONI " _
                        & "WHERE ID_STATO>1 " _
                        & "AND ID_PAGAMENTO IS NOT NULL " _
                        & "AND ID_STRUTTURA='" & par.IfNull(LettoreStrutture("ID"), "-1") & "' " _
                        & "AND ANNO='" & ANNO & "' " _
                        & "AND DATA_CERTIFICAZIONE<='" & dataAl & "' " _
                        & "AND ID_VOCE_PF IN (" _
                        & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                        & "WHERE CONNECT_BY_ISLEAF = 1 " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                        & "START WITH ID='" & idVoce & "') "
            Dim CERTIFICATO As Decimal = 0
            Dim LettoreCERTIFICATO As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LettoreCERTIFICATO.Read Then
                CERTIFICATO = par.IfNull(LettoreCERTIFICATO("APPROVATO"), 0)
            End If
            LettoreCERTIFICATO.Close()
            CERTIFICATO = CERTIFICATO - liquidato
            totaleCERTIFICATO = totaleCERTIFICATO + CERTIFICATO
            RIGA.Item("TOTALE_CERTIFICATO") = Format(CERTIFICATO, "##,##0.00")
            '*********************************************************************
            'IMPORTO RIT LEGGE CONSUNTIVATE

            par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(RIT_LEGGE_IVATA,0)),'999999990D9999')) AS APPROVATO FROM SISCOM_MI.PRENOTAZIONI " _
                        & "WHERE ID_STRUTTURA='" & par.IfNull(LettoreStrutture("ID"), "-1") & "' " _
                        & "AND ((ID_STATO=1) " _
                        & "or (id_stato=2 and data_certificazione >'" & dataAl & "'))" _
                        & "AND DATA_CONSUNTIVAZIONE<='" & dataAl & "' " _
                        & "AND ANNO='" & ANNO & "' " _
                        & "AND ID_VOCE_PF IN (" _
                        & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                        & "WHERE CONNECT_BY_ISLEAF = 1 " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                        & "START WITH ID='" & idVoce & "') "

            Dim RITENUTELEGGECONSUNTIVATE As Decimal = 0
            Dim LettoreRITENUTELEGGE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LettoreRITENUTELEGGE.Read Then
                RITENUTELEGGECONSUNTIVATE = par.IfNull(LettoreRITENUTELEGGE("APPROVATO"), 0)
            End If
            LettoreRITENUTELEGGE.Close()
            totaleRITENUTELEGGECONSUNTIVATE = totaleRITENUTELEGGECONSUNTIVATE + RITENUTELEGGECONSUNTIVATE
            RIGA.Item("TOTALE_RIT") = Format(RITENUTELEGGECONSUNTIVATE, "##,##0.00")
            '**************************************************************************************************
            ''IMPORTO RIT LEGGE CERTIFICATE

            'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(RIT_LEGGE_IVATA,0)),'999999990D9999')) AS APPROVATO FROM SISCOM_MI.PRENOTAZIONI " _
            '            & "WHERE ID_STRUTTURA='" & par.IfNull(LettoreStrutture("ID"), "-1") & "' " _
            '            & "AND ID_STATO=2" _
            '            & "AND DATA_PRENOTAZIONE<='" & dataAl & "' " _
            '            & "AND ID_VOCE_PF IN (" _
            '            & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
            '            & "WHERE CONNECT_BY_ISLEAF = 1 " _
            '            & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
            '            & "START WITH ID='" & idVoce & "') "

            'Dim RITENUTELEGGECERTIFICATE As Decimal = 0
            'Dim LettoreRITENUTELEGGECERTIFICATE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If LettoreRITENUTELEGGECERTIFICATE.Read Then
            '    RITENUTELEGGECERTIFICATE = par.IfNull(LettoreRITENUTELEGGECERTIFICATE("APPROVATO"), 0)
            'End If
            'LettoreRITENUTELEGGECERTIFICATE.Close()
            'totaleRITENUTELEGGECERTIFICATE = totaleRITENUTELEGGECERTIFICATE + RITENUTELEGGECERTIFICATE
            'RIGA.Item("TOTALE_RIT_CERT") = Format(RITENUTELEGGECERTIFICATE, "##,##0.00")
            ''**************************************************************************************************

            ''IMPORTO RIT LEGGE LIQUIDATE
            'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO,0)),'999999990D9999')) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
            '        & "WHERE DATA_MANDATO<='" & dataAl & "' " _
            '        & "AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STRUTTURA='" & par.IfNull(LettoreStrutture("ID"), "-1") & "' ) " _
            '        & "AND ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI " _
            '        & "WHERE CONNECT_BY_ISLEAF=1 " _
            '        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
            '        & "START WITH ID='" & idVoce & "'" _
            '        & ") "

            'Dim RITENUTELEGGELIQUIDATE As Decimal = 0
            'Dim LettoreRITENUTELEGGELIQUIDATE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If LettoreRITENUTELEGGELIQUIDATE.Read Then
            '    RITENUTELEGGELIQUIDATE = par.IfNull(LettoreRITENUTELEGGELIQUIDATE("IMPORTO"), 0)
            'End If
            'LettoreRITENUTELEGGELIQUIDATE.Close()
            'totaleRITENUTELEGGELIQUIDATE = totaleRITENUTELEGGELIQUIDATE + RITENUTELEGGELIQUIDATE
            'RIGA.Item("TOTALE_RIT_LIQ") = Format(RITENUTELEGGELIQUIDATE, "##,##0.00")
            ''**************************************************************************************************

            'IMPORTO RESIDUO
            Dim residuo As Decimal = 0
            residuo = budget + assestamento + variazioni - prenotato - CONSUNTIVATO - CERTIFICATO - liquidato - RITENUTELEGGECONSUNTIVATE
            RIGA.Item("DISPONIBILITA_RESIDUA") = Format(residuo, "##,##0.00")
            dt1.Rows.Add(RIGA)


        End While
        LettoreStrutture.Close()


        RIGA = dt1.NewRow
        RIGA.Item("STRUTTURA") = "TOTALE"
        RIGA.Item("BUDGET_INIZIALE") = Format(totaleBudget, "##,##0.00")
        RIGA.Item("BUDGET_ASSESTATO_+_VAR.") = Format(totaleBudgetAssestato, "##,##0.00")
        RIGA.Item("DISPONIBILITA_RESIDUA") = Format(totaleBudgetAssestato - totaleCERTIFICATO - totaleCONSUNTIVATO - totaleliquidato - totaleprenotato - totaleRITENUTELEGGECONSUNTIVATE, "##,##0.00")
        RIGA.Item("TOTALE_PRENOTATO") = Format(totaleprenotato, "##,##0.00")
        RIGA.Item("TOTALE_CONSUNTIVATO") = Format(totaleCONSUNTIVATO, "##,##0.00")
        RIGA.Item("TOTALE_RIT") = Format(totaleRITENUTELEGGECONSUNTIVATE, "##,##0.00")
        RIGA.Item("TOTALE_CERTIFICATO") = Format(totaleCERTIFICATO, "##,##0.00")
        RIGA.Item("TOTALE_PAGATO") = Format(totaleliquidato, "##,##0.00")
        dt1.Rows.Add(RIGA)

        DataGrid1.DataSource = dt1
        DataGrid1.DataBind()

        Dim nRighe As Integer = DataGrid1.Items.Count - 1
        For j As Integer = 0 To 8
            DataGrid1.Items(nRighe).Cells(j).Font.Bold = True
            DataGrid1.Items(nRighe).Cells(j).BackColor = Drawing.Color.Gainsboro
            DataGrid1.Items(nRighe).Cells(j).Font.Size = 9
        Next
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

    Private Function ControllaData(ByVal data As String) As String
        Dim dataF As String = Left(data, 4) & "-" & Mid(data, 5, 2) & "-" & Right(data, 2)
        Dim dataFine As String = ""
        Dim dataOggi As String = Format(Now.Date, "yyyyMMdd")
        Dim dataInizio As String = ""
        If IsDate(dataF) Then
            dataFine = CStr(Min(CInt(data), CInt(dataOggi)))
            dataInizio = Left(dataFine, 4) & "0101"
            Return Right(dataInizio, 2) & "-" & Mid(dataInizio, 5, 2) & "-" & Left(dataInizio, 4) & " AL " & Right(dataFine, 2) & "-" & Mid(dataFine, 5, 2) & "-" & Left(dataFine, 4)
        Else
            Return ""
        End If
    End Function
End Class
