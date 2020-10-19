<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_sinistra.aspx.vb" Inherits="CENSIMENTO_menu_sinistra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>menu</title>
    <script src="js/jsFunzioni.js" type="text/javascript"></script>
</head>
<body background="IMMCENSIMENTO/SX.jpg">
    <form id="Form1" method="post" runat="server">
    &nbsp; &nbsp;
    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
        Style="z-index: 104; left: 5px; position: absolute; top: 552px" Text="Label"
        Width="106px" ForeColor="#721C1F"></asp:Label>
    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
    <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px"
        Style="z-index: 110; left: 2px; position: absolute; top: 96px" Width="106px"
        ImageSet="Arrows" TabIndex="-1">
        <Nodes>
            <asp:TreeNode SelectAction="Expand" Text="Complessi" Value="0" Expanded="False">
                <asp:TreeNode Text="Ricerca" Value="4"></asp:TreeNode>
                <asp:TreeNode Text="Inserisci" Value="10"></asp:TreeNode>
                <asp:TreeNode Text="Elenco" Value="ElencoComp"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode SelectAction="Expand" Text="Edifici" Value="1" Expanded="False">
                <asp:TreeNode Text="Ricerca" Value="5"></asp:TreeNode>
                <asp:TreeNode Text="Inserisci" Value="11"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode SelectAction="Expand" Text="Unit&#224; Immob." Value="2" Expanded="False">
                <asp:TreeNode Text="Ricerca" Value="7" Expanded="False" SelectAction="SelectExpand">
                    <asp:TreeNode Text="Diretta" Value="Diretta"></asp:TreeNode>
                    <asp:TreeNode Text="Selettiva" Value="Selettiva"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Target="_blank" Text="Inserisci" Value="6"></asp:TreeNode>
                <asp:TreeNode Text="Caric. Massivo" Value="Caric_Massivo" SelectAction="Expand"> 
                    <asp:TreeNode Text="Progr. Intervento" Value="Progr_Intervento"></asp:TreeNode>
                </asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Occupanti" Value="Occupante"></asp:TreeNode>
            <asp:TreeNode Expanded="False" Text="Unit&#224; Comuni" Value="3" SelectAction="Expand">
                <asp:TreeNode Text="Ricerca" Value="9"></asp:TreeNode>
                <asp:TreeNode Text="Inserisci" Value="8"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Report" Value="4" Expanded="False" SelectAction="Expand">
                <asp:TreeNode Text="Occupazione" Value="Report"></asp:TreeNode>
                <asp:TreeNode Text="U.I. Libere" Value="UiDisp" Expanded="False" SelectAction="Expand">
                    <asp:TreeNode Text="Tec. Disd." Value="TecDis"></asp:TreeNode>
                    <asp:TreeNode Text="Tec. Disp." Value="Tecnico"></asp:TreeNode>
                    <asp:TreeNode Text="Amministr." Value="Amministrativo"></asp:TreeNode>
                    <asp:TreeNode Text="Immediatam.Disp" Value="AmmNuovaAssegn"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="U.I. Inag." Value="UiInag"></asp:TreeNode>
                <asp:TreeNode Text="U.I. Non Acc." Value="NonAcc"></asp:TreeNode>
                <asp:TreeNode Text="TOT x Tipo" Value="patrTipoUI"></asp:TreeNode>
                <asp:TreeNode Text="TOT x Stato" Value="patrStatoUI"></asp:TreeNode>
                <asp:TreeNode Text="TOT x Gruppi" Value="patrGruppiUI"></asp:TreeNode>
                <asp:TreeNode Text="App.Sloggio" Value="App.Sloggio"></asp:TreeNode>
                <asp:TreeNode Text="U.I. - Contratti" 
                    ToolTip="Dati Anagrafico-Contrattuali per UI" Value="RptUiRu">
                </asp:TreeNode>
                <asp:TreeNode Text="U.I. - Sottosoglia" 
                    ToolTip="Dati Anagrafico-Sottosoglia per UI" Value="RptUiRuSoglia">
                </asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Cens. Stato Manut" Value="CSM" Expanded="False" SelectAction="SelectExpand">
                <asp:TreeNode Text="Nuovo" Value="ElencoManut"></asp:TreeNode>
                <asp:TreeNode Text="Ricerca Immobili" Value="RicSchede"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Parametri" 
                Value="Parametri">
                <asp:TreeNode Text="Rif.Le.Edifici" ToolTip="Riferimenti Legislativi Edifici" 
                    Value="RifLeEdifici"></asp:TreeNode>
                <asp:TreeNode Text="Progr.Interventi" ToolTip="Programmazione interventi" 
                    Value="ProgrInterventi"></asp:TreeNode>
                <asp:TreeNode Text="Dest.Uso RL" ToolTip="Dest.Uso RL" Value="DestUsoRL">
                </asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Assegn. Sedi T." Value="ASSFILIALI"></asp:TreeNode>
 
            <asp:TreeNode NavigateUrl="~/CENSIMENTO/GuidaCensimento.pdf" Target="_blank" Text="Guida"
                ToolTip="Guida operativa" Value="Guida"></asp:TreeNode>
        </Nodes>
        <LevelStyles>
            <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                ForeColor="#721C1F" HorizontalPadding="1px" />
            <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                HorizontalPadding="3px" />
        </LevelStyles>
        <NodeStyle BorderStyle="None" Font-Names="arial" Font-Size="8pt" ForeColor="Black"
            HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
        <ParentNodeStyle Font-Bold="False" />
        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" Font-Names="Arial" Font-Size="9pt" />
        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
            VerticalPadding="0px" />
    </asp:TreeView>
    </form>
</body>
</html>
