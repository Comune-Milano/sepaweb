
Partial Class CENSIMENTO_menu_sinistra
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged

        If Session.Item("LAVORAZIONE") = "1" Then
            Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
            T1.Nodes(0).Selected = True
            Exit Sub
        End If
        Select Case T1.SelectedValue
            'Nodi SottoMenù Complessi
            Case "Flussi"
                Response.Write("<script>window.open('Flussi/Home.aspx');</script>")
            Case "Report"
                ' Response.Write("<script>window.open('Morosita.aspx');</script>")
                Response.Write("<script>parent.main.location.replace('RicMorosita.aspx');</script>")
            Case "Ricerca"
                Response.Write("<script>parent.main.location.replace('RicUtente.aspx');</script>")
            Case "Oneri"
                Response.Write("<script>parent.main.location.replace('Oneri.aspx?CHIAMA=O');</script>")
            Case "Media"
                Response.Write("<script>parent.main.location.replace('Oneri.aspx?CHIAMA=M');</script>")
            Case "PropManage"
                Response.Write("<script>parent.main.location.replace('Oneri.aspx?CHIAMA=P');</script>")
            Case "NumBolletta"
                Response.Write("<script>parent.main.location.replace('RicBolletta.aspx');</script>")

            Case "SingoleVoci"
                Response.Write("<script>parent.main.location.replace('../Contratti/Report/P_SingoleVoci.aspx');</script>")
            Case "SingVociNon"
                Response.Write("<script>parent.main.location.replace('../Contratti/Report/P_SingoleVociNon.aspx');</script>")

            Case "InserisciPag"
                If Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('../Contratti/Pagamenti/Manuale.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "RateEmesse"
                Response.Write("<script>parent.main.location.replace('../Contratti/Report/DistintaRateEmesse.aspx');</script>")
            Case "DSingoleVoci"
                Response.Write("<script>parent.main.location.replace('../Contratti/Report/DistintaRateSingoleVoci.aspx');</script>")
            Case "Accertato"
                Response.Write("<script>parent.main.location.replace('../Contratti/Report/RicercaAccertato.aspx');</script>")
            Case "Allegati"
                If Session.Item("MOD_CONT_ALLEGATI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('../Contratti/Report/AllegatiAccertato.aspx');</script>")
                End If
            Case "DataEmissione"
                Response.Write("<script>parent.main.location.replace('../Contratti/Report/PagamentiPervenuti.aspx?TIPO=1');</script>")
            Case "DataPagamento"
                Response.Write("<script>parent.main.location.replace('../Contratti/Report/PagamentiPervenuti.aspx?TIPO=2');</script>")
            Case "NPDataEmissione"
                Response.Write("<script>parent.main.location.replace('../Contratti/Report/PagamentiPervenuti.aspx?TIPO=3');</script>")
            Case "NPDataPagamento"
                Response.Write("<script>parent.main.location.replace('../Contratti/Report/PagamentiPervenuti.aspx?TIPO=4');</script>")
            Case "CompALER"
                Response.Write("<script>parent.main.location.replace('AnnoCalcComp.aspx?CHIAMA=POLI');</script>")
            Case "Gestione"
                Response.Write("<script>parent.main.location.replace('AnnoCalcComp.aspx?CHIAMA=MENSI');</script>")
            Case "Solleciti"
                Response.Write("<script>parent.main.location.replace('../Contratti/Report/Solleciti.aspx');</script>")
            Case "SituazioneCont."
                Response.Write("<script>parent.main.location.replace('SituazioneCont.aspx');</script>")
            Case "Prelievi"
                Response.Write("<script>parent.main.location.replace('Flussi/RicercaPrelievi.aspx');</script>")

            Case "Facility"
                Response.Write("<script>parent.main.location.replace('DateCompensi.aspx?CHIAMA=FACILITY');</script>")
                'Response.Write("<script>alert('Funzione al momento non disponibile!')</script>")
            Case "SitCont"
                Response.Write("<script>parent.main.location.replace('RicercaSituazContabile.aspx');</script>")
            Case "patrTipoUI"
                Response.Write("<script>parent.main.location.replace('../CENSIMENTO/Report/RicercaDaComplesso.aspx?U=1');</script>")
            Case "patrStatoUI"
                Response.Write("<script>parent.main.location.replace('../CENSIMENTO/Report/RicercaDaComplesso.aspx?U=2');</script>")
            Case "patrGruppiUI"
                Response.Write("<script>parent.main.location.replace('../CENSIMENTO/Report/RicercaDaComplesso.aspx?U=3');</script>")
            Case "RptEmissioni"
                Response.Write("<script>parent.main.location.replace('Report/ReportSituazioneEmissioni.aspx');</script>")
            Case "RimuoviAccertamento"
                Response.Write("<script>parent.main.location.replace('Report/ReportSituazioneEmissioniAcc.aspx');</script>")
            Case "RptIncassi"
                Response.Write("<script>parent.main.location.replace('Report/ReportSituazioneIncassi.aspx');</script>")
 			'************** 258/2017
            Case "RptMorosita"
                Response.Write("<script>parent.main.location.replace('Report/ReportSituazioneMorosita.aspx');</script>")
            Case "RptEmesso"
                Response.Write("<script>parent.main.location.replace('Report/ReportSituazioneMorosita.aspx?TIPO=EM');</script>")
            Case "ElencoEstrazioni"
                Response.Write("<script>window.open('../Contratti/VisualizzaEstrazioni_RU.aspx?TIPO=RPT_MOR','','');</script>")
                '**************
            Case "RptIncassiR"
                If Session.Item("MOD_REPORT_RUOLI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('Report/ReportSitIncassiRuoli.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "NuovoAssegno"
                Response.Write("<script>parent.main.location.replace('IncassiManuali.aspx');</script>")

            Case "RicercaAssegno"
                Response.Write("<script>parent.main.location.replace('RicercaIncassiManuali.aspx');</script>")

            Case "RptResidui"
                Response.Write("<script>parent.main.location.replace('Report/ReportGestioneResidui.aspx');</script>")
            Case "ElencoResidui"
                Response.Write("<script>parent.main.location.replace('Report/ElencoResidui.aspx');</script>")
            Case "RptEmissioni2"
                Response.Write("<script>parent.main.location.replace('Report/ReportSituazioneEmissioni2.aspx');</script>")

            Case "RptResidui2"
                Response.Write("<script>parent.main.location.replace('Report/ReportGestioneResidui2.aspx');</script>")

            Case "TipoIncExtra"
                Response.Write("<script>parent.main.location.replace('Report/TipologiaPagamenti.aspx');</script>")
            Case "TipoIncRuoli"
                Response.Write("<script>parent.main.location.replace('Report/TipologiaPagamentiRuolo.aspx');</script>")

            Case "ACompAcert"
                Response.Write("<script>parent.main.location.replace('Report/TipologiaAnno.aspx');</script>")

            Case "Capitoli"
                Response.Write("<script>parent.main.location.replace('Report/Capitoli.aspx');</script>")

            Case "ValCapitoli"
                Response.Write("<script>parent.main.location.replace('Report/Validita.aspx');</script>")
            Case "CompVoci"
                Response.Write("<script>parent.main.location.replace('Report/CompetenzaAnno.aspx');</script>")
            Case "LogRend"
                Response.Write("<script>parent.main.location.replace('LogRendicontazione.aspx');</script>")
            Case "AnomalieRend"
                Response.Write("<script>parent.main.location.replace('AnomalieRendicontazione.aspx');</script>")

        End Select
        T1.Nodes(0).Selected = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Dim tn As TreeNode

            Label3.Text = Format(Now(), "dd/MM/yyyy")

            If Session.Item("CONT_RAGIONERIA") = "0" Then
                T1.Nodes(0).Text = "---"
                T1.Nodes(0).ChildNodes.Clear()
                T1.Nodes(0).SelectAction = TreeNodeSelectAction.None
            End If

            If Session.Item("CONT_PATRIMONIALI") = "0" Then
                T1.Nodes(1).Text = "---"
                T1.Nodes(1).ChildNodes.Clear()
                T1.Nodes(1).SelectAction = TreeNodeSelectAction.None
            End If

            If Session.Item("CONT_FLUSSI") = "0" Then
                T1.Nodes(2).Text = "---"
                T1.Nodes(2).ChildNodes.Clear()
                T1.Nodes(2).SelectAction = TreeNodeSelectAction.None
            End If

            If Session.Item("CONT_RIMB_ALER") = "0" Then
                T1.Nodes(3).Text = "---"
                T1.Nodes(3).ChildNodes.Clear()
                T1.Nodes(3).SelectAction = TreeNodeSelectAction.None
            End If

            If Session.Item("CONT_PRELIEVI") = "0" Then
                T1.Nodes(4).Text = "---"
                T1.Nodes(4).ChildNodes.Clear()
                T1.Nodes(4).SelectAction = TreeNodeSelectAction.None
            End If

            If Session.Item("CONT_COMPENSI") = "0" Then
                T1.Nodes(5).Text = "---"
                T1.Nodes(5).ChildNodes.Clear()
                T1.Nodes(5).SelectAction = TreeNodeSelectAction.None
            End If

            If Session.Item("OPERATORE") <> "*" Then
                tn = T1.FindNode("Ragioneria/RptEmissioni2")

                If Not IsNothing(tn) Then
                    T1.Nodes(0).ChildNodes.Remove(tn)
                    tn = T1.FindNode("Ragioneria/RptResidui2")

                    T1.Nodes(0).ChildNodes.Remove(tn)
                End If

            End If
            'MANDATI DI PAGAMENTO 06/07/2011
            If Session.Item("MOD_GEST_TIPO_PAG") = 0 Then
                tn = T1.FindNode("Ragioneria/Gest. Tabelle")
                If Not IsNothing(tn) Then
                    T1.Nodes(0).ChildNodes.Remove(tn)
                End If
            End If

            If Session.Item("MOD_ANOMALIE_RENDICONTAZIONE") = 0 And Session.Item("MOD_LOG_RENDICONTAZIONE") = 0 Then
                par.RimuoviNodoMenu(T1, "RendPagamenti")
            ElseIf Session.Item("MOD_ANOMALIE_RENDICONTAZIONE") = 1 And Session.Item("MOD_LOG_RENDICONTAZIONE") = 0 Then
                par.RimuoviNodoMenu(T1, "LogRend")
            ElseIf Session.Item("MOD_ANOMALIE_RENDICONTAZIONE") = 0 And Session.Item("MOD_LOG_RENDICONTAZIONE") = 1 Then
                par.RimuoviNodoMenu(T1, "AnomalieRend")
            End If

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
