Imports System.IO
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports OfficeOpenXml
Imports Telerik.Web.UI.Upload
Imports Telerik.Web.UI

Partial Class Contratti_CaricamentoMassivoConguagli
    Inherits System.Web.UI.Page
    Public percentuale As Int64 = 0
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Private Sub Contratti_CaricamentoMassivoConguagli_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            CaricaAU()
        End If
        RadProgressArea1.Localization.Uploaded = "Avanzamento Totale"
        RadProgressArea1.Localization.UploadedFiles = "Avanzamento"
        RadProgressArea1.Localization.CurrentFileName = "Elaborazione in corso: "

    End Sub
    Protected Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
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

    Private Function UploadOnServer(ByVal Origine As Integer) As String
        UploadOnServer = ""
        Try
            If Origine = 1 Then
                If FileUpload1.HasFile = True Then
                    If File.Exists(Server.MapPath("..\FileTemp\") & FileUpload1.FileName) Then
                        File.Delete(Server.MapPath("..\FileTemp\") & FileUpload1.FileName)
                    End If
                    UploadOnServer = Server.MapPath("..\FileTemp\") & FileUpload1.FileName
                    FileUpload1.SaveAs(UploadOnServer)
                Else
                    par.modalDialogMessage("Attenzione", "Nessun file allegato!", Me.Page)
                End If
            Else
                If FileUpload2.HasFile = True Then
                    If File.Exists(Server.MapPath("..\FileTemp\") & FileUpload2.FileName) Then
                        File.Delete(Server.MapPath("..\FileTemp\") & FileUpload2.FileName)
                    End If
                    UploadOnServer = Server.MapPath("..\FileTemp\") & FileUpload2.FileName
                    FileUpload2.SaveAs(UploadOnServer)
                Else
                    par.modalDialogMessage("Attenzione", "Nessun file allegato!", Me.Page)
                End If
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Caricamento Massivo Voci - UploadOnServer - " & ex.Message)
            lblErrore.Text = "Provenienza: UploadOnServer - " & ex.Message
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
        End Try

        Return UploadOnServer
    End Function

    Private Sub btnAllega_Click(sender As Object, e As EventArgs) Handles btnAllega.Click
        If cmbAU.Items.Count > 0 Then
            If cmbAU.SelectedItem.Value <> "" Then
                Try
                    Dim FileName As String = UploadOnServer(1)
                    Dim objFile As Object
                    objFile = Server.CreateObject("Scripting.FileSystemObject")
                    ik = 0
                    If Not String.IsNullOrEmpty(FileName) Then
                        If objFile.FileExists(FileName) And FileName.Contains(".xlsx") Then
                            AllegafileTot(FileName, 1)
                        Else
                            par.modalDialogMessage("Attenzione", "Tipo file non valido. Selezionare un file .xlsx", Me.Page)
                        End If
                    End If
                Catch ex As Exception
                    Session.Add("ERRORE", "Provenienza: Caricamento Massivo Voci - btnAllega_Click - " & ex.Message)
                    lblErrore.Text = "Provenienza: btnAllega_Click - " & ex.Message
                    par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
                End Try
            Else
                par.modalDialogMessage("Attenzione", "Selezionare una Anagrafe Utenza di riferimento!", Me.Page)
            End If
        Else
            par.modalDialogMessage("Attenzione", "Selezionare una Anagrafe Utenza di riferimento!", Me.Page)
        End If

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
                'If par.IfEmpty(rowFoglio1.Item("PER_RATE"), 0) > 12 Then
                '    msgAnomalia &= "Valore non ammesso per la colonna Per Rate!\n"
                '    ControlliDati = False
                '    Exit For
                'End If
                'If (par.IfEmpty(CInt(rowFoglio1.Item("DA_RATA")), 0) + par.IfEmpty(CInt(rowFoglio1.Item("PER_RATE")), 0)) > 14 Then
                '    msgAnomalia &= "Valore non ammesso per le colonne Da Rata e Per Rate!\n"
                '    ControlliDati = False
                '    Exit For
                'End If
            End If
        Next

        Return ControlliDati
    End Function

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
    Protected Sub btnAllega1_Click(sender As Object, e As EventArgs) Handles btnAllega1.Click
        If cmbAU.Items.Count > 0 Then
            If cmbAU.SelectedItem.Value <> "" Then
                Try
                    Dim FileName As String = UploadOnServer(2)
                    Dim objFile As Object
                    objFile = Server.CreateObject("Scripting.FileSystemObject")
                    ik = 0
                    If Not String.IsNullOrEmpty(FileName) Then
                        If objFile.FileExists(FileName) And FileName.Contains(".xlsx") Then
                            AllegafileTot(FileName, 2)
                        Else
                            par.modalDialogMessage("Attenzione", "Tipo file non valido. Selezionare un file .xlsx", Me.Page)
                        End If
                    End If
                Catch ex As Exception
                    Session.Add("ERRORE", "Provenienza: Caricamento Massivo Voci - btnAllega1_Click - " & ex.Message)
                    lblErrore.Text = "Provenienza: btnAllega1_Click - " & ex.Message
                    par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
                End Try
            Else
                par.modalDialogMessage("Attenzione", "Selezionare una Anagrafe Utenza di riferimento!", Me.Page)
            End If
        Else
            par.modalDialogMessage("Attenzione", "Selezionare una Anagrafe Utenza di riferimento!", Me.Page)
        End If
    End Sub

    Private Sub InserisciVoceInSchemaTot(ByVal dtDettaglio As Data.DataTable, ByVal Origine As Integer)
        Try
            connData.apri(True)

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

            Dim TipoBollettaGest As Integer = 0

            par.cmd.CommandText = "select * from TAB_VOCI_CONG_AU where id_au=" & cmbAU.SelectedItem.Value
            Dim lettoreAU As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreAU.Read Then
                TipoBollettaGest = lettoreAU("id_tipo_gest")
            End If
            lettoreAU.Close()

            For Each riga1 As Data.DataRow In dtDettaglio.Rows
                numRigheElab = dtDettaglio.Rows.Count
                If riga1.Item("id_voce") <> "-1" Then
                    par.cmd.CommandText = "select bol_bollette_gest.* from SISCOM_MI.bol_bollette_gest where id_contratto=" & riga1.Item("id_contratto") & " and id_tipo=" & TipoBollettaGest & " and tipo_applicazione='N'"
                    Dim lettoreGEST As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettoreGEST.Read Then
                        par.cmd.CommandText = "select * from SISCOM_MI.bol_schema where id_contratto=" & riga1.Item("id_contratto") & " and id_voce=" & riga1.Item("id_voce") & " and anno=" & riga1.Item("anno") & " and da_rata=" & riga1.Item("da_rata") & " and per_rate=" & riga1.Item("per_rate")
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
                                   & "," & par.insDbValue(riga1.Item("importo_singola_rata"), False) & "," & par.insDbValue(lettore("anno"), False) & "," & Session.Item("id_operatore") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & par.insDbValue(riga1.Item("da_rata"), False) & "," & par.insDbValue(riga1.Item("per_rate"), False) & ")"
                            par.cmd.ExecuteNonQuery()
                            If Origine = 1 Then
                                strUpdate = "UPDATE SISCOM_MI.BOL_SCHEMA" _
                                    & " SET " _
                                    & " ID_ESERCIZIO_F = " & par.insDbValue(riga1.Item("id_esercizio_f"), False) & "," _
                                    & " IMPORTO = " & par.insDbValue(riga1.Item("importo"), False) & "," _
                                    & " DA_RATA = " & par.insDbValue(riga1.Item("da_rata"), False) & "," _
                                    & " PER_RATE = " & par.insDbValue(riga1.Item("per_rate"), False) & "," _
                                    & " IMPORTO_SINGOLA_RATA = " & par.insDbValue(riga1.Item("importo_singola_rata"), False) & " " _
                                    & " " _
                                    & " WHERE ID = " & par.IfNull(lettore("ID"), 0)
                                par.cmd.CommandText = strUpdate
                                strUpdate = strUpdate & ";"
                                par.cmd.ExecuteNonQuery()
                                strInsertEventiUpdate = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & par.insDbValue(riga1.Item("id_contratto"), False) & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F184','Inserimento massivo voce schema " & par.PulisciStrSql(riga1.Item("nomevoce")) & " - importo " & par.insDbValue(riga1.Item("importo"), False) & " euro - da rata " & par.insDbValue(riga1.Item("da_rata"), False) & " a rata " & par.insDbValue(riga1.Item("per_rate"), False) & "')"

                            Else
                                par.cmd.CommandText = "delete from SISCOM_MI.bol_schema where id_contratto=" & par.insDbValue(riga1.Item("id_contratto"), False) & " and anno>=" & par.insDbValue(riga1.Item("anno"), False) & " and id_voce=" & par.insDbValue(riga1.Item("id_voce"), False)
                                par.cmd.ExecuteNonQuery()

                                'par.scritturaSchemaBollette(riga1.Item("id_contratto"), lettore("id_unita"), lettore("id_voce"), lettore("importo"), lettore("per_rate"), lettore("anno") & Format(CInt(riga1.Item("da_rata")), "00"),,, , 2)
                                par.scritturaSchemaBollette(riga1.Item("id_contratto"), riga1.Item("id_unita"), riga1.Item("id_voce"), riga1.Item("importo"), riga1.Item("per_rate"), riga1.Item("anno") & Format(riga1.Item("da_rata"), "00"), , , , 2, True)

                                strInsertEventiUpdate = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & par.insDbValue(riga1.Item("id_contratto"), False) & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F184','Inserimento massivo voce schema " & par.PulisciStrSql(riga1.Item("nomevoce")) & " - importo Totale " & par.insDbValue(riga1.Item("importo"), False) & " euro - da rata " & par.insDbValue(riga1.Item("da_rata"), False) & " a rata " & par.insDbValue(riga1.Item("per_rate"), False) & "')"
                            End If

                            par.cmd.CommandText = strInsertEventiUpdate
                            par.cmd.ExecuteNonQuery()

                        Else
                            par.cmd.CommandText = "select SISCOM_MI.seq_bol_schema_storico.nextval from dual"
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

                            par.cmd.CommandText = "delete from SISCOM_MI.bol_schema where id_contratto=" & par.insDbValue(riga1.Item("id_contratto"), False) & " and anno>=" & par.insDbValue(riga1.Item("anno"), False) & " and id_voce=" & par.insDbValue(riga1.Item("id_voce"), False)
                            par.cmd.ExecuteNonQuery()
                            If Origine = 1 Then
                                par.scritturaSchemaBollette(riga1.Item("id_contratto"), riga1.Item("id_unita"), riga1.Item("id_voce"), riga1.Item("importo"), riga1.Item("per_rate"), riga1.Item("anno") & Format(riga1.Item("da_rata"), "00"), , , , 2, True)
                                strInsertEventi2 = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & par.insDbValue(riga1.Item("id_contratto"), False) & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F05','Inserimento massivo voce schema " & par.PulisciStrSql(riga1.Item("nomevoce")) & " - importo " & par.insDbValue(riga1.Item("importo"), False) & " euro - da rata " & par.insDbValue(riga1.Item("da_rata"), False) & " a rata " & par.insDbValue(riga1.Item("per_rate"), False) & "')"
                            Else
                                par.scritturaSchemaBollette(riga1.Item("id_contratto"), riga1.Item("id_unita"), riga1.Item("id_voce"), riga1.Item("importo"), riga1.Item("per_rate"), riga1.Item("anno") & Format(riga1.Item("da_rata"), "00"), , , , 2, True)

                                strInsertEventi2 = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & par.insDbValue(riga1.Item("id_contratto"), False) & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F05','Inserimento massivo voce schema " & par.PulisciStrSql(riga1.Item("nomevoce")) & " - importo Totale " & par.insDbValue(riga1.Item("importo"), False) & " euro - da rata " & par.insDbValue(riga1.Item("da_rata"), False) & " a rata " & par.insDbValue(riga1.Item("per_rate"), False) & "')"
                            End If
                            par.cmd.CommandText = strInsertEventi2
                            par.cmd.ExecuteNonQuery()
                        End If
                        lettore.Close()
                        par.cmd.CommandText = "update SISCOM_MI.bol_bollette_gest set tipo_applicazione='T' where id=" & lettoreGEST("id")
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "select cod_contratto from SISCOM_MI.rapporti_utenza where id=" & riga1.Item("id_contratto")
                        Dim lettoreGEST1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettoreGEST1.Read Then
                            lblEsito.Visible = True
                            txtRisultati.Visible = True
                            txtRisultati.Text &= "Contratto " & lettoreGEST1("cod_contratto") & ": Voce già utilizzata/Non Trovata" & vbCrLf
                            btnExport.Visible = True
                        End If
                        lettoreGEST1.Close()

                    End If
                Else
                    lblEsito.Visible = True
                    txtRisultati.Visible = True
                    txtRisultati.Text &= "Voce non valida: " & riga1.Item("NOMEVOCE") & vbCrLf
                    btnExport.Visible = True
                End If

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

            If lblEsito.Visible = True Then
                Session.Add("ExportCarMassivo", txtRisultati.Text)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: CaricamentoMassivoVoci - InserisciVoceInSchema - " & ex.Message)
            lblErrore.Text = "Provenienza: InserisciVoceInSchema - " & ex.Message
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
        End Try
    End Sub

    Private Sub AllegafileTot(ByVal nomeFile As String, ByVal Origine As Integer)
        lblErrore.Text = ""
        Dim xls As New ExcelSiSol
        Dim dtFoglio1 As New Data.DataTable
        Dim dtFoglio2 As New Data.DataTable
        Dim dtBolSchema As New Data.DataTable
        Dim msgControlli As String = ""
        txtRisultati.Text = ""
        btnExport.Visible = False
        Dim IndiceAttuale As Long = 0
        Dim DescrizioneAttuale As String = ""

        Dim RataMinimoPartenza As Integer = 0
        Dim AnnoMinimoPartenza As Integer = 0

        Dim IndiceContratto As Integer = 0
        Dim IndiceUnita As Integer = 0

        lblEsito.Visible = False

        Using pck As New OfficeOpenXml.ExcelPackage()
            Using stream = File.Open(nomeFile, FileMode.Open, FileAccess.Read)
                pck.Load(stream)
            End Using
            Dim ws As OfficeOpenXml.ExcelWorksheet = pck.Workbook.Worksheets(1)
            dtFoglio1 = xls.WorksheetToDataTable(ws, True)
        End Using


        If ControlliDati(dtFoglio1, msgControlli) = True Then
            connData.apri()

            Dim ElencoEserciziFinanziari As New Generic.Dictionary(Of Int64, Int64)
            par.cmd.CommandText = "SELECT ID,TO_NUMBER(TRIM(SUBSTR(INIZIO,1,4))) AS ANNO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO ORDER BY ID"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                ElencoEserciziFinanziari.Add(par.IfNull(lettore("ANNO"), -1), par.IfNull(lettore("ID"), -1))
            End While
            lettore.Close()

            par.cmd.CommandText = "SELECT 0 as ID, 0 as ID_CONTRATTO, 0 as ID_UNITA, 0 as ID_ESERCIZIO_F, 0 as ID_VOCE,'' as NOMEVOCE, 0.00 as IMPORTO, 0 as DA_RATA, 0 as PER_RATE, 0.00 as IMPORTO_SINGOLA_RATA, 0 as ANNO FROM dual"
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

                    IndiceContratto = 0
                    IndiceUnita = 0
                    par.cmd.CommandText = "select rapporti_utenza_prossima_bol.prossima_bolletta,rapporti_utenza.id,unita_contrattuale.id_unita,rapporti_utenza.data_riconsegna from SISCOM_MI.rapporti_utenza_prossima_bol,SISCOM_MI.rapporti_utenza,SISCOM_MI.unita_contrattuale where rapporti_utenza_prossima_bol.id_contratto=rapporti_utenza.id and rapporti_utenza.id=unita_contrattuale.id_contratto " _
                        & " and id_unita_principale is null and rapporti_utenza.cod_contratto='" & par.IfEmpty(riga1.Item("COD_CONTRATTO"), "") & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = True Then
                        If myReader.Read Then
                            IndiceContratto = par.IfNull(myReader("ID"), 0)
                            IndiceUnita = par.IfNull(myReader("ID_UNITA"), 0)
                            AnnoMinimoPartenza = Mid(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "XXXXXX"), 1, 4)
                            RataMinimoPartenza = Mid(par.IfNull(myReader("PROSSIMA_BOLLETTA"), "XXXXXX"), 5, 2)
                        End If
                        If par.IfNull(myReader("DATA_RICONSEGNA"), "") = "" Then
                            If AnnoMinimoPartenza & Format(RataMinimoPartenza, "00") > par.IfEmpty(riga1.Item("ANNO"), Year(Now)) & Format(CInt(par.IfEmpty(riga1.Item("DA_RATA"), 1)), "00") Then
                                lblEsito.Visible = True
                                txtRisultati.Visible = True
                                txtRisultati.Text &= "Cod. contratto con intervallo non valido: " & riga1.Item("COD_CONTRATTO") & vbCrLf
                                btnExport.Visible = True
                            Else
                                row = dtBolSchema.NewRow()
                                row.Item("ID_CONTRATTO") = IndiceContratto
                                row.Item("ID_UNITA") = IndiceUnita
                                If par.IfNull(riga1.Item("VOCE"), "") <> "" Then
                                    If par.IfNull(riga1.Item("VOCE"), "") <> DescrizioneAttuale Then
                                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE DESCRIZIONE='" & par.PulisciStrSql(riga1.Item("VOCE").ToString) & "'"
                                        Dim myReaderV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                        If myReaderV.Read Then
                                            row.Item("ID_VOCE") = myReaderV("ID")
                                            IndiceAttuale = myReaderV("ID")
                                        Else
                                            row.Item("ID_VOCE") = "-1"
                                            IndiceAttuale = 0
                                        End If
                                        myReaderV.Close()
                                        DescrizioneAttuale = par.IfNull(riga1.Item("VOCE"), "")
                                    Else
                                        row.Item("ID_VOCE") = IndiceAttuale
                                    End If
                                End If

                                row.Item("ID") = 0
                                row.Item("ID_ESERCIZIO_F") = ElencoEserciziFinanziari(par.IfEmpty(riga1.Item("ANNO"), Year(Now)))
                                If Origine = 1 Then
                                    row.Item("IMPORTO") = Format(riga1.Item("IMPORTO") * riga1.Item("PER_RATE"), "##,##0.00")
                                Else
                                    row.Item("IMPORTO") = par.IfEmpty(riga1.Item("IMPORTO"), 0)
                                End If
                                row.Item("DA_RATA") = par.IfEmpty(riga1.Item("DA_RATA"), 0)
                                row.Item("PER_RATE") = par.IfEmpty(riga1.Item("PER_RATE"), 0)
                                row.Item("IMPORTO_SINGOLA_RATA") = par.IfEmpty(riga1.Item("IMPORTO"), 0)
                                row.Item("ANNO") = par.IfEmpty(riga1.Item("ANNO"), Year(Now))
                                row.Item("NOMEVOCE") = par.IfEmpty(riga1.Item("VOCE"), "")

                                dtBolSchema.Rows.Add(row)
                            End If
                        Else
                            lblEsito.Visible = True
                            txtRisultati.Visible = True
                            txtRisultati.Text &= "Contratto CHIUSO: " & riga1.Item("COD_CONTRATTO") & vbCrLf
                            btnExport.Visible = True
                        End If
                    Else
                        lblEsito.Visible = True
                        txtRisultati.Visible = True
                        txtRisultati.Text &= "Cod. contratto non trovato: " & riga1.Item("COD_CONTRATTO") & vbCrLf
                        btnExport.Visible = True
                    End If
                    myReader.Close()

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
            InserisciVoceInSchemaTot(dtBolSchema, Origine)
            par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
        Else
            If Not String.IsNullOrEmpty(msgControlli) Then
                par.modalDialogMessage("Attenzione", msgControlli, Me.Page)
                Exit Sub
            End If
        End If

    End Sub

    Private Sub CaricaAU()
        par.caricaComboBox("select distinct UTENZA_BANDI.ID,UTENZA_BANDI.DESCRIZIONE from TAB_VOCI_CONG_AU,UTENZA_BANDI WHERE UTENZA_BANDI.ID=TAB_VOCI_CONG_AU.ID_AU ORDER BY UTENZA_BANDI.DESCRIZIONE ASC", cmbAU, "ID", "DESCRIZIONE", False)
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        If cmbAU.Items.Count > 0 Then
            If cmbAU.SelectedItem.Value <> "" Then
                Try
                    Dim Xls As Byte()
                    Dim NomeFileXls As String = ""

                    connData.apri()
                    par.cmd.CommandText = "select * FROM SISCOM_MI.SCHEMA_IMPORT_VOCI_SCHEMA WHERE ID=1"
                    Dim MyReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If MyReader2.Read Then
                        Xls = par.IfNull(MyReader2("schema_excel"), "")
                    End If
                    MyReader2.Close()



                    Dim bw As BinaryWriter

                    NomeFileXls = "Scheda_import_voci" & Format(Now, "yyyyMMddHHmmss")
                    Dim fileName As String = Server.MapPath("~\FileTemp\") & NomeFileXls & ".xlsx"
                    Dim fs As New FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite)
                    bw = New BinaryWriter(fs)
                    bw.Write(Xls)
                    bw.Flush()
                    bw.Close()

                    par.cmd.CommandText = "select t_voci_bolletta.id,t_voci_bolletta.descrizione from SISCOM_MI.t_voci_bolletta,TAB_VOCI_CONG_AU where t_voci_bolletta.id=TAB_VOCI_CONG_AU.id_voce and TAB_VOCI_CONG_AU .id_au=" & cmbAU.SelectedItem.Value & " order by 2 asc"
                    'par.cmd.CommandText = "select id,descrizione from t_voci_bolletta where selezionabile=1 order by 2 asc"
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
                    Dim NomeFilezip As String = "Scheda_import_voci_" & Format(Now, "yyyyMMddHHmmss") & ".zip"

                    zipfic = Server.MapPath("..\FileTemp\" & NomeFilezip)

                    Dim kkK As Integer = 0
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
                        connData.chiudi()
                        Exit Sub
                    Else
                        par.modalDialogMessage("Attenzione", "Si è verificato un errore durante il download. Riprovare!", Me.Page)
                    End If

                    connData.chiudi()
                Catch ex As Exception
                    connData.chiudi()
                    Session.Add("ERRORE", "Provenienza: Caricamento Massivo Voci - btnDownload_Click - " & ex.Message)
                    lblErrore.Text = "Provenienza: btnDownload_Click - " & ex.Message
                    par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
                End Try
            Else
                par.modalDialogMessage("Attenzione", "Selezionare una Anagrafe Utenza di riferimento!", Me.Page)
            End If
        Else
            par.modalDialogMessage("Attenzione", "Selezionare una Anagrafe Utenza di riferimento!", Me.Page)
        End If
    End Sub


End Class
