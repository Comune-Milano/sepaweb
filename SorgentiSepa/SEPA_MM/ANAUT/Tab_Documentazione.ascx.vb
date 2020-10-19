
Partial Class ANAUT_Tab_Documentazione
    Inherits UserControlSetIdMode

    Protected Sub DataGridDocum_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDocum.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#DFDFDF';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#C1C1C1';" _
                                & "document.getElementById('dic_Documenti1_idDoc').value='" & Replace(e.Item.Cells(0).Text, "'", "\'") & "'; document.getElementById('dic_Documenti1_descrizione').value='" & Replace(e.Item.Cells(2).Text, "'", "\'") & "';")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If CType(Me.Page.FindControl("inUscita"), HiddenField).Value = "1" Then
            Exit Sub
        End If
        imgAggDoc.Attributes.Add("OnClick", "javascript:AggiungiDocumento();")
        txtDataPresentaz.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        Dim CTRL As Control
        For Each CTRL In Me.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is DataGrid Then
                DirectCast(CTRL, DataGrid).Attributes.Add("onload", "javascript:document.getElementById('idDoc').value='-1';")
                DirectCast(CTRL, DataGrid).Attributes.Add("onload", "javascript:document.getElementById('idDocP').value='-1';")
            End If
        Next

        'btnDeleteDoc.Attributes.Add("OnClick", "javascript:document.getElementById('provenienza').value='doc';")


    End Sub

    Protected Sub btnDeleteDoc_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDeleteDoc.Click
        'If idDoc.Value = "-1" Then
        '    CType(Me.Page, Object).avviso()
        'Else
        '    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "Verifica();", True)
        'End If
    End Sub

    Protected Sub btnAggiungiDoc_Click(sender As Object, e As System.EventArgs) Handles btnAggiungiDoc.Click
        CaricaElencoDocumentazione()
    End Sub

    Private Sub CaricaElencoDocumentazione()
        CType(Me.Page, Object).CaricaDocumenti()
    End Sub

    Protected Sub DataGridDocum_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGridDocum.SelectedIndexChanged

    End Sub

    Protected Sub DataGridPresenti_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridPresenti.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#DFDFDF';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#C1C1C1';" _
                                & "document.getElementById('dic_Documenti1_idDocP').value='" & Replace(e.Item.Cells(0).Text, "'", "\'") & "'; document.getElementById('dic_Documenti1_descrizione').value='" & Replace(e.Item.Cells(2).Text, "'", "\'") & "';")
        End If
    End Sub

    Protected Sub DataGridPresenti_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGridPresenti.SelectedIndexChanged

    End Sub
End Class
