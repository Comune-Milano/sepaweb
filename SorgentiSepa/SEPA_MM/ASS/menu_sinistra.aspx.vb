Namespace CM

Partial Class menu
    Inherits PageSetIdMode

#Region " Codice generato da Progettazione Web Form "

    'Chiamata richiesta da Progettazione Web Form.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: questa chiamata al metodo è richiesta da Progettazione Web Form.
        'Non modificarla nell'editor del codice.
        InitializeComponent()
    End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Session.Item("OPERATORE") = "" Then
                Response.Redirect("~/AccessoNegato.htm", True)
                Exit Sub
            End If
            If Session.Item("OPERATORE") = "" Then
                Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
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
                    Response.Write("<script>parent.main.location.replace('SceltaTipoInvito.aspx');</script>")
                Case "Preferenze"
                    Response.Write("<script>parent.main.location.replace('SceltaPreferenze.aspx');</script>")
                Case 2
                    Response.Write("<script>parent.main.location.replace('RicercaAbbinamento.aspx');</script>")
                Case 3
                    Response.Write("<script>parent.main.location.replace('RicercaOfferta.aspx');</script>")
                Case 4
                    Response.Write("<script>parent.main.location.replace('RicercaOffertaCT.aspx');</script>")
                Case "ALER"
                    Response.Write("<script>parent.main.location.replace('DispAler.aspx');</script>")
                Case "COMUNE"
                    Response.Write("<script>parent.main.location.replace('RicercaUI.aspx');</script>")
                Case "Assegnazione"
                    If Session.Item("ASS_PROVV") = "1" Then
                        Response.Write("<script>parent.main.location.replace('RicercaAss.aspx');</script>")
                    Else
                        Response.Write("<script>alert('Non Disponibile o Utente non abilitato!');</script>")
                    End If
                Case "Annullo"
                    Response.Write("<script>parent.main.location.replace('RicercaAnnulli.aspx');</script>")
                Case "AnnulloProposta"
                    Response.Write("<script>parent.main.location.replace('RicercaAnnulliProp.aspx');</script>")
                Case "ElencoComune"
                    'Response.Write("<script>window.open('ElencoComune.aspx','Disponibilita','');</script>")
                    Response.Write("<script>parent.main.location.replace('Manutenzione.aspx');</script>")

                Case "AnnulloAcc"
                    Response.Write("<script>parent.main.location.replace('RicercaAnnulloAss.aspx');</script>")
                Case "StDispUI"
                    Response.Write("<script>parent.main.location.replace('RicercaStatoDispUI.aspx');</script>")
                Case "iter_decisioni"
                    Response.Write("<script>parent.main.location.replace('IntroProcessoDec.aspx');</script>")
                Case "report"
                    Response.Write("<script>parent.main.location.replace('RicercaAbbAutomatici.aspx');</script>")
                Case "modMotivazioni"
                    Response.Write("<script>parent.main.location.replace('InserimMotivRifiutoAnnAss.aspx');</script>")
                Case "inserimDich"
                    Response.Write("<script>window.open('../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=-1&GLocat=1&fuoriMilano=1');</script>")
                Case "ricercaDich"
                    Response.Write("<script>parent.main.location.replace('RicercaDichFuoriMI.aspx');</script>")
                Case "abbFuori"
                    Response.Write("<script>parent.main.location.replace('RicercaDichFuoriMI.aspx?AB=1');</script>")

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
End Namespace