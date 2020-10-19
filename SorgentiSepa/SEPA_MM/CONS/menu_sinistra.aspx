﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_sinistra.aspx.vb" Inherits="CONS_menu_sinistra" %>

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
                Style="z-index: 110; left: 3px; position: absolute; top: 96px" Width="106px" ImageSet="Arrows">
                <Nodes>
                    <asp:TreeNode SelectAction="SelectExpand" Text="Dichiarazioni"
                        Value="0" Expanded="False">
                        <asp:TreeNode Text="Ricerca" Value="1"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode SelectAction="SelectExpand" Text="Domande"
                        Value="2" Expanded="False">
                        <asp:TreeNode Text="Ricerca" Value="3"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode SelectAction="SelectExpand" Text="Prenotazioni"
                        Value="4" Expanded="False">
                        <asp:TreeNode Text="Elimina" Value="5"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode SelectAction="SelectExpand" Text="Statistiche"
                        Value="14" Expanded="False">
                        <asp:TreeNode Text="Consultazioni" Value="6"></asp:TreeNode>
                        <asp:TreeNode Text="Prenotazioni" Value="7"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode NavigateUrl="~/Trasparenza.aspx"
                        Target="_blank" Text="M. Trasparenza" Value="16"></asp:TreeNode>
                    <asp:TreeNode SelectAction="SelectExpand" Text="Sistema"
                        Value="8" Expanded="False">
                        <asp:TreeNode NavigateUrl="~/Bando.aspx" Target="_blank"
                            Text="Par. Bando" Value="9"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="~/regolamento n.1_140306.pdf"
                            Target="_blank" Text="Normativa" Value="10"></asp:TreeNode>
                    </asp:TreeNode>
                </Nodes>
                <LevelStyles>
                    <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F" HorizontalPadding="1px" />
                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F" HorizontalPadding="3px" />
                </LevelStyles>
                <NodeStyle BorderStyle="None" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                <ParentNodeStyle Font-Bold="False" />
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                    VerticalPadding="0px" />
            </asp:TreeView>
		</form>
	</body>

</html>
