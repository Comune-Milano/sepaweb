Imports ExpertPdf.HtmlToPdf

Partial Class Contratti_DettRateizz
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../CONDOMINI/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        lblCapitale.Text = ""
        lblInteresse.Text = ""
        lblNumRate.Text = ""
        lblSingRata.Text = ""
        RiempiTabella()
       
    End Sub


    Protected Sub RiempiTabella()

        Try
            Dim idBoll As Long = Request.QueryString("IDB")
            Dim idRateizz As Long
            Dim stringaSQL As String
            Dim RIGA As System.Data.DataRow

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT BOL_BOLLETTE.ID_RATEIZZAZIONE,(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTECONTRATTO," _
                    & "(CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE COD_FISCALE END) AS CFIVA, INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,scale_edifici.descrizione AS scala,unita_immobiliari.interno " _
                    & "FROM SISCOM_MI.ANAGRAFICA, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,siscom_mi.scale_edifici " _
                    & "WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = BOL_BOLLETTE.ID_CONTRATTO " _
                    & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND UNITA_IMMOBILIARI.ID = BOL_BOLLETTE.ID_UNITA AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID  " _
                    & "AND scale_edifici.ID(+) = unita_immobiliari.id_scala AND BOL_BOLLETTE.ID IN (" & idBoll & ")"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                lblSubtitle.Text = par.IfNull(lettore("INTECONTRATTO"), "") & ", " & par.IfNull(lettore("DESCRIZIONE"), "") & " civ." & par.IfNull(lettore("CIVICO"), "") & " sc." & par.IfNull(lettore("SCALA"), "") & " int." & par.IfNull(lettore("INTERNO"), "")
                idRateizz = par.IfNull(lettore("ID_RATEIZZAZIONE"), 0)
            End If
            lettore.Close()

            'par.cmd.CommandText = "select * from siscom_mi.BOL_RATEIZZAZIONI_DETT WHERE ID_BOLLETTA = " & idBoll
            'lettore = par.cmd.ExecuteReader
            'If lettore.Read Then
            '    idRateizz = par.IfNull(lettore("ID_RATEIZZAZIONE"), 0)
            'End If
            'lettore.Close()

            If idRateizz <> 0 Then
                stringaSQL = "select TO_CHAR(BOL_BOLLETTE.IMPORTO_PAGATO,'9G999G990D99') AS IMP_PAGATO,ID_BOLLETTA,TO_CHAR(TO_DATE(BOL_RATEIZZAZIONI_DETT.DATA_EMISSIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_EMISSIONE," _
                    & "TO_CHAR(TO_DATE(BOL_RATEIZZAZIONI_DETT.DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_SCADENZA," _
                    & " TO_CHAR(IMPORTO_RATA,'9G999G990D99') AS IMPORTO_RATA,TO_CHAR(QUOTA_CAPITALI,'9G999G990D99') AS QUOTA_CAPITALI,TO_CHAR(QUOTA_INTERESSI,'9G999G990D99') AS QUOTA_INTERESSI," _
                    & "TO_CHAR(RESIDUO,'9G999G990D99') as RESIDUO,(CASE WHEN NUM_RATA = '0' THEN 'ANTICIPO' ELSE TO_CHAR(BOL_RATEIZZAZIONI_DETT.NUM_RATA) END) AS NUM_RATA " _
                    & "FROM siscom_mi.BOL_RATEIZZAZIONI_DETT,siscom_mi.BOL_BOLLETTE WHERE BOL_BOLLETTE.ID(+) = BOL_RATEIZZAZIONI_DETT.ID_BOLLETTA AND BOL_RATEIZZAZIONI_DETT.ID_RATEIZZAZIONE=" & idRateizz & " ORDER BY BOL_RATEIZZAZIONI_DETT.NUM_RATA ASC"

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(stringaSQL, par.OracleConn)
                Dim dt As New Data.DataTable
                da.Fill(dt)

                par.cmd.CommandText = "SELECT TO_CHAR(SUM(BOL_BOLLETTE.IMPORTO_PAGATO),'9G999G990D99') AS IMPORTO_PAG,TO_CHAR(SUM(BOL_RATEIZZAZIONI_DETT.IMPORTO_RATA),'9G999G990D99') as IMP_RATA," _
                    & "TO_CHAR(SUM(BOL_RATEIZZAZIONI_DETT.QUOTA_CAPITALI),'9G999G990D99') as SUMCAPITALI,TO_CHAR(SUM(BOL_RATEIZZAZIONI_DETT.QUOTA_INTERESSI),'9G999G990D99') as SUMINTERESSI " _
                    & "FROM siscom_mi.BOL_RATEIZZAZIONI_DETT,siscom_mi.BOL_BOLLETTE WHERE BOL_BOLLETTE.ID(+) = BOL_RATEIZZAZIONI_DETT.ID_BOLLETTA AND BOL_RATEIZZAZIONI_DETT.ID_RATEIZZAZIONE=" & idRateizz & ""
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    RIGA = dt.NewRow()
                    RIGA.Item("NUM_RATA") = "TOTALE"
                    RIGA.Item("IMPORTO_RATA") = par.IfNull(lettore("IMP_RATA"), "0")
                    RIGA.Item("QUOTA_CAPITALI") = par.IfNull(lettore("SUMCAPITALI"), "0")
                    RIGA.Item("QUOTA_INTERESSI") = par.IfNull(lettore("SUMINTERESSI"), "0")
                    'RIGA.Item("RESIDUO") = par.IfNull(lettore("SUMRESIDUO"), "0")
                    RIGA.Item("IMP_PAGATO") = par.IfNull(lettore("IMPORTO_PAG"), "0")
                    dt.Rows.Add(RIGA)
                    lblCapitale.Text = " Capitale €. " & par.IfNull(lettore("SUMCAPITALI"), "") & ""
                End If
                lettore.Close()

                DataGridRate.DataSource = dt
                DataGridRate.DataBind()

                For Each di As DataGridItem In DataGridRate.Items
                    If di.Cells(8).Text.Contains(idBoll) Then
                        di.ForeColor = Drawing.Color.Red
                        For i As Integer = 0 To di.Cells.Count - 1
                            di.Cells(i).Font.Bold = True
                        Next
                    End If
                    If di.Cells(0).Text.Contains("TOTALE") Then
                        di.Cells(0).Font.Bold = True
                        di.BackColor = Drawing.ColorTranslator.FromHtml("#FFFF99")
                        For j As Integer = 0 To di.Cells.Count - 1
                            di.Cells(j).Font.Bold = True
                            di.Cells(j).ForeColor = Drawing.Color.Indigo
                        Next
                    End If
                Next

                par.cmd.CommandText = "select * from siscom_mi.bol_rateizzazioni where id=" & idRateizz & ""
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblInteresse.Text &= "- Interesse " & par.IfNull(lettore("TASSO_INTERESSE"), "0") & "%"
                    lblNumRate.Text = " - N° Rate " & par.IfNull(lettore("NUM_RATE"), "0") & ""
                End If
                lettore.Close()

                par.cmd.CommandText = "SELECT * FROM siscom_mi.BOL_RATEIZZAZIONI_DETT WHERE ID_RATEIZZAZIONE=" & idRateizz & ""
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblSingRata.Text &= " - Importo Singola Rata €. " & par.IfNull(lettore("IMPORTO_RATA"), "0") & ""
                End If
                lettore.Close()
            Else
                lblSubtitle.Visible = False
                'lblErrore.Text = "<br/>Piano di rateizzazione non disponibile per la bolletta specificata"
                btnPrintToPdf.Visible = False
                Response.Write("<script>alert('Attenzione! Piano di rateizzazione non disponibile per la bolletta specificata.')</script>")
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            lblErrore.Text = ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Protected Sub btnPrintToPdf_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPrintToPdf.Click
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

            pdfConverter1.PdfHeaderOptions.TextArea = New TextArea(10, 50, " " & Me.lblCapitale.Text & " " & Me.lblInteresse.Text & " " & Me.lblNumRate.Text & " " & Me.lblSingRata.Text, New System.Drawing.Font(New System.Drawing.FontFamily("Arial"), 8, System.Drawing.GraphicsUnit.Point))
            pdfConverter1.PdfHeaderOptions.TextArea.TextAlign = HorizontalTextAlign.Left

            Dim nomefile As String = "PianoRateizz_" & Format(Now, "yyyyMMddHHmm") & ".pdf"

            pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & nomefile)
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','Piano_Rateizz','');</script>")

        Catch ex As Exception
            lblErrore.Text = ex.Message
        End Try

    End Sub

End Class
