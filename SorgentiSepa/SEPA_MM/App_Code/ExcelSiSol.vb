Imports Microsoft.VisualBasic
Imports OfficeOpenXml
Imports System.IO
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports System.Globalization

Public Class ExcelSiSol
    Dim par As New CM.Global

    'Definizione delle Strutture
    ''' <summary>Struttura per la Gestione della Creazione del File Excel sulla risorsa</summary>
    Public Structure StrutturaNuovoFile
        Public NomeFileStruttura As String
        Public NomeFileOriginale As String
        Public PercorsoFileStruttura As String
        Public PercorsoFileStrutturaDownload As String
        Public SuccessoPercorsoFileStrutturaDownload As Boolean
        Public NewFile As FileInfo
        Public Estensione As String
    End Structure
    ''' <summary>Enum per la Gestione del Tipo di File da creare</summary>
    Public Enum Estensione
        Office2003_xls = 1
        Office2007_xlsx = 2
    End Enum
    ''' <summary>Enum per la Gestione del Tipo di Return del File Excel</summary>
    Public Enum TipoReturnFileExcel
        Download = 0
        ZipDownload = 1
    End Enum
    ''' <summary>Funzione per Instanziare la Struttura Pubblica del File Excel</summary>
    Public Function IstanziaFileExcel() As StrutturaNuovoFile

        IstanziaFileExcel = New StrutturaNuovoFile

        Return IstanziaFileExcel
    End Function
    ''' <summary>Funzione per Instanziare il File Excel</summary>
    Public Function IstanziaFile() As ExcelPackage
        IstanziaFile = New ExcelPackage

        Return IstanziaFile
    End Function
    ''' <summary>Funzione per Instanziare un WorkSheet del File Excel</summary>
    Public Function IstanziaWorkSheet() As ExcelWorksheet

        IstanziaWorkSheet = Nothing

        Return IstanziaWorkSheet
    End Function
    ''' <summary>Procedura per la Creazione del File Excel sulla risorsa</summary>
    ''' <param name="File">Definizione del Package che si sta usando per creare il File Excel</param>
    ''' <param name="Estensione">Definizione dell'estensione del File Excel (Office2003-2007)</param>
    ''' <param name="NomeFile">Definizione del Nome del File Excel</param>
    ''' <param name="DataFileName">Definizione Opzionale se inserire la Data all'interno del Nome del File Excel</param>
    ''' <param name="Percorso">Definizione del Percorso del File Excel</param>
    Public Function CreaFile(ByRef File As ExcelPackage, ByVal Estensione As Estensione, ByVal NomeFile As String, Optional ByVal DataFileName As Boolean = True, Optional ByVal Percorso As String = "~\/FileTemp\/") As StrutturaNuovoFile

        Dim FileExcel As New StrutturaNuovoFile
        FileExcel.SuccessoPercorsoFileStrutturaDownload = False
        If ContaCaratteriStringa("~", Percorso) > 0 Then
            FileExcel.PercorsoFileStrutturaDownload = Percorso.Replace("~\/", "")
            For i As Integer = 0 To 50
                If Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("" & FileExcel.PercorsoFileStrutturaDownload)) Then
                    If System.Web.HttpContext.Current.Server.MapPath(Percorso) = System.Web.HttpContext.Current.Server.MapPath("" & FileExcel.PercorsoFileStrutturaDownload) Then
                        FileExcel.SuccessoPercorsoFileStrutturaDownload = True
                        Exit For
                    Else
                        FileExcel.PercorsoFileStrutturaDownload = "..\/" & FileExcel.PercorsoFileStrutturaDownload
                        i = i + 1
                    End If
                Else
                    FileExcel.PercorsoFileStrutturaDownload = "..\/" & FileExcel.PercorsoFileStrutturaDownload
                    i = i + 1
                End If
            Next
        Else
            FileExcel.SuccessoPercorsoFileStrutturaDownload = True
            FileExcel.PercorsoFileStrutturaDownload = Percorso
        End If
        FileExcel.NomeFileOriginale = NomeFile
        FileExcel.PercorsoFileStruttura = Percorso
        If DataFileName Then
            NomeFile = NomeFile & Format(Now, "yyyyMMddHHmmss")
        Else
            NomeFile = NomeFile
        End If
        FileExcel.NomeFileStruttura = NomeFile
        Select Case Estensione
            Case 1
                FileExcel.Estensione = ".xls"
            Case 2
                FileExcel.Estensione = ".xlsx"
            Case Else
                FileExcel.Estensione = ".xls"
        End Select
        If System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(Percorso & NomeFile & FileExcel.Estensione)) Then
            System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(Percorso & NomeFile & FileExcel.Estensione))
        End If

        FileExcel.NewFile = New FileInfo(System.Web.HttpContext.Current.Server.MapPath(Percorso & NomeFile & FileExcel.Estensione))
        File = New ExcelPackage(FileExcel.NewFile)

        Return FileExcel
    End Function
    ''' <summary>Conta il numero di volte che si presenta un carattere all'interno della Stringa</summary>
    ''' <param name="car">Definizione del carattere da ricercare nella stringa.</param>
    ''' <param name="str">Definizione della stringa su cui ricercare il carattere.</param>
    Private Function ContaCaratteriStringa(car As String, str As String) As Long
        If Len(car) <> 1 Then Err.Raise(5)
        ContaCaratteriStringa = Len(str) - Len(Replace(str, car, "", , , vbTextCompare))
        Return ContaCaratteriStringa
    End Function
    ''' <summary>Procedura per Impostare i MetaData del File Excel</summary>
    ''' <param name="FileExcel">Definizione del File Excel su cui impostare i MetaData</param>
    ''' <param name="Titolo">Definizione del Titolo del File Excel</param>
    ''' <param name="Autore">Definizione dell'Autore del File Excel</param>
    ''' <param name="Commento">Definizione del Commento del File Excel</param>
    ''' <param name="Compagnia">Definizione della Compagnia del File Excel</param>
    ''' <param name="KeyWords">Definizione delle Parole Chiavi del File Excel</param>
    ''' <param name="SottoTitolo">Definizione del SottoTitolo del File Excel</param>
    Public Function SetMetaData(ByRef FileExcel As ExcelPackage, ByVal Titolo As String, ByVal Autore As String, Optional ByVal Commento As String = "", Optional ByVal Compagnia As String = "Sistemi & Soluzioni S.r.l.", Optional KeyWords As String = "", Optional ByVal SottoTitolo As String = "") As ExcelPackage

        FileExcel.Workbook.Properties.Title = Titolo
        If Not String.IsNullOrEmpty(Autore) Then
            FileExcel.Workbook.Properties.Author = Autore
        Else
            FileExcel.Workbook.Properties.Author = "© S&S Sistemi & Soluzioni S.r.l."
        End If
        FileExcel.Workbook.Properties.Comments = Commento
        FileExcel.Workbook.Properties.Company = Compagnia
        FileExcel.Workbook.Properties.Keywords = KeyWords
        FileExcel.Workbook.Properties.LastModifiedBy = System.Web.HttpContext.Current.Session.Item("OPERATORE")
        FileExcel.Workbook.Properties.Subject = SottoTitolo

        Return FileExcel
    End Function
    ''' <summary>Procedura per salvare il File Excel con le modifiche effettuate</summary>
    ''' <param name="FilePackage">Definizione del Package del File Excel</param>
    ''' <param name="FileExcel">Definizione del File Excel Impostato sulla risorsa</param>
    ''' <param name="Page">Definizione della Pagine.</param>
    ''' <param name="Type">Definizione del Tipo delle Pagina.</param>
    Public Function ChiudiDocumento(ByRef FilePackage As ExcelPackage, ByVal FileExcel As StrutturaNuovoFile, ByVal Page As System.Web.UI.Page, Type As System.Type) As Boolean
        ChiudiDocumento = False
        Try
            FilePackage.Save()
            ChiudiDocumento = True
        Catch ex1 As Exception
            If File.Exists(FileExcel.NewFile.ToString) Then
                For i As Integer = 0 To 10
                    Try
                        File.Delete(FileExcel.NewFile.ToString)
                        Exit For
                    Catch ex2 As Exception
                        i += 1
                    End Try
                Next
            End If
            ScriptManager.RegisterClientScriptBlock(Page, Type, "msg", "alert('Errore nella procedura di creazione File Excel!');", True)
            ChiudiDocumento = False
        End Try
        Return ChiudiDocumento
    End Function
    ''' <summary>Procedura per salvare il File Excel con le modifiche effettuate</summary>
    ''' <param name="FilePackage">Definizione del Package del File Excel</param>
    ''' <param name="FileExcel">Definizione del File Excel Impostato sulla risorsa</param>
    Public Function ChiudiDocumentoClean(ByRef FilePackage As ExcelPackage, ByVal FileExcel As StrutturaNuovoFile) As Boolean
        ChiudiDocumentoClean = False
        Try
            FilePackage.Save()
            ChiudiDocumentoClean = True
        Catch ex1 As Exception
            If File.Exists(FileExcel.NewFile.ToString) Then
                For i As Integer = 0 To 10
                    Try
                        File.Delete(FileExcel.NewFile.ToString)
                        Exit For
                    Catch ex2 As Exception
                        i += 1
                    End Try
                Next
            End If
            ChiudiDocumentoClean = False
        End Try
        Return ChiudiDocumentoClean
    End Function
    ''' <summary>Procedura per il Download del File Excel</summary>
    ''' <param name="FileExcel">Definizione del File Excel per il Download</param>
    ''' <param name="Page">Definizione della Pagine.</param>
    ''' <param name="Type">Definizione del Tipo delle Pagina.</param>
    ''' <param name="TipoReturnFileExcel">Definizione dell'Estensione del File che deve essere scaricato</param>
    Public Function RitornaFileExcel(ByVal FileExcel As StrutturaNuovoFile, ByVal Page As System.Web.UI.Page, Type As System.Type, Optional ByVal TipoReturnFileExcel As TipoReturnFileExcel = TipoReturnFileExcel.Download) As Boolean
        RitornaFileExcel = False
        If File.Exists(FileExcel.NewFile.ToString) Then
            If FileExcel.SuccessoPercorsoFileStrutturaDownload = False Then TipoReturnFileExcel = 0
            Select Case TipoReturnFileExcel
                Case 0
                    ScriptManager.RegisterClientScriptBlock(Page, Type, "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('" & FileExcel.PercorsoFileStrutturaDownload & FileExcel.NomeFileStruttura & FileExcel.Estensione & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');};", True)
                Case 1
                    CreaZipFile(FileExcel)
                    ScriptManager.RegisterStartupScript(Page, Type, "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('" & FileExcel.PercorsoFileStrutturaDownload & FileExcel.NomeFileStruttura & ".zip','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');};", True)
            End Select
        Else
            If File.Exists(FileExcel.NewFile.ToString) Then
                For i As Integer = 0 To 10
                    Try
                        File.Delete(FileExcel.NewFile.ToString)
                        Exit For
                    Catch ex2 As Exception
                        i += 1
                    End Try
                Next
            End If
            ScriptManager.RegisterClientScriptBlock(Page, Type, "msg", "alert('Errore nella procedura di creazione File Excel!');", True)
        End If
        RitornaFileExcel = True
    End Function
    ''' <summary>Procedura per la Creazione del File Zip contenente il File Excel</summary>
    ''' <param name="FileExcel">Definizione del File Excel da includere nel Pacchetto Zip</param>
    Private Function CreaZipFile(ByVal FileExcel As StrutturaNuovoFile) As String
        CreaZipFile = ""
        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        Dim strFile As String
        strFile = FileExcel.NewFile.ToString
        Dim strmFile As FileStream = File.OpenRead(strFile)
        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
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
        Dim zipfic As String = System.Web.HttpContext.Current.Server.MapPath(FileExcel.PercorsoFileStruttura & FileExcel.NomeFileStruttura & ".zip")
        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        strmZipOutputStream.SetLevel(6)
        strmZipOutputStream.PutNextEntry(theEntry)
        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        strmZipOutputStream.Finish()
        strmZipOutputStream.Close()
        File.Delete(strFile)
        Return CreaZipFile
    End Function
    ''' <summary>Procedura per la Creazione di Un WorkSheet all'interno del File Excel e Assegnazione</summary>
    ''' <param name="FileExcel">Definizione del File Excel su cui creare il WorkSheet</param>
    ''' <param name="WorkSheet">Definizione del WorkSheet da Assegnare per Lavorare</param>
    ''' <param name="NomeWorkSheet">Definizione del Nome da assegnare al WorkSheet del File Excel</param>
    Public Function AggiungiNuovoWorkSheetEAssegna(ByRef FileExcel As ExcelPackage, ByRef WorkSheet As ExcelWorksheet, ByVal NomeWorkSheet As String) As ExcelWorksheet
        AggiungiNuovoWorkSheetEAssegna = FileExcel.Workbook.Worksheets.Add(NomeWorkSheet)
    End Function
    ''' <summary>Procedura per riempire il WorkSheet attraverso la DataTable</summary>
    ''' <param name="worksheet">Definizione del WorkSheet da utilizzare</param>
    ''' <param name="dt">Definizione della DataTable da cui prelevare i Dati</param>
    ''' <param name="AutoFitCol">Definizione se le Colonne del File Excel devono essere adattate automaticamente</param>
    Public Function LoadExcelFromDT(ByRef WorkSheet As ExcelWorksheet, ByVal dt As Data.DataTable, Optional ByVal AutoFitCol As Boolean = True, Optional ByVal ReplaceTesto As Boolean = True, Optional ByVal IntestazionaColonna As Boolean = True) As ExcelWorksheet
        If IntestazionaColonna Then

            For j = 0 To dt.Columns.Count - 1 Step 1
                If ReplaceTesto Then
                    dt.Columns.Item(j).ColumnName = dt.Columns.Item(j).ColumnName.Replace("_", " ")
                Else
                    dt.Columns.Item(j).ColumnName = dt.Columns.Item(j).ColumnName
                End If
            Next
        End If
        WorkSheet.Cells.LoadFromDataTable(dt, IntestazionaColonna)
        If AutoFitCol Then WorkSheet.Cells.AutoFitColumns()
        If IntestazionaColonna Then
            WorkSheet.Row(1).Style.Font.Bold = True
            WorkSheet.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
        End If
        If IntestazionaColonna Then
            If ReplaceTesto Then
                For j = 0 To dt.Columns.Count - 1 Step 1
                    dt.Columns.Item(j).ColumnName = dt.Columns.Item(j).ColumnName.Replace(" ", "_")
                Next
            End If
        End If
        For k As Integer = 0 To dt.Columns.Count - 1 Step 1
            If IntestazionaColonna Then
                Try
                    If dt.Rows.Count > 0 AndAlso IsDate(WorkSheet.Cells.Value(1, k)) Then
                        WorkSheet.Column(k + 1).Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern
                    Else
                        WorkSheet.Column(k + 1).Style.Numberformat.Format = ""
                    End If
                Catch ex As Exception
                End Try
            Else
                Try
                    If dt.Rows.Count > 0 AndAlso IsDate(WorkSheet.Cells.Value(0, k)) Then
                        WorkSheet.Column(k + 1).Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern
                    Else
                        WorkSheet.Column(k + 1).Style.Numberformat.Format = ""
                    End If
                Catch ex As Exception
                End Try
            End If
        Next
        Return WorkSheet
    End Function

    Public Function LoadExcelFromDTCalLavori(ByRef WorkSheet As ExcelWorksheet, ByVal dt As Data.DataTable, Optional ByVal AutoFitCol As Boolean = True) As ExcelWorksheet
        Dim MultiElemento As Boolean = False
        Dim Elementi As Integer = 0

        For j = 0 To dt.Columns.Count - 1 Step 1
            dt.Columns.Item(j).ColumnName = dt.Columns.Item(j).ColumnName.Replace("_", " ")
        Next
        WorkSheet.Cells.LoadFromDataTable(dt, True)
        WorkSheet.Cells.AutoFitColumns()
        WorkSheet.Row(1).Style.Font.Bold = True
        WorkSheet.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
        WorkSheet.Cells.Style.WrapText = True
        For j = 1 To dt.Columns.Count
            WorkSheet.Cells(1, j).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            WorkSheet.Cells(1, j).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#7091c5"))

        Next

        For j = 0 To dt.Rows.Count
            WorkSheet.Cells(j + 1, 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            WorkSheet.Cells(j + 1, 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#dce5f3"))

            If j <> 0 Then
                For i = 0 To dt.Columns.Count
                    Elementi = 0
                    If InStr(WorkSheet.Cells(j + 1, i + 1).Text, " - ORDINE DA VERIFICARE ") > 0 Then
                        Elementi = Elementi + 1
                    End If
                    If InStr(WorkSheet.Cells(j + 1, i + 1).Text, " - ORDINE IN CARICO ") > 0 Then
                        Elementi = Elementi + 1
                    End If
                    If InStr(WorkSheet.Cells(j + 1, i + 1).Text, " - ORDINE ANNULLATO ") > 0 Then
                        Elementi = Elementi + 1
                    End If
                    If InStr(WorkSheet.Cells(j + 1, i + 1).Text, " - ORDINE EVASO ") > 0 Then '***
                        Elementi = Elementi + 1
                    End If
                    If InStr(WorkSheet.Cells(j + 1, i + 1).Text, " - RICHIESTA CONSUNTIVAZIONE ") > 0 Then
                        Elementi = Elementi + 1
                    End If
                    If InStr(WorkSheet.Cells(j + 1, i + 1).Text, " - ORDINE DA CONSUNTIVARE ") > 0 Then '***
                        Elementi = Elementi + 1
                    End If


                    If Elementi = 1 Then
                        If InStr(WorkSheet.Cells(j + 1, i + 1).Text, " - ORDINE DA VERIFICARE ") > 0 Then
                            WorkSheet.Cells(j + 1, i + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            '//////////////////////
                            '// 1433/2019
                            'WorkSheet.Cells(j + 1, i + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#d4ffbc"))
                            WorkSheet.Cells(j + 1, i + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#eeff03"))
                        End If
                        If InStr(WorkSheet.Cells(j + 1, i + 1).Text, " - ORDINE IN CARICO ") > 0 Then
                            WorkSheet.Cells(j + 1, i + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            WorkSheet.Cells(j + 1, i + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f18888"))
                        End If
                        If InStr(WorkSheet.Cells(j + 1, i + 1).Text, " - ORDINE ANNULLATO ") > 0 Then
                            WorkSheet.Cells(j + 1, i + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            WorkSheet.Cells(j + 1, i + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#b51a00"))
                        End If
                        If InStr(WorkSheet.Cells(j + 1, i + 1).Text, " - ORDINE EVASO") > 0 Then '***
                            WorkSheet.Cells(j + 1, i + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            '////////////////////
                            '// 1433/2019
                            'WorkSheet.Cells(j + 1, i + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#ffffff"))
                            WorkSheet.Cells(j + 1, i + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#08920f"))
                        End If
                        If InStr(WorkSheet.Cells(j + 1, i + 1).Text, " - RICHIESTA CONSUNTIVAZIONE ") > 0 Then
                            WorkSheet.Cells(j + 1, i + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            WorkSheet.Cells(j + 1, i + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#ffaa00"))
                        End If
                        If InStr(WorkSheet.Cells(j + 1, i + 1).Text, " - ORDINE DA CONSUNTIVARE ") > 0 Then '***
                            WorkSheet.Cells(j + 1, i + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            WorkSheet.Cells(j + 1, i + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#297fb8"))
                        End If

                    End If
                Next
            End If
        Next
        Return WorkSheet
    End Function
#Region "Funzioni"
    ''' <summary>Esporta DataTable in File Excel per Calendario Lavori</summary>
    ''' <param name="Estensione">Definizione dell'estensione del File Excel</param>
    ''' <param name="NomeFile">Definizione del Nome del File Excel</param>
    ''' <param name="NomeWorkSheet">Definizione del Nome del WorkSheet del File Excel</param>
    ''' <param name="dt">Definizione della DataTable da cui prelevare i Dati</param>
    ''' <param name="DataFileName">Definizione se inserire la data all'interno del Nome del File Excel</param>
    ''' <param name="Percorso">Definizione del Percorso del File Excel</param>
    ''' <param name="AutoFitCol">Definizione se le Colonne del File Excel devono essere adattate automaticamente</param>
    Public Function EsportaExcelDaDTCalLavori(ByVal Estensione As Estensione, ByVal NomeFile As String, ByVal NomeWorkSheet As String, ByVal dt As Data.DataTable, Optional ByVal DataFileName As Boolean = True, Optional ByVal Percorso As String = "~\/FileTemp\/", Optional AutoFitCol As Boolean = True) As String
        EsportaExcelDaDTCalLavori = ""
        Try

            Dim FileExcel = IstanziaFileExcel()
            Dim File = IstanziaFile()
            Dim WorkSheet = IstanziaWorkSheet()
            FileExcel = CreaFile(File, Estensione, NomeFile, DataFileName, Percorso)
            WorkSheet = AggiungiNuovoWorkSheetEAssegna(File, WorkSheet, NomeWorkSheet)
            LoadExcelFromDTCalLavori(WorkSheet, dt, AutoFitCol)
            SetMetaData(File, NomeFile, "S&S Sistemi & Soluzioni S.r.l.")
            If ChiudiDocumentoClean(File, FileExcel) Then
                File.Dispose()
                WorkSheet.Dispose()
                EsportaExcelDaDTCalLavori = FileExcel.NomeFileStruttura & FileExcel.Estensione
            End If
        Catch ex2 As OutOfMemoryException
            EsportaExcelDaDTCalLavori = "M"
        Catch ex As Exception
            EsportaExcelDaDTCalLavori = ""
        End Try
    End Function
    ''' <summary>Esporta DataTable in File Excel</summary>
    ''' <param name="Estensione">Definizione dell'estensione del File Excel</param>
    ''' <param name="NomeFile">Definizione del Nome del File Excel</param>
    ''' <param name="NomeWorkSheet">Definizione del Nome del WorkSheet del File Excel</param>
    ''' <param name="dt">Definizione della DataTable da cui prelevare i Dati</param>
    ''' <param name="DataFileName">Definizione se inserire la data all'interno del Nome del File Excel</param>
    ''' <param name="Percorso">Definizione del Percorso del File Excel</param>
    ''' <param name="AutoFitCol">Definizione se le Colonne del File Excel devono essere adattate automaticamente</param>
    Public Function EsportaExcelDaDT(ByVal Estensione As Estensione, ByVal NomeFile As String, ByVal NomeWorkSheet As String, ByVal dt As Data.DataTable, Optional ByVal DataFileName As Boolean = True, Optional ByVal Percorso As String = "~\/FileTemp\/", Optional AutoFitCol As Boolean = True, Optional ByVal ReplaceTesto As Boolean = True, Optional ByVal IntestazioneColonna As Boolean = True, Optional ByVal CreaZip As Boolean = False) As String
        EsportaExcelDaDT = ""
        Try
            NomeFile = NomeFile.Replace("/", "_")

            Dim FileExcel = IstanziaFileExcel()
            Dim File = IstanziaFile()
            Dim WorkSheet = IstanziaWorkSheet()
            FileExcel = CreaFile(File, Estensione, NomeFile, DataFileName, Percorso)
            WorkSheet = AggiungiNuovoWorkSheetEAssegna(File, WorkSheet, NomeWorkSheet)
            LoadExcelFromDT(WorkSheet, dt, AutoFitCol, ReplaceTesto, IntestazioneColonna)
            SetMetaData(File, NomeFile, "S&S Sistemi & Soluzioni S.r.l.")
            If ChiudiDocumentoClean(File, FileExcel) Then
                File.Dispose()
                WorkSheet.Dispose()
                EsportaExcelDaDT = FileExcel.NomeFileStruttura & FileExcel.Estensione
            End If
            If CreaZip Then
                CreaZipFile(FileExcel)
                Select Case Estensione
                    Case ExcelSiSol.Estensione.Office2003_xls
                        EsportaExcelDaDT = EsportaExcelDaDT.Replace(".xls", ".zip")
                    Case ExcelSiSol.Estensione.Office2007_xlsx
                        EsportaExcelDaDT = EsportaExcelDaDT.Replace(".xlsx", ".zip")
                End Select
            End If
        Catch ex2 As OutOfMemoryException
            EsportaExcelDaDT = "M"
        Catch ex As Exception
            EsportaExcelDaDT = ""
        End Try
    End Function

    ''' <summary>Esporta Più DataTable in un File Excel (creando diversi fogli)</summary>
    ''' <param name="Estensione">Definizione dell'estensione del File Excel</param>
    ''' <param name="NomeFile">Definizione del Nome del File Excel</param>
    ''' <param name="Dicdt">Definizione del dizionario contenente il nome dei fogli e le tabelle che si intendono esportare</param>
    ''' <param name="DataFileName">Definizione se inserire la data all'interno del Nome del File Excel</param>
    ''' <param name="Percorso">Definizione del Percorso del File Excel</param>
    ''' <param name="AutoFitCol">Definizione se le Colonne del File Excel devono essere adattate automaticamente</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EsportaExcelDaDTFogli(ByVal Estensione As Estensione, ByVal NomeFile As String, ByVal Dicdt As Generic.Dictionary(Of String, Data.DataTable), Optional ByVal DataFileName As Boolean = True, Optional ByVal Percorso As String = "~\/FileTemp\/", Optional AutoFitCol As Boolean = True, Optional ByVal ReplaceTesto As Boolean = True, Optional ByVal IntestazioneColonna As Boolean = True, Optional ByVal CreaZip As Boolean = False) As String
        EsportaExcelDaDTFogli = ""
        Try
            Dim FileExcel = IstanziaFileExcel()
            Dim File = IstanziaFile()
            Dim WorkSheet = IstanziaWorkSheet()

            FileExcel = CreaFile(File, Estensione, NomeFile, DataFileName, Percorso)
            Dim foglio As Generic.KeyValuePair(Of String, Data.DataTable)
            For Each foglio In Dicdt
                Dim NomeWorkSheet = foglio.Key
                Dim dt As Data.DataTable = foglio.Value

                WorkSheet = AggiungiNuovoWorkSheetEAssegna(File, WorkSheet, NomeWorkSheet)
                LoadExcelFromDT(WorkSheet, dt, AutoFitCol, ReplaceTesto, IntestazioneColonna)
            Next

            SetMetaData(File, NomeFile, "S&S Sistemi & Soluzioni S.r.l.")
            If ChiudiDocumentoClean(File, FileExcel) Then
                EsportaExcelDaDTFogli = FileExcel.NomeFileStruttura & FileExcel.Estensione
            End If
            If CreaZip Then
                CreaZipFile(FileExcel)
                Select Case Estensione
                    Case ExcelSiSol.Estensione.Office2003_xls
                        EsportaExcelDaDTFogli = EsportaExcelDaDTFogli.Replace(".xls", ".zip")
                    Case ExcelSiSol.Estensione.Office2007_xlsx
                        EsportaExcelDaDTFogli = EsportaExcelDaDTFogli.Replace(".xlsx", ".zip")
                End Select
            End If
        Catch ex2 As OutOfMemoryException
            EsportaExcelDaDTFogli = "M"
        Catch ex As System.Exception
            'If ex.GetHashCode =
            EsportaExcelDaDTFogli = ""
        End Try
    End Function
    ''' <summary>Esporta Datagrid in File Excel</summary>
    ''' <param name="Estensione">Definizione dell'estensione del File Excel</param>
    ''' <param name="NomeFile">Definizione del Nome del File Excel</param>
    ''' <param name="NomeWorkSheet">Definizione del Nome del WorkSheet del File Excel</param>
    ''' <param name="DataGrid">Definizione del DataGrid da cui prelevare i Dati</param>
    ''' <param name="DataFileName">Definizione se inserire la data all'interno del Nome del File Excel</param>
    ''' <param name="Percorso">Definizione del Percorso del File Excel</param>
    ''' <param name="AutoFitCol">Definizione se le Colonne del File Excel devono essere adattate automaticamente</param>
    Public Function EsportaExcelDaDataGrid(ByVal Estensione As Estensione, ByVal NomeFile As String, ByVal NomeWorkSheet As String, ByVal DataGrid As DataGrid, Optional DataFileName As Boolean = True, Optional ByVal Percorso As String = "~\/FileTemp\/", Optional AutoFitCol As Boolean = True) As String
        EsportaExcelDaDataGrid = ""
        Try
            Dim FileExcel = IstanziaFileExcel()
            Dim File = IstanziaFile()
            Dim WorkSheet = IstanziaWorkSheet()
            FileExcel = CreaFile(File, Estensione, NomeFile, DataFileName, Percorso)
            WorkSheet = AggiungiNuovoWorkSheetEAssegna(File, WorkSheet, NomeWorkSheet)
            Dim contacolonne As Long = 1
            For j = 0 To DataGrid.Columns.Count - 1 Step 1
                If DataGrid.Columns(j).Visible Then
                    WorkSheet.Cells(1, contacolonne).Value = DataGrid.Columns.Item(j).HeaderText
                    contacolonne = contacolonne + 1
                End If
            Next
            WorkSheet.Row(1).Style.Font.Bold = True
            WorkSheet.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
            Dim contariga As Long = 2
            contacolonne = 1
            For Each riga As DataGridItem In DataGrid.Items
                For j = 0 To DataGrid.Columns.Count - 1 Step 1
                    If DataGrid.Columns(j).Visible Then
                        If IsNumeric(Replace(par.IfNull(riga.Cells(j).Text, ""), "&nbsp;", "")) Then
                            '********************************
                            'MODIFICA 12-05-2016
                            'Condizione impostata per far memorizzare come testo i codice contratto tutti numerici che iniziano con lo 0.
                            'Se un numero comincia con 0 ed è decimale controlliamo che esista la virgola nel numero.
                            'Marco
                            '********************************
                            If Left(Replace(par.IfNull(riga.Cells(j).Text, ""), "&nbsp;", ""), 1).ToString <> "0" Or riga.Cells(j).Text.Contains(",") Then
                                If Not String.IsNullOrEmpty(Replace(par.IfNull(riga.Cells(j).Text, ""), "&nbsp;", "")) Then
                                    Try
                                        If Replace(par.IfNull(riga.Cells(j).Text, ""), "&nbsp;", "").Contains(",") Then
                                            Dim int As Decimal = Convert.ToDecimal(Replace(par.IfNull(riga.Cells(j).Text, ""), "&nbsp;", ""))
                                            WorkSheet.Cells(contariga, contacolonne).Value = int
                                        Else
                                            Dim int As Long = Convert.ToInt64(Replace(par.IfNull(riga.Cells(j).Text, ""), "&nbsp;", ""))
                                            WorkSheet.Cells(contariga, contacolonne).Value = int
                                        End If
                                    Catch ex As Exception
                                        WorkSheet.Cells(contariga, contacolonne).Value = par.RimuoviHTMLTAG(Replace(par.IfNull(riga.Cells(j).Text, ""), "&nbsp;", ""))
                                    End Try
                                Else
                                    WorkSheet.Cells(contariga, contacolonne).Value = par.RimuoviHTMLTAG(Replace(par.IfNull(riga.Cells(j).Text, ""), "&nbsp;", ""))
                                End If
                            Else
                                WorkSheet.Cells(contariga, contacolonne).Value = par.RimuoviHTMLTAG(Replace(par.IfNull(riga.Cells(j).Text, ""), "&nbsp;", ""))
                            End If
                        Else
                            WorkSheet.Cells(contariga, contacolonne).Value = par.RimuoviHTMLTAG(Replace(par.IfNull(riga.Cells(j).Text, ""), "&nbsp;", ""))
                        End If
                        Select Case DataGrid.Columns.Item(j).ItemStyle.HorizontalAlign
                            Case 1
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                            Case 2
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                            Case 3
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                            Case Else
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                        End Select
                        contacolonne = contacolonne + 1
                    End If
                Next
                contacolonne = 1
                contariga = contariga + 1
            Next
            If AutoFitCol Then WorkSheet.Cells.AutoFitColumns()
            SetMetaData(File, NomeFile, "S&S Sistemi & Soluzioni S.r.l.")
            If ChiudiDocumentoClean(File, FileExcel) Then
                EsportaExcelDaDataGrid = FileExcel.NomeFileStruttura & FileExcel.Estensione
            End If
        Catch ex As Exception
            EsportaExcelDaDataGrid = ""
        End Try
    End Function
    ''' <summary>Esporta Datagrid con Dati dalla DataTable in File Excel</summary>
    ''' <param name="Estensione">Definizione dell'estensione del File Excel</param>
    ''' <param name="NomeFile">Definizione del Nome del File Excel</param>
    ''' <param name="NomeWorkSheet">Definizione del Nome del WorkSheet del File Excel</param>
    ''' <param name="DataGrid">Definizione del DataGrid da cui prelevare lo Stile</param>
    ''' <param name="dt">Definizione del DataGrid da cui prelevare i Dati</param>
    ''' <param name="DataFileName">Definizione se inserire la data all'interno del Nome del File Excel</param>
    ''' <param name="Percorso">Definizione del Percorso del File Excel</param>
    ''' <param name="AutoFitCol">Definizione se le Colonne del File Excel devono essere adattate automaticamente</param>
    Public Function EsportaExcelDaDataGridWithDT(ByVal Estensione As Estensione, ByVal NomeFile As String, ByVal NomeWorkSheet As String, ByVal DataGrid As DataGrid, ByVal dt As Data.DataTable, Optional DataFileName As Boolean = True, Optional ByVal Percorso As String = "~\/FileTemp\/", Optional AutoFitCol As Boolean = True) As String

        EsportaExcelDaDataGridWithDT = ""
        Try
            Dim FileExcel = IstanziaFileExcel()
            Dim File = IstanziaFile()
            Dim WorkSheet = IstanziaWorkSheet()
            FileExcel = CreaFile(File, Estensione, NomeFile, DataFileName, Percorso)
            WorkSheet = AggiungiNuovoWorkSheetEAssegna(File, WorkSheet, NomeWorkSheet)
            Dim contacolonne As Long = 1
            For j = 0 To DataGrid.Columns.Count - 1 Step 1
                If DataGrid.Columns(j).Visible Then
                    WorkSheet.Cells(1, contacolonne).Value = DataGrid.Columns.Item(j).HeaderText
                    contacolonne = contacolonne + 1
                End If
            Next
            WorkSheet.Row(1).Style.Font.Bold = True
            WorkSheet.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
            Dim contariga As Long = 2
            contacolonne = 1
            For Each row As Data.DataRow In dt.Rows
                For j = 0 To DataGrid.Columns.Count - 1 Step 1
                    If DataGrid.Columns(j).Visible Then
                        If IsNumeric(Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")) Then
                            If Left(Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", ""), 1).ToString <> "0" Then
                                If Not String.IsNullOrEmpty(Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")) Then
                                    Try
                                        Dim int As Decimal = Convert.ToDecimal(Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", ""))
                                        WorkSheet.Cells(contariga, contacolonne).Value = int
                                    Catch ex As Exception
                                        WorkSheet.Cells(contariga, contacolonne).Value = Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")
                                    End Try
                                Else
                                    WorkSheet.Cells(contariga, contacolonne).Value = Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")
                                End If
                            Else
                                WorkSheet.Cells(contariga, contacolonne).Value = Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")
                            End If
                        Else
                            WorkSheet.Cells(contariga, contacolonne).Value = Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")
                        End If
                        Select Case DataGrid.Columns.Item(j).ItemStyle.HorizontalAlign
                            Case 1
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                            Case 2
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                            Case 3
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                            Case Else
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                        End Select
                        contacolonne = contacolonne + 1
                    End If
                Next
                contacolonne = 1
                contariga = contariga + 1
            Next
            If AutoFitCol Then WorkSheet.Cells.AutoFitColumns()
            SetMetaData(File, NomeFile, "S&S Sistemi & Soluzioni S.r.l.")
            If ChiudiDocumentoClean(File, FileExcel) Then
                EsportaExcelDaDataGridWithDT = FileExcel.NomeFileStruttura & FileExcel.Estensione
            End If
        Catch ex As Exception
            EsportaExcelDaDataGridWithDT = ""
        End Try
    End Function
    Public Function EsportaExcelDaDataGridWithDTColor(ByVal Estensione As Estensione, ByVal NomeFile As String, ByVal NomeWorkSheet As String, ByVal DataGrid As DataGrid, ByVal dt As Data.DataTable, Optional DataFileName As Boolean = True, Optional ByVal Percorso As String = "~\/FileTemp\/", Optional AutoFitCol As Boolean = True, Optional colonnacolor As String = "") As String
        EsportaExcelDaDataGridWithDTColor = ""
        Try
            Dim FileExcel = IstanziaFileExcel()
            Dim File = IstanziaFile()
            Dim WorkSheet = IstanziaWorkSheet()

            FileExcel = CreaFile(File, Estensione, NomeFile, DataFileName, Percorso)
            WorkSheet = AggiungiNuovoWorkSheetEAssegna(File, WorkSheet, NomeWorkSheet)
            Dim contacolonne As Long = 1
            Dim iColToColor As Integer = 1
            For j = 0 To DataGrid.Columns.Count - 1 Step 1
                If DataGrid.Columns(j).Visible Then
                    WorkSheet.Cells(1, contacolonne).Value = DataGrid.Columns.Item(j).HeaderText
                    If DataGrid.Columns.Item(j).HeaderText = colonnacolor Then
                        iColToColor = contacolonne
                    End If
                    contacolonne = contacolonne + 1
                End If
            Next
            WorkSheet.Row(1).Style.Font.Bold = True
            WorkSheet.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
            Dim contariga As Long = 2
            contacolonne = 1
            For Each row As Data.DataRow In dt.Rows
                For j = 0 To DataGrid.Columns.Count - 1 Step 1
                    If DataGrid.Columns(j).Visible Then
                        If IsNumeric(Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")) Then
                            If Left(Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", ""), 1).ToString <> "0" Then
                                If Not String.IsNullOrEmpty(Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")) Then
                                    Try
                                        Dim int As Long = Convert.ToInt64(Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", ""))
                                        WorkSheet.Cells(contariga, contacolonne).Value = int
                                    Catch ex As Exception
                                        WorkSheet.Cells(contariga, contacolonne).Value = Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")
                                    End Try
                                Else
                                    WorkSheet.Cells(contariga, contacolonne).Value = Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")
                                End If
                            Else
                                WorkSheet.Cells(contariga, contacolonne).Value = Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")
                            End If
                        Else
                            WorkSheet.Cells(contariga, contacolonne).Value = Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")
                        End If
                        Select Case DataGrid.Columns.Item(j).ItemStyle.HorizontalAlign
                            Case 1
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                            Case 2
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                            Case 3
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                            Case Else
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                        End Select
                        contacolonne = contacolonne + 1
                    End If

                Next

                Select Case row("ID_PERICOLO_SEGNALAZIONE").ToString
                    Case "1"
                        WorkSheet.Cells(contariga, iColToColor).Style.Fill.PatternType = Style.ExcelFillStyle.Solid

                        WorkSheet.Cells(contariga, iColToColor).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White)

                    Case "2"
                        WorkSheet.Cells(contariga, iColToColor).Style.Fill.PatternType = Style.ExcelFillStyle.Solid

                        WorkSheet.Cells(contariga, iColToColor).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Green)

                    Case "3"
                        WorkSheet.Cells(contariga, iColToColor).Style.Fill.PatternType = Style.ExcelFillStyle.Solid

                        WorkSheet.Cells(contariga, iColToColor).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)

                    Case "4"
                        WorkSheet.Cells(contariga, iColToColor).Style.Fill.PatternType = Style.ExcelFillStyle.Solid

                        WorkSheet.Cells(contariga, iColToColor).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)

                    Case "0"
                        WorkSheet.Cells(contariga, iColToColor).Style.Fill.PatternType = Style.ExcelFillStyle.Solid

                        WorkSheet.Cells(contariga, iColToColor).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Blue)

                    Case Else
                        ' WorkSheet.Cells(contariga, iColToColor).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White)

                End Select

                contacolonne = 1
                contariga = contariga + 1


            Next
            If AutoFitCol Then WorkSheet.Cells.AutoFitColumns()
            SetMetaData(File, NomeFile, "S&S Sistemi & Soluzioni S.r.l.")
            If ChiudiDocumentoClean(File, FileExcel) Then
                EsportaExcelDaDataGridWithDTColor = FileExcel.NomeFileStruttura & FileExcel.Estensione
            End If
        Catch ex As Exception
            EsportaExcelDaDataGridWithDTColor = ""
        End Try
    End Function
    Public Function EsportaExcelDaDataGridWithDTColorRadGrid(ByVal Estensione As Estensione, ByVal NomeFile As String, ByVal NomeWorkSheet As String, ByVal DataGrid As Telerik.Web.UI.RadGrid, ByVal dt As Data.DataTable, Optional DataFileName As Boolean = True, Optional ByVal Percorso As String = "~\/FileTemp\/", Optional AutoFitCol As Boolean = True, Optional colonnacolor As String = "") As String
        EsportaExcelDaDataGridWithDTColorRadGrid = ""
        Try
            Dim FileExcel = IstanziaFileExcel()
            Dim File = IstanziaFile()
            Dim WorkSheet = IstanziaWorkSheet()

            FileExcel = CreaFile(File, Estensione, NomeFile, DataFileName, Percorso)
            WorkSheet = AggiungiNuovoWorkSheetEAssegna(File, WorkSheet, NomeWorkSheet)
            Dim contacolonne As Long = 1
            Dim iColToColor As Integer = 1
            For j = 0 To DataGrid.Columns.Count - 1 Step 1
                If DataGrid.Columns(j).Visible Then
                    WorkSheet.Cells(1, contacolonne).Value = DataGrid.Columns.Item(j).HeaderText
                    If DataGrid.Columns.Item(j).HeaderText = colonnacolor Then
                        iColToColor = contacolonne
                    End If
                    contacolonne = contacolonne + 1
                End If
            Next
            WorkSheet.Row(1).Style.Font.Bold = True
            WorkSheet.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
            Dim contariga As Long = 2
            contacolonne = 1
            For Each row As Data.DataRow In dt.Rows
                For j = 0 To DataGrid.Columns.Count - 1 Step 1
                    If DataGrid.Columns(j).Visible Then
                        If IsNumeric(Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")) Then
                            If Left(Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", ""), 1).ToString <> "0" Then
                                If Not String.IsNullOrEmpty(Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")) Then
                                    Try
                                        Dim int As Long = Convert.ToInt64(Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", ""))
                                        WorkSheet.Cells(contariga, contacolonne).Value = int
                                    Catch ex As Exception
                                        WorkSheet.Cells(contariga, contacolonne).Value = Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")
                                    End Try
                                Else
                                    WorkSheet.Cells(contariga, contacolonne).Value = Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")
                                End If
                            Else
                                WorkSheet.Cells(contariga, contacolonne).Value = Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")
                            End If
                        Else
                            WorkSheet.Cells(contariga, contacolonne).Value = Replace(par.IfNull(row.Item(j).ToString, ""), "&nbsp;", "")
                        End If
                        Select Case DataGrid.Columns.Item(j).ItemStyle.HorizontalAlign
                            Case 1
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                            Case 2
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                            Case 3
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                            Case Else
                                WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                        End Select
                        contacolonne = contacolonne + 1
                    End If

                Next

                Select Case row("ID_PERICOLO_SEGNALAZIONE").ToString
                    Case "0"
                        WorkSheet.Cells(contariga, iColToColor + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid

                        WorkSheet.Cells(contariga, iColToColor + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Blue)

                    Case "1"
                        WorkSheet.Cells(contariga, iColToColor + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid

                        WorkSheet.Cells(contariga, iColToColor + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White)

                    Case "2"
                        WorkSheet.Cells(contariga, iColToColor + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid

                        WorkSheet.Cells(contariga, iColToColor + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Green)

                    Case "3"
                        WorkSheet.Cells(contariga, iColToColor + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid

                        WorkSheet.Cells(contariga, iColToColor + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)

                    Case "4"
                        WorkSheet.Cells(contariga, iColToColor + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid

                        WorkSheet.Cells(contariga, iColToColor + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)

                    Case Else
                        ' WorkSheet.Cells(contariga, iColToColor).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White)

                End Select

                contacolonne = 1
                contariga = contariga + 1


            Next
            If AutoFitCol Then WorkSheet.Cells.AutoFitColumns()
            SetMetaData(File, NomeFile, "S&S Sistemi & Soluzioni S.r.l.")
            If ChiudiDocumentoClean(File, FileExcel) Then
                EsportaExcelDaDataGridWithDTColorRadGrid = FileExcel.NomeFileStruttura & FileExcel.Estensione
            End If
        Catch ex As Exception
            EsportaExcelDaDataGridWithDTColorRadGrid = ""
        End Try
    End Function

    ''' <summary>Esporta Datagrid in File Excel</summary>
    ''' <param name="Estensione">Definizione dell'estensione del File Excel</param>
    ''' <param name="NomeFile">Definizione del Nome del File Excel</param>
    ''' <param name="NomeWorkSheet">Definizione del Nome del WorkSheet del File Excel</param>
    ''' <param name="DataGrid">Definizione del DataGrid da cui prelevare i Dati</param>
    ''' <param name="DataFileName">Definizione se inserire la data all'interno del Nome del File Excel</param>
    ''' <param name="Percorso">Definizione del Percorso del File Excel</param>
    ''' <param name="AutoFitCol">Definizione se le Colonne del File Excel devono essere adattate automaticamente</param>
    ''' <param name="IndiceRigaMin">Indice iniziale estrazione parziale righe datagrid</param>
    ''' <param name="IndiceRigaMax">Indice finale estrazione parziale righe datagrid (-1 per infinito)</param>
    ''' <param name="controlloRowspan">Controlla l'unione delle celle nel datagrid (solo per rowspan)</param>
    Public Function EsportaExcelDaDataGridParziale(ByVal Estensione As Estensione, ByVal NomeFile As String, ByVal NomeWorkSheet As String, ByVal DataGrid As DataGrid, Optional DataFileName As Boolean = True, Optional ByVal Percorso As String = "~\/FileTemp\/", Optional AutoFitCol As Boolean = True, Optional ByVal IndiceRigaMin As Integer = 0, Optional ByVal IndiceRigaMax As Integer = -1, Optional ByVal controlloRowspan As Boolean = False) As String
        EsportaExcelDaDataGridParziale = ""
        Try
            Dim FileExcel = IstanziaFileExcel()
            Dim File = IstanziaFile()
            Dim WorkSheet = IstanziaWorkSheet()
            FileExcel = CreaFile(File, Estensione, NomeFile, DataFileName, Percorso)
            WorkSheet = AggiungiNuovoWorkSheetEAssegna(File, WorkSheet, NomeWorkSheet)
            Dim contacolonne As Long = 1
            For j = 0 To DataGrid.Columns.Count - 1 Step 1
                If DataGrid.Columns(j).Visible Then
                    WorkSheet.Cells(1, contacolonne).Value = DataGrid.Columns.Item(j).HeaderText
                    contacolonne = contacolonne + 1
                End If
            Next
            WorkSheet.Row(1).Style.Font.Bold = True
            WorkSheet.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
            Dim contariga As Long = 2
            contacolonne = 1
            Dim indiceRiga As Integer = 0
            If IndiceRigaMax = -1 Then
                IndiceRigaMax = DataGrid.Items.Count - 1
            End If
            Dim value As String = ""
            Dim conversione(2) As String
            Dim unisci As Boolean = False
            Dim kkk As Integer = 1
            Dim indirizzoIniziale As String = ""
            Dim indirizzoFinale As String = ""
            For Each riga As DataGridItem In DataGrid.Items
                If indiceRiga >= IndiceRigaMin And indiceRiga <= IndiceRigaMax Then
                    For j = 0 To DataGrid.Columns.Count - 1 Step 1
                        If DataGrid.Columns(j).Visible Then
                            value = riga.Cells(j).Text.ToString.Replace("&nbsp;", "")
                            If controlloRowspan Then
                                indirizzoIniziale = WorkSheet.Cells(contariga, contacolonne).Address
                                indirizzoFinale = WorkSheet.Cells(contariga + Math.Max(riga.Cells(j).RowSpan, 1) - 1, contacolonne).Address
                                If indirizzoFinale <> indirizzoIniziale Then
                                    WorkSheet.SelectedRange(indirizzoIniziale & ":" & indirizzoFinale).Merge = True
                                    WorkSheet.SelectedRange(indirizzoIniziale).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                                    If AutoFitCol Then WorkSheet.SelectedRange(indirizzoIniziale).AutoFitColumns()
                                End If
                            End If
                            If riga.Cells(j).Visible = False Then
                                value = ""
                            End If
                            If Trim(value) <> "" Then
                                conversione = conversioneDati(DataGrid.Columns.Item(j).HeaderStyle.CssClass.ToString)
                                If conversione(0) <> "0" Then
                                    WorkSheet.Cells(contariga, contacolonne).Style.Numberformat.Format = conversione(0)
                                End If
                                Select Case conversione(1)
                                    Case "0"
                                        WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                    Case "1"
                                        If IsNumeric(value) Then
                                            WorkSheet.Cells(contariga, contacolonne).Value = CDec(value)
                                        Else
                                            WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                        End If
                                    Case "2"
                                        If IsDate(value) Then
                                            WorkSheet.Cells(contariga, contacolonne).Value = CDate(value)
                                        Else
                                            WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                        End If
                                    Case "3"
                                        If IsNumeric(value) Then
                                            WorkSheet.Cells(contariga, contacolonne).Value = CInt(value)
                                        Else
                                            WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                        End If
                                    Case Else
                                        Select Case DataGrid.Columns.Item(j).ItemStyle.HorizontalAlign
                                            Case 1
                                                WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                            Case 2
                                                If IsDate(value) Then
                                                    conversione = conversioneDati("formatoData")
                                                    WorkSheet.Cells(contariga, contacolonne).Style.Numberformat.Format = conversione(0)
                                                    WorkSheet.Cells(contariga, contacolonne).Value = CDate(value)
                                                Else
                                                    WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                                End If
                                            Case 3
                                                If IsNumeric(value) Then
                                                    If value.ToString.Contains(",") Then
                                                        'decimale
                                                        conversione = conversioneDati("formatoDecimale")
                                                        WorkSheet.Cells(contariga, contacolonne).Style.Numberformat.Format = conversione(0)
                                                        WorkSheet.Cells(contariga, contacolonne).Value = CDec(value)
                                                    Else
                                                        'intero
                                                        conversione = conversioneDati("formatoIntero")
                                                        WorkSheet.Cells(contariga, contacolonne).Style.Numberformat.Format = conversione(0)
                                                        WorkSheet.Cells(contariga, contacolonne).Value = CInt(value)
                                                    End If
                                                Else
                                                    WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                                End If
                                            Case Else
                                                WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                        End Select
                                End Select
                            Else
                                WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                            End If
                            Select Case DataGrid.Columns.Item(j).ItemStyle.HorizontalAlign
                                Case 1
                                    WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                                Case 2
                                    WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                                Case 3
                                    WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                                Case Else
                                    WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                            End Select
                            contacolonne = contacolonne + 1
                        End If
                    Next
                    contariga = contariga + 1
                End If
                indiceRiga += 1
                contacolonne = 1
            Next
            If AutoFitCol Then WorkSheet.Cells.AutoFitColumns()
            SetMetaData(File, NomeFile, "S&S Sistemi & Soluzioni S.r.l.")
            If ChiudiDocumentoClean(File, FileExcel) Then
                EsportaExcelDaDataGridParziale = FileExcel.NomeFileStruttura & FileExcel.Estensione
            End If
        Catch ex As Exception
            EsportaExcelDaDataGridParziale = ""
        End Try
    End Function
    Public Function EsportaExcelDaDataGridParzialeConRowspan(ByVal Estensione As Estensione, ByVal NomeFile As String, ByVal NomeWorkSheet As String, ByVal DataGrid As DataGrid, Optional DataFileName As Boolean = True, Optional ByVal Percorso As String = "~\/FileTemp\/", Optional AutoFitCol As Boolean = True, Optional ByVal IndiceRigaMin As Integer = 0, Optional ByVal IndiceRigaMax As Integer = -1, Optional ByVal controlloRowspan As Boolean = False, Optional dtdef As Data.DataTable = Nothing, Optional dtdef2 As Data.DataTable = Nothing, Optional dtdef3 As Data.DataTable = Nothing) As String
        EsportaExcelDaDataGridParzialeConRowspan = ""
        Try
            Dim FileExcel = IstanziaFileExcel()
            Dim File = IstanziaFile()
            Dim WorkSheet = IstanziaWorkSheet()
            FileExcel = CreaFile(File, Estensione, NomeFile, DataFileName, Percorso)
            WorkSheet = AggiungiNuovoWorkSheetEAssegna(File, WorkSheet, NomeWorkSheet)
            Dim contacolonne As Long = 1
            For j = 0 To DataGrid.Columns.Count - 1 Step 1
                If DataGrid.Columns(j).Visible Then
                    WorkSheet.Cells(1, contacolonne).Value = DataGrid.Columns.Item(j).HeaderText
                    contacolonne = contacolonne + 1
                End If
            Next
            WorkSheet.Row(1).Style.Font.Bold = True
            WorkSheet.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
            Dim contariga As Long = 2
            contacolonne = 1
            Dim indiceRiga As Integer = 0
            If IndiceRigaMax = -1 Then
                IndiceRigaMax = dtdef.Rows.Count - 1
            End If
            Dim value As String = ""
            Dim conversione(2) As String
            Dim unisci As Boolean = False
            Dim kkk As Integer = 1
            Dim indirizzoIniziale As String = ""
            Dim indirizzoFinale As String = ""
            Dim rigaRowspan As Data.DataRow
            Dim rigaVisibility As Data.DataRow
            Dim ii As Integer = -1
            For Each riga As Data.DataRow In dtdef.Rows
                ii += 1
                rigaRowspan = dtdef2.Rows(ii)
                rigaVisibility = dtdef3.Rows(ii)
                If indiceRiga >= IndiceRigaMin And indiceRiga <= IndiceRigaMax Then
                    For j = 0 To DataGrid.Columns.Count - 1 Step 1
                        If DataGrid.Columns(j).Visible Then
                            value = riga.Item(j).ToString.Replace("&nbsp;", "")
                            If controlloRowspan Then
                                indirizzoIniziale = WorkSheet.Cells(contariga, contacolonne).Address
                                indirizzoFinale = WorkSheet.Cells(contariga + Math.Max(rigaRowspan.Item(j), 1) - 1, contacolonne).Address
                                If indirizzoFinale <> indirizzoIniziale Then
                                    WorkSheet.SelectedRange(indirizzoIniziale & ":" & indirizzoFinale).Merge = True
                                    WorkSheet.SelectedRange(indirizzoIniziale).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                                    If AutoFitCol Then WorkSheet.SelectedRange(indirizzoIniziale).AutoFitColumns()
                                End If
                            End If
                            If rigaVisibility.Item(j) = 1 Then
                                value = ""
                            End If
                            If Trim(value) <> "" Then
                                conversione = conversioneDati(DataGrid.Columns.Item(j).HeaderStyle.CssClass.ToString)
                                If conversione(0) <> "0" Then
                                    WorkSheet.Cells(contariga, contacolonne).Style.Numberformat.Format = conversione(0)
                                End If
                                Select Case conversione(1)
                                    Case "0"
                                        WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                    Case "1"
                                        If IsNumeric(value) Then
                                            WorkSheet.Cells(contariga, contacolonne).Value = CDec(value)
                                        Else
                                            WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                        End If
                                    Case "2"
                                        If IsDate(value) Then
                                            WorkSheet.Cells(contariga, contacolonne).Value = CDate(value)
                                        Else
                                            WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                        End If
                                    Case "3"
                                        If IsNumeric(value) Then
                                            WorkSheet.Cells(contariga, contacolonne).Value = CInt(value)
                                        Else
                                            WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                        End If
                                    Case Else
                                        Select Case DataGrid.Columns.Item(j).ItemStyle.HorizontalAlign
                                            Case 1
                                                WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                            Case 2
                                                If IsDate(value) Then
                                                    conversione = conversioneDati("formatoData")
                                                    WorkSheet.Cells(contariga, contacolonne).Style.Numberformat.Format = conversione(0)
                                                    WorkSheet.Cells(contariga, contacolonne).Value = CDate(value)
                                                Else
                                                    WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                                End If
                                            Case 3
                                                If IsNumeric(value) Then
                                                    If value.ToString.Contains(",") Then
                                                        'decimale
                                                        conversione = conversioneDati("formatoDecimale")
                                                        WorkSheet.Cells(contariga, contacolonne).Style.Numberformat.Format = conversione(0)
                                                        WorkSheet.Cells(contariga, contacolonne).Value = CDec(value)
                                                    Else
                                                        'intero
                                                        conversione = conversioneDati("formatoIntero")
                                                        WorkSheet.Cells(contariga, contacolonne).Style.Numberformat.Format = conversione(0)
                                                        WorkSheet.Cells(contariga, contacolonne).Value = CInt(value)
                                                    End If
                                                Else
                                                    WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                                End If
                                            Case Else
                                                WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                                        End Select
                                End Select
                            Else
                                WorkSheet.Cells(contariga, contacolonne).Value = CStr(value)
                            End If
                            Select Case DataGrid.Columns.Item(j).ItemStyle.HorizontalAlign
                                Case 1
                                    WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                                Case 2
                                    WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                                Case 3
                                    WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                                Case Else
                                    WorkSheet.Cells(contariga, contacolonne).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                            End Select
                            contacolonne = contacolonne + 1
                        End If
                    Next
                    contariga = contariga + 1
                End If
                indiceRiga += 1
                contacolonne = 1
            Next
            If AutoFitCol Then WorkSheet.Cells.AutoFitColumns()
            SetMetaData(File, NomeFile, "S&S Sistemi & Soluzioni S.r.l.")
            If ChiudiDocumentoClean(File, FileExcel) Then
                EsportaExcelDaDataGridParzialeConRowspan = FileExcel.NomeFileStruttura & FileExcel.Estensione
            End If
        Catch ex As Exception
            EsportaExcelDaDataGridParzialeConRowspan = ""
        End Try
    End Function
    Public Function conversioneDati(ByVal cssClass As String) As String()
        Dim formato(2) As String
        formato(0) = "0"
        formato(1) = "0"
        Dim classe() As String
        classe = Trim(cssClass).ToString.Split(" ")
        For Each elemento As String In classe
            Select Case elemento
                Case "formatoTesto"
                    formato(0) = "0"
                    formato(1) = "0"
                Case "formatoValuta"
                    formato(0) = "€ #,##0.00"
                    formato(1) = "1"
                Case "formatoData"
                    formato(0) = "dd/mm/yyyy"
                    formato(1) = "2"
                Case "formatoData2"
                    formato(0) = "d-mmm"
                    formato(1) = "2"
                Case "formatoDecimale"
                    formato(0) = "#,##0.00"
                    formato(1) = "1"
                Case "formatoIntero"
                    formato(0) = "0"
                    formato(1) = "3"
                Case "formatoPercentualeDec"
                    formato(0) = "0.00%"
                    formato(1) = "1"
                Case "formatoPercentualeInt"
                    formato(0) = "0%"
                    formato(1) = "3"
                Case "formatoDecimaleNoMigliaia"
                    formato(0) = "0.00"
                    formato(1) = "1"
                Case "formatoTimeStamp"
                    formato(0) = "mm/dd/yyyy hh:mm"
                    formato(1) = "2"
                Case Else
                    formato(0) = "0"
                    formato(1) = "-1"
            End Select
        Next
        Return formato
    End Function
#End Region
    ''' <summary>
    ''' Esporta xlsx in una datatable
    ''' </summary>
    ''' <param name="Worksheet">Worksheet del file excel</param>
    ''' <param name="Intestazione">Se e presente un'intestazione alla prima riga del Worksheet</param>
    Public Function WorksheetToDataTable(Worksheet As ExcelWorksheet, Optional Intestazione As Boolean = True) As Data.DataTable
        Dim dt As New Data.DataTable()
        Dim totalCols As Integer = Worksheet.Dimension.[End].Column
        Dim totalRows As Integer = Worksheet.Dimension.[End].Row
        Dim startRow As Integer = If(Intestazione, 2, 1)
        Dim wsRow As ExcelRange
        Dim row As Data.DataRow
        For Each firstRowCell In Worksheet.Cells(1, 1, 1, totalCols)
            dt.Columns.Add(If(Intestazione, firstRowCell.Text, String.Format("Column {0}", firstRowCell.Start.Column)))
        Next
        For rowNum As Integer = startRow To totalRows
            wsRow = Worksheet.Cells(rowNum, 1, rowNum, totalCols)
            row = dt.NewRow()
            For Each cell In wsRow
                row(cell.Start.Column - 1) = cell.Text
            Next
            dt.Rows.Add(row)
        Next
        Return dt
    End Function

End Class
