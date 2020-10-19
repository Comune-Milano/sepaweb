
Partial Class com_spese
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Private Function Controlla() As Boolean
        Controlla = True
        If txtImporto.Text = "" Then
            L2.Visible = True
            Controlla = False
            Exit Function
        Else
            L2.Visible = False
        End If

        If IsNumeric(txtImporto.Text) = False Then
            L2.Visible = True
            Controlla = False
            Exit Function
        Else
            If Val(txtImporto.Text) > 10000 Then
                L2.Visible = False
            Else
                L2.Visible = True
                L2.Text = "(Valore superiore a 10.000 Euro)"
                Controlla = False
                Exit Function
            End If
        End If

        If InStr(txtImporto.Text, ".") = 0 Then
            L2.Visible = False
            If InStr(txtImporto.Text, ",") = 0 Then
                L2.Visible = False
            Else
                L2.Visible = True
                L2.Text = "(Valore interi, senza virgola)"
                Controlla = False
                Exit Function
            End If
        Else
            L2.Visible = True
            L2.Text = "(Valore interi, senza virgola)"
            Controlla = False
            Exit Function
        End If

        If txtDescrizione.Text <> "" Then
            L1.Visible = False
        Else
            L1.Visible = True
            Controlla = False
            Exit Function
        End If


    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        
        If Controlla() = True Then
            Cache(Session.Item("GRiga")) = txtRiga.Value
            Cache(Session.Item("GLista")) = par.MiaFormat(txtComponente.Text, 52) & " " & par.MiaFormat(txtImporto.Text, 6) & ",00   " & par.MiaFormat(txtDescrizione.Text, 100)

            Response.Clear()
            Response.Write("<script>window.close();</script>")
            Response.End()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Response.Write("<script></script>")
        If Not IsPostBack = True Then

            txtRiga.Value = par.Elimina160(Request.QueryString("RI"))
            txtComponente.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("CM"), 1, 52))
            txtImporto.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("IM"), 1, 6))
            If txtImporto.Text = "" Then txtImporto.Text = "0"
            txtDescrizione.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DS"), 1, 100))
            txtDescrizione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        End If

    End Sub
End Class
