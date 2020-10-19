
Partial Class Contratti_Proroga_AssDefinitiva_PaginaScelta
    Inherits PageSetIdMode

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsPostBack = False Then
            CodContratto.Value = Request.QueryString("COD")
        End If
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Select Case rdbScelta.SelectedValue
            Case "assegn_definitiva"
                Response.Redirect("AssegnDefinitiva.aspx?T=" + Request.QueryString("T") + "&IDC=" + Request.QueryString("IDC") + "&COD=" + CodContratto.Value + "&IDB=" + rdbScelta.SelectedValue & "")
            Case "proroga"
                Response.Redirect("Proroga.aspx?T=" + Request.QueryString("T") + "&IDC=" + Request.QueryString("IDC") + "&COD=" + CodContratto.Value + "&IDB=" + rdbScelta.SelectedValue & "")
            Case Else
                Response.Write("<script>alert('Selezionare la scelta prima di procedere!')</script>")
        End Select
    End Sub
End Class
