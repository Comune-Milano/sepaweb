Imports System.IO
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports OfficeOpenXml
Imports Telerik.Web.UI.Upload
Imports Telerik.Web.UI

Partial Class Contratti_CaricamMassivoIngiunzioni
    Inherits System.Web.UI.Page
    Public percentuale As Int64 = 0
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
            par.cmd.CommandText = " select * FROM siscom_mi.SCHEMA_IMPORT_BOLL_INGIUNTE"
            Dim MyReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader2.Read Then
                Xls = par.IfNull(MyReader2("schema_excel"), "")
            End If
            MyReader2.Close()

            connData.chiudi()

            Dim bw As BinaryWriter

            NomeFileXls = "Scheda_import_boll_ingiunte" & Format(Now, "yyyyMMddHHmmss")
            Dim fileName As String = Server.MapPath("~\FileTemp\") & NomeFileXls & ".xlsx"
            Dim fs As New FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite)
            bw = New BinaryWriter(fs)
            bw.Write(Xls)
            bw.Flush()
            bw.Close()

            par.cmd.CommandText = "select id,descrizione from siscom_mi.TIPO_BOLL_INGIUNZIONE order by 2 asc"
            Dim dtV As New Data.DataTable
            Dim daV As Oracle.DataAccess.Client.OracleDataAdapter
            daV = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            daV.Fill(dtV)

            Dim newFile As New FileInfo(fileName)
            Dim pck As New ExcelPackage(newFile)
            Dim ws = pck.Workbook.Worksheets(2)
            ws.Cells.Clear()

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
            Dim NomeFilezip As String = "Scheda_import_boll_ingiunte_" & Format(Now, "yyyyMMddHHmmss") & ".zip"

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
            Session.Add("ERRORE", "Provenienza: Caricamento Massivo Ingiunzioni - btnDownload_Click - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Caricamento Massivo Ingiunzioni - UploadOnServer - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Caricamento Massivo Ingiunzioni - btnAllega_Click - " & ex.Message)
            lblErrore.Text = "Provenienza: btnAllega_Click - " & ex.Message
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
        End Try
    End Sub

    Private Function ControlliDati(ByVal dtFoglio1 As Data.DataTable, ByRef msgAnomalia As String) As Boolean
        ControlliDati = True
        msgAnomalia = ""
        For Each rowFoglio1 As Data.DataRow In dtFoglio1.Rows
            If Not String.IsNullOrEmpty(rowFoglio1.Item("COD_CONTRATTO").ToString) And Not String.IsNullOrEmpty(rowFoglio1.Item("NUM_BOLLETTA").ToString) And Not String.IsNullOrEmpty(rowFoglio1.Item("TIPOLOGIA_INGIUNZIONE").ToString) Then
                If String.IsNullOrEmpty(rowFoglio1.Item("COD_CONTRATTO").ToString) Then
                    msgAnomalia &= "Valore nullo per la colonna Cod. Contratto!\n"
                    ControlliDati = False
                    Exit For
                End If
                If String.IsNullOrEmpty(rowFoglio1.Item("NUM_BOLLETTA").ToString) Then
                    msgAnomalia &= "Valore nullo per la Num. Bolletta!\n"
                    ControlliDati = False
                    Exit For
                End If
                If String.IsNullOrEmpty(rowFoglio1.Item("TIPOLOGIA_INGIUNZIONE").ToString) Then
                    msgAnomalia &= "Valore nullo per la Tipologia Ingiunzione!\n"
                    ControlliDati = False
                    Exit For
                End If
            Else
                msgAnomalia &= "Valori nulli per le colonne: \nCod. Contratto\n- Num. Bolletta\n- Tipologia Ingiunzione!\n"
                ControlliDati = False
                Exit For
            End If
        Next

        Return ControlliDati
    End Function

    Private Sub Allegafile(ByVal nomeFile As String)

        Dim xls As New ExcelSiSol
        Dim dtFoglio1 As New Data.DataTable
        Dim dtFoglio2 As New Data.DataTable
        Dim dtBolIngiunte As New Data.DataTable
        Dim msgControlli As String = ""
        txtRisultati.Text = ""
        Using pck As New OfficeOpenXml.ExcelPackage()
            Using stream = File.Open(nomeFile, FileMode.Open, FileAccess.Read)
                pck.Load(stream)
            End Using
            Dim ws As OfficeOpenXml.ExcelWorksheet = pck.Workbook.Worksheets(1)
            dtFoglio1 = xls.WorksheetToDataTable(ws, True)

        End Using

        If ControlliDati(dtFoglio1, msgControlli) = True Then

            connData.apri()

            par.cmd.CommandText = "SELECT 0 as ID_CONTRATTO, 0 as IDBOLL,0 AS ID_TIPO_INGIUNZIONE, '' AS COD_CONTRATTO, '' AS NUMBOLL, '' as TIPOINGIUNZIONE FROM dual"
            Dim daB1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt1 As New Data.DataTable
            daB1.Fill(dt1)
            daB1.Dispose()

            dtBolIngiunte = dt1.Clone
            Dim row As Data.DataRow

            Dim Total As Integer = dtFoglio1.Rows.Count
            Dim progress As RadProgressContext = RadProgressContext.Current
            progress.Speed = "N/A"
            Dim boll As Boolean = True
            For Each riga1 As Data.DataRow In dtFoglio1.Rows
                If Not String.IsNullOrEmpty(riga1.Item("COD_CONTRATTO").ToString) And Not String.IsNullOrEmpty(riga1.Item("NUM_BOLLETTA").ToString) And Not String.IsNullOrEmpty(riga1.Item("TIPOLOGIA_INGIUNZIONE").ToString) Then

                    par.cmd.CommandText = "select * from siscom_mi.bol_bollette,siscom_mi.rapporti_utenza where num_bolletta='" & par.IfEmpty(riga1.Item("NUM_BOLLETTA"), "") & "' and bol_Bollette.id_contratto=rapporti_utenza.id and cod_contratto='" & par.IfEmpty(riga1.Item("COD_CONTRATTO"), "") & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = False Then
                        txtRisultati.Visible = True
                        txtRisultati.Text &= "NUM. BOLLETTA non trovato: " & riga1.Item("NUM_BOLLETTA") & " per RU: " & par.IfEmpty(riga1.Item("COD_CONTRATTO"), "") & "" & vbCrLf
                    End If
                    myReader.Close()
                    par.cmd.CommandText = "select * from siscom_mi.rapporti_utenza where cod_contratto='" & par.IfEmpty(riga1.Item("COD_CONTRATTO"), "") & "'"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = False Then
                        txtRisultati.Visible = True
                        txtRisultati.Text &= "CODICE CONTRATTO non trovato: " & riga1.Item("COD_CONTRATTO") & vbCrLf
                    End If
                    myReader.Close()

                    If txtRisultati.Visible = False Then

                        'par.cmd.CommandText = "select bol_bollette.id_contratto,bol_bollette.id as idboll,cod_contratto,num_bolletta" _
                        '& " from siscom_mi.rapporti_utenza,siscom_mi.bol_bollette " _
                        '& " where rapporti_utenza.id=bol_bollette.id_contratto and " _
                        '& " bol_Bollette.num_bolletta='" & par.IfEmpty(riga1.Item("NUM_BOLLETTA"), "") & "' and id_bolletta_storno is null and id_bolletta_ric is null and nvl(fl_annullata,0)=0 " _
                        '& " And rapporti_utenza.cod_contratto='" & par.IfEmpty(riga1.Item("COD_CONTRATTO"), "") & "' order by 1 asc"

                        par.cmd.CommandText = "select bol_bollette.id_contratto,bol_bollette.id as idboll,cod_contratto,num_bolletta" _
                        & " from siscom_mi.rapporti_utenza,siscom_mi.bol_bollette " _
                        & " where rapporti_utenza.id=bol_bollette.id_contratto and nvl(fl_annullata,0) = 0 and nvl(importo_ruolo,0)=0 and " _
                        & " bol_Bollette.num_bolletta='" & par.IfEmpty(riga1.Item("NUM_BOLLETTA"), "") & "'" _
                        & " And rapporti_utenza.cod_contratto='" & par.IfEmpty(riga1.Item("COD_CONTRATTO"), "") & "' order by 1 asc"
                        myReader = par.cmd.ExecuteReader()
                        Do While myReader.Read
                            row = dtBolIngiunte.NewRow()
                            If par.IfNull(riga1.Item("TIPOLOGIA_INGIUNZIONE"), "") <> "" Then
                                par.cmd.CommandText = "SELECT * FROM siscom_mi.tipo_boll_ingiunzione WHERE DESCRIZIONE='" & par.PulisciStrSql(riga1.Item("TIPOLOGIA_INGIUNZIONE").ToString) & "'"
                                Dim myReaderV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderV.Read Then
                                    row.Item("ID_TIPO_INGIUNZIONE") = myReaderV("ID")
                                End If
                                myReaderV.Close()
                            End If


                            row.Item("id_contratto") = par.IfNull(myReader("id_contratto"), 0)
                            row.Item("idboll") = par.IfNull(myReader("idboll"), 0)

                            'myReader.Close()

                            row.Item("cod_contratto") = par.IfEmpty(riga1.Item("cod_contratto"), "")
                            row.Item("numboll") = par.IfEmpty(riga1.Item("num_bolletta"), "")
                            row.Item("TIPOINGIUNZIONE") = par.IfEmpty(riga1.Item("TIPOLOGIA_INGIUNZIONE"), "")

                            dtBolIngiunte.Rows.Add(row)
                        Loop
                        ' 2195/2018
                        myReader.Close()
                    End If
                End If


                ik = ik + 1
                progress.PrimaryTotal = Total * 2
                progress.PrimaryValue = ik
                progress.PrimaryPercent = Int((ik * 100) / (Total * 2))

                progress.SecondaryTotal = Total
                progress.SecondaryValue = ik
                progress.SecondaryPercent = Int((ik * 100) / Total)

                progress.CurrentOperationText = " " & ik.ToString() & " di " & Total


            Next
            connData.chiudi()
            InserisciIngiunzione(dtBolIngiunte)
            par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
        Else
            If Not String.IsNullOrEmpty(msgControlli) Then
                par.modalDialogMessage("Attenzione", msgControlli, Me.Page)
                Exit Sub
            End If
        End If

    End Sub

    Private Sub InserisciIngiunzione(ByVal dtDettaglio As Data.DataTable)
        Try
            connData.apri(True)

            Dim idBolStorico As Long = 0
            Dim numRigheElab As Int64 = 0
            Dim contatore As Int64 = 0
            Dim percent_avanz As Long = 0

            Dim Total As Integer = dtDettaglio.Rows.Count
            Dim progress As RadProgressContext = RadProgressContext.Current
            progress.Speed = "N/A"
            Dim ik1 As Long = 0

            For Each riga1 As Data.DataRow In dtDettaglio.Rows
                numRigheElab = dtDettaglio.Rows.Count

                par.cmd.CommandText = "select * from siscom_mi.bol_bollette where id_contratto=" & riga1.Item("id_contratto") & " and id=" & riga1.Item("idboll")
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then

                    par.cmd.CommandText = "UPDATE siscom_mi.bol_Bollette SET ID_TIPO_INGIUNZIONE=" & par.insDbValue(riga1.Item("id_tipo_ingiunzione"), False) & "," _
                        & " IMPORTO_INGIUNZIONE=(round((IMPORTO_TOTALE-NVL(QUOTA_SIND_B,0)-NVL(IMPORTO_RIC_B,0))-(NVL(IMPORTO_PAGATO,0)- NVL(IMPORTO_RIC_PAGATO_B,0)- NVL(QUOTA_SIND_PAGATA_B,0)),2)) WHERE ID=" & riga1.Item("idboll")
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO siscom_mi.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & riga1.Item("id_contratto") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F301','BOLLETTA: " & riga1.Item("numboll") & " TIPO INGIUNZIONE: " & riga1.Item("tipoingiunzione") & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                lettore.Close()

                ik = ik + 1
                progress.PrimaryTotal = Total * 2
                progress.PrimaryValue = ik
                progress.PrimaryPercent = Int((ik * 100) / (Total * 2))

                progress.SecondaryTotal = Total
                progress.SecondaryValue = ik1
                progress.SecondaryPercent = Int((ik1 * 100) / Total)

                progress.CurrentOperationText = " " & ik1.ToString() & " di " & Total
                ik1 = ik1 + 1

            Next
            connData.chiudi(True)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: CaricamMassivoIngiunzioni - InserisciIngiunzione - " & ex.Message)
            lblErrore.Text = "Provenienza: InserisciIngiunzione - " & ex.Message
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
