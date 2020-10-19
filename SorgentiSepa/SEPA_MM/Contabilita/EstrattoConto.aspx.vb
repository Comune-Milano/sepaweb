'**********************************************************************22/08/2011**********************************************************************************************
'****************CODICE AGGIORNATO PER NON FAR RIENTRARE NEL DEBITO/CREDITO DELL'INQUILINO LE BOLLETTE DERIVANTI DALLE RATEIZZAZIONI*******************************************
'************************************************per visualizzare le modifiche cerca BOL_BOLLETTE.ID_TIPO <> 5*****************************************************************************
Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class Contabilita_EstrattoConto
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            If Request.QueryString("IDANA") <> "" And Request.QueryString("IDCONT") <> "" Then
                CaricaSaldoContabile()
                CaricaImportiPeriodo()
            Else
                Response.Write("<script>alert('ERRORE RECUPERO INFORMAZIONI')</script>")

            End If


        End If


    End Sub
    Private Sub CaricaSaldoContabile()

        Dim DataDal As String
        Dim DataAl As String = ""
        Dim sStringaSQL As String = ""
        Dim DOVUTO As Decimal = 0
        Dim PAGATO As Decimal = 0

        '******APERTURA CONNESSIONE*****
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        Try

            Dim CondCompetenza As String = ""
            If Not String.IsNullOrEmpty(Request.QueryString("RIFDAL")) Then
                CondCompetenza = "AND BOL_BOLLETTE.RIFERIMENTO_DA >= " & Request.QueryString("RIFDAL") & ""
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("RIFAL")) Then
                CondCompetenza = CondCompetenza & "AND BOL_BOLLETTE.RIFERIMENTO_A <= " & Request.QueryString("RIFAL") & ""
            End If
            'Setto date periodo
            DataDal = Request.QueryString("DAL")
            DataAl = par.IfEmpty(Request.QueryString("AL"), Format(Date.Now, "yyyyMMdd"))

            Me.lblInformazione.Text = "* = Bollette pagate successivamente al " & par.FormattaData(DataAl)
            Me.lblInformazione.Text = lblInformazione.Text & "<br/>Le bollette elencate hanno data emissione compresa nelle date dell'estratto conto."
            Dim sValore As String
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader

            '***********DOVUTO RELATIVO ALLA PRIMA DATA*****************
            If (par.IfEmpty(DataDal, "Null") <> "Null") Then
                sValore = DataDal
                sStringaSQL = sStringaSQL & " AND BOL_BOLLETTE.DATA_EMISSIONE <= " & sValore

                ' '' '' ''sStringaSQL = "SELECT SUM(NVL(IMPORTO_TOTALE,0)-NVL(IMPORTO_RIC,0)-NVL(BOLLETTATO_QUOTA_SIND,0)) AS IMPORTO " _
                ' '' '' ''                    & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_FLUSSI " _
                ' '' '' ''                    & "WHERE FL_ANNULLATA = 0 " _
                ' '' '' ''                    & "AND BOL_FLUSSI.ID_BOLLETTA(+) = BOL_BOLLETTE.ID AND BOL_BOLLETTE.ID_TIPO <> 5 " _
                ' '' '' ''                    & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL
                sStringaSQL = "SELECT SUM(NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_RIC_B,0) - NVL(QUOTA_SIND_B,0)) AS IMPORTO, " _
                            & "SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)) AS PAGATO " _
                            & "FROM SISCOM_MI.BOL_BOLLETTE " _
                            & "WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) " _
                            & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL & " " & CondCompetenza
                par.cmd.CommandText = sStringaSQL
                myReader = par.cmd.ExecuteReader()
                'TOTALE DEL DEBITO A CARICO DI UN UTENTE
                If myReader.Read Then
                    DOVUTO = par.IfNull(myReader("IMPORTO"), 0)
                    PAGATO = par.IfNull(myReader("PAGATO"), 0)
                End If
                myReader.Close()
                sStringaSQL = ""

                '' '' ''***********PAGATO RELATIVO ALLA PRIMA DATA*****************
                ' '' '' ''If (par.IfEmpty(DataDal, "Null") <> "Null") Then
                ' '' '' ''    sValore = DataDal
                ' '' '' ''    sStringaSQL = sStringaSQL & " AND BOL_BOLLETTE.DATA_EMISSIONE <= " & sValore
                ' '' '' ''End If
                ' '' '' '' '' ''sStringaSQL = "SELECT SUM(NVL(IMPORTO_PAGATO,0)-NVL(IMPORTO_RIC,0)-NVL(BOLLETTATO_QUOTA_SIND,0)) AS PAGATO " _
                ' '' '' '' '' ''                    & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_FLUSSI " _
                ' '' '' '' '' ''                    & "WHERE FL_ANNULLATA = 0 " _
                ' '' '' '' '' ''                    & "AND BOL_FLUSSI.ID_BOLLETTA(+) = BOL_BOLLETTE.ID AND BOL_BOLLETTE.ID_TIPO <> 5 " _
                ' '' '' '' '' ''                    & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL

                ' '' ''sStringaSQL = "SELECT SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)) AS PAGATO" _
                ' '' ''            & "FROM SISCOM_MI.BOL_BOLLETTE " _
                ' '' ''            & "WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL ))" _
                ' '' ''            & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL

                '' ''par.cmd.CommandText = sStringaSQL
                '' ''myReader = par.cmd.ExecuteReader()
                '' ''If myReader.Read Then
                '' ''    PAGATO = par.IfNull(myReader("PAGATO"), 0)
                '' ''End If
                '' ''myReader.Close()


                Me.LblSaldoInizio.Text = "<span class='ttip' onmouseover=if(t1)t1.Show(event,sinizio) onmouseout=if(t1)t1.Hide(event)>Saldo contabile di quanto dovuto all'inizio del periodo pari a: &euro;." & Format(DOVUTO - PAGATO, "##,##0.00") & "</span>"
                Me.txtsldinizio.Value = "Saldo contabile di quanto dovuto all'inizio del periodo pari a: €." & Format(DOVUTO - PAGATO, "##,##0.00")
            Else
                LblSaldoInizio.Text = ""
                Me.txtsldinizio.Value = ""
            End If


            '*********PULISCO STINGA SQL PER SECONDO ESTRATTO*******
            sStringaSQL = ""
            par.cmd.CommandText = ""
            DOVUTO = 0
            PAGATO = 0

            '***********DOVUTO RELATIVO ALLA SECONDA DATA*****************

            If (par.IfEmpty(DataAl, "Null") <> "Null") Then
                sValore = DataAl
                sStringaSQL = sStringaSQL & " AND BOL_BOLLETTE.DATA_EMISSIONE <= " & sValore
            End If
            sStringaSQL = "SELECT SUM(NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_RIC_B,0) - NVL(QUOTA_SIND_B,0)) AS IMPORTO, " _
                        & "SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)) AS PAGATO " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE " _
                        & "WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) " _
                        & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL & " " & CondCompetenza
            par.cmd.CommandText = sStringaSQL
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                DOVUTO = par.IfNull(myReader("IMPORTO"), 0)
                PAGATO = par.IfNull(myReader("PAGATO"), 0)
            End If
            myReader.Close()
            sStringaSQL = ""

            ''***********PAGATO RELATIVO ALLA SECONDA DATA*****************

            'If (par.IfEmpty(DataAl, "Null") <> "Null") Then
            '    sValore = DataAl
            '    sStringaSQL = sStringaSQL & " AND BOL_BOLLETTE.DATA_PAGAMENTO <= " & sValore
            'End If
            'sStringaSQL = "SELECT SUM(NVL(IMPORTO_PAGATO,0)-NVL(IMPORTO_RIC,0)-NVL(BOLLETTATO_QUOTA_SIND,0)) AS PAGATO " _
            '                    & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_FLUSSI " _
            '                    & "WHERE FL_ANNULLATA = 0 " _
            '                    & "AND BOL_FLUSSI.ID_BOLLETTA(+) = BOL_BOLLETTE.ID AND BOL_BOLLETTE.ID_TIPO <> 5 " _
            '                    & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL
            'par.cmd.CommandText = sStringaSQL
            'myReader = par.cmd.ExecuteReader()

            'If myReader.Read Then
            Me.LblSaldoFine.Text = "<span class='ttip' onmouseover=if(t1)t1.Show(event,sfine) onmouseout=if(t1)t1.Hide(event)>Saldo contabile di quanto dovuto alla fine del periodo pari a: &euro;." & Format(DOVUTO - PAGATO, "##,##0.00") & "</span>"
            Me.txtsldfine.Value = "Saldo contabile di quanto dovuto alla fine del periodo pari a: €." & Format(DOVUTO - PAGATO, "##,##0.00")

            'End If
            'myReader.Close()

            '*********PULISCO STINGA SQL*******
            sStringaSQL = ""
            par.cmd.CommandText = ""
            DOVUTO = 0

            ''ULTIMO AGGIORNAMENTO DELLE INFORMAZIONI
            Dim UltimoPagam As String = 0
            UltimoPagam = par.FormattaData(Format(Now, "yyyyMMdd")) 'par.IfNull(myReaderTEMP("ULTIMA_BOLL"), 0)
            par.cmd.CommandText = ""
            par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" FROM SISCOM_MI.ANAGRAFICA WHERE ID = " & Request.QueryString("IDANA")
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.LblTitolo.Text = " - " & par.IfNull(myReader("INTESTATARIO"), "") & " DATA STAMPA: " & UltimoPagam

            End If
            myReader.Close()

            Me.lblSottoTitolo.Text = "Saldo Contabile per data EMISSIONE"
            If Not String.IsNullOrEmpty(DataDal) Then
                lblSottoTitolo.Text = lblSottoTitolo.Text & " dal " & par.FormattaData(DataDal)
            End If
            If Not String.IsNullOrEmpty(DataAl) Then
                lblSottoTitolo.Text = lblSottoTitolo.Text & " al " & par.FormattaData(DataAl)
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("RIFDAL")) Or Not String.IsNullOrEmpty(Request.QueryString("RIFAL")) Then
                lblSottoTitolo.Text = lblSottoTitolo.Text & " per COMPETENZA "
                If Not String.IsNullOrEmpty(Request.QueryString("RIFDAL")) Then
                    lblSottoTitolo.Text = lblSottoTitolo.Text & " dal " & par.FormattaData(Request.QueryString("RIFDAL"))
                End If
                If Not String.IsNullOrEmpty(Request.QueryString("RIFAL")) Then
                    lblSottoTitolo.Text = lblSottoTitolo.Text & " al " & par.FormattaData(Request.QueryString("RIFAL"))
                End If
            End If


            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

        End Try


    End Sub
    Private Sub CaricaImportiPeriodo()

        Dim testoTabella As String = ""
        Dim Nbolletta As String = ""
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader
        Dim COLORE As String = "#E6E6E6"
        Dim Contatore As Integer = 0
        Dim DataDal As String
        Dim DataAl As String = ""
        Dim GiorniRitardo As String = ""
        '*********VARIABILI APPOGGIO IMPORTI
        Dim Importo As Decimal = 0
        Dim Riclassificate As Decimal = 0
        Dim ImpContabile As Decimal = 0
        Dim ImpIncassato As Decimal = 0
        Dim ImpSaldo As Decimal = 0
        '*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/

        '*********VARIABILI PER I TOTALI
        Dim TotImporto As Decimal = 0
        Dim TotRiclassificate As Decimal = 0
        Dim TotImpContabile As Decimal = 0
        Dim TotImpIncassato As Decimal = 0
        Dim TotImpSaldoContabile As Decimal = 0
        '*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/


        Dim CondBollAnnullate As String = "(BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL ))"

        '******APERTURA CONNESSIONE*****
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        Try
            '**************Setto date periodo
            DataDal = Request.QueryString("DAL")
            DataAl = par.IfEmpty(Request.QueryString("AL"), Format(Date.Now, "yyyyMMdd"))

            '**************COLONNE DELLA TABELLA
            testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                            & "<tr>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span class='ttip' onmouseover=if(t1)t1.Show(event,numBol) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>NUM.BOL.</strong></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span class='ttip' onmouseover=if(t1)t1.Show(event,numRata) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>NUM.RATA</strong></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span class='ttip' onmouseover=if(t1)t1.Show(event,tipoR) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>TIPO</strong></span></td>" _
                            & "<td style='height: 19px;text-align:center'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,riferimento) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>RIFERIMENTO</strong></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,emissione) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,scadenza) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
                            & "<td style='height: 19px;text-align:right'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,bollettato) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>BOLLETTATO</strong></span></td>" _
                            & "<td style='height: 19px;text-align:right'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,riclass) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>IMP.RICLASSIFICATO</strong></span></td>" _
                            & "<td style='height: 19px;text-align:right'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,impCont) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>IMPORTO CONTABILE</strong></span></td>" _
                            & "<td style='height: 19px;text-align:center'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,dataPag) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>DATA PAGAMENTO</strong></span></td>" _
                            & "<td style='height: 19px;text-align:right'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,impIncassato) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>IMPORTO INCASSATO</strong></span></td>" _
                            & "<td style='height: 19px;text-align:right'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,saldoContab) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>SALDO CONTABILE</strong></span></td>" _
                            & "<td style='height: 19px;text-align:center'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,gRitardo) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>GIORNI RITARDO</strong></span></td>" _
                            & "</tr>"

            '**************SELEZIONO LE INFORMAZIONI RICHIESTE PER LA VISUALIZZAZIONE DEI DATI

            Dim condEmissione As String = ""
            Dim CondRiferimento As String = ""

            If Not String.IsNullOrEmpty(DataDal) Then
                condEmissione = " AND BOL_BOLLETTE.DATA_EMISSIONE >= '" & DataDal & "' "
            End If
            If Not String.IsNullOrEmpty(DataAl) Then
                condEmissione = condEmissione & " AND BOL_BOLLETTE.DATA_EMISSIONE <= '" & DataAl & "'"
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("RIFDAL")) Then
                CondRiferimento = "AND BOL_BOLLETTE.RIFERIMENTO_DA >= '" & Request.QueryString("RIFDAL") & "' "
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("RIFAL")) Then
                CondRiferimento = CondRiferimento & "AND BOL_BOLLETTE.RIFERIMENTO_A <= '" & Request.QueryString("RIFAL") & "'"
            End If

            par.cmd.CommandText = "SELECT BOL_BOLLETTE.*, " _
                                & "(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento, " _
                                & "(CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO " _
                                & "FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.TIPO_BOLLETTE WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) AND " _
                                & "TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO AND ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & condEmissione & " " & CondRiferimento & " " _
                                & " ORDER BY BOL_BOLLETTE.data_emissione DESC,bol_bollette.id desc"


            'par.cmd.CommandText = "SELECT NUM_BOLLETTA, BOL_BOLLETTE.ID,N_RATA,DATA_EMISSIONE, DATA_SCADENZA, DATA_PAGAMENTO, IMPORTO_PAGATO,ANNO,FL_ANNULLATA, (CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO,ID_TIPO, ID_BOLLETTA_RIC " _
            '                    & ",(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento,BOL_BOLLETTE.IMPORTO_TOTALE " _
            '                    & "FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.TIPO_BOLLETTE WHERE " & CondBollAnnullate & " AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO " _
            '                    & " AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " AND BOL_BOLLETTE.DATA_EMISSIONE >= " & DataDal & " AND BOL_BOLLETTE.DATA_EMISSIONE <= " & DataAl & " AND BOL_BOLLETTE.ID_TIPO <> 5  ORDER BY BOL_BOLLETTE.ID DESC  "
            myReader = par.cmd.ExecuteReader()
            Do While myReader.Read

                '********************SEZIONE AZZERA VARIABILI************************
                Nbolletta = ""

                Importo = 0
                Riclassificate = 0
                ImpContabile = 0
                ImpIncassato = 0
                ImpSaldo = 0
                GiorniRitardo = ""

                '*/*/*/*/*/*/*/*/*/*FINE SEZIONE AZZERA VARIABILI*/*/*/*/*/*/*/*/*/*

                If par.IfNull(myReader("N_RATA"), "") = "99" Then
                    Nbolletta = "MA/" & par.IfNull(myReader("ANNO"), "")
                ElseIf par.IfNull(myReader("N_RATA"), "") = "999" Then
                    Nbolletta = "AU/" & par.IfNull(myReader("ANNO"), "")
                ElseIf par.IfNull(myReader("N_RATA"), "") = "99999" Then
                    Nbolletta = "CO/" & par.IfNull(myReader("ANNO"), "")
                Else
                    Nbolletta = par.IfNull(myReader("N_RATA"), "") & "/" & par.IfNull(myReader("ANNO"), "")
                End If

                If par.IfNull(myReader("FL_ANNULLATA"), 0) <> 0 Then
                    COLORE = "#FF1800"
                End If

                Contatore = Contatore + 1


                testoTabella = testoTabella _
                & "<tr bgcolor = '" & COLORE & "'>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & Contatore & ")</span></td>"



                '**********SCRITTURA DATI INFORMATIVI DELLA BOLLETTA
                If DataAl < par.IfNull(myReader("DATA_PAGAMENTO"), 0) Then
                    testoTabella = testoTabella & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioBolletta.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "','Dettagli" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("NUM_BOLLETTA"), "") & "</a>   *</span></td>"
                Else
                    testoTabella = testoTabella & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioBolletta.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "','Dettagli" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("NUM_BOLLETTA"), "") & "</a>    </span></td>"
                End If
                testoTabella = testoTabella & "<td style='height: 19px'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & Nbolletta & "</span></td>" _
                                            & "<td style='height: 19px'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("TIPO"), "n.d.") & "</span></td>" _
                                            & "<td style='height: 19px;text-align:center;'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("RIFERIMENTO"), "") & "</span></td>" _
                                            & "<td style='height: 19px'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
                                            & "<td style='height: 19px'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>"

                '*/*/*/*/*/*/*/*/*/*/*/*/FINE SCRITTURA DATI INFORMATIVI DELLA BOLLETTA


                '**************************SCRITTURA COLONNE IMPORTO + DATA PAGAMENTO


                '********************IMPORTO TOTALE DELLA BOLLETTA*********************************
                Importo = par.IfNull(myReader("IMPORTO_TOTALE"), 0)
                TotImporto = TotImporto + Importo
                testoTabella = testoTabella _
                            & "<td style='height: 19px; text-align:right'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>&euro;." & Format(Importo, "##,##0.00") & "</span></td>"

                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*

                '***********************COLONNA MOROSITA (dovrebbe essere rinominata RICLASSIFICATE)***************************
                'par.cmd.CommandText = "SELECT SUM(IMPORTO) AS TOT_RIC FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "") & "  AND ID_VOCE IN (150,151) "
                'lettore2 = par.cmd.ExecuteReader
                'If lettore2.Read Then
                Riclassificate = par.IfNull(myReader("IMPORTO_RIC_B"), 0)
                'End If
                'lettore2.Close()
                testoTabella = testoTabella _
                            & "<td style='height: 19px; text-align:right'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>&euro;." & Format(Riclassificate, "##,##0.00") & "</span></td>"

                TotRiclassificate = TotRiclassificate + Riclassificate
                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*

                '***********************COLONNA IMPORTO CONTABILE********************************************

                'par.cmd.CommandText = "SELECT SUM(IMPORTO) AS TOT_CONTABILE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "") & " AND T_VOCI_BOLLETTA.ID= BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.COMPETENZA <> 3  AND ID_VOCE not in ( 150,151) "
                'lettore2 = par.cmd.ExecuteReader
                'If lettore2.Read Then
                '    ImpContabile = par.IfNull(lettore2("TOT_CONTABILE"), 0)

                'End If
                'lettore2.Close()
                ImpContabile = par.IfNull(myReader("IMPORTO_TOTALE"), 0) - par.IfNull(myReader("IMPORTO_RIC_B"), 0) - par.IfNull(myReader("QUOTA_SIND_B"), 0) '
                testoTabella = testoTabella _
                            & "<td style='height: 19px; text-align:right'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>&euro;." & Format(ImpContabile, "##,##0.00") & "</span></td>"

                TotImpContabile = TotImpContabile + ImpContabile
                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*

                '**********************DATA PAGAMENTO *******************************************
                testoTabella = testoTabella _
                & "<td style='height: 19px;text-align:center'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")) & "</span></td>"
                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*


                '***********************COLONNA IMPORTO INCASSATO********************************************
                'par.cmd.CommandText = "SELECT SUM(IMP_PAGATO) AS TOT_INCASSATO FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "") & " AND T_VOCI_BOLLETTA.ID= BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.COMPETENZA <> 3  AND ID_VOCE not in ( 150,151) "
                'lettore2 = par.cmd.ExecuteReader
                'If lettore2.Read Then
                '    ImpIncassato = par.IfNull(lettore2("TOT_INCASSATO"), 0)
                'End If
                'lettore2.Close()
                ImpIncassato = (par.IfNull(myReader("IMPORTO_PAGATO"), 0) - par.IfNull(myReader("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(myReader("QUOTA_SIND_PAGATA_B"), 0))
                testoTabella = testoTabella _
                            & "<td style='height: 19px; text-align:right'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>&euro;." & Format(ImpIncassato, "##,##0.00") & "</span></td>"

                TotImpIncassato = TotImpIncassato + ImpIncassato
                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*

                '***********************COLONNA SALDO CONTABILE********************************************

                ImpSaldo = ImpContabile - ImpIncassato
                TotImpSaldoContabile = TotImpSaldoContabile + ImpSaldo
                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*

                '***********************COLONNA GIORNI DI RITARDO********************************************
                If myReader("FL_ANNULLATA") = 0 Then
                    If par.IfNull(myReader("DATA_PAGAMENTO"), "") <> "" Then
                        'DIFFERENZA FRA DATA SCADENZA E DATA PAGAMENTE
                        GiorniRitardo = DateDiff(DateInterval.Day, CDate(par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))), CDate(par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), ""))))
                    Else
                        'DIFFERENZA FRA DATA SCADENZA E OGGI
                        GiorniRitardo = DateDiff(DateInterval.Day, CDate(par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))), Date.Now)
                    End If

                    If GiorniRitardo < 0 Then
                        GiorniRitardo = ""
                    End If
                Else
                    GiorniRitardo = "ANNULLATA"
                End If

                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*



                testoTabella = testoTabella _
                            & "<td style='height: 19px; text-align:right'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>&euro;." & Format(par.IfEmpty(ImpSaldo, 0), "##,##0.00") & "</span></td>" _
                            & "<td style='height: 19px; text-align:right'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & GiorniRitardo & "</span></td>" _
                            & "</tr>"



                If COLORE = "#FFFFFF" Then
                    COLORE = "#E6E6E6"
                Else
                    COLORE = "#FFFFFF"
                End If

            Loop
            myReader.Close()


            '*************************************RIGA DEI TOTLAI**************************************************************************************************************
            testoTabella = testoTabella _
                                    & "<tr>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE</strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "<td style='height: 19px; text-align:right'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong> &euro;." & Format(par.IfEmpty(TotImporto, 0), "##,##0.00") & "</strong></span></td>" _
                                    & "<td style='height: 19px; text-align:right'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong> &euro;." & Format(par.IfEmpty(TotRiclassificate, 0), "##,##0.00") & "</strong></span></td>" _
                                    & "<td style='height: 19px; text-align:right'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>&euro;." & Format(par.IfEmpty(TotImpContabile, 0), "##,##0.00") & "</strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "<td style='height: 19px; text-align:right'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>&euro;." & Format(par.IfEmpty(TotImpIncassato, 0), "##,##0.00") & "</strong></span></td>" _
                                    & "<td style='height: 19px; text-align:right'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>&euro;." & Format(par.IfEmpty(TotImpSaldoContabile, 0), "##,##0.00") & "</strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "</tr>"

            Me.TBL_ESTRATTO_CONTO.Text = testoTabella & "</table>"

            '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*

            '************************************BOLLETTE ANNULLATE*****************************************
            testoTabella = ""
            Contatore = 0


            par.cmd.CommandText = "SELECT ID,DATA_EMISSIONE, DATA_SCADENZA, DATA_PAGAMENTO, IMPORTO_PAGATO,DATA_ANNULLO " _
                                & ",(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento " _
                                & "FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO WHERE ( FL_ANNULLATA='1' OR FL_ANNULLATA='2') AND BOL_BOLLETTE.ID_CONTRATTO = INTESTATARI_RAPPORTO.ID_CONTRATTO " _
                                & "AND BOL_BOLLETTE.DATA_EMISSIONE<=INTESTATARI_RAPPORTO.DATA_FINE AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA = " & Request.QueryString("IDANA") & " AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") _
                                & " AND BOL_BOLLETTE.ID_TIPO <> 5 " & condEmissione & " " & CondRiferimento
            myReader = par.cmd.ExecuteReader()

            testoTabella = "<table cellpadding='0' cellspacing='0' width='60%'>" & vbCrLf _
                        & "<tr>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>NUM.BOLLETTA</strong></span></td>" _
                        & "<td style='height: 19px;text-align:center'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>RIFERIMENTO</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO</strong></span></td>" _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA ANNULLO</strong></span></td>" _
                        & "<td style='height: 19px;text-align:center'>" _
                        & "</tr>"

            TotImporto = 0

            Do While myReader.Read

                Contatore = Contatore + 1

                testoTabella = testoTabella _
                & "<tr bgcolor = '" & COLORE & "'>" _
                                        & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & Contatore & ")</span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioBolletta.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "','Dettagli" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & Format(par.IfNull(myReader("ID"), ""), "0000000000") & "</a></span></td>" _
                & "<td style='height: 19px;text-align:center;'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("RIFERIMENTO"), "") & "</span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>"

                par.cmd.CommandText = "SELECT SUM(IMPORTO) AS IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "")
                lettore2 = par.cmd.ExecuteReader

                If lettore2.Read Then
                    testoTabella = testoTabella _
                    & "<td style='height: 19px; text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>&euro;." & Format(par.IfNull(lettore2("IMPORTO"), 0), "##,##0.00") & "</span></td>"
                    TotImporto = TotImporto + par.IfNull(lettore2("IMPORTO"), "")
                End If
                lettore2.Close()
                testoTabella = testoTabella _
                    & "<td style='height: 19px; text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_ANNULLO"), "")) & "</span></td>"



                testoTabella = testoTabella & "</tr>"


                If COLORE = "#FFFFFF" Then
                    COLORE = "#E6E6E6"
                Else
                    COLORE = "#FFFFFF"
                End If
            Loop
            myReader.Close()

            If TotImporto > 0 Then

                testoTabella = testoTabella _
                & "<tr bgcolor = '" & COLORE & "'>" _
                                        & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'></span></td>"


                testoTabella = testoTabella _
                & "<td style='height: 19px; text-align:right'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>&euro;." & Format(par.IfNull(TotImporto, 0), "##,##0.00") & "</span></td>"



                testoTabella = testoTabella _
                    & "<td style='height: 19px; text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'></span></td>"



                testoTabella = testoTabella & "</tr>"

                Me.TBL_ANNULLATE.Text = testoTabella & "</table>"
            Else
                Me.TBL_ANNULLATE.Text = ""
                TBL_ANNULLATE0.Text = ""
            End If

            '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*


            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If




        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        End Try

    End Sub
    Function GenHtmlTable()
        Dim sRet As String
        sRet = "<table border=0 width='100%'><tr>"
        sRet = sRet & Me.LblTitolo.Text
        sRet = sRet & "</tr>"
        sRet = sRet & "<tr>" & LblSaldoInizio.Text & "</tr>"
        sRet = sRet & "<tr>" & LblSaldoFine.Text & "</tr></table>"
        sRet = sRet & TBL_ESTRATTO_CONTO.Text

        GenHtmlTable = sRet

    End Function

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Dim Html As String = ""
        'Dim stringWriter As New System.IO.StringWriter
        'Dim sourcecode As New HtmlTextWriter(stringWriter)

        Try

            Html = Me.TBL_ESTRATTO_CONTO.Text

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
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 20
            pdfConverter1.PdfDocumentOptions.RightMargin = 5
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("Estratto Conto " & LblTitolo.Text)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "Exp_Pagamenti_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile("<br /><br />Estratto Conto " & LblTitolo.Text & "<br /><br />" & Me.LblSaldoInizio.Text & "<br /><br />" & LblSaldoFine.Text & "<br /><br />" & Html, url & nomefile)
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','ExpPdfBollette','');</script>")

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub


    Protected Sub ImgBtnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnExport.Click
        Try

            Dim DataDal As String
            Dim DataAl As String = ""

            Dim condEmissione As String = ""
            Dim CondRiferimento As String = ""

            '******APERTURA CONNESSIONE*****
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            '**************Setto date periodo
            DataDal = Request.QueryString("DAL")
            DataAl = par.IfEmpty(Request.QueryString("AL"), Format(Date.Now, "yyyyMMdd"))

            If Not String.IsNullOrEmpty(DataDal) Then
                condEmissione = " AND BOL_BOLLETTE.DATA_EMISSIONE >= '" & DataDal & "' "
            End If
            If Not String.IsNullOrEmpty(DataAl) Then
                condEmissione = condEmissione & " AND BOL_BOLLETTE.DATA_EMISSIONE <= '" & DataAl & "'"
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("RIFDAL")) Then
                CondRiferimento = "AND BOL_BOLLETTE.RIFERIMENTO_DA >= '" & Request.QueryString("RIFDAL") & "' "
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("RIFAL")) Then
                CondRiferimento = CondRiferimento & "AND BOL_BOLLETTE.RIFERIMENTO_A <= '" & Request.QueryString("RIFAL") & "'"
            End If

            par.cmd.CommandText = "SELECT BOL_BOLLETTE.*, " _
                                & "(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento, " _
                                & "(CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO " _
                                & "FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.TIPO_BOLLETTE WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) AND " _
                                & "TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO AND ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & condEmissione & " " & CondRiferimento & " " _
                                & " ORDER BY BOL_BOLLETTE.data_emissione,riferimento_da, bol_bollette.id DESC"




            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            Dim Nbolletta As String = ""
            Dim ImpContabile As Decimal = 0
            Dim ImpIncassato As Decimal = 0
            Dim ImpSaldo As Decimal = 0
            Dim Giorniritardo As String = ""
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            sNomeFile = "ExpEstrattoConto_" & Format(Now, "yyyyMMddHHmmss")

            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, LblTitolo.Text, 12)

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 2, 1, lblSottoTitolo.Text, 12)


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 1, txtsldinizio.Value, 12)

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 4, 1, txtsldfine.Value, 12)


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 5, 1, "NUM. BOLLETTA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 5, 2, "NUM. RATA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 5, 3, "TIPO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 5, 4, "RIFERIMENTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 5, 5, "DATA_EMISSIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 5, 6, "DATA_SCADENZA.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 5, 7, "BOLLETTATO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 5, 8, "IMP.RICLASSIFICATO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 5, 9, "IMP.CONTABILE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 5, 10, "DATA PAGAMENTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 5, 11, "IMP.INCASSATO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 5, 12, "SALDO CONTABILE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 5, 13, "GIORNI RITARDO", 12)

                K = 6
                For Each row In dt.Rows
                    If par.IfNull(row.Item("N_RATA"), "") = "99" Then
                        Nbolletta = "MA/" & par.IfNull(row.Item("ANNO"), "")
                    ElseIf par.IfNull(row.Item("N_RATA"), "") = "999" Then
                        Nbolletta = "AU/" & par.IfNull(row.Item("ANNO"), "")
                    ElseIf par.IfNull(row.Item("N_RATA"), "") = "99999" Then
                        Nbolletta = "CO/" & par.IfNull(row.Item("ANNO"), "")
                    Else
                        Nbolletta = par.IfNull(row.Item("N_RATA"), "") & "/" & par.IfNull(row.Item("ANNO"), "")
                    End If
                    ImpContabile = par.IfNull(row.Item("IMPORTO_TOTALE"), 0) - par.IfNull(row.Item("IMPORTO_RIC_B"), 0) - par.IfNull(row.Item("QUOTA_SIND_B"), 0) '
                    ImpIncassato = (par.IfNull(row.Item("IMPORTO_PAGATO"), 0) - par.IfNull(row.Item("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(row.Item("QUOTA_SIND_PAGATA_B"), 0))

                    If row.Item("FL_ANNULLATA") = 0 Then
                        If par.IfNull(row.Item("DATA_PAGAMENTO"), "") <> "" Then
                            'DIFFERENZA FRA DATA SCADENZA E DATA PAGAMENTE
                            GiorniRitardo = DateDiff(DateInterval.Day, CDate(par.FormattaData(par.IfNull(row.Item("DATA_SCADENZA"), ""))), CDate(par.FormattaData(par.IfNull(row.Item("DATA_PAGAMENTO"), ""))))
                        Else
                            'DIFFERENZA FRA DATA SCADENZA E OGGI
                            GiorniRitardo = DateDiff(DateInterval.Day, CDate(par.FormattaData(par.IfNull(row.Item("DATA_SCADENZA"), ""))), Date.Now)
                        End If
                        If Giorniritardo < 0 Then
                            Giorniritardo = ""
                        End If
                    Else
                        GiorniRitardo = "ANNULLATA"
                    End If
                    ImpSaldo = ImpContabile - ImpIncassato


                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(row.Item("NUM_BOLLETTA"), ""), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(Nbolletta, ""), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(row.Item("TIPO"), ""), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(row.Item("RIFERIMENTO"), ""), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.FormattaData(par.IfNull(row.Item("DATA_EMISSIONE"), "")), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.FormattaData(par.IfNull(row.Item("DATA_SCADENZA"), "")), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, Format(par.IfNull(row.Item("IMPORTO_TOTALE"), 0), "##,##0.00"), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, Format(par.IfNull(row.Item("IMPORTO_TOTALE"), 0), "##,##0.00"), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(ImpContabile, ""), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.FormattaData(par.IfNull(row.Item("DATA_PAGAMENTO"), "")), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.IfNull(ImpIncassato, ""), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.IfNull(ImpSaldo, ""), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.IfNull(Giorniritardo, ""), 4)

                    i = i + 1
                    K = K + 1
                Next
                .SetColumnWidth(1, 1, 100)
                .SetColumnWidth(2, 15, 30)

                .CloseFile()
            End With

            'Dim objCrc32 As New Crc32()
            'Dim strmZipOutputStream As ZipOutputStream
            'Dim zipfic As String

            'zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            'strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            'strmZipOutputStream.SetLevel(6)
            ''
            'Dim strFile As String
            'strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
            'Dim strmFile As FileStream = File.OpenRead(strFile)
            'Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            ''
            'strmFile.Read(abyBuffer, 0, abyBuffer.Length)

            'Dim sFile As String = Path.GetFileName(strFile)
            'Dim theEntry As ZipEntry = New ZipEntry(sFile)
            'Dim fi As New FileInfo(strFile)
            'theEntry.DateTime = fi.LastWriteTime
            'theEntry.Size = strmFile.Length
            'strmFile.Close()
            'objCrc32.Reset()
            'objCrc32.Update(abyBuffer)
            'theEntry.Crc = objCrc32.Value
            'strmZipOutputStream.PutNextEntry(theEntry)
            'strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            'strmZipOutputStream.Finish()
            'strmZipOutputStream.Close()

            'File.Delete(strFile)
            Response.Redirect("..\FileTemp\" & sNomeFile & ".xls")
            'Response.Write("<script>window.open('../FileTemp/" & sNomeFile & ".xls','aa','');</script>")


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
