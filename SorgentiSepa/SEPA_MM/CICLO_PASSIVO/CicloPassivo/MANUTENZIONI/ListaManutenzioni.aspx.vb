
Partial Class CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_ListaManutenzioni
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim conndata As CM.datiConnessione = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.conndata = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            CaricaTabella()
        End If
    End Sub
    Protected Sub CaricaTabella()
        Try
            conndata.apri()
            Dim id As Integer = 0
            If Not IsNothing(Request.QueryString("id")) Then
                id = Request.QueryString("id")
            End If
            par.cmd.CommandText = "select id, progr||'/'||anno as NUMERO,descrizione from siscom_mi.manutenzioni where id_segnalazioni=" & id
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                DataGridManutenzioni.DataSource = dt
                DataGridManutenzioni.DataBind()
            End If
            conndata.chiudi()
        Catch ex As Exception
            conndata.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGridManutenzioni_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridManutenzioni.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------  
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white';}")
            e.Item.Attributes.Add("onclick", "if (selezionato) {selezionato.style.backgroundColor='';}selezionato=this;this.style.backgroundColor='red';document.getElementById('IDM').value='" & e.Item.Cells(par.IndDGC(DataGridManutenzioni, "ID")).Text & "';document.getElementById('txtmia').value='Hai selezionato la manutenzione n° " & e.Item.Cells(par.IndDGC(DataGridManutenzioni, "NUMERO")).Text & "';")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnVisualizza').click();")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
            e.Item.Attributes.Add("onclick", "if (selezionato) {selezionato.style.backgroundColor='';}selezionato=this;this.style.backgroundColor='red';document.getElementById('IDM').value='" & e.Item.Cells(par.IndDGC(DataGridManutenzioni, "ID")).Text & "';document.getElementById('txtmia').value='Hai selezionato la manutenzione n° " & e.Item.Cells(par.IndDGC(DataGridManutenzioni, "NUMERO")).Text & "';")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnVisualizza').click();")
        End If
    End Sub

    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If IDM.Value <> "0" Then
            Session.Item("IDMAN") = IDM.Value
            Response.Write("<script>self.close();</script>")
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "chiudi('btnManutenzioneVis1');", True)

        End If
    End Sub
End Class
