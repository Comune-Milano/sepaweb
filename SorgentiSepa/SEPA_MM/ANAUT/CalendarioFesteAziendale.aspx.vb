
Partial Class ANAUT_CalendarioFesteAziendale
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0

        If Not IsPostBack Then
            CaricaDati()
            BindGrid()
        End If

    End Sub

    Private Function BindGrid()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select note,substr(giorno,7,2)||'/'||substr(giorno,5,2)||'/'||substr(giorno,1,4) as giorno1 from UTENZA_CHIUSURE_PR WHERE ID_FILIALE IS NULL AND ID_SPORTELLO IS NULL AND ID_OPERATORE IS NULL order by giorno asc", par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "UTENZA_CHIUSURE_PR")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Function

    Private Function CaricaDati()
        Try

            Dim i As Integer = 1
            For i = 1 To 31
                cmbGiorno.Items.Add(i)
            Next

            cmbMese.Items.Add(New ListItem("Gennaio", "01"))
            cmbMese.Items.Add(New ListItem("Febbraio", "02"))
            cmbMese.Items.Add(New ListItem("Marzo", "03"))
            cmbMese.Items.Add(New ListItem("Aprile", "04"))
            cmbMese.Items.Add(New ListItem("Maggio", "05"))
            cmbMese.Items.Add(New ListItem("Giugno", "06"))
            cmbMese.Items.Add(New ListItem("Luglio", "07"))
            cmbMese.Items.Add(New ListItem("Agosto", "08"))
            cmbMese.Items.Add(New ListItem("Settembre", "09"))
            cmbMese.Items.Add(New ListItem("Ottobre", "10"))
            cmbMese.Items.Add(New ListItem("Novembre", "11"))
            cmbMese.Items.Add(New ListItem("Dicembre", "12"))

            cmbAnno.Items.Add(New ListItem(CStr(Year(Now)), CStr(Year(Now))))
            cmbAnno.Items.Add(New ListItem(CStr(Year(Now) + 1), CStr(Year(Now) + 1)))



        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Function

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.Cells(0).Text <> "GIORNO/MESE/ANNO" Then

            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#eeeeee'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
            Else
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#dcdcdc'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
            End If
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "Insert into UTENZA_CHIUSURE_PR (ID_FILIALE,ID_SPORTELLO,ID_OPERATORE, GIORNO, NOTE) Values (NULL,NULL,NULL, '" & CMBANNO.SelectedItem.Value & cmbMese.SelectedItem.Value & Format(CInt(cmbGiorno.SelectedItem.Text), "00") & "', '" & par.PulisciStrSql(txtNote.Text) & "')"
            par.cmd.ExecuteNonQuery()

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            BindGrid()
            txtNote.Text = ""
            cmbGiorno.SelectedIndex = -1
            cmbGiorno.Items.FindByText("1").Selected = True

            cmbMese.SelectedIndex = -1
            cmbMese.Items.FindByText("Gennaio").Selected = True

            cmbAnno.SelectedIndex = -1
            cmbAnno.Items.FindByText(Year(Now)).Selected = True

        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

  

    Private Sub MessJQuery(ByVal Messaggio As String, ByVal Tipo As Integer, Optional ByVal Titolo As String = "Messaggio")
        Try
            Dim sc As String = ""
            If Tipo = 0 Then
                sc = ScriptErrori(Messaggio, Titolo)
            Else
                sc = ScriptChiudi(Messaggio, Titolo)
            End If
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptMsg", sc, True)
        Catch ex As Exception
            'lblErrore.Text = ex.Message
            'lblErrore.Visible = True
        End Try
    End Sub
    Private Function ScriptErrori(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Function ScriptChiudi(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');self.close();}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub SceltaJQuery(ByVal Messaggio As String, ByVal Funzione As String, Optional ByVal Titolo As String = "Messaggio", Optional ByVal Funzione2 As String = "")
        Try
            Dim sc As String = ScriptScelta(Messaggio, Funzione, Titolo, Funzione2)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptScelta", sc, True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function ScriptScelta(ByVal Messaggio As String, ByVal Funzione As String, Optional ByVal Titolo As String = "Messaggio", Optional ByVal Funzione2 As String = "") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptScelta').text('" & Messaggio & "');")
            sb.Append("$('#ScriptScelta').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "', buttons: {'Si': function() { __doPostBack('" & Funzione & "', '');{$(this).dialog('close');} },'No': function() {$(this).dialog('close');" & Funzione2 & "}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub btnEliminaGiorno_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaGiorno.Click

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "DELETE FROM UTENZA_CHIUSURE_PR WHERE ID_FILIALE IS NULL AND ID_SPORTELLO IS NULL AND ID_OPERATORE IS NULL AND GIORNO='" & Mid(Replace(LBLID.Value, "/", ""), 5, 4) & Mid(Replace(LBLID.Value, "/", ""), 3, 2) & Mid(Replace(LBLID.Value, "/", ""), 1, 2) & "'"
            par.cmd.ExecuteNonQuery()

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            BindGrid()

        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub
End Class
