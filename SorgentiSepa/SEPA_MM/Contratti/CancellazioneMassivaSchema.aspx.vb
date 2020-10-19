Imports System.IO
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports OfficeOpenXml
Imports Telerik.Web.UI.Upload
Imports Telerik.Web.UI

Partial Class Contratti_CancellazioneMassivaSchema
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Public percentuale As Long = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then

        End If
    End Sub

    Private Sub ScaricaFile()
        Try
            Dim Xls As Byte()
            Dim NomeFileXls As String = ""

            connData.apri()
            par.cmd.CommandText = " select * FROM SISCOM_MI.SCHEMA_IMPORT_VOCI_SCHEMA WHERE ID=2 "
            Dim MyReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader2.Read Then
                Xls = par.IfNull(MyReader2("schema_excel"), "")
            End If
            MyReader2.Close()

            connData.chiudi()

            Dim bw As BinaryWriter

            NomeFileXls = "Scheda_Cancella_voci" & Format(Now, "yyyyMMddHHmmss")
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
            Dim NomeFilezip As String = "Scheda_Cancella_voci_" & Format(Now, "yyyyMMddHHmmss") & ".zip"

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

    Protected Sub btnDownload_Click(sender As Object, e As System.EventArgs) Handles btnDownload.Click
        ScaricaFile()
        'Try
        '    Dim Xls As Byte()
        '    Dim NomeFileXls As String = ""

        '    connData.apri()
        '    par.cmd.CommandText = " select * FROM SISCOM_MI.SCHEMA_IMPORT_VOCI_SCHEMA WHERE ID=2"
        '    Dim MyReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        '    If MyReader2.Read Then
        '        Xls = par.IfNull(MyReader2("schema_excel"), "")
        '    End If
        '    MyReader2.Close()

        '    connData.chiudi()

        '    Dim bw As BinaryWriter

        '    NomeFileXls = "Scheda_import_voci" & Format(Now, "yyyyMMddHHmmss")
        '    Dim fileName As String = Server.MapPath("~\FileTemp\") & NomeFileXls & ".xlsx"
        '    Dim fs As New FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite)
        '    bw = New BinaryWriter(fs)
        '    bw.Write(Xls)
        '    bw.Flush()
        '    bw.Close()

        '    Dim newFile As New FileInfo(fileName)

        '    Dim zipfic As String
        '    Dim NomeFilezip As String = "Scheda_import_voci_" & Format(Now, "yyyyMMddHHmmss") & ".zip"

        '    zipfic = Server.MapPath("..\FileTemp\" & NomeFilezip)

        '    Dim kkK As Integer = 0
        '    Dim objCrc32 As New Crc32()
        '    Dim strmZipOutputStream As ZipOutputStream

        '    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        '    strmZipOutputStream.SetLevel(6)

        '    Dim strFile As String = ""
        '    strFile = "~\FileTemp\" & NomeFileXls & ".xlsx"

        '    Dim ff As String = ""
        '    ff = ZipAllegatoDownload(strFile, NomeFileXls & ".xlsx")

        '    If File.Exists(Server.MapPath("~\FileTemp\") & ff) Then
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & ff & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        '        Exit Sub
        '    Else
        '        par.modalDialogMessage("Attenzione", "Si è verificato un errore durante il download. Riprovare!", Me.Page)
        '    End If

        'Catch ex As Exception
        '    If par.OracleConn.State = Data.ConnectionState.Open Then
        '        connData.chiudi(False)
        '    End If
        '    Session.Add("ERRORE", "Provenienza: Cancellazione Massiva Voci - btnDownload_Click - " & ex.Message)
        '    lblErrore.Text = "Provenienza: btnDownload_Click - " & ex.Message
        '    par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
        'End Try
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

    Protected Sub btnAllega_Click(sender As Object, e As System.EventArgs) Handles btnAllega.Click
        Try
            Dim FileName As String = UploadOnServer()
            Dim objFile As Object
            objFile = Server.CreateObject("Scripting.FileSystemObject")

            If Not String.IsNullOrEmpty(FileName) Then
                If objFile.FileExists(FileName) And FileName.Contains(".xlsx") Then
                    Allegafile(FileName)
                Else
                    par.modalDialogMessage("Attenzione", "Tipo file non valido. Selezionare un file .xlsx", Me.Page)
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Cancellazione Massiva Voci - btnAllega_Click - " & ex.Message)
            lblErrore.Text = "Provenienza: btnAllega_Click - " & ex.Message
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
        End Try
    End Sub

    Private Sub Allegafile(ByVal nomeFile As String)

        Dim xls As New ExcelSiSol
        Dim dtFoglio1 As New Data.DataTable
        Dim dtFoglio2 As New Data.DataTable
        Dim dtBolSchema As New Data.DataTable
        Dim msgControlli As String = ""
        txtRisultati.Text = ""
        Using pck As New OfficeOpenXml.ExcelPackage()
            Using stream = File.Open(nomeFile, FileMode.Open)
                pck.Load(stream)
            End Using
            Dim ws As OfficeOpenXml.ExcelWorksheet = pck.Workbook.Worksheets(1)
            dtFoglio1 = xls.WorksheetToDataTable(ws, True)
            'ws = pck.Workbook.Worksheets(2)
            'dtFoglio2 = xls.WorksheetToDataTable(ws, True)
        End Using
        'For Each rowFoglio1 As Data.DataRow In dtFoglio1.Rows
        '    Dim row2 As Data.DataRow
        '    If par.IfNull(rowFoglio1.Item("VOCE"), "") <> "" Then
        '        row2 = dtFoglio2.Select("DESCRIZIONE = '" & rowFoglio1.Item("VOCE").ToString & "'")(0)
        '    End If

        'Next
        Dim idBolStorico As Long = 0
        Dim rigaVuota As Integer = 0
        If ControlliDati(dtFoglio1, msgControlli) = True Then
            connData.apri(True)

            For Each riga1 As Data.DataRow In dtFoglio1.Rows
                If Not String.IsNullOrEmpty(riga1.Item("COD_CONTRATTO").ToString) And Not String.IsNullOrEmpty(riga1.Item("VOCE").ToString) And Not String.IsNullOrEmpty(riga1.Item("ANNO").ToString) Then
                    rigaVuota = 0
                    par.cmd.CommandText = "select * from SISCOM_MI.BOL_SCHEMA WHERE ID_CONTRATTO=(SELECT ID FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & par.IfEmpty(riga1.Item("COD_CONTRATTO"), "") & "') AND ANNO=" & riga1.Item("ANNO") & " AND ID_VOCE=(SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE DESCRIZIONE='" & par.PulisciStrSql(riga1.Item("VOCE").ToString) & "' AND SELEZIONABILE=1)"
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then

                        par.cmd.CommandText = "select SISCOM_MI.seq_bol_schema_storico.nextval from dual"
                        idBolStorico = par.cmd.ExecuteScalar

                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_SCHEMA_STORICO (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                               & "IMPORTO, DA_RATA_OLD, PER_RATE_OLD, IMPORTO_SINGOLA_RATA, IMPORTO_NUOVO, ANNO,ID_OPERATORE,DATA_ORA, DA_RATA_NEW, PER_RATE_NEW) " _
                               & "Values " _
                               & "(" & idBolStorico & "," & par.insDbValue(lettore("id_contratto"), False) _
                               & "," & par.insDbValue(lettore("id_unita"), False) & "," & par.insDbValue(lettore("id_esercizio_f"), False) _
                               & "," & par.insDbValue(lettore("id_voce"), False) & " ," _
                               & par.insDbValue(lettore("importo"), False) _
                               & "," & par.insDbValue(lettore("da_rata"), False) & "," & par.insDbValue(lettore("per_rate"), False) & "," & par.insDbValue(lettore("importo_singola_rata"), False) _
                               & ",0," & par.insDbValue(lettore("anno"), False) & "," & Session.Item("id_operatore") & ",'" & Format(Now, "yyyyMMddHHmmss") & "',null,null)"
                        par.cmd.ExecuteNonQuery()

                    End If
                    lettore.Close()

                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.BOL_SCHEMA WHERE ID_CONTRATTO=(SELECT ID FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & par.IfEmpty(riga1.Item("COD_CONTRATTO"), "") & "') AND ANNO=" & riga1.Item("ANNO") & " AND ID_VOCE=(SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE DESCRIZIONE='" & par.PulisciStrSql(riga1.Item("VOCE").ToString) & "' AND SELEZIONABILE=1)"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                            & "VALUES ((SELECT ID FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & par.IfEmpty(riga1.Item("COD_CONTRATTO"), "") & "')," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F06','Eliminazione massiva voce schema " & par.PulisciStrSql(riga1.Item("VOCE").ToString) & "')"
                    par.cmd.ExecuteNonQuery()
                Else
                    rigaVuota = rigaVuota + 1
                End If

                If rigaVuota > 5 Then
                    Exit For
                End If
            Next
            connData.chiudi(True)

            par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
        Else
            If Not String.IsNullOrEmpty(msgControlli) Then
                par.modalDialogMessage("Attenzione", msgControlli, Me.Page)
                Exit Sub
            End If
        End If

    End Sub


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
            Session.Add("ERRORE", "Provenienza: Cancellazione Massiva Voci - UploadOnServer - " & ex.Message)
            lblErrore.Text = "Provenienza: UploadOnServer - " & ex.Message
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
        End Try

        Return UploadOnServer
    End Function

    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Private Function ControlliDati(ByVal dtFoglio1 As Data.DataTable, ByRef msgAnomalia As String) As Boolean
        ControlliDati = True
        msgAnomalia = ""
        For Each rowFoglio1 As Data.DataRow In dtFoglio1.Rows
            If Not String.IsNullOrEmpty(rowFoglio1.Item("COD_CONTRATTO").ToString) And Not String.IsNullOrEmpty(rowFoglio1.Item("VOCE").ToString) And Not String.IsNullOrEmpty(rowFoglio1.Item("ANNO").ToString) Then
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
                
                If String.IsNullOrEmpty(rowFoglio1.Item("ANNO").ToString) Then
                    msgAnomalia &= "Valore nullo per la colonna Anno!\n"
                    ControlliDati = False
                    Exit For
                End If
                
            End If
        Next

        Return ControlliDati
    End Function

End Class
