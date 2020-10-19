<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_sinistra.aspx.vb" Inherits="VSA_menu_sinistra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>menu</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
</head>
<body background="../NuoveImm/comeblu_sx.jpg" bgcolor="#003399">
    <form id="Form1" method="post" runat="server">
    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
        Style="z-index: 100; left: 4px; position: absolute; top: 568px" Text="Label"
        Width="106px" ForeColor="#721C1F"></asp:Label>
    &nbsp; &nbsp;&nbsp;
    <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px"
        NodeWrap="True" Style="z-index: 108; left: 3px; position: absolute; top: 87px"
        Width="115px" ImageSet="Arrows">
        <LevelStyles>
            <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                ForeColor="#721C1F" HorizontalPadding="1px" />
            <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                HorizontalPadding="3px" />
        </LevelStyles>
        <Nodes>
            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Dichiarazioni" Value="0">
                <asp:TreeNode Text="Nuova" Value="1"></asp:TreeNode>
                <asp:TreeNode Text="Ricerca" Value="2"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Domande" Value="3">
                <asp:TreeNode Text="Nuova" Value="4"></asp:TreeNode>
                <asp:TreeNode Text="Ricerca" Value="5"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Graduatoria" Value="Graduatoria"></asp:TreeNode>
            <asp:TreeNode Expanded="False" NavigateUrl="~/Trasparenza.aspx" Target="_blank" Text="Trasparenza"
                ToolTip="Visualizza il modello trasparenza" Value="8"></asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Sistema" Value="17">
                <asp:TreeNode Text="Requisiti" Value="20" NavigateUrl="~/Requisiti.htm" Target="_blank">
                </asp:TreeNode>
                <asp:TreeNode Text="Istruzioni" Value="21" NavigateUrl="~/Manuale_SEPA_WEB.pdf" Target="_blank">
                </asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" Text="Guida" Value="Manuali" SelectAction="SelectExpand"
                ToolTip="Guide">
                <asp:TreeNode NavigateUrl="Guida_GestioneLocatari.pdf" Target="_blank" Text="Gest. Locatari"
                    Value="Locatari"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Doc. necessaria"
                Value="doc_necessaria">
                <asp:TreeNode Text="Riduz. canone" Value="riduz_canone"></asp:TreeNode>
                <asp:TreeNode Text="Ampliamento" Value="ampliamento"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="<p style='font-family: VERDana; font-size: 10px; font-weight: bold;'>&lt;a href=&quot;#&quot; onclick=&quot;window.open('../cf/codice.htm','cf','scrollbars=no,resizable=no,width=500,height=380,status=no,location=no,toolbar=no');&quot;&gt;Calcolo CF&lt;/a&gt;</p>"
                Value="CF"></asp:TreeNode>
        </Nodes>
        <NodeStyle BorderStyle="None" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black"
            HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
        <ParentNodeStyle Font-Bold="False" />
        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
            VerticalPadding="0px" />
    </asp:TreeView>
    </form>
</body>
</html>
