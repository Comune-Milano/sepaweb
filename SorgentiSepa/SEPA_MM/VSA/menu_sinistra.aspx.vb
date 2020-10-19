
Partial Class VSA_menu_sinistra
    Inherits PageSetIdMode
    Dim par As New CM.Global

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
                'Response.Write("<script>parent.main.location.replace('max.aspx?ID=-1&GLocat=1');</script>")
                Response.Write("<script>window.open('NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=-1&GLocat=1');</script>")
            Case 2
                Response.Write("<script>parent.main.location.replace('RicercaDichiarazioni.aspx');</script>")
            Case 4
                Response.Write("<script>parent.main.location.replace('AssociaDichiarazione.aspx');</script>")
            Case 5
                Response.Write("<script>parent.main.location.replace('RicercaDomande.aspx');</script>")
            Case 6
                Response.Write("<script>parent.main.location.replace('RicercaDomandePrecedenti.aspx');</script>")
            Case 7
                Response.Write("<script>window.open('../cf/codice.htm','cf','scrollbars=no,resizable=no,width=500,height=380,status=no,location=no,toolbar=no');</script>")
            Case 15
                Response.Write("<script>window.open('Prenotazione.aspx','Prenotazioni','top=0,left=0,width=400,height=270');</script>")
            Case 16
                Response.Write("<script>window.open('ListaPrenotazioni.aspx','Lista');</script>")
            Case 23
                Response.Write("<script>parent.main.location.replace('RicercaRinnovi.aspx');</script>")
            Case 154
                Response.Write("<script>parent.main.location.replace('RicercaIntegrazioni.aspx');</script>")
                'Case 100
                '    Response.Write("<script>parent.main.location.replace('SceltaTipoInvito.aspx');</script>")
            Case "Graduatoria"
                If Session.Item("MOD_EMRI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('Graduatoria.aspx');</script>")
                Else
                    Response.Write("<script>alert('Operatore non abilitato!');</script>")
                End If
            Case "riduz_canone"
                Response.Write("<script>parent.main.location.replace('Locatari/DocNecessari.aspx?T=3');</script>")
            Case "ampliamento"
                Response.Write("<script>parent.main.location.replace('Locatari/DocNecessari.aspx?T=2');</script>")
            Case "elencoDom"
                Response.Write("<script>parent.main.location.replace('Locatari/ElencoDomande.aspx');</script>")

        End Select
        T1.SelectedNode.Selected = False
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
