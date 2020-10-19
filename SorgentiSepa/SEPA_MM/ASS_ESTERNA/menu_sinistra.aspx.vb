
Partial Class ASS_ESTERNA_menu_sinistra
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Label3.Text = Format(Now(), "dd/MM/yyyy")
        End If

    End Sub

    Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged
        If Session.Item("LAVORAZIONE") = "1" Then
            Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
            Exit Sub
        End If
        Select Case T1.SelectedValue
            Case 1
                Response.Write("<script>parent.main.location.replace('InviaXML.aspx');</script>")
                'Case 2
                '    Response.Write("<script>parent.main.location.replace('RicercaAbbinamento.aspx');</script>")
                'Case 3
                '    Response.Write("<script>parent.main.location.replace('RicercaOfferta.aspx');</script>")
                'Case 4
                '    Response.Write("<script>parent.main.location.replace('RicercaOffertaCT.aspx');</script>")
        End Select
        T1.Nodes(1).Selected = True
    End Sub
End Class
