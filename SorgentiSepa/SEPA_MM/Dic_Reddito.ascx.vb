
Partial Class Dic_Reddito
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub btnAggiungi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiungi.Click

        If Not Cache(Session.Item("GLista")) Is Nothing Then
            ListBox1.Items.Add(New ListItem(Cache.Get(Session.Item("GLista")).ToString(), Cache.Get(Session.Item("GProgressivo")).ToString()))
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GProgressivo"))
        End If


        'Dim I As Integer

        'CType(Dic_Dett_Redditi1.FindControl("L1"), Label).Visible = False
        'CType(Dic_Dett_Redditi1.FindControl("L2"), Label).Visible = False

        'CType(Dic_Dett_Redditi1.FindControl("cmbComponente"), DropDownList).Items.Clear()
        'For I = 0 To CType(Me.Parent.FindControl("cmbComp"), DropDownList).Items.Count - 1
        '    CType(Dic_Dett_Redditi1.FindControl("cmbComponente"), DropDownList).Items.Add(New ListItem(CType(Me.Parent.FindControl("cmbComp"), DropDownList).Items(I).Text, CType(Me.Parent.FindControl("cmbComp"), DropDownList).Items(I).Value))
        'Next
        'CType(Dic_Dett_Redditi1.FindControl("txtOperazione"), TextBox).Text = "0"
        'CType(Dic_Dett_Redditi1.FindControl("txtIrpef"), TextBox).Text = "0"
        'CType(Dic_Dett_Redditi1.FindControl("txtAgrari"), TextBox).Text = "0"

        'ListBox1.Visible = False
        'Dic_Dett_Redditi1.Visible = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
	    If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            btnAggiungi.Attributes.Add("OnClick", "javascript:AggiungiReddito();document.getElementById('H1').value='0';document.getElementById('txtModificato').value='1';")
            btnModifica.Attributes.Add("OnClick", "javascript:ModificaReddito();document.getElementById('H1').value='0';document.getElementById('txtModificato').value='1';")
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
        ListBox1.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Dic_Reddito1_ListBox1');   if (obj1.selectedIndex!=-1) {document.getElementById('Dic_Reddito1_V1').value=obj1.options[obj1.selectedIndex].text;}")

    End Sub

    Protected Sub btnModifica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModifica.Click

        If Not Cache(Session.Item("GLista")) Is Nothing Then
            ListBox1.Items(Cache.Get(Session.Item("GRiga")).ToString()).Text = Cache.Get(Session.Item("GLista")).ToString()
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GRiga"))

        End If

        'If ListBox1.SelectedIndex >= 0 Then
        '    ListBox1.Visible = False
        '    CType(Dic_Dett_Redditi1.FindControl("L1"), Label).Visible = False
        '    CType(Dic_Dett_Redditi1.FindControl("L2"), Label).Visible = False
        '    CType(Dic_Dett_Redditi1.FindControl("txtirpef"), TextBox).Text = par.RicavaTesto(ListBox1.SelectedItem.Text, 37, 15)
        '    CType(Dic_Dett_Redditi1.FindControl("txtAgrari"), TextBox).Text = par.RicavaTesto(ListBox1.SelectedItem.Text, 56, 15)

        '    CType(Dic_Dett_Redditi1.FindControl("txtOperazione"), TextBox).Text = "1"
        '    CType(Dic_Dett_Redditi1.FindControl("txtRiga"), TextBox).Text = ListBox1.SelectedIndex

        '    CType(Dic_Dett_Redditi1.FindControl("cmbComponente"), DropDownList).SelectedIndex = -1
        '    CType(Dic_Dett_Redditi1.FindControl("cmbComponente"), DropDownList).Items.FindByValue(ListBox1.SelectedItem.Value).Selected = True

        '    Dic_Dett_Redditi1.Visible = True
        'Else
        '    Response.Write("<SCRIPT>alert('Selezionare un componente del nucleo!');</SCRIPT>")
        'End If
    End Sub

    Protected Sub btnElimina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnElimina.Click

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
        btnAggiungi.Enabled = False
        btnModifica.Enabled = False
        btnElimina.Enabled = False
    End Sub
End Class