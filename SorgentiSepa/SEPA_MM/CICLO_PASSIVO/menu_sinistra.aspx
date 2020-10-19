<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_sinistra.aspx.vb" Inherits="CENSIMENTO_menu_sinistra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>menu</title>
    <script src="../funzioni.js" type="text/javascript"></script>
    <script type="text/javascript">

        function CenterPage(pageURL, title, w, h) {
            var left = ((screen.width / 2) - (w / 2)) - 15;
            var top = ((screen.height / 2) - (h / 2)) - 15;
            var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        };
    </script>
</head>
<body background="../NuoveImm/comeblu_sx.jpg">
    <form id="Form1" method="post" runat="server">
        &nbsp; &nbsp;
    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
        Style="z-index: 104; left: 5px; position: absolute; top: 552px" Text="Label"
        Width="106px" ForeColor="#721C1F"></asp:Label>
        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
    <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Height="124px"
        Style="z-index: 110; left: 1px; position: absolute; top: 96px" Width="106px"
        ImageSet="Arrows" TabIndex="-1" onclick="Chiudi();">
        <LevelStyles>
            <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                ForeColor="#721C1F" HorizontalPadding="1px" />
            <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                HorizontalPadding="3px" />
            <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                HorizontalPadding="3px" />
            <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                HorizontalPadding="3px" />
        </LevelStyles>
        <Nodes>


            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Formalizzazione"
                Value="Formalizzazione">
                <asp:TreeNode Text="Nuovo" Value="NuovoBP" ToolTip="Inserimento Nuovo Piano"></asp:TreeNode>
                <asp:TreeNode Text="Ricerca" Value="RicercaBP" ToolTip="Ricerca Piano Finanziario"></asp:TreeNode>
                <asp:TreeNode Text="Sit. Operat." ToolTip="Associazione Piano Finanziario/Operatori"
                    Value="SitOperatori"></asp:TreeNode>
                <asp:TreeNode Text="Assegna Op." Value="AssegnaOp"></asp:TreeNode>
                <asp:TreeNode Text="Cambia V." ToolTip="Aggiunge o toglie le abilitazioni ad un singolo operatore sul piano finanziario in corso"
                    Value="CambiaV"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Compilazione" Value="CompilazioneBP" ToolTip="Inserimento Importi/Servizi/Lotti"></asp:TreeNode>
            <asp:TreeNode Text="Convalida Gestore" Value="ConvalidaAler" ToolTip="Convalida del Piano finanziario"></asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Capitoli" Value="Capitoli">
                <asp:TreeNode Text="Gestione" Value="GestioneCapitoli"></asp:TreeNode>
                <asp:TreeNode Text="Assegna" Value="AssegnaC" ToolTip="Assegna Capitoli di Spesa"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Convalida Com." Value="ConvalidaComune" ToolTip="Convalida Comune"></asp:TreeNode>
            <asp:TreeNode Text="Simula" Value="Simula"></asp:TreeNode>
            <asp:TreeNode Text="Voci Servizi" Value="Voci Servizi"></asp:TreeNode>
            <asp:TreeNode Text="Sit.Importi" ToolTip="Visualizza la situazione importi inseriti di un piano finanziario"
                Value="SitImporti"></asp:TreeNode>
            <asp:TreeNode Text="Variazioni" Value="Var" Expanded="False" SelectAction="Expand">
                <asp:TreeNode Text="Tra Voci" Value="Nuova"></asp:TreeNode>
                <asp:TreeNode Text="Tra Strutture" Value="VariazStrut"></asp:TreeNode>
                <asp:TreeNode Text="Situazione" Value="Situazione"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="Expand" Text="Assestamento" Value="Elenco">
                <asp:TreeNode Text="Nuovo" Value="NuovoAss"></asp:TreeNode>
                <asp:TreeNode Text="Elenco" Value="CompilaAss"></asp:TreeNode>
                <asp:TreeNode NavigateUrl="~/CICLO_PASSIVO/CicloPassivo/ASSESTAMENTO/GuidaAssestamento.pdf"
                    Target="_blank" Text="Guida" Value="ManAss"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Parametri" Value="Parametri" SelectAction="Expand" Expanded="False">
                <asp:TreeNode Text="Voci rimborso" Value="gest_rimborso"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode NavigateUrl="~/CICLO_PASSIVO/CicloPassivo/Plan/CICLO_PASSIVO.pdf" Target="_blank"
                Text="Guida" Value="Manuale"></asp:TreeNode>
            <asp:TreeNode Text="Duplicazione" ToolTip="Duplicazione" Value="Duplicazione"></asp:TreeNode>


            <%--<asp:TreeNode Text="Anag.Fornitori" Value="fornitori" Expanded="False" SelectAction="Expand"
                ToolTip="ANAGRAFE FORNITORI">
                <asp:TreeNode Text="Inserimento" Value="InserimentoFornitori"></asp:TreeNode>
                <asp:TreeNode Text="Ricerca" Value="Ricerca"></asp:TreeNode>
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Expanded="False" SelectAction="Expand" Text="Contratti" Value="APPALTI">
                <asp:TreeNode Text="Inserimento" Value="Inserimento Appalti"></asp:TreeNode>
                <asp:TreeNode Text="Ricerca" Value="Ricerca Appalti"></asp:TreeNode>
                <asp:TreeNode Text="Rit. Legge" Value="RitLegge"></asp:TreeNode>
                <asp:TreeNode Text="Sit.Contabile" ToolTip="Situazione Contabile Appalti" 
                    Value="PerAppalti"></asp:TreeNode>
                <asp:TreeNode NavigateUrl="~/CICLO_PASSIVO/CicloPassivo/APPALTI/GuidaAppalti.pdf"
                    Target="_blank" Text="Guida" Value="Manuale"></asp:TreeNode>
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Expanded="False" SelectAction="Expand" Text="Gestione Lotti" Value="GESTIONE LOTTI">
                <asp:TreeNode Text="Nuovo Lotto Ed." Value="Nuovo lotto E" ToolTip="Nuovo Lotto Edifici">
                </asp:TreeNode>
                <asp:TreeNode Text="Nuovo Lotto Im." Value="Nuovo lotto I" ToolTip="Nuovo Lotto Impianti">
                </asp:TreeNode>
                <asp:TreeNode Text="Ricerca" Value="Ricerca lotto"></asp:TreeNode>
                <asp:TreeNode NavigateUrl="~/CICLO_PASSIVO/CicloPassivo/LOTTI/Report/GuidaLOTTI.pdf"
                    Target="_blank" Text="Guida" ToolTip="Guida Operativa" Value="Manuale"></asp:TreeNode>
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Text="Manutenz. e Serv." Value="Manutenzioni" Expanded="False" SelectAction="Expand"
                ToolTip="Manutenzioni e Servizi">
                <asp:TreeNode Text="Importa ODL" ToolTip="Importa ODL" Value="ImportaODL"> </asp:TreeNode>
                <asp:TreeNode Text="Inserimento" ToolTip="Inserimento Ordine di Manutenzione" SelectAction="Expand"
                    Value="InserimentoM_0" Expanded="False">
                    <asp:TreeNode Text="Edifici" ToolTip="Lotto Edifici " Value="InserimentoM_0_Edifici">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Impianti" ToolTip="Lotto Impianti" Value="InserimentoM_0_Impianti">
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Ins. Fuori Lotto" ToolTip="Inserimento Ordine di Manutenzione Fuori Lotto"
                    SelectAction="Expand" Value="InserimentoM_1" Expanded="False">
                    <asp:TreeNode Text="Edifici" ToolTip="Edifici e/o Impianti" Value="InserimentoM_1_Edifici">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Impianti" ToolTip="Solo Impianti" Value="InserimentoM_1_Impianti">
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Ins. Gest. Sfitti" ToolTip="Inserimento Ordine di Manutenzione per gli Alloggi Sfitti"
                    Value="InserimentoSfitti"></asp:TreeNode>
                <asp:TreeNode Text="Ricerca" Value="RicercaM" SelectAction="Expand" ToolTip="Ricerca Manutenzioni (Selettiva o Diretta)"
                    Expanded="False">
                    <asp:TreeNode Text="Selettiva" Value="RicercaMS"></asp:TreeNode>
                    <asp:TreeNode Text="Diretta" Value="RicercaMD"></asp:TreeNode>
                    <asp:TreeNode Text="Alloggi Sfitti" Value="RicercaSfitti"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Consuntivazione" ToolTip="Ricerca le manutenzioni da consuntivare"
                    SelectAction="Expand" Value="Consuntivazione" Expanded="False">
                    <asp:TreeNode Text="Selettiva" ToolTip="Ricerca Selettiva" Value="Consuntivazione">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Diretta" ToolTip="Ricerca Diretta" Value="ConsuntivazioneD">
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="STR" ToolTip="Estrazioni STR" Value="Estrazioni STR" 
                    SelectAction="Expand">
                    <asp:TreeNode Text="Export STR" ToolTip="Export per STR Vision" Value="STR">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Patrimonio" ToolTip="Estrazione del patrimonio" 
                        Value="Patrimonio"></asp:TreeNode>
                    <asp:TreeNode Text="Import" ToolTip="Import da STR" Value="ImportSTR">
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode SelectAction="Expand" Text="SAL" ToolTip="Stampa, Ristampa e Annullamento Pagamenti"
                    Value="SAL" Expanded="False">
                    <asp:TreeNode Text="Nuovo" ToolTip="Crea un Nuovo SAL" Value="NuovoSAL"></asp:TreeNode>
                    <asp:TreeNode Text="Ricerca" ToolTip="Ristampa SAL, Annulla SAL o Cambia la Firma al SAL"
                        Value="RicercaSAL"></asp:TreeNode>
                    <asp:TreeNode Text="Stampa Pag." ToolTip="Stampa i pagamenti" Value="StampaPagamenti">
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Segnalazione" Value="Segnalazione" SelectAction="Expand" Expanded="False">
                    <asp:TreeNode Text="Ricerca" ToolTip="Ricerca segnalazioni" 
                        Value="RicercaSegnalazioni"></asp:TreeNode>
                    <asp:TreeNode Text="Nuove" Value="SegnalazioniNuove"></asp:TreeNode>
                    <asp:TreeNode Text="In Carico" ToolTip="Prese in Carico" Value="SegnalazioniCarico">
                    </asp:TreeNode>
                    <asp:TreeNode Text="In Verifica" Value="SegnalazioniVerifica"></asp:TreeNode>
                    <asp:TreeNode Text="In Ordine" ToolTip="Emesso in Ordine" Value="SegnalazioniOrdine">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Chius. mass." ToolTip="Chiusura massiva segnalazioni" 
                        Value="ChiusuraMassiva"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Importa ODL" ToolTip="Importa ODL" Value="ImportaODL">
                </asp:TreeNode>
                <asp:TreeNode NavigateUrl="~/CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Report/GuidaManutenzioni.pdf"
                    Target="_blank" Text="Guida" Value="Manuale" ToolTip="Guida Operativa"></asp:TreeNode>
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Expanded="False" SelectAction="Expand" Text="Ordini e Pagamenti" Value="Ordini e Pagamenti">
                <asp:TreeNode Text="Inserimento" ToolTip="Inserimento Pagamento" Value="Inserimento Pagamenti">
                </asp:TreeNode>
                <asp:TreeNode Text="Ricerca" ToolTip="Ricerca Pagamenti (Selettiva o Diretta)" SelectAction="Expand"
                    Value="Ricerca">
                    <asp:TreeNode Text="Selettiva" Value="RicercaPS"></asp:TreeNode>
                    <asp:TreeNode Text="Diretta" Value="RicercaPD"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode NavigateUrl="~/CICLO_PASSIVO/CicloPassivo/PAGAMENTI/Report/GuidaODL.pdf"
                    Target="_blank" Text="Guida" Value="Manuale" ToolTip="Guida Operativa"></asp:TreeNode>
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Expanded="False" SelectAction="Expand" Text="Pagam. a Canone" Value="Pagamenti a Canone">
                <asp:TreeNode Text="Da Approvare" ToolTip="Ricerca Pagamenti a Canone da Approvare"
                    Value="RicercaPagCanoneSelettiva"></asp:TreeNode>
                <asp:TreeNode Text="Approvati" ToolTip="Ricerca Pagamenti a Canone Approvati per Stampare il SAL e/o il PAGAMENTO"
                    Value="RicercaPagCanoneApprovati"></asp:TreeNode>
                <asp:TreeNode Text="Emesso SAL" ToolTip="Ricerca Pagamenti a Canone con SAL emesso per stampare il PAGAMENTO"
                    Value="StampaPagCanone"></asp:TreeNode>
                <asp:TreeNode NavigateUrl="~/CICLO_PASSIVO/CicloPassivo/PAGAMENTI_CANONE/Report/GuidaPagamentiCanone.pdf"
                    Target="_blank" Text="Guida" Value="Manuale" ToolTip="Guida Operativa"></asp:TreeNode>
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Expanded="False" SelectAction="Expand" Text="Utenze" 
                Value="MenuUtenze">
                <asp:TreeNode Text="Caric. File" Value="CarFatA2A"></asp:TreeNode>
                <asp:TreeNode Text="Ric. Fatture" Value="RicFatture"></asp:TreeNode>
                <asp:TreeNode Text="Ric. CDP Emessi" Value="RicPagamento"></asp:TreeNode>
                <asp:TreeNode Text="Fatt. con CDP" Value="FatCdp"></asp:TreeNode>
                <asp:TreeNode Text="Elenco POD" Value="POD"></asp:TreeNode>
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Expanded="False" SelectAction="Expand" Text="Custodi" 
                Value="MenuCustodi">
                <asp:TreeNode Text="Caric. File" Value="CaricCustodi"></asp:TreeNode>
                <asp:TreeNode Text="Ricerca" Value="RicercaCust"></asp:TreeNode>
                <asp:TreeNode Text="Ric. CDP Emessi" Value="RicCdpCust"></asp:TreeNode>
                <asp:TreeNode Text="Custodi con CDP" Value="CustCdp"></asp:TreeNode>
                <asp:TreeNode Text="Anagrafica" Value="AnCustodi"></asp:TreeNode>
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Expanded="False" SelectAction="Expand" Text="Multe" 
                ToolTip="Multe" Value="Multe">
                <asp:TreeNode Text="Caric. File" ToolTip="Carica file delle multe" 
                    Value="CaricFileMult"></asp:TreeNode>
                <asp:TreeNode Text="Ricerca" Value="RicMulte"></asp:TreeNode>
                <asp:TreeNode Text="Ric. CDP Emessi" Value="RicMultCdp"></asp:TreeNode>
                <asp:TreeNode Text="Multe con CDP" Value="MultCDP"></asp:TreeNode>
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Expanded="False" SelectAction="Expand" Text="Gestione Non Patrimoniale" Value="RRS"
                ToolTip="Gestione Non Patrimoniale">
                <asp:TreeNode Expanded="False" Text="Inserimento" Value="InserimentoRRS"></asp:TreeNode>
                <asp:TreeNode Expanded="False" SelectAction="Expand" Text="Ricerca" ToolTip="Ricerca Manutenzioni (Selettiva o Diretta)"
                    Value="RicercaRRS">
                    <asp:TreeNode Text="Selettiva" Value="RicercaRRS_S"></asp:TreeNode>
                    <asp:TreeNode Text="Diretta" Value="RicercaRRS_D"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Expanded="False" SelectAction="Expand" Text="Consuntivazione" ToolTip="Ricerca Ordini Non Patrimoniali da consuntivare"
                    Value="ConsuntivazioneRRS">
                    <asp:TreeNode Text="Selettiva" ToolTip="Ricerca Selettiva" Value="ConsuntivazioneRRS_S">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Diretta" ToolTip="Ricerca Diretta" Value="ConsuntivazioneRRS_D">
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Expanded="False" SelectAction="Expand" Text="SAL" ToolTip="Stampa, Ristampa e Annullamento Pagamenti"
                    Value="SAL_RRS">
                    <asp:TreeNode Text="Nuovo" ToolTip="Crea un Nuovo SAL" Value="NuovoSAL_RRS"></asp:TreeNode>
                    <asp:TreeNode Text="Ricerca" ToolTip="Ristampa SAL, Annulla SAL o Cambia la Firma al SAL"
                        Value="RicercaSAL_RRS"></asp:TreeNode>
                    <asp:TreeNode Text="Stampa Pag." ToolTip="Stampa i pagamenti" Value="StampaPagamentiRRS">
                    </asp:TreeNode>
                </asp:TreeNode>
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Text="Odl/Sal" ToolTip="Odl/Sal" Value="Odl/Sal"></asp:TreeNode>--%>
            <%--<asp:TreeNode Expanded="False" Text="Sit. Contabile" Value="SContabile" SelectAction="Expand">
                <asp:TreeNode Text="Generale" Value="Sit. Generale"></asp:TreeNode>
                <asp:TreeNode Text="Per Struttura" Value="Sit. Contabile"></asp:TreeNode>
                <asp:TreeNode Text="Pagamenti" Value="Sit_pag"></asp:TreeNode>
                <asp:TreeNode Text="ODL" Value="ODL"></asp:TreeNode>
                <asp:TreeNode Text="Servizi" Value="RicercaPerServizi"></asp:TreeNode>
                <asp:TreeNode Text="Contabilità" ToolTip="Contabilità" Value="ContabilitaNew">
                </asp:TreeNode>
                <asp:TreeNode Text="Contabilità Dett." ToolTip="Contabilità Dett." 
                    Value="ContabilitaNewDett"></asp:TreeNode>
                <asp:TreeNode Text="Per Esercizio" ToolTip="Per Esercizio Finanziario" 
                    Value="PerEsercizio"></asp:TreeNode>
                <asp:TreeNode Text="Sintesi" ToolTip="Sintesi" Value="Sintesi"></asp:TreeNode>
                <asp:TreeNode Text="Estrazione Pag." ToolTip="Estrazione Pagamenti" Value="EstrazionePag"></asp:TreeNode>
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Expanded="False" SelectAction="Expand" Text="Residui" Value="Residui">
                <asp:TreeNode Text="Uscite C.C." Value="ExitCC"></asp:TreeNode>
                <asp:TreeNode Text="Uscite Correnti" Value="ExitCorr"></asp:TreeNode>
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Mandati Pag." Value="MandatiPag.">
                <asp:TreeNode Text="Mandati Pag." Value="MandPag"></asp:TreeNode>
                <asp:TreeNode Text="Rit. Acconto" Value="RitAcconto"></asp:TreeNode>
                <asp:TreeNode Text="Rit. Legge" Value="RitLeggeMP"></asp:TreeNode>
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Text="Upload Firma" ToolTip="Upload Firma" Value="UploadFirma">
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Text="Spese Reversibili" Value="SpeseReversibili"></asp:TreeNode>--%>
            <%--<asp:TreeNode Text="Report Pagamenti" ToolTip="Report Pagamenti" Value="EstrazionePagamenti"></asp:TreeNode>--%>
            <%--<asp:TreeNode Text="Parametri" Value="Parametri" Expanded="False" 
                SelectAction="Expand">
                <asp:TreeNode Text="Mod. Pagamento" Value="ModPag"></asp:TreeNode>
                <asp:TreeNode Text="CDP Tracciati" Value="FatUtenze"></asp:TreeNode>
                <asp:TreeNode Text="Gest.Crediti" Value="GestCrediti"></asp:TreeNode>
            </asp:TreeNode>--%>
            <%--<asp:TreeNode Text="B.Manager" Value="BManager"></asp:TreeNode>--%>
            <%--<asp:TreeNode Text="Ciclo Passivo" Value="Ciclo Passivo"></asp:TreeNode>--%>
        </Nodes>
        <NodeStyle BorderStyle="None" Font-Names="arial" Font-Size="8pt" ForeColor="Black"
            HorizontalPadding="1px" NodeSpacing="0px" VerticalPadding="0px" />
        <ParentNodeStyle Font-Bold="False" />
        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" Font-Names="Arial" Font-Size="8pt" />
        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
            VerticalPadding="0px" />
    </asp:TreeView>
        <br />
        <script type="text/javascript">
            function Chiudi() {

                //var index = event.srcElement.clickedNodeIndex;
                //var node = document.getElementById('T1').getTreeNode(index); 
                //alert(node.getAttribute("Text"));
            }
        </script>
        <p>
            &nbsp;
        </p>
        <p>
            &nbsp;
        </p>
        <p>
            &nbsp;
        </p>
    </form>
</body>
</html>
