Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports System.Math
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiContabilitaDett
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public TestoPagina As String = ""
    Public TestoPagina2 As String = ""
    Public qsEsercizioFinanziario As String
    Dim dt1 As New Data.DataTable
    Dim dt2 As New Data.DataTable


    Dim totAssestamento As Decimal = 0
    Dim totResiduo As Decimal = 0
    Dim totPrenotato As Decimal = 0
    Dim totconsuntivato As Decimal = 0
    Dim totRitenuteConsuntivate As Decimal = 0
    Dim totRitenuteCertificate As Decimal = 0
    Dim totRitenuteLiquidate As Decimal = 0
    Dim totCertificato As Decimal = 0
    Dim totLiquidato As Decimal = 0
    Dim ANNO As String = ""



    Dim contatore As Integer = 0


    Public Property TestoPaginaPDF() As String
        Get
            If Not (ViewState("par_TestoPaginaPDF") Is Nothing) Then
                Return CStr(ViewState("par_TestoPaginaPDF"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_TestoPaginaPDF") = value
        End Set

    End Property

    Public Property DataEsercizio() As String
        Get
            If Not (ViewState("par_DataEsercizio") Is Nothing) Then
                Return CStr(ViewState("par_DataEsercizio"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DataEsercizio") = value
        End Set

    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("BP_GENERALE") <> 1 Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        ''CONTROLLO BP_GENERALE
        'If Session.Item("BP_GENERALE") <> 1 Then
        '    Response.Write("<script>alert('Operatore non abilitato per questa funzione!');</script>")
        '    Exit Sub
        'End If
        ''#########################
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)

        If IsPostBack = False Then
            Response.Flush()
            StampaTabella()
        End If
    End Sub

    Protected Sub StampaTabella()
        Dim sDataEsercizio As String = ""
        Dim FlagConnessione As Boolean
        Try
            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Dim dataAl As Integer = CInt(Request.QueryString("AL"))
            Dim dataDal As Integer = CInt(Request.QueryString("DAL"))
            Dim dataoggi As Integer = CInt(Format(Now.Date, "yyyyMMdd"))
            dataAl = Min(dataoggi, dataAl)

            '************ACQUISIZIONE INFORMAZIONI SUL PIANO FINANZIARIO*****************
            qsEsercizioFinanziario = Strings.Trim(Request.QueryString("EF_R"))
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                & "WHERE ID=(select ID_ESERCIZIO_FINANZIARIO from SISCOM_MI.PF_MAIN where ID=" & qsEsercizioFinanziario & ") "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                sDataEsercizio = " DAL " & par.FormattaData(par.IfNull(myReader1("INIZIO"), ""))
                ANNO = Left(par.IfNull(myReader1("INIZIO"), ""), 4)
                sDataEsercizio = sDataEsercizio & " AL " & Right(dataAl, 2) & "/" & Mid(dataAl, 5, 2) & "/" & Left(dataAl, 4)
            End If
            myReader1.Close()
            '****************************************************************************

            '************CREAZIONE INTESTAZIONE TABELLA PER REPORT, EXCEL E PDF*******
            TabellaCompleta.Text = "<p style='font-family: ARIAL; font-size: 14pt; font-weight: bold; text-align: center;'>SITUAZIONE CONTABILE: " & sDataEsercizio & "</p></br>"
            TestoPaginaPDF = "<p style='font-family: ARIAL; font-size: 14pt; font-weight: bold; text-align: center;'>SITUAZIONE CONTABILE - " & sDataEsercizio & "</p></br>"
            TabellaCompleta.Text = TabellaCompleta.Text & "<table width=""100%"" cellspacing=""0"" cellpadding=""4"" rules=""all"" border=""1"" id=""DataGrid1"">" _
                                      & "<tr align=""center"" style=""color: White; background-color: #507CD1; font-family: Arial; font-size: 9pt; font-weight: bold;"">" _
                                      & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">COD.</td>" _
                                      & "<td td align=""center"" style=""width:17%;font-weight: bold; font-style: normal; text-decoration: none; "">VOCE</td>" _
                                      & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">FILIALE</td>" _
                                      & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">LOTTO</td>" _
                                      & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">SERVIZIO</td>" _
                                      & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">VOCE SERVIZIO</td>" _
                                      & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE PRENOTATO</td>" _
                                      & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE CONSUNTIVATO</td>" _
                                      & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE RIT. LEGGE CONSUNTIVATE</td>" _
                                      & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE CERTIFICATO</td>" _
                                      & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE PAGATO</td>" _
                                      & "</tr>"
            TestoPaginaPDF = TestoPaginaPDF & "<table width=""100%"" cellspacing=""0"" cellpadding=""4"" rules=""all"" border=""1"" id=""DataGrid1"">" _
                                      & "<tr align=""center"" style=""color: White; background-color: #507CD1; font-family: Arial; font-size: 9pt; font-weight: bold;"">" _
                                      & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">COD.</td>" _
                                      & "<td td align=""center"" style=""width:17%;font-weight: bold; font-style: normal; text-decoration: none; "">VOCE</td>" _
                                      & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">FILIALE</td>" _
                                      & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">LOTTO</td>" _
                                      & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">SERVIZIO</td>" _
                                      & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">VOCE SERVIZIO</td>" _
                                      & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE PRENOTATO</td>" _
                                      & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE CONSUNTIVATO</td>" _
                                      & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE RIT. LEGGE CONSUNTIVATE</td>" _
                                      & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE CERTIFICATO</td>" _
                                      & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE PAGATO</td>" _
                                      & "</tr>"

            'CREAZIONE INTESTAZIONE DATATABLE
            dt2.Clear()
            dt2.Columns.Clear()
            dt2.Columns.Add("COD.")
            dt2.Columns.Add("VOCE")
            dt2.Columns.Add("FILIALE")
            dt2.Columns.Add("LOTTO")
            dt2.Columns.Add("SERVIZIO")
            dt2.Columns.Add("VOCE_SERVIZIO")
            dt2.Columns.Add("TOTALE_PRENOTATO")
            dt2.Columns.Add("TOTALE_CONSUNTIVATO")
            dt2.Columns.Add("TOTALE_RIT")
            dt2.Columns.Add("TOTALE CERTIFICATO")
            dt2.Columns.Add("TOTALE_PAGATO")
            dt1.Clear()
            dt1.Columns.Clear()
            dt1.Columns.Add("COD.")
            dt1.Columns.Add("VOCE")
            dt1.Columns.Add("FILIALE")
            dt1.Columns.Add("LOTTO")
            dt1.Columns.Add("SERVIZIO")
            dt1.Columns.Add("VOCE_SERVIZIO")
            dt1.Columns.Add("TOTALE_PRENOTATO")
            dt1.Columns.Add("TOTALE_CONSUNTIVATO")
            dt1.Columns.Add("TOTALE_RIT")
            dt1.Columns.Add("TOTALE CERTIFICATO")
            dt1.Columns.Add("TOTALE_PAGATO")

            Dim RIGA As Data.DataRow
            RIGA = dt2.NewRow
            RIGA.Item("COD.") = "COD."
            RIGA.Item("VOCE") = "VOCE"
            RIGA.Item("FILIALE") = "FILIALE"
            RIGA.Item("LOTTO") = "LOTTO"
            RIGA.Item("SERVIZIO") = "SERVIZIO"
            RIGA.Item("VOCE_SERVIZIO") = "VOCE SERVIZIO"
            RIGA.Item("TOTALE_PRENOTATO") = "TOTALE PRENOTATO"
            RIGA.Item("TOTALE_CONSUNTIVATO") = "TOTALE CONSUNTIVATO"
            RIGA.Item("TOTALE_RIT") = "TOTALE RIT. LEGGE CONSUNTIVATE"
            RIGA.Item("TOTALE CERTIFICATO") = "TOTALE CERTIFICATO"
            RIGA.Item("TOTALE_PAGATO") = "TOTALE PAGATO"
            dt2.Rows.Add(RIGA)
            '****************************************************************************

            DataEsercizio = sDataEsercizio
            '######### MODIFICA MARCO ###########
            Dim livelloSottovoci As String = ""
            If Request.QueryString("VOCI") = "False" Then
                'NON ELENCO LE SOTTOVOCI DEL PIANO FINANZIARIO
                livelloSottovoci = "AND LEVEL<3 "
            End If

            'ELENCO DI TUTTE LE VOCI CHE SARANNO STAMPATE
            par.cmd.CommandText = "SELECT id,id_piano_finanziario, codice, descrizione, id_voce_madre, indice, " _
                    & "id_capitolo, fl_cc, id_old, id_tipo_utilizzo, level " _
                    & "FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE PF_VOCI.ID_PIANO_FINANZIARIO = '" & qsEsercizioFinanziario & "'" & livelloSottovoci _
                    & "CONNECT BY PRIOR PF_VOCI.ID = PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH PF_VOCI.ID IN (SELECT ID " _
                    & "FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE ID_VOCE_MADRE IS NULL) " _
                    & "ORDER BY CODICE ASC"

            '####################################

            Dim LettoreElencoVoci As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            Dim budgetTOTALEcapitolo As Decimal = 0
            Dim assestamentoTOTALEcapitolo As Decimal = 0
            Dim variazioniTOTALEcapitolo As Decimal = 0
            Dim prenotatoTOTALEcapitolo As Decimal = 0
            Dim certificatoTOTALEcapitolo As Decimal = 0
            Dim consuntivatoTOTALEcapitolo As Decimal = 0
            Dim liquidatoTOTALEcapitolo As Decimal = 0
            Dim residuoTOTALEcapitolo As Decimal = 0
            Dim assestamentoTotaleCap As Decimal = 0
            Dim assestamentoTotaleSing As Decimal = 0
            Dim ritenuteLeggeConsuntivateTOTALEcapitolo As Decimal = 0
            Dim ritenuteLeggeCertificateTOTALEcapitolo As Decimal = 0
            Dim ritenuteLeggeLiquidateTOTALEcapitolo As Decimal = 0
            Dim codCap As Integer = 1


            While LettoreElencoVoci.Read


                '####################### VALORE LORDO, ASSESTAMENTO, VARIAZIONI ####################################
                'ELENCO DELLE VOCI FOGLIA CHE VANNO SOMMATE PER OGNI SINGOLA VOCE 
                par.cmd.CommandText = "SELECT " _
                    & "TRIM(TO_CHAR(SUM(NVL(VALORE_LORDO,0)),'999999990D9999')) AS VALORE_LORDO,TRIM(TO_CHAR(SUM(NVL(ASSESTAMENTO_VALORE_LORDO,0)),'999999990D9999')) AS ASSESTAMENTO_VALORE_LORDO," _
                    & "TRIM(TO_CHAR(SUM(NVL(VARIAZIONI, 0)),'999999990D9999')) AS VARIAZIONI " _
                    & "FROM SISCOM_MI.PF_VOCI_STRUTTURA " _
                    & "WHERE ID_VOCE IN (" _
                    & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "') "
                Dim ElencoCompletoSottovociDaSommare As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                Dim budgetTOTALEsingoleVoci As Decimal = 0
                Dim assestamentoTOTALEsingoleVoci As Decimal = 0
                Dim variazioniTOTALEsingoleVoci As Decimal = 0
                Dim ritenuteLeggeCertificateTOTALESingoleVoci As Decimal = 0
                Dim ritenuteLeggeConsuntivateTOTALESingoleVoci As Decimal = 0

                If ElencoCompletoSottovociDaSommare.Read Then
                    budgetTOTALEsingoleVoci = par.IfNull(ElencoCompletoSottovociDaSommare("VALORE_LORDO"), 0)
                    assestamentoTOTALEsingoleVoci = par.IfNull(ElencoCompletoSottovociDaSommare("ASSESTAMENTO_VALORE_LORDO"), 0)
                    variazioniTOTALEsingoleVoci = par.IfNull(ElencoCompletoSottovociDaSommare("VARIAZIONI"), 0)

                End If
                ElencoCompletoSottovociDaSommare.Close()


                ''############ MODIFICA VARIAZIONI POSTERIORI #############
                'Dim VARIAZIONI_NEGATIVE As Decimal = 0
                'Dim VARIAZIONI_POSITIVE As Decimal = 0

                'par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO,0)),'999G999G990D9999') FROM SISCOM_MI.EVENTI_VARIAZIONI " _
                '    & "WHERE ID_VOCE_DA IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 " _
                '    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "') " _
                '    & "AND SUBSTR(DATA_ORA,1,8)>'" & dataAl & "'"

                'Dim LettoreVariazioniNegative As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                'If LettoreVariazioniNegative.Read Then
                '    VARIAZIONI_NEGATIVE = par.IfNull(LettoreVariazioniNegative(0), 0)
                'End If
                'LettoreVariazioniNegative.Close()
                'variazioniTOTALEsingoleVoci = variazioniTOTALEsingoleVoci + CDec(VARIAZIONI_NEGATIVE)
                ''°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                'par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO,0)),'999G999G990D9999') FROM SISCOM_MI.EVENTI_VARIAZIONI " _
                '    & "WHERE ID_VOCE_A IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 " _
                '    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "') " _
                '    & "AND SUBSTR(DATA_ORA,1,8)>'" & dataAl & "'"

                'Dim LettoreVariazioniPositive As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                'If LettoreVariazioniPositive.Read Then
                '    VARIAZIONI_POSITIVE = par.IfNull(LettoreVariazioniPositive(0), 0)
                'End If
                'LettoreVariazioniPositive.Close()
                'variazioniTOTALEsingoleVoci = variazioniTOTALEsingoleVoci - CDec(VARIAZIONI_POSITIVE)
                ''°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                ''NEL RIEPILOGO GENERALE NON è NECESSARIO TENERE IN CONTO LE VARIAZIONI TRA STRUTTURE POICHè NEI TRASFERIMENTI INTERESSATI SARANNO UGUALI E OPPPOSTE


                ''###################################################################################################
                ''**************************************************************************************************
                ''####################### IMPORTO APPROVATO ASSESTAMENTO #########################
                ''ELENCO DELL'IMPORTO EVENTUALMENTE ASSESTATO DOPO LA DATA INSERITA
                ''QUESTO IMPORTO VA DETRATTO DAL TOTALE ASSESTAMENTO
                'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO_APPROVATO,0)),'999999990D9999')) AS ASSESTAMENTO_IMPORTO_APPROVATO " _
                '    & "FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI,SISCOM_MI.PF_ASSESTAMENTO " _
                '    & "WHERE ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "') " _
                '    & "AND PF_ASSESTAMENTO.ID=PF_ASSESTAMENTO_VOCI.ID_ASSESTAMENTO " _
                '    & "AND ID_STATO>=5 " _
                '    & "AND DATA_APP_COMUNE>'" & dataAl & "'"
                'Dim AssestamentoImportoApprovato As Decimal = 0
                'Dim LettoreAssestamentoImportoApprovato As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                'If LettoreAssestamentoImportoApprovato.Read Then
                '    AssestamentoImportoApprovato = par.IfNull(LettoreAssestamentoImportoApprovato("ASSESTAMENTO_IMPORTO_APPROVATO"), 0)
                'End If
                'LettoreAssestamentoImportoApprovato.Close()
                'assestamentoTOTALEsingoleVoci = assestamentoTOTALEsingoleVoci - AssestamentoImportoApprovato
                ''###################################################################################################
                ''**************************************************************************************************


                par.cmd.CommandText = "SELECT DISTINCT ID_LOTTO,ID_STRUTTURA,ID_VOCE_PF_IMPORTO," _
                    & "LOTTI.DESCRIZIONE AS LOTTO,TAB_sERVIZI.DESCRIZIONE AS SERVIZIO,PF_VOCI_IMPORTO.DESCRIZIONE AS VOCE_sERVIZIO," _
                    & "TAB_FILIALI.NOME AS FILIALE FROM SISCOM_MI.PRENOTAZIONI," _
                    & "SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.TAB_FILIALI,SISCOM_MI.LOTTI,SISCOM_MI.TAB_sERVIZI " _
                    & "WHERE PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID(+) " _
                    & "AND LOTTI.ID(+)=PF_VOCI_IMPORTO.ID_LOTTO " _
                    & "AND TAB_FILIALI.ID(+)=PRENOTAZIONI.ID_STRUTTURA " _
                    & "AND TAB_sERVIZI.ID(+)=PF_VOCI_IMPORTO.ID_SERVIZIO " _
                    & "AND ANNO='" & ANNO & "' " _
                    & "AND ID_VOCE_PF IN (" _
                    & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "') "
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim condizioneLotto As String = ""
                Dim condizioneStruttura As String = ""
                Dim condizioneServizio As String = ""
                Dim condizioneGenerale As String = ""
                Dim filiale As String = "&nbsp;"
                Dim lotto As String = "&nbsp;"
                Dim servizio As String = "&nbsp;"
                Dim VOCEservizio As String = "&nbsp;"
                While lettore.Read

                    If par.IfNull(lettore("ID_LOTTO"), -1) <> -1 Then
                        condizioneLotto = " AND ID_LOTTO=" & par.IfNull(lettore("ID_LOTTO"), "")
                    Else
                        condizioneLotto = " AND ID_LOTTO IS NULL"
                    End If
                    If par.IfNull(lettore("ID_STRUTTURA"), -1) <> -1 Then
                        condizioneStruttura = " AND ID_STRUTTURA=" & par.IfNull(lettore("ID_STRUTTURA"), "")
                    Else
                        condizioneStruttura = " AND ID_STRUTTURA IS NULL "
                    End If
                    If par.IfNull(lettore("ID_VOCE_PF_IMPORTO"), -1) <> -1 Then
                        condizioneServizio = " AND ID_VOCE_PF_IMPORTO=" & par.IfNull(lettore("ID_VOCE_PF_IMPORTO"), "")
                    Else
                        condizioneServizio = " AND ID_VOCE_PF_IMPORTO IS NULL "
                    End If

                    filiale = par.IfNull(lettore("FILIALE"), "&nbsp;")
                    servizio = par.IfNull(lettore("SERVIZIO"), "&nbsp;")
                    VOCEservizio = par.IfNull(lettore("VOCE_SERVIZIO"), "&nbsp;")
                    lotto = par.IfNull(lettore("LOTTO"), "&nbsp;")
                    condizioneGenerale = condizioneLotto & condizioneServizio & condizioneStruttura & " AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID(+) "


                    '####################### PRENOTATO #########################

                    Dim prenotatoTOTALEsingoleVoci As Decimal = 0
                    Dim TOTALEPAGATOSINGOLEVOCI As Decimal = 0

                    'ELENCO DEGLI IMPORTI DELLE SOTTOVOCI CHE VANNO SOMMATE PER OGNI SINGOLA VOCE
                    par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO_PRENOTATO,0)),'999999990D9999')) AS PRENOTATO FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO " _
                        & "WHERE (" _
                        & "(ID_STATO = 0 AND DATA_PRENOTAZIONE BETWEEN '" & dataDal & "' AND '" & dataAl & "') OR " _
                        & "(ID_STATO = 1 AND DATA_CONSUNTIVAZIONE >'" & dataAl & "') OR " _
                        & "(ID_STATO = 2 AND DATA_CERTIFICAZIONE >'" & dataAl & "' AND DATA_CONSUNTIVAZIONE > '" & dataAl & "') OR " _
                        & "(ID_STATO =-3 AND DATA_ANNULLO>'" & dataAl & "' AND (DATA_CONSUNTIVAZIONE IS NULL OR DATA_CONSUNTIVAZIONE>'" & dataAl & "') AND (DATA_CERTIFICAZIONE IS NULL OR DATA_CERTIFICAZIONE>'" & dataAl & "'))" _
                        & ") " _
                        & "AND ANNO='" & ANNO & "' " _
                        & condizioneGenerale _
                        & "AND ID_VOCE_PF IN (" _
                        & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                        & "WHERE CONNECT_BY_ISLEAF = 1 " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                        & "START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "') "


                    Dim ElencoImportiPrenotatiSottovociDaSommare As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If ElencoImportiPrenotatiSottovociDaSommare.Read Then
                        prenotatoTOTALEsingoleVoci = par.IfNull(ElencoImportiPrenotatiSottovociDaSommare("PRENOTATO"), 0)

                    End If
                    ElencoImportiPrenotatiSottovociDaSommare.Close()


                    '###################################################################################################
                    '**************************************************************************************************
                    '####################### CONSUNTIVATO #########################

                    Dim consuntivatoTOTALEsingoleVoci As Decimal = 0

                    'ELENCO DEGLI IMPORTI DELLE SOTTOVOCI CHE VANNO SOMMATE PER OGNI SINGOLA VOCE
                    par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO_APPROVATO,0)-NVL(RIT_LEGGE_IVATA,0)),'999999990D9999')) AS APPROVATO FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO  " _
                        & "WHERE ((ID_STATO=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & dataDal & "' AND '" & dataAl & "') " _
                        & "OR (ID_STATO=2  AND DATA_CONSUNTIVAZIONE BETWEEN '" & dataDal & "' AND '" & dataAl & "' AND DATA_CERTIFICAZIONE >'" & dataAl & "') OR" _
                        & "(ID_STATO=-3 AND DATA_ANNULLO>'" & dataAl & "' AND DATA_CONSUNTIVAZIONE BETWEEN '" & dataDal & "' AND '" & dataAl & "' AND (DATA_CERTIFICAZIONE IS NULL OR DATA_CERTIFICAZIONE>'" & dataAl & "')))" _
                        & "AND ANNO='" & ANNO & "' " _
                        & condizioneGenerale _
                        & "AND ID_VOCE_PF IN (" _
                        & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                        & "WHERE CONNECT_BY_ISLEAF = 1 " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                        & "START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "') "

                    Dim ElencoImportiAPPROVATISottovociDaSommare As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If ElencoImportiAPPROVATISottovociDaSommare.Read Then
                        consuntivatoTOTALEsingoleVoci = par.IfNull(ElencoImportiAPPROVATISottovociDaSommare("APPROVATO"), 0)

                    End If
                    ElencoImportiAPPROVATISottovociDaSommare.Close()

                    '###################################################################################################
                    '**************************************************************************************************
                    '####################### APPROVATO (NO RITENUTA DI LEGGE) #########################

                    Dim approvatoTOTALEsingoleVoci As Decimal = 0

                    'ELENCO DEGLI IMPORTI DELLE SOTTOVOCI CHE VANNO SOMMATE PER OGNI SINGOLA VOCE
                    par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO_APPROVATO,0)-NVL(RIT_LEGGE_IVATA,0)),'999999990D9999')) AS APPROVATO FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO  " _
                        & "WHERE ((id_stato=2 and data_certificazione BETWEEN '" & dataDal & "' AND '" & dataAl & "') " _
                        & " OR (ID_STATO=-3 AND DATA_ANNULLO>'" & dataAl & "' AND DATA_CONSUNTIVAZIONE BETWEEN '" & dataDal & "' AND '" & dataAl & "' AND DATA_CERTIFICAZIONE BETWEEN '" & dataDal & "' AND '" & dataAl & "'))" _
                        & "AND ANNO='" & ANNO & "' " _
                        & condizioneGenerale _
                        & "AND ID_VOCE_PF IN (" _
                        & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                        & "WHERE CONNECT_BY_ISLEAF = 1 " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                        & "START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "') "

                    Dim ElencoImportiAPPROVATISottovociDaSommare2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If ElencoImportiAPPROVATISottovociDaSommare2.Read Then
                        approvatoTOTALEsingoleVoci = par.IfNull(ElencoImportiAPPROVATISottovociDaSommare2("APPROVATO"), 0)

                    End If
                    ElencoImportiAPPROVATISottovociDaSommare2.Close()

                    '###################################################################################################
                    '**************************************************************************************************
                    '####################### LIQUIDATO(NO RITENUTA DI LEGGE) #########################

                    Dim TOTALELIQUIDATOSINGOLEVOCI As Decimal = 0

                    par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO_LIQUIDATO,0)),'999999990D9999')) AS IMPORTO FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO  " _
                        & "WHERE id_pagamento in (select id_pagamento from siscom_mi.pagamenti_liquidati where DATA_MANDATO BETWEEN '" & dataDal & "' AND '" & dataAl & "' AND ANNO_MANDATO='" & ANNO & "'  )" _
                        & condizioneGenerale _
                        & "AND ID_VOCE_PF IN (" _
                        & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                        & "WHERE CONNECT_BY_ISLEAF = 1 " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                        & "START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "') "

                    'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO,0)),'999999990D9999')) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    '    & "WHERE DATA_MANDATO BETWEEN '" & dataDal & "' AND '" & dataAl & "' " _
                    '    & "AND ANNO_MANDATO='" & ANNO & "' " _
                    '    & "AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) " _
                    '    & "AND ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    '    & "WHERE CONNECT_BY_ISLEAF=1 " _
                    '    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    '    & "START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "'" _
                    '    & ") "
                    '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                    '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                    '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°

                    Dim ElencoImportiLiquidatiSottovociDaSommare As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If ElencoImportiLiquidatiSottovociDaSommare.Read Then
                        TOTALELIQUIDATOSINGOLEVOCI = par.IfNull(ElencoImportiLiquidatiSottovociDaSommare("IMPORTO"), 0)
                    End If
                    ElencoImportiLiquidatiSottovociDaSommare.Close()

                    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    'MODIFICA 02/07/2013

                    par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO_RIT_LIQUIDATO,0)),'999999990D9999')) AS IMPORTO FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO  " _
                        & "WHERE id_pagamento_rit_legge in (select id_pagamento_rit_legge from siscom_mi.pagamenti_rit_liquidati where DATA_MANDATO BETWEEN '" & dataDal & "' AND '" & dataAl & "' AND ANNO_MANDATO='" & ANNO & "'  )" _
                        & condizioneGenerale _
                        & "AND ID_VOCE_PF IN (" _
                        & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                        & "WHERE CONNECT_BY_ISLEAF = 1 " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                        & "START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "') "



                    'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO,0)),'999999990D9999')) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI " _
                    '    & "WHERE DATA_MANDATO BETWEEN '" & dataDal & "' AND '" & dataAl & "' " _
                    '    & "AND ANNO_MANDATO='" & ANNO & "' " _
                    '    & "AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) " _
                    '    & "AND ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    '    & "WHERE CONNECT_BY_ISLEAF=1 " _
                    '    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    '    & "START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "'" _
                    '    & ") "
                    '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                    '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                    '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                    ElencoImportiLiquidatiSottovociDaSommare = par.cmd.ExecuteReader
                    If ElencoImportiLiquidatiSottovociDaSommare.Read Then
                        TOTALELIQUIDATOSINGOLEVOCI += par.IfNull(ElencoImportiLiquidatiSottovociDaSommare("IMPORTO"), 0)
                    End If
                    ElencoImportiLiquidatiSottovociDaSommare.Close()
                    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    'IMPORTO CERTIFICATO(NO RIT LEGGE)=IMPORTO APPROVATO(NO RIT LEGGE)-IMPORTO LIQUIDATO(NO RIT LEGGE)
                    Dim certificatoTOTALESINGOLEVOCI As Decimal = 0
                    certificatoTOTALESINGOLEVOCI = approvatoTOTALEsingoleVoci - TOTALELIQUIDATOSINGOLEVOCI



                    '###################################################################################################
                    '**************************************************************************************************
                    '####################### RITENUTE CONSUNTIVATE #########################

                    par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(RIT_LEGGE_IVATA,0)),'999999990D9999')) AS RIT " _
                        & "FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO  " _
                        & "WHERE ((ID_STATO=1) " _
                        & "or (id_stato=2 and data_certificazione >'" & dataAl & "'))" _
                        & "and data_consuntivazione BETWEEN '" & dataDal & "' AND '" & dataAl & "' " _
                        & "AND ANNO='" & ANNO & "' " _
                        & condizioneGenerale _
                        & "AND ID_VOCE_PF IN (" _
                        & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                        & "WHERE CONNECT_BY_ISLEAF = 1 " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                        & "START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "') "
                    ''*************************************************************
                    Dim ElencoRitenuteConsuntivate As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If ElencoRitenuteConsuntivate.Read Then
                        ritenuteLeggeConsuntivateTOTALESingoleVoci = par.IfNull(ElencoRitenuteConsuntivate("RIT"), 0)
                    End If
                    ElencoRitenuteConsuntivate.Close()

                    Dim ritenute As String = ""
                    If ritenuteLeggeCertificateTOTALESingoleVoci > 0 Then
                        ritenute = "<br />(di cui ritenute " & Format(ritenuteLeggeCertificateTOTALESingoleVoci, "##,##0.00") & ")"
                    End If

                    'DOPO MODIFICA 11/01/2012
                    ritenute = ""
                    '*************************************************************
                    '###################################################################################################

                    '###################################################################################################
                    '**************************************************************************************************
                    '####################### RITENUTE CERTIFICATE #########################

                    'RITENUTE
                    'OLD  SOSTITUITO PERCHè LA FUNZIONE GETSTATOPAGAMENTO RALLENTA L'ESECUZIONE
                    'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(RIT_LEGGE_IVATA,0)),'999999990D9999')) AS RIT " _
                    '    & "FROM SISCOM_MI.PRENOTAZIONI " _
                    '    & "WHERE PRENOTAZIONI.ID_STATO>1 " _
                    '    & "AND (SISCOM_MI.GETSTATOPAGAMENTO(PRENOTAZIONI.ID_PAGAMENTO)<>'EMESSO' OR SISCOM_MI.GETSTATOPAGAMENTO(PRENOTAZIONI.ID_PAGAMENTO) IS NULL) " _
                    '    & "AND ID_PAGAMENTO IS NOT NULL " _
                    '    & "AND DATA_PRENOTAZIONE<='" & dataAl & "' " _
                    '    & "AND ID_VOCE_PF IN (" _
                    '    & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    '    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    '    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    '    & "START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "') "

                    'ritenute certificate eliminate per effetto della modifica 13/01/2012
                    '°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~
                    '°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~
                    '°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~
                    'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(RIT_LEGGE_IVATA,0)),'999999990D9999')) AS RIT " _
                    '    & "FROM SISCOM_MI.PRENOTAZIONI " _
                    '    & "WHERE DATA_PRENOTAZIONE<='" & dataAl & "' " _
                    '    & "AND ID_STATO=2" _
                    '    & "AND ID_VOCE_PF IN (" _
                    '    & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    '    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    '    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    '    & "START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "') "

                    ''TOLTA DOPO MODIFICA 11/01/2012
                    ''& "AND (SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO)>0 " _
                    ''& "AND ID_PAGAMENTO IS NOT NULL " _
                    ''& AND PRENOTAZIONI.ID_STATO>1 " _
                    ' ''*************************************************************
                    'Dim ElencoRitenute As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    'If ElencoRitenute.Read Then
                    '    ritenuteLeggeCertificateTOTALESingoleVoci = par.IfNull(ElencoRitenute("RIT"), 0)
                    'End If
                    'ElencoRitenute.Close()
                    '°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~
                    '°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~
                    '°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~°~
                    '###################################################################################################
                    '**************************************************************************************************
                    '####################### RITENUTE LIQUIDATE #########################

                    Dim ritenuteLiquidateTOTALEsingoleVoci As Decimal = 0
                    '°°°°°°°°°°°°°°° NUOVO °°°°°°°°°°°°°°°
                    par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO_RIT_LIQUIDATO,0)),'999999990D9999')) AS IMPORTO FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO  " _
                        & "WHERE id_pagamento in (select id_pagamento_rit_legge from siscom_mi.pagamenti_rit_liquidati where DATA_MANDATO BETWEEN '" & dataDal & "' AND '" & dataAl & "' AND ANNO_MANDATO='" & ANNO & "'  )" _
                        & condizioneGenerale _
                        & "AND ID_VOCE_PF IN (" _
                        & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                        & "WHERE CONNECT_BY_ISLEAF = 1 " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                        & "START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "') "

                    'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(NVL(IMPORTO,0)),'999999990D9999')) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                    '    & "WHERE DATA_MANDATO BETWEEN '" & dataDal & "' AND '" & dataAl & "' " _
                    '    & "AND ANNO_MANDATO='" & ANNO & "' " _
                    '    & "AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI) " _
                    '    & "AND ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    '    & "WHERE CONNECT_BY_ISLEAF=1 " _
                    '    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    '    & "START WITH ID='" & par.IfNull(LettoreElencoVoci("ID"), "-1") & "'" _
                    '    & ") "
                    '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                    '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                    '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°

                    Dim ElencoImportiritenuteLiquidateSottovociDaSommare As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If ElencoImportiritenuteLiquidateSottovociDaSommare.Read Then
                        ritenuteLiquidateTOTALEsingoleVoci = par.IfNull(ElencoImportiritenuteLiquidateSottovociDaSommare("IMPORTO"), 0)
                    End If
                    ElencoImportiritenuteLiquidateSottovociDaSommare.Close()
                    '*************************************************************
                    '###################################################################################################
                    TOTALEPAGATOSINGOLEVOCI = TOTALELIQUIDATOSINGOLEVOCI + ritenuteLiquidateTOTALEsingoleVoci

                    assestamentoTotaleCap = budgetTOTALEcapitolo + assestamentoTOTALEcapitolo + variazioniTOTALEcapitolo
                    assestamentoTotaleSing = budgetTOTALEsingoleVoci + assestamentoTOTALEsingoleVoci + variazioniTOTALEsingoleVoci



                    Dim link As String = "href='javascript:void(0)' onclick=" & Chr(34) & "window.showModalDialog('DettaglioVociStruttura.aspx?ID=" & par.IfNull(LettoreElencoVoci("ID") & "&dataAl=" & dataAl, "") & "','window','status:no;dialogWidth:900px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');" & Chr(34)
                    If capitoloAttuale.Value = par.IfNull(Trim(Left(LettoreElencoVoci("CODICE"), 2)), "") Then
                        TestoPagina = TestoPagina & "<tr style=""background-color: white; font-family: Arial; font-size: 8pt;"">" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LettoreElencoVoci("CODICE"), "") & "</td>" _
                                                          & "<td align=""left"" style=""width:17%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ><a " & link & ">" & par.IfNull(LettoreElencoVoci("DESCRIZIONE"), "") & "</a></td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & filiale & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & lotto & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; "" >" & servizio & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; "" >" & VOCEservizio & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ><a href=""RisultatiPagamentiPerStruttura.aspx?" & "AL=" & Request.QueryString("AL") & "&IDS=-1&EF=" & qsEsercizioFinanziario & "&IDV=" & LettoreElencoVoci("ID") & """>" & Format(prenotatoTOTALEsingoleVoci, "##,##0.00") & "</a></td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ><a href=""RisultatiPagamentiPerStruttura.aspx?" & "AL=" & Request.QueryString("AL") & "&IDS=-1&EF=" & qsEsercizioFinanziario & "&IDV=" & LettoreElencoVoci("ID") & "&CONS=1"">" & Format(consuntivatoTOTALEsingoleVoci, "##,##0.00") & "</a></td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ><a href=""RisultatiPagamentiPerStruttura.aspx?" & "AL=" & Request.QueryString("AL") & "&IDS=-1&EF=" & qsEsercizioFinanziario & "&IDV=" & LettoreElencoVoci("ID") & "&RIT=1"">" & Format(ritenuteLeggeConsuntivateTOTALESingoleVoci, "##,##0.00") & "</a></td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ><a href=""RisultatiPagamentiPerStruttura.aspx?" & "AL=" & Request.QueryString("AL") & "&IDS=-1&EF=" & qsEsercizioFinanziario & "&IDV=" & LettoreElencoVoci("ID") & "&CERT=1"">" & Format(certificatoTOTALESINGOLEVOCI, "##,##0.00") & ritenute & "</a></td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ><a href=""RisultatiPagamentiPerStruttura.aspx?" & "AL=" & Request.QueryString("AL") & "&IDS=-1&EF=" & qsEsercizioFinanziario & "&IDV=" & LettoreElencoVoci("ID") & "&LIQ=1"">" & Format(TOTALELIQUIDATOSINGOLEVOCI, "##,##0.00") & "</a></td>" _
                                                          & "</tr>"
                        '& "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ><a href=""RisultatiPagamentiPerStruttura.aspx?" & "AL=" & Request.QueryString("AL") & "&IDS=-1&EF=" & qsEsercizioFinanziario & "&IDV=" & LettoreElencoVoci("ID") & "&RITLIQ=1"">" & Format(ritenuteLiquidateTOTALEsingoleVoci, "##,##0.00") & "</a></td>" _
                        If contatore <> 20 Then
                            contatore = contatore + 1
                            TestoPagina2 = TestoPagina2 & "<tr style=""background-color: white; font-family: Arial; font-size: 8pt;"">" _
                                                         & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LettoreElencoVoci("CODICE"), "") & "</td>" _
                                                         & "<td align=""left"" style=""width:17%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LettoreElencoVoci("DESCRIZIONE"), "") & "</td>" _
                                                         & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & filiale & "</td>" _
                                                         & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & lotto & "</td>" _
                                                         & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; "" >" & servizio & "</td>" _
                                                         & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; "" >" & VOCEservizio & "</td>" _
                                                         & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(prenotatoTOTALEsingoleVoci, "##,##0.00") & "</td>" _
                                                         & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(consuntivatoTOTALEsingoleVoci, "##,##0.00") & "</td>" _
                                                         & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(ritenuteLeggeConsuntivateTOTALESingoleVoci, "##,##0.00") & "</td>" _
                                                         & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(certificatoTOTALESINGOLEVOCI, "##,##0.00") & "</td>" _
                                                         & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(TOTALELIQUIDATOSINGOLEVOCI, "##,##0.00") & "</td>" _
                                                         & "</tr>"
                        Else
                            contatore = 0
                            TestoPagina2 = TestoPagina2 & "</table><p style='page-break-before: always'>&nbsp;</p>" _
                                     & "<table width=""100%"" cellspacing=""0"" cellpadding=""4"" rules=""all"" border=""1"" id=""DataGrid1"">" _
                                     & "<tr align=""center"" style=""color: White; background-color: #507CD1; font-family: Arial; font-size: 9pt; font-weight: bold;"">" _
                                     & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">COD.</td>" _
                                     & "<td td align=""center"" style=""width:17%;font-weight: bold; font-style: normal; text-decoration: none; "">VOCE</td>" _
                                     & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">FILIALE</td>" _
                                     & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">LOTTO</td>" _
                                     & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">SERVIZIO</td>" _
                                     & "<td td align=""center"" style=""width:5%;font-weight: bold; font-style: normal; text-decoration: none; "">VOCE SERVIZIO</td>" _
                                     & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE PRENOTATO</td>" _
                                     & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE CONSUNTIVATO</td>" _
                                     & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE RIT. LEGGE CONSUNTIVATE</td>" _
                                     & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE CERTIFICATO</td>" _
                                     & "<td td align=""center"" style=""width:8%;font-weight: bold; font-style: normal; text-decoration: none; "">TOTALE PAGATO</td>" _
                                     & "</tr>" _
                                     & "<tr style=""background-color: white; font-family: Arial; font-size: 8pt;"">" _
                                     & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LettoreElencoVoci("CODICE"), "") & "</td>" _
                                     & "<td align=""left"" style=""width:17%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LettoreElencoVoci("DESCRIZIONE"), "") & "</td>" _
                                     & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & filiale & "</td>" _
                                     & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & lotto & "</td>" _
                                     & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; "" >" & servizio & "</td>" _
                                     & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; "" >" & VOCEservizio & "</td>" _
                                     & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(prenotatoTOTALEsingoleVoci, "##,##0.00") & "</td>" _
                                     & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(consuntivatoTOTALEsingoleVoci, "##,##0.00") & "</td>" _
                                     & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(ritenuteLeggeConsuntivateTOTALESingoleVoci, "##,##0.00") & "</td>" _
                                     & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(certificatoTOTALESINGOLEVOCI, "##,##0.00") & "</td>" _
                                     & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(TOTALELIQUIDATOSINGOLEVOCI, "##,##0.00") & "</td>" _
                                     & "</tr>"
                        End If
                        '####################################################
                        RIGA = dt1.NewRow
                        RIGA.Item("COD.") = par.IfNull(LettoreElencoVoci("CODICE"), "")
                        RIGA.Item("VOCE") = par.IfNull(LettoreElencoVoci("DESCRIZIONE"), "")
                        RIGA.Item("FILIALE") = filiale
                        RIGA.Item("LOTTO") = lotto
                        RIGA.Item("SERVIZIO") = servizio
                        RIGA.Item("VOCE_SERVIZIO") = VOCEservizio
                        RIGA.Item("TOTALE_PRENOTATO") = Format(prenotatoTOTALEsingoleVoci, "##,##0.00")
                        RIGA.Item("TOTALE_CONSUNTIVATO") = Format(consuntivatoTOTALEsingoleVoci, "##,##0.00")
                        RIGA.Item("TOTALE_RIT") = Format(ritenuteLeggeConsuntivateTOTALESingoleVoci, "##,##0.00")
                        RIGA.Item("TOTALE CERTIFICATO") = Format(certificatoTOTALESINGOLEVOCI, "##,##0.00")
                        RIGA.Item("TOTALE_PAGATO") = Format(TOTALEPAGATOSINGOLEVOCI, "##,##0.00")
                        dt1.Rows.Add(RIGA)
                        '####################################################
                        'TOTALE SOLO SE VOCI DI LIVELLO 1
                        'COMMENTO PERCHè ADESSO PRENDO SOLO VOCI FOGLIA
                        'If par.IfNull(LettoreElencoVoci("LEVEL"), 0) = 1 Then
                        budgetTOTALEcapitolo = budgetTOTALEcapitolo + budgetTOTALEsingoleVoci
                        assestamentoTOTALEcapitolo = assestamentoTOTALEcapitolo + assestamentoTOTALEsingoleVoci
                        variazioniTOTALEcapitolo = variazioniTOTALEcapitolo + variazioniTOTALEsingoleVoci
                        prenotatoTOTALEcapitolo = prenotatoTOTALEcapitolo + prenotatoTOTALEsingoleVoci
                        consuntivatoTOTALEcapitolo = consuntivatoTOTALEcapitolo + consuntivatoTOTALEsingoleVoci
                        certificatoTOTALEcapitolo = certificatoTOTALEcapitolo + certificatoTOTALESINGOLEVOCI
                        liquidatoTOTALEcapitolo = liquidatoTOTALEcapitolo + TOTALEPAGATOSINGOLEVOCI
                        ritenuteLeggeConsuntivateTOTALEcapitolo = ritenuteLeggeConsuntivateTOTALEcapitolo + ritenuteLeggeConsuntivateTOTALESingoleVoci
                        'End If
                    Else




                        TabellaCompleta.Text = TabellaCompleta.Text & "<tr style=""background-color: gainsboro; font-family: Arial; font-size: 8pt;"">" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & codCap & "</td>" _
                                                          & "<td align=""left"" style=""width:17%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & desccap(codCap) & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; "" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; "" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(prenotatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(consuntivatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(ritenuteLeggeConsuntivateTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(certificatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(liquidatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "</tr>" & TestoPagina


                        totAssestamento = totAssestamento + assestamentoTotaleCap
                        totPrenotato = totPrenotato + prenotatoTOTALEcapitolo
                        totLiquidato = totLiquidato + liquidatoTOTALEcapitolo
                        totCertificato = totCertificato + certificatoTOTALEcapitolo
                        totconsuntivato = totconsuntivato + consuntivatoTOTALEcapitolo
                        totRitenuteConsuntivate = totRitenuteConsuntivate + ritenuteLeggeConsuntivateTOTALEcapitolo

                        TestoPaginaPDF = TestoPaginaPDF & "<tr style=""background-color: gainsboro; font-family: Arial; font-size: 8pt;"">" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & codCap & "</td>" _
                                                          & "<td align=""left"" style=""width:17%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & desccap(codCap) & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; "" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; "" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(prenotatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(consuntivatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(ritenuteLeggeConsuntivateTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(certificatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(liquidatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "</tr>" & TestoPagina2

                        '####################################################
                        TestoPagina2 = ""
                        RIGA = dt2.NewRow
                        RIGA.Item("COD.") = codCap
                        RIGA.Item("VOCE") = desccap(codCap)

                        RIGA.Item("FILIALE") = ""
                        RIGA.Item("LOTTO") = ""
                        RIGA.Item("SERVIZIO") = ""
                        RIGA.Item("VOCE_SERVIZIO") = ""

                        RIGA.Item("TOTALE_PRENOTATO") = Format(prenotatoTOTALEcapitolo, "##,##0.00")
                        RIGA.Item("TOTALE_CONSUNTIVATO") = Format(consuntivatoTOTALEcapitolo, "##,##0.00")
                        RIGA.Item("TOTALE_RIT") = Format(ritenuteLeggeConsuntivateTOTALEcapitolo, "##,##0.00")
                        RIGA.Item("TOTALE CERTIFICATO") = Format(certificatoTOTALEcapitolo, "##,##0.00")
                        RIGA.Item("TOTALE_PAGATO") = Format(liquidatoTOTALEcapitolo, "##,##0.00")
                        dt2.Rows.Add(RIGA)
                        dt2.Merge(dt1)
                        dt1.Rows.Clear()
                        '####################################################

                        TestoPagina = "<tr style=""background-color: white; font-family: Arial; font-size: 8pt;"">" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LettoreElencoVoci("CODICE"), "") & "</td>" _
                                                          & "<td align=""left"" style=""width:17%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ><a " & link & ">" & par.IfNull(LettoreElencoVoci("DESCRIZIONE"), "") & "</a></td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & filiale & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & lotto & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; "" >" & servizio & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; "" >" & VOCEservizio & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ><a href=""RisultatiPagamentiPerStruttura.aspx?" & "AL=" & Request.QueryString("AL") & "&IDS=-1&EF=" & qsEsercizioFinanziario & "&IDV=" & LettoreElencoVoci("ID") & """>" & Format(prenotatoTOTALEsingoleVoci, "##,##0.00") & "</a></td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ><a href=""RisultatiPagamentiPerStruttura.aspx?" & "AL=" & Request.QueryString("AL") & "&IDS=-1&EF=" & qsEsercizioFinanziario & "&IDV=" & LettoreElencoVoci("ID") & "&CONS=1"">" & Format(consuntivatoTOTALEsingoleVoci, "##,##0.00") & "</a></td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ><a href=""RisultatiPagamentiPerStruttura.aspx?" & "AL=" & Request.QueryString("AL") & "&IDS=-1&EF=" & qsEsercizioFinanziario & "&IDV=" & LettoreElencoVoci("ID") & "&RIT=1"">" & Format(ritenuteLeggeConsuntivateTOTALESingoleVoci, "##,##0.00") & "</a></td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ><a href=""RisultatiPagamentiPerStruttura.aspx?" & "AL=" & Request.QueryString("AL") & "&IDS=-1&EF=" & qsEsercizioFinanziario & "&IDV=" & LettoreElencoVoci("ID") & "&CERT=1"">" & Format(certificatoTOTALESINGOLEVOCI, "##,##0.00") & ritenute & "</a></td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ><a href=""RisultatiPagamentiPerStruttura.aspx?" & "AL=" & Request.QueryString("AL") & "&IDS=-1&EF=" & qsEsercizioFinanziario & "&IDV=" & LettoreElencoVoci("ID") & "&LIQ=1"">" & Format(TOTALELIQUIDATOSINGOLEVOCI, "##,##0.00") & "</a></td>" _
                                                          & "</tr>"
                        '& "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" ><a href=""RisultatiPagamentiPerStruttura.aspx?" & "AL=" & Request.QueryString("AL") & "&IDS=-1&EF=" & qsEsercizioFinanziario & "&IDV=" & LettoreElencoVoci("ID") & "&RITLIQ=1"">" & Format(ritenuteLiquidateTOTALEsingoleVoci, "##,##0.00") & "</a></td>" _
                        TestoPagina2 = "<tr style=""background-color: white; font-family: Arial; font-size: 8pt;"">" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LettoreElencoVoci("CODICE"), "") & "</td>" _
                                                          & "<td align=""left"" style=""width:17%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(LettoreElencoVoci("DESCRIZIONE"), "") & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; "" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: normal; font-style: normal; text-decoration: none; "" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(prenotatoTOTALEsingoleVoci, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(consuntivatoTOTALEsingoleVoci, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(ritenuteLeggeConsuntivateTOTALESingoleVoci, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(certificatoTOTALESINGOLEVOCI, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(TOTALELIQUIDATOSINGOLEVOCI, "##,##0.00") & "</td>" _
                                                          & "</tr>"


                        '####################################################
                        RIGA = dt1.NewRow
                        RIGA.Item("COD.") = par.IfNull(LettoreElencoVoci("CODICE"), "")
                        RIGA.Item("VOCE") = par.IfNull(LettoreElencoVoci("DESCRIZIONE"), "")
                        RIGA.Item("FILIALE") = filiale
                        RIGA.Item("LOTTO") = lotto
                        RIGA.Item("SERVIZIO") = servizio
                        RIGA.Item("VOCE_SERVIZIO") = VOCEservizio
                        RIGA.Item("TOTALE_PRENOTATO") = Format(prenotatoTOTALEsingoleVoci, "##,##0.00")
                        RIGA.Item("TOTALE_CONSUNTIVATO") = Format(consuntivatoTOTALEsingoleVoci, "##,##0.00")
                        RIGA.Item("TOTALE_RIT") = Format(ritenuteLeggeConsuntivateTOTALESingoleVoci, "##,##0.00")
                        RIGA.Item("TOTALE CERTIFICATO") = Format(certificatoTOTALESINGOLEVOCI, "##,##0.00")
                        RIGA.Item("TOTALE_PAGATO") = Format(TOTALELIQUIDATOSINGOLEVOCI, "##,##0.00")
                        dt1.Rows.Add(RIGA)
                        '####################################################

                        budgetTOTALEcapitolo = budgetTOTALEsingoleVoci
                        assestamentoTOTALEcapitolo = assestamentoTOTALEsingoleVoci
                        variazioniTOTALEcapitolo = variazioniTOTALEsingoleVoci
                        prenotatoTOTALEcapitolo = prenotatoTOTALEsingoleVoci
                        consuntivatoTOTALEcapitolo = consuntivatoTOTALEsingoleVoci
                        liquidatoTOTALEcapitolo = TOTALEPAGATOSINGOLEVOCI
                        certificatoTOTALEcapitolo = certificatoTOTALESINGOLEVOCI
                        ritenuteLeggeConsuntivateTOTALEcapitolo = ritenuteLeggeConsuntivateTOTALESingoleVoci
                        codCap = codCap + 1


                    End If

                    capitoloAttuale.Value = par.IfNull(Trim(Left(LettoreElencoVoci("CODICE"), 2)), "")

                End While

                lettore.Close()

            End While

            LettoreElencoVoci.Close()

            'INSERIMENTO DELL'ULTIMO BLOCCO NELLA TABELLA
            TabellaCompleta.Text = TabellaCompleta.Text & "<tr style=""background-color: gainsboro; font-family: Arial; font-size: 8pt;"">" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & codCap & "</td>" _
                                                          & "<td align=""left"" style=""width:17%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & desccap(codCap) & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; "" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; "" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(prenotatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(consuntivatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(ritenuteLeggeConsuntivateTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(certificatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(liquidatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "</tr>" & TestoPagina


            totAssestamento = totAssestamento + assestamentoTotaleCap
            totPrenotato = totPrenotato + prenotatoTOTALEcapitolo
            totLiquidato = totLiquidato + liquidatoTOTALEcapitolo
            totCertificato = totCertificato + certificatoTOTALEcapitolo
            totconsuntivato = totconsuntivato + consuntivatoTOTALEcapitolo
            totRitenuteConsuntivate = totRitenuteConsuntivate + ritenuteLeggeConsuntivateTOTALEcapitolo

            TestoPaginaPDF = TestoPaginaPDF & "<tr style=""background-color: gainsboro; font-family: Arial; font-size: 8pt;"">" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & codCap & "</td>" _
                                                          & "<td align=""left"" style=""width:17%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & desccap(codCap) & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; "" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; "" >" & "&nbsp;" & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(prenotatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(consuntivatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(ritenuteLeggeConsuntivateTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(certificatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(liquidatoTOTALEcapitolo, "##,##0.00") & "</td>" _
                                                          & "</tr>" & TestoPagina2

            '####################################################
            RIGA = dt2.NewRow
            RIGA.Item("COD.") = codCap
            RIGA.Item("VOCE") = desccap(codCap)
            RIGA.Item("FILIALE") = ""
            RIGA.Item("LOTTO") = ""
            RIGA.Item("SERVIZIO") = ""
            RIGA.Item("VOCE_SERVIZIO") = ""
            RIGA.Item("TOTALE_PRENOTATO") = Format(prenotatoTOTALEcapitolo, "##,##0.00")
            RIGA.Item("TOTALE_CONSUNTIVATO") = Format(consuntivatoTOTALEcapitolo, "##,##0.00")
            RIGA.Item("TOTALE_RIT") = Format(ritenuteLeggeConsuntivateTOTALEcapitolo, "##,##0.00")
            RIGA.Item("TOTALE CERTIFICATO") = Format(certificatoTOTALEcapitolo, "##,##0.00")
            RIGA.Item("TOTALE_PAGATO") = Format(liquidatoTOTALEcapitolo, "##,##0.00")
            dt2.Rows.Add(RIGA)
            dt2.Merge(dt1)

            totResiduo = totAssestamento - totPrenotato - totconsuntivato - totCertificato - totLiquidato - totRitenuteConsuntivate
            '####################################################
            TabellaCompleta.Text = TabellaCompleta.Text & "<tr style=""font-color:red; background-color: gainsboro; font-family: Arial; font-size: 8pt;"">" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >&nbsp;</td>" _
                                                          & "<td align=""left"" style=""width:17%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >TOTALE</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >&nbsp;</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >&nbsp;</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; "" >&nbsp;</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; "" >&nbsp;</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(totPrenotato, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(totconsuntivato, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(totRitenuteConsuntivate, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(totCertificato, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(totLiquidato, "##,##0.00") & "</td>" _
                                                          & "</tr></table>"

            TestoPaginaPDF = TestoPaginaPDF & "<tr style=""font-color:red; background-color: gainsboro; font-family: Arial; font-size: 8pt;"">" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >&nbsp;</td>" _
                                                          & "<td align=""left"" style=""width:17%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >TOTALE</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >&nbsp;</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >&nbsp;</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; "" >&nbsp;</td>" _
                                                          & "<td align=""left"" style=""width:5%; font-weight: bold; font-style: normal; text-decoration: none; "" >&nbsp;</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(totPrenotato, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(totconsuntivato, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(totRitenuteConsuntivate, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(totCertificato, "##,##0.00") & "</td>" _
                                                          & "<td align=""right"" style=""width:8%; font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(totLiquidato, "##,##0.00") & "</td>" _
                                                          & "</tr></table>"


            RIGA = dt2.NewRow
            RIGA.Item("COD.") = ""
            RIGA.Item("VOCE") = ""
            RIGA.Item("FILIALE") = ""
            RIGA.Item("LOTTO") = ""
            RIGA.Item("SERVIZIO") = ""
            RIGA.Item("VOCE_SERVIZIO") = ""
            RIGA.Item("TOTALE_PRENOTATO") = Format(totPrenotato, "##,##0.00")
            RIGA.Item("TOTALE_CONSUNTIVATO") = Format(totconsuntivato, "##,##0.00")
            RIGA.Item("TOTALE_RIT") = Format(totRitenuteConsuntivate, "##,##0.00")
            RIGA.Item("TOTALE CERTIFICATO") = Format(totCertificato, "##,##0.00")
            RIGA.Item("TOTALE_PAGATO") = Format(totLiquidato, "##,##0.00")
            dt2.Rows.Add(RIGA)

            Session.Add("dtPF", dt2)
            TabellaCompleta.Text = par.EliminaLink(TabellaCompleta.Text)

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Response.Write(ex.Message)
        End Try

    End Sub

    Private Function codCap(ByVal cod As String) As String
        If Len(cod) < 2 Then
            Return ""
        Else
            Return Left(cod, 1)
        End If
    End Function
 Protected Sub btnStampaPDF_Click(sender As Object, e As System.EventArgs) Handles btnStampaPDF.Click
        Dim NomeFile As String = "PF_" & Format(Now, "yyyyMMddHHmmss")
        Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
        sr.WriteLine(TestoPaginaPDF)
        sr.Close()
        Dim url As String = NomeFile
        Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
        Dim pdfConverter As PdfConverter = New PdfConverter
        If Licenza <> "" Then
            pdfConverter.LicenseKey = Licenza
        End If
        pdfConverter.PageWidth = 1600
        pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
        pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        pdfConverter.PdfDocumentOptions.ShowHeader = True
        pdfConverter.PdfDocumentOptions.ShowFooter = True
        pdfConverter.PdfDocumentOptions.LeftMargin = 20
        pdfConverter.PdfDocumentOptions.RightMargin = 20
        pdfConverter.PdfDocumentOptions.TopMargin = 5
        pdfConverter.PdfDocumentOptions.BottomMargin = 5
        pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

        pdfConverter.PdfFooterOptions.FooterText = ("")
        pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
        pdfConverter.PdfFooterOptions.DrawFooterLine = False
        pdfConverter.PdfFooterOptions.PageNumberText = "Pag "
        pdfConverter.PdfHeaderOptions.HeaderTextFontSize = 8
        pdfConverter.PdfHeaderOptions.HeaderTextFontType = ExpertPdf.HtmlToPdf.PdfFontType.None
        pdfConverter.PdfFooterOptions.ShowPageNumber = True

        pdfConverter.PdfHeaderOptions.HeaderText = Format(Now, "dd/MM/yyyy HH.mm")
        pdfConverter.PdfHeaderOptions.HeaderTextAlign = ExpertPdf.HtmlToPdf.HorizontalTextAlign.Right
        pdfConverter.PdfHeaderOptions.DrawHeaderLine = False
        pdfConverter.PdfHeaderOptions.HeaderTextFontSize = 8
        pdfConverter.PdfHeaderOptions.HeaderTextFontType = ExpertPdf.HtmlToPdf.PdfFontType.None
        pdfConverter.PdfHeaderOptions.HeaderHeight = 15


        pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".pdf")
        IO.File.Delete(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm")
        FIN.Value = "1"
        Response.Write("<script>window.open('..\\..\\..\\FileTemp\\" & NomeFile & ".pdf','Stampe');</script>")
    End Sub



    Private Function desccap(ByVal codcap As Integer) As String
        Select Case codcap
            Case 1
                Return "Spese per il property management"
            Case 2
                Return "Spese per il facility management"
            Case 3
                Return "Spese per contributo per sostegno agli inquilini"
            Case 4
                Return "Spese diverse"
            Case Else
                Return ""
        End Select
    End Function

    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Response.Write("<script>self.close();</script>")
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        '#### EXPORT IN EXCEL ####

        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            Dim datatable As Data.DataTable
            datatable = CType(HttpContext.Current.Session.Item("dtPF"), Data.DataTable)


            sNomeFile = "PF_EXCEL_" & Format(Now, "yyyyMMddHHmmss")
            i = 0

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
                .SetColumnWidth(1, 1, 20)
                .SetColumnWidth(2, 2, 120)
                .SetColumnWidth(3, 3, 30)
                .SetColumnWidth(4, 4, 30)
                .SetColumnWidth(5, 5, 30)
                .SetColumnWidth(6, 6, 30)
                .SetColumnWidth(7, 7, 30)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "SITUAZIONE CONTABILE: " & DataEsercizio)
                K = 2
                For Each row In datatable.Rows
                    If row.Item("COD.") = "COD." Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("COD."))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, row.Item("VOCE"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, row.Item("FILIALE"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, row.Item("LOTTO"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, row.Item("SERVIZIO"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("VOCE_SERVIZIO"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, row.Item("TOTALE_PRENOTATO"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, row.Item("TOTALE_CONSUNTIVATO"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, row.Item("TOTALE_RIT"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, row.Item("TOTALE CERTIFICATO"))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, row.Item("TOTALE_PAGATO"))

                    Else
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("COD.").ToString.Replace("&nbsp;", ""), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, row.Item("VOCE").ToString.Replace("&nbsp;", ""), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, row.Item("FILIALE").ToString.Replace("&nbsp;", ""), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, row.Item("LOTTO").ToString.Replace("&nbsp;", ""), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, row.Item("SERVIZIO").ToString.Replace("&nbsp;", ""), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("VOCE_SERVIZIO").ToString.Replace("&nbsp;", ""), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, row.Item("TOTALE_PRENOTATO").ToString.Replace("&nbsp;", ""), 4)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, row.Item("TOTALE_CONSUNTIVATO").ToString.Replace("&nbsp;", ""), 4)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, row.Item("TOTALE_RIT").ToString.Replace("&nbsp;", ""), 4)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, row.Item("TOTALE CERTIFICATO").ToString.Replace("&nbsp;", ""), 4)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, row.Item("TOTALE_PAGATO").ToString.Replace("&nbsp;", ""), 4)

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
        Session.Remove("dtPF")
    End Sub

   
End Class
