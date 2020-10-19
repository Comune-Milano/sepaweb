Imports ExpertPdf.HtmlToPdf
Imports System.IO

Partial Class Condomini_CreaPagamento
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sUnita(19) As String
    Dim sDecina(9) As String
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Response.Write("<script>alert('Funzione momentaneamente non disponibile!');self.close();</script>")

            PagamentoByID(Request.QueryString("ID"))
        End If
    End Sub
    Private Sub PagamentoByID(ByVal idPagamento As String)
        Try


            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim myLettore As Oracle.DataAccess.Client.OracleDataReader

            par.cmd.CommandText = "select distinct(tipo_pagamento) from siscom_mi.prenotazioni where id_pagamento = " & Request.QueryString("ID")

            'par.cmd.CommandText = "SELECT PAGAMENTI.*,PRENOTAZIONI.*," _
            '                    & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO = 2 THEN (SELECT ID FROM SISCOM_MI.COND_MOROSITA WHERE ID_PRENOTAZIONE = PRENOTAZIONI.ID) WHEN PRENOTAZIONI.TIPO_PAGAMENTO = 1 " _
            '                    & "THEN (SELECT ID FROM SISCOM_MI.COND_GESTIONE WHERE RATA_1_P = PAGAMENTI.ID OR RATA_2_P = PAGAMENTI.ID OR RATA_3_P = PAGAMENTI.ID OR RATA_4_P = PAGAMENTI.ID " _
            '                    & "OR RATA_5_P = PAGAMENTI.ID OR RATA_6_P = PAGAMENTI.ID)  END) AS ID_OGGETTO " _
            '                    & "FROM SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_PAGAMENTO = PAGAMENTI.ID AND PAGAMENTI.ID = " & Request.QueryString("ID")
            myLettore = par.cmd.ExecuteReader
            If myLettore.Read Then
                If myLettore(0) = 1 Then
                    PdfPagamento(Request.QueryString("ID"))
                ElseIf myLettore(0) = 2 Then
                    PdfPagamMorosita(Request.QueryString("ID"))
                End If
            Else
                Response.Write("<script>alert('ERRORE!Nessun pagamento trovato!');self.close();</script>")
            End If
            myLettore.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try

    End Sub
    Private Sub PdfPagamento(ByVal ID As String)
        Try
            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ModelloPagamento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()
            Dim InizioES As String = ""
            Dim FineEs As String = ""

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim tb1 As String = "<table style='width:100%;'>"
            Dim tb2 As String = "<table style='width:100%;'>"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            Dim lettDettagli As Oracle.DataAccess.Client.OracleDataReader
            Dim Matricola As String = ""
            Dim ibanFornitore As String = "- - -"
            par.cmd.CommandText = "select iban from siscom_mi.fornitori_iban where ID  = (select id_IBAN_FORNITORE from siscom_mi.pagamenti where id = " & ID & ")"
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                ibanFornitore = par.IfNull(myReader1("iban"), "- - -")
            End If
            myReader1.Close()

            'par.cmd.CommandText = "SELECT FORNITORI.* FROM SISCOM_MI.FORNITORI, SISCOM_MI.CONDOMINI WHERE condomini.ID =" & Request.QueryString("ID_COND") & " and fornitori.ID = condomini.ID_FORNITORE"
            par.cmd.CommandText = "SELECT FORNITORI.* FROM SISCOM_MI.FORNITORI, SISCOM_MI.PAGAMENTI WHERE PAGAMENTI.ID =" & ID & " and fornitori.ID = PAGAMENTI.ID_FORNITORE"

            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$chiamante$", "CONTO CORRENTE:")
                '*****************SCRITTURA TABELLA DETTAGLI dettagli
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "") & "</td>"
                tb1 = tb1 & "</tr>"
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> IBAN: " & ibanFornitore & "</td></tr>"
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> cod. fiscale: " & par.IfNull(myReader1("COD_FISCALE"), "") & "</td></table>"
                '*****************FINE SCRITTURA DETTAGLI

            End If
            myReader1.Close()

            ''par.cmd.CommandText = "SELECT * FROM SISCOM_MI.cond_gestione WHERE ID =" & idPrenotazione.Value
            ''myReader1 = par.cmd.ExecuteReader
            ''If myReader1.Read Then
            'InizioES = "01/01/2010"
            'FineEs = ""

            'InizioES = par.FormattaData(myReader1("DATA_INIZIO"))
            'FineEs = par.FormattaData(myReader1("DATA_FINE"))
            ''End If
            ''myReader1.Close()
            'contenuto = Replace(contenuto, "$dettagli_chiamante$", "12000X01")
            contenuto = Replace(contenuto, "$copia$", "COPIA DI ")

            contenuto = Replace(contenuto, "$fornitori$", tb1)
            contenuto = Replace(contenuto, "$grigliaM$", "")
            Dim idvocePf As String = ""
            par.cmd.CommandText = ""


            par.cmd.CommandText = "SELECT PAGAMENTI.*,PRENOTAZIONI.*,SISCOM_MI.GETDATA(PAGAMENTI.DATA_SCADENZA) AS D_SCAD, T_ESERCIZIO_FINANZIARIO.INIZIO AS INIZIO_ESERCIZIO,T_ESERCIZIO_FINANZIARIO.FINE AS FINE_ESERCIZIO " _
                                & "FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO, SISCOM_MI.PAGAMENTI " _
                                & "WHERE PRENOTAZIONI.ID_PAGAMENTO = " & ID & " AND PAGAMENTI.ID = PRENOTAZIONI.ID_PAGAMENTO AND PF_VOCI.ID = PRENOTAZIONI.ID_VOCE_PF " _
                                & "AND PF_VOCI.ID_PIANO_FINANZIARIO = PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID"
            myReader1 = par.cmd.ExecuteReader
            '*****Peppe Modify 27/04/2011 secondo nuove direttive stampa modelli pagamento
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$anno$", par.IfNull(myReader1("ANNO"), ""))
                contenuto = Replace(contenuto, "$progr$", par.IfNull(myReader1("PROGR"), ""))
                contenuto = Replace(contenuto, "$dettagli_chiamante$", "12000X01")
                contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(myReader1("DATA_EMISSIONE")))
                contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(myReader1("DATA_STAMPA")))
                contenuto = Replace(contenuto, "$TOT$", Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00"))
                InizioES = "01/01/" & par.IfNull(myReader1("DATA_SCADENZA"), "0000").ToString.Substring(0, 4)
                FineEs = "31/12/" & par.IfNull(myReader1("DATA_SCADENZA"), "0000").ToString.Substring(0, 4)
                ''*****************SCRITTURA TABELLA CENTRALE DETTAGLI PAGAMENTO
                'tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>Rata Esercizio Finanziario dal " & InizioES & " al " & FineEs & "</td></tr>"
                'tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>con scadenza: " & par.FormattaData(par.IfNull(myReader1("DATA_SCADENZA"), "")) & "</td></tr>"
                'tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
                'tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
                '*****************SCRITTURA TABELLA CENTRALE DETTAGLI PAGAMENTO
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE"), "N.D.") & "</td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
                '*****************
                par.cmd.CommandText = "SELECT COND_VOCI_SPESA.DESCRIZIONE, PRENOTAZIONI.* FROM SISCOM_MI.COND_VOCI_SPESA_PF,SISCOM_MI.COND_VOCI_SPESA, SISCOM_MI.PRENOTAZIONI " _
                                    & "WHERE PRENOTAZIONI.ID_PAGAMENTO =" & ID & " AND COND_VOCI_SPESA.ID = COND_VOCI_SPESA_PF.ID_VOCE_COND " _
                                    & "AND (PRENOTAZIONI.ID_VOCE_PF = COND_VOCI_SPESA_PF.ID_VOCE_PF OR PRENOTAZIONI.ID_VOCE_PF_IMPORTO = COND_VOCI_SPESA_PF.ID_VOCE_PF_IMPORTO)"
                 lettDettagli = par.cmd.ExecuteReader
                While lettDettagli.Read
                    tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & par.IfNull(lettDettagli("DESCRIZIONE"), "n.d.") & " €.</td>"
                    tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(par.IfNull(lettDettagli("IMPORTO_APPROVATO"), 0), "##,##0.00") & "</td>"
                    tb2 = tb2 & "</tr>"

                    If String.IsNullOrEmpty(idvocePf) Then
                        idvocePf = lettDettagli("ID_VOCE_PF")
                    Else
                        idvocePf = idvocePf & "," & lettDettagli("ID_VOCE_PF")
                    End If

                End While
                lettDettagli.Close()
                tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>TOTALE €.</td>"
                tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00") & "</td>"
                tb2 = tb2 & "</tr>"

                tb2 = tb2 & "</table>"
                contenuto = Replace(contenuto, "$dettagli$", tb2)
                '*****************FINE SCRITTURA DETTAGLI
                contenuto = Replace(contenuto, "$imp_letterale$", "EURO " & NumeroInLettere(Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00")))
                Dim modalita As String
                Dim condizione As String

                par.cmd.CommandText = "select descrizione from siscom_mi.tipo_modalita_pag where id =" & par.IfNull(myReader1("ID_TIPO_MODALITA_PAG"), -1)
                modalita = par.IfNull(par.cmd.ExecuteScalar, "")
                par.cmd.CommandText = "select descrizione from siscom_mi.tipo_pagamento where id =" & par.IfNull(myReader1("ID_TIPO_PAGAMENTO"), -1)
                condizione = par.IfNull(par.cmd.ExecuteScalar, "")
                contenuto = Replace(contenuto, "$modalita$", par.IfEmpty(modalita, "n.d."))
                contenuto = Replace(contenuto, "$condizione$", par.IfEmpty(condizione, "n.d."))
                contenuto = Replace(contenuto, "$dscadenza$", par.IfNull(myReader1("D_SCAD"), "---"))

            End If
            myReader1.Close()

            tb1 = "<table style='width:100%;'>"
            tb2 = "<table style='width:100%;'>"
            Dim tb3 As String = "<table style='width:100%;'>"
            Dim tbAnBp As String = "<table style='width:100%;'>"
            par.cmd.CommandText = "SELECT PF_VOCI.*,PRENOTAZIONI.IMPORTO_PRENOTATO FROM SISCOM_MI.PF_VOCI, SISCOM_MI.PRENOTAZIONI WHERE PF_VOCI.ID IN (" & idvocePf & ")" _
                & " AND PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PRENOTAZIONI.ID_PAGAMENTO = " & ID
            myReader1 = par.cmd.ExecuteReader

            While myReader1.Read
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:12pt;font-family :Arial ;'> " & par.IfNull(myReader1("CODICE"), "") & "</td>"
                tb1 = tb1 & "</tr>"

                tbAnBp = tbAnBp & "<tr><td style='text-align: left; font-size:12pt;font-family :Arial ;'> " & par.AnnoBPPag(ID) & "</td>"
                tbAnBp = tbAnBp & "</tr>"

                tb2 = tb2 & "<tr><td style='text-align: left; font-size:12pt;font-family :Arial ;'> " & par.IfNull(myReader1("DESCRIZIONE"), "") & "</td>"
                tb2 = tb2 & "</tr>"

                tb3 = tb3 & "<tr><td style='text-align: right; font-size:12pt;font-family :Arial ;'> €." & Format(par.IfNull(myReader1("IMPORTO_PRENOTATO"), 0), "##,##0.00") & "</td>"
                tb3 = tb3 & "</tr>"

            End While

            tb1 = tb1 & "</table>"
            tbAnBp = tbAnBp & "</table>"
            tb2 = tb2 & "</table>"
            tb3 = tb3 & "</table>"

            contenuto = Replace(contenuto, "$cod_capitolo$", tb1)
            contenuto = Replace(contenuto, "$annobp$", tbAnBp)

            contenuto = Replace(contenuto, "$voce_pf$", tb2)
            contenuto = Replace(contenuto, "$TOTSING$", tb3)

            myReader1.Close()

            par.cmd.CommandText = "SELECT COD_ANA FROM OPERATORI WHERE ID IN (SELECT ID_OPERATORE FROM SISCOM_MI.EVENTI_PAGAMENTI WHERE ID_PAGAMENTO = " & ID & " AND COD_EVENTO = 'F98' )"
            myReader1 = par.cmd.ExecuteReader

            If myReader1.Read Then
                Matricola = par.IfNull(myReader1("COD_ANA"), "n.d.")
            End If

            myReader1.Close()

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
            pdfConverter1.PdfDocumentOptions.TopMargin = 20
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "AttPagamento_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\NuoveImm\"))
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','AttPagamento','');self.close();</script>")





        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try


    End Sub
    Private Sub PdfPagamMorosita(ByVal ID As String)
        Try

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ModelloPagamento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()
            Dim Matricola As String = ""

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim tb1 As String = "<table style='width:100%;'>"
            Dim tb2 As String = "<table style='width:100%;'>"
            Dim ibanFornitore As String = "- - -"
            par.cmd.CommandText = "select iban from siscom_mi.fornitori_iban where ID  = (select id_IBAN_FORNITORE from siscom_mi.pagamenti where id = " & ID & ")"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader1.Read Then
                ibanFornitore = par.IfNull(myReader1("iban"), "- - -")
            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT FORNITORI.* FROM SISCOM_MI.FORNITORI, SISCOM_MI.CONDOMINI WHERE condomini.ID =" & Request.QueryString("ID_COND") & " and fornitori.ID = condomini.ID_FORNITORE"

            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CONDOMINI WHERE ID =" & CType(Me.Page, Object).vIdCondominio
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$chiamante$", "CONTO CORRENTE:")
                'contenuto = Replace(contenuto, "$den_chiamante$", myReader1("DENOMINAZIONE"))
                'contenuto = Replace(contenuto, "$dettaglio$", myReader1("DENOMINAZIONE"))
                'contenuto = Replace(contenuto, "$sc_rata$", par.FormattaData(txtScadenza.Value))
                'contenuto = Replace(contenuto, "$iban$", par.IfNull(myReader1("IBAN"), "n.d."))

                '*****************SCRITTURA TABELLA DETTAGLI dettagli
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "") & "</td></tr>"
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> IBAN: " & ibanFornitore & "</td></tr>"
                tb1 = tb1 & "<tr><td style='tex-align: left; font-size:14pt;font-family :Arial ;'> cod. fiscale: " & par.IfNull(myReader1("COD_FISCALE"), "") & "</td><tr></table>"
                '*****************FINE SCRITTURA DETTAGLI

            End If
            myReader1.Close()

            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_MOROSITA WHERE ID =" & txtidMorosita.Value
            'myReader1 = par.cmd.ExecuteReader
            'If myReader1.Read Then

            '    S = S & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>dell' Esercizio Finanziario dal: " & par.FormattaData(myReader1("DATA_INIZIO")) & " al " & par.FormattaData(myReader1("DATA_FINE")) & " </td>"
            'tb1 = tb1 & "</table>"
            'contenuto = Replace(contenuto, "$dettagli_chiamante$", "")
            'contenuto = Replace(contenuto, "$fornitori$", tb1)
            contenuto = Replace(contenuto, "$fornitori$", tb1)
            contenuto = Replace(contenuto, "$grigliaM$", "")
            contenuto = Replace(contenuto, "$copia$", "COPIA DI ")

            'End If
            'myReader1.Close()

            Dim idvocePf As String = ""
            par.cmd.CommandText = "SELECT PAGAMENTI.*, prenotazioni.*,SISCOM_MI.GETDATA(PAGAMENTI.DATA_SCADENZA) AS D_SCAD,T_ESERCIZIO_FINANZIARIO.INIZIO AS INIZIO_ESERCIZIO,T_ESERCIZIO_FINANZIARIO.FINE AS FINE_ESERCIZIO FROM siscom_mi.prenotazioni,SISCOM_MI.PAGAMENTI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE prenotazioni.id_pagamento = pagamenti.id and PAGAMENTI.ID = " & ID & " AND PF_VOCI.ID = prenotazioni.ID_VOCE_PF AND PF_VOCI.ID_PIANO_FINANZIARIO = PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID"
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$anno$", par.IfNull(myReader1("ANNO"), ""))
                contenuto = Replace(contenuto, "$progr$", par.IfNull(myReader1("PROGR"), ""))
                contenuto = Replace(contenuto, "$dettagli_chiamante$", "12000X01")
                contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(myReader1("DATA_EMISSIONE")))
                contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(myReader1("DATA_STAMPA")))
                contenuto = Replace(contenuto, "$TOT$", Format(par.IfNull(myReader1("IMPORTO_APPROVATO"), 0), "##,##0.00"))
                contenuto = Replace(contenuto, "$TOTSING$", "€." & Format(par.IfNull(myReader1("IMPORTO_APPROVATO"), 0), "##,##0.00"))

                '*****************SCRITTURA TABELLA CENTRALE DETTAGLI PAGAMENTO
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE"), "MOROSITA' CONDOMINIALE") & "</td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"

                tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>IMPORTO RATA €.</td>"
                tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(par.IfNull(myReader1("IMPORTO_APPROVATO"), 0), "##,##0.00") & "</td>"
                tb2 = tb2 & "</tr></table>"
                contenuto = Replace(contenuto, "$dettagli$", tb2)
                '*****************FINE SCRITTURA DETTAGLI
                contenuto = Replace(contenuto, "$imp_letterale$", "EURO " & NumeroInLettere(Format(par.IfNull(myReader1("IMPORTO_APPROVATO"), 0), "##,##0.00")))
                idvocePf = myReader1("ID_VOCE_PF")

                Dim modalita As String
                Dim condizione As String

                par.cmd.CommandText = "select descrizione from siscom_mi.tipo_modalita_pag where id =" & par.IfNull(myReader1("ID_TIPO_MODALITA_PAG"), -1)
                modalita = par.IfNull(par.cmd.ExecuteScalar, "")
                par.cmd.CommandText = "select descrizione from siscom_mi.tipo_pagamento where id =" & par.IfNull(myReader1("ID_TIPO_PAGAMENTO"), -1)
                condizione = par.IfNull(par.cmd.ExecuteScalar, "")
                contenuto = Replace(contenuto, "$modalita$", par.IfEmpty(modalita, "n.d."))
                contenuto = Replace(contenuto, "$condizione$", par.IfEmpty(condizione, "n.d."))
                contenuto = Replace(contenuto, "$dscadenza$", par.IfNull(myReader1("D_SCAD"), "---"))


            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE id = " & idvocePf
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$cod_capitolo$", par.IfNull(myReader1("CODICE"), 0))
                contenuto = Replace(contenuto, "$voce_pf$", par.IfNull(myReader1("DESCRIZIONE"), 0))

                contenuto = Replace(contenuto, "$finanziamento$", "Gestione Comune di Milano")
            End If
            myReader1.Close()
            contenuto = Replace(contenuto, "$annobp$", par.AnnoBPPag(ID))

            par.cmd.CommandText = "SELECT COD_ANA FROM OPERATORI WHERE ID IN (SELECT ID_OPERATORE FROM SISCOM_MI.EVENTI_PAGAMENTI WHERE ID_PAGAMENTO = " & ID & " AND COD_EVENTO = 'F98' )"
            myReader1 = par.cmd.ExecuteReader

            If myReader1.Read Then
                Matricola = par.IfNull(myReader1("COD_ANA"), "n.d.")
            End If

            myReader1.Close()


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
            pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "AttPagamento_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\NuoveImm\"))
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','AttPagamento','');self.close();</script>")




        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabMorosita"
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

End Class
