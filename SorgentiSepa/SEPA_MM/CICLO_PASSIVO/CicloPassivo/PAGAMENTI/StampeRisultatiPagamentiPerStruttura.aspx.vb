Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports System.Math

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_StampeRisultatiPagamentiPerStruttura
    Inherits PageSetIdMode

    Dim par As New CM.Global
    Dim dt As New Data.DataTable
    Dim dtExcel As New Data.DataTable
    Dim Str As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            If Request.QueryString("CONS") = 1 Then
                StampaTabellaConsuntivato()
            Else
                If Request.QueryString("LIQ") = 1 Then
                    StampaTabellaLiquidazioni()
                Else
                    If Not IsNothing(Request.QueryString("IDV")) Then
                        StampaTabellaPrenotazioni()
                    Else
                        StampaTabellaPagamenti()
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub StampaTabellaLiquidazioni()
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim esercizioFinanziario As String = Request.QueryString("EF")
            Dim idStruttura As String = Request.QueryString("IDS")
            Dim controlloStruttura As String = ""
            Dim CONTROLLODATA As String = ""
            If Not IsNothing(Request.QueryString("AL")) Then
                CONTROLLODATA = "AND PAGAMENTI_LIQUIDATI.DATA_MANDATO <='" & Request.QueryString("AL") & "' "
            End If

            '****************************************************************************
            Dim dataAl As Integer
            If Not IsNothing(Request.QueryString("AL")) Then
                dataAl = CInt(Request.QueryString("AL"))
                Dim dataoggi As Integer = CInt(Format(Now.Date, "yyyyMMdd"))
                dataAl = Min(dataoggi, dataAl)
            Else
                Dim dataoggi As Integer = CInt(Format(Now.Date, "yyyyMMdd"))
                dataAl = dataoggi
            End If
            '************ACQUISIZIONE INFORMAZIONI SUL PIANO FINANZIARIO*****************
            Dim sDataEsercizio As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                & "WHERE ID=(select ID_ESERCIZIO_FINANZIARIO from SISCOM_MI.PF_MAIN where ID=" & esercizioFinanziario & ") "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                sDataEsercizio = " DAL " & par.FormattaData(par.IfNull(myReader1("INIZIO"), ""))
                sDataEsercizio = sDataEsercizio & " AL " & Right(dataAl, 2) & "/" & Mid(dataAl, 5, 2) & "/" & Left(dataAl, 4)
            End If
            myReader1.Close()
            '****************************************************************************


            lblTitolo.Text = ""
            If idStruttura <> "-1" Then
                controlloStruttura = "AND PRENOTAZIONI.ID_STRUTTURA='" & idStruttura & "' "
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & idStruttura & "'"
                Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LETTORE.Read Then
                    lblTitolo.Text = "SITUAZIONE LIQUIDAZIONI - STRUTTURA " & par.IfNull(LETTORE(0), "") & " - " & sDataEsercizio
                End If
                LETTORE.Close()
            Else
                lblTitolo.Text = "SITUAZIONE GENERALE LIQUIDAZIONI " & " - " & sDataEsercizio
            End If

            '######## ELENCO DEI PAGAMENTI PER STRUTTURA ED ESERCIZIO FINANZIARIO ######
            par.cmd.CommandText = "SELECT DISTINCT PRENOTAZIONI.ID AS ID_PRE,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                & "PF_VOCI.CODICE AS CODICE,PF_VOCI.DESCRIZIONE AS VOCE," _
                & "(CASE WHEN PRENOTAZIONI.IMPORTO_APPROVATO IS NULL THEN TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_PRENOTATO,'999G999G990D99')) ELSE TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_APPROVATO,'999G999G990D99')) END) AS IMPORTO_PRENOTATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE PAGAMENTI.DESCRIZIONE END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(MANUTENZIONI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO," _
                & "TAB_STATI_ODL.DESCRIZIONE AS STATO_ODL," _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                & "PRENOTAZIONI.TIPO_PAGAMENTO AS TIPO_ADP," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "CASE WHEN PAGAMENTI.PROGR IS NOT NULL THEN PAGAMENTI.PROGR ||'/'|| PAGAMENTI.ANNO ELSE '' END  AS ADP," _
                & "TO_CHAR(TO_DATE(DATA_EMISSIONE,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_ADP," _
                & "TRIM(TO_CHAR(PAGAMENTI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO_ADP," _
                & "CASE WHEN TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) IS NULL THEN '0,00' ELSE TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) END AS RITLEGGE_ADP," _
                & "SISCOM_MI.GETSTATOPAGAMENTO(PAGAMENTI.ID) AS STATO_PAGAMENTO,PRENOTAZIONI.ID_APPALTO AS ID_A,PRENOTAZIONI.ID_FORNITORE AS ID_F," _
                & "PRENOTAZIONI.ID_PAGAMENTO AS ID_P,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE,PAGAMENTI.PROGR AS PROGRESSIVO,PF_VOCI.ID AS IDVOCE " _
                & "FROM SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PAGAMENTI.ID " _
                & "AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PF_VOCI.ID " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND TAB_STATI_PAGAMENTI.ID(+)=PAGAMENTI.ID_STATO " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND TAB_STATI_ODL.ID(+)=MANUTENZIONI.STATO " _
                & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & esercizioFinanziario & "' " _
                & "AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & Request.QueryString("IDV") & "') " _
                & controlloStruttura _
                & CONTROLLODATA _
                & "AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "ORDER BY PRENOTAZIONI.ID_PAGAMENTO,PRENOTAZIONI.ID ASC "

            Dim lettorePagamenti As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim RIGA As Data.DataRow
            Dim RIGAexcel As Data.DataRow
            dt.Columns.Clear()
            dt.Rows.Clear()
            dt.Columns.Add("CODICE")
            dt.Columns.Add("VOCE")
            dt.Columns.Add("DESCRIZIONE_TIPO")
            dt.Columns.Add("FILIALE")
            dt.Columns.Add("IMPORTO_PRENOTATO")
            dt.Columns.Add("DESCRIZIONE_PAG")
            dt.Columns.Add("IMPORTO")
            dt.Columns.Add("FORNITORE")
            dt.Columns.Add("TIPO_ADP")
            dt.Columns.Add("ADP")
            dt.Columns.Add("DATA_ADP")
            dt.Columns.Add("IMPORTO_ADP")
            dt.Columns.Add("RIT_LEGGE")
            dt.Columns.Add("ID_PAG")
            dt.Columns.Add("MAE")
            dt.Columns.Add("ID_PRE")

            '*************************
            'DT PER EXCEL
            dtExcel.Columns.Clear()
            dtExcel.Rows.Clear()
            dtExcel.Columns.Add("CODICE")
            dtExcel.Columns.Add("VOCE")
            dtExcel.Columns.Add("DESCRIZIONE_TIPO")
            dtExcel.Columns.Add("FILIALE")
            dtExcel.Columns.Add("IMPORTO_PRENOTATO")
            dtExcel.Columns.Add("DESCRIZIONE_PAG")
            dtExcel.Columns.Add("IMPORTO")
            dtExcel.Columns.Add("FORNITORE")
            dtExcel.Columns.Add("ADP")
            dtExcel.Columns.Add("DATA_ADP")
            dtExcel.Columns.Add("IMPORTO_ADP")
            dtExcel.Columns.Add("RIT_LEGGE")
            dtExcel.Columns.Add("ID_PAG")
            dtExcel.Columns.Add("NUM_MAE")
            dtExcel.Columns.Add("DATA_MAE")
            dtExcel.Columns.Add("IMPORTO_MAE")
            dtExcel.Columns.Add("ID_PRE")
            dtExcel.Columns.Add("ID_MAE")
            '*************************

            Dim i As Integer = 0
            Dim codice As String = ""
            Dim totaleImportoPrenotazione As Decimal = 0
            Dim totalePrenotazione As Decimal = 0
            Dim totaleImportoODL As Decimal = 0
            Dim totaleADP As Decimal = 0
            Dim totaleRITLEGGEADP As Decimal = 0
            Dim totaleImportoADP As Decimal = 0
            Dim adpPrec As String = ""
            Dim id_pag_prec As String = "-1"
            Dim id_mae_prec As String = "-1"

            While lettorePagamenti.Read

                i = i + 1
                RIGA = dt.NewRow
                RIGA.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                RIGA.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                RIGA.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                RIGA.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                RIGA.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                RIGA.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                RIGA.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                RIGA.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                RIGA.Item("TIPO_ADP") = par.IfNull(lettorePagamenti("TIPO_ADP"), "")
                If par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "3" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "4" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "6" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "7" Then
                    RIGA.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                Else
                    RIGA.Item("ADP") = "<a href=""javascript:ApriPagamenti(" & par.IfNull(lettorePagamenti("ID_P"), -1) & "," & par.IfNull(lettorePagamenti("TIPO_ADP"), 0) & ")"">" & par.IfNull(lettorePagamenti("ADP"), "") & "</a>"
                End If
                Dim ImportoADPperSomma As Decimal = 0
                RIGA.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")

                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'CONTO MAE, DETERMINO IL NUMERO DI MAE PERCHè HO BISOGNO DI SCRIVERE L'IMPORTO ADP
                'IN MANIERA CORRETTA E CONTROLLO L'IMPORTO TOTALE
                Dim nMae As Integer = 0
                par.cmd.CommandText = "SELECT count(*) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) & "' " _
                    & CONTROLLODATA
                Dim lettoreNumeroMandati As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreNumeroMandati.Read Then
                    nMae = par.IfNull(lettoreNumeroMandati(0), 0)
                End If
                lettoreNumeroMandati.Close()
                Dim nMaeGEN As Integer = 0
                par.cmd.CommandText = "SELECT count(*) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) & "' "
                Dim lettoreNumeroMandatiGEN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreNumeroMandatiGEN.Read Then
                    nMaeGEN = par.IfNull(lettoreNumeroMandatiGEN(0), 0)
                End If
                lettoreNumeroMandatiGEN.Close()
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'DETERMINO L'IMPORTO TOTALE MAE IN PAGAMENTI LIQUIDATI CON DATA
                Dim ImportoTotaleMandati As Decimal = 0
                par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) & "' " _
                    & CONTROLLODATA
                Dim lettoreImportoMandati As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreImportoMandati.Read Then
                    ImportoTotaleMandati = par.IfNull(lettoreImportoMandati(0), 0)
                End If
                lettoreImportoMandati.Close()
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'DETERMINO L'IMPORTO TOTALE MAE IN PAGAMENTI LIQUIDATI SENZA DATA
                Dim ImportoTotaleMandatiGenerale As Decimal = 0
                par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) & "' "
                Dim lettoreImportoMandatiGenerale As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreImportoMandatiGenerale.Read Then
                    ImportoTotaleMandatiGenerale = par.IfNull(lettoreImportoMandatiGenerale(0), 0)
                End If
                lettoreImportoMandatiGenerale.Close()
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'DETERMINO L'IMPORTO TOTALE APPROVATO
                Dim totaleImportoApprovato As Decimal = 0
                par.cmd.CommandText = "SELECT DISTINCT" _
                    & "(CASE WHEN PRENOTAZIONI.IMPORTO_APPROVATO IS NULL THEN TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_PRENOTATO,'999G999G990D99')) ELSE TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_APPROVATO,'999G999G990D99')) END) AS IMPORTO_PRENOTATO,PRENOTAZIONI.ID " _
                    & "FROM SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI, SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL, SISCOM_MI.TIPO_PAGAMENTI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                    & "AND PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PAGAMENTI.ID " _
                    & "AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PF_VOCI.ID " _
                    & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                    & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                    & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                    & "AND TAB_STATI_PAGAMENTI.ID(+)=PAGAMENTI.ID_STATO " _
                    & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                    & "AND TAB_STATI_ODL.ID(+)=MANUTENZIONI.STATO " _
                    & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & esercizioFinanziario & "' " _
                    & "AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & Request.QueryString("IDV") & "') " _
                    & CONTROLLODATA _
                    & "AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL " _
                    & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                    & "AND PAGAMENTI.PROGR='" & par.IfNull(lettorePagamenti("PROGRESSIVO"), "") & "' "
                Dim totaleImportiApprovati As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While totaleImportiApprovati.Read
                    totaleImportoApprovato = totaleImportoApprovato + CDec(par.IfNull(totaleImportiApprovati(0), 0))
                End While
                totaleImportiApprovati.Close()
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'DEFINISCO UNA TOLLERANZA PARI A UN EURO PER I PROBLEMI RELATIVI AI CENTESIMI NEI PAGAMENTI
                Dim TOLLERANZA As Decimal = 1
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'CONTROLLO SE IL TOTALE APPROVATO è UGUALE ALL'IMPORTO TOTALE GENERALE DEI MANDATI
                Dim importiUguali As Boolean = False
                If totaleImportoApprovato < ImportoTotaleMandatiGenerale + TOLLERANZA And totaleImportoApprovato > ImportoTotaleMandatiGenerale - TOLLERANZA Then
                    importiUguali = True
                End If
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'SE GLI IMPORTI SONO UGUALI, SCRIVO DIRETTAMENTE IL TOTALE SENZA INDICARE DEI PARZIALI
                Dim IMPORTOADPEXCEL As String = ""
                If importiUguali = True Then
                    RIGA.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                    IMPORTOADPEXCEL = Format(CDec(par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")), "##,##0.00")
                    ImportoADPperSomma = CDec(par.IfNull(lettorePagamenti("IMPORTO_ADP"), ""))
                Else
                    If nMae > 1 Then
                        'SE CI SONO PIù MANDATI INDICO IL PARZIALE FINO A QUEL MOMENTO LIQUIDATO E TRA PARENTESI IL TOTALE COMPLESSIVO DEI MANDATI
                        RIGA.Item("IMPORTO_ADP") = totaleImportoApprovato & "<br />(" & par.IfNull(lettorePagamenti("IMPORTO_ADP"), 0) & ")"
                        IMPORTOADPEXCEL = Format(CDec(totaleImportoApprovato), "##,##0.00")
                        ImportoADPperSomma = totaleImportoApprovato
                    Else
                        RIGA.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                        IMPORTOADPEXCEL = Format(CDec(par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")), "##,##0.00")
                        ImportoADPperSomma = CDec(par.IfNull(lettorePagamenti("IMPORTO_ADP"), ""))
                    End If
                End If
                RIGA.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")
                RIGA.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                RIGA.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), "")
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'MAE
                'LEGGO TUTTI I MANDATI RELATIVI AL PAGAMENTO, CONSIDERO SE ILLUMINARLI GIALLI O ROSSI
                par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.ID AS ID_MAE ,NUM_MANDATO ||'/'||ANNO_MANDATO AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,DATA_MANDATO AS DM,IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) & "'"
                Dim lettoreMAE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim lettoreMAE2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim idmae As String = "-1"
                If lettoreMAE2.Read Then
                    idmae = par.IfNull(lettoreMAE2("ID_MAE"), "-1")
                End If
                lettoreMAE2.Close()
                RIGA.Item("MAE") = ""
                Dim check As Integer = 0
                Dim count As Integer = 0
                Dim importoTotale As Decimal = 0
                Dim enter As Boolean = False
                Dim MAE As Boolean = False
                If id_mae_prec = idmae Then
                    MAE = True
                End If
                While lettoreMAE.Read
                    RIGAexcel = dtExcel.NewRow
                    'COLORE ROSSO
                    RIGAexcel.Item("IMPORTO_ADP") = IMPORTOADPEXCEL
                    Dim colorRed As String = ""
                    If par.IfNull(lettoreMAE("DM"), "") > dataAl Then
                        If nMaeGEN > 1 Then
                            If importiUguali = False Then
                                'NON TUTTI I PAGAMENTI SONO STATI LIQUIDATI ALTRIMENTI LASCIAMO LA SITUAZIONE INVARIATA
                                'QUANDO CI SONO PIù MANDATI E UNO DI QUESTI NON è ANCORA STATO LIQUIDATO LO COLORO DI ROSSO
                            Else
                                colorRed = "bgcolor=""#CC6666"""
                                RIGA.Item("IMPORTO_ADP") = Format(CDec(totaleImportoApprovato) - CDec(par.IfNull(lettoreMAE("IMPORTO"), "")), "##,##0.00") & "<br />(" & Format(CDec(par.IfNull(lettorePagamenti("IMPORTO_ADP"), 0)), "##,##0.00") & ")"
                                ImportoADPperSomma = CDec(totaleImportoApprovato) - CDec(par.IfNull(lettoreMAE("IMPORTO"), ""))
                                RIGAexcel.Item("IMPORTO_ADP") = Format(CDec(totaleImportoApprovato) - CDec(par.IfNull(lettoreMAE("IMPORTO"), "")), "##,##0.00")

                            End If
                        End If
                    End If

                    'COLORE GIALLO
                    Dim giallo As Boolean = False
                    If nMae > 1 And importiUguali = False Then
                        par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.ID AS ID_MAE ," _
                            & "NUM_MANDATO ||'/'||ANNO_MANDATO AS MAE," _
                            & "TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO," _
                            & "DATA_MANDATO AS DM," _
                            & "IMPORTO " _
                            & "FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                            & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) & "' " _
                            & "AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & Request.QueryString("IDV") & "') " _
                            & "AND PAGAMENTI_LIQUIDATI.IMPORTO='" & Replace(par.IfNull(lettoreMAE("IMPORTO"), 0), ".", "") & "' " _
                            & CONTROLLODATA

                        Dim lettoreControlloMAE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettoreControlloMAE.Read Then
                            giallo = True
                        End If
                        lettoreControlloMAE.Close()
                    End If
                    If check = 0 Then
                        RIGA.Item("MAE") = "<table style=""font-family: Arial, Helvetica, sans-serif; font-size: xx-small;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;"" bgcolor=""#EEEEEE"" width=""100%""><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">MAE</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">DATA</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">IMPORTO</th>"
                        Dim BGCOLOR As String = ""
                        If nMae > 1 Then
                            If giallo Then
                                BGCOLOR = "bgcolor=""#FFFF66"""
                            End If
                        End If
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr " & BGCOLOR & " " & colorRed & "><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"
                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")
                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                        Else
                            If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")

                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        If MAE = False Then
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        check = 1
                        count = count + 1
                    Else
                        Dim BGCOLOR As String = ""
                        If nMae > 1 Then
                            If giallo Then
                                BGCOLOR = "bgcolor=""#FFFF66"""
                            End If
                        End If
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr " & BGCOLOR & " " & colorRed & "><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"
                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")

                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                        Else
                            If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        If MAE = False Then
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        count = count + 1
                    End If
                    dtExcel.Rows.Add(RIGAexcel)

                    importoTotale = importoTotale + par.IfNull(lettoreMAE("IMPORTO"), 0)
                End While
                lettoreMAE.Close()


                If RIGA.Item("MAE") <> "" Then
                    If count = 1 Then
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "</table>"
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td colspan='2' style=""border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">TOTALE</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(importoTotale, "##,##0.00") & "</td></tr>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "</table>"

                        RIGAexcel = dtExcel.NewRow
                        RIGAexcel.Item("IMPORTO_ADP") = Replace(RIGA.Item("IMPORTO_ADP"), "<br />", "")
                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")

                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                        Else
                            If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        RIGAexcel.Item("NUM_MAE") = "TOTALE"
                        RIGAexcel.Item("DATA_MAE") = ""
                        RIGAexcel.Item("IMPORTO_MAE") = Format(importoTotale, "##,##0.00")
                        RIGAexcel.Item("ID_MAE") = -1
                        dtExcel.Rows.Add(RIGAexcel)

                    End If
                Else
                    RIGAexcel = dtExcel.NewRow
                    RIGAexcel.Item("IMPORTO_ADP") = Replace(RIGA.Item("IMPORTO_ADP"), "<br />", "")
                    RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                    RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                    RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                    RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                    RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                    RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                    RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                    RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                    RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                    RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                    RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")

                    If i = 1 Then
                        RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                        RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                    Else
                        If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                        Else
                            RIGAexcel.Item("ADP") = ""
                            RIGAexcel.Item("DATA_ADP") = ""
                            RIGAexcel.Item("IMPORTO_ADP") = ""
                        End If
                    End If

                    RIGAexcel.Item("NUM_MAE") = ""
                    RIGAexcel.Item("DATA_MAE") = ""
                    RIGAexcel.Item("IMPORTO_MAE") = ""
                    RIGAexcel.Item("ID_MAE") = -1
                    dtExcel.Rows.Add(RIGAexcel)
                End If
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                dt.Rows.Add(RIGA)

                If idPagamentoPrec.Value <> par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) Then
                    'totaleADP = totaleADP + CDec(par.IfNull(lettorePagamenti("IMPORTO_ADP"), 0))
                    totaleADP = totaleADP + ImportoADPperSomma
                End If
                totaleRITLEGGEADP = totaleRITLEGGEADP + CDec(par.IfNull(lettorePagamenti("RITLEGGE_ADP"), 0))
                totalePrenotazione = totalePrenotazione + CDec(par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), 0))

                idPagamentoPrec.Value = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1)
                adpPrec = par.IfNull(lettorePagamenti("ADP"), "")
                id_mae_prec = idmae
            End While

            RIGA = dt.NewRow
            RIGA.Item("CODICE") = "TOTALE VOCI"
            RIGA.Item("VOCE") = ""
            RIGA.Item("FILIALE") = ""
            RIGA.Item("IMPORTO_PRENOTATO") = Format(totalePrenotazione, "##,##0.00")
            RIGA.Item("DESCRIZIONE_PAG") = ""
            RIGA.Item("IMPORTO") = ""
            RIGA.Item("FORNITORE") = ""
            RIGA.Item("ADP") = "TOTALE ADP + RIT.LEGGE"
            RIGA.Item("DATA_ADP") = Format(totaleADP + totaleRITLEGGEADP, "##,##0.00")
            RIGA.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGA.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGA.Item("ID_PAG") = ""
            RIGA.Item("MAE") = ""
            RIGA.Item("ID_PRE") = ""
            dt.Rows.Add(RIGA)

            RIGAexcel = dtExcel.NewRow
            RIGAexcel.Item("CODICE") = "TOTALE VOCI"
            RIGAexcel.Item("VOCE") = ""
            RIGAexcel.Item("FILIALE") = ""
            RIGAexcel.Item("DESCRIZIONE_TIPO") = ""
            RIGAexcel.Item("IMPORTO_PRENOTATO") = Format(totalePrenotazione, "##,##0.00")
            RIGAexcel.Item("DESCRIZIONE_PAG") = ""
            RIGAexcel.Item("IMPORTO") = ""
            RIGAexcel.Item("FORNITORE") = ""
            RIGAexcel.Item("ADP") = "TOTALE ADP + RIT.LEGGE"
            RIGAexcel.Item("DATA_ADP") = Format(totaleADP + totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("ID_PAG") = ""
            RIGAexcel.Item("NUM_MAE") = ""
            RIGAexcel.Item("DATA_MAE") = ""
            RIGAexcel.Item("IMPORTO_MAE") = ""
            RIGAexcel.Item("ID_PRE") = ""
            RIGAexcel.Item("ID_MAE") = ""
            dtExcel.Rows.Add(RIGAexcel)

            lettorePagamenti.Close()
            If dt.Rows.Count > 1 Then

                DataGrid1.Visible = True
                DataGrid1.DataSource = dt
                If idStruttura <> "-1" Then
                    DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "FILIALE")).Visible = False
                End If
                DataGrid1.AllowPaging = False
                Session.Add("PAG_STRU", dtExcel)
                DataGrid1.DataBind()
                For Each di As DataGridItem In DataGrid1.Items
                    If di.Cells(1).Text.Contains("TOTALE VOCI") Then
                        di.ForeColor = Drawing.Color.Red
                        di.BackColor = Drawing.Color.Lavender
                        For j As Integer = 0 To di.Cells.Count - 1
                            di.Cells(j).Font.Bold = True
                        Next
                    End If
                Next
            Else
                DataGrid1.Visible = False
                lblErrore.Text = "La ricerca di pagamenti liquidati non ha prodotto risultati"
            End If
            '*****************CHIUSURA CONNESSIONE***************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Response.Write(ex.Message)
            '*****************CHIUSURA CONNESSIONE***************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub StampaTabellaPrenotazioni()
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim esercizioFinanziario As String = Request.QueryString("EF")
            Dim idStruttura As String = Request.QueryString("IDS")
            Dim controlloStruttura As String = ""
            Dim CONTROLLODATA As String = ""
            If Not IsNothing(Request.QueryString("AL")) Then
                CONTROLLODATA = "AND DATA_PRENOTAZIONE <='" & Request.QueryString("AL") & "' "
            End If

            '****************************************************************************
            Dim dataAl As Integer
            If Not IsNothing(Request.QueryString("AL")) Then
                dataAl = CInt(Request.QueryString("AL"))
                Dim dataoggi As Integer = CInt(Format(Now.Date, "yyyyMMdd"))
                dataAl = Min(dataoggi, dataAl)
            Else
                Dim dataoggi As Integer = CInt(Format(Now.Date, "yyyyMMdd"))
                dataAl = dataoggi
            End If
            '************ACQUISIZIONE INFORMAZIONI SUL PIANO FINANZIARIO*****************
            Dim sDataEsercizio As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                & "WHERE ID=(select ID_ESERCIZIO_FINANZIARIO from SISCOM_MI.PF_MAIN where ID=" & esercizioFinanziario & ") "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                sDataEsercizio = " DAL " & par.FormattaData(par.IfNull(myReader1("INIZIO"), ""))
                sDataEsercizio = sDataEsercizio & " AL " & Right(dataAl, 2) & "/" & Mid(dataAl, 5, 2) & "/" & Left(dataAl, 4)
            End If
            myReader1.Close()
            '****************************************************************************

            lblTitolo.Text = ""
            If idStruttura <> "-1" Then
                controlloStruttura = "AND PRENOTAZIONI.ID_STRUTTURA='" & idStruttura & "' "
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & idStruttura & "'"
                Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LETTORE.Read Then
                    lblTitolo.Text = "SITUAZIONE PRENOTAZIONI - STRUTTURA " & par.IfNull(LETTORE(0), "") & " - " & sDataEsercizio
                End If
                LETTORE.Close()
            Else
                lblTitolo.Text = "SITUAZIONE GENERALE PRENOTAZIONI - " & sDataEsercizio
            End If

            '######## ELENCO DELLE PRENOTAZIONI PER ESERCIZIO FINANZIARIO STRUTTURA E VOCE SELEZIONATA ######
            par.cmd.CommandText = "SELECT PRENOTAZIONI.ID AS ID_PRE,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                & "PF_VOCI.CODICE AS CODICE,PF_VOCI.DESCRIZIONE AS VOCE," _
                & "IMPORTO_PRENOTATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE PAGAMENTI.DESCRIZIONE END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(MANUTENZIONI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO," _
                & "TAB_STATI_ODL.DESCRIZIONE AS STATO_ODL," _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                & "PRENOTAZIONI.TIPO_PAGAMENTO AS TIPO_ADP," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "CASE WHEN PAGAMENTI.PROGR IS NOT NULL THEN PAGAMENTI.PROGR ||'/'|| PAGAMENTI.ANNO ELSE '' END  AS ADP," _
                & "TO_CHAR(TO_DATE(DATA_EMISSIONE,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_ADP," _
                & "TRIM(TO_CHAR(PAGAMENTI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO_ADP," _
                & "CASE WHEN TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) IS NULL THEN '0,00' ELSE TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) END AS RITLEGGE_ADP," _
                & "SISCOM_MI.GETSTATOPAGAMENTO(PAGAMENTI.ID) AS STATO_PAGAMENTO,PRENOTAZIONI.ID_APPALTO AS ID_A,PRENOTAZIONI.ID_FORNITORE AS ID_F,PRENOTAZIONI.ID_PAGAMENTO AS ID_P,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE " _
                & "FROM SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND TAB_STATI_PAGAMENTI.ID(+)=PAGAMENTI.ID_STATO " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND TAB_STATI_ODL.ID(+)=MANUTENZIONI.STATO " _
                & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & esercizioFinanziario & "' " _
                & "AND PF_VOCI.ID IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & Request.QueryString("IDV") & "') " _
                & controlloStruttura _
                & CONTROLLODATA _
                & "AND PRENOTAZIONI.ID_PAGAMENTO IS NULL " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "ORDER BY PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR ASC "

            Dim lettorePagamenti As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim RIGA As Data.DataRow
            Dim RIGAexcel As Data.DataRow
            dt.Columns.Clear()
            dt.Rows.Clear()
            dt.Columns.Add("CODICE")
            dt.Columns.Add("VOCE")
            dt.Columns.Add("DESCRIZIONE_TIPO")
            dt.Columns.Add("FILIALE")
            dt.Columns.Add("IMPORTO_PRENOTATO")
            dt.Columns.Add("DESCRIZIONE_PAG")
            dt.Columns.Add("IMPORTO")
            dt.Columns.Add("FORNITORE")
            dt.Columns.Add("TIPO_ADP")
            dt.Columns.Add("ADP")
            dt.Columns.Add("DATA_ADP")
            dt.Columns.Add("IMPORTO_ADP")
            dt.Columns.Add("RIT_LEGGE")
            dt.Columns.Add("ID_PAG")
            dt.Columns.Add("MAE")
            dt.Columns.Add("ID_PRE")

            '*************************
            'DT PER EXCEL
            dtExcel.Columns.Clear()
            dtExcel.Rows.Clear()
            dtExcel.Columns.Add("CODICE")
            dtExcel.Columns.Add("VOCE")
            dtExcel.Columns.Add("DESCRIZIONE_TIPO")
            dtExcel.Columns.Add("FILIALE")
            dtExcel.Columns.Add("IMPORTO_PRENOTATO")
            dtExcel.Columns.Add("DESCRIZIONE_PAG")
            dtExcel.Columns.Add("IMPORTO")
            dtExcel.Columns.Add("FORNITORE")
            dtExcel.Columns.Add("ADP")
            dtExcel.Columns.Add("DATA_ADP")
            dtExcel.Columns.Add("IMPORTO_ADP")
            dtExcel.Columns.Add("RIT_LEGGE")
            dtExcel.Columns.Add("ID_PAG")
            dtExcel.Columns.Add("NUM_MAE")
            dtExcel.Columns.Add("DATA_MAE")
            dtExcel.Columns.Add("IMPORTO_MAE")
            dtExcel.Columns.Add("ID_PRE")
            dtExcel.Columns.Add("ID_MAE")
            '*************************

            Dim i As Integer = 0
            Dim codice As String = ""
            Dim totaleImportoPrenotazione As Decimal = 0
            Dim totalePrenotazione As Decimal = 0
            Dim totaleImportoODL As Decimal = 0
            Dim totaleADP As Decimal = 0
            Dim totaleRITLEGGEADP As Decimal = 0
            Dim totaleImportoADP As Decimal = 0
            Dim adpPrec As String = ""
            Dim id_pag_prec As String = "-1"
            Dim id_mae_prec As String = "-1"

            While lettorePagamenti.Read
                i = i + 1
                RIGA = dt.NewRow
                RIGA.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                RIGA.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                RIGA.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                RIGA.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                RIGA.Item("IMPORTO_PRENOTATO") = Format(par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), ""), "##,##0.00")
                RIGA.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                RIGA.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                RIGA.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                RIGA.Item("TIPO_ADP") = par.IfNull(lettorePagamenti("TIPO_ADP"), "")
                If par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "3" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "4" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "6" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "7" Then
                    RIGA.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                Else
                    RIGA.Item("ADP") = "<a href=""javascript:ApriPagamenti(" & par.IfNull(lettorePagamenti("ID_P"), -1) & "," & par.IfNull(lettorePagamenti("TIPO_ADP"), 0) & ")"">" & par.IfNull(lettorePagamenti("ADP"), "") & "</a>"
                End If
                RIGA.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                RIGA.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                RIGA.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")
                RIGA.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                RIGA.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), "")

                '@@@@@@@@@@@@@@@@ MAE @@@@@@@@@@@@@@@@@
                par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.ID AS ID_MAE ,NUM_MANDATO ||'/'||ANNO_MANDATO AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) & "'"
                Dim lettoreMAE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim lettoreMAE2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim idmae As String = "-1"

                If lettoreMAE2.Read Then
                    idmae = par.IfNull(lettoreMAE2("ID_MAE"), "-1")
                End If
                lettoreMAE2.Close()
                RIGA.Item("MAE") = ""
                Dim check As Integer = 0
                Dim count As Integer = 0
                Dim importoTotale As Decimal = 0
                Dim enter As Boolean = False
                Dim MAE As Boolean = False
                If id_mae_prec = idmae Then
                    MAE = True
                End If
                While lettoreMAE.Read
                    RIGAexcel = dtExcel.NewRow
                    If check = 0 Then
                        RIGA.Item("MAE") = "<table style=""font-family: Arial, Helvetica, sans-serif; font-size: xx-small;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;"" bgcolor=""#EEEEEE"" width=""100%""><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">MAE</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">DATA</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">IMPORTO</th>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"

                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")
                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                            RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")

                        Else
                            If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                                RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        If MAE = False Then
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        check = 1
                        count = count + 1
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"


                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")

                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                            RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")

                        Else
                            If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                                RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        If MAE = False Then
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        count = count + 1
                    End If
                    dtExcel.Rows.Add(RIGAexcel)

                    importoTotale = importoTotale + par.IfNull(lettoreMAE("IMPORTO"), 0)
                End While
                lettoreMAE.Close()


                If RIGA.Item("MAE") <> "" Then
                    If count = 1 Then
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "</table>"
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td colspan='2' style=""border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">TOTALE</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(importoTotale, "##,##0.00") & "</td></tr>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "</table>"

                        RIGAexcel = dtExcel.NewRow

                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")

                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                            RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                        Else
                            If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                                RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        RIGAexcel.Item("NUM_MAE") = "TOTALE"
                        RIGAexcel.Item("DATA_MAE") = ""
                        RIGAexcel.Item("IMPORTO_MAE") = Format(importoTotale, "##,##0.00")
                        RIGAexcel.Item("ID_MAE") = -1
                        dtExcel.Rows.Add(RIGAexcel)

                    End If
                Else
                    RIGAexcel = dtExcel.NewRow

                    RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                    RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                    RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                    RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                    RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                    RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                    RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                    RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                    RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                    RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                    RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")

                    If i = 1 Then
                        RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                        RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                        RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                    Else
                        If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                            RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                        Else
                            RIGAexcel.Item("ADP") = ""
                            RIGAexcel.Item("DATA_ADP") = ""
                            RIGAexcel.Item("IMPORTO_ADP") = ""
                        End If
                    End If

                    RIGAexcel.Item("NUM_MAE") = ""
                    RIGAexcel.Item("DATA_MAE") = ""
                    RIGAexcel.Item("IMPORTO_MAE") = ""
                    RIGAexcel.Item("ID_MAE") = -1
                    dtExcel.Rows.Add(RIGAexcel)
                End If
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                dt.Rows.Add(RIGA)

                If idPagamentoPrec.Value <> par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) Then
                    totaleADP = totaleADP + CDec(par.IfNull(lettorePagamenti("IMPORTO_ADP"), 0))
                End If
                totaleRITLEGGEADP = totaleRITLEGGEADP + CDec(par.IfNull(lettorePagamenti("RITLEGGE_ADP"), 0))
                totalePrenotazione = totalePrenotazione + CDec(par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), 0))

                idPagamentoPrec.Value = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1)
                adpPrec = par.IfNull(lettorePagamenti("ADP"), "")
                id_mae_prec = idmae
            End While

            RIGA = dt.NewRow
            RIGA.Item("CODICE") = "TOTALE VOCI"
            RIGA.Item("VOCE") = ""
            RIGA.Item("FILIALE") = ""
            RIGA.Item("IMPORTO_PRENOTATO") = Format(totalePrenotazione, "##,##0.00")
            RIGA.Item("DESCRIZIONE_PAG") = ""
            RIGA.Item("IMPORTO") = ""
            RIGA.Item("FORNITORE") = ""
            RIGA.Item("ADP") = "TOTALE ADP + RIT.LEGGE"
            RIGA.Item("DATA_ADP") = Format(totaleADP + totaleRITLEGGEADP, "##,##0.00")
            RIGA.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGA.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGA.Item("ID_PAG") = ""
            RIGA.Item("MAE") = ""
            RIGA.Item("ID_PRE") = ""
            dt.Rows.Add(RIGA)

            RIGAexcel = dtExcel.NewRow
            RIGAexcel.Item("CODICE") = "TOTALE VOCI"
            RIGAexcel.Item("VOCE") = ""
            RIGAexcel.Item("FILIALE") = ""
            RIGAexcel.Item("DESCRIZIONE_TIPO") = ""
            RIGAexcel.Item("IMPORTO_PRENOTATO") = Format(totalePrenotazione, "##,##0.00")
            RIGAexcel.Item("DESCRIZIONE_PAG") = ""
            RIGAexcel.Item("IMPORTO") = ""
            RIGAexcel.Item("FORNITORE") = ""
            RIGAexcel.Item("ADP") = "TOTALE ADP + RIT.LEGGE"
            RIGAexcel.Item("DATA_ADP") = Format(totaleADP + totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("ID_PAG") = ""
            RIGAexcel.Item("NUM_MAE") = ""
            RIGAexcel.Item("DATA_MAE") = ""
            RIGAexcel.Item("IMPORTO_MAE") = ""
            RIGAexcel.Item("ID_PRE") = ""
            RIGAexcel.Item("ID_MAE") = ""
            dtExcel.Rows.Add(RIGAexcel)

            lettorePagamenti.Close()
            If dt.Rows.Count > 1 Then

                DataGrid1.Visible = True
                DataGrid1.DataSource = dt
                If idStruttura <> "-1" Then
                    DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "FILIALE")).Visible = False
                End If
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "DESCRIZIONE_PAG")).Visible = True
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "DATA_ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "IMPORTO_ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "RIT_LEGGE")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "MAE")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "IMPORTO_PRENOTATO")).HeaderText = "IMPORTO PRENOTATO"
                DataGrid1.AllowPaging = False

                Session.Add("PAG_STRU", dtExcel)
                DataGrid1.DataBind()
                For Each di As DataGridItem In DataGrid1.Items
                    If di.Cells(1).Text.Contains("TOTALE VOCI") Then
                        di.ForeColor = Drawing.Color.Red
                        di.BackColor = Drawing.Color.Lavender
                        For j As Integer = 0 To di.Cells.Count - 1
                            di.Cells(j).Font.Bold = True
                        Next
                    End If
                Next
            Else
                DataGrid1.Visible = False
                lblErrore.Text = "La ricerca di prenotazioni non ha prodotto risultati"
            End If
            '*****************CHIUSURA CONNESSIONE***************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Response.Write(ex.Message)
            '*****************CHIUSURA CONNESSIONE***************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub StampaTabellaPagamenti()
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim esercizioFinanziario As String = Request.QueryString("EF")
            Dim idStruttura As String = Request.QueryString("IDS")
            Dim controlloStruttura As String = ""


            '****************************************************************************
            Dim dataAl As Integer
            If Not IsNothing(Request.QueryString("AL")) Then
                dataAl = CInt(Request.QueryString("AL"))
                Dim dataoggi As Integer = CInt(Format(Now.Date, "yyyyMMdd"))
                dataAl = Min(dataoggi, dataAl)
            Else
                Dim dataoggi As Integer = CInt(Format(Now.Date, "yyyyMMdd"))
                dataAl = dataoggi
            End If
            '************ACQUISIZIONE INFORMAZIONI SUL PIANO FINANZIARIO*****************
            Dim sDataEsercizio As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                & "WHERE ID=(select ID_ESERCIZIO_FINANZIARIO from SISCOM_MI.PF_MAIN where ID=" & esercizioFinanziario & ") "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                sDataEsercizio = " DAL " & par.FormattaData(par.IfNull(myReader1("INIZIO"), ""))
                sDataEsercizio = sDataEsercizio & " AL " & Right(dataAl, 2) & "/" & Mid(dataAl, 5, 2) & "/" & Left(dataAl, 4)
            End If
            myReader1.Close()
            '****************************************************************************



            lblTitolo.Text = ""
            If idStruttura <> "-1" Then
                controlloStruttura = "AND PRENOTAZIONI.ID_STRUTTURA='" & idStruttura & "' "
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & idStruttura & "'"
                Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LETTORE.Read Then
                    lblTitolo.Text = "SITUAZIONE PAGAMENTI - STRUTTURA " & par.IfNull(LETTORE(0), "") & " - " & sDataEsercizio
                End If
                LETTORE.Close()
            Else
                lblTitolo.Text = "SITUAZIONE GENERALE PAGAMENTI " & " - " & sDataEsercizio
            End If

            '######## ELENCO DEI PAGAMENTI PER STRUTTURA ED ESERCIZIO FINANZIARIO ######
            par.cmd.CommandText = "SELECT PRENOTAZIONI.ID AS ID_PRE,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                & "PF_VOCI.CODICE AS CODICE,PF_VOCI.DESCRIZIONE AS VOCE," _
                & "(CASE WHEN PRENOTAZIONI.IMPORTO_APPROVATO IS NULL THEN TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_PRENOTATO,'999G999G990D99')) ELSE TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_APPROVATO,'999G999G990D99')) END) AS IMPORTO_PRENOTATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE PAGAMENTI.DESCRIZIONE END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(MANUTENZIONI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO," _
                & "TAB_STATI_ODL.DESCRIZIONE AS STATO_ODL," _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                & "PRENOTAZIONI.TIPO_PAGAMENTO AS TIPO_ADP," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "CASE WHEN PAGAMENTI.PROGR IS NOT NULL THEN PAGAMENTI.PROGR ||'/'|| PAGAMENTI.ANNO ELSE '' END  AS ADP," _
                & "TO_CHAR(TO_DATE(DATA_EMISSIONE,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_ADP," _
                & "TRIM(TO_CHAR(PAGAMENTI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO_ADP," _
                & "CASE WHEN TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) IS NULL THEN '0,00' ELSE TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) END AS RITLEGGE_ADP," _
                & "SISCOM_MI.GETSTATOPAGAMENTO(PAGAMENTI.ID) AS STATO_PAGAMENTO,PRENOTAZIONI.ID_APPALTO AS ID_A,PRENOTAZIONI.ID_FORNITORE AS ID_F,PRENOTAZIONI.ID_PAGAMENTO AS ID_P,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE " _
                & "FROM SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND TAB_STATI_PAGAMENTI.ID(+)=PAGAMENTI.ID_STATO " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND TAB_STATI_ODL.ID(+)=MANUTENZIONI.STATO " _
                & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & esercizioFinanziario & "' " _
                & controlloStruttura _
                & "AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "ORDER BY PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR ASC "

            Dim lettorePagamenti As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim RIGA As Data.DataRow
            Dim RIGAexcel As Data.DataRow
            dt.Columns.Clear()
            dt.Rows.Clear()
            dt.Columns.Add("CODICE")
            dt.Columns.Add("VOCE")
            dt.Columns.Add("DESCRIZIONE_TIPO")
            dt.Columns.Add("FILIALE")
            dt.Columns.Add("IMPORTO_PRENOTATO")
            dt.Columns.Add("DESCRIZIONE_PAG")
            dt.Columns.Add("IMPORTO")
            dt.Columns.Add("FORNITORE")
            dt.Columns.Add("TIPO_ADP")
            dt.Columns.Add("ADP")
            dt.Columns.Add("DATA_ADP")
            dt.Columns.Add("IMPORTO_ADP")
            dt.Columns.Add("RIT_LEGGE")
            dt.Columns.Add("ID_PAG")
            dt.Columns.Add("MAE")
            dt.Columns.Add("ID_PRE")


            '*************************
            'DT PER EXCEL
            dtExcel.Columns.Clear()
            dtExcel.Rows.Clear()
            dtExcel.Columns.Add("CODICE")
            dtExcel.Columns.Add("VOCE")
            dtExcel.Columns.Add("DESCRIZIONE_TIPO")
            dtExcel.Columns.Add("FILIALE")
            dtExcel.Columns.Add("IMPORTO_PRENOTATO")
            dtExcel.Columns.Add("DESCRIZIONE_PAG")
            dtExcel.Columns.Add("IMPORTO")
            dtExcel.Columns.Add("FORNITORE")
            dtExcel.Columns.Add("ADP")
            dtExcel.Columns.Add("DATA_ADP")
            dtExcel.Columns.Add("IMPORTO_ADP")
            dtExcel.Columns.Add("RIT_LEGGE")
            dtExcel.Columns.Add("ID_PAG")
            dtExcel.Columns.Add("NUM_MAE")
            dtExcel.Columns.Add("DATA_MAE")
            dtExcel.Columns.Add("IMPORTO_MAE")
            dtExcel.Columns.Add("ID_PRE")
            dtExcel.Columns.Add("ID_MAE")
            '*************************

            Dim i As Integer = 0
            Dim codice As String = ""
            Dim totaleImportoPrenotazione As Decimal = 0
            Dim totalePrenotazione As Decimal = 0
            Dim totaleImportoODL As Decimal = 0
            Dim totaleADP As Decimal = 0
            Dim totaleRITLEGGEADP As Decimal = 0
            Dim totaleImportoADP As Decimal = 0
            Dim adpPrec As String = ""
            Dim id_pag_prec As String = "-1"
            Dim id_mae_prec As String = "-1"
            Dim numeroStampa As String = ""
            While lettorePagamenti.Read
                i = i + 1
                RIGA = dt.NewRow
                RIGA.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                RIGA.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                RIGA.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                RIGA.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                RIGA.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                RIGA.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                RIGA.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                RIGA.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                RIGA.Item("TIPO_ADP") = par.IfNull(lettorePagamenti("TIPO_ADP"), "")
                If par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "3" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "4" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "6" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "7" Then
                    RIGA.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                Else
                    RIGA.Item("ADP") = "<a href=""javascript:ApriPagamenti(" & par.IfNull(lettorePagamenti("ID_P"), -1) & "," & par.IfNull(lettorePagamenti("TIPO_ADP"), 0) & ")"">" & par.IfNull(lettorePagamenti("ADP"), "") & "</a>"
                End If
                RIGA.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                RIGA.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                RIGA.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")
                RIGA.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                RIGA.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), "")

                '@@@@@@@@@@@@@@@@ MAE @@@@@@@@@@@@@@@@@
                par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.ID AS ID_MAE ,NUM_MANDATO ||'/'||ANNO_MANDATO AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) & "'"
                Dim lettoreMAE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim lettoreMAE2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim idmae As String = "-1"

                If lettoreMAE2.Read Then
                    idmae = par.IfNull(lettoreMAE2("ID_MAE"), "-1")
                End If
                lettoreMAE2.Close()
                RIGA.Item("MAE") = ""
                Dim check As Integer = 0
                Dim count As Integer = 0
                Dim importoTotale As Decimal = 0
                Dim enter As Boolean = False
                Dim MAE As Boolean = False

                If id_mae_prec = idmae Then
                    MAE = True
                End If

                While lettoreMAE.Read



                    RIGAexcel = dtExcel.NewRow
                    If check = 0 Then
                        RIGA.Item("MAE") = "<table style=""font-family: Arial, Helvetica, sans-serif; font-size: xx-small;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;"" bgcolor=""#EEEEEE"" width=""100%""><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">MAE</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">DATA</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">IMPORTO</th>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"

                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")
                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                            RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")

                        Else
                            If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                                RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        If MAE = False Then
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        check = 1
                        count = count + 1
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"


                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")

                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                            RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")

                        Else
                            If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                                RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        If MAE = False Then
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        count = count + 1
                    End If
                    dtExcel.Rows.Add(RIGAexcel)

                    importoTotale = importoTotale + par.IfNull(lettoreMAE("IMPORTO"), 0)
                End While
                lettoreMAE.Close()


                If RIGA.Item("MAE") <> "" Then
                    If count = 1 Then
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "</table>"
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td colspan='2' style=""border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">TOTALE</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(importoTotale, "##,##0.00") & "</td></tr>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "</table>"

                        RIGAexcel = dtExcel.NewRow

                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")

                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                            RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                        Else
                            If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                                RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        RIGAexcel.Item("NUM_MAE") = "TOTALE"
                        RIGAexcel.Item("DATA_MAE") = ""
                        RIGAexcel.Item("IMPORTO_MAE") = Format(importoTotale, "##,##0.00")
                        RIGAexcel.Item("ID_MAE") = -1
                        dtExcel.Rows.Add(RIGAexcel)

                    End If
                Else
                    RIGAexcel = dtExcel.NewRow

                    RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                    RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                    RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                    RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                    RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                    RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                    RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                    RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                    RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                    RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                    RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")

                    If i = 1 Then
                        RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                        RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                        RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                    Else
                        If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                            RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                        Else
                            RIGAexcel.Item("ADP") = ""
                            RIGAexcel.Item("DATA_ADP") = ""
                            RIGAexcel.Item("IMPORTO_ADP") = ""
                        End If
                    End If

                    RIGAexcel.Item("NUM_MAE") = ""
                    RIGAexcel.Item("DATA_MAE") = ""
                    RIGAexcel.Item("IMPORTO_MAE") = ""
                    RIGAexcel.Item("ID_MAE") = -1
                    dtExcel.Rows.Add(RIGAexcel)
                End If
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                dt.Rows.Add(RIGA)

                If idPagamentoPrec.Value <> par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) Then
                    totaleADP = totaleADP + CDec(par.IfNull(lettorePagamenti("IMPORTO_ADP"), 0))
                End If
                totaleRITLEGGEADP = totaleRITLEGGEADP + CDec(par.IfNull(lettorePagamenti("RITLEGGE_ADP"), 0))
                totalePrenotazione = totalePrenotazione + CDec(par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), 0))

                idPagamentoPrec.Value = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1)
                adpPrec = par.IfNull(lettorePagamenti("ADP"), "")
                id_mae_prec = idmae
            End While

            RIGA = dt.NewRow
            RIGA.Item("CODICE") = "TOTALE VOCI"
            RIGA.Item("VOCE") = ""
            RIGA.Item("FILIALE") = ""
            RIGA.Item("IMPORTO_PRENOTATO") = Format(totalePrenotazione, "##,##0.00")
            RIGA.Item("DESCRIZIONE_PAG") = ""
            RIGA.Item("IMPORTO") = ""
            RIGA.Item("FORNITORE") = ""
            RIGA.Item("ADP") = "TOTALE ADP + RIT.LEGGE"
            RIGA.Item("DATA_ADP") = Format(totaleADP + totaleRITLEGGEADP, "##,##0.00")
            RIGA.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGA.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGA.Item("ID_PAG") = ""
            RIGA.Item("MAE") = ""
            RIGA.Item("ID_PRE") = ""
            dt.Rows.Add(RIGA)

            RIGAexcel = dtExcel.NewRow
            RIGAexcel.Item("CODICE") = "TOTALE VOCI"
            RIGAexcel.Item("VOCE") = ""
            RIGAexcel.Item("FILIALE") = ""
            RIGAexcel.Item("DESCRIZIONE_TIPO") = ""
            RIGAexcel.Item("IMPORTO_PRENOTATO") = Format(totalePrenotazione, "##,##0.00")
            RIGAexcel.Item("DESCRIZIONE_PAG") = ""
            RIGAexcel.Item("IMPORTO") = ""
            RIGAexcel.Item("FORNITORE") = ""
            RIGAexcel.Item("ADP") = "TOTALE ADP + RIT.LEGGE"
            RIGAexcel.Item("DATA_ADP") = Format(totaleADP + totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("ID_PAG") = ""
            RIGAexcel.Item("NUM_MAE") = ""
            RIGAexcel.Item("DATA_MAE") = ""
            RIGAexcel.Item("IMPORTO_MAE") = ""
            RIGAexcel.Item("ID_PRE") = ""
            RIGAexcel.Item("ID_MAE") = ""
            dtExcel.Rows.Add(RIGAexcel)

            lettorePagamenti.Close()
            If dt.Rows.Count > 1 Then
                DataGrid1.Visible = True
                DataGrid1.DataSource = dt
                If idStruttura <> "-1" Then
                    DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "FILIALE")).Visible = False
                End If

                Session.Add("PAG_STRU", dtExcel)
                DataGrid1.DataBind()
                'If Request.QueryString("IDS") = "-1" Then
                '    Pagamento.Value = "1"
                'End If
                If idStruttura <> "-1" Then
                    DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "FILIALE")).Visible = False
                End If
                If Session.Item("BP_GENERALE") <> 1 Then
                    DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "MAE")).Visible = False
                End If
                For Each di As DataGridItem In DataGrid1.Items
                    If di.Cells(1).Text.Contains("TOTALE VOCI") Then
                        di.ForeColor = Drawing.Color.Red
                        di.BackColor = Drawing.Color.Lavender
                        For j As Integer = 0 To di.Cells.Count - 1
                            di.Cells(j).Font.Bold = True
                        Next
                    End If
                Next
            Else
                DataGrid1.Visible = False
                lblErrore.Text = "La ricerca di pagamenti non ha prodotto risultati"
            End If


            '*****************CHIUSURA CONNESSIONE***************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Response.Write(ex.Message)
            '*****************CHIUSURA CONNESSIONE***************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub stampaTabellaConsuntivato()
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim esercizioFinanziario As String = Request.QueryString("EF")
            Dim idStruttura As String = Request.QueryString("IDS")
            Dim controlloStruttura As String = ""
            Dim CONTROLLODATA As String = ""
            Dim CONTROLLODATA2 As String = ""
            If Not IsNothing(Request.QueryString("AL")) Then
                CONTROLLODATA = "AND DATA_PRENOTAZIONE <='" & Request.QueryString("AL") & "' "
                CONTROLLODATA2 = "AND DATA_MANDATO <='" & Request.QueryString("AL") & "' "
            End If

            '****************************************************************************
            Dim dataAl As Integer
            If Not IsNothing(Request.QueryString("AL")) Then
                dataAl = CInt(Request.QueryString("AL"))
                Dim dataoggi As Integer = CInt(Format(Now.Date, "yyyyMMdd"))
                dataAl = Min(dataoggi, dataAl)
            Else
                Dim dataoggi As Integer = CInt(Format(Now.Date, "yyyyMMdd"))
                dataAl = dataoggi
            End If
            '************ACQUISIZIONE INFORMAZIONI SUL PIANO FINANZIARIO*****************
            Dim sDataEsercizio As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                & "WHERE ID=(select ID_ESERCIZIO_FINANZIARIO from SISCOM_MI.PF_MAIN where ID=" & esercizioFinanziario & ") "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                sDataEsercizio = " DAL " & par.FormattaData(par.IfNull(myReader1("INIZIO"), ""))
                sDataEsercizio = sDataEsercizio & " AL " & Right(dataAl, 2) & "/" & Mid(dataAl, 5, 2) & "/" & Left(dataAl, 4)
            End If
            myReader1.Close()
            '****************************************************************************

            lblTitolo.Text = ""
            If idStruttura <> "-1" Then
                controlloStruttura = "AND PRENOTAZIONI.ID_STRUTTURA='" & idStruttura & "' "
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & idStruttura & "'"
                Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LETTORE.Read Then
                    lblTitolo.Text = "SITUAZIONE CONSUNTIVATO - STRUTTURA " & par.IfNull(LETTORE(0), "") & " - " & sDataEsercizio
                End If
                LETTORE.Close()
            Else
                lblTitolo.Text = "SITUAZIONE GENERALE CONSUNTIVATO - " & sDataEsercizio
            End If

            '######## ELENCO DELLE PRENOTAZIONI PER ESERCIZIO FINANZIARIO STRUTTURA E VOCE SELEZIONATA ######
            par.cmd.CommandText = "SELECT PRENOTAZIONI.ID AS ID_PRE,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                & "PF_VOCI.CODICE AS CODICE,PF_VOCI.DESCRIZIONE AS VOCE," _
                & "IMPORTO_APPROVATO AS IMPORTO_PRENOTATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE PAGAMENTI.DESCRIZIONE END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(MANUTENZIONI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO," _
                & "TAB_STATI_ODL.DESCRIZIONE AS STATO_ODL," _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                & "PRENOTAZIONI.TIPO_PAGAMENTO AS TIPO_ADP," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "CASE WHEN PAGAMENTI.PROGR IS NOT NULL THEN PAGAMENTI.PROGR ||'/'|| PAGAMENTI.ANNO ELSE '' END  AS ADP," _
                & "TO_CHAR(TO_DATE(DATA_EMISSIONE,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_ADP," _
                & "TRIM(TO_CHAR(PAGAMENTI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO_ADP," _
                & "CASE WHEN TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) IS NULL THEN '0,00' ELSE TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) END AS RITLEGGE_ADP," _
                & "SISCOM_MI.GETSTATOPAGAMENTO(PAGAMENTI.ID) AS STATO_PAGAMENTO,PRENOTAZIONI.ID_APPALTO AS ID_A,PRENOTAZIONI.ID_FORNITORE AS ID_F,PRENOTAZIONI.ID_PAGAMENTO AS ID_P,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE " _
                & "FROM SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND TAB_STATI_PAGAMENTI.ID(+)=PAGAMENTI.ID_STATO " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND TAB_STATI_ODL.ID(+)=MANUTENZIONI.STATO " _
                & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & esercizioFinanziario & "' " _
                & "AND PF_VOCI.ID IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & Request.QueryString("IDV") & "') " _
                & controlloStruttura _
                & CONTROLLODATA _
                & "AND ((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PAGAMENTI.ID " & CONTROLLODATA2 & "  AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PF_VOCI.ID) = 0 OR " _
                & "(SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PAGAMENTI.ID " & CONTROLLODATA2 & "  AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PF_VOCI.ID) IS NULL " _
                & ") " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "ORDER BY PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR ASC "

            Dim lettorePagamenti As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim RIGA As Data.DataRow
            Dim RIGAexcel As Data.DataRow
            dt.Columns.Clear()
            dt.Rows.Clear()
            dt.Columns.Add("CODICE")
            dt.Columns.Add("VOCE")
            dt.Columns.Add("DESCRIZIONE_TIPO")
            dt.Columns.Add("FILIALE")
            dt.Columns.Add("IMPORTO_PRENOTATO")
            dt.Columns.Add("DESCRIZIONE_PAG")
            dt.Columns.Add("IMPORTO")
            dt.Columns.Add("FORNITORE")
            dt.Columns.Add("TIPO_ADP")
            dt.Columns.Add("ADP")
            dt.Columns.Add("DATA_ADP")
            dt.Columns.Add("IMPORTO_ADP")
            dt.Columns.Add("RIT_LEGGE")
            dt.Columns.Add("ID_PAG")
            dt.Columns.Add("MAE")
            dt.Columns.Add("ID_PRE")

            '*************************
            'DT PER EXCEL
            dtExcel.Columns.Clear()
            dtExcel.Rows.Clear()
            dtExcel.Columns.Add("CODICE")
            dtExcel.Columns.Add("VOCE")
            dtExcel.Columns.Add("DESCRIZIONE_TIPO")
            dtExcel.Columns.Add("FILIALE")
            dtExcel.Columns.Add("IMPORTO_PRENOTATO")
            dtExcel.Columns.Add("DESCRIZIONE_PAG")
            dtExcel.Columns.Add("IMPORTO")
            dtExcel.Columns.Add("FORNITORE")
            dtExcel.Columns.Add("ADP")
            dtExcel.Columns.Add("DATA_ADP")
            dtExcel.Columns.Add("IMPORTO_ADP")
            dtExcel.Columns.Add("RIT_LEGGE")
            dtExcel.Columns.Add("ID_PAG")
            dtExcel.Columns.Add("NUM_MAE")
            dtExcel.Columns.Add("DATA_MAE")
            dtExcel.Columns.Add("IMPORTO_MAE")
            dtExcel.Columns.Add("ID_PRE")
            dtExcel.Columns.Add("ID_MAE")
            '*************************

            Dim i As Integer = 0
            Dim codice As String = ""
            Dim totaleImportoPrenotazione As Decimal = 0
            Dim totalePrenotazione As Decimal = 0
            Dim totaleImportoODL As Decimal = 0
            Dim totaleADP As Decimal = 0
            Dim totaleRITLEGGEADP As Decimal = 0
            Dim totaleImportoADP As Decimal = 0
            Dim adpPrec As String = ""
            Dim id_pag_prec As String = "-1"
            Dim id_mae_prec As String = "-1"

            While lettorePagamenti.Read


                i = i + 1
                RIGA = dt.NewRow
                RIGA.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                RIGA.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                RIGA.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                RIGA.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                RIGA.Item("IMPORTO_PRENOTATO") = Format(par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), ""), "##,##0.00")
                RIGA.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                RIGA.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                RIGA.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                RIGA.Item("TIPO_ADP") = par.IfNull(lettorePagamenti("TIPO_ADP"), "")
                If par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "3" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "4" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "6" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "7" Then
                    RIGA.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                Else
                    RIGA.Item("ADP") = "<a href=""javascript:ApriPagamenti(" & par.IfNull(lettorePagamenti("ID_P"), -1) & "," & par.IfNull(lettorePagamenti("TIPO_ADP"), 0) & ")"">" & par.IfNull(lettorePagamenti("ADP"), "") & "</a>"
                End If
                RIGA.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                RIGA.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                RIGA.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")
                RIGA.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                RIGA.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), "")

                '@@@@@@@@@@@@@@@@ MAE @@@@@@@@@@@@@@@@@
                par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.ID AS ID_MAE ,NUM_MANDATO ||'/'||ANNO_MANDATO AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) & "'"
                Dim lettoreMAE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim lettoreMAE2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim idmae As String = "-1"

                If lettoreMAE2.Read Then
                    idmae = par.IfNull(lettoreMAE2("ID_MAE"), "-1")
                End If
                lettoreMAE2.Close()
                RIGA.Item("MAE") = ""
                Dim check As Integer = 0
                Dim count As Integer = 0
                Dim importoTotale As Decimal = 0
                Dim enter As Boolean = False
                Dim MAE As Boolean = False
                If id_mae_prec = idmae Then
                    MAE = True
                End If
                While lettoreMAE.Read
                    RIGAexcel = dtExcel.NewRow
                    If check = 0 Then
                        RIGA.Item("MAE") = "<table style=""font-family: Arial, Helvetica, sans-serif; font-size: xx-small;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;"" bgcolor=""#EEEEEE"" width=""100%""><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">MAE</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">DATA</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">IMPORTO</th>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"

                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")
                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                            RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")

                        Else
                            If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                                RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        If MAE = False Then
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        check = 1
                        count = count + 1
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"


                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")

                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                            RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")

                        Else
                            If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                                RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        If MAE = False Then
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        count = count + 1
                    End If
                    dtExcel.Rows.Add(RIGAexcel)

                    importoTotale = importoTotale + par.IfNull(lettoreMAE("IMPORTO"), 0)
                End While
                lettoreMAE.Close()


                If RIGA.Item("MAE") <> "" Then
                    If count = 1 Then
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "</table>"
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td colspan='2' style=""border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">TOTALE</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(importoTotale, "##,##0.00") & "</td></tr>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "</table>"

                        RIGAexcel = dtExcel.NewRow

                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")

                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                            RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                        Else
                            If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                                RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        RIGAexcel.Item("NUM_MAE") = "TOTALE"
                        RIGAexcel.Item("DATA_MAE") = ""
                        RIGAexcel.Item("IMPORTO_MAE") = Format(importoTotale, "##,##0.00")
                        RIGAexcel.Item("ID_MAE") = -1
                        dtExcel.Rows.Add(RIGAexcel)

                    End If
                Else
                    RIGAexcel = dtExcel.NewRow

                    RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                    RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                    RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                    RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                    RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                    RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                    RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                    RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                    RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                    RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), -1)
                    RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")

                    If i = 1 Then
                        RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                        RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                        RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                    Else
                        If par.IfNull(lettorePagamenti("ADP"), "") <> adpPrec Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                            RIGAexcel.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                        Else
                            RIGAexcel.Item("ADP") = ""
                            RIGAexcel.Item("DATA_ADP") = ""
                            RIGAexcel.Item("IMPORTO_ADP") = ""
                        End If
                    End If

                    RIGAexcel.Item("NUM_MAE") = ""
                    RIGAexcel.Item("DATA_MAE") = ""
                    RIGAexcel.Item("IMPORTO_MAE") = ""
                    RIGAexcel.Item("ID_MAE") = -1
                    dtExcel.Rows.Add(RIGAexcel)
                End If
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                dt.Rows.Add(RIGA)

                If idPagamentoPrec.Value <> par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) Then
                    totaleADP = totaleADP + CDec(par.IfNull(lettorePagamenti("IMPORTO_ADP"), 0))
                End If
                totaleRITLEGGEADP = totaleRITLEGGEADP + CDec(par.IfNull(lettorePagamenti("RITLEGGE_ADP"), 0))
                totalePrenotazione = totalePrenotazione + CDec(par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), 0))

                idPagamentoPrec.Value = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1)
                adpPrec = par.IfNull(lettorePagamenti("ADP"), "")
                id_mae_prec = idmae
            End While

            RIGA = dt.NewRow
            RIGA.Item("CODICE") = "TOTALE VOCI"
            RIGA.Item("VOCE") = ""
            RIGA.Item("FILIALE") = ""
            RIGA.Item("IMPORTO_PRENOTATO") = Format(totalePrenotazione, "##,##0.00")
            RIGA.Item("DESCRIZIONE_PAG") = ""
            RIGA.Item("IMPORTO") = ""
            RIGA.Item("FORNITORE") = ""
            RIGA.Item("ADP") = "TOTALE ADP + RIT.LEGGE"
            RIGA.Item("DATA_ADP") = Format(totaleADP + totaleRITLEGGEADP, "##,##0.00")
            RIGA.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGA.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGA.Item("ID_PAG") = ""
            RIGA.Item("MAE") = ""
            RIGA.Item("ID_PRE") = ""
            dt.Rows.Add(RIGA)

            RIGAexcel = dtExcel.NewRow
            RIGAexcel.Item("CODICE") = "TOTALE VOCI"
            RIGAexcel.Item("VOCE") = ""
            RIGAexcel.Item("FILIALE") = ""
            RIGAexcel.Item("DESCRIZIONE_TIPO") = ""
            RIGAexcel.Item("IMPORTO_PRENOTATO") = Format(totalePrenotazione, "##,##0.00")
            RIGAexcel.Item("DESCRIZIONE_PAG") = ""
            RIGAexcel.Item("IMPORTO") = ""
            RIGAexcel.Item("FORNITORE") = ""
            RIGAexcel.Item("ADP") = "TOTALE ADP + RIT.LEGGE"
            RIGAexcel.Item("DATA_ADP") = Format(totaleADP + totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("ID_PAG") = ""
            RIGAexcel.Item("NUM_MAE") = ""
            RIGAexcel.Item("DATA_MAE") = ""
            RIGAexcel.Item("IMPORTO_MAE") = ""
            RIGAexcel.Item("ID_PRE") = ""
            RIGAexcel.Item("ID_MAE") = ""
            dtExcel.Rows.Add(RIGAexcel)

            lettorePagamenti.Close()
            If dt.Rows.Count > 1 Then

                DataGrid1.Visible = True
                DataGrid1.DataSource = dt
                If idStruttura <> "-1" Then
                    DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "FILIALE")).Visible = False
                End If
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "DESCRIZIONE_PAG")).Visible = True
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "DATA_ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "IMPORTO_ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "RIT_LEGGE")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "MAE")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "IMPORTO_PRENOTATO")).HeaderText = "IMPORTO CONSUNTIVATO"
                DataGrid1.AllowPaging = False

                Session.Add("PAG_STRU", dtExcel)
                DataGrid1.DataBind()
                For Each di As DataGridItem In DataGrid1.Items
                    If di.Cells(1).Text.Contains("TOTALE VOCI") Then
                        di.ForeColor = Drawing.Color.Red
                        di.BackColor = Drawing.Color.Lavender
                        For j As Integer = 0 To di.Cells.Count - 1
                            di.Cells(j).Font.Bold = True
                        Next
                    End If
                Next
            Else
                DataGrid1.Visible = False
                lblErrore.Text = "La ricerca di prenotazioni non ha prodotto risultati"
            End If
            '*****************CHIUSURA CONNESSIONE***************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Response.Write(ex.Message)
            '*****************CHIUSURA CONNESSIONE***************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub


    Protected Sub stampaPDF()
        Try
            '*******************************************************************************************************
            '********************************************* STAMPA PDF **********************************************
            '*******************************************************************************************************
            Dim NomeFile As String = "SITCONTABILE_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)
            Me.DataGrid1.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString
            Html = eliminaLink(Html)
            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
            Dim pdfConverter As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter.LicenseKey = Licenza
            End If
            pdfConverter.PageWidth = "1600"
            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter.PdfDocumentOptions.LeftMargin = 20
            pdfConverter.PdfDocumentOptions.RightMargin = 20
            pdfConverter.PdfDocumentOptions.TopMargin = 5
            pdfConverter.PdfDocumentOptions.BottomMargin = 5
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter.PdfHeaderOptions.HeaderText = lblTitolo.Text
            pdfConverter.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter.PdfHeaderOptions.HeaderTextFontSize = 14
            pdfConverter.PdfHeaderOptions.HeaderTextColor = Color.Black
            pdfConverter.PdfDocumentOptions.ShowHeader = True
            pdfConverter.PdfFooterOptions.FooterText = ("")
            pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter.PdfFooterOptions.DrawFooterLine = False
            pdfConverter.PdfFooterOptions.PageNumberText = ""
            pdfConverter.PdfFooterOptions.ShowPageNumber = False
            pdfConverter.PdfDocumentOptions.ShowFooter = True
            pdfConverter.SavePdfFromHtmlStringToFile(Html, url & NomeFile)
            DataGrid1.HeaderStyle.ForeColor = Color.White
            Response.Write("<script>self.close();window.open('..\\..\\..\\FileTemp\\" & NomeFile & "','Stampe');</script>")
            '*******************************************************************************************************
        Catch ex As Exception
            Response.Write("<script>alert('Si è verificato un errore durante la stampa del documento!');self.close();</script>")
        End Try
        
    End Sub

    Protected Sub DataGrid1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.PreRender
        'UNISCO LE CELLE DELLE COLONNE ADP DATA E MAE CHE HANNO LO STESSO ID_PAGAMENTO
        Dim count As Integer = 0
        Dim memIDpag As Long = 0
        Dim cella0 As Long
        Dim cellainizio As Integer = TrovaIndiceColonna(DataGrid1, "ADP")
        Dim cellafine As Integer = TrovaIndiceColonna(DataGrid1, "IMPORTO_ADP")
        For j As Integer = DataGrid1.Items.Count - 1 To 0 Step -1
            If j = DataGrid1.Items.Count - 1 Then
                Try
                    cella0 = CLng(DataGrid1.Items(j).Cells(0).Text)
                Catch ex As Exception
                    cella0 = -1
                End Try
                memIDpag = cella0
            Else
                Try
                    cella0 = CLng(DataGrid1.Items(j).Cells(0).Text)
                Catch ex As Exception
                    cella0 = -1
                End Try
                If cella0 = memIDpag And memIDpag <> -1 Then
                    If DataGrid1.Items(j + 1).Cells(cellainizio).RowSpan = 0 Then
                        For g As Integer = cellainizio To cellafine
                            DataGrid1.Items(j).Cells(g).RowSpan = DataGrid1.Items(j + 1).Cells(g).RowSpan + 2
                        Next
                    Else
                        For g As Integer = cellainizio To cellafine
                            DataGrid1.Items(j).Cells(g).RowSpan = DataGrid1.Items(j + 1).Cells(g).RowSpan + 1
                        Next
                    End If
                    For g As Integer = cellainizio To cellafine
                        DataGrid1.Items(j + 1).Cells(g).RowSpan = 0
                        DataGrid1.Items(j + 1).Cells(g).Visible = False

                    Next
                End If
                memIDpag = cella0
            End If
        Next
        'PER IL MAE
        cellainizio = TrovaIndiceColonna(DataGrid1, "MAE")
        cellafine = TrovaIndiceColonna(DataGrid1, "MAE")
        For j As Integer = DataGrid1.Items.Count - 1 To 0 Step -1
            If j = DataGrid1.Items.Count - 1 Then
                Try
                    cella0 = CLng(DataGrid1.Items(j).Cells(0).Text)
                Catch ex As Exception
                    cella0 = -1
                End Try
                memIDpag = cella0
            Else
                Try
                    cella0 = CLng(DataGrid1.Items(j).Cells(0).Text)
                Catch ex As Exception
                    cella0 = -1
                End Try
                If cella0 = memIDpag And memIDpag <> -1 Then
                    If DataGrid1.Items(j + 1).Cells(cellainizio).RowSpan = 0 Then
                        For g As Integer = cellainizio To cellafine
                            DataGrid1.Items(j).Cells(g).RowSpan = DataGrid1.Items(j + 1).Cells(g).RowSpan + 2
                        Next
                    Else
                        For g As Integer = cellainizio To cellafine
                            DataGrid1.Items(j).Cells(g).RowSpan = DataGrid1.Items(j + 1).Cells(g).RowSpan + 1
                        Next
                    End If
                    For g As Integer = cellainizio To cellafine
                        DataGrid1.Items(j + 1).Cells(g).RowSpan = 0
                        DataGrid1.Items(j + 1).Cells(g).Visible = False
                    Next
                End If
                memIDpag = cella0
            End If
        Next
        stampaPDF()
    End Sub

    Private Sub Datagrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        Dim indice = TrovaIndiceColonna(DataGrid1, "ADP")
        If e.Item.ItemType = ListItemType.Header Then
            e.Item.Cells(indice).ColumnSpan = 4
            e.Item.Cells(indice + 1).Visible = False
            e.Item.Cells(indice + 2).Visible = False
            e.Item.Cells(indice + 3).Visible = False
            e.Item.Cells(indice).Text = "<table style='color: #FFFFFF; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: x-small;' width='100%'><tr><td colspan='4' align='center'>ADP</td></tr><tr><td style='width:25%' align='center'>NUM</td><td style='width:25%' align='center'>DATA</td><td style='width:25%' align='center'>IMPORTO</td><td style='width:25%' align='center'>FONDO<br />RIT.LEGGE</td></tr></table>"
        End If
    End Sub

    Function TrovaIndiceColonna(ByVal dgv As DataGrid, ByVal nameCol As String) As Integer
        TrovaIndiceColonna = -1
        Dim Indice As Integer = 0
        Try
            For Each c As DataGridColumn In dgv.Columns
                If String.Equals(nameCol, DirectCast(c, System.Web.UI.WebControls.BoundColumn).DataField, StringComparison.InvariantCultureIgnoreCase) Then
                    TrovaIndiceColonna = Indice
                    Exit For
                End If
                Indice = Indice + 1
            Next

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "TrovaIndiceColonna - " & ex.Message
        End Try

        Return TrovaIndiceColonna

    End Function

    Public Function eliminaLink(ByVal html As String) As String
        Dim check As Integer = 0
        Dim nuovoHtml_parte1 As String = ""
        Dim nuovoHtml_parte2 As String = ""
        Dim nuovoHtml As String = html
        While check = 0
            Dim inizioStringa As Integer = 0
            Dim fineStringa As Integer = nuovoHtml.IndexOf("<a href")
            If fineStringa <> -1 Then
                nuovoHtml_parte1 = nuovoHtml.Substring(inizioStringa, fineStringa)
                nuovoHtml_parte2 = nuovoHtml.Substring(fineStringa + 1)
                Dim finestringa_parte2 As Integer = nuovoHtml_parte2.IndexOf(">")
                nuovoHtml = Replace(nuovoHtml_parte1 & nuovoHtml_parte2.Substring(finestringa_parte2 + 1), "</a>", "")
            Else
                check = 1
            End If
        End While
        Return nuovoHtml
    End Function

End Class
