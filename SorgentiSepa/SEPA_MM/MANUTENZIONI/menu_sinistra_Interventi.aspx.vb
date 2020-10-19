
Partial Class MANUTENZIONI_menusinistraInterventi
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Label3.Text = Format(Now(), "dd/MM/yyyy")
        End If

    End Sub

    Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged
        If Session.Item("LAVORAZIONE") = "1" Then
            Response.Write("<script>alert('Chiudere la maschera utilizzando la funzione Esci.\nSe invece avete chiuso una maschera senza premere il pulsante ESCI, uscire dal sistema e rientrare!')</script>")
            T1.Nodes(0).Selected = True
            Exit Sub
        End If
        Select Case T1.SelectedValue
            '*************INSERIMENTO
            Case 2
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "COMP")
                Response.Write("<script>parent.main.location.replace('Int_Manutenzione.aspx');</script>")
            Case 3
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "EDIF")
                Response.Write("<script>parent.main.location.replace('Int_Manutenzione.aspx');</script>")
            Case 4
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "UNI_COM")
                Response.Write("<script>parent.main.location.replace('Int_Manutenzione.aspx');</script>")
            Case 5
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "UNI_IMMOB")
                Response.Write("<script>parent.main.location.replace('Int_Manutenzione.aspx');</script>")
            Case 24
                Session.Add("BUILDING_TYPE", "IMPIANTI")
                Response.Write("<script>parent.main.location.replace('Int_Manutenzione.aspx');</script>")

                '***********RICERCA
            Case 6
                Session.Add("BUILDING_TYPE", "COMP")
                Response.Write("<script>parent.main.location.replace('RicercaInterventi.aspx');</script>")
            Case 7
                Session.Add("BUILDING_TYPE", "EDIF")
                Response.Write("<script>parent.main.location.replace('RicercaInterventi.aspx');</script>")
            Case 8
                Session.Add("BUILDING_TYPE", "UNI_COM")
                Response.Write("<script>parent.main.location.replace('RicercaInterventi.aspx');</script>")
            Case 9
                Session.Add("BUILDING_TYPE", "UNI_IMMOB")
                Response.Write("<script>parent.main.location.replace('RicercaInterventi.aspx');</script>")
            Case 25
                Session.Add("BUILDING_TYPE", "IMPIANTI")
                Response.Write("<script>parent.main.location.replace('RicercaInterventi.aspx');</script>")
            Case "StManut"
                Response.Write("<script>parent.main.location.replace('RicercaStManutentivo.aspx');</script>")

                '*****SERVIZI***********
                '******INSERIMENTO
            Case 12
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "COMP")
                Response.Write("<script>parent.main.location.replace('Servizio_Manut.aspx');</script>")
            Case 13
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "EDIF")
                Response.Write("<script>parent.main.location.replace('Servizio_Manut.aspx');</script>")
            Case 14
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "UNI_COM")
                Response.Write("<script>parent.main.location.replace('Servizio_Manut.aspx');</script>")
            Case 15
                Session.Add("ID", 0)
                Session.Add("BUILDING_TYPE", "UNI_IMMOB")
                Response.Write("<script>parent.main.location.replace('Servizio_Manut.aspx');</script>")
                '***********RICERCA
            Case 16
                Session.Add("BUILDING_TYPE", "COMP")
                Response.Write("<script>parent.main.location.replace('RicercaServizi.aspx');</script>")
            Case 17
                Session.Add("BUILDING_TYPE", "EDIF")
                Response.Write("<script>parent.main.location.replace('RicercaServizi.aspx');</script>")
            Case 18
                Session.Add("BUILDING_TYPE", "UNI_COM")
                Response.Write("<script>parent.main.location.replace('RicercaServizi.aspx');</script>")
            Case 19
                Session.Add("BUILDING_TYPE", "UNI_IMMOB")
                Response.Write("<script>parent.main.location.replace('RicercaServizi.aspx');</script>")
            Case 20
                Response.Write("<script>parent.main.location.replace('Fornitori.aspx');</script>")
            Case 21

                Response.Write("<script>parent.main.location.replace('SelettivaComplessi.aspx?CHIAMA=UTENZE');</script>")
            Case "rptman"
                Response.Write("<script>window.open('rptman.aspx','Disponibilita','');</script>")

            Case 22
                Response.Write("<script>parent.main.location.replace('SelettivaEdifici.aspx?CHIAMA=UTENZE');</script>")
            Case 23
                Response.Write("<script>parent.main.location.replace('Ricerca_Utenze_Fornitori.aspx');</script>")

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
