
Partial Class CICLO_PASSIVO_CicloPassivo_Plan_GestioneCapitoli
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

            Dim Str As String = "SELECT * FROM SISCOM_MI.PF_CAPITOLI where id<>12 ORDER BY COD asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds, "PF_CAPITOLI")

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

    Protected Sub DataGridCapitoli_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridCapitoli.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridCapitoli.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGridCapitoli_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridCapitoli.SelectedIndexChanged

    End Sub

    Protected Sub img_ChiudiSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiSchema.Click
        txtCodice.Text = ""
        txtDescrizione.Text = ""
        Me.txtid.Value = ""
        Me.txtmia.Text = "Nessuna Selezione"
    End Sub

    Protected Sub img_InserisciSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciSchema.Click
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

                If par.IfEmpty(Me.txtCodice.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtDescrizione.Text, "Null") <> "Null" Then

                    par.cmd.CommandText = "select * FROM SISCOM_MI.PF_CAPITOLI WHERE UPPER(COD) = '" & par.PulisciStrSql(Me.txtCodice.Text.ToUpper) & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Response.Write("<script>alert('Codice capitolo già inserito!')</script>")
                        Me.txtCodice.Text = ""
                        txtDescrizione.Text = ""
                        '*****CHIUSURA DEL MYREADER
                        myReader.Close()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_CAPITOLI (ID,COD,DESCRIZIONE) VALUES (SISCOM_MI.SEQ_PF_CAPITOLI.NEXTVAL,'" & par.PulisciStrSql(Me.txtCodice.Text.ToUpper) & "','" & par.PulisciStrSql(Me.txtDescrizione.Text) & "')"
                    par.cmd.ExecuteNonQuery()





                    Me.txtCodice.Text = ""
                    Me.txtDescrizione.Text = ""
                    Me.txtid.Value = ""
                    Me.txtmia.Text = "Nessuna Selezione"

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script>alert('Operazione effettuata!')</script>")
                    BindGrid()
                Else
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script>alert('Campi obbligatori!')</script>")
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

        If par.IfEmpty(Me.txtCodice.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtDescrizione.Text, "Null") <> "Null" Then
            ''*********************APERTURA CONNESSIONE**********************

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select * FROM SISCOM_MI.PF_CAPITOLI WHERE UPPER(COD) = '" & par.PulisciStrSql(Me.txtCodice.Text.ToUpper) & "' AND ID <> " & Me.txtid.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Response.Write("<script>alert('Codice capitolo già inserito!')</script>")
                Me.txtCodice.Text = ""
                txtDescrizione.Text = ""
                '*****CHIUSURA DEL MYREADER
                myReader.Close()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If

            par.cmd.CommandText = "UPDATE SISCOM_MI.PF_CAPITOLI SET COD = '" & par.PulisciStrSql(Me.txtCodice.Text.ToUpper) & "',DESCRIZIONE='" & par.PulisciStrSql(Me.txtDescrizione.Text) & "' WHERE ID = " & Me.txtid.Value
            par.cmd.ExecuteNonQuery()



            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            par.cmd.Dispose()
            Response.Write("<script>alert('Operazione effettuata!')</script>")
            BindGrid()

            Me.TextBox1.Value = "0"
            Me.txtCodice.Text = ""
            Me.txtDescrizione.Text = ""
            Me.txtid.Value = ""
            Me.txtmia.Text = "Nessuna Selezione"
        End If

    End Sub

    Protected Sub ImgModifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgModifica.Click
        If txtid.Value.ToString <> "" Then
            ''*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT  * FROM SISCOM_MI.PF_CAPITOLI WHERE ID = " & Me.txtid.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.txtCodice.Text = par.IfNull(myReader("COD").ToString, "")
                Me.txtDescrizione.Text = par.IfNull(myReader("DESCRIZIONE").ToString, "")
               
            End If
            'vTesto = Me.txtDescrizione.Text
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
        If eliminato.Value = "1" Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                If par.IfEmpty(Me.txtid.Value, "") <> "" Then

                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.PF_CAPITOLI WHERE ID = " & Me.txtid.Value
                    par.cmd.ExecuteNonQuery()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    BindGrid()
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
                    lblErrore.Text = "Capitolo in uso. Non è possibile eliminare!"
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


    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click

    End Sub
End Class
