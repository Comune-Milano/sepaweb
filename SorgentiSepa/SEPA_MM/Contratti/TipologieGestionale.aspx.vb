
Partial Class Contratti_TipologieGestionale
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            CaricaGestionali()
        End If
    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function


    Private Sub CaricaGestionali()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim sStringaSQL As String = ""

            sStringaSQL = "SELECT id,descrizione,(case when utilizzabile=1 then 'SI' else 'NO' end) as utilizzabile,(case when fl_ripartibile=1 then 'SI' else 'NO' end) as fl_ripartibile FROM SISCOM_MI.TIPO_BOLLETTE_GEST ORDER BY DESCRIZIONE ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            DataGrDocGest.DataSource = dt
            DataGrDocGest.DataBind()

            txtmia.Text = "Nessuna Selezione"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub DataGrDocGest_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrDocGest.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la tipologia " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la tipologia " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
    End Sub

    Protected Sub btn_inserisci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_inserisci.Click
        Try
            Dim scriptblock As String = ""

            If txtMotivazione.Text = "" Then
                scriptblock = "<script language='javascript' type='text/javascript'> alert('Inserire la motivazione!');</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                End If
                Exit Try
            End If

            If Modificato.Value <> "2" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "INSERT INTO siscom_mi.TIPO_BOLLETTE_GEST (ID,DESCRIZIONE,ACRONIMO,UTILIZZABILE,FL_RIPARTIBILE) VALUES (SISCOM_MI.SEQ_TIPO_BOLLETTE_GEST.NEXTVAL,'" & par.PulisciStrSql(txtMotivazione.Text) & "'," _
                    & "'" & par.PulisciStrSql(txtAcronimo.Text) & "'," & Valore01(chkUtilizzabile.Checked) & "," & Valore01(chkRipartibile.Checked) & ")"
                par.cmd.ExecuteNonQuery()

                scriptblock = "<script language='javascript' type='text/javascript'>alert('Operazione Effettuata!');</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                End If
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Modificato.Value = "0"

                txtMotivazione.Text = ""

                CaricaGestionali()
            Else
                Update()
            End If


        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub Update()
        Try
            Dim scriptblock As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If txtMotivazione.Text = "" Then
                scriptblock = "<script language='javascript' type='text/javascript'> alert('Inserire la motivazione!');</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                End If
                Exit Try
            End If

            par.cmd.CommandText = "UPDATE siscom_mi.TIPO_BOLLETTE_GEST SET DESCRIZIONE = '" & par.PulisciStrSql(txtMotivazione.Text) & "',ACRONIMO='" & par.PulisciStrSql(txtAcronimo.Text) & "',UTILIZZABILE=" & Valore01(chkUtilizzabile.Checked) & ",FL_RIPARTIBILE=" & Valore01(chkRipartibile.Checked) & " WHERE ID = " & LBLID.Value
            par.cmd.ExecuteNonQuery()

            scriptblock = "<script language='javascript' type='text/javascript'>alert('Operazione Effettuata!');</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Modificato.Value = "0"
            CaricaGestionali()
            LBLID.Value = 0
            txtmia.Text = "Nessuna Selezione"
            txtMotivazione.Text = ""

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnElimina_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click
        Try
            Dim scriptblock As String = ""

            If ConfElimina.Value = 1 Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "SELECT * FROM siscom_mi.BOL_BOLLETTE_GEST WHERE ID_TIPO=" & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    Response.Write("<script>alert('Non e\' possibile cancellare il dato selezionato!!');</script>")
                Else
                    par.cmd.CommandText = "DELETE FROM siscom_mi.TIPO_BOLLETTE_GEST WHERE ID = " & LBLID.Value
                    par.cmd.ExecuteNonQuery()

                    scriptblock = "<script language='javascript' type='text/javascript'>alert('Operazione Effettuata!');</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5666")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5666", scriptblock)
                    End If
                End If
                myReader.Close()

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Modificato.Value = 0
            txtmia.Text = "Nessuna Selezione"
            LBLID.Value = 0
            ConfElimina.Value = 0
            DataGrDocGest.CurrentPageIndex = 0
            CaricaGestionali()

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btn_chiudi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_chiudi.Click
        txtMotivazione.Text = ""
        LBLID.Value = 0
        Me.txtmia.Text = "Nessuna Selezione"
        Modificato.Value = "0"
        lbl_titMotiv.Text = "Nuova Motivazione"
    End Sub

    Protected Sub btnModifica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnModifica.Click
        Try
            lbl_titMotiv.Text = "Modifica Motivazione"

            If LBLID.Value <> "0" Then
                ''*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPO_BOLLETTE_GEST WHERE ID = " & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    txtMotivazione.Text = par.IfNull(myReader("DESCRIZIONE"), "")
                    txtAcronimo.Text = par.IfNull(myReader("ACRONIMO"), "")
                    If par.IfNull(myReader("UTILIZZABILE"), "") = 1 Then
                        chkUtilizzabile.Checked = True
                    Else
                        chkUtilizzabile.Checked = False
                    End If
                    If par.IfNull(myReader("FL_RIPARTIBILE"), "") = 1 Then
                        chkRipartibile.Checked = True
                    Else
                        chkRipartibile.Checked = False
                    End If
                End If
                myReader.Close()

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Me.Modificato.Value = 2
            Else
                '  Me.Modificato.Value = 0
                ' Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
