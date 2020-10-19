<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettaglioEdificio.aspx.vb" Inherits="MANUTENZIONI_DettaglioEdificio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pagina senza titolo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <strong><span style="font-family: Arial">Riepilogo</span></strong>&nbsp;<strong><span
            style="font-family: Arial">Tabella Selezionata</span></strong><br />
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 8px; position: absolute; top: 96px"
            Width="74px">Cod. Edificio</asp:Label>
        <asp:Label ID="TabMillesimale" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 97px; position: absolute;
            top: 42px" Width="457px"></asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 8px; position: absolute; top: 42px"
            Width="74px">Tab.Millesimale</asp:Label>
        <asp:Label ID="descTab" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 96px; position: absolute; top: 69px"
            Width="456px"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblCodEdificio" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 97px; position: absolute;
            top: 96px" Width="239px"></asp:Label>
        <asp:Label ID="lblDenominazione" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 97px; position: absolute;
            top: 121px" Width="457px"></asp:Label>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 9px; position: absolute; top: 69px"
            Width="67px">Descrizione</asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 8px; position: absolute; top: 121px"
            Width="74px">Denominazione</asp:Label>
        <asp:Label ID="lblTotUnita" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 97px; position: absolute; top: 181px"
            Width="127px"></asp:Label>
        <asp:Label ID="lblTOT" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 8px; position: absolute; top: 181px"
            Width="70px">Totale Unità</asp:Label>
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 9px; position: absolute; top: 151px"
            Width="50px">Indirizzo</asp:Label>
        <asp:Label ID="lblIndirizzo" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 97px; position: absolute;
            top: 150px" Width="257px"></asp:Label>
        <asp:Label ID="lblCivico" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 359px; position: absolute; top: 149px"
            Width="69px"></asp:Label>
        <asp:Label ID="lblCap" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 433px; position: absolute; top: 149px"
            Width="49px"></asp:Label>
        <asp:Label ID="lblLocalita" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 487px; position: absolute; top: 149px"
            Width="69px"></asp:Label>
    </div>
    </form>
</body>
</html>
