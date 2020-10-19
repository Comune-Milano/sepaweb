
Partial Class Contabilita_RisBolletta
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("RICBOLLETTA")
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Cerca()
        End If



    End Sub
    Private Sub Cerca()

        Try
            Query = Session.Item("RICBOLLETTA")
            BindGrid()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub BindGrid()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Query, par.OracleConn)

        Dim dt As New Data.DataTable
        da.Fill(dt)

        For Each row As Data.DataRow In dt.Rows

            par.cmd.CommandText = "select SUM(IMPORTO) FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & row.Item("ID_BOLLETTA")
            myReaderS = par.cmd.ExecuteReader()
            If myReaderS.Read Then
                row.Item("IMPORTO") = par.IfNull(myReaderS(0), "0,00")
            End If
            myReaderS.Close()
        Next

        DataGridBollette.DataSource = dt
        DataGridBollette.DataBind()
        LnlNumeroRisultati.Text = "  - " & DataGridBollette.Items.Count & " nella pagina - Totale:" & dt.Rows.Count

        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Public Property Query() As String
        Get
            If Not (ViewState("par_QUERY") Is Nothing) Then
                Return CStr(ViewState("par_QUERY"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_QUERY") = value
        End Set

    End Property

    Protected Sub btnAnteprima_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnteprima.Click
        If IdAnagrafica.Value = "" Then

            Response.Write("<script>alert('Nessuna Bolletta selezionata!')</script>")
        Else

            Response.Write("<script>window.open('../Contratti/AnteprimaBolletta.aspx?ID=" & Idbolletta.Value & "','Anteprima', '');</script>")
        End If

    End Sub

    Protected Sub btnVisualizzaDettaglio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizzaDettaglio.Click
        If IdAnagrafica.Value = "" Then

            Response.Write("<script>alert('Nessuna Bolletta selezionata!')</script>")
        Else

            Response.Write("<script>window.open('DatiUtenza.aspx?C=RisUtenza&IDANA=" & Me.IdAnagrafica.Value & "&IDCONT=" & Me.IdContratto.Value & "','DatiUtente', '');</script>")
        End If

    End Sub

    Protected Sub DataGridBollette_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridBollette.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la bolletta: " & e.Item.Cells(0).Text & "';document.getElementById('IdAnagrafica').value='" & e.Item.Cells(1).Text & "';document.getElementById('Idbolletta').value='" & e.Item.Cells(0).Text & "';document.getElementById('IdContratto').value='" & e.Item.Cells(2).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la bolletta: " & e.Item.Cells(0).Text & "';document.getElementById('IdAnagrafica').value='" & e.Item.Cells(1).Text & "';document.getElementById('Idbolletta').value='" & e.Item.Cells(0).Text & "';document.getElementById('IdContratto').value='" & e.Item.Cells(2).Text & "'")

        End If
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Session.Remove("RICBOLLETTA")
        Response.Write("<script>document.location.href=""RicBolletta.aspx""</script>")

    End Sub

    Protected Sub DataGridBollette_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridBollette.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGridBollette.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
End Class
