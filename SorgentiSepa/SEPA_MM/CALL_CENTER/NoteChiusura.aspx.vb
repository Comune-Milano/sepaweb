
Partial Class CALL_CENTER_NoteChiusura
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then
            Response.Flush()
            Cerca()
            CaricaDiv()
        End If

    End Sub
    Private Sub Cerca()
        Try
            par.cmd.CommandText = "SELECT ID_TIPO_GUASTO, SEGNALAZIONI_NOTE_CHIUSURA.DESCRIZIONE AS NOTA_DESC,TIPOLOGIE_GUASTI.DESCRIZIONE AS TIPO_SEGN FROM SISCOM_MI.SEGNALAZIONI_NOTE_CHIUSURA,SISCOM_MI.TIPOLOGIE_GUASTI WHERE TIPOLOGIE_GUASTI.ID = SEGNALAZIONI_NOTE_CHIUSURA.ID_TIPO_GUASTO"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)

            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridGuasti.DataSource = dt
            DataGridGuasti.DataBind()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub
    Private Sub CaricaDiv()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPOLOGIE_GUASTI"
            cmbTipo.Items.Add(New ListItem("--", "-1"))
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbTipo.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()


            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception


            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub

    Protected Sub Salva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Salva.Click
        Try


            Dim str As String = ""

            If Not String.IsNullOrEmpty(Me.txtDescrizione.Text) AndAlso Me.cmbTipo.SelectedValue <> "-1" Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "insert into siscom_mi.SEGNALAZIONI_NOTE_CHIUSURA (ID_TIPO_GUASTO,descrizione) " _
                    & "values (" & Me.cmbTipo.SelectedValue & ", '" & par.PulisciStrSql(Me.txtDescrizione.Text.ToUpper) & "')"

                par.cmd.ExecuteNonQuery()
                Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    '*********************CHIUSURA CONNESSIONE**********************
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If


                Cerca()
            Else
                Response.Write("<script>alert('Definire il tipo e la descrizione!')</script>")
            End If

            Me.TextBox1.Value = 1
            Me.txtid.Value = "0"
            Me.txtOldDesc.Value = ""
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try


    End Sub

    Protected Sub DataGridGuasti_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridGuasti.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato : " & e.Item.Cells(2).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtOldDesc').value='" & e.Item.Cells(2).Text.Replace("'", "\'") & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato : " & e.Item.Cells(2).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtOldDesc').value='" & e.Item.Cells(2).Text.Replace("'", "\'") & "'")

        End If

    End Sub

    Protected Sub btnElimina_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click
        Try
            If Me.txtid.Value <> "0" And Me.txtOldDesc.Value <> "" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.SEGNALAZIONI_NOTE_CHIUSURA WHERE ID_TIPO_GUASTO = " & txtid.Value & " AND DESCRIZIONE = '" & par.PulisciStrSql(Me.txtOldDesc.Value) & "'"

                par.cmd.ExecuteNonQuery()

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    '*********************CHIUSURA CONNESSIONE**********************
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If


                Me.txtid.Value = "0"
                Me.txtOldDesc.Value = ""
                Cerca()
                Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")

            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub
End Class
