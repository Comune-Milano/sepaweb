Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports System.Math
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiPagamentiPerStruttura
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable
    Dim dtExcel As New Data.DataTable
    Dim dtExcelrit As New Data.DataTable
    Dim Str As String = ""
    Dim ANNO As String = ""
    Public percentuale As Long = 0
    Dim condizionePU As String = ""
    Dim condizionePU2 As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        'Dim Str As String
        'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        'Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        'Str = Str & "<" & "/div>"
        'Response.Write(Str)
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("PU")) Then
                If Request.QueryString("PU") = 0 Then
                    condizionePU = " PRENOTAZIONI.TIPO_PAGAMENTO NOT IN (12,13) AND "
                    condizionePU2 = " B.TIPO_PAGAMENTO NOT IN (12,13) AND "
                Else
                    condizionePU = ""
                    condizionePU2 = ""
                End If
            Else
                condizionePU = ""
                condizionePU2 = ""
            End If
            URLdiProvenienza.Value = Request.ServerVariables("HTTP_REFERER")
            'Response.Flush()
            If Request.QueryString("RITCERT") = 1 Then
                StampaTabellaRitCert()
            Else
                If Request.QueryString("RIT") = 1 Then
                    StampaTabellaRit()
                Else
                    If Request.QueryString("RITLIQ") = 1 Then
                        StampaTabellaRitLiq()
                    Else
                        If Request.QueryString("CONS") = 1 Then
                            stampaTabellaConsuntivato()
                        Else
                            If Request.QueryString("CERT") = 1 Then
                                stampaTabellaCertificato()
                                stampaRitenuteLeggeCertificate()
                            Else
                                If Request.QueryString("LIQ") = 1 Then
                                    StampaTabellaLiquidazioni()
                                Else
                                    If Not IsNothing(Request.QueryString("IDV")) Then
                                        StampaTabellaPrenotazioni()
                                    Else
                                        If Session.Item("BP_GENERALE") = 1 Then
                                            StampaTabellaPagamenti()
                                            'StampaTabellaPagamentiRitLegge()
                                        Else
                                            If Session.Item("ID_STRUTTURA") = Request.QueryString("IDS") Then
                                                StampaTabellaPagamenti()
                                            Else
                                                Response.Write("<script>top.location.href=""../../../AccessoNegato.htm""</script>")
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If


        End If
    End Sub
    '################################################# PRENOTAZIONI ####################################################

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
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
                CONTROLLODATA = "AND DATA_PRENOTAZIONE <='" & Request.QueryString("AL") & "' "
            End If
            Dim DATAALS As String = ""
            '****************************************************************************
            Dim dataAl As Integer
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
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
                DATAALS = CStr(Min(CLng(Format(Now, "yyyyMMdd")), CLng(par.IfNull(myReader1("FINE"), ""))))
                ANNO = Left(par.IfNull(myReader1("INIZIO"), ""), 4)
            End If
            myReader1.Close()
            '****************************************************************************

            If DATAALS <> "" And CONTROLLODATA = "" Then
                CONTROLLODATA = "AND DATA_PRENOTAZIONE <='" & DATAALS & "' "
                dataAl = DATAALS
            End If
            sDataEsercizio = sDataEsercizio & " AL " & Right(DATAALS, 2) & "/" & Mid(DATAALS, 5, 2) & "/" & Left(DATAALS, 4)


            lblTitolo.Text = ""
            If idStruttura <> "-1" Then
                controlloStruttura = "AND PRENOTAZIONI.ID_STRUTTURA='" & idStruttura & "' "
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & idStruttura & "'"
                Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LETTORE.Read Then
                    lblTitolo.Text = "SITUAZIONE IMPORTI PRENOTATI - STRUTTURA " & par.IfNull(LETTORE(0), "") & " - " & sDataEsercizio
                End If
                LETTORE.Close()
            Else
                lblTitolo.Text = "SITUAZIONE GENERALE IMPORTI PRENOTATI - " & sDataEsercizio
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
                & "SISCOM_MI.GETSTATOPAGAMENTO(PAGAMENTI.ID) AS STATO_PAGAMENTO,PRENOTAZIONI.ID_APPALTO AS ID_A,PRENOTAZIONI.ID_FORNITORE AS ID_F,PRENOTAZIONI.ID_PAGAMENTO AS ID_P,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND TAB_STATI_PAGAMENTI.ID(+)=PAGAMENTI.ID_STATO " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND TAB_STATI_ODL.ID(+)=MANUTENZIONI.STATO " _
                & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & esercizioFinanziario & "' " _
                & "AND PF_VOCI.ID IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & Request.QueryString("IDV") & "') " _
                & controlloStruttura _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND (PRENOTAZIONI.ID_STATO=0 OR " _
                & "(PRENOTAZIONI.ID_STATO = 1 AND DATA_CONSUNTIVAZIONE >'" & dataAl & "') OR " _
                & "(PRENOTAZIONI.ID_STATO = 2 AND DATA_CERTIFICAZIONE >'" & dataAl & "' AND DATA_CONSUNTIVAZIONE > '" & dataAl & "') OR " _
                & "(PRENOTAZIONI.ID_STATO=-3 AND PRENOTAZIONI.DATA_ANNULLO>'" & dataAl & "' AND (DATA_CONSUNTIVAZIONE IS NULL OR DATA_CONSUNTIVAZIONE>'" & dataAl & "') AND (DATA_CERTIFICAZIONE IS NULL OR DATA_CERTIFICAZIONE>'" & dataAl & "'))) " _
                & "AND (PRENOTAZIONI.IMPORTO_PRENOTATO>0 OR PRENOTAZIONI.IMPORTO_APPROVATO>0) " _
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
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
            dt.Columns.Add("NUM_REPERTORIO")
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
            dtExcel.Columns.Add("NUM_REPERTORIO")
            dtExcel.Columns.Add("ADP")
            dtExcel.Columns.Add("DATA_ADP")
            dtExcel.Columns.Add("IMPORTO_ADP")
            dtExcel.Columns.Add("RIT_LEGGE")
            dtExcel.Columns.Add("ID_PAG")
            dtExcel.Columns.Add("CUP")
            dtExcel.Columns.Add("CIG")
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
                RIGA.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.ID AS ID_MAE ," _
                    & " (SELECT CIG FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CIG," _
                    & " (SELECT CUP FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CUP," _
                    & " /*(SELECT CIG FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS CIG," _
                    & " (SELECT CUP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS CUP,*/" _
                    & " (SELECT NUMERO_PAGAMENTO||'/'||ANNO_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
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
                        RIGA.Item("MAE") = "<table style=""font-family: Arial, Helvetica, sans-serif; font-size: xx-small;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;"" bgcolor=""#EEEEEE"" width=""100%""><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CUP</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CIG</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">PROTOCOLLO</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">DATA</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">IMPORTO</th>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"

                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        check = 1
                        count = count + 1
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"


                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
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
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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

                        RIGAexcel.Item("CUP") = "TOTALE"
                        RIGAexcel.Item("CIG") = ""
                        RIGAexcel.Item("NUM_MAE") = ""
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
                    RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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

                    RIGAexcel.Item("CUP") = ""
                    RIGAexcel.Item("CIG") = ""
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
            RIGA.Item("NUM_REPERTORIO") = ""
            RIGA.Item("ADP") = "TOTALE ADP"
            RIGA.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
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
            RIGAexcel.Item("NUM_REPERTORIO") = ""
            RIGAexcel.Item("ADP") = "TOTALE ADP"
            RIGAexcel.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("ID_PAG") = ""
            RIGAexcel.Item("CUP") = ""
            RIGAexcel.Item("CIG") = ""
            RIGAexcel.Item("NUM_MAE") = ""
            RIGAexcel.Item("DATA_MAE") = ""
            RIGAexcel.Item("IMPORTO_MAE") = ""
            RIGAexcel.Item("ID_PRE") = ""
            RIGAexcel.Item("ID_MAE") = ""
            dtExcel.Rows.Add(RIGAexcel)

            lettorePagamenti.Close()
            If dt.Rows.Count > 1 Then

                DataGrid1.Visible = True
                btnExport.Visible = True
                btnStampaPDF.Visible = True
                lblErrore.Text = ""
                DataGrid1.DataSource = dt
                If idStruttura <> "-1" Then
                    DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "FILIALE")).Visible = False
                End If
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "DESCRIZIONE_PAG")).Visible = True
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "DATA_ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "IMPORTO_ADP")).Visible = False
                'DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "RIT_LEGGE")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "MAE")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "IMPORTO_PRENOTATO")).HeaderText = "IMPORTO PRENOTATO"
                DataGrid1.AllowPaging = False

                'Session.Add("PAG_STRU", dtExcel)
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
                btnExport.Visible = False
                btnStampaPDF.Visible = False
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

    Protected Sub esportaPrenotazioni()
        EsportaExcelAutomaticoDaDataGrid(DataGrid1, "importiPrenotati")
        ''#### EXPORT IN EXCEL ####
        'Try
        '    Dim myExcelFile As New CM.ExcelFile
        '    Dim i As Long
        '    Dim K As Long
        '    Dim sNomeFile As String
        '    Dim row As System.Data.DataRow
        '    Dim datatable As Data.DataTable
        '    datatable = Session.Item("PAG_STRU")
        '    'datatable = DT
        '    sNomeFile = "PRE_" & Format(Now, "yyyyMMddHHmmss")
        '    i = 0
        '    Dim nRighe As Integer = datatable.Rows.Count
        '    With myExcelFile
        '        .CreateFile(Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls"))
        '        .PrintGridLines = False
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
        '        .SetDefaultRowHeight(14)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
        '        .SetFont("Arial", 12, CM.ExcelFile.FontFormatting.xlsBold)
        '        .SetColumnWidth(1, 1, 14) 'codice
        '        .SetColumnWidth(2, 2, 70) 'voce
        '        .SetColumnWidth(3, 3, 70) 'tipo
        '        .SetColumnWidth(4, 4, 50) 'tipo
        '        .SetColumnWidth(5, 5, 60) 'filiale
        '        .SetColumnWidth(6, 6, 22) 'importo prenotazione
        '        .SetColumnWidth(7, 7, 52) 'fornitore
        '        K = 1
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont3, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, lblTitolo.Text)
        '        K = K + 1
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "CODICE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "VOCE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "TIPO PAGAMENTO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "DESCRIZIONE PAGAMENTO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "FILIALE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "IMPORTO PRENOTATO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "FORNITORE", 0)
        '        K = K + 1
        '        For Each row In datatable.Rows
        '            If row.Item("CODICE") = "TOTALE VOCI" Then
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, " ", 0)

        '            Else
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, row.Item("VOCE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, row.Item("DESCRIZIONE_TIPO"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, row.Item("DESCRIZIONE_PAG"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, row.Item("FILIALE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, row.Item("FORNITORE"), 0)
        '            End If
        '            i = i + 1
        '            K = K + 1
        '        Next
        '        .CloseFile()
        '    End With
        '    Dim objCrc32 As New Crc32()
        '    Dim strmZipOutputStream As ZipOutputStream
        '    Dim zipfic As String
        '    zipfic = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".zip")
        '    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        '    strmZipOutputStream.SetLevel(6)
        '    Dim strFile As String
        '    strFile = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls")
        '    Dim strmFile As FileStream = File.OpenRead(strFile)
        '    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '    strmFile.Read(abyBuffer, 0, abyBuffer.Length)
        '    Dim sFile As String = Path.GetFileName(strFile)
        '    Dim theEntry As ZipEntry = New ZipEntry(sFile)
        '    Dim fi As New FileInfo(strFile)
        '    theEntry.DateTime = fi.LastWriteTime
        '    theEntry.Size = strmFile.Length
        '    strmFile.Close()
        '    objCrc32.Reset()
        '    objCrc32.Update(abyBuffer)
        '    theEntry.Crc = objCrc32.Value
        '    strmZipOutputStream.PutNextEntry(theEntry)
        '    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        '    strmZipOutputStream.Finish()
        '    strmZipOutputStream.Close()
        '    File.Delete(strFile)
        '    Response.Redirect("..\..\..\FileTemp\" & sNomeFile & ".zip")
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub

    '###################################################################################################################
    '################################################# CONSUNTIVATO ####################################################

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
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
                CONTROLLODATA = "AND DATA_CONSUNTIVAZIONE <='" & Request.QueryString("AL") & "' "
            End If
            Dim DATAALS As String = ""
            '****************************************************************************
            Dim dataAl As Integer
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
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
                DATAALS = CStr(Min(CLng(Format(Now, "yyyyMMdd")), CLng(par.IfNull(myReader1("FINE"), ""))))
                ANNO = Left(par.IfNull(myReader1("INIZIO"), ""), 4)

            End If
            myReader1.Close()
            '****************************************************************************
            If DATAALS <> "" And CONTROLLODATA = "" Then
                CONTROLLODATA = "AND DATA_CONSUNTIVAZIONE <='" & DATAALS & "' "
                dataAl = DATAALS
            End If
            sDataEsercizio = sDataEsercizio & " AL " & Right(dataAl, 2) & "/" & Mid(dataAl, 5, 2) & "/" & Left(dataAl, 4)

            lblTitolo.Text = ""
            If idStruttura <> "-1" Then
                controlloStruttura = "AND PRENOTAZIONI.ID_STRUTTURA='" & idStruttura & "' "
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & idStruttura & "'"
                Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LETTORE.Read Then
                    lblTitolo.Text = "SITUAZIONE IMPORTI CONSUNTIVATI - STRUTTURA " & par.IfNull(LETTORE(0), "") & " - " & sDataEsercizio
                End If
                LETTORE.Close()
            Else
                lblTitolo.Text = "SITUAZIONE GENERALE IMPORTI CONSUNTIVATI - " & sDataEsercizio
            End If

            '######## ELENCO DELLE PRENOTAZIONI PER ESERCIZIO FINANZIARIO STRUTTURA E VOCE SELEZIONATA ######
            par.cmd.CommandText = "SELECT PRENOTAZIONI.ID AS ID_PRE,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                & "PF_VOCI.CODICE AS CODICE,PF_VOCI.DESCRIZIONE AS VOCE," _
                & "NVL(IMPORTO_APPROVATO,0)-NVL(RIT_LEGGE_IVATA,0) as IMPORTO_PRENOTATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE PAGAMENTI.DESCRIZIONE END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(MANUTENZIONI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO2," _
                & "TRIM(TO_CHAR(NVL(PRENOTAZIONI.IMPORTO_APPROVATO,0)-NVL(PRENOTAZIONI.RIT_LEGGE_IVATA,0),'999G999G990D99')) AS IMPORTO," _
                & "TAB_STATI_ODL.DESCRIZIONE AS STATO_ODL," _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                & "PRENOTAZIONI.TIPO_PAGAMENTO AS TIPO_ADP," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "CASE WHEN PAGAMENTI.PROGR IS NOT NULL THEN PAGAMENTI.PROGR ||'/'|| PAGAMENTI.ANNO ELSE '' END  AS ADP," _
                & "TO_CHAR(TO_DATE(DATA_EMISSIONE,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_ADP," _
                & "TRIM(TO_CHAR(PAGAMENTI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO_ADP," _
                & "CASE WHEN TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) IS NULL THEN '0,00' ELSE TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) END AS RITLEGGE_ADP," _
                & "SISCOM_MI.GETSTATOPAGAMENTO(PAGAMENTI.ID) AS STATO_PAGAMENTO,PRENOTAZIONI.ID_APPALTO AS ID_A,PRENOTAZIONI.ID_FORNITORE AS ID_F,PRENOTAZIONI.ID_PAGAMENTO AS ID_P,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+) " _
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
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "and ((PRENOTAZIONI.ID_STATO=1 AND DATA_CONSUNTIVAZIONE <='" & dataAl & "') " _
                & "OR (PRENOTAZIONI.ID_STATO=2  AND DATA_CONSUNTIVAZIONE <='" & dataAl & "' AND DATA_CERTIFICAZIONE >'" & dataAl & "') OR" _
                & "(PRENOTAZIONI.ID_STATO=-3 AND PRENOTAZIONI.DATA_ANNULLO>'" & dataAl & "' AND DATA_CONSUNTIVAZIONE<='" & dataAl & "' AND (DATA_CERTIFICAZIONE IS NULL OR DATA_CERTIFICAZIONE>'" & dataAl & "')))" _
                & "AND NVL(PRENOTAZIONI.IMPORTO_APPROVATO,0)-NVL(PRENOTAZIONI.RIT_LEGGE_IVATA,0)>0 " _
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
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
            dt.Columns.Add("NUM_REPERTORIO")
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
            dtExcel.Columns.Add("NUM_REPERTORIO")
            dtExcel.Columns.Add("ADP")
            dtExcel.Columns.Add("DATA_ADP")
            dtExcel.Columns.Add("IMPORTO_ADP")
            dtExcel.Columns.Add("RIT_LEGGE")
            dtExcel.Columns.Add("ID_PAG")
            dtExcel.Columns.Add("CUP")
            dtExcel.Columns.Add("CIG")
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
                RIGA.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.ID AS ID_MAE ," _
                    & " (SELECT CIG FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CIG," _
                    & " (SELECT CUP FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CUP," _
                    & " (SELECT NUMERO_PAGAMENTO||'/'||ANNO_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
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
                        RIGA.Item("MAE") = "<table style=""font-family: Arial, Helvetica, sans-serif; font-size: xx-small;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;"" bgcolor=""#EEEEEE"" width=""100%""><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CUP</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CIG</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">PROTOCOLLO</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">DATA</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">IMPORTO</th>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"

                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        check = 1
                        count = count + 1
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"


                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
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
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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

                        RIGAexcel.Item("CUP") = "TOTALE"
                        RIGAexcel.Item("CIG") = ""
                        RIGAexcel.Item("NUM_MAE") = ""
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
                    RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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

                    RIGAexcel.Item("CUP") = ""
                    RIGAexcel.Item("CIG") = ""
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
            RIGA.Item("NUM_REPERTORIO") = ""
            RIGA.Item("ADP") = "TOTALE ADP"
            RIGA.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
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
            RIGAexcel.Item("NUM_REPERTORIO") = ""
            RIGAexcel.Item("ADP") = "TOTALE ADP"
            RIGAexcel.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("ID_PAG") = ""
            RIGAexcel.Item("CUP") = ""
            RIGAexcel.Item("CIG") = ""
            RIGAexcel.Item("NUM_MAE") = ""
            RIGAexcel.Item("DATA_MAE") = ""
            RIGAexcel.Item("IMPORTO_MAE") = ""
            RIGAexcel.Item("ID_PRE") = ""
            RIGAexcel.Item("ID_MAE") = ""
            dtExcel.Rows.Add(RIGAexcel)

            lettorePagamenti.Close()
            If dt.Rows.Count > 1 Then

                DataGrid1.Visible = True
                btnExport.Visible = True
                btnStampaPDF.Visible = True
                lblErrore.Text = ""
                DataGrid1.DataSource = dt
                If idStruttura <> "-1" Then
                    DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "FILIALE")).Visible = False
                End If
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "DESCRIZIONE_PAG")).Visible = True
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "DATA_ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "IMPORTO_ADP")).Visible = False
                'DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "RIT_LEGGE")).Visible = False
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
                btnExport.Visible = False
                btnStampaPDF.Visible = False
                lblErrore.Text = "La ricerca di pagamenti consuntivati non ha prodotto risultati"
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

    Protected Sub esportaConsuntivato()
        EsportaExcelAutomaticoDaDataGrid(DataGrid1, "importiConsuntivati")
        ''#### EXPORT IN EXCEL ####
        'Try
        '    Dim myExcelFile As New CM.ExcelFile
        '    Dim i As Long
        '    Dim K As Long
        '    Dim sNomeFile As String
        '    Dim row As System.Data.DataRow
        '    Dim datatable As Data.DataTable
        '    datatable = Session.Item("PAG_STRU")
        '    'datatable = DT
        '    sNomeFile = "PRE_" & Format(Now, "yyyyMMddHHmmss")
        '    i = 0
        '    Dim nRighe As Integer = datatable.Rows.Count
        '    With myExcelFile
        '        .CreateFile(Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls"))
        '        .PrintGridLines = False
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
        '        .SetDefaultRowHeight(14)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
        '        .SetFont("Arial", 12, CM.ExcelFile.FontFormatting.xlsBold)
        '        .SetColumnWidth(1, 1, 14) 'codice
        '        .SetColumnWidth(2, 2, 70) 'voce
        '        .SetColumnWidth(3, 3, 70) 'tipo
        '        .SetColumnWidth(4, 4, 50) 'tipo
        '        .SetColumnWidth(5, 5, 60) 'filiale
        '        .SetColumnWidth(6, 6, 22) 'importo prenotazione
        '        .SetColumnWidth(7, 7, 52) 'fornitore
        '        K = 1
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont3, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, lblTitolo.Text)
        '        K = K + 1
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "CODICE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "VOCE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "TIPO PAGAMENTO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "DESCRIZIONE PAGAMENTO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "FILIALE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "IMPORTO PRENOTATO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "FORNITORE", 0)
        '        K = K + 1
        '        For Each row In datatable.Rows
        '            If row.Item("CODICE") = "TOTALE VOCI" Then
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, " ", 0)

        '            Else
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, row.Item("VOCE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, row.Item("DESCRIZIONE_TIPO"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, row.Item("DESCRIZIONE_PAG"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, row.Item("FILIALE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, row.Item("FORNITORE"), 0)
        '            End If
        '            i = i + 1
        '            K = K + 1
        '        Next
        '        .CloseFile()
        '    End With
        '    Dim objCrc32 As New Crc32()
        '    Dim strmZipOutputStream As ZipOutputStream
        '    Dim zipfic As String
        '    zipfic = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".zip")
        '    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        '    strmZipOutputStream.SetLevel(6)
        '    Dim strFile As String
        '    strFile = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls")
        '    Dim strmFile As FileStream = File.OpenRead(strFile)
        '    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '    strmFile.Read(abyBuffer, 0, abyBuffer.Length)
        '    Dim sFile As String = Path.GetFileName(strFile)
        '    Dim theEntry As ZipEntry = New ZipEntry(sFile)
        '    Dim fi As New FileInfo(strFile)
        '    theEntry.DateTime = fi.LastWriteTime
        '    theEntry.Size = strmFile.Length
        '    strmFile.Close()
        '    objCrc32.Reset()
        '    objCrc32.Update(abyBuffer)
        '    theEntry.Crc = objCrc32.Value
        '    strmZipOutputStream.PutNextEntry(theEntry)
        '    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        '    strmZipOutputStream.Finish()
        '    strmZipOutputStream.Close()
        '    File.Delete(strFile)
        '    Response.Redirect("..\..\..\FileTemp\" & sNomeFile & ".zip")
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub

    '###################################################################################################################
    '################################################# CERTIFICATO ####################################################

    Protected Sub stampaTabellaCertificato()
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
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
                CONTROLLODATA = "AND DATA_CERTIFICAZIONE <='" & Request.QueryString("AL") & "' "
                CONTROLLODATA2 = "AND DATA_MANDATO <='" & Request.QueryString("AL") & "' "
            End If
            Dim DATAALS As String = ""
            '****************************************************************************
            Dim dataAl As Integer
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
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
                DATAALS = CStr(Min(CLng(Format(Now, "yyyyMMdd")), CLng(par.IfNull(myReader1("FINE"), ""))))
                ANNO = Left(par.IfNull(myReader1("INIZIO"), ""), 4)
            End If
            myReader1.Close()
            '****************************************************************************

            If DATAALS <> "" And CONTROLLODATA = "" Then
                CONTROLLODATA = "AND DATA_CERTIFICAZIONE <='" & DATAALS & "' "
                CONTROLLODATA2 = "AND DATA_MANDATO <='" & DATAALS & "' "
                dataAl = DATAALS
            End If
            sDataEsercizio = sDataEsercizio & " AL " & Right(dataAl, 2) & "/" & Mid(dataAl, 5, 2) & "/" & Left(dataAl, 4)

            lblTitolo.Text = ""
            If idStruttura <> "-1" Then
                controlloStruttura = "AND PRENOTAZIONI.ID_STRUTTURA='" & idStruttura & "' "
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & idStruttura & "'"
                Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LETTORE.Read Then
                    lblTitolo.Text = "SITUAZIONE IMPORTI CERTIFICATI - STRUTTURA " & par.IfNull(LETTORE(0), "") & " - " & sDataEsercizio
                End If
                LETTORE.Close()
            Else
                lblTitolo.Text = "SITUAZIONE GENERALE IMPORTI CERTIFICATI - " & sDataEsercizio
            End If

            '######## ELENCO DELLE PRENOTAZIONI PER ESERCIZIO FINANZIARIO STRUTTURA E VOCE SELEZIONATA ######
            par.cmd.CommandText = "SELECT PRENOTAZIONI.ID AS ID_PRE,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                & "PF_VOCI.CODICE AS CODICE,PF_VOCI.DESCRIZIONE AS VOCE," _
                & "NVL(IMPORTO_APPROVATO,0) AS IMPORTO_PRENOTATO2," _
                & "NVL(IMPORTO_APPROVATO,0)-NVL(RIT_LEGGE_IVATA,0) AS IMPORTO_PRENOTATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE PAGAMENTI.DESCRIZIONE END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(NVL(PRENOTAZIONI.IMPORTO_APPROVATO,0),'999G999G990D99')) AS IMPORTO," _
                & "TRIM(TO_CHAR(MANUTENZIONI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO2," _
                & "TAB_STATI_ODL.DESCRIZIONE AS STATO_ODL," _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                & "PRENOTAZIONI.TIPO_PAGAMENTO AS TIPO_ADP," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "CASE WHEN PAGAMENTI.PROGR IS NOT NULL THEN PAGAMENTI.PROGR ||'/'|| PAGAMENTI.ANNO ELSE '' END  AS ADP," _
                & "TO_CHAR(TO_DATE(DATA_EMISSIONE,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_ADP," _
                & "TRIM(TO_CHAR(PAGAMENTI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO_ADP," _
                & "CASE WHEN TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) IS NULL THEN '0,00' ELSE TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) END AS RITLEGGE_ADP," _
                & "SISCOM_MI.GETSTATOPAGAMENTO(PAGAMENTI.ID) AS STATO_PAGAMENTO,PRENOTAZIONI.ID_APPALTO AS ID_A,PRENOTAZIONI.ID_FORNITORE AS ID_F,PRENOTAZIONI.ID_PAGAMENTO AS ID_P,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+) " _
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
                & "AND ((PRENOTAZIONI.id_stato=2 and data_certificazione<='" & dataAl & "') " _
                & " OR (PRENOTAZIONI.ID_STATO=-3 AND PRENOTAZIONI.DATA_ANNULLO>'" & dataAl & "' AND DATA_CONSUNTIVAZIONE<='" & dataAl & "' AND DATA_CERTIFICAZIONE<='" & dataAl & "'))" _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
                & "AND nvl(PRENOTAZIONI.importo_approvato,0)<>nvl(rit_legge_ivata,0) " _
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
            dt.Columns.Add("NUM_REPERTORIO")
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
            dtExcel.Columns.Add("NUM_REPERTORIO")
            dtExcel.Columns.Add("ADP")
            dtExcel.Columns.Add("DATA_ADP")
            dtExcel.Columns.Add("IMPORTO_ADP")
            dtExcel.Columns.Add("RIT_LEGGE")
            dtExcel.Columns.Add("ID_PAG")
            dtExcel.Columns.Add("CUP")
            dtExcel.Columns.Add("CIG")
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
                RIGA.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.ID AS ID_MAE ," _
                    & " (SELECT CIG FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CIG," _
                    & " (SELECT CUP FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CUP," _
                    & " (SELECT NUMERO_PAGAMENTO||'/'||ANNO_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
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
                        RIGA.Item("MAE") = "<table style=""font-family: Arial, Helvetica, sans-serif; font-size: xx-small;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;"" bgcolor=""#EEEEEE"" width=""100%""><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CUP</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CIG</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">PROTOCOLLO</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">DATA</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">IMPORTO</th>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"

                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        check = 1
                        count = count + 1
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"


                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
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
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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

                        RIGAexcel.Item("CUP") = "TOTALE"
                        RIGAexcel.Item("CIG") = ""
                        RIGAexcel.Item("NUM_MAE") = ""
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
                    RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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

                    RIGAexcel.Item("CUP") = ""
                    RIGAexcel.Item("CIG") = ""
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
            RIGA.Item("NUM_REPERTORIO") = ""
            RIGA.Item("ADP") = "TOTALE ADP"
            RIGA.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
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
            RIGAexcel.Item("NUM_REPERTORIO") = ""
            RIGAexcel.Item("ADP") = "TOTALE ADP"
            RIGAexcel.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("ID_PAG") = ""
            RIGAexcel.Item("CUP") = ""
            RIGAexcel.Item("CIG") = ""
            RIGAexcel.Item("NUM_MAE") = ""
            RIGAexcel.Item("DATA_MAE") = ""
            RIGAexcel.Item("IMPORTO_MAE") = ""
            RIGAexcel.Item("ID_PRE") = ""
            RIGAexcel.Item("ID_MAE") = ""
            dtExcel.Rows.Add(RIGAexcel)

            lettorePagamenti.Close()
            If dt.Rows.Count > 1 Then

                DataGrid1.Visible = True
                btnExport.Visible = True
                btnStampaPDF.Visible = True
                lblErrore.Text = ""
                DataGrid1.DataSource = dt
                If idStruttura <> "-1" Then
                    DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "FILIALE")).Visible = False
                End If
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "DESCRIZIONE_PAG")).Visible = True
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "DATA_ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "IMPORTO_ADP")).Visible = False
                'DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "RIT_LEGGE")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "MAE")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "IMPORTO_PRENOTATO")).HeaderText = "IMPORTO CERTIFICATO"
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
                btnExport.Visible = False
                btnStampaPDF.Visible = False
                lblErrore.Text = "La ricerca di pagamenti certificati non ha prodotto risultati"
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

    Protected Sub stampaRitenuteLeggeCertificate()
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
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
                CONTROLLODATA = "AND DATA_CERTIFICAZIONE <='" & Request.QueryString("AL") & "' "
                CONTROLLODATA2 = "AND DATA_MANDATO <='" & Request.QueryString("AL") & "' "
            End If
            Dim DATAALS As String = ""
            '****************************************************************************
            Dim dataAl As Integer
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
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
                DATAALS = CStr(Min(CLng(Format(Now, "yyyyMMdd")), CLng(par.IfNull(myReader1("FINE"), ""))))
                ANNO = Left(par.IfNull(myReader1("INIZIO"), ""), 4)
            End If
            myReader1.Close()
            '****************************************************************************

            If DATAALS <> "" And CONTROLLODATA = "" Then
                CONTROLLODATA = "AND DATA_CERTIFICAZIONE <='" & DATAALS & "' "
                CONTROLLODATA2 = "AND DATA_MANDATO <='" & DATAALS & "' "
                dataAl = DATAALS
            End If
            sDataEsercizio = sDataEsercizio & " AL " & Right(dataAl, 2) & "/" & Mid(dataAl, 5, 2) & "/" & Left(dataAl, 4)

            lblTitolorit.Text = ""
            If idStruttura <> "-1" Then
                controlloStruttura = "AND PRENOTAZIONI.ID_STRUTTURA='" & idStruttura & "' "
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & idStruttura & "'"
                Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LETTORE.Read Then
                    lblTitolorit.Text = "SITUAZIONE IMPORTI CERTIFICATI - STRUTTURA " & par.IfNull(LETTORE(0), "") & " - " & sDataEsercizio
                End If
                LETTORE.Close()
            Else
                lblTitoloRit.Text = "SITUAZIONE GENERALE RITENUTE DI LEGGE CERTIFICATE - " & sDataEsercizio
            End If

            '######## ELENCO DELLE PRENOTAZIONI PER ESERCIZIO FINANZIARIO STRUTTURA E VOCE SELEZIONATA ######
            par.cmd.CommandText = "SELECT PRENOTAZIONI.ID AS ID_PRE,PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE AS ID_PAGAMENTO_RIT_LEGGE," _
                & "PF_VOCI.CODICE AS CODICE,PF_VOCI.DESCRIZIONE AS VOCE," _
                & "NVL(IMPORTO_APPROVATO,0) AS IMPORTO_PRENOTATO2," _
                & "NVL(RIT_LEGGE_IVATA,0) AS IMPORTO_PRENOTATO," _
                & "(case when (nvl(prenotazioni.importo_approvato,0)=nvl(prenotazioni.rit_legge_ivata,0)) then (" _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (SELECT A.PROGR ||'/'||a.anno|| ' del ' || TO_CHAR(TO_DATE(a.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') FROM SISCOM_MI.MANUTENZIONI A,SISCOM_MI.PRENOTAZIONI B,SISCOM_MI.PRENOTAZIONI_PRE_RIT_LEGGE WHERE A.ID_PRENOTAZIONE_PAGAMENTO(+)=B.ID AND PRENOTAZIONI_PRE_RIT_LEGGE.ID_PRENOTAZIONE=B.ID AND PRENOTAZIONI_PRE_RIT_LEGGE.ID_PRENOTAZIONE_RIT_LEGGE=PRENOTAZIONI.ID)  ELSE PAGAMENTI.DESCRIZIONE END)" _
                & ") else (" _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE PAGAMENTI.DESCRIZIONE END)" _
                & ") end) as DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(NVL(PRENOTAZIONI.IMPORTO_APPROVATO,0),'999G999G990D99')) AS IMPORTO," _
                & "TRIM(TO_CHAR(MANUTENZIONI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO2," _
                & "TAB_STATI_ODL.DESCRIZIONE AS STATO_ODL," _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                & "PRENOTAZIONI.TIPO_PAGAMENTO AS TIPO_ADP," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "CASE WHEN PAGAMENTI.PROGR IS NOT NULL THEN PAGAMENTI.PROGR ||'/'|| PAGAMENTI.ANNO ELSE '' END  AS ADP," _
                & "TO_CHAR(TO_DATE(DATA_EMISSIONE,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_ADP," _
                & "TRIM(TO_CHAR(PAGAMENTI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO_ADP," _
                & "CASE WHEN TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) IS NULL THEN '0,00' ELSE TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) END AS RITLEGGE_ADP," _
                & "SISCOM_MI.GETSTATOPAGAMENTO(PAGAMENTI.ID) AS STATO_PAGAMENTO,PRENOTAZIONI.ID_APPALTO AS ID_A,PRENOTAZIONI.ID_FORNITORE AS ID_F,PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE AS ID_P,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE=PAGAMENTI.ID(+) " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+) " _
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
                & "AND ((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_rit_LIQUIDATI WHERE PAGAMENTI_rit_LIQUIDATI.ID_PAGAMENTO_RIT_LEGGE=PAGAMENTI.ID " & CONTROLLODATA2 & "  AND PAGAMENTI_rit_LIQUIDATI.ID_VOCE_PF=PF_VOCI.ID) = 0 OR " _
                & "(SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_rit_LIQUIDATI WHERE PAGAMENTI_rit_LIQUIDATI.ID_PAGAMENTO_RIT_LEGGE=PAGAMENTI.ID " & CONTROLLODATA2 & "  AND PAGAMENTI_rit_LIQUIDATI.ID_VOCE_PF=PF_VOCI.ID) IS NULL " _
                & ") " _
                & "and data_Cert_rit_legge<='" & dataAl & "' " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE IS NOT NULL " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
                & "ORDER BY PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE,MANUTENZIONI.PROGR ASC "

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
            dt.Columns.Add("NUM_REPERTORIO")
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
            dtExcel.Columns.Add("NUM_REPERTORIO")
            dtExcel.Columns.Add("ADP")
            dtExcel.Columns.Add("DATA_ADP")
            dtExcel.Columns.Add("IMPORTO_ADP")
            dtExcel.Columns.Add("RIT_LEGGE")
            dtExcel.Columns.Add("ID_PAG")
            dtExcel.Columns.Add("CUP")
            dtExcel.Columns.Add("CIG")
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
                RIGA.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
                RIGA.Item("TIPO_ADP") = par.IfNull(lettorePagamenti("TIPO_ADP"), "")
                If par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "3" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "4" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "6" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "7" Then
                    RIGA.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                Else
                    RIGA.Item("ADP") = "<a href=""javascript:ApriPagamenti(" & par.IfNull(lettorePagamenti("ID_P"), -1) & "," & par.IfNull(lettorePagamenti("TIPO_ADP"), 0) & ")"">" & par.IfNull(lettorePagamenti("ADP"), "") & "</a>"
                End If
                RIGA.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                RIGA.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                RIGA.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")
                RIGA.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO_RIT_LEGGE"), "")
                RIGA.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), "")

                '@@@@@@@@@@@@@@@@ MAE @@@@@@@@@@@@@@@@@
                par.cmd.CommandText = "SELECT PAGAMENTI_RIT_LIQUIDATI.ID AS ID_MAE ," _
                    & " '' AS CIG," _
                    & " '' AS CUP," _
                    & " (SELECT NUMERO_PAGAMENTO||'/'||ANNO_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,IMPORTO FROM SISCOM_MI.PAGAMENTI_rit_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO_RIT_LEGGE='" & par.IfNull(lettorePagamenti("ID_PAGAMENTO_RIT_LEGGE"), -1) & "'"
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
                        RIGA.Item("MAE") = "<table style=""font-family: Arial, Helvetica, sans-serif; font-size: xx-small;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;"" bgcolor=""#EEEEEE"" width=""100%""><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CUP</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CIG</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">PROTOCOLLO</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">DATA</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">IMPORTO</th>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"

                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO_RIT_LEGGE"), "")
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        check = 1
                        count = count + 1
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"


                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO_RIT_LEGGE"), "")
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
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
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO_RIT_LEGGE"), "")
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

                        RIGAexcel.Item("CUP") = "TOTALE"
                        RIGAexcel.Item("CIG") = ""
                        RIGAexcel.Item("NUM_MAE") = ""
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
                    RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
                    RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO_RIT_LEGGE"), "")
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

                    RIGAexcel.Item("CUP") = ""
                    RIGAexcel.Item("CIG") = ""
                    RIGAexcel.Item("NUM_MAE") = ""
                    RIGAexcel.Item("DATA_MAE") = ""
                    RIGAexcel.Item("IMPORTO_MAE") = ""
                    RIGAexcel.Item("ID_MAE") = -1
                    dtExcel.Rows.Add(RIGAexcel)
                End If
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                dt.Rows.Add(RIGA)

                If idPagamentoPrec.Value <> par.IfNull(lettorePagamenti("ID_PAGAMENTO_RIT_LEGGE"), -1) Then
                    totaleADP = totaleADP + CDec(par.IfNull(lettorePagamenti("IMPORTO_ADP"), 0))
                End If
                totaleRITLEGGEADP = totaleRITLEGGEADP + CDec(par.IfNull(lettorePagamenti("RITLEGGE_ADP"), 0))
                totalePrenotazione = totalePrenotazione + CDec(par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), 0))

                idPagamentoPrec.Value = par.IfNull(lettorePagamenti("ID_PAGAMENTO_RIT_LEGGE"), -1)
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
            RIGA.Item("NUM_REPERTORIO") = ""
            RIGA.Item("ADP") = "TOTALE ADP"
            RIGA.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
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
            RIGAexcel.Item("NUM_REPERTORIO") = ""
            RIGAexcel.Item("ADP") = "TOTALE ADP"
            RIGAexcel.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("ID_PAG") = ""
            RIGAexcel.Item("CUP") = ""
            RIGAexcel.Item("CIG") = ""
            RIGAexcel.Item("NUM_MAE") = ""
            RIGAexcel.Item("DATA_MAE") = ""
            RIGAexcel.Item("IMPORTO_MAE") = ""
            RIGAexcel.Item("ID_PRE") = ""
            RIGAexcel.Item("ID_MAE") = ""
            dtExcel.Rows.Add(RIGAexcel)

            lettorePagamenti.Close()
            If dt.Rows.Count > 1 Then

                DataGridRit.Visible = True
                excelRit.Visible = True
                stampaRit.Visible = True
                lblErroreRit.Text = ""
                DataGridRit.DataSource = dt
                If idStruttura <> "-1" Then
                    DataGridRit.Columns.Item(TrovaIndiceColonna(DataGridRit, "FILIALE")).Visible = False
                End If
                DataGridRit.Columns.Item(TrovaIndiceColonna(DataGridRit, "DESCRIZIONE_PAG")).Visible = True
                DataGridRit.Columns.Item(TrovaIndiceColonna(DataGridRit, "ADP")).Visible = False
                DataGridRit.Columns.Item(TrovaIndiceColonna(DataGridRit, "DATA_ADP")).Visible = False
                DataGridRit.Columns.Item(TrovaIndiceColonna(DataGridRit, "IMPORTO_ADP")).Visible = False
                'DataGridRit.Columns.Item(TrovaIndiceColonna(DataGridRit, "RIT_LEGGE")).Visible = False
                DataGridRit.Columns.Item(TrovaIndiceColonna(DataGridRit, "MAE")).Visible = False
                DataGridRit.Columns.Item(TrovaIndiceColonna(DataGridRit, "IMPORTO_PRENOTATO")).HeaderText = "IMPORTO CERTIFICATO"
                DataGridRit.AllowPaging = False

                'Session.Add("PAG_STRU_rit", dtExcel)
                DataGridRit.DataBind()
                For Each di As DataGridItem In DataGridRit.Items
                    If di.Cells(1).Text.Contains("TOTALE VOCI") Then
                        di.ForeColor = Drawing.Color.Red
                        di.BackColor = Drawing.Color.Lavender
                        For j As Integer = 0 To di.Cells.Count - 1
                            di.Cells(j).Font.Bold = True
                        Next
                    End If
                Next
            Else
                DataGridRit.Visible = False
                excelRit.Visible = False
                stampaRit.Visible = False
                lblErroreRit.Text = "La ricerca di ritenute non ha prodotto risultati"
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

    Protected Sub esportaCertificato()
        EsportaExcelAutomaticoDaDataGrid(DataGrid1, "importiCertificati")
        ''#### EXPORT IN EXCEL ####
        'Try
        '    Dim myExcelFile As New CM.ExcelFile
        '    Dim i As Long
        '    Dim K As Long
        '    Dim sNomeFile As String
        '    Dim row As System.Data.DataRow
        '    Dim datatable As Data.DataTable
        '    datatable = Session.Item("PAG_STRU")
        '    'datatable = DT
        '    sNomeFile = "CONS_" & Format(Now, "yyyyMMddHHmmss")
        '    i = 0
        '    Dim nRighe As Integer = datatable.Rows.Count
        '    With myExcelFile
        '        .CreateFile(Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls"))
        '        .PrintGridLines = False
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
        '        .SetDefaultRowHeight(14)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
        '        .SetFont("Arial", 12, CM.ExcelFile.FontFormatting.xlsBold)
        '        .SetColumnWidth(1, 1, 14) 'codice
        '        .SetColumnWidth(2, 2, 70) 'voce
        '        .SetColumnWidth(3, 3, 70) 'tipo
        '        .SetColumnWidth(4, 4, 50) 'tipo
        '        .SetColumnWidth(5, 5, 60) 'filiale
        '        .SetColumnWidth(6, 6, 25) 'importo prenotazione
        '        .SetColumnWidth(7, 7, 52) 'fornitore
        '        K = 1
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont3, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, lblTitolo.Text)
        '        K = K + 1
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "CODICE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "VOCE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "TIPO PAGAMENTO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "DESCRIZIONE PAGAMENTO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "FILIALE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "IMPORTO CONSUNTIVATO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "FORNITORE", 0)
        '        K = K + 1
        '        For Each row In datatable.Rows
        '            If row.Item("CODICE") = "TOTALE VOCI" Then
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, " ", 0)

        '            Else
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, row.Item("VOCE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, row.Item("DESCRIZIONE_TIPO"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, row.Item("DESCRIZIONE_PAG"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, row.Item("FILIALE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, row.Item("FORNITORE"), 0)
        '            End If
        '            i = i + 1
        '            K = K + 1
        '        Next
        '        .CloseFile()
        '    End With
        '    Dim objCrc32 As New Crc32()
        '    Dim strmZipOutputStream As ZipOutputStream
        '    Dim zipfic As String
        '    zipfic = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".zip")
        '    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        '    strmZipOutputStream.SetLevel(6)
        '    Dim strFile As String
        '    strFile = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls")
        '    Dim strmFile As FileStream = File.OpenRead(strFile)
        '    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '    strmFile.Read(abyBuffer, 0, abyBuffer.Length)
        '    Dim sFile As String = Path.GetFileName(strFile)
        '    Dim theEntry As ZipEntry = New ZipEntry(sFile)
        '    Dim fi As New FileInfo(strFile)
        '    theEntry.DateTime = fi.LastWriteTime
        '    theEntry.Size = strmFile.Length
        '    strmFile.Close()
        '    objCrc32.Reset()
        '    objCrc32.Update(abyBuffer)
        '    theEntry.Crc = objCrc32.Value
        '    strmZipOutputStream.PutNextEntry(theEntry)
        '    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        '    strmZipOutputStream.Finish()
        '    strmZipOutputStream.Close()
        '    File.Delete(strFile)
        '    Response.Redirect("..\..\..\FileTemp\" & sNomeFile & ".zip")
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub

    '###################################################################################################################
    '################################################# LIQUIDATO ####################################################

    Protected Sub StampaTabellaLiquidazioni()
        Try
            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='../../Immagini/load.gif' alt='Estrazione in corso...' ><br>Estrazione in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
            Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var scarica; scarica = ''; var testo; testo = ''; var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

            Response.Write(Str)
            Response.Flush()
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
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
                CONTROLLODATA = "AND PAGAMENTI_LIQUIDATI.DATA_MANDATO <='" & Request.QueryString("AL") & "' "
                CONTROLLODATA2 = "AND PAGAMENTI_rit_LIQUIDATI.DATA_MANDATO <='" & Request.QueryString("AL") & "' "
            End If
            Dim DATAALS As String = ""
            '****************************************************************************
            Dim dataAl As Integer
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
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
                DATAALS = CStr(Min(CLng(Format(Now, "yyyyMMdd")), CLng(par.IfNull(myReader1("FINE"), ""))))
                ANNO = Left(par.IfNull(myReader1("INIZIO"), ""), 4)

            End If
            myReader1.Close()
            '****************************************************************************
            If DATAALS <> "" And CONTROLLODATA = "" Then
                CONTROLLODATA = "AND PAGAMENTI_LIQUIDATI.DATA_MANDATO <='" & DATAALS & "' "
                CONTROLLODATA2 = "AND PAGAMENTI_rit_LIQUIDATI.DATA_MANDATO <='" & DATAALS & "' "
                dataAl = DATAALS
            End If
            sDataEsercizio = sDataEsercizio & " AL " & Right(dataAl, 2) & "/" & Mid(dataAl, 5, 2) & "/" & Left(dataAl, 4)

            lblTitolo.Text = ""
            If idStruttura <> "-1" Then
                controlloStruttura = "AND PRENOTAZIONI.ID_STRUTTURA='" & idStruttura & "' "
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & idStruttura & "'"
                Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LETTORE.Read Then
                    lblTitolo.Text = "SITUAZIONE IMPORTI LIQUIDATI - STRUTTURA " & par.IfNull(LETTORE(0), "") & " - " & sDataEsercizio
                End If
                LETTORE.Close()
            Else
                lblTitolo.Text = "SITUAZIONE GENERALE IMPORTI LIQUIDATI " & " - " & sDataEsercizio
            End If

            '######## ELENCO DEI PAGAMENTI PER STRUTTURA ED ESERCIZIO FINANZIARIO ######
            par.cmd.CommandText = "SELECT DISTINCT PRENOTAZIONI.ID AS ID_PRE,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                & "PF_VOCI.CODICE AS CODICE,PF_VOCI.DESCRIZIONE AS VOCE," _
                & "(CASE WHEN PRENOTAZIONI.IMPORTO_APPROVATO IS NULL THEN TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_PRENOTATO,'999G999G990D99')) ELSE TRIM(TO_CHAR(NVL(PRENOTAZIONI.IMPORTO_APPROVATO,0)-NVL(RIT_LEGGE_IVATA,0),'999G999G990D99')) END) AS IMPORTO_PRENOTATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE PAGAMENTI.DESCRIZIONE END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(MANUTENZIONI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO," _
                & "TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_APPROVATO,'999G999G990D99')) AS IMPORTO," _
                & "TAB_STATI_ODL.DESCRIZIONE AS STATO_ODL," _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                & "PRENOTAZIONI.TIPO_PAGAMENTO AS TIPO_ADP," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "CASE WHEN (PAGAMENTI.PROGR IS NOT NULL AND PAGAMENTI.PROGR>0) THEN PAGAMENTI.PROGR ||'/'|| PAGAMENTI.ANNO ELSE '' END  AS ADP," _
                & "TO_CHAR(TO_DATE(DATA_EMISSIONE,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_ADP," _
                & "TRIM(TO_CHAR(PAGAMENTI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO_ADP," _
                & "CASE WHEN TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) IS NULL THEN '0,00' ELSE TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) END AS RITLEGGE_ADP," _
                & "SISCOM_MI.GETSTATOPAGAMENTO(PAGAMENTI.ID) AS STATO_PAGAMENTO,PRENOTAZIONI.ID_APPALTO AS ID_A,PRENOTAZIONI.ID_FORNITORE AS ID_F," _
                & "PRENOTAZIONI.ID_PAGAMENTO AS ID_P,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE,PAGAMENTI.PROGR AS PROGRESSIVO,PF_VOCI.ID AS IDVOCE,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+) " _
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
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
                & "UNION " _
                & "SELECT DISTINCT PRENOTAZIONI.ID AS ID_PRE,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                & "PF_VOCI.CODICE AS CODICE,PF_VOCI.DESCRIZIONE AS VOCE," _
                & "(CASE WHEN PRENOTAZIONI.IMPORTO_APPROVATO IS NULL THEN TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_PRENOTATO,'999G999G990D99')) ELSE TRIM(TO_CHAR(NVL(PRENOTAZIONI.IMPORTO_APPROVATO,0)-NVL(RIT_LEGGE_IVATA,0),'999G999G990D99')) END) AS IMPORTO_PRENOTATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE PAGAMENTI.DESCRIZIONE END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(MANUTENZIONI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO," _
                & "TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_APPROVATO,'999G999G990D99')) AS IMPORTO," _
                & "TAB_STATI_ODL.DESCRIZIONE AS STATO_ODL," _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                & "PRENOTAZIONI.TIPO_PAGAMENTO AS TIPO_ADP," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "CASE WHEN (PAGAMENTI.PROGR IS NOT NULL AND PAGAMENTI.PROGR>0) THEN PAGAMENTI.PROGR ||'/'|| PAGAMENTI.ANNO ELSE '' END  AS ADP," _
                & "TO_CHAR(TO_DATE(DATA_EMISSIONE,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_ADP," _
                & "TRIM(TO_CHAR(PAGAMENTI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO_ADP," _
                & "CASE WHEN TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) IS NULL THEN '0,00' ELSE TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) END AS RITLEGGE_ADP," _
                & "SISCOM_MI.GETSTATOPAGAMENTO(PAGAMENTI.ID) AS STATO_PAGAMENTO,PRENOTAZIONI.ID_APPALTO AS ID_A,PRENOTAZIONI.ID_FORNITORE AS ID_F," _
                & "PRENOTAZIONI.ID_PAGAMENTO AS ID_P,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE,PAGAMENTI.PROGR AS PROGRESSIVO,PF_VOCI.ID AS IDVOCE,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+) " _
                & "AND PAGAMENTI_RIT_LIQUIDATI.ID_PAGAMENTO_rit_legge=PAGAMENTI.ID " _
                & "AND PAGAMENTI_RIT_LIQUIDATI.ID_VOCE_PF=PF_VOCI.ID " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND TAB_STATI_PAGAMENTI.ID(+)=PAGAMENTI.ID_STATO " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND TAB_STATI_ODL.ID(+)=MANUTENZIONI.STATO " _
                & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & esercizioFinanziario & "' " _
                & "AND PAGAMENTI_RIT_LIQUIDATI.ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & Request.QueryString("IDV") & "') " _
                & controlloStruttura _
                & CONTROLLODATA2 _
                & "AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
                & "ORDER BY 2,1 "

            Dim daLettorePagamenti As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtLettorePagamenti As New Data.DataTable
            daLettorePagamenti.Fill(dtLettorePagamenti)
            daLettorePagamenti.Dispose()
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
            dt.Columns.Add("NUM_REPERTORIO")
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
            dtExcel.Columns.Add("NUM_REPERTORIO")
            dtExcel.Columns.Add("ADP")
            dtExcel.Columns.Add("DATA_ADP")
            dtExcel.Columns.Add("IMPORTO_ADP")
            dtExcel.Columns.Add("RIT_LEGGE")
            dtExcel.Columns.Add("ID_PAG")
            dtExcel.Columns.Add("CUP")
            dtExcel.Columns.Add("CIG")
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

            Dim contatore As Integer = 0
            Dim numeroRighe As Integer = dtLettorePagamenti.Rows.Count
            Dim percentualePrecedente As Integer = 0
            For Each lettorePagamenti As Data.DataRow In dtLettorePagamenti.Rows
                'While lettorePagamenti.Read

                contatore = contatore + 1
                percentuale = (contatore * 100) / numeroRighe

                If percentualePrecedente <> percentuale Then
                    Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                    Response.Flush()
                End If
                percentualePrecedente = percentuale

                i = i + 1
                RIGA = dt.NewRow
                RIGA.Item("CODICE") = par.IfNull(lettorePagamenti.Item("CODICE"), "")
                RIGA.Item("VOCE") = par.IfNull(lettorePagamenti.Item("VOCE"), "")
                RIGA.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti.Item("DESCRIZIONE_TIPO"), "")
                RIGA.Item("FILIALE") = par.IfNull(lettorePagamenti.Item("FILIALE"), "")
                RIGA.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti.Item("IMPORTO_PRENOTATO"), "")
                RIGA.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti.Item("DESCRIZIONE_PAG"), "")
                RIGA.Item("IMPORTO") = par.IfNull(lettorePagamenti.Item("IMPORTO"), "")
                RIGA.Item("FORNITORE") = par.IfNull(lettorePagamenti.Item("FORNITORE"), "")
                RIGA.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti.Item("NUM_REPERTORIO"), "")
                RIGA.Item("TIPO_ADP") = par.IfNull(lettorePagamenti.Item("TIPO_ADP"), "")
                If par.IfNull(lettorePagamenti.Item("TIPO_ADP"), "") <> "3" And par.IfNull(lettorePagamenti.Item("TIPO_ADP"), "") <> "4" And par.IfNull(lettorePagamenti.Item("TIPO_ADP"), "") <> "6" And par.IfNull(lettorePagamenti.Item("TIPO_ADP"), "") <> "7" Then
                    RIGA.Item("ADP") = par.IfNull(lettorePagamenti.Item("ADP"), "")
                Else
                    RIGA.Item("ADP") = "<a href=""javascript:ApriPagamenti(" & par.IfNull(lettorePagamenti.Item("ID_P"), -1) & "," & par.IfNull(lettorePagamenti.Item("TIPO_ADP"), 0) & ")"">" & par.IfNull(lettorePagamenti.Item("ADP"), "") & "</a>"
                End If
                Dim ImportoADPperSomma As Decimal = 0
                RIGA.Item("DATA_ADP") = par.IfNull(lettorePagamenti.Item("DATA_ADP"), "")

                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'CONTO MAE, DETERMINO IL NUMERO DI MAE PERCHè HO BISOGNO DI SCRIVERE L'IMPORTO ADP
                'IN MANIERA CORRETTA E CONTROLLO L'IMPORTO TOTALE
                Dim nMae As Integer = 0
                par.cmd.CommandText = "SELECT count(*) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti.Item("ID_PAGAMENTO"), -1) & "' " _
                    & CONTROLLODATA
                Dim lettoreNumeroMandati As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreNumeroMandati.Read Then
                    nMae = par.IfNull(lettoreNumeroMandati(0), 0)
                End If
                lettoreNumeroMandati.Close()
                Dim nMaeGEN As Integer = 0
                par.cmd.CommandText = "SELECT count(*) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti.Item("ID_PAGAMENTO"), -1) & "' "
                Dim lettoreNumeroMandatiGEN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreNumeroMandatiGEN.Read Then
                    nMaeGEN = par.IfNull(lettoreNumeroMandatiGEN(0), 0)
                End If
                lettoreNumeroMandatiGEN.Close()
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'DETERMINO L'IMPORTO TOTALE MAE IN PAGAMENTI LIQUIDATI CON DATA
                Dim ImportoTotaleMandati As Decimal = 0
                par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti.Item("ID_PAGAMENTO"), -1) & "' " _
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
                    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti.Item("ID_PAGAMENTO"), -1) & "' "
                Dim lettoreImportoMandatiGenerale As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreImportoMandatiGenerale.Read Then
                    ImportoTotaleMandatiGenerale = par.IfNull(lettoreImportoMandatiGenerale(0), 0)
                End If
                lettoreImportoMandatiGenerale.Close()
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°

                'DETERMINO L'IMPORTO TOTALE APPROVATO
                Dim totaleImportoApprovato As Decimal = 0
                par.cmd.CommandText = "SELECT DISTINCT" _
                    & "(CASE WHEN PRENOTAZIONI.IMPORTO_APPROVATO IS NULL THEN TRIM(TO_CHAR(NVL(PRENOTAZIONI.IMPORTO_PRENOTATO,0),'999G999G990D99')) ELSE TRIM(TO_CHAR(NVL(PRENOTAZIONI.IMPORTO_APPROVATO,0)-NVL(RIT_LEGGE_IVATA,0),'999G999G990D99')) END) AS IMPORTO_PRENOTATO,PRENOTAZIONI.ID " _
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
                    & "AND PAGAMENTI.PROGR='" & par.IfNull(lettorePagamenti.Item("PROGRESSIVO"), "") & "' "
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
                    'RIGA.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti.item("IMPORTO_ADP"), "")
                    RIGA.Item("IMPORTO_ADP") = totaleImportoApprovato
                    IMPORTOADPEXCEL = Format(CDec(totaleImportoApprovato), "##,##0.00")
                    'ImportoADPperSomma = CDec(par.IfNull(lettorePagamenti.item("IMPORTO_ADP"), ""))
                    ImportoADPperSomma = totaleImportoApprovato
                Else
                    If nMae > 1 Then
                        'SE CI SONO PIù MANDATI INDICO IL PARZIALE FINO A QUEL MOMENTO LIQUIDATO E TRA PARENTESI IL TOTALE COMPLESSIVO DEI MANDATI
                        RIGA.Item("IMPORTO_ADP") = totaleImportoApprovato & "<br />(" & par.IfNull(lettorePagamenti.Item("IMPORTO_ADP"), 0) & ")"
                        IMPORTOADPEXCEL = Format(CDec(totaleImportoApprovato), "##,##0.00")
                        ImportoADPperSomma = totaleImportoApprovato
                    Else
                        'RIGA.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti.item("IMPORTO_ADP"), "")
                        RIGA.Item("IMPORTO_ADP") = totaleImportoApprovato
                        IMPORTOADPEXCEL = Format(CDec(totaleImportoApprovato), "##,##0.00")
                        'ImportoADPperSomma = CDec(par.IfNull(lettorePagamenti.item("IMPORTO_ADP"), ""))
                        ImportoADPperSomma = totaleImportoApprovato
                    End If
                End If
                RIGA.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti.Item("RITLEGGE_ADP"), "")
                RIGA.Item("ID_PAG") = par.IfNull(lettorePagamenti.Item("ID_PAGAMENTO"), "")
                RIGA.Item("ID_PRE") = par.IfNull(lettorePagamenti.Item("ID_PRE"), "")
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'MAE
                'LEGGO TUTTI I MANDATI RELATIVI AL PAGAMENTO, CONSIDERO SE ILLUMINARLI GIALLI O ROSSI
                par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.ID AS ID_MAE ," _
                    & " (SELECT CIG FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CIG," _
                    & " (SELECT CUP FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CUP," _
                    & " (SELECT NUMERO_PAGAMENTO||'/'||ANNO_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,DATA_MANDATO AS DM,IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti.Item("ID_PAGAMENTO"), -1) & "'"
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
                                RIGA.Item("IMPORTO_ADP") = Format(CDec(totaleImportoApprovato) - CDec(par.IfNull(lettoreMAE("IMPORTO"), "")), "##,##0.00") & "<br />(" & Format(CDec(par.IfNull(lettorePagamenti.Item("IMPORTO_ADP"), 0)), "##,##0.00") & ")"
                                ImportoADPperSomma = CDec(totaleImportoApprovato) - CDec(par.IfNull(lettoreMAE("IMPORTO"), ""))
                                RIGAexcel.Item("IMPORTO_ADP") = Format(CDec(totaleImportoApprovato) - CDec(par.IfNull(lettoreMAE("IMPORTO"), "")), "##,##0.00")

                            End If
                        End If
                    End If

                    'COLORE GIALLO
                    Dim giallo As Boolean = False
                    If nMae > 1 And importiUguali = False Then
                        par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.ID AS ID_MAE ," _
                            & " (SELECT CIG FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CIG," _
                    & " (SELECT CUP FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CUP," _
                            & "(SELECT NUMERO_PAGAMENTO||'/'||ANNO_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS MAE," _
                            & "TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO," _
                            & "DATA_MANDATO AS DM," _
                            & "IMPORTO " _
                            & "FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                            & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti.Item("ID_PAGAMENTO"), -1) & "' " _
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
                        RIGA.Item("MAE") = "<table style=""font-family: Arial, Helvetica, sans-serif; font-size: xx-small;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;"" bgcolor=""#EEEEEE"" width=""100%""><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CUP</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CIG</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">PROTOCOLLO</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">DATA</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">IMPORTO</th>"
                        Dim BGCOLOR As String = ""
                        If nMae > 1 Then
                            If giallo Then
                                BGCOLOR = "bgcolor=""#FFFF66"""
                            End If
                        End If
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr " & BGCOLOR & " " & colorRed & "><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"
                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti.Item("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti.Item("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti.Item("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti.Item("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti.Item("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti.Item("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti.Item("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti.Item("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti.Item("NUM_REPERTORIO"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti.Item("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti.Item("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti.Item("RITLEGGE_ADP"), "")
                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti.Item("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti.Item("DATA_ADP"), "")
                        Else
                            If par.IfNull(lettorePagamenti.Item("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti.Item("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti.Item("DATA_ADP"), "")

                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        If MAE = False Then
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
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
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr " & BGCOLOR & " " & colorRed & "><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"
                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti.Item("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti.Item("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti.Item("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti.Item("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti.Item("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti.Item("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti.Item("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti.Item("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti.Item("NUM_REPERTORIO"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti.Item("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti.Item("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti.Item("RITLEGGE_ADP"), "")

                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti.Item("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti.Item("DATA_ADP"), "")
                        Else
                            If par.IfNull(lettorePagamenti.Item("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti.Item("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti.Item("DATA_ADP"), "")
                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        If MAE = False Then
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
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
                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti.Item("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti.Item("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti.Item("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti.Item("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti.Item("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti.Item("FILIALE"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti.Item("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti.Item("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti.Item("NUM_REPERTORIO"), "")
                        RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti.Item("ID_PAGAMENTO"), "")
                        RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti.Item("ID_PRE"), -1)
                        RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti.Item("RITLEGGE_ADP"), "")

                        If i = 1 Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti.Item("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti.Item("DATA_ADP"), "")
                        Else
                            If par.IfNull(lettorePagamenti.Item("ADP"), "") <> adpPrec Then
                                RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti.Item("ADP"), "")
                                RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti.Item("DATA_ADP"), "")
                            Else
                                RIGAexcel.Item("ADP") = ""
                                RIGAexcel.Item("DATA_ADP") = ""
                                RIGAexcel.Item("IMPORTO_ADP") = ""
                            End If
                        End If

                        RIGAexcel.Item("CUP") = "TOTALE"
                        RIGAexcel.Item("CIG") = ""
                        RIGAexcel.Item("NUM_MAE") = ""
                        RIGAexcel.Item("DATA_MAE") = ""
                        RIGAexcel.Item("IMPORTO_MAE") = Format(importoTotale, "##,##0.00")
                        RIGAexcel.Item("ID_MAE") = -1
                        dtExcel.Rows.Add(RIGAexcel)

                    End If
                Else
                    RIGAexcel = dtExcel.NewRow
                    RIGAexcel.Item("IMPORTO_ADP") = Replace(RIGA.Item("IMPORTO_ADP"), "<br />", "")
                    RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti.Item("CODICE"), "")
                    RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti.Item("VOCE"), "")
                    RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti.Item("DESCRIZIONE_TIPO"), "")
                    RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti.Item("IMPORTO_PRENOTATO"), "")
                    RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti.Item("DESCRIZIONE_PAG"), "")
                    RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti.Item("FILIALE"), "")
                    RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti.Item("IMPORTO"), "")
                    RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti.Item("FORNITORE"), "")
                    RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti.Item("NUM_REPERTORIO"), "")
                    RIGAexcel.Item("ID_PAG") = par.IfNull(lettorePagamenti.Item("ID_PAGAMENTO"), "")
                    RIGAexcel.Item("ID_PRE") = par.IfNull(lettorePagamenti.Item("ID_PRE"), -1)
                    RIGAexcel.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti.Item("RITLEGGE_ADP"), "")

                    If i = 1 Then
                        RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti.Item("ADP"), "")
                        RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti.Item("DATA_ADP"), "")
                    Else
                        If par.IfNull(lettorePagamenti.Item("ADP"), "") <> adpPrec Then
                            RIGAexcel.Item("ADP") = par.IfNull(lettorePagamenti.Item("ADP"), "")
                            RIGAexcel.Item("DATA_ADP") = par.IfNull(lettorePagamenti.Item("DATA_ADP"), "")
                        Else
                            RIGAexcel.Item("ADP") = ""
                            RIGAexcel.Item("DATA_ADP") = ""
                            RIGAexcel.Item("IMPORTO_ADP") = ""
                        End If
                    End If

                    RIGAexcel.Item("CUP") = ""
                    RIGAexcel.Item("CIG") = ""
                    RIGAexcel.Item("NUM_MAE") = ""
                    RIGAexcel.Item("DATA_MAE") = ""
                    RIGAexcel.Item("IMPORTO_MAE") = ""
                    RIGAexcel.Item("ID_MAE") = -1
                    dtExcel.Rows.Add(RIGAexcel)
                End If
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                dt.Rows.Add(RIGA)

                If idPagamentoPrec.Value <> par.IfNull(lettorePagamenti.Item("ID_PAGAMENTO"), -1) Then
                    'totaleADP = totaleADP + CDec(par.IfNull(lettorePagamenti.item("IMPORTO_ADP"), 0))
                    totaleADP = totaleADP + ImportoADPperSomma
                End If
                totaleRITLEGGEADP = totaleRITLEGGEADP + CDec(par.IfNull(lettorePagamenti.Item("RITLEGGE_ADP"), 0))
                totalePrenotazione = totalePrenotazione + CDec(par.IfNull(lettorePagamenti.Item("IMPORTO_PRENOTATO"), 0))

                idPagamentoPrec.Value = par.IfNull(lettorePagamenti.Item("ID_PAGAMENTO"), -1)
                adpPrec = par.IfNull(lettorePagamenti.Item("ADP"), "")
                id_mae_prec = idmae
                'End While
            Next

            RIGA = dt.NewRow
            RIGA.Item("CODICE") = "TOTALE VOCI"
            RIGA.Item("VOCE") = ""
            RIGA.Item("FILIALE") = ""
            RIGA.Item("IMPORTO_PRENOTATO") = Format(totalePrenotazione, "##,##0.00")
            RIGA.Item("DESCRIZIONE_PAG") = ""
            RIGA.Item("IMPORTO") = ""
            RIGA.Item("FORNITORE") = ""
            RIGA.Item("NUM_REPERTORIO") = ""
            RIGA.Item("ADP") = ""
            'RIGA.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
            RIGA.Item("DATA_ADP") = ""
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
            RIGAexcel.Item("NUM_REPERTORIO") = ""
            RIGAexcel.Item("ADP") = ""
            'RIGAexcel.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("DATA_ADP") = ""
            RIGAexcel.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("ID_PAG") = ""
            RIGAexcel.Item("CUP") = ""
            RIGAexcel.Item("CIG") = ""
            RIGAexcel.Item("NUM_MAE") = ""
            RIGAexcel.Item("DATA_MAE") = ""
            RIGAexcel.Item("IMPORTO_MAE") = ""
            RIGAexcel.Item("ID_PRE") = ""
            RIGAexcel.Item("ID_MAE") = ""
            dtExcel.Rows.Add(RIGAexcel)

            'lettorePagamenti.Close()
            If dt.Rows.Count > 1 Then

                DataGrid1.Visible = True
                btnExport.Visible = True
                btnStampaPDF.Visible = True
                lblErrore.Text = ""
                'DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "RIT_LEGGE")).Visible = False
                DataGrid1.DataSource = dt
                If idStruttura <> "-1" Then
                    DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "FILIALE")).Visible = False
                End If
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "IMPORTO_PRENOTATO")).HeaderText = "IMPORTO LIQUIDATO"
                DataGrid1.AllowPaging = False
                Session.Add("PAG_STRU_LIQ", dtExcel)
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
                btnExport.Visible = False
                btnStampaPDF.Visible = False
                lblErrore.Text = "La ricerca di pagamenti liquidati non ha prodotto risultati"


            End If

            Response.Write("<script>document.getElementById('dvvvPre').style.display = 'none';</script>")
            Response.Flush()

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

    Protected Sub esportaLiquidazioni()
        '#### EXPORT IN EXCEL ####
        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            Dim datatable As Data.DataTable
            datatable = Session.Item("PAG_STRU_LIQ")
            'datatable = DT
            sNomeFile = "LIQUIDAZIONI_" & Format(Now, "yyyyMMddHHmmss")
            i = 0
            Dim nRighe As Integer = datatable.Rows.Count
            With myExcelFile
                .CreateFile(Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Arial", 12, CM.ExcelFile.FontFormatting.xlsBold)
                .SetColumnWidth(1, 1, 14) 'codice
                .SetColumnWidth(2, 2, 70) 'voce
                .SetColumnWidth(3, 3, 70) 'tipo
                .SetColumnWidth(4, 4, 60) 'importo odl
                .SetColumnWidth(5, 5, 60) 'filiale
                .SetColumnWidth(6, 6, 25) 'importo consuntivato
                .SetColumnWidth(7, 7, 52) 'fornitore
                .SetColumnWidth(8, 8, 52) 'num_repertorio
                .SetColumnWidth(9, 9, 25) 'adp
                .SetColumnWidth(10, 10, 10) 'data adp
                .SetColumnWidth(11, 11, 28) 'importo adp
                .SetColumnWidth(12, 12, 14) 'ritlegge adp
                .SetColumnWidth(13, 13, 10) 'mae
                .SetColumnWidth(14, 14, 10) 'data mae
                .SetColumnWidth(15, 15, 14) 'importo mae
                K = 1
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont3, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, lblTitolo.Text)
                K = K + 1
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "CODICE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "VOCE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "TIPO PAGAMENTO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "DESCRIZIONE PAGAMENTO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "FILIALE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "IMPORTO CONSUNTIVATO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "FORNITORE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, "REPERTORIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, "ADP", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, "DATA ADP", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, "IMPORTO ADP", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, "RIT.LEGGE ADP", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, "CUP", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, "CIG", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, "PROTOCOLLO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, "DATA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, "IMPORTO", 0)
                K = K + 1
                Dim id_pre As Integer
                Dim id_pag As Integer
                Dim id_mae As Integer
                For Each row In datatable.Rows
                    If row.Item("CODICE") = "TOTALE VOCI" Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, row.Item("VOCE"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, " ", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, " ", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, " ", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, row.Item("FORNITORE"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, row.Item("NUM_REPERTORIO"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, row.Item("ADP"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, row.Item("DATA_ADP"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, row.Item("IMPORTO_ADP"), 4)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, row.Item("RIT_LEGGE"), 4)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, row.Item("CUP"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, row.Item("CIG"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, row.Item("NUM_MAE"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, row.Item("DATA_MAE"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, " ", 4)
                    Else
                        If i = 0 Then
                            id_pre = row.Item("ID_PRE")
                            id_mae = row.Item("ID_MAE")
                            id_pag = row.Item("ID_PAG")
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, row.Item("VOCE"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, row.Item("DESCRIZIONE_TIPO"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, row.Item("DESCRIZIONE_PAG"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, row.Item("FILIALE"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, row.Item("FORNITORE"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, row.Item("NUM_REPERTORIO"), 0)
                            If row.Item("ADP") = "" And row.Item("IMPORTO_ADP") = "" Then
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, row.Item("ADP"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, row.Item("DATA_ADP"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, row.Item("IMPORTO_ADP"), 4)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, row.Item("RIT_LEGGE"), 4)
                            Else
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, row.Item("ADP"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, row.Item("DATA_ADP"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, row.Item("IMPORTO_ADP"), 4)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, row.Item("RIT_LEGGE"), 4)
                            End If

                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, row.Item("CUP"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, row.Item("CIG"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, row.Item("NUM_MAE"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, row.Item("DATA_MAE"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, row.Item("IMPORTO_MAE"), 4)
                        Else
                            If row.Item("ID_PRE") = id_pre Then
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, " ", 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, " ", 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, " ", 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, " ", 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, " ", 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, " ", 4)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, " ", 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, " ", 0)
                                If row.Item("ADP") = "" And row.Item("IMPORTO_ADP") = "" Then
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, " ", 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, " ", 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, " ", 4)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, " ", 4)
                                Else
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, " ", 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, " ", 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, " ", 4)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, " ", 4)
                                End If
                                If CInt(row.Item("ID_MAE")) = id_mae Then
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, " ", 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, " ", 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, " ", 4)
                                Else
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, row.Item("CUP"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, row.Item("CIG"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, row.Item("NUM_MAE"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, row.Item("DATA_MAE"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, row.Item("IMPORTO_MAE"), 4)
                                End If

                            Else
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, row.Item("VOCE"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, row.Item("DESCRIZIONE_TIPO"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, row.Item("DESCRIZIONE_PAG"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, row.Item("FILIALE"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, row.Item("FORNITORE"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, row.Item("NUM_REPERTORIO"), 0)
                                If row.Item("ADP") = "" And row.Item("IMPORTO_ADP") = "" Then
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, row.Item("ADP"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, row.Item("DATA_ADP"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, row.Item("IMPORTO_ADP"), 4)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, row.Item("RIT_LEGGE"), 4)
                                Else
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, row.Item("ADP"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, row.Item("DATA_ADP"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, row.Item("IMPORTO_ADP"), 4)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, row.Item("RIT_LEGGE"), 4)
                                End If
                                If CInt(row.Item("ID_MAE")) = id_mae Then
                                    If row.Item("ID_PAG") = id_pag Then
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, " ", 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, " ", 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, " ", 4)
                                    Else
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, " ", 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, " ", 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, " ", 4)
                                    End If

                                Else
                                    If row.Item("ID_PAG") = id_pag Then
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, "", 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, "", 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, "", 4)
                                    Else
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, row.Item("CUP"), 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, row.Item("CIG"), 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, row.Item("NUM_MAE"), 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, row.Item("DATA_MAE"), 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, row.Item("IMPORTO_MAE"), 4)
                                    End If

                                End If

                            End If
                            id_pre = row.Item("ID_PRE")
                            id_mae = CInt(row.Item("ID_MAE"))
                            id_pag = CInt(row.Item("ID_PAG"))
                        End If
                    End If
                    i = i + 1
                    K = K + 1
                Next
                .CloseFile()
            End With
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String
            zipfic = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".zip")
            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            Dim strFile As String
            strFile = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)
            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            File.Delete(strFile)
            Response.Redirect("..\..\..\FileTemp\" & sNomeFile & ".zip")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    '###################################################################################################################
    '################################################# RIT. LEGGE ####################################################

    Protected Sub StampaTabellaRit()
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
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
                CONTROLLODATA = "AND DATA_CONSUNTIVAZIONE <='" & Request.QueryString("AL") & "' "
            End If

            Dim DATAALS As String = ""
            '****************************************************************************
            Dim dataAl As Integer
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
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
                DATAALS = CStr(Min(CLng(Format(Now, "yyyyMMdd")), CLng(par.IfNull(myReader1("FINE"), ""))))
                ANNO = Left(par.IfNull(myReader1("INIZIO"), ""), 4)

            End If
            myReader1.Close()
            '****************************************************************************

            If DATAALS <> "" And CONTROLLODATA = "" Then
                CONTROLLODATA = "AND DATA_CONSUNTIVAZIONE <='" & DATAALS & "' "
                dataAl = DATAALS
            End If
            sDataEsercizio = sDataEsercizio & " AL " & Right(dataAl, 2) & "/" & Mid(dataAl, 5, 2) & "/" & Left(dataAl, 4)

            lblTitolo.Text = ""
            If idStruttura <> "-1" Then
                controlloStruttura = "AND PRENOTAZIONI.ID_STRUTTURA='" & idStruttura & "' "
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & idStruttura & "'"
                Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LETTORE.Read Then
                    lblTitolo.Text = "SITUAZIONE RITENUTE DI LEGGE - STRUTTURA " & par.IfNull(LETTORE(0), "") & " - " & sDataEsercizio
                End If
                LETTORE.Close()
            Else
                lblTitolo.Text = "SITUAZIONE GENERALE RITENUTE DI LEGGE - " & sDataEsercizio
            End If

            '######## ELENCO DELLE PRENOTAZIONI PER ESERCIZIO FINANZIARIO STRUTTURA E VOCE SELEZIONATA ######
            par.cmd.CommandText = "SELECT PRENOTAZIONI.ID AS ID_PRE,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                & "PF_VOCI.CODICE AS CODICE,PF_VOCI.DESCRIZIONE AS VOCE," _
                & "NVL(RIT_LEGGE_IVATA,0) AS IMPORTO_PRENOTATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE PAGAMENTI.DESCRIZIONE END) AS DESCRIZIONE_PAG," _
                & "TRIM (TO_CHAR (nvl(manutenzioni.importo_consuntivato,0),'999G999G990D99')) AS IMPORTO," _
                & "TAB_STATI_ODL.DESCRIZIONE AS STATO_ODL," _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                & "PRENOTAZIONI.TIPO_PAGAMENTO AS TIPO_ADP," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "CASE WHEN PAGAMENTI.PROGR IS NOT NULL THEN PAGAMENTI.PROGR ||'/'|| PAGAMENTI.ANNO ELSE '' END  AS ADP," _
                & "TO_CHAR(TO_DATE(DATA_EMISSIONE,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_ADP," _
                & "TRIM(TO_CHAR(PAGAMENTI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO_ADP," _
                & "CASE WHEN TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) IS NULL THEN '0,00' ELSE TRIM(TO_CHAR(PRENOTAZIONI.RIT_LEGGE_IVATA,'999G999G990D99')) END AS RITLEGGE_ADP," _
                & "SISCOM_MI.GETSTATOPAGAMENTO(PAGAMENTI.ID) AS STATO_PAGAMENTO,PRENOTAZIONI.ID_APPALTO AS ID_A,PRENOTAZIONI.ID_FORNITORE AS ID_F,PRENOTAZIONI.ID_PAGAMENTO AS ID_P,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+) " _
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
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "and ((PRENOTAZIONI.ID_STATO=1 AND DATA_CONSUNTIVAZIONE <='" & dataAl & "') " _
                & "OR (PRENOTAZIONI.ID_STATO=2  AND DATA_CONSUNTIVAZIONE <='" & dataAl & "' AND DATA_CERTIFICAZIONE >'" & dataAl & "') OR" _
                & "(PRENOTAZIONI.ID_STATO=-3 AND PRENOTAZIONI.DATA_ANNULLO>'" & dataAl & "' AND DATA_CONSUNTIVAZIONE<='" & dataAl & "' AND (DATA_CERTIFICAZIONE IS NULL OR DATA_CERTIFICAZIONE>'" & dataAl & "')))" _
                & "AND NVL(RIT_LEGGE_IVATA,0)>0 " _
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
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
            dt.Columns.Add("NUM_REPERTORIO")
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
            dtExcel.Columns.Add("NUM_REPERTORIO")
            dtExcel.Columns.Add("ADP")
            dtExcel.Columns.Add("DATA_ADP")
            dtExcel.Columns.Add("IMPORTO_ADP")
            dtExcel.Columns.Add("RIT_LEGGE")
            dtExcel.Columns.Add("ID_PAG")
            dtExcel.Columns.Add("CUP")
            dtExcel.Columns.Add("CIG")
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
                RIGA.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.ID AS ID_MAE ," _
                    & " (SELECT CIG FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CIG," _
                    & " (SELECT CUP FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CUP," _
                    & " (SELECT NUMERO_PAGAMENTO||'/'||ANNO_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
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
                        RIGA.Item("MAE") = "<table style=""font-family: Arial, Helvetica, sans-serif; font-size: xx-small;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;"" bgcolor=""#EEEEEE"" width=""100%""><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CUP</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CIG</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">PROTOCOLLO</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">DATA</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">IMPORTO</th>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"

                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        check = 1
                        count = count + 1
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"


                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
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
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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

                        RIGAexcel.Item("CUP") = "TOTALE"
                        RIGAexcel.Item("CIG") = ""
                        RIGAexcel.Item("NUM_MAE") = ""
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
                    RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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

                    RIGAexcel.Item("CUP") = ""
                    RIGAexcel.Item("CIG") = ""
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
            RIGA.Item("NUM_REPERTORIO") = ""
            RIGA.Item("ADP") = "TOTALE ADP"
            RIGA.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
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
            RIGAexcel.Item("NUM_REPERTORIO") = ""
            RIGAexcel.Item("ADP") = "TOTALE ADP"
            RIGAexcel.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("ID_PAG") = ""
            RIGAexcel.Item("CUP") = ""
            RIGAexcel.Item("CIG") = ""
            RIGAexcel.Item("NUM_MAE") = ""
            RIGAexcel.Item("DATA_MAE") = ""
            RIGAexcel.Item("IMPORTO_MAE") = ""
            RIGAexcel.Item("ID_PRE") = ""
            RIGAexcel.Item("ID_MAE") = ""
            dtExcel.Rows.Add(RIGAexcel)

            lettorePagamenti.Close()
            If dt.Rows.Count > 1 Then

                DataGrid1.Visible = True
                btnExport.Visible = True
                btnStampaPDF.Visible = True
                lblErrore.Text = ""
                DataGrid1.DataSource = dt
                If idStruttura <> "-1" Then
                    DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "FILIALE")).Visible = False
                End If
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "DESCRIZIONE_PAG")).Visible = True
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "DATA_ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "IMPORTO_ADP")).Visible = False
                'DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "RIT_LEGGE")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "MAE")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "IMPORTO_PRENOTATO")).HeaderText = "IMPORTO RIT. LEGGE CONSUNTIVATE"
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
                btnExport.Visible = False
                btnStampaPDF.Visible = False
                lblErrore.Text = "La ricerca di ritenute di legge consuntivate non ha prodotto risultati"
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

    Protected Sub esportaRit()
        EsportaExcelAutomaticoDaDataGrid(DataGrid1, "importiRitLegge")
        ''#### EXPORT IN EXCEL ####
        'Try
        '    Dim myExcelFile As New CM.ExcelFile
        '    Dim i As Long
        '    Dim K As Long
        '    Dim sNomeFile As String
        '    Dim row As System.Data.DataRow
        '    Dim datatable As Data.DataTable
        '    datatable = Session.Item("PAG_STRU")
        '    'datatable = DT
        '    sNomeFile = "PRE_" & Format(Now, "yyyyMMddHHmmss")
        '    i = 0
        '    Dim nRighe As Integer = datatable.Rows.Count
        '    With myExcelFile
        '        .CreateFile(Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls"))
        '        .PrintGridLines = False
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
        '        .SetDefaultRowHeight(14)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
        '        .SetFont("Arial", 12, CM.ExcelFile.FontFormatting.xlsBold)
        '        .SetColumnWidth(1, 1, 14) 'codice
        '        .SetColumnWidth(2, 2, 70) 'voce
        '        .SetColumnWidth(3, 3, 70) 'tipo
        '        .SetColumnWidth(4, 4, 50) 'tipo
        '        .SetColumnWidth(5, 5, 60) 'filiale
        '        .SetColumnWidth(6, 6, 22) 'importo prenotazione
        '        .SetColumnWidth(7, 7, 52) 'fornitore
        '        K = 1
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont3, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, lblTitolo.Text)
        '        K = K + 1
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "CODICE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "VOCE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "TIPO PAGAMENTO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "DESCRIZIONE PAGAMENTO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "FILIALE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "IMPORTO PRENOTATO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "FORNITORE", 0)
        '        K = K + 1
        '        For Each row In datatable.Rows
        '            If row.Item("CODICE") = "TOTALE VOCI" Then
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, " ", 0)

        '            Else
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, row.Item("VOCE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, row.Item("DESCRIZIONE_TIPO"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, row.Item("DESCRIZIONE_PAG"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, row.Item("FILIALE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, row.Item("FORNITORE"), 0)
        '            End If
        '            i = i + 1
        '            K = K + 1
        '        Next
        '        .CloseFile()
        '    End With
        '    Dim objCrc32 As New Crc32()
        '    Dim strmZipOutputStream As ZipOutputStream
        '    Dim zipfic As String
        '    zipfic = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".zip")
        '    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        '    strmZipOutputStream.SetLevel(6)
        '    Dim strFile As String
        '    strFile = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls")
        '    Dim strmFile As FileStream = File.OpenRead(strFile)
        '    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '    strmFile.Read(abyBuffer, 0, abyBuffer.Length)
        '    Dim sFile As String = Path.GetFileName(strFile)
        '    Dim theEntry As ZipEntry = New ZipEntry(sFile)
        '    Dim fi As New FileInfo(strFile)
        '    theEntry.DateTime = fi.LastWriteTime
        '    theEntry.Size = strmFile.Length
        '    strmFile.Close()
        '    objCrc32.Reset()
        '    objCrc32.Update(abyBuffer)
        '    theEntry.Crc = objCrc32.Value
        '    strmZipOutputStream.PutNextEntry(theEntry)
        '    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        '    strmZipOutputStream.Finish()
        '    strmZipOutputStream.Close()
        '    File.Delete(strFile)
        '    Response.Redirect("..\..\..\FileTemp\" & sNomeFile & ".zip")
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub

    Protected Sub StampaTabellaRitCert()
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
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
                CONTROLLODATA = "AND DATA_CERTIFICAZIONE <='" & Request.QueryString("AL") & "' "
            End If

            Dim DATAALS As String = ""
            '****************************************************************************
            Dim dataAl As Integer
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
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
                DATAALS = CStr(Min(CLng(Format(Now, "yyyyMMdd")), CLng(par.IfNull(myReader1("FINE"), ""))))
                ANNO = Left(par.IfNull(myReader1("INIZIO"), ""), 4)
            End If
            myReader1.Close()
            '****************************************************************************

            If DATAALS <> "" And CONTROLLODATA = "" Then
                CONTROLLODATA = "AND DATA_CERTIFICAZIONE <='" & DATAALS & "' "
                dataAl = DATAALS
            End If
            sDataEsercizio = sDataEsercizio & " AL " & Right(dataAl, 2) & "/" & Mid(dataAl, 5, 2) & "/" & Left(dataAl, 4)

            lblTitolo.Text = ""
            If idStruttura <> "-1" Then
                controlloStruttura = "AND PRENOTAZIONI.ID_STRUTTURA='" & idStruttura & "' "
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & idStruttura & "'"
                Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LETTORE.Read Then
                    lblTitolo.Text = "SITUAZIONE RITENUTE DI LEGGE - STRUTTURA " & par.IfNull(LETTORE(0), "") & " - " & sDataEsercizio
                End If
                LETTORE.Close()
            Else
                lblTitolo.Text = "SITUAZIONE GENERALE RITENUTE DI LEGGE - " & sDataEsercizio
            End If

            '######## ELENCO DELLE PRENOTAZIONI PER ESERCIZIO FINANZIARIO STRUTTURA E VOCE SELEZIONATA ######
            par.cmd.CommandText = "SELECT PRENOTAZIONI.ID AS ID_PRE,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                & "PF_VOCI.CODICE AS CODICE,PF_VOCI.DESCRIZIONE AS VOCE," _
                & "NVL(RIT_LEGGE_IVATA,0) AS IMPORTO_PRENOTATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE PAGAMENTI.DESCRIZIONE END) AS DESCRIZIONE_PAG," _
                & "TRIM (TO_CHAR (nvl(manutenzioni.importo_consuntivato,0),'999G999G990D99')) AS IMPORTO," _
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
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND ((PRENOTAZIONI.id_stato=2 and data_certificazione<='" & dataAl & "') " _
                & " OR (PRENOTAZIONI.ID_STATO=-3 AND PRENOTAZIONI.DATA_ANNULLO>'" & dataAl & "' AND DATA_CONSUNTIVAZIONE<='" & dataAl & "' AND DATA_CERTIFICAZIONE<='" & dataAl & "'))" _
                & "AND NVL(RIT_LEGGE_IVATA,0)>0 " _
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
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
            dtExcel.Columns.Add("CUP")
            dtExcel.Columns.Add("CIG")
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
                par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.ID AS ID_MAE ," _
                    & " (SELECT CIG FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CIG," _
                    & " (SELECT CUP FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CUP," _
                    & " (SELECT NUMERO_PAGAMENTO||'/'||ANNO_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
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
                        RIGA.Item("MAE") = "<table style=""font-family: Arial, Helvetica, sans-serif; font-size: xx-small;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;"" bgcolor=""#EEEEEE"" width=""100%""><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CUP</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CIG</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">PROTOCOLLO</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">DATA</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">IMPORTO</th>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"

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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        check = 1
                        count = count + 1
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"


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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
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

                        RIGAexcel.Item("CUP") = "TOTALE"
                        RIGAexcel.Item("CIG") = ""
                        RIGAexcel.Item("NUM_MAE") = ""
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

                    RIGAexcel.Item("CUP") = ""
                    RIGAexcel.Item("CIG") = ""
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
            RIGA.Item("ADP") = "TOTALE ADP"
            RIGA.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
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
            RIGAexcel.Item("ADP") = "TOTALE ADP"
            RIGAexcel.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("ID_PAG") = ""
            RIGAexcel.Item("CUP") = ""
            RIGAexcel.Item("CIG") = ""
            RIGAexcel.Item("NUM_MAE") = ""
            RIGAexcel.Item("DATA_MAE") = ""
            RIGAexcel.Item("IMPORTO_MAE") = ""
            RIGAexcel.Item("ID_PRE") = ""
            RIGAexcel.Item("ID_MAE") = ""
            dtExcel.Rows.Add(RIGAexcel)

            lettorePagamenti.Close()
            If dt.Rows.Count > 1 Then

                DataGrid1.Visible = True
                btnExport.Visible = True
                btnStampaPDF.Visible = True
                lblErrore.Text = ""
                DataGrid1.DataSource = dt
                If idStruttura <> "-1" Then
                    DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "FILIALE")).Visible = False
                End If
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "DESCRIZIONE_PAG")).Visible = True
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "DATA_ADP")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "IMPORTO_ADP")).Visible = False
                'DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "RIT_LEGGE")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "MAE")).Visible = False
                DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "IMPORTO_PRENOTATO")).HeaderText = "IMPORTO RIT. LEGGE CERTIFICATE"
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
                btnExport.Visible = False
                btnStampaPDF.Visible = False
                lblErrore.Text = "La ricerca di ritenute di legge certificate non ha prodotto risultati"
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

    Protected Sub esportaRitCert()
        EsportaExcelAutomaticoDaDataGrid(DataGrid1, "importiRitLeggeCertificate")
        ''#### EXPORT IN EXCEL ####
        'Try
        '    Dim myExcelFile As New CM.ExcelFile
        '    Dim i As Long
        '    Dim K As Long
        '    Dim sNomeFile As String
        '    Dim row As System.Data.DataRow
        '    Dim datatable As Data.DataTable
        '    datatable = Session.Item("PAG_STRU")
        '    'datatable = DT
        '    sNomeFile = "PRE_" & Format(Now, "yyyyMMddHHmmss")
        '    i = 0
        '    Dim nRighe As Integer = datatable.Rows.Count
        '    With myExcelFile
        '        .CreateFile(Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls"))
        '        .PrintGridLines = False
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
        '        .SetDefaultRowHeight(14)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
        '        .SetFont("Arial", 12, CM.ExcelFile.FontFormatting.xlsBold)
        '        .SetColumnWidth(1, 1, 14) 'codice
        '        .SetColumnWidth(2, 2, 70) 'voce
        '        .SetColumnWidth(3, 3, 70) 'tipo
        '        .SetColumnWidth(4, 4, 50) 'tipo
        '        .SetColumnWidth(5, 5, 60) 'filiale
        '        .SetColumnWidth(6, 6, 22) 'importo prenotazione
        '        .SetColumnWidth(7, 7, 52) 'fornitore
        '        K = 1
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont3, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, lblTitolo.Text)
        '        K = K + 1
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "CODICE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "VOCE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "TIPO PAGAMENTO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "DESCRIZIONE PAGAMENTO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "FILIALE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "IMPORTO PRENOTATO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "FORNITORE", 0)
        '        K = K + 1
        '        For Each row In datatable.Rows
        '            If row.Item("CODICE") = "TOTALE VOCI" Then
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, " ", 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, " ", 0)

        '            Else
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, row.Item("VOCE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, row.Item("DESCRIZIONE_TIPO"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, row.Item("DESCRIZIONE_PAG"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, row.Item("FILIALE"), 0)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
        '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, row.Item("FORNITORE"), 0)
        '            End If
        '            i = i + 1
        '            K = K + 1
        '        Next
        '        .CloseFile()
        '    End With
        '    Dim objCrc32 As New Crc32()
        '    Dim strmZipOutputStream As ZipOutputStream
        '    Dim zipfic As String
        '    zipfic = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".zip")
        '    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        '    strmZipOutputStream.SetLevel(6)
        '    Dim strFile As String
        '    strFile = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls")
        '    Dim strmFile As FileStream = File.OpenRead(strFile)
        '    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '    strmFile.Read(abyBuffer, 0, abyBuffer.Length)
        '    Dim sFile As String = Path.GetFileName(strFile)
        '    Dim theEntry As ZipEntry = New ZipEntry(sFile)
        '    Dim fi As New FileInfo(strFile)
        '    theEntry.DateTime = fi.LastWriteTime
        '    theEntry.Size = strmFile.Length
        '    strmFile.Close()
        '    objCrc32.Reset()
        '    objCrc32.Update(abyBuffer)
        '    theEntry.Crc = objCrc32.Value
        '    strmZipOutputStream.PutNextEntry(theEntry)
        '    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        '    strmZipOutputStream.Finish()
        '    strmZipOutputStream.Close()
        '    File.Delete(strFile)
        '    Response.Redirect("..\..\..\FileTemp\" & sNomeFile & ".zip")
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub

    '###################################################################################################################
    '################################################# RIT LEGGE LIQUIDATE ####################################################

    Protected Sub StampaTabellaRitLiq()
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
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
                CONTROLLODATA = "AND PAGAMENTI_LIQUIDATI.DATA_MANDATO <='" & Request.QueryString("AL") & "' "
            End If
            Dim DATAALS As String = ""
            '****************************************************************************
            Dim dataAl As Integer
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
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
                DATAALS = CStr(Min(CLng(Format(Now, "yyyyMMdd")), CLng(par.IfNull(myReader1("FINE"), ""))))
                ANNO = Left(par.IfNull(myReader1("INIZIO"), ""), 4)

            End If
            myReader1.Close()
            '****************************************************************************

            If DATAALS <> "" And CONTROLLODATA = "" Then
                CONTROLLODATA = "AND PAGAMENTI_LIQUIDATI.DATA_MANDATO <='" & DATAALS & "' "
                dataAl = DATAALS
            End If
            sDataEsercizio = sDataEsercizio & " AL " & Right(dataAl, 2) & "/" & Mid(dataAl, 5, 2) & "/" & Left(dataAl, 4)

            lblTitolo.Text = ""
            If idStruttura <> "-1" Then
                controlloStruttura = "AND PRENOTAZIONI.ID_STRUTTURA='" & idStruttura & "' "
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & idStruttura & "'"
                Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LETTORE.Read Then
                    lblTitolo.Text = "SITUAZIONE RITENUTE DI LEGGE LIQUIDATE - STRUTTURA " & par.IfNull(LETTORE(0), "") & " - " & sDataEsercizio
                End If
                LETTORE.Close()
            Else
                lblTitolo.Text = "SITUAZIONE GENERALE RITENUTE DI LEGGE LIQUIDATE " & " - " & sDataEsercizio
            End If

            '######## ELENCO DEI PAGAMENTI PER STRUTTURA ED ESERCIZIO FINANZIARIO ######
            par.cmd.CommandText = "SELECT DISTINCT PRENOTAZIONI.ID AS ID_PRE,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                & "PF_VOCI.CODICE AS CODICE,PF_VOCI.DESCRIZIONE AS VOCE," _
                & "(CASE WHEN PRENOTAZIONI.IMPORTO_APPROVATO IS NULL THEN TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_PRENOTATO,'999G999G990D99')) ELSE TRIM(TO_CHAR(NVL(PRENOTAZIONI.IMPORTO_APPROVATO,0)-NVL(RIT_LEGGE_IVATA,0),'999G999G990D99')) END) AS IMPORTO_PRENOTATO," _
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
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE=PAGAMENTI.ID(+) " _
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
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
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
            dtExcel.Columns.Add("CUP")
            dtExcel.Columns.Add("CIG")
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
                    & "(CASE WHEN PRENOTAZIONI.IMPORTO_APPROVATO IS NULL THEN TRIM(TO_CHAR(NVL(PRENOTAZIONI.IMPORTO_PRENOTATO,0),'999G999G990D99')) ELSE TRIM(TO_CHAR(NVL(PRENOTAZIONI.IMPORTO_APPROVATO,0)-NVL(RIT_LEGGE_IVATA,0),'999G999G990D99')) END) AS IMPORTO_PRENOTATO,PRENOTAZIONI.ID " _
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
                    'RIGA.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                    RIGA.Item("IMPORTO_ADP") = totaleImportoApprovato
                    IMPORTOADPEXCEL = Format(CDec(totaleImportoApprovato), "##,##0.00")
                    'ImportoADPperSomma = CDec(par.IfNull(lettorePagamenti("IMPORTO_ADP"), ""))
                    ImportoADPperSomma = totaleImportoApprovato
                Else
                    If nMae > 1 Then
                        'SE CI SONO PIù MANDATI INDICO IL PARZIALE FINO A QUEL MOMENTO LIQUIDATO E TRA PARENTESI IL TOTALE COMPLESSIVO DEI MANDATI
                        RIGA.Item("IMPORTO_ADP") = totaleImportoApprovato & "<br />(" & par.IfNull(lettorePagamenti("IMPORTO_ADP"), 0) & ")"
                        IMPORTOADPEXCEL = Format(CDec(totaleImportoApprovato), "##,##0.00")
                        ImportoADPperSomma = totaleImportoApprovato
                    Else
                        'RIGA.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                        RIGA.Item("IMPORTO_ADP") = totaleImportoApprovato
                        IMPORTOADPEXCEL = Format(CDec(totaleImportoApprovato), "##,##0.00")
                        'ImportoADPperSomma = CDec(par.IfNull(lettorePagamenti("IMPORTO_ADP"), ""))
                        ImportoADPperSomma = totaleImportoApprovato
                    End If
                End If
                RIGA.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")
                RIGA.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), "")
                RIGA.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), "")
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'MAE
                'LEGGO TUTTI I MANDATI RELATIVI AL PAGAMENTO, CONSIDERO SE ILLUMINARLI GIALLI O ROSSI
                par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.ID AS ID_MAE ," _
                    & " (SELECT CIG FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CIG," _
                    & " (SELECT CUP FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CUP," _
                    & " (SELECT NUMERO_PAGAMENTO||'/'||ANNO_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,DATA_MANDATO AS DM,IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
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
                            & " (SELECT CIG FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CIG," _
                    & " (SELECT CUP FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CUP," _
                            & "(SELECT NUMERO_PAGAMENTO||'/'||ANNO_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS MAE," _
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
                        RIGA.Item("MAE") = "<table style=""font-family: Arial, Helvetica, sans-serif; font-size: xx-small;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;"" bgcolor=""#EEEEEE"" width=""100%""><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CUP</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CIG</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">PROTOCOLLO</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">DATA</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">IMPORTO</th>"
                        Dim BGCOLOR As String = ""
                        If nMae > 1 Then
                            If giallo Then
                                BGCOLOR = "bgcolor=""#FFFF66"""
                            End If
                        End If
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr " & BGCOLOR & " " & colorRed & "><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
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
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr " & BGCOLOR & " " & colorRed & "><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
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

                        RIGAexcel.Item("CUP") = "TOTALE"
                        RIGAexcel.Item("CIG") = ""
                        RIGAexcel.Item("NUM_MAE") = ""
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

                    RIGAexcel.Item("CUP") = ""
                    RIGAexcel.Item("CIG") = ""
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
            RIGA.Item("ADP") = "TOTALE ADP"
            'RIGA.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
            RIGA.Item("DATA_ADP") = ""
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
            RIGAexcel.Item("ADP") = "TOTALE ADP"
            'RIGAexcel.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("DATA_ADP") = ""
            RIGAexcel.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("ID_PAG") = ""
            RIGAexcel.Item("CUP") = ""
            RIGAexcel.Item("CIG") = ""
            RIGAexcel.Item("NUM_MAE") = ""
            RIGAexcel.Item("DATA_MAE") = ""
            RIGAexcel.Item("IMPORTO_MAE") = ""
            RIGAexcel.Item("ID_PRE") = ""
            RIGAexcel.Item("ID_MAE") = ""
            dtExcel.Rows.Add(RIGAexcel)

            lettorePagamenti.Close()
            If dt.Rows.Count > 1 Then

                DataGrid1.Visible = True
                btnExport.Visible = True
                btnStampaPDF.Visible = True
                lblErrore.Text = ""
                'DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "RIT_LEGGE")).Visible = False
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
                btnExport.Visible = False
                btnStampaPDF.Visible = False
                lblErrore.Text = "La ricerca di ritenute di legge liquidate non ha prodotto risultati"
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

    Protected Sub esportaRitLiq()
        EsportaExcelAutomaticoDaDataGrid(DataGrid1, "importiRitLeggeLiquidate")
    End Sub

    '###################################################################################################################

    Protected Sub GeneraPDF(ByVal datagrid1 As DataGrid)
        Dim Html As String = ""
        Dim stringWriter As New System.IO.StringWriter
        Dim sourcecode As New HtmlTextWriter(stringWriter)

        stringWriter = New System.IO.StringWriter
        sourcecode = New HtmlTextWriter(stringWriter)
        DataGrid1.RenderControl(sourcecode)
        sourcecode.Flush()
        Html = Html & stringWriter.ToString

        Html = eliminaLink(Html)
        Dim url As String = Server.MapPath("..\..\..\FileTemp\")
        Dim pdfConverter1 As PdfConverter = New PdfConverter

        Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
        If Licenza <> "" Then
            pdfConverter1.LicenseKey = Licenza
        End If

        pdfConverter1.PageWidth = 1600
        pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
        pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        pdfConverter1.PdfDocumentOptions.ShowHeader = True
        pdfConverter1.PdfDocumentOptions.ShowFooter = True
        pdfConverter1.PdfDocumentOptions.LeftMargin = 10
        pdfConverter1.PdfDocumentOptions.RightMargin = 15
        pdfConverter1.PdfDocumentOptions.TopMargin = 10
        pdfConverter1.PdfDocumentOptions.BottomMargin = 10
        pdfConverter1.PdfHeaderOptions.HeaderHeight = 60
        pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
        pdfConverter1.PdfHeaderOptions.HeaderText = UCase(lblTitolo.Text)
        pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
        pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 14
        pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold

        pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontType = PdfFontType.HelveticaBold
        pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 10

        pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
        pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.Color.Blue
        pdfConverter1.PdfHeaderOptions.HeaderSubtitleText = ""
        pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.Color.Red
        pdfConverter1.PdfFooterOptions.FooterText = ("")
        pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
        pdfConverter1.PdfFooterOptions.DrawFooterLine = False
        pdfConverter1.PdfFooterOptions.PageNumberText = "pag."
        pdfConverter1.PdfFooterOptions.ShowPageNumber = False

        Dim nomefile As String = "ExpDetPreventivi" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
        pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & nomefile)
        Response.Write("<script>window.open('../../../FileTemp/" & nomefile & "','ExpDetPreventivi','');</script>")
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
            Html = stringWriter.ToString
            'LUNGHEZZA TOTALE DELLA STRINGA HTML
            Dim lunghezzaHTML As String = Html.Length
            'DIVISIONE DELLA STRINGA IN 2, CHIUDO ALLA FINE DELLA RIGA LA TABELLA E LA RIAPRO ALLA SUCCESSIVA
            Dim lunghezzaSplit1 As Integer = Html.Length / 2
            Dim split1 As String = Html.Substring(0, lunghezzaSplit1)
            Dim indiceSplit1 As Integer = split1.LastIndexOf("</tr>")
            split1 = Html.Substring(0, indiceSplit1) & "</tr></table>"
            'DETERMINO L'INDICE DELLA SECONDA PARTE DELLA STRINGA, CONTROLLANDO CHE CI SIANO DEI ROWSPAN PER DIVIDERE PER BENE I DUE BLOCCHI
            Dim indiceInizioSplit2 As String = indiceSplit1 + 4
            Dim split2 As String = Html.Substring(indiceInizioSplit2)
            split2 = Html.Substring(indiceInizioSplit2, split2.IndexOf("rowspan"))
            Dim indiceInterruzione As Integer = indiceInizioSplit2 + 4 + split2.LastIndexOf("<tr")
            split1 = Html.Substring(0, indiceInterruzione - 4) & "</table>"
            'IMPOSTO LO STILE DELLA TABELLA
            split2 = "<table cellspacing=""0"" cellpadding=""4"" rules=""all"" border=""1"" id=""DataGrid1"" style=""color:#333333;border-color:#507CD1;border-width:1px;border-style:Solid;width:100%;border-collapse:collapse;""><tr " & Html.Substring(indiceInterruzione)

            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
            Dim pdfConverter As PdfConverter = New PdfConverter
            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter.LicenseKey = Licenza
            End If

            pdfConverter.PageWidth = "1600"
            pdfConverter.PdfDocumentOptions.PdfPageSize = ExpertPdf.HtmlToPdf.PdfPageSize.A4
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = ExpertPdf.HtmlToPdf.PDFPageOrientation.Landscape
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = ExpertPdf.HtmlToPdf.PdfCompressionLevel.NoCompression
            pdfConverter.PdfDocumentOptions.InternalLinksEnabled = False
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
            pdfConverter.SavePdfFromHtmlStringToFile(eliminaLink(split1), url & "_1" & NomeFile)
            pdfConverter.SavePdfFromHtmlStringToFile(eliminaLink(split2), url & "_2" & NomeFile)
            DataGrid1.HeaderStyle.ForeColor = Color.White

            Dim pdfDocument As ExpertPdf.MergePdf.PdfDocumentOptions = New ExpertPdf.MergePdf.PdfDocumentOptions
            Dim PDFMerge As ExpertPdf.MergePdf.PDFMerge = New ExpertPdf.MergePdf.PDFMerge(pdfDocument)
            PDFMerge.AppendPDFFile(url & "_1" & NomeFile)
            PDFMerge.AppendPDFFile(url & "_2" & NomeFile)
            PDFMerge.SaveMergedPDFToFile(url & NomeFile)

            Response.Write("<script>window.open('..\\..\\..\\FileTemp\\" & NomeFile & "','StampePag');</script>")
            '*******************************************************************************************************
        Catch ex As Exception
            Response.Write("<script>alert('Si è verificato un errore durante la stampa del documento!');self.close();</script>")
        End Try

    End Sub

    Protected Sub StampaTabellaPagamenti()
        If Request.QueryString("IDS") = "-1" Then
            Response.Redirect("RisultatiPagamentiPerStrutturaCompleta.aspx?IDS=-1&EF=" & Request.QueryString("EF"), True)
        End If
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
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
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
                Dim DATAAL2 As String = CStr(Min(CLng(Format(Now, "yyyyMMdd")), CLng(par.IfNull(myReader1("FINE"), ""))))
                sDataEsercizio = sDataEsercizio & " AL " & par.FormattaData(DATAAL2)
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
                & "SISCOM_MI.GETSTATOPAGAMENTO(PAGAMENTI.ID) AS STATO_PAGAMENTO,PRENOTAZIONI.ID_APPALTO AS ID_A,PRENOTAZIONI.ID_FORNITORE AS ID_F,PRENOTAZIONI.ID_PAGAMENTO AS ID_P,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
                & "WHERE " & condizionePU & " PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+) " _
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
            dt.Columns.Add("NUM_REPERTORIO")
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
            dtExcel.Columns.Add("NUM_REPERTORIO")
            dtExcel.Columns.Add("ADP")
            dtExcel.Columns.Add("DATA_ADP")
            dtExcel.Columns.Add("IMPORTO_ADP")
            dtExcel.Columns.Add("RIT_LEGGE")
            dtExcel.Columns.Add("ID_PAG")
            dtExcel.Columns.Add("CUP")
            dtExcel.Columns.Add("CIG")
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
                RIGA.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                'par.cmd.CommandText = "SELECT PAGAMENTI_LIQUIDATI.ID AS ID_MAE ,(SELECT CIG FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS CIG,(SELECT CUP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS CUP,(SELECT NUMERO_PAGAMENTO||'/'||ANNO_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                '    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) & "'"
                par.cmd.CommandText = "SELECT id_pagamento_mm AS ID_MAE ," _
                    & " (SELECT CIG FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CIG," _
                    & " (SELECT CUP FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CUP," _
                    & " (SELECT NUMERO_PAGAMENTO||'/'||ANNO_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,sum(IMPORTO) as importo FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO='" & par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) & "' group by id_pagamento_mm,data_mandato"
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
                        RIGA.Item("MAE") = "<table style=""font-family: Arial, Helvetica, sans-serif; font-size: xx-small;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;"" bgcolor=""#EEEEEE"" width=""100%""><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CUP</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CIG</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">PROTOCOLLO</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">DATA</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">IMPORTO</th>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"

                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        check = 1
                        count = count + 1
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"


                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
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
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td colspan='4' style=""border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">TOTALE</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(importoTotale, "##,##0.00") & "</td></tr>"
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
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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

                        RIGAexcel.Item("CUP") = "TOTALE"
                        RIGAexcel.Item("CIG") = ""
                        RIGAexcel.Item("NUM_MAE") = ""
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
                    RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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

                    RIGAexcel.Item("CUP") = ""
                    RIGAexcel.Item("CIG") = ""
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
            RIGA.Item("NUM_REPERTORIO") = ""
            RIGA.Item("ADP") = "TOTALE ADP"
            RIGA.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
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
            RIGAexcel.Item("NUM_REPERTORIO") = ""
            RIGAexcel.Item("ADP") = "TOTALE ADP"
            RIGAexcel.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("ID_PAG") = ""
            RIGAexcel.Item("CUP") = ""
            RIGAexcel.Item("CIG") = ""
            RIGAexcel.Item("NUM_MAE") = ""
            RIGAexcel.Item("DATA_MAE") = ""
            RIGAexcel.Item("IMPORTO_MAE") = ""
            RIGAexcel.Item("ID_PRE") = ""
            RIGAexcel.Item("ID_MAE") = ""
            dtExcel.Rows.Add(RIGAexcel)

            lettorePagamenti.Close()
            If dt.Rows.Count > 1 Then
                DataGrid1.Visible = True
                btnExport.Visible = True
                btnStampaPDF.Visible = True
                lblErrore.Text = ""
                DataGrid1.DataSource = dt
                If idStruttura <> "-1" Then
                    DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "FILIALE")).Visible = False
                End If
                If Session.Item("BP_GENERALE") <> 1 Then
                    DataGrid1.Columns.Item(TrovaIndiceColonna(DataGrid1, "MAE")).Visible = False
                End If
                Session.Add("PAG_STRU", dtExcel)
                DataGrid1.DataBind()
                If Request.QueryString("IDS") = "-1" Then
                    Pagamento.Value = "1"
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
                btnExport.Visible = False
                btnStampaPDF.Visible = False
                lblErrore.Text = "La ricerca di pagamenti non ha prodotto risultati"
            End If

            '###############
            'PERCHè CI METTE TROPPO TEMPO
            If Pagamento.Value = "1" Then
                btnStampaPDF.Visible = False
            End If
            '###############

            'Response.Expires = -1500
            'Response.AddHeader("PRAGMA", "NO-CACHE")
            'Response.AddHeader("CACHE-CONTROL", "PRIVATE")
            'Response.CacheControl = "PRIVATE"

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

    Protected Sub StampaTabellaPagamentiRitLegge()
        If Request.QueryString("IDS") = "-1" Then
            Response.Redirect("RisultatiPagamentiPerStrutturaCompleta.aspx?IDS=-1&EF=" & Request.QueryString("EF"), True)
        End If
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
            If Not IsNothing(Request.QueryString("AL")) And Request.QueryString("AL") <> "" Then
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
                Dim DATAAL2 As String = CStr(Min(CLng(Format(Now, "yyyyMMdd")), CLng(par.IfNull(myReader1("FINE"), ""))))
                sDataEsercizio = sDataEsercizio & " AL " & par.FormattaData(DATAAL2)
            End If
            myReader1.Close()
            '****************************************************************************



            lblTitoloRit.Text = ""
            If idStruttura <> "-1" Then
                controlloStruttura = "AND PRENOTAZIONI.ID_STRUTTURA='" & idStruttura & "' "
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & idStruttura & "'"
                Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LETTORE.Read Then
                    lblTitoloRit.Text = "SITUAZIONE PAGAMENTI RIT LEGGE - STRUTTURA " & par.IfNull(LETTORE(0), "") & " - " & sDataEsercizio
                End If
                LETTORE.Close()
            Else
                lblTitoloRit.Text = "SITUAZIONE GENERALE PAGAMENTI " & " - " & sDataEsercizio
            End If

            '######## ELENCO DEI PAGAMENTI PER STRUTTURA ED ESERCIZIO FINANZIARIO ######
            par.cmd.CommandText = "SELECT PRENOTAZIONI.ID AS ID_PRE,PRENOTAZIONI.ID_PAGAMENTO_rit_legge AS ID_PAGAMENTO," _
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
                & "SISCOM_MI.GETSTATOPAGAMENTO(PAGAMENTI.ID) AS STATO_PAGAMENTO,PRENOTAZIONI.ID_APPALTO AS ID_A,PRENOTAZIONI.ID_FORNITORE AS ID_F,PRENOTAZIONI.ID_PAGAMENTO_rit_legge AS ID_P,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
                & "WHERE " & condizionePU & " PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE=PAGAMENTI.ID(+) " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND TAB_STATI_PAGAMENTI.ID(+)=PAGAMENTI.ID_STATO " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND TAB_STATI_ODL.ID(+)=MANUTENZIONI.STATO " _
                & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & esercizioFinanziario & "' " _
                & controlloStruttura _
                & "AND PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE IS NOT NULL " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "ORDER BY PRENOTAZIONI.ID_PAGAMENTO_rit_legge,MANUTENZIONI.PROGR ASC "

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
            dt.Columns.Add("NUM_REPERTORIO")
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
            dtExcelrit.Columns.Clear()
            dtExcelrit.Rows.Clear()
            dtExcelrit.Columns.Add("CODICE")
            dtExcelrit.Columns.Add("VOCE")
            dtExcelrit.Columns.Add("DESCRIZIONE_TIPO")
            dtExcelrit.Columns.Add("FILIALE")
            dtExcelrit.Columns.Add("IMPORTO_PRENOTATO")
            dtExcelrit.Columns.Add("DESCRIZIONE_PAG")
            dtExcelrit.Columns.Add("IMPORTO")
            dtExcelrit.Columns.Add("FORNITORE")
            dtExcelrit.Columns.Add("NUM_REPERTORIO")
            dtExcelrit.Columns.Add("ADP")
            dtExcelrit.Columns.Add("DATA_ADP")
            dtExcelrit.Columns.Add("IMPORTO_ADP")
            dtExcelrit.Columns.Add("RIT_LEGGE")
            dtExcelrit.Columns.Add("ID_PAG")
            dtExcelrit.Columns.Add("CUP")
            dtExcelrit.Columns.Add("CIG")
            dtExcelrit.Columns.Add("NUM_MAE")
            dtExcelrit.Columns.Add("DATA_MAE")
            dtExcelrit.Columns.Add("IMPORTO_MAE")
            dtExcelrit.Columns.Add("ID_PRE")
            dtExcelrit.Columns.Add("ID_MAE")
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
                RIGA.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
                RIGA.Item("TIPO_ADP") = par.IfNull(lettorePagamenti("TIPO_ADP"), "")
                If par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "3" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "4" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "6" And par.IfNull(lettorePagamenti("TIPO_ADP"), "") <> "7" Then
                    RIGA.Item("ADP") = par.IfNull(lettorePagamenti("ADP"), "")
                Else
                    RIGA.Item("ADP") = "<a href=""javascript:ApriPagamenti(" & par.IfNull(lettorePagamenti("ID_P"), -1) & "," & par.IfNull(lettorePagamenti("TIPO_ADP"), 0) & ")"">" & par.IfNull(lettorePagamenti("ADP"), "") & "</a>"
                End If
                RIGA.Item("DATA_ADP") = par.IfNull(lettorePagamenti("DATA_ADP"), "")
                RIGA.Item("IMPORTO_ADP") = par.IfNull(lettorePagamenti("IMPORTO_ADP"), "")
                RIGA.Item("RIT_LEGGE") = par.IfNull(lettorePagamenti("RITLEGGE_ADP"), "")
                RIGA.Item("ID_PAG") = par.IfNull(lettorePagamenti("ID_PAGAMENTO_RIT_LEGGE"), "")
                RIGA.Item("ID_PRE") = par.IfNull(lettorePagamenti("ID_PRE"), "")

                '@@@@@@@@@@@@@@@@ MAE @@@@@@@@@@@@@@@@@
                par.cmd.CommandText = "SELECT PAGAMENTI_RIT_LIQUIDATI.ID AS ID_MAE ," _
                    & " (SELECT CIG FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CIG," _
                    & " (SELECT CUP FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER((SELECT NUMERO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)) AND ANNO=TO_NUMBER((SELECT ANNO_CDP FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID)))) AS CUP," _
                    & " (SELECT NUMERO_PAGAMENTO||'/'||ANNO_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE ID_PAGAMENTO_MM=PAGAMENTI_MM.ID) AS MAE,TO_CHAR(TO_DATE(DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_MANDATO,IMPORTO FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI " _
                    & "WHERE ID_PAGAMENTO_RIT_LEGGE='" & par.IfNull(lettorePagamenti("ID_PAGAMENTO_RIT_LEGGE"), -1) & "'"
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



                    RIGAexcel = dtExcelrit.NewRow
                    If check = 0 Then
                        RIGA.Item("MAE") = "<table style=""font-family: Arial, Helvetica, sans-serif; font-size: xx-small;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;"" bgcolor=""#EEEEEE"" width=""100%""><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CUP</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">CIG</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">PROTOCOLLO</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">DATA</th><th style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">IMPORTO</th>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"

                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        check = 1
                        count = count + 1
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CUP"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("CIG"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("MAE"), "") & "</td><td style=""width:33%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & par.IfNull(lettoreMAE("DATA_MANDATO"), "") & "</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00") & "</td></tr>"


                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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
                            RIGAexcel.Item("CUP") = par.IfNull(lettoreMAE("CUP"), "")
                            RIGAexcel.Item("CIG") = par.IfNull(lettoreMAE("CIG"), "")
                            RIGAexcel.Item("NUM_MAE") = par.IfNull(lettoreMAE("MAE"), "")
                            RIGAexcel.Item("DATA_MAE") = par.IfNull(lettoreMAE("DATA_MANDATO"), "")
                            RIGAexcel.Item("IMPORTO_MAE") = Format(par.IfNull(lettoreMAE("IMPORTO"), 0), "##,##0.00")
                            RIGAexcel.Item("ID_MAE") = par.IfNull(lettoreMAE("ID_MAE"), -1)
                        Else
                            RIGAexcel.Item("CUP") = ""
                            RIGAexcel.Item("CIG") = ""
                            RIGAexcel.Item("NUM_MAE") = ""
                            RIGAexcel.Item("DATA_MAE") = ""
                            RIGAexcel.Item("IMPORTO_MAE") = ""
                            RIGAexcel.Item("ID_MAE") = -1
                        End If

                        count = count + 1
                    End If
                    dtExcelrit.Rows.Add(RIGAexcel)

                    importoTotale = importoTotale + par.IfNull(lettoreMAE("IMPORTO"), 0)
                End While
                lettoreMAE.Close()


                If RIGA.Item("MAE") <> "" Then
                    If count = 1 Then
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "</table>"
                    Else
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "<tr><td colspan='2' style=""border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">TOTALE</td><td align=""right"" style=""width:34%;border-width:1px;border-color:gray;border-collapse:collapse;border-style:solid;"">" & Format(importoTotale, "##,##0.00") & "</td></tr>"
                        RIGA.Item("MAE") = RIGA.Item("MAE") & "</table>"

                        RIGAexcel = dtExcelrit.NewRow

                        RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                        RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                        RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                        RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                        RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                        RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                        RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                        RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                        RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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

                        RIGAexcel.Item("CUP") = "TOTALE"
                        RIGAexcel.Item("CIG") = ""
                        RIGAexcel.Item("NUM_MAE") = ""
                        RIGAexcel.Item("DATA_MAE") = ""
                        RIGAexcel.Item("IMPORTO_MAE") = Format(importoTotale, "##,##0.00")
                        RIGAexcel.Item("ID_MAE") = -1
                        dtExcelrit.Rows.Add(RIGAexcel)

                    End If
                Else
                    RIGAexcel = dtExcelrit.NewRow

                    RIGAexcel.Item("CODICE") = par.IfNull(lettorePagamenti("CODICE"), "")
                    RIGAexcel.Item("VOCE") = par.IfNull(lettorePagamenti("VOCE"), "")
                    RIGAexcel.Item("DESCRIZIONE_TIPO") = par.IfNull(lettorePagamenti("DESCRIZIONE_TIPO"), "")
                    RIGAexcel.Item("IMPORTO_PRENOTATO") = par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), "")
                    RIGAexcel.Item("DESCRIZIONE_PAG") = par.IfNull(lettorePagamenti("DESCRIZIONE_PAG"), "")
                    RIGAexcel.Item("FILIALE") = par.IfNull(lettorePagamenti("FILIALE"), "")
                    RIGAexcel.Item("IMPORTO") = par.IfNull(lettorePagamenti("IMPORTO"), "")
                    RIGAexcel.Item("FORNITORE") = par.IfNull(lettorePagamenti("FORNITORE"), "")
                    RIGAexcel.Item("NUM_REPERTORIO") = par.IfNull(lettorePagamenti("NUM_REPERTORIO"), "")
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

                    RIGAexcel.Item("CUP") = ""
                    RIGAexcel.Item("CIG") = ""
                    RIGAexcel.Item("NUM_MAE") = ""
                    RIGAexcel.Item("DATA_MAE") = ""
                    RIGAexcel.Item("IMPORTO_MAE") = ""
                    RIGAexcel.Item("ID_MAE") = -1
                    dtExcelrit.Rows.Add(RIGAexcel)
                End If
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                dt.Rows.Add(RIGA)

                If idPagamentoRitLeggePrec.Value <> par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1) Then
                    totaleADP = totaleADP + CDec(par.IfNull(lettorePagamenti("IMPORTO_ADP"), 0))
                End If
                totaleRITLEGGEADP = totaleRITLEGGEADP + CDec(par.IfNull(lettorePagamenti("RITLEGGE_ADP"), 0))
                totalePrenotazione = totalePrenotazione + CDec(par.IfNull(lettorePagamenti("IMPORTO_PRENOTATO"), 0))

                idPagamentoRitLeggePrec.Value = par.IfNull(lettorePagamenti("ID_PAGAMENTO"), -1)
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
            RIGA.Item("NUM_REPERTORIO") = ""
            RIGA.Item("ADP") = "TOTALE ADP"
            RIGA.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
            RIGA.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGA.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGA.Item("ID_PAG") = ""
            RIGA.Item("MAE") = ""
            RIGA.Item("ID_PRE") = ""
            dt.Rows.Add(RIGA)

            RIGAexcel = dtExcelrit.NewRow
            RIGAexcel.Item("CODICE") = "TOTALE VOCI"
            RIGAexcel.Item("VOCE") = ""
            RIGAexcel.Item("FILIALE") = ""
            RIGAexcel.Item("DESCRIZIONE_TIPO") = ""
            RIGAexcel.Item("IMPORTO_PRENOTATO") = Format(totalePrenotazione, "##,##0.00")
            RIGAexcel.Item("DESCRIZIONE_PAG") = ""
            RIGAexcel.Item("IMPORTO") = ""
            RIGAexcel.Item("FORNITORE") = ""
            RIGAexcel.Item("NUM_REPERTORIO") = ""
            RIGAexcel.Item("ADP") = "TOTALE ADP"
            RIGAexcel.Item("DATA_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("IMPORTO_ADP") = Format(totaleADP, "##,##0.00")
            RIGAexcel.Item("RIT_LEGGE") = Format(totaleRITLEGGEADP, "##,##0.00")
            RIGAexcel.Item("ID_PAG") = ""
            RIGAexcel.Item("CUP") = ""
            RIGAexcel.Item("CIG") = ""
            RIGAexcel.Item("NUM_MAE") = ""
            RIGAexcel.Item("DATA_MAE") = ""
            RIGAexcel.Item("IMPORTO_MAE") = ""
            RIGAexcel.Item("ID_PRE") = ""
            RIGAexcel.Item("ID_MAE") = ""
            dtExcelrit.Rows.Add(RIGAexcel)

            lettorePagamenti.Close()
            If dt.Rows.Count > 1 Then
                DataGridRit.Visible = True
                excelRit.Visible = True
                stampaRit.Visible = True
                lblErroreRit.Text = ""
                DataGridRit.DataSource = dt
                If idStruttura <> "-1" Then
                    DataGridRit.Columns.Item(TrovaIndiceColonna(DataGridRit, "FILIALE")).Visible = False
                End If
                If Session.Item("BP_GENERALE") <> 1 Then
                    DataGridRit.Columns.Item(TrovaIndiceColonna(DataGridRit, "MAE")).Visible = False
                End If
                'Session.Add("PAG_STRU_rit", dtExcelrit)
                DataGridRit.DataBind()
                If Request.QueryString("IDS") = "-1" Then
                    Pagamento.Value = "1"
                End If
                For Each di As DataGridItem In DataGridRit.Items
                    If di.Cells(1).Text.Contains("TOTALE VOCI") Then
                        di.ForeColor = Drawing.Color.Red
                        di.BackColor = Drawing.Color.Lavender
                        For j As Integer = 0 To di.Cells.Count - 1
                            di.Cells(j).Font.Bold = True
                        Next
                    End If
                Next
            Else
                DataGridRit.Visible = False
                excelRit.Visible = False
                stampaRit.Visible = False
                lblErroreRit.Text = "La ricerca di ritenute non ha prodotto risultati"
            End If

            '###############
            'PERCHè CI METTE TROPPO TEMPO
            If Pagamento.Value = "1" Then
                btnStampaPDF.Visible = False
            End If
            '###############

            'Response.Expires = -1500
            'Response.AddHeader("PRAGMA", "NO-CACHE")
            'Response.AddHeader("CACHE-CONTROL", "PRIVATE")
            'Response.CacheControl = "PRIVATE"

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

    Protected Sub esportaPagamenti()
        '#### EXPORT IN EXCEL ####
        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            Dim datatable As Data.DataTable
            datatable = Session.Item("PAG_STRU")
            'datatable = DT
            sNomeFile = "PAG_" & Format(Now, "yyyyMMddHHmmss")
            i = 0
            Dim nRighe As Integer = datatable.Rows.Count
            With myExcelFile
                .CreateFile(Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Arial", 12, CM.ExcelFile.FontFormatting.xlsBold)
                .SetColumnWidth(1, 1, 14) 'codice
                .SetColumnWidth(2, 2, 70) 'voce
                .SetColumnWidth(3, 3, 70) 'tipo
                .SetColumnWidth(4, 4, 60) 'importo odl
                .SetColumnWidth(5, 5, 60) 'filiale
                .SetColumnWidth(6, 6, 22) 'importo prenotazione
                .SetColumnWidth(7, 7, 52) 'fornitore
                .SetColumnWidth(8, 9, 52) 'num repertorio
                .SetColumnWidth(9, 9, 25) 'adp
                .SetColumnWidth(10, 10, 12) 'data adp
                .SetColumnWidth(11, 11, 14) 'importo adp
                .SetColumnWidth(12, 12, 14) 'ritlegge adp
                .SetColumnWidth(13, 13, 10) 'mae
                .SetColumnWidth(14, 14, 10) 'data mae
                .SetColumnWidth(15, 15, 14) 'importo mae
                K = 1
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont3, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, lblTitolo.Text)
                K = K + 1
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "CODICE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "VOCE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "TIPO PAGAMENTO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "DESCRIZIONE PAGAMENTO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "FILIALE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "IMPORTO PRENOTATO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "FORNITORE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, "REPERTORIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, "ADP", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, "DATA ADP", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, "IMPORTO ADP", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, "RIT.LEGGE ADP", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, "CUP", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, "CIG", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, "PROTOCOLLO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, "DATA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, "IMPORTO", 0)
                K = K + 1
                Dim id_pre As Integer
                Dim id_pag As Integer
                Dim id_mae As Integer
                For Each row In datatable.Rows
                    If row.Item("CODICE") = "TOTALE VOCI" Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, row.Item("VOCE"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, " ", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, " ", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, " ", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, row.Item("FORNITORE"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, row.Item("NUM_REPERTORIO"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, row.Item("ADP"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, row.Item("DATA_ADP"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, row.Item("IMPORTO_ADP"), 4)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, row.Item("RIT_LEGGE"), 4)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, row.Item("CUP"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, row.Item("CIG"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, row.Item("NUM_MAE"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, row.Item("DATA_MAE"), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder + CM.ExcelFile.CellAlignment.xlsBottomBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, row.Item("IMPORTO_MAE"), 4)
                    Else
                        If i = 0 Then
                            id_pre = row.Item("ID_PRE")
                            id_mae = row.Item("ID_MAE")
                            id_pag = row.Item("ID_PAG")
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, row.Item("VOCE"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, row.Item("DESCRIZIONE_TIPO"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, row.Item("DESCRIZIONE_PAG"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, row.Item("FILIALE"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, row.Item("FORNITORE"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, row.Item("NUM_REPERTORIO"), 0)
                            If row.Item("ADP") = "" And row.Item("IMPORTO_ADP") = "" Then
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, row.Item("ADP"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, row.Item("DATA_ADP"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, row.Item("IMPORTO_ADP"), 4)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, row.Item("RIT_LEGGE"), 4)
                            Else
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, row.Item("ADP"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, row.Item("DATA_ADP"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, row.Item("IMPORTO_ADP"), 4)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, row.Item("RIT_LEGGE"), 4)
                            End If

                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, row.Item("CUP"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, row.Item("CIG"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, row.Item("NUM_MAE"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, row.Item("DATA_MAE"), 0)
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, row.Item("IMPORTO_MAE"), 4)
                        Else
                            If row.Item("ID_PRE") = id_pre Then
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, " ", 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, " ", 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, " ", 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, " ", 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, " ", 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, " ", 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, " ", 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, " ", 0)
                                If row.Item("ADP") = "" And row.Item("IMPORTO_ADP") = "" Then
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, " ", 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, " ", 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, " ", 4)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, " ", 4)
                                Else
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, " ", 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, " ", 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, " ", 4)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, " ", 4)
                                End If
                                If CInt(row.Item("ID_MAE")) = id_mae Then
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, " ", 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, " ", 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, " ", 4)
                                Else
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, row.Item("CUP"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, row.Item("CIG"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, row.Item("NUM_MAE"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, row.Item("DATA_MAE"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, row.Item("IMPORTO_MAE"), 4)
                                End If

                            Else
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, row.Item("VOCE"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, row.Item("DESCRIZIONE_TIPO"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, row.Item("DESCRIZIONE_PAG"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, row.Item("FILIALE"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_PRENOTATO"), 4)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, row.Item("FORNITORE"), 0)
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, row.Item("NUM_REPERTORIO"), 0)
                                If row.Item("ADP") = "" And row.Item("IMPORTO_ADP") = "" Then
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, row.Item("ADP"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, row.Item("DATA_ADP"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, row.Item("IMPORTO_ADP"), 4)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, row.Item("RIT_LEGGE"), 4)
                                Else
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, row.Item("ADP"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, row.Item("DATA_ADP"), 0)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, row.Item("IMPORTO_ADP"), 4)
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, row.Item("RIT_LEGGE"), 4)
                                End If
                                If CInt(row.Item("ID_MAE")) = id_mae Then
                                    If row.Item("ID_PAG") = id_pag Then
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, " ", 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, " ", 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, " ", 4)
                                    Else
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, " ", 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, " ", 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, " ", 4)
                                    End If

                                Else
                                    If row.Item("ID_PAG") = id_pag Then
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, " ", 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, " ", 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, " ", 4)
                                    Else
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, row.Item("CUP"), 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, row.Item("CIG"), 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, row.Item("NUM_MAE"), 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, row.Item("DATA_MAE"), 0)
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, row.Item("IMPORTO_MAE"), 4)
                                    End If

                                End If

                            End If
                            id_pre = row.Item("ID_PRE")
                            id_mae = CInt(row.Item("ID_MAE"))
                            id_pag = CInt(row.Item("ID_PAG"))
                        End If
                    End If
                    i = i + 1
                    K = K + 1
                Next
                .CloseFile()
            End With
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String
            zipfic = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".zip")
            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            Dim strFile As String
            strFile = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)
            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            File.Delete(strFile)
            Response.Redirect("..\..\..\FileTemp\" & sNomeFile & ".zip")
        Catch ex As Exception
            Response.Write(ex.Message)
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
    End Sub

    Private Sub Datagrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        Dim INDICE As Integer
        INDICE = TrovaIndiceColonna(DataGrid1, "ADP")
        If e.Item.ItemType = ListItemType.Header Then
            e.Item.Cells(INDICE).ColumnSpan = 3
            e.Item.Cells(INDICE + 1).Visible = False
            e.Item.Cells(INDICE + 2).Visible = False
            'e.Item.Cells(INDICE + 3).Visible = False
            e.Item.Cells(INDICE).Text = "<table style='color: #FFFFFF; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: x-small;' width='100%'><tr><td colspan='4' align='center'>ADP</td></tr><tr><td style='width:25%' align='center'>NUM</td><td style='width:25%' align='center'>DATA</td><td style='width:25%' align='center'>IMPORTO</td></tr></table>"
        End If
    End Sub

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

  

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            If Request.QueryString("CONS") = 1 Then
                stampaTabellaCertificato()
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

    
    Protected Sub EsportaExcelAutomaticoDaDataGrid(ByVal datagrid As DataGrid, Optional ByVal nomeFile As String = "", Optional ByVal FattoreLarghezzaColonne As Decimal = 4.75, Optional ByVal EliminazioneLink As Boolean = True)
        'CONTO IL NUMERO DELLE COLONNE DEL DATAGRID
        Dim NumeroColonneDatagrid As Integer = datagrid.Columns.Count
        'CONTO IL NUMERO DELLE COLONNE VISIBILI DEL DATAGRID
        Dim NumeroColonneVisibiliDatagrid As Integer = 0
        For indiceColonna As Integer = 0 To NumeroColonneDatagrid - 1 Step 1
            If datagrid.Columns.Item(indiceColonna).Visible = True Then
                NumeroColonneVisibiliDatagrid = NumeroColonneVisibiliDatagrid + 1
            End If
        Next
        'INIZIALIZZAZIONE RIGHE, COLONNE E FILENAME
        Dim FileExcel As New CM.ExcelFile
        Dim indiceRighe As Long = 0
        Dim IndiceColonne As Long = 1
        Dim FileName As String = nomeFile & Format(Now, "yyyyMMddHHmmss")
        Dim LarghezzaMinimaColonna As Integer = 30
        Dim allineamentoCella As String = "Center"
        Dim LarghezzaDataGrid As Integer = datagrid.Width.Value
        Dim TipoLarghezzaDataGrid As UnitType = datagrid.Width.Type
        Dim LarghezzaColonnaHeader As Decimal = 0
        Dim LarghezzaColonnaItem As Decimal = 0
        'SETTO A ZERO LA VARIABILE DELLE RIGHE
        indiceRighe = 0
        With FileExcel
            'CREO IL FILE 
            .CreateFile(Server.MapPath("..\..\..\FileTemp\" & FileName & ".xls"))
            .PrintGridLines = False
            .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
            .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
            .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
            .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
            .SetDefaultRowHeight(14)
            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
            'GESTIONE LARGHEZZA DELLE COLONNE TRAMITE FATTORE DATO IN INPUT OPZIONALE
            Dim indiceVisibile As Integer = 1
            For j = 0 To NumeroColonneDatagrid - 1 Step 1
                'GESTIONE LARGHEZZA DELLE COLONNE TRAMITE FATTORE DATO IN INPUT OPZIONALE
                If datagrid.Columns.Item(j).Visible = True Then
                    If datagrid.Columns.Item(j).HeaderStyle.Width.Type = UnitType.Pixel Then
                        If TipoLarghezzaDataGrid = UnitType.Pixel Then
                            LarghezzaColonnaHeader = datagrid.Columns.Item(j).HeaderStyle.Width.Value * 100 / LarghezzaDataGrid
                        Else
                            LarghezzaColonnaHeader = 0
                        End If
                    Else
                        If TipoLarghezzaDataGrid = UnitType.Percentage Then
                            LarghezzaColonnaHeader = datagrid.Columns.Item(j).HeaderStyle.Width.Value
                        Else
                            LarghezzaColonnaHeader = 0
                        End If
                    End If

                    If datagrid.Columns.Item(j).ItemStyle.Width.Type = UnitType.Pixel Then
                        If TipoLarghezzaDataGrid = UnitType.Pixel Then
                            LarghezzaColonnaHeader = datagrid.Columns.Item(j).ItemStyle.Width.Value * 100 / LarghezzaDataGrid
                        Else
                            LarghezzaColonnaHeader = 0
                        End If
                    Else
                        If TipoLarghezzaDataGrid = UnitType.Percentage Then
                            LarghezzaColonnaHeader = datagrid.Columns.Item(j).ItemStyle.Width.Value
                        Else
                            LarghezzaColonnaHeader = 0
                        End If
                    End If
                    LarghezzaMinimaColonna = FattoreLarghezzaColonne * Max(LarghezzaColonnaHeader, LarghezzaColonnaItem)
                    .SetColumnWidth(indiceVisibile, indiceVisibile, Max(LarghezzaMinimaColonna, 30))
                    'GESTIONE DELLE INTESTAZIONI
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, IndiceColonne, indiceVisibile, datagrid.Columns.Item(j).HeaderText, 0)
                    indiceVisibile = indiceVisibile + 1
                End If
            Next
            indiceRighe = indiceRighe + 1
            For Each Items As DataGridItem In datagrid.Items
                indiceRighe = indiceRighe + 1
                Dim Cella As Integer = 0
                For IndiceColonne = 0 To NumeroColonneDatagrid - 1
                    'RIEPILOGO ALLINEAMENTI
                    'CENTER 2,LEFT 1,RIGHT 3
                    'CONSIDERO DI FORMATO NUMERICO TUTTE LE CELLE CON ALLINEAMENTO A DESTRA
                    If datagrid.Columns.Item(IndiceColonne).Visible = True Then
                        allineamentoCella = datagrid.Columns.Item(IndiceColonne).ItemStyle.HorizontalAlign
                        Select Case EliminazioneLink
                            Case False
                                Select Case allineamentoCella
                                    Case 1
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), 0)
                                    Case 2
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), 0)
                                    Case 3
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", ""), 4)
                                    Case Else
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), 0)
                                End Select

                            Case True
                                Select Case allineamentoCella
                                    Case 1
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, eliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                    Case 2
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, eliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                    Case 3
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, eliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", "")), 4)
                                    Case Else
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, eliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                End Select
                            Case Else
                                Select Case allineamentoCella
                                    Case 1
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, eliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                    Case 2
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, eliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                    Case 3
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, eliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", "")), 4)
                                    Case Else
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, eliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                End Select
                        End Select
                        Cella = Cella + 1
                    End If
                Next

            Next
            'CHIUSURA FILE
            .CloseFile()
        End With
        'COSTRUZIONE ZIPFILE
        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        Dim zipfic As String
        zipfic = Server.MapPath("..\..\..\FileTemp\" & FileName & ".zip")
        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        strmZipOutputStream.SetLevel(6)
        Dim strFile As String
        strFile = Server.MapPath("..\..\..\FileTemp\" & FileName & ".xls")
        Dim strmFile As FileStream = File.OpenRead(strFile)
        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        strmFile.Read(abyBuffer, 0, abyBuffer.Length)
        Dim sFile As String = Path.GetFileName(strFile)
        Dim theEntry As ZipEntry = New ZipEntry(sFile)
        Dim fi As New FileInfo(strFile)
        theEntry.DateTime = fi.LastWriteTime
        theEntry.Size = strmFile.Length
        strmFile.Close()
        objCrc32.Reset()
        objCrc32.Update(abyBuffer)
        theEntry.Crc = objCrc32.Value
        strmZipOutputStream.PutNextEntry(theEntry)
        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        strmZipOutputStream.Finish()
        strmZipOutputStream.Close()
        'File.Delete(strFile)
        Response.Redirect("..\..\..\FileTemp\" & FileName & ".zip", False)






        'Try
        '    Dim myExcelFile As New CM.ExcelFile
        '    Dim i As Long
        '    Dim K As Long
        '    Dim sNomeFile As String
        '    sNomeFile = "DetSBol" & Format(Now, "yyyyMMddHHmmss")
        '    i = 0
        '    With myExcelFile
        '        .CreateFile(Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls"))
        '        .PrintGridLines = False
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
        '        .SetDefaultRowHeight(14)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
        '        .SetColumnWidth(1, 11, 38)
        '        K = 1
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, UCase(lbltitolo.Text), 0)
        '        K = 2
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "COD. EDIFICIO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "EDIFICIO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "SUP. SERVIZI GENERALI", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "COSTO SERVIZI GENERALI", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "COSTO AL MQ SERVIZI GENERALI", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "SUP. SERVIZI RISCALDAMENTO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "COSTO SERVIZI RISCALDAMENTO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, "COSTO AL MQ SERVIZI RISCALDAMENTO", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, "SUP. SERVIZI ASCENSORE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, "COSTO SERVIZI ASCENSORE", 0)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, "COSTO AL MQ SERVIZI ASCENSORE", 0)
        '        K = 3
        '        For Each Items As DataGridItem In DataGrid1.Items
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, eliminaLink(Replace(par.IfNull(Items.Cells(TrovaIndiceColonna(DataGrid1, "COD_EDIFICIO")).Text, 0), "&nbsp;", "")), 0)
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, Replace(par.IfNull(Items.Cells(TrovaIndiceColonna(DataGrid1, "NOME_EDIFICIO")).Text, 0), "&nbsp;", ""), 0)
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, Replace(par.IfNull(Items.Cells(TrovaIndiceColonna(DataGrid1, "SUPERFICIE_300")).Text, 0), "&nbsp;", ""), 4)
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, Replace(par.IfNull(Items.Cells(TrovaIndiceColonna(DataGrid1, "IMPORTO_303")).Text, 0), "&nbsp;", ""), 4)
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, Replace(par.IfNull(Items.Cells(TrovaIndiceColonna(DataGrid1, "COSTO_303")).Text, 0), "&nbsp;", ""), 4)
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, Replace(par.IfNull(Items.Cells(TrovaIndiceColonna(DataGrid1, "SUPERFICIE_302")).Text, 0), "&nbsp;", ""), 4)
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, Replace(par.IfNull(Items.Cells(TrovaIndiceColonna(DataGrid1, "IMPORTO_302")).Text, 0), "&nbsp;", ""), 4)
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, Replace(par.IfNull(Items.Cells(TrovaIndiceColonna(DataGrid1, "COSTO_302")).Text, 0), "&nbsp;", ""), 4)
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, Replace(par.IfNull(Items.Cells(TrovaIndiceColonna(DataGrid1, "SUPERFICIE_300")).Text, 0), "&nbsp;", ""), 4)
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, Replace(par.IfNull(Items.Cells(TrovaIndiceColonna(DataGrid1, "IMPORTO_300")).Text, 0), "&nbsp;", ""), 4)
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, Replace(par.IfNull(Items.Cells(TrovaIndiceColonna(DataGrid1, "COSTO_300")).Text, 0), "&nbsp;", ""), 4)
        '            i = i + 1
        '            K = K + 1
        '        Next
        '        .CloseFile()
        '    End With
        '    Dim objCrc32 As New Crc32()
        '    Dim strmZipOutputStream As ZipOutputStream
        '    Dim zipfic As String
        '    zipfic = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".zip")
        '    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        '    strmZipOutputStream.SetLevel(6)
        '    Dim strFile As String
        '    strFile = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls")
        '    Dim strmFile As FileStream = File.OpenRead(strFile)
        '    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '    strmFile.Read(abyBuffer, 0, abyBuffer.Length)
        '    Dim sFile As String = Path.GetFileName(strFile)
        '    Dim theEntry As ZipEntry = New ZipEntry(sFile)
        '    Dim fi As New FileInfo(strFile)
        '    theEntry.DateTime = fi.LastWriteTime
        '    theEntry.Size = strmFile.Length
        '    strmFile.Close()
        '    objCrc32.Reset()
        '    objCrc32.Update(abyBuffer)
        '    theEntry.Crc = objCrc32.Value
        '    strmZipOutputStream.PutNextEntry(theEntry)
        '    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        '    strmZipOutputStream.Finish()
        '    strmZipOutputStream.Close()
        '    File.Delete(strFile)
        '    Response.Redirect("..\..\..\FileTemp\" & sNomeFile & ".zip", False)
        'Catch ex As Exception
        'End Try
    End Sub

    'Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
    '    EsportaExcelAutomaticoDaDataGrid(DataGrid1, "0")
    'End Sub

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

        End Try
        Return TrovaIndiceColonna
    End Function

    
    Protected Sub DataGridRit_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridRit.ItemCreated
        Dim INDICE As Integer
        INDICE = TrovaIndiceColonna(DataGridRit, "ADP")
        If e.Item.ItemType = ListItemType.Header Then
            e.Item.Cells(INDICE).ColumnSpan = 3
            e.Item.Cells(INDICE + 1).Visible = False
            e.Item.Cells(INDICE + 2).Visible = False
            'e.Item.Cells(INDICE + 3).Visible = False
            e.Item.Cells(INDICE).Text = "<table style='color: #FFFFFF; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: x-small;' width='100%'><tr><td colspan='4' align='center'>ADP</td></tr><tr><td style='width:25%' align='center'>NUM</td><td style='width:25%' align='center'>DATA</td><td style='width:25%' align='center'>IMPORTO</td></tr></table>"
        End If
    End Sub

    Protected Sub DataGridRit_PreRender(sender As Object, e As System.EventArgs) Handles DataGridRit.PreRender
        'UNISCO LE CELLE DELLE COLONNE ADP DATA E MAE CHE HANNO LO STESSO ID_PAGAMENTO
        Dim count As Integer = 0
        Dim memIDpag As Long = 0
        Dim cella0 As Long
        Dim cellainizio As Integer = TrovaIndiceColonna(DataGridRit, "ADP")
        Dim cellafine As Integer = TrovaIndiceColonna(DataGridRit, "IMPORTO_ADP")
        For j As Integer = DataGridRit.Items.Count - 1 To 0 Step -1
            If j = DataGridRit.Items.Count - 1 Then
                Try
                    cella0 = CLng(DataGridRit.Items(j).Cells(0).Text)
                Catch ex As Exception
                    cella0 = -1
                End Try
                memIDpag = cella0
            Else
                Try
                    cella0 = CLng(DataGridRit.Items(j).Cells(0).Text)
                Catch ex As Exception
                    cella0 = -1
                End Try
                If cella0 = memIDpag And memIDpag <> -1 Then
                    If DataGridRit.Items(j + 1).Cells(cellainizio).RowSpan = 0 Then
                        For g As Integer = cellainizio To cellafine
                            DataGridRit.Items(j).Cells(g).RowSpan = DataGridRit.Items(j + 1).Cells(g).RowSpan + 2
                        Next
                    Else
                        For g As Integer = cellainizio To cellafine
                            DataGridRit.Items(j).Cells(g).RowSpan = DataGridRit.Items(j + 1).Cells(g).RowSpan + 1
                        Next
                    End If
                    For g As Integer = cellainizio To cellafine
                        DataGridRit.Items(j + 1).Cells(g).RowSpan = 0
                        DataGridRit.Items(j + 1).Cells(g).Visible = False
                    Next
                End If
                memIDpag = cella0
            End If
        Next
        'PER IL MAE
        cellainizio = TrovaIndiceColonna(DataGridRit, "MAE")
        cellafine = TrovaIndiceColonna(DataGridRit, "MAE")
        For j As Integer = DataGridRit.Items.Count - 1 To 0 Step -1
            If j = DataGridRit.Items.Count - 1 Then
                Try
                    cella0 = CLng(DataGridRit.Items(j).Cells(0).Text)
                Catch ex As Exception
                    cella0 = -1
                End Try
                memIDpag = cella0
            Else
                Try
                    cella0 = CLng(DataGridRit.Items(j).Cells(0).Text)
                Catch ex As Exception
                    cella0 = -1
                End Try
                If cella0 = memIDpag And memIDpag <> -1 Then
                    If DataGridRit.Items(j + 1).Cells(cellainizio).RowSpan = 0 Then
                        For g As Integer = cellainizio To cellafine
                            DataGridRit.Items(j).Cells(g).RowSpan = DataGridRit.Items(j + 1).Cells(g).RowSpan + 2
                        Next
                    Else
                        For g As Integer = cellainizio To cellafine
                            DataGridRit.Items(j).Cells(g).RowSpan = DataGridRit.Items(j + 1).Cells(g).RowSpan + 1
                        Next
                    End If
                    For g As Integer = cellainizio To cellafine
                        DataGridRit.Items(j + 1).Cells(g).RowSpan = 0
                        DataGridRit.Items(j + 1).Cells(g).Visible = False
                    Next
                End If
                memIDpag = cella0
            End If
        Next
    End Sub

    Protected Sub indietro_Click(sender As Object, e As System.EventArgs) Handles indietro.Click
        If URLdiProvenienza.Value <> "" Then
            Response.Redirect(URLdiProvenienza.Value)
        End If
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        If Request.QueryString("RITCERT") = 1 Then
            esportaRitCert()
        Else
            If Request.QueryString("RIT") = 1 Then
                esportaRit()
            Else
                If Request.QueryString("RITLIQ") = 1 Then
                    esportaRitLiq()
                Else
                    If Request.QueryString("CONS") = 1 Then
                        esportaConsuntivato()
                    Else
                        If Request.QueryString("CERT") = 1 Then
                            esportaCertificato()
                        Else
                            If Request.QueryString("LIQ") = 1 Then
                                esportaLiquidazioni()
                            Else
                                If Not IsNothing(Request.QueryString("IDV")) Then
                                    esportaPrenotazioni()
                                Else
                                    If Session.Item("BP_GENERALE") = 1 Then
                                        esportaPagamenti()
                                    Else
                                        If Session.Item("ID_STRUTTURA") = Request.QueryString("IDS") Then
                                            esportaPagamenti()
                                        Else
                                            Response.Write("<script>top.location.href=""../../../AccessoNegato.htm""</script>")
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If



        'If Request.QueryString("CONS") = 1 Then
        '    esportaCertificato()
        'Else
        '    If Request.QueryString("LIQ") = 1 Then
        '        esportaLiquidazioni()
        '    Else
        '        If Not IsNothing(Request.QueryString("IDV")) Then
        '            esportaPrenotazioni()
        '        Else
        '            esportaPagamenti()
        '        End If
        '    End If
        'End If
    End Sub

    Protected Sub btnStampaPDF_Click(sender As Object, e As System.EventArgs) Handles btnStampaPDF.Click
        GeneraPDF(DataGrid1)


        'If ConfermaPagamento.Value = "1" Then
        '    stampaPDF()
        'Else
        '    Dim controlloData As String = ""
        '    If Not IsNothing(Request.QueryString("AL")) Then
        '        controlloData = "&AL=" & Request.QueryString("AL")
        '    End If

        '    If Request.QueryString("RIT") = 1 Then
        '        StampaTabellaRit()
        '    Else
        '        If Request.QueryString("RITLIQ") = 1 Then
        '            StampaTabellaRitLiq()
        '        Else
        '            If Request.QueryString("CONS") = 1 Then
        '                Response.Write("<script>window.open('StampeRisultatiPagamentiPerStruttura.aspx?IDS=" & Request.QueryString("IDS") & "&EF=" & Request.QueryString("EF") & "&IDV=" & Request.QueryString("IDV") & "&CONS=1" & controlloData & "','S_CONS');</script>")
        '            Else
        '                If Request.QueryString("CERT") = 1 Then
        '                    stampaTabellaCertificato()
        '                Else
        '                    If Request.QueryString("LIQ") = 1 Then
        '                        Response.Write("<script>window.open('StampeRisultatiPagamentiPerStruttura.aspx?IDS=" & Request.QueryString("IDS") & "&EF=" & Request.QueryString("EF") & "&IDV=" & Request.QueryString("IDV") & "&LIQ=1" & controlloData & "','S_LIQ');</script>")
        '                    Else
        '                        'If Not IsNothing(Request.QueryString("IDV")) Then
        '                        '    Response.Write("<script>window.open('StampeRisultatiPagamentiPerStruttura.aspx?IDS=" & Request.QueryString("IDS") & "&EF=" & Request.QueryString("EF") & "&IDV=" & Request.QueryString("IDV") & "" & controlloData & "','S_PRE');</script>")
        '                        'Else
        '                        '    Response.Write("<script>window.open('StampeRisultatiPagamentiPerStruttura.aspx?IDS=" & Request.QueryString("IDS") & "&EF=" & Request.QueryString("EF") & "" & controlloData & "','S_PAG');</script>")
        '                        'End If
        '                        If Not IsNothing(Request.QueryString("IDV")) Then
        '                            StampaTabellaPrenotazioni()
        '                        Else
        '                            If Session.Item("BP_GENERALE") = 1 Then
        '                                StampaTabellaPagamenti()
        '                            Else
        '                                If Session.Item("ID_STRUTTURA") = Request.QueryString("IDS") Then
        '                                    StampaTabellaPagamenti()
        '                                Else
        '                                    Response.Write("<script>top.location.href=""../../../AccessoNegato.htm""</script>")
        '                                End If
        '                            End If
        '                        End If
        '                    End If
        '                End If
        '            End If
        '        End If
        '    End If
        'End If
    End Sub

    Protected Sub excelRit_Click(sender As Object, e As System.EventArgs) Handles excelRit.Click
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridRit, "ExportRitLegge")
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../../../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If
    End Sub

    Protected Sub stampaRit_Click(sender As Object, e As System.EventArgs) Handles stampaRit.Click
        GeneraPDF(DataGridRit)
    End Sub
End Class