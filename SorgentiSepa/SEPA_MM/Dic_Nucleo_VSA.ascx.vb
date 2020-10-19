

Partial Class Dic_Nucleo_VSA
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
            Button5.Attributes.Add("OnClick", "javascript:ModificaSpese();document.getElementById('H1').value='0';")
            Button2.Attributes.Add("OnClick", "javascript:EliminaSoggetto();document.getElementById('H1').value='0';")
            'Button2.Attributes.Add("OnClick", "javascript://document.getElementById('H1').value='0';")
            Button1.Attributes.Add("OnClick", "javascript:document.getElementById('H1').value='0';")
            Button3.Attributes.Add("OnClick", "javascript:document.getElementById('H1').value='0';")

            lblEliminati.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "document.getElementById('H1').value='0';window.open('../VSA/ElencoEliminati.aspx?IDDOM=" & CType(Me.Page, Object).lIdDomanda & "','ElencoEliminati','top=0,left=0,width=670,height=420,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">Clicca qui per visualizzare l'elenco dei componenti eliminati</a>"
            iddich.Value = CType(Me.Page, Object).lIdDichiarazione

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
    End Sub

    Private Function CaricaGradiParenti()
        'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        'par.SettaCommand(par)
        'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        '‘‘par.cmd.Transaction = par.myTrans

        'par.cmd.CommandText = "SELECT * FROM T_TIPO_PARENTELA WHERE COD>1 ORDER BY COD ASC"
        'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        'Dim lsiFrutto As New ListItem("CAPOFAMIGLIA", "1")
        'CType(Dic_Dett_N1.FindControl("cmbParenti"), DropDownList).Items.Add(lsiFrutto)

        'Do While myReader.Read()
        '    lsiFrutto = New ListItem(myReader("DESCRIZIONE"), myReader("COD"))
        '    CType(Dic_Dett_N1.FindControl("cmbParenti"), DropDownList).Items.Add(lsiFrutto)
        'Loop
        'CType(Dic_Dett_N1.FindControl("cmbParenti"), DropDownList).SelectedIndex = -1
    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

        'ListBox1.Visible = False
        'ListBox2.Visible = False

        'CType(Dic_Dett_N1.FindControl("L1"), Label).Visible = False
        'CType(Dic_Dett_N1.FindControl("L2"), Label).Visible = False
        'CType(Dic_Dett_N1.FindControl("L3"), Label).Visible = False
        'CType(Dic_Dett_N1.FindControl("L4"), Label).Visible = False
        'CType(Dic_Dett_N1.FindControl("L5"), Label).Visible = False
        'CType(Dic_Dett_N1.FindControl("L6"), Label).Visible = False
        'CType(Dic_Dett_N1.FindControl("L7"), Label).Visible = False

        'CType(Dic_Dett_N1.FindControl("txtCognome"), TextBox).Text = ""
        'CType(Dic_Dett_N1.FindControl("txtNome"), TextBox).Text = ""
        'CType(Dic_Dett_N1.FindControl("txtData"), TextBox).Text = ""
        'CType(Dic_Dett_N1.FindControl("txtCF"), TextBox).Text = ""
        'CType(Dic_Dett_N1.FindControl("txtInv"), TextBox).Text = "0"
        'CType(Dic_Dett_N1.FindControl("txtASL"), TextBox).Text = ""

        'CType(Dic_Dett_N1.FindControl("cmbParenti"), DropDownList).SelectedIndex = -1
        'CType(Dic_Dett_N1.FindControl("cmbAcc"), DropDownList).SelectedIndex = -1

        'CType(Dic_Dett_N1.FindControl("txtOperazione"), TextBox).Text = "0"
        'CType(Dic_Dett_N1.FindControl("txtCognome"), TextBox).Enabled = True
        'CType(Dic_Dett_N1.FindControl("txtNome"), TextBox).Enabled = True
        'CType(Dic_Dett_N1.FindControl("txtData"), TextBox).Enabled = True
        'CType(Dic_Dett_N1.FindControl("txtCF"), TextBox).Enabled = True
        'Dic_Dett_N1.Visible = True

    End Sub


    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click

        'If ListBox1.SelectedIndex >= 0 Then
        '    ListBox1.Visible = False
        '    ListBox2.Visible = False
        '    CType(Dic_Dett_N1.FindControl("L1"), Label).Visible = False
        '    CType(Dic_Dett_N1.FindControl("L2"), Label).Visible = False
        '    CType(Dic_Dett_N1.FindControl("L3"), Label).Visible = False
        '    CType(Dic_Dett_N1.FindControl("L4"), Label).Visible = False
        '    CType(Dic_Dett_N1.FindControl("L5"), Label).Visible = False
        '    CType(Dic_Dett_N1.FindControl("L6"), Label).Visible = False
        '    CType(Dic_Dett_N1.FindControl("L7"), Label).Visible = False
        '    CType(Dic_Dett_N1.FindControl("txtCognome"), TextBox).Text = par.RicavaTesto(ListBox1.SelectedItem.Text, 1, 25)
        '    CType(Dic_Dett_N1.FindControl("txtNome"), TextBox).Text = par.RicavaTesto(ListBox1.SelectedItem.Text, 27, 25)
        '    CType(Dic_Dett_N1.FindControl("txtData"), TextBox).Text = par.RicavaTesto(ListBox1.SelectedItem.Text, 53, 10)
        '    CType(Dic_Dett_N1.FindControl("txtCF"), TextBox).Text = par.RicavaTesto(ListBox1.SelectedItem.Text, 64, 16)

        '    CType(Dic_Dett_N1.FindControl("cmbParenti"), DropDownList).SelectedIndex = -1
        '    If par.RicavaTesto(ListBox1.SelectedItem.Text, 81, 25) <> "" Then
        '        CType(Dic_Dett_N1.FindControl("cmbParenti"), DropDownList).Items.FindByText(par.RicavaTesto(ListBox1.SelectedItem.Text, 81, 25)).Selected = True
        '    Else
        '        CType(Dic_Dett_N1.FindControl("cmbParenti"), DropDownList).Items.FindByValue("1").Selected = True
        '    End If
        '    CType(Dic_Dett_N1.FindControl("txtInv"), TextBox).Text = par.RicavaTesto(ListBox1.SelectedItem.Text, 107, 6)
        '    CType(Dic_Dett_N1.FindControl("txtASL"), TextBox).Text = par.RicavaTesto(ListBox1.SelectedItem.Text, 114, 5)
        '    CType(Dic_Dett_N1.FindControl("cmbAcc"), DropDownList).SelectedIndex = -1
        '    If par.RicavaTesto(ListBox1.SelectedItem.Text, 120, 2) <> "" Then
        '        CType(Dic_Dett_N1.FindControl("cmbAcc"), DropDownList).Items.FindByText(par.RicavaTesto(ListBox1.SelectedItem.Text, 120, 2)).Selected = True
        '    Else
        '        CType(Dic_Dett_N1.FindControl("cmbAcc"), DropDownList).Items.FindByValue(0).Selected = True
        '    End If
        '    CType(Dic_Dett_N1.FindControl("txtOperazione"), TextBox).Text = "1"
        '    CType(Dic_Dett_N1.FindControl("txtRiga"), TextBox).Text = ListBox1.SelectedIndex
        '    If ListBox1.SelectedIndex = 0 Then
        '        CType(Dic_Dett_N1.FindControl("txtCognome"), TextBox).Enabled = False
        '        CType(Dic_Dett_N1.FindControl("txtNome"), TextBox).Enabled = False
        '        CType(Dic_Dett_N1.FindControl("txtData"), TextBox).Enabled = False
        '        CType(Dic_Dett_N1.FindControl("txtCF"), TextBox).Enabled = False
        '    Else
        '        CType(Dic_Dett_N1.FindControl("txtCognome"), TextBox).Enabled = True
        '        CType(Dic_Dett_N1.FindControl("txtNome"), TextBox).Enabled = True
        '        CType(Dic_Dett_N1.FindControl("txtData"), TextBox).Enabled = True
        '        CType(Dic_Dett_N1.FindControl("txtCF"), TextBox).Enabled = True
        '    End If
        '    Dic_Dett_N1.Visible = True
        'Else
        '    Response.Write("<script>alert('Selezionare un componente del nucleo!');</script>")
        'End If
        'CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Dim I As Integer
        'Dim scriptblock As String

        'If ListBox1.SelectedIndex >= 0 Then
        '    If ListBox1.SelectedIndex = 0 Then
        '        scriptblock = "<script language='javascript' type='text/javascript'>" _
        '        & "alert('Non è possibile cancellare questo componente!');" _
        '        & "</script>"
        '        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript2")) Then
        '            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript2", scriptblock)
        '        End If
        '    Else
        '        Dim RIMOSSO As Boolean
        '        Dim Trovato As Boolean

        '        Trovato = False
        '        RIMOSSO = True
        '        'While RIMOSSO = True
        '        For I = 0 To CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox1"), ListBox).Items.Count - 1
        '            If CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox1"), ListBox).Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
        '                'CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox1"), ListBox).Items.Remove(CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox1"), ListBox).Items(I))
        '                Trovato = True
        '                RIMOSSO = True
        '                Exit For
        '            Else
        '                RIMOSSO = False
        '            End If
        '        Next
        '        'End While

        '        RIMOSSO = True
        '        'While RIMOSSO = True
        '        For I = 0 To CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox2"), ListBox).Items.Count - 1
        '            If CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox2"), ListBox).Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
        '                'CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox2"), ListBox).Items.Remove(CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox2"), ListBox).Items(I))
        '                Trovato = True
        '                RIMOSSO = True
        '                'Exit For
        '                Exit For
        '            Else
        '                RIMOSSO = False
        '            End If
        '        Next
        '        'End While

        '        RIMOSSO = True
        '        'While RIMOSSO = True
        '        For I = 0 To CType(Page.Form.FindControl("Dic_Reddito1").FindControl("listbox1"), ListBox).Items.Count - 1
        '            If CType(Page.Form.FindControl("Dic_Reddito1").FindControl("listbox1"), ListBox).Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
        '                'CType(Page.Form.FindControl("Dic_Reddito1").FindControl("listbox1"), ListBox).Items.Remove(CType(Page.Form.FindControl("Dic_Reddito1").FindControl("listbox1"), ListBox).Items(I))
        '                RIMOSSO = True
        '                'Exit For
        '                Exit For
        '            Else
        '                RIMOSSO = False
        '            End If
        '        Next
        '        'End While

        '        RIMOSSO = True
        '        'While RIMOSSO = True
        '        For I = 0 To CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox1"), ListBox).Items.Count - 1
        '            If CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox1"), ListBox).Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
        '                'CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox1"), ListBox).Items.Remove(CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox1"), ListBox).Items(I))
        '                RIMOSSO = True
        '                Trovato = True
        '                'Exit For
        '                Exit For
        '            Else
        '                RIMOSSO = False
        '            End If
        '        Next
        '        'End While

        '        RIMOSSO = True
        '        'While RIMOSSO = True
        '        For I = 0 To CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox2"), ListBox).Items.Count - 1
        '            If CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox2"), ListBox).Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
        '                'CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox2"), ListBox).Items.Remove(CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("listbox2"), ListBox).Items(I))
        '                Trovato = True
        '                RIMOSSO = True
        '                'Exit For
        '                Exit For
        '            Else
        '                RIMOSSO = False
        '            End If
        '        Next
        '        'End While
        '        If Trovato = False Then

        '            RIMOSSO = False
        '            While RIMOSSO = False
        '                For I = 0 To ListBox2.Items.Count - 1
        '                    If ListBox2.Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
        '                        ListBox2.Items.Remove(ListBox2.Items(I))
        '                        RIMOSSO = True
        '                        Exit For
        '                    Else
        '                        RIMOSSO = False
        '                    End If
        '                Next
        '                RIMOSSO = True
        '            End While

        '            If ProgrDaCancellare = "" Then
        '                ProgrDaCancellare = ProgrDaCancellare & " and (progr=" & ListBox1.SelectedItem.Value
        '            Else
        '                ProgrDaCancellare = ProgrDaCancellare & " or progr=" & ListBox1.SelectedItem.Value
        '            End If
        '            ListBox1.Items.Remove(ListBox1.SelectedItem)
        '        Else
        '            scriptblock = "<script language='javascript' type='text/javascript'>" _
        '            & "alert('Non è possibile cancellare questo componente perchè compare in altre liste. Eliminare prima da tutte le liste!');" _
        '            & "</script>"
        '            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript")) Then
        '                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", scriptblock)
        '            End If
        '        End If
        '    End If

        'Else
        '    scriptblock = "<script language='javascript' type='text/javascript'>" _
        '    & "alert('Selezionare un componente dalla lista!');" _
        '    & "</script>"
        '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript1")) Then
        '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript1", scriptblock)
        '    End If
        'End If


        '****************** 12/03/2012 FUNZIONE PER CANCELLARE A CASCATA ***********

        CancellaACascata()
       
    End Sub

    Private Sub CancellaACascata()
        Try
            Dim I As Integer
            Dim scriptblock As String

            Dim listPatrimonio As ListBox
            Dim listPatrimonio2 As ListBox
            Dim listReddito As ListBox
            Dim listIntegrazione As ListBox
            Dim listIntegrazione2 As ListBox
            Dim listReddConvenz As ListBox

            Dim numgiro As Integer
            Dim elim As Integer

            listPatrimonio = CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox1"), ListBox)
            listPatrimonio2 = CType(Page.Form.FindControl("Dic_Patrimonio1").FindControl("listbox2"), ListBox)

            listReddito = CType(Page.Form.FindControl("Dic_Reddito1").FindControl("listbox1"), ListBox)

            listIntegrazione = CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("Listbox1"), ListBox)
            listIntegrazione2 = CType(Page.Form.FindControl("Dic_Integrazione1").FindControl("Listbox2"), ListBox)

            listReddConvenz = CType(Page.Form.FindControl("Dic_Reddito_Conv1").FindControl("listbox1"), ListBox)

            If ListBox1.SelectedIndex >= 0 Then
                If ListBox1.SelectedIndex = 0 Then
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Non è possibile cancellare questo componente!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript2")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript2", scriptblock)
                    End If
                Else
                    If Not Cache(Session.Item("GLista")) Is Nothing Then

                        MotivoUscita.Value = Trim(par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 86, 2))
                        DataUscita.Value = Trim(par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 88, 10))

                        'numgiro = 0
                        'For I = 0 To listPatrimonio.Items.Count - 1
                        '    If I <= listPatrimonio.Items.Count - 1 Then
                        '        If listPatrimonio.Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
                        '            listPatrimonio.Items.Remove(listPatrimonio.Items(I))
                        '            'I = 0
                        '        End If
                        '    End If
                        '    If I = listPatrimonio.Items.Count - 1 And numgiro <= listPatrimonio.Items.Count - 1 Then
                        '        numgiro = numgiro + 1
                        '        I = 0
                        '    End If
                        'Next

                        numgiro = 0
                        elim = 0
                        For I = 0 To listPatrimonio.Items.Count - 1
                            If I = ((listPatrimonio.Items.Count - 1) + elim) And numgiro <= ((listPatrimonio.Items.Count - 1) + elim) Then
                                numgiro = numgiro + 1
                                I = 0
                            End If
                            If I <= listPatrimonio.Items.Count - 1 Then
                                If listPatrimonio.Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
                                    listPatrimonio.Items.Remove(listPatrimonio.Items(I))
                                    elim = elim + 1
                                End If
                            End If
                        Next


                        numgiro = 0
                        elim = 0
                        For I = 0 To listPatrimonio2.Items.Count - 1
                            If I = ((listPatrimonio2.Items.Count - 1) + elim) And numgiro <= ((listPatrimonio2.Items.Count - 1) + elim) Then
                                numgiro = numgiro + 1
                                I = 0
                            End If
                            If I <= listPatrimonio2.Items.Count - 1 Then
                                If listPatrimonio2.Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
                                    listPatrimonio2.Items.Remove(listPatrimonio2.Items(I))
                                    elim = elim + 1
                                End If
                            End If
                        Next

                        numgiro = 0
                        elim = 0
                        For I = 0 To listReddito.Items.Count - 1
                            If I = ((listReddito.Items.Count - 1) + elim) And numgiro <= ((listReddito.Items.Count - 1) + elim) Then
                                numgiro = numgiro + 1
                                I = 0
                            End If
                            If I <= listReddito.Items.Count - 1 Then
                                If listReddito.Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
                                    listReddito.Items.Remove(listReddito.Items(I))
                                    elim = elim + 1
                                End If
                            End If
                        Next

                        numgiro = 0
                        elim = 0
                        For I = 0 To listReddConvenz.Items.Count - 1
                            If I = ((listReddConvenz.Items.Count - 1) + elim) And numgiro <= ((listReddConvenz.Items.Count - 1) + elim) Then
                                numgiro = numgiro + 1
                                I = 0
                            End If
                            If I <= listReddConvenz.Items.Count - 1 Then
                                If listReddConvenz.Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
                                    listReddConvenz.Items.Remove(listReddConvenz.Items(I))
                                    elim = elim + 1
                                End If
                            End If
                        Next

                        numgiro = 0
                        elim = 0
                        For I = 0 To listIntegrazione.Items.Count - 1
                            If I = ((listIntegrazione.Items.Count - 1) + elim) And numgiro <= ((listIntegrazione.Items.Count - 1) + elim) Then
                                numgiro = numgiro + 1
                                I = 0
                            End If
                            If I <= listIntegrazione.Items.Count - 1 Then
                                If listIntegrazione.Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
                                    listIntegrazione.Items.Remove(listIntegrazione.Items(I))
                                    elim = elim + 1
                                End If
                            End If
                        Next

                        numgiro = 0
                        elim = 0
                        For I = 0 To listIntegrazione2.Items.Count - 1
                            If I = ((listIntegrazione2.Items.Count - 1) + elim) And numgiro <= ((listIntegrazione2.Items.Count - 1) + elim) Then
                                numgiro = numgiro + 1
                                I = 0
                            End If
                            If I <= listIntegrazione2.Items.Count - 1 Then
                                If listIntegrazione2.Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
                                    listIntegrazione2.Items.Remove(listIntegrazione2.Items(I))
                                    elim = elim + 1
                                End If
                            End If
                        Next

                        numgiro = 0
                        elim = 0
                        For I = 0 To ListBox2.Items.Count - 1
                            If I = ((ListBox2.Items.Count - 1) + elim) And numgiro <= ((ListBox2.Items.Count - 1) + elim) Then
                                numgiro = numgiro + 1
                                I = 0
                            End If
                            If I <= ListBox2.Items.Count - 1 Then
                                If ListBox2.Items(I).Value = ListBox1.Items(ListBox1.SelectedIndex).Value Then
                                    ListBox2.Items.Remove(ListBox2.Items(I))
                                    elim = elim + 1
                                End If
                            End If
                        Next


                        If ProgrDaCancellare = "" Then
                            ProgrDaCancellare = ProgrDaCancellare & " and (progr=" & ListBox1.SelectedItem.Value
                        Else
                            ProgrDaCancellare = ProgrDaCancellare & " or progr=" & ListBox1.SelectedItem.Value
                        End If

                        ListBox1.Items.Remove(ListBox1.SelectedItem)

                    End If

                End If

            End If
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GSpese"))
            Cache.Remove(Session.Item("GRiga"))

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
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

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim prova As String = ""
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
            txtprogr.Text = Val(txtprogr.Text) + 1
            ListBox1.Items.Add(New ListItem(par.MiaFormat(Cache.Get(Session.Item("GLista")).ToString(), 153), txtprogr.Text))

            txtidTipoVIA.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 154, 2)
            txtVIA.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 156, 25)
            txtCIVICO.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 181, 5)
            txtCOMUNE.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 186, 25)
            txtCAP.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 211, 5)
            txtDOCIDENT.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 216, 15)
            txtDATADOC.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 231, 10)
            txtRILASCIO.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 241, 25)
            txtSOGGIORNO.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 266, 15)
            txtDATASogg.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 281, 10)
            txtREFERENTE.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 291, 2)

            If Not Cache(Session.Item("GSpese")) Is Nothing Then
                ListBox2.Items.Add(New ListItem(Cache.Get(Session.Item("GSpese")).ToString(), txtprogr.Text))
            End If
            Cache.Remove(Session.Item("GLista"))
            Cache.Remove(Session.Item("GSpese"))
            NuovoCompon.Value = "1"
        Else
            Response.Write("<script language='javascript' type='text/javascript'>" _
                    & "alert('Inserire i dati del nuovo componente!');" _
                    & "</script>")
        End If
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
            ListBox1.Items(Cache.Get(Session.Item("GRiga")).ToString()).Text = par.MiaFormat(Cache.Get(Session.Item("GLista")).ToString(), 153)
            If txtprogr.Text = "" Then
                txtprogr.Text = "0"
            End If
            txtID.Value = ListBox1.SelectedItem.Value
            txtidTipoVIA.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 154, 2)
            txtVIA.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 156, 25)
            txtCIVICO.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 181, 5)
            txtCOMUNE.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 186, 25)
            txtCAP.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 211, 5)
            txtDOCIDENT.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 216, 15)
            txtDATADOC.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 231, 10)
            txtRILASCIO.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 241, 25)
            txtSOGGIORNO.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 266, 15)
            txtDATASogg.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 281, 10)
            txtREFERENTE.Value = par.RicavaTesto(Cache.Get(Session.Item("GLista")).ToString(), 291, 2)

            If Not Cache(Session.Item("GSpese")) Is Nothing Then
                For I = 0 To ListBox2.Items.Count - 1
                    If ListBox2.Items(I).Value = ListBox1.Items(CInt(Cache.Get(Session.Item("GRiga")).ToString())).Value Then
                        ListBox2.Items.Remove(ListBox2.Items(I))
                        Exit For
                    End If
                Next
                'ListBox2.Items.Add(New ListItem(Cache.Get("SPESE").ToString(), txtprogr.Text))
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