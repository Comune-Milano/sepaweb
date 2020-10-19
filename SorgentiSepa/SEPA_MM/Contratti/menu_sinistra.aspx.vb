
Partial Class ANAUT_menu_sinistra
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Dim tn As TreeNode
            Label3.Text = Format(Now(), "dd/MM/yyyy")

            tn = T1.FindNode("ImposteAnteGestore")
            If Not IsNothing(tn) Then
                T1.Nodes.Remove(tn)
            End If

            If Session.Item("ID_OPERATORE") <> "1" And Session.Item("ID_OPERATORE") <> "1436" Then
                tn = T1.FindNode("Contratti/agg_nucleo")
                If Not IsNothing(tn) Then
                    T1.Nodes(0).ChildNodes.Remove(tn)
                End If
            End If
            If Session.Item("FL_RUAU") = "0" Then
                tn = T1.FindNode("ReportRUAU")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
            End If
            If Session.Item("FL_RUSALDI") = "0" Then
                tn = T1.FindNode("ReportRUSaldi")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
            End If
            If Session.Item("FL_ANNULLAVSA") = "0" Then
                par.RimuoviNodoMenu(T1, "AnnullaDom")
            End If
            If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                tn = T1.FindNode("Parametri")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
            Else
                tn = T1.FindNode("Parametri1")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
            End If

            If Session.Item("REST_INT_DEP_CAUS") = "0" And Session.Item("MOD_MOTIVI_DECISIONI") = "0" Then
                tn = T1.FindNode("Parametri1")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
            End If
            If Session.Item("MOD_MASS_INGIUNZIONI") = "0" Then
                par.RimuoviNodoMenu(T1, "caric_mass_ing")
            
        End If

            If Session.Item("MOD_SPALMATORE") = "0" Then
                par.RimuoviNodoMenu(T1, "Spalmatore")
            
        End If
            
        End If
    End Sub

    Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            'Response.Write("<script>alert('CIAO,La tua sessione è scaduta. La pagina sarà chiusa.');self.close();top.location.href='../Portale.aspx';</script>")
        End If

        If Session.Item("LAVORAZIONE") = "1" Then
            Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA.\nSe invece avete chiuso una maschera senza premere il pulsante ESCI, chiudere qyesta finestra e rientrare!')</script>")
            Exit Sub
        End If

        Select Case T1.SelectedValue
            'Case 1
            '   Response.Write("<script>window.open('Contratto.aspx?ID=-1','Contratto" & Format(Now, "hhss") & "','height=610,width=900');</script>")
            Case "2"

                Response.Write("<script>parent.main.location.replace('RicercaContratti.aspx');</script>")

            Case "ERP"
                If Session.Item("CONT_INSERIMENTO") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('NuovoContratto.aspx?TIPO=1');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
                '**** 29/04/2015 Erp fuori Milano
            Case "erpFuoriMI"
                If Session.Item("CONT_INSERIMENTO") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('NuovoContrattoFM.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
                '**** 29/04/2015 FINE Erp fuori Milano
            Case "CambiEmergenza"
                If Session.Item("CONT_INSERIMENTO") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('NuovoContratto.aspx?TIPO=1&ORIG=1');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If

            Case "CAMBI"
                If Session.Item("CONT_INSERIMENTO") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('NuovoContratto.aspx?TIPO=2');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "DIVERSI"
                If Session.Item("CONT_INSERIMENTO") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('NuovoContratto.aspx?TIPO=3');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "Mobilita"
                If Session.Item("CONT_INSERIMENTO") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('NuovoContratto.aspx?TIPO=4');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "392"
                If Session.Item("CONT_INSERIMENTO") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('NuovoContratto.aspx?TIPO=5');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "431"
                If Session.Item("CONT_INSERIMENTO") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('NuovoContratto.aspx?TIPO=6');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "Convenzionato"
                If Session.Item("CONT_INSERIMENTO") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('NuovoContratto.aspx?TIPO=11');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "Abusivi"
                If Session.Item("CONT_INSERIMENTO") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('NuovoContratto.aspx?TIPO=7');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "AnnullaDom"

                Response.Write("<script>parent.main.location.replace('../VSA/Locatari/AnnulloDomanda.aspx');</script>")

            Case "Anagrafica"
                'If Session.Item("CONT_LETTURA") = "0" Then
                Session.Add("CONTRATTOAPERTO", "0")
                Response.Write("<script>window.open('Anagrafica/menu.htm','Anagrafica','height=500,top=0,left=0,width=620');</script>")
                'Else
                'Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                'End If
                '*** 17/02/2014 link Gestione Comuni
            Case "ElencoComuni"
                If Session.Item("MOD_DISTANZE_COMUNI") = "1" Then
                    Response.Write("<script>window.open('../AMMSEPA/OperatoreSUA/elencoComuniSUA.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
                '*** 17/02/2014 FINE link Gestione Comuni
            Case "Unita Assegnate"
                Response.Write("<script>parent.main.location.replace('UnitaAssegnate.aspx');</script>")
            Case "Simula"
                If Session.Item("PARAMETRI_CONTRATTI_BOLL") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('SimulaB.aspx?S=1');</script>")
                End If
            Case "Emetti"
                If Session.Item("PARAMETRI_CONTRATTI_BOLL") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('SimulaB.aspx?S=0');</script>")
                End If
            Case "Int. Legali"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('InteressiLegali.aspx');</script>")
                End If
            Case "ISTAT"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('VariazioniIstat.aspx');</script>")
                End If
            Case "Bollette"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('ParametriBolletta.aspx');</script>")
                End If
            Case "Locatore"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('ParametriLocatore.aspx');</script>")
                End If
                '**** 14/02/2014 Parametri richiedente per registrazione contratto
                '/////////////////////////////////////
            Case "RestCauz"
                If Session.Item("REST_INT_DEP_CAUS") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('ParametriDepCauz.aspx');</script>")
                End If
                '/////////////////////////////////
            Case "RestCauz1"
                If Session.Item("REST_INT_DEP_CAUS") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('ParametriDepCauz.aspx');</script>")
                End If
                '/////////////////////////////////
            Case "Richiedente"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('ParametriRichiedente.aspx');</script>")
                End If
            Case "Versamento"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('ParametriVersamRegistraz.aspx');</script>")
                End If
                '**** 14/02/2014 fine Parametri richiedente per registrazione contratto 
            Case "Lettera Bollette"
                Response.Write("<script>parent.main.location.replace('Comunicazioni/LetteraBolletta.aspx');</script>")
            Case "Lettera Fattura"
                Response.Write("<script>parent.main.location.replace('Comunicazioni/LetteraFattura.aspx');</script>")
            Case "ElencoSimulazioni"
                Response.Write("<script>parent.main.location.replace('ElencoSimulazioni.aspx?T=SIMULAZIONE');</script>")
            Case "RateEmesse"
                Response.Write("<script>parent.main.location.replace('Report/DistintaRateEmesse.aspx');</script>")
            Case "DSingoleVoci"
                Response.Write("<script>parent.main.location.replace('Report/DistintaRateSingoleVoci.aspx');</script>")
            Case "DataEmissione"
                Response.Write("<script>parent.main.location.replace('Report/PagamentiPervenuti.aspx?TIPO=1');</script>")
            Case "DataPagamento"
                Response.Write("<script>parent.main.location.replace('Report/PagamentiPervenuti.aspx?TIPO=2');</script>")
            Case "NPDataEmissione"
                Response.Write("<script>parent.main.location.replace('Report/PagamentiPervenuti.aspx?TIPO=3');</script>")
            Case "NPDataPagamento"
                Response.Write("<script>parent.main.location.replace('Report/PagamentiPervenuti.aspx?TIPO=4');</script>")
            Case "ADEGUAISTAT"
                If Session.Item("CONT_ISTAT") = "1" Then
                    Response.Write("<script>parent.main.location.replace('SceltaIstat.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "VisualizzaISTAT"
                Response.Write("<script>parent.main.location.replace('VisualizzaIstat.aspx');</script>")
                'Case "CalcolaInteressi"
                '    If Session.Item("CONT_INTERESSI") = "1" Then
                '        Response.Write("<script>parent.main.location.replace('Interessi.aspx');</script>")
                '    Else
                '        Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                '    End If
            Case "VisualizzaInteressi"
                'Response.Write("<script>parent.main.location.replace('InteressiVisualizza.aspx');</script>")
                Response.Write("<script>parent.main.location.replace('ElaborazioneInteressiDepCauz.aspx');</script>")
            Case "Commissariati"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('Commissariati.aspx');</script>")
                End If

                '13/07/2012 Link per inserimento delle motivazioni e competenze
            Case "motivSPOST_ANNULL"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    '******************* SOLO PER SUPER USER *******************
                    'If Session.Item("ID_CAF") = 2 And Session.Item("RESPONSABILE") = 1 Then
                    Response.Write("<script>parent.main.location.replace('InserimMotivSpostAnnull.aspx');</script>")
                    'Else
                    'Response.Write("<script>alert('Utente non abilitato!');</script>")
                    'End If
                End If
            Case "Mandatario"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('ParametriMandatario.aspx?T=0');</script>")
                End If

            Case "Mandatario1"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('ParametriMandatario.aspx?T=1');</script>")
                End If
            Case "Mandatario2"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('ParametriMandatario.aspx?T=2');</script>")
                End If
            Case "Mandatario3"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('ParametriMandatario.aspx?T=3');</script>")
                End If

            Case "SingoleVoci"
                Response.Write("<script>parent.main.location.replace('Report/P_SingoleVoci.aspx');</script>")
            Case "GestModelli"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    If Session.Item("PARAMETRI_CONTRATTI_TESTO") = "1" Then
                        Response.Write("<script>parent.main.location.replace('../GestioneModelli/Elenco.aspx');</script>")
                    Else
                        Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                    End If
                End If
            Case "Registrazione"
                If Session.Item("CONT_REGISTRAZIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RegistrazioneTelematicaNEW.aspx');</script>")
                    'Response.Write("<script>parent.main.location.replace('RegistrazioneTelematica.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "modify_rli"
                If Session.Item("CONT_REGISTRAZIONE") = "1" Then
                    Response.Write("<script>window.open('ModificaRLI.aspx','');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "AdempSucc"
                If Session.Item("CONT_IMPOSTE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('AdempimentiSucc.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "cessioni"
                If Session.Item("CONT_IMPOSTE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('AdempimentiSucc.aspx?C=1');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "Ricevute"
                If Session.Item("CONT_REGISTRAZIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RicevutaEntratel.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "RicevuteImposte"
                If Session.Item("CONT_REGISTRAZIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RicevuteEntratelImposte.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "RicevuteCessioni"
                If Session.Item("CONT_REGISTRAZIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RicevutaEntratel.aspx?C=1');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "RicercaRic"
                If Session.Item("CONT_REGISTRAZIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RicercaRicevuteImposte.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
			Case "Frontespizio"
                Response.Write("<script>parent.main.location.replace('GeneraFrontespizio.aspx');</script>")

            Case "RimarcaturaRU"
                If Session.Item("CONT_REGISTRAZIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('MarcaContrattiRegistrAE.aspx');</script>")
                    'Response.Write("<script>parent.main.location.replace('RegistrazioneTelematica.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If

            'Case "ElencoRegistrazione"
            '    Response.Write("<script>parent.main.location.replace('ElencoRegistrazioni.aspx');</script>")
            Case "CreaImposte"
                If Session.Item("CONT_IMPOSTE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('SceltaImposte.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "ElencoImposte"
                Response.Write("<script>parent.main.location.replace('ElencoImposte.aspx');</script>")
            Case "ElencoEmissioni"
                Response.Write("<script>parent.main.location.replace('ElencoEmissioni.aspx');</script>")
            Case "InserisciPag"
                If Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('Pagamenti/Manuale.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "Elimina"
                If Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('EliminaBozze.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "sondrio"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('ParametriSondrio.aspx');</script>")
                End If
            Case "anagrProc"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('ParametriCodProcessi.aspx');</script>")
                End If
            Case "motiviProcesso", "motiviProcesso1"
                If Session.Item("MOD_MOTIVI_DECISIONI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('GestioneMotiviProcessDecis.aspx');</script>")
                End If
            Case "ElencoFileBanca"
                Response.Write("<script>parent.main.location.replace('ElencoFileBanca.aspx');</script>")
            Case "ElencoAnnulli"
                Response.Write("<script>parent.main.location.replace('ElencoAnnulli.aspx');</script>")
            Case "Strutturata"
                Response.Write("<script>parent.main.location.replace('RicercaStrutturata.aspx');</script>")
            Case "InScadenza"
                Response.Write("<script>parent.main.location.replace('RicercaScadenza.aspx');</script>")
            Case "RecuperiForzosi"
                Response.Write("<script>parent.main.location.replace('RicercaSfrEsec.aspx');</script>")
            Case "ElencoInScadenza"
                Response.Write("<script>parent.main.location.replace('Scadenza/RicercaElencoScadenza.aspx');</script>")
            Case "ElencoProrogati"
                Response.Write("<script>parent.main.location.replace('Prorogati/RicercaElencoProrogati.aspx');</script>")
            Case "Forze"
                If Session.Item("CONT_INSERIMENTO") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('NuovoContratto.aspx?TIPO=10');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "Virtuale"
                If Session.Item("CONT_INSERIMENTO_V") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('Virtuale.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If

            Case "Num.Bolletta"
                ' If Session.Item("CONT_LETTURA") = "0" Then
                Response.Write("<script>parent.main.location.replace('../Contabilita/RicBolletta.aspx');</script>")
                'Else
                'Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                'End If

            Case "Inquilini"
                'If Session.Item("CONT_LETTURA") = "0" Then
                Response.Write("<script>parent.main.location.replace('../Contabilita/RicUtente.aspx');</script>")
                'Else
                'Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")

                'End If

            Case "PG_EXTRA_MAV"
                If Session.Item("MOD_CONT_P_EXTRA") = "1" Then
                    Response.Write("<script>parent.main.location.replace('Pagamenti/RicercaPagManuale.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If

            Case "PG_MANUALE_RUOLO"
                If Session.Item("MOD_PAG_RUOLI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('Pagamenti/RicercaPagManuale.aspx?T=R');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If

            Case "PG_MANUALE_INGIUNZ"
                If Session.Item("MOD_PAG_INGIUNZ") = "1" Then
                    Response.Write("<script>parent.main.location.replace('Pagamenti/RicercaPagManuale.aspx?T=I');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If



            Case "RptPgExtra"
                If Session.Item("MOD_CONT_P_EXTRA") = "1" Then
                    Response.Write("<script>parent.main.location.replace('Pagamenti/RicercaRptPgExtraMav.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
                'Response.Write("<script>alert('Funzione in Aggiornamento!');</script>")
            Case "RptPgExtraRuoli"
                If Session.Item("MOD_REPORT_RUOLI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('Pagamenti/RicercaRptPgExtraMav.aspx?T=R');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "RptBollRuoli"
                If Session.Item("MOD_REPORT_RUOLI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('Pagamenti/RicercaBolletteRuolo.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If


            Case "RptPgExtraIng"
                If Session.Item("MOD_REPORT_INGIUNZ") = "1" Then
                    Response.Write("<script>parent.main.location.replace('Pagamenti/RicercaRptPgExtraMav.aspx?T=I');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
            Case "RptBollIng"
                If Session.Item("MOD_REPORT_INGIUNZ") = "1" Then
                    Response.Write("<script>parent.main.location.replace('Pagamenti/RicercaBolletteIng.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If

            Case "RptGenerale"
                'If Session.Item("MOD_CONT_P_EXTRA") = "1" Then
                '    Response.Write("<script>parent.main.location.replace('Pagamenti/RicercaRptPgExtraMav.aspx?CALL=GEN');</script>")
                'Else
                '    Response.Write("<script>alert('Utente non abilitato!');</script>")
                'End If
                Response.Write("<script>alert('Funzione in Aggiornamento!');</script>")


            Case "RatEmesse"
                Response.Write("<script>parent.main.location.replace('pagina_home.aspx');window.open('../RATEIZZAZIONE/RateizzEmesse.aspx','Nuova','');</script>")

            Case "DettPagEffett"
                Dim s As String = "var chiedicsv;" _
                    & "chiedicsv = window.confirm('Attenzione...La ricerca effettuata è impossibile da visualizzare. Esportare in un file .csv?');" _
                    & "if (chiedicsv == true) {" _
                    & "window.open('Pagamenti/RptPgExtraMavCSV.aspx','RptPgExtraMavCSV','');" _
                    & "}"
                Response.Write("<script>" & s & "</script>")

            Case "VSA"
                Response.Write("<script>parent.main.location.replace('RicercaContratti_VSA.aspx');</script>")
            Case "RptApp"
                Response.Write("<script>window.open('RptAppuntamenti.aspx' , 'ModuloRappSloggio', 'width=1200,height=800','menubar=no, scrollbars=yes');</script>")
            Case "agg_nucleo"
                Response.Write("<script>parent.main.location.replace('AggiornamentoNucleo.aspx');</script>")

                '14/12/2012 SPOSTAMENTO DA PARTITA GESTIONALE A CONTABILE
            Case "SpostaGestionali"

                'If Session.Item("ID_CAF") = 2 And Session.Item("RESPONSABILE") = 1 Then
                If Session.Item("MOD_ELAB_MASS_GEST") = 1 Then
                    Response.Write("<script>parent.main.location.replace('RicercaGestionali.aspx');</script>")
                Else
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                End If
                'Else
                'Response.Write("<script>alert('Utente non abilitato!');</script>")
                'End If
            Case "elenco_elaboraz"
                Response.Write("<script>parent.main.location.replace('ElencoElaborazMassive.aspx');</script>")



            Case "MotiviStorno"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('InserimMotiviStorno.aspx');</script>")
                End If

            Case "doc_gest"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('TipologieGestionale.aspx');</script>")
                End If
            Case "tip_boll"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('TipologieBollette.aspx');</script>")
                End If

                ''Case "AnteAler"
                ''    If Session.Item("CONT_IMPOSTE") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                ''        Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                ''        'Response.Write("<script>parent.main.location.replace('REG_PREGRESSE/SceltaImposteAnteAler.aspx');</script>")
                ''    Else
                ''        Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                ''    End If
                ''Case "RicevuteAnteAler"
                ''    If Session.Item("CONT_IMPOSTE") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                ''        Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                ''        'Response.Write("<script>parent.main.location.replace('REG_PREGRESSE/RicevuteImposteAnteAler.aspx');</script>")
                ''    Else
                ''        Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                ''    End If

                'nuovo case xml ante gestore anno 2014 17/06/2014
            Case "AnteAler"
                If Session.Item("CONT_IMPOSTE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('REG_PREGRESSE/SceltaImposteAnteAler.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "RicevuteAnteAler"
                If Session.Item("CONT_IMPOSTE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('REG_PREGRESSE/RicevuteImposteAnteAler.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "ElencoFileAnte"
                If Session.Item("CONT_IMPOSTE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('REG_PREGRESSE/ElencoImposteAnte.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "report_doc_gest"
                Response.Write("<script>window.open('Report/RicercaDocGestionali.aspx');</script>")
            Case "ReCa"
                If Session.Item("MOD_RECA_GEST") = "1" And Session.Item("PARAMETRI_CONTRATTI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('ParameReCaGest.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "RicercaAE"
                Response.Write("<script>parent.main.location.replace('DatiRegistrazioneS1.aspx');</script>")
            Case "PrioritàVoci"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Or Session.Item("CONT_LETTURA") = "1" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('ParametriPrioritaVoci.aspx');</script>")
                End If
            Case "DateBlocco"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Or Session.Item("CONT_LETTURA") = "1" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('DateBlocco.aspx');</script>")
                End If
            Case "spostaDate"
                If Session.Item("PARAMETRI_CONTRATTI") = "0" Or Session.Item("CONT_LETTURA") = "1" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('ParamDataScadBoll.aspx');</script>")
                End If
            Case "ReportRUAU"
                Response.Write("<script>parent.main.location.replace('CaricaRUAU.aspx?T=1');</script>")
            Case "ReportRUSaldi"
                Response.Write("<script>parent.main.location.replace('CaricaRUAU.aspx?T=2');</script>")
		
            Case "RptAzLegali"
                Response.Write("<script>window.open('RptAzLegali.aspx','','');</script>")
            Case "RptAzLegali2"
                Response.Write("<script>window.open('RptAzLegali_2.aspx','','');</script>")
            Case "AccessoUffGiudiziario"
                Response.Write("<script>window.open('RptAzLegali_3.aspx','','');</script>")
            Case "caricamVoci"
                Response.Write("<script>parent.main.location.replace('CaricamentoMassivoVoci.aspx');</script>")
            Case "caric_mass_ing"
                If Session.Item("MOD_MASS_INGIUNZIONI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CaricamMassivoIngiunzioni.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "rptVociSchema"
                Response.Write("<script>parent.main.location.replace('RptVociBollSchema.aspx');</script>")
            Case "ImportaNote" 'max 21/07/2015
                If Session.Item("MOD_CONT_NOTE") = "1" And Session.Item("CONT_LETTURA") = "0" Then
                    Response.Write("<script>parent.main.location.replace('InserimentoNote.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "estrazioni_ru"
                Response.Write("<script>window.open('Estrazioni_RU.aspx','','width=1100,height=900,resizable=yes');</script>")
            Case "elenco_estraz"
                Response.Write("<script>window.open('VisualizzaEstrazioni_RU.aspx','','');</script>")
                'MAX 26/04/2016
            Case "Rest.Dep.Cauzioni"
                Response.Write("<script>parent.main.location.replace('RicercaRestDepositi.aspx');</script>")
            Case "DepRestit"
                Response.Write("<script>parent.main.location.replace('RicercaDepositiINRest.aspx');</script>")
            Case "DepCauzionali"
                Response.Write("<script>parent.main.location.replace('RicercaDepCauzionali.aspx');</script>")
            Case "Situazione", "Situazione1"
                Response.Write("<script>parent.main.location.replace('RicercaRegistrAE.aspx');</script>")
                'Response.Write("<script>window.open('SituazioneAE.aspx');</script>")
			Case "RptRateizzStr"
                Response.Write("<script>window.open('RptRateizzStraordinaria1.aspx','','');</script>")
            Case "RptRateizzStr2"
                Response.Write("<script>window.open('RptRateizzStraordinaria2.aspx','','');</script>")
            Case "RptRateizzStr0"
                Response.Write("<script>window.open('RptRateizzStraordinaria0.aspx','','');</script>")
            Case "ConguaglioAU"
                Response.Write("<script>parent.main.location.replace('CaricamentoMassivoConguagli.aspx');</script>")
                'MAX 30/04/2018
            Case "AdeguaISTATERP"
                If Session.Item("CONT_ISTAT") = "1" Then
                    Response.Write("<script>parent.main.location.replace('SceltaIstatERP.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "Visualizzaistaterp"
                Response.Write("<script>parent.main.location.replace('VisualizzaIstatERP.aspx');</script>")
            Case "Spalmatore"
                Response.Write("<script>window.open('Spalmatore/SpalmatoreHome.aspx','Spalm','width=1100,height=900,resizable=yes');</script>")
            Case "EliminaVoci"
                Response.Write("<script>parent.main.location.replace('CancellazioneMassivaSchema.aspx');</script>")
        End Select

        T1.SelectedNode.Selected = False
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
