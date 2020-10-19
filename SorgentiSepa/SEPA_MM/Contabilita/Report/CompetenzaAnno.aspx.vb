
Partial Class Contabilita_Report_CompetenzaAnno
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            caricaElencoVoci()
        End If
    End Sub
    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Redirect("../pagina_home.aspx")
    End Sub

    Private Sub caricaElencoVoci()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'max e armando 16/05/2014
            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 order by descrizione"
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE (ID<10000 OR ID IN (SELECT DISTINCT id_voce FROM siscom_mi.BOL_SCHEMA)) ORDER BY descrizione"
            '******************************
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If dt.Rows.Count > 0 Then
                DataGridVoci.DataSource = dt
                DataGridVoci.DataBind()
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante il caricamento delle voci!');", True)
        End Try
    End Sub



    Protected Sub DataGridVoci_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridVoci.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('txtSelezionato').value='Hai selezionato la voce " & e.Item.Cells(1).Text.Replace("'", "\'").Replace("&nbsp;", "") & "';document.getElementById('HiddenFieldSelezionato').value='" & e.Item.Cells(0).Text & "';")
            e.Item.Attributes.Add("onDblclick", "ApriVoce();")
        End If
    End Sub

    Protected Sub ImageButtonVisualizza_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonVisualizza.Click
        HiddenFieldSelezionato.Value = ""
        txtSelezionato.Text = "Nessuna Selezione"
    End Sub
End Class
