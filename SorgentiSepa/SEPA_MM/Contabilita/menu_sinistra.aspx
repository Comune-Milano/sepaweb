<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_sinistra.aspx.vb" Inherits="CENSIMENTO_menu_sinistra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>menu</title>
</head>
<body background="../NuoveImm/comeblu_sx.jpg">
    <form id="Form1" method="post" runat="server">
    &nbsp; &nbsp;
    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
        Style="z-index: 104; left: 5px; position: absolute; top: 552px" Text="Label"
        Width="106px" ForeColor="#721C1F"></asp:Label>
    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
    <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Height="10px"
        Style="z-index: 110; left: 2px; position: absolute; top: 96px" Width="106px"
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
            <asp:TreeNode Text="Ragioneria" Value="Ragioneria" Expanded="False" SelectAction="SelectExpand">
                <asp:TreeNode SelectAction="SelectExpand" Text="Gest. Tabelle" Value="Gest. Tabelle"
                    Expanded="False">
                    <asp:TreeNode Text="Anno Comp. Accert." Value="ACompAcert"></asp:TreeNode>
                    <asp:TreeNode Text="Tipo Inc. ExtraMav" Value="TipoIncExtra"></asp:TreeNode>
                    <asp:TreeNode Text="Tipo Inc. Ruoli" Value="TipoIncRuoli"></asp:TreeNode>
                    <asp:TreeNode Text="Capitoli" Value="Capitoli"></asp:TreeNode>
                    <asp:TreeNode Text="Categorie/Capitoli" Value="ValCapitoli"></asp:TreeNode>
                    <asp:TreeNode Text="Comp. Voci" Value="CompVoci"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Inserisci Pag." Value="InserisciPag" ToolTip="Inserimento manuale pagamenti">
                </asp:TreeNode>
                <asp:TreeNode Text="Rpt. Emissioni" Value="RptEmissioni"></asp:TreeNode>
                <asp:TreeNode Text="Annulla Accert." ToolTip="Annulla Accertamento" Value="RimuoviAccertamento">
                </asp:TreeNode>
<asp:TreeNode Text="Verifica Morosità" Value="Verifica Mor." Expanded="False" 
                    SelectAction="SelectExpand">
                <asp:TreeNode Text="Rpt. Morosità" Value="RptMorosita"></asp:TreeNode>
                <asp:TreeNode Text="Rpt. Emesso" Value="RptEmesso"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Elenco Rpt." Value="ElencoEstrazioni"></asp:TreeNode>
                <asp:TreeNode Text="Rpt. Incassi" Value="RptIncassi"></asp:TreeNode>
                <asp:TreeNode Text="Rpt. Incassi Ruoli" Value="RptIncassiR"></asp:TreeNode>
                <asp:TreeNode Text="Rpt.Residui" Value="RptResidui"></asp:TreeNode>
                <asp:TreeNode Text="Elenco Residui" Value="ElencoResidui"></asp:TreeNode>
                <asp:TreeNode Text="Rate Emesse" Value="Rate Emesse" SelectAction="SelectExpand"
                    Expanded="False">
                    <asp:TreeNode Text="Distinta" Value="RateEmesse"></asp:TreeNode>
                    <asp:TreeNode Text="Sing. Voci" ToolTip="Pagamenti NON Pervenuti per singole voci"
                        Value="DSingoleVoci"></asp:TreeNode>
                    <asp:TreeNode Text="Accertato" Value="Accertato"></asp:TreeNode>
                    <asp:TreeNode Text="Allegati" Value="Allegati"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Expanded="False" SelectAction="SelectExpand" Text="Pag. Pervenuti"
                    Value="Pagam. Pervenuti" ToolTip="Pagamenti Pervenuti">
                    <asp:TreeNode Text="Distinta" Value="DataEmissione" ToolTip="Distinta Pagamenti Pervenuti">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Sing. Voci" Value="SingoleVoci" ToolTip="Pagamenti Pervenuti per singole voci">
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode SelectAction="SelectExpand" Text="Pag. Non Perv." Value="NonPervenuti"
                    Expanded="False" ToolTip="Pagamenti NON Pervenuti">
                    <asp:TreeNode Text="Distinta" Value="NPDataEmissione" ToolTip="Distinta pagamenti NON pervenuti">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Sing.Voci" Value="SingVociNon"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Report Pagam." Value="Report"></asp:TreeNode>
                <asp:TreeNode Text="Solleciti" Value="Solleciti"></asp:TreeNode>
                <asp:TreeNode SelectAction="Expand" Text="Incassi Non Attr." Value="Gest. Assegni">
                    <asp:TreeNode Text="Nuovo" Value="NuovoAssegno"></asp:TreeNode>
                    <asp:TreeNode Text="Ricerca" Value="RicercaAssegno"></asp:TreeNode>
                </asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Expanded="False" Text="Cons.Patrimoniali" Value="Cons.Patrimoniali"
                SelectAction="SelectExpand">
                <asp:TreeNode Text="Oneri" Value="Oneri"></asp:TreeNode>
                <asp:TreeNode Text="Media" Value="Media"></asp:TreeNode>
                <asp:TreeNode Text="Prop.Manag." Value="PropManage"></asp:TreeNode>
                <asp:TreeNode Text="Tot x Tipo" Value="patrTipoUI"></asp:TreeNode>
                <asp:TreeNode Text="Tot x Stato" Value="patrStatoUI"></asp:TreeNode>
                <asp:TreeNode Text="TOT x Gruppi" Value="patrGruppiUI"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Flussi Finanz." Value="Flussi"></asp:TreeNode>
            <asp:TreeNode Text="Rimb. Sp. Gestore" Value="a" Expanded="False" SelectAction="Expand">
                <asp:TreeNode Text="Report" Value="CompALER"></asp:TreeNode>
                <asp:TreeNode Text="Gestione" Value="Gestione"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Prelievi" Value="Prelievi"></asp:TreeNode>
            <asp:TreeNode Text="Compensi Gestore" Value="Facility"></asp:TreeNode>
            <asp:TreeNode Expanded="False" SelectAction="Expand" Text="Rend.Pagamenti" ToolTip="Rendicontazione Pagamenti"
                Value="RendPagamenti">
                <asp:TreeNode Text="Anomalie Rend." ToolTip="Anomalie Rendicontazione" Value="AnomalieRend">
                </asp:TreeNode>
                <asp:TreeNode Text="Log Rend." ToolTip="Log Rendicontazione" Value="LogRend"></asp:TreeNode>
            </asp:TreeNode>
        </Nodes>
        <NodeStyle BorderStyle="None" Font-Names="arial" Font-Size="8pt" ForeColor="Black"
            HorizontalPadding="1px" NodeSpacing="0px" VerticalPadding="0px" />
        <ParentNodeStyle Font-Bold="False" />
        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" Font-Names="Arial" Font-Size="8pt" />
        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
            VerticalPadding="0px" />
    </asp:TreeView>
    </form>
    <p>
        .</p>
</body>
</html>
