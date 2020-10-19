Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Fondo_solidarieta_PrintPresentazione
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            idContr.Value = Request.QueryString("IDC")
            dataPr.Value = Request.QueryString("DPR")
            StampaDomanda()
        End If
    End Sub

    Private Sub StampaDomanda()
        Try
            connData.apri()

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("DomandaPresentazione.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim CodContratto As String = ""
            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO, (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END ) AS INTECONTRATTO " _
                & "FROM SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.RAPPORTI_UTENZA " _
                & "WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
                & "SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND " _
                & "RAPPORTI_UTENZA.ID=SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & idContr.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(lettore("INTECONTRATTO"), 0))
                contenuto = Replace(contenuto, "$codContratto$", par.IfNull(lettore("cod_contratto"), 0))
            End If
            lettore.Close()

            contenuto = Replace(contenuto, "$dataOggi$", Format(Now, "dd/MM/yyyy"))

            par.cmd.CommandText = "select * from SISCOM_MI.DOMANDE_FONDO_SOLIDARIETA where id_contratto=" & idContr.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                contenuto = Replace(contenuto, "$dataPres$", par.FormattaData(dt.Rows(0).Item("DATA_PRESENTAZIONE")))
                contenuto = Replace(contenuto, "$idTelematico$", "Numero: " & dt.Rows(0).Item("ID") & "/2016")
            End If

            contenuto = Replace(contenuto, "$nomeZona$", "Municipio 0" & Request.QueryString("NZ"))

            connData.chiudi()

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
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "PresentazContributoSolid" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\NuoveImm\"))

            Response.Redirect("..\FileTemp\" & nomefile, False)

            File.Delete(nomefile)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "StampaDomanda:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
