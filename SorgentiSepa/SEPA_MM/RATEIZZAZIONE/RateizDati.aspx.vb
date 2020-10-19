Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class RATEIZZAZIONE_RateizDati
    Inherits PageSetIdMode
    Dim dt As Data.DataTable
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If Session.Item("OPERATORE") = "" Then
                Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            End If

            Dim Str As String
            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='../CONDOMINI/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"


            Response.Write(Str)

            'If Not IsPostBack Then

            If Not String.IsNullOrEmpty(Request.QueryString("IDRAT")) Then
                Dim giorniScad As Integer = 0
                Dim tasso As Decimal = 0

                '******APERTURA CONNESSIONE*****
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Me.lblTitolo.Text = "PIANO DI RATEIZZAZIONE"
                par.cmd.CommandText = "select * from siscom_mi.bol_rateizzazioni where id = " & Request.QueryString("IDRAT")
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    tasso = par.IfNull(lettore("tasso_interesse"), -1)
                    Me.lblCapitale.Text = par.IfNull(lettore("imp_anticipo"), 0) + par.IfNull(lettore("imp_residuo"), 0)
                    Me.lblInteresse.Text = Format(par.IfEmpty(tasso, 0), "")


                    'par.cmd.CommandText = "SELECT (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTECONTRATTO," _
                    '                    & "(CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE COD_FISCALE END) AS CFIVA, INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,scale_edifici.descrizione AS scala,unita_immobiliari.interno " _
                    '                    & "FROM SISCOM_MI.ANAGRAFICA, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,siscom_mi.scale_edifici " _
                    '                    & "WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = BOL_BOLLETTE.ID_CONTRATTO " _
                    '                    & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND UNITA_IMMOBILIARI.ID = BOL_BOLLETTE.ID_UNITA AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID  " _
                    '                    & "AND scale_edifici.ID(+) = unita_immobiliari.id_scala AND BOL_BOLLETTE.ID IN (" & Request.QueryString("IDBOL") & ")"



                    par.cmd.CommandText = "SELECT (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTECONTRATTO," _
                                        & "(CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE COD_FISCALE END) AS CFIVA, INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,scale_edifici.descrizione AS scala,unita_immobiliari.interno " _
                                        & "FROM siscom_mi.ANAGRAFICA, siscom_mi.SOGGETTI_CONTRATTUALI, siscom_mi.UNITA_IMMOBILIARI, siscom_mi.INDIRIZZI, siscom_mi.SCALE_EDIFICI,siscom_mi.UNITA_CONTRATTUALE " _
                                        & "WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.id_anagrafica AND SOGGETTI_CONTRATTUALI.id_contratto = " & Request.QueryString("IDCONTRATTO") _
                                        & " AND SOGGETTI_CONTRATTUALI.cod_tipologia_occupante = 'INTE' AND UNITA_CONTRATTUALE.ID_CONTRATTO = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                                        & "AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.id_indirizzo = INDIRIZZI.ID " _
                                        & "AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.id_scala"
                End If
                lettore.Close()
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    Me.lblSubtitle.Text = lettore("INTECONTRATTO") & " , " & lettore("DESCRIZIONE") & " civ." & lettore("CIVICO") & " sc." & lettore("SCALA") & " int." & lettore("INTERNO")
                End If
                lettore.Close()
                Dim table As New Data.DataTable
                par.cmd.CommandText = "SELECT num_rata AS numrata, " _
                                    & "to_char(to_date(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS emissione, " _
                                    & "to_char(to_date(data_scadenza,'yyyymmdd'),'dd/mm/yyyy') AS scadenza, " _
                                    & "trim(TO_CHAR(importo_rata,'9G999G999G999G999G990D99')) AS importorata, " _
                                    & "trim(TO_CHAR(quota_capitali,'9G999G999G999G999G990D99')) AS quotacapitali, " _
                                    & "trim(TO_CHAR(quota_interessi,'9G999G999G999G999G990D99')) AS quotainteressi, " _
                                    & "trim(TO_CHAR(residuo,'9G999G999G999G999G990D99')) AS importoresiduo " _
                                    & "FROM siscom_mi.bol_rateizzazioni_dett WHERE num_rata > 0 AND id_rateizzazione = " & Request.QueryString("IDRAT") & " and fl_annullata = 0 order  by num_rata asc"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(table)

                DataGridRate.DataSource = table
                DataGridRate.DataBind()

                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Me.lblNumRate.Text = table.Rows.Count
                Me.lblImpRata.Text = table.Rows(0).Item("IMPORTORATA")

                Response.Flush()


            Else
               
                Dim giorniScad As Integer = 0
                Dim tasso As Decimal = 0
                '******APERTURA CONNESSIONE*****
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "SELECT TASSO FROM SISCOM_MI.TAB_INTERESSI_LEGALI WHERE ANNO = (SELECT MAX(ANNO) FROM SISCOM_MI.TAB_INTERESSI_LEGALI) "
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                If lettore.Read Then
                    tasso = par.IfNull(lettore("TASSO"), 0)
                End If
                lettore.Close()

                par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE DESCRIZIONE = 'GIORNO DEL MESE SCADENZA RATEIZZAZIONE'"
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    giorniScad = lettore(0)
                End If
                lettore.Close()

                'CONTROLLO PELLEGRI
                If String.IsNullOrEmpty(Request.QueryString("RS")) Then


                    If Not String.IsNullOrEmpty(Request.QueryString("IMPRATA")) Then
                        Dim msgAnomalia As String = ""
                        par.VerificaImportoMinimo(0, Request.QueryString("IMPRATA").Replace(".", ""), Request.QueryString("CAPITALE").ToString.Replace(".", ""), Request.QueryString("EMISSIONE"), tasso, giorniScad, msgAnomalia)
                        If Not String.IsNullOrEmpty(msgAnomalia) Then
                            Response.Write("<script>alert('Operazione interrotta!\n" & msgAnomalia & "');self.close();</script>")

                            Exit Sub
                        End If
                    Else
                        Dim msgAnomalia As String = ""
                        par.VerificaImportoMinimo(Request.QueryString("NRATE"), 0, Request.QueryString("CAPITALE").ToString.Replace(".", ""), Request.QueryString("EMISSIONE"), tasso, giorniScad, msgAnomalia)
                        If Not String.IsNullOrEmpty(msgAnomalia) Then
                            Response.Write("<script>alert('Operazione interrotta!\n" & msgAnomalia & "');self.close();</script>")

                            Exit Sub
                        End If
                    End If
                End If
                'FINE CONTROLLO PELLEGRI

                'par.cmd.CommandText = "SELECT (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTECONTRATTO," _
                '                    & "(CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE COD_FISCALE END) AS CFIVA, INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,scale_edifici.descrizione AS scala,unita_immobiliari.interno " _
                '                    & "FROM SISCOM_MI.ANAGRAFICA, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,siscom_mi.scale_edifici " _
                '                    & "WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = BOL_BOLLETTE.ID_CONTRATTO " _
                '                    & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND UNITA_IMMOBILIARI.ID = BOL_BOLLETTE.ID_UNITA AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID  " _
                '                    & "AND scale_edifici.ID(+) = unita_immobiliari.id_scala AND BOL_BOLLETTE.ID IN (" & Request.QueryString("IDBOL") & ")"



                par.cmd.CommandText = "SELECT (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTECONTRATTO," _
                                    & "(CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE COD_FISCALE END) AS CFIVA, INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,scale_edifici.descrizione AS scala,unita_immobiliari.interno " _
                                    & "FROM siscom_mi.ANAGRAFICA, siscom_mi.SOGGETTI_CONTRATTUALI, siscom_mi.UNITA_IMMOBILIARI, siscom_mi.INDIRIZZI, siscom_mi.SCALE_EDIFICI,siscom_mi.UNITA_CONTRATTUALE " _
                                    & "WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.id_anagrafica AND SOGGETTI_CONTRATTUALI.id_contratto = " & Request.QueryString("IDCONTRATTO") _
                                    & " AND SOGGETTI_CONTRATTUALI.cod_tipologia_occupante = 'INTE' AND UNITA_CONTRATTUALE.ID_CONTRATTO = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                                    & "AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.id_indirizzo = INDIRIZZI.ID " _
                                    & "AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.id_scala"
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    Me.lblSubtitle.Text = lettore("INTECONTRATTO") & " , " & lettore("DESCRIZIONE") & " civ." & lettore("CIVICO") & " sc." & lettore("SCALA") & " int." & lettore("INTERNO")
                End If
                lettore.Close()


                par.cmd.CommandText = "SELECT (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTECONTRATTO, " _
                                    & "(CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE COD_FISCALE END) AS CFIVA ,"




                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Dim nRate As Integer = 0
                If Not String.IsNullOrEmpty(Request.QueryString("NRATE")) Then
                    nRate = Request.QueryString("NRATE")
                End If

                If Not String.IsNullOrEmpty(par.IfEmpty(Request.QueryString("CAPITALE"), 0)) And Not String.IsNullOrEmpty(par.IfEmpty(Request.QueryString("EMISSIONE"), "")) Then

                    If Not String.IsNullOrEmpty(Request.QueryString("NRATE")) Then
                        dt = par.Ammortamento(Request.QueryString("CAPITALE").ToString.Replace(".", ""), Request.QueryString("NRATE"), tasso, 12, Request.QueryString("EMISSIONE"), giorniScad)
                    ElseIf Not String.IsNullOrEmpty(Request.QueryString("IMPRATA")) Then
                        dt = par.AmmortamentoPerRata(Request.QueryString("CAPITALE").ToString.Replace(".", ""), Request.QueryString("IMPRATA").Replace(".", ""), tasso, 12, Request.QueryString("EMISSIONE"), giorniScad, nRate)
                    End If
                    Me.lblCapitale.Text = Format(par.IfEmpty(Request.QueryString("CAPITALE"), ""), "")
                    Me.lblInteresse.Text = Format(par.IfEmpty(tasso, 0), "")
                    Me.lblNumRate.Text = dt.Rows.Count
                    Me.lblImpRata.Text = dt.Rows(0).Item("IMPORTORATA")

                    DataGridRate.DataSource = dt
                    DataGridRate.DataBind()


                    Response.Flush()

                End If

            End If


            If Request.QueryString("PRINT") = 1 Then
                PrintPdf()
            End If




        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnPrintToPdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPrintToPdf.Click
        PrintPdf()
    End Sub


    Private Sub PrintPdf()
        Dim Html As String = ""
        Dim stringWriter As New System.IO.StringWriter
        Dim sourcecode As New HtmlTextWriter(stringWriter)

        Try
            Me.DataGridRate.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString


            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 15
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 60
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfHeaderOptions.HeaderText = "PIANO DI RATEIZZAZIONE"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 14
            pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold

            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 10

            pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
            pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.Color.Blue
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleText = Me.lblSubtitle.Text
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.Color.Red
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag."
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            pdfConverter1.PdfHeaderOptions.TextArea = New TextArea(10, 50, "CAPITALE €. " & Me.lblCapitale.Text & " - INTERESSE " & Me.lblInteresse.Text & "% - N° Rate " & Me.lblNumRate.Text & " - Importo Singola Rata €. " & Me.lblImpRata.Text, New System.Drawing.Font(New System.Drawing.FontFamily("Arial"), 8, System.Drawing.GraphicsUnit.Point))
            pdfConverter1.PdfHeaderOptions.TextArea.TextAlign = HorizontalTextAlign.Left

            If String.IsNullOrEmpty(Request.QueryString("IDRAT")) Then
                pdfConverter1.PdfFooterOptions.TextArea = New TextArea(0, 0, "SIMULAZIONE PIANO DI RATEIZZAZIONE", New System.Drawing.Font(New System.Drawing.FontFamily("Arial"), 20, System.Drawing.GraphicsUnit.Point))
                pdfConverter1.PdfFooterOptions.TextArea.TextAlign = HorizontalTextAlign.Left
            End If


            Dim nomefile As String = "Rateizzaz" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & nomefile)
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','Rateizza','');</script>")

        Catch ex As Exception
            'Me.lblErrore.Visible = True
            'lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender


    End Sub
End Class
