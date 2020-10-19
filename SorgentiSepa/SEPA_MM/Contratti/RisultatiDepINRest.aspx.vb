Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class Contratti_RisultatiDepINRest
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Dim DT As New Data.DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
           & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
           & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
           & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
           & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
           & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
           & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
           & "</td></tr></table></div></div>"
        Response.Write(Loading)

        If Not IsPostBack Then
            Response.Flush()
            CaricaDati()
        End If
        txtDataCert.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataMan.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

    End Sub

    Public Property Tabella() As String
        Get
            If Not (ViewState("par_Tabella") Is Nothing) Then
                Return CStr(ViewState("par_Tabella"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tabella") = value
        End Set
    End Property


    Private Function CaricaDati()
        Try
            Dim S As String = ""
            Dim S1 As String = ""

            Dim S2 As String = ""
            Dim ss As String = "("

            Dim trovato As Boolean = False

            Dim sCDC As String = Request.QueryString("CDC")
            Dim sDCPD As String = Request.QueryString("DCPD")
            Dim sDCPA As String = Request.QueryString("DCPA")
            Dim sTP As String = Request.QueryString("TP")
            Dim sST As String = Request.QueryString("ST")

            If sCDC <> "" Then
                If trovato = True Then S = S & " AND "
                trovato = True
                S = S & " RAPPORTI_UTENZA.COD_CONTRATTO='" & UCase(sCDC) & "' "
            End If

            If sDCPD <> "" Then
                If trovato = True Then S = S & " AND "
                trovato = True
                S = S & " RAPPORTI_UTENZA_DEP_CAUZ.DATA_OPERAZIONE>='" & UCase(sDCPD) & "' "
            End If

            If sDCPA <> "" Then
                If trovato = True Then S = S & " AND "
                trovato = True
                S = S & " RAPPORTI_UTENZA_DEP_CAUZ.DATA_OPERAZIONE='" & UCase(sDCPA) & "' "
            End If

            If sTP <> "" Then
                If trovato = True Then S = S & " AND "
                trovato = True
                S = S & " RAPPORTI_UTENZA_DEP_CAUZ.TIPO_PAGAMENTO IN (" & UCase(sTP) & ") "
            End If

            If sST <> "-1" Then
                If trovato = True Then S = S & " AND "
                trovato = True
                S = S & " TAB_FILIALI.ID= " & UCase(sST)
            End If

            If S <> "" Then S = S & " And "

            Tabella = " SELECT RAPPORTI_UTENZA.ID AS IDC,RAPPORTI_UTENZA_DEP_CAUZ.NUM_CDP,RAPPORTI_UTENZA_DEP_CAUZ.ANNO_CDP,RAPPORTI_UTENZA_DEP_CAUZ.NUM_MANDATO,RAPPORTI_UTENZA_DEP_CAUZ.ANNO_MANDATO,TO_CHAR(TO_DATE(RAPPORTI_UTENZA_DEP_CAUZ.DATA_MANDATO,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_MANDATO,TO_CHAR(TO_DATE(RAPPORTI_UTENZA_DEP_CAUZ.DATA_CERT_PAG,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_CERT_PAG,RAPPORTI_UTENZA.COD_CONTRATTO,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC, " _
                    & " CASE WHEN ANAGRAFICA.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS INTESTATARIO, " _
                    & " TO_CHAR(TO_DATE(RAPPORTI_UTENZA_DEP_CAUZ.DATA_OPERAZIONE,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_OPERAZIONE, " _
                    & " RAPPORTI_UTENZA_DEP_CAUZ.CREDITO,RAPPORTI_UTENZA_DEP_CAUZ.INTERESSI,RAPPORTI_UTENZA_DEP_CAUZ.NOTE_PAGAMENTO,TAB_MOD_RESTITUZIONE.DESCRIZIONE AS TIPO_PAGAMENTO,TAB_FILIALI.NOME AS STRUTTURA,RAPPORTI_UTENZA_DEP_CAUZ.ID_BOLLETTA " _
                    & " FROM SISCOM_MI.TAB_FILIALI,SISCOM_MI.FILIALI_UI,SISCOM_MI.TAB_MOD_RESTITUZIONE,SISCOM_MI.RAPPORTI_UTENZA_DEP_CAUZ,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_CONTRATTUALE " _
                    & " WHERE  " & S _
                    & " RAPPORTI_UTENZA_DEP_CAUZ.num_mandato is null and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND FILIALI_UI.ID_UI (+)=UNITA_CONTRATTUALE.ID_UNITA AND " _
                    & " RAPPORTI_UTENZA_DEP_CAUZ.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA " _
                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                    & " AND TAB_MOD_RESTITUZIONE.ID=RAPPORTI_UTENZA_DEP_CAUZ.TIPO_PAGAMENTO AND TAB_FILIALI.ID=FILIALI_UI.ID_FILIALE and FILIALI_UI.INIZIO_VALIDITA=(SELECT MAX(INIZIO_VALIDITA) FROM SISCOM_MI.FILIALI_UI WHERE ID_UI=unita_contrattuale.id_unita) "

            Tabella = Tabella & " ORDER BY INTESTATARIO ASC "
            BindGrid()
        Catch ex As Exception
            'Beep()
        End Try
    End Function

    Private Function BindGrid()
        'Dim da As Data.OracleClient.OracleDataAdapter
        PAR.cmd.CommandText = Tabella
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd.CommandText, PAR.OracleConn)
        da.Fill(DT)

        DataGridRateEmesse.DataSource = DT
        DataGridRateEmesse.DataBind()
        Session.Add("MIADT", DT)

        Label2.Text = DataGridRateEmesse.Items.Count & " nella pagina - Totale :" & DT.Rows.Count
    End Function

    Protected Sub imgSelezionaTutto_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgSelezionaTutto.Click
        Try
            Dim DT1 As New Data.DataTable
            DT1 = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)

            For Each riga As DataGridItem In DataGridRateEmesse.Items

                If CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = False Then
                    CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = True
                End If
            Next
            Session.Item("MIADT") = DT1

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Nuovogruppoconv_1 - " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub

    Protected Sub imgDeselezionaTutto_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgDeselezionaTutto.Click
        Try
            Dim DT1 As New Data.DataTable
            DT1 = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)

            For Each riga As DataGridItem In DataGridRateEmesse.Items

                If CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = True Then
                    CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = False
                End If

            Next
            Session.Item("MIADT") = DT1
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Nuovogruppoconv_1 - " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Try
            Dim Trovato As Boolean = False
            Dim DT1 As New Data.DataTable
            DT1 = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)

            If txtDataCert.Text = "" Or txtNumCDP.Text = "" Or txtAnnoCDP.Text = "" Or txtDataMan.Text = "" Or txtNumMandato.Text = "" Or txtAnnoMandato.Text = "" Then
                Response.Write("<script>alert('Attenzione...tutti i campi devono essere valorizzati!');</script>")
                Exit Sub
            End If

            If Len(txtAnnoCDP.Text) <> 4 Or Val(txtAnnoCDP.Text) = 0 Then
                Response.Write("<script>alert('Attenzione...Il campo Anno CDP e anno MANDATO devono essere composti da di 4 numeri (aaaa)!');</script>")
                Exit Sub
            End If

            If Len(txtAnnoMandato.Text) <> 4 Or Val(txtAnnoMandato.Text) = 0 Then
                Response.Write("<script>alert('Attenzione...Il campo Anno CDP e anno MANDATO devono essere composti da di 4 numeri (aaaa)!');</script>")
                Exit Sub
            End If

            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                PAR.SettaCommand(PAR)
            End If
            PAR.myTrans = PAR.OracleConn.BeginTransaction()

            Dim I As Long = 0

            For Each riga As DataGridItem In DataGridRateEmesse.Items
                If CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = True Then
                    Trovato = True
                    PAR.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA_DEP_CAUZ SET DATA_CERT_PAG='" & PAR.AggiustaData(txtDataCert.Text) & "',NUM_CDP='" & PAR.PulisciStrSql(txtNumCDP.Text) & "',ANNO_CDP=" & PAR.PulisciStrSql(txtAnnoCDP.Text) & ",DATA_MANDATO='" & PAR.AggiustaData(txtDataMan.Text) & "',NUM_MANDATO='" & txtNumMandato.Text & "',ANNO_MANDATO=" & txtAnnoMandato.Text & "  WHERE ID_CONTRATTO=" & PAR.PulisciStrSql(PAR.IfNull(DT1.Rows(I).Item("IDC"), "-1"))
                    PAR.cmd.ExecuteNonQuery()

                    'verifico e imposto la data di pagamento
                    PAR.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO=IMPORTO WHERE ID_BOLLETTA=" & PAR.IfNull(DT1.Rows(I).Item("ID_BOLLETTA"), "-1")
                    PAR.cmd.ExecuteNonQuery()

                    PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & PAR.IfNull(DT1.Rows(I).Item("ID_BOLLETTA"), "-1")
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    Do While myReaderA.Read
                        PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO) VALUES " _
                                            & "(" & PAR.IfNull(myReaderA("ID"), -1) & ",'" & PAR.AggiustaData(txtDataMan.Text) & "'," & PAR.VirgoleInPunti(myReaderA("IMPORTO")) & ",2)"
                        PAR.cmd.ExecuteNonQuery()
                    Loop
                    myReaderA.Close()

                  

                End If
                I = I + 1
            Next

            If Trovato = True Then
                Response.Write("<script>alert('Operazione effettuata.');</script>")
            Else
                Response.Write("<script>alert('Attenzione...Non è stato selezionato nessun elemento dalla lista');</script>")
            End If
            PAR.myTrans.Commit()
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Data.OracleClient.OracleConnection.ClearAllPools()
            BindGrid()

        Catch ex As Exception
            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Data.OracleClient.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:Inserimento dati DEp.Cauzionale - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COD. CONTRATTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "TIPO CONTRATTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "INTESTATARIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "DATA OPERAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "CREDITO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "INTERESSI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "TIPO PAGAMENTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "NOTE.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "STRUTTURA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "DATA CERT.PAGAMENTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "NUM. CDP", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "ANNO CDP", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "DATA MANDATO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "NUM MANDATO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "ANNO MANDATO", 12)

                K = 2
                For Each row In DT.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("COD_CONTRATTO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("COD_TIPOLOGIA_CONTR_LOC"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("INTESTATARIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("DATA_OPERAZIONE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("CREDITO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("INTERESSI"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("TIPO_PAGAMENTO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NOTE_PAGAMENTO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("STRUTTURA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("DATA_CERT_PAG"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NUM_CDP"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("ANNO_CDP"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("DATA_MANDATO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NUM_MANDATO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("ANNO_MANDATO"), "")))

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


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
