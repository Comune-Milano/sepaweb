
Partial Class Condomini_RisultatiInquilini
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        If Not IsPostBack Then
            '********BISOGNA SEMPRE METTERLO NEL POSTBACK!
            '********SE FUORI EVENTUALI METODI CHE USANO LA RESP.WRITE SI IMBALLANO PERCHè LUI LE PULISCE TUTTE!
            Response.Flush()
            If Session.Item("RICINQUILINI") <> "" Then
                BindGrid()
                If Request.QueryString("P") <> "" Then
                    cambiataPagina(Request.QueryString("P"))
                    'Request.QueryString("P") = ""
                End If

            End If
        End If

    End Sub
    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Session.Item("RICINQUILINI").ToString, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "CONDOMINI, INDIRIZZI,COMPLESSI_EDIFICI")
        DataGridCondInquil.DataSource = ds
        DataGridCondInquil.DataBind()
        LnlNumeroRisultati.Text = "  - " & DataGridCondInquil.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        '*********************CHIUSURA CONNESSIONE**********************

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


    End Sub

    Protected Sub DataGridCondInquil_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridCondInquil.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Condominio dell\'inquilino : " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Condominio dell\'inquilino : " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>parent.main.location.replace('RicercaInquilini.aspx');</script>")
        Session.Remove("RICINQUILINI")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")
        Session.Remove("RICINQUILINI")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If Not String.IsNullOrEmpty(txtid.Value) Then
            Response.Write("<script>parent.main.location.replace('Condominio.aspx?IdCond=" & txtid.Value & "&CALL=RisultatiInquilini&P=" & Me.DataGridCondInquil.CurrentPageIndex & "');</script>")
            'Session.Remove("RICINQUILINI")
        Else
            Response.Write("<script>alert('Selezionare un condominio da visualizzare!');</script>")
        End If

    End Sub

    Protected Sub DataGridCondInquil_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridCondInquil.PageIndexChanged
        cambiataPagina(e.NewPageIndex)
    End Sub
    Sub cambiataPagina(ByVal numero As Integer)
        If numero >= 0 Then
            'Label3.Text = "0"
            DataGridCondInquil.CurrentPageIndex = numero
            BindGrid()
        End If
    End Sub
End Class
