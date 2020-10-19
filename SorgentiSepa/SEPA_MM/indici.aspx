<%@ Page Language="VB" AutoEventWireup="false" CodeFile="indici.aspx.vb" Inherits="indici1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Indici Patrimoniali</title>
</head>
<body style="font-size: 12pt; font-family: Times New Roman" background="NuoveImm/SfondoStatoDomanda.jpg">
    <table align="center" cellpadding="0" cellspacing="0" style="z-index: 102; left: 10px;
        width: 450px; position: absolute; top: 11px; height: 408px">
        <tr>
            <td style="width: 616px; height: 24px; color: #660000;">
                <img id="STAMPA" onclick="javascript:window.print();" src="IMG/print.jpg" style="z-index: 100; left: 431px; position: absolute;
                    top: 0px; cursor: pointer;" alt="Stampa questa finestra" />
                <span style="font-size: 16pt; font-family: Arial"><strong>Indici</strong></span></td>
        </tr>
        <tr>
            <td style="width: 616px; color: #660000; height: 24px">
                <asp:Label ID="Label13" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 122; left: 81px; position: static; top: 61px"
                    Text="Label" ForeColor="#000000" Width="190px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 616px; color: #660000; height: 24px">
                <table width="100%">
                    <tr>
                        <td style="width: 197px; height: 21px">
                <asp:Label ID="Label2" runat="server" Style="z-index: 100; left: 14px; position: static;
                    top: 103px; text-align: left" Text="ISE-Erp" Width="158px" ForeColor="#000000"></asp:Label></td>
                        <td style="height: 21px">
                <asp:Label ID="L1" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 101; left: 212px; position: static; top: 48px"
                    Text="Label" ForeColor="#000000" Width="45px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 197px">
                <asp:Label ID="Label3" runat="server" Style="z-index: 102; left: 19px; position: static;
                    top: 112px; text-align: left" Text="ISEE-Erp" Width="104px" ForeColor="#000000"></asp:Label></td>
                        <td>
                <asp:Label ID="L2" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 109; left: 193px; position: static; top: 94px"
                    Text="Label" ForeColor="#000000" Width="45px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 197px">
                <asp:Label ID="Label4" runat="server" Style="z-index: 107; left: 20px; position: static;
                    top: 148px; text-align: left" Text="ISP-ERP" Width="104px" ForeColor="#000000"></asp:Label></td>
                        <td>
                <asp:Label ID="L3" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 108; left: 154px; position: static; top: 148px"
                    Text="Label" ForeColor="#000000" Width="45px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 197px">
                <asp:Label ID="Label5" runat="server" Style="z-index: 105; left: -51px; position: static;
                    top: -12px; text-align: left" Text="ISR-ERP" Width="76px" ForeColor="#000000"></asp:Label></td>
                        <td>
                <asp:Label ID="L4" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 106; left: 154px; position: static; top: 172px"
                    Text="Label" ForeColor="#000000" Width="45px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 197px">
                <asp:Label ID="Label14" runat="server" Style="z-index: 123; left: 20px; position: static;
                    top: 197px; text-align: left" Text="PSE" Width="76px" ForeColor="#000000"></asp:Label></td>
                        <td>
                <asp:Label ID="LBLPSE" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 124; left: 154px; position: static; top: 197px"
                    Text="Label" ForeColor="#000000" Width="44px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 197px">
                <asp:Label ID="Label15" runat="server" Style="z-index: 126; left: 272px; position: static;
                    top: 197px; text-align: left" Text="VSE" Width="76px" ForeColor="#000000"></asp:Label></td>
                        <td>
                <asp:Label ID="LBLVSE" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 127; left: 276px; position: static; top: 161px"
                    Text="Label" ForeColor="#000000" Width="43px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 197px">
                <asp:Label ID="Label6" runat="server" Style="z-index: 103; left: 20px; position: static;
                    top: 221px; text-align: left" Text="Disagio Abitativo" Width="119px" ForeColor="Black"></asp:Label></td>
                        <td>
                <asp:Label ID="L5" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 104; left: 154px; position: static; top: 221px"
                    Text="Label" ForeColor="Black" Width="47px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 197px">
                <asp:Label ID="Label7" runat="server" Style="z-index: 110; left: 20px; position: static;
                    top: 246px; text-align: left" Text="Disagio Economico" Width="119px" ForeColor="Black"></asp:Label></td>
                        <td>
                <asp:Label ID="L6" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 111; left: 201px; position: static; top: 213px"
                    Text="Label" ForeColor="Black" Width="45px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 197px">
                <asp:Label ID="Label8" runat="server" Style="z-index: 112; left: 20px; position: static;
                    top: 271px; text-align: left" Text="Disagio Familiare" Width="142px" ForeColor="Black"></asp:Label></td>
                        <td>
                <asp:Label ID="L7" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 114; left: 199px; position: static; top: 235px"
                    Text="Label" ForeColor="Black" Width="44px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 197px">
                <asp:Label ID="Label1" runat="server" Style="z-index: 113; left: 20px; position: static;
                    top: 296px; text-align: left" Text="Disagio Residenza" Width="142px" ForeColor="Black"></asp:Label></td>
                        <td>
                <asp:Label ID="Label10" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 115; left: 154px; position: static; top: 296px"
                    Text="Label" Width="44px" ForeColor="Black"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 197px">
                <asp:Label ID="Label9" runat="server" Style="z-index: 116; left: 20px; position: static;
                    top: 324px; text-align: left" Text="ISBAR" Width="103px" ForeColor="Black"></asp:Label></td>
                        <td>
                <asp:Label ID="L8" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 117; left: 206px; position: static; top: 286px"
                    Text="Label" ForeColor="#000000" Width="48px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 197px">
                <asp:Label ID="Label11" runat="server" Style="z-index: 118; left: 20px; position: static;
                    top: 349px; text-align: left" Text="ISBARC" Width="149px" ForeColor="Black"></asp:Label></td>
                        <td>
                <asp:Label ID="L9" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 119; left: 209px; position: static; top: 306px"
                    Text="Label" ForeColor="#000000" Width="45px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 197px">
                <asp:Label ID="Label12" runat="server" Style="z-index: 120; left: 20px; position: static;
                    top: 375px; text-align: left" Text="ISBARC/R" Width="143px" ForeColor="Black"></asp:Label></td>
                        <td>
                <asp:Label ID="L10" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 121; left: 154px; position: static; top: 375px"
                    Text="Label" ForeColor="#000000" Width="43px"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />

</body>
</html>
