
Partial Class CONS_menu_sinistra
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
                Response.Write("<script>parent.main.location.replace('RicercaDichiarazioni.aspx');</script>")
            Case 3
                Response.Write("<script>parent.main.location.replace('RicercaDomande.aspx');</script>")
            Case 5
                Response.Write("<script>parent.main.location.replace('RicercaPrenotazioni.aspx');</script>")
            Case 6
                Response.Write("<script>parent.main.location.replace('StatConsultazioni.aspx');</script>")
            Case 7
                Response.Write("<script>parent.main.location.replace('StatPrenotazioni.aspx');</script>")
        End Select
        T1.Nodes(0).Selected = True
    End Sub

    Protected Sub T1_TreeNodeExpanded(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles T1.TreeNodeExpanded
        If e.Node.Parent Is Nothing Then
            For Each node As TreeNode In (CType(sender, TreeView)).Nodes
                If Not (node.Equals(e.Node)) Then
                    node.Collapse()
                End If
            Next
            Return
        End If

        Dim tn As TreeNode = e.Node.Parent
        For Each node As TreeNode In tn.ChildNodes
            If Not (node.Equals(e.Node)) Then
                node.Collapse()
            End If
        Next
    End Sub
End Class
