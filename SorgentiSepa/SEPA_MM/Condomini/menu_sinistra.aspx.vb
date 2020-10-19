
Partial Class CONDOMINI_menu_sinistra
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                Label3.Text = Format(Now(), "dd/MM/yyyy")
                If Session.Item("MOD_CONDOMINIO_SL") = 1 Then
                    NodiSolaLettura()
                End If
                ''If Session.Item("LIVELLO") <> 1 Then
                ''    If Session.Item("ID_CAF") = 6 Then
                ''        NodiSolaLettura()
                ''    End If
                ''End If
            Catch ex As Exception

            End Try
        End If

    End Sub

    Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If

        If Session.Item("LAVORAZIONE") = "1" Then
            Response.Write("<script>alert('Chiudere la maschera utilizzando la funzione Esci.\nSe invece avete chiuso una maschera senza premere il pulsante ESCI, uscire dal sistema e rientrare!')</script>")
            T1.Nodes(2).Selected = True
            Exit Sub
        End If

        Select Case T1.SelectedValue

            Case "InsAmminist" 'nuovo 
                Response.Write("<script>parent.main.location.replace('Anagrafica/Inserimento.aspx');</script>")
            Case "RicAmminist" 'nuovo
                Response.Write("<script>parent.main.location.replace('Anagrafica/Ricerca.aspx');</script>")
            Case "RicCondomini" 'nuovo
                Response.Write("<script>parent.main.location.replace('RicercaCondominio.aspx');</script>")

            Case "Inserimento"
                Response.Write("<script>parent.main.location.replace('Condominio.aspx');</script>")


            Case "AnAmministratori" ' nuovo
                Response.Write("<script>parent.main.location.replace('RicRptAmmCond.aspx');</script>")
            Case "AnCondomini"
                Response.Write("<script>parent.main.location.replace('RicRptAnCondomini.aspx');</script>")


            Case "Inquilini"
                Response.Write("<script>parent.main.location.replace('RicercaInquilini.aspx');</script>")
            Case "AnCond"
                Response.Write("<script>parent.main.location.replace('AnCondomini.aspx');</script>")
            Case "RptConv"
                Response.Write("<script>parent.main.location.replace('RptDateConvocazioni.aspx');</script>")
            Case "BManager"
                Response.Write("<script>parent.main.location.replace('BManager.aspx');</script>")
            Case "DatiPerChiusura"
                Response.Write("<script>parent.main.location.replace('RisultatiDatiChiusura.aspx');</script>")

            Case "Delegati"
                Response.Write("<script>parent.main.location.replace('Delegati.aspx');</script>")
            Case "VSpesa"

                Response.Write("<script>parent.main.location.replace('VocSpesa.aspx');</script>")
            Case "PatEvent"
                Response.Write("<script>parent.main.location.replace('EventiPatrimoniali.aspx?TUTTI=1');</script>")
            Case "RptNonVerb"
                Response.Write("<script>window.open('RptConvNonVerb.aspx');parent.main.location.replace('pagina_home.aspx');</script>")
                'Case "NEventi"
                '    Response.Write("<script>window.open('RptConvNonVerb.aspx');</script>")

            Case "RPagamenti"
                'Response.Write("<script>alert('Funzione non ancora disponibile!');</script>")

                Response.Write("<script>parent.main.location.replace('RicPagamenti.aspx');</script>")
            Case "PrintEtichette"

                Response.Write("<script>parent.main.location.replace('StampaEtichette.aspx');</script>")

            Case "RptInquilini"   'modificato
                Response.Write("<script>parent.main.location.replace('RicRptInquilini.aspx');</script>")


                'Response.Write("<script>window.open('ReportInquilini.aspx');</script>")
            Case "MorEmesse"
                Response.Write("<script>parent.main.location.replace('RicMorEmesse.aspx');</script>")

            Case "Morosita"
                Response.Write("<script>parent.main.location.replace('RicMorosita.aspx');</script>")



                '--------------------VOCI DA CANCELLARE
            Case "SoloIndirizzo"
                Response.Write("<script>parent.main.location.replace('RicSoloIndirizzo.aspx');</script>")
            Case "Indirizzo"
                Response.Write("<script>parent.main.location.replace('RicercaIndirizzo.aspx');</script>")
                'Case "Scadenze"
                '    Response.Write("<script>parent.main.location.replace('PagamScadenza.aspx');</script>")
                'Case "Anagrafica"
                '    Response.Write("<script>window.open('Anagrafica/menu.htm','Anagrafica','height=500,top=0,left=0,width=620');</script>")

                'Case "Amminist"
                '    Response.Write("<script>parent.main.location.replace('RicercaAmminist.aspx');</script>")

            Case "Contabilità"
                Response.Write("<script>parent.main.location.replace('RicRptUltmContab.aspx');</script>")


            Case "Preventivi"

                Response.Write("<script>parent.main.location.replace('RicContabCondomini.aspx');</script>")

            Case "GestAmmInd"

                Response.Write("<script>parent.main.location.replace('ElencoIndiretti.aspx');</script>")




                ''    '19/10/2012 SEZIONE CONTRIBUTO CALORE
                'parametri(calcolo)
            Case "Definizione"
                Response.Write("<script>parent.main.location.replace('ContCalore/Parametri.aspx');</script>")

                'preventivo
            Case "ElAvDiritto"
                Response.Write("<script>parent.main.location.replace('ContCalore/CalcolaAventiDiritto.aspx?TIPO=NUOVO');</script>")
            Case "Approva"
                Response.Write("<script>parent.main.location.replace('ContCalore/Approvazione.aspx?TIPO=NUOVO');</script>")
            Case "Anomalie"
                Response.Write("<script>parent.main.location.replace('ContCalore/ElAnomalie.aspx?TIPO=NUOVO');</script>")
            Case "ElApprovati"
                Response.Write("<script>parent.main.location.replace('ContCalore/PreElApprovati.aspx?TIPO=NUOVO');</script>")


                'consuntivo
            Case "ElCons"
                Response.Write("<script>parent.main.location.replace('ContCalore/CalcolaAventiDiritto.aspx?TIPO=CONGUAGLIO');</script>")
            Case "ApprovaCons"
                Response.Write("<script>parent.main.location.replace('ContCalore/Approvazione.aspx?TIPO=CONGUAGLIO');</script>")
            Case "AnomalieCons"
                Response.Write("<script>parent.main.location.replace('ContCalore/ElAnomalie.aspx?TIPO=CONGUAGLIO');</script>")
            Case "ElApprovatiCons"
                Response.Write("<script>parent.main.location.replace('ContCalore/PreElApprovati.aspx?TIPO=CONGUAGLIO');</script>")

        End Select
        'T1.Nodes(2).Selected = True
        T1.SelectedNode.Selected = False

    End Sub

    Private Sub NodiSolaLettura()
        '*******NODO 1
        'T1.Nodes(1).SelectAction = TreeNodeSelectAction.None
        'T1.Nodes(1).ChildNodes.Clear()
        'T1.Nodes(1).Text = "- - - -"


        Dim tn As TreeNode
        tn = T1.FindNode("Ins")
        If Not IsNothing(tn) Then
            T1.Nodes.Remove(tn)
        End If

        '*******NODO 3
        'T1.Nodes(3).SelectAction = TreeNodeSelectAction.None
        'T1.Nodes(3).ChildNodes.Clear()
        'T1.Nodes(3).Text = "- - - -"
        'tn = T1.FindNode("Report")
        'If Not IsNothing(tn) Then
        '    T1.Nodes.Remove(tn)
        'End If

        '*******NODO 4


        'T1.Nodes(4).SelectAction = TreeNodeSelectAction.None
        'T1.Nodes(4).ChildNodes.Clear()
        'T1.Nodes(4).Text = "- - - -"
        tn = T1.FindNode("Gest.Tabelle")
        If Not IsNothing(tn) Then
            T1.Nodes.Remove(tn)
        End If
        '' '' ''*******NODO 5
        '' '' ''T1.Nodes(5).SelectAction = TreeNodeSelectAction.None
        '' '' ''T1.Nodes(5).Text = "- - - -"
        '' '' ''T1.Nodes(5).ChildNodes(0).SelectAction = TreeNodeSelectAction.None
        '' '' ''T1.Nodes(5).ChildNodes(0).Text = "- - - -"
        '' '' ''T1.Nodes(5).ChildNodes(1).SelectAction = TreeNodeSelectAction.None
        '' '' ''T1.Nodes(5).ChildNodes(1).Text = "- - - -"
        '' '' ''T1.Nodes(5).ChildNodes(2).SelectAction = TreeNodeSelectAction.None
        '' '' ''T1.Nodes(5).ChildNodes(2).Text = "- - - -"
        '' '' ''T1.Nodes(5).ChildNodes(3).SelectAction = TreeNodeSelectAction.None
        '' '' ''T1.Nodes(5).ChildNodes(3).Text = "- - - -"

        '*******NODO 6
        'T1.Nodes(6).SelectAction = TreeNodeSelectAction.None
        'T1.Nodes(6).ChildNodes.Clear()
        'T1.Nodes(6).Text = "- - - -"
        tn = T1.FindNode("PatEvent")
        If Not IsNothing(tn) Then
            T1.Nodes.Remove(tn)
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
