
Partial Class ANAUT_SceltaCreazioneLista
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0
        If IsPostBack = False Then

        End If
    End Sub

    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        If RadioButton1.Checked = True Then
            Response.Redirect("CreazioneListaAuto.aspx")
        End If
        If RadioButton2.Checked = True Then
            Response.Redirect("NuovaListaManuale.aspx")
        End If
        If RadioButton3.Checked = True Then
            Response.Redirect("NuovaListaExcel.aspx")
        End If
        If RadioButton4.Checked = True Then
            Response.Redirect("NuovaListaCSV.aspx")
        End If
    End Sub
End Class
