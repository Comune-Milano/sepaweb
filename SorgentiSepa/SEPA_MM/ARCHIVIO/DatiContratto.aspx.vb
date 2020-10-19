Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ARCHIVIO_DatiContratto
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            If par.DeCriptaMolto(Request.QueryString("LT")) <> "LETTURA" Then
                If Request.QueryString("LT") = "46412446461946416791764641641971944194946548928525822828652525255878787897987987" Then
                    If Session.Item("MOD_ARCHIVIO") <> "1" Then
                        Response.Redirect("~/AccessoNegato.htm", True)
                        Exit Sub
                    End If
                    If Session.Item("MOD_ARCHIVIO_IM") = "0" Then
                        btnNuovo.Visible = False
                        btnModifica.Visible = False
                    End If
                    If Session.Item("MOD_ARCHIVIO_C") = "0" Then
                        ImgElimina.Visible = False
                    End If
                Else
                    ImgElimina.Visible = False
                    btnNuovo.Visible = False
                    btnModifica.Visible = False
                    Response.Redirect("~/AccessoNegato.htm", True)
                    Exit Sub
                End If
            Else
                ImgElimina.Visible = False
                btnNuovo.Visible = False
                btnModifica.Visible = False
            End If
            IndiceContratto = Request.QueryString("id")
            CaricaDati()
        End If
    End Sub

    Private Sub CaricaDati()
        Try
            Dim Intestatario As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)



            par.cmd.CommandText = "select indirizzi.descrizione,indirizzi.civico,indirizzi.cap,indirizzi.localita, unita_immobiliari.cod_unita_immobiliare,siscom_mi.getintestatari(rapporti_utenza.id) as intestatario,rapporti_utenza.cod_contratto from siscom_mi.indirizzi, siscom_mi.unita_contrattuale,siscom_mi.unita_immobiliari,siscom_mi.rapporti_utenza where indirizzi.id (+)=unita_immobiliari.id_indirizzo and unita_contrattuale.id_contratto (+)=rapporti_utenza.id and unita_contrattuale.id_unita_principale is null and unita_immobiliari.id (+)=unita_contrattuale.id_unita and rapporti_utenza.id=" & IndiceContratto
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Intestatario = par.IfNull(myReader("intestatario"), ",")
                lblDatiContratto.Text = par.IfNull(myReader("cod_contratto"), "---")
                lblIntestatario.Text = Mid(Intestatario, 1, Len(Intestatario) - 1)

                lblDatiUnita.Text = par.IfNull(myReader("cod_unita_immobiliare"), "---")
                lblIndirizzo.Text = par.IfNull(myReader("descrizione"), "") & " " & par.IfNull(myReader("civico"), "") & " - " & par.IfNull(myReader("cap"), "") & " " & par.IfNull(myReader("localita"), "")
            End If
            myReader.Close()

            sStringaSQL1 = "select RAPPORTI_UTENZA_ARCHIVIO.ID,TAB_GESTORI_ARCHIVIO.DESCRIZIONE AS GESTORE,RAPPORTI_UTENZA_ARCHIVIO.COD_EUSTORGIO,RAPPORTI_UTENZA_ARCHIVIO.FALDONE,RAPPORTI_UTENZA_ARCHIVIO.SCATOLA,RAPPORTI_UTENZA_ARCHIVIO.ID_CONTRATTO,TAB_CAT_EUSTORGIO.DESCRIZIONE AS CATEGORIA,RAPPORTI_UTENZA_ARCHIVIO.NOTE from SISCOM_MI.TAB_GESTORI_ARCHIVIO, SISCOM_MI.TAB_CAT_EUSTORGIO,siscom_mi.rapporti_utenza,siscom_mi.rapporti_utenza_archivio where TAB_GESTORI_ARCHIVIO.COD (+)=RAPPORTI_UTENZA_ARCHIVIO.GESTORE AND TAB_CAT_EUSTORGIO.COD(+)=RAPPORTI_UTENZA_ARCHIVIO.CATEGORIA AND rapporti_utenza_archivio.id_contratto (+)=rapporti_utenza.id and rapporti_utenza.id=" & IndiceContratto & " order by cod_eustorgio asc"
            BindGrid()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub BindGrid()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "SISCOM_MI.RAPPORTI_UTENZA_ARCHIVIO")
            Datagrid2.DataSource = ds
            Datagrid2.DataBind()

        Catch ex As Exception
            par.OracleConn.Close()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Sub


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


    Public Property IndiceContratto() As String
        Get
            If Not (ViewState("par_IndiceContratto") Is Nothing) Then
                Return CStr(ViewState("par_IndiceContratto"))
            Else
                Return "-1"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndiceContratto") = value
        End Set

    End Property

    Protected Sub ImgAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgAnnulla.Click
        Response.Write("<script>self.close();</script>")
    End Sub

    Protected Sub Datagrid2_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Silver'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")

        End If
    End Sub

    Protected Sub Datagrid2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles Datagrid2.SelectedIndexChanged

    End Sub

    Protected Sub btnNuovo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnNuovo.Click

        Response.Write("<script>window.open('SchedaEustorgio.aspx?SC=-1&ID=" & IndiceContratto & "&COD=" & lblDatiContratto.Text & "', '" & LBLID.Value & "', 'height=350,width=600');</script>")
        
    End Sub

    Public Sub btnAggiorna_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAggiorna.Click
        BindGrid()
    End Sub

    Protected Sub btnModifica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnModifica.Click
        If LBLID.Value <> "" Then
            Response.Write("<script>window.open('SchedaEustorgio.aspx?SC=" & LBLID.Value & "&ID=" & IndiceContratto & "&COD=" & lblDatiContratto.Text & "', '" & LBLID.Value & "', 'height=350,width=600');</script>")
        Else
            Response.Write("<script>alert('Selezionare un elemento dalla lista');</script>")
        End If
    End Sub

    Protected Sub ImgElimina_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgElimina.Click
        If LBLID.Value <> "" Then
            Try
                Dim EUSTORGIO As String = ""
                Dim FALDONE As String = ""

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()

                par.cmd.CommandText = "select * from SISCOM_MI.RAPPORTI_UTENZA_ARCHIVIO where id=" & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    EUSTORGIO = par.IfNull(myReader("COD_EUSTORGIO"), "")
                    FALDONE = par.IfNull(myReader("FALDONE"), "")
                End If
                myReader.Close()

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.RAPPORTI_UTENZA_ARCHIVIO WHERE ID=" & LBLID.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI_ARCHIVIO (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
               & "VALUES (" & IndiceContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
               & "'F56','" & par.PulisciStrSql("VALORI PRESENTI: COD. EUSTORGIO:" & EUSTORGIO & " - FALDONE:" & FALDONE) & "')"
                par.cmd.ExecuteNonQuery()

                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                BindGrid()
                Response.Write("<script>alert('Operazione effettuata!');</script>")
            Catch ex As Exception
                par.myTrans.Rollback()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
        Else
            Response.Write("<script>alert('Selezionare un elemento dalla lista');</script>")
        End If
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        ExportXLS_Chiama100()
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
                    .SetColumnWidth(2, 2, 20)
                    .SetColumnWidth(3, 3, 30)
                    .SetColumnWidth(4, 4, 15)
                    .SetColumnWidth(5, 5, 45)
                    .SetColumnWidth(6, 6, 20)
                    .SetColumnWidth(7, 7, 45)
                    .SetColumnWidth(8, 8, 20)
                    .SetColumnWidth(9, 9, 25)
                    .SetColumnWidth(10, 10, 20)
                    .SetColumnWidth(11, 11, 25)
                    .SetColumnWidth(12, 12, 20)
                    .SetColumnWidth(13, 13, 20)
                    .SetColumnWidth(14, 14, 20)
                    .SetColumnWidth(15, 15, 55)
                    .SetColumnWidth(16, 16, 60)
                    .SetColumnWidth(17, 17, 30)
                    .SetColumnWidth(18, 18, 20)
                    .SetColumnWidth(19, 19, 35)
                    .SetColumnWidth(20, 20, 20)
                    .SetColumnWidth(21, 21, 25)
                    .SetColumnWidth(22, 22, 20)
                    .SetColumnWidth(23, 23, 20)
                    .SetColumnWidth(24, 24, 20)
                    .SetColumnWidth(25, 25, 20)
                    .SetColumnWidth(26, 26, 20)
                    .SetColumnWidth(27, 27, 20)
                    .SetColumnWidth(28, 28, 20)



                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COD. CONTRATTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INTESTATARIO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "COD.UNITA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "INDIRIZZO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "COD.EUSTORGIO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "GESTORE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "CATEGORIA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "SCATOLA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "FALDONE", 12)



                    K = 2
                    For Each row In dt.Rows
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(lblDatiContratto.Text))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(lblIntestatario.Text))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(lblDatiUnita.Text))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(lblIndirizzo.Text))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_EUSTORGIO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("GESTORE"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CATEGORIA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SCATOLA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FALDONE"), "")))



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


            ' Response.Write("<script>window.open('../FileTemp/" & FileCSV & ".zip','Estrazione','');</script>")
            Response.Redirect("..\FileTemp\" & FileCSV & ".zip")

        Catch ex As Exception
            par.OracleConn.Close()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try

    End Function

End Class
