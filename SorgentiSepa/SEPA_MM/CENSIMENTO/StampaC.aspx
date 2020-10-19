<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StampaC.aspx.vb" Inherits="CENSIMENTO_Stampa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Stampa</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 109px" Width="85px">Tipo Complesso :</asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 157px" Width="161px">Codice Ubicazione legge 392/78 :</asp:Label>
        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 133px" Width="99px">Livello di Possesso :</asp:Label>
        <asp:Label ID="Lbl3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 72px" Width="32px">Lotto :</asp:Label>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 160px; position: absolute; top: 72px" Width="64px">Cod.Complesso :</asp:Label>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 368px; position: absolute; top: 72px" Width="48px">Cod.GIMI :</asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 181px" Width="64px">Den.Complesso :</asp:Label>
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 205px" Width="64px">Provenienza :</asp:Label>
        <asp:Label ID="lblprovenienza" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 196px; position: absolute; top: 205px"
            Width="396px"></asp:Label>
        &nbsp;
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
            Style="z-index: 100; left: 152px; position: absolute; top: 8px" Width="424px">DATI RIEPILOGATIVI COMPLESSO IMMOBILIARE</asp:Label>
        <asp:Label ID="lbllotto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 80px; position: absolute; top: 72px" Width="32px"></asp:Label>
        <asp:Label ID="lblcodcomplesso" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 248px; position: absolute; top: 72px"
            Width="64px"></asp:Label>
        <asp:Label ID="lblcodgimi" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 424px; position: absolute; top: 72px" Width="48px"></asp:Label>
        &nbsp;
        <asp:Label ID="lbltipocom" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 198px; position: absolute; top: 109px" Width="396px"></asp:Label>
        <asp:Label ID="lbllivposs" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 197px; position: absolute; top: 133px" Width="396px"></asp:Label>
        <asp:Label ID="lblcodubicaz" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 196px; position: absolute; top: 157px"
            Width="396px"></asp:Label>
        <asp:Label ID="lbldencomp" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 196px; position: absolute; top: 181px" Width="396px"></asp:Label>
        <hr style="left: 16px; width: 608px; position: absolute; top: 262px; height: 1px" />
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 376px; position: absolute; top: 278px" Width="24px">Civico :</asp:Label>
        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 264px; position: absolute; top: 302px" Width="24px">CAP</asp:Label>
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 302px" Width="40px">Comune</asp:Label>
        <asp:Label ID="lblcomune" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 88px; position: absolute; top: 302px" Width="136px"></asp:Label>
        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 326px" Width="1px">Località :</asp:Label>
        <asp:Label ID="lblvia" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 278px" Width="320px"></asp:Label>
        <asp:Label ID="lblcivico" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 424px; position: absolute; top: 278px" Width="24px"></asp:Label>
        <asp:Label ID="lblcap" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 312px; position: absolute; top: 302px" Width="64px"></asp:Label>
        <asp:Label ID="lbllocalita" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 88px; position: absolute; top: 326px" Width="136px"></asp:Label><hr style="left: 16px; width: 608px; position: absolute; top: 350px; height: 1px" />
        &nbsp;
        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Height="104px" Style="z-index: 100; left: 32px; position: absolute; top: 374px"
            Width="560px" Visible="False"></asp:Label>
        <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 478px" Width="192px" Visible="False">UTENZE MILLESIMALI</asp:Label>
        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Height="104px" Style="z-index: 100; left: 32px; position: absolute; top: 494px"
            Width="560px"></asp:Label>
        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 225px" Width="64px">Commissariato:</asp:Label>
        <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: absolute; top: 244px" Width="33px">Filiale:</asp:Label>
        <asp:Label ID="lblCommissariato" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 195px; position: absolute; top: 225px"
            Width="397px"></asp:Label>
        <asp:Label ID="lblFiliale" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 194px; position: absolute; top: 244px" Width="398px"></asp:Label>
    
    </div>
    </form>
                <script type="text/javascript">
                self.focus();
            </script>
</body>
</html>
