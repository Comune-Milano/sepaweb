
Partial Class ANAUT_Tab_Nucleo
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        imgAggComp.Attributes.Add("OnClick", "javascript:AggiungiNucleo();document.getElementById('H1').value='0';")
        ImgModifica.Attributes.Add("OnClick", "javascript:ModificaNucleo();document.getElementById('H1').value='0';")
        btnDelete.Attributes.Add("OnClick", "javascript:EliminaSoggetto();document.getElementById('provenienza').value='nucleo';document.getElementById('H1').value='0';")
        ImgModifySpese.Attributes.Add("OnClick", "javascript:ModificaSpese();document.getElementById('H1').value='0';")

        Dim CTRL As Control
        For Each CTRL In Me.Controls
           If TypeOf CTRL Is DataGrid Then
                DirectCast(CTRL, DataGrid).Attributes.Add("onload", "javascript:document.getElementById('idComp').value='0';")
         
            End If
        Next

        lblEliminati.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "document.getElementById('H1').value='0';window.showModalDialog('../../VSA/ElencoEliminati.aspx?IDDOM=" & CType(Me.Page, Object).lIdDomanda & "','ElencoEliminati','status:no;dialogWidth:670px;dialogHeight:420px;dialogHide:true;help:no;scroll:no');" & Chr(34) & ">Clicca qui per visualizzare l'elenco dei componenti eliminati</a>"

        
    End Sub

    Protected Sub DataGridComponenti_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridComponenti.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#DFDFDF';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#C1C1C1';" _
                                & "document.getElementById('idComp').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_Nucleo1_cognome').value='" & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Nucleo1_nome').value='" & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('Tab_Nucleo1_data_nasc').value='" & e.Item.Cells(3).Text & "';" _
                                & "document.getElementById('Tab_Nucleo1_cod_fiscale').value='" & e.Item.Cells(4).Text & "';document.getElementById('Tab_Nucleo1_parentela').value='" & e.Item.Cells(5).Text & "';document.getElementById('Tab_Nucleo1_perc_inval').value='" & e.Item.Cells(6).Text & "';document.getElementById('Tab_Nucleo1_asl').value='" & e.Item.Cells(7).Text & "';" _
                                & "document.getElementById('Tab_Nucleo1_tipo_inval').value='" & e.Item.Cells(8).Text & "';document.getElementById('Tab_Nucleo1_natura_inval').value='" & e.Item.Cells(9).Text & "';document.getElementById('Tab_Nucleo1_ind_accomp').value='" & e.Item.Cells(10).Text & "';document.getElementById('Tab_Nucleo1_nuovoComp').value='" & e.Item.Cells(11).Text & "';document.getElementById('Tab_Nucleo1_dataIngr').value='" & e.Item.Cells(12).Text & "';")
        End If
    End Sub

    'Protected Sub btnDelete_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDelete.Click
    '    If CType(Me.Page.FindControl("idComp"), HiddenField).Value = "0" Then
    '        '  ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "alert('Selezionare un componente dalla lista!');", True)
    '        CType(Me.Page, Object).avviso()

    '    Else
    '        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "Verifica();", True)

    '        '   CType(Me.Page, Object).EliminaComponente()


    '    End If
    'End Sub



    Protected Sub DataGridSpese_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridSpese.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#DFDFDF';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#C1C1C1';" _
                                & "document.getElementById('Tab_Nucleo1_idCompSpese').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_Nucleo1_componente').value='" & Replace(e.Item.Cells(1).Text & " " & e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('Tab_Nucleo1_importo').value='" & e.Item.Cells(3).Text & "';" _
                                & "document.getElementById('Tab_Nucleo1_descrizione').value='" & e.Item.Cells(4).Text & "';")
        End If
    End Sub
End Class
