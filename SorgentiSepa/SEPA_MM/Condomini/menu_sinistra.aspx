<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_sinistra.aspx.vb" Inherits="CONDOMINI_menu_sinistra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>menu</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
</head>
<body style="background-attachment: fixed; background-image: url('Immagini/CondominiSX.jpg');
    background-repeat: no-repeat">
    <form id="Form1" method="post" runat="server">
    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
        Style="z-index: 100; left: 5px; position: absolute; top: 564px" Width="106px"
        ForeColor="#721C1F"></asp:Label>
    <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px"
        Style="z-index: 104; left: 3px; position: absolute; top: 97px" Width="106px"
        ImageSet="Arrows" TabIndex="-1">
        <LevelStyles>
            <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                ForeColor="#721C1F" HorizontalPadding="1px" />
            <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                HorizontalPadding="3px" />
            <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                HorizontalPadding="3px" />
            <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                HorizontalPadding="3px" />
        </LevelStyles>
        <Nodes>
            <asp:TreeNode Text="Elenco" Value="AnCond"></asp:TreeNode>
            <asp:TreeNode Text="Inserimento" Value="Ins" SelectAction="Expand" 
                Expanded="False">
                <asp:TreeNode Text="Amministratore" Value="InsAmminist"></asp:TreeNode>
                <asp:TreeNode Text="Condominio" Value="Inserimento"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="Expand" Text="Ricerca" Value="Ricerca">
                <asp:TreeNode Text="Amministratore" Value="RicAmminist"></asp:TreeNode>
                <asp:TreeNode Text="Inquilino" Value="Inquilini"></asp:TreeNode>
                <asp:TreeNode Text="Pagamenti" Value="RPagamenti"></asp:TreeNode>
                <asp:TreeNode Text="Condomini" Value="RicCondomini"></asp:TreeNode>
                <asp:TreeNode Text="Dati per Chiusura" Value="DatiPerChiusura"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Report" Value="Report" Expanded="False" SelectAction="Expand">
                <asp:TreeNode Text="An. Amministratori" Value="AnAmministratori"></asp:TreeNode>
                <asp:TreeNode Text="Inquilini" Value="RptInquilini"></asp:TreeNode>
                <asp:TreeNode Text="An.Condomini" Value="AnCondomini"></asp:TreeNode>
                <asp:TreeNode Text="Rpt. Convocazioni" Value="RptConv"></asp:TreeNode>
                <asp:TreeNode Text="Rpt. Conv. Non Verb." Value="RptNonVerb"></asp:TreeNode>
                <asp:TreeNode Text="Morosità Emesse" Value="MorEmesse"></asp:TreeNode>
                <asp:TreeNode Expanded="False" Text="Gest.pagate/redicontate" 
                    Value="Contabilità">
                </asp:TreeNode>
                <asp:TreeNode Text="Preventivi" Value="Preventivi"></asp:TreeNode>
                <asp:TreeNode Text="Stampa Etichette" Value="PrintEtichette"></asp:TreeNode>
                <asp:TreeNode Text="Morosità" Value="Morosita"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Gest.Tabelle" Value="Gest.Tabelle" Expanded="False" SelectAction="Expand">
                <asp:TreeNode Text="B. Manager" Value="BManager"></asp:TreeNode>
                <asp:TreeNode Text="Delegati" Value="Delegati"></asp:TreeNode>
                <asp:TreeNode Text="Voci Spesa" Value="VSpesa"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="Expand" Text="Contributo Calore" 
                Value="Contributo Calore">
                <asp:TreeNode Text="Def. Parametri" Value="Definizione"></asp:TreeNode>
                <asp:TreeNode Text="Preventivo" Value="Preventivo" Expanded="False" 
                    SelectAction="Expand">
                    <asp:TreeNode Text="Elabora" Value="ElAvDiritto"></asp:TreeNode>
                    <asp:TreeNode Text="El. Anomalie" Value="Anomalie"></asp:TreeNode>
                    <asp:TreeNode Text="Approva" Value="Approva"></asp:TreeNode>
                    <asp:TreeNode Text="El. Approvati" Value="ElApprovati"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Expanded="False" SelectAction="Expand" Text="Consuntivo" 
                    Value="Consuntivo">
                    <asp:TreeNode Text="Elabora" Value="ElCons"></asp:TreeNode>
                    <asp:TreeNode Text="El. Anomalie" Value="AnomalieCons"></asp:TreeNode>
                    <asp:TreeNode Text="Approva" Value="ApprovaCons"></asp:TreeNode>
                    <asp:TreeNode Text="El. Approvati" Value="ElApprovatiCons"></asp:TreeNode>
                </asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Eventi Patrimoniali" Value="PatEvent"></asp:TreeNode>
            <asp:TreeNode Text="Guida" Value="Guida" NavigateUrl="~/Condomini/GuidaCondomini.pdf"
                Target="_blank" ToolTip="Guida Operativa Condomini"></asp:TreeNode>
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
