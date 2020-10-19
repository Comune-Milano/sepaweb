
Partial Class dic_Documenti
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Button1.Attributes.Add("OnClick", "javascript:AggiungiDocumento();")
        End If
        ListBox1.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Dic_Documenti1_ListBox1');if (obj1.selectedIndex!=-1) {document.getElementById('Dic_Documenti1_V2').value=obj1.options[obj1.selectedIndex].text;}")
    End Sub

    Public Sub DisattivaTutto()
        Button1.Enabled = False
        Button2.Enabled = False
        ListBox1.Enabled = False

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not Cache(Session.Item("GLista")) Is Nothing Then
            ListBox1.Items.Add(New ListItem(Cache.Get(Session.Item("GLista")).ToString(), Cache.Get(Session.Item("GProgressivo")).ToString()))
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GProgressivo"))
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListBox1.SelectedIndex >= 0 Then

            Dim i As Integer
            For i = 0 To ListBox1.Items.Count - 1
                If Replace(Trim(Replace(ListBox1.Items(i).Text, Chr(160), "")), " ", "") = Replace(Trim(Replace(V2.Value, Chr(160), "")), " ", "") Then

                    ListBox1.Items.Remove(ListBox1.Items(i))
                    Exit Sub
                End If
            Next
            Response.Write("<script>alert('Non riesco a cancellare!');</script>")
        Else
            Response.Write("<script>alert('Selezionare un elemento della lista!');</script>")
        End If
    End Sub

    Public Function RicavaidCOMPMOB() As Long
        RicavaidCOMPMOB = idCOMPMOB
    End Function

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


End Class
