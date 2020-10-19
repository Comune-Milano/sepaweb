
Partial Class Dic_Integrazione
    Inherits UserControlSetIdMode
    Dim par As New CM.Global


    Protected Sub btnAgg1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgg1.Click
        If Not Cache(Session.Item("GLista")) Is Nothing Then
            ListBox1.Items.Add(New ListItem(Cache.Get(Session.Item("GLista")).ToString(), Cache.Get(Session.Item("GProgressivo")).ToString()))
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GProgressivo"))
        End If
        'Dim I As Integer

        'CType(Dic_Dett_AltriRedditi1.FindControl("L3"), Label).Visible = False
        'CType(Dic_Dett_AltriRedditi1.FindControl("cmbComponente"), DropDownList).Items.Clear()
        'For I = 0 To CType(Me.Parent.FindControl("cmbComp"), DropDownList).Items.Count - 1
        '    CType(Dic_Dett_AltriRedditi1.FindControl("cmbComponente"), DropDownList).Items.Add(New ListItem(CType(Me.Parent.FindControl("cmbComp"), DropDownList).Items(I).Text, CType(Me.Parent.FindControl("cmbComp"), DropDownList).Items(I).Value))
        'Next
        'CType(Dic_Dett_AltriRedditi1.FindControl("txtOperazione"), TextBox).Text = "0"
        'CType(Dic_Dett_AltriRedditi1.FindControl("txtimporto"), TextBox).Text = "0"



        'ListBox1.Visible = False
        'ListBox2.Visible = False
        'Dic_Dett_AltriRedditi1.Visible = True
    End Sub

    Protected Sub btnModifica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModifica.Click
        If Not Cache(Session.Item("GLista")) Is Nothing Then
            ListBox1.Items(Cache.Get(Session.Item("GRiga")).ToString()).Text = Cache.Get(Session.Item("GLista")).ToString()
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GRiga"))
        End If
        'If ListBox1.SelectedIndex >= 0 Then
        '    ListBox1.Visible = False
        '    ListBox2.Visible = False

        '    CType(Dic_Dett_AltriRedditi1.FindControl("L3"), Label).Visible = False
        '    CType(Dic_Dett_AltriRedditi1.FindControl("txtimporto"), TextBox).Text = par.RicavaTesto(ListBox1.SelectedItem.Text, 52, 8)

        '    CType(Dic_Dett_AltriRedditi1.FindControl("txtOperazione"), TextBox).Text = "1"
        '    CType(Dic_Dett_AltriRedditi1.FindControl("txtRiga"), TextBox).Text = ListBox1.SelectedIndex

        '    CType(Dic_Dett_AltriRedditi1.FindControl("cmbComponente"), DropDownList).SelectedIndex = -1
        '    CType(Dic_Dett_AltriRedditi1.FindControl("cmbComponente"), DropDownList).Items.FindByValue(ListBox1.SelectedItem.Value).Selected = True

        '    Dic_Dett_AltriRedditi1.Visible = True
        'Else
        '    Response.Write("<SCRIPT>alert('Selezionare un componente del nucleo!');</SCRIPT>")
        'End If
    End Sub


    Protected Sub btnElimina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnElimina.Click
        If ListBox1.SelectedIndex >= 0 Then
            'ListBox1.Items.Remove(ListBox1.SelectedItem)
            Dim i As Integer
            For i = 0 To ListBox1.Items.Count - 1
                If Trim(par.Elimina160(ListBox1.Items(i).Text)) = Trim(par.Elimina160(V2.Value)) Then
                    ListBox1.Items.Remove(ListBox1.Items(i))
                    Exit Sub
                End If
            Next
        Else
            Response.Write("<SCRIPT>alert('Selezionare un componente della lista!');</SCRIPT>")
        End If
    End Sub

    Protected Sub btnAggiungi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiungi.Click
        If Not Cache(Session.Item("GLista")) Is Nothing Then
            ListBox2.Items.Add(New ListItem(Cache.Get(Session.Item("GLista")).ToString(), Cache.Get(Session.Item("GProgressivo")).ToString()))
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GProgressivo"))
        End If
        'Dim I As Integer

        'CType(Dic_Dett_Detrazioni1.FindControl("L3"), Label).Visible = False

        'CType(Dic_Dett_Detrazioni1.FindControl("cmbComponente"), DropDownList).Items.Clear()
        'For I = 0 To CType(Me.Parent.FindControl("cmbComp"), DropDownList).Items.Count - 1
        '    CType(Dic_Dett_Detrazioni1.FindControl("cmbComponente"), DropDownList).Items.Add(New ListItem(CType(Me.Parent.FindControl("cmbComp"), DropDownList).Items(I).Text, CType(Me.Parent.FindControl("cmbComp"), DropDownList).Items(I).Value))
        'Next
        'CType(Dic_Dett_Detrazioni1.FindControl("txtOperazione"), TextBox).Text = "0"
        'CType(Dic_Dett_Detrazioni1.FindControl("txtimporto"), TextBox).Text = "0"

        'CType(Dic_Dett_Detrazioni1.FindControl("cmbDetrazione"), DropDownList).SelectedIndex = -1
        'CType(Dic_Dett_Detrazioni1.FindControl("cmbDetrazione"), DropDownList).Items.FindByText("IRPEF").Selected = True


        'ListBox1.Visible = False
        'ListBox2.Visible = False
        'Dic_Dett_Detrazioni1.Visible = True
    End Sub


    Protected Sub btnMod_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMod.Click
        If Not Cache(Session.Item("GLista")) Is Nothing Then
            ListBox2.Items(Cache.Get(Session.Item("GRiga")).ToString()).Text = Cache.Get(Session.Item("GLista")).ToString()
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GRiga"))
        End If
        'If ListBox2.SelectedIndex >= 0 Then
        '    ListBox1.Visible = False
        '    ListBox2.Visible = False

        '    CType(Dic_Dett_Detrazioni1.FindControl("L3"), Label).Visible = False
        '    CType(Dic_Dett_Detrazioni1.FindControl("txtimporto"), TextBox).Text = par.RicavaTesto(ListBox2.SelectedItem.Text, 68, 8)

        '    CType(Dic_Dett_Detrazioni1.FindControl("txtOperazione"), TextBox).Text = "1"
        '    CType(Dic_Dett_Detrazioni1.FindControl("txtRiga"), TextBox).Text = ListBox2.SelectedIndex

        '    CType(Dic_Dett_Detrazioni1.FindControl("cmbComponente"), DropDownList).SelectedIndex = -1
        '    CType(Dic_Dett_Detrazioni1.FindControl("cmbComponente"), DropDownList).Items.FindByValue(ListBox2.SelectedItem.Value).Selected = True

        '    CType(Dic_Dett_Detrazioni1.FindControl("cmbDetrazione"), DropDownList).SelectedIndex = -1
        '    CType(Dic_Dett_Detrazioni1.FindControl("cmbDetrazione"), DropDownList).Items.FindByText(par.RicavaTesto(ListBox2.SelectedItem.Text, 32, 35)).Selected = True

        '    Dic_Dett_Detrazioni1.Visible = True
        'Else
        '    Response.Write("<SCRIPT>alert('Selezionare un componente della lista!');</SCRIPT>")
        'End If
    End Sub


    Protected Sub btnCanc1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanc1.Click
        If ListBox2.SelectedIndex >= 0 Then
            'ListBox2.Items.Remove(ListBox2.SelectedItem)
            Dim i As Integer
            For i = 0 To ListBox2.Items.Count - 1
                If Trim(par.Elimina160(ListBox2.Items(i).Text)) = Trim(par.Elimina160(V1.Value)) Then
                    ListBox2.Items.Remove(ListBox2.Items(i))
                    Exit Sub
                End If
            Next
        Else
            Response.Write("<SCRIPT>alert('Selezionare un componente della lista!');</SCRIPT>")
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            btnAgg1.Attributes.Add("OnClick", "javascript:AggiungiInt();")
            btnModifica.Attributes.Add("OnClick", "javascript:ModificaInt();")
            btnAggiungi.Attributes.Add("OnClick", "javascript:AggiungiDet();")
            btnMod.Attributes.Add("OnClick", "javascript:ModificaDet();")
        End If
        ListBox2.Attributes.Add("OnClick", "javascript:obj2=document.getElementById('Dic_Integrazione1_ListBox2');if (obj2.selectedIndex!=-1) {document.getElementById('Dic_Integrazione1_V1').value=obj2.options[obj2.selectedIndex].text;}")
        ListBox1.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Dic_Integrazione1_ListBox1');if (obj1.selectedIndex!=-1) {document.getElementById('Dic_Integrazione1_V2').value=obj1.options[obj1.selectedIndex].text;}")

        Dim CTRL As Control
        For Each CTRL In Me.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is Button Then
                DirectCast(CTRL, Button).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            End If
        Next
    End Sub

    Public Sub DisattivaTutto()
        btnAgg1.Enabled = False
        btnModifica.Enabled = False
        btnElimina.Enabled = False
        btnAggiungi.Enabled = False
        btnMod.Enabled = False
        btnCanc1.Enabled = False
        txtData1.Enabled = False
    End Sub
End Class