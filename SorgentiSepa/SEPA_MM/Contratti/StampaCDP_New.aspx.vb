Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_StampaCDP_New
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sUnita(19) As String
    Dim sDecina(9) As String
    Dim Totale As Double = 0

    Public Property IndicePagamento() As Long
        Get
            If Not (ViewState("par_IndicePagamento") Is Nothing) Then
                Return CLng(ViewState("par_IndicePagamento"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IndicePagamento") = value
        End Set
    End Property

    Public Property IndiceBolletta() As String
        Get
            If Not (ViewState("par_IndiceBolletta") Is Nothing) Then
                Return CLng(ViewState("par_IndiceBolletta"))
            Else
                Return "-1"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndiceBolletta") = value
        End Set
    End Property

    Public Property TipoPagamentoFornitore() As String
        Get
            If Not (ViewState("par_TipoPagamentoFornitore") Is Nothing) Then
                Return CLng(ViewState("par_TipoPagamentoFornitore"))
            Else
                Return "NULL"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_TipoPagamentoFornitore") = value
        End Set
    End Property

    Public Property ModalitaPagamentoFornitore() As String
        Get
            If Not (ViewState("par_ModalitaPagamentoFornitore") Is Nothing) Then
                Return CLng(ViewState("par_ModalitaPagamentoFornitore"))
            Else
                Return "NULL"
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_ModalitaPagamentoFornitore") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            IndiceBolletta = Request.QueryString("ID")
            CaricaDati(CDbl(IndiceBolletta))
        End If
        txtDScadenza.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Private Sub CaricaDati(ByVal IdBolletta As Double)
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT PAGAMENTI.DESCRIZIONE_BREVE,PAGAMENTI.ID_IBAN_FORNITORE,PAGAMENTI.DATA_SCADENZA,PAGAMENTI.DESCRIZIONE,PAGAMENTI.ID AS IDP,PAGAMENTI.DATA_EMISSIONE,FORNITORI.ID_TIPO_MODALITA_PAG,FORNITORI.ID_TIPO_PAGAMENTO,FORNITORI.ID FROM SISCOM_MI.FORNITORI,SISCOM_MI.PAGAMENTI,SISCOM_MI.ELENCO_BOLL_CDP WHERE FORNITORI.ID=PAGAMENTI.ID_FORNITORE AND PAGAMENTI.ID=ELENCO_BOLL_CDP.ID_CDP AND ELENCO_BOLL_CDP.ID_BOLLETTA=" & IdBolletta
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                'par.cmd.CommandText = "SELECT FORNITORI_IBAN.ID, FORNITORI_IBAN.IBAN FROM SISCOM_MI.FORNITORI_IBAN WHERE FORNITORI_IBAN.FL_ATTIVO = 1 AND ID_FORNITORE=" & lettore("ID") & " order by FL_ATTIVO ASC"
                par.cmd.CommandText = "SELECT FORNITORI_IBAN.ID, FORNITORI_IBAN.IBAN FROM SISCOM_MI.FORNITORI_IBAN WHERE ID_FORNITORE=" & lettore("ID") & " order by FL_ATTIVO ASC"
                Dim lettore1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Me.cmbIbanFornitore.Items.Clear()
                While lettore1.Read
                    cmbIbanFornitore.Items.Add(New ListItem(par.IfNull(lettore1("IBAN"), " "), par.IfNull(lettore1("ID"), "")))
                End While
                lettore1.Close()
                IndicePagamento = CDbl(lettore("IDP"))
                TipoPagamentoFornitore = par.IfNull(lettore("ID_TIPO_PAGAMENTO"), "NULL")
                ModalitaPagamentoFornitore = par.IfNull(lettore("ID_TIPO_MODALITA_PAG"), "NULL")
                lblDescrizione.Text = par.IfNull(lettore("DESCRIZIONE"), "")
                lblDataEmissione.Text = par.FormattaData(par.IfNull(lettore("DATA_EMISSIONE"), ""))
                If par.IfNull(lettore("DATA_SCADENZA"), 0) = 0 Then
                    txtDScadenza.Text = par.FormattaData(CalcolaDataScadenza(lettore("ID_TIPO_MODALITA_PAG"), lettore("ID_TIPO_PAGAMENTO"), lettore("DATA_EMISSIONE"), IndicePagamento))
                    stampato.Value = "0"
                Else
                    txtDScadenza.Text = par.FormattaData(par.IfNull(lettore("DATA_SCADENZA"), ""))
                    cmbIbanFornitore.SelectedIndex = -1
                    If par.IfNull(lettore("ID_IBAN_FORNITORE"), -1) <> -1 Then
                        cmbIbanFornitore.Items.FindByValue(par.IfNull(lettore("ID_IBAN_FORNITORE"), "")).Selected = True
                    End If
                    txtDescrizione.Text = par.IfNull(lettore("DESCRIZIONE_BREVE"), "")

                    txtDescrizione.Enabled = False
                    txtDScadenza.Enabled = False
                    cmbIbanFornitore.Enabled = False
                    'If stampato.Value = "0" Or stampato.Value = "" Then
                    '    Response.Write("<script>alert('Questo certificato di pagamento è stato già stampato. Premere Conferma per visualizzare.');</script>")
                    'End If
                    stampato.Value = "1"
                    Creastampa()
                    End If
            End If
            lettore.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Function CalcolaDataScadenza(ByVal TipoModalita As Integer, ByVal tipoPagamento As Integer, ByVal DataScadPagamento As String, ByVal IDP As Long) As String
        CalcolaDataScadenza = ""
        If Not String.IsNullOrEmpty(TipoModalita) Then
            Dim Table As String = ""
            Dim Column As String = ""
            Dim FlSomma As Integer = 0
            Dim DaySum As Integer = 0
            par.cmd.CommandText = "SELECT tab_rif,fld_rif,fl_somma_giorni FROM siscom_mi.TAB_DATE_MODALITA_PAG WHERE ID = (SELECT id_data_riferimento FROM siscom_mi.TIPO_MODALITA_PAG WHERE ID = " & TipoModalita & ")"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                Table = par.IfNull(lettore("tab_rif"), "")
                Column = par.IfNull(lettore("fld_rif"), "")
                FlSomma = par.IfNull(lettore("fl_somma_giorni"), "")
            End If
            lettore.Close()

            If Not String.IsNullOrEmpty(Table) And Not String.IsNullOrEmpty(Column) Then
                par.cmd.CommandText = "select " & Column & " from siscom_Mi." & Table & " where id = " & IDP
                CalcolaDataScadenza = par.IfNull(par.cmd.ExecuteScalar, "")
            End If
            If String.IsNullOrEmpty(CalcolaDataScadenza) Then
                CalcolaDataScadenza = DataScadPagamento
            End If
            If Not String.IsNullOrEmpty(CalcolaDataScadenza) Then
                If FlSomma = 1 Then
                    par.cmd.CommandText = "select nvl(num_giorni,0) from siscom_mi.tipo_pagamento where id = " & tipoPagamento
                    DaySum = par.IfNull(par.cmd.ExecuteScalar, 0)

                    If DaySum > 0 Then
                        CalcolaDataScadenza = Date.Parse(par.FormattaData(CalcolaDataScadenza), New System.Globalization.CultureInfo("it-IT", False)).AddDays(DaySum).ToString("dd/MM/yyyy")
                        CalcolaDataScadenza = par.AggiustaData(CalcolaDataScadenza)
                    End If
                End If
            End If
        End If

        If String.IsNullOrEmpty(CalcolaDataScadenza) Then
            CalcolaDataScadenza = DataScadPagamento
        End If

    End Function

    Protected Sub btnPagamento_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPagamento.Click

        If txtConferma.Value = 1 Then
            If stampato.Value = "0" Then
                If par.AggiustaData(Me.txtDScadenza.Text) < Format(Now, "yyyyMMdd") Then
                    Response.Write("<script>alert('La data scadenza non può essere precedente a quella odierna!');</script>")
                    Exit Sub
                End If
            End If
            Creastampa()
        End If
    End Sub

    Private Sub Creastampa()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.myTrans = par.OracleConn.BeginTransaction()

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "select * from siscom_mi.pagamenti where id=" & IndicePagamento
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                If par.IfNull(myReader1("DATA_SCADENZA"), -1) = -1 Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_APPROVATO= IMPORTO_PRENOTATO, ID_STATO = 2,DATA_SCADENZA='" & par.AggiustaData(Me.txtDScadenza.Text) & "' where ID_PAGAMENTO = " & IndicePagamento
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.PAGAMENTI SET DATA_STAMPA='" & Format(Now, "yyyyMMdd") & "',ID_IBAN_FORNITORE=" & par.IfEmpty(par.PulisciStrSql(Me.cmbIbanFornitore.SelectedValue.ToString), "NULL") & "," _
                           & "data_scadenza='" & par.AggiustaData(Me.txtDScadenza.Text) & "',ID_TIPO_PAGAMENTO=" & TipoPagamentoFornitore & ",ID_TIPO_MODALITA_PAG=" & ModalitaPagamentoFornitore & ",DESCRIZIONE_BREVE='" & par.PulisciStrSql(txtDescrizione.Text) & "' where id=" & IndicePagamento
                    par.cmd.ExecuteNonQuery()
                    stampato.Value = "1"
                End If
            End If
            myReader1.Close()

            PdfPagamento(IndicePagamento)
            par.myTrans.Commit()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'btnPagamento.Visible = False
            If stampato.Value = "0" Then
                CaricaDati(CDbl(IndiceBolletta))
            End If
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey111", "self.close();", True)
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub PdfPagamento(ByVal ID As String)
        'Try

        Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ModelloPagamento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
        Dim contenuto As String = sr1.ReadToEnd()
        sr1.Close()
        Dim InizioES As String = ""
        Dim FineEs As String = ""
        Dim Matricola As String = ""

        Dim tb1 As String = "<table style='width:100%;'>"
        Dim tb2 As String = "<table style='width:100%;'>"

        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim lettDettagli As Oracle.DataAccess.Client.OracleDataReader
        par.cmd.CommandText = "SELECT FORNITORI.*," _
                            & "(select descrizione from siscom_mi.tipo_modalita_pag where id = fornitori.id_tipo_modalita_pag) as modalita, " _
                            & "(select descrizione from siscom_mi.tipo_pagamento where id = fornitori.id_tipo_pagamento) as tipo_pag " _
                            & "FROM SISCOM_MI.FORNITORI, SISCOM_MI.PAGAMENTI WHERE PAGAMENTI.ID =" & ID & " and fornitori.ID = PAGAMENTI.ID_FORNITORE"
        myReader1 = par.cmd.ExecuteReader
        If myReader1.Read Then
            contenuto = Replace(contenuto, "$chiamante$", "CONTO CORRENTE:")
            contenuto = Replace(contenuto, "$modalita$", Replace(par.IfNull(myReader1("modalita"), ""), "NULLO", ""))
            contenuto = Replace(contenuto, "$condizione$", Replace(par.IfNull(myReader1("tipo_pag"), ""), "INDEFINITO", ""))

            '*****************SCRITTURA TABELLA DETTAGLI dettagli
            tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "") & "</td>"
            tb1 = tb1 & "</tr>"
            If cmbIbanFornitore.Items.Count <> 0 Then
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> IBAN: " & par.IfEmpty(Me.cmbIbanFornitore.SelectedItem.ToString.ToUpper, "") & "</td></tr>"
            Else
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> IBAN:</td></tr>"
            End If

            tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> cod. fiscale: " & par.IfNull(myReader1("COD_FISCALE"), "") & "</td><tr></table>"
            '*****************FINE SCRITTURA DETTAGLI
        End If
        myReader1.Close()
        contenuto = Replace(contenuto, "$dscadenza$", Me.txtDScadenza.Text)
        contenuto = Replace(contenuto, "$copia$", "")
        contenuto = Replace(contenuto, "$fornitori$", tb1)
        contenuto = Replace(contenuto, "$grigliaM$", "")
        contenuto = Replace(contenuto, "$annobp$", par.AnnoBPPag(ID))
        Dim idvocePf As String = ""
        par.cmd.CommandText = ""


        par.cmd.CommandText = "SELECT PAGAMENTI.*,PRENOTAZIONI.*, prenotazioni.data_scadenza as d_scad,T_ESERCIZIO_FINANZIARIO.INIZIO AS INIZIO_ESERCIZIO,T_ESERCIZIO_FINANZIARIO.FINE AS FINE_ESERCIZIO " _
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
            InizioES = "01/01/" & myReader1("d_scad").ToString.Substring(0, 4)
            FineEs = "31/12/" & myReader1("d_scad").ToString.Substring(0, 4)

            idvocePf = par.IfNull(myReader1("ID_VOCE_PF"), "")
            '*****************SCRITTURA TABELLA CENTRALE DETTAGLI PAGAMENTO
            tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE"), "N.D.") & "</td></tr>"
            tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
            tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
            tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>Note: " & par.IfNull(myReader1("DESCRIZIONE_BREVE"), "N.D.") & "</td></tr>"
            tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
            tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
            '*****************
            'par.cmd.CommandText = "SELECT COND_VOCI_SPESA.DESCRIZIONE, PRENOTAZIONI.* FROM SISCOM_MI.COND_VOCI_SPESA,SISCOM_MI.COND_VOCI_SPESA_PF,  SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_PAGAMENTO =" & ID _
            '                    & " AND COND_VOCI_SPESA.ID = COND_VOCI_SPESA_PF.ID_VOCE_COND AND (PRENOTAZIONI.ID_VOCE_PF = COND_VOCI_SPESA_PF.ID_VOCE_PF OR PRENOTAZIONI.ID_VOCE_PF_IMPORTO = COND_VOCI_SPESA_PF.ID_VOCE_PF_IMPORTO)"
            'lettDettagli = par.cmd.ExecuteReader
            'While lettDettagli.Read
            '    tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & par.IfNull(lettDettagli("DESCRIZIONE"), "n.d.") & " €.</td>"
            '    tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(par.IfNull(lettDettagli("IMPORTO_APPROVATO"), 0), "##,##0.00") & "</td>"
            '    tb2 = tb2 & "</tr>"
            '    If String.IsNullOrEmpty(idvocePf) Then
            '        idvocePf = lettDettagli("ID_VOCE_PF")
            '    Else
            '        idvocePf = idvocePf & "," & lettDettagli("ID_VOCE_PF")
            '    End If
            'End While
            'lettDettagli.Close()
            tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>TOTALE €.</td>"
            tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00") & "</td>"
            tb2 = tb2 & "</tr>"
            tb2 = tb2 & "</table>"
            contenuto = Replace(contenuto, "$dettagli$", tb2)
            '*****************FINE SCRITTURA DETTAGLI
            If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) > 0 Then
                contenuto = Replace(contenuto, "$imp_letterale$", "EURO " & NumeroInLettere(Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00")))
            Else
                contenuto = Replace(contenuto, "$imp_letterale$", "EURO " & NumeroInLettere(Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) * -1, "##,##0.00")))
            End If

        End If
            myReader1.Close()

            tb1 = "<table style='width:100%;'>"
            tb2 = "<table style='width:100%;'>"
            Dim tb3 As String = "<table style='width:100%;'>"
        par.cmd.CommandText = "SELECT PF_VOCI.*,PRENOTAZIONI.IMPORTO_PRENOTATO FROM SISCOM_MI.PF_VOCI, SISCOM_MI.PRENOTAZIONI WHERE PF_VOCI.ID IN (SELECT ID_VOCE_PF " _
                        & "FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO, SISCOM_MI.PAGAMENTI " _
                        & "WHERE PRENOTAZIONI.ID_PAGAMENTO = " & ID & " AND PAGAMENTI.ID = PRENOTAZIONI.ID_PAGAMENTO AND PF_VOCI.ID = PRENOTAZIONI.ID_VOCE_PF " _
                        & "AND PF_VOCI.ID_PIANO_FINANZIARIO = PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID )" _
                & " AND PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PRENOTAZIONI.ID_PAGAMENTO = " & ID
            myReader1 = par.cmd.ExecuteReader

            While myReader1.Read
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:12pt;font-family :Arial ;'> " & par.IfNull(myReader1("CODICE"), "") & "</td>"
                tb1 = tb1 & "</tr>"

            tb2 = tb2 & "<tr><td style='text-align: left; font-size:12pt;font-family :Arial ;'> " & UCase(Left(par.IfNull(myReader1("DESCRIZIONE"), ""), 1)) & Right(par.IfNull(myReader1("DESCRIZIONE"), ""), Len(par.IfNull(myReader1("DESCRIZIONE"), "")) - 1) & "</td>"
                tb2 = tb2 & "</tr>"

                tb3 = tb3 & "<tr><td style='text-align: right; font-size:12pt;font-family :Arial ;'> €." & Format(par.IfNull(myReader1("IMPORTO_PRENOTATO"), 0), "##,##0.00") & "</td>"
                tb3 = tb3 & "</tr>"

            End While

            tb1 = tb1 & "</table>"
            tb2 = tb2 & "</table>"
            tb3 = tb3 & "</table>"

            contenuto = Replace(contenuto, "$cod_capitolo$", tb1)
            contenuto = Replace(contenuto, "$voce_pf$", tb2)
            contenuto = Replace(contenuto, "$TOTSING$", tb3)

            myReader1.Close()

            par.cmd.CommandText = "SELECT COD_ANA FROM OPERATORI WHERE ID = " & Session.Item("ID_OPERATORE")
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
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
        'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "AttPagamento_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\NuoveImm\"))
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','AttPagamento','');</script>")

            '*********************CHIUSURA CONNESSIONE**********************


            'Catch ex As Exception
            '    Me.lblErrore.Visible = True
            '    lblErrore.Text = ex.Message
            'End Try

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
