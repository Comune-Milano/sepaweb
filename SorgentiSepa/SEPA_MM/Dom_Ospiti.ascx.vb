
Partial Class Dom_Ospiti
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
            Button2.Attributes.Add("OnClick", "javascript:document.getElementById('H1').value='0';")

            iddom.Value = CType(Me.Page, Object).lIdDomanda
            '**********modifiche campi***********
            Dim CTRL As Control
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is ListBox Then
                    DirectCast(CTRL, ListBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is Button Then
                    DirectCast(CTRL, Button).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                End If
            Next
        End If
        'ListBox1.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Dom_Ospiti1_ListBox1');if (obj1.selectedIndex!=-1) {document.getElementById('Dom_Ospiti1_V1').value=obj1.options[obj1.selectedIndex].text;}")
    End Sub


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim scriptblock As String

       If ListBox1.SelectedIndex >= 0 Then
            If ProgrDaCancellare = "" Then
                ProgrDaCancellare = ProgrDaCancellare & " and (id=" & ListBox1.SelectedItem.Value
            Else
                ProgrDaCancellare = ProgrDaCancellare & " or id=" & ListBox1.SelectedItem.Value
            End If
            ListBox1.Items.Remove(ListBox1.SelectedItem)
        Else
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('Selezionare un componente dalla lista!');" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript1")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript1", scriptblock)
            End If
        End If
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        If Not Cache(Session.Item("GLista")) Is Nothing Then
            For I As Integer = 0 To ListBox1.Items.Count - 1
                If par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 64, 16) = par.RicavaTesto(ListBox1.Items(I).Text, 64, 16) Then
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Ospite già presente!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript10")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript10", scriptblock)
                    End If
                    Cache.Remove(Session.Item("GLista"))
                    Exit Sub
                End If
            Next I

            txtprogr.Text = Val(txtprogr.Text) + 1
            ListBox1.Items.Add(New ListItem(par.MiaFormat(Cache.Get(Session.Item("GLista")).ToString(), 112), txtprogr.Text))

            txtidTipoVIA.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 114, 2)
            txtVIA.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 116, 25)
            txtCIVICO.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 141, 5)
            txtCOMUNE.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 146, 25)
            txtCAP.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 171, 5)
            txtDOCIDENT.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 176, 15)
            txtDATADOC.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 191, 10)
            txtRILASCIO.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 201, 25)
            txtSOGGIORNO.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 226, 15)
            txtDATASogg.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 241, 10)
            txtREFERENTE.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 251, 2)



            Cache.Remove(Session.Item("GLista"))
        End If
    End Sub

    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim I As Integer

        If Not Cache(Session.Item("GLista")) Is Nothing Then
            For I = 0 To ListBox1.Items.Count - 1
                If par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 64, 16) = par.RicavaTesto(ListBox1.Items(I).Text, 64, 16) Then
                    If I <> Cache.Get(Session.Item("GRiga")).ToString() Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('Ospite già presente nel nucleo!');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript11")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript11", scriptblock)
                        End If
                        Cache.Remove(Session.Item("GLista"))
                        Cache.Remove(Session.Item("GRiga"))
                        Exit Sub
                    End If
                End If
            Next I
            ListBox1.Items(Cache.Get(Session.Item("GRiga")).ToString()).Text = par.MiaFormat(Cache.Get(Session.Item("GLista")).ToString(), 112)
            If txtprogr.Text = "" Then
                txtprogr.Text = "0"
            End If

            txtIDospite.Value = ListBox1.SelectedItem.Value
            txtidTipoVIA.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 114, 2)
            txtVIA.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 116, 25)
            txtCIVICO.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 141, 5)
            txtCOMUNE.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 146, 25)
            txtCAP.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 171, 5)
            txtDOCIDENT.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 176, 15)
            txtDATADOC.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 191, 10)
            txtRILASCIO.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 201, 25)
            txtSOGGIORNO.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 226, 15)
            txtDATASogg.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 241, 10)
            txtREFERENTE.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 251, 2)


            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GRiga"))
        End If
    End Sub

    Public Sub DisattivaTutto()
        Button4.Enabled = False
        Button6.Enabled = False
        Button2.Enabled = False
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


End Class