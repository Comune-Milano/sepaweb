Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports ExpertPdf.HtmlToPdf
Imports System.Math
Imports System.Drawing

Partial Class RiepilogoVariazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public TabellaRiepilogo As String = ""
    Public TabellaThead As String = ""
    Public Titolo As STRING= ""
    Dim dt2 As New Data.DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or (Session.Item("BP_VARIAZIONI_SL") <> 1 And Session.Item("BP_VARIAZIONI") <> 1) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        lblErrore.Visible = False
        Session.Remove("DT2RV")
        CaricaVoci()
    End Sub

    Protected Sub CaricaVoci()

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Session.Remove("DT2RV")

            Dim RiepilogoFinanziamentiAttribuiti As Decimal = 0
            Dim RiepilogoVariazioni As Decimal = 0
            Dim RiepilogoFinanziamentiVariati As Decimal = 0

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN " _
                & "WHERE T_ESERCIZIO_FINANZIARIO.ID = PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                & "AND PF_MAIN.ID='" & Request.QueryString("AN") & "' "


            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                Dim ANNO As String = par.IfNull(myReader("INIZIO"), "")
                If Len(ANNO) = 8 Then
                    Titolo = Left(ANNO, 4)
                End If
            End If
            myReader.Close()

            '##### QUERY VOCI #####
            'SELECT * FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_CAPITOLI WHERE PF_CAPITOLI.ID = PF_VOCI.ID_CAPITOLO AND id_piano_finanziario = 1 AND id_voce_madre 
            'IS NOT NULL AND INSTR(CODICE,'.',1,3)=0 
            '######################
            TabellaThead = "<tr align=""center"" style=""color: White; background-color: #507CD1; font-family: Arial; font-size: 9pt; font-weight: bold;"">" _
                & "<td align=""center"" style=""font-weight: bold; font-style: normal; text-decoration: none;  white-space: nowrap;"">CAP. BIL. COM.</td>" _
                & "<td align=""center"" style=""font-weight: bold; font-style: normal; text-decoration: none;  white-space: nowrap;"">COD</td>" _
                & "<td align=""center"" style=""font-weight: bold; font-style: normal; text-decoration: none;  white-space: nowrap;"">VOCE</td>" _
                & "<td align=""center"" style=""font-weight: bold; font-style: normal; text-decoration: none;  white-space: nowrap;"">FINANZIAMENTI ATTRIBUITI</td>" _
                & "<td align=""center"" style=""font-weight: bold; font-style: normal; text-decoration: none;  white-space: nowrap;"">VARIAZIONI</td>" _
                & "<td align=""center"" style=""font-weight: bold; font-style: normal; text-decoration: none;  white-space: nowrap;"">FINANZIAMENTI VARIATI</td><tr>"




            '§§§§§§§§§§§§§§§§§§§§§§§§
            '#### dt per export ####
            dt2.Clear()
            dt2.Columns.Clear()
            dt2.Columns.Add("CAP")
            dt2.Columns.Add("COD")
            dt2.Columns.Add("VOCE")
            dt2.Columns.Add("FINANZIAMENTI")
            dt2.Columns.Add("VARIAZIONI")
            dt2.Columns.Add("FINANZIAMENTI_VARIATI")
            '§§§§§§§§§§§§§§§§§§§§§§§§
            Dim RIGA As Data.DataRow

            '###########  RAGGRUPPAMENTO PER CAPITOLI ##############
            par.cmd.CommandText = "SELECT DISTINCT PF_CAPITOLI.COD,PF_CAPITOLI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_STRUTTURA,SISCOM_MI.PF_CAPITOLI " _
                & "WHERE (SELECT COUNT(*) FROM SISCOM_MI.PF_VOCI A WHERE A.ID_VOCE_MADRE = PF_VOCI.ID)= 0 " _
                & "AND ID_PIANO_FINANZIARIO='" & Request.QueryString("AN") & "' AND PF_VOCI.ID=PF_VOCI_STRUTTURA.ID_VOCE AND PF_CAPITOLI.ID=PF_VOCI.ID_CAPITOLO ORDER BY COD ASC"
            myReader = par.cmd.ExecuteReader

            While myReader.Read
                '########### SELEZIONE DELLE VOCI DEL BILANCIO ############
                'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_CAPITOLI,SISCOM_MI.PF_VOCI_STRUTTURA WHERE PF_CAPITOLI.ID = PF_VOCI.ID_CAPITOLO " _
                '    & "AND ID_PIANO_FINANZIARIO = '" & Request.QueryString("AN") & "' AND ID_VOCE_MADRE IS NOT NULL AND INSTR(CODICE,'.',1,3)=0 " _
                '    & "AND COD='" & par.IfNull(myReader("COD"), "") & "' " _
                '    & "AND PF_VOCI.ID=PF_VOCI_STRUTTURA.ID_VOCE"

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_CAPITOLI WHERE PF_CAPITOLI.ID = PF_VOCI.ID_CAPITOLO " _
                    & "AND ID_PIANO_FINANZIARIO = '" & Request.QueryString("AN") & "' AND ID_VOCE_MADRE IS NOT NULL AND INSTR(CODICE,'.',1,3)=0 " _
                    & "AND COD='" & par.IfNull(myReader("COD"), "") & "'"

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                Dim dt As New Data.DataTable()
                da.Fill(dt)

                'IMPOSTO NUMR PER IL ROWSPAN PER IL CAP. BIL. COM.
                Dim NUMR As Integer = 0

                Dim budgetTotale As Decimal = 0
                Dim variazioniTotale As Decimal = 0
                Dim finanziamentiVariatiTotale As Decimal = 0

                For Each r As Data.DataRow In dt.Rows
                    Dim ElVoci As String = ""
                    Dim Budget As Decimal = 0
                    Dim Variazioni As Decimal = 0
                    Dim finanziamentiVariati As Decimal = 0

                    '*******SELEZIONE DELLE VOCI FIGLIE ASSOCIATE ALLA VOCE PRINCIPALE
                    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                                      & "WHERE ID_PIANO_FINANZIARIO='" & Request.QueryString("AN") & "' " _
                                      & "AND (ID='" & r.Item("ID") & "' " _
                                      & "OR ID_VOCE_MADRE='" & r.Item("ID") & "' " _
                                      & "OR ID_VOCE_MADRE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE='" & r.Item("ID") & "')) " _
                                      & "ORDER BY CODICE"

                    Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader
                    LETTORE = par.cmd.ExecuteReader
                    While LETTORE.Read
                        If ElVoci = "" Then
                            ElVoci = par.IfNull(LETTORE(0), "")
                        Else
                            ElVoci = ElVoci & "," & par.IfNull(LETTORE(0), "")
                        End If
                    End While
                    LETTORE.Close()
                    '**********END ---- SELEZIONE DELLE VOCI FIGLIE ASSOCIATE ALLA VOCE PRINCIPALE


                    '**********SELEZIONE DELL'IMPORTO DI BUDGET STANZIATO
                    par.cmd.CommandText = "SELECT ID_VOCE,(NVL(VALORE_LORDO,0) + NVL(ASSESTAMENTO_VALORE_LORDO,0)) AS BUDGET " _
                        & ",PF_VOCI_STRUTTURA.VARIAZIONI " _
                        & "FROM SISCOM_MI.PF_VOCI_STRUTTURA,SISCOM_MI.PF_VOCI " _
                        & "WHERE ID_VOCE IN (" & ElVoci & ") " _
                        & "AND PF_VOCI.ID=PF_VOCI_STRUTTURA.ID_VOCE " _
                        & "AND ID_PIANO_FINANZIARIO='" & Request.QueryString("AN") & "'"
                    LETTORE = par.cmd.ExecuteReader
                    Budget = 0
                    Variazioni = 0
                    While LETTORE.Read

                        par.cmd.CommandText = "SELECT PF_VOCI.ID FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_STRUTTURA " _
                            & "WHERE PF_VOCI.ID_VOCE_MADRE='" & LETTORE("ID_VOCE") & "' " _
                            & "AND PF_VOCI.ID=PF_VOCI_STRUTTURA.ID_VOCE " _
                            & "AND ID_PIANO_FINANZIARIO='" & Request.QueryString("AN") & "'"

                        Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If lettore2.Read Then
                            '??
                        Else

                            Budget = Budget + Decimal.Parse(par.IfNull(LETTORE("BUDGET"), "0"))
                            Variazioni = Variazioni + Decimal.Parse(par.IfNull(LETTORE("VARIAZIONI"), "0"))
                        End If
                        lettore2.Close()
                    End While
                    LETTORE.Close()

                    '#### VARIAZIONI FINANZIAMENTI #####
                    finanziamentiVariati = Budget + Variazioni

                    If NUMR = 0 Then

                        TabellaRiepilogo = TabellaRiepilogo & "<tr style=""background-color: White; font-family: Arial; font-size: 8pt;"">" _
                         & "<td align=""center"" rowspan=""" & dt.Rows.Count & """ style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & par.IfNull(myReader("COD"), "") & "</td>" _
                         & "<td style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & r.Item("CODICE") & "</td>" _
                         & "<td align=""left"" style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & r.Item("DESCRIZIONE") & "</td>" _
                         & "<td align=""right""  style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(Budget, "##,##0.00") & "</td>" _
                         & "<td align=""right""  style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(Variazioni, "##,##0.00") & "</td>" _
                         & "<td align=""right""  style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(finanziamentiVariati, "##,##0.00") & "</td></tr>"
                        NUMR = NUMR + 1

                        RIGA = dt2.NewRow
                        RIGA.Item("CAP") = par.IfNull(myReader("COD"), "")
                        RIGA.Item("COD") = r.Item("CODICE")
                        RIGA.Item("VOCE") = r.Item("DESCRIZIONE")
                        RIGA.Item("FINANZIAMENTI") = Format(Budget, "##,##0.00")
                        RIGA.Item("VARIAZIONI") = Format(Variazioni, "##,##0.00")
                        RIGA.Item("FINANZIAMENTI_VARIATI") = Format(finanziamentiVariati, "##,##0.00")
                        dt2.Rows.Add(RIGA)


                    Else

                        TabellaRiepilogo = TabellaRiepilogo & "<tr style=""background-color: White; font-family: Arial; font-size: 8pt;"">" _
                         & "<td style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & r.Item("CODICE") & "</td>" _
                         & "<td align=""left"" style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & r.Item("DESCRIZIONE") & "</td>" _
                         & "<td align=""right""  style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(Budget, "##,##0.00") & "</td>" _
                         & "<td align=""right""  style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(Variazioni, "##,##0.00") & "</td>" _
                         & "<td align=""right""  style=""font-weight: normal; font-style: normal; text-decoration: none; white-space: nowrap;"" >" & Format(finanziamentiVariati, "##,##0.00") & "</td></tr>"

                        RIGA = dt2.NewRow
                        RIGA.Item("CAP") = par.IfNull(myReader("COD"), "")
                        RIGA.Item("COD") = r.Item("CODICE")
                        RIGA.Item("VOCE") = r.Item("DESCRIZIONE")
                        RIGA.Item("FINANZIAMENTI") = Format(Budget, "##,##0.00")
                        RIGA.Item("VARIAZIONI") = Format(Variazioni, "##,##0.00")
                        RIGA.Item("FINANZIAMENTI_VARIATI") = Format(finanziamentiVariati, "##,##0.00")
                        dt2.Rows.Add(RIGA)

                    End If

                    '############# TOTALI CAPITOLO PER CAPITOLO ################
                    budgetTotale = budgetTotale + Format(Budget, "##,##0.00")
                    variazioniTotale = variazioniTotale + Format(variazioniTotale, "##,##0.00")
                    finanziamentiVariatiTotale = Format(budgetTotale + variazioniTotale, "##,##0.00")

                Next

                '######## RIEPILOGO CAPITOLO PER CAPITOLO ##########
                TabellaRiepilogo = TabellaRiepilogo _
                     & "<tr style=""color: Red; background-color: Lavender; font-family: Arial; font-size: 8pt;"">" _
                     & "<td style=""font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"">&nbsp;</td>" _
                     & "<td align=""left"" style=""font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"">&nbsp;</td>" _
                     & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;"">TOTALE " & par.IfNull(myReader("DESCRIZIONE"), "") & "</td>" _
                     & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;"">" & Format(budgetTotale, "##,##0.00") & "</td>" _
                     & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;"">" & Format(variazioniTotale, "##,##0.00") & "</td>" _
                     & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;"">" & Format(finanziamentiVariatiTotale, "##,##0.00") & "</td></tr>"

                RIGA = dt2.NewRow
                RIGA.Item("CAP") = ""
                RIGA.Item("COD") = ""
                RIGA.Item("VOCE") = "TOTALE " & par.IfNull(myReader("DESCRIZIONE"), "")
                RIGA.Item("FINANZIAMENTI") = Format(budgetTotale, "##,##0.00")
                RIGA.Item("VARIAZIONI") = Format(variazioniTotale, "##,##0.00")
                RIGA.Item("FINANZIAMENTI_VARIATI") = Format(finanziamentiVariatiTotale, "##,##0.00")
                dt2.Rows.Add(RIGA)

                '############# TOTALI COMPLESSIVI ################
                RiepilogoFinanziamentiAttribuiti = RiepilogoFinanziamentiAttribuiti + budgetTotale
                RiepilogoVariazioni = RiepilogoVariazioni + variazioniTotale
                RiepilogoFinanziamentiVariati = RiepilogoFinanziamentiVariati + finanziamentiVariatiTotale

            End While
            myReader.Close()

            '###### ULTIMA RIGA PER IL TOTALE COMPLESSIVO PREVENTIVO ########
            TabellaRiepilogo = TabellaRiepilogo _
                     & "<tr style=""color: Red; background-color: Lavender; font-family: Arial; font-size: 8pt;"">" _
                     & "<td style=""font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"">&nbsp;</td>" _
                     & "<td align=""left"" style=""font-weight: bold; font-style: normal; text-decoration: none; white-space: nowrap;"">&nbsp;</td>" _
                     & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;"">TOTALE</td>" _
                     & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;"">" & Format(RiepilogoFinanziamentiAttribuiti, "##,##0.00") & "</td>" _
                     & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;white-space: nowrap;"">" & Format(RiepilogoVariazioni, "##,##0.00") & "</td>" _
                     & "<td align=""right"" style=""font-weight: bold; font-style: normal; text-decoration: none;"">" & Format(RiepilogoFinanziamentiVariati, "##,##0.00") & "</td></tr>"

            RIGA = dt2.NewRow
            RIGA.Item("CAP") = ""
            RIGA.Item("COD") = ""
            RIGA.Item("VOCE") = "TOTALE"
            RIGA.Item("FINANZIAMENTI") = Format(RiepilogoFinanziamentiAttribuiti, "##,##0.00")
            RIGA.Item("VARIAZIONI") = Format(RiepilogoVariazioni, "##,##0.00")
            RIGA.Item("FINANZIAMENTI_VARIATI") = Format(RiepilogoFinanziamentiVariati, "##,##0.00")
            dt2.Rows.Add(RIGA)
            Session.Add("DT2RV", dt2)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            btnExport.Visible = False
            btnStampaPDF.Visible = False
            TabellaRiepilogo = ""
            TabellaThead = ""
            lblErrore.Visible = True
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        '#### EXPORT IN EXCEL ####

        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            Dim datatable As Data.DataTable
            datatable = CType(HttpContext.Current.Session.Item("DT2RV"), Data.DataTable)
            sNomeFile = "RiepilogoGeneraleVariazioni" & Titolo & "_" & Format(Now, "yyyyMMddHHmm")
            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                '.SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)
                .SetColumnWidth(1, 1, 16)
                .SetColumnWidth(2, 2, 16)
                .SetColumnWidth(3, 3, 120)
                .SetColumnWidth(4, 4, 30)
                .SetColumnWidth(5, 5, 30)
                .SetColumnWidth(6, 6, 30)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "CAP. BIL. COM.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "COD", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "VOCE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "FINANZIAMENTI ATTRIBUITI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "VARIAZIONI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "FINANZIAMENTI VARIATI", 0)
                K = 2
                For Each row In dt2.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, dt2.Rows(i).Item("CAP"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, dt2.Rows(i).Item("COD"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, dt2.Rows(i).Item("VOCE"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, dt2.Rows(i).Item("FINANZIAMENTI"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, dt2.Rows(i).Item("VARIAZIONI"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, dt2.Rows(i).Item("FINANZIAMENTI_VARIATI"))
                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

            Dim strFile As String
            strFile = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls")
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
            Session.Remove("DT2RV")
            Response.Redirect("..\..\..\FileTemp\" & sNomeFile & ".zip")

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnStampaPDF_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampaPDF.Click
        '### STAMPA PDF ###
        Try
            Dim NomeFile As String = "RiepilogoGeneraleVariazioni" & Titolo & "_" & Format(Now, "yyyyMMddHHmmss")
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            Dim TABELLA As String = "<html><head></head><body><table cellspacing=""0"" cellpadding=""4"" rules=""all"" border=""1"" id=""DataGrid1"" style=""color: #333333;" _
                                   & "border-color: #507CD1; border-width: 1px; border-style: Solid; width: 100%; border-collapse: collapse;"">" _
                                   & TabellaThead & TabellaRiepilogo & "</table><script type=""text/javascript"">window.focus();self.focus();</script></body></html>"

            sr.WriteLine(TABELLA)
            sr.Close()
            Dim url As String = NomeFile
            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            Dim pdfConverter As PdfConverter = New PdfConverter
            If Licenza <> "" Then
                pdfConverter.LicenseKey = Licenza
            End If
            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter.PageWidth = 1200
            pdfConverter.PdfDocumentOptions.ShowHeader = True
            pdfConverter.PdfDocumentOptions.ShowFooter = True
            pdfConverter.PdfDocumentOptions.LeftMargin = 20
            pdfConverter.PdfDocumentOptions.RightMargin = 20
            pdfConverter.PdfDocumentOptions.TopMargin = 5
            pdfConverter.PdfDocumentOptions.BottomMargin = 5
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter.PdfFooterOptions.FooterText = ("")
            pdfConverter.PdfHeaderOptions.HeaderText = "Riepilogo Variazioni tra Voci - Raggruppamento per Capitoli (Esercizio Finanziario " & Titolo & ")"
            pdfConverter.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter.PdfFooterOptions.DrawFooterLine = False
            pdfConverter.PdfFooterOptions.PageNumberText = ""
            pdfConverter.PdfFooterOptions.ShowPageNumber = False
            pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".pdf")
            File.Delete(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm")
            Session.Remove("DT2RV")
            'Response.Redirect("..\..\..\FileTemp\" & NomeFile & ".pdf")
            FIN.Value = "1"
            Response.Write("<script>window.open('..\\..\\..\\FileTemp\\" & NomeFile & ".pdf','StampeVariazioni');</script>")
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try


    End Sub

End Class