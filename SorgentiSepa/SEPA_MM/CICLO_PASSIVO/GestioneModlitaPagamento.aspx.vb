
Partial Class CICLO_PASSIVO_GestioneModlitaPagamento
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../../../AccessoNegato.htm""</script>")
        End If

        Me.connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then
            CaricaModalita()
            CaricaTipo()

        End If

    End Sub
    Private Sub CaricaModalita()
        Try



            connData.apri(False)
            par.cmd.CommandText = ""
            par.cmd.CommandText = "SELECT TIPO_MODALITA_PAG.ID,nvl(TAB_DATE_MODALITA_PAG.ID,-1) AS id_data_rif,codice,TIPO_MODALITA_PAG.descrizione, " _
                                & "TAB_DATE_MODALITA_PAG.DESCRIZIONE AS data_modlita_pag " _
                                & "FROM siscom_mi.TIPO_MODALITA_PAG,siscom_mi.TAB_DATE_MODALITA_PAG " _
                                & "WHERE TIPO_MODALITA_PAG.ID_DATA_RIFERIMENTO= TAB_DATE_MODALITA_PAG.ID(+) "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            dgvModalPag.DataSource = dt
            dgvModalPag.DataBind()
            connData.chiudi(False)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaModalita - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub
    Protected Sub dgvModalPag_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvModalPag.ItemDataBound
        Try

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                par.caricaComboBox("SELECT ID, descrizione FROM siscom_mi.TAB_DATE_MODALITA_PAG ORDER BY descrizione ASC", CType(e.Item.FindControl("cmbModPag"), DropDownList), "ID", "DESCRIZIONE", True)
                CType(e.Item.FindControl("cmbModPag"), DropDownList).SelectedValue = par.IfNull(e.Item.Cells(1).Text.ToString, "-1")
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " dgvModalPag_ItemDataBound - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub CaricaTipo()
        Try
            connData.apri(False)
            par.cmd.CommandText = "SELECT id,codice,descrizione,num_giorni FROM siscom_mi.TIPO_PAGAMENTO ORDER BY codice ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            dgvTipoPagamento.DataSource = dt
            dgvTipoPagamento.DataBind()
            connData.chiudi(False)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaTipo - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub
    Protected Sub dgvTipoPagamento_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvTipoPagamento.ItemDataBound
        Try


            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the jsFunzioni and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                CType(e.Item.FindControl("txtGiorni"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'numbers');")

            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " dgvTipoPagamento_ItemDataBound - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub SalvaModalita()
        For Each i As DataGridItem In dgvModalPag.Items
            par.cmd.CommandText = "UPDATE siscom_mi.TIPO_MODALITA_PAG set ID_DATA_RIFERIMENTO = " & CType(i.FindControl("cmbModPag"), DropDownList).SelectedValue.Replace("-1", "NULL") & " WHERE ID = " & i.Cells(0).Text
            par.cmd.ExecuteNonQuery()
        Next
    End Sub
    Private Sub SalvaTipo()
        For Each i As DataGridItem In dgvTipoPagamento.Items
            par.cmd.CommandText = "UPDATE siscom_mi.TIPO_PAGAMENTO set num_giorni = " & par.IfEmpty(CType(i.FindControl("txtGiorni"), TextBox).Text, "null") & " WHERE ID = " & i.Cells(0).Text
            par.cmd.ExecuteNonQuery()

        Next

    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            connData.apri(False)
            SalvaModalita()
            SalvaTipo()
            connData.chiudi(False)
            Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnSalva_Click - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home_ncp.aspx""</script>")
    End Sub
End Class
