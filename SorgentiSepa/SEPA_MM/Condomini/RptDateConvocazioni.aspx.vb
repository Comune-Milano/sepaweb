
Partial Class Condomini_RptDateConvocazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtDataDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If Not String.IsNullOrEmpty(txtDataDal.Text) And Not String.IsNullOrEmpty(txtDataAl.Text) Then
            If par.AggiustaData(txtDataDal.Text) > par.AggiustaData(txtDataAl.Text) Then
                Response.Write("<script>alert('La data di ricerca finale deve essere maggiore di quella di inizio. Riprovare!');</script>")
                Exit Sub
            End If
        End If
        Response.Write("<script>window.open('RptConvocazioni.aspx?DAL=" & par.AggiustaData(Me.txtDataDal.Text) & "&AL=" & par.AggiustaData(Me.txtDataAl.Text) & "','Convocaz', '');</script>")
    End Sub
End Class
