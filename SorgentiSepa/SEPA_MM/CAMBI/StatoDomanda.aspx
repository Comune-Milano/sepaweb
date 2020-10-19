<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StatoDomanda.aspx.vb" Inherits="CAMBI_StatoDomanda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Stato Domanda</title>
</head>
<body style="font-size: 12pt; font-family: Times New Roman; background-attachment: fixed; background-image: url(../NuoveImm/SfondoStatoDomanda.jpg); background-repeat: no-repeat;">
    <table align="center" cellpadding="0" cellspacing="0" style="z-index: 100; left: 10px;
        width: 450px; position: absolute; top: 11px; height: 300px">
        <tr>
            <td style="width: 616px; height: 30px; color: #660000;">
                <img id="STAMPA" onclick="javascript:window.print();" src="../IMG/print.jpg" style="z-index: 100; left: 429px; position: absolute;
                    top: 1px; cursor: pointer;" alt="Stampa questa finestra" />
                <span style="font-size: 16pt; font-family: Arial"><strong>Stato Domanda</strong></span></td>
        </tr>
        <tr>
            <td  style="border-right: dimgray 1px solid; border-left: dimgray 1px solid;
                width: 616px; border-bottom: dimgray 1px solid; height: 284px; text-align: center"
                valign="top">
                <br />
                <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 100;
                    left: 7px; position: absolute; top: 49px" Text="Protocollo"></asp:Label>
                <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 101;
                    left: 7px; position: absolute; top: 73px" Text="Nominativo"></asp:Label>
                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 102;
                    left: 7px; position: absolute; top: 97px" Text="STATO"></asp:Label>
                <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 103;
                    left: 7px; position: absolute; top: 265px" Text="Motivazione"></asp:Label>
                <asp:Label ID="lblpg" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                    Style="z-index: 104; left: 87px; position: absolute; top: 39px; text-align: left"
                    Width="342px"></asp:Label>
                <asp:Label ID="lblNominativo" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="9pt" Style="z-index: 105; left: 87px; position: absolute; top: 72px;
                    text-align: left" Width="342px"></asp:Label>
                <asp:Label ID="lblStato" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                    Style="z-index: 106; left: 88px; position: absolute; top: 97px; text-align: left"
                    Width="342px"></asp:Label>
                <asp:Label ID="lblMotivo" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                    Height="34px" Style="z-index: 107; left: 88px; position: absolute; top: 265px;
                    text-align: left" Width="342px"></asp:Label>
                <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 108;
                    left: 7px; position: absolute; top: 122px" Text="ISBARC/R ATTUALE"></asp:Label>
                <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 109;
                    left: 7px; position: absolute; top: 144px" Text="ISBARC/R GRADUATORIA"></asp:Label>
                <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 110;
                    left: 7px; position: absolute; top: 167px" Text="Nucleo"></asp:Label>
                <asp:Label ID="lblNucleo" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
                    Height="93px" Style="z-index: 111; left: 89px; position: absolute; top: 168px;
                    text-align: left" Width="342px"></asp:Label>
                <asp:Label ID="LBLiSBARCOP" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                    Style="z-index: 112; left: 166px; position: absolute; top: 122px; text-align: left"
                    Width="261px"></asp:Label>
                <asp:Label ID="LBLISBARCGR" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                    Style="z-index: 114; left: 166px; position: absolute; top: 144px; text-align: left"
                    Width="261px"></asp:Label>
                <a href="CodiciEsclusione.aspx" target="_blank"> <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="8pt" Style="left: 0px;
                    position: absolute; top: 318px" Text="Clicca qui per Visualizza il significato dei codici di esclusione" Width="301px"></asp:Label></a>
                <br />
                <br />
            </td>
        </tr>
    </table>
    <br />
    <br />

</body>
</html>
