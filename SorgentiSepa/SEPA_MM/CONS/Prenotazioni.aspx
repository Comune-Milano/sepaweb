<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Prenotazioni.aspx.vb" Inherits="CONS_Prenotazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Prenotazioni on-line</title>
</head>
<body style="font-size: 12pt; font-family: Times New Roman">
    <form runat="server">
    <table align="center" cellpadding="0" cellspacing="0" style="z-index: 102; left: 10px;
        width: 450px; position: absolute; top: 11px;">
        <tr>
            <td background="../IMG/Prenotazioni_1.jpg" style="width: 616px;" height="32">
                &nbsp;</td>
        </tr>
        <tr>
            <td bgcolor="#dfe2e5" style="border-right: dimgray 1px solid; border-left: dimgray 1px solid;
                width: 616px; height: 284px; text-align: left; border-bottom-width: 1px; border-bottom-color: dimgray;"
                valign="top">
                &nbsp;&nbsp;<br />
                &nbsp;<span style="font-size: 11pt; font-family: Arial"><strong>Elenco disponibilità</strong></span><br />
                &nbsp;<span style="font-size: 8pt; font-family: Arial">(La data scelta potrebbe non
                    essere disponibile al momento della&nbsp; conferma perchè nel frattempo riservata
                    ad altro utente)<br />
                </span>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="TIMES" Font-Size="10pt"
                    Style="z-index: 100; left: 0px; position: static; top: 0px" Visible="False" Width="421px"></asp:Label><br />
                <asp:RadioButtonList ID="R1" runat="server" Style="z-index: 100; left: 0px;
                    position: static; top: 0px" Font-Names="Courier New" Font-Size="8pt" RepeatColumns="1">
                </asp:RadioButtonList>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            </td>
        </tr>
        <tr>
            <td bgcolor="#dfe2e5" style="border-right: dimgray 1px solid; border-left: dimgray 1px solid;
                width: 616px; border-bottom: dimgray 1px solid; height: 39px; text-align: right"
                valign="middle">
                <asp:Button ID="btnSuccessivi" runat="server" Style="z-index: 100; left: 0px; position: static;
                    top: 0px" Text="15 gg Successivi" Visible="False" />&nbsp; &nbsp;<asp:Button ID="btnConferma" runat="server" Style="z-index: 100; left: 0px; position: static;
                    top: 0px" Text="Conferma" />
                &nbsp;
                <asp:Button ID="Button2" runat="server" OnClientClick="window.close()" Style="z-index: 100;
                    left: 0px; position: static; top: 0px" Text="Esci" />
                &nbsp;
            </td>
        </tr>
    </table>
    <br />
    <br />
</form>
</body>
</html>
