<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicevutaPrenotazione.aspx.vb" Inherits="RicevutaPrenotazione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ricevuta Prenotazione Domanda</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <br />
        <br />
        <asp:Image ID="Image1" runat="server" ImageUrl="~/IMG/logo.gif" Style="z-index: 100;
            left: 0px; position: absolute; top: 0px" />
        <br />
        <br />
        <strong><span style="font-size: 14pt">Settore Assegnazione Alloggi di Erp<br />
        </span>Ufficio Bandi</strong>&nbsp; Tel. 02-88464424<br />
        <br />
        <br />
        <br />
        <table width="100%">
            <tr>
                <td style="text-align: center">
                    <span style="font-size: 14pt; font-family: Arial"><strong>RICEVUTA PRENOTAZIONE DOMANDA</strong></span></td>
            </tr>
        </table>
        <br />
        <br />
        <table style="z-index: 107; left: 0px; font-family: Times New Roman; position: static;
            top: 0px" width="100%">
            <tr>
                <td width="30%">
                    <span style="font-size: 14pt; font-family: Arial">Intestata a:</span></td>
                <td style="height: 24px; text-align: left">
                </td>
            </tr>
            <tr>
                <td style="height: 26px" width="30%">
                </td>
                <td style="height: 26px; text-align: center">
                </td>
            </tr>
            <tr>
                <td width="30%">
                    <span style="font-size: 14pt">Cognome:</span></td>
                <td style="height: 24px; text-align: left">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="14pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="387px"></asp:Label></td>
            </tr>
            <tr>
                <td width="30%">
                    <span style="font-size: 14pt">Nome:</span></td>
                <td style="height: 24px; text-align: left">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="14pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="391px"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 26px" width="30%">
                    <span style="font-size: 14pt">Codice Fiscale:</span></td>
                <td style="height: 26px; text-align: left">
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="14pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="396px"></asp:Label></td>
            </tr>
            <tr>
                <td width="30%">
                    <span style="font-size: 14pt">Recapito Telefonico:</span></td>
                <td style="height: 24px; text-align: left">
                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="14pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="435px"></asp:Label></td>
            </tr>
        </table>
    
    </div>
        <br />
        <br />
        <span style="font-size: 14pt">&nbsp;<br />
            <br />
            <table style="z-index: 104; left: 0px; font-family: Times New Roman; position: static;
                top: 0px" width="100%">
                <tr>
                    <td style="text-align: left">
                        <span style="font-size: 14pt">N.B.<br />
                            La presente ricevuta non sostituisce la domanda di bando che dovrà essere regolarmente
                            presentata ai sensi del regolamento regionale 1/2004.<br />
                            La S.V. sarà contattata al recapito telefonico sopra indicato per il completamento
                            della procedura.</span></td>
                </tr>
            </table>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="lblData" runat="server" Font-Names="Arial" Font-Size="12pt" Style="z-index: 102;
                left: 0px; position: static; top: 0px" Width="387px"></asp:Label><br />
            <br />
            <br />
            <table width="100%">
                <tr>
                    <td width="50%">
                        <span style="font-size: 12pt">Operatore</span></td>
                    <td align="center" width="50%">
                        <span style="font-size: 12pt">Il Richiedente</span></td>
                </tr>
                <tr>
                    <td style="width: 83px">
                        <asp:Label ID="lblOperatore" runat="server" Font-Names="Arial" Font-Size="12pt" Style="z-index: 100;
                            left: 0px; position: static; top: 0px" Width="387px"></asp:Label></td>
                    <td align="center" style="width: 120px">
                        <br />
                    </td>
                </tr>
            </table>
        </span>
    </form>
</body>
</html>
