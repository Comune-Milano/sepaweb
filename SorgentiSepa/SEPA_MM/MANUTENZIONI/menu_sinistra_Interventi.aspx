<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_sinistra_Interventi.aspx.vb" Inherits="MANUTENZIONI_menusinistraInterventi" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>menu</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	</head>
	<body background="../NuoveImm/comeblu_sx.jpg">
		<form id="Form1" method="post" runat="server">
            &nbsp; &nbsp;
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                Style="z-index: 104; left: 4px; position: absolute; top: 568px" Text="Label" Width="106px" ForeColor="#721C1F"></asp:Label>
            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
            <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px"
                Style="z-index: 110; left: 2px; position: absolute; top: 96px" Width="106px" ImageSet="Arrows" TabIndex="-1">
                <Nodes>
                    <asp:TreeNode SelectAction="Expand" Text="MANUTENZIONI" Value="MANUTENZIONI" Expanded="False">
                        <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Inserimento" Value="0">
                            <asp:TreeNode Text="Complesso" Value="2"></asp:TreeNode>
                            <asp:TreeNode Text="Edificio" Value="3"></asp:TreeNode>
                            <asp:TreeNode Text="Unit&#224; Imm." Value="5"></asp:TreeNode>
                            <asp:TreeNode Text="Unit&#224; Com." Value="4"></asp:TreeNode>
                            <asp:TreeNode Text="Impianti" Value="24"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Ricerca" Value="1">
                            <asp:TreeNode Text="Complessi" Value="6"></asp:TreeNode>
                            <asp:TreeNode Text="Edifici" Value="7"></asp:TreeNode>
                            <asp:TreeNode Text="Unit&#224; Imm." Value="9"></asp:TreeNode>
                            <asp:TreeNode Text="Unit&#224; Com." Value="8"></asp:TreeNode>
                            <asp:TreeNode Text="Impianti" Value="25"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Ver. St. Manut." Value="StManut"></asp:TreeNode>
                        <asp:TreeNode Text="Report Manut." Value="rptman"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="SERVIZI" Value="SERVIZI" SelectAction="Expand" Expanded="False">
                        <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Inserimento" Value="10">
                            <asp:TreeNode Text="Complesso" Value="12"></asp:TreeNode>
                            <asp:TreeNode Text="Edificio" Value="13"></asp:TreeNode>
                            <asp:TreeNode Text="Unit&#224; Imm." Value="15"></asp:TreeNode>
                            <asp:TreeNode Text="Unit&#224; Com." Value="14"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Ricerca" Value="11">
                            <asp:TreeNode Text="Complessi" Value="16"></asp:TreeNode>
                            <asp:TreeNode Text="Edifici" Value="17"></asp:TreeNode>
                            <asp:TreeNode Text="Unit&#224; Imm." Value="19"></asp:TreeNode>
                            <asp:TreeNode Text="Unit&#224; Com." Value="18"></asp:TreeNode>
                        </asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="UTENZE" Value="UTENZE" Expanded="False" SelectAction="Expand">
                        <asp:TreeNode Text="An. Fornitori" Value="20"></asp:TreeNode>
                        <asp:TreeNode Text="Ricerche" Value="Ricerche" Expanded="False" SelectAction="Expand">
                            <asp:TreeNode Text="Complessi" Value="21"></asp:TreeNode>
                            <asp:TreeNode Text="Edificio" Value="22"></asp:TreeNode>
                        </asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="RPT FORNITORI" Value="23"></asp:TreeNode>
                </Nodes>
                <LevelStyles>
                    <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F" HorizontalPadding="1px" />
                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F" HorizontalPadding="3px" />
                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="Black" HorizontalPadding="1px" />
                </LevelStyles>
                <NodeStyle BorderStyle="None" />
                <ParentNodeStyle ChildNodesPadding="1px" Font-Names="Arial" Font-Size="8pt" 
                    HorizontalPadding="2px" />
                <HoverNodeStyle Font-Size="8pt" Font-Names="Arial" />
                <SelectedNodeStyle Font-Names="Arial" Font-Size="8pt" Font-Underline="False" />
                <RootNodeStyle Font-Names="Arial" Font-Size="8pt" HorizontalPadding="1px" />
                <LeafNodeStyle ChildNodesPadding="1px" HorizontalPadding="1px" Font-Names="Arial" Font-Size="8pt" />
            </asp:TreeView>
		</form>
	</body>

</html>


