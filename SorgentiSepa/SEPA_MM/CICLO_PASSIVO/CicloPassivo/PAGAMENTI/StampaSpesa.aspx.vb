Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class StampaSpesa
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sValorePrenotazione As String
    Public sValoreODL As String
    Public lIdConnessione As String

    Dim sUnita(19) As String
    Dim sDecina(9) As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sData As String = ""
        Dim sStr1 As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            sValorePrenotazione = Request.QueryString("COD")
            sValoreODL = Request.QueryString("ODL")
            lIdConnessione = Request.QueryString("CON")


            '' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

            If IsNothing(par.myTrans) Then

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
            Else
                ‘‘par.cmd.Transaction = par.myTrans
            End If
            '***********************************************


            Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "select DATA_STAMPA from SISCOM_MI.PRENOTAZIONI where PRENOTAZIONI.ID=" & sValorePrenotazione
            myReaderS = par.cmd.ExecuteReader

            If myReaderS.Read Then
                sData = par.IfNull(myReaderS(0), "")
            End If
            myReaderS.Close()


            '' ID_STATO=1       (TAB_STATI_PAGAMENTI.ID =0=PRENOTATO, 1 EMESSO, 5=PAGATO)
            If sData = "" Then

                'PRENOTAZIONI
                par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set DATA_STAMPA='" & Format(Now, "yyyyMMdd") & "'" _
                                  & "  where ID=" & sValorePrenotazione

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                '********************************************


                '****Scrittura evento EMISSIONE DEL PAGAMENTO
                'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & sValorePrenotazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F97','')"
                'par.cmd.ExecuteNonQuery()
                'par.cmd.CommandText = ""

                '****Scrittura evento EMISSIONE DEL PAGAMENTO in ODL
                par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & sValoreODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F97','')"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                Response.Write("<script>alert('Impegno di spesa emesso e storicizzato!');</script>")

                'ElseIf iStato = 1 Then
                '    'PAGAMENTI
                '    par.cmd.CommandText = "update SISCOM_MI.PAGAMENTI set DATA_EMISSIONE='" & Format(Now, "yyyyMMdd") & "'," _
                '                                                     & "  DATA_STAMPA='" & Format(Now, "yyyyMMdd") & "'," _
                '                                                     & " ID_STATO=1" _
                '                      & " where ID=" & sValorePagamento

                '    par.cmd.ExecuteNonQuery()
                '    par.cmd.CommandText = ""
                '    '********************************************


                '    '****Scrittura evento EMISSIONE DEL PAGAMENTO
                '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & sValorePagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','')"
                '    par.cmd.ExecuteNonQuery()
                '    par.cmd.CommandText = ""

                '    Response.Write("<script>alert('Il pagamento è stato emesso e storicizzato!');</script>")

            End If

            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & sValoreODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F90','Stampa Impegno di Spesa e Ordine di Lavoro')"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""


            par.myTrans.Commit() 'COMMIT
            par.cmd.CommandText = ""

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloSpesa.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            'PF_VOCI_IMPORTO.ID as ""ID_VOCI""

            sStr1 = "select ODL.ANNO,ODL.PROGR,PRENOTAZIONI.DATA_PRENOTAZIONE,PRENOTAZIONI.DATA_STAMPA,PRENOTAZIONI.DESCRIZIONE," _
                        & " ODL.ID as ID_ODL,ODL.PREN_NETTO,ODL.IVA,ODL.CASSA,ODL.RIT_ACCONTO,ODL.PREN_NO_IVA," _
                        & " FORNITORI.RAGIONE_SOCIALE, FORNITORI.COGNOME, FORNITORI.NOME,FORNITORI.COD_FORNITORE," _
                        & " PF_VOCI.CODICE as ""COD_VOCE"",PF_VOCI.DESCRIZIONE as ""DESC_VOCE"" " _
                 & " from SISCOM_MI.PRENOTAZIONI,SISCOM_MI.FORNITORI,SISCOM_MI.PF_VOCI,SISCOM_MI.ODL " _
                 & " where   PRENOTAZIONI.ID=" & sValorePrenotazione _
                     & " and PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID (+) " _
                     & "  and PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID (+) " _
                     & "  and PRENOTAZIONI.ID=ODL.ID_PRENOTAZIONE (+) "

            '& " and PAGAMENTI.ID_VOCE_PF=PF_VOCI_IMPORTO.ID (+) " 
            '& " and PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) "
            '& "  and PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID (+) " 


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            myReader1 = par.cmd.ExecuteReader

            If myReader1.Read Then

                'REFERENTE
                par.cmd.CommandText = "select COGNOME,NOME from SEPA.OPERATORI " _
                                   & " where ID=(select ID_OPERATORE from SISCOM_MI.EVENTI_ODL " _
                                             & " where ID_ODL=" & par.IfNull(myReader1("ID_ODL"), -1) _
                                             & "   and COD_EVENTO='F97')"
                myReaderS = par.cmd.ExecuteReader
                If myReaderS.Read Then
                    contenuto = Replace(contenuto, "$chiamante$", "REFERENTE:")
                    contenuto = Replace(contenuto, "$dettagli_chiamante$", par.IfNull(myReaderS("COGNOME"), "") & " - " & par.IfNull(myReaderS("NOME"), ""))
                Else
                    contenuto = Replace(contenuto, "$chiamante$", "REFERENTE:")
                    contenuto = Replace(contenuto, "$dettagli_chiamante$", "")

                End If
                myReaderS.Close()


                contenuto = Replace(contenuto, "$anno$", myReader1("ANNO"))
                contenuto = Replace(contenuto, "$progr$", par.IfNull(myReader1("PROGR"), "")) 'myReader1("PROGR_FORNITORE"))

                contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(par.IfNull(myReader1("DATA_PRENOTAZIONE"), "-1")))



                contenuto = Replace(contenuto, "$IBAN_TITOLO$", "")
                contenuto = Replace(contenuto, "$iban$", "") 'myReader1("IBAN"))

                'FORNITORI
                If par.IfNull(myReader1("RAGIONE_SOCIALE"), "") = "" Then
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        contenuto = Replace(contenuto, "$fornitori$", par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), ""))
                    Else
                        contenuto = Replace(contenuto, "$fornitori$", par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), ""))
                    End If

                Else
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        contenuto = Replace(contenuto, "$fornitori$", par.IfNull(myReader1("RAGIONE_SOCIALE"), ""))
                    Else
                        contenuto = Replace(contenuto, "$fornitori$", par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), ""))
                    End If
                End If


                contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(par.IfNull(myReader1("DATA_STAMPA"), "")))

                Dim cassa, iva, ritenuta, imponibile, netto, lordo As Decimal
                Dim risultato1 As Decimal

                'A) netto
                'B) cassa=cassa% su A (netto*CASSA)/100
                'C) risultato1=A+B
                'D) iva=iva% su C (risultato1*iva)/100
                'E) TOTALE FATTURA= C+D + IMPONIBILE
                'F) RITENUTA ACCONTO= ritenuta% su A (netto*ritenuta)/100


                'A) IMPORTO
                netto = par.IfNull(myReader1("PREN_NETTO"), 0)
                imponibile = par.IfNull(myReader1("PREN_NO_IVA"), 0)

                iva = par.IfNull(myReader1("IVA"), 0)
                cassa = par.IfNull(myReader1("CASSA"), 0)
                ritenuta = par.IfNull(myReader1("RIT_ACCONTO"), 0)


                'B) CASSA
                cassa = (netto * cassa) / 100

                'C) A+B
                risultato1 = netto + cassa

                'D)
                iva = (risultato1 * iva) / 100

                'E)  C+D ovvero A+B+D + IMPONIBILE
                lordo = netto + cassa + iva + imponibile

                'F) RITENUTA
                ritenuta = (netto * ritenuta) / 100


                contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(lordo, 0), "", "##,##0.00"))

                Dim S2 As String = "<table style='width:100%;'>"
                S2 = S2 & "<tr>"
                S2 = S2 & "<td style='text-align: left; width:45%; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE"), "") & "</td>"
                S2 = S2 & "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>Importo Lordo Prenotato €</td>"
                S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(lordo, 0), "", "##,##0.00") & "</td>"
                'S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Cassa di Previdenza (" & par.IfNull(myReader1("CASSA"), 0) & "%) €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(cassa, 0), "", "##,##0.00") & "</td>"
                'S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA (" & par.IfNull(myReader1("IVA"), 0) & "%) €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(iva, 0), "", "##,##0.00") & "</td>"

                If ritenuta <> 0 Then
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:45%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>di cui Rit. Acconto (" & par.IfNull(myReader1("RIT_ACCONTO"), 0) & "%) €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(ritenuta, 0), "", "##,##0.00") & "</td>"
                End If

                'S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Imponibile non soggetta a IVA €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(imponibile, 0), "", "##,##0.00") & "</td>"
                'S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Importo Lordo €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(lordo, 0), "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr></table>"


                Dim T As String = "<table style='width:100%;'>"
                T = T & "<tr>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & S2 & "</td>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'></td>"

                T = T & "</tr></table>"

                contenuto = Replace(contenuto, "$dettagli$", T)

                contenuto = Replace(contenuto, "$dettagli2$", par.IfNull(myReader1("DESCRIZIONE"), ""))

                contenuto = Replace(contenuto, "$pagina$", "<p style='page-break-before: always'>&nbsp;</p>")


                '*****************FINE SCRITTURA DETTAGLI
                contenuto = Replace(contenuto, "$imp_letterale$", "") 'NumeroInLettere(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)))

                'contenuto = Replace(contenuto, "$dettaglio$", "SPESE")

                contenuto = Replace(contenuto, "$cod_capitolo$", par.IfNull(myReader1("COD_VOCE"), ""))
                contenuto = Replace(contenuto, "$voce_pf$", par.IfNull(myReader1("DESC_VOCE"), ""))
                'contenuto = Replace(contenuto, "$finanziamento$", "Gestione Comune di Milano")


                contenuto = Replace(contenuto, "$chiamante2$", "") ' EX "UFFICIO CONTABILITA' E RENDICONTAZIONE"

                Dim TestoGrigliaM As String = "<p style='page-break-before: always'>&nbsp;</p>"


            End If
            myReader1.Close()



            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


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
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\..\NuoveImm\"))

            Dim i As Integer = 0
            For i = 0 To 10000
            Next

            Response.Write("<script>window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');self.close();</script>")



        Catch ex As Exception

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza: Stampa Pagamento Spese" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub




    '******************************************************************************
    '                               NumeroToLettere
    '
    '                Converte il numero intero in lettere
    '
    ' Input : ImportoN                -->Importo Numerico
    '
    ' Ouput : NumeroToLettere         -->Il numero in lettere
    '******************************************************************************
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
    Function ChangeStr(ByRef sBuffer As String, ByRef OldChar As String, _
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



End Class
