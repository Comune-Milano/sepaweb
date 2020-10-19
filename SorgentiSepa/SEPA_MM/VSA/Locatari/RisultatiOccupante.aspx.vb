
Partial Class PED_RisultatiOccupante
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            LBLID.Text = Request.QueryString("T")
            'par.OracleConn.Open()
            'Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(Session.Item("PEDOCC"), par.OracleConn)
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
            'Label3.Text = "0"
            'Do While myReader.Read()
            '    Label3.Text = CInt(Label3.Text) + 1
            'Loop
            'Label3.Text = Label3.Text
            'cmd.Dispose()
            'myReader.Close()
            'par.OracleConn.Close()
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Session.Item("PEDOCC"), par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        '*********************CHIUSURA CONNESSIONE**********************

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaOccupante.aspx""</script>")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>self.close();</script>")
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white';}")

            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'unità di " & Replace(e.Item.Cells(1).Text, "'", "\'") & " - Cod.UI: " & e.Item.Cells(2).Text & "';document.getElementById('NOMEINTEST').value='" & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('IDCONTR').value='" & e.Item.Cells(0).Text & "';document.getElementById('CODUI').value='" & e.Item.Cells(2).Text & "';document.getElementById('CODCONTR').value='" & e.Item.Cells(3).Text & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'unità di " & Replace(e.Item.Cells(1).Text, "'", "\'") & " - Cod.UI: " & e.Item.Cells(2).Text & "';document.getElementById('NOMEINTEST').value='" & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('IDCONTR').value='" & e.Item.Cells(0).Text & "';document.getElementById('CODUI').value='" & e.Item.Cells(2).Text & "';document.getElementById('CODCONTR').value='" & e.Item.Cells(3).Text & "';")

        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub


    Protected Sub btnSeleziona_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSeleziona.Click
        Dim saldo2 As String = ""
        If IDCONTR.Value <> "" Then
            saldo2 = Format(par.CalcolaSaldoAttuale(IDCONTR.Value), "##,##0.00")
        End If

        If CODUI.Value = "" Then
            Response.Write("<script>alert('Nessuna Unita selezionata!');</script>")
        Else
            Response.Write("<script>window.opener.document.forms['form1'].elements['LBLintest2'].value='" & Replace(NOMEINTEST.Value, "'", "\'") & "';window.opener.document.forms['form1'].elements['HiddenLBLsaldo2'].value='" & saldo2 & "';window.opener.document.forms['form1'].elements['txtImporto2'].value='';window.opener.document.forms['form1'].elements['txtSaldo2'].value='" & saldo2 & "';window.opener.document.forms['form1'].elements['LBLid2'].value='" & IDCONTR.Value & "';window.opener.document.forms['form1'].elements['txtUIscambio'].value='" & CODUI.Value & "';window.opener.document.forms['form1'].elements['LBLcodContr2'].value='" & CODCONTR.Value & "';self.close();</script>")
        End If
    End Sub
End Class
