Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Condomini_RisultatiIndirizzo
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property QUERY() As String
        Get
            If Not (ViewState("par_QUERY") Is Nothing) Then
                Return CStr(ViewState("par_QUERY"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_QUERY") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        If Not IsPostBack Then
            '********BISOGNA SEMPRE METTERLO NEL POSTBACK!
            '********SE FUORI EVENTUALI METODI CHE USANO LA RESP.WRITE SI IMBALLANO PERCHè LUI LE PULISCE TUTTE!
            Response.Flush()
            Me.rdbGestione.SelectedValue = "T"
            'vIndirizzo = Request.QueryString("I")
            'vCivico = Request.QueryString("Civ")
            Cerca()
            'If Request.QueryString("P") <> "" Then
            '    cambiataPagina(Request.QueryString("P"))
            '    'Request.QueryString("P") = ""
            'End If
        End If
    End Sub
    Private Sub Cerca()
        Try
            Dim bTrovato As Boolean
            bTrovato = False
            If rdbGestione.SelectedValue = "T" Then
                QUERY = "SELECT  COMUNI_NAZIONI.NOME AS CITTA, TO_CHAR(CONDOMINI.ID,'00000') AS COD_CONDOMINIO,COND_AMMINISTRATORI.ID AS ID_AMM , (COND_AMMINISTRATORI.COGNOME ||' '|| COND_AMMINISTRATORI.NOME )AS AMMINIST, CONDOMINI.ID, CONDOMINI.DENOMINAZIONE AS CONDOMINIO,  (CASE WHEN TIPO_GESTIONE = 'D' THEN 'DIRETTA' WHEN TIPO_GESTIONE = 'I' THEN 'INDIRETTA' ELSE ''END )AS GESTIONE,  " _
                      & "MIL_PRO_TOT_COND,MIL_COMPRO_TOT_COND,MIL_SUP_TOT_COND,MIL_GEST_TOT_COND " _
                      & "FROM SISCOM_MI.CONDOMINI,SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE,SEPA.COMUNI_NAZIONI " _
                      & "WHERE COMUNI_NAZIONI.COD(+) = CONDOMINI.COD_COMUNE AND COND_AMMINISTRATORI.ID(+) = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE " _
                      & "AND COND_AMMINISTRAZIONE.ID_CONDOMINIO(+) = CONDOMINI.ID AND COND_AMMINISTRAZIONE.DATA_FINE IS NULL "
            ElseIf rdbGestione.SelectedValue = "D" Then
                QUERY = "SELECT  COMUNI_NAZIONI.NOME AS CITTA, TO_CHAR(CONDOMINI.ID,'00000') AS COD_CONDOMINIO,COND_AMMINISTRATORI.ID AS ID_AMM , (COND_AMMINISTRATORI.COGNOME ||' '|| COND_AMMINISTRATORI.NOME )AS AMMINIST, CONDOMINI.ID, CONDOMINI.DENOMINAZIONE AS CONDOMINIO,(CASE WHEN TIPO_GESTIONE = 'D' THEN 'DIRETTA' WHEN TIPO_GESTIONE = 'I' THEN 'INDIRETTA' ELSE ''END )AS GESTIONE, " _
                      & "MIL_PRO_TOT_COND,MIL_COMPRO_TOT_COND,MIL_SUP_TOT_COND,MIL_GEST_TOT_COND " _
                      & "FROM SISCOM_MI.CONDOMINI,SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE,SEPA.COMUNI_NAZIONI " _
                      & "WHERE TIPO_GESTIONE = 'D' AND COMUNI_NAZIONI.COD(+) = CONDOMINI.COD_COMUNE AND COND_AMMINISTRATORI.ID(+) = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE AND COND_AMMINISTRAZIONE.ID_CONDOMINIO(+) = CONDOMINI.ID AND COND_AMMINISTRAZIONE.DATA_FINE IS NULL "
            ElseIf rdbGestione.SelectedValue = "I" Then
                QUERY = "SELECT  COMUNI_NAZIONI.NOME AS CITTA, TO_CHAR(CONDOMINI.ID,'00000') AS COD_CONDOMINIO,COND_AMMINISTRATORI.ID AS ID_AMM , (COND_AMMINISTRATORI.COGNOME ||' '|| COND_AMMINISTRATORI.NOME )AS AMMINIST, CONDOMINI.ID, CONDOMINI.DENOMINAZIONE AS CONDOMINIO,(CASE WHEN TIPO_GESTIONE = 'D' THEN 'DIRETTA' WHEN TIPO_GESTIONE = 'I' THEN 'INDIRETTA' ELSE ''END )AS GESTIONE, " _
                      & "MIL_PRO_TOT_COND,MIL_COMPRO_TOT_COND,MIL_SUP_TOT_COND,MIL_GEST_TOT_COND " _
                      & "FROM SISCOM_MI.CONDOMINI,SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE,SEPA.COMUNI_NAZIONI " _
                      & "WHERE TIPO_GESTIONE = 'I' AND COMUNI_NAZIONI.COD(+) = CONDOMINI.COD_COMUNE AND COND_AMMINISTRATORI.ID(+) = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE AND COND_AMMINISTRAZIONE.ID_CONDOMINIO(+) = CONDOMINI.ID AND COND_AMMINISTRAZIONE.DATA_FINE IS NULL "
            End If
            'If Not String.IsNullOrEmpty(QueryEdifici) Then
            '    QUERY = QueryEdifici

            'ElseIf Not String.IsNullOrEmpty(QueryComplessi) Then
            '    QUERY = QueryComplessi
            'Else
            '    QUERY = "SELECT  COND_AMMINISTRATORI.ID AS ID_AMM , (COND_AMMINISTRATORI.COGNOME || COND_AMMINISTRATORI.NOME )AS AMMINIST, CONDOMINI.ID, CONDOMINI.DENOMINAZIONE AS CONDOMINIO, INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO , COMPLESSI_IMMOBILIARI.DENOMINAZIONE  AS COMP_EDIF FROM SISCOM_MI.CONDOMINI, SISCOM_MI.INDIRIZZI, SISCOM_MI.COMPLESSI_IMMOBILIARI , SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE WHERE CONDOMINI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID (+) AND  INDIRIZZI.ID(+) = COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO AND COND_AMMINISTRATORI.ID = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE AND COND_AMMINISTRAZIONE.ID_CONDOMINIO = CONDOMINI.ID AND COND_AMMINISTRAZIONE.DATA_FINE IS NULL AND CONDOMINI.ID_EDIFICIO IS NULL UNION SELECT  COND_AMMINISTRATORI.ID AS ID_AMM , (COND_AMMINISTRATORI.COGNOME || COND_AMMINISTRATORI.NOME )AS AMMINIST, CONDOMINI.ID, CONDOMINI.DENOMINAZIONE AS CONDOMINIO, INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO , EDIFICI.DENOMINAZIONE  AS COMP_EDIF FROM SISCOM_MI.CONDOMINI, SISCOM_MI.INDIRIZZI, SISCOM_MI.EDIFICI , SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE WHERE CONDOMINI.ID_EDIFICIO = EDIFICI.ID (+) AND  INDIRIZZI.ID(+) = EDIFICI.ID_INDIRIZZO_PRINCIPALE AND COND_AMMINISTRATORI.ID = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE AND COND_AMMINISTRAZIONE.ID_CONDOMINIO = CONDOMINI.ID AND COND_AMMINISTRAZIONE.DATA_FINE IS NULL AND CONDOMINI.ID_COMPLESSO IS NULL "
            'End If
            'If vIndirizzo <> "-1" AndAlso par.IfEmpty(vIndirizzo, "Null") <> "Null" Then
            '    sValore = vIndirizzo
            '    condizione = condizione & "AND INDIRIZZI.DESCRIZIONE = '" & sValore & "'"
            '    If Not String.IsNullOrEmpty(vCivico) Then
            '        sValore = vCivico
            '        condizione = condizione & " AND SISCOM_MI.INDIRIZZI.CIVICO = '" & sValore & "'"
            '    End If
            'End If
            QUERY = QUERY & " ORDER BY CONDOMINIO ASC"
            BindGrid()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub BindGrid()
        Try

            par.OracleConn.Open()
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(QUERY, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            'Dim ds As New Data.DataSet()
            'da.Fill(ds, "CONDOMINI, INDIRIZZI,COMPLESSI_EDIFICI")
            DataGridCondom.DataSource = dt
            DataGridCondom.DataBind()
            LnlNumeroRisultati.Text = "  - " & DataGridCondom.Items.Count & " "
            '*********************CHIUSURA CONNESSIONE**********************
            'Session.Add("dtExport", ds.Tables(0))
            da.Dispose()
            'ds.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Protected Sub DataGridCondom_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridCondom.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la il Condominio: " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
            e.Item.Attributes.Add("onDblclick", "ApriCondominio();")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Condominio " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
            e.Item.Attributes.Add("onDblclick", "ApriCondominio();")

        End If
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        'Session.Remove("dtExport")
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        BindGrid()
        Esporta()
        'Try
        '    Dim myExcelFile As New CM.ExcelFile
        '    Dim i As Long
        '    Dim K As Long
        '    Dim sNomeFile As String
        '    Dim row As System.Data.DataRow
        '    Dim dt As New Data.DataTable
        '    dt = CType(HttpContext.Current.Session.Item("dtExport"), Data.DataTable)
        '    sNomeFile = "ExpElencoCondomini_" & Format(Now, "yyyyMMddHHmmss")
        '    i = 0
        '    With myExcelFile
        '        .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
        '        .PrintGridLines = False
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
        '        .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
        '        .SetDefaultRowHeight(14)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
        '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
        '        .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "CODICE", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "CONDOMINIO", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "CITTA", 12)
        '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "AMMINISTRATORE", 12)
        '        '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "MILL.PROP.", 12)
        '        '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "MILL.COMPROP.", 12)
        '        '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "MILL.SUP.", 12)
        '        '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "MILL.GEST.", 12)
        '        K = 2
        '        For Each row In dt.Rows
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("COD_CONDOMINIO"))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt.Rows(i).Item("CONDOMINIO"), ""))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt.Rows(i).Item("CITTA"), ""))
        '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(dt.Rows(i).Item("AMMINIST"), ""))
        '            '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.IfNull(dt.Rows(i).Item("MIL_PRO_TOT_COND"), ""))
        '            '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfNull(dt.Rows(i).Item("MIL_COMPRO_TOT_COND"), ""))
        '            '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(dt.Rows(i).Item("MIL_SUP_TOT_COND"), ""))
        '            '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(dt.Rows(i).Item("MIL_GEST_TOT_COND"), ""))
        '            i = i + 1
        '            K = K + 1
        '        Next
        '        .CloseFile()
        '    End With
        '    'Dim objCrc32 As New Crc32()
        '    'Dim strmZipOutputStream As ZipOutputStream
        '    'Dim zipfic As String
        '    'zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")
        '    'strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        '    'strmZipOutputStream.SetLevel(6)
        '    'Dim strFile As String
        '    'strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
        '    'Dim strmFile As FileStream = File.OpenRead(strFile)
        '    'Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '    'strmFile.Read(abyBuffer, 0, abyBuffer.Length)
        '    'Dim sFile As String = Path.GetFileName(strFile)
        '    'Dim theEntry As ZipEntry = New ZipEntry(sFile)
        '    'Dim fi As New FileInfo(strFile)
        '    'theEntry.DateTime = fi.LastWriteTime
        '    'theEntry.Size = strmFile.Length
        '    'strmFile.Close()
        '    'objCrc32.Reset()
        '    'objCrc32.Update(abyBuffer)
        '    'theEntry.Crc = objCrc32.Value
        '    'strmZipOutputStream.PutNextEntry(theEntry)
        '    'strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        '    'strmZipOutputStream.Finish()
        '    'strmZipOutputStream.Close()
        '    'File.Delete(strFile)
        '    Response.Redirect("..\FileTemp\" & sNomeFile & ".xls")
        '    'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        'Catch ex As Exception
        '    Me.LblErrore.Visible = True
        '    LblErrore.Text = ex.Message
        'End Try
    End Sub
    Protected Sub rdbGestione_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbGestione.SelectedIndexChanged
        Cerca()
    End Sub
    Private Sub Esporta()
        Try
            If DataGridCondom.Items.Count > 0 Then
                Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridCondom, "ExportAnCondomini", 90 / 100, , , False)
                If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                    Response.Redirect("..\/FileTemp\/" & nomefile, False)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun dato da esportare!');</script>")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
End Class