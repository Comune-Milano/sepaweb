Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports System.Collections.Generic
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class Contabilita_CompMensiliFPAler
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            Try

                Dim TotAlLibe As Double = 0
                Dim TotAlOccu As Double = 0
                Dim TotBoLibe As Double = 0
                Dim TotBoOccu As Double = 0
                Dim TotNeLibe As Double = 0
                Dim TotNeOccu As Double = 0
                Dim TotRiga As Double = 0
                Dim SommaToTRiga As Double = 0
                Dim SommaTotIvat As Double = 0
                Dim whereDate As String = ""

                If Request.QueryString("M_INIZIO") <> "-1" Then

                    whereDate = " WHERE ANNO_MESE>=" & Request.QueryString("A_INIZIO") & Request.QueryString("M_INIZIO")
                    Me.lblTitolo.Text = lblTitolo.Text & " da " & NomeMeseDaNumero(Request.QueryString("M_INIZIO")) & "  " & Request.QueryString("A_INIZIO")
                    Me.lblTitolo2.Text = lblTitolo2.Text & " da " & NomeMeseDaNumero(Request.QueryString("M_INIZIO")) & "  " & Request.QueryString("A_INIZIO")
                    Me.lblTitolo3.Text = lblTitolo3.Text & " da " & NomeMeseDaNumero(Request.QueryString("M_INIZIO")) & "  " & Request.QueryString("A_INIZIO")

                End If

                If Request.QueryString("M_FINE") <> "-1" Then
                    If whereDate = "" Then
                        whereDate = " WHERE "
                    Else
                        whereDate = whereDate & " AND "
                    End If
                    whereDate = whereDate & " ANNO_MESE<=" & Request.QueryString("A_FINE") & Request.QueryString("M_FINE")
                    Me.lblTitolo.Text = lblTitolo.Text & " fino a " & NomeMeseDaNumero(Request.QueryString("M_FINE")) & "  " & Request.QueryString("A_FINE")
                    Me.lblTitolo2.Text = lblTitolo2.Text & " fino a " & NomeMeseDaNumero(Request.QueryString("M_FINE")) & "  " & Request.QueryString("A_FINE")
                    Me.lblTitolo3.Text = lblTitolo3.Text & " fino a " & NomeMeseDaNumero(Request.QueryString("M_FINE")) & "  " & Request.QueryString("A_FINE")

                End If


                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If


                Dim query As String
                query = "SELECT substr(anno_mese,0,4)as anno, Anno_mese, trim(TO_CHAR(AL_LIBERI_F,'9G999G999G990D99')) as AL_LIBERI_F ,trim(TO_CHAR(AL_OCCUPATI_F,'9G999G999G990D99')) as AL_OCCUPATI_F,trim(TO_CHAR(B_LIBERI_F,'9G999G999G990D99')) as B_LIBERI_F,trim(TO_CHAR(B_OCCUPATI_F,'9G999G999G990D99')) as B_OCCUPATI_F,trim(TO_CHAR(N_LIBERI_F,'9G999G999G990D99')) as N_LIBERI_F,trim(TO_CHAR(N_OCCUPATI_F,'9G999G999G990D99')) as N_OCCUPATI_F,'0' as TOT_M,trim(TO_CHAR(ivato_f,'9G999G999G990D99')) as TOT_IVATO FROM SISCOM_MI.COMPENSI_ALER_MESE" _
                & " " & whereDate & " ORDER BY ANNO_MESE ASC"

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(query, par.OracleConn)

                Dim dt As New Data.DataTable()

                da.Fill(dt)
                Dim row As Data.DataRow
                Dim iva As Decimal = 1.21
                For Each row In dt.Rows
                    TotRiga = 0


                    row.Item("ANNO_MESE") = AnnoMeseInLink(row.Item("ANNO_MESE"))

                    TotAlLibe = TotAlLibe + CDbl(par.IfNull(row.Item("AL_LIBERI_F"), 0))
                    TotRiga = TotRiga + par.IfNull(row.Item("AL_LIBERI_F"), 0)

                    TotAlOccu = TotAlOccu + CDbl(par.IfNull(row.Item("AL_OCCUPATI_F"), 0))
                    TotRiga = TotRiga + par.IfNull(row.Item("AL_OCCUPATI_F"), 0)

                    TotBoLibe = TotBoLibe + CDbl(par.IfNull(row.Item("B_LIBERI_F"), 0))
                    TotRiga = TotRiga + par.IfNull(row.Item("B_LIBERI_F"), 0)

                    TotBoOccu = TotBoOccu + CDbl(par.IfNull(row.Item("B_OCCUPATI_F"), 0))
                    TotRiga = TotRiga + par.IfNull(row.Item("B_OCCUPATI_F"), 0)

                    TotNeLibe = TotNeLibe + CDbl(par.IfNull(row.Item("N_LIBERI_F"), 0))
                    TotRiga = TotRiga + par.IfNull(row.Item("N_LIBERI_F"), 0)

                    TotNeOccu = TotNeOccu + CDbl(par.IfNull(row.Item("N_OCCUPATI_F"), 0))
                    TotRiga = TotRiga + par.IfNull(row.Item("N_OCCUPATI_F"), 0)


                    '************totale mensile per riga************************
                    row.Item("TOT_M") = Format(TotRiga, "##,##0.00")

                    'row.Item("TOT_IVATO") = Format((TotRiga * iva), "##,##0.00")

                    SommaToTRiga = SommaToTRiga + TotRiga
                    SommaTotIvat = SommaTotIvat + (TotRiga * 1.2)
                Next

                row = dt.NewRow
                row.Item("ANNO_MESE") = "T O T A L E"
                row.Item("AL_LIBERI_F") = Format(TotAlLibe, "##,##0.00")
                row.Item("AL_OCCUPATI_F") = Format(TotAlOccu, "##,##0.00")
                row.Item("B_LIBERI_F") = Format(TotBoLibe, "##,##0.00")
                row.Item("B_OCCUPATI_F") = Format(TotBoOccu, "##,##0.00")
                row.Item("N_LIBERI_F") = Format(TotNeLibe, "##,##0.00")
                row.Item("N_OCCUPATI_F") = Format(TotNeOccu, "##,##0.00")
                row.Item("TOT_M") = Format(SommaToTRiga, "##,##0.00")
                row.Item("TOT_IVATO") = Format(SommaTotIvat, "##,##0.00")

                dt.Rows.Add(row)


                DgvFacility.DataSource = dt
                DgvFacility.DataBind()
                Session.Add("DT_MENS_F", dt)
                '------------------------------------------------------------------------------

                row = Nothing
                TotAlLibe = 0
                TotAlOccu = 0
                TotBoLibe = 0
                TotBoOccu = 0
                TotNeLibe = 0
                TotNeOccu = 0
                SommaToTRiga = 0
                SommaTotIvat = 0
                query = "SELECT substr(anno_mese,0,4)as anno, Anno_mese, trim(TO_CHAR(AL_LIBERI_P,'9G999G999G990D99')) as AL_LIBERI_P ,trim(TO_CHAR(AL_OCCUPATI_P,'9G999G999G990D99')) as AL_OCCUPATI_P,trim(TO_CHAR(B_LIBERI_P,'9G999G999G990D99')) as B_LIBERI_P,trim(TO_CHAR(B_OCCUPATI_P,'9G999G999G990D99')) as B_OCCUPATI_P,trim(TO_CHAR(N_LIBERI_P,'9G999G999G990D99')) as N_LIBERI_P,trim(TO_CHAR(N_OCCUPATI_P,'9G999G999G990D99')) as N_OCCUPATI_P,'0' as TOT_M,trim(TO_CHAR(ivato_p,'9G999G999G990D99')) as TOT_IVATO  FROM SISCOM_MI.COMPENSI_ALER_MESE" _
                & " " & whereDate & " ORDER BY ANNO_MESE ASC"

                da = New Oracle.DataAccess.Client.OracleDataAdapter(query, par.OracleConn)

                dt = New Data.DataTable()
                da.Fill(dt)

                Dim row2 As Data.DataRow

                For Each row2 In dt.Rows
                    TotRiga = 0



                    TotAlLibe = TotAlLibe + par.IfNull(row2.Item("AL_LIBERI_P"), 0)
                    TotRiga = TotRiga + par.IfNull(row2.Item("AL_LIBERI_P"), 0)

                    TotAlOccu = TotAlOccu + par.IfNull(row2.Item("AL_OCCUPATI_P"), 0)
                    TotRiga = TotRiga + par.IfNull(row2.Item("AL_OCCUPATI_P"), 0)

                    TotBoLibe = TotBoLibe + par.IfNull(row2.Item("B_LIBERI_P"), 0)
                    TotRiga = TotRiga + par.IfNull(row2.Item("B_LIBERI_P"), 0)

                    TotBoOccu = TotBoOccu + par.IfNull(row2.Item("B_OCCUPATI_P"), 0)
                    TotRiga = TotRiga + par.IfNull(row2.Item("B_OCCUPATI_P"), 0)

                    TotNeLibe = TotNeLibe + par.IfNull(row2.Item("N_LIBERI_P"), 0)
                    TotRiga = TotRiga + par.IfNull(row2.Item("N_LIBERI_P"), 0)

                    TotNeOccu = TotNeOccu + par.IfNull(row2.Item("N_OCCUPATI_P"), 0)
                    TotRiga = TotRiga + par.IfNull(row2.Item("N_OCCUPATI_P"), 0)

                    row2.Item("ANNO_MESE") = AnnoMeseInLink(row2.Item("ANNO_MESE"))
                    '************totale mensile per riga************************
                    row2.Item("TOT_M") = Format(TotRiga, "##,##0.00")
                    row2.Item("TOT_IVATO") = Format((TotRiga * 1.2), "##,##0.00")

                    SommaToTRiga = SommaToTRiga + TotRiga
                    SommaTotIvat = SommaTotIvat + (TotRiga * 1.2)

                Next
                row2 = dt.NewRow
                row2.Item("ANNO_MESE") = "T O T A L E"

                row2.Item("AL_LIBERI_P") = Format(TotAlLibe, "##,##0.00")
                row2.Item("AL_OCCUPATI_P") = Format(TotAlOccu, "##,##0.00")
                row2.Item("B_LIBERI_P") = Format(TotBoLibe, "##,##0.00")
                row2.Item("B_OCCUPATI_P") = Format(TotBoOccu, "##,##0.00")
                row2.Item("N_LIBERI_P") = Format(TotNeLibe, "##,##0.00")
                row2.Item("N_OCCUPATI_P") = Format(TotNeOccu, "##,##0.00")
                row2.Item("TOT_M") = Format(SommaToTRiga, "##,##0.00")
                row2.Item("TOT_IVATO") = Format(SommaTotIvat, "##,##0.00")


                dt.Rows.Add(row2)

                DataGridCompProp.DataSource = dt
                DataGridCompProp.DataBind()
                Session.Add("DT_MENS_P", dt)

                '*********NUOVA TABELLA TOTALE MENSILI GENERALE******************

                query = "SELECT substr(anno_mese,0,4)as anno, Anno_mese,trim(TO_CHAR(TOTALE,'9G999G999G990D99')) as TOTALE,'0' as TOT_IVATO  FROM SISCOM_MI.COMPENSI_ALER_MESE" _
                        & " " & whereDate & " ORDER BY ANNO_MESE ASC"

                da = New Oracle.DataAccess.Client.OracleDataAdapter(query, par.OracleConn)

                dt = New Data.DataTable()
                da.Fill(dt)
                Dim TOT_TOTALI As Double = 0
                Dim TOT_I_TOTALI As Double = 0


                For Each row In dt.Rows
                    row.Item("ANNO_MESE") = NomeMeseDaNumero(row.Item("anno_mese").ToString.Substring(4, 2))
                    row.Item("TOT_IVATO") = Format((par.IfNull(row.Item("TOTALE"), 0) * 1.2), "##,##0.00")
                    TOT_TOTALI = TOT_TOTALI + par.IfNull(row.Item("TOTALE"), 0)
                    TOT_I_TOTALI = TOT_I_TOTALI + row.Item("TOT_IVATO")
                Next

                row2 = dt.NewRow
                row2.Item("ANNO_MESE") = "T O T A L E"
                row2.Item("TOTALE") = Format(TOT_TOTALI, "##,##0.00")
                row2.Item("TOT_IVATO") = Format(TOT_I_TOTALI, "##,##0.00")

                dt.Rows.Add(row2)



                DataGridCompTotale.DataSource = dt
                DataGridCompTotale.DataBind()
                Session.Add("DT_TOTALI", dt)

                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
        End If
    End Sub
    Private Function AnnoMeseInLink(ByVal annomese As String) As String
        If annomese <> "" Then
            AnnoMeseInLink = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('CompensiFacProp.aspx?MESE=" & annomese.Substring(4, 2) & "&ANNO=" & annomese.Substring(0, 4) & "','DettaglioMensile','');" & Chr(34) & ">" & NomeMeseDaNumero(annomese.Substring(4, 2)) & "</a>"

        Else
            AnnoMeseInLink = ""
        End If

        Return AnnoMeseInLink
    End Function
    Private Function NomeMeseDaNumero(ByVal Numero As Integer) As String

        NomeMeseDaNumero = ""
        If Numero = "01" Then
            NomeMeseDaNumero = "Gennaio"
        ElseIf Numero = "02" Then
            NomeMeseDaNumero = "Febbraio"
        ElseIf Numero = "03" Then
            NomeMeseDaNumero = "Marzo"
        ElseIf Numero = "04" Then
            NomeMeseDaNumero = "Aprile"
        ElseIf Numero = "05" Then
            NomeMeseDaNumero = "Maggio"
        ElseIf Numero = "06" Then
            NomeMeseDaNumero = "Giugno"
        ElseIf Numero = "07" Then
            NomeMeseDaNumero = "Luglio"
        ElseIf Numero = "08" Then
            NomeMeseDaNumero = "Agosto"
        ElseIf Numero = "09" Then
            NomeMeseDaNumero = "Settembre"
        ElseIf Numero = "10" Then
            NomeMeseDaNumero = "Ottobre"
        ElseIf Numero = "11" Then
            NomeMeseDaNumero = "Novembre"
        ElseIf Numero = "12" Then
            NomeMeseDaNumero = "Dicembre"
        End If

        Return NomeMeseDaNumero

    End Function


    Protected Sub btnExport0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport0.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            Dim dt As New Data.DataTable
            dt = Session.Item("DT_MENS_F")
            sNomeFile = "ExportCompMensileFacility_" & Format(Now, "yyyyMMddHHmmss")

            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
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


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "ANNO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "MESE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "COMP. ALLOGGI LIBERI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "COMP. ALLOGGI LOCATI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "COMP. BOX LIBERI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "COMP. BOX LOCATI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "COMP. USI DIVERSI LIBERI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "COMP. USI DIVERSI LOCATI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "TOTALE IMPONIBILE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "TOTALE IVATO", 12)

                K = 2
                For Each row In dt.Rows

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ANNO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ANNO_MESE").ToString.Substring(dt.Rows(i).Item("ANNO_MESE").ToString.IndexOf(">") + 1).Replace("</a>", ""), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("AL_LIBERI_F"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("AL_OCCUPATI_F"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("B_LIBERI_F"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("B_OCCUPATI_F"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("N_LIBERI_F"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("N_OCCUPATI_F"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TOT_M"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TOT_IVATO"), "")))

                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
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
            Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")

            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport1.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            Dim dt As New Data.DataTable
            dt = Session.Item("DT_MENS_P")
            sNomeFile = "ExportCompMensileFacility_" & Format(Now, "yyyyMMddHHmmss")

            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
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

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "ANNO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "MESE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "COMP. ALLOGGI LIBERI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "COMP. ALLOGGI LOCATI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "COMP. BOX LIBERI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "COMP. BOX LOCATI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "COMP. NEGOZI LIBERI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "COMP. NEGOZI LOCATI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "TOTALE IMPONIBILE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "TOTALE IVATO", 12)

                K = 2
                For Each row In dt.Rows

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ANNO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ANNO_MESE").ToString.Substring(dt.Rows(i).Item("ANNO_MESE").ToString.IndexOf(">") + 1).Replace("</a>", ""), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("AL_LIBERI_P"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("AL_OCCUPATI_P"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("B_LIBERI_P"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("B_OCCUPATI_P"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("N_LIBERI_P"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("N_OCCUPATI_P"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TOT_M"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TOT_IVATO"), "")))

                    i = i + 1
                    K = K + 1
                Next


                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
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
            Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")

            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport2.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            Dim dt As New Data.DataTable
            dt = Session.Item("DT_TOTALI")
            sNomeFile = "ExportCompMensileFacility_" & Format(Now, "yyyyMMddHHmmss")

            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
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

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "ANNO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "MESE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "TOTALE IMPONIBILE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "TOTALE IVATO", 12)

                K = 2
                For Each row In dt.Rows

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ANNO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ANNO_MESE").ToString.Substring(dt.Rows(i).Item("ANNO_MESE").ToString.IndexOf(">") + 1).Replace("</a>", ""), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TOTALE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TOT_IVATO"), "")))

                    i = i + 1
                    K = K + 1
                Next


                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
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
            Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")

            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class
