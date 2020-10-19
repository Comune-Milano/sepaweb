Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class Contratti_Report_DettagliAnnullate
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Dim ELENCO() As String
    Dim DT As New Data.DataTable
    Dim StringaSQL As String = ""



    Public Property KK() As Integer
        Get
            If Not (ViewState("par_KK") Is Nothing) Then
                Return CInt(ViewState("par_KK"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_KK") = value
        End Set
    End Property


    Public Property Tabella() As String
        Get
            If Not (ViewState("par_Tabella") Is Nothing) Then
                Return CStr(ViewState("par_Tabella"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tabella") = value
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                Dim I As Integer = 1
                Dim DESCRIZIONE_T As String = ""

                PAR.OracleConn.Open()
                par.SettaCommand(par)
                DESCRIZIONE_T = "BOLLETTE ANNULLATE</br>"


                DT.Columns.Add("N.BOLLETTA")
                DT.Columns.Add("RATA")
                DT.Columns.Add("INTESTATARIO")
                DT.Columns.Add("EMESSA")
                DT.Columns.Add("PAGATA")
                DT.Columns.Add("PERIODO")
                DT.Columns.Add("SCADENZA")
                DT.Columns.Add("INDIRIZZO")
                DT.Columns.Add("NOTE")
                DT.Columns.Add("IMPORTO")
                DT.Columns.Add("COD.CONTRATTO")


                Dim RIGA As System.Data.DataRow


                

                Dim TOTALE As Double = 0
                Response.Write(DESCRIZIONE_T & "</br>")
                Response.Write("<table style='width:100%;'><tr style='font-family: ARIAL; font-size: 10px; font-weight: bold'><td></td><td>N.BOLLETTA</td><td>RATA</td><td>COD.CONTRATTO</td><td>INTESTATARIO</td><td>EMESSA IL</td><td>PAGATA IL</td><td>PERIODO RIF.</td><td>SCADENZA</td><td>INDIRIZZO UNITA&#39;</td><td>NOTE</td><td style='text-align: right'>IMPORTO</td></tr>")

                RIGA = DT.NewRow()
                RIGA.Item("N.BOLLETTA") = "BOLLETTE ANNULLATE"
                RIGA.Item("RATA") = ""
                RIGA.Item("INTESTATARIO") = ""
                RIGA.Item("EMESSA") = ""
                RIGA.Item("PAGATA") = ""
                RIGA.Item("PERIODO") = ""
                RIGA.Item("SCADENZA") = ""
                RIGA.Item("INDIRIZZO") = ""
                RIGA.Item("NOTE") = ""
                RIGA.Item("IMPORTO") = ""
                RIGA.Item("COD.CONTRATTO") = ""
                DT.Rows.Add(RIGA)

                PAR.cmd.CommandText = Session.Item("REPORT2")
                Dim myReaderAa As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                Do While myReaderAa.Read
                    Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold;border-bottom-style: dashed; border-bottom-width: thin'><td style='border-bottom-style: dashed; border-bottom-width: thin'>" & I & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin'>" & Format(PAR.IfNull(myReaderAa("ID"), "0"), "0000000000") & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin'>" & RATA(PAR.IfNull(myReaderAa("N_RATA"), "")) & "/" & PAR.IfNull(myReaderAa("ANNO"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin'>" & RATA(PAR.IfNull(myReaderAa("COD_CONTRATTO"), "")) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin'><a href=" & Chr(34) & "..\..\Contabilita\DatiUtenza.aspx?C=RisUtenza&IDCONT=" & PAR.IfNull(myReaderAa("ID_CONTRATTO"), "-1") & "&IDANA=" & PAR.IfNull(myReaderAa("cod_affittuario"), "-1") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & PAR.IfNull(myReaderAa("INTESTATARIO"), "") & "</a></td><td style='border-bottom-style: dashed; border-bottom-width: thin'>" & PAR.FormattaData(PAR.IfNull(myReaderAa("DATA_EMISSIONE"), "")) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin'>" & PAR.FormattaData(PAR.IfNull(myReaderAa("DATA_PAGAMENTO"), "")) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin'>" & PAR.FormattaData(PAR.IfNull(myReaderAa("RIFERIMENTO_DA"), "")) & "--" & PAR.FormattaData(PAR.IfNull(myReaderAa("RIFERIMENTO_A"), "")) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin'>" & PAR.FormattaData(PAR.IfNull(myReaderAa("DATA_SCADENZA"), "")) & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin'>" & PAR.IfNull(myReaderAa("INDIRIZZO"), "") & " " & PAR.IfNull(myReaderAa("CAP_CITTA"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: thin'>" & PAR.IfNull(myReaderAa("NOTE"), "") & "</td><td style='text-align: right;border-bottom-style: dashed; border-bottom-width: thin'>" & Format(PAR.IfNull(myReaderAa("IMPORTO"), "0"), "0.00") & "</td></tr>")
                    RIGA = DT.NewRow()

                    RIGA.Item("N.BOLLETTA") = Format(PAR.IfNull(myReaderAa("ID"), "0"), "0000000000")
                    RIGA.Item("RATA") = RATA(PAR.IfNull(myReaderAa("N_RATA"), "")) & "/" & PAR.IfNull(myReaderAa("ANNO"), "")
                    RIGA.Item("INTESTATARIO") = PAR.IfNull(myReaderAa("INTESTATARIO"), "")
                    RIGA.Item("EMESSA") = PAR.FormattaData(PAR.IfNull(myReaderAa("DATA_EMISSIONE"), ""))
                    RIGA.Item("PAGATA") = PAR.FormattaData(PAR.IfNull(myReaderAa("DATA_PAGAMENTO"), ""))
                    RIGA.Item("PERIODO") = PAR.FormattaData(PAR.IfNull(myReaderAa("RIFERIMENTO_DA"), "")) & "--" & PAR.FormattaData(PAR.IfNull(myReaderAa("RIFERIMENTO_A"), ""))
                    RIGA.Item("SCADENZA") = PAR.FormattaData(PAR.IfNull(myReaderAa("DATA_SCADENZA"), ""))
                    RIGA.Item("INDIRIZZO") = PAR.IfNull(myReaderAa("INDIRIZZO"), "") & " " & PAR.IfNull(myReaderAa("CAP_CITTA"), "")
                    RIGA.Item("NOTE") = PAR.IfNull(myReaderAa("NOTE"), "")
                    RIGA.Item("IMPORTO") = Format(PAR.IfNull(myReaderAa("IMPORTO"), "0"), "0.00")
                    RIGA.Item("COD.CONTRATTO") = PAR.IfNull(myReaderAa("COD_CONTRATTO"), "")
                    DT.Rows.Add(RIGA)


                    TOTALE = TOTALE + CDbl(PAR.IfNull(myReaderAa("IMPORTO"), 0))
                    I = I + 1

                Loop
                myReaderAa.Close()

                PAR.cmd.Dispose()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Write("<tr style='font-family: ARIAL; font-size: 9px; font-weight: bold'><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>TOTALE</td><td style='text-align: right'>" & Format(TOTALE, "##,##0.00") & "</td></tr>")
                Response.Write("</table>")

                RIGA = DT.NewRow()
                RIGA.Item("N.BOLLETTA") = ""
                RIGA.Item("RATA") = ""
                RIGA.Item("INTESTATARIO") = ""
                RIGA.Item("EMESSA") = ""
                RIGA.Item("PAGATA") = ""
                RIGA.Item("PERIODO") = ""
                RIGA.Item("SCADENZA") = ""
                RIGA.Item("INDIRIZZO") = ""
                RIGA.Item("NOTE") = "totale"
                RIGA.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                RIGA.Item("COD.CONTRATTO") = ""
                DT.Rows.Add(RIGA)

                Session.Add("MIADT", DT)


            Catch ex As Exception
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try
        End If
    End Sub

    Function RATA(ByVal NUMERO As String) As String
        Select Case NUMERO
            Case "99"
                RATA = "MA"
            Case "999"
                RATA = "AU"
            Case "99999"
                RATA = "CO"
            Case Else
                RATA = NUMERO
        End Select
    End Function

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")

            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\..\FileTemp\" & sNomeFile & ".xls"))
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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "N.BOLLETTA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "RATA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "INTESTATARIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "EMESSA IL", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "PAGATA IL", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "PERIODO RIF.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "SCADENZA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "INDIRIZZO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "NOTE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "IMPORTO", 0)

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "COD.CONTRATTO", 0)


                K = 2
                For Each row In DT.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("N.BOLLETTA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("RATA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("INTESTATARIO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("EMESSA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("PAGATA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("PERIODO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("SCADENZA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("INDIRIZZO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NOTE"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("IMPORTO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("COD.CONTRATTO"), 0)))
                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\..\FileTemp\" & sNomeFile & ".xls")
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
            Response.Redirect("..\..\FileTemp\" & sNomeFile & ".zip")


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
