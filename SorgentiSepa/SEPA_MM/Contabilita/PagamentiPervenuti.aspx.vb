'**********************************************************************22/08/2011**********************************************************************************************
'****************CODICE AGGIORNATO PER NON FAR RIENTRARE NEL DEBITO/CREDITO DELL'INQUILINO LE BOLLETTE DERIVANTI DALLE RATEIZZAZIONI*******************************************
'************************************************per visualizzare le modifiche cerca BOL_BOLLETTE.ID_TIPO <> 5*****************************************************************************

Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class Contabilita_PagamentiPervenuti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            If Not IsPostBack Then

                If Request.QueryString("IDANA") <> "" Then
                    Dim COLORE As String = "#E6E6E6"

                    Dim sStringaSQL As String = ""
                    'Dim sValore As String
                    Dim testoTabella As String = ""
                    Dim testoTabellaVoci As String = ""
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                    Dim TotFinalePagato As Double = 0
                    Dim TotDaPagare As Double = 0
                    Dim bTrovato As Boolean = False

                    Dim Contatore As Integer = 0


                    If Request.QueryString("NONPERV").ToString = "" Then
                        '*****lblrange di date
                        If Request.QueryString("DAL") <> "" Or Request.QueryString("AL") <> "" Then
                            If Request.QueryString("DAL") <> "" Then
                                lblRangeDate.Text = lblRangeDate.Text & " Dal " & par.FormattaData(Request.QueryString("DAL"))
                            End If

                            If Request.QueryString("AL") <> "" Then
                                lblRangeDate.Text = lblRangeDate.Text & " Al " & par.FormattaData(Request.QueryString("AL"))
                            End If
                        Else
                            Me.lblRangeDate.Visible = False
                        End If

                        If Request.QueryString("DAL") <> "" Then
                            bTrovato = True
                            sStringaSQL = sStringaSQL & " BOL_BOLLETTE.DATA_PAGAMENTO>='" & par.PulisciStrSql(Request.QueryString("DAL")) & "' "
                        End If

                        If Request.QueryString("AL") <> "" Then
                            If bTrovato = True Then sStringaSQL = sStringaSQL & " AND "
                            sStringaSQL = sStringaSQL & " BOL_BOLLETTE.DATA_PAGAMENTO<='" & par.PulisciStrSql(Request.QueryString("AL")) & "' "
                        End If

                        If sStringaSQL <> "" Then
                            sStringaSQL = sStringaSQL & " AND "
                        End If

                        '***********ESTRATTO pagamenti pervenuti nell'intervallo*****************


                        testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                        & "<tr>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>NUM.BOLLETTA</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>TIPO</strong></span></td>" _
                        & "<td style='height: 19px;text-align:center'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>RIFERIMENTO</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>NUM.RATA</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>VOCI BOLLETTA</strong></span></td>" _
                        & "<td style='height: 19px;text-align:center'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA PAGAMENTO</strong></span></td>" _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO INCASSATO</strong></span></td>" _
                        & "</tr>"
                        testoTabellaVoci = "<table cellpadding='0' cellspacing='0' width='100%'>" _
                        & "<tr>" _
                        & "<td style='height: 15'>" _
                        & "<span style='font-size: 6pt; font-family:Courier New'><strong>DESCRIZIONE</strong></span></td>" _
                        & "<td style='height: 15px;text-align:right'>" _
                        & "<span style='font-size: 6pt; font-family: Courier New'><strong>IMPORTO</strong></span></td>" _
                        & "</tr>"
                        '******APERTURA CONNESSIONE*****
                        If par.OracleConn.State = Data.ConnectionState.Closed Then
                            par.OracleConn.Open()
                            par.SettaCommand(par)
                        End If
                        ''ULTIMO AGGIORNAMENTO DELLE INFORMAZIONI
                        Dim UltimoPagam As String = 0
                        'par.cmd.CommandText = "SELECT to_char(to_date(MAX(DATA_PAGAMENTO),'yyyymmdd'),'dd/mm/yyyy') AS ULTIMA_BOLL FROM SISCOM_MI.BOL_BOLLETTE WHERE n_rata<>999 and N_RATA<>99"
                        'Dim myReaderTEMP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        'If myReaderTEMP.Read Then
                        UltimoPagam = par.FormattaData(Format(Now, "yyyyMMdd")) ' par.IfNull(myReaderTEMP("ULTIMA_BOLL"), 0)
                        'End If
                        'myReaderTEMP.Close()
                        'par.cmd.CommandText = ""

                        'ULTIMO AGGIORNAMENTO DELLE INFORMAZIONI
                        par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" FROM SISCOM_MI.ANAGRAFICA WHERE ID = " & Request.QueryString("IDANA")
                        myReader = par.cmd.ExecuteReader()

                        If myReader.Read Then
                            Me.LblTitolo.Text = par.IfNull(myReader("INTESTATARIO"), "") & " - informazioni aggiornate al: " & UltimoPagam
                        End If
                        myReader.Close()

                        If Request.QueryString("NONPERV").ToString = "" Then
                            sStringaSQL = "SELECT  BOL_BOLLETTE.*,(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento , (CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.TIPO_BOLLETTE  WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = BOL_BOLLETTE.ID_CONTRATTO AND IMPORTO_PAGATO IS NOT  NULL AND " & sStringaSQL & "  SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = " & Request.QueryString("IDANA") & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO ORDER BY BOL_BOLLETTE.ID DESC"
                            Me.lblTipoPagam.Text = "Pagamenti Pervenuti"

                        ElseIf Request.QueryString("NONPERV") = 1 Then
                            sStringaSQL = "SELECT  BOL_BOLLETTE.*,(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento , (CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.TIPO_BOLLETTE  WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = BOL_BOLLETTE.ID_CONTRATTO  AND IMPORTO_PAGATO IS NULL AND " & sStringaSQL & " SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = " & Request.QueryString("IDANA") & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO ORDER BY BOL_BOLLETTE.ID DESC"
                            Me.lblTipoPagam.Text = "Pagamenti NON Pervenuti"
                        End If

                        par.cmd.CommandText = sStringaSQL
                        myReader = par.cmd.ExecuteReader
                        Dim Nbolletta As String

                        Do While myReader.Read
                            Nbolletta = ""
                            If par.IfNull(myReader("N_RATA"), "") = "99" Then
                                Nbolletta = "MA/" & par.IfNull(myReader("ANNO"), "")
                            ElseIf par.IfNull(myReader("N_RATA"), "") = "999" Then
                                Nbolletta = "AU/" & par.IfNull(myReader("ANNO"), "")
                            ElseIf par.IfNull(myReader("N_RATA"), "") = "99999" Then
                                Nbolletta = "CO/" & par.IfNull(myReader("ANNO"), "")
                            Else
                                Nbolletta = par.IfNull(myReader("N_RATA"), "") & "/" & par.IfNull(myReader("ANNO"), "")
                            End If

                            Contatore = Contatore + 1
                            testoTabella = testoTabella _
                            & "<tr>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & Contatore & ")</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioBolletta.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "','Dettagli" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("NUM_BOLLETTA"), "") & "</a></span></td>"

                            If par.IfNull(myReader("TIPO"), "n.d.") = "MOR" Or par.IfNull(myReader("TIPO"), "n.d.") = "FIN" Then
                                testoTabella = testoTabella _
                                & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DetMorosita.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "','DettMorosita" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("TIPO"), "n.d.") & "</a></span></td>"
                            Else
                                testoTabella = testoTabella _
                                    & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("TIPO"), "n.d.") & "</span></td>"
                            End If

                            testoTabella = testoTabella _
                            & "<td style='height: 19px;text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("RIFERIMENTO"), "") & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & Nbolletta & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & testoTabellaVoci & "</span>"

                            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*, T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), 0) & " AND ID_VOCE = T_VOCI_BOLLETTA.ID"
                            myReader2 = par.cmd.ExecuteReader

                            Do While myReader2.Read
                                testoTabella = testoTabella _
                                & "<tr bgcolor = '" & COLORE & "'>" _
                                & "<td style='height: 15px'>" _
                                & "<span style='font-size: 6pt; font-family: Courier New'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioImpPagati.aspx?IDBOLLETTA=" & par.IfNull(myReader("ID"), 0) & "&IDVOCE=" & par.IfNull(myReader2("ID"), "") & "','DettPagati" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader2("DESCRIZIONE"), "") & "</a></span></td>" _
                                & "<td style='height: 15px;text-align:right'>" _
                                & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(myReader2("IMPORTO"), 0)), "##,##0.00") & "</span></td></tr>"
                                'TotDaPagare = TotDaPagare + par.IfNull(myReader2("IMPORTO"), 0)
                                If COLORE = "#FFFFFF" Then
                                    COLORE = "#E6E6E6"
                                Else
                                    COLORE = "#FFFFFF"
                                End If
                            Loop
                            myReader2.Close()
                            TotDaPagare = par.IfNull(myReader("IMPORTO_TOTALE"), 0) - par.IfNull(myReader("IMPORTO_RIC_B"), 0)
                            testoTabella = testoTabella _
                            & "<tr>" _
                            & "<td style='height: 15px'>" _
                            & "<span style='font-size: 6pt; font-family: Courier New'>TOTALE</span></td>" _
                            & "<td style='height: 15px;text-align:right'>" _
                            & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(TotDaPagare, 0)), "##,##0.00") & "</span></td>"



                            testoTabella = testoTabella & "</table></td>" _
                            & "<td style='height: 19px;text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfEmpty(par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")), "N.D.") & "</span></td>"

                            If Format(TotDaPagare, "0.00") <> par.IfNull(myReader("IMPORTO_PAGATO"), 0) Then
                                testoTabella = testoTabella & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial;color: #c30307'>€." & Format((par.IfNull(myReader("IMPORTO_PAGATO"), 0) - par.IfNull(myReader("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(myReader("QUOTA_SIND_PAGATA_B"), 0)), "##,##0.00") & "</span></td>" _
                                            & "</tr>"
                            Else
                                testoTabella = testoTabella & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>€." & Format((par.IfNull(myReader("IMPORTO_PAGATO"), 0) - par.IfNull(myReader("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(myReader("QUOTA_SIND_PAGATA_B"), 0)), "##,##0.00") & "</span></td>" _
                                            & "</tr>"
                            End If


                            TotFinalePagato = TotFinalePagato + (par.IfNull(myReader("IMPORTO_PAGATO"), 0) - par.IfNull(myReader("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(myReader("QUOTA_SIND_PAGATA_B"), 0))
                            'If par.IfNull(myReader("TIPO"), "n.d.") <> "MOR" Or par.IfNull(myReader("TIPO"), "n.d.") <> "FIN" Then
                            '    TotFinalePagato = TotFinalePagato + par.IfNull(myReader("IMPORTO_PAGATO"), 0)
                            'Else
                            '    Dim lettore As Oracle.DataAccess.Client.OracleDataReader
                            '    par.cmd.CommandText = "SELECT SUM(IMPORTO_PAGATO) AS TOT_MOROSITA FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_BOLLETTA_RIC =" & par.IfNull(myReader("ID"), 0)
                            '    lettore = par.cmd.ExecuteReader
                            '    If lettore.Read Then
                            '        TotFinalePagato = TotFinalePagato + (par.IfNull(myReader("IMPORTO_PAGATO"), 0) - par.IfNull(lettore("TOT_MOROSITA"), 0))

                            '    Else
                            '        TotFinalePagato = TotFinalePagato + par.IfNull(myReader("IMPORTO_PAGATO"), 0)
                            '    End If
                            '    lettore.Close()
                            'End If


                            TotDaPagare = 0
                        Loop
                        testoTabella = testoTabella _
                        & "<tr>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>TOTALE</span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>€." & Format((par.IfNull(TotFinalePagato, 0)), "##,##0.00") & "</span></td>"

                        myReader.Close()
                        Me.TBL_PAGAMENTI_PERVENUTI.Text = testoTabella & "</table>"



                    Else
                        '*******************PAGAMENTI NON PERVENUTI

                        '*****lblrange di date
                        If Request.QueryString("DAL") <> "" Or Request.QueryString("AL") <> "" Then
                            If Request.QueryString("DAL") <> "" Then
                                lblRangeDate.Text = lblRangeDate.Text & "Dal " & par.FormattaData(Request.QueryString("DAL"))
                            End If
                            If Request.QueryString("AL") <> "" Then
                                lblRangeDate.Text = lblRangeDate.Text & " Al " & par.FormattaData(Request.QueryString("AL"))
                            End If
                        Else
                            Me.lblRangeDate.Visible = False
                        End If


                        Dim GiorniRitardo As String = 0

                        If Request.QueryString("DAL") <> "" Then
                            bTrovato = True
                            sStringaSQL = sStringaSQL & " BOL_BOLLETTE.DATA_SCADENZA>='" & par.PulisciStrSql(Request.QueryString("DAL")) & "' "
                        End If
                        If Request.QueryString("AL") <> "" Then
                            If bTrovato = True Then sStringaSQL = sStringaSQL & " AND "
                            sStringaSQL = sStringaSQL & " BOL_BOLLETTE.DATA_SCADENZA<='" & par.PulisciStrSql(Request.QueryString("AL")) & "' "
                        End If
                        If sStringaSQL <> "" Then
                            sStringaSQL = sStringaSQL & " AND "
                        End If
                        bTrovato = False
                        Dim sRangePagamento As String = ""

                        'If Request.QueryString("DAL") <> "" Then
                        '    If bTrovato = False Then
                        '        sRangePagamento = sRangePagamento & " OR ("
                        '    End If
                        '    bTrovato = True
                        '    sRangePagamento = sRangePagamento & " BOL_BOLLETTE.DATA_PAGAMENTO>='" & par.PulisciStrSql(Request.QueryString("DAL")) & "' "
                        'End If
                        'If Request.QueryString("AL") <> "" Then
                        '    If bTrovato = False Then
                        '        sRangePagamento = sRangePagamento & " OR ("
                        '    End If
                        '    If bTrovato = True Then sRangePagamento = sRangePagamento & " AND "
                        '    sRangePagamento = sRangePagamento & " BOL_BOLLETTE.DATA_PAGAMENTO<='" & par.PulisciStrSql(Request.QueryString("AL")) & "' "
                        'End If
                        'If sRangePagamento <> "" Then
                        '    sRangePagamento = sRangePagamento & ") "
                        'End If

                        '***********ESTRATTO pagamenti pervenuti nell'intervallo*****************


                        testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                        & "<tr>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>NUM.BOLLETTA</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>TIPO</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>NUM.RATA</strong></span></td>" _
                        & "<td style='height: 19px;text-align:center'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>RIFERIMENTO</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>VOCI BOLLETTA</strong></span></td>" _
                        & "<td style='height: 19px;text-align:center'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA PAGAMENTO</strong></span></td>" _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO DA PAGARE</strong></span></td>" _
                        & "</tr>"

                        testoTabellaVoci = "<table cellpadding='0' cellspacing='0' width='100%'>" _
                        & "<tr>" _
                        & "<td style='height: 15'>" _
                        & "<span style='font-size: 6pt; font-family:Courier New'><strong>DESCRIZIONE</strong></span></td>" _
                        & "<td style='height: 15px;text-align:right'>" _
                        & "<span style='font-size: 6pt; font-family: Courier New'><strong>IMPORTO</strong></span></td>" _
                        & "</tr>"
                        '******APERTURA CONNESSIONE*****
                        If par.OracleConn.State = Data.ConnectionState.Closed Then
                            par.OracleConn.Open()
                            par.SettaCommand(par)
                        End If
                        'ULTIMO AGGIORNAMENTO DELLE INFORMAZIONI
                        Dim UltimoPagam As String = 0
                        'par.cmd.CommandText = "SELECT to_char(to_date(MAX(DATA_PAGAMENTO),'yyyymmdd'),'dd/mm/yyyy') AS ULTIMA_BOLL FROM SISCOM_MI.BOL_BOLLETTE WHERE n_rata<>999 and N_RATA<>99"
                        'Dim myReaderTEMP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        'If myReaderTEMP.Read Then
                        UltimoPagam = par.FormattaData(Format(Now, "yyyyMMdd")) ' par.IfNull(myReaderTEMP("ULTIMA_BOLL"), 0)
                        'End If
                        'myReaderTEMP.Close()
                        par.cmd.CommandText = ""

                        'ULTIMO AGGIORNAMENTO DELLE INFORMAZIONI
                        par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" FROM SISCOM_MI.ANAGRAFICA WHERE ID = " & Request.QueryString("IDANA")
                        myReader = par.cmd.ExecuteReader()

                        If myReader.Read Then
                            Me.LblTitolo.Text = par.IfNull(myReader("INTESTATARIO"), "") & " - informazioni aggiornate al: " & UltimoPagam
                        End If
                        myReader.Close()


                        'sStringaSQL = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO WHERE  INTESTATARI_RAPPORTO.ID_CONTRATTO = BOL_BOLLETTE.ID_CONTRATTO AND FL_ANNULLATA = '0' AND (IMPORTO_PAGATO IS NULL) AND " & sStringaSQL & " INTESTATARI_RAPPORTO.ID_ANAGRAFICA = " & Request.QueryString("IDANA") & " AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & "  ORDER BY BOL_BOLLETTE.ID DESC"
                        sStringaSQL = "SELECT  BOL_BOLLETTE.*,(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento , (CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.TIPO_BOLLETTE  WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) AND  SOGGETTI_CONTRATTUALI.ID_CONTRATTO = BOL_BOLLETTE.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE'  AND (IMPORTO_PAGATO <> importo_totale OR IMPORTO_PAGATO IS NULL) AND " & sStringaSQL & " SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = " & Request.QueryString("IDANA") & " AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO ORDER BY BOL_BOLLETTE.ID DESC"

                        Me.lblTipoPagam.Text = "Pagamenti NON Pervenuti"

                        par.cmd.CommandText = sStringaSQL
                        myReader = par.cmd.ExecuteReader
                        Dim Nbolletta As String

                        Do While myReader.Read

                            Nbolletta = ""
                            If par.IfNull(myReader("N_RATA"), "") = "99" Then
                                Nbolletta = "MA/" & par.IfNull(myReader("ANNO"), "")
                            ElseIf par.IfNull(myReader("N_RATA"), "") = "999" Then
                                Nbolletta = "AU/" & par.IfNull(myReader("ANNO"), "")
                            ElseIf par.IfNull(myReader("N_RATA"), "") = "99999" Then
                                Nbolletta = "CO/" & par.IfNull(myReader("ANNO"), "")
                            Else
                                Nbolletta = par.IfNull(myReader("N_RATA"), "") & "/" & par.IfNull(myReader("ANNO"), "")
                            End If

                            Contatore = Contatore + 1
                            testoTabella = testoTabella _
                            & "<tr>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & Contatore & ")</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioBolletta.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "','Dettagli" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("NUM_BOLLETTA"), "") & "</a></span></td>"

                            If par.IfNull(myReader("TIPO"), "n.d.") = "MOR" Or par.IfNull(myReader("TIPO"), "n.d.") = "FIN" Then
                                testoTabella = testoTabella _
                                & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DetMorosita.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "','DettMorosita" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("TIPO"), "n.d.") & "</a></span></td>"
                            Else
                                testoTabella = testoTabella _
                                    & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("TIPO"), "n.d.") & "</span></td>"
                            End If

                            testoTabella = testoTabella _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & Nbolletta & "</span></td>" _
                            & "<td style='height: 19px;text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("RIFERIMENTO"), "") & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>" _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & testoTabellaVoci & "</span>"

                            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*, T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), 0) & " AND ID_VOCE = T_VOCI_BOLLETTA.ID"
                            myReader2 = par.cmd.ExecuteReader

                            Do While myReader2.Read
                                testoTabella = testoTabella _
                                & "<tr bgcolor = '" & COLORE & "'>" _
                                & "<td style='height: 15px'>" _
                                & "<span style='font-size: 6pt; font-family: Courier New'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioImpPagati.aspx?IDBOLLETTA=" & par.IfNull(myReader("ID"), 0) & "&IDVOCE=" & par.IfNull(myReader2("ID"), "") & "','DettPagati" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader2("DESCRIZIONE"), "") & "</a></span></td>" _
                                & "<td style='height: 15px;text-align:right'>" _
                                & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(myReader2("IMPORTO"), 0)), "##,##0.00") & "</span></td></tr>"
                                'TotDaPagare = TotDaPagare + par.IfNull(myReader2("IMPORTO"), 0)
                                If COLORE = "#FFFFFF" Then
                                    COLORE = "#E6E6E6"
                                Else
                                    COLORE = "#FFFFFF"
                                End If
                            Loop
                            myReader2.Close()
                            TotDaPagare = (par.IfNull(myReader("IMPORTO_TOTALE"), 0) - par.IfNull(myReader("IMPORTO_RIC_B"), 0) - par.IfNull(myReader("QUOTA_SIND_B"), 0)) - ((par.IfNull(myReader("IMPORTO_PAGATO"), 0) - par.IfNull(myReader("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(myReader("QUOTA_SIND_PAGATA_B"), 0)))

                            testoTabella = testoTabella _
                            & "<tr>" _
                            & "<td style='height: 15px'>" _
                            & "<span style='font-size: 6pt; font-family: Courier New'>TOTALE</span></td>" _
                            & "<td style='height: 15px;text-align:right'>" _
                            & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(TotDaPagare, 0)), "##,##0.00") & "</span></td>"

                            'DIFFERENZA FRA DATA SCADENZA E OGGI
                            GiorniRitardo = DateDiff(DateInterval.Day, CDate(par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))), Date.Now)
                            If GiorniRitardo < 0 Then
                                GiorniRitardo = ""
                            End If
                            testoTabella = testoTabella & "</table></td>" _
                            & "<td style='height: 19px;text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfEmpty(par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")), "N.D.") & "</span></td>" _
                            & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfNull(TotDaPagare, 0), "##,##0.00") & "</span></td>" _
                            & "</tr>"

                            'If par.IfNull(myReader("TIPO"), "n.d.") <> "MOR" And par.IfNull(myReader("TIPO"), "n.d.") <> "FIN" Then
                            TotFinalePagato = TotFinalePagato + TotDaPagare
                            'Else
                            'Dim lettore As Oracle.DataAccess.Client.OracleDataReader
                            'par.cmd.CommandText = "SELECT SUM(IMPORTO_TOTALE) AS TOT_MOROSITA FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_BOLLETTA_RIC =" & par.IfNull(myReader("ID"), 0)
                            'lettore = par.cmd.ExecuteReader
                            'If lettore.Read Then
                            '    TotFinalePagato = TotFinalePagato + (TotDaPagare - par.IfNull(lettore("TOT_MOROSITA"), 0))

                            'Else
                            '    TotFinalePagato = TotFinalePagato + par.IfNull(TotDaPagare, 0)
                            'End If
                            'lettore.Close()
                            'End If

                            TotDaPagare = 0
                            GiorniRitardo = 0
                        Loop
                        testoTabella = testoTabella _
                        & "<tr>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>TOTALE</span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>€." & Format((par.IfNull(TotFinalePagato, 0)), "##,##0.00") & "</span></td>"

                        myReader.Close()
                        Me.TBL_PAGAMENTI_PERVENUTI.Text = testoTabella & "</table>"








                    End If

                    '*********************CHIUSURA CONNESSIONE**********************
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                End If

            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Dim Html As String = ""
        'Dim stringWriter As New System.IO.StringWriter
        'Dim sourcecode As New HtmlTextWriter(stringWriter)

        Try

            Html = TBL_PAGAMENTI_PERVENUTI.Text

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
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 10
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = (LblTitolo.Text & " - " & lblTipoPagam.Text & " " & Me.lblRangeDate.Text)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "Exp_Pagamenti_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile(LblTitolo.Text & "<br /><br />" & lblTipoPagam.Text & " " & Me.lblRangeDate.Text & "<br /><br />" & Html, url & nomefile)
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','ExpPdfBollette','');</script>")

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class
