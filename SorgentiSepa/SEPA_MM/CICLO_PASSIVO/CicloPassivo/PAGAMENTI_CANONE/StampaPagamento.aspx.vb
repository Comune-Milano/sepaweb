Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports System.Math

Partial Class StampaPagamento
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sValorePagamento As String
    Public sProgrammaChimante As String

    Dim sUnita(19) As String
    Dim sDecina(9) As String

    Public importoPRE, oneriPRE, risultato1PRE, astaPRE, risultato2PRE, ritenutaPRE, ritenutaNoIvaT, risultato3PRE, ivaPRE, risultato4PRE, risultatoImponibilePRE As Decimal

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim sProgrammaChimante As String = ""
        Dim sStr1 As String

        Dim perc_sconto, perc_iva, perc_oneri As Decimal
        Dim oneri, asta, iva, ritenuta, risultato1, risultato2, risultato3, risultato4, risultato4Tot, risultato3Tot, importoDaPagare As Decimal

        Dim FlagConnessione As String = False

        Try


            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If
            sValorePagamento = Request.QueryString("PAG")
            sProgrammaChimante = Request.QueryString("CHIAMANTE")
            Dim TipoAllegato As String = par.getIdOggettoAllegatiWs("Ordine")
            Dim descrizione As String = "Stampa pagamento"
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA PAGAMENTO DI SISTEMA") & "," & par.getIdOggettoTipoAllegatiWs("STAMPA RIELABORAZIONE PAGAMENTO DI SISTEMA")
            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO IN ( " & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato & " AND ID_OGGETTO = " & sValorePagamento & "Order BY ID DESC"
            Dim nome As String = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
            If String.IsNullOrEmpty(nome) Then


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where  ID=" & sValorePagamento
                myReader1 = par.cmd.ExecuteReader

                If myReader1.Read Then
                    If par.IfNull(myReader1("DATA_STAMPA"), "") = "" Then
                        myReader1.Close()

                        'UPDATE PAGAMENTI
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "update SISCOM_MI.PAGAMENTI set ID_STATO=1, DATA_STAMPA=" & Format(Now, "yyyyMMdd") & " where ID=" & sValorePagamento
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                        Response.Write("<script>alert('Il pagamento è stato emesso e storicizzato!');</script>")
                    Else
                        myReader1.Close()
                    End If
                Else
                    myReader1.Close()
                End If


                'UPDATE PRENOTAZIONI
                'par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=2  where ID_PAGAMENTO=" & sValorePagamento
                'par.cmd.ExecuteNonQuery()
                'par.cmd.CommandText = ""


                'UPDATE EVENTI PAGAMENTI
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & sValorePagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','Stampa Mandato di Pagamento')"
                par.cmd.ExecuteNonQuery()

                'Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloPagamento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloPagamentoMANU2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()
                contenuto = Replace(contenuto, "$annobp$", par.AnnoBPPag(sValorePagamento))


                'PAGAMENTI.IMPORTO_NO_IVA
                '& " PF_VOCI.CODICE as ""COD_VOCE"",PF_VOCI.DESCRIZIONE as ""DESC_VOCE"", "
                '& "   and  PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID (+) " _
                '     & "   and  PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID (+) "
                ',SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_IMPORTO
                sStr1 = "select PAGAMENTI.ANNO,PAGAMENTI.PROGR,PAGAMENTI.PROGR_APPALTO,PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.DATA_SAL,PAGAMENTI.DATA_STAMPA,PAGAMENTI.DESCRIZIONE," _
                 & " PAGAMENTI.IMPORTO_CONSUNTIVATO,PAGAMENTI.CONTO_CORRENTE," _
                 & " FORNITORI.RAGIONE_SOCIALE, FORNITORI.COGNOME, FORNITORI.NOME,FORNITORI.COD_FORNITORE,FORNITORI.ID as ID_FORNITORE," _
                 & " APPALTI.ID as ""ID_APPALTO"",APPALTI.NUM_REPERTORIO,APPALTI.DATA_REPERTORIO,APPALTI.DESCRIZIONE AS ""APPALTI_DESC"",APPALTI.CIG," _
                 & " APPALTI.FL_RIT_LEGGE , " _
                 & "(SELECT descrizione FROM siscom_mi.TIPO_MODALITA_PAG WHERE ID = pagamenti.id_tipo_modalita_pag) AS tipo_modalita,(SELECT descrizione FROM siscom_mi.TIPO_pagamento WHERE ID = pagamenti.id_tipo_pagamento) AS tipo_pag,pagamenti.data_scadenza " _
                 & " from SISCOM_MI.PAGAMENTI,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                 & " where  PAGAMENTI.ID=" & sValorePagamento _
                 & "   and  PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) " _
                 & "   and  PAGAMENTI.ID_APPALTO=APPALTI.ID (+) "

                ' ''sStr1 = "select PAGAMENTI.ANNO,PAGAMENTI.PROGR,PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.DATA_STAMPA,PAGAMENTI.DESCRIZIONE," _
                ' ''            & " PAGAMENTI.IMPORTO_CONSUNTIVATO,PAGAMENTI.CONTO_CORRENTE," _
                ' ''            & " FORNITORI.RAGIONE_SOCIALE, FORNITORI.COGNOME, FORNITORI.NOME,FORNITORI.COD_FORNITORE," _
                ' ''            & " APPALTI.ID as ""ID_APPALTO"",APPALTI.NUM_REPERTORIO,APPALTI.DATA_REPERTORIO,APPALTI.DESCRIZIONE AS ""APPALTI_DESC"",APPALTI.CIG," _
                ' ''            & " APPALTI.FL_RIT_LEGGE " _
                ' ''     & " from SISCOM_MI.PAGAMENTI,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                ' ''     & " where  PAGAMENTI.ID=" & sValorePagamento _
                ' ''     & "   and  PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) " _
                ' ''     & "   and  PAGAMENTI.ID_APPALTO=APPALTI.ID (+) "



                par.cmd.CommandText = sStr1
                myReader1 = par.cmd.ExecuteReader

                If myReader1.Read Then
                    'contenuto = Replace(contenuto, "$chiamante$", "") '"CONTRATTO:")

                    contenuto = Replace(contenuto, "$anno$", myReader1("ANNO"))
                    contenuto = Replace(contenuto, "$progr$", myReader1("PROGR"))
                    contenuto = Replace(contenuto, "$annoSAL$", myReader1("ANNO"))
                    contenuto = Replace(contenuto, "$progrSAL$", myReader1("PROGR_APPALTO"))


                    contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(par.IfNull(myReader1("DATA_EMISSIONE"), "-1")))
                    contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(par.IfNull(myReader1("DATA_STAMPA"), "")))

                    'contenuto = Replace(contenuto, "$dettagli_chiamante$", "") ' "N." & myReader1("NUM_REPERTORIO") & " del " & par.FormattaData(par.IfNull(myReader1("DATA_REPERTORIO"), "-1")) & " " & myReader1("APPALTI_DESC"))

                    contenuto = Replace(contenuto, "$contratto$", "N." & myReader1("NUM_REPERTORIO") & " del " & par.FormattaData(myReader1("DATA_REPERTORIO")) & " " & myReader1("APPALTI_DESC"))

                    contenuto = Replace(contenuto, "$CIG$", par.IfNull(myReader1("CIG"), ""))
                    contenuto = Replace(contenuto, "$conto_corrente$", par.IfNull(myReader1("CONTO_CORRENTE"), "12000X01"))
                    contenuto = Replace(contenuto, "$modalita$", par.IfNull(myReader1("tipo_modalita"), ""))
                    contenuto = Replace(contenuto, "$condizione$", par.IfNull(myReader1("tipo_pag"), ""))
                    contenuto = Replace(contenuto, "$datascadenza$", par.FormattaData(par.IfNull(myReader1("data_scadenza"), "")))
                    contenuto = Replace(contenuto, "$descrpag$", par.IfNull(myReader1("DESCRIZIONE"), ""))


                    'FORNITORI
                    Dim sFORNITORI As String = ""
                    If par.IfNull(myReader1("RAGIONE_SOCIALE"), "") = "" Then
                        If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                            sFORNITORI = par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), "")
                        Else
                            sFORNITORI = par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), "")
                        End If

                    Else
                        If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                            sFORNITORI = par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                        Else
                            sFORNITORI = par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                        End If
                    End If
                    contenuto = Replace(contenuto, "$fornitoreIntestazione$", sFORNITORI)
                    'INDIRIZZO FORNITORE
                    Dim sIndirizzoFornitore1 As String = ""
                    Dim sComuneFornitore1 As String = ""
                    par.cmd.CommandText = "select TIPO,INDIRIZZO,CIVICO,CAP,COMUNE " _
                                    & " from   SISCOM_MI.FORNITORI_INDIRIZZI" _
                                    & " where ID_FORNITORE=" & par.IfNull(myReader1("ID_FORNITORE"), 0)

                    Dim myReaderTT As Oracle.DataAccess.Client.OracleDataReader
                    myReaderTT = par.cmd.ExecuteReader
                    While myReaderTT.Read

                        sIndirizzoFornitore1 = par.IfNull(myReaderTT("TIPO"), "") _
                                        & " " & par.IfNull(myReaderTT("INDIRIZZO"), "") _
                                        & " " & par.IfNull(myReaderTT("CIVICO"), "")

                        sComuneFornitore1 = par.IfNull(myReaderTT("CAP"), "") _
                                        & " " & par.IfNull(myReaderTT("COMUNE"), "")

                    End While
                    myReaderTT.Close()
                    contenuto = Replace(contenuto, "$fornitore_indirizzo$", sIndirizzoFornitore1)
                    contenuto = Replace(contenuto, "$comuneIntestazione$", sComuneFornitore1)

                    'IBAN **************************************************
                    par.cmd.CommandText = "select IBAN from SISCOM_MI.FORNITORI_IBAN " _
                                   & " where ID in (select ID_IBAN from SISCOM_MI.APPALTI_IBAN where ID_APPALTO=" & par.IfNull(myReader1("ID_APPALTO"), 0) & ")"

                    Dim myReaderBP As Oracle.DataAccess.Client.OracleDataReader
                    myReaderBP = par.cmd.ExecuteReader

                    While myReaderBP.Read
                        sFORNITORI = sFORNITORI & "<br/>" & par.IfNull(myReaderBP("IBAN"), "")
                    End While
                    myReaderBP.Close()
                    contenuto = Replace(contenuto, "$fornitori$", sFORNITORI)
                    '*********************************************************
                    Dim id_voce As Integer = -1
                    par.cmd.CommandText = "select distinct(ID_VOCE_PF)  as ID_VOCE , " _
                                        & "(SELECT DISTINCT RTRIM (LTRIM (SUBSTR (inizio, 1, 4))) AS annoBp FROM siscom_mi.T_ESERCIZIO_FINANZIARIO  WHERE id IN " _
                                        & "(select id_esercizio_finanziario from siscom_mi.pf_main where id in (SELECT DISTINCT id_piano_finanziario FROM siscom_mi.pf_voci WHERE id = PRENOTAZIONI.ID_VOCE_PF))" _
                                        & ") AS ANNO " _
                                    & " from SISCOM_MI.PRENOTAZIONI where ID_PAGAMENTO=" & sValorePagamento

                    myReaderBP = par.cmd.ExecuteReader
                    If myReaderBP.Read Then
                        id_voce = par.IfNull(myReaderBP("ID_VOCE"), 0)
                    End If
                    myReaderBP.Close()

                    'RIEPILOGO SAL
                    Dim penale As Decimal = 0
                    'par.cmd.CommandText = "select PRENOTAZIONI.* " _
                    '               & " from   SISCOM_MI.PRENOTAZIONI" _
                    '               & " where PRENOTAZIONI.ID_PAGAMENTO=" & sValorePagamento
                    par.cmd.CommandText = "select PRENOTAZIONI.*,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                                          & " from   SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI_PENALI" _
                                          & " where ID_PAGAMENTO=" & sValorePagamento _
                                          & "   and ID_VOCE_PF_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO where ID_VOCE=" & id_voce & ")" _
                                          & "   and SISCOM_MI.PRENOTAZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_PRENOTAZIONE (+) "

                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                    myReader2 = par.cmd.ExecuteReader()
                    Dim idAppalto As String = "-1"
                    While myReader2.Read
                        CalcolaImporti(par.IfNull(myReader2("IMPORTO_APPROVATO"), 0), par.IfNull(myReader1("FL_RIT_LEGGE"), 0), par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO", par.IfNull(myReader2("PERC_IVA"), -1))
                        idAppalto = par.IfNull(myReader2("ID_APPALTO"), "-1")
                        penale = CDec(par.IfNull(myReader2("PENALE2"), "0"))
                    End While
                    myReader2.Close()


                    contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(importoPRE, 0), "", "##,##0.00"))

                    'modifica marco/pepep 05/01/2011
                    par.cmd.CommandText = "select rit_legge_ivata from siscom_mi.prenotazioni where id_pagamento = " & sValorePagamento
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim ritDb As Decimal
                    Dim A As Integer = 0
                    While lettore.Read
                        ritDb = par.IfNull(lettore("rit_legge_ivata"), 0)
                        If ritDb <> 0 Then
                            A = 1
                        End If
                    End While
                    lettore.Close()
                    If A <> 1 Then
                        ritenutaPRE = 0
                    End If
                    par.cmd.CommandText = "select TRIM(TO_CHAR(sum(anticipo_contrattuale),'9G999G999G999G999G990D99')) as anticipo_contrattuale " _
                                        & "from siscom_mi.prenotazioni " _
                                        & "where id_pagamento = " & sValorePagamento
                    Dim anticipoContrattuale As Decimal = par.IfNull(par.cmd.ExecuteScalar, 0)



                    Dim S2 As String = "<table style='width:100%;'>"
                    S2 = S2 & "<tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE"), "") & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A lordo compresi oneri €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato4PRE, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Oneri di sicurezza €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(oneriPRE, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A lordo esclusi oneri €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato1PRE, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Ribasso d'asta €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(astaPRE, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A netto esclusi oneri €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato2PRE, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A netto compresi oneri €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato3PRE, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Ritenuta di legge 0,5% (con IVA) €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(ritenutaPRE, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Imponibile (al netto trattenute) €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultatoImponibilePRE, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>IVA €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(ivaPRE, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    Dim percIva As String = "0"
                    If anticipoContrattuale > 0 Then
                        Dim totaleIVA As String = ""
                        par.cmd.CommandText = "SELECT nvl(PERC_IVA,0) as PERC_IVA FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & idAppalto & ")"
                        Dim lettore1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore1.Read Then
                            percIva = par.IfNull(lettore1("PERC_IVA"), "0")
                        End If
                        lettore1.Close()
                        totaleIVA = anticipoContrattuale * CDec(percIva) / 100
                        S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                        S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Recupero anticipazione contrattuale €</td>"
                        S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & anticipoContrattuale & "</td>"
                        S2 = S2 & "</tr><tr>"
                        S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                        S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Importo IVA recupero (" & IsNumFormat(percIva, "", "##,##0") & "%) €</td>"
                        S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(totaleIVA, "", "##,##0.00") & "</td>"
                        S2 = S2 & "</tr><tr>"
                    End If
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A netto compresi oneri e IVA €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(importoPRE, "", "##,##0.00") & "</td>"

                    If penale > 0 Then
                        S2 = S2 & "</tr><tr>"
                        S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                        S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Penale €</td>"
                        S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(-penale, "", "##,##0.00") & "</td>"
                        '    S2 = S2 & "</tr><tr>"
                    End If


                    If anticipoContrattuale > 0 Then

                        S2 = S2 & "</tr><tr>"
                        S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                        S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Totale compreso IVA €</td>"
                        S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(importoPRE - anticipoContrattuale, "", "##,##0.00") & "</td>"
                    ElseIf penale > 0 Then
                        S2 = S2 & "</tr><tr>"
                        S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                        S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Totale compreso IVA €</td>"
                        S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(importoPRE - penale, "", "##,##0.00") & "</td>"
                    End If

                    S2 = S2 & "</tr></table>"

                    Dim T As String = "<table style='width:100%;'>"
                    T = T & "<tr>"
                    T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & S2 & "</td>"
                    T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'></td>"

                    T = T & "</tr></table>"

                    contenuto = Replace(contenuto, "$dettagli$", T)
                    ''****************************
                    Dim TestoGrigliaINTESTAZIONE As String = "" ' "<p style='page-break-after: always'>&nbsp;</p>"

                    TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<table cellspacing='0' style='width:50%; border: 1px solid black;border-collapse: collapse;' >"
                    TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >A netto compresi oneri</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px '  >€ " & IsNumFormat(risultato3PRE, "", "##,##0.00") & "</td>" _
                                              & "</tr>" _
                                              & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Ritenuta di legge 0,5 % (senza IVA)</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px '  >€ " & IsNumFormat(ritenutaNoIvaT, "", "##,##0.00") & "</td>" _
                                              & "</tr>" _
                                              & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Imponibile (al netto delle trattenute) </td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px '  >€ " & IsNumFormat(risultatoImponibilePRE, "", "##,##0.00") & "</td>" _
                                              & "</tr>"
                    'If rimborsoT > 0 Then
                    '    TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                    '                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Imponibile rimborsi</td>" _
                    '                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >€ " & IsNumFormat(imponibile_rimborso, "", "##,##0.00") & "</td>" _
                    '                              & "</tr>"
                    'End If
                    If penale > 0 Then
                        TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Penale</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >€ " & IsNumFormat(-penale, "", "##,##0.00") & "</td>" _
                                              & "</tr>"
                    End If

                    TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "</table>"
                    contenuto = Replace(contenuto, "$grigliaIntestazione$", TestoGrigliaINTESTAZIONE)

                    '*********** DETTAGLIO GRIGLIA VOCI BP
                    'TestoPagina = TestoPagina & "</table>"
                    Dim TestoGrigliaBP As String = "" '"<p style='page-break-before: always'>&nbsp;</p>"

                    TestoGrigliaBP = TestoGrigliaBP & "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
                    TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 10pt; font-weight: bold'>" _
                                              & "<td align='left' style='border-bottom-style: dashed; width: 10%; border-bottom-width: 1px; border-bottom-color: #000000'>CODICE BP</td>" _
                                              & "<td align='left' style='border-bottom-style: dashed; width: 10%; border-bottom-width: 1px; border-bottom-color: #000000'>ANNO BP</td>" _
                                              & "<td align='left' style='border-bottom-style: dashed; width: 60%; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE BP</td>" _
                                              & "<td align='right' style='border-bottom-style: dashed; width: 20%; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                              & "</tr>"


                    'ESTRAGGO TUTTE LE VOCI BP DIVERSE
                    risultato4Tot = 0

                    'par.cmd.CommandText = "select distinct(ID_VOCE) from SISCOM_MI.PF_VOCI_IMPORTO " _
                    '                  & " where ID in (select ID_VOCE_PF_IMPORTO from SISCOM_MI.PRENOTAZIONI where ID_PAGAMENTO=" & sValorePagamento & ")"

                    par.cmd.CommandText = "select distinct(ID_VOCE_PF)  as ID_VOCE , " _
                                            & "(SELECT DISTINCT RTRIM (LTRIM (SUBSTR (inizio, 1, 4))) AS annoBp FROM siscom_mi.T_ESERCIZIO_FINANZIARIO  WHERE id IN " _
                                            & "(select id_esercizio_finanziario from siscom_mi.pf_main where id in (SELECT DISTINCT id_piano_finanziario FROM siscom_mi.pf_voci WHERE id = PRENOTAZIONI.ID_VOCE_PF))" _
                                            & ") AS ANNO " _
                                        & " from SISCOM_MI.PRENOTAZIONI where ID_PAGAMENTO=" & sValorePagamento


                    myReaderBP = par.cmd.ExecuteReader

                    While myReaderBP.Read
                        'X OGNI TIPO DI VOCE
                        par.cmd.CommandText = "select PRENOTAZIONI.* " _
                                          & " from   SISCOM_MI.PRENOTAZIONI " _
                                          & " where ID_PAGAMENTO=" & sValorePagamento _
                                          & "   and ID_VOCE_PF=" & par.IfNull(myReaderBP("ID_VOCE"), 0)

                        'par.cmd.CommandText = "select PRENOTAZIONI.*,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                        '                  & " from   SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI_PENALI" _
                        '                  & " where ID_PAGAMENTO=" & sValorePagamento _
                        '                  & "   and ID_VOCE_PF_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO where ID_VOCE=" & par.IfNull(myReaderBP("ID_VOCE"), 0) & ")" _
                        '                  & "   and SISCOM_MI.PRENOTAZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_PRENOTAZIONE (+) "


                        Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader
                        myReaderB = par.cmd.ExecuteReader

                        While myReaderB.Read
                            '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                            If par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0) > 0 Then

                                If par.IfNull(myReaderB("ID_VOCE_PF_IMPORTO"), 0) <> 0 Then
                                    sStr1 = "select APPALTI_LOTTI_SERVIZI.*,APPALTI.FL_RIT_LEGGE " _
                                    & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI " _
                                    & "  where APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & par.IfNull(myReaderB("ID_VOCE_PF_IMPORTO"), 0) _
                                    & "  and   APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & par.IfNull(myReaderB("ID_APPALTO"), 0) _
                                    & "  and   APPALTI.ID=" & par.IfNull(myReaderB("ID_APPALTO"), 0)


                                    Dim myReaderB2 As Oracle.DataAccess.Client.OracleDataReader
                                    par.cmd.CommandText = sStr1
                                    myReaderB2 = par.cmd.ExecuteReader

                                    If myReaderB2.Read Then

                                        'perc_oneri = par.IfNull(myReaderB2("PERC_ONERI_SIC_CAN"), 0)

                                        'D3= D1(-(D1 * D2 / 100))
                                        'D9= D4*100/D3
                                        Dim D3 As Decimal = 0
                                        D3 = par.IfNull(myReaderB2("IMPORTO_CANONE"), 0) - ((par.IfNull(myReaderB2("IMPORTO_CANONE"), 0) * par.IfNull(myReaderB2("SCONTO_CANONE"), 0)) / 100)

                                        perc_oneri = (par.IfNull(myReaderB2("ONERI_SICUREZZA_CANONE"), 0) * 100) / D3


                                        perc_sconto = par.IfNull(myReaderB2("SCONTO_CANONE"), 0)

                                        If par.IfNull(myReaderB("PERC_IVA"), -1) = -1 Then
                                            perc_iva = par.IfNull(myReaderB2("IVA_CANONE"), 0)
                                        Else
                                            perc_iva = par.IfNull(myReaderB("PERC_IVA"), 0)
                                        End If

                                        risultato3 = ((par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0) * 100) / (100 + perc_iva))

                                        'ALIQUOTA 0,5% sul NETTO senza ONERI vedi sopra
                                        If par.IfNull(myReaderB2("FL_RIT_LEGGE"), 0) = 1 Then
                                            ritenuta = (risultato3 * 0.5) / 100
                                            ritenuta = Round(ritenuta, 2)
                                            'ritenuta_IVATA = ritenuta + ((ritenuta * perc_iva) / 100)
                                        Else
                                            ritenuta = 0
                                        End If

                                        oneri = risultato3 - (risultato3 * 100 / (100 + perc_oneri))        'Oneri di Sicurezza (txtOneri)
                                        oneri = Round(oneri, 2)

                                        risultato2 = risultato3 - oneri                                     'A netto esclusi oneri (txtNetto) OK

                                        asta = (risultato2 / ((100 - perc_sconto) / 100)) - risultato2      'Ribasso d'asta  (txtRibassoAsta) OK
                                        asta = Round(asta, 2)

                                        risultato1 = risultato2 + asta                                      'A lordo esclusi oneri (txtOneriImporto)

                                        risultato4 = risultato1 + oneri                                     'A lordo compresi oneri (txtImporto)

                                        importoDaPagare = risultato3 - ritenuta + (((risultato3 - ritenuta) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
                                        importoDaPagare = Round(importoDaPagare, 2)

                                        'risultato3Tot = risultato3Tot + par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0)
                                        risultato3Tot = risultato3Tot + importoDaPagare


                                        iva = importoDaPagare - ((importoDaPagare * 100) / (100 + perc_iva))            'IVA
                                        iva = Round(iva, 2)



                                        Dim myReaderB3 As Oracle.DataAccess.Client.OracleDataReader
                                        par.cmd.CommandText = "select CODICE,DESCRIZIONE from SISCOM_MI.PF_VOCI where ID=" & par.IfNull(myReaderBP("ID_VOCE"), 0)
                                        myReaderB3 = par.cmd.ExecuteReader

                                        If myReaderB3.Read Then
                                            TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("CODICE"), "") & "</td>" _
                                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderBP("ANNO"), "") & "</td>" _
                                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("DESCRIZIONE"), "") & "</td>" _
                                                                        & "<td align='right'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & IsNumFormat(importoDaPagare, "", "##,##0.00") & "</td>" _
                                                                        & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                                                                        & "</tr>"

                                        End If
                                        myReaderB3.Close()

                                    End If
                                    myReaderB2.Close()
                                Else

                                    Dim myReaderB3 As Oracle.DataAccess.Client.OracleDataReader
                                    par.cmd.CommandText = "select CODICE,DESCRIZIONE from SISCOM_MI.PF_VOCI where ID=" & par.IfNull(myReaderB("ID_VOCE_PF"), 0)
                                    myReaderB3 = par.cmd.ExecuteReader

                                    If myReaderB3.Read Then
                                        risultato3Tot = risultato3Tot + par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0)
                                        TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("CODICE"), "") & "</td>" _
                                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("DESCRIZIONE"), "") & "</td>" _
                                                                        & "<td align='right'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & IsNumFormat(par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0), "", "##,##0.00") & "</td>" _
                                                                        & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                                                                        & "</tr>"

                                    End If
                                    myReaderB3.Close()

                                End If

                            End If
                        End While
                        myReaderB.Close()

                    End While
                    myReaderBP.Close()
                    If anticipoContrattuale <> 0 Then


                        TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                  & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                  & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                  & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                  & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " RECUPERO ANTICIPAZIONE CONTRATTUALE   : " & IsNumFormat(anticipoContrattuale + (anticipoContrattuale * percIva / 100), "", "##,##0.00") & "</td>" _
                                  & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                  & "</tr>"
                    End If


                    TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                              & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                              & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                              & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                  & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " TOTALE   : " & IsNumFormat(risultato3Tot - anticipoContrattuale, "", "##,##0.00") & "</td>" _
                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                              & "</tr>"

                    contenuto = Replace(contenuto, "$grigliaBP$", TestoGrigliaBP)
                    '********************************


                    '*****************FINE SCRITTURA DETTAGLI
                    contenuto = Replace(contenuto, "$imp_letterale$", "") 'NumeroInLettere(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)))

                    'contenuto = Replace(contenuto, "$dettaglio$", "SPESE")

                    'contenuto = Replace(contenuto, "$cod_capitolo$", "") 'par.IfNull(myReader1("COD_VOCE"), ""))
                    'contenuto = Replace(contenuto, "$voce_pf$", "") ' par.IfNull(myReader1("DESC_VOCE"), ""))
                    'contenuto = Replace(contenuto, "$finanziamento$", "Gestione Comune di Milano")

                    par.cmd.CommandText = "SELECT INITCAP(COGNOME||' '||NOME) FROM SEPA.OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE")
                    Dim lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim chiamante2 As String = ""
                    If lett.Read Then
                        chiamante2 = par.IfNull(lett(0), "")
                    End If
                    lett.Close()
                    contenuto = Replace(contenuto, "$chiamante2$", chiamante2)
                    par.cmd.CommandText = "SELECT INITCAP(GESTORI_ORDINI.DESCRIZIONE) FROM SISCOM_MI.GESTORI_ORDINI,SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI " _
                            & " WHERE APPALTI.ID_gESTORE_ORDINI=GESTORI_ORDINI.ID AND PAGAMENTI.ID_APPALTO=APPALTI.ID AND PAGAMENTI.ID=" & sValorePagamento
                    lett = par.cmd.ExecuteReader
                    Dim gestore As String = ""
                    If lett.Read Then
                        gestore = par.IfNull(lett(0), "")
                    End If
                    lett.Close()
                    contenuto = Replace(contenuto, "$proponente$", gestore)
                    contenuto = Replace(contenuto, "$grigliaM$", "")


                End If
                myReader1.Close()




                Dim url As String = Server.MapPath("..\..\..\FileTemp\")
                Dim pdfConverter1 As PdfConverter = New PdfConverter

                Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
                If Licenza <> "" Then
                    pdfConverter1.LicenseKey = Licenza
                End If

                pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
                pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                pdfConverter1.PdfDocumentOptions.ShowFooter = False
                pdfConverter1.PdfDocumentOptions.LeftMargin = 30
                pdfConverter1.PdfDocumentOptions.RightMargin = 30
                pdfConverter1.PdfDocumentOptions.TopMargin = 30
                pdfConverter1.PdfDocumentOptions.BottomMargin = 10
                pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                pdfConverter1.PdfFooterOptions.FooterText = ("")
                pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
                pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                pdfConverter1.PdfFooterOptions.PageNumberText = ""
                pdfConverter1.PdfFooterOptions.ShowPageNumber = False

                Dim nomefile As String = "AttPagamento_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
                nomefile = par.NomeFileManut("CDP", sValorePagamento) & ".pdf"
                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\..\NuoveImm\"))

                'Dim i As Integer = 0
                'For i = 0 To 10000
                'Next
                'GIANCARLO 16-02-2017
                'inserimento della stampa cdp negli allegati

                idTipoOggetto = par.getIdOggettoTipoAllegatiWs("STAMPA RIELABORAZIONE PAGAMENTO DI SISTEMA")


                par.cmd.CommandText = "SELECT ID_CARTELLA FROM SISCOM_MI.ALLEGATI_WS_OGGETTI WHERE ID = " & TipoAllegato
                Dim idCartella As String = par.IfNull(par.cmd.ExecuteScalar.ToString, "")
                par.AllegaDocumentoWS(Server.MapPath("../../../FileTemp/" & nomefile), nomefile, idCartella, descrizione, idTipoOggetto, TipoAllegato, sValorePagamento, "../../../ALLEGATI/ORDINI/")

                If sProgrammaChimante = "RICERCHE" Then
                    Response.Write("<script>window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');</script>")
                Else
                    Response.Write("<script>window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');self.close();</script>")
                End If
            Else
                Response.Write("<script>window.open('../../../ALLEGATI/ORDINI/" & nome & "','SAL','');self.close();</script>")

            End If
            '*********************CHIUSURA CONNESSIONE**********************
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If



        Catch ex As Exception

            '*********************CHIUSURA CONNESSIONE**********************
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub



    Function NumeroInLettere(ByVal Numero As String) As String

        '************************
        'Gestisce la virgola
        '************************
        Dim PosVirg As Integer
        Dim Lettere As String

        Numero$ = ChangeStr(Numero$, ".", "")
        PosVirg% = InStr(Numero$, ",")

        If PosVirg% Then
            Lettere$ = NumInLet(Mid(Numero$, 1, Len(Numero) + PosVirg% - 1))
            Lettere$ = Lettere$ & "\" & Format(CInt(Mid(Numero$, PosVirg% + 1, Len(Numero$))), "00")
        Else
            Lettere$ = NumInLet(CDbl(Numero$))
        End If

        NumeroInLettere = Lettere$

    End Function

    Private Function NumInLet(ByVal N As Double) As String

        '************************************************
        'inizializzo i due arry di numeri
        '************************************************
        SetNumeri()

        Dim ValT As Double     'Valore Temporaneo per la conversione
        Dim iCent As Integer    'Valore su cui calcolare le centinaia
        Dim L As String     'Importo in Lettere

        NumInLet = "zero"

        If N = 0 Then Exit Function

        ValT = N
        L = ""

        'miliardi
        iCent = Int(ValT / 1000000000.0#)
        If iCent Then
            If iCent = 1 Then
                L = "unmiliardo"
            Else
                L = LCent(iCent) + "miliardi"
            End If
            ValT = ValT - CDbl(iCent) * 1000000000.0#
        End If

        'milioni
        iCent = Int(ValT / 1000000.0#)
        If iCent Then
            If iCent = 1 Then
                L = L + "unmilione"
            Else
                L = L + LCent(iCent) + "milioni"
            End If
            ValT = ValT - CDbl(iCent) * 1000000.0#
        End If

        'miliaia
        iCent = Int(ValT / 1000)
        If iCent Then
            If iCent = 1 Then
                L = L + "mille"
            Else
                L = L + LCent(iCent) + "mila"
            End If
            ValT = ValT - CDbl(iCent) * 1000
        End If

        ''centinaia
        'If ValT Then
        '    L = L + LCent(CInt(ValT))
        'End If
        If ValT Then
            L = L + LCent(Fix(CDbl(ValT)))
        End If

        NumInLet = L

    End Function

    Function LCent(ByVal N As Integer) As String

        ' Ritorna xx% (1/999) convertito in lettere
        Dim Numero As String
        Dim Lettere As String
        Dim Centinaia As Integer
        Dim Decine As Integer
        Dim x As Integer
        Dim Unita As Integer
        Dim sDec As String

        Numero$ = Format(N, "000")

        Lettere$ = ""
        Centinaia% = Val(Left$(Numero$, 1))
        If Centinaia% Then
            If Centinaia% > 1 Then
                Lettere = sUnita(Centinaia%)
            End If
            Lettere = Lettere + "cento"
        End If

        Decine% = (N Mod 100)
        If Decine% Then
            Select Case Decine%
                Case Is >= 20                               'Decine
                    sDec = sDecina(Val(Mid$(Numero$, 2, 1)))
                    x% = Len(sDec)
                    Unita% = Val(Right$(Numero$, 1))          'Unita
                    If Unita% = 1 Or Unita% = 8 Then x% = x% - 1
                    Lettere$ = Lettere$ & Left(sDec, x%) & sUnita(Unita%)    'Tolgo l'ultima lettera della decina per i
                Case Else
                    Lettere$ = Lettere$ + sUnita(Decine)
            End Select
        End If

        LCent$ = Lettere$

    End Function


    Sub SetNumeri()

        '************************************************
        ' Stringhe per traslitterazione numeri
        '************************************************
        sUnita(1) = "uno"
        sUnita(2) = "due"
        sUnita(3) = "tre"
        sUnita(4) = "quattro"
        sUnita(5) = "cinque"
        sUnita(6) = "sei"
        sUnita(7) = "sette"
        sUnita(8) = "otto"
        sUnita(9) = "nove"
        sUnita(10) = "dieci"
        sUnita(11) = "undici"
        sUnita(12) = "dodici"
        sUnita(13) = "tredici"
        sUnita(14) = "quattordici"
        sUnita(15) = "quindici"
        sUnita(16) = "sedici"
        sUnita(17) = "diciassette"
        sUnita(18) = "diciotto"
        sUnita(19) = "diciannove"

        sDecina(1) = "dieci"
        sDecina(2) = "venti"
        sDecina(3) = "trenta"
        sDecina(4) = "quaranta"
        sDecina(5) = "cinquanta"
        sDecina(6) = "sessanta"
        sDecina(7) = "settanta"
        sDecina(8) = "ottanta"
        sDecina(9) = "novanta"

    End Sub

    '*********************************************************************
    '                ChangeStr - da usare con versioni minori del Vb6
    '
    'Input  = Stringa                           -->Da convertire
    '         Lettera da sostituire             -->Da convertire
    '         Nuova lettera da rimpiazzare      -->Da convertire
    '
    'Ouput  = Stringa rimpiazzata
    '
    '*********************************************************************
    Function ChangeStr(ByRef sBuffer As String, ByRef OldChar As String,
                       ByRef NewChar As String) As String

        Dim TmpBuf As String
        Dim p As Integer

        On Error GoTo ErrChangeStr

        ChangeStr$ = ""   'Default Error

        TmpBuf$ = sBuffer$
        p% = InStr(TmpBuf$, OldChar$)
        Do While p > 0
            TmpBuf$ = Left$(TmpBuf$, p% - 1) + NewChar$ + Mid$(TmpBuf$, p% + Len(OldChar$))
            p% = InStr(p% + Len(NewChar$), TmpBuf$, OldChar$)
        Loop
        ChangeStr$ = TmpBuf$

        Exit Function

ErrChangeStr:
        ChangeStr$ = ""

    End Function


    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function



    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function


    Sub CalcolaImporti(ByVal importo As Decimal, ByVal fl_ritenuta As Integer, ByVal Id_Voce As Long, ByVal Id_Appalto As Long, ByVal Tipo As String, ByVal PERC_IVA_PRENOTAZIONI As Decimal)

        Dim sStr1 As String
        Dim perc_sconto, perc_iva As Decimal
        Dim oneri, asta, iva, ritenuta, risultato1, ritenuta_IVATA, risultato2, risultato3, risultato4 As Decimal

        Dim perc_oneri, importoDaPagare As Decimal
        Dim FlagConnessione As Boolean

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If



            sStr1 = "select APPALTI_LOTTI_SERVIZI.* " _
                & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                & "  where ID_PF_VOCE_IMPORTO=" & Id_Voce _
                & "  and   ID_APPALTO=" & Id_Appalto

            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            myReaderT = par.cmd.ExecuteReader

            If myReaderT.Read Then

                'perc_oneri = par.IfNull(myReaderT("PERC_ONERI_SIC_CAN"), 0)

                'D3= D1(-(D1 * D2 / 100))
                'D9= D4*100/D3
                Dim D3 As Decimal = 0
                D3 = par.IfNull(myReaderT("IMPORTO_CANONE"), 0) - ((par.IfNull(myReaderT("IMPORTO_CANONE"), 0) * par.IfNull(myReaderT("SCONTO_CANONE"), 0)) / 100)

                perc_oneri = (par.IfNull(myReaderT("ONERI_SICUREZZA_CANONE"), 0) * 100) / D3

                perc_sconto = par.IfNull(myReaderT("SCONTO_CANONE"), 0)

                If PERC_IVA_PRENOTAZIONI = -1 Then
                    perc_iva = par.IfNull(myReaderT("IVA_CANONE"), 0)
                Else
                    perc_iva = PERC_IVA_PRENOTAZIONI
                End If

            End If
            myReaderT.Close()

            risultato3 = ((importo * 100) / (100 + perc_iva))               'A netto compresi oneri (o Imponibile ) txtNettoOneri OK
            'risultato3 = Round(risultato3, 2)
            risultato3PRE = risultato3PRE + risultato3


            'ALIQUOTA 0,5% 
            If par.IfNull(fl_ritenuta, 0) = 1 Then
                'X=(G*100)/(100+PERC_ONERI)
                'F=(X*0,5)/(100-0,5)

                'ritenuta = ((risultato3 * 100) / (100 + perc_oneri))
                'ritenuta = (ritenuta * 0.5) / (100 - 0.5)

                ritenuta = (risultato3 * 0.5) / 100
                ritenuta = Round(ritenuta, 2)
                ritenuta_IVATA = ritenuta + ((ritenuta * perc_iva) / 100)
                ritenuta_IVATA = Round(ritenuta_IVATA, 2)
            Else
                ritenuta = 0
            End If
            ritenutaPRE = ritenutaPRE + ritenuta_IVATA                                'Ritenuta di legge 0,5% txtRitenuta Ok
            ritenutaNoIvaT = ritenutaNoIvaT + ritenuta
            'e-mail del 13/07/2011 'MODIFICA (imponibile che appariva prima - la ritenuta senza iva) diventato "Imponibile netto trattenute"
            risultatoImponibilePRE = risultatoImponibilePRE + risultato3 - ritenuta


            oneri = risultato3 - (risultato3 * 100 / (100 + perc_oneri))        'Oneri di Sicurezza (txtOneri)
            'oneri = Round(oneri, 2)
            oneriPRE = oneriPRE + oneri

            risultato2 = risultato3 - oneri                                     'A netto esclusi oneri (txtNetto) OK
            risultato2PRE = risultato2PRE + risultato2

            asta = (risultato2 / ((100 - perc_sconto) / 100)) - risultato2      'Ribasso d'asta  (txtRibassoAsta) OK
            'asta = Round(asta, 2)
            astaPRE = astaPRE + asta

            risultato1 = risultato2 + asta                                      'A lordo esclusi oneri (txtOneriImporto)
            risultato1PRE = risultato1PRE + risultato1

            risultato4 = risultato1 + oneri                                     'A lordo compresi oneri (txtImporto)
            risultato4PRE = risultato4PRE + risultato4


            importoDaPagare = risultato3 - ritenuta + (((risultato3 - ritenuta) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
            importoDaPagare = Round(importoDaPagare, 2)
            importoPRE = importoPRE + importoDaPagare

            iva = importoDaPagare - ((importoDaPagare * 100) / (100 + perc_iva))            'IVA
            'iva = Round(iva, 2)
            ivaPRE = ivaPRE + iva

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

            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

End Class
