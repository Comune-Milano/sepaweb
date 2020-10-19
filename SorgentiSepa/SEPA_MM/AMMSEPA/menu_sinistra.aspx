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
    <style type="text/css">
        .style1
        {
            height: 95px;
        }
        .style2
        {
            height: 19px;
        }
        .style3
        {
            height: 50px;
        }
    </style>
</head>
<body bgcolor="#ffffff">
    <form id="Form1" method="post" runat="server">
    <table cellpadding="0" cellspacing="0" style="background-image: url(Immagini/comeblu_sx.jpg);
        left: 0px; position: absolute; top: 0px; height: 579px; background-repeat: no-repeat;"
        width="100%">
        <tr>
            <td class="style2">
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style3">
            </td>
        </tr>
        <tr style="height: 350px" valign="top">
            <td>
                <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px"
                    Style="z-index: 104; left: 246px; top: 273px" Width="119px" ImageSet="Arrows">
                    <LevelStyles>
                        <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                            ForeColor="#721C1F" HorizontalPadding="1px" />
                        <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                            HorizontalPadding="3px" />
                    </LevelStyles>
                    <Nodes>
                        <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Errori" Value="Errori">
                            <asp:TreeNode Text="Parametri" Value="ParamErrori"></asp:TreeNode>
                            <asp:TreeNode Text="Visualizza" Value="VisErrori"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Bollette" Value="Bollette">
                            <asp:TreeNode Text="Ripristina" Value="Ripristina"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Expanded="False" Text="Rend.Pagamenti" ToolTip="Rendicontazione Pagamenti"
                            Value="Rend.Pagamenti" SelectAction="SelectExpand">
                            <asp:TreeNode Text="Errori" Value="Errori"></asp:TreeNode>
                            <asp:TreeNode Text="Anomalie" Value="Anomalie"></asp:TreeNode>
                            <asp:TreeNode Text="Log" Value="Log"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Operatori" Value="1" Expanded="False" SelectAction="SelectExpand">
                            <asp:TreeNode ImageUrl="~/NuoveImm/Albero_2.gif" Text="Nuovo" Value="2"></asp:TreeNode>
                            <asp:TreeNode ImageUrl="~/NuoveImm/Albero_2.gif" Text="Ricerca" Value="3"></asp:TreeNode>
                            <asp:TreeNode Text="Revoca Tutti" ToolTip="Revoca Tutti gli operatori attivi in questo momento"
                                Value="RevocaTutti"></asp:TreeNode>
                            <asp:TreeNode Text="Ripristina Tutti" ToolTip="Ripristina tutti gli operatori revocati con funzione &quot;Revoca Tutti&quot;"
                                Value="AbilitaTutti"></asp:TreeNode>
                            <asp:TreeNode Target="_blank" Text="Stato" Value="Stato"></asp:TreeNode>
                            <asp:TreeNode Text="Log Ingressi" Value="LogIngressi"></asp:TreeNode>
                            <asp:TreeNode Text="Accesso RU" Value="AccessoRU"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Utenze Cont. Sol." Value="utenza_cont_solid"></asp:TreeNode>
                        <asp:TreeNode Text="Par. Password" Value="Password"></asp:TreeNode>
                        <asp:TreeNode Text="Commissariati" Value="Commissariati"></asp:TreeNode>
                        <asp:TreeNode Text="Strutture" Value="Filiali"></asp:TreeNode>
                        <asp:TreeNode Text="Quartieri" Value="Quartieri"></asp:TreeNode>
                        <asp:TreeNode Text="Zone" Value="Zone"></asp:TreeNode>
                        <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Comuni" Value="Comuni">
                            <asp:TreeNode Text="Nuovo" Value="NuovoComune"></asp:TreeNode>
                            <asp:TreeNode Text="Elenco" Value="ElencoComuni"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Gestione Eventi" Value="Gestione_Eventi"></asp:TreeNode>
                        <asp:TreeNode Text="Esci" Value="50"></asp:TreeNode>
                    </Nodes>
                    <NodeStyle BorderStyle="None" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black"
                        HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                        VerticalPadding="0px" />
                </asp:TreeView>
                <asp:TreeView ID="T2" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px"
                    Style="z-index: 104; left: 3px; top: 97px" Width="119px" ImageSet="Arrows">
                    <LevelStyles>
                        <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                            ForeColor="#721C1F" HorizontalPadding="1px" />
                        <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                            HorizontalPadding="3px" />
                    </LevelStyles>
                    <Nodes>
                        <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Operatori" Value="1">
                            <asp:TreeNode Text="Nuovo" Value="2" ImageUrl="~/NuoveImm/Albero_2.gif"></asp:TreeNode>
                            <asp:TreeNode Text="Ricerca" Value="3" ImageUrl="~/NuoveImm/Albero_2.gif"></asp:TreeNode>
                            <asp:TreeNode Text="Assegna Strutt." Value="15"></asp:TreeNode>
                            <asp:TreeNode Text="Revoca Tutti" Value="4"></asp:TreeNode>
                            <asp:TreeNode Text="Ripristina Tutti" Value="5"></asp:TreeNode>
                            <asp:TreeNode Text="Stato" Value="6" Target="_blank"></asp:TreeNode>
                            <asp:TreeNode Text="Log Ingressi" Value="7"></asp:TreeNode>
                            <asp:TreeNode Text="Accesso RU" Value="AccessoRU"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Commissariati" Value="8"></asp:TreeNode>
                        <asp:TreeNode Text="Strutture" Value="9"></asp:TreeNode>
                        <asp:TreeNode Text="Quartieri" Value="10"></asp:TreeNode>
                        <asp:TreeNode Text="Microzone" Value="Microzone"></asp:TreeNode>
                        <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Comuni" Value="11">
                            <asp:TreeNode Text="Nuovo" Value="12"></asp:TreeNode>
                            <asp:TreeNode Text="Elenco" Value="13"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="R.U. Abusivi" Value="RuAbusivi"></asp:TreeNode>
                        <asp:TreeNode Text="Esci" Value="50"></asp:TreeNode>
                    </Nodes>
                    <NodeStyle BorderStyle="None" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black"
                        HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                        VerticalPadding="0px" />
                </asp:TreeView>
                <asp:TreeView ID="T3" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px"
                    ImageSet="Arrows" Style="z-index: 104; left: 3px; top: 97px" Width="119px">
                    <LevelStyles>
                        <asp:TreeNodeStyle Font-Bold="True" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                            ForeColor="#721C1F" HorizontalPadding="1px" />
                        <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                            HorizontalPadding="3px" />
                    </LevelStyles>
                    <Nodes>
                        <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Operatori" Value="1">
                            <asp:TreeNode ImageUrl="~/NuoveImm/Albero_2.gif" Text="Nuovo" Value="2"></asp:TreeNode>
                            <asp:TreeNode ImageUrl="~/NuoveImm/Albero_2.gif" Text="Ricerca" Value="3"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Esci" Value="50"></asp:TreeNode>
                    </Nodes>
                    <NodeStyle BorderStyle="None" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black"
                        HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                        VerticalPadding="0px" />
                </asp:TreeView>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td align="center" style="text-align: left">
                &nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    Style="z-index: 100; left: 1px; position: static; top: 57px" Text="Label" Width="106px"
                    ForeColor="#721C1F"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp; &nbsp;<br />
                &nbsp;<br />
                &nbsp;<br />
                &nbsp;<br />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
