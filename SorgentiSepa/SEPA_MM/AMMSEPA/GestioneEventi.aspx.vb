
Partial Class AMMSEPA_GestioneEventi
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
        Try
            Dim StringaSQL As String = "select eventi_gestione.ID_OPERATORE, eventi_gestione.COD_EVENTO, SUBSTR(eventi_gestione.DATA_ORA,7,2)||'/'||SUBSTR(eventi_gestione.DATA_ORA,5,2)||'/'||SUBSTR(eventi_gestione.DATA_ORA,1,4)||' - '||SUBSTR(eventi_gestione.DATA_ORA,9,2)||':'||SUBSTR(eventi_gestione.DATA_ORA,11,2) AS ""DATA_ORA"", eventi_gestione.MOTIVAZIONE, operatori.ID, operatori.OPERATORE, siscom_mi.tab_eventi.COD, siscom_mi.tab_eventi.DESCRIZIONE" _
                                    & " from eventi_gestione, operatori, siscom_mi.tab_eventi" _
                                    & " where eventi_gestione.ID_OPERATORE = operatori.ID AND eventi_gestione.COD_EVENTO = siscom_mi.tab_eventi.COD"
            If Request.QueryString("OPERATORE") <> -1 Then
                StringaSQL = StringaSQL & " and id_operatore = " & Request.QueryString("OPERATORE") & ""
            End If
            If Request.QueryString("DATADAL") <> "000000" Then
                StringaSQL = StringaSQL & " AND data_ora > " & Request.QueryString("DATADAL") & ""
            End If
            If Request.QueryString("DATAAL") <> "000000" Then
                StringaSQL = StringaSQL & " AND data_ora < " & Request.QueryString("DATAAL") & ""
            End If
            StringaSQL = StringaSQL & " order by eventi_gestione.DATA_ORA desc"
            par.OracleConn.Open()
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSQL, par.OracleConn)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "log_errori")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            Label12.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale: " & ds.Tables(0).Rows.Count
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If ds.Tables(0).Rows.Count = 0 Then
                Response.Write("<script>alert('La ricerca non ha prodotto risultati!');document.location.href=""RicercaEventi.aspx""</script>")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
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

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaEventi.aspx""</script>")
    End Sub
End Class
