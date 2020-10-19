
Partial Class Condomini_Delegati
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        If Not IsPostBack Then
            Response.Flush()
            Cerca()
        End If
    End Sub
    Private Sub Cerca()
        Try

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_BUILDING_MANAGER ORDER BY COGNOME ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "COND_BUILDING_MANAGER")
            DataGridBManager.DataSource = ds
            DataGridBManager.DataBind()


            '*********************CHIUSURA CONNESSIONE**********************

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()




        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If Not String.IsNullOrEmpty(Me.txtCognome.Text) Then
            Try
                If Me.TextBox1.Value = 2 Then
                    Update()
                Else
                    '*******************APERURA CONNESSIONE*********************
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_BUILDING_MANAGER(ID,COGNOME,NOME) VALUES(SISCOM_MI.SEQ_COND_BUILDING_MANAGER.NEXTVAL,'" & par.PulisciStrSql(Me.txtCognome.Text.ToUpper) & "', '" & par.PulisciStrSql(Me.TxtNome.Text.ToUpper) & "')"
                    par.cmd.ExecuteNonQuery()
                    Me.txtCognome.Text = ""
                    Me.TxtNome.Text = ""


                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Me.TextBox1.Value = "0"
                    Cerca()
                End If

            Catch ex As Exception
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
            End Try

        End If
    End Sub
    Private Sub Update()
        If Not String.IsNullOrEmpty(Me.txtCognome.Text) Then
            Try
                '*******************APERURA CONNESSIONE*********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_BUILDING_MANAGER SET COGNOME = ' " & par.PulisciStrSql(Me.txtCognome.Text.ToUpper) & "', NOME = '" & par.PulisciStrSql(Me.TxtNome.Text.ToUpper) & "' WHERE ID = " & Me.txtid.Value
                par.cmd.ExecuteNonQuery()
                Me.txtid.Value = ""
                Me.txtmia.Text = "Nessuna Voce Selezionata"
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Me.TextBox1.Value = "0"
                Cerca()

            Catch ex As Exception
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
            End Try

        End If
    End Sub

    Protected Sub DataGridDelegati_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridBManager.ItemDataBound
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

    Protected Sub ImgModifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgModifica.Click
        If txtid.Value.ToString <> "" Then
            ''*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT  * FROM SISCOM_MI.COND_BUILDING_MANAGER WHERE ID = " & Me.txtid.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.TxtNome.Text = myReader("NOME").ToString
                Me.txtCognome.Text = myReader("COGNOME").ToString
            End If
            'vTesto = Me.txtDescrizione.Text
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            par.cmd.Dispose()
            Me.txtmia.Text = "Nessuna Voce Selezionata"
            Me.TextBox1.Value = "2"

        Else
            Me.TextBox1.Value = 0
            Response.Write("<script>alert('Nessuna Voce selezionata!')</script>")

        End If
    End Sub

    Protected Sub ImgBtnAggiungi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnAggiungi.Click
        Try
            If txtConfElimina.Value = 1 Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                If par.IfEmpty(Me.txtid.Value, "") <> "" Then

                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_BUILDING_MANAGER WHERE ID = " & Me.txtid.Value
                    par.cmd.ExecuteNonQuery()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Cerca()
                    Me.txtid.Value = ""
                    Me.txtmia.Text = "Nessuna Voce Selezionata"
                Else
                    Response.Write("<script>alert('Nessuna voce selezionata!')</script>")
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
            Else
                Me.txtConfElimina.Value = 0
                Me.txtid.Value = 0
                Me.txtmia.Text = "Nessuna Voce Selezionata"
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.LblErrore.Visible = True
            If EX1.Number = 2292 Then
                LblErrore.Text = "Delegato in uso. Non è possibile eliminare il delegato selezionato!"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Else
                LblErrore.Text = EX1.Message
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")

    End Sub
End Class
