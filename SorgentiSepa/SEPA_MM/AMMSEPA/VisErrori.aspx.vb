
Partial Class AMMSEPA_VisErrori
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:500px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        Response.Flush()
        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub


    Private Sub BindGrid()
        par.OracleConn.Open()
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select SUBSTR(DATA_ORA,7,2)||'/'||SUBSTR(DATA_ORA,5,2)||'/'||SUBSTR(DATA_ORA,1,4)||' - '||SUBSTR(DATA_ORA,9,2)||':'||SUBSTR(DATA_ORA,11,2) AS ""DATA_ORA"",DESCRIZIONE from SISCOM_MI.log_errori order by data_ora desc", par.OracleConn)
        Dim ds As New Data.DataSet()
        da.Fill(ds, "log_errori")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label12.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale: " & ds.Tables(0).Rows.Count
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
