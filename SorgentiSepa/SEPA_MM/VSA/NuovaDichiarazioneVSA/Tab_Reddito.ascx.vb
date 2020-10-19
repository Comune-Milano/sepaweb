
Partial Class ANAUT_Tab_Reddito
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim CTRL As Control
        For Each CTRL In Me.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is DataGrid Then
                DirectCast(CTRL, DataGrid).Attributes.Add("onload", "javascript:document.getElementById('idCompReddito').value=''; document.getElementById('idCompDetraz').value='';")

            End If
        Next

        btnDeleteRedd.Attributes.Add("OnClick", "javascript:document.getElementById('provenienza').value='reddito';")
        imgDeleteRedd.Attributes.Add("OnClick", "javascript:document.getElementById('provenienza').value='detrazioni';")
    End Sub

    Protected Sub DataGridRedditi_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridRedditi.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#DFDFDF';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#C1C1C1';" _
                                & "document.getElementById('Dic_Reddito1_idCompReddito').value='" & e.Item.Cells(0).Text & "';document.getElementById('Dic_Reddito1_idReddito').value='" & e.Item.Cells(1).Text & "';document.getElementById('Dic_Reddito1_idCompDetraz').value=''")
        End If
    End Sub

    Protected Sub DataGridDetraz_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDetraz.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#DFDFDF';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#C1C1C1';" _
                                & "document.getElementById('Dic_Reddito1_idCompDetraz').value='" & e.Item.Cells(0).Text & "';document.getElementById('Dic_Reddito1_idDetraz').value='" & e.Item.Cells(1).Text & "';document.getElementById('Dic_Reddito1_importo').value='" & e.Item.Cells(4).Text & "';document.getElementById('Dic_Reddito1_tipoDetraz').value='" & e.Item.Cells(5).Text & "';document.getElementById('Dic_Reddito1_idCompReddito').value=''")
        End If

    End Sub

    Protected Sub imgDeleteRedd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgDeleteRedd.Click
        idCompReddito.Value = ""
        If idCompDetraz.Value = "" Then
            CType(Me.Page, Object).avviso()
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "Verifica();", True)

        End If
    End Sub

    Protected Sub btnDeleteRedd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDeleteRedd.Click
        idCompDetraz.Value = ""
        If idCompReddito.Value = "" Then
            CType(Me.Page, Object).avviso()
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "Verifica();", True)

        End If
    End Sub

    Protected Sub btnCancella_Click(sender As Object, e As System.EventArgs) Handles btnCancella.Click
        CType(Me.Page, Object).EliminaRedditi()
    End Sub
End Class
