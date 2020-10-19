Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class Contratti_RisultatiRestDep
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String
    Dim scriptblock As String

    Dim sCDC As String = ""
    Dim sDCPD As String = ""
    Dim sDCPA As String = ""
    Dim sIDPD As String = ""
    Dim sIDPA As String = ""
    Dim sNCDP As String = ""
    Dim sACDP As String = ""
    Dim sNMAN As String = ""
    Dim sAMAN As String = ""
    Dim sDMAND As String = ""
    Dim sDMANA As String = ""


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
         If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then

            sCDC = Request.QueryString("CDC")
            sDCPD = Request.QueryString("DCPD")
            sDCPA = Request.QueryString("DCPA")
            sIDPD = Request.QueryString("IDPD")
            sIDPA = Request.QueryString("IDPA")
            sNCDP = Request.QueryString("NCDP")
            sACDP = Request.QueryString("ACDP")
            sNMAN = Request.QueryString("NMAN")
            sAMAN = Request.QueryString("AMAN")
            sDMAND = Request.QueryString("DMAND")
            sDMANA = Request.QueryString("DMANA")

            Response.Flush()
            LBLID.Value = "-1"
            Cerca()
        End If
    End Sub

    Private Function Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String

        bTrovato = False
        sStringaSql = ""


        bTrovato = False
        sStringaSql = ""

        If sCDC <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sCDC
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sDCPD <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sDCPD
            bTrovato = True
            sStringaSql = sStringaSql & " DATA_CERT_PAG>='" & sValore & "' "
        End If
        If sDCPA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sDCPA
            bTrovato = True
            sStringaSql = sStringaSql & " DATA_CERT_PAG<='" & sValore & "' "
        End If

        If sDMAND <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sDMAND
            bTrovato = True
            sStringaSql = sStringaSql & " DATA_MANDATO>='" & sValore & "' "
        End If
        If sDMANA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sDMANA
            bTrovato = True
            sStringaSql = sStringaSql & " DATA_MANDATO<='" & sValore & "' "
        End If

        If sNMAN <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sNMAN
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " NUM_MANDATO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sAMAN <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sAMAN
            sCompara = " = "
            bTrovato = True
            sStringaSql = sStringaSql & " ANNO_MANDATO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sNCDP <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sNCDP
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " NUM_CDP " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sACDP <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sACDP
            sCompara = " = "
            bTrovato = True
            sStringaSql = sStringaSql & " ANNO_CDP " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sIDPD <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sIDPD
            bTrovato = True
            sStringaSql = sStringaSql & " CREDITO>=" & par.VirgoleInPunti(sValore) & " "
        End If
        If sIDPA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sIDPA
            bTrovato = True
            sStringaSql = sStringaSql & " CREDITO<=" & par.VirgoleInPunti(sValore) & " "
        End If


        sStringaSQL1 = "SELECT RAPPORTI_UTENZA.ID,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''Contratto.aspx?ID='||RAPPORTI_UTENZA.ID||'&COD='||RAPPORTI_UTENZA.COD_CONTRATTO||''',''Dettagli'',''height=780,width=1160'');£>'||RAPPORTI_UTENZA.COD_CONTRATTO||'</a>','$','&'),'£','" & Chr(34) & "') as  cod_contratto ,(CASE WHEN ANAGRAFICA.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END) AS INTESTATARIO,RAPPORTI_UTENZA_DEP_CAUZ.*,TO_CHAR(TO_DATE(DATA_CERT_PAG,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_CERT_PAG_1,TO_CHAR(TO_DATE(DATA_MANDATO,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_MANDATO_1 FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.RAPPORTI_UTENZA_DEP_CAUZ,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND RAPPORTI_UTENZA.ID=RAPPORTI_UTENZA_DEP_CAUZ.ID_CONTRATTO and RAPPORTI_UTENZA_DEP_CAUZ.num_mandato is not null "


        If sStringaSql <> "" Then
            If Left(sStringaSql, 4) = " AND" Then
                sStringaSql = Replace(sStringaSql, "AND", " ")
            End If
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If

        sStringaSQL1 = sStringaSQL1 & " ORDER BY ""INTESTATARIO"" ASC"

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
        Try

            Dim miocolore As String = "#99CCFF"

            Dim dt As New Data.DataTable
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

            da.Fill(dt)

            Label4.Text = DataGrid1.Items.Count
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()

            Label4.Text = dt.Rows.Count

        Catch ex As Exception
            ' par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza: Risultati Ricerca Dom. Gest.Locatari - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub



    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        If H1.Value = "1" Then
            ExportXLS_Chiama100()
            H1.Value = "0"
        End If
    End Sub

    Private Function ExportXLS_Chiama100()

        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String = ""
        Dim row As System.Data.DataRow
        Dim dt As New Data.DataTable
        Dim par As New CM.Global

        Dim FileCSV As String = ""

        Try
            par.OracleConn.Open()
            FileCSV = "Estrazione" & Format(Now, "yyyyMMddHHmmss")

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter

            da = New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            da.Fill(dt)

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
                    .SetColumnWidth(1, 1, 30)
                    .SetColumnWidth(2, 2, 35)
                    .SetColumnWidth(3, 3, 20)
                    .SetColumnWidth(4, 4, 40)
                    .SetColumnWidth(5, 5, 70)
                    .SetColumnWidth(6, 6, 30)
                    .SetColumnWidth(7, 7, 20)
                    .SetColumnWidth(8, 8, 25)
                    .SetColumnWidth(9, 9, 25)
                    .SetColumnWidth(10, 10, 20)
                    .SetColumnWidth(11, 11, 20)
                    .SetColumnWidth(12, 12, 25)
                    .SetColumnWidth(13, 13, 25)
                    .SetColumnWidth(14, 14, 20)
                    .SetColumnWidth(15, 15, 20)
                    .SetColumnWidth(16, 16, 30)
                    .SetColumnWidth(17, 17, 20)
                    .SetColumnWidth(18, 18, 20)
                    .SetColumnWidth(19, 19, 25)


                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "INTESTATARIO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "COD.CONTRATTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "DATA CERT.PAGAMENTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "NUMERO CDP", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "ANNO CDP", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "NUM.MANDATO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "DATA MANDATO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "ANNO MANDATO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "IMPORTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "INTERESSI", 12)

                    K = 2
                    For Each row In dt.Rows
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTESTATARIO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_CERT_PAG_1"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NUM_CDP"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ANNO_CDP"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NUM_MANDATO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_MANDATO_1"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ANNO_MANDATO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CREDITO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTERESSI"), "")))


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
            Data.OracleClient.OracleConnection.ClearAllPools()


            ' Response.Write("<script>window.open('../FileTemp/" & FileCSV & ".zip','Estrazione','');</script>")
            Response.Redirect("..\FileTemp\" & FileCSV & ".zip")

        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try




    End Function

End Class
