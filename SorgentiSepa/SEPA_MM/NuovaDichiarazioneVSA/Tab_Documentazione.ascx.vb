
Partial Class ANAUT_Tab_Documentazione
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub DataGridDocum_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDocum.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#DFDFDF';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#C1C1C1';" _
                                & "document.getElementById('dic_Documenti1_idDoc').value='" & Replace(e.Item.Cells(0).Text, "'", "\'") & "'; document.getElementById('dic_Documenti1_descrizione').value='" & Replace(e.Item.Cells(2).Text, "'", "\'") & "';")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        imgAggDoc.Attributes.Add("OnClick", "javascript:AggiungiDocumento();")
        txtDataDocManc.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        ''If Not IsPostBack Then
        ''    CaricaLista()
        ''End If
        'Dim CTRL As Control
        For Each CTRL In Me.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is DataGrid Then
                DirectCast(CTRL, DataGrid).Attributes.Add("onload", "javascript:document.getElementById('idDoc').value='-1';")
            End If
        Next

        btnDeleteDoc.Attributes.Add("OnClick", "javascript:document.getElementById('provenienza').value='doc';")


    End Sub

    Protected Sub btnDeleteDoc_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDeleteDoc.Click
        If idDoc.Value = "-1" Then
            ' ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "alert('Selezionare un documento dalla lista!');", True)
            'CType(Me.Page, Object).MessJQuery("Selezionare un componente dalla lista!", 0, "Attenzione")
            CType(Me.Page, Object).avviso()
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "Verifica();", True)
            '  CType(Me.Page, Object).EliminaDocumenti()
            ' idDoc.Value = "-1"
        End If
    End Sub

    Public Sub CaricaLista()
        Try

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            chkListDocumenti.Items.Clear()
            par.cmd.CommandText = "select id,descrizione from vsa_doc_necessari order by descrizione asc"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            While lettore.Read
                Me.chkListDocumenti.Items.Add(New ListItem(par.IfNull(lettore("descrizione"), " "), par.IfNull(lettore("id"), -1)))
            End While
            lettore.Close()

            'If Not String.IsNullOrEmpty(CType(Me.Page, Object).lIdDichiarazione) Then
            '    par.cmd.CommandText = "select id_doc from vsa_doc_allegati where id_dichiarazione = " & CType(Me.Page, Object).lIdDichiarazione
            '    lettore = par.cmd.ExecuteReader
            '    While lettore.Read
            '        documAlleg.Value = 1
            '        Me.chkListDocumenti.Items.FindByValue(lettore("id_doc")).Selected = True
            '    End While
            '    lettore.Close()
            'End If

            'If CType(Me.Page.FindControl("btnSalva"), ImageButton).Visible = False Then
            'DisattivaTutto()
            'End If

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try
    End Sub

    Public Sub DisattivaTutto()
        txtNote.Enabled = False
        imgAggDoc.Visible = False
        btnDeleteDoc.Visible = False
        txtDataDocManc.Enabled = False
        chkDocManc.Enabled = False
        chkListDocumenti.Enabled = False
    End Sub
End Class
