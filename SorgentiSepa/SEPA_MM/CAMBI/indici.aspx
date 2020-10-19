<%@ Page Language="VB" AutoEventWireup="false" CodeFile="indici.aspx.vb" Inherits="CAMBI_indici" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Indici Patrimoniali</title>
</head>
<body style="font-size: 12pt; font-family: Times New Roman; background-attachment: fixed; background-image: url(../NuoveImm/SfondoStatoDomanda.jpg); background-repeat: no-repeat;">
    <table align="center" cellpadding="0" cellspacing="0" style="z-index: 102; left: 10px;
        width: 450px; position: absolute; top: 11px; height: 408px">
        <tr>
            <td style="width: 616px; height: 24px; color: #660000;">
                <img id="STAMPA" onclick="javascript:window.print();" src="../IMG/print.jpg" style="z-index: 100; left: 419px; position: absolute;
                    top: 11px; cursor: pointer;" alt="Stampa questa finestra" />
                <span style="font-size: 16pt; font-family: Arial"><strong>Indici</strong></span></td>
        </tr>
        <tr>
            <td style="border-right: dimgray 1px solid; border-left: dimgray 1px solid;
                width: 616px; border-bottom: dimgray 1px solid; height: 284px; text-align: center"
                valign="top">
                <asp:Label ID="Label2" runat="server" Style="z-index: 100; left: 20px; position: absolute;
                    top: 97px; text-align: left" Text="ISE-Erp" Width="158px"></asp:Label>
                <asp:Label ID="L1" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 101; left: 154px; position: absolute; top: 97px"
                    Text="Label"></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" Style="z-index: 102; left: 20px; position: absolute;
                    top: 122px; text-align: left" Text="ISEE-Erp" Width="104px"></asp:Label>
                <asp:Label ID="Label6" runat="server" Style="z-index: 103; left: 20px; position: absolute;
                    top: 221px; text-align: left" Text="Disagio Abitativo" Width="119px"></asp:Label>
                <asp:Label ID="L5" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 104; left: 154px; position: absolute; top: 221px"
                    Text="Label"></asp:Label>
                <asp:Label ID="Label5" runat="server" Style="z-index: 105; left: 20px; position: absolute;
                    top: 172px; text-align: left" Text="ISR-ERP" Width="76px"></asp:Label>
                <asp:Label ID="L4" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 106; left: 154px; position: absolute; top: 172px"
                    Text="Label"></asp:Label>
                <asp:Label ID="Label4" runat="server" Style="z-index: 107; left: 20px; position: absolute;
                    top: 148px; text-align: left" Text="ISP-ERP" Width="104px"></asp:Label>
                <asp:Label ID="L3" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 108; left: 154px; position: absolute; top: 148px"
                    Text="Label"></asp:Label>
                <asp:Label ID="L2" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 109; left: 154px; position: absolute; top: 122px"
                    Text="Label"></asp:Label>
                &nbsp; &nbsp; &nbsp;
                <asp:Label ID="Label7" runat="server" Style="z-index: 110; left: 20px; position: absolute;
                    top: 246px; text-align: left" Text="Disagio Economico" Width="119px"></asp:Label>
                <asp:Label ID="L6" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 111; left: 154px; position: absolute; top: 246px"
                    Text="Label"></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="Label8" runat="server" Style="z-index: 112; left: 20px; position: absolute;
                    top: 271px; text-align: left" Text="Disagio Familiare" Width="142px"></asp:Label>
                <asp:Label ID="Label1" runat="server" Style="z-index: 113; left: 20px; position: absolute;
                    top: 296px; text-align: left" Text="Disagio Residenza" Width="142px"></asp:Label>
                <asp:Label ID="L7" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 114; left: 154px; position: absolute; top: 271px"
                    Text="Label"></asp:Label>
                <asp:Label ID="Label10" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 115; left: 154px; position: absolute; top: 296px"
                    Text="Label" Width="44px"></asp:Label>
                <asp:Label ID="Label9" runat="server" Style="z-index: 116; left: 20px; position: absolute;
                    top: 324px; text-align: left" Text="ISBAR" Width="101px"></asp:Label>
                <asp:Label ID="L8" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 117; left: 154px; position: absolute; top: 323px"
                    Text="Label"></asp:Label>
                <asp:Label ID="Label11" runat="server" Style="z-index: 118; left: 20px; position: absolute;
                    top: 349px; text-align: left" Text="ISBARC" Width="149px"></asp:Label>
                <asp:Label ID="L9" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 119; left: 154px; position: absolute; top: 349px"
                    Text="Label"></asp:Label>
                <asp:Label ID="Label12" runat="server" Style="z-index: 120; left: 20px; position: absolute;
                    top: 375px; text-align: left" Text="ISBARC/R" Width="143px"></asp:Label>
                <asp:Label ID="L10" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 121; left: 154px; position: absolute; top: 375px"
                    Text="Label"></asp:Label>
                <asp:Label ID="Label13" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 122; left: 21px; position: absolute; top: 59px"
                    Text="Label"></asp:Label>
                <asp:Label ID="Label14" runat="server" Style="z-index: 123; left: 20px; position: absolute;
                    top: 197px; text-align: left" Text="PSE" Width="76px"></asp:Label>
                <asp:Label ID="LBLPSE" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 124; left: 154px; position: absolute; top: 197px"
                    Text="Label"></asp:Label>
                <asp:Label ID="LBLVSE" runat="server" CssClass="CssValori" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" Style="z-index: 127; left: 315px; position: absolute; top: 197px"
                    Text="Label"></asp:Label>
                <asp:Label ID="Label15" runat="server" Style="z-index: 126; left: 272px; position: absolute;
                    top: 197px; text-align: left" Text="VSE" Width="76px"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <br />

</body>
</html>

