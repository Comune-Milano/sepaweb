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
	<body bgColor="#eceff2" style="background-attachment: fixed; background-image: url(../../NuoveImm/comeblu_sx.jpg); background-repeat: no-repeat">
		<form id="Form1" method="post" runat="server">
            &nbsp;
            &nbsp;&nbsp;&nbsp;
            <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px" Style="z-index: 104; left: 3px; position: absolute;
                top: 97px" Width="106px" ImageSet="Arrows">
                <LevelStyles>
                    <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                        ForeColor="#721C1F" HorizontalPadding="1px"/>
                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                        HorizontalPadding="3px" />
                </LevelStyles>
                <Nodes>
                    <asp:TreeNode Text="Anagrafica" Value="Anagrafica">
                        <asp:TreeNode Text="Inserimento" Value="Inserimento"></asp:TreeNode>
                        <asp:TreeNode Text="Ricerca" Value="Ricerca">
                        </asp:TreeNode>
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
