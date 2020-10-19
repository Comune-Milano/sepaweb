<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_sinistra.aspx.vb" Inherits="CENSIMENTO_menu_sinistra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
	<head runat="server">
		<title>menu</title>
	</head>
	<body background="../NuoveImm/comeblu_sx.jpg">
		<form id="Form1" method="post" runat="server">
            &nbsp; &nbsp;
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                Style="z-index: 104; left: 5px; position: absolute; top: 552px" Text="Label" Width="106px" ForeColor="#721C1F"></asp:Label>
            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
            <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px"
                Style="z-index: 110; left: 1px; position: absolute; top: 96px" 
                Width="106px" ImageSet="Arrows" TabIndex="-1" onclick="Chiudi();">
                                <LevelStyles>
                    <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                        ForeColor="#721C1F" HorizontalPadding="1px"/>
                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                        HorizontalPadding="3px" />
                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                        HorizontalPadding="3px" />      
                     <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                        HorizontalPadding="3px" />                   
                </LevelStyles>
                <Nodes>
                    <asp:TreeNode Text="Segnalazioni" Value="SEGNALAZIONE" Expanded="False" 
                        SelectAction="SelectExpand">
                        <asp:TreeNode Text="Nuova" Value="Nuova"></asp:TreeNode>
                        <asp:TreeNode Text="Ricerca" 
                            Value="RicercaS"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Expanded="False" SelectAction="Expand" Text="Report" 
                        Value="Report">
                        <asp:TreeNode Text="Stato Interv." Value="StatoInt"></asp:TreeNode>
                        <asp:TreeNode Text="Situaz.Interv." Value="SitInt"></asp:TreeNode>
                        <asp:TreeNode Text="Tempi" Value="Tempi"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Supporto" Value="SupportTable" Expanded="False" 
                        SelectAction="Expand">
                        <asp:TreeNode Text="Tipo Guasti" Value="TGuasti"></asp:TreeNode>
                        <asp:TreeNode Text="Note Chiusura" Value="ClosingNote"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode NavigateUrl="~/CALL_CENTER/GuidaCallCenter.pdf" Target="_blanck" 
                        Text="Guida" Value="Guida"></asp:TreeNode>
                </Nodes>
                <NodeStyle BorderStyle="None" Font-Names="arial" Font-Size="8pt" ForeColor="Black" HorizontalPadding="1px" NodeSpacing="0px" VerticalPadding="0px" />
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
                &nbsp;</p>
		    <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
		</form>

	</body>

</html>