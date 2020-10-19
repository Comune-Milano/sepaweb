
Partial Class AMMSEPA_RicercaEventi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        lblErrore.Visible = False
        If Not IsPostBack Then
            par.RiempiDListConVuoto(Me, par.OracleConn, "cmbOperatore", "select id, operatore from operatori where id in (select id_operatore from eventi_gestione) order by operatore asc", "OPERATORE", "ID")
            txtDataDal0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataAl0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If Page.IsValid Then
            Dim operatore As String = cmbOperatore.SelectedValue
            If txtDataDal0.Text <> "" And txtDataAl0.Text <> "" Then
                If txtDataDal0.Text > txtDataAl0.Text Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Date inserite incongruenti"
                    Exit Sub
                End If
            End If
            Dim data1 As String = Right(txtDataDal0.Text, 4) & Mid(txtDataDal0.Text, 4, 2) & Left(txtDataDal0.Text, 2) & "000000"
            Dim data2 As String = Right(txtDataAl0.Text, 4) & Mid(txtDataAl0.Text, 4, 2) & Left(txtDataAl0.Text, 2) & "000000"
            Response.Write("<script>location.replace('GestioneEventi.aspx?OPERATORE=" & operatore & "&DATADAL=" & data1 & "&DATAAL=" & data2 & "');</script>")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
