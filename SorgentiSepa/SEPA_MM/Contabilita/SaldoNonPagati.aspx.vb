'**********************************************************************22/08/2011**********************************************************************************************
'****************CODICE AGGIORNATO PER NON FAR RIENTRARE NEL DEBITO/CREDITO DELL'INQUILINO LE BOLLETTE DERIVANTI DALLE RATEIZZAZIONI*******************************************
'************************************************per visualizzare le modifiche cerca BOL_BOLLETTE.ID_TIPO <> 5*****************************************************************************

Partial Class Contabilita_SaldoNonPagati
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim TotImporto As Double = 0
    Dim TotContabile As Double = 0
    Dim TotPagato As Double = 0
    Dim TotMorosita As Decimal = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaDettaglioSaldo()
        End If

    End Sub
    Private Sub CaricaDettaglioSaldo()
        Try
            '******APERTURA CONNESSIONE*****

            If Request.QueryString("ID_CONT") <> "" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                Dim testoTabella As String = ""
                Dim COLORE As String = "#E6E6E6"
                Dim EmessoContabile As Double = 0
                Dim ImportoBolletta As Double = 0
                Dim Contatore As Integer = 0
                Dim Nbolletta As String
                Dim GiorniRitardo As String = 0

                'par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"",COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.ANAGRAFICA, SISCOM_MI.INTESTATARI_RAPPORTO WHERE RAPPORTI_UTENZA.ID = " & Request.QueryString("ID_CONT") & " AND INTESTATARI_RAPPORTO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ANAGRAFICA.ID = INTESTATARI_RAPPORTO.ID_ANAGRAFICA"
                par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"",COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE RAPPORTI_UTENZA.ID = " & Request.QueryString("ID_CONT") & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.cod_tipologia_occupante = 'INTE'"

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Me.LblIntestazione.Text = myReader("INTESTATARIO") & " COD. CONTRATTO " & myReader("COD_CONTRATTO") '& " - " & UltimoPagam
                End If

                myReader.Close()
                testoTabella = "<table cellpadding='1' cellspacing='1' width='100%'>" & vbCrLf _
                                & "<tr>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>NUMERO BOLLETTA</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>NUMERO RATA</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>TIPO   </strong></span></td>" _
                                & "<td style='height: 19px;text-align:center'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>RIFERIMENTO</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA PAGAMENTO</strong></span></td>" _
                                & "<td style='height: 19px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO</strong></span></td>" _
                                & "<td style='height: 19px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>MOR/RIC</strong></span></td>" _
                                & "<td style='height: 19px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO CONTABILE</strong></span></td>" _
                                & "<td style='height: 19px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO PAGATO</strong></span></td>" _
                                & "<td style='height: 19px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>GIORNI RITARDO</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "</td>" _
                                & "<td style='height: 19px'>" _
                                & "</td>" _
                                & "</tr>"


                par.cmd.CommandText = "SELECT BOL_BOLLETTE.*, " _
                                    & "(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento, " _
                                    & "(CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO " _
                                    & "FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.TIPO_BOLLETTE WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) AND " _
                                    & "TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO AND ID_CONTRATTO = " & Request.QueryString("ID_CONT") & "" _
                                    & " ORDER BY BOL_BOLLETTE.ID DESC"
                myReader = par.cmd.ExecuteReader
                Do While myReader.Read
                    Nbolletta = ""
                    ImportoBolletta = 0

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
                    & "<span style='font-size: 8pt; font-family: Arial'>" & Contatore & ")</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioBolletta.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("ID_CONT") & "','Dettagli" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("NUM_BOLLETTA"), "") & "</a></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & Nbolletta & "</span></td>"

                    If par.IfNull(myReader("TIPO"), "n.d.") = "MOR" Or par.IfNull(myReader("TIPO"), "n.d.") = "FIN" Then
                        testoTabella = testoTabella _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DetMorosita.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "','DettMorosita" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("TIPO"), "n.d.") & "</a></span></td>"
                    Else
                        testoTabella = testoTabella _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("TIPO"), "n.d.") & "</span></td>"
                    End If

                    testoTabella = testoTabella _
                    & "<td style='height: 19px;text-align:center'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("RIFERIMENTO"), " ") & "</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")) & "</span></td>"

                    '**************colonna importo TOTALE****************************************************************************************************************************
                    testoTabella = testoTabella _
                    & "<td style='height: 19px;text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format((par.IfNull(myReader("IMPORTO_TOTALE"), 0)), "##,##0.00") & "</span></td>"
                    TotImporto = TotImporto + par.IfNull(myReader("IMPORTO_TOTALE"), 0)
                    ImportoBolletta = par.IfNull(myReader("IMPORTO_TOTALE"), 0)

                    '**************colonna MOR/RIC**********************************************************************************************************************************
                    testoTabella = testoTabella _
                    & "<td style='height: 19px;text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfNull(myReader("IMPORTO_RIC_B"), 0), "##,##0.00") & "</span></td>"
                    TotMorosita = TotMorosita + par.IfNull(myReader("IMPORTO_RIC_B"), 0)

                    '**************colonna EMESSO CONTABILE*************************************************************************************************************************
                    EmessoContabile = par.IfNull(myReader("IMPORTO_TOTALE"), 0) - par.IfNull(myReader("IMPORTO_RIC_B"), 0) - par.IfNull(myReader("QUOTA_SIND_B"), 0) '
                    TotContabile = TotContabile + EmessoContabile
                    testoTabella = testoTabella _
                                            & "<td style='height: 19px;text-align:right'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>€." & Format((par.IfNull(EmessoContabile, 0)), "##,##0.00") & "</span></td>"


                    '************colonna IMPORTO PAGATO****************************************************************************************************************************
                    If myReader("IMPORTO_TOTALE") <> par.IfNull(myReader("IMPORTO_PAGATO"), 0) Then
                        testoTabella = testoTabella _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial;color: #c30307'>€." & Format((par.IfNull(myReader("IMPORTO_PAGATO"), 0) - par.IfNull(myReader("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(myReader("QUOTA_SIND_PAGATA_B"), 0)), "##,##0.00") & "</span></td>"
                    Else
                        testoTabella = testoTabella _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial;'>€." & Format((par.IfNull(myReader("IMPORTO_PAGATO"), 0) - par.IfNull(myReader("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(myReader("QUOTA_SIND_PAGATA_B"), 0)), "##,##0.00") & "</span></td>"
                    End If
                    TotPagato = TotPagato + (par.IfNull(myReader("IMPORTO_PAGATO"), 0) - par.IfNull(myReader("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(myReader("QUOTA_SIND_PAGATA_B"), 0))

                    ''************colonna GIORNI DI RITARDO****************************************************************************************************************************
                    If ImportoBolletta > 0 Then
                        If par.IfNull(myReader("DATA_PAGAMENTO"), "") <> "" Then
                            'DIFFERENZA FRA DATA SCADENZA E DATA PAGAMENTE
                            GiorniRitardo = DateDiff(DateInterval.Day, CDate(par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))), CDate(par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), ""))))
                        Else
                            'DIFFERENZA FRA DATA SCADENZA E OGGI
                            GiorniRitardo = DateDiff(DateInterval.Day, CDate(par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))), Date.Now)
                        End If
                    Else
                        GiorniRitardo = -1
                    End If

                    If GiorniRitardo < 0 Then
                        GiorniRitardo = " "
                    End If

                    If par.IfNull(myReader("FL_ANNULLATA"), 0) <> 0 Then
                        GiorniRitardo = "ANNULLATA"
                    End If
                    testoTabella = testoTabella _
                    & "<td style='height: 19px;text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & GiorniRitardo & "</span></td>"


                    If COLORE = "#FFFFFF" Then
                        COLORE = "#E6E6E6"
                    Else
                        COLORE = "#FFFFFF"
                    End If

                Loop

                myReader.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                testoTabella = testoTabella _
                & "<tr style = 'background-color: #D8D8D8;'>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
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
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                & "<td style='height: 19px; text-align:right'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotImporto, "##,##0.00") & "</strong></span></td>" _
                & "<td style='height: 19px; text-align:right'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotMorosita, "##,##0.00") & "</strong></span></td>" _
                & "<td style='height: 19px; text-align:right'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotContabile, "##,##0.00") & "</strong></span></td>" _
                & "<td style='height: 19px; text-align:right'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotPagato, "##,##0.00") & "</strong></span></td>" _
                & "<td style='height: 19px'>" _
                & "</td>" _
                & "</tr>"



                Me.TBL_DETTAGLIO_SALDO.Text = testoTabella & "</table>"


            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
    'Private Sub CaricaDettaglioSaldo()

    '    Try
    '        '******APERTURA CONNESSIONE*****

    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If

    '        If Request.QueryString("ID_CONT") <> "" Then

    '            Dim testoTabella As String = ""
    '            Dim COLORE As String = "#E6E6E6"
    '            Dim EmessoContabile As Double = 0
    '            Dim ImportoBolletta As Double = 0
    '            Dim Contatore As Integer = 0


    '            par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"",COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.ANAGRAFICA, SISCOM_MI.INTESTATARI_RAPPORTO WHERE RAPPORTI_UTENZA.ID = " & Request.QueryString("ID_CONT") & " AND INTESTATARI_RAPPORTO.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ANAGRAFICA.ID = INTESTATARI_RAPPORTO.ID_ANAGRAFICA"
    '            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            If myReader.Read Then
    '                Me.LblIntestazione.Text = myReader("INTESTATARIO") & " COD. CONTRATTO " & myReader("COD_CONTRATTO") '& " - " & UltimoPagam
    '            End If

    '            myReader.Close()
    '            testoTabella = "<table cellpadding='1' cellspacing='1' width='100%'>" & vbCrLf _
    '                            & "<tr>" _
    '                            & "<td style='height: 19px'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    '                            & "<td style='height: 19px'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial'><strong>NUMERO BOLLETTA</strong></span></td>" _
    '                            & "<td style='height: 19px'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial'><strong>NUMERO RATA</strong></span></td>" _
    '                            & "<td style='height: 19px'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial'><strong>TIPO   </strong></span></td>" _
    '                            & "<td style='height: 19px;text-align:center'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial'><strong>RIFERIMENTO</strong></span></td>" _
    '                            & "<td style='height: 19px'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
    '                            & "<td style='height: 19px'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
    '                            & "<td style='height: 19px'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA PAGAMENTO</strong></span></td>" _
    '                            & "<td style='height: 19px;text-align:right'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO</strong></span></td>" _
    '                            & "<td style='height: 19px;text-align:right'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial'><strong>MOROSITA</strong></span></td>" _
    '                            & "<td style='height: 19px;text-align:right'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO CONTABILE</strong></span></td>" _
    '                            & "<td style='height: 19px;text-align:right'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO PAGATO</strong></span></td>" _
    '                            & "<td style='height: 19px;text-align:right'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial'><strong>GIORNI RITARDO</strong></span></td>" _
    '                            & "<td style='height: 19px'>" _
    '                            & "</td>" _
    '                            & "<td style='height: 19px'>" _
    '                            & "</td>" _
    '                            & "</tr>"



    '            'SE BOSGONA PRENDERE SOLO QUELLI NON PAGATI DECOMENNTARE LE CONDIZIONI PER DATA PAGAMENTO
    '            par.cmd.CommandText = "SELECT BOL_BOLLETTE.*, " _
    '                                & "(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento, " _
    '                                & "(CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO " _
    '                                & "FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.TIPO_BOLLETTE WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) AND " _
    '                                & "TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO AND ID_CONTRATTO = " & Request.QueryString("ID_CONT") & "" _
    '                                & " ORDER BY BOL_BOLLETTE.ID DESC"
    '            myReader = par.cmd.ExecuteReader
    '            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
    '            Dim Nbolletta As String
    '            Do While myReader.Read
    '                Nbolletta = ""
    '                ImportoBolletta = 0

    '                If par.IfNull(myReader("N_RATA"), "") = "99" Then
    '                    Nbolletta = "MA/" & par.IfNull(myReader("ANNO"), "")
    '                ElseIf par.IfNull(myReader("N_RATA"), "") = "999" Then
    '                    Nbolletta = "AU/" & par.IfNull(myReader("ANNO"), "")
    '                ElseIf par.IfNull(myReader("N_RATA"), "") = "99999" Then
    '                    Nbolletta = "CO/" & par.IfNull(myReader("ANNO"), "")
    '                Else
    '                    Nbolletta = par.IfNull(myReader("N_RATA"), "") & "/" & par.IfNull(myReader("ANNO"), "")
    '                End If

    '                If par.IfNull(myReader("FL_ANNULLATA"), 0) <> 0 Then
    '                    COLORE = "#FF1800"
    '                End If


    '                Contatore = Contatore + 1

    '                testoTabella = testoTabella _
    '                & "<tr bgcolor = '" & COLORE & "'>" _
    '                & "<td style='height: 19px'>" _
    '                & "<span style='font-size: 8pt; font-family: Arial'>" & Contatore & ")</span></td>" _
    '                & "<td style='height: 19px'>" _
    '                & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioBolletta.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("ID_CONT") & "','Dettagli" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("NUM_BOLLETTA"), "") & "</a></span></td>" _
    '                & "<td style='height: 19px'>" _
    '                & "<span style='font-size: 8pt; font-family: Arial'>" & Nbolletta & "</span></td>"

    '                If par.IfNull(myReader("TIPO"), "n.d.") = "MOR" Or par.IfNull(myReader("TIPO"), "n.d.") = "FIN" Then
    '                    testoTabella = testoTabella _
    '                    & "<td style='height: 19px'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DetMorosita.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "','DettMorosita" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("TIPO"), "n.d.") & "</a></span></td>"
    '                Else
    '                    testoTabella = testoTabella _
    '                    & "<td style='height: 19px'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("TIPO"), "n.d.") & "</span></td>"
    '                End If


    '                testoTabella = testoTabella _
    '                & "<td style='height: 19px;text-align:center'>" _
    '                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("RIFERIMENTO"), " ") & "</span></td>" _
    '                & "<td style='height: 19px'>" _
    '                & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
    '                & "<td style='height: 19px'>" _
    '                & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>" _
    '                & "<td style='height: 19px'>" _
    '                & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")) & "</span></td>"


    '                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader
    '                If par.IfNull(myReader("ID_TIPO"), 0) = 4 Or par.IfNull(myReader("ID_TIPO"), 0) = 3 Then

    '                    testoTabella = testoTabella _
    '                    & "<td style='height: 19px;text-align:right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(0, "##,##0.00") & "</span></td>"
    '                    TotImporto = TotImporto + 0
    '                    'si può eliminare e prendere da bol_bollette
    '                    '*****IMPORTO DELLA BOLLETTA DI MOROSITA PRELEVANDO IMPORTO DALLE SINGOLE VOCI DI BOLLETTA
    '                    'par.cmd.CommandText = "SELECT SUM(IMPORTO) AS IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "") 'IMPORTO BOLLETTA DI MOROSITA'
    '                    'myReader2 = par.cmd.ExecuteReader
    '                    'If myReader2.Read Then
    '                    testoTabella = testoTabella _
    '                    & "<td style='height: 19px;text-align:right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format((par.IfNull(myReader("IMPORTO_TOTALE"), 0)), "##,##0.00") & "</span></td>"
    '                    TotMorosita = TotMorosita + par.IfNull(myReader("IMPORTO_TOTALE"), 0)
    '                    ImportoBolletta = par.IfNull(myReader("IMPORTO_TOTALE"), 0)
    '                    'End If

    '                    '*******IMPORTO CONTABILE DELLA BOLLETTA DI MOROSITA**************
    '                    par.cmd.CommandText = "SELECT SUM(importo) AS BOL_MOROSITA " _
    '                                        & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA " _
    '                                        & "WHERE BOL_BOLLETTE.FL_ANNULLATA=0 AND BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA " _
    '                                        & "AND BOL_BOLLETTE_VOCI.ID_VOCE = T_VOCI_BOLLETTA.ID AND T_VOCI_BOLLETTA.COMPETENZA <> 3 " _
    '                                        & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC=" & par.IfNull(myReader("ID"), "")
    '                    myReader3 = par.cmd.ExecuteReader

    '                    If myReader3.Read Then
    '                        EmessoContabile = par.IfNull(myReader("IMPORTO_TOTALE"), 0) - par.IfNull(myReader3("BOL_MOROSITA"), 0)
    '                        TotContabile = TotContabile + EmessoContabile
    '                        testoTabella = testoTabella _
    '                        & "<td style='height: 19px;text-align:right'>" _
    '                        & "<span style='font-size: 8pt; font-family: Arial'>€." & Format((par.IfNull(EmessoContabile, 0)), "##,##0.00") & "</span></td>"
    '                    End If
    '                    myReader3.Close()


    '                Else
    '                    If par.IfNull(myReader("ID_TIPO"), 1) <> 5 Then
    '                        par.cmd.CommandText = "SELECT SUM(IMPORTO) AS IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "")
    '                    Else
    '                        par.cmd.CommandText = "SELECT SUM(IMPORTO) AS IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "") & " AND (ID_VOCE = 678 OR ID_VOCE = 407)"

    '                    End If

    '                    myReader2 = par.cmd.ExecuteReader

    '                    If myReader2.Read Then
    '                        testoTabella = testoTabella _
    '                        & "<td style='height: 19px;text-align:right'>" _
    '                        & "<span style='font-size: 8pt; font-family: Arial'>€." & Format((par.IfNull(myReader2("IMPORTO"), 0)), "##,##0.00") & "</span></td>"
    '                        TotImporto = TotImporto + par.IfNull(myReader2("IMPORTO"), 0)
    '                        ImportoBolletta = par.IfNull(myReader2("importo"), 0)
    '                    End If
    '                    testoTabella = testoTabella _
    '                    & "<td style='height: 19px;text-align:right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(0, "##,##0.00") & "</span></td>"
    '                    TotMorosita = TotMorosita + 0

    '                    '*******IMPORTO CONTABILE DETRATTE LE QUOTE SINDACALI**************
    '                    par.cmd.CommandText = "SELECT SUM(IMPORTO) AS QUOTE_SINDACALI FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE.FL_ANNULLATA=0 AND T_VOCI_BOLLETTA.ID= BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.COMPETENZA = 3 AND BOL_BOLLETTE.ID = " & par.IfNull(myReader("ID"), "0000000000") & " "
    '                    myReader3 = par.cmd.ExecuteReader
    '                    If myReader3.Read Then
    '                        EmessoContabile = par.IfNull(myReader2("IMPORTO"), 0) - par.IfNull(myReader3("QUOTE_SINDACALI"), 0)
    '                        TotContabile = TotContabile + EmessoContabile
    '                        testoTabella = testoTabella _
    '                        & "<td style='height: 19px;text-align:right'>" _
    '                        & "<span style='font-size: 8pt; font-family: Arial'>€." & Format((par.IfNull(EmessoContabile, 0)), "##,##0.00") & "</span></td>"
    '                    End If
    '                    myReader3.Close()
    '                    myReader2.Close()
    '                End If

    '                '**********ATTENZIONE, CHIEDERE CO
    '                par.cmd.CommandText = "SELECT SUM(IMPORTO_PAGATO) AS PAGATO FROM SISCOM_MI.BOL_BOLLETTE WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) AND ID = " & par.IfNull(myReader("ID"), "")

    '                myReader3 = par.cmd.ExecuteReader
    '                If myReader3.Read Then

    '                    If par.IfNull(myReader("DATA_PAGAMENTO"), "") <> "" Then

    '                        If myReader("IMPORTO_TOTALE") <> par.IfNull(myReader3("PAGATO"), 0) Then
    '                            testoTabella = testoTabella _
    '                            & "<td style='height: 19px;text-align:right'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial;color: #c30307'>€." & Format((par.IfNull(myReader3("PAGATO"), 0)), "##,##0.00") & "</span></td>"
    '                        Else
    '                            testoTabella = testoTabella _
    '                            & "<td style='height: 19px;text-align:right'>" _
    '                            & "<span style='font-size: 8pt; font-family: Arial'>€." & Format((par.IfNull(myReader3("PAGATO"), 0)), "##,##0.00") & "</span></td>"
    '                        End If
    '                    Else
    '                        testoTabella = testoTabella _
    '                        & "<td style='height: 19px;text-align:right'>" _
    '                        & "<span style='font-size: 8pt; font-family: Arial;color: #c30307'>€." & Format(0, "##,##0.00") & "</span></td>"

    '                    End If
    '                    If par.IfNull(myReader("ID_TIPO"), 0) <> 4 And par.IfNull(myReader3("PAGATO"), 0) > 0 Then
    '                        TotPagato = TotPagato + par.IfNull(myReader3("PAGATO"), 0)
    '                    ElseIf par.IfNull(myReader("ID_TIPO"), 0) = 4 And par.IfNull(myReader3("PAGATO"), 0) > 0 Then
    '                        TotPagato = TotPagato + EmessoContabile
    '                    End If
    '                Else
    '                    testoTabella = testoTabella _
    '                    & "<td style='height: 19px;text-align:right'>" _
    '                    & "<span style='font-size: 8pt; font-family: Arial;color: #c30307'>€." & Format(0, "##,##0.00") & "</span></td>"

    '                End If
    '                myReader3.Close()

    '                Dim GiorniRitardo As String = 0

    '                If ImportoBolletta > 0 Then
    '                    If par.IfNull(myReader("DATA_PAGAMENTO"), "") <> "" Then
    '                        'DIFFERENZA FRA DATA SCADENZA E DATA PAGAMENTE
    '                        GiorniRitardo = DateDiff(DateInterval.Day, CDate(par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))), CDate(par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), ""))))
    '                    Else
    '                        'DIFFERENZA FRA DATA SCADENZA E OGGI
    '                        GiorniRitardo = DateDiff(DateInterval.Day, CDate(par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))), Date.Now)
    '                    End If
    '                    'Se giorni di ritardo è negativo allroa non lo faccio apparire proprio, per richiesta di Lorenzo
    '                Else
    '                    GiorniRitardo = -1
    '                End If

    '                If GiorniRitardo < 0 Then
    '                    GiorniRitardo = ""
    '                End If
    '                If par.IfNull(myReader("FL_ANNULLATA"), 0) <> 0 Then
    '                    GiorniRitardo = "ANNULLATA"
    '                End If


    '                testoTabella = testoTabella _
    '                & "<td style='height: 19px;text-align:right'>" _
    '                & "<span style='font-size: 8pt; font-family: Arial'>" & GiorniRitardo & "</span></td>"

    '                If COLORE = "#FFFFFF" Then
    '                    COLORE = "#E6E6E6"
    '                Else
    '                    COLORE = "#FFFFFF"
    '                End If
    '            Loop

    '            myReader.Close()
    '            '*********************CHIUSURA CONNESSIONE**********************
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '            testoTabella = testoTabella _
    '            & "<tr>" _
    '            & "<td style='height: 19px'>" _
    '            & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    '            & "<td style='height: 19px'>" _
    '            & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE</strong></span></td>" _
    '            & "<td style='height: 19px'>" _
    '            & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    '            & "<td style='height: 19px'>" _
    '            & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    '            & "<td style='height: 19px'>" _
    '            & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    '            & "<td style='height: 19px'>" _
    '            & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    '            & "<td style='height: 19px'>" _
    '            & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    '            & "<td style='height: 19px'>" _
    '            & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    '            & "<td style='height: 19px; text-align:right'>" _
    '            & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotImporto, "##,##0.00") & "</strong></span></td>" _
    '            & "<td style='height: 19px; text-align:right'>" _
    '            & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotMorosita, "##,##0.00") & "</strong></span></td>" _
    '            & "<td style='height: 19px; text-align:right'>" _
    '            & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotContabile, "##,##0.00") & "</strong></span></td>" _
    '            & "<td style='height: 19px; text-align:right'>" _
    '            & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotPagato, "##,##0.00") & "</strong></span></td>" _
    '            & "<td style='height: 19px'>" _
    '            & "</td>" _
    '            & "<td style='height: 19px'>" _
    '            & "</td>" _
    '            & "</tr>"

    '            Me.TBL_DETTAGLIO_SALDO.Text = testoTabella & "</table>"
    '        Else
    '            Response.Write("<SCRIPT>alert('Impossibile trovare informazioni sulle bollette di questo contratto!');</SCRIPT>")
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '    End Try
    'End Sub
End Class
