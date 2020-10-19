Imports System.Collections
Imports System.Data.OleDb
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.IO

Partial Class SituazionePagamenti

    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable
    Public RisultatiVisibility As String = "hidden"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        lblRisultati.Visible = False
        DataGrid1.Visible = False
        btnExport.Visible = False

        lblTitolo.Text = "Situazione Pagamenti"

        '##### CARICAMENTO PAGINA #####
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)

        Try
            If Not IsPostBack Then
                If Not IsNothing(Session.Item("dt_elencoMandati")) Then
                    Session.Remove("dt_elencoMandati")
                End If

                caricaEserciziFinanziari()

                '#### CARICAMENTO DDL ANNI e STATI####

                apriConnessione()
                Try
                    par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.V_PAGAMENTI.ANNO FROM SISCOM_MI.V_PAGAMENTI"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    While myReader.Read
                        If par.IfNull(myReader("ANNO"), 0) <> 0 Then
                            ddlAnno.Items.Add(New ListItem(myReader("ANNO")))
                        End If
                    End While
                    myReader.Close()

                    par.cmd.CommandText = "SELECT DISTINCT ""STATO PAGAMENTO"" FROM SISCOM_MI.V_PAGAMENTI"
                    myReader = par.cmd.ExecuteReader
                    While myReader.Read
                        If par.IfNull(myReader("STATO PAGAMENTO"), "") <> "" Then
                            ddlStatoPagamento.Items.Add(New ListItem(myReader("STATO PAGAMENTO")))
                        End If
                    End While
                    myReader.Close()

                    BindGrid()

                Catch ex As Exception
                    chiudiConnessione()
                    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                    Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
                End Try
                par.cmd.Dispose()
                chiudiConnessione()

            End If

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub apriConnessione()
        Try
            If par.OracleConn.State = 0 Then
                par.OracleConn.Open()
            End If
            par.cmd = par.OracleConn.CreateCommand
        Catch ex As Exception
            Response.Write("<script>parent.main.location.replace('SituazionePagamenti.aspx');</script>")
        End Try

    End Sub

    Protected Sub chiudiConnessione()
        Try
            If par.OracleConn.State = 1 Then

                par.cmd.Dispose()
                par.OracleConn.Close()

            End If
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Response.Write("<script>parent.main.location.replace('SituazionePagamenti.aspx');</script>")
        End Try

    End Sub

    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click
        If Not IsNothing(Session.Item("dt_elencoMandati")) Then
            Session.Remove("dt_elencoMandati")
        End If
        Response.Redirect("../../pagina_home.aspx")
    End Sub

    '#######################################
    'Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
    '    If e.NewPageIndex >= 0 Then

    '        DataGrid1.CurrentPageIndex = e.NewPageIndex
    '        DataGrid1.DataSource = Session.Item("dt_elencoMandati")
    '        DataGrid1.DataBind()
    '        DataGrid1.Visible = True
    '        RisultatiVisibility = "visible"
    '        btnExport.Visible = True
    '    End If

    'End Sub
    '#######################################

    Protected Sub BindGrid()
        'DataGrid1.CurrentPageIndex = 0
        apriConnessione()

        Try
            Dim wherecond As String = "WHERE "


            If ddlAnno.SelectedValue <> "Qualsiasi" Then
                wherecond = wherecond & "ANNO='" & ddlAnno.SelectedValue & "' "
            Else
                wherecond = wherecond & "ANNO LIKE '%' "
            End If
            If ddlStatoPagamento.SelectedValue <> "Qualsiasi" Then
                wherecond = wherecond & "AND ""STATO PAGAMENTO""='" & ddlStatoPagamento.SelectedValue & "' "
            Else
                wherecond = wherecond & "AND ""STATO PAGAMENTO"" LIKE '%' "
            End If

            If ddlEsercizio.SelectedValue <> "Qualsiasi" Then
                wherecond = wherecond & "AND ID_ESERCIZIO_FINANZIARIO='" & ddlEsercizio.SelectedValue & "'"
            Else
                wherecond = wherecond & "AND ID_ESERCIZIO_FINANZIARIO LIKE '%'"
            End If


            'If ddlAnno.SelectedValue <> "Qualsiasi" And ddlStatoPagamento.SelectedValue <> "Qualsiasi" Then
            '    wherecond = "WHERE ANNO='" & ddlAnno.SelectedValue & "' AND ""STATO PAGAMENTO""='" & ddlStatoPagamento.SelectedValue & "'"
            'ElseIf ddlAnno.SelectedValue = "Qualsiasi" And ddlStatoPagamento.SelectedValue <> "Qualsiasi" Then
            '    wherecond = "WHERE ""STATO PAGAMENTO""='" & ddlStatoPagamento.SelectedValue & "'"
            'ElseIf ddlAnno.SelectedValue <> "Qualsiasi" And ddlStatoPagamento.SelectedValue = "Qualsiasi" Then
            '    wherecond = "WHERE ANNO='" & ddlAnno.SelectedValue & "'"
            'Else
            '    wherecond = ""
            'End If


            Dim STRINGASQL As String = "SELECT SISCOM_MI.V_PAGAMENTI.*,TO_CHAR(TO_DATE(DATA_EMISSIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_EMISSIONE_MOD," _
                                       & "TO_CHAR(TO_DATE(DATA_STAMPA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_STAMPA_MOD," _
                                       & "TO_CHAR(TO_DATE(INIZIO,'yyyymmdd'),'dd/mm/yyyy') || ' - ' || TO_CHAR(TO_DATE(FINE,'yyyymmdd'),'dd/mm/yyyy') AS ESERCIZIO_FINANZIARIO " _
                                       & "FROM SISCOM_MI.V_PAGAMENTI,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " & wherecond _
                                       & " AND V_PAGAMENTI.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
                                       & "ORDER BY NUM"
            par.cmd.CommandText = STRINGASQL
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            '########## CREO TABELLA ##########
            dt.Clear()
            dt.Columns.Clear()
            dt.Rows.Clear()
            dt.Columns.Add("NUM")
            dt.Columns.Add("ANNO")
            dt.Columns.Add("DATA_EMISSIONE")
            dt.Columns.Add("DATA_STAMPA")
            dt.Columns.Add("DESCRIZIONE")
            dt.Columns.Add("IMPORTO_CONSUNTIVATO")
            dt.Columns.Add("STATO_PAGAMENTO")
            dt.Columns.Add("FORNITORE")
            dt.Columns.Add("REPERTORIO")
            dt.Columns.Add("CAPITOLO")
            dt.Columns.Add("STRUTTURA")
            dt.Columns.Add("ESERCIZIO_FINANZIARIO")
            Dim importoD As Double = 0
            Dim ROW As Data.DataRow
            While myReader1.Read
                ROW = dt.NewRow()
                ROW.Item("NUM") = UCase(par.IfNull(myReader1("NUM"), ""))
                ROW.Item("ANNO") = UCase(par.IfNull(myReader1("ANNO"), ""))
                ROW.Item("DATA_EMISSIONE") = UCase(par.IfNull(myReader1("DATA_EMISSIONE_MOD"), ""))
                ROW.Item("DATA_STAMPA") = UCase(par.IfNull(myReader1("DATA_STAMPA_MOD"), ""))
                ROW.Item("DESCRIZIONE") = UCase(par.IfNull(myReader1("DESCRIZIONE"), ""))
                If par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) = 0 Then
                    ROW.Item("IMPORTO_CONSUNTIVATO") = ""
                Else
                    importoD = CDbl(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0))
                    ROW.Item("IMPORTO_CONSUNTIVATO") = CStr(importoD.ToString("#,##0.00"))

                End If
                ROW.Item("STATO_PAGAMENTO") = UCase(par.IfNull(myReader1("STATO PAGAMENTO"), ""))
                ROW.Item("FORNITORE") = UCase(par.IfNull(myReader1("FORNITORE"), ""))
                ROW.Item("REPERTORIO") = UCase(par.IfNull(myReader1("REPERTORIO"), ""))
                ROW.Item("CAPITOLO") = UCase(par.IfNull(myReader1("CAPITOLO"), ""))
                ROW.Item("STRUTTURA") = UCase(par.IfNull(myReader1("STRUTTURA"), ""))
                ROW.Item("ESERCIZIO_FINANZIARIO") = par.IfNull(myReader1("ESERCIZIO_FINANZIARIO"), "")
                dt.Rows.Add(ROW)
            End While
            myReader1.Close()

            If dt.Rows.Count > 0 Then
                Session.Add("dt_elencoMandati", dt)
                DataGrid1.DataSource = dt
                DataGrid1.DataBind()
                DataGrid1.Visible = True
                RisultatiVisibility = "visible"
                btnExport.Visible = True
            Else
                If ddlAnno.SelectedValue = "Qualsiasi" Then
                    lblRisultati.Text = "Non è possibile visualizzare nessun mandato di pagamento"
                Else
                    lblRisultati.Text = "Non è presente nessun mandato di pagamento per l'anno " & ddlAnno.SelectedValue & " con stato di pagamento """ & ddlStatoPagamento.SelectedValue & """"
                End If

                lblRisultati.Visible = True

            End If

        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
        par.cmd.Dispose()
        chiudiConnessione()

    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click


        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim row As System.Data.DataRow
        Dim par As New CM.Global
        Dim FileXLS As String = ""

        Try


            FileXLS = "MandatiPagamento" & Format(Now, "yyyyMMddHHmm")
            Dim dt As Data.DataTable = Session.Item("dt_elencoMandati")

            If dt.Rows.Count > 0 Then
                i = 0
                With myExcelFile
                    .CreateFile(Server.MapPath("..\..\..\FileTemp\" & FileXLS & ".xls"))
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
                    .SetColumnWidth(1, 2, 10)
                    .SetColumnWidth(3, 3, 17)
                    .SetColumnWidth(4, 4, 14)
                    .SetColumnWidth(5, 5, 90)
                    .SetColumnWidth(6, 6, 25)
                    .SetColumnWidth(7, 7, 20)
                    .SetColumnWidth(8, 8, 60)
                    .SetColumnWidth(9, 9, 17)
                    .SetColumnWidth(10, 10, 100)
                    .SetColumnWidth(11, 11, 52)
                    .SetColumnWidth(12, 12, 23)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "NUM", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "ANNO", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "DATA EMISSIONE", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "DATA STAMPA", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "DESCRIZIONE", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "IMPORTO CONSUNTIVATO", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "STATO PAGAMENTO", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "FORNITORE", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "REPERTORIO", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "CAPITOLO", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "STRUTTURA", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "ESERCIZIO FINANZIARIO", 0)
                    K = 2
                    For Each row In dt.Rows
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(dt.Rows(i).Item("NUM"), " "), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt.Rows(i).Item("ANNO"), " "), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt.Rows(i).Item("DATA_EMISSIONE"), " "), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(dt.Rows(i).Item("DATA_STAMPA"), " "), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), " "), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfNull(dt.Rows(i).Item("IMPORTO_CONSUNTIVATO"), " "), 4)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(dt.Rows(i).Item("STATO_PAGAMENTO"), " "), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(dt.Rows(i).Item("FORNITORE"), " "), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(dt.Rows(i).Item("REPERTORIO"), " "), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.IfNull(dt.Rows(i).Item("CAPITOLO"), " "), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.IfNull(dt.Rows(i).Item("STRUTTURA"), " "), 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.IfNull(dt.Rows(i).Item("ESERCIZIO_FINANZIARIO"), " "), 0)
                        i = i + 1
                        K = K + 1
                    Next
                    .CloseFile()
                End With

                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                Dim zipfic As String
                zipfic = Server.MapPath("..\..\..\FileTemp\" & FileXLS & ".zip")
                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                Dim strFile As String
                strFile = Server.MapPath("..\..\..\FileTemp\" & FileXLS & ".xls")
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
                Response.Redirect("..\..\..\FileTemp\" & FileXLS & ".zip")
            End If

        Catch ex As Exception

            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione del file');location.replace('SituazionePagamenti.aspx');</script>")

        End Try
    End Sub

    Protected Sub ddlStatoPagamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStatoPagamento.SelectedIndexChanged

        BindGrid()
    End Sub

    Protected Sub ddlAnno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAnno.SelectedIndexChanged

        BindGrid()
    End Sub

    Protected Sub caricaEserciziFinanziari()
        Try
            'APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            'SELEZIONO I PIANI FINANZIARI APPROVATI
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                        & "WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
                        & "AND ID_STATO='5'"


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While myReader1.Read
                Dim ANNOINIZIO As String = par.IfNull(myReader1("INIZIO"), "")
                Dim ANNOFINE As String = par.IfNull(myReader1("FINE"), "")
                If Len(ANNOINIZIO) = 8 And Len(ANNOFINE) = 8 Then
                    ANNOINIZIO = ConvertiData(ANNOINIZIO)
                    ANNOFINE = ConvertiData(ANNOFINE)
                    ddlEsercizio.Items.Add(New ListItem(ANNOINIZIO & "   -   " & ANNOFINE, par.IfNull(myReader1("ID_ESERCIZIO_FINANZIARIO"), 0)))
                    'If par.IfNull(myReader1("ID_STATO"), 0) = 5 Then
                    '    ddlEsercizio.SelectedValue = par.IfNull(myReader1("ID_ESERCIZIO_FINANZIARIO"), 0)
                    'End If
                Else
                    'ERRORE USCIRE DALLA PAGINA
                    Response.Write("<script>alert('Si è verificato un errore durante il caricamento degli esercizi finanziari.');location.replace('../../pagina_home.aspx');</script>")
                End If

            End While

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub

    Private Function ConvertiData(ByVal dataIn As String) As String
        Dim dataOut As String = ""
        If Len(dataIn) = 8 Then
            Return Right(dataIn, 2) & "/" & Mid(dataIn, 5, 2) & "/" & Left(dataIn, 4)
        Else
            Return ""
        End If
    End Function

    Protected Sub ddlEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEsercizio.SelectedIndexChanged
        BindGrid()
    End Sub
End Class
