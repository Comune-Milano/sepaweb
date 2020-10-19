Namespace CM

Partial Class menu
        Inherits PageSetIdMode
        Dim par As New CM.Global

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
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
            If Not IsPostBack Then
                Label3.Text = Format(Now(), "dd/MM/yyyy")

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "select extra from odg"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Dim a As TreeNode
                If myReader.Read Then
                    If par.IfNull(myReader("EXTRA"), "0") = "0" Then
                        If T1.Nodes(9).ChildNodes(0).Text = "Nuova" Then
                            a = T1.Nodes(9).ChildNodes(0)
                            T1.Nodes(9).ChildNodes.Remove(a)
                        End If
                    End If
                End If

                If Session.Item("MOD_ERP_POS_GRAD") = "0" Then
                    par.RimuoviNodoMenu(T1, "CPGrad")
                End If

                If Session.Item("ID_CAF") = "2" Then
                    par.RimuoviNodoMenu(T1, "500")
                    par.RimuoviNodoMenu(T1, "10")
                    par.RimuoviNodoMenu(T1, "14")
                    par.RimuoviNodoMenu(T1, "9")
                    par.RimuoviNodoMenu(T1, "154")
                End If


                par.cmd.Dispose()
                myReader.Close()
                par.OracleConn.Close()

            End If
        End Sub


        Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged
            If Session.Item("LAVORAZIONE") = "1" Then
                Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
                Exit Sub
            End If
            Select Case T1.SelectedValue
                Case 1
                    Response.Write("<script>parent.main.location.replace('max.aspx?ID=-1');</script>")
                Case 2
                    Response.Write("<script>parent.main.location.replace('RicercaDichiarazioni.aspx');</script>")
                Case 4
                    Response.Write("<script>parent.main.location.replace('AssociaDichiarazione.aspx');</script>")
                Case 5
                    Response.Write("<script>parent.main.location.replace('RicercaDomande.aspx');</script>")
                Case 6
                    Response.Write("<script>parent.main.location.replace('RicercaDomandePrecedenti.aspx');</script>")
                Case 7
                    'Response.Write("<script>window.open('cf/codice.htm','cf','scrollbars=no,resizable=no,width=500,height=380,status=no,location=no,toolbar=no');</script>")
                Case 15
                    Response.Write("<script>window.open('Prenotazione.aspx','Prenotazioni','top=0,left=0,width=400,height=270');</script>")
                Case 16
                    Response.Write("<script>window.open('ListaPrenotazioni.aspx','Lista');</script>")
                Case 23
                    'If Session.Item("ID_CAF") = 6 Or Session.Item("ID_CAF") = 2 Then
                    Response.Write("<script>parent.main.location.replace('RicercaRinnovi.aspx');</script>")
                    'Else
                    'Response.Write("<script>alert('Funzione non disponibile al momento per lavori in corso..');</script>")
                    'End If
                Case 154
                    'If Session.Item("ID_CAF") = 6 Or Session.Item("ID_CAF") = 2 Then
                    Response.Write("<script>parent.main.location.replace('RicercaIntegrazioni.aspx');</script>")
                    'Else
                    'Response.Write("<script>alert('Funzione non disponibile al momento per lavori in corso..');</script>")
                    'End If
                Case 500
                    Response.Write("<script>parent.main.location.replace('CambioIntestazione.aspx');</script>")
                Case "CPGrad"
                    Response.Write("<script>parent.main.location.replace('CambioPosGrad.aspx');</script>")
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
