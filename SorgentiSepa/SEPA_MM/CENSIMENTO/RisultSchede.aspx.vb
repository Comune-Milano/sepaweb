Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class CENSIMENTO_RisultSchede
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaRisultati()
        End If
    End Sub

    Private Sub CaricaRisultati()
        Try

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If



            Dim likeScheda As String = ""
            Dim codTScheda As String = ""
            If Request.QueryString("SCHEDA") <> "-1" Then
                scheda.Value = Request.QueryString("SCHEDA")
                likeScheda = "AND cod_tipo_elemento LIKE '" & Request.QueryString("SCHEDA") & "%'"
                codTScheda = "AND COD_TIPO_SCHEDA = '" & Request.QueryString("SCHEDA") & "'"
                LnlNumeroRisultati.Text = "ELENCO IMMOBILI PER LA SCHEDA : " & Request.QueryString("SCHEDA")
            Else
                LnlNumeroRisultati.Text = "ELENCO IMMOBILI SU CUI SONO CARICATE SCHEDE RILIEVO"

            End If


            par.cmd.CommandText = "SELECT DISTINCT " _
                                & "(CASE WHEN MATERIALI.id_complesso IS NOT NULL THEN ('COMPLESSO: '||COMPLESSI_IMMOBILIARI.denominazione) WHEN MATERIALI.id_edificio IS NOT NULL THEN ('EDIFICIO: '||EDIFICI.denominazione) " _
                                & "ELSE ('UNITA COMUNE COD. : '||UNITA_COMUNI.cod_unita_comune) END) AS IMMOBILE, " _
                                & "(CASE WHEN MATERIALI.ID_COMPLESSO IS NOT NULL THEN MATERIALI.ID_COMPLESSO WHEN MATERIALI.ID_EDIFICIO IS NOT NULL THEN MATERIALI.ID_EDIFICIO ELSE MATERIALI.ID_UNITA_COMUNE END) AS ID, " _
                                & " 'IN CORSO' AS data_censimento " _
                                & "FROM siscom_mi.MATERIALI, " _
                                & "siscom_mi.COMPLESSI_IMMOBILIARI " _
                                & ", siscom_mi.EDIFICI " _
                                & ",siscom_mi.UNITA_COMUNI " _
                                & "WHERE MATERIALI.id_complesso = COMPLESSI_IMMOBILIARI.ID(+) " _
                                & "AND MATERIALI.id_edificio = EDIFICI.ID(+) " _
                                & "AND MATERIALI.id_unita_comune = UNITA_COMUNI.ID(+) " _
                                & " " & likeScheda & " " _
                                & "UNION " _
                                & "SELECT DISTINCT " _
                                & "(CASE WHEN MATERIALI_ST.id_complesso IS NOT NULL THEN ('COMPLESSO: '||COMPLESSI_IMMOBILIARI.denominazione) WHEN MATERIALI_ST.id_edificio IS NOT NULL THEN ('EDIFICIO: '||EDIFICI.denominazione) " _
                                & "ELSE ('UNITA COMUNE COD. : '||UNITA_COMUNI.cod_unita_comune) END) AS IMMOBILE, " _
                                & "(CASE WHEN MATERIALI_ST.ID_COMPLESSO IS NOT NULL THEN MATERIALI_ST.ID_COMPLESSO WHEN MATERIALI_ST.ID_EDIFICIO IS NOT NULL THEN MATERIALI_ST.ID_EDIFICIO ELSE MATERIALI_ST.ID_UNITA_COMUNE END) AS ID, " _
                                & "TO_CHAR(TO_DATE(DATA_CENSIMENTO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_CENSIMENTO " _
                                & "FROM siscom_mi.MATERIALI_ST, " _
                                & "siscom_mi.COMPLESSI_IMMOBILIARI " _
                                & ", siscom_mi.EDIFICI " _
                                & ",siscom_mi.UNITA_COMUNI " _
                                & "WHERE MATERIALI_ST.id_complesso = COMPLESSI_IMMOBILIARI.ID(+) " _
                                & "AND MATERIALI_ST.id_edificio = EDIFICI.ID(+) " _
                                & "AND MATERIALI_ST.id_unita_comune = UNITA_COMUNI.ID(+) " _
                                & " " & likeScheda & " " _
                                & "UNION " _
                                & "SELECT DISTINCT " _
                                & "(CASE WHEN INTERVENTI.id_complesso IS NOT NULL THEN ('COMPLESSO: '||COMPLESSI_IMMOBILIARI.denominazione) WHEN INTERVENTI.id_edificio IS NOT NULL THEN ('EDIFICIO: '||EDIFICI.denominazione) " _
                                & "ELSE ('UNITA COMUNE COD. : '||UNITA_COMUNI.cod_unita_comune) END) AS IMMOBILE, " _
                                & "(CASE WHEN INTERVENTI.ID_COMPLESSO IS NOT NULL THEN INTERVENTI.ID_COMPLESSO WHEN INTERVENTI.ID_EDIFICIO IS NOT NULL THEN INTERVENTI.ID_EDIFICIO ELSE INTERVENTI.ID_UNITA_COMUNE END) AS ID, " _
                                & " 'IN CORSO' AS data_censimento " _
                                & "FROM siscom_mi.INTERVENTI, " _
                                & "siscom_mi.COMPLESSI_IMMOBILIARI " _
                                & ", siscom_mi.EDIFICI " _
                                & ",siscom_mi.UNITA_COMUNI " _
                                & "WHERE INTERVENTI.id_complesso = COMPLESSI_IMMOBILIARI.ID(+) " _
                                & "AND INTERVENTI.id_edificio = EDIFICI.ID(+) " _
                                & "AND INTERVENTI.id_unita_comune = UNITA_COMUNI.ID(+) " _
                                & " " & likeScheda & " " _
                                & "UNION " _
                                & "SELECT DISTINCT " _
                                & "(CASE WHEN INTERVENTI_ST.id_complesso IS NOT NULL THEN ('COMPLESSO: '||COMPLESSI_IMMOBILIARI.denominazione) WHEN INTERVENTI_ST.id_edificio IS NOT NULL THEN ('EDIFICIO: '||EDIFICI.denominazione) " _
                                & "ELSE ('UNITA COMUNE COD. : '||UNITA_COMUNI.cod_unita_comune) END) AS IMMOBILE, " _
                                & "(CASE WHEN INTERVENTI_ST.ID_COMPLESSO IS NOT NULL THEN INTERVENTI_ST.ID_COMPLESSO WHEN INTERVENTI_ST.ID_EDIFICIO IS NOT NULL THEN INTERVENTI_ST.ID_EDIFICIO ELSE INTERVENTI_ST.ID_UNITA_COMUNE END) AS ID, " _
                                & "TO_CHAR(TO_DATE(DATA_CENSIMENTO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_CENSIMENTO " _
                                & "FROM siscom_mi.INTERVENTI_ST, " _
                                & "siscom_mi.COMPLESSI_IMMOBILIARI " _
                                & ", siscom_mi.EDIFICI " _
                                & ",siscom_mi.UNITA_COMUNI " _
                                & "WHERE INTERVENTI_ST.id_complesso = COMPLESSI_IMMOBILIARI.ID(+) " _
                                & "AND INTERVENTI_ST.id_edificio = EDIFICI.ID(+) " _
                                & "AND INTERVENTI_ST.id_unita_comune = UNITA_COMUNI.ID(+) " _
                                & " " & likeScheda & " " _
                                & "UNION " _
                                & "SELECT DISTINCT " _
                                & "(CASE WHEN ANALISI_PRESTAZIONALE.id_complesso IS NOT NULL THEN ('COMPLESSO: '||COMPLESSI_IMMOBILIARI.denominazione) WHEN ANALISI_PRESTAZIONALE.id_edificio IS NOT NULL THEN ('EDIFICIO: '||EDIFICI.denominazione) " _
                                & "ELSE ('UNITA COMUNE COD. : '||UNITA_COMUNI.cod_unita_comune) END) AS IMMOBILE, " _
                                & "(CASE WHEN ANALISI_PRESTAZIONALE.ID_COMPLESSO IS NOT NULL THEN ANALISI_PRESTAZIONALE.ID_COMPLESSO WHEN ANALISI_PRESTAZIONALE.ID_EDIFICIO IS NOT NULL THEN ANALISI_PRESTAZIONALE.ID_EDIFICIO ELSE ANALISI_PRESTAZIONALE.ID_UNITA_COMUNE END) AS ID, " _
                                & " 'IN CORSO' AS data_censimento " _
                                & "FROM siscom_mi.ANALISI_PRESTAZIONALE, " _
                                & "siscom_mi.COMPLESSI_IMMOBILIARI " _
                                & ", siscom_mi.EDIFICI " _
                                & ",siscom_mi.UNITA_COMUNI " _
                                & "WHERE ANALISI_PRESTAZIONALE.id_complesso = COMPLESSI_IMMOBILIARI.ID(+) " _
                                & "AND ANALISI_PRESTAZIONALE.id_edificio = EDIFICI.ID(+) " _
                                & "AND ANALISI_PRESTAZIONALE.id_unita_comune = UNITA_COMUNI.ID(+) " _
                                & " " & likeScheda & " " _
                                & "UNION " _
                                & "SELECT DISTINCT " _
                                & "(CASE WHEN ANALISI_PRESTAZIONALE_ST.id_complesso IS NOT NULL THEN ('COMPLESSO: '||COMPLESSI_IMMOBILIARI.denominazione) WHEN ANALISI_PRESTAZIONALE_ST.id_edificio IS NOT NULL THEN ('EDIFICIO: '||EDIFICI.denominazione) " _
                                & "ELSE ('UNITA COMUNE COD. : '||UNITA_COMUNI.cod_unita_comune) END) AS IMMOBILE, " _
                                & "(CASE WHEN ANALISI_PRESTAZIONALE_ST.ID_COMPLESSO IS NOT NULL THEN ANALISI_PRESTAZIONALE_ST.ID_COMPLESSO WHEN ANALISI_PRESTAZIONALE_ST.ID_EDIFICIO IS NOT NULL THEN ANALISI_PRESTAZIONALE_ST.ID_EDIFICIO ELSE ANALISI_PRESTAZIONALE_ST.ID_UNITA_COMUNE END) AS ID, " _
                                & "TO_CHAR(TO_DATE(DATA_CENSIMENTO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_CENSIMENTO " _
                                & "FROM siscom_mi.ANALISI_PRESTAZIONALE_ST, " _
                                & "siscom_mi.COMPLESSI_IMMOBILIARI " _
                                & ", siscom_mi.EDIFICI " _
                                & ",siscom_mi.UNITA_COMUNI " _
                                & "WHERE ANALISI_PRESTAZIONALE_ST.id_complesso = COMPLESSI_IMMOBILIARI.ID(+) " _
                                & "AND ANALISI_PRESTAZIONALE_ST.id_edificio = EDIFICI.ID(+) " _
                                & "AND ANALISI_PRESTAZIONALE_ST.id_unita_comune = UNITA_COMUNI.ID(+) " _
                                & " " & likeScheda & " " _
                                & "UNION " _
                                & "SELECT DISTINCT " _
                                & "(CASE WHEN ANOMALIE.id_complesso IS NOT NULL THEN ('COMPLESSO: '||COMPLESSI_IMMOBILIARI.denominazione) WHEN ANOMALIE.id_edificio IS NOT NULL THEN ('EDIFICIO: '||EDIFICI.denominazione) " _
                                & "ELSE ('UNITA COMUNE COD. : '||UNITA_COMUNI.cod_unita_comune) END) AS IMMOBILE, " _
                                & "(CASE WHEN ANOMALIE.ID_COMPLESSO IS NOT NULL THEN ANOMALIE.ID_COMPLESSO WHEN ANOMALIE.ID_EDIFICIO IS NOT NULL THEN ANOMALIE.ID_EDIFICIO ELSE ANOMALIE.ID_UNITA_COMUNE END) AS ID, " _
                                & " 'IN CORSO' AS data_censimento " _
                                & "FROM siscom_mi.ANOMALIE, " _
                                & "siscom_mi.COMPLESSI_IMMOBILIARI " _
                                & ", siscom_mi.EDIFICI " _
                                & ",siscom_mi.UNITA_COMUNI " _
                                & "WHERE ANOMALIE.id_complesso = COMPLESSI_IMMOBILIARI.ID(+) " _
                                & "AND ANOMALIE.id_edificio = EDIFICI.ID(+) " _
                                & "AND ANOMALIE.id_unita_comune = UNITA_COMUNI.ID(+) " _
                                & " " & likeScheda & " " _
                                & "UNION " _
                                & "SELECT DISTINCT " _
                                & "(CASE WHEN ANOMALIE_ST.id_complesso IS NOT NULL THEN ('COMPLESSO: '||COMPLESSI_IMMOBILIARI.denominazione) WHEN ANOMALIE_ST.id_edificio IS NOT NULL THEN ('EDIFICIO: '||EDIFICI.denominazione) " _
                                & "ELSE ('UNITA COMUNE COD. : '||UNITA_COMUNI.cod_unita_comune) END) AS IMMOBILE, " _
                                & "(CASE WHEN ANOMALIE_ST.ID_COMPLESSO IS NOT NULL THEN ANOMALIE_ST.ID_COMPLESSO WHEN ANOMALIE_ST.ID_EDIFICIO IS NOT NULL THEN ANOMALIE_ST.ID_EDIFICIO ELSE ANOMALIE_ST.ID_UNITA_COMUNE END) AS ID, " _
                                & "TO_CHAR(TO_DATE(DATA_CENSIMENTO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_CENSIMENTO " _
                                & "FROM siscom_mi.ANOMALIE_ST, " _
                                & "siscom_mi.COMPLESSI_IMMOBILIARI " _
                                & ", siscom_mi.EDIFICI " _
                                & ",siscom_mi.UNITA_COMUNI " _
                                & "WHERE ANOMALIE_ST.id_complesso = COMPLESSI_IMMOBILIARI.ID(+) " _
                                & "AND ANOMALIE_ST.id_edificio = EDIFICI.ID(+) " _
                                & "AND ANOMALIE_ST.id_unita_comune = UNITA_COMUNI.ID(+) " _
                                & " " & likeScheda & " " _
                                & "UNION " _
                                & "SELECT DISTINCT " _
                                & "(CASE WHEN DIMENSIONI_ELEMENTI.id_complesso IS NOT NULL THEN ('COMPLESSO: '||COMPLESSI_IMMOBILIARI.denominazione) WHEN DIMENSIONI_ELEMENTI.id_edificio IS NOT NULL THEN ('EDIFICIO: '||EDIFICI.denominazione) " _
                                & "ELSE ('UNITA COMUNE COD. : '||UNITA_COMUNI.cod_unita_comune) END) AS IMMOBILE, " _
                                & "(CASE WHEN DIMENSIONI_ELEMENTI.ID_COMPLESSO IS NOT NULL THEN DIMENSIONI_ELEMENTI.ID_COMPLESSO WHEN DIMENSIONI_ELEMENTI.ID_EDIFICIO IS NOT NULL THEN DIMENSIONI_ELEMENTI.ID_EDIFICIO ELSE DIMENSIONI_ELEMENTI.ID_UNITA_COMUNE END) AS ID, " _
                                & " 'IN CORSO' AS data_censimento " _
                                & "FROM siscom_mi.DIMENSIONI_ELEMENTI, " _
                                & "siscom_mi.COMPLESSI_IMMOBILIARI " _
                                & ", siscom_mi.EDIFICI " _
                                & ",siscom_mi.UNITA_COMUNI " _
                                & "WHERE DIMENSIONI_ELEMENTI.id_complesso = COMPLESSI_IMMOBILIARI.ID(+) " _
                                & "AND DIMENSIONI_ELEMENTI.id_edificio = EDIFICI.ID(+) " _
                                & "AND DIMENSIONI_ELEMENTI.id_unita_comune = UNITA_COMUNI.ID(+) " _
                                & " " & likeScheda & " " _
                                & "UNION " _
                                & "SELECT DISTINCT " _
                                & "(CASE WHEN DIMENSIONI_ELEMENTI_ST.id_complesso IS NOT NULL THEN ('COMPLESSO: '||COMPLESSI_IMMOBILIARI.denominazione) WHEN DIMENSIONI_ELEMENTI_ST.id_edificio IS NOT NULL THEN ('EDIFICIO: '||EDIFICI.denominazione) " _
                                & "ELSE ('UNITA COMUNE COD. : '||UNITA_COMUNI.cod_unita_comune) END) AS IMMOBILE, " _
                                & "(CASE WHEN DIMENSIONI_ELEMENTI_ST.ID_COMPLESSO IS NOT NULL THEN DIMENSIONI_ELEMENTI_ST.ID_COMPLESSO WHEN DIMENSIONI_ELEMENTI_ST.ID_EDIFICIO IS NOT NULL THEN DIMENSIONI_ELEMENTI_ST.ID_EDIFICIO ELSE DIMENSIONI_ELEMENTI_ST.ID_UNITA_COMUNE END) AS ID, " _
                                & "TO_CHAR(TO_DATE(DATA_CENSIMENTO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_CENSIMENTO " _
                                & "FROM siscom_mi.DIMENSIONI_ELEMENTI_ST, " _
                                & "siscom_mi.COMPLESSI_IMMOBILIARI " _
                                & ", siscom_mi.EDIFICI " _
                                & ",siscom_mi.UNITA_COMUNI " _
                                & "WHERE DIMENSIONI_ELEMENTI_ST.id_complesso = COMPLESSI_IMMOBILIARI.ID(+) " _
                                & "AND DIMENSIONI_ELEMENTI_ST.id_edificio = EDIFICI.ID(+) " _
                                & "AND DIMENSIONI_ELEMENTI_ST.id_unita_comune = UNITA_COMUNI.ID(+) " _
                                & " " & likeScheda & " " _
                                & "UNION " _
                                & "SELECT DISTINCT " _
                                & "(CASE WHEN STATO_DEGRADO.id_complesso IS NOT NULL THEN ('COMPLESSO: '||COMPLESSI_IMMOBILIARI.denominazione) WHEN STATO_DEGRADO.id_edificio IS NOT NULL THEN ('EDIFICIO: '||EDIFICI.denominazione) " _
                                & "ELSE ('UNITA COMUNE COD. : '||UNITA_COMUNI.cod_unita_comune) END) AS IMMOBILE, " _
                                & "(CASE WHEN STATO_DEGRADO.ID_COMPLESSO IS NOT NULL THEN STATO_DEGRADO.ID_COMPLESSO WHEN STATO_DEGRADO.ID_EDIFICIO IS NOT NULL THEN STATO_DEGRADO.ID_EDIFICIO ELSE STATO_DEGRADO.ID_UNITA_COMUNE END) AS ID, " _
                                & " 'IN CORSO' AS data_censimento " _
                                & "FROM siscom_mi.STATO_DEGRADO, " _
                                & "siscom_mi.COMPLESSI_IMMOBILIARI " _
                                & ", siscom_mi.EDIFICI " _
                                & ",siscom_mi.UNITA_COMUNI " _
                                & "WHERE STATO_DEGRADO.id_complesso = COMPLESSI_IMMOBILIARI.ID(+) " _
                                & "AND STATO_DEGRADO.id_edificio = EDIFICI.ID(+) " _
                                & "AND STATO_DEGRADO.id_unita_comune = UNITA_COMUNI.ID(+) " _
                                & " " & codTScheda & " " _
                                & "UNION " _
                                & "SELECT DISTINCT " _
                                & "(CASE WHEN STATO_DEGRADO_ST.id_complesso IS NOT NULL THEN ('COMPLESSO: '||COMPLESSI_IMMOBILIARI.denominazione) WHEN STATO_DEGRADO_ST.id_edificio IS NOT NULL THEN ('EDIFICIO: '||EDIFICI.denominazione) " _
                                & "ELSE ('UNITA COMUNE COD. : '||UNITA_COMUNI.cod_unita_comune) END) AS IMMOBILE, " _
                                & "(CASE WHEN STATO_DEGRADO_ST.ID_COMPLESSO IS NOT NULL THEN STATO_DEGRADO_ST.ID_COMPLESSO WHEN STATO_DEGRADO_ST.ID_EDIFICIO IS NOT NULL THEN STATO_DEGRADO_ST.ID_EDIFICIO ELSE STATO_DEGRADO_ST.ID_UNITA_COMUNE END) AS ID, " _
                                & "TO_CHAR(TO_DATE(DATA_CENSIMENTO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_CENSIMENTO " _
                                & "FROM siscom_mi.STATO_DEGRADO_ST, " _
                                & "siscom_mi.COMPLESSI_IMMOBILIARI " _
                                & ", siscom_mi.EDIFICI " _
                                & ",siscom_mi.UNITA_COMUNI " _
                                & "WHERE STATO_DEGRADO_ST.id_complesso = COMPLESSI_IMMOBILIARI.ID(+) " _
                                & "AND STATO_DEGRADO_ST.id_edificio = EDIFICI.ID(+) " _
                                & "AND STATO_DEGRADO_ST.id_unita_comune = UNITA_COMUNI.ID(+) " _
                                & "" & codTScheda & "  ORDER BY IMMOBILE ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim dt As New Data.DataTable()
            da.Fill(dt)

            DataGrid1.DataSource = dt
            DataGrid1.DataBind()
            Session.Add("dtExport", dt)

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('id').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtdesc').value='" & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtdata').value='" & par.AggiustaData(e.Item.Cells(2).Text.Replace("IN CORSO", "")) & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('id').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtdesc').value='" & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtdata').value='" & par.AggiustaData(e.Item.Cells(2).Text.Replace("IN CORSO", "")) & "'")

        End If



    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            CaricaRisultati()
        End If

    End Sub


    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            Dim dt As New Data.DataTable
            dt = CType(HttpContext.Current.Session.Item("dtExport"), Data.DataTable)
            If Request.QueryString("SCHEDA") <> "-1" Then
                sNomeFile = "ExpCensimentoScheda_" & Request.QueryString("SCHEDA") & "_" & Format(Now, "yyyyMMddHHmmss")
            Else
                sNomeFile = "ExpCensimentoSchedeRilievo_" & Format(Now, "yyyyMMddHHmmss")

            End If

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


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "IMMOBILE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "DATA CENSIMENTO", 12)
                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "CITTA", 12)
                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "AMMINISTRATORE", 12)

                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("IMMOBILE"))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt.Rows(i).Item("DATA_CENSIMENTO"), ""))
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt.Rows(i).Item("CITTA"), ""))
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(dt.Rows(i).Item("AMMINIST"), ""))

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
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try


    End Sub
End Class
