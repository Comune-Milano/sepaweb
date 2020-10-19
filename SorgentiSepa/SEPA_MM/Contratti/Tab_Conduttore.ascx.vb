
Partial Class Contratti_Tab_Conduttore
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global



    'Protected Sub btnInserisciOspite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInserisciOspite.Click
    '    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"

    '    lstOspiti.Items.Add(New ListItem(PAR.MiaFormat(Format(Val(txtNumeroOspiti.Text), "00"), 2) & " " & PAR.MiaFormat(txtDataInizio.Text, 15) & " " & PAR.FormattaData(txtDataFine.Text), -1))

    'End Sub



    Protected Sub btnAggiungiCond_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggiungiCond.Click

        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        If Session.Item("IDANA") <> "" Then
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
            End If
            par.SettaCommand(par)
            PAR.cmd.CommandText = "SELECT ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID=" & Session.Item("IDANA")
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReader2.Read
                If PAR.IfNull(myReader2("RAGIONE_SOCIALE"), "") <> "" Then
                    lstIntestatari.Items.Add(New ListItem(PAR.MiaFormat(PAR.IfNull(myReader2("RAGIONE_SOCIALE"), ""), 40) & " " & PAR.MiaFormat(PAR.IfNull(myReader2("PARTITA_IVA"), ""), 16) & " " & "NON ATTRIBUIBILE", myReader2("ID")))
                    'CType(Page.FindControl("Tab_Comunicazioni1").FindControl("txtPresso"), TextBox).Text = PAR.IfNull(myReader2("RAGIONE_SOCIALE"), "")
                Else
                    lstIntestatari.Items.Add(New ListItem(PAR.MiaFormat(PAR.IfNull(myReader2("COGNOME"), ""), 20) & " " & PAR.MiaFormat(PAR.IfNull(myReader2("NOME"), ""), 16) & " " & PAR.MiaFormat(PAR.IfNull(myReader2("COD_FISCALE"), ""), 16) & " " & "NON ATTRIBUIBILE", myReader2("ID")))
                    'CType(Page.FindControl("Tab_Comunicazioni1").FindControl("txtPresso"), TextBox).Text = PAR.IfNull(myReader2("COGNOME"), "") & " " & PAR.IfNull(myReader2("NOME"), "")
                End If

            Loop
            myReader2.Close()
        End If
        Session.Item("IDANA") = ""

    End Sub


    Protected Sub btnAggiungiComp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggiungiComp.Click
        If Session.Item("IDANA") <> "" Then
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
            End If
            par.SettaCommand(par)
            PAR.cmd.CommandText = "SELECT ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID=" & Session.Item("IDANA")
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReader2.Read
                If PAR.IfNull(myReader2("RAGIONE_SOCIALE"), "") <> "" Then
                    lstComponenti.Items.Add(New ListItem(PAR.MiaFormat(PAR.IfNull(myReader2("RAGIONE_SOCIALE"), ""), 40) & " " & PAR.MiaFormat(PAR.IfNull(myReader2("PARTITA_IVA"), ""), 16) & " " & "NON ATTRIBUIBILE", myReader2("ID")))
                Else
                    lstComponenti.Items.Add(New ListItem(PAR.MiaFormat(PAR.IfNull(myReader2("COGNOME"), ""), 20) & " " & PAR.MiaFormat(PAR.IfNull(myReader2("NOME"), ""), 16) & " " & PAR.MiaFormat(PAR.IfNull(myReader2("COD_FISCALE"), ""), 16) & " " & "NON ATTRIBUIBILE", myReader2("ID")))
                End If
            Loop
            myReader2.Close()
            CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"
        End If
        Session.Item("IDANA") = ""
    End Sub

    'Protected Sub btnChiudiOspiti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudiOspiti.Click
    '    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
    'End Sub

    Protected Sub imgDiventaINT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDiventaINT.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        If lstComponenti.SelectedIndex >= 0 Then

            Dim i As Integer
            For i = 0 To lstComponenti.Items.Count - 1
                If Trim(lstComponenti.Items(i).Text) = Trim(V1.Value) Then
                    lstIntestatari.Items.Add(New ListItem(V1.Value, lstComponenti.Items(i).Value))
                    lstComponenti.Items.Remove(lstComponenti.Items(i))
                    Exit Sub
                End If
            Next
        Else
            Response.Write("<SCRIPT>alert('Selezionare un componente della lista!');</SCRIPT>")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        lstComponenti.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Tab_Conduttore1_lstComponenti');if (obj1.selectedIndex!=-1) {document.getElementById('Tab_Conduttore1_V1').value=obj1.options[obj1.selectedIndex].text;}")
        lstIntestatari.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Tab_Conduttore1_lstIntestatari');if (obj1.selectedIndex!=-1) {document.getElementById('Tab_Conduttore1_V2').value=obj1.options[obj1.selectedIndex].text;}")
        'lstOspiti.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Tab_Conduttore1_lstOspiti');if (obj1.selectedIndex!=-1) {document.getElementById('Tab_Conduttore1_V3').value=obj1.options[obj1.selectedIndex].text;}")
        txtAl.Attributes.Add("onblur", "javascript:confronta_data(document.getElementById('Tab_Conduttore1_txtDal').value,document.getElementById('Tab_Conduttore1_txtAl').value);")

    End Sub

    Protected Sub img_EliminaComp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_EliminaComp.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        If lstComponenti.SelectedIndex >= 0 Then
            Dim i As Integer
            For i = 0 To lstComponenti.Items.Count - 1
                If Trim(lstComponenti.Items(i).Text) = Trim(V1.Value) Then
                    lstComponenti.Items.Remove(lstComponenti.Items(i))
                    CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"
                    Exit Sub
                End If
            Next

        Else
            Response.Write("<SCRIPT>alert('Selezionare un componente della lista!');</SCRIPT>")
        End If
    End Sub

    Protected Sub imgEliminaCond_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgEliminaCond.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        If lstIntestatari.SelectedIndex >= 0 Then

            Dim i As Integer
            For i = 0 To lstIntestatari.Items.Count - 1
                If Trim(lstIntestatari.Items(i).Text) = Trim(V2.Value) Then
                    lstIntestatari.Items.Remove(lstIntestatari.Items(i))
                    Exit Sub
                End If
            Next
        Else
            Response.Write("<SCRIPT>alert('Selezionare un componente della lista!');</SCRIPT>")
        End If
    End Sub

    'Protected Sub img_EliminaOspite_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_EliminaOspite.Click
    '    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
    '    If lstOspiti.SelectedIndex >= 0 Then

    '        Dim i As Integer
    '        For i = 0 To lstOspiti.Items.Count - 1
    '            If Trim(lstOspiti.Items(i).Text) = Trim(V3.Value) Then
    '                lstOspiti.Items.Remove(lstOspiti.Items(i))
    '                CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"
    '                Exit Sub
    '            End If
    '        Next
    '    Else
    '        Response.Write("<SCRIPT>alert('Selezionare un componente della lista!');</SCRIPT>")
    '    End If
    'End Sub

    Protected Sub Img_DiventaComp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Img_DiventaComp.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        If lstIntestatari.SelectedIndex >= 0 Then

            Dim i As Integer
            For i = 0 To lstIntestatari.Items.Count - 1
                If Trim(lstIntestatari.Items(i).Text) = Trim(V2.Value) Then
                    lstComponenti.Items.Add(New ListItem(V2.Value, lstIntestatari.Items(i).Value))
                    lstIntestatari.Items.Remove(lstIntestatari.Items(i))
                    Exit Sub
                End If
            Next
        Else
            Response.Write("<SCRIPT>alert('Selezionare un componente della lista!');</SCRIPT>")
        End If
    End Sub

    Public Sub Disabilita_Tutto()
        lstIntestatari.Enabled = False
        lstComponenti.Enabled = False
        Img_DiventaComp.Enabled = False
        img_EliminaComp.Enabled = False
        'img_EliminaOspite.Enabled = False
        img_InserisciOspite.Enabled = False
        btnAggiungiComp.Enabled = False
        btnAggiungiCond.Enabled = False
        imgDiventaINT.Enabled = False
        imgEliminaCond.Enabled = False

        lstOspiti.Enabled = False
    End Sub

    Public Function Disabilita_Tutto_Tranne_Comp()
        lstIntestatari.Enabled = False
        btnAggiungiCond.Enabled = False
        imgEliminaCond.Enabled = False



        lstComponenti.Enabled = True
        Img_DiventaComp.Enabled = False
        img_EliminaComp.Enabled = True
        btnAggiungiComp.Enabled = True
        imgDiventaINT.Enabled = False


        img_InserisciOspite.Enabled = False
        lstOspiti.Enabled = False



    End Function


    'Protected Sub img_InserisciOspite_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciOspite.Click
    '    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"

    '    If txtAl.Text <> "" And txtDal.Text <> "" And txtNominativo.Text <> "" And txtCodiceFiscale.Text <> "" And PAR.ControllaCF(txtCodiceFiscale.Text) = True Then
    '        CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

    '        lstOspiti.Items.Add(New ListItem(PAR.MiaFormat(UCase(txtNominativo.Text), 30) & " " & PAR.MiaFormat(txtCodiceFiscale.Text, 16) & " " & PAR.MiaFormat(txtDal.Text, 15) & " " & PAR.MiaFormat(txtAl.Text, 15), -1))

    '        txtNominativo.Text = ""
    '        txtCodiceFiscale.Text = ""
    '        txtNominativo.Text = ""
    '        txtDal.Text = ""
    '        txtAl.Text = ""
    '    Else
    '        Response.Write("<script>alert('Valori mancanti o codice fiscale errato!');</script>")
    '    End If

    'End Sub



End Class
