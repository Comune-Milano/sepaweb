<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_RicercaINS_2.ascx.vb"
    Inherits="Tab_RicercaINS_2" %>
<style type="text/css">
    .style1
    {
        height: 26px;
    }
    .style2
    {
        width: 40px;
        height: 26px;
    }
</style>
<script type="text/javascript" src="prototype.lite.js"></script>
<script type="text/javascript" src="moo.fx.js"></script>
<script type="text/javascript" src="moo.fx.pack.js"></script>
<table>
    <tr>
        <td style="vertical-align: middle; text-align: left;" class="TitoloH1">
            <asp:Label ID="lblSottoTitolo" runat="server" Font-Bold="True"  Style="z-index: 100; left: 48px; top: 64px"
                Width="200px">Ricerca Ubicazione per:</asp:Label>
        </td>
        <td>
            <asp:RadioButtonList ID="RBL1" runat="server" AutoPostBack="True" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Black" RepeatDirection="Horizontal" Width="200px">
                <asp:ListItem Selected="True">Complesso</asp:ListItem>
                <asp:ListItem Value="Edificio"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td style="width: 330px">
        </td>
    </tr>
</table>
<table style="width: 100%" class="FontTelerik">
    <tr>
        <td style="height: 20px" width="10%">
        </td>
        <td style="height: 20px">
        </td>
        <td style="width: 40px; height: 20px">
        </td>
    </tr>
    <tr>
        <td style="height: 20px" width="10%">
            <asp:Label ID="Label1" runat="server" Style="z-index: 100; left: 48px; top: 32px"
                Width="130px">Esercizio Finanziario*</asp:Label>
        </td>
        <td style="height: 20px">
            <telerik:RadComboBox ID="cmbEsercizio" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
        <td style="width: 40px; height: 20px">
        </td>
    </tr>
    <tr>
        <td width="10%">
            <asp:Label ID="lblIndirizzo" runat="server" Style="z-index: 100; left: 48px; top: 96px"
                Width="130px">Indirizzo*</asp:Label>
        </td>
        <td>
            <telerik:RadComboBox ID="cmbIndirizzo" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
        <td style="width: 40px;">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" width="10%">
            <asp:Label ID="lblVoce" runat="server" Style="z-index: 100; left: 48px; top: 32px"
                Width="130px">Tipo Voce*</asp:Label>
        </td>
        <td class="style1">
            <telerik:RadComboBox ID="cmbVoce" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
        <td class="style2">
        </td>
    </tr>
    <tr>
        <td style="height: 21px" width="10%">
            <asp:Label ID="LblAppalto" runat="server" Style="z-index: 100; left: 48px; top: 64px"
                Width="130px">Num. Repertorio*</asp:Label>
        </td>
        <td style="height: 21px">
            <telerik:RadComboBox ID="cmbAppalto" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
        <td style="height: 21px; width: 40px;">
        </td>
    </tr>
    <tr>
        <td width="10%">
        </td>
        <td>
        </td>
        <td style="width: 40px">
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top; text-align: left" width="10%">
        </td>
        <td>
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" ForeColor="Red" Style="left: 152px;
                top: 304px" Text="Label" Visible="False" Width="300px"></asp:Label>
        </td>
        <td style="width: 40px">
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top; text-align: left" width="10%">
        </td>
        <td>
            &nbsp;
        </td>
        <td style="width: 40px">
        </td>
    </tr>
    <asp:HiddenField ID="TextBox1" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtTIPO" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtSTATO_PF" runat="server"></asp:HiddenField>
</table>
<script type="text/javascript">

    //    myOpacity = new fx.Opacity('AiutoRicerca', { duration: 200 });
    //    //myOpacity.hide();

    //    if (document.getElementById('Tab_Ricerca2_TextBox1').value != '2') {
    //        myOpacity.hide(); ;
    //    }
        
</script>
