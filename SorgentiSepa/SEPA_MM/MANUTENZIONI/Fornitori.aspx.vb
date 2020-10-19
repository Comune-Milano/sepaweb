
Partial Class MANUTENZIONI_Fornitori
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
                Exit Sub
            End If
            BindGrid()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub BindGrid()
        Try

            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            '*********************RIEMPIMENTO DATAGRID**********************
            par.cmd.CommandText = "SELECT   ID, DESCRIZIONE FROM SISCOM_MI.ANAGRAFICA_FORNITORI ORDER BY DESCRIZIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "ANAGRAFICA_FORNITORI")
            DataGridFornitori.DataSource = ds
            DataGridFornitori.DataBind()

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub
    Protected Sub DataGridFornitori_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridFornitori.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(0).Text, "'", "") & "';document.getElementById('txtid').value='" & e.Item.Cells(1).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(0).Text, "'", "") & "';document.getElementById('txtid').value='" & e.Item.Cells(1).Text & "'")

        End If
    End Sub

    Protected Sub img_InserisciSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciSchema.Click
        If Me.TextBox1.Text = 2 Then
            Update()
        ElseIf Me.TextBox1.Text = 1 Then
            Salva()
        End If
        Me.txtid.Text = ""
        Me.txtmia.Text = ""
    End Sub
    Private Sub Salva()
        Try
            If par.IfEmpty(Me.txtDescrizione.Text, "null") <> "null" Then

                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA_FORNITORI WHERE DESCRIZIONE = '" & par.PulisciStrSql(Me.txtDescrizione.Text.ToString) & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Me.TextBox1.Text = 0
                    Response.Write("<script>alert('Fornitore esistente! Nessun dato salvato')</script>")

                    '*********************CHIUSURA CONNESSIONE**********************
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.ANAGRAFICA_FORNITORI(ID, DESCRIZIONE) VALUES (SISCOM_MI.SEQ_ANAGRAFICA_FORNITORI.NEXTVAL, '" & par.PulisciStrSql(Me.txtDescrizione.Text) & "')"
                par.cmd.ExecuteNonQuery()
                Me.txtmia.Text = ""
                Me.txtid.Text = ""
                Me.txtDescrizione.Text = ""
                Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")
                BindGrid()

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub
    Private Sub Update()
        Try
            If par.IfEmpty(Me.txtDescrizione.Text, "null") <> "null" AndAlso par.IfEmpty(Me.txtid.Text, "null") <> "null" Then

                If Me.txtDescrizione.Text <> vTesto Then

                    '*********************APERTURA CONNESSIONE**********************
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA_FORNITORI WHERE DESCRIZIONE = '" & par.PulisciStrSql(Me.txtDescrizione.Text.ToString) & "' AND ID <> " & Me.txtid.Text
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Me.TextBox1.Text = 0
                        Response.Write("<script>alert('Fornitore esistente!Nessun dato salvato!')</script>")
                        Me.txtmia.Text = ""
                        Me.txtid.Text = ""
                        Me.txtDescrizione.Text = ""
                        '*********************CHIUSURA CONNESSIONE**********************
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                        Exit Sub
                    End If
                    par.cmd.CommandText = "UPDATE SISCOM_MI.ANAGRAFICA_FORNITORI SET DESCRIZIONE = '" & par.PulisciStrSql(Me.txtDescrizione.Text) & "' WHERE ID = " & Me.txtid.Text
                    par.cmd.ExecuteNonQuery()

                    '*********************CHIUSURA CONNESSIONE**********************
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    Me.TextBox1.Text = 0
                    Me.txtmia.Text = ""
                    Me.txtid.Text = ""
                    Me.txtDescrizione.Text = ""
                    Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")
                    BindGrid()
                Else
                    Me.TextBox1.Text = 0
                    Me.txtmia.Text = ""
                    Me.txtid.Text = ""
                    Me.txtDescrizione.Text = ""
                    Response.Write("<script>alert('Nessuna modifica apportata al fornitore!')</script>")

                End If

            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub
    Protected Sub ImgModifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgModifica.Click
        Try
            If par.IfEmpty(Me.txtid.Text, "") <> "" Then
                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.ANAGRAFICA_FORNITORI WHERE ID = " & Me.txtid.Text.ToString
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Me.txtDescrizione.Text = myReader("DESCRIZIONE").ToString
                End If
                vTesto = Me.txtDescrizione.Text
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Me.TextBox1.Text = "2"

            Else
                Me.TextBox1.Text = 0
                Response.Write("<script>alert('Nessuna Voce selezionata!')</script>")

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()

        End Try
    End Sub

    Protected Sub ImgBtnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnElimina.Click
        Try
            If par.IfEmpty(Me.txtid.Text, "") <> "" Then

                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.ANAGRAFICA_FORNITORI WHERE ID = " & Me.txtid.Text.ToString
                par.cmd.ExecuteNonQuery()
                Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")
                BindGrid()
                Me.TextBox1.Text = 0
                Me.txtmia.Text = ""
                Me.txtid.Text = ""
                Me.txtDescrizione.Text = ""

            Else
                Me.TextBox1.Text = 0
                Response.Write("<script>alert('Nessuna Voce selezionata!')</script>")


            End If
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 2292 Then
                Response.Write("<script>alert('Scala in uso!Impossibile Eliminare!');</script>")
                par.myTrans.Rollback()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub img_ChiudiSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiSchema.Click
        Me.txtid.Text = ""
        Me.txtDescrizione.Text = ""
        Me.txtmia.Text = ""
    End Sub
    Private Property vTesto() As String
        Get
            If Not (ViewState("par_idEdificio") Is Nothing) Then
                Return CStr(ViewState("par_idEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_idEdificio") = value
        End Set

    End Property
End Class
