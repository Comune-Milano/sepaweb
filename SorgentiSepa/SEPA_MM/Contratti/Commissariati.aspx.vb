
Partial Class Contratti_InteressiLegali
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            BindGrid()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub BindGrid()

        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        Dim Str As String = "SELECT * FROM SISCOM_MI.TAB_commissariati ORDER BY descrizione asc"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)

        Dim ds As New Data.DataSet()

        da.Fill(ds, "SISCOM_MI.TAB_COMMISSARIATI")

        DataGridIntLegali.DataSource = ds
        DataGridIntLegali.DataBind()

        par.OracleConn.Close()


    End Sub

    Protected Sub DataGridIntLegali_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridIntLegali.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Protected Sub DataGridIntLegali_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridIntLegali.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridIntLegali.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub

    Protected Sub ImgBtnAggiungi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnAggiungi.Click
        Me.Elimina()
    End Sub
    Private Sub Elimina()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If par.IfEmpty(Me.txtid.Value, "") <> "" Then

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.TAB_COMMISSARIATI WHERE ID = " & Me.txtid.Value
                par.cmd.ExecuteNonQuery()
                par.OracleConn.Close()

                BindGrid()
            Else
                Response.Write("<script>alert('Nessuna voce selezionata!')</script>")
                par.OracleConn.Close()

            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub img_ChiudiSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiSchema.Click
        txtCommissariato.Text = ""

    End Sub


    Protected Sub img_InserisciSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciSchema.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If par.IfEmpty(Me.txtCommissariato.Text, "") <> "" Then

                par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_COMMISSARIATI WHERE DESCRIZIONE = '" & par.PulisciStrSql(Me.txtCommissariato.Text) & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Response.Write("<script>alert('Valore già inserito!')</script>")
                    Me.txtCommissariato.Text = ""

                    Exit Sub
                End If
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.TAB_COMMISSARIATI (ID,DESCRIZIONE) VALUES (SISCOM_MI.SEQ_TAB_COMMISSARIATI.NEXTVAL,'" & par.PulisciStrSql(Me.txtCommissariato.Text) & "')"
                par.cmd.ExecuteNonQuery()
                par.OracleConn.Close()
                BindGrid()
            Else
                Response.Write("<script>alert('Campo obbligatorio!')</script>")

            End If
            Me.txtCommissariato.Text = ""

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

End Class
