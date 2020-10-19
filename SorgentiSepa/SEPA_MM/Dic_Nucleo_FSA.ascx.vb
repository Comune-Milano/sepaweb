
Partial Class Dic_Nucleo_FSA
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
            Button4.Attributes.Add("OnClick", "javascript:AggiungiNucleo();")
            Button6.Attributes.Add("OnClick", "javascript:ModificaNucleo();")
            Button5.Attributes.Add("OnClick", "javascript:ModificaSpese();")
        End If

    End Sub

    Private Function CaricaGradiParenti()
    End Function

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim I As Integer
        Dim scriptblock As String


        If ListBox1.SelectedIndex >= 0 Then
            If ListBox1.SelectedIndex = 0 Then
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Non è possibile cancellare questo componente!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript2")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript2", scriptblock)
                End If
            Else
                Dim RIMOSSO As Boolean
                Dim Trovato As Boolean

                Trovato = False
                RIMOSSO = True
                'While RIMOSSO = True
                For I = 0 To CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox1"), ListBox).Items.Count - 1
                    If CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox1"), ListBox).Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
                        'CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox1"), ListBox).Items.Remove(CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox1"), ListBox).Items(I))
                        Trovato = True
                        RIMOSSO = True
                        Exit For
                    Else
                        RIMOSSO = False
                    End If
                Next
                'End While

                RIMOSSO = True
                'While RIMOSSO = True
                For I = 0 To CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox2"), ListBox).Items.Count - 1
                    If CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox2"), ListBox).Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
                        'CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox2"), ListBox).Items.Remove(CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox2"), ListBox).Items(I))
                        Trovato = True
                        RIMOSSO = True
                        'Exit For
                        Exit For
                    Else
                        RIMOSSO = False
                    End If
                Next
                'End While

                RIMOSSO = True
                'While RIMOSSO = True
                For I = 0 To CType(Page.Form.FindControl("Dic_Reddito1").FindControl("listbox1"), ListBox).Items.Count - 1
                    If CType(Page.Form.FindControl("Dic_Reddito1").FindControl("listbox1"), ListBox).Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
                        'CType(Page.Form.FindControl("Dic_Reddito1").FindControl("listbox1"), ListBox).Items.Remove(CType(Page.Form.FindControl("Dic_Reddito1").FindControl("listbox1"), ListBox).Items(I))
                        RIMOSSO = True
                        'Exit For
                        Exit For
                    Else
                        RIMOSSO = False
                    End If
                Next
                'End While

                RIMOSSO = True
                'While RIMOSSO = True
                For I = 0 To CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox1"), ListBox).Items.Count - 1
                    If CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox1"), ListBox).Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
                        'CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox1"), ListBox).Items.Remove(CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox1"), ListBox).Items(I))
                        RIMOSSO = True
                        Trovato = True
                        'Exit For
                        Exit For
                    Else
                        RIMOSSO = False
                    End If
                Next
                'End While

                RIMOSSO = True
                'While RIMOSSO = True
                For I = 0 To CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox2"), ListBox).Items.Count - 1
                    If CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox2"), ListBox).Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
                        'CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox2"), ListBox).Items.Remove(CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox2"), ListBox).Items(I))
                        Trovato = True
                        RIMOSSO = True
                        'Exit For
                        Exit For
                    Else
                        RIMOSSO = False
                    End If
                Next
                'End While
                If Trovato = False Then

                    RIMOSSO = False
                    While RIMOSSO = False
                        For I = 0 To ListBox2.Items.Count - 1
                            If ListBox2.Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
                                ListBox2.Items.Remove(ListBox2.Items(I))
                                RIMOSSO = True
                                Exit For
                            Else
                                RIMOSSO = False
                            End If
                        Next
                        RIMOSSO = True
                    End While

                    If ProgrDaCancellare = "" Then
                        ProgrDaCancellare = ProgrDaCancellare & " and (progr=" & ListBox1.SelectedItem.Value
                    Else
                        ProgrDaCancellare = ProgrDaCancellare & " or progr=" & ListBox1.SelectedItem.Value
                    End If
                    ListBox1.Items.Remove(ListBox1.SelectedItem)
                Else
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Non è possibile cancellare questo componente perchè compare in altre liste. Eliminare prima da tutte le liste!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", scriptblock)
                    End If
                End If
            End If
        Else
            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "alert('Selezionare un componente dalla lista!');" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript1")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript1", scriptblock)
            End If
        End If
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click

        'MsgBox(Cache.Get("RIGA").ToString())
        'MsgBox(Cache.Get("LISTA").ToString())

        If Not Cache(Session.Item("GLista")) Is Nothing Then
            ListBox2.Items(Cache.Get(Session.Item("GRiga")).ToString()).Text = Cache.Get(Session.Item("GLista")).ToString()
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GRiga"))
        End If

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim i As Integer

        CType(Me.Parent.FindControl("cmbComp"), DropDownList).Items.Clear()
        For i = 0 To ListBox1.Items.Count - 1
            CType(Me.Parent.FindControl("cmbComp"), DropDownList).Items.Add(New ListItem(par.RicavaTesto(ListBox1.Items(i).Text, 1, 25) & "," & par.RicavaTesto(ListBox1.Items(i).Text, 27, 25), ListBox1.Items(i).Value))
        Next

    End Sub

    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim I As Integer

        If Not Cache(Session.Item("GLista")) Is Nothing Then
            For I = 0 To ListBox1.Items.Count - 1
                If par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 64, 16) = par.RicavaTesto(ListBox1.Items(I).Text, 64, 16) Then
                    If I <> Cache.Get(Session.Item("GRiga")).ToString() Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('Componente già presente nel nucleo!');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript11")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript11", scriptblock)
                        End If
                        Cache.Remove(Session.Item("GLista"))
                        Cache.Remove(Session.Item("GSpese"))
                        Cache.Remove(Session.Item("GRiga"))
                        Exit Sub
                    End If
                End If
            Next I
            ListBox1.Items(Cache.Get(Session.Item("GRiga")).ToString()).Text = Cache.Get(Session.Item("GLista")).ToString()
            If txtProgr.Text = "" Then
                txtProgr.Text = "0"
            End If
            If ProgrDaCancellare = "" Then
                ProgrDaCancellare = ProgrDaCancellare & " and (progr=" & ListBox1.SelectedItem.Value
            Else
                ProgrDaCancellare = ProgrDaCancellare & " or progr=" & ListBox1.SelectedItem.Value
            End If
            If Not Cache(Session.Item("GSpese")) Is Nothing Then
                For I = 0 To ListBox2.Items.Count - 1
                    If ListBox2.Items(I).Value = ListBox1.Items(CInt(Cache.Get(Session.Item("GRiga")).ToString())).Value Then
                        ListBox2.Items.Remove(ListBox2.Items(I))
                        Exit For
                    End If
                Next
                'ListBox2.Items.Add(New ListItem(Cache.Get("SPESE").ToString(), txtProgr.Text))
                ListBox2.Items.Add(New ListItem(Cache.Get(Session.Item("GSpese")).ToString(), Cache.Get(Session.Item("GRiga")).ToString()))
            Else
                For I = 0 To ListBox2.Items.Count - 1
                    'MsgBox(ListBox2.Items(I).Value)
                    'MsgBox(ListBox1.Items(CInt(Cache.Get("RIGA").ToString())).Value)
                    If ListBox2.Items(I).Value = ListBox1.Items(CInt(Cache.Get(Session.Item("GRiga")).ToString())).Value Then
                        ListBox2.Items.Remove(ListBox2.Items(I))
                        Exit For
                    End If
                Next
            End If
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GSpese"))
            Cache.Remove(Session.Item("GRiga"))
        End If
    End Sub

    Public Sub DisattivaTutto()
        Button4.Enabled = False
        Button6.Enabled = False
        Button2.Enabled = False
        Button5.Enabled = False
        txtPsico.Enabled = False
        'chkSingolo.EnableTheming = False
        'ChkEntrambi.Enabled = False
        cmbEntrambi.Enabled = False
        cmbSingolo.Enabled = False
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

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim i As Long = 0
        If Not Cache(Session.Item("GLista")) Is Nothing Then
            For I = 0 To ListBox1.Items.Count - 1
                If par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 64, 16) = par.RicavaTesto(ListBox1.Items(I).Text, 64, 16) Then
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Componente già presente nel nucleo!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript10")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript10", scriptblock)
                    End If
                    Cache.Remove(Session.Item("GLista"))
                    Cache.Remove(Session.Item("GSpese"))
                    Exit Sub
                End If
            Next I
            txtProgr.Text = Val(txtProgr.Text) + 1
            ListBox1.Items.Add(New ListItem(Cache.Get(Session.Item("GLista")).ToString(), txtProgr.Text))
            If Not Cache(Session.Item("GSpese")) Is Nothing Then
                ListBox2.Items.Add(New ListItem(Cache.Get(Session.Item("GSpese")).ToString(), txtProgr.Text))
            End If
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GSpese"))
        End If
    End Sub
End Class
