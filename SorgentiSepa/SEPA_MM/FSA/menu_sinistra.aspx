<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_sinistra.aspx.vb" Inherits="FSA_menu_sinistra" %>

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
                    <asp:TreeNode Text="------------------" Value="------------------"></asp:TreeNode>
                    <asp:TreeNode Expanded="False" SelectAction="SelectExpand"
                        Text="Funzioni" Value="33">
                        <asp:TreeNode NavigateUrl="~/FSA/AssERP.aspx"
                            Target="_blank" Text="Ricerca Ass. ERP" ToolTip="Ricerca Assegnatari ERP" Value="34">
                        </asp:TreeNode>
                        <asp:TreeNode NavigateUrl="~/FSA/AssANA.aspx"
                            Target="_blank" Text="Ricerca Occ. ERP" ToolTip="Ricerca Occupanti ERP" Value="35">
                        </asp:TreeNode>
                        <asp:TreeNode Text="Ricerca Sfrattati" ToolTip="Ricerca Sfrattati"
                            Value="36" NavigateUrl="~/FSA/AssSfratti.aspx" Target="_BLANK"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="~/FSA/ElencoMandati.aspx" Target="_BLANK" Text="Da Liquidare"
                            Value="666"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="~/FSA/ElencoMandatiEff.aspx" Target="_BLANK" 
                            Text="Mandati Eff." Value="7777"></asp:TreeNode>
                        <asp:TreeNode NavigateUrl="~/FSA/ElencoNonIdonei.aspx" Target="_BLANK" 
                            Text="Negativi" Value="Negativi"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="------------------" Value="----------------"></asp:TreeNode>
                    <asp:TreeNode Expanded="False" Text="Calcolo CF"
                        Value="7"></asp:TreeNode>
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
