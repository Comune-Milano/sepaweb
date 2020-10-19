<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_RicercaINS_1.ascx.vb"
    Inherits="Tab_RicercaINS_1" %>
<style type="text/css">
    .style1
    {
        height: 20px;
        width: 181px;
    }
    .style2
    {
        width: 181px;
    }
    .style3
    {
        width: 181px;
        height: 23px;
    }
    .style4
    {
        height: 23px;
    }
</style>
<link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
<table class="FontTelerik">
    <tr>
        <td style="vertical-align: middle; text-align: left;" class="TitoloH1">
            Ricerca Ubicazione per:
        </td>
        <td>
            <asp:RadioButtonList ID="RBL1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                Width="200px">
                <asp:ListItem Selected="True">Complesso</asp:ListItem>
                <asp:ListItem Value="Edificio"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td style="width: 330px;">
        </td>
    </tr>
    <asp:HiddenField ID="HiddenField1" runat="server" />
</table>
<table style="width: 100%" class="FontTelerik">
    <tr>
        <td class="style1">
        </td>
        <td style="height: 20px">
        </td>
    </tr>
    <tr>
        <td style="width: 10%">
            <asp:Label ID="Label2" runat="server">Esercizio Finanziario*</asp:Label>
        </td>
        <td>
            <telerik:RadComboBox ID="cmbEsercizio" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td class="style2">
            <asp:Label ID="lblTipoServizio" runat="server">Servizio*</asp:Label>
        </td>
        <td>
            <telerik:RadComboBox ID="cmbServizio" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td class="style2">
            <asp:Label ID="lblTipoServizioDett" runat="server">Voce DGR</asp:Label>
        </td>
        <td>
            <telerik:RadComboBox ID="cmbServizioVoce" Width="40%" AppendDataBoundItems="true"
                Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td class="style2">
            <asp:Label ID="LblAppalto" runat="server">Num. Repertorio</asp:Label>
        </td>
        <td>
            <telerik:RadComboBox ID="cmbAppalto" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td class="style2">
            <asp:Label ID="LblComplesso" runat="server">Complesso</asp:Label>
        </td>
        <td>
            <telerik:RadComboBox ID="cmbComplesso" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td class="style2">
            <asp:Label ID="LblEdificio" runat="server">Edificio</asp:Label>
        </td>
        <td>
            <telerik:RadComboBox ID="cmbEdificio" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td class="style2">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top; text-align: left" class="style2">
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top; text-align: left" class="style3">
        </td>
        <td class="style4">
            &nbsp;
        </td>
    </tr>
    <asp:HiddenField ID="txtTIPO" runat="server"></asp:HiddenField>
</table>
