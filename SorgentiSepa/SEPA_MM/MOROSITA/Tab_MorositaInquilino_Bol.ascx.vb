Imports System.Collections

Imports System.IO

Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums


Partial Class Tab_Morosita_Bollette
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                'BindGrid_Bollette()
                Me.txtIdComponente.Value = 1
            End If


            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Then
                FrmSolaLettura()
            End If

        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub



    'BOLLETTE GRID1
    Private Sub BindGrid_Bollette()
        'Dim StringaSql As String
        'Dim FlagConnessione As Boolean

        'Dim vidMorosita As Long
        'Dim vIdContratto As Long

        'Dim nBollette As String = ""

        'Try

        '    vidMorosita = CType(Me.Page.FindControl("txtIdMorosita"), HiddenField).Value
        '    'vIdAnagrafica = CType(Me.Page.FindControl("txtIdAnagrafica"), HiddenField).Value
        '    vIdContratto = CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value


        '    FlagConnessione = False
        '    If PAR.OracleConn.State = Data.ConnectionState.Closed Then
        '        PAR.OracleConn.Open()
        '        par.SettaCommand(par)

        '        FlagConnessione = True
        '    End If

        '    '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        '    'PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        '    'par.SettaCommand(par)
        '    'PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        '    '‘‘par.cmd.Transaction = par.myTrans

        '    ' & "   and SISCOM_MI.BOL_BOLLETTE.COD_AFFITTUARIO=" & vIdAnagrafica 

        '    If PAR.IfEmpty(CType(Me.Page.FindControl("txtIdBollette1"), HiddenField).Value, 0) <> 0 Then
        '        nBollette = CType(Me.Page.FindControl("txtIdBollette1"), HiddenField).Value
        '    End If

        '    If PAR.IfEmpty(CType(Me.Page.FindControl("txtIdBollette2"), HiddenField).Value, 0) <> 0 Then
        '        If nBollette <> "" Then
        '            nBollette = nBollette & "," & CType(Me.Page.FindControl("txtIdBollette2"), HiddenField).Value
        '        Else
        '            nBollette = CType(Me.Page.FindControl("txtIdBollette2"), HiddenField).Value
        '        End If
        '    End If


        '    StringaSql = " select BOL_BOLLETTE.ID," _
        '                        & " CASE WHEN N_RATA = '99'     THEN 'MA'" _
        '                            & "  WHEN N_RATA = '999'    THEN 'AU'" _
        '                            & "  WHEN N_RATA = '99999'  THEN 'CO'" _
        '                            & "  WHEN N_RATA = NULL     THEN '??'" _
        '                            & " ELSE LPAD(N_RATA,2,'0') END ||'/'|| DECODE(BOL_BOLLETTE.FL_ANNULLATA,0,'VALIDA',1,'ANNULLATA') as NUMERO_RATA, " _
        '                        & " to_char(to_date(substr(BOL_BOLLETTE.RIFERIMENTO_DA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as RIFERIMENTO_DA," _
        '                        & " to_char(to_date(substr(BOL_BOLLETTE.RIFERIMENTO_A,1,8),'YYYYmmdd'),'DD/MM/YYYY') as RIFERIMENTO_A," _
        '                        & " TRIM(TO_CHAR( (select SUM(IMPORTO) from SISCOM_MI.BOL_BOLLETTE_VOCI where ID_BOLLETTA=SISCOM_MI.BOL_BOLLETTE.ID),'9G999G999G999G999G990D99')) as IMPORTO," _
        '                        & " to_char(to_date(substr(BOL_BOLLETTE.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_EMISSIONE," _
        '                        & " to_char(to_date(substr(BOL_BOLLETTE.DATA_SCADENZA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_SCADENZA," _
        '                        & " TRIM(TO_CHAR(BOL_BOLLETTE.IMPORTO_PAGATO,'9G999G999G999G999G990D99')) AS PAGAMENTO,BOL_BOLLETTE.NOTE " _
        '              & " from  SISCOM_MI.BOL_BOLLETTE   " _
        '              & " where SISCOM_MI.BOL_BOLLETTE.ID_MOROSITA =" & vidMorosita _
        '              & "   and SISCOM_MI.BOL_BOLLETTE.ID_CONTRATTO=" & vIdContratto _
        '              & "   and SISCOM_MI.BOL_BOLLETTE.ID_BOLLETTA_RIC in (" & nBollette & ")" _
        '              & " order by BOL_BOLLETTE.DATA_EMISSIONE DESC,SISCOM_MI.BOL_BOLLETTE.N_RATA DESC"

        '    PAR.cmd.CommandText = StringaSql

        '    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        '    Dim ds As New Data.DataTable

        '    da.Fill(ds)

        '    DataGrid1.DataSource = ds
        '    DataGrid1.DataBind()

        '    da.Dispose()
        '    ds.Dispose()

        '    If FlagConnessione = True Then
        '        PAR.cmd.Dispose()
        '        PAR.OracleConn.Close()
        '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '    End If

        'Catch ex As Exception

        '    If FlagConnessione = True Then
        '        PAR.cmd.Dispose()
        '        PAR.OracleConn.Close()
        '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '    End If

        '    Session.Item("LAVORAZIONE") = "0"
        '    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
        '    Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        'End Try



    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Morosita_Bollette_txtSel1').value='Hai selezionato: " & e.Item.Cells(1).Text & "';document.getElementById('Tab_Morosita_Bollette_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Morosita_Bollette_txtSel1').value='Hai selezionato: " & e.Item.Cells(1).Text & "';document.getElementById('Tab_Morosita_Bollette_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub



    Private Sub FrmSolaLettura()
        Try

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next

        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub



    Protected Sub btnApri1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApri1.Click
        Me.txtIdComponente.Value = 1
    End Sub

    Protected Sub btn_Export_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Export.Click
        Dim myExcelFile As New CM.ExcelFile
        Dim i, k As Integer
        Dim sNomeFile As String

        Try

            sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")

            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\") & sNomeFile & ".xls")

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


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "N.TIPO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "PERIODO DAL", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "PERIODO AL", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "IMPORTO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "EMISSIONE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "SCADENZA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "PAGAMENTO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "DATA PAGAMENTO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "NOTE", 0)

                .SetColumnWidth(1, 8, 20)
                .SetColumnWidth(9, 9, 40)

                k = 2

                For i = 0 To Me.DataGrid1.Items.Count - 1

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, k, 1, Me.DataGrid1.Items(i).Cells(1).Text, 0)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, k, 2, Me.DataGrid1.Items(i).Cells(2).Text, 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, k, 3, Me.DataGrid1.Items(i).Cells(3).Text, 0)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, k, 4, Me.DataGrid1.Items(i).Cells(4).Text, 4)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, k, 5, Me.DataGrid1.Items(i).Cells(5).Text, 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, k, 6, Me.DataGrid1.Items(i).Cells(6).Text, 0)

                    If Me.DataGrid1.Items(i).Cells(7).Text = "&nbsp;" Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, k, 7, "0,00", 4)
                    Else
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, k, 7, PAR.IfEmpty(Me.DataGrid1.Items(i).Cells(7).Text, "0,00"), 4)
                    End If

                    If Me.DataGrid1.Items(i).Cells(8).Text = "&nbsp;" Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, k, 8, "", 0)
                    Else
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, k, 8, Me.DataGrid1.Items(i).Cells(8).Text, 0)
                    End If
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, k, 9, Me.DataGrid1.Items(i).Cells(9).Text, 0)

                    k = k + 1
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

            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
End Class
