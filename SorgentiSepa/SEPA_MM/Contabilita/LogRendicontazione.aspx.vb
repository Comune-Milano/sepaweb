
Partial Class Contabilita_LogRendicontazione
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSQL As String = ""

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click

        'If par.IfEmpty(Me.txtdata.Text, "Null") <> "Null" Then
        '    '  Dim sValore As String
        '    ' sValore = par.AggiustaData(Me.txtdata.Text)
        '    ' sStringaSQL = sStringaSQL & " AND DATA_ORA like '" & sValore & "'%"
        'End If
        Dim Elaborazioni As String = ""

        If ChElaborazioni.Checked = True Then
            Elaborazioni = "1"
        Else
            Elaborazioni = "0"
        End If

        Response.Write("<script>location.replace('RisultatoLog.aspx?E=" & Elaborazioni & "&DAL=" & par.IfEmpty(par.AggiustaData(Me.txtdatadal.Text), "") & "&AL=" & par.IfEmpty(par.AggiustaData(Me.txtdataal.Text), "") & "');</script>")


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Me.txtdatadal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtdataal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub
End Class

