
Partial Class Dic_Patrimonio
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not Cache(Session.Item("GLista")) Is Nothing Then
            ListBox1.Items.Add(New ListItem(Cache.Get(Session.Item("GLista")).ToString(), Cache.Get(Session.Item("GProgressivo")).ToString()))
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GProgressivo"))
            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
        End If

        'Dim I As Integer

        'CType(Dic_Dett_Mob1.FindControl("L1"), Label).Visible = False
        'CType(Dic_Dett_Mob1.FindControl("L2"), Label).Visible = False
        'CType(Dic_Dett_Mob1.FindControl("L3"), Label).Visible = False


        'CType(Dic_Dett_Mob1.FindControl("cmbComponente"), DropDownList).Items.Clear()
        'For I = 0 To CType(Me.Parent.FindControl("cmbComp"), DropDownList).Items.Count - 1
        '    CType(Dic_Dett_Mob1.FindControl("cmbComponente"), DropDownList).Items.Add(New ListItem(CType(Me.Parent.FindControl("cmbComp"), DropDownList).Items(I).Text, CType(Me.Parent.FindControl("cmbComp"), DropDownList).Items(I).Value))
        'Next
        'CType(Dic_Dett_Mob1.FindControl("txtOperazione"), TextBox).Text = "0"
        'CType(Dic_Dett_Mob1.FindControl("txtabi"), TextBox).Text = ""
        'CType(Dic_Dett_Mob1.FindControl("txtcab"), TextBox).Text = ""
        'CType(Dic_Dett_Mob1.FindControl("txtcin"), TextBox).Text = ""
        'CType(Dic_Dett_Mob1.FindControl("txtinter"), TextBox).Text = ""
        'CType(Dic_Dett_Mob1.FindControl("txtimporto"), TextBox).Text = "0"

        'ListBox1.Visible = False
        'ListBox2.Visible = False
        'Dic_Dett_Mob1.Visible = True
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If	
        If Not IsPostBack Then
            annoRedd.Value = CType(Me.Page, Object).annoRedd
            Button1.Attributes.Add("OnClick", "javascript:AggiungiPatrimonio();document.getElementById('H1').value='0';")
            btnModifica.Attributes.Add("OnClick", "javascript:ModificaPatrimonio();document.getElementById('H1').value='0';")
            btnAggiungi.Attributes.Add("OnClick", "javascript:AggiungiPatrimonioI();document.getElementById('H1').value='0';")
            btnMod.Attributes.Add("OnClick", "javascript:ModificaPatrimonioI();document.getElementById('H1').value='0';")
            Button2.Attributes.Add("OnClick", "javascript:document.getElementById('H1').value='0';")
            Button6.Attributes.Add("OnClick", "javascript:document.getElementById('H1').value='0';")
            'V1.Text = ListBox2.Items(0).Text


            '**********modifiche campi***********
            Dim CTRL As Control
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                ElseIf TypeOf CTRL Is ListBox Then
                    DirectCast(CTRL, ListBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                ElseIf TypeOf CTRL Is Button Then
                    DirectCast(CTRL, Button).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                End If
            Next
        End If

        ListBox2.Attributes.Add("OnClick", "javascript:obj2=document.getElementById('Dic_Patrimonio1_ListBox2');if (obj2.selectedIndex!=-1) {document.getElementById('Dic_Patrimonio1_V1').value=obj2.options[obj2.selectedIndex].value;}")
        ListBox1.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Dic_Patrimonio1_ListBox1');if (obj1.selectedIndex!=-1) {document.getElementById('Dic_Patrimonio1_V2').value=obj1.options[obj1.selectedIndex].text;}")

    End Sub

    Protected Sub btnModifica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModifica.Click
        If Not Cache(Session.Item("GLista")) Is Nothing Then
            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

            ListBox1.Items(Cache.Get(Session.Item("GRiga")).ToString()).Text = Cache.Get(Session.Item("GLista")).ToString()

            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GRiga"))
        Else
            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

        End If
        'If ListBox1.SelectedIndex >= 0 Then
        '    ListBox1.Visible = False
        '    ListBox2.Visible = False
        '    CType(Dic_Dett_Mob1.FindControl("L1"), Label).Visible = False
        '    CType(Dic_Dett_Mob1.FindControl("L2"), Label).Visible = False
        '    CType(Dic_Dett_Mob1.FindControl("L3"), Label).Visible = False

        '    CType(Dic_Dett_Mob1.FindControl("txtabi"), TextBox).Text = par.RicavaTesto(ListBox1.SelectedItem.Text, 27, 5)
        '    CType(Dic_Dett_Mob1.FindControl("txtcab"), TextBox).Text = par.RicavaTesto(ListBox1.SelectedItem.Text, 33, 5)
        '    CType(Dic_Dett_Mob1.FindControl("txtcin"), TextBox).Text = par.RicavaTesto(ListBox1.SelectedItem.Text, 39, 1)
        '    CType(Dic_Dett_Mob1.FindControl("txtinter"), TextBox).Text = par.RicavaTesto(ListBox1.SelectedItem.Text, 41, 30)
        '    CType(Dic_Dett_Mob1.FindControl("txtimporto"), TextBox).Text = par.RicavaTesto(ListBox1.SelectedItem.Text, 72, 8)

        '    CType(Dic_Dett_Mob1.FindControl("txtOperazione"), TextBox).Text = "1"
        '    CType(Dic_Dett_Mob1.FindControl("txtRiga"), TextBox).Text = ListBox1.SelectedIndex

        '    CType(Dic_Dett_Mob1.FindControl("cmbComponente"), DropDownList).SelectedIndex = -1
        '    CType(Dic_Dett_Mob1.FindControl("cmbComponente"), DropDownList).Items.FindByValue(ListBox1.SelectedItem.Value).Selected = True

        '    Dic_Dett_Mob1.Visible = True
        'Else
        '    Response.Write("<SCRIPT>alert('Selezionare un componente del nucleo!');</SCRIPT>")
        'End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListBox1.SelectedIndex >= 0 Then
            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

            Dim i As Integer
            For i = 0 To ListBox1.Items.Count - 1
                If Trim(par.Elimina160(ListBox1.Items(i).Text)) = Trim(par.Elimina160(V2.Value)) Then

                    If idCOMPMOB = "" Then
                        idCOMPMOB = idCOMPMOB & "  (progr=" & ListBox1.Items(i).Value
                    Else
                        idCOMPMOB = idCOMPMOB & " or progr=" & ListBox1.Items(i).Value
                    End If

                    ListBox1.Items.Remove(ListBox1.Items(i))
                    Exit Sub
                End If
            Next



            'ListBox1.Items.Remove(ListBox1.SelectedItem)
        Else
            Response.Write("<SCRIPT>alert('Selezionare un componente della lista!');</SCRIPT>")
        End If
    End Sub


    Protected Sub btnAggiungi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiungi.Click

        If Not Cache(Session.Item("GLista")) Is Nothing Then

            ListBox2.Items.Add(New ListItem(Cache.Get(Session.Item("GLista")).ToString(), Cache.Get(Session.Item("GProgressivo")).ToString()))

            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GProgressivo"))

            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

        Else
            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
        End If

    End Sub



    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
        If ListBox2.SelectedIndex >= 0 Then
            Dim i As Integer
            For i = 0 To ListBox2.Items.Count - 1
                If Trim(par.Elimina160(ListBox2.Items(i).Value)) = Trim(par.Elimina160(V1.Value)) Then
                    ListBox2.Items.Remove(ListBox2.Items(i))
                    Exit Sub
                End If
            Next
            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

        Else
            Response.Write("<SCRIPT>alert('Selezionare un componente della lista!');</SCRIPT>")
        End If
    End Sub

    Protected Sub btnMod_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMod.Click
        If Not Cache(Session.Item("GLista")) Is Nothing Then
            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

            ListBox2.Items(Cache.Get(Session.Item("GRiga")).ToString()).Text = Cache.Get(Session.Item("GLista")).ToString()

            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GRiga"))
        Else
            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

        End If
        'If ListBox2.SelectedIndex >= 0 Then
        '    ListBox1.Visible = False
        '    ListBox2.Visible = False
        '    CType(Dic_Dett_Imm1.FindControl("txtperc"), TextBox).Text = par.RicavaTesto(ListBox2.SelectedItem.Text, 48, 6)
        '    CType(Dic_Dett_Imm1.FindControl("txtvalore"), TextBox).Text = par.RicavaTesto(ListBox2.SelectedItem.Text, 55, 8)
        '    CType(Dic_Dett_Imm1.FindControl("txtmutuo"), TextBox).Text = par.RicavaTesto(ListBox2.SelectedItem.Text, 67, 8)

        '    CType(Dic_Dett_Imm1.FindControl("txtOperazione"), TextBox).Text = "1"
        '    CType(Dic_Dett_Imm1.FindControl("txtRiga"), TextBox).Text = ListBox2.SelectedIndex

        '    CType(Dic_Dett_Imm1.FindControl("cmbComponente"), DropDownList).SelectedIndex = -1
        '    CType(Dic_Dett_Imm1.FindControl("cmbComponente"), DropDownList).Items.FindByValue(ListBox2.SelectedItem.Value).Selected = True

        '    CType(Dic_Dett_Imm1.FindControl("cmbTipo"), DropDownList).SelectedIndex = -1
        '    CType(Dic_Dett_Imm1.FindControl("cmbTipo"), DropDownList).Items.FindByText(par.RicavaTesto(ListBox2.SelectedItem.Text, 27, 20)).Selected = True

        '    CType(Dic_Dett_Imm1.FindControl("cmbResidenza"), DropDownList).SelectedIndex = -1
        '    CType(Dic_Dett_Imm1.FindControl("cmbResidenza"), DropDownList).Items.FindByText(par.RicavaTesto(ListBox2.SelectedItem.Text, 79, 2)).Selected = True

        '    Dic_Dett_Imm1.Visible = True
        'Else
        '    Response.Write("<SCRIPT>alert('Selezionare un componente del nucleo!');</SCRIPT>")
        'End If
    End Sub

    Public Sub DisattivaTutto()
        Button1.Enabled = False
        btnModifica.Enabled = False
        Button2.Enabled = False
        btnAggiungi.Enabled = False
        btnMod.Enabled = False
        Button6.Enabled = False
    End Sub

    Public Function RicavaidCOMPMOB() As Long
        RicavaidCOMPMOB = idCOMPMOB
    End Function

    Public Property idCOMPMOB() As String
        Get
            If Not (ViewState("par_idCOMP_PATR_MOB") Is Nothing) Then
                Return CStr(ViewState("par_idCOMP_PATR_MOB"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_idCOMP_PATR_MOB") = value
        End Set

    End Property
End Class