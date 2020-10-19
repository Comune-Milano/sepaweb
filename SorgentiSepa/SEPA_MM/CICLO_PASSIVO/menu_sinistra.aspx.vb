

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
            Case "Odl/Sal"
                If Session.Item("BP_MS") = "1" Then
                    'Response.Write("<script>parent.main.location.replace('ElencoODLSAL.aspx');</script>")
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "AperturaPagina", "ApriModuloStandard('ElencoODLSAL.aspx', 'ODL SU SAL');", True)
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
			Case "Patrimonio"
                If Session.Item("FL_ESTRAZIONE_STR") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/EstrazionePatrimonio.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "STR"
                If Session.Item("FL_ESTRAZIONE_STR") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaManutenzioniSTR.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "ImportSTR"
                If Session.Item("FL_CONSUNTIVAZIONE_STR") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/ImportSTR.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
                'Case "IntegrazioneSTR"
                '    If Session.Item("FL_STR") <> "1" Then
                '        ScriptManager.RegisterStartupScript(Page, GetType(Page), "AperturaPagina", "ApriModuloStandard('CicloPassivo/MANUTENZIONI/Estrazioni.aspx', 'INTEGRAZIONE SEP@WEB - STR VISION');", True)
                '    Else
                '        Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                '    End If

            Case "NuovoBP"
                If Session.Item("BP_FORMALIZZAZIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/Plan/Prospetto.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If

            Case "RicercaBP"
                If Session.Item("BP_FORMALIZZAZIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/Plan/RicercaPF.aspx?C=0');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If

            Case "AssegnaOp"
                'Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")

                If Session.Item("BP_FORMALIZZAZIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/Plan/RicercaPF.aspx?C=9');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If

            Case "CambiaV"
                If Session.Item("BP_FORMALIZZAZIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/Plan/RicercaPF.aspx?C=11');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If

            Case "CompilazioneBP"
                If Session.Item("BP_COMPILAZIONE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/Plan/RicercaPF.aspx?C=1');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If

            Case "ConvalidaAler"
                If Session.Item("BP_CONV_ALER") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/Plan/RicercaPF.aspx?C=2');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If

            Case "AssegnaC"
                If Session.Item("BP_CAPITOLI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/Plan/RicercaPF.aspx?C=3');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If

            Case "GestioneCapitoli"
                If Session.Item("BP_CAPITOLI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/Plan/GestioneCapitoli.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If

            Case "ConvalidaComune"
                If Session.Item("BP_CONV_COMUNE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/Plan/RicercaPF.aspx?C=4');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "SitOperatori"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/Plan/RicercaPF.aspx?C=5');</script>")

            Case "gest_rimborso"
                If Session.Item("FL_PARAM_CICLO_PASSIVO") = "0" Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>window.open('ParametriGestCreditoNew.aspx','','width=1100,height=900,resizable=yes');</script>")
                End If
            Case "Simula"
                Response.Write("<script>alert('Non disponibile. Tabelle Millesimali non fornite!');</script>")
                'Response.Write("<script>parent.main.location.replace('CicloPassivo/Plan/Simula.aspx?IDPF=25');</script>")

            Case "InserimentoFornitori"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/APPALTI/Fornitori.aspx?ID=-1');</script>")

            Case "Ricerca"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/APPALTI/RicercaFornitore.aspx');</script>")


            Case "Nuovo lotto E"
                Session.Add("ID", 0)
                Response.Write("<script>parent.main.location.replace('CicloPassivo/LOTTI/NuovoLotto.aspx?T=E');</script>")

            Case "Nuovo lotto I"
                Session.Add("ID", 0)
                Response.Write("<script>parent.main.location.replace('CicloPassivo/LOTTI/NuovoLotto.aspx?T=I');</script>")

            Case "Ricerca lotto"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/LOTTI/RicercaLotti.aspx');</script>")

                'Case "Scambio"
                '    Response.Write("<script>parent.main.location.replace('CicloPassivo/LOTTI/RicercaLottiScambio.aspx');</script>")
                '    'Response.Write("<script>alert('Non disponibile!');</script>")

            Case "Inserimento Appalti"
                Session.Add("IDA", 0)
                Response.Write("<script>parent.main.location.replace('CicloPassivo/APPALTI/SceltaLotto.aspx');</script>")

            Case "Ricerca Appalti"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/APPALTI/RicercaAppalti.aspx');</script>")

                'EPIFANI
                'MANUTENAZIONI
            Case "InserimentoM_0_Edifici"     'INSERIMENTO MANUTENAZIONI (EDIFICI e/o IMPIANTI)
                Session.Add("ID", 0)

                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    'Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/Manutenzioni.aspx?TIPO=0');</script>")
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaManutenzioniINS.aspx?TIPOR=0');</script>")
                End If


            Case "InserimentoM_0_Impianti"     'INSERIMENTO MANUTENAZIONI (SOLO EDIFICI)
                Session.Add("ID", 0)

                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaManutenzioniINS.aspx?TIPOR=1');</script>")
                End If

            Case "InserimentoM_1_Edifici"     'INSERIMENTO MANUTENAZIONI FUORI LOTTO (EDIFICI e/o IMPIANTI)
                Session.Add("ID", 0)

                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaManutenzioniINS.aspx?TIPOR=2');</script>")
                End If

            Case "InserimentoM_1_Impianti"     'INSERIMENTO MANUTENAZIONI FUORI LOTTO (SOLO IMPIANTI)
                Session.Add("ID", 0)

                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaManutenzioniINS.aspx?TIPOR=3');</script>")
                End If


            Case "InserimentoSfitti"

                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaSfitti.aspx?');</script>")
                End If


            Case "RicercaMS"         'RICERCA MANUTENZIONI SELETTIVA
                Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaManutenzioni.aspx');</script>")

            Case "RicercaMD"         'RICERCA MANUTENZIONI DIRETTA
                Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaManutenzioniD.aspx');</script>")

            Case "RicercaSfitti"         'RICERCA MANUTENZIONI ALLOGGI SFITTI
                Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaManutenzioniSfitti.aspx');</script>")


            Case "Consuntivazione"  'RICERCA MANUTENZIONI DA CONSUNTIVARE (SELETTIVA)

                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaConsuntivi.aspx');</script>")
                End If

            Case "ConsuntivazioneD"  'RICERCA MANUTENZIONI DA CONSUNTIVARE (DIRETTA)

                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaConsuntiviD.aspx');</script>")
                End If


            Case "NuovoSAL"        'RICERCA MANUTENZIONI CONSUNTIVATI DA emettere il SAL

                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaSAL.aspx');</script>")
                End If

            Case "RicercaSAL"        'RICERCA SAL DA RI-STAMPARE, ANNULLARE o CAMBIARE LA FIRMA
                Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaSAL_FIRMA.aspx');</script>")

            Case "StampaPagamenti"  'RICERCA i Pagamenti da Stampare
                Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaPagamenti.aspx');</script>")

                'Case "AnnullaPAG"        'RICERCA PAGAMENTI DA ANNULLARE
                '    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RisultatiPagamentiAnnulla.aspx');</script>")


                'SEGNALAZIONI
            Case "SegnalazioniNuove"
                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RisultatiSegnalazioni.aspx?ST=0&ORD=" & "PER" & "');</script>")
                End If
            Case "RicercaSegnalazioni"
                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaSegnalazioni.aspx');</script>")
                End If
            Case "SenzaODLemesso"
                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaSegnalazioniNOEmesso.aspx');</script>")
                End If
            Case "ChiusuraMassiva"
                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/ChiusuraSegnalazioni.aspx');</script>")
                End If
            Case "SegnalazioniCarico"
                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RisultatiSegnalazioni.aspx?ST=1&ORD=" & "PER" & "');</script>")
                End If

            Case "SegnalazioniVerifica"
                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RisultatiSegnalazioni.aspx?ST=3&ORD=" & "PER" & "');</script>")
                End If

            Case "SegnalazioniOrdine"
                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RisultatiSegnalazioni.aspx?ST=4&ORD=" & "PER" & "');</script>")
                End If
                '*******************


                'ORDINI E PAGAMENTI
            Case "Inserimento Pagamenti"
                Session.Add("IDA", 0)

                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/SceltaVoce.aspx');</script>")
                End If

            Case "RicercaPS"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/RicercaPagamenti.aspx');</script>")

                'PUCCIA 06/05/2015 FATTURE UTENZE
            Case "RicFatture"
                Response.Write("<script>window.open('CicloPassivo/PAGAMENTI/FattureUtenze.aspx');</script>")
            Case "RicPagamento"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/RicercaPagamentiUtenza.aspx?TIPO=U');</script>")
            Case "FatCdp"
                Response.Write("<script>window.open('CicloPassivo/PAGAMENTI/FattureCDP.aspx');</script>")
            Case "POD"
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "AperturaPagina", "ApriModuloStandard('CicloPassivo/PAGAMENTI/GestionePod.aspx', 'GESTIONE POD');", True)
                'Response.Write("<script>ApriModuloStandard('CicloPassivo/PAGAMENTI/GestionePod.aspx', 'GESTIONE POD');</script>")
                'Response.Write("<script>window.open('CicloPassivo/PAGAMENTI/GestionePod.aspx');</script>")
            Case "CarFatA2A"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/FattureCaricamentoNew.aspx?TIPO=U');</script>")

            Case "CaricFileMult"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/FattureCaricamentoNew.aspx?TIPO=M');</script>")
            Case "RicMulte"
                Response.Write("<script>window.open('CicloPassivo/PAGAMENTI/MulteCaricate.aspx');</script>")
            Case "RicMultCdp"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/RicercaPagamentiUtenza.aspx?TIPO=M');</script>")
            Case "MultCDP"
                Response.Write("<script>window.open('CicloPassivo/PAGAMENTI/MulteCaricate.aspx?PAGATE=1');</script>")
            Case "CambioIVA"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/CambioIVAodl.aspx');</script>")
                'puccia CUSTODI 
            Case "CaricCustodi"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/FattureCaricamentoNew.aspx?TIPO=C');</script>")

            Case "RicercaCust"
                Response.Write("<script>window.open('CicloPassivo/PAGAMENTI/CustodiPagamenti.aspx?PAGATE=0');</script>")
            Case "RicCdpCust"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/RicercaPagamentiUtenza.aspx?TIPO=C');</script>")
            Case "CustCdp"
                Response.Write("<script>window.open('CicloPassivo/PAGAMENTI/CustodiPagamenti.aspx?PAGATE=1');</script>")
            Case "RicercaPD"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/RicercaPagamentiD.aspx');</script>")
            Case "AnCustodi"
                'Response.Write("<script>window.open('GestCustodi.aspx','GestCustodi','height=800,witdh=800,scrollbars=no');</script>")
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "AperturaPagina", "ApriModuloStandard('GestCustodi.aspx', 'GESTIONE CUSTODI');", True)

                '**********************
            Case "CarFatA2A"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/FattureCaricamentoNew.aspx?TIPO=U');</script>")

                'PAGAMENTI A CANONE
            Case "RicercaPagCanoneSelettiva"    'RICERCA SELETTIVA DA APPROVARE
                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI_CANONE/RicercaPagamenti.aspx?TIPO=DA_APPROVARE');</script>")
                End If

            Case "RicercaPagCanoneApprovati"         'RICERCA x DATE
                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI_CANONE/RicercaPagamentiS.aspx?TIPO=APPROVATI');</script>")
                ''APPROVATI,APPROVATI_SCADENZA,DA_STAMPARE_PAG

            Case "StampaPagCanone"          ' STAMPA PAGAMENTO
                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI_CANONE/RicercaPagamentiS.aspx?TIPO=DA_STAMPARE_PAG');</script>")

                '******************************


            Case "Voci Servizi"
                If Session.Item("BP_VOCI_SERVIZI") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/Plan/VociServizio/SceltaServizio.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If


                'RRS
            Case "InserimentoRRS"    'RICERCA SELETTIVA

                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/RRS/RicercaRRS_INS.aspx?');</script>")
                End If

            Case "RicercaRRS_S"         'RICERCA MANUTENZIONI SELETTIVA
                Response.Write("<script>parent.main.location.replace('CicloPassivo/RRS/RicercaRRS.aspx');</script>")

            Case "RicercaRRS_D"         'RICERCA MANUTENZIONI DIRETTA
                Response.Write("<script>parent.main.location.replace('CicloPassivo/RRS/RicercaRRS_D.aspx');</script>")

                'Case "RicercaSfittiRRS"         'RICERCA MANUTENZIONI ALLOGGI SFITTI
                '    Response.Write("<script>parent.main.location.replace('CicloPassivo/RRS/RicercaRRS_Sfitti.aspx');</script>")


            Case "ConsuntivazioneRRS_S"  'RICERCA MANUTENZIONI DA CONSUNTIVARE (SELETTIVA)
                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/RRS/RicercaConsuntiviRRS_S.aspx');</script>")
                End If

            Case "ConsuntivazioneRRS_D"  'RICERCA MANUTENZIONI DA CONSUNTIVARE (DIRETTA)
                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/RRS/RicercaConsuntiviRRS_D.aspx');</script>")
                End If

            Case "NuovoSAL_RRS"        'RICERCA MANUTENZIONI CONSUNTIVATI DA emettere il SAL
                If IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Response.Write("<script>alert('Utente non abilitato oppure non ha la struttura assegnata!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/RRS/RicercaSAL_RRS.aspx');</script>")
                End If

            Case "RicercaSAL_RRS"        'RICERCA SAL DA RI-STAMPARE, ANNULLARE o CAMBIARE LA FIRMA
                Response.Write("<script>parent.main.location.replace('CicloPassivo/RRS/RicercaSAL_RRS_FIRMA.aspx');</script>")

            Case "StampaPagamentiRRS"  'RICERCA i Pagamenti da Stampare
                Response.Write("<script>parent.main.location.replace('CicloPassivo/RRS/RicercaPagamentiRRS.aspx');</script>")

                '****************************
            Case "SitImporti"
                If Session.Item("BP_GENERALE") = "1" Then
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/Plan/RicercaPF.aspx?C=12');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If

            Case "PerAppalti"
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) And (IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1") Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/SituazioneContabilePerAppalti.aspx');</script>")
                End If

            Case "PerEsercizio"
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) And (IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1") Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/SituazioneContabile.aspx');</script>")
                End If
            Case "Sintesi"
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) And (IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1") Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/SituazioneContabileSintesi.aspx');</script>")
                End If
            Case "EstrazionePag"
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) And (IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1") Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/EstrazioneCDP.aspx');</script>")
                End If
            Case "EstrazionePagamenti"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/EstrazionePagamenti.aspx');</script>")
            Case "Sit. Contabile"
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) And (IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1") Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/RicercaSitContabile.aspx');</script>")
                End If

            Case "MandPag"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/APPALTI/PagamentoCertificatiExcel.aspx');</script>")

                'Case "SitPagamenti"
                '    Response.Write("<script>parent.main.location.replace('CicloPassivo/APPALTI/SituazionePagamenti.aspx');</script>")

                'Case "Sit_pag"
                '    If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) And (IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1") Then
                '        Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                '    Else
                '        Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/RicercaSitPagam.aspx');</script>")
                '    End If
            Case "RitAcconto"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/APPALTI/PagamentoRitenuteExcel.aspx');</script>")
            Case "RitLeggeMP"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/APPALTI/PagamentoRitLeggeExcel.aspx');</script>")
            Case "Sit. Contabile"
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) And (IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1") Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/RicercaSitContabile.aspx');</script>")
                End If
            Case "Sit. Generale"
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/RicercaSitContabileGenerale.aspx');</script>")
                End If
            Case "ContabilitaNew"
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/Contabilita.aspx');</script>")
                End If
            Case "ContabilitaNewDett"
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/ContabilitaDett.aspx');</script>")
                End If
            Case "Sit_pag"
                '*/*/*/*/*/ MARCO-PEPPE MODIFY 11/11/2011
                'If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                '    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                'Else

                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/PagamentiPerStruttura.aspx');</script>")
                'End If
            Case "ODL"

                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) And (IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1") Then
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                Else

                    Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/ODLperStruttura.aspx');</script>")
                End If

            Case "RitLegge"

                Response.Write("<script>parent.main.location.replace('CicloPassivo/APPALTI/RitenuteLegge.aspx');</script>")
                '-*-*-*Update menù 01/08/2011

            Case "Nuova"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/VARIAZIONI/VariazioneFondi.aspx');</script>")
            Case "VariazStrut"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/VARIAZIONI/TrasferimentoFondi.aspx');</script>")

            Case "Situazione"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/VARIAZIONI/SituazioneVariazioni.aspx');</script>")
                '-*-*-*-*-*-*END Update menù 01/08/2011

            Case "NuovoAss"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/ASSESTAMENTO/NuovoAssestamento.aspx');</script>")
            Case "CompilaAss"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/ASSESTAMENTO/ScegliAssestamento.aspx');</script>")
            Case "RicercaPerServizi"
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                    Response.Write("<script>alert('Utente non abilitato!');</script>")
                Else
                    Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/RicercaPerServizi.aspx');</script>")
                End If

            Case "ExitCC"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/RicercaResidui.aspx');</script>")
            Case "ExitCorr"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/PAGAMENTI/RicercaCorrenti.aspx');</script>")
                'Case "ExitCC"
                '    Response.Write("<script>parent.main.location.replace('pagina_home.aspx');window.open('CicloPassivo/PAGAMENTI/StampaPFresidui.aspx?EF_R=1','_blank','resizable=yes,height=800,width=1000,top=0,left=0,scrollbars=yes');</script>")
                'Case "ExitCorr"
                '    Response.Write("<script>parent.main.location.replace('pagina_home.aspx');window.open('CicloPassivo/PAGAMENTI/StampaPFcorrenti.aspx?EF_R=1','_blank','resizable=yes,height=800,width=1000,top=0,left=0,scrollbars=yes');</script>")

            Case "SpeseReversibili"
                Response.Write("<script>window.open('../SPESE_REVERSIBILI/Default.aspx','_blank','resizable=1,statusbar=0,width=1200,height=800');</script>")
            Case "Duplicazione"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/Plan/ProceduraDuplicazionePF.aspx');</script>")
            Case "UploadFirma"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/Manutenzioni/UploadFirma.aspx');</script>")
            Case "ModPag"
                Response.Write("<script>parent.main.location.replace('GestioneModlitaPagamento.aspx');</script>")
            Case "FatUtenze"
                Response.Write("<script>parent.main.location.replace('GestioneFatUtenze.aspx');</script>")
            Case "BManager"
                If Session.Item("MOD_BUILDING_MANAGER") = "1" Then
                    Response.Write("<script>parent.main.location.replace('RisultatoBuildingManager.aspx');</script>")
                Else
                    Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                End If
            Case "ImportaODL"
                Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/ImportaODL.aspx');</script>")
                'Case "GestCrediti"
                '    If Session.Item("FL_PARAM_CICLO_PASSIVO") = "0" Then
                '        Response.Write("<script>alert('Non disponibile o Utente non abilitato!');</script>")
                '    Else
                '        Response.Write("<script>parent.main.location.replace('../Contratti/ParametriGestCredito.aspx');</script>")
                '    End If
        End Select
        T1.SelectedNode.Selected = False

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tn As TreeNode
        Dim IndiceMenu As Integer = 0


        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If

        If Not IsPostBack Then


            If Session.Item("ID_OPERATORE") <> 1755 Then
                par.RimuoviNodoMenu(T1, "ImportaODL")
                par.RimuoviNodoMenu(T1, "CambioIVA")
            End If
			If Session.Item("FL_UTENZE") <> "1" Then
                par.RimuoviNodoMenu(T1, "MenuCustodi")
                par.RimuoviNodoMenu(T1, "MenuUtenze")
                par.RimuoviNodoMenu(T1, "Multe")
            End If
            If Session.Item("FL_ESTRAZIONE_STR") <> "1" Then
                par.RimuoviNodoMenu(T1, "STR")
            End If
            If Session.Item("FL_CONSUNTIVAZIONE_STR") <> "1" Then
                par.RimuoviNodoMenu(T1, "ImportSTR")
            End If
            If Session.Item("FL_ESTRAZIONE_STR") <> "1" And Session.Item("FL_CONSUNTIVAZIONE_STR") <> "1" Then
                par.RimuoviNodoMenu(T1, "Estrazioni STR")
            End If
            'PIANI FINANZIARI (0)
            If Session.Item("BP_NUOVO") <> "1" Then
                tn = T1.FindNode("BusinessPlan/Formalizzazione/NuovoBP")
                If Not IsNothing(tn) Then
                    T1.Nodes(0).ChildNodes(0).ChildNodes.Remove(tn)
                End If
            End If

            If Session.Item("BP_FORMALIZZAZIONE") <> "1" Then
                tn = T1.FindNode("BusinessPlan/Formalizzazione")
                If Not IsNothing(tn) Then
                    T1.Nodes(0).ChildNodes.Remove(tn)
                End If
            End If

            If Session.Item("BP_COMPILAZIONE") <> "1" Then
                tn = T1.FindNode("BusinessPlan/CompilazioneBP")
                If Not IsNothing(tn) Then
                    T1.Nodes(0).ChildNodes.Remove(tn)
                End If
            End If


            If Session.Item("BP_CONV_ALER") <> "1" Then
                tn = T1.FindNode("BusinessPlan/ConvalidaAler")
                If Not IsNothing(tn) Then
                    T1.Nodes(0).ChildNodes.Remove(tn)
                End If
            End If


            If Session.Item("BP_CAPITOLI") <> "1" Then
                tn = T1.FindNode("BusinessPlan/Capitoli")
                If Not IsNothing(tn) Then
                    T1.Nodes(0).ChildNodes.Remove(tn)
                End If
            End If


            If Session.Item("BP_CONV_COMUNE") <> "1" Then
                tn = T1.FindNode("BusinessPlan/ConvalidaComune")
                If Not IsNothing(tn) Then
                    T1.Nodes(0).ChildNodes.Remove(tn)
                End If
            End If

            tn = T1.FindNode("BusinessPlan/Simula")
            If Not IsNothing(tn) Then
                T1.Nodes(0).ChildNodes.Remove(tn)
            End If


            If Session.Item("BP_VOCI_SERVIZI") <> "1" Then
                tn = T1.FindNode("BusinessPlan/Voci Servizi")
                If Not IsNothing(tn) Then
                    T1.Nodes(0).ChildNodes.Remove(tn)
                End If
            End If

            If Session.Item("BP_GENERALE") <> "1" Then
                tn = T1.FindNode("BusinessPlan/SitImporti")
                If Not IsNothing(tn) Then
                    T1.Nodes(0).ChildNodes.Remove(tn)
                End If
            End If

            If Session.Item("MOD_ASS_NUOVO") = 0 Then
                tn = T1.FindNode("BusinessPlan/Elenco")
                If Not IsNothing(tn) Then
                    tn.ChildNodes.Remove(T1.FindNode("BusinessPlan/Elenco/NuovoAss"))
                End If
            End If
            '***************gestione menù sinistra Assestamento
            If Session.Item("BP_GENERALE") <> 1 And Session.Item("MOD_ASS_CONV_COMU") <> 1 Then
                tn = T1.FindNode("BusinessPlan/Elenco")
                If Not IsNothing(tn) Then
                    T1.Nodes(0).ChildNodes.Remove(tn)
                End If
            End If

            '***************gestione menù sinistra Variazioni

            If Session.Item("BP_VARIAZIONI") <> 1 And Session.Item("BP_VARIAZIONI_SL") <> 1 Then
                tn = T1.FindNode("BusinessPlan/Var")
                If Not IsNothing(tn) Then
                    T1.Nodes(0).ChildNodes.Remove(tn)
                End If

                'Else
                '    If Session.Item("BP_VARIAZIONI_SL") = 1 Then
                '        tn = T1.FindNode("BusinessPlan/Var/Situazione")
                '        If Not IsNothing(tn) Then
                '            T1.Nodes(0).ChildNodes(T1.Nodes(0).ChildNodes.Count - 3).ChildNodes.Remove(tn)
                '        End If

                '    End If
            Else
                If Session.Item("BP_VARIAZIONI_SL") = 1 Then
                    tn = T1.FindNode("BusinessPlan/Var")
                    If Not IsNothing(tn) Then
                        tn.ChildNodes.Remove(T1.FindNode("BusinessPlan/Var/Nuova"))
                        tn.ChildNodes.Remove(T1.FindNode("BusinessPlan/Var/VariazStrut"))
                    End If
                End If
            End If
            If Session.Item("FL_PARAM_CICLO_PASSIVO") = 0 Then
                tn = T1.FindNode("Parametri")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
            End If


            'If Session.Item("BP_VARIAZIONI") <> 1 And Session.Item("BP_VARIAZIONI_SL") <> 1 Then
            '    tn = T1.FindNode("BusinessPlan/Var")
            '    If Not IsNothing(tn) Then
            '        T1.Nodes(0).ChildNodes.Remove(tn)
            '    End If
            'End If

            '***************************
            '***************************


            'MANDATI DI PAGAMENTO 06/07/2011
            If Session.Item("MOD_MAND_PAGAMENTO") = 0 Then
                tn = T1.FindNode("MandatiPag.")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
            End If

            '***************************

            IndiceMenu = 2  'FORNITORI (1)


            'CONTRATTI (2)
            If Session.Item("BP_CC") <> "1" Then
                tn = T1.FindNode("APPALTI")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
                tn = T1.FindNode("fornitori")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
                IndiceMenu = 1
            Else
                If Session.Item("BP_CC_L") = "1" Then
                    tn = T1.FindNode("APPALTI/Inserimento Appalti")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes.Remove(tn)
                    End If

                    tn = T1.FindNode("fornitori/Inserimento")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes.Remove(tn)
                    End If
                End If

                IndiceMenu = IndiceMenu + 1

            End If


            '***********************

            'LOTTI (3)
            If Session.Item("BP_LO") <> "1" Then
                tn = T1.FindNode("GESTIONE LOTTI")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
            Else
                If Session.Item("BP_LO_L") = "1" Then
                    tn = T1.FindNode("GESTIONE LOTTI/Nuovo lotto E")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes.Remove(tn)
                    End If

                    tn = T1.FindNode("GESTIONE LOTTI/Nuovo lotto I")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes.Remove(tn)
                    End If
                End If

                IndiceMenu = IndiceMenu + 1
            End If


            '************************************

            'MANUTENZIONI (4)
            If Session.Item("BP_MS") <> "1" Then
                tn = T1.FindNode("Manutenzioni")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
            Else
                If Session.Item("BP_MS_L") = "1" Then
                    tn = T1.FindNode("Manutenzioni/InserimentoM_0")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes.Remove(tn)
                    End If

                    tn = T1.FindNode("Manutenzioni/InserimentoM_1")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes.Remove(tn)
                    End If

                    tn = T1.FindNode("Manutenzioni/InserimentoSfitti")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes.Remove(tn)
                    End If

                    tn = T1.FindNode("Manutenzioni/Consuntivazione")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes.Remove(tn)
                    End If

                    tn = T1.FindNode("Manutenzioni/SAL/NuovoSAL")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes(1).ChildNodes.Remove(tn)
                    End If

                    tn = T1.FindNode("Manutenzioni/Segnalazione")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes.Remove(tn)
                    End If

                End If

                IndiceMenu = IndiceMenu + 1
            End If


            '*********************************

            'ORDINI E PAGAMENTI (5)
            If Session.Item("BP_OP") <> "1" Then
                tn = T1.FindNode("Ordini e Pagamenti")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
            Else
                If Session.Item("BP_OP_L") = "1" Then

                    tn = T1.FindNode("Ordini e Pagamenti/Inserimento Pagamenti")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes.Remove(tn)
                    End If
                End If

                IndiceMenu = IndiceMenu + 1
            End If


            '************************************

            'PAGAMENTI A CANONE (6)
            If Session.Item("BP_PC") <> "1" Then
                tn = T1.FindNode("Pagamenti a Canone")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
            Else

                If Session.Item("BP_PC_L") = "1" Then
                    tn = T1.FindNode("Pagamenti a Canone/StampaPagCanone")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes.Remove(tn)
                    End If

                End If

                IndiceMenu = IndiceMenu + 1
            End If

            '******************************************


            'MANUTENZIONI RRS (7) DA AGGIUNGERE NELLA GESTIONE OPERATORI
            If Session.Item("BP_MS") <> "1" Then
                tn = T1.FindNode("RRS")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
            Else
                If Session.Item("BP_RSS_L") = "1" Then
                    tn = T1.FindNode("RRS/InserimentoRRS")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes.Remove(tn)
                    End If


                    tn = T1.FindNode("RRS/ConsuntivazioneRRS")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes.Remove(tn)
                    End If

                    tn = T1.FindNode("RRS/SAL_RRS/NuovoSAL_RRS")
                    If Not IsNothing(tn) Then
                        T1.Nodes(IndiceMenu).ChildNodes(1).ChildNodes.Remove(tn)
                    End If

                End If

                IndiceMenu = IndiceMenu + 1
            End If

            'MANDATI DI PAGAMENTO 06/07/2011

            If Session.Item("MOD_MAND_PAGAMENTO") = 0 Then
                tn = T1.FindNode("Mandati Pag.")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If

            End If

            If Session.Item("BP_GENERALE") = 0 Then
                tn = T1.FindNode("SContabile")
                If Not IsNothing(tn) Then
                    tn.ChildNodes.Remove(T1.FindNode("SContabile/Sit. Generale"))

                    tn.ChildNodes.Remove(T1.FindNode("SContabile/RicercaPerServizi"))



                    'tn.ChildNodes.Remove(T1.FindNode("SContabile/ODL"))
                    'tn.ChildNodes.Remove(T1.FindNode("SContabile/Sit_pag"))
                End If
            End If


            If Session.Item("BP_RESIDUI") = 0 Then
                tn = T1.FindNode("Residui")
                If Not IsNothing(tn) Then
                    'tn.ChildNodes.Remove(T1.FindNode("Residui"))
                    'tn.ChildNodes.Remove(T1.FindNode("Residui/ExitCC"))
                    T1.Nodes.Remove(tn)


                    'tn.ChildNodes.Remove(T1.FindNode("SContabile/ODL"))
                    'tn.ChildNodes.Remove(T1.FindNode("SContabile/Sit_pag"))
                End If
            End If




            '***************15/11/2012 spese reversibili
            If Session.Item("FL_SPESE_REVERSIBILI") = 0 Then
                tn = T1.FindNode("SpeseReversibili")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                    'tn.ChildNodes.Remove(T1.FindNode("SpeseReversibili"))
                End If

            End If


            If Session.Item("FL_AUTORIZZAZIONE_ODL") = "0" Then
                tn = T1.FindNode("UploadFirma")
                If Not IsNothing(tn) Then
                    T1.Nodes.Remove(tn)
                End If
            End If


            tn = T1.FindNode("MandatiPag.")
            If Not IsNothing(tn) Then
                T1.Nodes.Remove(tn)
            End If

            Label3.Text = Format(Now(), "dd/MM/yyyy")

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
