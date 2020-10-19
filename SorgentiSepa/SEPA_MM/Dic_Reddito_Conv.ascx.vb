
Partial Class Dich_Reddito_Conv
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If	
        If Not IsPostBack Then
            btnAgg1.Attributes.Add("OnClick", "javascript:AggiungiRedditoConv();document.getElementById('H1').value='0';document.getElementById('txtModificato').value='1';")
            btnModifica.Attributes.Add("OnClick", "javascript:ModificaRedditoConv();document.getElementById('H1').value='0';document.getElementById('txtModificato').value='1';")
            btnElimina.Attributes.Add("OnClick", "javascript:document.getElementById('H1').value='0';document.getElementById('txtModificato').value='1';")

            '**********modifiche campi***********
            Dim CTRL As Control
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is ListBox Then
                    DirectCast(CTRL, ListBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                End If
            Next

        End If
        ListBox1.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Dic_Reddito_Conv1_ListBox1');if (obj1.selectedIndex!=-1) {document.getElementById('Dic_Reddito_Conv1_V1').value=obj1.options[obj1.selectedIndex].text;}")
        Label12.Visible = False
        txtMinori.Visible = False

    End Sub

    Protected Sub btnAgg1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgg1.Click
        If Not Cache(Session.Item("GLista")) Is Nothing Then
            ListBox1.Items.Add(New ListItem(Cache.Get(Session.Item("GLista")).ToString(), Cache.Get(Session.Item("GProgressivo")).ToString()))
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GProgressivo"))
            'CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

        End If
    End Sub

    Protected Sub btnModifica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModifica.Click
        If Not Cache(Session.Item("GLista")) Is Nothing Then
            ListBox1.Items.Remove(ListBox1.SelectedItem)
            ListBox1.Items.Add(New ListItem(Cache.Get(Session.Item("GLista")).ToString(), Cache.Get(Session.Item("GProgressivo")).ToString()))

            'ListBox1.Items(Cache.Get(Session.Item("GRiga")).ToString()).Text = Cache.Get(Session.Item("GLista")).ToString()
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GRiga"))
            'CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

        End If
    End Sub

    Protected Sub btnElimina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnElimina.Click
        'CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

        If ListBox1.SelectedIndex >= 0 Then
            'ListBox1.Items.Remove(ListBox1.SelectedItem)
            Dim i As Integer
            For i = 0 To ListBox1.Items.Count - 1
                If Trim(par.Elimina160(ListBox1.Items(i).Text)) = Trim(par.Elimina160(V1.Value)) Then
                    ListBox1.Items.Remove(ListBox1.Items(i))
                    Exit Sub
                End If
            Next

        Else
            Response.Write("<SCRIPT>alert('Selezionare un componente della lista!');</SCRIPT>")
        End If
    End Sub

    Public Sub DisattivaTutto()
        btnAgg1.Enabled = False
        btnModifica.Enabled = False
        btnElimina.Enabled = False
        txtMinori.Enabled = False
    End Sub
End Class
