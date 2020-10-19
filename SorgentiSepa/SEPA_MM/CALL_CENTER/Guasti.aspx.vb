
Partial Class CALL_CENTER_Guasti
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
            CERCA()
        End If



    End Sub

    Private Sub CERCA()
        Try
            par.cmd.CommandText = "SELECT tipologie_guasti.ID,tipologie_guasti.descrizione " _
                                & "FROM siscom_Mi.tipologie_guasti " _
                                & "ORDER BY tipologie_guasti.descrizione ASC "

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

    Protected Sub DataGridGuasti_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridGuasti.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato : " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato : " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub




    Protected Sub Salva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Salva.Click
        Try

            Dim str As String = ""
            If Not String.IsNullOrEmpty(Me.txtDescrizione.Text) Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                If txtid.Value = 0 Then
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.TIPOLOGIE_GUASTI WHERE UPPER(DESCRIZIONE) = '" & par.PulisciStrSql(UCase(txtDescrizione.Text)) & "'"
                    Dim num As Integer = 0
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        num = par.IfNull(lettore(0), 0)
                    End If
                    lettore.Close()
                    If num = 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.TIPOLOGIE_GUASTI (ID,DESCRIZIONE,ID_TIPO_ST,ID_STRUTTURA) " _
                            & "VALUES (SISCOM_MI.SEQ_TIPOLOGIE_GUASTI.NEXTVAL, '" & par.PulisciStrSql(UCase(Me.txtDescrizione.Text)) & "',NULL,NULL)"
                        par.cmd.ExecuteNonQuery()
                        Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                    Else
                        Response.Write("<script>alert('Tipologia già presente!');</script>")
                    End If
                Else
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.TIPOLOGIE_GUASTI WHERE UPPER(DESCRIZIONE) = '" & par.PulisciStrSql(UCase(txtDescrizione.Text)) & "' AND ID<>" & txtid.Value
                    Dim num As Integer = 0
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        num = par.IfNull(lettore(0), 0)
                    End If
                    lettore.Close()
                    If num = 0 Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.TIPOLOGIE_GUASTI SET DESCRIZIONE = '" & par.PulisciStrSql(UCase(Me.txtDescrizione.Text)) _
                                        & "', ID_TIPO_ST = NULL, ID_STRUTTURA = NULL WHERE ID = " & txtid.Value
                        par.cmd.ExecuteNonQuery()
                        Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                    Else
                        Response.Write("<script>alert('Tipologia già presente!');</script>")
                    End If
                End If
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    '*********************CHIUSURA CONNESSIONE**********************
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                CERCA()
            End If
            Me.TextBox1.Value = 1
            Me.txtid.Value = "0"
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

    Protected Sub btnModifica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnModifica.Click
        If Me.txtid.Value > 0 Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPOLOGIE_GUASTI WHERE ID=" & txtid.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim descrizione As String = ""
                If lettore.Read Then
                    descrizione = par.IfNull(lettore("DESCRIZIONE"), "")
                End If
                txtDescrizione.Text = descrizione

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
        Else
            Response.Write("<script>alert('Selezionare un elemento dalla lista!')</script>")
        End If
    End Sub
End Class
