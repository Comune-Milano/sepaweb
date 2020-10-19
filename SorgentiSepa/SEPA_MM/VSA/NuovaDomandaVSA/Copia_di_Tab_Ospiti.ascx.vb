
Partial Class VSA_NuovaDomandaVSA_Tab_Ospiti
    Inherits UserControlSetIdMode
    Dim I As Integer
    Dim par As New CM.Global
    Dim scriptblock As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack = True Then
            'CaricaGradiParenti()

            Button4.Attributes.Add("OnClick", "javascript:AggiungiNucleo();document.getElementById('H1').value='0';")
            Button6.Attributes.Add("OnClick", "javascript:ModificaNucleo();document.getElementById('H1').value='0';")
            Button2.Attributes.Add("OnClick", "javascript:EliminaSoggetto();document.getElementById('H1').value='0';document.getElementById('provenienza').value='ospiti';")

            iddom.Value = CType(Me.Page, Object).lIdDomanda

            '**********modifiche campi***********
            Dim CTRL As Control
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DataGrid Then
                    DirectCast(CTRL, DataGrid).Attributes.Add("onload", "javascript:document.getElementById('idOsp').value='0';")
                ElseIf TypeOf CTRL Is Image Then
                    DirectCast(CTRL, Image).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                End If
            Next
        End If
        'ListBox1.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Dom_Ospiti1_ListBox1');if (obj1.selectedIndex!=-1) {document.getElementById('Dom_Ospiti1_V1').value=obj1.options[obj1.selectedIndex].text;}")
    End Sub

    Protected Sub DataGridOspiti_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridOspiti.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#DFDFDF';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#C1C1C1';" _
                                & "document.getElementById('idOsp').value='" & e.Item.Cells(0).Text & "';document.getElementById('Dom_Ospiti1_cognome').value='" & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Dom_Ospiti1_nome').value='" & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('Dom_Ospiti1_data_nasc').value='" & e.Item.Cells(3).Text & "';" _
                                & "document.getElementById('Dom_Ospiti1_cod_fiscale').value='" & e.Item.Cells(4).Text & "';document.getElementById('Dom_Ospiti1_data_inizio').value='" & e.Item.Cells(5).Text & "';document.getElementById('Dom_Ospiti1_IDselectedRow').value='" & DataGridOspiti.SelectedIndex & "';document.getElementById('Dom_Ospiti1_data_fine').value='" & e.Item.Cells(6).Text & "';")

        End If
    End Sub 

    Public Property ProgrDaCancellare() As String
        Get
            If Not (ViewState("par_ProgrDaCancellare") Is Nothing) Then
                Return CStr(ViewState("par_ProgrDaCancellare"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_ProgrDaCancellare") = value
        End Set

    End Property

    Public Sub DisattivaTutto()
        Button4.Enabled = False
        Button6.Enabled = False
        Button2.Enabled = False
    End Sub

End Class
