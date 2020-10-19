Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class MANUTENZIONI_RisultatiStManutentivo
    Inherits PageSetIdMode
    Dim par As New CM.Global


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
            Response.Flush()

            Cerca()

        End If
    End Sub
    Private Sub Cerca()

        Try
            Query = Session.Item("RICSTMANUT")
            BindGrid()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Public Property Query() As String
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
    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Query, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "STATO_MANUTENTIVO, UNITA_IMMOBILIARI")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count

        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaStManutentivo.aspx""</script>")
        Session.Remove("RICSTMANUT")

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
        Session.Remove("RICSTMANUT")
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Contratto COD. " & e.Item.Cells(1).Text & "';document.getElementById('IdContratto').value='" & e.Item.Cells(0).Text & "';document.getElementById('CodContratto').value='" & e.Item.Cells(1).Text & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Contratto COD. " & e.Item.Cells(1).Text & "';document.getElementById('IdContratto').value='" & e.Item.Cells(0).Text & "';document.getElementById('CodContratto').value='" & e.Item.Cells(1).Text & "';")

        End If
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        If DataGrid1.Items.Count > 0 Then
            Export()
        Else
            Response.Write("<SCRIPT>alert('Impossibile eseguire l\'export, la tabella è vuota!');</SCRIPT>")

        End If
    End Sub
    Private Sub Export()
        Dim dt As New Data.DataTable
        Dim par As New CM.Global

        Dim FileCSV As String = ""
        Dim row As System.Data.DataRow
        Dim i As Long = 0

        Try
            par.OracleConn.Open()
            FileCSV = "StatoManut_" & Format(Now, "yyyyMMddHHmmss")

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter

            da = New Oracle.DataAccess.Client.OracleDataAdapter(Query, par.OracleConn)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim sSql As String
                sSql = "CREATE TABLE ESTRAZIONE ([COD. UINTA IMMOBILIARE] TEXT(50),[DATA VISITA PRE SLOGGIO] TEXT(15),[DATA VISITA SLOGGIO] TEXT(15), [DATA CONSEGNA CHIAVI] TEXT(15), [DATA RIPRESA CHIAVI] TEXT(15), [DITTA] TEXT(50))"

                Dim cnString As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                       "Data Source=" & Server.MapPath("Export\" & FileCSV & ".xls;") & _
                       "Extended Properties=""Excel 8.0;HDR=YES"""

                Dim cn As New OleDbConnection(cnString)
                cn.Open()

                Dim cmd As New OleDbCommand(sSql, cn)
                cmd.ExecuteNonQuery()

                Dim cmd1 As New OleDbCommand
                cmd1.Connection = cn
                For Each row In dt.Rows
                    'sr.WriteLine(par.IfNull(dt.Rows(i).Item("RATA"), 0) & ";" & par.IfNull(dt.Rows(i).Item("INTESTATARIO"), 0) & ";" & par.IfNull(dt.Rows(i).Item("PERIODO"), 0) & ";" & par.IfNull(dt.Rows(i).Item("AFFITTO"), 0) & ";" & par.IfNull(dt.Rows(i).Item("SPESE"), 0) & ";" & par.IfNull(dt.Rows(i).Item("REGISTRAZIONE"), 0) & ";" & par.IfNull(dt.Rows(i).Item("TOT"), 0) & ";")
                    sSql = "INSERT INTO ESTRAZIONE  VALUES ('" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_UNITA_IMMOBILIARE"), 0)) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_PRE_SLOGGIO"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_S"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_CONSEGNA_CHIAVI"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_RIPRESA_CHIAVI"), "")) & "','" & par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DITTA"), "")) & "')"
                    cmd1.CommandText = sSql
                    cmd1.ExecuteNonQuery()
                    i = i + 1
                Next
                cn.Close()
            End If

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("Export\" & FileCSV & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

            Dim strFile As String
            strFile = Server.MapPath("Export\" & FileCSV & ".xls")
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
            Response.Write("<script>window.open('Export/" & FileCSV & ".zip','Estrazione','');</script>")
            Response.Redirect("Export\" & FileCSV & ".zip")


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
End Class
