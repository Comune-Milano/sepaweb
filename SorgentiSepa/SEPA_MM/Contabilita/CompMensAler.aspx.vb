Imports System.Collections.Generic
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Partial Class Contabilita_CompMensAler
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim BollBimestreMassivo As Double = 0
    Dim Accertato As Double = -1
    Dim IncassoCumulato As Double = 0

    'dichiaro le liste che fanno capo alla classe definita
    Dim lstCompensi As System.Collections.Generic.List(Of CM.CompensiMensili)
    Dim lstCompetenze As System.Collections.Generic.List(Of CM.Competenze)
    Dim testoTabellaRiepilogo As String = ""
    Dim testoTabellaCompetenze As String
    Dim DataBollMassiva As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        lstCompensi = CType(HttpContext.Current.Session.Item("LSTCOMPENSI"), System.Collections.Generic.List(Of CM.CompensiMensili))
        lstCompetenze = CType(HttpContext.Current.Session.Item("LSTCOMPETENZE"), System.Collections.Generic.List(Of CM.Competenze))

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:300px; left:750px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"


        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            lstCompensi.Clear()
            lstCompetenze.Clear()

            If Request.QueryString("ANNO") <> "" Then
                Me.lblTitolo.Text = "RIMBORSO SPESE GESTORE ANNO " & Request.QueryString("ANNO") & " RIPARTIZIONE MENSILE"

                CalcolaCompensi(Request.QueryString("ANNO"), "PADRE")
                CalcolaCompensi(Request.QueryString("ANNO"), "FIGLIO")


                If Request.QueryString("ANNO") > 2009 Then 'perchè noi abbiamo iniziato a bollettare dal 2009, chiamare l'algoritmo per un anno precedente al 2009 sarebbe inutile.
                    CalcolaCompensi(Request.QueryString("ANNO") - 1, "FIGLIO")
                End If

                ScriviTabella()
            End If
        End If
    End Sub
    Private Sub ScriviTabella()
        Dim COLORE As String = "#E6E6E6"
        Dim LineaScritta As Boolean = False
        Dim FiglioTrovato As Boolean = False
        Dim TotRiga As Double = 0

        lstCompetenze.Sort(AddressOf Compare)

        testoTabellaCompetenze = "<table cellpadding='1' cellspacing='2' width='100%'>" _
                                & "<tr bgcolor = '#CCCCFF'>" _
                                & "<td style='height: 15; width : 50%'>" _
                                & "<span style='font-size: 8pt; font-family:Courier New'><strong>BIMESTRE</strong></span></td>" _
                                & "<td style='height: 15px;text-align:center; width : 20%'>" _
                                & "<span style='font-size: 8pt; font-family: Courier New'><strong>ANNO</strong></span></td>" _
                                & "<td style='height: 15px;text-align:right; width : 30%'>" _
                                & "<span style='font-size: 8pt; font-family: Courier New'><strong>IMPORTO</strong></span></td>" _
                                & "</tr>"

        testoTabellaRiepilogo = testoTabellaRiepilogo & "<table cellpadding='1' cellspacing='2' width='100%'>" & vbCrLf _
                                & "<tr bgcolor = '#E6E6E6' >" _
                                & "<td style='height: 19px;text-align: center; width : 30%'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>MESE</strong></span></td>" _
                                & "<td style='height: 19px;text-align: center'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>COMPETENZA MESE</strong></span></td>" _
                                & "<td style='height: 19px;text-align: center'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>COMPETENZE BIMESTRI PRECEDENTI</strong></span></td>" _
                                & "<td style='height: 19px;text-align: center'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE</strong></span></td>" _
                                & "</tr>"

        For Each CompMensili As CM.CompensiMensili In lstCompensi
            LineaScritta = False
            FiglioTrovato = False

            testoTabellaRiepilogo = testoTabellaRiepilogo & "<tr bgcolor = '#FFFFFF' >" _
                                    & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>" & NomeMeseDaNumero(CompMensili.MESE) & "</strong></span></td>" _
                                    & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC;text-align: right'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>€ " & Format(CompMensili.IMPORTO, "##,##0.00") & "</strong></span></td>" _
                                    & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>" & testoTabellaCompetenze & "</strong></span>"
            TotRiga = TotRiga + CompMensili.IMPORTO
            For Each AltreCompetenze As CM.Competenze In lstCompetenze
                If CompMensili.ID = AltreCompetenze.ID_COMPENSI Then
                    testoTabellaRiepilogo = testoTabellaRiepilogo & "<tr bgcolor = '" & COLORE & "'>" _
                            & "<td style='height: 15px'>" _
                            & "<span style='font-size: 8pt; font-family: Courier New'>" & AltreCompetenze.BIMESTRE & "</span></td>" _
                            & "<td style='height: 15px;text-align:center'>" _
                            & "<span style='font-size: 8pt; font-family: Courier New'>" & AltreCompetenze.ANNO & "</span></td>" _
                            & "<td style='height: 15px;text-align:right'>" _
                            & "<span style='font-size: 8pt; font-family: Courier New'>€ " & Format(AltreCompetenze.IMPORTO, "##,##0.00") & "</span></td>"
                    TotRiga = TotRiga + AltreCompetenze.IMPORTO
                    If COLORE = "#FFFFFF" Then
                        COLORE = "#E6E6E6"
                    Else
                        COLORE = "#FFFFFF"
                    End If
                Else
                    If LineaScritta = False And EsisteFiglio(CompMensili.ID) = False Then
                        testoTabellaRiepilogo = testoTabellaRiepilogo & "<tr bgcolor = '" & COLORE & "'>" _
                                & "<td style='height: 15px'>" _
                                & "<span style='font-size: 8pt; font-family: Courier New'>- - - - - -</span></td>" _
                                & "<td style='height: 15px;text-align:center'>" _
                                & "<span style='font-size: 8pt; font-family: Courier New'>- - - -</span></td>" _
                                & "<td style='height: 15px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Courier New'>- - - - - - - -</span></td>"
                        LineaScritta = True
                        If COLORE = "#FFFFFF" Then
                            COLORE = "#E6E6E6"
                        Else
                            COLORE = "#FFFFFF"
                        End If
                    End If

                End If

            Next
            testoTabellaRiepilogo = testoTabellaRiepilogo & "</table></td>"

            testoTabellaRiepilogo = testoTabellaRiepilogo & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC;text-align: right'>" _
        & "<span style='font-size: 8pt; font-family: Arial'><strong>€ " & Format(TotRiga, "##,##0.00") & "</strong></span></td>" _
        & "</tr>"
            TotRiga = 0
        Next



        Me.TBL_COMPENSO_MENSILE.Text = testoTabellaRiepilogo & "</table>"
    End Sub
    Private Sub CalcolaCompensi(ByVal anno As String, ByVal ChiRiempire As String)
        Try
            '******APERTURA CONNESSIONE*****
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            '**************LOOP GENERALE PER TUTTI E 12 I MESI
            Dim Mese As Integer = 1
            For Mese = 1 To 12 Step 2
                If Mese Mod 2 = 1 Then
                    DefinisciBimestre(Mese, anno)
                    If BollBimestreMassivo > 0 Then
                        Rendicontazione(Mese, anno, ChiRiempire)
                        'MesiTblRiepilogo(Mese)
                    Else

                    End If
                End If
            Next
            'TBL_COMPENSO_BIMESTRALE.Text = testoTabella & "</table>"
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
    Private Function EsisteFiglio(ByVal idPadre As Integer) As Boolean
        EsisteFiglio = False
        For Each AltreCompetenze As CM.Competenze In lstCompetenze
            If idPadre = AltreCompetenze.ID_COMPENSI Then
                EsisteFiglio = True
            End If
        Next
        Return EsisteFiglio
    End Function
    Private Sub DefinisciBimestre(ByVal Month As Integer, ByVal anno As String)
        Try

            If Month Mod 2 = 1 Then
                '**************TOTALE BOLLETTATO MASSIVO BIMESTRE IN CONSIDERAZIONE DI COMPETENZA GESTORE***************
                'questa ci mette 1sec
                'par.cmd.CommandText = "SELECT (SUM(BOL_BOLLETTE_VOCI.IMPORTO)) AS BOLLETTATOMASSIVO FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE.RIF_FILE<>'MOD' AND BOL_BOLLETTE.RIF_FILE<>'MAV'AND BOL_BOLLETTE.RIF_FILE<>'MOR'AND BOL_BOLLETTE.RIF_FILE<>'REC'AND RIFERIMENTO_DA>= " & anno & MeseQuery(Month) & "01 AND RIFERIMENTO_A<= " & anno & MeseQuery(Month + 1) & "31 AND T_VOCI_BOLLETTA.COMPETENZA=2 AND fl_annullata = 0 "
                'CAMBIO CONDIZIONE CON (SUBSTR(RIF_FILE,1,3)='BO_' PER PRENDERE SOLO TUTTE LE BOLLETTE MASSIVE
                'questa ci mette 12sec
                par.cmd.CommandText = "SELECT DISTINCT(bol_bollette.data_emissione) AS Data_bol_massiva, N_rata FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE.RIF_FILE LIKE'BO_%' AND anno = " & anno & " AND fl_annullata = 0 AND N_RATA>=" & Month & " AND N_RATA<=" & Month + 1 & " ORDER BY N_RATA ASC"
                Dim ReaderDataMassiva As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If ReaderDataMassiva.Read Then
                    DataBollMassiva = ReaderDataMassiva("DATA_BOL_MASSIVA")
                End If
                ReaderDataMassiva.Close()

                par.cmd.CommandText = "SELECT (SUM(BOL_BOLLETTE_VOCI.IMPORTO)) AS BOLLETTATOMASSIVO FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE.RIF_FILE like'BO_%' AND RIFERIMENTO_DA>= " & anno & MeseQuery(Month) & "01 AND RIFERIMENTO_A<= " & anno & MeseQuery(Month + 1) & "31 AND T_VOCI_BOLLETTA.COMPETENZA=2 AND fl_annullata = 0 "

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                BollBimestreMassivo = 0
                If myReader.Read Then
                    BollBimestreMassivo = par.IfNull(myReader("BOLLETTATOMASSIVO"), 0)
                End If
                myReader.Close()
                '**************SCRIVO NELLA LBL CHE COMPONE LA TABELLA, IL BIMESTRE IL BOLLETTATO
            Else
                Exit Sub
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
    Private Sub Rendicontazione(ByVal Month As Integer, ByVal anno As String, ByVal ChiRiempire As String)
        Try

            Dim T As Integer = 0
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


                '*******************NUOVI CONTRATTI CUMULATI**************
                par.cmd.CommandText = "SELECT (SUM(BOL_BOLLETTE_VOCI.IMPORTO)) AS NUOVICONTRATTI FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND (BOL_BOLLETTE.RIF_FILE='MOD' OR BOL_BOLLETTE.RIF_FILE='MAV'OR BOL_BOLLETTE.RIF_FILE='REC') AND RIFERIMENTO_DA>= " & anno & MeseQuery(Month) & "01 AND RIFERIMENTO_A<= " & anno & MeseQuery(Month + 1) & "31 AND DATA_EMISSIONE <=" & OttieniDataEmissioneDaMassiva(DataBollMassiva, T) & " AND T_VOCI_BOLLETTA.COMPETENZA=2 AND fl_annullata = 0 "
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    NuoviContratti = par.IfNull(myReader("NUOVICONTRATTI"), 0)
                End If

                SommaNuoviTmenoUno = NuoviContratti - SommaNuoviTmenoUno
                'testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                '                        & "<span style='font-size: 8pt; font-family: Arial'>" & Format(NuoviContratti, "##,##0.00") & "</span></td>"


                '********************ANNULLAMENTI CUMULATI**************
                par.cmd.CommandText = "SELECT (SUM(BOL_BOLLETTE_VOCI.IMPORTO)) AS ANNULLAMENTI FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND RIF_FILE NOT LIKE 'BO_%' AND RIFERIMENTO_DA>= '" & anno & MeseQuery(Month) & "01' AND RIFERIMENTO_A<= " & anno & MeseQuery(Month + 1) & "31 AND DATA_ANNULLO>= " & anno & MeseQuery(Month) & "01 AND DATA_ANNULLO<= " & OttieniDataEmissioneDaMassiva(DataBollMassiva, T) & "   AND T_VOCI_BOLLETTA.COMPETENZA=2 AND fl_annullata = 1 AND (DATA_PAGAMENTO IS NULL OR DATA_PAGAMENTO>" & OttieniDataEmissioneDaMassiva(DataBollMassiva, T) & ")"
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    Annullamenti = par.IfNull(myReader("ANNULLAMENTI"), 0)
                End If
                AnnullamentiOgniT = Annullamenti - AnnullamentiOgniT

                '********************MESSE IN MORA CUMULATI(annullati)*************
                par.cmd.CommandText = "SELECT (SUM(BOL_BOLLETTE_VOCI.IMPORTO)) AS MORA FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND RIFERIMENTO_DA>= " & anno & MeseQuery(Month) & "01 AND RIFERIMENTO_A<= " & anno & MeseQuery(Month + 1) & "31 AND DATA_MORA >= '" & anno & MeseQuery(Month) & "01' AND DATA_MORA <=" & OttieniDataEmissioneDaMassiva(DataBollMassiva, T) & " AND T_VOCI_BOLLETTA.COMPETENZA=2"
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    MORA = par.IfNull(myReader("MORA"), 0)
                End If
                DiffMoreTmenoUno = MORA - DiffMoreTmenoUno

                '********************ACCERTATO*************
                Accertato = BollBimestreMassivo + NuoviContratti - Annullamenti

                '********************BOLLETTATO PER PROCEDURA*************
                BollettatoProcedura = BollBimestreMassivo + NuoviContratti - Annullamenti - MORA
                'testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                '                        & "<span style='font-size: 8pt; font-family: Arial'>" & Format(BollettatoProcedura, "##,##0.00") & "</span></td>"


                ''********************INCASSO CUMULATO*********************
                par.cmd.CommandText = "SELECT (SUM (BOL_BOLLETTE_VOCI.IMPORTO)) AS INCASSATO FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE.DATA_PAGAMENTO IS NOT NULL AND BOL_BOLLETTE.RIF_FILE<>'MOR'AND T_VOCI_BOLLETTA.COMPETENZA=2 AND fl_annullata = 0 AND RIFERIMENTO_DA>= '" & anno & MeseQuery(Month) & "01' AND RIFERIMENTO_A<= '" & anno & MeseQuery(Month + 1) & "31' AND DATA_PAGAMENTO>='" & anno & MeseQuery(Month) & "' AND DATA_PAGAMENTO <='" & OttieniDataEmissioneDaMassiva(DataBollMassiva, T) & "'"
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    IncassoCumulato = par.IfNull(myReader("INCASSATO"), 0)
                    IncassiCumulatiTDiff = IncassoCumulato - IncassiCumulatiTDiff
                End If

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


                '*******************SOGLIA 80% ******************************
                If BollettatoProcedura > 0 Then
                    Soglia80 = BollettatoProcedura * 0.80000000000000004
                Else
                    Soglia80 = 0
                End If

                '*****************RIPIANATO AL GESTORE*************************
                If IncassoCumulato < Soglia80 Then
                    RipianatoALER = Math.Min(AlgoritmiCumulati, Soglia80)
                Else
                    RipianatoALER = IncassoCumulato
                End If

                RipianatoAlerOgniT = RipianatoALER - RipianatoAlerOgniT

                '*****************RIPIANATO AL GESTORE OGNI 30GG***************

                RipianatoAlerOgniT = RipianatoAlerOgniT
                '*****************RIPIANATO PER MESSA IN MORA***************
                RipianatoAlerMORA = 0.5 * DiffMoreTmenoUno

                '*****************AGGIUSTAMENTO*****************************
                If RipianatoAlerOgniT < 0 Then
                    Aggiustamento = (-RipianatoAlerOgniT - (0.80000000000000004 * (AnnullamentiOgniT - SommaNuoviTmenoUno)))
                Else
                    Aggiustamento = 0
                End If
                '*****************T O T A L E GESTORE**************************
                TOTALERogniT = RipianatoAlerOgniT + RipianatoAlerMORA + Aggiustamento



                '********************CHIAMATA RIEMPIMENTO CLASSI PADRE-FIGLIO , E FIGLIO DI ANNO -1
                If anno = Request.QueryString("ANNO") Then
                    If ChiRiempire = "PADRE" Then
                        'TotaliMensili(Month, T, RipianatoAlerOgniT)
                        TotaliMensiliDaMassiva(Month, DataBollMassiva, T, TOTALERogniT)
                    Else
                        'RiempiCompetenze(anno, Month, T, RipianatoAlerOgniT)
                        If Month = 5 And T = 60 Then
                            Beep()
                        End If
                        RiempiCompetenzeDaMassiva(Month, DataBollMassiva, T, TOTALERogniT)
                    End If
                ElseIf anno = Request.QueryString("ANNO") - 1 Then
                    'RiempiCompetenze(anno, Month, T, RipianatoAlerOgniT)
                    RiempiCompetenzeDaMassiva(Month, DataBollMassiva, T, TOTALERogniT)

                End If

                '********************FINE CHIAMATA




                '*****************RIPIANATO PER MESSA IN MORA***************
                RipianatoAlerMORA = 0.5 * DiffMoreTmenoUno

                '*****************AGGIUSTAMENTO*****************************

                If RipianatoAlerOgniT < 0 Then
                    Aggiustamento = (-RipianatoAlerOgniT - (0.80000000000000004 * (AnnullamentiOgniT - SommaNuoviTmenoUno)))
                Else
                    Aggiustamento = 0
                End If
                'testoTabella = testoTabella & "<td style='height: 19px;text-align: right'>" _
                '                            & "<span style='font-size: 8pt; font-family: Arial'>" & Format(Aggiustamento, "##,##0.00") & "</span></td>"



                '*****************T O T A L E GESTORE**************************
                TOTALERogniT = RipianatoAlerOgniT + RipianatoAlerMORA + Aggiustamento
                '****************DA DARE AL GESTORE IN FASE DI RENDICONTAZIONE PER MORA




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


            Dim a As String
            a = "USCITO"
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub TotaliMensili(ByVal month As Integer, ByVal Giorni As Integer, ByVal Importo As Double)
        Dim compensoMese As CM.CompensiMensili

        If Giorni > 0 And Giorni <= 60 Then
            Dim AddMonth As Integer
            AddMonth = Giorni / 30
            If month + AddMonth <= 13 Then
                Select Case NomeMeseDaNumero(month + AddMonth - 1)
                    Case "Gennaio"
                        compensoMese = New CM.CompensiMensili(lstCompensi.Count, Request.QueryString("ANNO"), "01", Importo)
                        lstCompensi.Add(compensoMese)
                        compensoMese = Nothing
                    Case "Febbraio"
                        compensoMese = New CM.CompensiMensili(lstCompensi.Count, Request.QueryString("ANNO"), "02", Importo)
                        lstCompensi.Add(compensoMese)
                        compensoMese = Nothing
                    Case "Marzo"
                        compensoMese = New CM.CompensiMensili(lstCompensi.Count, Request.QueryString("ANNO"), "03", Importo)
                        lstCompensi.Add(compensoMese)
                        compensoMese = Nothing
                    Case "Aprile"
                        compensoMese = New CM.CompensiMensili(lstCompensi.Count, Request.QueryString("ANNO"), "04", Importo)
                        lstCompensi.Add(compensoMese)
                        compensoMese = Nothing
                    Case "Maggio"
                        compensoMese = New CM.CompensiMensili(lstCompensi.Count, Request.QueryString("ANNO"), "05", Importo)
                        lstCompensi.Add(compensoMese)
                        compensoMese = Nothing
                    Case "Giugno"
                        compensoMese = New CM.CompensiMensili(lstCompensi.Count, Request.QueryString("ANNO"), "06", Importo)
                        lstCompensi.Add(compensoMese)
                        compensoMese = Nothing
                    Case "Luglio"
                        compensoMese = New CM.CompensiMensili(lstCompensi.Count, Request.QueryString("ANNO"), "07", Importo)
                        lstCompensi.Add(compensoMese)
                        compensoMese = Nothing
                    Case "Agosto"
                        compensoMese = New CM.CompensiMensili(lstCompensi.Count, Request.QueryString("ANNO"), "08", Importo)
                        lstCompensi.Add(compensoMese)
                        compensoMese = Nothing
                    Case "Settembre"
                        compensoMese = New CM.CompensiMensili(lstCompensi.Count, Request.QueryString("ANNO"), "09", Importo)
                        lstCompensi.Add(compensoMese)
                        compensoMese = Nothing
                    Case "Ottobre"
                        compensoMese = New CM.CompensiMensili(lstCompensi.Count, Request.QueryString("ANNO"), "10", Importo)
                        lstCompensi.Add(compensoMese)
                        compensoMese = Nothing
                    Case "Novembre"
                        compensoMese = New CM.CompensiMensili(lstCompensi.Count, Request.QueryString("ANNO"), "11", Importo)
                        lstCompensi.Add(compensoMese)
                        compensoMese = Nothing
                    Case "Dicembre"
                        compensoMese = New CM.CompensiMensili(lstCompensi.Count, Request.QueryString("ANNO"), "12", Importo)
                        lstCompensi.Add(compensoMese)
                        compensoMese = Nothing
                End Select

            End If

        End If
    End Sub
    Private Sub RiempiCompetenze(ByVal anno As Integer, ByVal month As Integer, ByVal Giorni As Integer, ByVal Importo As Double)
        Dim competenze As CM.Competenze

        If Giorni > 60 Then
            Dim AddMonth As Integer
            AddMonth = Giorni / 30
            If month + AddMonth <= 13 Then
                For Each gen As CM.CompensiMensili In lstCompensi
                    If gen.MESE = month + AddMonth - 1 Then
                        competenze = New CM.Competenze(lstCompetenze.Count, gen.ID, anno, gen.MESE, BimestreDaMese(month), Importo)
                        lstCompetenze.Add(competenze)
                        competenze = Nothing
                    End If
                Next
            Else
                Dim i As Integer
                i = 0
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
                For Each gen As CM.CompensiMensili In lstCompensi
                    If gen.MESE = M - 1 Then
                        competenze = New CM.Competenze(lstCompetenze.Count, gen.ID, anno - 1, gen.MESE, BimestreDaMese(month), Importo)
                        lstCompetenze.Add(competenze)
                        competenze = Nothing
                    End If
                Next
            End If
        End If
    End Sub
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
    Private Function BimestreDaMese(ByVal mese As Integer) As String
        BimestreDaMese = ""
        If mese >= 1 And mese <= 2 Then
            BimestreDaMese = "Gennaio-Febbraio"
        ElseIf mese >= 3 And mese <= 4 Then
            BimestreDaMese = "Marzo-Aprile"

        ElseIf mese >= 5 And mese <= 6 Then
            BimestreDaMese = "Maggio-Giugno"

        ElseIf mese >= 7 And mese <= 8 Then
            BimestreDaMese = "Luglio-Agosto"

        ElseIf mese >= 9 And mese <= 10 Then
            BimestreDaMese = "Settembre-Ottobre"

        ElseIf mese >= 11 And mese <= 12 Then
            BimestreDaMese = "Novebre-Dicembre"
        End If

        Return BimestreDaMese
    End Function

    Private Shared Function Compare(ByVal x As CM.Competenze, ByVal y As CM.Competenze) As Integer


        Dim mesi() As String = {"Gennaio-Febbraio", "Marzo-Aprile", "Maggio-Giugno", "Luglio-Agosto", "Settembre-Ottobre", "Novebre-Dicembre"}
        Dim lista As List(Of String) = New List(Of String)(mesi)

        Dim bimestre1 As Integer
        Dim bimestre2 As Integer
        bimestre1 = lista.IndexOf("Maggio-Giugno")



        bimestre1 = lista.IndexOf(x.BIMESTRE)
        bimestre2 = lista.IndexOf(y.BIMESTRE)



        Return (String.Compare(x.ANNO & bimestre1.ToString(), y.ANNO & bimestre2.ToString()))

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
    Private Sub TotaliMensiliDaMassiva(ByVal month As Integer, ByVal DataMassiva As String, ByVal Giorni As Integer, ByVal Importo As Double)
        Dim compensoMese As CM.CompensiMensili
        'Dim anno As String = DataMassiva.Substring(0, 4)
        'Dim month As String = DataMassiva.Substring(4, 2)
        'Dim giorno As String = DataMassiva.Substring(6, 2)
        Dim Scrivi As Boolean = False
        Dim NuovoMese As Integer
        Dim AddMonth As Integer
        AddMonth = Giorni / 30
        If DataBollMassiva.Substring(4, 2) >= month Then
            If Giorni = 0 Then
                NuovoMese = month
            Else
                NuovoMese = month + AddMonth
            End If

            If NuovoMese <> CInt(DataMassiva.Substring(4, 2)) + AddMonth Then
                Importo = 0
            End If
            Scrivi = True
        Else
            If Giorni > 0 Then
                If CInt(DataMassiva.Substring(4, 2)) + AddMonth = month Then
                    NuovoMese = month
                Else
                    NuovoMese = month + AddMonth
                End If
                Scrivi = True
            End If

        End If
        If Scrivi = True Then
            If NuovoMese <= month + 1 Then
                If NuovoMese < 13 Then
                    Dim MeseDaScrivere As String = ""
                    If NuovoMese < 10 Then
                        MeseDaScrivere = 0 & NuovoMese
                    Else
                        MeseDaScrivere = NuovoMese
                    End If
                    compensoMese = New CM.CompensiMensili(lstCompensi.Count, Request.QueryString("ANNO"), MeseDaScrivere, Importo)
                    lstCompensi.Add(compensoMese)
                    compensoMese = Nothing

                End If
            End If
        End If


    End Sub
    Private Sub RiempiCompetenzeDaMassiva(ByVal MeseBim As Integer, ByVal DataMassiva As String, ByVal Giorni As Integer, ByVal Importo As Double)
        Dim competenze As CM.Competenze
        Dim annoBol As String = DataMassiva.Substring(0, 4)
        Dim meseBol As String = DataMassiva.Substring(4, 2)
        Dim giornoBol As String = DataMassiva.Substring(6, 2)

        'If Giorni > 60 Then
        Dim AddMonth As Integer
        AddMonth = Giorni / 30
        If CInt(meseBol) + AddMonth < 13 Then
            For Each father As CM.CompensiMensili In lstCompensi
                If meseBol >= MeseBim Then

                    If ((father.MESE = CInt(meseBol) + AddMonth) And father.MESE > MeseBim + 1) Then
                        competenze = New CM.Competenze(lstCompetenze.Count, father.ID, annoBol, father.MESE, BimestreDaMese(MeseBim), Importo)
                        lstCompetenze.Add(competenze)
                        competenze = Nothing
                    End If

                Else
                    If (father.MESE = CInt(meseBol) + AddMonth) And (MeseBim <> CInt(meseBol) + AddMonth) Then
                        competenze = New CM.Competenze(lstCompetenze.Count, father.ID, annoBol, father.MESE, BimestreDaMese(MeseBim), Importo)
                        lstCompetenze.Add(competenze)
                        competenze = Nothing

                    End If
                End If

            Next
        Else
            Dim i As Integer
            i = 0
            Dim finito As Boolean = False
            Dim M As Integer = (CInt(meseBol) + AddMonth) - 12
            While finito = False
                i = i + 1
                If M <= 11 Then
                    annoBol = annoBol + i
                    finito = True
                Else
                    M = M - 12
                End If
            End While
            For Each gen As CM.CompensiMensili In lstCompensi
                If gen.MESE = M Then
                    competenze = New CM.Competenze(lstCompetenze.Count, gen.ID, annoBol - 1, gen.MESE, BimestreDaMese(MeseBim), Importo)
                    lstCompetenze.Add(competenze)
                    competenze = Nothing
                End If
            Next
        End If


        'End If
    End Sub

    Protected Sub ImgPDF_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgPDF.Click
        Try
            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 10
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = ""
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False

            Dim nomefile As String = "Export_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            Dim Titolo As String = "<span style='font-family:Arial;font-size:12pt;font-weight:bold;'>RIMBORSO SPESE GESTORE ANNO " & Request.QueryString("ANNO") & " RIPARTIZIONE MENSILE</span>"
            pdfConverter1.SavePdfFromHtmlStringToFile(Titolo & TBL_COMPENSO_MENSILE.Text, url & nomefile)

            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','CompMensile','');</script>")

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class
