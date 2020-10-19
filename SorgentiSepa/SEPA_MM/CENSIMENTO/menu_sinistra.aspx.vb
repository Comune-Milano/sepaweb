
Partial Class CENSIMENTO_menu_sinistra
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged

        If Session.Item("LAVORAZIONE") = "1" Then
            Response.Write("<script>alert('Chiudere la maschera utilizzando la funzione Esci.\nSe invece avete chiuso una maschera senza premere il pulsante ESCI, uscire dal sistema e rientrare!')</script>")
            T1.Nodes(0).Selected = True
            Exit Sub
        End If
        Select Case T1.SelectedValue
            'Nodi SottoMenù Complessi
            Case 4
                Response.Write("<script>parent.main.location.replace('RicercaComplessi.aspx');</script>")
            Case 10
                Response.Write("<script>parent.main.location.replace('InserimentoComplessi.aspx');</script>")

                'Nodi Sottomenù Edifici
            Case 5
                Response.Write("<script>parent.main.location.replace('RicercaEdifici.aspx');</script>")
            Case 11
                Response.Write("<script>parent.main.location.replace('InserimentoEdifici.aspx');</script>")

                'Nodi SottoMenù Unità Immobiliari
            Case "Diretta"
                Response.Write("<script>parent.main.location.replace('DirettaUI.aspx');</script>")
            Case "Selettiva"
                Response.Write("<script>parent.main.location.replace('RicercaUI.aspx');</script>")
            Case "Occupante"
                Response.Write("<script>parent.main.location.replace('RicercaOccupante.aspx');</script>")
            Case "Report"
                Response.Write("<script>parent.main.location.replace('RicercaStatoUI.aspx');</script>")
            Case "UiDisp"
                'Response.Write("<script>window.open('UiDisponibili.aspx','','');</script>")
            Case "UiInag"
                Response.Write("<script>window.open('UiInagibili.aspx','','');</script>")
            Case "NonAcc"
                Response.Write("<script>window.open('UiNonAccessibili.aspx','','');</script>")
            Case 6
                Response.Write("<script>parent.main.location.replace('InserimentoUniImmob.aspx');</script>")

                'Nodi SottoMenù Unità Comuni
            Case 9
                Response.Write("<script>parent.main.location.replace('RicercaUC.aspx');</script>")
            Case 8
                Response.Write("<script>parent.main.location.replace('UnitàComEdifici.aspx');</script>")

            Case "RptUiRu"
                Response.Write("<script>parent.main.location.replace('RptUnitaImm.aspx');</script>")
            Case "Tecnico"
                Response.Write("<script>parent.main.location.replace('Report/Ricerca.aspx?T=1');</script>")
            Case "TecDis"
                Response.Write("<script>parent.main.location.replace('Report/Ricerca.aspx?T=3');</script>")
            Case "Amministrativo"

                'Response.Write("<script>alert('Non disponibile');</script>")
                Response.Write("<script>parent.main.location.replace('Report/Ricerca.aspx?T=2');</script>")
            Case "AmmNuovaAssegn"
                Response.Write("<script>parent.main.location.replace('Report/Ricerca.aspx?T=4');</script>")

            Case "patrTipoUI"
                Response.Write("<script>parent.main.location.replace('Report/RicercaDaComplesso.aspx?U=1');</script>")
            Case "patrStatoUI"
                Response.Write("<script>parent.main.location.replace('Report/RicercaDaComplesso.aspx?U=2');</script>")
            Case "patrGruppiUI"
                Response.Write("<script>parent.main.location.replace('Report/RicercaDaComplesso.aspx?U=3');</script>")

            Case "ElencoManut"
                'Response.Write("<script>alert('Funzione non ancora disponibile!');</script>")

                Response.Write("<script>parent.main.location.replace('ElencoCensimenti.aspx');</script>")
            Case "ElencoComp"
                'Response.Write("<script>alert('Funzione non ancora disponibile!');</script>")

                Response.Write("<script>parent.main.location.replace('ElencoComplessi.aspx');</script>")

            Case "RicSchede"
                Response.Write("<script>parent.main.location.replace('RicercaPerSchedaCens.aspx');</script>")
            Case "App.Sloggio"

                Response.Write("<script>window.open('../CONTRATTI/RptAppuntamenti.aspx','VerbaleSloggio', 'menubar=no, scrollbars=yes', 'width=1000,height=700');</script>")
            Case "NonAcc"
                Response.Write("<script>window.open('UiNonAccessibili.aspx','','');</script>")
            Case "RifLeEdifici"
                If Session.Item("RIF_LEG_EDIFICI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('EdificiRifLegislativi.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "DestUsoRL"
                If Session.Item("LIVELLO_WEB") = "1" Or Session.Item("RESPONSABILE") = "1" Then
                    'Response.Write("<script>parent.main.location.replace('DestinazioniUsoRL.aspx');</script>")
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "AperturaPagina", "ApriModuloStandard('DestinazioniUsoRL.aspx', 'Destinazioni Uso RL');", True)
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "ASSFILIALI"
                Response.Write("<script>parent.main.location.replace('AssFiliali.aspx');</script>")
            Case "RptUiRuSoglia"
                Response.Write("<script>parent.main.location.replace('RptUnitaImmSoglia.aspx');</script>")
            Case "ProgrInterventi"
                If Session.Item("FL_PRG_INTERVENTI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('GestEventiPRG.aspx');</script>")
                Else
                    Response.Write("<script>alert('Operazione non disponibile o utente non abilitato!');</script>")
                End If
            Case "Progr_Intervento"
                If Session.Item("FL_PRG_INTERVENTI_MASSIVO") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CaricMassivoProgrIntervento.aspx');</script>")
                Else
                    Response.Write("<script>alert('Operazione non disponibile o utente non abilitato!');</script>")
                End If
        End Select
        T1.SelectedNode.Selected = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
            Label3.Text = Format(Now(), "dd/MM/yyyy")
            If Session("PED2_SOLOLETTURA") = "1" Or Session.Item("ID_CAF") <> "6" Then
                T1.Nodes(0).ChildNodes(1).SelectAction = TreeNodeSelectAction.None
                T1.Nodes(0).ChildNodes(1).Text = "- - - -"

                T1.Nodes(1).ChildNodes(1).SelectAction = TreeNodeSelectAction.None
                T1.Nodes(1).ChildNodes(1).Text = "- - - -"

                'T1.Nodes(2).ChildNodes(2).SelectAction = TreeNodeSelectAction.None
                'T1.Nodes(2).ChildNodes(2).Text = "- - - -"
                T1.Nodes(2).ChildNodes(1).SelectAction = TreeNodeSelectAction.None
                T1.Nodes(2).ChildNodes(1).Text = "- - - -"



                T1.Nodes(4).ChildNodes(1).SelectAction = TreeNodeSelectAction.None
                T1.Nodes(4).ChildNodes(1).Text = "- - - -"


                'T1.Nodes(1).ChildNodes(0).SelectAction = TreeNodeSelectAction.None
                'T1.Nodes(1).ChildNodes(0).Text = "- - - -"
                'T1.Nodes(2).ChildNodes(0).SelectAction = TreeNodeSelectAction.None
                'T1.Nodes(2).ChildNodes(0).Text = "- - - -"
                'T1.Nodes(3).ChildNodes(0).SelectAction = TreeNodeSelectAction.None
                'T1.Nodes(3).ChildNodes(0).Text = "- - - -"
            End If
            If Session("PED2_ESTERNA") = "1" Then

                'PEPPE MODIFY BY RICHIESTA MAX 11/11/2009
                'T1.Nodes(2).ChildNodes(0).ChildNodes(2).Text = "- - - -"
                'T1.Nodes(2).ChildNodes(0).ChildNodes(2).SelectAction = TreeNodeSelectAction.None
            End If
                'ANTONELLO MODIFY 05/07/2013 - ASSEGNAZIONE FILIALE
                If Session.Item("LIVELLO") <> "1" Then
                    If Not (Session.Item("ID_CAF") = 2 And Session.Item("RESPONSABILE") = 1) Then
                        Dim tn As TreeNode
                        tn = T1.FindNode("ASSFILIALI")
                        If Not IsNothing(tn) Then
                            T1.Nodes.Remove(tn)
                        End If
                    End If
                End If
            Catch ex As Exception
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                Session.Add("ERRORE", "Provenienza: Censimento - Menu - " & ex.Message)
                Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
            End Try

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
