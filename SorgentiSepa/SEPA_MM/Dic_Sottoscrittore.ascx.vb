
Partial Class Dic_Sottoscrittore
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim scriptblock1 As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If	
        If Not IsPostBack Then
            txtData1.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtDataNascita.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")

            '**********modifiche campi***********
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
        End If
    End Sub


    Protected Sub cmbNazioneNas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNazioneNas.SelectedIndexChanged
        If cmbNazioneNas.Items.FindByText("ITALIA").Selected = False Then
            cmbPrNas.Visible = False
            Label6.Visible = False
            Label7.Visible = False
            cmbComuneNas.Visible = False
            txtDataNascita.Text = ""
            txtbinserito.Text = "0"
        Else
            cmbPrNas.Visible = True
            Label6.Visible = True
            Label7.Visible = True
            cmbComuneNas.Visible = True
            txtDataNascita.Text = ""
            txtbinserito.Text = "0"
        End If
        'par.SetFocusControl(Page, "Dic_Sottoscrittore1_cmbPrNas")

        'scriptblock1 = "<script language='javascript' type='text/javascript'>" _
        '& "AggTabDic('5',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);document.getElementById('img1').src='IMG/Sotto.gif';document.getElementById('img2').src='IMG/NoSotto.gif';document.getElementById('Dic_Sottoscrittore1_txtS').value='1';" _
        '& "</script>"
        'document.getElementById('txtTab').value,document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style
        'scriptblock1 = "<script language='javascript' type='text/javascript'>" _
        '& "AggTabDic('5',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style);document.getElementById('img1').src='IMG/Sotto.gif';document.getElementById('img2').src='IMG/NoSotto.gif';document.getElementById('Dic_Sottoscrittore1_txtS').value='1';" _
        '& "</script>"
        '        scriptblock1 = "<script language='javascript' type='text/javascript'>" _
        '& "document.getElementById('img1').src='IMG/Sotto.gif';document.getElementById('img2').src='IMG/NoSotto.gif';document.getElementById('Dic_Sottoscrittore1_txtS').value='1';" _
        '& "</script>"

        '        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript40")) Then
        '            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript40", scriptblock1)
        '        End If


    End Sub

    Protected Sub cmbPrNas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrNas.SelectedIndexChanged
        Dim item As ListItem

        txtDataNascita.Text = ""
        txtbinserito.Text = "0"
        item = cmbPrNas.SelectedItem
        par.RiempiDList(Me, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")

        'par.SetFocusControl(Page, "Dic_Sottoscrittore1_cmbComuneNas")
        'scriptblock1 = "<script language='javascript' type='text/javascript'>" _
        '& "AggTabDic('5',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);document.getElementById('img1').src='IMG/Sotto.gif';document.getElementById('img2').src='IMG/NoSotto.gif';document.getElementById('txtS').value='1';" _
        '& "</script>"
        '        scriptblock1 = "<script language='javascript' type='text/javascript'>" _
        '& "AggTabDic('5',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style);document.getElementById('img1').src='IMG/Sotto.gif';document.getElementById('img2').src='IMG/NoSotto.gif';document.getElementById('Dic_Sottoscrittore1_txtS').value='1';" _
        '& "</script>"
        '        scriptblock1 = "<script language='javascript' type='text/javascript'>" _
        '& "document.getElementById('Dic_Sottoscrittore1_img1').src='../IMG/Sotto.gif';document.getElementById('Dic_Sottoscrittore1_img2').src='../IMG/NoSotto.gif';document.getElementById('Dic_Sottoscrittore1_txtS').value='1';" _
        '& "</script>"


        'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript40")) Then
        '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript40", scriptblock1)
        'End If
    End Sub

    Protected Sub cmbNazioneRes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNazioneRes.SelectedIndexChanged
        If cmbNazioneRes.Items.FindByText("ITALIA").Selected = False Then
            cmbPrRes.Visible = False
            Label11.Visible = False
            Label10.Visible = False
            cmbComuneRes.Visible = False
        Else
            cmbPrRes.Visible = True
            Label11.Visible = True
            Label10.Visible = True
            cmbComuneRes.Visible = True
        End If
        'par.SetFocusControl(Page, "Dic_Sottoscrittore1_cmbPrRes")
        'scriptblock1 = "<script language='javascript' type='text/javascript'>" _
        '& "AggTabDic('5',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);document.getElementById('img1').src='IMG/Sotto.gif';document.getElementById('img2').src='IMG/NoSotto.gif';document.getElementById('Dic_Sottoscrittore1_txtS').value='1';" _
        '& "</script>"
        '        scriptblock1 = "<script language='javascript' type='text/javascript'>" _
        '& "AggTabDic('5',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style);document.getElementById('img1').src='IMG/Sotto.gif';document.getElementById('img2').src='IMG/NoSotto.gif';document.getElementById('Dic_Sottoscrittore1_txtS').value='1';" _
        '& "</script>"
        '        scriptblock1 = "<script language='javascript' type='text/javascript'>" _
        '& "document.getElementById('img1').src='IMG/Sotto.gif';document.getElementById('img2').src='IMG/NoSotto.gif';document.getElementById('Dic_Sottoscrittore1_txtS').value='1';" _
        '& "</script>"


        'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript40")) Then
        '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript40", scriptblock1)
        'End If
    End Sub

    Protected Sub cmbPrRes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrRes.SelectedIndexChanged
        Dim item As ListItem
        Dim cmd As New Oracle.DataAccess.Client.OracleCommand()

        item = cmbPrRes.SelectedItem
        'par.OracleConn.Open()
        par.RiempiDList(Me, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")
        'par.OracleConn.Close()
        txtCAPRes.Text = ""
        'par.SetFocusControl(Page, "Dic_Sottoscrittore1_cmbComuneRes")


        item = cmbComuneRes.SelectedItem
        par.OracleConn.Open()
        par.SettaCommand(par)
        cmd.CommandText = "SELECT CAP FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(item.Text) & "'"

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
        If myReader.Read() Then
            txtCAPRes.Text = myReader(0)
        End If
        myReader.Close()
        par.OracleConn.Close()
        par.OracleConn.Dispose()

        'scriptblock1 = "<script language='javascript' type='text/javascript'>" _
        '& "AggTabDic('5',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);document.getElementById('img1').src='IMG/Sotto.gif';document.getElementById('img2').src='IMG/NoSotto.gif';document.getElementById('Dic_Sottoscrittore1_txtS').value='1';" _
        '& "</script>"
        '        scriptblock1 = "<script language='javascript' type='text/javascript'>" _
        '& "AggTabDic('5',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style);document.getElementById('img1').src='IMG/Sotto.gif';document.getElementById('img2').src='IMG/NoSotto.gif';document.getElementById('Dic_Sottoscrittore1_txtS').value='1';" _
        '& "</script>"
        'scriptblock1 = "<script language='javascript' type='text/javascript'>" _
        '& "document.getElementById('Dic_Sottoscrittore1_img1').src='../IMG/Sotto.gif';document.getElementById('Dic_Sottoscrittore1_img2').src='../IMG/NoSotto.gif';document.getElementById('Dic_Sottoscrittore1_txtS').value='1';" _
        '& "</script>"


        '        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript40")) Then
        '            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript40", scriptblock1)
        '        End If


    End Sub

    Protected Sub cmbComuneRes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComuneRes.SelectedIndexChanged
        Dim item As ListItem
        Dim cmd As New Oracle.DataAccess.Client.OracleCommand()

        item = cmbComuneRes.SelectedItem
        par.OracleConn.Open()
        par.SettaCommand(par)
        cmd.CommandText = "SELECT CAP FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(item.Text) & "'"

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
        If myReader.Read() Then
            txtCAPRes.Text = myReader(0)
        End If
        myReader.Close()
        par.OracleConn.Close()
        par.OracleConn = Nothing
        'par.SetFocusControl(Page, "Dic_Sottoscrittore1_cmbTipoIRes")

        'scriptblock1 = "<script language='javascript' type='text/javascript'>" _
        '& "AggTabDic('5',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style);document.getElementById('img1').src='IMG/Sotto.gif';document.getElementById('img2').src='IMG/NoSotto.gif';document.getElementById('Dic_Sottoscrittore1_txtS').value='1';" _
        '& "</script>"
'        scriptblock1 = "<script language='javascript' type='text/javascript'>" _
'& "AggTabDic('5',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style);document.getElementById('img1').src='IMG/Sotto.gif';document.getElementById('img2').src='IMG/NoSotto.gif';document.getElementById('Dic_Sottoscrittore1_txtS').value='1';" _
        '& "</script>"
        '        scriptblock1 = "<script language='javascript' type='text/javascript'>" _
        '& "document.getElementById('img1').src='../IMG/Sotto.gif';document.getElementById('img2').src='../IMG/NoSotto.gif';document.getElementById('Dic_Sottoscrittore1_txtS').value='1';" _
        '& "</script>"


        '        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript40")) Then
        '            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript40", scriptblock1)
        '        End If
    End Sub


End Class