﻿
Partial Class MANUTENZIONI_RisultatiInterventi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If

        If Not IsPostBack Then

            LBLID.Text = Request.QueryString("T")

            par.OracleConn.Open()
            Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(Session.Item("PED"), par.OracleConn)
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
            Label3.Text = "0"
            Do While myReader.Read()
                Label3.Text = CInt(Label3.Text) + 1
            Loop
            Label3.Text = Label3.Text
            myReader.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            BindGrid()
        End If


    End Sub
    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Session.Item("PED"), par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            Session.Add("ID", txtid.Text)
            If Request.QueryString("CHIAMA") = "S" Then
                Response.Redirect("Servizio_Manut.aspx")
            Else
                Response.Redirect("Int_Manutenzione.aspx")
            End If
        End If

    End Sub

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        LBLID.Text = e.Item.Cells(0).Text
        Label2.Text = "Hai selezionato Cod. Complesso: " & e.Item.Cells(1).Text

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(0).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(1).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(0).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(1).Text & "'")

        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub
End Class
