<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_sinistra.aspx.vb" Inherits="ANAUT_menu_sinistra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>menu</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	</head>
	<body background="../NuoveImm/comeblu_sx.jpg" bgColor="#eceff2">
		<form id="Form1" method="post" runat="server">
            &nbsp;&nbsp;&nbsp;
            <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104; left: 3px; position: absolute;
                top: 98px; height: 276px; width: 114px;" ImageSet="Arrows">
                <LevelStyles>
                    <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                        ForeColor="#721C1F" HorizontalPadding="1px"/>
                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                        HorizontalPadding="1px" />
                        <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                        HorizontalPadding="1px" />
                </LevelStyles>
                <Nodes>
                    <asp:TreeNode Text="Ricerca" Value="2"></asp:TreeNode>
                    <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Report" 
                        Value="Report">
                        <asp:TreeNode Text="Riepilogo" Value="Riepilogo"></asp:TreeNode>
                        <asp:TreeNode Text="Da Verificare" Value="DaVerificare" 
                            NavigateUrl="~/ANAUT/ElencoVerifiche.aspx" Target="_blank"></asp:TreeNode>
                        <asp:TreeNode Text="Ind.Sospese" Value="IndSospese" 
                            NavigateUrl="~/ANAUT/ElencoSospensioni.aspx" Target="_blank"></asp:TreeNode>
                        <asp:TreeNode Text="Doc. Mancante" Value="DocMancante"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Expanded="False" SelectAction="SelectExpand" 
                        Text="Gestione AU" Value="AnagrafeUtenza">
                        <asp:TreeNode Text="Tasso Rendim." Value="TassoRendimento"></asp:TreeNode>
                        <asp:TreeNode Text="Apert./Chiusura" Value="GestioneAU"></asp:TreeNode>
                        <asp:TreeNode Text="Conguaglio" Value="Conguaglio"></asp:TreeNode>
                        <asp:TreeNode Text="Modelli" Value="ModelliAU"></asp:TreeNode>
                        <asp:TreeNode Text="Str./Sp./Op." ToolTip="Strutture/Sportelli/Operatori" 
                            Value="FilSpOp"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Expanded="False" SelectAction="SelectExpand" 
                        Text="Convocazioni AU" Value="ConvocazioniA">
                        <asp:TreeNode Text="Assegnatari" Value="Assegnatari" 
                            ToolTip="Elenco di tutti gli assegnatari"></asp:TreeNode>
                        <asp:TreeNode Text="Convocabili" ToolTip="Elenco dei possibili convocabili" 
                            Value="Convocabili"></asp:TreeNode>
                        <asp:TreeNode Text="Gest.Mot.Escl." 
                            ToolTip="Gestione Motivi Esclusione Convocabili" Value="GestEsclusione">
                        </asp:TreeNode>
                        <asp:TreeNode Text="Elenco Conv." Value="ElencoConv"></asp:TreeNode>
                        <asp:TreeNode Text="Gruppi Conv." Value="GruppiConv"></asp:TreeNode>
                        <asp:TreeNode Text="Liste Conv." Value="CreaListaConv" 
                            ToolTip="Gestione Liste di Convocazione"></asp:TreeNode>
                        <asp:TreeNode Text="Simulazione" Value="Simulazione"></asp:TreeNode>
                        <asp:TreeNode Text="Elenco Simulaz." ToolTip="Elenco Simulazioni effettuate" 
                            Value="ElencoSimulazioni"></asp:TreeNode>
                        <asp:TreeNode Text="Genera Lettere" Value="EmissioneLettere"></asp:TreeNode>
                        <asp:TreeNode Text="Carica Data Sped." ToolTip="Carica Data Spedizione" 
                            Value="DataSpedizione"></asp:TreeNode>
                        <asp:TreeNode Text="Ricerca Conv." Value="ReportConvocazioni"></asp:TreeNode>
                        <asp:TreeNode Text="Elenco Lettere" Value="ElencoLettere"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Agenda AU" 
                        Value="AgendaAU">
                        <asp:TreeNode Text="Sit. Generale" ToolTip="Situazione generale agenda" 
                            Value="SitGenerale"></asp:TreeNode>
                        <asp:TreeNode Text="Cerca Inquilino" 
                            ToolTip="Cerca un inquilino tra quelli con appuntamento" Value="CercaInq">
                        </asp:TreeNode>
                        <asp:TreeNode Text="Inq.SenzaApp." Value="Inq.SenzaApp."></asp:TreeNode>
                        <asp:TreeNode Text="Sospese" ToolTip="Convocazioni sospese" 
                            Value="Sospese"></asp:TreeNode>
                        <asp:TreeNode Text="Gest.Mot.Sosp." ToolTip="Gestione motivi sospensione" 
                            Value="GestMotSosp"></asp:TreeNode>
                        <asp:TreeNode Text="FronteSpizi" Value="FronteSpizi" Expanded="False" 
                            SelectAction="SelectExpand">
                            <asp:TreeNode Text="Genera" Value="FronteSpizio"></asp:TreeNode>
                            <asp:TreeNode Text="Elenco" Value="ElencoFS"></asp:TreeNode>
                        </asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode SelectAction="SelectExpand" 
                        Text="Diffide" Value="Diffide" Expanded="False">
                        <asp:TreeNode Text="Tempistica" Value="Tempistica">
                        </asp:TreeNode>
                        <asp:TreeNode Text="Incomplete" Value="Incomplete" SelectAction="SelectExpand">
                            <asp:TreeNode Text="Crea Let." ToolTip="Crea Lettere Diffida" 
                                Value="CreaIncomplete"></asp:TreeNode>
                            <asp:TreeNode Text="Elenco File" ToolTip="Elenco file prodotti" 
                                Value="ElencoFileIncompleti"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Non Rispondenti" 
                            Value="Non Rispondenti" Expanded="False" SelectAction="SelectExpand">
                            <asp:TreeNode Text="Crea Let." 
                                ToolTip="Crea lettere di diffida" Value="NonRispondenti">
                            </asp:TreeNode>
                            <asp:TreeNode Text="Elenco File" ToolTip="Elenco di tutti i file prodotti" 
                                Value="ElencoFile"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Ric. PostAler" 
                            ToolTip="Elenco delle lettere inviate con relativa ricevuta postaler, se presente" 
                            Value="RicPostAler"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Verifiche Chius." 
                        Value="Verifiche" ToolTip="Verifiche pre-chiusura AU">
                        <asp:TreeNode Text="Report Sit." ToolTip="Report Situazione" Value="ReportSit">
                        </asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Expanded="False" SelectAction="SelectExpand" 
                        Text="Applicazione AU" Value="Applicazione AU">
                        <asp:TreeNode Text="Verifica" Value="Verifica123" Expanded="False" 
                            SelectAction="SelectExpand">
                            <asp:TreeNode Text="Risp." ToolTip="Verifica Rispondenti" Value="VerificaApp">
                            </asp:TreeNode>
                            <asp:TreeNode Text="Non Risp." ToolTip="Verifica Non Rispondenti" 
                                Value="VerificaAppNON"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Gruppi" 
                            Value="Gruppi">
                            <asp:TreeNode Text="Nuovo" Value="NuovoGruppo"></asp:TreeNode>
                            <asp:TreeNode Text="Ricerca" Value="RicercaGruppo"></asp:TreeNode>
                            <asp:TreeNode Text="Abusivi" Value="Verifica"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode SelectAction="SelectExpand" Text="Simula" 
                            Value="Simula" Expanded="False">
                            <asp:TreeNode Text="Rispondenti" Value="SimulaRispondenti"></asp:TreeNode>
                            <asp:TreeNode Text="Non Risp." Value="NonRisp"></asp:TreeNode>
                            <asp:TreeNode Text="Abusivi" Value="SimulaAbusivi"></asp:TreeNode>
                            <asp:TreeNode Text="AU 392/78" Value="AU39278"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Applica" 
                            Value="Applica">
                            <asp:TreeNode Text="Rispondenti" Value="Rispondenti"></asp:TreeNode>
                            <asp:TreeNode Text="Non Risp." Value="ApplicaNonRispondenti"></asp:TreeNode>
                            <asp:TreeNode Text="Abusivi" Value="ApplicaAbusivi"></asp:TreeNode>
                            <asp:TreeNode Text="Applica Can." 
                                ToolTip="Applica Canoni e Contributo Canone" 
                                Value="ApplicaCanoni"></asp:TreeNode>
                            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Elenco App." 
                                Value="ElencoApp">
                                <asp:TreeNode Text="Risp." Value="AppRisp"></asp:TreeNode>
                            </asp:TreeNode>
                        </asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Decadenze" 
                        Value="Decadenze">
                        <asp:TreeNode Text="Elenco Proposte" Value="ElencoProposte"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Expanded="False" Text="Carico/Scarico"                       
                        Value="20" SelectAction="SelectExpand">
                        <asp:TreeNode NavigateUrl="~/ANAUT/DomandeCarico.aspx"
                            Target="_Blank" Text="Elenchi" Value="21"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="~/ANAUT/Scarico.aspx"
                            Target="_blank" Text="Scarico" Value="Scarico"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="~/ANAUT/CercaDistinte.aspx"
                            Target="_blank" Text="Distinte" Value="Distinte"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Doc.Necessaria" Value="DocNecessaria"></asp:TreeNode>
                    <asp:TreeNode NavigateUrl="~/cf/codice.htm"
                        Target="_blank" Text="Calcolo CF" ToolTip="calcolo del codice fiscale" Value="3">
                    </asp:TreeNode>
                </Nodes>
                <NodeStyle BorderStyle="None" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                <ParentNodeStyle Font-Bold="False" />
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                    VerticalPadding="0px" />
            </asp:TreeView><asp:TreeView ID="TreeView1" runat="server" Font-Names="arial" 
                Font-Size="8pt" Height="10px" Style="z-index: 104; left: 3px; position: absolute;
                top: 97px" Width="106px" Visible="False" ImageSet="Arrows">
                <LevelStyles>
                    <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                        ForeColor="#721C1F" HorizontalPadding="1px" />
                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                        HorizontalPadding="3px" />
                </LevelStyles>
                <Nodes>
                    <asp:TreeNode Text="Ricerca" Value="2"></asp:TreeNode>
                    <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Report" 
                        Value="Report">
                        <asp:TreeNode Text="Riepilogo" Value="Riepilogo"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="~/ANAUT/ElencoVerifiche.aspx" Target="_blank" 
                            Text="Da Verificare" Value="Da Verificare"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="~/ANAUT/ElencoSospensioni.aspx" Target="_blank" 
                            Text="Ind.Sospese" Value="Ind.Sospese"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode SelectAction="None" Text="---" Value="3"></asp:TreeNode>
                </Nodes>
                <NodeStyle BorderStyle="None" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                <ParentNodeStyle Font-Bold="False" />
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                    VerticalPadding="0px" />
            </asp:TreeView>
		</form>
	</body>

</html>
