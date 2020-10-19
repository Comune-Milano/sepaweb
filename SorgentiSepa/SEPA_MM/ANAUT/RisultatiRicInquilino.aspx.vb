Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_RisultatiRicInquilino
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreAUSI As String
    Dim sValoreC As String
    Dim sValoreN As String
    Dim sValoreCO As String
    Dim sValoreCON As String
    Dim sValoreIDF As String
    Dim sValoreIDO As String
    Dim sValoreDA As String
    Dim sValoreA As String
    Dim sValoreM As String
    Dim sValoreST As String
    Dim sValore392 As String
    Dim sValore431 As String
    Dim sValoreOP As String
    Dim sStringaSql As String
    Dim scriptblock As String
    Dim dt As New System.Data.DataTable

    Private Function ExportXLS()
        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String = ""
        Dim row As System.Data.DataRow
        Dim par As New CM.Global

        Dim FileCSV As String = ""

        Try

            dt = CType(HttpContext.Current.Session.Item("AA1"), Data.DataTable)

            FileCSV = "Estrazione" & Format(Now, "yyyyMMddHHmmss")

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    i = 0
                    With myExcelFile

                        .CreateFile(Server.MapPath("..\FileTemp\" & FileCSV & ".xls"))
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

                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "NOMINATIVO")
                        '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "COD.CONTRATTO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "FILIALE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "N.CONVOCAZIONE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "STATO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "MOTIVO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "GIORNO APPUNTAMENTO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "ORA APPUNTAMENTO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "PRESA IN CARICO AUSI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "DATA PRESA IN CARICO AUSI")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "OPERATORE")

                        K = 2
                        For Each row In dt.Rows
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(dt.Rows(i).Item("NOMINATIVO"), ""))
                            '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt.Rows(i).Item("NOME"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt.Rows(i).Item("FILIALE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(dt.Rows(i).Item("N_CONVOCAZIONE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.IfNull(dt.Rows(i).Item("STATOCONV"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfNull(dt.Rows(i).Item("MOTIVO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(dt.Rows(i).Item("GIORNO_APP"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(dt.Rows(i).Item("ORE_APP"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(dt.Rows(i).Item("CARICO_AUSI"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.IfNull(dt.Rows(i).Item("DATA_AUSI"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.IfNull(dt.Rows(i).Item("OPERATORE"), ""))

                            i = i + 1
                            K = K + 1
                        Next

                        .CloseFile()
                    End With

                End If

                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                Dim zipfic As String

                zipfic = Server.MapPath("..\FileTemp\" & FileCSV & ".zip")

                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)

                Dim strFile As String
                strFile = Server.MapPath("..\FileTemp\" & FileCSV & ".xls")
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
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()

                File.Delete(strFile)

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                Dim scriptblock As String = "<script language='javascript' type='text/javascript'>" _
                                        & "window.open('../FileTemp/" & FileCSV & ".zip','Estrazione','');" _
                                        & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript30023")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript30023", scriptblock)
                End If


                'Response.Redirect("..\FileTemp\" & FileCSV & ".zip")
            Else

            End If

        Catch ex As Exception
            HttpContext.Current.Session.Remove("AA1")

            Session.Add("ERRORE", "Provenienza:Export Excel Convocazioni - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try



    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()
        If Not IsPostBack Then

            sValoreAUSI = par.DeCriptaMolto(Request.QueryString("AU"))
            sValoreC = par.DeCriptaMolto(Request.QueryString("C"))
            sValoreN = par.DeCriptaMolto(Request.QueryString("N"))
            sValoreCON = par.DeCriptaMolto(Request.QueryString("CON"))
            sValoreCO = par.DeCriptaMolto(Request.QueryString("CO"))
            sValoreIDF = par.DeCriptaMolto(Request.QueryString("IDF"))
            sValoreIDO = par.DeCriptaMolto(Request.QueryString("IDO"))
            sValoreDA = Request.QueryString("DA")
            sValoreA = Request.QueryString("A")
            sValoreST = Request.QueryString("ST")
            sValoreM = par.DeCriptaMolto(Request.QueryString("M"))
            sValore392 = Request.QueryString("392")
            sValore431 = Request.QueryString("431")
            sValoreOP = par.DeCripta(Request.QueryString("OP"))
            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Value = "-1"
            Cerca()

        End If
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Key", "<script>MakeStaticHeader('" + DataGrid1.ClientID + "', 350, 653 , 25 ,true); </script>", False)
    End Sub

    Private Function Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String


        bTrovato = False
        sStringaSql = ""

        If sValoreC <> "" Then
            sValore = sValoreC
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " (agenda_appuntamenti.cognome LIKE '" & par.PulisciStrSql(sValore) & "%' OR CONVOCAZIONI_AU.COGNOME LIKE '" & par.PulisciStrSql(sValore) & "%') "
        End If

        If sValoreN <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreN
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " (agenda_appuntamenti.nome LIKE '%" & par.PulisciStrSql(sValore) & "' OR CONVOCAZIONI_AU.NOME LIKE '%" & par.PulisciStrSql(sValore) & "') "
        End If

        If sValoreDA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreDA

            sCompara = " >= "

            bTrovato = True
            sStringaSql = sStringaSql & " (SUBSTR(AGENDA_APPUNTAMENTI.INIZIO,1,8) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' or  CONVOCAZIONI_AU.DATA_APP " & sCompara & " '" & par.PulisciStrSql(sValore) & "') "
        End If

        If sValoreA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreA

            sCompara = " <= "

            bTrovato = True
            sStringaSql = sStringaSql & " (SUBSTR(AGENDA_APPUNTAMENTI.INIZIO,1,8) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' or CONVOCAZIONI_AU.DATA_APP " & sCompara & " '" & par.PulisciStrSql(sValore) & "') "
        End If


        If sValoreCO <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreCO
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreCON <> "" And IsNumeric(sValoreCON) Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreCON
            sCompara = " = "
            bTrovato = True
            sStringaSql = sStringaSql & " (AGENDA_APPUNTAMENTI.ID_CONVOCAZIONE " & sCompara & " " & CInt(sValore) & " or CONVOCAZIONI_AU.ID" & sCompara & " " & CInt(sValore) & ") "
        End If

        If sValoreIDF <> "TUTTE LE SEDI" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreIDF
            sCompara = " = "
            bTrovato = True
            'sStringaSql = sStringaSql & " CONVOCAZIONI_AU.ID_FILIALE" & sCompara & " " & CInt(sValore) & " "
            sStringaSql = sStringaSql & " (AGENDA_APPUNTAMENTI.ID_FILIALE" & sCompara & " " & CInt(sValore) & " or CONVOCAZIONI_AU.ID_FILIALE" & sCompara & " " & CInt(sValore) & ") "
        End If

        If sValoreIDO <> "TUTTI GLI SPORTELLI" And sValoreIDO <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreIDO
            sCompara = " = "
            bTrovato = True
            sStringaSql = sStringaSql & " (AGENDA_APPUNTAMENTI.ID_SPORTELLO" & sCompara & " " & CInt(sValore) & " OR CONVOCAZIONI_AU.ID_SPORTELLO" & sCompara & " " & CInt(sValore) & ") "
        End If

        If sValoreST <> "TUTTI" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreST
            sCompara = " = "
            bTrovato = True
            sStringaSql = sStringaSql & " CONVOCAZIONI_AU.ID_STATO" & sCompara & " " & CInt(sValore) & " "
        End If

        If sValoreM <> "--" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreM
            sCompara = " = "
            bTrovato = True
            sStringaSql = sStringaSql & " TAB_MOTIVO_ANNULLO_APP.ID" & sCompara & " " & CInt(sValore) & " "
        End If

        If sValoreAUSI <> "False" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreAUSI
            sCompara = " = "
            bTrovato = True
            sStringaSql = sStringaSql & " CONVOCAZIONI_AU.CARICO_AUSI=1 "
        End If

        

        'If Request.QueryString("X") = "1" And sValoreST = "1" Then
        '    If bTrovato = True Then sStringaSql = sStringaSql & " AND "
        '    bTrovato = True
        '    sStringaSql = sStringaSql & " CONVOCAZIONI_AU.ID_MOTIVO_ANNULLO=2 "
        'End If


        Dim ID_AU As String = ""
        Dim RU392 As String = ""
        Dim RU431 As String = ""

        par.OracleConn.Open()
        Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("SELECT MAX(ID) FROM UTENZA_BANDI", par.OracleConn)
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
        If myReader.Read Then
            ID_AU = myReader(0)
        End If
        myReader.Close()
        par.OracleConn.Close()

        If UCase(par.DeCriptaMolto(sValore392)) = "TRUE" Then
            RU392 = " RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC='EQC392' AND "
        End If
        If UCase(par.DeCriptaMolto(sValore431)) = "TRUE" Then
            RU431 = " RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC='L43198' AND "
        End If

        If sValoreOP <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreOP
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " upper(GetOperatoreAU(NVL((SELECT UTENZA_DICHIARAZIONI.ID FROM UTENZA_DICHIARAZIONI,SISCOM_MI.RAPPORTI_UTENZA WHERE UTENZA_DICHIARAZIONI.RAPPORTO=RAPPORTI_UTENZA.COD_CONTRATTO AND ID_BANDO=" & ID_AU & " AND RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO),0))) " & sCompara & " '" & par.PulisciStrSql(UCase(sValore)) & "' "
        End If

        If Session.Item("MOD_AU_CONV_VIS_TUTTO") = "1" Or Request.QueryString("X") = "1" Then
            sStringaSQL1 = "SELECT NVL((SELECT DECODE(ID_STATO,0,'DA COMPLETARE',1,'COMPLETA',2,'DA CANCELLARE') FROM UTENZA_DICHIARAZIONI,SISCOM_MI.RAPPORTI_UTENZA WHERE UTENZA_DICHIARAZIONI.RAPPORTO=RAPPORTI_UTENZA.COD_CONTRATTO AND ID_BANDO=" & ID_AU & " AND RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO),'DA INSERIRE') AS STATO_SCHEDA_AU,CONVOCAZIONI_AU.id,(CASE WHEN NVL(agenda_appuntamenti.cognome,'') <> '' THEN agenda_appuntamenti.cognome ELSE convocazioni_au.cognome END)||' '||(CASE WHEN NVL(agenda_appuntamenti.nome,'') <> '' THEN agenda_appuntamenti.nome ELSE convocazioni_au.nome END) AS NOMINATIVO,RAPPORTI_UTENZA.COD_CONTRATTO,TO_CHAR(TO_DATE(SUBSTR(AGENDA_APPUNTAMENTI.INIZIO,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS GIORNO_APP,SUBSTR(AGENDA_APPUNTAMENTI.INIZIO,9,2)||'.'||SUBSTR(AGENDA_APPUNTAMENTI.INIZIO,11,2) AS ORE_APP,TO_CHAR(CONVOCAZIONI_AU.ID,'0000000000') AS N_CONVOCAZIONE,TAB_FILIALI.NOME AS FILIALE,CONVOCAZIONI_AU_STATI.DESCRIZIONE AS STATOCONV,TAB_MOTIVO_ANNULLO_APP.descrizione as MOTIVO,TO_CHAR(TO_DATE(CONVOCAZIONI_AU.DATA_AUSI,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_AUSI,DECODE(CONVOCAZIONI_AU.CARICO_AUSI,'0','','1','SI') AS CARICO_AUSI,GetOperatoreAU(NVL((SELECT UTENZA_DICHIARAZIONI.ID FROM UTENZA_DICHIARAZIONI,SISCOM_MI.RAPPORTI_UTENZA WHERE UTENZA_DICHIARAZIONI.RAPPORTO=RAPPORTI_UTENZA.COD_CONTRATTO AND ID_BANDO=" & ID_AU & " AND RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO),0)) AS OPERATORE FROM SISCOM_MI.TAB_MOTIVO_ANNULLO_APP,SISCOM_MI.CONVOCAZIONI_AU_STATI,SISCOM_MI.TAB_FILIALI, SISCOM_MI.CONVOCAZIONI_AU,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.AGENDA_APPUNTAMENTI WHERE " & RU392 & " " & RU431 & " AGENDA_APPUNTAMENTI.ID_CONVOCAZIONE (+)=CONVOCAZIONI_AU.ID AND CONVOCAZIONI_AU.ID_MOTIVO_ANNULLO=TAB_MOTIVO_ANNULLO_APP.ID (+) AND CONVOCAZIONI_AU_STATI.ID=CONVOCAZIONI_AU.ID_STATO AND  RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO AND TAB_FILIALI.ID=CONVOCAZIONI_AU.ID_FILIALE AND CONVOCAZIONI_AU.ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & ID_AU & ")"
        Else
            sStringaSQL1 = "SELECT NVL((SELECT DECODE(ID_STATO,0,'DA COMPLETARE',1,'COMPLETA',2,'DA CANCELLARE') FROM UTENZA_DICHIARAZIONI,SISCOM_MI.RAPPORTI_UTENZA WHERE UTENZA_DICHIARAZIONI.RAPPORTO=RAPPORTI_UTENZA.COD_CONTRATTO AND ID_BANDO=" & ID_AU & " AND RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO),'DA INSERIRE') AS STATO_SCHEDA_AU,CONVOCAZIONI_AU.id,(CASE WHEN NVL(agenda_appuntamenti.cognome,'') <> '' THEN agenda_appuntamenti.cognome ELSE convocazioni_au.cognome END)||' '||(CASE WHEN NVL(agenda_appuntamenti.nome,'') <> '' THEN agenda_appuntamenti.nome ELSE convocazioni_au.nome END) AS NOMINATIVO,RAPPORTI_UTENZA.COD_CONTRATTO,TO_CHAR(TO_DATE(SUBSTR(AGENDA_APPUNTAMENTI.INIZIO,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS GIORNO_APP,SUBSTR(AGENDA_APPUNTAMENTI.INIZIO,9,2)||'.'||SUBSTR(AGENDA_APPUNTAMENTI.INIZIO,11,2) AS ORE_APP,TO_CHAR(CONVOCAZIONI_AU.ID,'0000000000') AS N_CONVOCAZIONE,TAB_FILIALI.NOME AS FILIALE,CONVOCAZIONI_AU_STATI.DESCRIZIONE AS STATOCONV,TAB_MOTIVO_ANNULLO_APP.descrizione as MOTIVO,TO_CHAR(TO_DATE(CONVOCAZIONI_AU.DATA_AUSI,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_AUSI,DECODE(CONVOCAZIONI_AU.CARICO_AUSI,'0','','1','SI') AS CARICO_AUSI,GetOperatoreAU(NVL((SELECT UTENZA_DICHIARAZIONI.ID FROM UTENZA_DICHIARAZIONI,SISCOM_MI.RAPPORTI_UTENZA WHERE UTENZA_DICHIARAZIONI.RAPPORTO=RAPPORTI_UTENZA.COD_CONTRATTO AND ID_BANDO=" & ID_AU & " AND RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO),0)) AS OPERATORE FROM SISCOM_MI.TAB_MOTIVO_ANNULLO_APP,SISCOM_MI.CONVOCAZIONI_AU_STATI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.CONVOCAZIONI_AU,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.AGENDA_APPUNTAMENTI WHERE " & RU392 & " " & RU431 & " AGENDA_APPUNTAMENTI.ID_CONVOCAZIONE (+)=CONVOCAZIONI_AU.ID AND  CONVOCAZIONI_AU.ID_MOTIVO_ANNULLO=TAB_MOTIVO_ANNULLO_APP.ID (+) AND CONVOCAZIONI_AU_STATI.ID=CONVOCAZIONI_AU.ID_STATO AND AGENDA_APPUNTAMENTI.id_filiale in (select id_UFFICIO from operatori where id=" & Session.Item("ID_OPERATORE") & ") and RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO AND TAB_FILIALI.ID=AGENDA_APPUNTAMENTI.ID_FILIALE AND CONVOCAZIONI_AU.ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & ID_AU & ")"
        End If


        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY NOMINATIVO ASC"

        BindGrid()
    End Function

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Private Sub BindGrid()
        par.OracleConn.Open()
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
        Dim ds As New Data.DataSet()
        da.Fill(ds, "AGENDA_APPUNTAMENTI,AGENDA_APPUNTAMENTI")
        da.Fill(dt)
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        HttpContext.Current.Session.Add("AA1", dt)

        Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or
   e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")

            'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('TextBox7').value='Hai selezionato : " & Replace(e.Item.Cells(1).Text, "'", "\'") & " " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('TextBox7').value='Hai selezionato : " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';")

        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaInquilino.aspx?T=" & Request.QueryString("X") & """</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If LBLID.Value = "-1" Or LBLID.Value = "" Then
            Response.Write("<script>alert('Nessun Appuntamento selezionato!')</script>")
        Else
            ' Response.Write("<script>location.replace='SchedaAppuntamento.aspx?ID=" & par.CriptaMolto(LBLID.Value) & "';</script>")
            scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "location.replace('SchedaAppuntamento.aspx?T=0&X=" & Request.QueryString("X") & "&ID=" & par.CriptaMolto(LBLID.Value) & "');" _
                            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript20")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript20", scriptblock)
            End If
        End If
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        ExportXLS()
    End Sub
End Class
