Imports System.IO
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums


Partial Class ANAUT_GestioneModelliAU
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            If Not IsPostBack Then
                Cerca()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
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


    Private Sub Cerca()
        sStringaSQL1 = "select utenza_bandi.descrizione as DESCR_BANDO,UTENZA_TIPO_DOC.*,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''VisModello.aspx?ID='||UTENZA_TIPO_DOC.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||'Visualizza'||'</a>','$','&'),'£','" & Chr(34) & "') as MODELLO1,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''TestModello.aspx?ID='||UTENZA_TIPO_DOC.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||'TEST'||'</a>','$','&'),'£','" & Chr(34) & "') as TEST FROM UTENZA_TIPO_DOC,UTENZA_BANDI WHERE UTENZA_BANDI.ID=UTENZA_TIPO_DOC.ID_BANDO order by UTENZA_TIPO_DOC.DESCRIZIONE ASC,ID_BANDO desc"
        BindGrid()
    End Sub

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Private Sub BindGrid()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "UTENZA_BANDI")
            Label4.Text = DataGrid1.Items.Count
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            Label4.Text = " - " & DataGrid1.Items.Count & " nella lista"

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Silver'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato: " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")


        End If
    End Sub


   

    Protected Sub btnNuovo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNuovo.Click
        BindGrid()
    End Sub

    Protected Sub btnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click
        If H1.Value <> "0" Then
            SceltaJQuery("Eliminare il modello selezionato? Non sarà più possibile annullare questa operazione.", "btnEliminaModello", "Attenzione...", "")
        Else
            MessJQuery("Scegliere un documento dalla lista.", 0, "Attenzione")
        End If
    End Sub

    Protected Sub btnEliminaModello_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaModello.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "DELETE from UTENZA_TIPO_DOC where ID =" & LBLID.Value

            par.cmd.ExecuteNonQuery()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            MessJQuery("Operazione Effettuata", 0, "Info...")
            BindGrid()
            TextBox3.Text = "Nessuna Selezione"
            LBLID.Value = ""
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            TextBox3.Text = ex.Message
        End Try

    End Sub
End Class
