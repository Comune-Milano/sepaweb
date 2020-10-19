

Partial Class CENSIMENTO_menu_sinistra
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged
        Select Case T1.SelectedValue
            Case "Agenda"
                Response.Write("<script>var left=(screen.width/2)-(1000/2);var top=(screen.height/2)-(770/2);var targetWin = window.open('../SEGNALAZIONI/Agenda/Agenda.aspx', 'AgendaSegnalazioni', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,copyhistory=no,width=1000,height=770,top=' + top + ',left=' + left);</script>")
            Case "RicercaS"
                Response.Write("<script>parent.main.location.replace('../CALL_CENTER/RicercaS.aspx?PROV=S');</script>")
            Case "StatiAppuntamenti"
                Response.Write("<script>parent.main.location.replace('../SEGNALAZIONI/Agenda/StatiAppuntamenti.aspx');</script>")
        End Select
        T1.SelectedNode.Selected = False
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If
        If Not IsPostBack Then
            Label3.Text = Format(Now(), "dd/MM/yyyy")
        End If
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
