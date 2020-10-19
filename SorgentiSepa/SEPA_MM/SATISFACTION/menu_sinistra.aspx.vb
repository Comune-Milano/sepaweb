
Partial Class FSA_menu_sinistra
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Inserire qui il codice utente necessario per inizializzare la pagina
        Dim tn As TreeNode
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""LoginBandoFSA.aspx""</script>")
        End If
        If Not IsPostBack Then
            Label3.Text = Format(Now(), "dd/MM/yyyy")

            'If par.OracleConn.State = Data.ConnectionState.Open Then
            '    Exit Sub
            'Else
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)
            'End If

            'par.cmd.CommandText = "select extra from odg"
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'Dim a As TreeNode
            'If myReader.Read Then
            '    If par.IfNull(myReader("EXTRA"), "0") = "0" Then
            '        'If T1.Nodes(8).ChildNodes(0).Text = "Nuova" Then
            '        '    a = T1.Nodes(8).ChildNodes(0)
            '        '    T1.Nodes(8).ChildNodes.Remove(a)
            '        'End If
            '    End If
            'End If

            'par.cmd.Dispose()
            'myReader.Close()
            'par.OracleConn.Close()

        End If

        If Session.Item("MOD_SATISFACTION_SL") = "1" Then
            tn = T1.FindNode("Inserimento")
            If Not IsNothing(tn) Then
                T1.Nodes.Remove(tn)
            End If
        End If

    End Sub

    Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged
        If Session.Item("LAVORAZIONE") = "1" Then
            Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
            Exit Sub
        End If
        Select Case T1.SelectedValue
            Case "Inserimento"
                Response.Write("<script>parent.main.location.replace('IntroInserimento.aspx');</script>")
            Case "Questionari"
                Response.Write("<script>parent.main.location.replace('RicercaSchede.aspx');</script>")
            Case "Servizi"
                Response.Write("<script>parent.main.location.replace('RicercaServizi.aspx');</script>")
                'Case "Grafici"
                'Response.Write("<script>parent.main.location.replace('GraficiQuestionario.aspx');</script>")
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
