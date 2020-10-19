

Partial Class MOROSITA_menu_sinistra
    Inherits PageSetIdMode

    Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged

        If Session.Item("LAVORAZIONE") = "1" Then
            Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
            T1.Nodes(0).Selected = True
            Exit Sub
        End If



        Select Case T1.SelectedValue
            Case "Multiselezione"
                Response.Write("<script>window.open('RicercaDebitoriMultiSelezione.aspx','_blank','status=no,scrollbars=yes,resizable=yes');</script>")
            Case "AnalisiStatistica"
                Response.Write("<script>window.open('RicercaDebitoriMultiSelezione2.aspx','_blank','status=no,scrollbars=yes,resizable=yes');</script>")
                
            Case "MultiselezioneMor"
                Response.Write("<script>window.open('RicercaDebitoriMultiSelezioneMor.aspx','_blank','status=no,scrollbars=yes,resizable=yes');</script>")




                '*** GESTIONE MOROSITA'
            Case "RicercaDebitori"
                Response.Write("<script>parent.main.location.replace('RicercaDebitori.aspx?');</script>")
                'Response.Write("<script>parent.main.location.replace('../Contratti/RicercaStrutturata.aspx');</script>")

            Case "RicercaMorosita"
                Response.Write("<script>parent.main.location.replace('RicercaMorosita.aspx');</script>")

            Case "RicercaIntestatari"
                Response.Write("<script>parent.main.location.replace('RicercaInquilino.aspx');</script>")

                'REPORT *******************************************************************************************************
            Case "ReportContabili"
                Response.Write("<script>parent.main.location.replace('RicercaReportContabili.aspx');</script>")

            Case "ReportMorosita"
                Response.Write("<script>parent.main.location.replace('RicercaReportMorosita.aspx');</script>")

            Case "ReportStatistica"
                'Response.Write("<script>parent.main.location.replace('RicercaReport.aspx?');</script>")

                Response.Write("<script>window.open('Flussi_MorositaGest.aspx" _
                                                 & "','STAMPA" & Format(Now, "hhss") & "');</script>")

                '***************************************************************************************************************


            Case "ElaboraPosteAler"
                Response.Write("<script>parent.main.location.replace('ElaboraPosteAler.aspx');</script>")

            Case "NuovoLegale"

                If Session.Item("MOD_MOROSITA_SL") = "1" Or (Session.Item("MOD_MOROSITA_SL") <> "1" And Session.Item("ID_STRUTTURA") <> 16 And Session.Item("BP_GENERALE") <> "1") Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Session.Add("ID", 0)
                    Response.Write("<script>parent.main.location.replace('Legali.aspx');</script>")
                End If


            Case "RicercaLegale"
                Response.Write("<script>parent.main.location.replace('RicercaLegali.aspx');</script>")


            Case "AffidamentoLegale"

                Response.Write("<script>parent.main.location.replace('RicercaMorositaLegale.aspx');</script>")

            Case "ReportGestori"
                Response.Write("<script>parent.main.location.replace('RicercaReportGestori.aspx');</script>")

                'Tabelle di Supporto
            Case "Tribunali"
                Response.Write("<script>parent.main.location.replace('Legali_Tribunali.aspx');</script>")

            Case "filtri"
                Response.Write("<script>window.open('Filtri.aspx');</script>")

        End Select
        T1.SelectedNode.Selected = False

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tn As TreeNode


        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If

        If Not IsPostBack Then
            If Session.Item("MOD_MOROSITA_SL") = "1" Then

                ''*********** Elimino GEST.LEGALI/Ricerca *******************
                'tn = T1.FindNode("GestioneLegali")
                'If Not IsNothing(tn) Then
                '    tn.ChildNodes.Remove(T1.FindNode("GestioneLegali/NuovoLegale"))
                'End If

                ''*********** Elimino AffidamentoLegale *******************
                'tn = T1.FindNode("AffidamentoLegale")
                'If Not IsNothing(tn) Then
                '    T1.Nodes.Remove(tn)
                'End If



            End If
            '********** Cancella nodi Gestione Legali e Affida Pratiche Legali
            tn = T1.FindNode("GestioneLegali")
            If Not IsNothing(tn) Then
                T1.Nodes.Remove(tn)
            End If

            tn = T1.FindNode("AffidamentoLegale")
            If Not IsNothing(tn) Then
                T1.Nodes.Remove(tn)
            End If

            'tn = T1.FindNode("Report")
            'If Not IsNothing(tn) Then
            '    T1.Nodes.Remove(tn)
            'End If

           

        End If

        Label3.Text = Format(Now(), "dd/MM/yyyy")


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
