
Partial Class ANAUT_DocNecessari
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)
                Dim Indice As String = "0"

                par.cmd.CommandText = "select * from utenza_bandi where stato=1 order by id desc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    lblBando.Text = "BANDO " & par.IfNull(myReader("descrizione"), "") & " - Data: " & Format(Now, "dd/MM/yyyy")
                    Indice = par.IfNull(myReader("id"), "0")
                End If
                myReader.Close()


                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                BindGrid()

                If Session.Item("ANAGRAFE_CONSULTAZIONE") = "1" Then
                    sololettura.Value = "1"
                    ImgBtnAggiungi.Visible = False
                    ImgModifica.Visible = False
                Else
                    sololettura.Value = "0"
                    ImgBtnAggiungi.Visible = True
                    ImgModifica.Visible = True
                End If

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblBando.Text = ex.Message
            End Try
        End If
    End Sub

    Private Sub BindGrid()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim Str As String = ""

            Str = "select id,descrizione from vsa_doc_necessari where id in (select id_documento from vsa_doc_tipo_necessari where id_tipo_domanda = " & Request.QueryString("T") & ")"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds, "VSA_DOC_NECESSARI")

            DataGridIntLegali.DataSource = ds
            DataGridIntLegali.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")
    End Sub

    Protected Sub img_InserisciSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciSchema.Click
        Try
            If Me.TextBox1.Value = 2 Then
                Update()
            ElseIf Me.TextBox1.Value = 1 Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                If par.IfEmpty(Me.txtQuartiere.Text, "") <> "" Then
                    Dim SOGGETTO As String = "0"

                    par.cmd.CommandText = "select * FROM VSA_DOC_NECESSARI WHERE upper(DESCRIZIONE) = '" & par.PulisciStrSql(Me.txtQuartiere.Text.ToUpper) & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Response.Write("<script>alert('Tipo già inserito!')</script>")
                        Me.txtQuartiere.Text = ""
                        '*********************CHIUSURA CONNESSIONE**********************
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub

                    End If
                    myReader.Close()
                    par.cmd.CommandText = "INSERT INTO VSA_DOC_NECESSARI (ID,DESCRIZIONE) VALUES (SEQ_VSA_DOC_NECESSARI.NEXTVAL,'" & par.PulisciStrSql(Me.txtQuartiere.Text.ToUpper) & "')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT * from VSA_DOC_NECESSARI WHERE ID = (SELECT MAX(ID) FROM VSA_DOC_NECESSARI)"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        par.cmd.CommandText = "INSERT INTO VSA_DOC_TIPO_NECESSARI (ID_DOCUMENTO,ID_TIPO_DOMANDA) VALUES (" & myReader("ID") & "," & Request.QueryString("T") & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader.Close()

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    BindGrid()
                Else
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script>alert('Campo obbligatorio!')</script>")
                End If
                Me.txtQuartiere.Text = ""
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub Update()
        Try

            If par.IfEmpty(Me.txtQuartiere.Text, "Null") <> "Null" Then
                ''*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "select * FROM VSA_DOC_NECESSARI WHERE upper(DESCRIZIONE) = '" & par.PulisciStrSql(Me.txtQuartiere.Text.ToUpper) & "' AND ID <>" & txtid.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Response.Write("<script>alert('Tipo già inserito!')</script>")
                    Me.txtQuartiere.Text = ""
                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If
                Dim SOGGETTO As String = "0"

                par.cmd.CommandText = "UPDATE VSA_DOC_NECESSARI SET DESCRIZIONE = '" & par.PulisciStrSql(Me.txtQuartiere.Text.ToUpper) & "' WHERE ID = " & Me.txtid.Value
                par.cmd.ExecuteNonQuery()

                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                par.cmd.Dispose()
                BindGrid()
                Me.TextBox1.Value = "0"
                Me.txtid.Value = ""
                Me.txtmia.Text = "Nessuna Selezione"
                Me.txtQuartiere.Text = ""

            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub DataGridIntLegali_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridIntLegali.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    Protected Sub DataGridIntLegali_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridIntLegali.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridIntLegali.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGridIntLegali_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridIntLegali.SelectedIndexChanged

    End Sub

    Protected Sub ImgModifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgModifica.Click
        If txtid.Value.ToString <> "" Then
            ''*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM VSA_DOC_NECESSARI WHERE ID = " & Me.txtid.Value

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.txtQuartiere.Text = myReader("DESCRIZIONE").ToString
                
            End If
            myReader.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            par.cmd.Dispose()

            Me.TextBox1.Value = "2"

        Else
            Me.TextBox1.Value = 0
            Response.Write("<script>alert('Nessuna Voce selezionata!')</script>")

        End If
    End Sub

    Protected Sub ImgBtnAggiungi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnAggiungi.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If par.IfEmpty(Me.txtid.Value, "") <> "" Then

                par.cmd.CommandText = "select * FROM VSA_DOC_NECESSARI WHERE ID = " & txtid.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.HasRows Then
                    Response.Write("<script>alert('Tipologia documento usata in delle dichirazioni. Non è possibile eliminare!')</script>")
                    Me.txtQuartiere.Text = ""
                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                par.cmd.CommandText = "DELETE FROM VSA_DOC_NECESSARI WHERE ID = " & Me.txtid.Value
                par.cmd.ExecuteNonQuery()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                BindGrid()
                '******pulizia variabili uso per modifica e cancellazione!!!++++++
                Me.txtmia.Text = "Nessuna Selezione"
                Me.txtid.Value = ""
            Else
                Response.Write("<script>alert('Nessuna voce selezionata!')</script>")
                par.OracleConn.Close()

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            If EX1.Number = 2292 Then
                lblErrore.Text = "In uso. Non è possibile eliminare!"
            Else
                lblErrore.Text = EX1.Message
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub img_ChiudiSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiSchema.Click

    End Sub
End Class
