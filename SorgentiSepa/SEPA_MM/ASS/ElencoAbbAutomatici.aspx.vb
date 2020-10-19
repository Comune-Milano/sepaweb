
Partial Class ASS_ElencoAbbAutomatici
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dtElenco As New System.Data.DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsPostBack = False Then
            RiempiDatagrid()
        End If
    End Sub

    Private Sub RiempiDatagrid()
        Try
            dtElenco = Session.Item("dtAbbinamenti")
            DataGridAbb.DataSource = dtElenco
            DataGridAbb.DataBind()

            lblTotale.Text = "Totale abbinamenti automatici convalidati: " & DataGridAbb.Items.Count

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Session.Remove("dtAbbinamenti")
        Response.Write("<script>self.close();</script>")
    End Sub

    
    Protected Sub MenuReport_MenuItemClick(sender As Object, e As System.Web.UI.WebControls.MenuEventArgs) Handles MenuReport.MenuItemClick
        Select Case MenuReport.SelectedValue
            Case "1"
                Response.Redirect("..\ALLEGATI\ABBINAMENTI\" & Request.QueryString("NF") & ".xls", False)
            Case "2"
                Response.Redirect("..\ALLEGATI\ABBINAMENTI\" & Request.QueryString("NF2") & ".xls", False)
        End Select
    End Sub
End Class
