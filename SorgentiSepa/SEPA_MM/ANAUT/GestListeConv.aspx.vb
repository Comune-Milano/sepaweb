
Partial Class ANAUT_GestListeConv
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0
        If IsPostBack = False Then
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()

        Try


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim Str As String = "SELECT replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''DettagliListaConv.aspx?ID='||UTENZA_LISTE.ID||''',''Dettagli'','''');£>'||'DETTAGLI'||'</a>','$','&'),'£','" & Chr(34) & "') as VISUALIZZA,UTENZA_LISTE.* FROM UTENZA_LISTE ORDER BY UTENZA_LISTE.DATA_ORA DESC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds, "UTENZA_LISTE")

            DataGridCapitoli.DataSource = ds
            DataGridCapitoli.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub DataGridCapitoli_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridCapitoli.ItemDataBound
        If e.Item.Cells(1).Text <> "DESCRIZIONE" Then
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#eeeeee'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';")
            Else
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#dcdcdc'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';")
            End If
        End If
    End Sub

    Protected Sub DataGridCapitoli_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridCapitoli.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridCapitoli.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGridCapitoli_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridCapitoli.SelectedIndexChanged
       
    End Sub

    Protected Sub ImgBtnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnElimina.Click
        If eliminato.Value = "1" Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans


                par.cmd.CommandText = "UPDATE UTENZA_LISTE_CDETT SET ID_LISTA_CONV=NULL where ID_LISTA_CONV = " & txtid.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "delete from UTENZA_LISTE where id=" & txtid.Value
                par.cmd.ExecuteNonQuery()

                par.myTrans.Commit()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione effettuata!');</script>")
                txtid.Value = ""
                eliminato.Value = "0"
                txtmia.Text = "Nessuna Selezione"
                BindGrid()

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If
    End Sub
End Class
