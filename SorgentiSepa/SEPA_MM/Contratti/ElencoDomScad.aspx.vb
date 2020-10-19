Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class Contratti_ElencoDomScad
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then
            Response.Flush()
            CaricaListe()
            BindGrid()
        End If

    End Sub

    Private Sub CaricaListe()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.caricaComboBox("select * from t_motivo_domanda_vsa order by descrizione asc", cmbTipodomanda, "ID", "DESCRIZIONE", True)
            cmbTipodomanda.SelectedIndex = -1
            cmbTipodomanda.Items.FindByValue("-1").Selected = True


            Dim SS As String = "select " _
            & "OPERATORI.OPERATORE,OPERATORI.ID " _
            & " " _
            & "from domande_bando_vsa,dichiarazioni_vsa,comp_nucleo_vsa,domande_vsa_alloggio,t_motivo_domanda_vsa,eventi_bandi_vsa,operatori " _
            & "where domande_bando_vsa.id_dichiarazione = dichiarazioni_vsa.id and comp_nucleo_vsa.id_dichiarazione = dichiarazioni_vsa.id " _
            & "and comp_nucleo_vsa.progr = 0 and domande_vsa_alloggio.id_domanda = domande_bando_vsa.id and eventi_bandi_vsa.id_domanda = domande_bando_vsa.id " _
            & "and t_motivo_domanda_vsa.id = domande_bando_vsa.id_motivo_domanda and fl_autorizzazione = 0 and dichiarazioni_vsa.id_stato<>2 and eventi_bandi_vsa.cod_evento = 'F190' and eventi_bandi_vsa.id_operatore=operatori.id(+) " _
            & "and ((TO_DATE(DATA_PRESENTAZIONE,'yyyymmdd') + (SELECT TEMPO_GG FROM TEMPI_PROCESSI_VSA WHERE ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID)) - TO_DATE(SYSDATE) >= 0)" _
            & "and ((TO_DATE(DATA_PRESENTAZIONE,'yyyymmdd') + (SELECT TEMPO_GG FROM TEMPI_PROCESSI_VSA WHERE ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID)) - TO_DATE(SYSDATE) <= 10) " _
            & "ORDER BY OPERATORE ASC"

            par.caricaComboBox(SS, cmbOperatore, "ID", "OPERATORE", True)
            cmbOperatore.SelectedIndex = -1
            cmbOperatore.Items.FindByValue("-1").Selected = True

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try


    End Sub

    Public Property strSQL() As String
        Get
            If Not (ViewState("par_strSQL") Is Nothing) Then
                Return CStr(ViewState("par_strSQL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_strSQL") = value
        End Set

    End Property

    Public Property strSQLOperatore() As String
        Get
            If Not (ViewState("par_strSQLOperatore") Is Nothing) Then
                Return CStr(ViewState("par_strSQLOperatore"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_strSQLOperatore") = value
        End Set

    End Property

    Public Property strSQLDomanda() As String
        Get
            If Not (ViewState("par_strSQLDomanda") Is Nothing) Then
                Return CStr(ViewState("par_strSQLDomanda"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_strSQLDomanda") = value
        End Set

    End Property

    Private Sub BindGrid()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            'Dim strSQL As String = ""
            Dim conta As Integer = 0

            Dim dtDomande As New Data.DataTable

            strSQL = "select t_motivo_domanda_vsa.id,comp_nucleo_vsa.COGNOME||' '||comp_nucleo_vsa.NOME AS RICH,INDIRIZZO||', '||CIVICO AS INDIRIZZO,CONTRATTO_NUM,t_motivo_domanda_vsa.descrizione as tipo_dom," _
            & "TO_CHAR(TO_DATE(DATA_PRESENTAZIONE,'yyyymmdd') + (SELECT TEMPO_GG FROM TEMPI_PROCESSI_VSA WHERE ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID),'dd/mm/yyyy') AS DATASCAD,OPERATORI.OPERATORE," _
            & "TO_CHAR(TO_DATE(DATA_PRESENTAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATAPRES,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../VSA/NuovaDomandaVSA/domandaNuova.aspx?CH=1$ID='||DOMANDE_BANDO_VSA.ID||''',''Domande'','''');£>'||DOMANDE_BANDO_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as PGDOM," _
            & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../VSA/DichAUnuova.aspx?CH=2$ID='||DICHIARAZIONI_VSA.ID||''',''Dichiarazione'',''height=550,top=200,left=350,width=670'');£>'||DICHIARAZIONI_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as pgdich " _
            & "from domande_bando_vsa,dichiarazioni_vsa,comp_nucleo_vsa,domande_vsa_alloggio,t_motivo_domanda_vsa,eventi_bandi_vsa,operatori " _
            & "where domande_bando_vsa.id_dichiarazione = dichiarazioni_vsa.id and comp_nucleo_vsa.id_dichiarazione = dichiarazioni_vsa.id " _
            & "and comp_nucleo_vsa.progr = 0 and domande_vsa_alloggio.id_domanda = domande_bando_vsa.id and eventi_bandi_vsa.id_domanda = domande_bando_vsa.id " _
            & "and t_motivo_domanda_vsa.id = domande_bando_vsa.id_motivo_domanda and fl_autorizzazione = 0 and dichiarazioni_vsa.id_stato<>2 and eventi_bandi_vsa.cod_evento = 'F190' and eventi_bandi_vsa.id_operatore=operatori.id(+) " _
            & "and ((TO_DATE(DATA_PRESENTAZIONE,'yyyymmdd') + (SELECT TEMPO_GG FROM TEMPI_PROCESSI_VSA WHERE ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID)) - TO_DATE(SYSDATE) >= 0)" _
            & "and ((TO_DATE(DATA_PRESENTAZIONE,'yyyymmdd') + (SELECT TEMPO_GG FROM TEMPI_PROCESSI_VSA WHERE ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID)) - TO_DATE(SYSDATE) <= 10) " _
            & strSQLDomanda & " " & strSQLOperatore _
            & " ORDER BY T_MOTIVO_DOMANDA_VSA.DESCRIZIONE,DATA_PRESENTAZIONE ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(strSQL, par.OracleConn)
            da.Fill(dtDomande)
            DataGrid1.DataSource = dtDomande
            conta = dtDomande.Rows.Count
            DataGrid1.DataBind()

            Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & conta

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white';}")
            'e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';")
        End If

        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
            'e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';")
        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        If H1.Value = "1" Then
            ExportXLS_Chiama100()
            H1.Value = "0"
        End If
    End Sub

    Private Sub ExportXLS_Chiama100()
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
            FileCSV = "Estraz_Domande" & Format(Now, "yyyyMMddHHmmss")

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter

            da = New Oracle.DataAccess.Client.OracleDataAdapter(strSQL, par.OracleConn)
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
                    .SetColumnWidth(3, 3, 25)
                    .SetColumnWidth(4, 4, 35)
                    .SetColumnWidth(5, 5, 25)
                    .SetColumnWidth(6, 6, 30)
                    .SetColumnWidth(7, 7, 20)
                    .SetColumnWidth(8, 8, 25)
                    .SetColumnWidth(9, 9, 25)
                    

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "RICHIEDENTE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INDIRIZZO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "COD.RAPPORTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "TIPO DOMANDA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "DATA PRESENTAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "DATA SCADENZA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "NUM.DOMANDA", 12)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "NUM.DICHIARAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "OPERATORE", 12)

                    K = 2
                    For Each row In dt.Rows
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RICH"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CONTRATTO_NUM"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO_DOM"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATAPRES"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATASCAD"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(Left(Right(par.IfNull(dt.Rows(i).Item("PGDOM"), ""), 14), 10)))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(Left(Right(par.IfNull(dt.Rows(i).Item("PGDICH"), ""), 14), 10)))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("OPERATORE"), "")))

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


            Response.Redirect("..\FileTemp\" & FileCSV & ".zip")

        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub cmbTipodomanda_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipodomanda.SelectedIndexChanged
        Try
            If cmbTipodomanda.SelectedItem.Value <> "-1" Then
                strSQLDomanda = " AND t_motivo_domanda_vsa.ID=" & cmbTipodomanda.SelectedItem.Value
            Else
                strSQLDomanda = ""
            End If
            BindGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cmbOperatore_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbOperatore.SelectedIndexChanged
        Try
            If cmbOperatore.SelectedItem.Value <> "-1" Then
                strSQLOperatore = " AND OPERATORI.ID=" & cmbOperatore.SelectedItem.Value
            Else
                strSQLOperatore = ""
            End If
            BindGrid()
        Catch ex As Exception

        End Try
    End Sub
End Class
