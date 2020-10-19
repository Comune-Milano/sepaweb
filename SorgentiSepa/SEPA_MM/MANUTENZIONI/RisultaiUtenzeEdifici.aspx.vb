
Partial Class MANUTENZIONI_RisultaiUtenzeEdifici
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        If Not IsPostBack Then
            BindGrid()

        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")

    End Sub
    Private Sub BindGrid()
        Try
            Dim QUERY As String = ""

            If Request.QueryString("CHIAMA") = "EDIF" Then
                QUERY = Session.Item("PED")

            ElseIf Request.QueryString("CHIAMA") = "COMP" Then


                If Request.QueryString("ID") <> "-1" Then
                    QUERY = QUERY & " AND COMPLESSI_IMMOBILIARI.ID =" & Request.QueryString("ID")
                End If
                If Request.QueryString("TIPOLOGIA") <> "-1" Then
                    QUERY = QUERY & " AND UTENZE.COD_TIPOLOGIA = '" & Request.QueryString("TIPOLOGIA") & "'"
                End If
                If Request.QueryString("FORNITORE") <> "-1" Then
                    QUERY = QUERY & " AND UTENZE.ID_FORNITORE = " & Request.QueryString("FORNITORE")
                End If

                If par.IfEmpty(Request.QueryString("CONT"), "null") <> "null" Then
                    QUERY = QUERY & " AND UTENZE.CONTATORE = '" & Request.QueryString("CONT") & "'"
                End If

                If par.IfEmpty(Request.QueryString("CONTR"), "null") <> "null" Then
                    QUERY = QUERY & " AND UTENZE.CONTRATTO ='" & Request.QueryString("CONTR") & "'"
                End If

                QUERY = "SELECT DISTINCT siscom_mi.UTENZE.ID,SISCOM_MI.COMPLESSI_IMMOBILIARI.ID AS ID_IMMOBILE, SISCOM_MI.TIPOLOGIA_UTENZA.DESCRIZIONE AS TIPOLOGIA,SISCOM_MI.ANAGRAFICA_FORNITORI.DESCRIZIONE AS DESC_FORNITORE, siscom_mi.UTENZE.CONTATORE,siscom_mi.UTENZE.CONTRATTO,siscom_mi.UTENZE.DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_UTENZA, siscom_mi.TABELLE_MILLESIMALI,SISCOM_MI.ANAGRAFICA_FORNITORI, siscom_mi.UTENZE_TABELLE_MILLESIMALI,siscom_mi.UTENZE, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE siscom_mi.UTENZE_TABELLE_MILLESIMALI.id_tabella_millesimale=siscom_mi.TABELLE_MILLESIMALI.ID AND siscom_mi.UTENZE_TABELLE_MILLESIMALI.ID_UTENZA = siscom_mi.UTENZE.ID AND COMPLESSI_IMMOBILIARI.ID = TABELLE_MILLESIMALI.ID_COMPLESSO AND ANAGRAFICA_FORNITORI.ID=UTENZE.ID_FORNITORE AND TIPOLOGIA_UTENZA.COD = UTENZE.COD_TIPOLOGIA" & QUERY

            End If

            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(QUERY, par.OracleConn)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il FORNITORE " & Replace(e.Item.Cells(1).Text, "'", "") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtIdEdif').value='" & e.Item.Cells(2).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il FORNITORE " & Replace(e.Item.Cells(1).Text, "'", "") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtIdEdif').value='" & e.Item.Cells(2).Text & "'")

        End If
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If Request.QueryString("CHIAMA") = "EDIF" Then
            Response.Redirect("Ut_Tab_Millesimali.aspx?ID=" & Me.txtid.Text & "&IDEDIF=" & Me.txtIdEdif.Text & "&IDCOMP=0&CHIAMA=" & Request.QueryString("CHIAMA"))
        Else
            Response.Redirect("Ut_Tab_Millesimali.aspx?ID=" & Me.txtid.Text & "&IDEDIF=0&IDCOMP=" & Me.txtIdEdif.Text & "&IDCOMPCHIAMA=" & Request.QueryString("ID") & "&CHIAMA=" & Request.QueryString("CHIAMA") & "&TIPOLOGIA=" & Request.QueryString("TIPOLOGIA") & "&FORNITORE=" & Request.QueryString("FORNITORE") & "&CONT=" & Request.QueryString("CONT") & "&CONTR=" & Request.QueryString("CONTR"))
        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

End Class
