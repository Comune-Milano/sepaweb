<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StatoDomanda.aspx.vb" Inherits="StatoDomanda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Stato Domanda</title>
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}

// ]]>
</script>
</head>
<body style="font-size: 12pt; font-family: Times New Roman" background="NuoveImm/SfondoStatoDomanda.jpg">
    <table align="center" cellpadding="0" cellspacing="0" style="z-index: 102; left: 13px;
        width: 466px; position: absolute; top: 47px; height: 300px" id="TABLE1" onclick="return TABLE1_onclick()">
        <tr>
            <td style="width: 923px; height: 30px">
                <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 100;
                    left: 18px; position: static; top: 36px" Text="Protocollo"></asp:Label></td>
            <td style="width: 616px; height: 30px">
                <asp:Label ID="lblpg" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                    Style="z-index: 104; left: 87px; position: static; top: 39px; text-align: left"
                    Width="342px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 923px; height: 30px">
                <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 101;
                    left: 7px; position: static; top: 73px" Text="Nominativo"></asp:Label></td>
            <td style="width: 616px; height: 30px">
                <asp:Label ID="lblNominativo" runat="server" Font-Bold="True" Font-Names="arial"
                    Font-Size="9pt" Style="z-index: 105; left: 87px; position: static; top: 72px;
                    text-align: left" Width="342px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 923px; height: 30px">
                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 102;
                    left: 7px; position: static; top: 97px" Text="STATO"></asp:Label></td>
            <td style="width: 616px; height: 30px">
                <asp:Label ID="lblStato" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                    Style="z-index: 106; left: 88px; position: static; top: 97px; text-align: left"
                    Width="342px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 923px; height: 30px">
                <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 108;
                    left: 7px; position: static; top: 122px" Text="ISBARC/R ATTUALE"></asp:Label></td>
            <td style="width: 616px; height: 30px">
                <asp:Label ID="LBLiSBARCOP" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                    Style="z-index: 112; left: 166px; position: static; top: 122px; text-align: left"
                    Width="261px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 923px; height: 30px">
                <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 109;
                    left: 7px; position: static; top: 144px" Text="ISBARC/R GRADUATORIA"></asp:Label></td>
            <td style="width: 616px; height: 30px">
                <asp:Label ID="LBLISBARCGR" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                    Style="z-index: 114; left: 166px; position: static; top: 144px; text-align: left"
                    Width="261px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 923px; height: 30px">
                <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 110;
                    left: 26px; position: static; top: 0px" Text="Nucleo" Width="44px"></asp:Label></td>
            <td style="width: 616px; height: 30px">
                <asp:Label ID="lblNucleo" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
                    Height="93px" Style="z-index: 111; left: 89px; position: static; top: 168px;
                    text-align: left" Width="342px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 923px; height: 30px" valign="top">
                <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 103;
                    left: 7px; position: static; top: 265px" Text="Motivazione"></asp:Label></td>
            <td style="width: 616px; height: 30px">
                <asp:Label ID="lblMotivo" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                    Height="34px" Style="z-index: 107; left: 88px; position: static; top: 265px;
                    text-align: left" Width="342px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 923px; height: 30px">
                </td>
            <td style="width: 616px; height: 30px">
                <a href="CodiciEsclusione.aspx" target="_blank"><span style="font-size: 8pt; font-family: Arial">Clicca qui per Visualizzare il significato
                    dei codici di esclusione</span></a></td>
        </tr>
    </table>
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Stato Domanda</span></strong><br />
    <br />
                &nbsp;
    <img id="STAMPA" alt="Stampa questa finestra" onclick="javascript:window.print();"
        src="IMG/print.jpg" style="z-index: 100; left: 458px; cursor: pointer; position: absolute;
        top: 10px" />

</body>
</html>
