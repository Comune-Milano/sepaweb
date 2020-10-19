
Partial Class ARPA_LOMBARDIA_OpenPage
    Inherits System.Web.UI.MasterPage
    Dim par As New CM.Global
    Public StringaSiscom As String = "SISCOM_MI."
    Public version As String = Mid(System.Configuration.ConfigurationManager.AppSettings("Versione"), 10).ToString.Trim().Replace(" ", "")

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Session.Item("OPERATORE")) Then
            If String.IsNullOrEmpty(Trim(Session.Item("OPERATORE"))) Then
                Response.Redirect("~/AccessoNegato.htm", False)
                Response.End()
            Else
                If par.getLockSessione = False Then
                    Response.Redirect("~/AccessoNegato.htm", False)
                    Response.End()
                End If
            End If
        Else
            Response.Redirect("~/AccessoNegato.htm", False)
            Response.End()
        End If
        Me.ID = "MasterOpen"
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("KEY")) Then
                If par.getLockPage(Request.QueryString("KEY").ToString) = False Then
                    Response.Redirect("~/AccessoNegato.htm", False)
                    Response.End()
                End If
            Else
                Response.Redirect("~/AccessoNegato.htm", False)
                Response.End()
            End If
        End If
    End Sub
End Class

