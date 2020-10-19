Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class Contratti_Report_DownLoad
    Inherits PageSetIdMode
    Dim dt As New Data.DataTable
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim FileCSV As String = "Operatori_" & Format(Now, "yyyyMMddHHmmss")

        Dim i As Long = 0
        Try


            FileCSV = "EXPORT_OP_STATUS_" & Format(Now, "yyyyMMddHHmmss")


            'If Request.QueryString("CHIAMA") = 0 Then 'DISTINTA RATE EMESSE
            '    FileCSV = "Distinta_" & Format(Now, "yyyyMMddHHmmss")
            'End If

            'If Request.QueryString("CHIAMA") = 1 Then
            '    FileCSV = "PagamentiPervenuti_" & Format(Now, "yyyyMMddHHmmss")
            'End If

            'If Request.QueryString("CHIAMA") = 1000 Then
            '    FileCSV = "PagamentiNONPervenuti_" & Format(Now, "yyyyMMddHHmmss")
            'End If

            'If Request.QueryString("CHIAMA") = 99 Or Request.QueryString("CHIAMA") = 100 Then
            '    FileCSV = "ISTAT_" & Format(Now, "yyyyMMddHHmmss")
            'End If

            'If Request.QueryString("CHIAMA") = 2 Then 'PAGAMENTI SINGOLE VOCI
            '    FileCSV = "PagamentiSingoleVoci" & Format(Now, "yyyyMMddHHmmss")
            'End If


            dt = CType(HttpContext.Current.Session.Item("AA"), Data.DataTable)

            If dt.Rows.Count > 0 Then


                If Request.QueryString("CHIAMA") = 0 Then

                    ExportXLS_Chiama0()

                    '    Dim sSql As String
                    '    sSql = "CREATE TABLE OPERATORI ([ENTE] TEXT(50),[OPERATORE] TEXT(50),[COGNOME] TEXT(50),[NOME] TEXT(50),[CF] TEXT(16), [REVOCA] TEXT (3),[ERP] TEXT(3), [BANDO CAMBI] TEXT(3), [FSA] TEXT(2), [ANAGRAFE UTENZA] TEXT(2), [AU-SOLO CONSULTAZIONE] TEXT(2),[AU-PROPOSTA DI DECADENZA] TEXT(2), [ABBINAMENTO] TEXT(2), [CONSULTAZIONE] TEXT(2), [CONTRATTI] TEXT(2), [PARAMETRI CONTRATTI] TEXT(2), [EMISSIONE BOLLETTE] TEXT(2), [INTERESSI LEGALI] TEXT(2), [CENSIMENTO ALLOGGI] TEXT(2), [CENSIMENTO ALLOGGI ESTERNA] TEXT(2), [CENSIMENTO ALLOGGI SOLA LETTURA] TEXT(2), [CONT.ABBINAMENTO 392] TEXT(2), [CONT.ABBINAMENTO ERP] TEXT(2), [CONT.ABBINAMENTO 431] TEXT(2), [CONT.ABBINAMENTO UD] TEXT(2), [CONT.ABBINAMENTO OCC.ABUSIVE] TEXT(2), [CONT.PROVV. ASSEGNAZIONE] TEXT(2), [CONT. SOLA LETTURA] TEXT(2), [CONT. OP. FILIALE DIS/RECUPERO UI] TEXT(2), [CONT. INSERIMENTO] TEXT(2), [CONT. CALCOLO AGG.ISTAT] TEXT(2), [CONT. CALCOLO INTERESSI] TEXT(2), [CONT. REG. MASSIVA] TEXT(2), [CONT. CALCOLO IMPOSTE] TEXT(2))"

                    '    Dim cnString As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                    '           "Data Source=" & Server.MapPath("..\StampeCanoni27\" & FileCSV & ".xls;") & _
                    '           "Extended Properties=""Excel 8.0;HDR=YES"""


                    '    Dim cn As New OleDbConnection(cnString)

                    '    cn.Open()

                    '    Dim cmd As New OleDbCommand(sSql, cn)
                    '    cmd.ExecuteNonQuery()

                    '    Dim cmd1 As New OleDbCommand
                    '    cmd1.Connection = cn
                    '    For Each row In dt.Rows
                    '        'sr.WriteLine(par.IfNull(dt.Rows(i).Item("RATA"), 0) & ";" & par.IfNull(dt.Rows(i).Item("INTESTATARIO"), 0) & ";" & par.IfNull(dt.Rows(i).Item("PERIODO"), 0) & ";" & par.IfNull(dt.Rows(i).Item("AFFITTO"), 0) & ";" & par.IfNull(dt.Rows(i).Item("SPESE"), 0) & ";" & par.IfNull(dt.Rows(i).Item("REGISTRAZIONE"), 0) & ";" & par.IfNull(dt.Rows(i).Item("TOT"), 0) & ";")
                    '        sSql = "INSERT INTO OPERATORI  VALUES ('" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ENTE"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("OPERATORE"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COGNOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_FISCALE"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("REVOCATO"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ERP"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("B_CAM"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FSA"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("AN_UT"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CONS_AN_UT"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PROP_DEC"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ABB"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CONSULTAZIONE"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CONTRATTI"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CONTRATTI_PARAM"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("EMISS_BOLL"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTE_LEGALI"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PED2"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PED2_ESTERNA"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PED2_READ_ONLY"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ABB_OCC_ABUS"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ABB_392"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ABB_ERP"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ABB_431"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ABB_UD"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PROVV_ASSEGN"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CONT_LETTURA"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("OP_FILIALE"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CONT_INSER"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CONT_CALC_AGG_ISTAT"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CONT_CALC_INT"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("REGI_MASS_CONT"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CALC_IMP"), "")) & "')"
                    '        cmd1.CommandText = sSql
                    '        cmd1.ExecuteNonQuery()
                    '        i = i + 1
                    '    Next
                    '    cn.Close()

                End If

                ''sr.Close()

                'Dim objCrc32 As New Crc32()
                'Dim strmZipOutputStream As ZipOutputStream
                'Dim zipfic As String

                'zipfic = Server.MapPath("..\StampeCanoni27\" & FileCSV & ".zip")

                'strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                'strmZipOutputStream.SetLevel(6)
                ''
                'Dim strFile As String
                'strFile = Server.MapPath("..\StampeCanoni27\" & FileCSV & ".xls")
                'Dim strmFile As FileStream = File.OpenRead(strFile)
                'Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                ''
                'strmFile.Read(abyBuffer, 0, abyBuffer.Length)

                'Dim sFile As String = Path.GetFileName(strFile)
                'Dim theEntry As ZipEntry = New ZipEntry(sFile)
                'Dim fi As New FileInfo(strFile)
                'theEntry.DateTime = fi.LastWriteTime
                'theEntry.Size = strmFile.Length
                'strmFile.Close()
                'objCrc32.Reset()
                'objCrc32.Update(abyBuffer)
                'theEntry.Crc = objCrc32.Value
                'strmZipOutputStream.PutNextEntry(theEntry)
                'strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                'strmZipOutputStream.Finish()
                'strmZipOutputStream.Close()

                'File.Delete(strFile)
                'Response.Redirect("..\StampeCanoni27\" & FileCSV & ".zip")
                'Response.Write("<script>window.open('../Varie/" & FileCSV & ".zip','Distinta','');</script>")

            End If
        Catch ex As Exception

        End Try
    End Sub


    Function SiNo(ByVal valore As String) As String
        If valore = "1" Then
            SiNo = "X"
        Else
            SiNo = ""
        End If
    End Function

    Private Sub ExportXLS_Chiama0()
        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String
        Dim row As System.Data.DataRow

        dt = CType(HttpContext.Current.Session.Item("AA"), Data.DataTable)
        sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")

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


            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "ENTE", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "STRUTTURA", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "INDIRIZZO STRUTTURA", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "OPERATORE", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "COGNOME", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "NOME", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "CF", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "REVOCA", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "ERP", 12)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "CAMBI", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "FSA", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "ANAGRAFE UTENZA", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "ANAGRAFE UTENZA (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "ANAGRAFE UTENZA-PROPOSTA DECADENZA", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "ANAGRAFE UTENZA-DECISIONE DECADENZA", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "ABBINAMENTO", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "ABBINAMENTO-ERP", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "ABBINAMENTO-392", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "ABBINAMENTO-431", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "ABBINAMENTO-UD", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 21, "ABBINAMENTO-O.A.", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 22, "ABBINAMENTO-F.O.", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 23, "ABBINAMENTO-C.S.", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 24, "ABBINAMENTO-C.CONVENZIONATO", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 25, "ABBINAMENTO-PROVV.ASSEGNAZIONE", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 26, "SUBENTRI,VOLTURE,RID.CANONE", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 27, "CAMBI EMERGENZA", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 28, "PED", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 29, "CONSULTAZIONE", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 30, "MANUTENZIONI", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 31, "ANAGRAFE PATRIMONIO", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 32, "ANAGRAFE PATRIMONIO (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 33, "ANAGRAFE PATRIMONIO IV e V LOTTO", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 34, "ANAGRAFE PATRIMONIO CENSIMENTO ST. MANUTENTIVO", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 35, "ANAGRAFE PATRIMONIO CENSIMENTO ST. MANUTENTIVO (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 36, "CONTABILITA", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 37, "CONTABILITA-RAGIONERIA", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 38, "CONTABILITA-CONS.PATRIMONIALI", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 39, "CONTABILITA-FLUSSI FINANZIARI", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 40, "CONTABILITA-RIMBORSI SPESE GESTORE", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 41, "CONTABILITA-PRELIEVI", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 42, "CONTABILITA-COMPENSI GESTORE", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 43, "CICLO PASSIVO", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 44, "BP-Nuovo Piano Finanziario", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 45, "BP-Formalizzazione", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 46, "BP-Formalizzazione (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 47, "BP-Compilazione", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 48, "BP-Compilazione (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 49, "BP-Convalida Gestore", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 50, "BP-Convalida Gestore (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 51, "BP-Assegn. Capitoli", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 52, "BP-Assegn. Capitoli (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 53, "BP-Convalida Comune", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 54, "BP-Convalida Comune (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 55, "BP-Gestione Voci Servizi", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 56, "BP-Voci Servizi (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 57, "Manutenzioni e Servizi", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 58, "Manutenzioni e Servizi (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 59, "Ordini e Pagamenti", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 60, "Ordini e Pagamenti (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 61, "Pagamenti a Canone", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 62, "Pagamenti a Canone (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 63, "Lotti", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 64, "Lotti (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 65, "Contratti", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 66, "Contratti (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 67, "CALL CENTER", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 68, "CALL CENTER (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 69, "CALL CENTER-Operatore Comunale", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 70, "CONDOMINIO", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 71, "CONDOMINIO (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 72, "CONTRATTI E BOLLETTE", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 73, "CONTRATTI E BOLLETTE (solo lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 74, "Ins. Contratti", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 75, "Ins. Contratti VIRTUALI", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 76, "Registrazione Cont.", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 77, "Calcolo Imposte", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 78, "Calcolo Agg. ISTAT", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 79, "Parametri", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 80, "Testo Contratti", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 81, "Op. Sede Territoriale - Dis./Recup. UI", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 82, "Calcolo Interessi Leg.", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 83, "Emissione Bollette", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 84, "Morosità", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 85, "GESTIONE AUTONOMA", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 86, "GESTIONE AUTONOMA (Solo Lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 87, "ANAGRAFE UTENZA-DOC.NECESSARIA", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 88, "GESTIONE MOROSITA", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 89, "GESTIONE MOROSITA (Solo Lettura)", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 90, "Rinnovo USD", 0)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 91, "Cambio Intestazione BOX", 0)
            K = 2
            For Each row In dt.Rows
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ENTE"), "")))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ST_ALER"), "")))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO_FILIALE"), "")))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("OPERATORE"), "")))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COGNOME"), "")))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NOME"), "")))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_FISCALE"), "")))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("REVOCATO"), ""))))

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_ERP"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CAMBI"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_FSA"), ""))))

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_AU"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_AU_CONS"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_AU_PROP_DEC"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_AU_DECIDI_DEC"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_ABB"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FL_ABB_ERP"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FL_ABB_392"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FL_ABB_431"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FL_ABB_UD"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FL_ABB_OA"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FL_ABB_FO"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FL_ABB_CS"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FL_ABB_CONV"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 25, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FL_ASS_PROVV"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 26, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_ABB_DEC"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 27, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_EMRI"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 28, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_PED"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 29, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONS"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 30, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_MANUTENZIONI"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 31, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_PED2"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 32, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_PED2_SOLO_LETTURA"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 33, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_PED2_ESTERNA"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 34, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CENS_MANUT"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 35, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CENS_MANUT_SL"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 36, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONTABILE"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 37, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("mod_cont_ragioneria"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 38, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("mod_cont_patrimoniali"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 39, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("mod_cont_flussi"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 40, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("mod_cont_RIMB"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 41, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONT_PRELIEVI"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 42, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONT_COMPENSI"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 43, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CICLO_P"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 44, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_NUOVO_PIANO"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 45, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_FORMALIZZAZIONE"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 46, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_FORMALIZZAZIONE_L"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 47, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_COMPILAZIONE"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 48, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_COMPILAZIONE_L"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 49, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_CONV_ALER"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 50, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_CONV_ALER_L"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 51, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_CAPITOLI"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 52, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_CAPITOLI_L"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 53, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_CONV_COMUNE"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 54, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_CONV_COMUNE_L"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 55, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_VOCI_SERVIZI"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 56, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_VOCI_SERVIZI_L"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 57, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_MS"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 58, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_MS_L"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 59, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_OP"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 60, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_OP_L"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 61, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_PC"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 62, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_PC_L"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 63, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_LO"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 64, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_LO_L"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 65, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_CC"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 66, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BP_CC_L"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 67, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CALL_CENTER"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 68, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CALL_CENTER_SL"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 69, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CALL_CENTER_COM"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 70, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONDOMINIO"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 71, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONDOMINIO_SL"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 72, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONTRATTI"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 73, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONTRATTI_L"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 74, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONTRATTI_INS"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 75, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONTRATTI_INS_V"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 76, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONTRATTI_REG"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 77, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONTRATTI_IMP"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 78, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONTRATTI_ISTAT"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 79, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("mod_contratti_PARAM"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 80, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("mod_contratti_testo"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 81, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("mod_contratti_d"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 82, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONTRATTI_INT"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 83, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("mod_contratti_BOLL"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 84, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONTRATTI_MOR"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 85, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_AUTOGESTIONI"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 86, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_AUTOGESTIONI_SL"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 87, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_AU_DOC_NEC"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 88, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_MOROSITA"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 89, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_MOROSITA_SL"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 90, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONT_RINN_USD"), ""))))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 91, SiNo(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOD_CONT_CAMBIO_BOX"), ""))))



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



    End Sub
End Class
