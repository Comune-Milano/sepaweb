
Partial Class VSA_NuovaDomandaVSA_Tab_Note
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Public Property idCOMPMOB() As String
        Get
            If Not (ViewState("par_idCOMP_1") Is Nothing) Then
                Return CStr(ViewState("par_idCOMP_1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_idCOMP_1") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Button1.Attributes.Add("OnClick", "javascript:AggiungiDocumento();document.getElementById('H1').value='0';")
            txtDataDocManc.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Button2.Attributes.Add("OnClick", "javascript:document.getElementById('H1').value='0';")
            txtDataSoprall.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            If CType(Me.Page, Object).lIdDichiarazione <> "-1" Then
                Me.idDic.Value = CType(Me.Page, Object).lIdDichiarazione
            End If


            '**********modifiche campi***********
            Dim CTRL As Control
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                End If
            Next
            If ListBox1.Items.Count > 0 And chkDocManc.Checked = False Then
                documMancante.Value = 1
            Else
                documMancante.Value = 0
            End If
        End If
        ListBox1.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Dom_Note1_ListBox1');if (obj1.selectedIndex!=-1) {document.getElementById('Dom_Note1_V2').value=obj1.options[obj1.selectedIndex].text;}")

    End Sub
    Public Sub DisattivaTutto()
        txtNote.Enabled = False
        Button1.Enabled = False
        'Button2.Enabled = False
        ListBox1.Enabled = False
        txtDataDocManc.Enabled = False
        chkDocManc.Enabled = False
        chkSosp.Enabled = False
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not Cache(Session.Item("GLista")) Is Nothing Then
            ListBox1.Items.Add(New ListItem(Cache.Get(Session.Item("GLista")).ToString(), Cache.Get(Session.Item("GProgressivo")).ToString()))
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GProgressivo"))
            documMancante.Value = 1
            CType(Page.FindControl("txtModificato"), HiddenField).Value = "1"

        End If

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
        'documMancante.Value = 0

        If ListBox1.SelectedIndex >= 0 Then

            Dim i As Integer
            For i = 0 To ListBox1.Items.Count - 1
                If Trim(par.Elimina160(ListBox1.Items(i).Text)) = Trim(par.Elimina160(V2.Value)) Then

                    If idCOMPMOB = "" Then
                        idCOMPMOB = idCOMPMOB & " and (id_doc=" & ListBox1.Items(i).Value
                    Else
                        idCOMPMOB = idCOMPMOB & " or id_doc=" & ListBox1.Items(i).Value
                    End If
                    ListBox1.Items.Remove(ListBox1.Items(i))
                    Exit Sub
                End If
            Next

            If ListBox1.Items.Count = 0 Then
                documMancante.Value = 0
            End If
            'ListBox1.Items.Remove(ListBox1.SelectedItem)
        Else
            Response.Write("<SCRIPT>alert('Selezionare un elemento della lista!');</SCRIPT>")
        End If
    End Sub
End Class

