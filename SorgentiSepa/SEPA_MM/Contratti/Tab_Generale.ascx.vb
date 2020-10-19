
Partial Class Contratti_Generale
    Inherits UserControlSetIdMode
    Public IndiceDomanda As String
    Public IndiceDichiarazione As String
    Public IndiceAnagrafe As String
    Public IndiceContratto As String
    Public Provenienza As String



    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub

    Protected Sub imgCreaAU_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCreaAU.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = "0"


    End Sub

    Protected Sub btnNewIstanza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNewIstanza.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = "0"
    End Sub

End Class
