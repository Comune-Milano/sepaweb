
Partial Class ANAUT_menu_sinistra
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If Not IsPostBack Then
            'MAX 14/10/2014
            If Session.Item("MOD_AU_CF") <> "1" Then
                par.RimuoviNodoMenu(T1, "3")
            End If

            If Session.Item("MOD_AU_RICERCA") <> "1" Then
                par.RimuoviNodoMenu(T1, "2")
            End If

            If Session.Item("MOD_AU_REPORT") <> "1" Then
                par.RimuoviNodoMenu(T1, "Report")
            End If

            If Session.Item("MOD_AU_AGENDA_CERCA") <> "1" Then
                par.RimuoviNodoMenu(T1, "CercaInq")
                par.RimuoviNodoMenu(T1, "SitGenerale")
                par.RimuoviNodoMenu(T1, "FronteSpizi")
                par.RimuoviNodoMenu(T1, "Inq.SenzaApp.")
            End If

            If Session.Item("MOD_AU_AGENDA_SOSPESE") <> "1" Then
                par.RimuoviNodoMenu(T1, "Sospese")
            End If

            If Session.Item("MOD_AU_AGENDA_MOTS") <> "1" Then
                par.RimuoviNodoMenu(T1, "GestMotSosp")
            End If

            If Session.Item("AU_DOC_NEC") <> "1" Then
                par.RimuoviNodoMenu(T1, "DocNecessaria")
            End If

            If Session.Item("DECIDI_DEC") <> "1" Then
                par.RimuoviNodoMenu(T1, "Decadenze")
                par.RimuoviNodoMenu(T1, "DecidiProposte")
            End If

            If Session.Item("MOD_AU_DIFF_MP") <> "1" Then
                par.RimuoviNodoMenu(T1, "NonRispondenti")
                par.RimuoviNodoMenu(T1, "NonRispondenti45")
                par.RimuoviNodoMenu(T1, "RicPostAler")
                par.RimuoviNodoMenu(T1, "ElencoFileIncompleti")
                par.RimuoviNodoMenu(T1, "ElencoFile")
                par.RimuoviNodoMenu(T1, "Non Rispondenti")
                '
            End If

            If Session.Item("MOD_AU_NUOVOGRUPPO") <> "1" Then
                par.RimuoviNodoMenu(T1, "NuovoGruppo")
                par.RimuoviNodoMenu(T1, "RicercaGruppo")
            End If

            If Session.Item("MOD_AU_SIMULA_APPLICA_AU") <> "1" Then
                par.RimuoviNodoMenu(T1, "SimulaRispondenti")
                par.RimuoviNodoMenu(T1, "Verifica")
                par.RimuoviNodoMenu(T1, "NonRisp")
                par.RimuoviNodoMenu(T1, "Rispondenti")
                par.RimuoviNodoMenu(T1, "ApplicaNonRispondenti")
                par.RimuoviNodoMenu(T1, "ElencoGenerale")
            End If

            If Session.Item("MOD_AU_CONV_SINDACATI") <> "1" Then
                par.RimuoviNodoMenu(T1, "Sospese")
            End If

            If Session.Item("MOD_AU_GESTIONE") <> "1" Then
                par.RimuoviNodoMenu(T1, "TassoRendimento")
                par.RimuoviNodoMenu(T1, "GestioneAU")
                par.RimuoviNodoMenu(T1, "GestMotSosp")
                par.RimuoviNodoMenu(T1, "Tempistica")
                par.RimuoviNodoMenu(T1, "ReportSit")
                par.RimuoviNodoMenu(T1, "Verifiche")
            End If

            If Session.Item("MOD_AU_GESTIONE_MOD") <> "1" Then
                par.RimuoviNodoMenu(T1, "ModelliAU")
            End If

            If Session.Item("MOD_AU_GESTIONE_STR") <> "1" Then
                par.RimuoviNodoMenu(T1, "FilSpOp")
            End If

            If Session.Item("MOD_AU_GESTIONE_LISTE") <> "1" Then
                par.RimuoviNodoMenu(T1, "CreaListaConv")
            End If

            If Session.Item("MOD_AU_GESTIONE_CONVOCABILI") <> "1" Then
                par.RimuoviNodoMenu(T1, "ElencoConv")
                par.RimuoviNodoMenu(T1, "ElencoLettere")
                par.RimuoviNodoMenu(T1, "convocabili")
                par.RimuoviNodoMenu(T1, "ReportConvocazioni")
            End If

            If Session.Item("MOD_AU_GESTIONE_ESCLUSIONI") <> "1" Then
                par.RimuoviNodoMenu(T1, "GestEsclusione")
            End If

            If Session.Item("MOD_AU_GESTIONE_GRUPPI") <> "1" Then
                par.RimuoviNodoMenu(T1, "GruppiConv")
            End If

            If Session.Item("MOD_AU_CREA_CONV") <> "1" Then
                par.RimuoviNodoMenu(T1, "Simulazione")
                par.RimuoviNodoMenu(T1, "ElencoSimulazioni")
                par.RimuoviNodoMenu(T1, "EmissioneLettere")
                par.RimuoviNodoMenu(T1, "DataSpedizione")
            End If

            If Session.Item("MOD_AU_DIFF_MP") <> "1" Then
                par.RimuoviNodoMenu(T1, "CreaIncomplete")
                par.RimuoviNodoMenu(T1, "ElencoFileIncompleti")
                par.RimuoviNodoMenu(T1, "Incomplete")
                '
            End If

            If Session.Item("MOD_AU_SIMULA_APPLICA_AU") <> "1" Then
                par.RimuoviNodoMenu(T1, "VerificaApp")
                par.RimuoviNodoMenu(T1, "VerificaAppNON")
                par.RimuoviNodoMenu(T1, "SimulaAbusivi")
                par.RimuoviNodoMenu(T1, "ApplicaAbusivi")
                par.RimuoviNodoMenu(T1, "ApplicaCanoni")
            End If

            If Session.Item("LIVELLO") <> "1" Then
                par.RimuoviNodoMenu(T1, "20")
            End If

            If Session.Item("MOD_AU_GESTIONE") <> "1" And Session.Item("MOD_AU_GESTIONE_MOD") <> "1" And Session.Item("MOD_AU_GESTIONE_STR") <> "1" Then
                par.RimuoviNodoMenu(T1, "AnagrafeUtenza")
            End If

            If Session.Item("MOD_AU_CREA_CONV") <> "1" And Session.Item("MOD_AU_GESTIONE_CONVOCABILI") <> "1" And Session.Item("MOD_AU_GESTIONE_ESCLUSIONI") <> "1" And Session.Item("MOD_AU_GESTIONE_GRUPPI") <> "1" And Session.Item("MOD_AU_GESTIONE_LISTE") <> "1" Then
                par.RimuoviNodoMenu(T1, "ConvocazioniA")
            End If

            If Session.Item("MOD_AU_AGENDA_MOTS") <> "1" And Session.Item("MOD_AU_AGENDA_SOSPESE") <> "1" And Session.Item("MOD_AU_AGENDA_CERCA") <> "1" Then
                par.RimuoviNodoMenu(T1, "AgendaAU")
            End If

            If Session.Item("MOD_AU_SIMULA_APPLICA_AU") <> "1" Then
                par.RimuoviNodoMenu(T1, "Verifica123")
            End If
            If Session.Item("MOD_AU_NUOVOGRUPPO") <> "1" And Session.Item("MOD_AU_SIMULA_APPLICA_AU") <> "1" Then
                par.RimuoviNodoMenu(T1, "Gruppi")
            End If
            If Session.Item("MOD_AU_SIMULA_APPLICA_AU") <> "1" Then
                par.RimuoviNodoMenu(T1, "Simula")
                par.RimuoviNodoMenu(T1, "Applica")
            End If

            If Session.Item("MOD_AU_SIMULA_APPLICA_AU") <> "1" And Session.Item("MOD_AU_NUOVOGRUPPO") <> "1" And Session.Item("MOD_AU_SIMULA_APPLICA_AU") <> "1" Then
                par.RimuoviNodoMenu(T1, "Applicazione AU")
            End If

            If Session.Item("MOD_AU_GESTIONE") <> "1" And Session.Item("MOD_AU_DIFF_MP") <> "1" Then
                par.RimuoviNodoMenu(T1, "Diffide")
            End If

            If Session.Item("ANAGRAFE_CONSULTAZIONE") = "1" And Session.Item("LIVELLO") <> "1" Then
                T1.Visible = False
                TreeView1.Visible = True
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
                Response.Write("<script>alert('Non disponibile!');</script>")
            Case 2
                If Session.Item("MOD_AU_RICERCA") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RicercaDichiarazioni.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "DecidiProposte"
                If Session.Item("DECIDI_DEC") = "1" Then
                    Response.Write("<script>parent.main.location.replace('DecidiProposte.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "ElencoProposte"
                If Session.Item("DECIDI_DEC") = "1" Then
                    Response.Write("<script>window.open('ElencoProposteDec.aspx','','');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "Riepilogo"
                If Session.Item("MOD_AU_REPORT") = "1" Then
                    Response.Write("<script>parent.main.location.replace('Riepilogo.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "DocNecessaria"
                Response.Write("<script>parent.main.location.replace('DocNecessari.aspx');</script>")
            Case "NonRispondenti"
                If Session.Item("MOD_AU_DIFF_MP") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RicercaNonRispondenti.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "NonRispondenti45"
                If Session.Item("MOD_AU_DIFF_MP") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RicercaNonR45.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "ElencoFile"
                Response.Write("<script>parent.main.location.replace('ElencoDiffideIncomplete.aspx?T=1');</script>")
            Case "RicPostAler"
                If Session.Item("MOD_AU_DIFF_MP") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RicercaRicPostAler.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "NuovoGruppo"
                If Session.Item("MOD_AU_NUOVOGRUPPO") = "1" Then
                    Response.Write("<script>parent.main.location.replace('GruppoAU.aspx?ID=-1');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "RicercaGruppo"
                If Session.Item("MOD_AU_NUOVOGRUPPO") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RicercaGruppiLavoro.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If

            Case "SimulaRispondenti"
                If Session.Item("MOD_AU_SIMULA_APPLICA_AU") = "1" Then
                    Response.Write("<script>parent.main.location.replace('SimulaApplica.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "AU39278"
                If Session.Item("MOD_AU_SIMULA_APPLICA_AU") = "1" Then
                    Response.Write("<script>parent.main.location.replace('Simula39278.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "Verifica"
                If Session.Item("MOD_AU_SIMULA_APPLICA_AU") = "1" Then
                    Response.Write("<script>parent.main.location.replace('VerificaS.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "NonRisp"
                If Session.Item("MOD_AU_SIMULA_APPLICA_AU") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RicercaSimulazioneNonR.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "Rispondenti"
                If Session.Item("MOD_AU_SIMULA_APPLICA_AU") = "1" Then
                    Response.Write("<script>parent.main.location.replace('SimulaApplica1.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "ApplicaNonRispondenti"
                If Session.Item("MOD_AU_SIMULA_APPLICA_AU") = "1" Then
                    Response.Write("<script>parent.main.location.replace('PreApplicazione.aspx?T=2');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If

            Case "ElencoGenerale"
                If Session.Item("MOD_AU_SIMULA_APPLICA_AU") = "1" Then
                    Response.Write("<script>window.open('ElencoGeneraleApplicazioneAU.aspx','','');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "Guida"
                Response.Write("<script>window.open('Applicazione_AU.pdf','','');</script>")

            Case "ParametriCon"
                Response.Write("<script>parent.main.location.replace('ParametriConvocazioni.aspx');</script>")
            Case "ImportXLS"
                Response.Write("<script>alert('Non Disponibile!');</script>")
            Case "ReportConvocazioni"
                If Session.Item("MOD_AU_GESTIONE_CONVOCABILI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RicercaConvocazioni.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "GeneraLettera"
                Response.Write("<script>alert('Non Disponibile!');</script>")
            Case "ElencoLettere"
                If Session.Item("MOD_AU_GESTIONE_CONVOCABILI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('ElencoLettereConvocazioni.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "SitGenerale"
                Response.Write("<script>window.open('SituazioneAgenda.aspx','','');</script>")
            Case "CercaInq"
                Response.Write("<script>parent.main.location.replace('RicercaInquilino.aspx');</script>")
            Case "Inq.SenzaApp."
                Response.Write("<script>parent.main.location.replace('InserimentoManuale.aspx');</script>")
            Case "FronteSpizio"
                Response.Write("<script>parent.main.location.replace('Stampe/FascicoloMassivo.aspx');</script>")
            Case "ElencoFS"
                Response.Write("<script>parent.main.location.replace('ElencoFileFronteSpizi.aspx');</script>")
            Case "Sportelli"
                Response.Write("<script>parent.main.location.replace('ElencoSportelli.aspx');</script>")
            Case "Sospese"
                If Session.Item("MOD_AU_CONV_SINDACATI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RicercaInquilino.aspx?T=1');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If

            Case "TassoRendimento"
                If Session.Item("MOD_AU_GESTIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('GestioneTasso.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "GestioneAU"
                If Session.Item("MOD_AU_GESTIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('GestioneAU.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "ModelliAU"
                If Session.Item("MOD_AU_GESTIONE_MOD") = "1" Then
                    Response.Write("<script>parent.main.location.replace('GestioneModelliAU.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "FilSpOp"
                If Session.Item("MOD_AU_GESTIONE_STR") = "1" Then
                    Response.Write("<script>parent.main.location.replace('GestioneFilSpOp.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "CreaListaConv"
                If Session.Item("MOD_AU_GESTIONE_LISTE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('GestListeConv.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "Assegnatari"
                If Session.Item("MOD_AU_GESTIONE_CONVOCABILI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('Assegnatari.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "Convocabili"
                If Session.Item("MOD_AU_GESTIONE_CONVOCABILI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('Convocabili.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "ElencoConv"
                If Session.Item("MOD_AU_GESTIONE_CONVOCABILI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('ListeConvocabili.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "GestEsclusione"
                If Session.Item("MOD_AU_GESTIONE_ESCLUSIONI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('GestEsclusi.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "GruppiConv"
                If Session.Item("MOD_AU_GESTIONE_GRUPPI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('GestGruppiConv.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "Simulazione"
                If Session.Item("MOD_AU_CREA_CONV") = "1" Then
                    Response.Write("<script>parent.main.location.replace('SimulazioneConv.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "ElencoSimulazioni"
                If Session.Item("MOD_AU_CREA_CONV") = "1" Then
                    Response.Write("<script>parent.main.location.replace('ElencoSimulazioniConv.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "EmissioneLettere"
                If Session.Item("MOD_AU_CREA_CONV") = "1" Then
                    Response.Write("<script>parent.main.location.replace('EmissioneConvocazioni.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "GestMotSosp"
                If Session.Item("MOD_AU_GESTIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('GestioneSosp.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "DataSpedizione"
                If Session.Item("MOD_AU_CREA_CONV") = "1" Then
                    Response.Write("<script>parent.main.location.replace('SpedizioneConvocazioni.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "Tempistica"
                If Session.Item("MOD_AU_GESTIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('TempisticaDiffide.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If

            Case "CreaIncomplete"
                If Session.Item("MOD_AU_DIFF_MP") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RicercaIncompleti.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If

            Case "ElencoFileIncompleti"
                If Session.Item("MOD_AU_DIFF_MP") = "1" Then
                    Response.Write("<script>parent.main.location.replace('ElencoDiffideIncomplete.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If

            Case "ReportSit"
                If Session.Item("MOD_AU_GESTIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('VerifichePreChiusura.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "VerificaApp"
                If Session.Item("MOD_AU_SIMULA_APPLICA_AU") = "1" Then
                    Response.Write("<script>parent.main.location.replace('SimulaGenerale0.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "VerificaAppNON"
                If Session.Item("MOD_AU_SIMULA_APPLICA_AU") = "1" Then
                    Response.Write("<script>parent.main.location.replace('SimulaGenerale0NON.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "SimulaAbusivi"
                If Session.Item("MOD_AU_SIMULA_APPLICA_AU") = "1" Then
                    Response.Write("<script>window.open('SimulazioneApplicazioneAbusivi.aspx','','');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If

            Case "ApplicaAbusivi"
                If Session.Item("MOD_AU_SIMULA_APPLICA_AU") = "1" Then
                    Response.Write("<script>parent.main.location.replace('PreApplicazione.aspx?T=3');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "ApplicaCanoni"
                If Session.Item("MOD_AU_SIMULA_APPLICA_AU") = "1" Then
                    Response.Write("<script>parent.main.location.replace('ApplicaCanone.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "AppRisp"
                Response.Write("<script>parent.main.location.replace('ApplicaCanoneElenco.aspx');</script>")
            Case "DocMancante"
                Response.Write("<script>parent.main.location.replace('rptDocMancante.aspx');</script>")
            Case "Conguaglio"
                Response.Write("<script>parent.main.location.replace('GestCongAU.aspx');</script>")
        End Select
        T1.SelectedNode.Selected = False
    End Sub

    Protected Sub TreeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeView1.SelectedNodeChanged
        If Session.Item("LAVORAZIONE") = "1" Then
            Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
            Exit Sub
        End If
        Select Case TreeView1.SelectedValue

            Case 2
                Response.Write("<script>parent.main.location.replace('RicercaDichiarazioni.aspx');</script>")
            Case "Riepilogo"
                Response.Write("<script>parent.main.location.replace('Riepilogo.aspx');</script>")
        End Select
        TreeView1.Nodes(1).Selected = True
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
