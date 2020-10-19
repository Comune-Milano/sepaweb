
Partial Class Contabilita_AnomalieRendicontazione
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSQL As String = ""

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click

        'If par.IfEmpty(Me.txtdatadal.Text, "Null") <> "Null" Then
        '    '  Dim sValore As String
        '    ' sValore = par.AggiustaData(Me.txtdata.Text)
        '    ' sStringaSQL = sStringaSQL & " AND DATA_ORA like '" & sValore & "'%"
        'End If


        Response.Write("<script>location.replace('RisultatoMavAnomalie.aspx?" _
                       & "DAL=" & par.IfEmpty(par.AggiustaData(Me.txtdatadal.Text), "") _
                       & "&AL=" & par.IfEmpty(par.AggiustaData(Me.txtdataal.Text), "") _
                       & "&VALUTADAL=" & par.IfEmpty(par.AggiustaData(Me.txtValutaDal.Text), "") _
                       & "&VALUTAAL=" & par.IfEmpty(par.AggiustaData(Me.txtValutaAl.Text), "") _
                       & "&FI=" & par.VaroleDaPassare(Me.txtfile.Text.Trim) _
                       & "&BO=" & Me.txtbolletta.Text.Trim _
                       & "&NCONTO=" & par.VaroleDaPassare(Me.txtNumConto.Text) & "');</script>")


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Me.txtdatadal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtdataal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtValutaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtValutaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Me.txtfile.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            Me.txtbolletta.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        End If
    End Sub
End Class
