<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_sinistra.aspx.vb" Inherits="ANAUT_menu_sinistra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript" src="Funzioni.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>menu</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
</head>
<body bgcolor="#eceff2" style="background-attachment: fixed; background-image: url(../NuoveImm/ContrattiSX.jpg);
    background-repeat: no-repeat">
    <form id="Form1" method="post" runat="server">
    <div style="margin-left: 40px">
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            Style="z-index: 100; left: 4px; position: absolute; top: 568px" Text="Label"
            Width="106px" ForeColor="#721C1F"></asp:Label>
    </div>
    &nbsp;&nbsp;&nbsp;
    <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px"
        Style="z-index: 104; left: 3px; position: absolute; top: 96px" Width="106px"
        ImageSet="Arrows" TabIndex="-1">
        <LevelStyles>
            <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                ForeColor="#721C1F" HorizontalPadding="1px" />
            <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                HorizontalPadding="3px" />
            <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                HorizontalPadding="3px" />
            <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                HorizontalPadding="3px" />
            <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                HorizontalPadding="3px" />
        </LevelStyles>
        <Nodes>
            <asp:TreeNode Text="Contratti" Value="Contratti" Expanded="False" SelectAction="SelectExpand">
                <asp:TreeNode Text="Inserimento" Value="1" SelectAction="SelectExpand" Expanded="False">
                    <asp:TreeNode Text="E.R.P." ToolTip="Assegnazioni effettuate da bando E.R.P." Value="ERP">
                    </asp:TreeNode>
                    <asp:TreeNode Text="E.R.P. (fuori Milano)" ToolTip="Assegnazioni fuori Milano" Value="erpFuoriMI">
                    </asp:TreeNode>
                    <asp:TreeNode Text="CAMBI" ToolTip="Assegnazioni effettuate da Bando Cambi" Value="CAMBI">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Usi Diversi" ToolTip="Assegnazione di Usi Diversi" Value="DIVERSI">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Cambi Cons." ToolTip="Assegnazione da Cambi Consensuali" Value="Mobilita">
                    </asp:TreeNode>
                    <asp:TreeNode Text="392/78" ToolTip="Contratti di tipo 392/78" Value="392"></asp:TreeNode>
                    <asp:TreeNode Text="431/98" ToolTip="Contratti di tipo 431/98" Value="431"></asp:TreeNode>
                    <asp:TreeNode Text="Abusivi" ToolTip="Rapporti Abusivi" Value="Abusivi"></asp:TreeNode>
                    <asp:TreeNode Text="Art.22 C.10 rr 1/04" ToolTip="Cambi in Emergenza ART.22 C.10 RR 1/2004"
                        Value="CambiEmergenza"></asp:TreeNode>
                    <asp:TreeNode Text="Forze dell'Ordine" ToolTip="Forze dell'Ordine" Value="Forze">
                    </asp:TreeNode>
                    <asp:TreeNode Text="C. Convenzionato" ToolTip="Canone Convenzionato" Value="Convenzionato">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Virtuale" ToolTip="Inserimento di un contratto virtuale" Value="Virtuale">
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Elimina Bozze" Value="Elimina"></asp:TreeNode>
                <asp:TreeNode Text="Annullo Dom. Gest. Loc." Value="AnnullaDom"></asp:TreeNode>
                <asp:TreeNode Text="Ricerca" Value="Ricerca" SelectAction="SelectExpand" Expanded="False">
                    <asp:TreeNode Text="Libera" Value="2"></asp:TreeNode>
                    <asp:TreeNode Text="Gest.Locat.Alloggi" Value="VSA"></asp:TreeNode>
                    <asp:TreeNode Text="In Scadenza" Value="InScadenza"></asp:TreeNode>
                    <asp:TreeNode Text="Recuperi Forzosi" Value="RecuperiForzosi"></asp:TreeNode>
                    <asp:TreeNode Text="El. Contr. In Scaden." Value="ElencoInScadenza"></asp:TreeNode>
                    <asp:TreeNode Text="El. Contr. Prorogati" Value="ElencoProrogati"></asp:TreeNode>
                    <asp:TreeNode Text="Dep.Cauz. Liquidati" ToolTip="Depositi Cauzionali Liquidati"
                        Value="Rest.Dep.Cauzioni"></asp:TreeNode>
                    <asp:TreeNode Text="Sit.Dep.Cauzionali" ToolTip="Situazione Depositi Cauzionali"
                        Value="DepCauzionali"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="ISTAT" Value="ISTAT33">
                    <asp:TreeNode Text="ERP" Value="ERP123" Expanded="False" SelectAction="SelectExpand"
                        ToolTip="RU di tipo ERP">
                        <asp:TreeNode Text="Adegua" Value="AdeguaISTATERP"></asp:TreeNode>
                        <asp:TreeNode Text="Visualizza" Value="Visualizzaistaterp"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="DIV. ERP" Value="DIV. ERP" Expanded="False" SelectAction="SelectExpand"
                        ToolTip="Ru di tipo diverso da ERP">
                        <asp:TreeNode Text="Adegua" Value="ADEGUAISTAT"></asp:TreeNode>
                        <asp:TreeNode Text="Visualizza" Value="VisualizzaISTAT"></asp:TreeNode>
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Interessi DPC" Value="Interessi">
                    <asp:TreeNode Text="Visualizza" Value="VisualizzaInteressi"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Trasmissione AE"
                    ToolTip="Registrazione Telematica dei contratti" Value="ss">
                    <asp:TreeNode Text="Prima Stipula" Value="Prima Stipula" Expanded="False" SelectAction="SelectExpand">
                        <asp:TreeNode Text="Crea XML Stipula" Value="Registrazione"></asp:TreeNode>
                        <asp:TreeNode Text="Elenco File" Value="ElencoRegistrazione" NavigateUrl="~/Contratti/ElencoRegistrazioni.aspx"
                            Target="_blank"></asp:TreeNode>
                        <asp:TreeNode Text="Ricevute" Value="Ricevute"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Adempim.Successivi" Value="Imposte" Expanded="False" SelectAction="SelectExpand"
                        ToolTip="Versamento Imposte telematiche">
                        <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Crea XML Succ."
                            Value="XMLSucc">
                            <asp:TreeNode Text="Rinn./Pror./Risol." Value="AdempSucc"></asp:TreeNode>
                            <asp:TreeNode Text="Cessioni" Value="cessioni"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Elenco File" Value="ElencoImposte"></asp:TreeNode>
                        <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Ricevute" Value="RicevuteIm">
                            <asp:TreeNode Text="Rinn./Pror./Risol." Value="RicevuteImposte"></asp:TreeNode>
                            <asp:TreeNode Text="Cessioni" Value="RicevuteCessioni"></asp:TreeNode>
                        </asp:TreeNode>
                        <%-- <asp:TreeNode Text="Ricerca Ric." Value="RicercaRic"></asp:TreeNode>
                        <asp:TreeNode Text="Ricerca Dati" Value="RicercaAE"></asp:TreeNode>--%>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Agg. data decorr.AE" Value="RimarcaturaRU"></asp:TreeNode>
                    <asp:TreeNode Text="Modifica file RLI" Value="modify_rli"></asp:TreeNode>
                    <asp:TreeNode Text="Situaz. Trasmiss. AE" Value="Situazione1"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Agg.Nucleo" Value="agg_nucleo"></asp:TreeNode>
                <asp:TreeNode Text="Importa Note" Value="ImportaNote"></asp:TreeNode>
<asp:TreeNode Text="Genera Frontespizio" Value="Frontespizio"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Bollettazione" Value="Bollettazione" Expanded="False" SelectAction="SelectExpand">
                <asp:TreeNode Text="Emissione" Value="Emissione" SelectAction="SelectExpand" Expanded="False">
                    <asp:TreeNode Text="Simulazione" Value="Simula"></asp:TreeNode>
                    <asp:TreeNode Text="Elenco Simulaz." Value="ElencoSimulazioni"></asp:TreeNode>
                    <asp:TreeNode Text="Emetti" Value="Emetti"></asp:TreeNode>
                    <asp:TreeNode Text="Elenco Emissioni" Value="ElencoEmissioni"></asp:TreeNode>
                </asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Ragioneria" Value="Ragioneria">
                <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Ricerca" Value="Ricerca1">
                    <asp:TreeNode Text="Inquilini" Value="Inquilini"></asp:TreeNode>
                    <asp:TreeNode Text="Num.Bolletta" Value="Num.Bolletta"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Pagamenti" Value="Pagamenti0">
                    <asp:TreeNode Text="Inserisci Pag." ToolTip="Inserimento manuale pagamenti" Value="InserisciPag">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Pag. Manuale" ToolTip="Pagamenti extra mav" Value="PG_EXTRA_MAV">
                    </asp:TreeNode>
                    <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Report Pag. Manuali"
                        Value="Report Pag.Extra Mav">
                        <asp:TreeNode Text="Data/Operatore" Value="RptPgExtra"></asp:TreeNode>
                        <asp:TreeNode Text="Riepilogo CSV" Value="DettPagEffett"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode NavigateUrl="~/Contratti/Pagamenti/GuidaPagamentiExtraMav.pdf" Target="_blank"
                        Text="Guida Pag. Manuali" ToolTip="Guida operativa" Value="Guida Extra Mav">
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Ingiunzioni" Value="Ingiunzioni0">
                    <asp:TreeNode Text="Pag. Manuale Ing." Value="PG_MANUALE_INGIUNZ" ToolTip="Pagamenti Manuali Bollette Ingiunte">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Caricam.massivo ingiunz." Value="caric_mass_ing"></asp:TreeNode>
                    <asp:TreeNode Text="Report Ingiunzioni" ToolTip="Report Pag. Manuali Ingiunzioni"
                        Value="RptPgExtraIng_0" Expanded="False" SelectAction="SelectExpand">
                        <asp:TreeNode Text="Rpt pagam. ing." Value="RptPgExtraIng"></asp:TreeNode>
                        <asp:TreeNode Text="Rpt boll. ing." Value="RptBollIng"></asp:TreeNode>
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Ruoli" Value="Ruoli0">
                    <asp:TreeNode Text="Pag. Manuale Ruoli" ToolTip="Pagamenti Manuali Bollette a Ruolo"
                        Value="PG_MANUALE_RUOLO"></asp:TreeNode>
                    <asp:TreeNode SelectAction="SelectExpand" Text="Report Ruoli" ToolTip="Report Pag. Manuali Ruoli"
                        Value="RptPgExtraRuoli_0">
                        <asp:TreeNode Text="Report pagam. ruoli" Value="RptPgExtraRuoli"></asp:TreeNode>
                        <asp:TreeNode Text="Report boll. ruolo" Value="RptBollRuoli"></asp:TreeNode>
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Voci Schema" Value="VociSchema0">
                    <asp:TreeNode Text="Stampa Voci" ToolTip="Stampa Voci schema" Value="rptVociSchema">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Priorità Voci" Value="PrioritàVoci"></asp:TreeNode>
                    <asp:TreeNode Text="Voci Schema" Value="caricamVoci1" SelectAction="SelectExpand">
                        <asp:TreeNode Text="Carica" Value="caricamVoci"></asp:TreeNode>
                        <asp:TreeNode Text="Elimina" Value="EliminaVoci"></asp:TreeNode>
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Rateizzazioni Emesse"
                    Value="RatEmesse"></asp:TreeNode>
                <asp:TreeNode Text="Elenco Elaboraz." Value="elenco_elaboraz"></asp:TreeNode>
                <asp:TreeNode Text="Caricam. Conguagli AU" Value="ConguaglioAU"></asp:TreeNode>
                <asp:TreeNode Text="Report Doc. Gest." Value="report_doc_gest"></asp:TreeNode>
                <asp:TreeNode Text="Spostam. doc. gestionali" Value="SpostaGestionali"></asp:TreeNode>
                <asp:TreeNode Text="Dep. Cauz. in Restit." ToolTip="Depositi cauzionali in restituzione"
                    Value="DepRestit"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Anagrafica" Value="Anagrafica"></asp:TreeNode>
            <asp:TreeNode Text="Elenco Comuni" Value="Comuni" NavigateUrl="~/AMMSEPA/OperatoreSUA/elencoComuniSUA.aspx?C=1"
                Target="_blank"></asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Parametri" Value="Parametri">
                <asp:TreeNode Text="Agg. ISTAT" ToolTip="Gestione indici ISTAT" Value="ISTAT"></asp:TreeNode>
                <asp:TreeNode Text="Interessi Legali" ToolTip="Gestione degli interessi legali" Value="Int. Legali">
                </asp:TreeNode>
                <asp:TreeNode Text="Bollette" Value="Bollette"></asp:TreeNode>
                <asp:TreeNode Text="Locatore" Value="Locatore"></asp:TreeNode>
                <asp:TreeNode Text="Richiedente" Value="Richiedente"></asp:TreeNode>
                <asp:TreeNode Text="Mandatari" Value="Mandatario123" Expanded="False" SelectAction="SelectExpand">
                    <asp:TreeNode Text="431,ERP Mod." Value="Mandatario1"></asp:TreeNode>
                    <asp:TreeNode Text="Usi Diversi" Value="Mandatario2"></asp:TreeNode>
                    <asp:TreeNode Text="392/78" Value="Mandatario3"></asp:TreeNode>
                    <asp:TreeNode Text="Art.15 e altri" Value="Mandatario"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Commissariati" Value="Commissariati"></asp:TreeNode>
                <asp:TreeNode Text="Gest. Modelli" Value="GestModelli"></asp:TreeNode>
                <asp:TreeNode Text="Motivaz. spost./annull." Value="motivSPOST_ANNULL"></asp:TreeNode>
                <asp:TreeNode Text="Motivazioni storno" Value="MotiviStorno"></asp:TreeNode>
                <asp:TreeNode Text="Documenti Gestionali" Value="doc_gest"></asp:TreeNode>
                <asp:TreeNode Text="Tipologie Bollette" Value="tip_boll"></asp:TreeNode>
                <asp:TreeNode Text="Versamento" Value="Versamento"></asp:TreeNode>
                <asp:TreeNode Text="Contabilizzazione ReCa" Value="ReCa"></asp:TreeNode>
                <asp:TreeNode Text="Date blocco" ToolTip="Date blocco" Value="DateBlocco"></asp:TreeNode>
                <asp:TreeNode Text="Spostam. Date Scad.Bolletta" Value="spostaDate"></asp:TreeNode>
                <asp:TreeNode Text="Rest. Dep. Cauzionale" ToolTip="Parametri Restituzione Deposito Cauzionale"
                    Value="RestCauz"></asp:TreeNode>
 <asp:TreeNode Text="Anagrafica Processi" Value="anagrProc"></asp:TreeNode>
                <asp:TreeNode Text="Motivi Decisioni" Value="motiviProcesso"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Parametri" Value="Parametri1">
                <asp:TreeNode Text="Rest. Dep. Cauzionale" ToolTip="Parametri Restituzione Deposito Cauzionale"
                    Value="RestCauz1"></asp:TreeNode>
                <asp:TreeNode Text="Motivi Decisioni" Value="motiviProcesso1"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Rpt Appuntamenti" Value="RptApp"></asp:TreeNode>
            <asp:TreeNode Text="Imposte Ante Gestore" Value="ImposteAnteGestore" Expanded="False"
                SelectAction="Expand">
                <asp:TreeNode Text="Crea File" Value="AnteAler"></asp:TreeNode>
                <asp:TreeNode Text="Elenco File" Value="ElencoFileAnte"></asp:TreeNode>
                <asp:TreeNode Text="Ricevute" Value="RicevuteAnteAler"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Report RU-AU" Value="ReportRUAU"></asp:TreeNode>
            <asp:TreeNode Text="Report RU-Saldi" Value="ReportRUSaldi"></asp:TreeNode>
            <asp:TreeNode Text="Estrazioni RU" Value="Estrazioni RU" SelectAction="Expand" Expanded="False">
                <asp:TreeNode Text="Varie RU" Value="estrazioni_ru"></asp:TreeNode>
                <asp:TreeNode Text="Elenco" Value="elenco_estraz"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Report Az. Legali" SelectAction="Expand" Expanded="False">
                <asp:TreeNode Text="Mononucleo Deced." Value="RptAzLegali2"></asp:TreeNode>
                <asp:TreeNode Text="Decreto Decadenza" Value="RptAzLegali"></asp:TreeNode>
                <asp:TreeNode Text="Accesso Uff. Giudiz." Value="AccessoUffGiudiziario"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Report Rateizz. Str." SelectAction="Expand">
            <asp:TreeNode Text="Esiti Generali" Value="RptRateizzStr0"></asp:TreeNode>
                <asp:TreeNode Text="RU max 9 boll. pagate" Value="RptRateizzStr"></asp:TreeNode>
                <asp:TreeNode Text="RU nucleo non capiente" Value="RptRateizzStr2"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Compensazione Crediti" Value="Spalmatore"></asp:TreeNode>
            <asp:TreeNode Expanded="False" Text="Guide" Value="Manuali" SelectAction="SelectExpand">
                <asp:TreeNode NavigateUrl="~/Contratti/Procedura_Sloggio.pdf" Target="_blank" Text="Procedura Sloggio"
                    Value="Procedura Sloggio"></asp:TreeNode>
                <asp:TreeNode NavigateUrl="~/RATEIZZAZIONE/GuidaRateizzazioni.pdf" Target="_blank"
                    Text="Rateizzazione" Value="Rateizzazione"></asp:TreeNode>
                <asp:TreeNode NavigateUrl="~/VSA/Guida_GestioneLocatari.pdf" Target="_blank" Text="Gest. Locatari"
                    Value="Gest. Locatari"></asp:TreeNode>
                <asp:TreeNode NavigateUrl="~/Gestione_locatari/Guida_Gestione Locatari_MM.docx" Target="_blank" Text="Nuova Norm. Gest.Loc."
                    Value="Gest. Locatari"></asp:TreeNode>
                <asp:TreeNode NavigateUrl="~/RATEIZZAZIONE/Guida_RateizzStr.pdf" Target="_blank"
                    Text="Rateizz.Straord." Value="Rateizz_Straord"></asp:TreeNode>
            </asp:TreeNode>
        </Nodes>
        <NodeStyle BorderStyle="None" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black"
            HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
        <ParentNodeStyle Font-Bold="False" />
        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
            VerticalPadding="0px" />
    </asp:TreeView>
    <script type="text/javascript">


    </script>
    </form>
    <p>
        &nbsp;
    </p>
</body>
</html>
