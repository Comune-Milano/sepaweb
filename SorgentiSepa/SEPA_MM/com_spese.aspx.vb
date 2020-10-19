
Partial Class com_spese
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        If txtImporto.Text = "" Then
            L2.Visible = True
        Else
            L2.Visible = False
        End If

        If IsNumeric(txtImporto.Text) = False Then
            L2.Visible = True
        Else
            If Val(txtImporto.Text) > 10000 Then
                L2.Visible = False
            Else
                L2.Visible = True
                L2.Text = "(Valore superiore a 10.000 Euro)"
            End If
        End If

        If InStr(txtImporto.Text, ".") = 0 Then
            L2.Visible = False
            If InStr(txtImporto.Text, ",") = 0 Then
                L2.Visible = False
            Else
                L2.Visible = True
                L2.Text = "(Valore interi)"
            End If
        Else
            L2.Visible = True
            L2.Text = "(Valore interi)"
        End If

        If L2.Visible = True Then
            Exit Sub
        End If
        Cache(Session.Item("GRiga")) = txtRiga.Text
        Cache(Session.Item("GLista")) = par.MiaFormat(txtComponente.Text, 52) & " " & par.MiaFormat(txtImporto.Text, 6) & ",00   " & par.MiaFormat(txtDescrizione.Text, 17)

        Response.Clear()
        Response.Write("<script>window.close();</script>")
        Response.End()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Response.Write("<script></script>")
        If Not IsPostBack = True Then

            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))
            txtComponente.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("CM"), 1, 52))
            txtImporto.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("IM"), 1, 6))
            If txtImporto.Text = "" Then txtImporto.Text = "0"
            txtDescrizione.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DS"), 1, 17))
            txtDescrizione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        End If

    End Sub
End Class
