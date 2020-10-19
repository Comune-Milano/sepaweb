
Partial Class AMMSEPA_GestioneMicrozone
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:500px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        Response.Flush()
        CaricaMicrozone()
    End Sub

    Private Sub CaricaMicrozone()
        Try
            Dim sStringaSQL As String = ""
            par.OracleConn.Open()
            sStringaSQL = "SELECT * FROM SISCOM_MI.TAB_MICROZONE ORDER BY DESCRIZIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            lblrecord.Text = "Trovate: " & dt.Rows.Count & " microzone"
            DataGrMicrozone.DataSource = dt
            DataGrMicrozone.DataBind()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            txtmia.Text = "Nessuna Selezione"

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub DataGrMicrozone_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrMicrozone.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la microzona " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la microzona " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
    End Sub

    Protected Sub DataGrMicrozone_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrMicrozone.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrMicrozone.CurrentPageIndex = e.NewPageIndex
            CaricaMicrozone()
        End If
    End Sub

    Protected Sub btnelimina_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click
        Try
            If ConfElimina.Value = 1 Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand
                par.cmd.CommandText = "SELECT TAB_MICROZONE.* FROM SISCOM_MI.TAB_MICROZONE,SISCOM_MI.EDIFICI WHERE EDIFICI.ID_MICROZONA = TAB_MICROZONE.ID AND TAB_MICROZONE.ID =" & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    Response.Write("<script>alert('Non è possibile cancellare il dato selezionato!!');</script>")
                Else
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.TAB_MICROZONE WHERE ID = " & LBLID.Value
                    par.cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Operazione completata!!');</script>")
                End If
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            LBLID.Value = 0
            ConfElimina.Value = 0
            CaricaMicrozone()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub img_InserisciSchema_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciSchema.Click
        Try
            If TextBox1.Value <> 2 Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand

                If par.IfEmpty(txtMicrozona.Text, "") <> "" And par.IfEmpty(txtValore.Text, "") <> "" Then
                    par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_MICROZONE WHERE upper(DESCRIZIONE) = '" & par.PulisciStrSql(txtMicrozona.Text.ToUpper) & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Response.Write("<script>alert('Valore già inserito!')</script>")
                        txtMicrozona.Text = ""

                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.TAB_MICROZONE (ID,DESCRIZIONE,VALORE_MEDIO) VALUES (SISCOM_MI.SEQ_TAB_MICROZONE.NEXTVAL,'" & par.PulisciStrSql(txtMicrozona.Text.ToUpper) & "'," & par.VirgoleInPunti(txtValore.Text) & ")"
                    par.cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('Operazione completata!')</script>")
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Else
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    TextBox1.Value = 2
                    Response.Write("<script>alert('Campo obbligatorio!')</script>")
                End If
                txtMicrozona.Text = ""
                txtValore.Text = ""
                CaricaMicrozone()
            Else
                Update()
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub Update()
        Try

            If par.IfEmpty(txtMicrozona.Text, "") <> "" And par.IfEmpty(txtValore.Text, "") <> "" Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_MICROZONE WHERE upper(DESCRIZIONE) = '" & par.PulisciStrSql(txtMicrozona.Text.ToUpper) & "' AND ID <>" & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Response.Write("<script>alert('Valore già inserito!')</script>")
                    txtMicrozona.Text = ""

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                par.cmd.CommandText = "UPDATE SISCOM_MI.TAB_MICROZONE SET DESCRIZIONE = '" & par.PulisciStrSql(txtMicrozona.Text.ToUpper) & "',VALORE_MEDIO=" & par.VirgoleInPunti(txtValore.Text) & " WHERE ID = " & LBLID.Value
                par.cmd.ExecuteNonQuery()

                Response.Write("<script>alert('Operazione completata!')</script>")

                par.OracleConn.Close()
                par.cmd.Dispose()
                CaricaMicrozone()
                TextBox1.Value = "0"
                LBLID.Value = ""
                txtmia.Text = "Nessuna Selezione"
                txtMicrozona.Text = ""
                txtValore.Text = ""
            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Campo obbligatorio!')</script>")
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub img_ChiudiSchema_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiSchema.Click
        txtMicrozona.Text = ""
        txtValore.Text = ""
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

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_MICROZONE WHERE ID = " & LBLID.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                txtMicrozona.Text = par.IfNull(myReader("DESCRIZIONE"), "")
                txtValore.Text = par.IfNull(myReader("VALORE_MEDIO"), "")
            End If
            myReader.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            par.cmd.Dispose()

            Me.TextBox1.Value = "2"
        Else
            Me.TextBox1.Value = 0
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('../../pagina_home.aspx');</script>")
    End Sub
End Class
