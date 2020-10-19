
Partial Class AMMSEPA_Zone
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:500px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        
        If Not IsPostBack Then
            Response.Flush()
            CaricaZone()
        End If
    End Sub

    Private Sub CaricaZone()
        Try
            Dim sStringaSQL As String = ""
            par.OracleConn.Open()
            sStringaSQL = "SELECT * FROM ZONA_ALER ORDER BY ZONA ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            lblrecord.Text = "Trovate: " & dt.Rows.Count & " zone"
            DataGrZone.DataSource = dt
            DataGrZone.DataBind()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            txtmia.Text = "Nessuna Selezione"
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblerrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub DataGrZone_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrZone.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la zona " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la zona " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
    End Sub

    Protected Sub DataGrZone_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrZone.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrZone.CurrentPageIndex = e.NewPageIndex
            CaricaZone()
        End If
    End Sub

    Protected Sub btnElimina_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click
        Try
            If ConfElimina.Value = 1 Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand
                par.cmd.CommandText = "SELECT ZONA_ALER.* FROM ZONA_ALER,SISCOM_MI.EDIFICI WHERE EDIFICI.ID_ZONA = ZONA_ALER.COD AND ZONA_ALER.COD =" & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    Response.Write("<script>alert('Non è possibile cancellare il dato selezionato!!');</script>")
                Else
                    par.cmd.CommandText = "DELETE FROM ZONA_ALER WHERE COD = " & LBLID.Value
                    par.cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Operazione completata!!');</script>")
                End If
                myReader.Close()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            TextBox1.Value = "0"
            LBLID.Value = 0
            ConfElimina.Value = 0
            CaricaZone()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub img_InserisciSchema_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciSchema.Click
        Try
            If TextBox1.Value <> "2" Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand

                If par.IfEmpty(txtNomeZona.Text, "") <> "" And par.IfEmpty(txtDescrizione.Text, "") <> "" Then
                    par.cmd.CommandText = "select * FROM ZONA_ALER WHERE ZONA = " & txtNomeZona.Text
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Response.Write("<script>alert('Numero zona già inserito!')</script>")
                        txtNomeZona.Text = ""
                        txtDescrizione.Text = ""
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If
                    myReader.Close()
                    par.cmd.CommandText = "INSERT INTO ZONA_ALER (COD,ZONA,NOMINATIVO) VALUES (SEQ_ZONA_ALER.NEXTVAL," & txtNomeZona.Text & ",'" & par.PulisciStrSql(txtDescrizione.Text) & "')"
                    par.cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('Operazione completata!')</script>")
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    TextBox1.Value = "0"
                Else
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    TextBox1.Value = "1"
                    Response.Write("<script>alert('Campi obbligatori!')</script>")
                End If
                txtNomeZona.Text = ""
                txtDescrizione.Text = ""

                CaricaZone()
            Else
                Update()
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblerrore.Text = ex.Message
        End Try
    End Sub

    Private Sub Update()
        Try

            If par.IfEmpty(txtNomeZona.Text, "") <> "" And par.IfEmpty(txtDescrizione.Text, "") <> "" Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "select * FROM ZONA_ALER WHERE ZONA = " & txtNomeZona.Text & " AND COD <>" & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Response.Write("<script>alert('Numero zona già inserito!')</script>")
                    txtNomeZona.Text = ""
                    txtDescrizione.Text = ""
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If
                myReader.Close()

                par.cmd.CommandText = "UPDATE ZONA_ALER SET ZONA= " & txtNomeZona.Text & ",NOMINATIVO = '" & par.PulisciStrSql(txtDescrizione.Text) & "' WHERE COD = " & LBLID.Value
                par.cmd.ExecuteNonQuery()

                Response.Write("<script>alert('Operazione completata!')</script>")

                par.OracleConn.Close()
                par.cmd.Dispose()
                CaricaZone()
                TextBox1.Value = "0"
                LBLID.Value = ""
                txtmia.Text = "Nessuna Selezione"
                txtNomeZona.Text = ""
                txtDescrizione.Text = ""
            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Campi obbligatori!')</script>")
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblerrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub img_ChiudiSchema_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiSchema.Click
        txtNomeZona.Text = ""
        txtDescrizione.Text = ""
        LBLID.Value = ""
        Me.txtmia.Text = "Nessuna Selezione"
        TextBox1.Value = "0"
    End Sub

    Protected Sub btnModifica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnModifica.Click
        If LBLID.Value <> "" Then
            ''*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM ZONA_ALER WHERE COD = " & LBLID.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                txtDescrizione.Text = par.IfNull(myReader("NOMINATIVO"), "")
                txtNomeZona.Text = par.IfNull(myReader("ZONA"), "")
            End If
            myReader.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            par.cmd.Dispose()

            Me.TextBox1.Value = "2"
        Else

            Me.TextBox1.Value = "0"
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub

    
End Class
