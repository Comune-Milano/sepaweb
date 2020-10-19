<%@ Page Language="VB" AutoEventWireup="false" Inherits="CM.menu" CodeFile="./menu_sinistra.aspx.vb"
    EnableSessionState="ReadOnly" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>menu</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
</head>
<body background="../NuoveImm/comeblu_sx.jpg">
    <form id="Form1" method="post" runat="server">
    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
        Style="z-index: 100; left: 4px; position: absolute; top: 568px" Text="Label"
        Width="106px" ForeColor="#721C1F"></asp:Label>
    &nbsp;&nbsp;&nbsp;
    <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px"
        Style="z-index: 104; left: 3px; position: absolute; top: 97px" Width="110px"
        ImageSet="Arrows">
        <LevelStyles>
            <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                ForeColor="#721C1F" HorizontalPadding="1px" />
            <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                HorizontalPadding="3px" />
            <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                HorizontalPadding="3px" />
        </LevelStyles>
        <Nodes>
            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Disponibilit&#224;"
                Value="Disponibilita">
                <asp:TreeNode Text="UI Gestore" Value="ALER"></asp:TreeNode>
                <asp:TreeNode Text="UI Comune" Value="COMUNE"></asp:TreeNode>
                <asp:TreeNode Text="Elenco D.Comune" ToolTip="Elenco Disponibilit&#224; Unit&#224; Comune"
                    Value="ElencoComune"></asp:TreeNode>
                <asp:TreeNode Text="Stato Disp. U.I." Value="StDispUI"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Gest.Inviti" Value="Gest.Inviti">
                <asp:TreeNode Text="Inviti" ToolTip="Questa funzione ha lo scopo di invitare gli utenti per l'offerta di un alloggio"
                    Value="1"></asp:TreeNode>
                <asp:TreeNode Text="Preferenze" ToolTip="Questa funzione permette di specificare le eventuali preferenze espresse dall'utente"
                    Value="Preferenze"></asp:TreeNode>
                <asp:TreeNode Text="Ann./Restit." ToolTip="Annullo Invito o Restituzione alloggio al Comune"
                    Value="Annullo"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Procedim.Assegn."
                Value="Proposte">
                <asp:TreeNode Text="Abbinamento" ToolTip="Tramite questa funzione &#232; possibile abbinare un alloggio ad un utente invitato"
                    Value="2"></asp:TreeNode>
                <asp:TreeNode NavigateUrl="../ASS/Abb_Automatico_p1.aspx" Text="Abb.Automatico" ToolTip="Abbinamento Automatico"
                    Value="Automatico" Target="_blank"></asp:TreeNode>
                <asp:TreeNode SelectAction="SelectExpand" Text="Abb. fuori Milano" Value="Abb. fuori Milano">
                    <asp:TreeNode Text="Inser. Dich." Value="inserimDich" ToolTip="Inserimento Dichiarazione">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Ricerca Dich." Value="ricercaDich" ToolTip="Ricerca Dichiarazione">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Abb. fuori MI" Value="abbFuori" ToolTip="Abbinamento"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Lista Abb.Autom." Value="report"></asp:TreeNode>
                <asp:TreeNode Text="Flusso Assegn." Value="iter_decisioni"></asp:TreeNode>
                <asp:TreeNode Text="Inserim. Motivaz." Value="modMotivazioni"></asp:TreeNode>
                <%--<asp:TreeNode Text="Annullo Prop." ToolTip="Annullo Proposta" Value="AnnulloProposta">
                </asp:TreeNode>
                <asp:TreeNode Text="Accetta/Rifiuta" ToolTip="Accetta o Rifiuta una proposta" Value="3">
                </asp:TreeNode>
                <asp:TreeNode Text="Annullo Acc." ToolTip="Annullo Accettazione" Value="AnnulloAcc">
                </asp:TreeNode>--%>
            </asp:TreeNode>
            <%--<asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Provvedimenti" Value="Provvedimenti">
                <asp:TreeNode Text="Assegnazione" Value="Assegnazione"></asp:TreeNode>
                <asp:TreeNode Text="Revoca" Value="Revoca" NavigateUrl="javascript:alert('Non disponibile');">
                </asp:TreeNode>
            </asp:TreeNode>--%>
            <asp:TreeNode Text="Contratto" Value="4"></asp:TreeNode>
            <asp:TreeNode NavigateUrl="~/ASS/Situazione.aspx" Target="_blank" Text="Info Situazione"
                Value="200"></asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Sistema" Value="8">
                <asp:TreeNode NavigateUrl="~/Bando.aspx" Target="_blank" Text="Par. Bando" Value="9">
                </asp:TreeNode>
                <asp:TreeNode NavigateUrl="~/regolamento n.1_140306.pdf" Target="_blank" Text="Normativa" 
                    Value="10"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode NavigateUrl="~/ASS/ReportOfferteAbbinamenti.aspx" Target="_blank" Text="RPT Offerte/Abbin." 
                Value="1000"></asp:TreeNode>
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
