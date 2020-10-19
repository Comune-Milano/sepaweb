Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class RATEIZZAZIONE_ExportGenerale
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim xls As New ExcelSiSol

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            Cerca()

        End If

    End Sub
    Private Sub Cerca()

        Try
            Dim qIdRat As String = ""
            If Request.QueryString("IDCONT") > "0" Then
                qIdRat = " and bol_rateizzazioni.id_contratto = " & Request.QueryString("IDCONT")
            End If

            par.cmd.CommandText = "SELECT BOL_RATEIZZAZIONI.ID AS id_rateizzazione,decode(fl_annullata,0,'NO',1,'SI') AS RAT_ANNULLATA, cod_contratto,TO_CHAR(TO_DATE (BOL_RATEIZZAZIONI.data_emissione, 'yyyymmdd'),'dd/mm/yyyy') AS INIZIO,DECODE(tipo_rateizzazione,0,'NUMERO RATE',1,'IMPORTO RATA') AS TIPO_RAT, " _
                                & "(SELECT (cognome ||' '||nome) " _
                                & "FROM siscom_mi.ANAGRAFICA,siscom_mi.SOGGETTI_CONTRATTUALI " _
                                & "WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.id_anagrafica AND SOGGETTI_CONTRATTUALI.id_contratto = RAPPORTI_UTENZA.ID AND COD_TIPOLOGIA_OCCUPANTE = 'INTE') AS INTESTATARIO, " _
                                & "DECODE(bol_rateizzazioni.imp_anticipo,0,'NO',imp_anticipo) AS anticipo," _
                                & "(SELECT COUNT(ID) FROM siscom_mi.bol_rateizzazioni_dett WHERE id_rateizzazione =bol_rateizzazioni.ID AND fl_annullata = 0 AND num_rata >0 ) AS NUM_RATE," _
                                & "(SELECT COUNT(ID) FROM siscom_mi.bol_rateizzazioni_dett WHERE id_rateizzazione =bol_rateizzazioni.ID AND fl_annullata = 0 AND  id_bolletta IS NOT NULL) AS NUM_MAV_EMESSI," _
                                & "NVL((SELECT COUNT(bol_bollette.ID) FROM siscom_mi.bol_bollette,siscom_mi.bol_rateizzazioni_dett WHERE bol_rateizzazioni_dett.id_bolletta " _
                                & "IS NOT NULL AND bol_rateizzazioni_dett.fl_annullata = 0 AND NVL(importo_pagato,0)<>0 AND bol_bollette.ID = id_bolletta " _
                                & "AND bol_rateizzazioni_dett.ID_rateizzazione = bol_rateizzazioni.ID " _
                                & "GROUP BY bol_rateizzazioni_dett.id_rateizzazione),0)AS mav_pagati,(IMP_ANTICIPO +IMP_RESIDUO) AS TOT_RAT, " _
                                & "'' AS rata,''AS mese,0.0 AS importo_rata,0.0 AS quota_capitali,0.0 AS quota_interessi,'' AS num_bolletta,0.0 AS importo_bolletta,'' as data_emissione,'' as data_scadenza,'' as PAGATA " _
                                & "FROM siscom_mi.BOL_RATEIZZAZIONI,siscom_mi.RAPPORTI_UTENZA " _
                                & "WHERE RAPPORTI_UTENZA.ID = BOL_RATEIZZAZIONI.id_contratto " & qIdRat & " ORDER BY BOL_RATEIZZAZIONI.data_emissione ASC"


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            Dim dtDett As New Data.DataTable
            da.Fill(dt)
            Dim dtCompleta As New Data.DataTable
            dtCompleta = dt.Clone
            Dim rigaAdd As Data.DataRow

            For Each row As Data.DataRow In dt.Rows
                rigaAdd = dtCompleta.NewRow
                rigaAdd.ItemArray = row.ItemArray

                dtCompleta.Rows.Add(rigaAdd)

                par.cmd.CommandText = "SELECT DECODE(num_rata,0,'anticipo',num_rata) AS RATA, " _
                                    & "TO_CHAR(TO_DATE(BOL_RATEIZZAZIONI_DETT.data_emissione,'yyyymmdd'),'month yyyy') AS mese, " _
                                    & "importo_rata,quota_capitali,quota_interessi,BOL_BOLLETTE.num_bolletta,BOL_BOLLETTE.importo_totale AS importo_bolletta, " _
                                    & "TO_CHAR(TO_DATE (BOL_BOLLETTE.data_emissione, 'yyyymmdd'),'dd/mm/yyyy') AS data_emissione, " _
                                    & "TO_CHAR(TO_DATE (BOL_BOLLETTE.data_scadenza, 'yyyymmdd'),'dd/mm/yyyy') AS data_scadenza, " _
                                    & "(CASE WHEN NVL(importo_pagato,0)<>0 THEN 'SI' ELSE 'NO'END) AS pagata " _
                                    & "FROM siscom_mi.BOL_RATEIZZAZIONI_DETT,siscom_mi.BOL_BOLLETTE " _
                                    & "WHERE BOL_BOLLETTE.ID(+) = BOL_RATEIZZAZIONI_DETT.id_bolletta " _
                                    & "AND BOL_RATEIZZAZIONI_DETT.ID_RATEIZZAZIONE = " & row.Item("id_rateizzazione") _
                                    & " ORDER BY num_rata ASC"
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                dtDett = New Data.DataTable
                da.Fill(dtCompleta)
                'dtCompleta.Merge(dtDett)
            Next
            dtCompleta.Columns.RemoveAt(0)
            'Export(dtCompleta)
            Dim nomefile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportPianiRientro", "Foglio", dtCompleta)
            If File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
                Response.Redirect("../FileTemp/" & nomefile, False)
            End If


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try



    End Sub

    Private Sub Export(ByVal dt As Data.DataTable)
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            sNomeFile = "ExportRat" & Format(Now, "yyyyMMddHHmmss")

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


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, " CODICE CONTRATTO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INIZIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "TIPO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "INTESTATARIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "ANTICIPO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "N. RATE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "N. MAV EMESSI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "N. MAV PAGATI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "IMPORTO RATEIZZATO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "RATA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "MESE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "IMPORTO RATA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "QUOTA CAPITALI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "QUOTA INTERESSI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "NUM. BOLLETTA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "IMPORTO BOLLETTA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "DATA EMISSIONE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "DATA SCADENZA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "PAGATA", 0)

                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(row.Item("COD_CONTRATTO"), " "), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(row.Item("INIZIO"), " "), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(row.Item("TIPO_RAT"), " "), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(row.Item("INTESTATARIO"), " "), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.IfNull(row.Item("ANTICIPO"), " "), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfNull(row.Item("NUM_RATE"), " "), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(row.Item("NUM_MAV_EMESSI"), " "), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(row.Item("MAV_PAGATI"), " "), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(row.Item("TOT_RAT"), " "), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.IfNull(row.Item("RATA"), " "), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.IfNull(row.Item("MESE"), " "), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.IfNull(row.Item("IMPORTO_RATA"), 0), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.IfNull(row.Item("QUOTA_CAPITALI"), 0), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.IfNull(row.Item("QUOTA_INTERESSI"), 0), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.IfNull(row.Item("NUM_BOLLETTA"), " "), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.IfNull(row.Item("IMPORTO_BOLLETTA"), 0), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.IfNull(row.Item("DATA_EMISSIONE"), " "), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.IfNull(row.Item("DATA_SCADENZA"), " "), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.IfNull(row.Item("PAGATA"), " "), 0)

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
            Response.Redirect("..\FileTemp\" & sNomeFile & ".zip", False)
            'Response.Write("<script>window.open('../FileTemp/" & sNomeFile & ".zip','','');</script>")
            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub
End Class
