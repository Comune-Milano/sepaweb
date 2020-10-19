<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_sinistra.aspx.vb" Inherits="CAMBI_menu_sinistra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>menu</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	</head>
	<body background="../NuoveImm/comeblu_sx.jpg" bgColor="#003399">
		<form id="Form1" method="post" runat="server">
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                Style="z-index: 100; left: 4px; position: absolute; top: 568px" Text="Label" Width="106px" ForeColor="#721C1F"></asp:Label>
            &nbsp; &nbsp;&nbsp;
            <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px" NodeWrap="True" Style="z-index: 108; left: 3px;
                position: absolute; top: 87px" Width="115px" ImageSet="Arrows">
                <LevelStyles>
                    <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                        ForeColor="#721C1F" HorizontalPadding="1px" />
                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                        HorizontalPadding="3px" />
                </LevelStyles>
                <Nodes>
                    <asp:TreeNode Expanded="False" SelectAction="SelectExpand"
                        Text="Dichiarazioni" Value="0">
                        <asp:TreeNode Text="Nuova" Value="1"></asp:TreeNode>
                        <asp:TreeNode Text="Ricerca" Value="2"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Expanded="False" SelectAction="SelectExpand"
                        Text="Domande" Value="3">
                        <asp:TreeNode Text="Nuova" Value="4"></asp:TreeNode>
                        <asp:TreeNode Text="Ricerca" Value="5"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode SelectAction="None" Text="------------------" Value="---------"></asp:TreeNode>
                    <asp:TreeNode Expanded="False" Text="Carico/Scarico"
                        Value="10" SelectAction="SelectExpand">
                        <asp:TreeNode NavigateUrl="~/CAMBI/DomandeCarico.aspx"
                            Target="_blank" Text="Elenchi" Value="11"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="~/CAMBI/Scarico.aspx"
                            Target="_blank" Text="Scarico" Value="12"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="~/CAMBI/CercaDistinte.aspx"
                            Target="_blank" Text="Distinte" Value="13"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="------------------" Value="------------------"></asp:TreeNode>
                    <asp:TreeNode Expanded="False" Text="Calcolo CF"
                        Value="7"></asp:TreeNode>
                    <asp:TreeNode Expanded="False" NavigateUrl="~/Trasparenza.aspx"
                        Target="_blank" Text="Trasparenza" ToolTip="Visualizza il modello trasparenza"
                        Value="8"></asp:TreeNode>
                    <asp:TreeNode Expanded="False" SelectAction="SelectExpand"
                        Text="Sistema" Value="17">
                        <asp:TreeNode NavigateUrl="~/Bando.aspx" Target="_blank"
                            Text="Par. Bando" Value="18"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="~/regolamento n.1_140306.pdf"
                            Target="_blank" Text="Normativa" Value="19"></asp:TreeNode>
                        <asp:TreeNode Text="Requisiti" Value="20" NavigateUrl="~/Requisiti.htm" Target="_blank"></asp:TreeNode>
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

