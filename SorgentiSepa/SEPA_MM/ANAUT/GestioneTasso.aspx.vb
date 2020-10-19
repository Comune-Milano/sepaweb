
Partial Class ANAUT_GestioneTasso
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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

            Dim Str As String = "SELECT * FROM TAB_TASSO_RENDIMENTO ORDER BY ANNO DESC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds, "TAB_TASSO_RENDIMENTO")

            DataGridCapitoli.DataSource = ds
            DataGridCapitoli.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub


    Protected Sub DataGridCapitoli_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridCapitoli.ItemDataBound
        If e.Item.Cells(1).Text <> "TASSO DI INTERESSE" Then
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#eeeeee'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(0).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';")
            Else
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#dcdcdc'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(0).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';")
            End If
        End If
    End Sub

    Protected Sub DataGridCapitoli_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridCapitoli.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridCapitoli.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGridCapitoli_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGridCapitoli.SelectedIndexChanged

    End Sub

    Protected Sub img_ChiudiSchema_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiSchema.Click
        txtAnno.Text = ""
        txtTasso.Text = ""
        Me.txtid.Value = ""
        Me.txtmia.Text = "Nessuna Selezione"
    End Sub

    Protected Sub img_InserisciSchema_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciSchema.Click
        Try
            Dim CodComune As String = ""

            If Me.TextBox1.Value = 2 Then
                Update()
            ElseIf Me.TextBox1.Value = 1 Then
                Dim lIdIndirizzo As Double = 0

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                If par.IfEmpty(Me.txtAnno.Text, "Null") <> "Null" And par.IfEmpty(Me.txtTasso.Text, "Null") <> "Null" And IsNumeric(txtAnno.Text) And IsNumeric(txtTasso.Text) Then

                    par.cmd.CommandText = "select * FROM TAB_TASSO_RENDIMENTO WHERE ANNO = '" & par.PulisciStrSql(Me.txtAnno.Text.ToUpper) & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Response.Write("<script>alert('Anno già inserito!')</script>")

                        txtAnno.Text = ""
                        txtTasso.Text = ""
                        '*****CHIUSURA DEL MYREADER
                        myReader.Close()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        txtAnno.Enabled = True
                        Exit Sub
                    End If

                    par.cmd.CommandText = "INSERT INTO TAB_TASSO_RENDIMENTO (ANNO,TASSO) VALUES (" & par.PulisciStrSql(Me.txtAnno.Text) & "," & par.VirgoleInPunti(Me.txtTasso.Text) & ")"
                    par.cmd.ExecuteNonQuery()


                    txtAnno.Text = ""
                    txtTasso.Text = ""
                    Me.txtid.Value = ""
                    Me.txtmia.Text = "Nessuna Selezione"

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    BindGrid()
                Else
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script>alert('Campi obbligatori!')</script>")
                    txtAnno.Enabled = True
                End If
                ' Me.txtNome.Text = ""
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub Update()

        If par.IfEmpty(Me.txtAnno.Text, "Null") <> "Null" And par.IfEmpty(Me.txtTasso.Text, "Null") <> "Null" And IsNumeric(txtAnno.Text) And IsNumeric(txtTasso.Text) Then
            ''*********************APERTURA CONNESSIONE**********************

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select count(anno) FROM TAB_TASSO_RENDIMENTO WHERE ANNO = " & par.PulisciStrSql(Me.txtAnno.Text.ToUpper) & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If myReader(0) > 1 Then
                    Response.Write("<script>alert('Anno già inserito!')</script>")
                    txtAnno.Text = ""
                    txtTasso.Text = ""
                    txtAnno.Enabled = True
                    '*****CHIUSURA DEL MYREADER
                    myReader.Close()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Me.TextBox1.Value = "0"
                    Exit Sub
                End If
            End If

            par.cmd.CommandText = "UPDATE TAB_TASSO_RENDIMENTO SET TASSO=" & par.VirgoleInPunti(Me.txtTasso.Text) & " WHERE ANNO = " & Me.txtAnno.Text
            par.cmd.ExecuteNonQuery()


            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            par.cmd.Dispose()
            BindGrid()

            Me.TextBox1.Value = "0"
            txtAnno.Text = ""
            txtTasso.Text = ""
            txtAnno.Enabled = True
            Me.txtid.Value = ""
            Me.txtmia.Text = "Nessuna Selezione"
        End If

    End Sub

    Protected Sub ImgModifica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgModifica.Click
        If txtid.Value <> "" Then
            ''*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT  * FROM TAB_TASSO_RENDIMENTO WHERE ANNO = " & txtid.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.txtTasso.Text = par.IfNull(myReader("TASSO").ToString, "")
                Me.txtAnno.Text = par.IfNull(myReader("ANNO").ToString, "")
                txtAnno.Enabled = False
            End If
            'vTesto = Me.txtDescrizione.Text
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            par.cmd.Dispose()

            Me.TextBox1.Value = "2"

        Else
            Me.TextBox1.Value = 0
            Response.Write("<script>alert('Nessuna Anno selezionato!')</script>")
            txtAnno.Enabled = True
        End If
    End Sub

    Protected Sub ImgBtnAggiungi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnAggiungi.Click
        If eliminato.Value = "1" Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                If par.IfEmpty(txtid.Value, "") <> "" Then

                    par.cmd.CommandText = "DELETE FROM TAB_TASSO_RENDIMENTO WHERE ANNO = " & txtid.Value
                    par.cmd.ExecuteNonQuery()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Me.TextBox1.Value = "0"
                    txtAnno.Text = ""
                    txtTasso.Text = ""
                    txtAnno.Enabled = True
                    Me.txtid.Value = ""
                    Me.txtmia.Text = "Nessuna Selezione"
                    BindGrid()
                Else
                    Response.Write("<script>alert('Nessuna Anno selezionato!')</script>")
                    par.OracleConn.Close()

                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
            Catch EX1 As Oracle.DataAccess.Client.OracleException
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Me.lblErrore.Visible = True
                If EX1.Number = 2292 Then
                    lblErrore.Text = "Anno in uso. Non è possibile eliminare!"
                Else
                    lblErrore.Text = EX1.Message
                End If
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
        End If
    End Sub
End Class
