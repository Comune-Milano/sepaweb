<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_sinistra.aspx.vb" Inherits="ASS_ESTERNA_menu_sinistra" %>

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
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                Style="z-index: 100; left: 4px; position: absolute; top: 568px" Text="Label" Width="106px" ForeColor="#721C1F"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px" Style="z-index: 104; left: 3px; position: absolute;
                top: 97px" Width="106px" ImageSet="Arrows">
                <LevelStyles>
                    <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                        ForeColor="#721C1F" HorizontalPadding="1px" />
                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                        HorizontalPadding="3px" />
                </LevelStyles>
                <Nodes>
                    <asp:TreeNode Text="Invio XML" Value="1" ToolTip="Invio esito degli abbinamenti tramite xml"></asp:TreeNode>
                    <asp:TreeNode Expanded="False" SelectAction="None"
                        Text="Sistema" Value="8">
                        <asp:TreeNode NavigateUrl="~/Bando.aspx" Target="_blank"
                            Text="Par. Bando" Value="9"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="~/regolamento n.1_140306.pdf"
                            Target="_blank" Text="Normativa" Value="10"></asp:TreeNode>
                    </asp:TreeNode>
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

