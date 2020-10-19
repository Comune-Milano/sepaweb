
Partial Class Contabilita_CompensiAler

    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim testoTabella As String = ""
    'Dim testoTabellaRiepilogo As String = ""
    Dim BollBimestreMassivo As Double = 0
    Dim Accertato As Double = -1
    Dim IncassoCumulato As Double = 0
    Dim dtDateMassive As System.Data.DataTable
    Dim i As Integer = 1
    Dim DataBollMassiva As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:300px; left:750px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"


        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            If Request.QueryString("ANNO") <> "" Then
                Me.lblTitolo.Text = "CALCOLO RIMBORSO SPESE GESTORE ANNO " & Request.QueryString("ANNO")
                'testoTabellaRiepilogo = testoTabellaRiepilogo & "<table cellpadding='1' cellspacing='2' width='40%'>" & vbCrLf _
                '& "<tr bgcolor = '#E6E6E6' >" _
                '& "<td style='height: 19px;text-align: center; width : 30%'>" _
                '& "<span style='font-size: 8pt; font-family: Arial'><strong>MESE</strong></span></td>" _
                '& "<td style='height: 19px;text-align: center'>" _
                '& "<span style='font-size: 8pt; font-family: Arial'><strong>TOTAE MENSILE GESTORE</strong></span></td>" _
                '& "</tr>"
                CalcolaCompensi()
                'TblRiepilogo()
                'Me.TBL_COMPENSO_RIEPILOGO.Text = testoTabellaRiepilogo & "</table>"
            Else
                Response.Write("<script>alert('Impossibile eseguire il calcolo!Selezionare un anno!');</script>")
                Response.Write("<script>window.close();</script>")
            End If
        End If
    End Sub

    Private Sub CalcolaCompensi()
        Try
            '******APERTURA CONNESSIONE*****
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT DISTINCT(bol_bollette.data_emissione) AS Data_bol_massiva, N_rata FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE.RIF_FILE LIKE'BO_%' AND anno = " & Request.QueryString("ANNO") & " AND fl_annullata = 0 AND N_RATA>=1 AND N_RATA<=12 ORDER BY N_RATA ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtDateMassive = New Data.DataTable
            da.Fill(dtDateMassive)


            '**************LOOP GENERALE PER TUTTI E 12 I MESI
            Dim Mese As Integer = 1
            For Mese = 1 To 12 Step 2
                If Mese Mod 2 = 1 Then

                    DefinisciBimestre(Mese)
                    If BollBimestreMassivo > 0 Then
                        Rendicontazione(Mese)
                        'MesiTblRiepilogo(Mese)
                    Else
                        testoTabella = testoTabella _
                                        & "<table cellpadding='1' cellspacing='2' width='100%'>" & vbCrLf _
                                                    & "<tr bgcolor = '#E6E6E6' >" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>GIORNI</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>BOLLETTATO MASSIVO</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>NUOVI CONTRATTI</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>ANNULLI</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>MESSE IN MORA CUMULATI(annullati)</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>ACCERTATO</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>BOLLETTATO PER PROCEDURA</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>INCASSO CUMULATO</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>ALGORITMI CUMULATI</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>SOGLIA 80%</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>RIPIANATO A GESTORE CUMULATO</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>RIPIANATO A GESTORE OGNI 30GG</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>RIPIANATO A GESTORE PER MESSA IN MORA</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>AGGIUSTAMENTO</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE GESTORE</strong></span></td>" _
                                                    & "<td style='height: 19px;text-align: center'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>DA DARE AL GESTORE PER MESSA IN MORA IN FASE DI RENDICONTAZIONE</strong></span></td>" _
                                                    & "</tr>"
                    End If
                End If
            Next
            TBL_COMPENSO_BIMESTRALE.Text = testoTabella & "</table>"
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
    Private Sub DefinisciBimestre(ByVal Month As Integer)
        Try

            If Month Mod 2 = 1 Then
                '**************TOTALE BOLLETTATO MASSIVO BIMESTRE IN CONSIDERAZIONE DI COMPETENZA GESTORE***************
                'questa ci mette 1sec
                'par.cmd.CommandText = "SELECT (SUM(BOL_BOLLETTE_VOCI.IMPORTO)) AS BOLLETTATOMASSIVO FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE.RIF_FILE<>'MOD' AND BOL_BOLLETTE.RIF_FILE<>'MAV'AND BOL_BOLLETTE.RIF_FILE<>'MOR'AND BOL_BOLLETTE.RIF_FILE<>'REC'AND RIFERIMENTO_DA>= " & Request.QueryString("ANNO") & MeseQuery(Month) & "01 AND RIFERIMENTO_A<= " & Request.QueryString("ANNO") & MeseQuery(Month + 1) & "31 AND T_VOCI_BOLLETTA.COMPETENZA=2 AND fl_annullata = 0 "
                'CAMBIO CONDIZIONE CON (SUBSTR(RIF_FILE,1,3)='BO_' PER PRENDERE SOLO TUTTE LE BOLLETTE MASSIVE
                'questa ci mette 12sec

                par.cmd.CommandText = "SELECT DISTINCT(bol_bollette.data_emissione) AS Data_bol_massiva, N_rata FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE.RIF_FILE LIKE'BO_%' AND anno = " & Request.QueryString("ANNO") & " AND fl_annullata = 0 AND N_RATA>=" & Month & " AND N_RATA<=" & Month + 1 & " ORDER BY N_RATA ASC"
                Dim ReaderDataMassiva As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If ReaderDataMassiva.Read Then
                    DataBollMassiva = ReaderDataMassiva("DATA_BOL_MASSIVA")
                Else
                    DataBollMassiva = ""
                End If
                ReaderDataMassiva.Close()
                'CONTROLLO CHE LE DATE DI BOLLETTAZIONE MASSIVE SIANO RECUPERATE DAL SISTEMA
                If dtDateMassive.Rows.Count > 0 Then
                    par.cmd.CommandText = "SELECT (SUM(BOL_BOLLETTE_VOCI.IMPORTO)) AS BOLLETTATOMASSIVO FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE.RIF_FILE like'BO_%' AND RIFERIMENTO_DA>= " & Request.QueryString("ANNO") & MeseQuery(Month) & "01 AND RIFERIMENTO_A<= " & Request.QueryString("ANNO") & MeseQuery(Month + 1) & "31 AND T_VOCI_BOLLETTA.COMPETENZA=2 AND fl_annullata = 0 "

                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    BollBimestreMassivo = 0
                    If myReader.Read Then
                        testoTabella = testoTabella & "<table cellpadding='1' cellspacing='2' width='40%'>" & vbCrLf _
                                        & "<tr bgcolor = '#E6E6E6' >" _
                                        & "<td style='height: 19px;text-align: center; width : 30%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>BIMESTRE</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center; width : 10%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA BOLL. MASSIVA</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>BOLLETTATO MASSIVO COMPETENZA GESTORE</strong></span></td>" _
                                        & "</tr>" _
                                        & "<tr bgcolor = '#FFFFFF'>" _
                                        & "<td style='height: 19px;text-align: center; width : 30%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>" & NomeMeseDaNumero(Month) & " - " & NomeMeseDaNumero(Month + 1) & "</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center; width : 30%'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>" & par.FormattaData(DataBollMassiva) & "</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: right;'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>€ " & Format(par.IfNull(myReader("BOLLETTATOMASSIVO"), 0), "##,##0.00") & "</strong></span></td>" _
                                        & "</tr>" _
                                        & "</table>"
                        BollBimestreMassivo = par.IfNull(myReader("BOLLETTATOMASSIVO"), 0)
                    End If
                    myReader.Close()
                    '**************SCRIVO NELLA LBL CHE COMPONE LA TABELLA, IL BIMESTRE IL BOLLETTATO
                Else
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
    Private Sub Rendicontazione(ByVal Month As Integer)
        Try

            Dim T As Integer = 0
            testoTabella = testoTabella _
                            & "<table cellpadding='1' cellspacing='2' width='100%'>" & vbCrLf _
                                        & "<tr bgcolor = '#E6E6E6' >" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>GIORNI</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>BOLLETTATO MASSIVO</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>NUOVI CONTRATTI</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>ANNULLI</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>MESSE IN MORA CUMULATI(annullati)</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>ACCERTATO</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>BOLLETTATO PER PROCEDURA</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>INCASSO CUMULATO</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>ALGORITMI CUMULATI</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>SOGLIA 80%</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>RIPIANATO A GESTORE CUMULATO</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>RIPIANATO A GESTORE OGNI 30GG</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>RIPIANATO A GESTORE PER MESSA IN MORA</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>AGGIUSTAMENTO</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE GESTORE</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DA DARE AL GESTORE PER MESSA IN MORA IN FASE DI RENDICONTAZIONE</strong></span></td>" _
                                        & "</tr>"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader

            Dim NuoviContratti As Double
            Dim SommaNuoviTmenoUno As Double = 0

            Dim Annullamenti As Double
            Dim AnnullamentiOgniT As Double = 0

            Dim MORA As Double = 0
            Dim DiffMoreTmenoUno As Double = 0

            Dim AlgoritmiCumulati As Double
            Dim AlgoritmiCumulatiOgniT As Double = 0

            Dim BollettatoProcedura As Double

            Dim Soglia80 As Double

            Dim RipianatoALER As Double
            Dim RipianatoAlerOgniT As Double = 0

            Dim Aggiustamento As Double

            Dim RipianatoAlerMORA As Double

            Dim TOTALERogniT As Double

            Dim TOTALER As Double = 0
            Dim TOTRendicont As Double = 0

            Dim IncassiCumulatiTDiff As Double = 0

            If DataBollMassiva <> "" Then

                While Accertato <> IncassoCumulato + MORA And OttieniDataEmissioneDaMassiva(DataBollMassiva, T) <= Format(Date.Now, "yyyyMMdd")
                    NuoviContratti = 0
                    Annullamenti = 0
                    MORA = 0
                    Accertato = 0
                    AlgoritmiCumulati = 0
                    BollettatoProcedura = 0
                    Soglia80 = 0
                    RipianatoALER = 0
                    Aggiustamento = 0
                    RipianatoAlerMORA = 0
                    TOTALERogniT = 0
                    IncassoCumulato = 0
                    If T = 180 Then
                        Beep()
                    End If
                    testoTabella = testoTabella & "<tr bgcolor = '#FFFFFF'>" _
                                            & "<td style='height: 19px;text-align: left'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & T & "</span></td>" _
                                            & "<td style='height: 19px;text-align: right'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & Format(BollBimestreMassivo, "##,##0.00") & "</span></td>"


                    '*******************NUOVI CONTRATTI CUMULATI**************
                    par.cmd.CommandText = "SELECT (SUM(BOL_BOLLETTE_VOCI.IMPORTO)) AS NUOVICONTRATTI FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND (BOL_BOLLETTE.RIF_FILE='MOD' OR BOL_BOLLETTE.RIF_FILE='MAV'OR BOL_BOLLETTE.RIF_FILE='REC') AND RIFERIMENTO_DA>= " & Request.QueryString("ANNO") & MeseQuery(Month) & "01 AND RIFERIMENTO_A<= " & Request.QueryString("ANNO") & MeseQuery(Month + 1) & "31 AND DATA_EMISSIONE <=" & OttieniDataEmissioneDaMassiva(DataBollMassiva, T) & " AND T_VOCI_BOLLETTA.COMPETENZA=2 AND fl_annullata = 0 "
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        NuoviContratti = par.IfNull(myReader("NUOVICONTRATTI"), 0)
                    End If
                    If T = 150 Then
                        Beep()
                    End If
                    SommaNuoviTmenoUno = NuoviContratti - SommaNuoviTmenoUno
                    testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & Format(NuoviContratti, "##,##0.00") & "</span></td>"


                    '********************ANNULLAMENTI CUMULATI**************
                    par.cmd.CommandText = "SELECT (SUM(BOL_BOLLETTE_VOCI.IMPORTO)) AS ANNULLAMENTI FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND RIF_FILE NOT LIKE 'BO_%' AND RIFERIMENTO_DA>= '" & Request.QueryString("ANNO") & MeseQuery(Month) & "01' AND RIFERIMENTO_A<= " & Request.QueryString("ANNO") & MeseQuery(Month + 1) & "31 AND DATA_ANNULLO>= " & Request.QueryString("ANNO") & MeseQuery(Month) & "01 AND DATA_ANNULLO<= " & OttieniDataEmissioneDaMassiva(DataBollMassiva, T) & "   AND T_VOCI_BOLLETTA.COMPETENZA=2 AND fl_annullata = 1 AND (DATA_PAGAMENTO IS NULL OR DATA_PAGAMENTO>" & OttieniDataEmissione(Request.QueryString("ANNO"), Month, T) & ")"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        Annullamenti = par.IfNull(myReader("ANNULLAMENTI"), 0)
                    End If
                    testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & Format(Annullamenti, "##,##0.00") & "</span></td>"
                    AnnullamentiOgniT = Annullamenti - AnnullamentiOgniT

                    '********************MESSE IN MORA CUMULATI(annullati)*************
                    par.cmd.CommandText = "SELECT (SUM(BOL_BOLLETTE_VOCI.IMPORTO)) AS MORA FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND RIFERIMENTO_DA>= " & Request.QueryString("ANNO") & MeseQuery(Month) & "01 AND RIFERIMENTO_A<= " & Request.QueryString("ANNO") & MeseQuery(Month + 1) & "31 AND DATA_MORA >= '" & Request.QueryString("ANNO") & MeseQuery(Month) & "01' AND DATA_MORA <=" & OttieniDataEmissioneDaMassiva(DataBollMassiva, T) & " AND T_VOCI_BOLLETTA.COMPETENZA=2"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        MORA = par.IfNull(myReader("MORA"), 0)
                    End If
                    DiffMoreTmenoUno = MORA - DiffMoreTmenoUno
                    testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & Format(MORA, "##,##0.00") & "</span></td>"

                    '********************ACCERTATO*************
                    Accertato = BollBimestreMassivo + NuoviContratti - Annullamenti
                    testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & Format(Accertato, "##,##0.00") & "</span></td>"

                    '********************BOLLETTATO PER PROCEDURA*************
                    BollettatoProcedura = BollBimestreMassivo + NuoviContratti - Annullamenti - MORA
                    testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & Format(BollettatoProcedura, "##,##0.00") & "</span></td>"


                    ''********************INCASSO CUMULATO*********************
                    par.cmd.CommandText = "SELECT (SUM (BOL_BOLLETTE_VOCI.IMPORTO)) AS INCASSATO FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL AND BOL_BOLLETTE.RIF_FILE<>'MOR'AND T_VOCI_BOLLETTA.COMPETENZA=2 AND fl_annullata = 0 AND RIFERIMENTO_DA>= '" & Request.QueryString("ANNO") & MeseQuery(Month) & "01' AND RIFERIMENTO_A<= '" & Request.QueryString("ANNO") & MeseQuery(Month + 1) & "31' AND DATA_PAGAMENTO>='" & Request.QueryString("ANNO") & MeseQuery(Month) & "' AND DATA_PAGAMENTO <='" & OttieniDataEmissioneDaMassiva(DataBollMassiva, T) & "'"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        IncassoCumulato = par.IfNull(myReader("INCASSATO"), 0)
                        IncassiCumulatiTDiff = IncassoCumulato - IncassiCumulatiTDiff
                    End If
                    testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & Format(IncassoCumulato, "##,##0.00") & "</span></td>"

                    '*******************ALGORITMI CUMULATI**********************

                    If T > 0 Then
                        If T = 30 Then
                            AlgoritmiCumulati = IncassoCumulato + (0.5 * (BollettatoProcedura - IncassoCumulato))
                            AlgoritmiCumulatiOgniT = AlgoritmiCumulati
                        Else
                            AlgoritmiCumulati = AlgoritmiCumulatiOgniT + (0.5 * (IncassiCumulatiTDiff))
                            AlgoritmiCumulatiOgniT = AlgoritmiCumulati
                        End If
                    End If

                    testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                                                & "<span style='font-size: 8pt; font-family: Arial'>" & Format(AlgoritmiCumulati, "##,##0.00") & "</span></td>"

                    '*******************SOGLIA 80% ******************************
                    If BollettatoProcedura > 0 Then
                        Soglia80 = BollettatoProcedura * 0.8
                    Else
                        Soglia80 = 0
                    End If
                    testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'>" & Format(Soglia80, "##,##0.00") & "</span></td>"

                    '*****************RIPIANATO A GESTORE*************************
                    If IncassoCumulato < Soglia80 Then
                        RipianatoALER = Math.Min(AlgoritmiCumulati, Soglia80)
                    Else
                        RipianatoALER = IncassoCumulato
                    End If

                    RipianatoAlerOgniT = RipianatoALER - RipianatoAlerOgniT

                    testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                                                & "<span style='font-size: 8pt; font-family: Arial'>" & Format(RipianatoALER, "##,##0.00") & "</span></td>"

                    '*****************RIPIANATO A GESTORE OGNI 30GG***************
                    testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'>" & Format(RipianatoAlerOgniT, "##,##0.00") & "</span></td>"



                    '*****************RIPIANATO PER MESSA IN MORA***************
                    RipianatoAlerMORA = 0.5 * DiffMoreTmenoUno
                    testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                                                & "<span style='font-size: 8pt; font-family: Arial'>" & Format(RipianatoAlerMORA, "##,##0.00") & "</span></td>"

                    '*****************AGGIUSTAMENTO*****************************
                    'If T = 180 Then
                    '    Beep()
                    'End If
                    If RipianatoAlerOgniT < 0 Then
                        Aggiustamento = (-RipianatoAlerOgniT - (0.8 * (AnnullamentiOgniT - SommaNuoviTmenoUno)))
                    Else
                        Aggiustamento = 0
                    End If
                    testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                                                & "<span style='font-size: 8pt; font-family: Arial'>" & Format(Aggiustamento, "##,##0.00") & "</span></td>"



                    '*****************T O T A L E GESTORE**************************
                    TOTALERogniT = RipianatoAlerOgniT + RipianatoAlerMORA + Aggiustamento
                    testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                                                & "<span style='font-size: 8pt; font-family: Arial'>" & Format(par.IfNull(TOTALERogniT, 0), "##,##0.00") & "</span></td>"
                    '****************DA DARE AL GESTORE IN FASE DI RENDICONTAZIONE PER MORA
                    testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                                                & "<span style='font-size: 8pt; font-family: Arial'>" & Format((RipianatoAlerMORA - Aggiustamento), "##,##0.00") & "</span></td>" _
                                                & "</tr>"




                    SommaNuoviTmenoUno = NuoviContratti
                    RipianatoAlerOgniT = RipianatoALER
                    AnnullamentiOgniT = Annullamenti
                    IncassiCumulatiTDiff = IncassoCumulato
                    DiffMoreTmenoUno = MORA
                    TOTALER = TOTALER + TOTALERogniT
                    TOTRendicont = TOTRendicont + (RipianatoAlerMORA - Aggiustamento)
                    T = T + 30
                End While
                'RIGA TOTALE GENERALE AL GESTORE OGNI BIMESTRE
            End If

            testoTabella = testoTabella & "<tr bgcolor = '#6495ED' >" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>T O T</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                        & "<td style='height: 19px;text-align: center'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                        & "<td style='height: 19px;text-align: right'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>" & Format(TOTALER, "##,##0.00") & "</strong></span></td>" _
                                        & "<td style='height: 19px;text-align: right'>" _
                                        & "<span style='font-size: 8pt; font-family: Arial'><strong>" & Format(TOTRendicont, "##,##0.00") & "</strong></span></td>" _
                                        & "</tr>"

            Dim a As String
            a = "USCITO"
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    'Private Sub TblRiepilogo()
    '    testoTabellaRiepilogo = testoTabellaRiepilogo & "<tr bgcolor = '#FFFFFF'>" _
    '                    & "<td style='height: 19px;text-align: left'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>Gennaio</span></td>" _
    '                    & "<td style='height: 19px;text-align: right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Gennaio, "##,##0.00") & "</span></td>" _
    '                    & "</tr>" _
    '                    & "<tr bgcolor = '#FFFFFF'>" _
    '                    & "<td style='height: 19px;text-align: left'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>Febbraio</span></td>" _
    '                    & "<td style='height: 19px;text-align: right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Febbraio, "##,##0.00") & "</span></td>" _
    '                    & "</tr>" _
    '                    & "<tr bgcolor = '#FFFFFF'>" _
    '                    & "<td style='height: 19px;text-align: left'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>Marzo</span></td>" _
    '                    & "<td style='height: 19px;text-align: right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Marzo, "##,##0.00") & "</span></td>" _
    '                    & "</tr>" _
    '                    & "<tr bgcolor = '#FFFFFF'>" _
    '                    & "<td style='height: 19px;text-align: left'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>Aprile</span></td>" _
    '                    & "<td style='height: 19px;text-align: right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Aprile, "##,##0.00") & "</span></td>" _
    '                    & "</tr>" _
    '                    & "<tr bgcolor = '#FFFFFF'>" _
    '                    & "<td style='height: 19px;text-align: left'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>Maggio</span></td>" _
    '                    & "<td style='height: 19px;text-align: right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Maggio, "##,##0.00") & "</span></td>" _
    '                    & "</tr>" _
    '                    & "<tr bgcolor = '#FFFFFF'>" _
    '                    & "<td style='height: 19px;text-align: left'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>Giugno</span></td>" _
    '                    & "<td style='height: 19px;text-align: right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Giugno, "##,##0.00") & "</span></td>" _
    '                    & "</tr>" _
    '                    & "<tr bgcolor = '#FFFFFF'>" _
    '                    & "<td style='height: 19px;text-align: left'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>Luglio</span></td>" _
    '                    & "<td style='height: 19px;text-align: right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Luglio, "##,##0.00") & "</span></td>" _
    '                    & "</tr>" _
    '                    & "<tr bgcolor = '#FFFFFF'>" _
    '                    & "<td style='height: 19px;text-align: left'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>Agosto</span></td>" _
    '                    & "<td style='height: 19px;text-align: right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Agosto, "##,##0.00") & "</span></td>" _
    '                    & "</tr>" _
    '                    & "<tr bgcolor = '#FFFFFF'>" _
    '                    & "<td style='height: 19px;text-align: left'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>Settembre</span></td>" _
    '                    & "<td style='height: 19px;text-align: right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Settembre, "##,##0.00") & "</span></td>" _
    '                    & "</tr>" _
    '                    & "<tr bgcolor = '#FFFFFF'>" _
    '                    & "<td style='height: 19px;text-align: left'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>Ottobre</span></td>" _
    '                    & "<td style='height: 19px;text-align: right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Ottobre, "##,##0.00") & "</span></td>" _
    '                    & "</tr>" _
    '                    & "<tr bgcolor = '#FFFFFF'>" _
    '                    & "<td style='height: 19px;text-align: left'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>Novembre</span></td>" _
    '                    & "<td style='height: 19px;text-align: right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Novembre, "##,##0.00") & "</span></td>" _
    '                    & "</tr>" _
    '                    & "<tr bgcolor = '#FFFFFF'>" _
    '                    & "<td style='height: 19px;text-align: left'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>Dicembre</span></td>" _
    '                    & "<td style='height: 19px;text-align: right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Dicembre, "##,##0.00") & "</span></td>" _
    '                    & "</tr>"

    'End Sub
    'Private Sub TotaliMensili(ByVal month As Integer, ByVal Giorni As Integer, ByVal Importo As Double)
    '    If Giorni > 0 Then
    '        Dim AddMonth As Integer
    '        AddMonth = Giorni / 30
    '        If month + AddMonth <= 13 Then
    '            Select Case NomeMeseDaNumero(month + AddMonth - 1)
    '                Case "Gennaio"
    '                    Gennaio = Gennaio + Importo
    '                Case "Febbraio"
    '                    Febbraio = Febbraio + Importo
    '                Case "Marzo"
    '                    Marzo = Marzo + Importo
    '                Case "Aprile"
    '                    Aprile = Aprile + Importo
    '                Case "Maggio"
    '                    Maggio = Maggio + Importo
    '                Case "Giugno"
    '                    Giugno = Giugno + Importo
    '                Case "Luglio"
    '                    Luglio = Luglio + Importo
    '                Case "Agosto"
    '                    Agosto = Agosto + Importo
    '                Case "Settembre"
    '                    Settembre = Settembre + Importo
    '                Case "Ottobre"
    '                    Ottobre = Ottobre + Importo
    '                Case "Novembre"
    '                    Novembre = Novembre + Importo
    '                Case "Dicembre"
    '                    Dicembre = Dicembre + Importo
    '            End Select
    '        End If

    '    End If
    'End Sub
    Private Function NomeMeseDaNumero(ByVal Numero As Integer) As String
        NomeMeseDaNumero = ""
        If Numero = 1 Then
            NomeMeseDaNumero = "Gennaio"
        ElseIf Numero = 2 Then
            NomeMeseDaNumero = "Febbraio"
        ElseIf Numero = 3 Then
            NomeMeseDaNumero = "Marzo"
        ElseIf Numero = 4 Then
            NomeMeseDaNumero = "Aprile"
        ElseIf Numero = 5 Then
            NomeMeseDaNumero = "Maggio"
        ElseIf Numero = 6 Then
            NomeMeseDaNumero = "Giugno"
        ElseIf Numero = 7 Then
            NomeMeseDaNumero = "Luglio"
        ElseIf Numero = 8 Then
            NomeMeseDaNumero = "Agosto"
        ElseIf Numero = 9 Then
            NomeMeseDaNumero = "Settembre"
        ElseIf Numero = 10 Then
            NomeMeseDaNumero = "Ottobre"
        ElseIf Numero = 11 Then
            NomeMeseDaNumero = "Novembre"
        ElseIf Numero = 12 Then
            NomeMeseDaNumero = "Dicembre"
        End If

        Return NomeMeseDaNumero
    End Function
    Private Function MeseQuery(ByVal month As Integer) As String
        MeseQuery = ""
        If month.ToString.Length <= 1 Then
            MeseQuery = 0 & month
        Else
            MeseQuery = month
        End If
        Return MeseQuery
    End Function
    Private Function OttieniDataEmissione(ByVal anno As Integer, ByVal month As Integer, ByVal T As Integer) As String

        OttieniDataEmissione = ""
        Dim AddMonth As Integer
        Dim i As Integer
        i = 0
        If T = 0 Then
            OttieniDataEmissione = anno & MeseQuery(month) & "01"
        Else
            AddMonth = T / 30
            If month + AddMonth <= 13 Then
                OttieniDataEmissione = anno & MeseQuery(month + AddMonth - 1) & "31"
            Else
                Dim finito As Boolean = False
                Dim M As Integer = (month + AddMonth) - 12
                While finito = False
                    i = i + 1
                    If M <= 11 Then
                        anno = anno + i
                        finito = True
                    Else
                        M = M - 12
                    End If
                End While
                OttieniDataEmissione = anno & MeseQuery(M - 1) & "31"
            End If
        End If
            Return OttieniDataEmissione
    End Function
    Private Function OttieniInizioPeriodo(ByVal anno As Integer, ByVal month As Integer, ByVal T As Integer) As String
        OttieniInizioPeriodo = ""
        If T = 0 Then
            OttieniInizioPeriodo = anno & MeseQuery(month) & "01"
        Else
            Dim dt As New Date(anno, month, 1)
            dt = dt.AddDays(T)
            OttieniInizioPeriodo = dt.Year & MeseQuery(dt.Month) & "01"
        End If

        Return OttieniInizioPeriodo


    End Function
    Private Function OttieniFinePeriodo(ByVal anno As Integer, ByVal month As Integer, ByVal T As Integer) As String
        OttieniFinePeriodo = ""

        If T = 0 Then
            OttieniFinePeriodo = anno & MeseQuery(Month) & "01"
        Else
            Dim dt As New Date(anno, Month, 1)
            dt = dt.AddDays(T)
            OttieniFinePeriodo = dt.Year & MeseQuery(dt.Month) & "31"
        End If

        Return OttieniFinePeriodo
    End Function
    Private Function OttieniDataEmissioneDaMassiva(ByVal DataMassiva As String, ByVal T As Integer) As String
        Dim anno As String = DataMassiva.Substring(0, 4)
        Dim month As String = DataMassiva.Substring(4, 2)
        Dim giorno As String = DataMassiva.Substring(6, 2)
        OttieniDataEmissioneDaMassiva = ""
        Dim AddMonth As Integer
        Dim i As Integer
        i = 0
        If T = 0 Then
            OttieniDataEmissioneDaMassiva = anno & MeseQuery(month) & giorno
        Else
            If giorno > 1 Then
                AddMonth = T / 30
                If month + AddMonth < 13 Then
                    OttieniDataEmissioneDaMassiva = anno & MeseQuery(month + AddMonth) & giorno
                Else
                    Dim finito As Boolean = False
                    Dim M As Integer = (month + AddMonth) - 12
                    While finito = False
                        i = i + 1
                        If M <= 11 Then
                            anno = anno + i
                            finito = True
                        Else
                            M = M - 12
                        End If
                    End While
                    OttieniDataEmissioneDaMassiva = anno & MeseQuery(M) & giorno
                End If

            Else
                AddMonth = T / 30
                If month + AddMonth <= 13 Then
                    OttieniDataEmissioneDaMassiva = anno & MeseQuery(month + AddMonth - 1) & giorno
                Else
                    Dim finito As Boolean = False
                    Dim M As Integer = (month + AddMonth) - 12
                    While finito = False
                        i = i + 1
                        If M <= 11 Then
                            anno = anno + i
                            finito = True
                        Else
                            M = M - 12
                        End If
                    End While
                    OttieniDataEmissioneDaMassiva = anno & MeseQuery(M - 1) & giorno
                End If

            End If

        End If
        Return OttieniDataEmissioneDaMassiva
    End Function
End Class
