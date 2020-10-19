Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports System.Data.OleDb
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class VSA_Canone
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim risultato As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            Dim IMPORTO As Double = 0
            Dim NuovoTransitorio As Double = 0
            Dim LOCATIVO As String = ""
            Dim IDUNITA As Long = 0
            Dim cod As String = Request.QueryString("IDUNITA")
            Dim TotaleEmesso As Decimal = 0
            Dim TestoDaScrivere As String = ""
            Dim parte1 As String = ""
            Dim parte2 As String = ""
            Dim parte3 As String = ""
            Dim parte4 As String = ""
            Dim nuovaStrStampa0 As String = ""
            Dim nuovaStrStampa As String = ""
            Dim stringaPreRECA1 As String = ""
            Dim stringaPreRECA2 As String = ""
            Dim stringaPreRECA3 As String = ""
            Dim stringaPreRECA4 As String = ""


            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                If Len(cod) = 19 Then
                    cod = Mid(cod, 1, 17)
                End If

                Dim idcontr As Long = 0
                par.cmd.CommandText = "SELECT id FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & Request.QueryString("COD") & "'"
                Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderX1.Read Then
                    idcontr = myReaderX1("id")
                End If
                myReaderX1.Close()

                'par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & cod & "'"
                par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE WHERE UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & idcontr & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    IDUNITA = par.IfNull(myReader("ID"), 0)
                End If
                myReader.Close()

                Dim comunicazioni As String = ""
                Dim idc As Long = 0
                Dim ANNO_INIZIO As Integer = 0
                Dim ANNO_FINE As Integer = 0
                Dim PER_ANNI As Integer = 0
                Dim dataFine As String = ""
                Dim dataInizioValidita As String = ""
                Dim IDdich As String = ""
                Dim stringaPreRECA As String = ""
                Dim dataBollettUltima As String = ""
                Dim dataBollettazGener As String = ""
                Dim prossimaBoll As String = ""

                'Dim ANNICONGUAGLIO As Integer = 0
                If IDUNITA <> 0 Then

                    par.cmd.CommandText = "SELECT DATA_EVENTO,DATA_INIZIO_VAL,DATA_FINE_VAL,DICHIARAZIONI_VSA.ID_STATO,DICHIARAZIONI_VSA.ID,DOMANDE_BANDO_VSA.REDDITI_PRE_RECA,DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA FROM DICHIARAZIONI_VSA,DOMANDE_BANDO_VSA WHERE DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID=" & Request.QueryString("ID")
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        stringaPreRECA = par.IfNull(myReader("REDDITI_PRE_RECA"), "")
                        ANNO_INIZIO = CInt(Mid(par.IfNull(myReader("DATA_INIZIO_VAL"), Year(Now)), 1, 4))
                        idDichiarazione = par.IfNull(myReader("ID"), "")

                        'dataInizioValidita = par.IfNull(myReader("data_evento"), "")
                        dataInizioValidita = par.IfNull(myReader("DATA_INIZIO_VAL"), "")
                        IDdich = par.IfNull(myReader("ID"), "")

                        If par.IfNull(myReader("DATA_FINE_VAL"), "") = "29991231" Then
                            dataFine = Year(Now) & "1231"
                            'dataFine = "20120630"
                        Else
                            dataFine = par.IfNull(myReader("DATA_FINE_VAL"), "")
                        End If

                        PER_ANNI = DateDiff(DateInterval.Year, CDate(par.FormattaData(myReader("DATA_INIZIO_VAL"))), CDate(par.FormattaData(dataFine)))

                        If ANNO_INIZIO + PER_ANNI > Year(Now) Then
                            ANNO_FINE = Year(Now)
                        Else
                            ANNO_FINE = ANNO_INIZIO + PER_ANNI
                        End If

                        'PROROGA ASSEGNAZIONE TEMPORANEA
                        'If par.IfNull(myReader("ID_CAUSALE_DOMANDA"), "") = "27" Then
                        '    PER_ANNI = 0
                        'End If


                        'ANNICONGUAGLIO = ANNO_INIZIO + PER_ANNI

                        'If par.IfNull(myReader("ID_CAUSALE_DOMANDA"), "") = "28" Then
                        '    ANNICONGUAGLIO = Year(Now)
                        'End If

                        If myReader("ID_STATO") = "0" Then
                            Response.Write("<script>alert('Attenzione, la dichiarazione risulta INCOMPLETA. Verrà calcolato un canone in area di DECADENZA. Assicurarsi di inserire tutti i redditi, mettere in stato COMPLETA la dichiarazione e salvare prima di verificare il canone.');</script>")
                        End If
                    End If
                    myReader.Close()


                    '03/06/2014 CERCO APPLICAZIONI DI CANONE SUCCESSIVE ALLA VALIDITA' DELLA DOMANDA
                    Dim maxFineVal As String = ""
                    par.cmd.CommandText = "SELECT MAX(FINE_VALIDITA_CAN) as maxFineVal FROM SISCOM_MI.CANONI_EC WHERE ID_CONTRATTO=" & idcontr & " AND TIPO_PROVENIENZA IN (SELECT ID FROM T_TIPO_PROVENIENZA WHERE VALIDA=1)"
                    Dim myReaderF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderF.Read Then
                        maxFineVal = par.IfNull(myReaderF("maxFineVal"), "")
                    End If
                    myReaderF.Close()

                    If maxFineVal = "" Then
                        par.cmd.CommandText = "select fine_canone from utenza_bandi where stato=1 order by id desc"
                        Dim myReaderF1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderF1.Read Then
                            maxFineVal = par.IfNull(myReaderF1("fine_canone"), "")
                        End If
                        myReaderF1.Close()
                    End If

                    Dim anno1 As Integer = 0
                    Dim anno2 As Integer = 0

                    anno1 = Mid(dataInizioValidita, 1, 4)
                    'anno2 = Year(Now)
                    anno2 = Mid(maxFineVal, 1, 4)

                    If dataFine >= maxFineVal Then
                        PER_ANNI = 0
                        For j As Integer = anno1 To anno2 - 1
                            PER_ANNI += 1
                        Next
                    End If

                    If ANNO_INIZIO + PER_ANNI > Year(Now) Then
                        ANNO_FINE = Year(Now)
                    Else
                        ANNO_FINE = ANNO_INIZIO + PER_ANNI
                    End If
                    '03/06/2014 FINE

                    par.cmd.CommandText = "SELECT * FROM siscom_mi.rapporti_utenza_prossima_bol WHERE prossima_bolletta IS NOT NULL ORDER BY prossima_bolletta DESC"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Select Case Mid(par.IfNull(myReader("PROSSIMA_BOLLETTA"), ""), 5, 2)
                            Case "01", "03", "05", "07", "08", "10", "12"
                                dataBollettazGener = DateAdd(DateInterval.Month, -1, CDate(par.FormattaData(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "") & "31")))
                            Case "02"
                                dataBollettazGener = DateAdd(DateInterval.Month, -1, CDate(par.FormattaData(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "") & "28")))
                            Case "04", "06", "09", "11"
                                dataBollettazGener = DateAdd(DateInterval.Month, -1, CDate(par.FormattaData(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "") & "30")))
                        End Select
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.id,RAPPORTI_UTENZA_PROSSIMA_BOL.PROSSIMA_BOLLETTA FROM SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL,SISCOM_MI.RAPPORTI_UTENZA WHERE RAPPORTI_UTENZA_PROSSIMA_BOL.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND COD_CONTRATTO='" & Request.QueryString("COD") & "'"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        idc = myReader("id")
                        prossimaBoll = par.IfNull(myReader("PROSSIMA_BOLLETTA"), "")
                        'Select Case Mid(par.IfNull(myReader("PROSSIMA_BOLLETTA"), ""), 5, 2)
                        '    Case "01", "03", "05", "07", "08", "10", "12"
                        '        dataBollettUltima = DateAdd(DateInterval.Month, -1, CDate(par.FormattaData(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "") & "31")))
                        '    Case "02"
                        '        dataBollettUltima = DateAdd(DateInterval.Month, -1, CDate(par.FormattaData(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "") & "28")))
                        '    Case "04", "06", "09", "11"
                        '        dataBollettUltima = DateAdd(DateInterval.Month, -1, CDate(par.FormattaData(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "") & "30")))
                        'End Select
                        '*** CASE ERRATO NEL CALCOLO DI dataBollettUltima, MARCO 18/07/2017 *** 
                        dataBollettUltima = CDate(par.FormattaData(myReader("PROSSIMA_BOLLETTA") & "01")).AddDays(-1)
                    End If
                    myReader.Close()



                    par.cmd.CommandText = "SELECT distinct(unita_immobiliari.ID),indirizzi.descrizione AS INDIRIZZO,COMUNI_NAZIONI.NOME AS COMUNE_DI,COMUNI_NAZIONI.SIGLA AS PROVINCIA,indirizzi.civico, indirizzi.cap, unita_immobiliari.* FROM siscom_mi.indirizzi,siscom_mi.edifici,siscom_mi.unita_immobiliari,siscom_mi.complessi_immobiliari,COMUNI_NAZIONI WHERE COMUNI_NAZIONI.COD=INDIRIZZI.COD_COMUNE AND complessi_immobiliari.ID = edifici.id_complesso AND edifici.ID = unita_immobiliari.id_edificio AND indirizzi.ID = edifici.id_indirizzo_principale AND unita_immobiliari.ID =" & IDUNITA
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        parte1 = parte1 & "CALCOLO CANONE L.R. 27/07   - Data Calcolo: " & Format(Now, "dd/MM/yyyy") & "<br/>" & "<br/>"

                        parte1 = parte1 & "ALLOGGIO COD.: " & par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), "") & "<br/>"

                        parte1 = parte1 & "INDIRIZZO    : " & par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), "") & " " & par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE_DI"), "") & " (" & par.IfNull(myReader("PROVINCIA"), "") & ")" & "<br/>"

                        If Request.QueryString("COD") <> "" Then
                            par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,siscom_mi.rapporti_utenza WHERE RAPPORTI_UTENZA.COD_CONTRATTO='" & Request.QueryString("COD") & "' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE') AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA ORDER BY SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE DESC"
                            Dim myReader345 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader345.Read Then
                                parte1 = parte1 & "<br/>" & "CODICE CONTRATTO: " & Request.QueryString("COD") & " - INTESTATARIO: " & par.IfNull(myReader345("INTESTATARIO"), "") & "<br/>" & "<br/>"
                            End If
                            myReader345.Close()
                        End If

                    End If
                    myReader.Close()

                    'If stringaPreRECA3 <> "" Then
                    '    stringaPreRECA3 = "Situazione PRE-RECA" & stringaPreRECA3
                    'End If
                    'stringaPreRECA3 = Replace(stringaPreRECA3, vbCrLf, "</br>")
                    nuovaStrStampa0 = parte1 '& "<p style='color: #000080'>" & stringaPreRECA3 & "</p>"
                    Dim riferimentoDa As String = ""

                    For I = ANNO_INIZIO To ANNO_FINE
                        parte2 = ""
                        parte3 = ""
                        parte4 = ""
                        stringaPreRECA3 = ""
                        LOCATIVO = ""
                        comunicazioni = ""

                        sISEE = ""
                        sISE = ""
                        sISR = ""
                        sISP = ""
                        sVSE = ""
                        sREDD_DIP = ""
                        sREDD_ALT = ""
                        sLimitePensione = ""
                        sPER_VAL_LOC = ""
                        sPERC_INC_MAX_ISE_ERP = ""
                        sCANONE_MIN = ""
                        sISE_MIN = ""
                        sCanone = ""
                        sNOTE = ""
                        sDEM = ""
                        sSUPCONVENZIONALE = ""
                        sCOSTOBASE = ""
                        sZONA = ""
                        sMOTIVODECADENZA = ""
                        'TestoDaScrivere = Replace(Replace(par.CalcolaCanone27(CDbl(Request.QueryString("ID")), 3, IDUNITA, Request.QueryString("COD"), IMPORTO, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, I), vbCrLf, "</br>"), "CALCOLO CANONE L.R. 27/07", "CALCOLO CANONE L.R. 27/07 ANNO " & I)

                        par.cmd.CommandText = "SELECT * FROM CANONI_PRE_RECA WHERE ID_DOMANDA='" & Request.QueryString("ID") & "' AND ANNO_RIFERIMENTO=" & I
                        myReader = par.cmd.ExecuteReader()
                        While myReader.Read
                            Select Case par.IfNull(myReader("NUM_PARTE"), "")
                                'Case 1
                                '    stringaPreRECA1 = par.IfNull(myReader("TESTO_CANONE"), "")
                                Case 2
                                    stringaPreRECA2 = par.IfNull(myReader("TESTO_CANONE"), "")
                                Case 3
                                    stringaPreRECA3 = par.IfNull(myReader("TESTO_CANONE"), "")
                                Case 4
                                    stringaPreRECA4 = par.IfNull(myReader("TESTO_CANONE"), "")
                            End Select

                        End While
                        myReader.Close()


                        'RICHIAMO FUNZIONE CALCOLACANONE27RECA CHE GENERA STAMPE A PEZZI
                        par.CalcolaCanone27RECA(CDbl(Request.QueryString("ID")), 3, IDUNITA, Request.QueryString("COD"), IMPORTO, NuovoTransitorio, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, I)
                        parte2 = Replace(parte2, vbCrLf, "</br>")
                        parte3 = Replace(parte3, vbCrLf, "</br>")
                        parte4 = Replace(parte4, vbCrLf, "</br>")

                        stringaPreRECA2 = Replace(stringaPreRECA2, vbCrLf, "</br>")
                        stringaPreRECA3 = Replace(stringaPreRECA3, vbCrLf, "</br>")
                        stringaPreRECA4 = Replace(stringaPreRECA4, vbCrLf, "</br>")

                        nuovaStrStampa = nuovaStrStampa & parte2 & "<br/><br/><br/><p style='color: #000080'>" & stringaPreRECA4 & "</p><br/>" & parte4

                        Dim dataFineBollettato As String = ""

                        Dim ggCredito As Integer = 0

                        If IMPORTO <> 0 Then
                            Dim GiorniDiffEmesso As Long = 0
                            Dim TotGiorniEmesso As Decimal = 0

                            Dim GiorniDiffEmessoNuovo As Long = 0
                            Dim TotGiorniEmessoNuovo As Decimal = 0
                            'Dim DiffDaApplicare As Decimal = 0

                            TotGiorniEmessoNuovo = Format(IMPORTO / 12, "0.00")
                            Dim inizioCalcolo As String = ""
                            Dim fineCalcolo As String = ""
                            TotaleEmesso = 0

                            riferimentoDa = I & "1231"



                            'GiorniDiffEmesso = DateDiff(DateInterval.Day, CDate(par.FormattaData(myReader0("riferimento_da"))), CDate(par.FormattaData(myReader0("riferimento_a"))))



                            'DIFFERENZA IN GIORNI TRA INIZIO VALIDITA' E FINE VALIDITA'
                            If I & "0101" < dataInizioValidita Then
                                inizioCalcolo = dataInizioValidita
                            Else
                                inizioCalcolo = I & "0101"
                            End If

                            If I & "1231" < dataFine Then
                                fineCalcolo = I & "1231"
                                If fineCalcolo > par.AggiustaData(dataBollettUltima) Then
                                    fineCalcolo = par.AggiustaData(dataBollettUltima)
                                End If
                            Else
                                If I >= Year(Now) Then
                                    If fineCalcolo > par.AggiustaData(dataBollettUltima) Then
                                        fineCalcolo = par.AggiustaData(dataBollettUltima)
                                    Else
                                        fineCalcolo = dataFine
                                    End If
                                Else
                                    fineCalcolo = dataFine
                                End If
                            End If

                            'GiorniDiffEmesso = DateDiff(DateInterval.Day, CDate(par.FormattaData(inizioCalcolo)), CDate(par.FormattaData(fineCalcolo)))

                            'GiorniDiffEmesso = Giorno360(CDate(par.FormattaData(inizioCalcolo)), CDate(par.FormattaData(fineCalcolo)), True) + 1


                            'If GiorniDiffEmesso = 0 Then GiorniDiffEmesso = 1
                            'If GiorniDiffEmesso = 27 Then GiorniDiffEmesso = 30
                            'If GiorniDiffEmesso = 28 Then GiorniDiffEmesso = 30
                            'If GiorniDiffEmesso = 29 Then GiorniDiffEmesso = 30
                            'If GiorniDiffEmesso = 31 Then GiorniDiffEmesso = 30
                            'If GiorniDiffEmesso = 91 Then GiorniDiffEmesso = 90
                            'If GiorniDiffEmesso = 92 Then GiorniDiffEmesso = 90
                            'If GiorniDiffEmesso = 58 Then GiorniDiffEmesso = 60
                            'If GiorniDiffEmesso = 59 Then GiorniDiffEmesso = 60
                            'If GiorniDiffEmesso = 61 Then GiorniDiffEmesso = 60
                            'If GiorniDiffEmesso = 364 Then GiorniDiffEmesso = 360
                            'If GiorniDiffEmesso = 365 Then GiorniDiffEmesso = 360
                            'If GiorniDiffEmesso = 366 Then GiorniDiffEmesso = 360

                            'If GiorniDiffEmesso Mod 30 <> 0 And GiorniDiffEmesso >= 30 Then GiorniDiffEmesso = GiorniDiffEmesso - (GiorniDiffEmesso Mod 30)

                            Dim numMesiBolle As Integer = 0
                            numMesiBolle = DateDiff(DateInterval.Month, CDate(par.FormattaData(inizioCalcolo)), CDate(par.FormattaData(fineCalcolo))) - 1
                            GiorniDiffEmesso = numMesiBolle * 30
                            If GiorniDiffEmesso < 0 Then
                                GiorniDiffEmesso = 0
                            End If
                            Dim giorniPrimoMese As Integer = 0
                            Dim giorniUltimoMese As Integer = 0

                            If CInt(inizioCalcolo.Substring(6, 2)) > 30 Then
                                giorniPrimoMese = 30
                            Else
                                giorniPrimoMese = (30 - inizioCalcolo.Substring(6, 2)) + 1
                            End If
                            If CInt(fineCalcolo.Substring(6, 2)) = 28 And CInt(fineCalcolo.Substring(4, 2)) = 2 Then
                                giorniUltimoMese = 30
                            Else
                                If CInt(fineCalcolo.Substring(6, 2)) = 31 Then
                                    giorniUltimoMese = 30
                                Else
                                    giorniUltimoMese = fineCalcolo.Substring(6, 2)
                                End If
                            End If

                            GiorniDiffEmesso = GiorniDiffEmesso + giorniPrimoMese + giorniUltimoMese



                            TotGiorniEmesso = par.CalcolaEmesso(idc, inizioCalcolo, fineCalcolo, 4)

                            TotaleEmesso = TotaleEmesso + Format(TotGiorniEmesso, "0.00")


                        End If

                        If dataInizioValidita > I & "0101" Then
                            If I = Year(Now) Then
                                If par.AggiustaData(dataBollettUltima) > dataFine Then
                                    dataFineBollettato = par.FormattaData(dataFine)
                                Else
                                    dataFineBollettato = dataBollettUltima
                                End If
                            Else
                                dataFineBollettato = "31/12/" & I
                            End If
                            TestoDaScrivere = TestoDaScrivere & "<br />BOLLETTATO NELL'ANNO " & I & " rapportato al periodo di riferimento (dal " & par.FormattaData(dataInizioValidita) & " al " & dataFineBollettato & ") : " & Format(TotaleEmesso, "##,##0.00") & "<br /><br />"
                            nuovaStrStampa = nuovaStrStampa & "<br />BOLLETTATO NELL'ANNO " & I & " rapportato al periodo di riferimento (dal " & par.FormattaData(dataInizioValidita) & " al " & dataFineBollettato & ") : " & Format(TotaleEmesso, "##,##0.00") & "<br /><br />"
                        Else
                            If I = Year(Now) Then
                                If dataBollettazGener > dataBollettUltima Then
                                    dataFineBollettato = dataBollettUltima
                                Else
                                    If dataFineBollettato < dataFine Then
                                        dataFineBollettato = par.FormattaData(dataFine)
                                    Else
                                        dataFineBollettato = par.FormattaData(dataBollettazGener)
                                    End If
                                End If
                            Else
                                dataFineBollettato = "31/12/" & I
                            End If
                            TestoDaScrivere = TestoDaScrivere & "<br />BOLLETTATO NELL'ANNO " & I & " rapportato al periodo di riferimento (dal 01/01/" & I & " al " & dataFineBollettato & ") : " & Format(TotaleEmesso, "##,##0.00") & "<br /><br />"
                            nuovaStrStampa = nuovaStrStampa & "<br />BOLLETTATO NELL'ANNO " & I & " rapportato al periodo di riferimento (dal 01/01/" & I & " al " & dataFineBollettato & ") : " & Format(TotaleEmesso, "##,##0.00") & "<br /><br />"
                        End If

                        Dim STRdettaglio As String = ""
                        CalcolaScontoCostoBase(idc, CDec(prossimaBoll), STRdettaglio, I)

                        Label1.Text = Label1.Text & nuovaStrStampa & STRdettaglio
                        TestoDaScrivere = ""
                        nuovaStrStampa = ""

                    Next
                    If stringaPreRECA3 <> "" Then
                        If stringaPreRECA3.Contains("Dati reddituali non importati") = False Then
                            stringaPreRECA3 = Replace(stringaPreRECA3, Mid(stringaPreRECA3, 56, 13), "")
                        End If
                    End If
                    parte3 = Replace(parte3, Mid(parte3, 56, 13), "")

                    Label1.Text = nuovaStrStampa0 & "<p style='color: #000080'>" & stringaPreRECA3 & "</p>" & parte3 & "<br/><br/><br/>" & Label1.Text & "<p style='color: #000080'>*NOTE*: in blu la situazione precedente alla revisione canone </p>"

                    'par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID=" & Request.QueryString("ID") & " AND CREDITO_TEORICO IS NOT NULL"
                    'myReader = par.cmd.ExecuteReader
                    'If myReader.Read Then
                    '    Dim tipo As String = "DEBITO"
                    '    If par.IfNull(myReader("CREDITO_TEORICO"), 0) < 0 Then
                    '        tipo = "CREDITO"
                    '    End If
                    '    Label1.Text = Replace(Replace(par.CalcolaCanone27(CDbl(Request.QueryString("ID")), 3, IDUNITA, Request.QueryString("COD"), IMPORTO, LOCATIVO, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, _
                    '    sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL), vbCrLf, "</br>"), "CALCOLO CANONE L.R. 27/07", "CALCOLO CANONE L.R. 27/07 E L.R. 36/2008") _
                    '    & "<br/>" & tipo & " TEORICO DAL: " & par.FormattaData(par.IfNull(myReader("CT_DAL"), "")) & " " _
                    '    & "AL: " & par.FormattaData(par.IfNull(myReader("CT_AL"), "")) & "............" & Format(Math.Abs(par.IfNull(myReader("CREDITO_TEORICO"), 0)), "##,##0.00") & "<br/></br>Quanto dichiarato dall'utente sarà aggiornato con la prossima Anagrafe Utenza ai fini di un possibile conguaglio"
                    'End If
                    'myReader.Close()

                    'par.cmd.CommandText = "SELECT ID_AREA_ECONOMICA,SOTTO_AREA,CANONE,DATA_CALCOLO FROM SISCOM_MI.CANONI_EC WHERE ID_CONTRATTO=" & idcontr & " AND CANONE IS NOT NULL ORDER BY DATA_CALCOLO DESC"
                    'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    'Dim dt As New Data.DataTable
                    'Dim contaEC As Integer = 0
                    'da.Fill(dt)
                    'For Each row As Data.DataRow In dt.Rows
                    '    contaEC = contaEC + 1
                    '    If contaEC = 1 Then
                    '        Label1.Text &= "<br/><br/><br/> AREA - FASCIA - CANONE ATTUALE (DATA CALCOLO " & par.FormattaData(Mid(row.Item("DATA_CALCOLO"), 1, 8)) & ") : <br/><br/>"
                    '        Select Case row.Item("ID_AREA_ECONOMICA")
                    '            Case 1
                    '                Label1.Text &= vbCrLf & vbTab & "Area:.....................................................PROTEZIONE"
                    '            Case 2
                    '                Label1.Text &= vbCrLf & vbTab & "Area:.....................................................ACCESSO"
                    '            Case 3
                    '                Label1.Text &= vbCrLf & vbTab & "Area:.....................................................PERMANENZA"
                    '        End Select
                    '        Label1.Text &= vbCrLf & vbTab & "Fascia:..................................................." & row.Item("SOTTO_AREA")
                    '        Label1.Text &= vbCrLf & vbTab & "Canone:..................................................." & row.Item("CANONE")
                    '        Exit For
                    '    End If
                    'Next


                Else
                    Label1.Text = "ERRORE, UNITA' IMMOBILIARE NON TROVATA!"
                End If

                If Request.QueryString("A") = "1" Then
                    Dim testoPDF As String = ""
                    testoPDF = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title><style type='text/css'>.style1{font-family: 'Courier New';font-size: medium;}</style></head><body><span class='style1'>" & Label1.Text & "</span></body></html>"

                    'STAMPA AGGIUNTA IN ALLEGATI
                    Dim url As String = Server.MapPath("..\FileTemp\")
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
                    pdfConverter1.PdfDocumentOptions.ShowFooter = True
                    pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
                    pdfConverter1.PdfFooterOptions.DrawFooterLine = False

                    Dim nomefile As String = "CR_" & IDdich & "-" & Format(Now, "yyyyMMddHHmmss")
                    pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(testoPDF, url & nomefile & ".pdf", Server.MapPath("..\IMG\"))


                    '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI
                    Dim objCrc32 As New Crc32()
                    Dim strmZipOutputStream As ZipOutputStream
                    Dim zipfic As String

                    zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

                    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                    strmZipOutputStream.SetLevel(6)
                    '
                    Dim strFile As String
                    strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
                    Dim strmFile As FileStream = File.OpenRead(strFile)
                    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                    '
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
                    Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

                End If
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Label1.Text = "ERRORE..." & ex.Message
            End Try

        End If

    End Sub


    Private Function CalcolaScontoCostoBase(ByVal idContratto As Long, ByVal rataProssima As Integer, ByRef STRdettaglio As String, ByVal annocalcolo As Integer, Optional ByVal soloCalcolo As Boolean = False, Optional nomeTabella As String = "") As Decimal
        Try
            Dim canoneMIN As Decimal = 0
            Dim nuovoCostoBase As Decimal = 0
            Dim valoreConvenz As Decimal = 0
            Dim valoreLocativo As Decimal = 0
            Dim canoneSopp As Decimal = 0
            Dim canoneApp As Decimal = 0
            Dim esclusione As String = ""
            Dim idEdificio As Long = 0
            Dim idUnita As Long = 0
            Dim scontoCostoBase As Integer = 0
            Dim scontoOk As Boolean = False
            Dim canoneClasse As Decimal = 0
            Dim CanoneErpRegime As Decimal = 0
            Dim tipoCanone As String = ""
            Dim numRate As Integer = 0
            Dim calcoloUltimoAnno As Boolean = False
            Dim codContratto As String = ""
            Dim TestoFile As String = ""
            Dim fileName As String = codContratto & ".txt"
            Dim NuovoCanone As Double = 0
            Dim NuovoTransitorio As Double = 0
            Dim LOCATIVO As String = ""

            Dim comunicazioni As String = ""
            Dim AreaEcono As Integer = 0

            Dim parte1 As String = ""
            Dim parte2 As String = ""
            Dim parte3 As String = ""
            Dim parte4 As String = ""

            Dim canoneScontato As Decimal = 0
            Dim delta As Decimal = 0
            Dim importo694 As Decimal = 0

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE id_unita_principale is null and ID_CONTRATTO=" & idContratto
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idEdificio = par.IfNull(myReader0("ID_EDIFICIO"), "")
                idUnita = par.IfNull(myReader0("ID_UNITA"), "")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idContratto
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                numRate = par.IfNull(myReader0("NRO_RATE"), 0)
                codContratto = par.IfNull(myReader0("COD_CONTRATTO"), 0)
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EDIFICI WHERE ID=" & idEdificio
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                scontoCostoBase = par.IfNull(myReader0("SCONTO_COSTO_BASE"), 0)
            End If
            myReader0.Close()

            TestoFile = Replace(par.CalcolaCanone27RECA(CDbl(Request.QueryString("ID")), 3, idUnita, codContratto, NuovoCanone, NuovoTransitorio, LOCATIVO, comunicazioni, AreaEcono, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, annocalcolo), "CALCOLO CANONE L.R. 27/07", "CALCOLO CANONE L.R. 27/07 ANNO " & Year(Now))

            'STRdettaglio = STRdettaglio & vbCrLf & vbTab & "SCONTO COSTO BASE:........................................" & scontoCostoBase * -1 & "%"
            'STRdettaglio = STRdettaglio & vbCrLf & vbTab & "CANONE SCONTATO:.........................................." & Format(canoneScontato, "##,##0.00")
            'STRdettaglio = STRdettaglio & vbCrLf & vbTab & "DIFFERENZA DA APPLIC. IN BOLLETTA:........................" & Format(delta, "##,##0.00")

            Dim tipoCanoneApp As String = ""

            If NuovoCanone = CDec(sCANONECLASSEISTAT) Then
                tipoCanoneApp = "CLASSE"
            ElseIf NuovoCanone = (CDec(sCANONE_MIN) * 12) Then
                tipoCanoneApp = "MINIMO AREA"
            ElseIf NuovoCanone <> CDec(sCANONECLASSEISTAT) Then
                tipoCanoneApp = "SOPPORTABILE"
            End If


            Dim canoneIniz As Decimal = 0
            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI." & nomeTabella & " WHERE ID_CONTRATTO=" & idContratto & " AND ID_DICHIARAZIONE=" & idDichiarazione & " and COMPETENZA=" & annocalcolo & " order by competenza desc"
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            calcoloUltimoAnno = True
            canoneIniz = NuovoCanone 'par.IfNull(myReader("CANONE"), 0)
            tipoCanone = tipoCanoneApp
            If AreaEcono = 4 Then
                esclusione = "a)"
            End If
            If NuovoCanone = sCANONE_MIN Then 'par.IfNull(myReader("CANONE_MINIMO_AREA"), 0) Then
                esclusione = "b)"
            End If
            If NuovoCanone < 0 Then
                esclusione = "c)"
            End If

            If scontoCostoBase <> 0 And esclusione = "" Then
                scontoOk = True
            End If

            'canoneMIN = par.IfNull(myReader("CANONE_MINIMO_AREA"), "0") * 12
            canoneMIN = CDec(sCANONE_MIN) * 12
            'nuovoCostoBase = par.IfNull(myReader("COSTOBASE"), "0") + ((scontoCostoBase / 100) * par.IfNull(myReader("COSTOBASE"), "0"))
            nuovoCostoBase = CDec(sCOSTOBASE) + ((scontoCostoBase / 100) * CDec(sCOSTOBASE))
            'valoreConvenz = nuovoCostoBase * par.IfNull(myReader("SUPCONVENZIONALE"), "0") * par.IfNull(myReader("DEM"), "0") * par.IfNull(myReader("ZONA"), "0") * par.IfNull(myReader("PIANO"), "0") * par.IfNull(myReader("CONSERVAZIONE"), "0") * par.IfNull(myReader("VETUSTA"), "0")

            valoreConvenz = nuovoCostoBase * CDec(sSUPCONVENZIONALE) * CDec(sDEM) * CDec(sZONA) * CDec(sPIANO) * CDec(sCONSERVAZIONE) * CDec(sVETUSTA)
            valoreLocativo = (valoreConvenz * 5) / 100

            'canoneSopp = par.IfNull(myReader("CANONE_SOPPORTABILE"), "0")
            canoneSopp = CDec(sCANONESOPP)
            'canoneApp = Format((par.IfNull(myReader("PERC_VAL_LOC"), "0") * valoreLocativo) / 100, "0.00")
            canoneApp = Format((CDec(sPER_VAL_LOC) * valoreLocativo) / 100, "0.00")

            'canoneClasse = (canoneApp + ((canoneApp * CDec(par.IfNull(myReader("PERC_ISTAT_APPLICATA"), "0"))) / 100)) * par.IfNull(myReader("COEFF_NUCLEO_FAM"), "0")
            canoneClasse = (canoneApp + ((canoneApp * CDec(sISTAT)) / 100)) * CDec(sCOEFFFAM)

            If CDec(canoneSopp) < CDec(canoneClasse) Then
                If CDec(canoneSopp) > CDec(canoneMIN) Then
                    CanoneErpRegime = canoneSopp
                    'tipoCanone = "SOPPORTABILE"
                Else
                    CanoneErpRegime = canoneMIN
                    'tipoCanone = "MINIMO AREA"
                End If
            Else
                If CDec(canoneClasse) > CDec(canoneMIN) Then
                    CanoneErpRegime = canoneClasse
                    'tipoCanone = "CLASSE"
                Else
                    CanoneErpRegime = canoneMIN
                    'tipoCanone = "MINIMO AREA"
                End If
            End If

            'End If
            'myReader.Close()


            If calcoloUltimoAnno = True And soloCalcolo = False Then
                'CONTROLLO SE IN BOL_SCHEMA ESISTE LA VOCE 694

                If tipoCanone <> "CLASSE" Or esclusione <> "" Then
                    'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_SCHEMA WHERE ID_CONTRATTO=" & idContratto & " AND ANNO=" & annocalcolo & " AND ID_VOCE=" & IdVoceContributoCanone 'AND ID_VOCE=694"
                    'Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReaderC.Read Then
                    '    importo694 = par.IfNull(myReaderC("IMPORTO_SINGOLA_RATA"), 0)

                    '    par.cmd.CommandText = "DELETE FROM SISCOM_MI.BOL_SCHEMA WHERE ID_CONTRATTO=" & idContratto & " AND ANNO=" & annocalcolo & " AND ID_VOCE=" & IdVoceContributoCanone 'AND ID_VOCE=694"
                    '    par.cmd.ExecuteNonQuery()

                    '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    '    & "VALUES (" & idContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    '    & "'F184','ELIMINATA VOCE Riduzione Canone (euro " & par.VirgoleInPunti(Format(importo694 * -1, "##,##0.00")) & ") IN SEGUITO A RECA')"
                    '    par.cmd.ExecuteNonQuery()
                    'End If
                    'myReaderC.Close()
                End If

                If tipoCanone = "CLASSE" And scontoOk = True Then
                    canoneScontato = CanoneErpRegime '- ((CanoneErpRegime * (scontoCostoBase * -1)) / 100)

                    delta = Math.Round((canoneIniz - canoneScontato), 2)

                    'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_SCHEMA WHERE ID_CONTRATTO=" & idContratto & " AND ANNO=" & annocalcolo & " AND ID_VOCE=" & IdVoceContributoCanone & " ORDER BY ID DESC" 'AND ID_VOCE=694 ORDER BY ID DESC"
                    'Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReaderC.Read = False Then
                    '    If delta > 0 Then
                    '        par.cmd.CommandText = "UPDATE SISCOM_MI.CANONI_EC SET SCONTO_COSTO_BASE='" & scontoCostoBase & "',DELTA_1243_12='" & Format(delta * -1, "0.00") & "',CANONE_1243_12='" & Format(canoneScontato, "0.00") & "',TIPO_CANONE_APP='" & tipoCanone & "' WHERE ID_CONTRATTO=" & idContratto & " AND ID_DICHIARAZIONE=" & lIdDichiarazione & " AND ID_BANDO_AU IS NOT NULL"
                    '        par.cmd.ExecuteNonQuery()

                    '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE,IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values (SISCOM_MI.SEQ_BOL_SCHEMA.NEXTVAL, " & idContratto & ", " & idUnita & ", " & par.RicavaEsercizioCorrente & ", " & IdVoceContributoCanone & "," & par.VirgoleInPunti(Format((delta / numRate) * ((numRate - rataProssima) + 1) * -1, "##,##0.00")) & ", " & rataProssima & ", " & (numRate - rataProssima) + 1 & ", " & par.VirgoleInPunti(Format((delta * -1) / numRate, "##,##0.00")) & ", " & annocalcolo & ")"
                    '        par.cmd.ExecuteNonQuery()

                    '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & idContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F184','AGGIUNTA VOCE Riduzione Canone (euro " & par.VirgoleInPunti(Format((delta * -1) / numRate, "##,##0.00")) & ") DA RATA " & rataProssima & " PER " & (numRate - rataProssima) + 1 & " RATE')"
                    '        par.cmd.ExecuteNonQuery()
                    '    End If
                    'Else
                    '    If delta > 0 Then
                    '        par.cmd.CommandText = "UPDATE SISCOM_MI.CANONI_EC SET SCONTO_COSTO_BASE='" & scontoCostoBase & "',DELTA_1243_12='" & Format(delta * -1, "0.00") & "',CANONE_1243_12='" & Format(canoneScontato, "0.00") & "',TIPO_CANONE_APP='" & tipoCanone & "' WHERE ID_CONTRATTO=" & idContratto & " AND ID_DICHIARAZIONE=" & lIdDichiarazione & " AND ID_BANDO_AU IS NOT NULL"
                    '        par.cmd.ExecuteNonQuery()

                    '        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_SCHEMA SET IMPORTO=" & par.VirgoleInPunti(Format((delta / numRate) * ((numRate - rataProssima) + 1) * -1, "##,##0.00")) & ",DA_RATA=" & rataProssima & ",PER_RATE=" & (numRate - rataProssima) + 1 & ",IMPORTO_SINGOLA_RATA=" & par.VirgoleInPunti(Format((delta * -1) / numRate, "##,##0.00")) & " WHERE ID=" & par.IfNull(myReaderC("ID"), "")
                    '        par.cmd.ExecuteNonQuery()

                    '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & idContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F184','MODIFICATA VOCE Riduzione Canone da euro " & par.VirgoleInPunti(par.IfNull(myReaderC("IMPORTO_SINGOLA_RATA"), "")) & " A euro " & par.VirgoleInPunti(Format((delta * -1) / numRate, "##,##0.00")) & " IN SEGUITO A RECA')"
                    '        par.cmd.ExecuteNonQuery()
                    '    End If
                    'End If
                    'myReaderC.Close()
                End If
                'If esclusione <> "" Then
                '    par.cmd.CommandText = "UPDATE SISCOM_MI.CANONI_EC SET ESCLUSIONE_1243_12='" & esclusione & "' WHERE ID_CONTRATTO=" & idContratto & " AND ID_DICHIARAZIONE=" & idDichiarazione & " AND ID_BANDO_AU IS NOT NULL"
                '    par.cmd.ExecuteNonQuery()
                'End If
            Else
                If tipoCanone = "CLASSE" And scontoOk = True Then
                    canoneScontato = CanoneErpRegime '- ((CanoneErpRegime * (scontoCostoBase * -1)) / 100)

                    delta = Math.Round((canoneIniz - canoneScontato), 2)
                End If
            End If

            'Return delta

            'Dim TestoFile As String = ""
            'Dim fileName As String = codContratto & ".txt"
            'Dim NuovoCanone As Double = 0
            'Dim NuovoTransitorio As Double = 0
            'Dim LOCATIVO As String = ""

            'Dim comunicazioni As String = ""
            'Dim AreaEcono As Integer = 0

            'Dim parte1 As String = ""
            'Dim parte2 As String = ""
            'Dim parte3 As String = ""
            'Dim parte4 As String = ""

            'TestoFile = Replace(par.CalcolaCanone27RECA(CDbl(HiddenID.Value), 3, idUnita, codContratto, nuovocanone, NuovoTransitorio, LOCATIVO, comunicazioni, AreaEcono, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, parte1, parte2, parte3, parte4, Year(Now)), "CALCOLO CANONE L.R. 27/07", "CALCOLO CANONE L.R. 27/07 ANNO " & Year(Now))
            If canoneScontato > 0 Then
                STRdettaglio = STRdettaglio & vbCrLf & vbTab & "SCONTO COSTO BASE:........................................" & scontoCostoBase * -1 & "% <br />"
                STRdettaglio = STRdettaglio & vbCrLf & vbTab & "CANONE SCONTATO:.........................................." & Format(canoneScontato, "##,##0.00") & "<br />"
                STRdettaglio = STRdettaglio & vbCrLf & vbTab & "DIFFERENZA DA APPLIC. IN BOLLETTA:........................" & Format(delta, "##,##0.00") & "<br />"

                STRdettaglio = STRdettaglio & vbCrLf & vbTab & "<br /><br />"
            End If
            'TestoFile = TestoFile & STRdettaglio

            'Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\StampeCanoni27\") & fileName, False, System.Text.Encoding.Default)
            'sr.WriteLine(TestoFile)
            'sr.Close()

            'If System.IO.File.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\StampeCanoni27\") & fileName) = True Then

            '    par.cmd.CommandText = "UPDATE SISCOM_MI.CANONI_EC SET TESTO=:TESTO WHERE ID_DICHIARAZIONE = " & lIdDichiarazione & " AND ID_CONTRATTO = " & idContratto & " AND ID_BANDO_AU IS NOT NULL"

            '    Dim objStream As Stream = File.Open(Server.MapPath("..\ALLEGATI\CONTRATTI\StampeCanoni27\") & fileName, FileMode.Open)
            '    Dim buffer(objStream.Length) As Byte
            '    objStream.Read(buffer, 0, objStream.Length)
            '    objStream.Close()

            '    Dim parmData As New Oracle.DataAccess.Client.OracleParameter
            '    With parmData
            '        .Direction = Data.ParameterDirection.Input
            '        .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Blob
            '        .ParameterName = "TESTO"
            '        .Value = buffer
            '    End With

            '    par.cmd.Parameters.Add(parmData)
            '    par.cmd.ExecuteNonQuery()
            '    System.IO.File.Delete(Server.MapPath("..\ALLEGATI\CONTRATTI\StampeCanoni27\") & fileName)
            '    par.cmd.Parameters.Remove(parmData)

            '    buffer = Nothing
            '    objStream = Nothing
            'End If


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label1.Text = "ERRORE..." & ex.Message
        End Try
    End Function


    Function Giorno360(ByVal dataIniziale As Date, ByVal dataFinale As Date, Optional metodo As Boolean = False) As Long
        dataIniziale = CDate("28/02/2013")
        dataFinale = CDate("16/11/2013")
        If Not metodo Then 'Metodo Americano 
            If Day(dataIniziale) = 31 Then
                dataIniziale = DateSerial(Year(dataIniziale), Month(dataIniziale), 30)
            End If
            If Day(dataFinale) = 31 And Day(dataIniziale) < 30 Then
                dataFinale = DateAdd("d", 1, dataFinale)
            ElseIf Day(dataFinale) = 31 And Day(dataIniziale) >= 30 Then
                dataFinale = DateSerial(Year(dataFinale), Month(dataFinale), 30)
            End If
        Else   'Metodo Europeo 
            If Day(dataIniziale) = 31 Then
                dataIniziale = DateSerial(Year(dataIniziale), Month(dataIniziale), 30)
            End If
            If Day(dataFinale) = 31 Then
                dataFinale = DateSerial(Year(dataFinale), Month(dataFinale), 30)
            End If
            If Day(dataIniziale) = 28 And Month(dataIniziale) = 2 Then
                dataIniziale = DateSerial(Year(dataIniziale), Month(dataIniziale), 30)
            End If

            If Day(dataIniziale) = 29 And Month(dataIniziale) = 2 Then
                dataIniziale = DateSerial(Year(dataIniziale), Month(dataIniziale), 30)
            End If
            If Day(dataFinale) = 28 And Month(dataFinale) = 2 Then
                dataFinale = DateSerial(Year(dataFinale), Month(dataFinale), 30)
            End If

            If Day(dataFinale) = 29 And Month(dataFinale) = 2 Then
                dataFinale = DateSerial(Year(dataFinale), Month(dataFinale), 30)
            End If
        End If
        Giorno360 = DateDiff("m", dataIniziale, dataFinale) * 30 + (Day(dataFinale) - Day(dataIniziale))
    End Function

    Public Property idDichiarazione() As Long
        Get
            If Not (ViewState("par_idDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_idDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idDichiarazione") = value
        End Set
    End Property

    Public Property sNOTE() As String
        Get
            If Not (ViewState("par_sNOTE") Is Nothing) Then
                Return CStr(ViewState("par_sNOTE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNOTE") = value
        End Set
    End Property

    Public Property sDEM() As String
        Get
            If Not (ViewState("par_sDEM") Is Nothing) Then
                Return CStr(ViewState("par_sDEM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDEM") = value
        End Set
    End Property

    Public Property sSUPCONVENZIONALE() As String
        Get
            If Not (ViewState("par_sSUPCONVENZIONALE") Is Nothing) Then
                Return CStr(ViewState("par_sSUPCONVENZIONALE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPCONVENZIONALE") = value
        End Set
    End Property

    Public Property sCOSTOBASE() As String
        Get
            If Not (ViewState("par_sCOSTOBASE") Is Nothing) Then
                Return CStr(ViewState("par_sCOSTOBASE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOSTOBASE") = value
        End Set
    End Property

    Public Property sZONA() As String
        Get
            If Not (ViewState("par_sZONA") Is Nothing) Then
                Return CStr(ViewState("par_sZONA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sZONA") = value
        End Set
    End Property

    Public Property sPIANO() As String
        Get
            If Not (ViewState("par_sPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPIANO") = value
        End Set
    End Property

    Public Property sCONSERVAZIONE() As String
        Get
            If Not (ViewState("par_sCONSERVAZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sCONSERVAZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCONSERVAZIONE") = value
        End Set
    End Property

    Public Property sVETUSTA() As String
        Get
            If Not (ViewState("par_sVETUSTA") Is Nothing) Then
                Return CStr(ViewState("par_sVETUSTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVETUSTA") = value
        End Set
    End Property

    Public Property sINCIDENZAISE() As String
        Get
            If Not (ViewState("par_sINCIDENZAISE") Is Nothing) Then
                Return CStr(ViewState("par_sINCIDENZAISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sINCIDENZAISE") = value
        End Set
    End Property


    Public Property sCOEFFFAM() As String
        Get
            If Not (ViewState("par_sCOEFFFAM") Is Nothing) Then
                Return CStr(ViewState("par_sCOEFFFAM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOEFFFAM") = value
        End Set
    End Property

    Public Property sSOTTOAREA() As String
        Get
            If Not (ViewState("par_sSOTTOAREA") Is Nothing) Then
                Return CStr(ViewState("par_sSOTTOAREA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSOTTOAREA") = value
        End Set
    End Property

    Public Property sMOTIVODECADENZA() As String
        Get
            If Not (ViewState("par_sMOTIVODECADENZA") Is Nothing) Then
                Return CStr(ViewState("par_sMOTIVODECADENZA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOTIVODECADENZA") = value
        End Set
    End Property

    Public Property sNUMCOMP() As String
        Get
            If Not (ViewState("par_sNUMCOMP") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP") = value
        End Set
    End Property

    Public Property sNUMCOMP66() As String
        Get
            If Not (ViewState("par_sNUMCOMP66") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP66"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP66") = value
        End Set
    End Property

    Public Property sNUMCOMP100() As String
        Get
            If Not (ViewState("par_sNUMCOMP100") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100") = value
        End Set
    End Property

    Public Property sNUMCOMP100C() As String
        Get
            If Not (ViewState("par_sNUMCOMP100C") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100C"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100C") = value
        End Set
    End Property

    Public Property sPREVDIP() As String
        Get
            If Not (ViewState("par_sPREVDIP") Is Nothing) Then
                Return CStr(ViewState("par_sPREVDIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPREVDIP") = value
        End Set
    End Property

    Public Property sDETRAZIONI() As String
        Get
            If Not (ViewState("par_sDETRAZIONI") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONI") = value
        End Set
    End Property

    Public Property sMOBILIARI() As String
        Get
            If Not (ViewState("par_sMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOBILIARI") = value
        End Set
    End Property


    Public Property sIMMOBILIARI() As String
        Get
            If Not (ViewState("par_sIMMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sIMMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sIMMOBILIARI") = value
        End Set
    End Property

    Public Property sCOMPLESSIVO() As String
        Get
            If Not (ViewState("par_sCOMPLESSIVO") Is Nothing) Then
                Return CStr(ViewState("par_sCOMPLESSIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOMPLESSIVO") = value
        End Set
    End Property

    Public Property sDETRAZIONEF() As String
        Get
            If Not (ViewState("par_sDETRAZIONEF") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONEF"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONEF") = value
        End Set
    End Property

    Public Property sANNOCOSTRUZIONE() As String
        Get
            If Not (ViewState("par_sANNOCOSTRUZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sANNOCOSTRUZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOCOSTRUZIONE") = value
        End Set
    End Property

    Public Property sLOCALITA() As String
        Get
            If Not (ViewState("par_sLOCALITA") Is Nothing) Then
                Return CStr(ViewState("par_sLOCALITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLOCALITA") = value
        End Set
    End Property

    Public Property sASCENSORE() As String
        Get
            If Not (ViewState("par_sASCENSORE") Is Nothing) Then
                Return CStr(ViewState("par_sASCENSORE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sASCENSORE") = value
        End Set
    End Property

    Public Property sDESCRIZIONEPIANO() As String
        Get
            If Not (ViewState("par_sDESCRIZIONEPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sDESCRIZIONEPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDESCRIZIONEPIANO") = value
        End Set
    End Property

    Public Property sSUPNETTA() As String
        Get
            If Not (ViewState("par_sSUPNETTA") Is Nothing) Then
                Return CStr(ViewState("par_sSUPNETTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPNETTA") = value
        End Set
    End Property

    Public Property sALTRESUP() As String
        Get
            If Not (ViewState("par_sALTRESUP") Is Nothing) Then
                Return CStr(ViewState("par_sALTRESUP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALTRESUP") = value
        End Set
    End Property

    Public Property sMINORI15() As String
        Get
            If Not (ViewState("par_sMINORI15") Is Nothing) Then
                Return CStr(ViewState("par_sMINORI15"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMINORI15") = value
        End Set
    End Property


    Public Property sMAGGIORI65() As String
        Get
            If Not (ViewState("par_sMAGGIORI65") Is Nothing) Then
                Return CStr(ViewState("par_sMAGGIORI65"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMAGGIORI65") = value
        End Set
    End Property

    Public Property sSUPACCESSORI() As String
        Get
            If Not (ViewState("par_sSUPACCESSORI") Is Nothing) Then
                Return CStr(ViewState("par_sSUPACCESSORI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPACCESSORI") = value
        End Set
    End Property

    Public Property sVALORELOCATIVO() As String
        Get
            If Not (ViewState("par_sVALORELOCATIVO") Is Nothing) Then
                Return CStr(ViewState("par_sVALORELOCATIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALORELOCATIVO") = value
        End Set
    End Property

    Public Property sCANONEMINIMO() As String
        Get
            If Not (ViewState("par_sCANONEMINIMO") Is Nothing) Then
                Return CStr(ViewState("par_sCANONEMINIMO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONEMINIMO") = value
        End Set
    End Property

    Public Property sCANONECLASSE() As String
        Get
            If Not (ViewState("par_sCANONECLASSE") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSE") = value
        End Set
    End Property

    Public Property sCANONESOPP() As String
        Get
            If Not (ViewState("par_sCANONESOPP") Is Nothing) Then
                Return CStr(ViewState("par_sCANONESOPP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONESOPP") = value
        End Set
    End Property


    Public Property sVALOCIICI() As String
        Get
            If Not (ViewState("par_sVALOCIICI") Is Nothing) Then
                Return CStr(ViewState("par_sVALOCIICI"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALOCIICI") = value
        End Set
    End Property

    Public Property sALLOGGIOIDONEO() As String
        Get
            If Not (ViewState("par_sALLOGGIOIDONEO") Is Nothing) Then
                Return CStr(ViewState("par_sALLOGGIOIDONEO"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALLOGGIOIDONEO") = value
        End Set
    End Property

    Public Property sISTAT() As String
        Get
            If Not (ViewState("par_sISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISTAT") = value
        End Set
    End Property

    Public Property sCANONECLASSEISTAT() As String
        Get
            If Not (ViewState("par_sCANONECLASSEISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSEISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSEISTAT") = value
        End Set
    End Property

    Public Property VAL_LOCATIVO_UNITA() As String
        Get
            If Not (ViewState("par_VAL_LOCATIVO_UNITA") Is Nothing) Then
                Return CStr(ViewState("par_VAL_LOCATIVO_UNITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_VAL_LOCATIVO_UNITA") = value
        End Set

    End Property



    Public Property CanoneCorrente() As Double
        Get
            If Not (ViewState("par_CanoneCorrente") Is Nothing) Then
                Return CDbl(ViewState("par_CanoneCorrente"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Double)
            ViewState("par_CanoneCorrente") = value
        End Set

    End Property


    Public Property sANNOINIZIOVAL() As String
        Get
            If Not (ViewState("par_sANNOINIZIOVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOINIZIOVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOINIZIOVAL") = value
        End Set
    End Property

    Public Property sANNOFINEVAL() As String
        Get
            If Not (ViewState("par_sANNOFINEVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOFINEVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOFINEVAL") = value
        End Set
    End Property

    Public Property AreaEconomica() As Integer
        Get
            If Not (ViewState("par_AreaEconomica") Is Nothing) Then
                Return CInt(ViewState("par_AreaEconomica"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_AreaEconomica") = value
        End Set

    End Property


    Public Property sISEE() As String
        Get
            If Not (ViewState("par_sISEE") Is Nothing) Then
                Return CStr(ViewState("par_sISEE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISEE") = value
        End Set

    End Property


    Public Property sISE() As String
        Get
            If Not (ViewState("par_sISE") Is Nothing) Then
                Return CStr(ViewState("par_sISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE") = value
        End Set
    End Property

    Public Property sVSE() As String
        Get
            If Not (ViewState("par_sVSE") Is Nothing) Then
                Return CStr(ViewState("par_sVSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVSE") = value
        End Set
    End Property


    Public Property sPSE() As String
        Get
            If Not (ViewState("par_sPSE") Is Nothing) Then
                Return CStr(ViewState("par_sPSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPSE") = value
        End Set
    End Property

    Public Property sISP() As String
        Get
            If Not (ViewState("par_sISP") Is Nothing) Then
                Return CStr(ViewState("par_sISP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISP") = value
        End Set
    End Property


    Public Property sISR() As String
        Get
            If Not (ViewState("par_sISR") Is Nothing) Then
                Return CStr(ViewState("par_sISR"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISR") = value
        End Set
    End Property

    Public Property sREDD_DIP() As String
        Get
            If Not (ViewState("par_sREDD_DIP") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_DIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_DIP") = value
        End Set
    End Property

    Public Property sREDD_ALT() As String
        Get
            If Not (ViewState("par_sREDD_ALT") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_ALT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_ALT") = value
        End Set
    End Property

    Public Property sLimitePensione() As String
        Get
            If Not (ViewState("par_sLimitePensione") Is Nothing) Then
                Return CStr(ViewState("par_sLimitePensione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLimitePensione") = value
        End Set
    End Property

    Public Property sPER_VAL_LOC() As String
        Get
            If Not (ViewState("par_sPER_VAL_LOC") Is Nothing) Then
                Return CStr(ViewState("par_sPER_VAL_LOC"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPER_VAL_LOC") = value
        End Set
    End Property


    Public Property sPERC_INC_MAX_ISE_ERP() As String
        Get
            If Not (ViewState("par_sPERC_INC_MAX_ISE_ERP") Is Nothing) Then
                Return CStr(ViewState("par_sPERC_INC_MAX_ISE_ERP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPERC_INC_MAX_ISE_ERP") = value
        End Set
    End Property

    Public Property sCANONE_MIN() As String
        Get
            If Not (ViewState("par_sCANONE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sCANONE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONE_MIN") = value
        End Set
    End Property

    Public Property sISE_MIN() As String
        Get
            If Not (ViewState("par_sISE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sISE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE_MIN") = value
        End Set
    End Property

    Public Property sCanone() As String
        Get
            If Not (ViewState("par_sCanone") Is Nothing) Then
                Return CStr(ViewState("par_sCanone"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCanone") = value
        End Set
    End Property
End Class
