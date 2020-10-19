
Partial Class ANAUT_menu_sinistra
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tn As TreeNode

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Label3.Text = Format(Now(), "dd/MM/yyyy")
        End If

        If Session.Item("GA_L") = "1" Then
            tn = T1.FindNode("Proposte G.A./NuovaGA")
            If Not IsNothing(tn) Then
                T1.Nodes.Item(1).ChildNodes.Remove(tn)
            End If
        End If



    End Sub

    Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If

        If Session.Item("LAVORAZIONE") = "1" Then
            ''Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA.\nSe invece avete chiuso una maschera senza premere il pulsante ESCI, chiudere questa finestra e rientrare!')</script>")
            Response.Write("<script>alert('Chiudere la maschera utilizzando il pulsante ESCI.\nSe invece avete chiuso una maschera senza premere il pulsante ESCI, uscire dal sistema e rientrare!')</script>")

            T1.Nodes(1).Selected = True
            Exit Sub
        End If

        Select Case T1.SelectedValue
            Case "Richiesta"
                Response.Write("<script>parent.main.location.replace('RichiestaAutogest.aspx');</script>")
            Case "Ricerca"
                Response.Write("<script>alert('Funzione non ancora disponibile!');</script>")
                'Response.Write("<script>parent.main.location.replace('RicercaEdificio.aspx');</script>")
            Case "NuovaGA"
                Response.Write("<script>parent.main.location.replace('GestioneAutonoma.aspx');</script>")
            Case "Elenco"
                Response.Write("<script>parent.main.location.replace('ElencoGestAutonome.aspx');</script>")

        End Select
        T1.Nodes(1).Selected = True


        'T1.Nodes(3).Selected = True
    End Sub


End Class
