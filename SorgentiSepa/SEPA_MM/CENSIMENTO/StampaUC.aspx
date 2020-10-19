<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StampaUC.aspx.vb" Inherits="CENSIMENTO_StampaUC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Stampa</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
            Style="z-index: 100; left: 248px; position: absolute; top: 16px" Width="304px">DATI RIEPILOGATIVI UNITA' COMUNE</asp:Label>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 160px" Width="112px">Tipologia Unità Comune</asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 224px" Width="152px">Localizzazione Unità Comune</asp:Label>
        <asp:Label ID="lbllocalizzazione" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 200px; position: absolute; top: 224px"
            Width="344px"></asp:Label>
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 104px" Width="56px">Edificio</asp:Label>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 288px" Width="96px">Numero Piani Scale</asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 72px" Width="56px">Complesso</asp:Label>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 192px" Width="128px">Disponibilità Unità Comune</asp:Label>
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 256px" Width="128px">Numero Piani Ascensore</asp:Label>
        <asp:Label ID="lblcomplesso" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 96px; position: absolute; top: 72px"
            Width="312px"></asp:Label>
        <asp:Label ID="lbledificio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 96px; position: absolute; top: 104px" Width="312px"></asp:Label>
        <asp:Label ID="lbltipoUnita" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 200px; position: absolute; top: 160px"
            Width="344px"></asp:Label>
        &nbsp;
        <asp:Label ID="lbldisponib" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 200px; position: absolute; top: 192px" Width="344px"></asp:Label>
        <asp:Label ID="LBLNUMPIANIASC" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 200px; position: absolute; top: 256px"
            Width="344px"></asp:Label>
        <asp:Label ID="lblnumpianiSC" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 200px; position: absolute; top: 288px" Width="344px"></asp:Label>
        <asp:Label ID="Label41" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 136px" Width="32px">Cod.</asp:Label>
        <asp:Label ID="LblCod" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 80px; position: absolute; top: 136px" Width="112px"></asp:Label>
        <asp:Label ID="Label42" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 8px; position: absolute; top: 311px" Width="40px">DIMENSIONI</asp:Label>
        <hr style="left: 72px; width: 576px; position: absolute; top: 319px; height: 1px" />
        <asp:Label ID="lblDimensioni" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Height="104px" Style="z-index: 100; left: 32px; position: absolute;
            top: 335px" Width="560px"></asp:Label>
    
    </div>
    </form>
                <script type="text/javascript">
                self.focus();
            </script>
</body>
</html>
