
Partial Class ANAUT_Tab_Patrimonio
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

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
                DirectCast(CTRL, DataGrid).Attributes.Add("onload", "javascript:document.getElementById('idCompMob').value='0'; document.getElementById('idCompImmob').value='0';")

            End If
        Next

        btnDeleteMob.Attributes.Add("OnClick", "javascript:document.getElementById('provenienza').value='pmobile';")
        imgDeleteImmob.Attributes.Add("OnClick", "javascript:document.getElementById('provenienza').value='pimmobile';")



    End Sub

    Protected Sub DataGridMob_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridMob.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#DFDFDF';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#C1C1C1';" _
                                & "document.getElementById('Dic_Patrimonio1_idCompMob').value='" & e.Item.Cells(0).Text & "';document.getElementById('Dic_Patrimonio1_idPatrMob').value='" & e.Item.Cells(1).Text & "';document.getElementById('Dic_Patrimonio1_abi').value='" & e.Item.Cells(5).Text & "';document.getElementById('Dic_Patrimonio1_inter').value='" & Replace(e.Item.Cells(6).Text, "'", "\'") & "';document.getElementById('Dic_Patrimonio1_tipo').value='" & Replace(e.Item.Cells(4).Text, "'", "\'") & "';document.getElementById('Dic_Patrimonio1_importo').value='" & e.Item.Cells(7).Text & "';document.getElementById('Dic_Patrimonio1_prp').value='" & e.Item.Cells(8).Text & "';")
        End If
    End Sub

    Protected Sub DataGridImmob_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridImmob.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#DFDFDF';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#C1C1C1';" _
                                & "document.getElementById('Dic_Patrimonio1_idCompImmob').value='" & e.Item.Cells(0).Text & "';document.getElementById('Dic_Patrimonio1_idPatrImmob').value='" & e.Item.Cells(1).Text & "';document.getElementById('Dic_Patrimonio1_tipoImmob').value='" & par.IfEmpty(e.Item.Cells(4).Text, "") & "';document.getElementById('Dic_Patrimonio1_tipoPropr').value='" & Replace(e.Item.Cells(5).Text, "'", "\'") & "';document.getElementById('Dic_Patrimonio1_perc').value='" & e.Item.Cells(9).Text & "';document.getElementById('Dic_Patrimonio1_valore').value='" & e.Item.Cells(6).Text & "';document.getElementById('Dic_Patrimonio1_mutuo').value='" & e.Item.Cells(7).Text & "';document.getElementById('Dic_Patrimonio1_catastale').value='" & e.Item.Cells(12).Text & "';document.getElementById('Dic_Patrimonio1_comune').value='" & Replace(e.Item.Cells(11).Text, "'", "\'") & "';document.getElementById('Dic_Patrimonio1_vani').value='" & e.Item.Cells(10).Text & "';document.getElementById('Dic_Patrimonio1_sup').value='" & e.Item.Cells(8).Text & "';document.getElementById('Dic_Patrimonio1_rendita').value='" & par.IfEmpty(e.Item.Cells(13).Text, "") & "';document.getElementById('Dic_Patrimonio1_valoreM').value='" & par.IfEmpty(e.Item.Cells(14).Text, "") & "';")
        End If
    End Sub

    Protected Sub btnDeleteMob_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDeleteMob.Click
        idCompImmob.Value = "0"
        If idCompMob.Value = "0" Then
            CType(Me.Page, Object).avviso()
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "Verifica();", True)
        End If
    End Sub

    Protected Sub imgDeleteImmob_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgDeleteImmob.Click
        idCompMob.Value = "0"
        If idCompImmob.Value = "-1" Then
            CType(Me.Page, Object).avviso()
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "Verifica();", True)
        End If
    End Sub
End Class
