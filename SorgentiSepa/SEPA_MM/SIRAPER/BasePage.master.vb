
Partial Class SIRAPER_BasePage
    Inherits PageSetMasterIdMode

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        Me.ID = "MasterPage"
        If Not IsPostBack Then
            If (Me.MainContent.Page.ToString().ToUpper.Contains("HOME") Or Me.MainContent.Page.ToString().ToUpper.Contains("RICERCA") Or Me.MainContent.Page.ToString().ToUpper.Contains("GESTIONE") Or Me.MainContent.Page.ToString().ToUpper.Contains("PROCEDURE")) Then
                DisableEnableMenu(False)
            Else
                DisableEnableMenu(True)
            End If
        End If
    End Sub
    Public Sub DisableEnableMenu(Optional ByVal Disattivato As Boolean = True)
        If Disattivato = True Then
            Me.CopriMenu.Style("visibility") = "visible"
        Else
            Me.CopriMenu.Style("visibility") = "hidden"
        End If
    End Sub
    Protected Sub btnNuovo_Click(sender As Object, e As System.EventArgs) Handles btnNuovo.Click
        Response.Redirect("Siraper.aspx", False)
    End Sub
    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        Response.Redirect("Ricerca.aspx", False)
    End Sub
    Protected Sub btnGestione_Click(sender As Object, e As System.EventArgs) Handles btnGestione.Click
        Response.Redirect("Gestione.aspx", False)
    End Sub
End Class