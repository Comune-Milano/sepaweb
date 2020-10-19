' TAB RIEPILOGO GENERALE DELLA MANUTENZIONE

Partial Class Tab_Manu_Riepilogo
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then


            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If
        End If
    End Sub


    Private Sub FrmSolaLettura()
        Try

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub




    Protected Sub btnINFO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnINFO.Click

        If Me.HLink_Appalto.Text <> "" Then
            Response.Write("<script>window.open('../APPALTI/AppaltiNP.aspx?IDA=" & CType(Me.Page.FindControl("txtIdAppalto"), HiddenField).Value & "&LE=1','Appalto','height=550,width=800');</script>")

        End If
        'CType(Tab_Manu_Riepilogo.FindControl("txtAppalto"), Label).Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../APPALTI/Appalti.aspx?A=" & par.IfNull(myReader1("ID_APPALTO"), -1) & "&IDL=" & Id_Lotto & " ','Appalto','height=550,width=800');" & Chr(34) & ">" & Strings.Left(par.PulisciStrSql(par.IfNull(myReader1("APPALTO"), "")), 20) & "..." & "</a>"

    End Sub

    Protected Sub DataGridAppalti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridAppalti.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Manu_Riepilogo_txtSel1').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Manu_Riepilogo_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Manu_Riepilogo_txtSel1').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Manu_Riepilogo_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")
            'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Manu_Dettagli_txtSel1').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Manu_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub


End Class
