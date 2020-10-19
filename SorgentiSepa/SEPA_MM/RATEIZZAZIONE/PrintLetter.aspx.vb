Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class RATEIZZAZIONE_PrintLetter
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If
        If Not IsPostBack Then
            RiconosciDebito()
        End If

    End Sub

    Private Sub RiconosciDebito()
        Try
            If Not String.IsNullOrEmpty(Request.QueryString("IDBOLL")) Then
                '*******************APERURA CONNESSIONE*********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                Dim sr1 As StreamReader
                If Not String.IsNullOrEmpty(Request.QueryString("IDDICH")) Then
                    sr1 = New StreamReader(Server.MapPath("..\TestoModelli\RiconoscDebitoNEW.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Else
                    sr1 = New StreamReader(Server.MapPath("..\TestoModelli\RiconoscDebito.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                End If
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()

                Dim CodContratto As String = ""
                par.cmd.CommandText = "SELECT (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTECONTRATTO," _
                    & "(CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE COD_FISCALE END) AS CFIVA, INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,scale_edifici.descrizione AS scala,unita_immobiliari.interno, indirizzi.localita " _
                    & "FROM SISCOM_MI.ANAGRAFICA, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,siscom_mi.scale_edifici " _
                    & "WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = BOL_BOLLETTE.ID_CONTRATTO " _
                    & "AND UNITA_IMMOBILIARI.ID = BOL_BOLLETTE.ID_UNITA AND " _
                    & "SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID  AND scale_edifici.ID(+) = unita_immobiliari.id_scala AND BOL_BOLLETTE.ID IN (" & Session.Item("IDBOLLETTE") & ")"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                If lettore.Read Then

                    contenuto = Replace(contenuto, "$INTESTATARIO$", par.IfNull(lettore("INTECONTRATTO"), 0))

                    contenuto = Replace(contenuto, "$NUMALLOGGIO$", par.IfNull(lettore("interno"), 0))


                    contenuto = Replace(contenuto, "$INDIRIZZO$", par.IfNull(lettore("descrizione"), 0))

                    contenuto = Replace(contenuto, "$CIVICO$", par.IfNull(lettore("civico"), 0))

                    contenuto = Replace(contenuto, "$LOCALITA$", par.IfNull(lettore("LOCALITA"), 0))

                End If
                lettore.Close()
                Dim idContratto As Long = 0
                par.cmd.CommandText = "SELECT BOL_BOLLETTE.*, COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.BOL_BOLLETTE WHERE RAPPORTI_UTENZA.ID = BOL_BOLLETTE.ID_CONTRATTO AND BOL_BOLLETTE.ID IN (" & Session.Item("IDBOLLETTE") & ")"
                lettore = par.cmd.ExecuteReader

                If lettore.Read Then
                    contenuto = Replace(contenuto, "$CODCONTRATTO$", par.IfNull(lettore("COD_CONTRATTO"), 0))
                    CodContratto = par.IfNull(lettore("COD_CONTRATTO"), 0)
                    idContratto = par.IfNull(lettore("ID_CONTRATTO"), 0)
                End If
                lettore.Close()
                par.cmd.CommandText = "SELECT SUM ((NVL (IMPORTO_TOTALE, 0) - NVL(bol_bollette.QUOTA_SIND_B,0)) -( NVL (IMPORTO_PAGATO, 0)- NVL(bol_bollette.QUOTA_SIND_PAGATA_B,0))) AS DEBITO FROM SISCOM_MI.BOL_BOLLETTE WHERE ID IN (" & Session.Item("IDBOLLETTE") & ")"
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    contenuto = Replace(contenuto, "$TOTBOLLETTA$", Format(par.IfNull(lettore("DEBITO"), 0), "##,##0.00"))

                End If
                lettore.Close()

                Dim ratStraord As Integer = 0

                If Not String.IsNullOrEmpty(Request.QueryString("IDDICH")) Then
                    contenuto = Replace(contenuto, "$DEBITO$", Format(RicavaDebito(idContratto, Request.QueryString("IDDICH")), "##,##0.00"))
                Else
                    contenuto = Replace(contenuto, "$DEBITO$", Format(par.CalcolaSaldoAttuale(idContratto), "##,##0.00"))
                End If


                contenuto = Replace(contenuto, "$DATAOGGI$", Format(Now, "dd/MM/yyyy"))


                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


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

                Dim nomefile As String = "RiconosciDebito" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\NuoveImm\"))



                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                Dim zipfic As String
                Dim NomeZipfile As String = Format(Now, "yyyyMMddHHmmss") & "_008_" & CodContratto
                zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\" & NomeZipfile & ".zip")

                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                '
                Dim strFile As String
                strFile = Server.MapPath("..\FileTemp\" & nomefile)
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

                'File.Delete(strFile)


                ' Response.Write("<script>window.open('../FileTemp/" & nomefile & "','RiconosciDebito','');self.close();</script>")
                Response.Redirect("..\FileTemp\" & nomefile, False)

            Else
                Response.Write("ERRORE MANCA BOLLETTA")

            End If
        Catch ex As Exception

            Session.Add("ERRORE", "Ricalcolo Canone:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Private Function RicavaDebito(ByVal IdContratto As Long, ByVal idDich As Long) As Decimal
        Dim DaChiudere As Boolean = False

        Dim TotSaldo As Double = 0
        Dim idGr As Integer = 0
        Dim idGrOrd As Integer = 0

        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            DaChiudere = True
            par.cmd = par.OracleConn.CreateCommand()
        End If

        par.cmd.CommandText = "SELECT NVL(BOL_RATEIZZAZIONI.SALDO,0) AS deb FROM siscom_mi.BOL_RATEIZZAZIONI WHERE FL_ANNULLATA=0 AND ID_DIC_REDDITI=" & idDich & " AND BOL_RATEIZZAZIONI.ID_CONTRATTO = " & IdContratto
        Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If MyReader.Read Then
            TotSaldo = par.IfNull(MyReader("DEB"), 0)
        End If
        MyReader.Close()

        If DaChiudere = True Then
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End If

        RicavaDebito = TotSaldo

    End Function
End Class
