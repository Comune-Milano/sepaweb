'*** LISTA RISULTATO MOROSITA REPORT Proviene da : RicercaReport.aspx

Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums


Partial Class MOROSITA_RisultatiReport
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""Portale.aspx""</script>")
        End If


        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If Not IsPostBack Then

            Response.Flush()

            Cerca()
            Elabora()

        End If

    End Sub


    Public Property sStringaSQL() As String
        Get
            If Not (ViewState("par_sStringaSQL") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL") = value
        End Set

    End Property

    Private Sub Cerca()
        Dim sStringaSQL1 As String = ""
        Dim sValore As String = ""
        Dim sCompara As String = ""


        Dim sValoreComune As String = ""
        Dim sValoreIndirizzo As String = ""
        Dim sValoreCognome As String = ""
        Dim sValoreNome As String = ""
        Dim sValoreCivico As String = ""

        Dim sValoreTribunale As String = ""
        Dim sOrdinamento As String = ""


        sValoreComune = Request.QueryString("CO")
        sValoreIndirizzo = Request.QueryString("IN")

        sValoreCognome = Request.QueryString("CG")
        sValoreNome = Request.QueryString("NM")
        sValoreCivico = Request.QueryString("CI")

        sValoreTribunale = Request.QueryString("TR")

        sOrdinamento = Request.QueryString("ORD")



        sStringaSQL = "select MOROSITA_LEGALI.ID as ID,trim(COGNOME) as COGNOME,trim(MOROSITA_LEGALI.NOME) as NOME, " _
                         & " RTRIM(LTRIM(TIPO_INDIRIZZO||' '||INDIRIZZO|| ' N. '||CIVICO)) as INDIRIZZO," _
                         & " trim(SEPA.COMUNI_NAZIONI.NOME) as COMUNE, " _
                         & " MOROSITA_LEGALI.CAP as CAP,trim(TEL_1) as TEL_1,trim(TEL_2) as TEL_2,trim(CELL) as CELL, trim(FAX) as FAX,trim(EMAIL) as EMAIL,trim(NOTE) as NOTE" _
                   & " from  SISCOM_MI.MOROSITA_LEGALI,SEPA.COMUNI_NAZIONI " _
                   & " where  SISCOM_MI.MOROSITA_LEGALI.COD_COMUNE=SEPA.COMUNI_NAZIONI.COD (+) "



        '*** COGNOME 
        If Strings.Trim(sValoreCognome) <> "" Then
            sValore = Strings.UCase(sValoreCognome)
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If

            If sStringaSQL1 <> "" Then sStringaSQL1 = sStringaSQL1 & " and "
            sStringaSQL1 = sStringaSQL1 & "  UPPER(COGNOME) " & sCompara & " '" & par.PulisciStrSql(UCase(sValore)) & "' "
        End If
        '********************************

        '*** NOME 
        If Strings.Trim(sValoreNome) <> "" Then
            sValore = Strings.UCase(sValoreNome)
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If

            If sStringaSQL1 <> "" Then sStringaSQL1 = sStringaSQL1 & " and "
            sStringaSQL1 = sStringaSQL1 & "  UPPER(NOME) " & sCompara & " '" & par.PulisciStrSql(UCase(sValore)) & "' "
        End If
        '********************************


        '*** COMUNE 
        If Strings.Trim(sValoreComune) <> "-1" Then
            sValore = Strings.UCase(sValoreComune)
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If

            If sStringaSQL1 <> "" Then sStringaSQL1 = sStringaSQL1 & " and "
            sStringaSQL1 = sStringaSQL1 & "  UPPER(COD_COMUNE) " & sCompara & " '" & par.PulisciStrSql(UCase(sValore)) & "' "
        End If
        '********************************


        '*** INDIRIZZO 
        If Strings.Trim(sValoreIndirizzo) <> "" Then
            sValore = Strings.UCase(sValoreIndirizzo)
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If

            If sStringaSQL1 <> "" Then sStringaSQL1 = sStringaSQL1 & " and "
            sStringaSQL1 = sStringaSQL1 & "  UPPER(INDIRIZZO) " & sCompara & " '" & par.PulisciStrSql(UCase(sValore)) & "' "
        End If
        '********************************

        '*** CIVICO 
        If Strings.Trim(sValoreCivico) <> "" Then
            sValore = Strings.UCase(sValoreCivico)
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If

            If sStringaSQL1 <> "" Then sStringaSQL1 = sStringaSQL1 & " and "
            sStringaSQL1 = sStringaSQL1 & "  UPPER(CIVICO) " & sCompara & " '" & par.PulisciStrSql(UCase(sValore)) & "' "
        End If
        '********************************


        If par.IfEmpty(sValoreTribunale, "-1") <> "-1" Then
            If sStringaSQL1 <> "" Then sStringaSQL1 = sStringaSQL1 & " and "
            sStringaSQL1 = sStringaSQL1 & "  ID_TRIBUNALI_COMPETENTI=" & sValoreTribunale
        End If


        Select Case sOrdinamento
            Case "COGNOME_NOME"
                sOrdinamento = " order by COGNOME, NOME"
            Case "INDIRIZZO"
                sOrdinamento = " order by COD_COMUNE, INDIRIZZO, CIVICO"
            Case Else
                sOrdinamento = " order by COGNOME, NOME"
        End Select

        If sStringaSQL1 <> "" Then sStringaSQL1 = " and " & sStringaSQL1
        sStringaSQL = sStringaSQL & sStringaSQL1 & sOrdinamento


    End Sub


    Sub Elabora()
        Dim FlagConnessione As Boolean

        Dim TestoPagina As String = ""
        Dim myExcelFile As New CM.ExcelFile

        Dim sNomeFile As String
        Dim NomeFilePDF As String

        Dim sValore As String = ""
        Dim sCompara As String = ""
        Dim i, K As Long
        Dim dt As New Data.DataTable


        Try

            Me.txtNomeFile.Value = Format(Now, "yyyyMMddHHmmss")
            sNomeFile = "ExportLegali_" & Me.txtNomeFile.Value


            dt.Columns.Add("ID")
            dt.Columns.Add("COGNOME")
            dt.Columns.Add("NOME")
            dt.Columns.Add("INDIRIZZO")
            dt.Columns.Add("COMUNE")
            dt.Columns.Add("CAP")
            dt.Columns.Add("TEL_1")
            dt.Columns.Add("TEL_2")
            dt.Columns.Add("CELL")
            dt.Columns.Add("FAX")
            dt.Columns.Add("EMAIL")
            dt.Columns.Add("NOTE")


            Dim row As System.Data.DataRow

            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\") & sNomeFile & ".xls")

                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COGNOME", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "NOME", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "INDIRIZZO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "COMUNE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "CAP", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "TELEFONO 1", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "TELEFONO 2", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "CELLULARE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "FAX", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "E-MAIL", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "NOTE", 12)

                .SetColumnWidth(1, 2, 20)
                .SetColumnWidth(3, 4, 25)
                .SetColumnWidth(5, 10, 15)
                .SetColumnWidth(11, 11, 30)


                K = 2

                'PDF ***********************
                NomeFilePDF = "ReportLegaliMorosita_" & Me.txtNomeFile.Value
                TestoPagina = "<p style='font-family: ARIAL; font-size: 14pt; font-weight: bold; text-align: center;'>ELENCO LEGALI MOROSITA: </p></br>"

                TestoPagina = TestoPagina & "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                          & "<td align='left' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>COGNOME</td>" _
                                          & "<td align='left' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>NOME</td>" _
                                          & "<td align='left' style='border-bottom-style: dashed; width:30%; border-bottom-width: 1px; border-bottom-color: #000000'>INDIRIZZO</td>" _
                                          & "<td align='left' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>COMUNE</td>" _
                                          & "<td align='left' style='border-bottom-style: dashed; width:5%; border-bottom-width: 1px; border-bottom-color: #000000'>CAP</td>" _
                                          & "<td align='left' style='border-bottom-style: dashed; width:5%; border-bottom-width: 1px; border-bottom-color: #000000'>TELEFONO 1</td>" _
                                          & "<td align='left' style='border-bottom-style: dashed; width:5%; border-bottom-width: 1px; border-bottom-color: #000000'>TELEFONO 2</td>" _
                                          & "<td align='left' style='border-bottom-style: dashed; width:5%; border-bottom-width: 1px; border-bottom-color: #000000'>CELLULARE</td>" _
                                          & "<td align='left' style='border-bottom-style: dashed; width:5%; border-bottom-width: 1px; border-bottom-color: #000000'>FAX</td>" _
                                          & "<td align='left' style='border-bottom-style: dashed; width:5%; border-bottom-width: 1px; border-bottom-color: #000000'>E-MAIL</td>" _
                                          & "<td align='left' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>NOTE</td>" _
                                          & "</tr>"
                '*************************************************

                ' APRO CONNESSIONE
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    FlagConnessione = True
                End If


                par.cmd.CommandText = sStringaSQL
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader1.Read()

                    row = dt.NewRow()
                    row.Item("ID") = par.IfNull(myReader1("ID"), -1)
                    row.Item("COGNOME") = par.IfNull(myReader1("COGNOME"), "")
                    row.Item("NOME") = par.IfNull(myReader1("NOME"), "")

                    row.Item("INDIRIZZO") = par.IfNull(myReader1("INDIRIZZO"), "")
                    row.Item("COMUNE") = par.IfNull(myReader1("COMUNE"), "")
                    row.Item("CAP") = par.IfNull(myReader1("CAP"), "")
                    row.Item("TEL_1") = par.IfNull(myReader1("TEL_1"), "")
                    row.Item("TEL_2") = par.IfNull(myReader1("TEL_2"), "")
                    row.Item("CELL") = par.IfNull(myReader1("CELL"), "")
                    row.Item("FAX") = par.IfNull(myReader1("FAX"), "")
                    row.Item("EMAIL") = par.IfNull(myReader1("EMAIL"), "")
                    row.Item("NOTE") = par.IfNull(myReader1("NOTE"), "")



                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(myReader1("COGNOME"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(myReader1("NOME"), ""))

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(myReader1("INDIRIZZO"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(myReader1("COMUNE"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(myReader1("CAP"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(myReader1("TEL_1"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(myReader1("TEL_2"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(myReader1("CELL"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(myReader1("EMAIL"), ""))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(myReader1("NOTE"), ""))


                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("COGNOME"), "") & "</td>" _
                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("NOME"), "") & "</td>" _
                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("INDIRIZZO"), "") & "</td>" _
                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("COMUNE"), "") & "</td>" _
                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("CAP"), "") & "</td>" _
                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("TEL_1"), "") & "</td>" _
                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("TEL_2"), "") & "</td>" _
                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("CELL"), "") & "</td>" _
                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("EMAIL"), "") & "</td>" _
                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("NOTE"), "") & "</td>"
                    TestoPagina = TestoPagina & "</tr>"

                    K = K + 1
                    dt.Rows.Add(row)

                Loop
                myReader1.Close()


                If K = 2 Then
                    .CloseFile()

                    File.Delete(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))


                    row = dt.NewRow()

                    row.Item("ID") = "-1"
                    row.Item("COGNOME") = ""
                    row.Item("NOME") = ""
                    row.Item("INDIRIZZO") = ""
                    row.Item("COMUNE") = ""
                    row.Item("CAP") = ""
                    row.Item("TEL_1") = ""
                    row.Item("TEL_2") = ""
                    row.Item("CELL") = ""
                    row.Item("FAX") = ""
                    row.Item("EMAIL") = ""
                    row.Item("NOTE") = ""

                    dt.Rows.Add(row)

                    DataGrid1.DataSource = dt
                    DataGrid1.DataBind()
                    Me.Label1.Text = dt.Rows.Count - 1
                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If

                    Me.txtNomeFile.Value = ""
                    Exit Sub
                End If

                DataGrid1.DataSource = dt
                DataGrid1.DataBind()

                .CloseFile()
            End With


            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")

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

            '*** PDF

            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\") & NomeFilePDF & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(TestoPagina)
            sr.Close()

            Dim url As String = NomeFilePDF
            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")

            Dim pdfConverter As PdfConverter = New PdfConverter

            If Licenza <> "" Then
                pdfConverter.LicenseKey = Licenza
            End If

            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter.PdfDocumentOptions.ShowHeader = False
            pdfConverter.PdfDocumentOptions.ShowFooter = False
            pdfConverter.PdfDocumentOptions.LeftMargin = 20
            pdfConverter.PdfDocumentOptions.RightMargin = 20
            pdfConverter.PdfDocumentOptions.TopMargin = 5
            pdfConverter.PdfDocumentOptions.BottomMargin = 5
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter.PdfDocumentOptions.ShowHeader = False
            pdfConverter.PdfFooterOptions.FooterText = ("")
            pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter.PdfFooterOptions.DrawFooterLine = False
            pdfConverter.PdfFooterOptions.PageNumberText = ""
            pdfConverter.PdfFooterOptions.ShowPageNumber = False


            pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\FileTemp\") & NomeFilePDF & ".htm", Server.MapPath("..\FileTemp\") & NomeFilePDF & ".pdf")
            IO.File.Delete(Server.MapPath("..\FileTemp\") & NomeFilePDF & ".htm")
            'Response.Redirect("..\FileTemp\" & NomeFilePDF & ".pdf")

            For i = 0 To 10000
            Next

            'Response.Write("<script>window.open('../FileTemp/" & NomeFilePDF & ".pdf','','');self.close();</script>")
            'Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")
            'Response.Write("<script>var cazzo=window.open('../FileTemp/" & NomeFilePDF & ".pdf','','');cazzo.focus();</script>") 'nella stessa pagina chiede dove salvare
            'Response.Write("<script>window.open('" & Server.MapPath("..\FileTemp\") & "ReportMorosita_" & Me.txtNomeFile.Value & ".pdf','','');</script>")


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

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il legale: " & Replace(e.Item.Cells(1).Text, "'", "\'") & " " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il legale: " & Replace(e.Item.Cells(1).Text, "'", "\'") & " " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtID.Value = "" Or txtID.Value = "-1" Then
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            Session.Add("ID", Me.txtID.Value)

            Dim sValoreComune As String = ""
            Dim sValoreIndirizzo As String = ""
            Dim sValoreCognome As String = ""
            Dim sValoreNome As String = ""
            Dim sValoreCivico As String = ""

            Dim sValoreTribunale As String = ""

            Dim sOrdinamento As String = ""


            sValoreComune = Request.QueryString("CO")
            sValoreIndirizzo = Request.QueryString("IN")

            sValoreCognome = Request.QueryString("CG")
            sValoreNome = Request.QueryString("NM")
            sValoreCivico = Request.QueryString("CI")

            sValoreTribunale = Request.QueryString("TR")

            sOrdinamento = Request.QueryString("ORD")

            Response.Write("<script>location.replace('Legali.aspx?CO=" & par.VaroleDaPassare(sValoreComune) _
                                                             & "&IN=" & par.VaroleDaPassare(sValoreIndirizzo) _
                                                             & "&CI=" & par.VaroleDaPassare(sValoreCivico) _
                                                             & "&CG=" & par.VaroleDaPassare(sValoreCognome) _
                                                             & "&NM=" & par.VaroleDaPassare(sValoreNome) _
                                                             & "&TR=" & par.VaroleDaPassare(sValoreTribunale) _
                                                             & "&ORD=" & sOrdinamento _
                                                    & "');</script>")


        End If

    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click

        Response.Write("<script>document.location.href=""RicercaLegali.aspx""</script>")

    End Sub



    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        If Trim(Me.txtNomeFile.Value) <> "" Then

            Response.Redirect("..\FileTemp\ExportLegali_" & Me.txtNomeFile.Value & ".zip")
        Else
            Response.Write("<script>alert('Nessun legale trovato!')</script>")
        End If
    End Sub

    Protected Sub btnStampa_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        If Trim(Me.txtNomeFile.Value) <> "" Then

            Response.Write("<script>window.open('../FileTemp/ReportLegaliMorosita_" & Me.txtNomeFile.Value & ".pdf','','');</script>")
        Else
            Response.Write("<script>alert('Nessun legale trovato!')</script>")
        End If
    End Sub
End Class
