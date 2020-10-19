

Partial Class CENSIMENTO_menu_sinistra
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged
        Select Case T1.SelectedValue

            Case "Agenda"
                Response.Write("<script>var left=(screen.width/2)-(1000/2);var top=(screen.height/2)-(700/2);var targetWin = window.open('Agenda/Agenda.aspx', 'AgendaCallCenter', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,copyhistory=no,width=1000,height=700,top=' + top + ',left=' + left);</script>")
            Case "ParametriAgenda"
                Response.Write("<script>parent.main.location.replace('Agenda/ParametriAgenda.aspx');</script>")
            Case "Nuova"
                Response.Write("<script>parent.main.location.replace('pagina_home.aspx');window.open('NuovaS.aspx','Nuova','height=700px,width=900px,resizable=yes');</script>")

            Case "Complesso"
                Response.Write("<script>parent.main.location.replace('RicercaComplessi.aspx');</script>")
            Case "Edificio"
                Response.Write("<script>parent.main.location.replace('RicercaEdifici.aspx');</script>")
            Case "Inquilino"
                Response.Write("<script>parent.main.location.replace('RicercaOccupante.aspx');</script>")
            Case "Selettiva"
                Response.Write("<script>parent.main.location.replace('RicercaUI.aspx');</script>")
            Case "RicercaS"
                Response.Write("<script>parent.main.location.replace('RicercaS.aspx');</script>")
            Case "Comunali"
                Response.Write("<script>parent.main.location.replace('RisultatiS.aspx?C=1&T=-1&D=&A=');</script>")


            Case "TGuasti"
                Response.Write("<script>parent.main.location.replace('Guasti.aspx');</script>")
            Case "ClosingNote"
                Response.Write("<script>parent.main.location.replace('NoteChiusura.aspx');</script>")


                '***********************REPORT***************************
            Case "SitInt"
                'Response.Write("<script>alert('Funzione non disponibile');</script>")
                Response.Write("<script>parent.main.location.replace('RicercaRpt.aspx?tipo=1');</script>")
            Case "StatoInt"
                Response.Write("<script>parent.main.location.replace('RicercaRpt.aspx?tipo=0');</script>")
                'Response.Write("<script>alert('Funzione non disponibile');</script>")


            Case "Tempi"
                'Response.Write("<script>alert('Funzione non disponibile');</script>")
                Response.Write("<script>parent.main.location.replace('RicercaRpt.aspx?tipo=2');</script>")

        End Select

        T1.SelectedNode.Selected = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If


        If Not IsPostBack Then
            Label3.Text = Format(Now(), "dd/MM/yyyy")

            'Try


            '    par.OracleConn.Open()
            '     par.SettaCommand(par)

            '    par.cmd.CommandText = "SELECT MOD_CALL_CENTER_COM FROM OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE")
            '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    If myReader.Read Then
            '        If par.IfNull(myReader(0), "0") = "0" Then
            '            Session.Add("OP_COM", "0")
            '        Else
            '            Session.Add("OP_COM", "1")
            '        End If
            '    End If
            '    myReader.Close()
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



            'Catch ex As Exception
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'End Try

            Dim tn As TreeNode

            If Session.Item("MOD_CALL_SL") = "1" Then
                tn = T1.FindNode("SEGNALAZIONE")
                If Not IsNothing(tn) Then
                    tn.ChildNodes.Remove(T1.FindNode("SEGNALAZIONE/Nuova"))

                End If
            End If
            If Session.Item("MOD_CALL_GEST") = "0" Then
                tn = T1.FindNode("SupportTable")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
                'tn = T1.FindNode("ClosingNote")
                'If Not IsNothing(tn) Then
                '    T1.Nodes.Remove(tn)
                'End If

            End If

            'If Session.Item("MOD_CALL_SL") = "1" Then
            '    tn = T1.FindNode("SEGNALAZIONE")
            '    If Not IsNothing(tn) Then
            '        T1.Nodes(0).ChildNodes.Remove(tn)
            '    End If
            'End If

            'If Session.Item("OP_COM") = "1" Then
            '    tn = T1.FindNode("Comunali")
            '    If Not IsNothing(tn) Then
            '        T1.Nodes.Remove(tn)
            '    End If
            'End If

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
