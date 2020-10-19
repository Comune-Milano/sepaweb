<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_sinistra.aspx.vb" Inherits="ANAUT_menu_sinistra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript" src="Funzioni.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>menu</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	</head>
	<body bgColor="#eceff2" style="background-attachment: fixed; background-image: url(../NuoveImm/ContrattiSX.jpg); background-repeat: no-repeat">
		<form id="Form1" method="post" runat="server">
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                Style="z-index: 100; left: 4px; position: absolute; top: 568px" Text="Label" Width="106px" ForeColor="#721C1F"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="7pt" Height="1px" Style="z-index: 104; left: 0px; position: absolute;
                top: 96px" Width="1px" ImageSet="Arrows">
                <LevelStyles>
                    <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                        ForeColor="#721C1F" HorizontalPadding="1px"/>
                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                        HorizontalPadding="3px" />
                </LevelStyles>
                <Nodes>
                    <asp:TreeNode Text="Impianti" Value="Impianti" Expanded="True" 
                        SelectAction="SelectExpand">
                        <asp:TreeNode Text="Inserimento" Value="1" SelectAction="SelectExpand" 
                            Expanded="False">
                            <asp:TreeNode Text="Acque Meteo" ToolTip="Gestione Stazione di Sollevamento Acque Meteoriche"
                                Value="ME"></asp:TreeNode>
                            <asp:TreeNode Text="Antincendio" ToolTip="Gestione Impianto Antincendio" Value="AN">
                            </asp:TreeNode>
                            <asp:TreeNode Text="Canna Fumaria" ToolTip="Gestione Canna Fumaria" Value="CF"></asp:TreeNode>
                            <asp:TreeNode Text="Centr.le Idrica" ToolTip="Gestione Cetrale Idrica" Value="ID"></asp:TreeNode>
                            <asp:TreeNode Text="Centr.le Termica" ToolTip="Gestione Impianti Centrale Termica (Riscaldamento e Acqua Calda)"
                                Value="TE"></asp:TreeNode>
                            <asp:TreeNode Text="Citofonico" ToolTip="Gestione Impianti Citofonici (Analogico, Digitale, VideoCitofono)"
                                Value="CI"></asp:TreeNode>
                            <asp:TreeNode Text="Elettrico" ToolTip="Gestione Impianti Elettrici e di Illuminazione"
                                Value="EL"></asp:TreeNode>
                            <asp:TreeNode Text="GAS" ToolTip="Gestione Impianti GAS (Riscaldamento, Acqua Calda, Cucina)"
                                Value="GA"></asp:TreeNode>
                            <asp:TreeNode Text="Sollevamento" ToolTip="Gestione Impianti di Sollevamento (Acensore, Montacarichi, Montascale, Piattaforme Elevatrici)"
                                Value="SO"></asp:TreeNode>
                            <asp:TreeNode Text="Teleriscaldam." ToolTip="Gestione Impianti Teleriscaldamento (Riscaldamento e Acqua Calda)"
                                Value="TR"></asp:TreeNode>
                            <asp:TreeNode Text="Termico Auton." ToolTip="Gestione Impianti Termici Autonomi fino a 35 Kw (Riscaldamento e Combinata)"
                                Value="TA"></asp:TreeNode>
                            <asp:TreeNode Text="Tutela Immobile" ToolTip="Gestione Tutela dell'Immobile (Cancelli, Recinzioni, Antintrusione, ecc)"
                                Value="TU"></asp:TreeNode>
                            <asp:TreeNode Text="TV" ToolTip="Gestione Impianto TV (Analogico, Digitale Terrestre, Satellitare)"
                                Value="TV"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Ricerca" Value="Ricerca" SelectAction="SelectExpand" 
                            Expanded="False">
                            <asp:TreeNode Text="Libera" Value="2"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Guida" Value="Manuale" Expanded="False" ToolTip="Guida Operativa" NavigateUrl="~/IMPIANTI/Report/GuidaImpianti.pdf" Target="_blank" >
                        </asp:TreeNode>
                    </asp:TreeNode>
                </Nodes>
                <NodeStyle BorderStyle="None" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                <ParentNodeStyle Font-Bold="False" />
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                    VerticalPadding="0px" />
            </asp:TreeView>
            &nbsp; &nbsp;&nbsp;&nbsp;<br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;<br />
            <br />
            <br />
            <br />
            &nbsp;<asp:Image ID="ImageAllarme" runat="server" ImageUrl="~/IMPIANTI/Immagini/AllarmeGIALLO.png" /><br />
            &nbsp;
            <asp:TreeView ID="TVerifiche" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px" Style="z-index: 104; left: 48px; position: absolute;
                top: 488px" Width="1px" ImageSet="Arrows">
                <ParentNodeStyle Font-Bold="False" />
                <LevelStyles>
                    <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                        ForeColor="#721C1F" HorizontalPadding="1px"/>
                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                        HorizontalPadding="3px" />
                </LevelStyles>
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                    VerticalPadding="0px" />
                <Nodes>
                    <asp:TreeNode Text="Verifiche" Value="Verifiche" Expanded="True" 
                        SelectAction="SelectExpand">
                    </asp:TreeNode>
                </Nodes>
                <NodeStyle BorderStyle="None" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
            </asp:TreeView>
		</form>
	</body>

</html>
