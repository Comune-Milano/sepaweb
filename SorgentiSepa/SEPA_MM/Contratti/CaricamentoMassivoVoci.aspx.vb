Imports System.IO
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports OfficeOpenXml
Imports Telerik.Web.UI.Upload
Imports Telerik.Web.UI

Partial Class Contratti_CaricamentoMassivoVoci
    Inherits System.Web.UI.Page
    Public percentuale As INT64 = 0
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then
            'RadProgressArea1.ProgressIndicators = RadProgressArea1.ProgressIndicators And Not ProgressIndicators.SelectedFilesCount

        End If
        RadProgressArea1.Localization.Uploaded = "Avanzamento Totale"
        RadProgressArea1.Localization.UploadedFiles = "Avanzamento"
        RadProgressArea1.Localization.CurrentFileName = "Elaborazione in corso: "
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As System.EventArgs) Handles btnDownload.Click
        Try
            Dim Xls As Byte()
            Dim NomeFileXls As String = ""

            connData.apri()
            par.cmd.CommandText = " select * FROM siscom_mi.SCHEMA_IMPORT_VOCI_SCHEMA "
            Dim MyReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader2.Read Then
                Xls = par.IfNull(MyReader2("schema_excel"), "")
            End If
            MyReader2.Close()

            connData.chiudi()

            Dim bw As BinaryWriter

            NomeFileXls = "Scheda_import_voci" & Format(Now, "yyyyMMddHHmmss")
            Dim fileName As String = Server.MapPath("~\FileTemp\") & NomeFileXls & ".xlsx"
            Dim fs As New FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite)
            bw = New BinaryWriter(fs)
            bw.Write(Xls)
            bw.Flush()
            bw.Close()

            par.cmd.CommandText = "select id,descrizione from siscom_mi.t_voci_bolletta where selezionabile=1 order by 2 asc"
            Dim dtV As New Data.DataTable
            Dim daV As Oracle.DataAccess.Client.OracleDataAdapter
            daV = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            daV.Fill(dtV)

            Dim newFile As New FileInfo(fileName)
            Dim pck As New ExcelPackage(newFile)
            Dim ws = pck.Workbook.Worksheets(2)
            ws.Cells.Clear()

            ws.Cells("A1").Value = "ID"
            ws.Cells("B1").Value = "DESCRIZIONE"

            Dim fileName2 As String = fileName
            Dim cont As Integer = 1
            For Each rowDati As Data.DataRow In dtV.Rows
                cont = cont + 1
                ws.Cells("A" & cont & "").Value = rowDati.Item("ID")
                ws.Cells("B" & cont & "").Value = rowDati.Item("DESCRIZIONE")
            Next
            Dim newFile2 As New FileInfo(fileName2)
            pck.SaveAs(newFile2)

            Dim zipfic As String
            Dim NomeFilezip As String = "Scheda_import_voci_" & Format(Now, "yyyyMMddHHmmss") & ".zip"

            zipfic = Server.MapPath("..\FileTemp\" & NomeFilezip)

            Dim kkK As Int64 = 0
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

            Dim strFile As String = ""
            strFile = "~\FileTemp\" & NomeFileXls & ".xlsx"

            Dim ff As String = ""
            ff = ZipAllegatoDownload(strFile, NomeFileXls & ".xlsx")

            If File.Exists(Server.MapPath("~\FileTemp\") & ff) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & ff & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Exit Sub
            Else
                par.modalDialogMessage("Attenzione", "Si è verificato un errore durante il download. Riprovare!", Me.Page)
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Caricamento Massivo Voci - btnDownload_Click - " & ex.Message)
            lblErrore.Text = "Provenienza: btnDownload_Click - " & ex.Message
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
        End Try
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Private Function ZipAllegatoDownload(ByVal strFile As String, ByVal nomeFile As String) As String
        ZipAllegatoDownload = ""
        Dim zipFic As String = ""
        Dim estensioneAllegato As String = Mid(Server.MapPath(strFile), Server.MapPath(strFile).IndexOf(".") + 1)
        Dim AllegatoCompleto As String = nomeFile.Replace(estensioneAllegato, ".zip")
        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        Dim strmFile As FileStream = File.OpenRead(Server.MapPath("../FileTemp/" & nomeFile))
        Dim abyBuffer(strmFile.Length - 1) As Byte
        strmFile.Read(abyBuffer, 0, abyBuffer.Length)
        Dim sFile As String = Path.GetFileName(Server.MapPath("../FileTemp/" & nomeFile))
        Dim theEntry As ZipEntry = New ZipEntry(sFile)
        Dim fi As New FileInfo(Server.MapPath("../FileTemp/" & nomeFile))
        theEntry.DateTime = fi.LastWriteTime
        theEntry.Size = strmFile.Length
        strmFile.Close()
        objCrc32.Reset()
        objCrc32.Update(abyBuffer)
        theEntry.Crc = objCrc32.Value
        If File.Exists(Server.MapPath("../FileTemp/") & AllegatoCompleto) Then
            File.Delete(Server.MapPath("../FileTemp/") & AllegatoCompleto)
        End If
        zipFic = Server.MapPath("../FileTemp/") & AllegatoCompleto
        strmZipOutputStream = New ZipOutputStream(File.Create(zipFic))
        strmZipOutputStream.SetLevel(6)
        strmZipOutputStream.PutNextEntry(theEntry)
        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        strmZipOutputStream.Finish()
        strmZipOutputStream.Close()
        If File.Exists(Server.MapPath(strFile)) Then
            File.Delete(Server.MapPath(strFile))
        End If
        ZipAllegatoDownload = AllegatoCompleto
    End Function

    Private Function UploadOnServer() As String
        UploadOnServer = ""
        Try
            If FileUpload1.HasFile = True Then
                UploadOnServer = Server.MapPath("..\FileTemp\") & FileUpload1.FileName
                FileUpload1.SaveAs(UploadOnServer)
            Else
                par.modalDialogMessage("Attenzione", "Nessun file allegato!", Me.Page)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Caricamento Massivo Voci - UploadOnServer - " & ex.Message)
            lblErrore.Text = "Provenienza: UploadOnServer - " & ex.Message
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
        End Try

        Return UploadOnServer
    End Function

    Protected Sub btnAllega_Click(sender As Object, e As System.EventArgs) Handles btnAllega.Click
        Try
            Dim FileName As String = UploadOnServer()
            Dim objFile As Object
            objFile = Server.CreateObject("Scripting.FileSystemObject")
            ik = 0

            If Not String.IsNullOrEmpty(FileName) Then
                If objFile.FileExists(FileName) And FileName.Contains(".xlsx") Then
                    Allegafile(FileName)
                Else
                    par.modalDialogMessage("Attenzione", "Tipo file non valido. Selezionare un file .xlsx", Me.Page)
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Caricamento Massivo Voci - btnAllega_Click - " & ex.Message)
            lblErrore.Text = "Provenienza: btnAllega_Click - " & ex.Message
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
        End Try
    End Sub

    Private Function ControlliDati(ByVal dtFoglio1 As Data.DataTable, ByRef msgAnomalia As String) As Boolean
        ControlliDati = True
        msgAnomalia = ""
        For Each rowFoglio1 As Data.DataRow In dtFoglio1.Rows
            If Not String.IsNullOrEmpty(rowFoglio1.Item("COD_CONTRATTO").ToString) And Not String.IsNullOrEmpty(rowFoglio1.Item("VOCE").ToString) And Not String.IsNullOrEmpty(rowFoglio1.Item("DA_RATA").ToString) And Not String.IsNullOrEmpty(rowFoglio1.Item("PER_RATE").ToString) And Not String.IsNullOrEmpty(rowFoglio1.Item("ANNO").ToString) And Not String.IsNullOrEmpty(rowFoglio1.Item("IMPORTO").ToString) Then
                If String.IsNullOrEmpty(rowFoglio1.Item("COD_CONTRATTO").ToString) Then
                    msgAnomalia &= "Valore nullo per la colonna Cod. Contratto!\n"
                    ControlliDati = False
                    Exit For
                End If
                If String.IsNullOrEmpty(rowFoglio1.Item("VOCE").ToString) Then
                    msgAnomalia &= "Valore nullo per la colonna Voce!\n"
                    ControlliDati = False
                    Exit For
                End If
                If String.IsNullOrEmpty(rowFoglio1.Item("DA_RATA").ToString) Then
                    msgAnomalia &= "Valore nullo per la colonna Da Rata!\n"
                    ControlliDati = False
                    Exit For
                End If
                If String.IsNullOrEmpty(rowFoglio1.Item("PER_RATE").ToString) Then
                    msgAnomalia &= "Valore nullo per la colonna Per Rate!\n"
                    ControlliDati = False
                    Exit For
                End If
                If String.IsNullOrEmpty(rowFoglio1.Item("ANNO").ToString) Then
                    msgAnomalia &= "Valore nullo per la colonna Anno!\n"
                    ControlliDati = False
                    Exit For
                End If
                If String.IsNullOrEmpty(rowFoglio1.Item("IMPORTO").ToString) Then
                    msgAnomalia &= "Valore nullo per la colonna Importo!\n"
                    ControlliDati = False
                End If
                If par.IfEmpty(rowFoglio1.Item("DA_RATA"), 0) > 12 Then
                    msgAnomalia &= "Valore non ammesso per la colonna Da Rata!\n"
                    ControlliDati = False
                    Exit For
                End If
                If par.IfEmpty(rowFoglio1.Item("PER_RATE"), 0) > 12 Then
                    msgAnomalia &= "Valore non ammesso per la colonna Per Rate!\n"
                    ControlliDati = False
                    Exit For
                End If
                If (par.IfEmpty(CInt(rowFoglio1.Item("DA_RATA")), 0) + par.IfEmpty(CInt(rowFoglio1.Item("PER_RATE")), 0)) > 14 Then
                    msgAnomalia &= "Valore non ammesso per le colonne Da Rata e Per Rate!\n"
                    ControlliDati = False
                    Exit For
                End If
            End If
        Next

        Return ControlliDati
    End Function

    Private Sub Allegafile(ByVal nomeFile As String)

        Dim xls As New ExcelSiSol
        Dim dtFoglio1 As New Data.DataTable
        Dim dtFoglio2 As New Data.DataTable
        Dim dtBolSchema As New Data.DataTable
        Dim msgControlli As String = ""
        txtRisultati.Text = ""
        Using pck As New OfficeOpenXml.ExcelPackage()
            Using stream = File.Open(nomeFile, FileMode.Open, FileAccess.Read)
                pck.Load(stream)
            End Using
            Dim ws As OfficeOpenXml.ExcelWorksheet = pck.Workbook.Worksheets(1)
            dtFoglio1 = xls.WorksheetToDataTable(ws, True)
            ws = pck.Workbook.Worksheets(2)
            dtFoglio2 = xls.WorksheetToDataTable(ws, True)
        End Using
        'For Each rowFoglio1 As Data.DataRow In dtFoglio1.Rows
        '    Dim row2 As Data.DataRow
        '    If par.IfNull(rowFoglio1.Item("VOCE"), "") <> "" Then
        '        row2 = dtFoglio2.Select("DESCRIZIONE = '" & rowFoglio1.Item("VOCE").ToString & "'")(0)
        '    End If

        'Next
        If ControlliDati(dtFoglio1, msgControlli) = True Then

            connData.apri()
            Dim ElencoEserciziFinanziari As New Generic.Dictionary(Of Int64, Int64)
            par.cmd.CommandText = "SELECT ID,TO_NUMBER(TRIM(SUBSTR(INIZIO,1,4))) AS ANNO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO ORDER BY ID"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                ElencoEserciziFinanziari.Add(par.IfNull(lettore("ANNO"), -1), par.IfNull(lettore("ID"), -1))
            End While
            lettore.Close()
            par.cmd.CommandText = "SELECT 0 as ID, 0 as ID_CONTRATTO, 0 as ID_UNITA, 0 as ID_ESERCIZIO_F, 0 as ID_VOCE,'' as NOMEVOCE, 0.00 as IMPORTO, 0 as DA_RATA, 0 as PER_RATE, 0.00 as IMPORTO_SINGOLA_RATA, 0 as ANNO,'' AS COD_CONTRATTO FROM dual"
            Dim daB1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt1 As New Data.DataTable
            daB1.Fill(dt1)
            daB1.Dispose()

            dtBolSchema = dt1.Clone
            Dim row As Data.DataRow

            Dim Total As Integer = dtFoglio1.Rows.Count
            Dim progress As RadProgressContext = RadProgressContext.Current
            progress.Speed = "N/A"

            For Each riga1 As Data.DataRow In dtFoglio1.Rows
                If Not String.IsNullOrEmpty(riga1.Item("COD_CONTRATTO").ToString) And Not String.IsNullOrEmpty(riga1.Item("VOCE").ToString) And Not String.IsNullOrEmpty(riga1.Item("DA_RATA").ToString) And Not String.IsNullOrEmpty(riga1.Item("PER_RATE").ToString) And Not String.IsNullOrEmpty(riga1.Item("ANNO").ToString) And Not String.IsNullOrEmpty(riga1.Item("IMPORTO").ToString) Then
                    row = dtBolSchema.NewRow()

                    par.cmd.CommandText = "select rapporti_utenza.id,unita_contrattuale.id_unita,COD_CONTRATTO from siscom_mi.rapporti_utenza,siscom_mi.unita_contrattuale where rapporti_utenza.id=unita_contrattuale.id_contratto " _
                        & " and id_unita_principale is null and rapporti_utenza.cod_contratto='" & par.IfEmpty(riga1.Item("COD_CONTRATTO"), "") & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = True Then

                        If myReader.Read Then
                            row.Item("COD_CONTRATTO") = par.IfNull(myReader("COD_CONTRATTO"), "")
                            row.Item("ID_CONTRATTO") = par.IfNull(myReader("ID"), 0)
                            row.Item("ID_UNITA") = par.IfNull(myReader("ID_UNITA"), 0)
                        End If
                        myReader.Close()

                        Dim rowVoce As Data.DataRow
                        If par.IfNull(riga1.Item("VOCE"), "") <> "" Then
                            rowVoce = dtFoglio2.Select("DESCRIZIONE = '" & riga1.Item("VOCE").ToString & "'")(0)
                            row.Item("ID_VOCE") = rowVoce.Item(0)
                            'par.cmd.CommandText = "SELECT * FROM siscom_mi.T_VOCI_BOLLETTA WHERE DESCRIZIONE='" & par.PulisciStrSql(riga1.Item("VOCE").ToString) & "'"
                            'Dim myReaderV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            'If myReaderV.Read Then
                            '    row.Item("ID_VOCE") = myReaderV("ID")
                            'End If
                            'myReaderV.Close()
                        End If

                        row.Item("ID") = 0
                        If par.IfEmpty(riga1.Item("ANNO"), -1) > Year(Now) + 10 Then
                            txtRisultati.Visible = True
                            txtRisultati.Text &= "Voce NON inserita per il contratto " & riga1.Item("COD_CONTRATTO") & " con anno " & riga1.Item("ANNO") & " " & vbCrLf
                        Else
                        row.Item("ID_ESERCIZIO_F") = ElencoEserciziFinanziari(par.IfEmpty(riga1.Item("ANNO"), -1))

                        row.Item("IMPORTO") = Format(riga1.Item("IMPORTO") * riga1.Item("PER_RATE"), "##,##0.00")
                        row.Item("DA_RATA") = par.IfEmpty(riga1.Item("DA_RATA"), 0)
                        row.Item("PER_RATE") = par.IfEmpty(riga1.Item("PER_RATE"), 0)
                        row.Item("IMPORTO_SINGOLA_RATA") = par.IfEmpty(riga1.Item("IMPORTO"), 0)
                        row.Item("ANNO") = par.IfEmpty(riga1.Item("ANNO"), Year(Now))
                        row.Item("NOMEVOCE") = par.IfEmpty(riga1.Item("VOCE"), "")

                        dtBolSchema.Rows.Add(row)
                        End If
                    Else
                        txtRisultati.Visible = True
                        txtRisultati.Text &= "Cod. contratto non trovato: " & riga1.Item("COD_CONTRATTO") & vbCrLf
                    End If
                End If


                ik = ik + 1
                progress.PrimaryTotal = Total * 2
                progress.PrimaryValue = iK
                progress.PrimaryPercent = Int((iK * 100) / (Total * 2))

                progress.SecondaryTotal = Total
                progress.SecondaryValue = ik
                progress.SecondaryPercent = Int((ik * 100) / Total)

                progress.CurrentOperationText = " " & ik.ToString() & " di " & Total


            Next
            connData.chiudi()
            InserisciVoceInSchema(dtBolSchema)
            par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
        Else
            If Not String.IsNullOrEmpty(msgControlli) Then
                par.modalDialogMessage("Attenzione", msgControlli, Me.Page)
                Exit Sub
            End If
        End If

    End Sub

    Private Sub InserisciVoceInSchema(ByVal dtDettaglio As Data.DataTable)
        Try
            connData.apri(True)

            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\") & " IstruzioniSqlVoci_" & Format(Now, "yyyyMMddhhmmss") & ".txt", False, System.Text.Encoding.Default)

            Dim idBolStorico As Long = 0
            Dim numRigheElab As Int64 = 0
            Dim contatore As Int64 = 0
            Dim percent_avanz As Long = 0
            Dim strUpdate As String = ""
            Dim strInsertEventiUpdate As String = ""
            Dim strInsert As String = ""
            Dim strInsertEventi2 As String = ""

            Dim Total As Integer = dtDettaglio.Rows.Count
            Dim progress As RadProgressContext = RadProgressContext.Current
            progress.Speed = "N/A"
            Dim ik1 As Long = 0

            For Each riga1 As Data.DataRow In dtDettaglio.Rows
                numRigheElab = dtDettaglio.Rows.Count
                par.cmd.CommandText = "select * from siscom_mi.bol_schema where id_contratto=" & riga1.Item("id_contratto") & " and id_voce=" & riga1.Item("id_voce") & " and anno=" & riga1.Item("anno") & " and da_rata=" & riga1.Item("da_rata") & " and per_rate=" & riga1.Item("per_rate")
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then

                    par.cmd.CommandText = "select siscom_mi.seq_bol_schema_storico.nextval from dual"
                    idBolStorico = par.cmd.ExecuteScalar

                    par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA_STORICO (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                           & "IMPORTO, DA_RATA_OLD, PER_RATE_OLD, IMPORTO_SINGOLA_RATA, IMPORTO_NUOVO, ANNO,ID_OPERATORE,DATA_ORA, DA_RATA_NEW, PER_RATE_NEW) " _
                           & "Values " _
                           & "(" & idBolStorico & "," & par.insDbValue(lettore("id_contratto"), False) _
                           & "," & par.insDbValue(lettore("id_unita"), False) & "," & par.insDbValue(lettore("id_esercizio_f"), False) _
                           & "," & par.insDbValue(lettore("id_voce"), False) & " ," _
                           & par.insDbValue(lettore("importo"), False) _
                           & "," & par.insDbValue(lettore("da_rata"), False) & "," & par.insDbValue(lettore("per_rate"), False) & "," & par.insDbValue(lettore("importo_singola_rata"), False) _
                           & "," & par.insDbValue(riga1.Item("importo_singola_rata"), False) & "," & par.insDbValue(lettore("anno"), False) & "," & Session.Item("id_operatore") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & par.insDbValue(riga1.Item("da_rata"), False) & "," & par.insDbValue(riga1.Item("per_rate"), False) & ")"
                    par.cmd.ExecuteNonQuery()

                    strUpdate = "UPDATE SISCOM_MI.BOL_SCHEMA" _
                            & " SET " _
                            & " ID_ESERCIZIO_F = " & par.insDbValue(riga1.Item("id_esercizio_f"), False) & "," _
                            & " IMPORTO = " & par.insDbValue(riga1.Item("importo"), False) & "," _
                            & " DA_RATA = " & par.insDbValue(riga1.Item("da_rata"), False) & "," _
                            & " PER_RATE = " & par.insDbValue(riga1.Item("per_rate"), False) & "," _
                            & " IMPORTO_SINGOLA_RATA = " & par.insDbValue(riga1.Item("importo_singola_rata"), False) & ", " _
                            & " FL_DA_RIPETERE = 1" _
                            & " WHERE ID = " & par.IfNull(lettore("ID"), 0)
                    par.cmd.CommandText = strUpdate
                    strUpdate = strUpdate & ";"
                    par.cmd.ExecuteNonQuery()

                    strInsertEventiUpdate = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & par.insDbValue(riga1.Item("id_contratto"), False) & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F184','Inserimento massivo voce schema " & par.PulisciStrSql(riga1.Item("nomevoce")) & " - importo " & par.insDbValue(riga1.Item("importo"), False) & " euro - da rata " & par.insDbValue(riga1.Item("da_rata"), False) & " a rata " & par.insDbValue(riga1.Item("per_rate"), False) & "')"
                    par.cmd.CommandText = strInsertEventiUpdate
                    strInsertEventiUpdate = strInsertEventiUpdate & ";"
                    par.cmd.ExecuteNonQuery()

                Else
                    par.cmd.CommandText = "select siscom_mi.seq_bol_schema_storico.nextval from dual"
                    idBolStorico = par.cmd.ExecuteScalar

                    par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA_STORICO (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                           & "IMPORTO, DA_RATA_OLD, PER_RATE_OLD, IMPORTO_SINGOLA_RATA, IMPORTO_NUOVO, ANNO,ID_OPERATORE,DATA_ORA, DA_RATA_NEW, PER_RATE_NEW) " _
                           & "Values " _
                           & "(" & idBolStorico & "," & par.insDbValue(riga1.Item("id_contratto"), False) _
                           & "," & par.insDbValue(riga1.Item("id_unita"), False) & "," & par.insDbValue(riga1.Item("id_esercizio_f"), False) _
                           & "," & par.insDbValue(riga1.Item("id_voce"), False) & " ,0" _
                           & ",null,null,0" _
                           & "," & par.insDbValue(riga1.Item("importo_singola_rata"), False) & "," & par.insDbValue(riga1.Item("anno"), False) & "," & Session.Item("id_operatore") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & par.insDbValue(riga1.Item("da_rata"), False) & "," & par.insDbValue(riga1.Item("per_rate"), False) & ")"
                    par.cmd.ExecuteNonQuery()

                    strInsert = "Insert into SISCOM_MI.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                           & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO, FL_DA_RIPETERE) " _
                           & "Values " _
                           & "(siscom_mi.seq_bol_schema.nextval," & par.insDbValue(riga1.Item("id_contratto"), False) _
                           & "," & par.insDbValue(riga1.Item("id_unita"), False) & "," & par.insDbValue(riga1.Item("id_esercizio_f"), False) _
                           & "," & par.insDbValue(riga1.Item("id_voce"), False) & " ," _
                           & par.insDbValue(riga1.Item("importo"), False) _
                           & "," & par.insDbValue(riga1.Item("da_rata"), False) & "," & par.insDbValue(riga1.Item("per_rate"), False) & "," & par.insDbValue(riga1.Item("importo_singola_rata"), False) _
                           & "," & par.insDbValue(riga1.Item("anno"), False) & ",1)"
                    par.cmd.CommandText = strInsert
                    par.cmd.ExecuteNonQuery()
                    strInsert = strInsert & ";"

                    strInsertEventi2 = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & par.insDbValue(riga1.Item("id_contratto"), False) & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F05','Inserimento massivo voce schema " & par.PulisciStrSql(riga1.Item("nomevoce")) & " - importo " & par.insDbValue(riga1.Item("importo"), False) & " euro - da rata " & par.insDbValue(riga1.Item("da_rata"), False) & " a rata " & par.insDbValue(riga1.Item("per_rate"), False) & "')"
                    par.cmd.CommandText = strInsertEventi2
                    strInsertEventi2 = strInsertEventi2 & ";"
                    par.cmd.ExecuteNonQuery()

                End If
                lettore.Close()

                sr.WriteLine(strInsertEventiUpdate & vbCrLf & strInsert & vbCrLf & strInsertEventi2 & vbCrLf & strUpdate)

                ik = ik + 1
                progress.PrimaryTotal = Total * 2
                progress.PrimaryValue = iK
                progress.PrimaryPercent = Int((iK * 100) / (Total * 2))

                progress.SecondaryTotal = Total
                progress.SecondaryValue = ik1
                progress.SecondaryPercent = Int((ik1 * 100) / Total)

                progress.CurrentOperationText = " " & ik1.ToString() & " di " & Total
                ik1 = ik1 + 1

            Next
            connData.chiudi(True)

            sr.Close()

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: CaricamentoMassivoVoci - InserisciVoceInSchema - " & ex.Message)
            lblErrore.Text = "Provenienza: InserisciVoceInSchema - " & ex.Message
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
        End Try
    End Sub

    Public Property ik() As Long
        Get
            If Not (ViewState("par_ik") Is Nothing) Then
                Return CLng(ViewState("par_ik"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_ik") = value
        End Set
    End Property

End Class
