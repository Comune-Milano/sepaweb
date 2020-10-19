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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            If Session.Item("OPERATORE") = "" Then
                Response.Redirect("~/AccessoNegato.htm", True)
                Exit Sub
            End If
            'T1 = administrator
            'T2 = superuser
            'T3 = gestione operatori (flag)



            If Not IsPostBack Then
                Label3.Text = Format(Now(), "dd/MM/yyyy")

                T1.Visible = False
                T2.Visible = False
                T3.Visible = False


                If Session.Item("LIVELLO") = "1" Then '*, lorenzo, marco
                    T1.Visible = True
                    T2.Visible = False
                    T3.Visible = False
                Else

                    If Session.Item("ID_CAF") = 2 And Session.Item("RESPONSABILE") = 1 Then
                        'T2
                        T1.Visible = False
                        T2.Visible = True
                        T3.Visible = False
                    ElseIf Session.Item("GEST_OPERATORI") = "1" And Session.Item("ID_CAF") = "6" Then
                        'T3
                        T1.Visible = False
                        T2.Visible = False
                        T3.Visible = True
                    End If
                End If

            End If
        End Sub

        Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged
            If Session.Item("LAVORAZIONE") = "1" Then
                Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
                Exit Sub
            End If
            Select Case T1.SelectedValue
                Case 1
                    'Response.Write("<script>parent.main.location.replace('SceltaTipoInvito.aspx');</script>")
                Case "LogIngressi"
                    Response.Write("<script>parent.main.location.replace('RicercaLogIngressi.aspx');</script>")
                Case 2
                    Response.Write("<script>parent.main.location.replace('Operatore.aspx?ID=-1');</script>")
                Case 3
                    Response.Write("<script>parent.main.location.replace('RicercaOp.aspx');</script>")
                Case "Password"
                    Response.Write("<script>parent.main.location.replace('Password.aspx');</script>")
                Case 50
                    Response.Write("<script>top.location.replace('../AreaPrivata.aspx');</script>")
                Case "ParamErrori"
                    Response.Write("<script>parent.main.location.replace('ParametriErrori.aspx');</script>")
                Case "VisErrori"
                    Response.Write("<script>parent.main.location.replace('VisErrori.aspx');</script>")
                Case "RevocaTutti"
                    Response.Write("<script>parent.main.location.replace('RevocaTutti.aspx');</script>")
                Case "AbilitaTutti"
                    Response.Write("<script>parent.main.location.replace('AnnullaRevoca.aspx');</script>")
                Case "Commissariati"
                    Response.Write("<script>parent.main.location.replace('Commissariati.aspx');</script>")
                Case "Filiali"
                    Response.Write("<script>parent.main.location.replace('Filiali.aspx');</script>")
                Case "Quartieri"
                    Response.Write("<script>parent.main.location.replace('Quartieri.aspx');</script>")
                Case "Zone"
                    Response.Write("<script>parent.main.location.replace('Zone.aspx');</script>")
                Case "Ripristina"
                    Response.Write("<script>parent.main.location.replace('RipristinaAnnullo.aspx');</script>")
                Case "Errori"
                    Response.Write("<script>parent.main.location.replace('MavErrori.aspx');</script>")
                Case "Anomalie"
                    Response.Write("<script>parent.main.location.replace('MavAnomalie.aspx');</script>")
                Case "Log"
                    Response.Write("<script>parent.main.location.replace('Log.aspx');</script>")
                Case "NuovoComune"
                    Response.Write("<script>parent.main.location.replace('NuovoComune.aspx');</script>")
                Case "Gestione_Eventi"
                    Response.Write("<script>parent.main.location.replace('RicercaEventi.aspx');</script>")
                Case "ElencoComuni"
                    Response.Write("<script>parent.main.location.replace('OperatoreSUA/elencoComuniSUA.aspx');</script>")
                Case "Stato"
                    Response.Write("<script>parent.main.location.replace('RicercaStatoOperatori.aspx');</script>")
                Case "AccessoRU"
                    Response.Write("<script>parent.main.location.replace('AccessoRU.aspx');</script>")
                Case "utenza_cont_solid"
                    If Session.Item("ID_OPERATORE") = 1 Then
                        Response.Write("<script>parent.main.location.replace('../Fondo_solidarieta/GestUtenteFondoSolid.aspx');</script>")
                    End If
            End Select
            T1.Nodes(0).Selected = True
        End Sub

        Protected Sub T2_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T2.SelectedNodeChanged
            If Session.Item("LAVORAZIONE") = "1" Then
                Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
                Exit Sub
            End If
            Select Case T2.SelectedValue
                'MENU OPERATORI
                Case 2
                    Response.Write("<script>parent.main.location.replace('OperatoreSUA/OperatoreSUA.aspx?ID=-1');</script>")
                Case 3
                    Response.Write("<script>parent.main.location.replace('OperatoreSUA/RicercaOPSUA.aspx');</script>")
                Case 4
                    Response.Write("<script>parent.main.location.replace('OperatoreSUA/RevocaTuttiSUA.aspx');</script>")
                Case 5
                    Response.Write("<script>parent.main.location.replace('OperatoreSUA/AnnullaRevocaSUA.aspx');</script>")
                Case 6
                    Response.Write("<script>parent.main.location.replace('RicercaStatoOperatori.aspx');</script>")
                Case 7
                    Response.Write("<script>parent.main.location.replace('OperatoreSUA/RicercaLogIngressiSUA.aspx');</script>")
                    'MENU COMMISSARIATI
                Case 8
                    Response.Write("<script>parent.main.location.replace('Commissariati.aspx');</script>")
                    'MENU STRUTTURE
                Case 9
                    Response.Write("<script>parent.main.location.replace('Filiali.aspx');</script>")

                    'MENU QUARTIERI
                Case 10
                    Response.Write("<script>parent.main.location.replace('Quartieri.aspx');</script>")
                    'MENU COMUNI
                Case 12
                    Response.Write("<script>parent.main.location.replace('NuovoComune.aspx');</script>")
                Case 13
                    Response.Write("<script>parent.main.location.replace('OperatoreSUA/elencoComuniSUA.aspx');</script>")
                Case 15
                    Response.Write("<script>parent.main.location.replace('OperatoreSUA/assegnaStrutturaSUA.aspx');</script>")
                    'MENU ESCI
                Case 50
                    Response.Write("<script>top.location.replace('../AreaPrivata.aspx');</script>")
                Case "RuAbusivi"
                    Response.Write("<script>parent.main.location.replace('OperatoreSUA/RUAbusivi.aspx');</script>")
                Case "Microzone" 'per microzone
                    Response.Write("<script>parent.main.location.replace('OperatoreSUA/GestioneMicrozone.aspx');</script>")
                Case "AccessoRU"
                    Response.Write("<script>parent.main.location.replace('AccessoRU.aspx');</script>")
            End Select
            T2.Nodes(0).Selected = True
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

        Protected Sub T3_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T3.SelectedNodeChanged
            Select Case T3.SelectedValue

                Case 2
                    Response.Write("<script>parent.main.location.replace('Operatore.aspx?ID=-1');</script>")
                Case 3
                    Response.Write("<script>parent.main.location.replace('RicercaOp.aspx');</script>")

                Case 50
                    Response.Write("<script>top.location.replace('../AreaPrivata.aspx');</script>")

            End Select
            T2.Nodes(0).Selected = True

        End Sub
    End Class
End Namespace