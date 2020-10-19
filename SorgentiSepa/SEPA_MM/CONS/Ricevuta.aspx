<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Ricevuta.aspx.vb" Inherits="CONS_Ricevuta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ricevuta Prenotazione</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td>
                    <img src="../IMG/logoColore.jpg" style="z-index: 100; left: 0px; position: static;
                        top: 0px" /></td>
            </tr>
            <tr>
                <td>
                    &nbsp;Settore Assegnazione Alloggi di Erp<br />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <strong><span style="font-size: 14pt">PRENOTAZIONE APPUNTAMENTO DOMANDA N°
                        <asp:Label ID="Label1" runat="server" Style="z-index: 100; left: 0px; position: static;
                            top: 0px" Text="Label" Width="51px"></asp:Label></span></strong></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblNome" runat="server" Font-Bold="True" Font-Names="Times New Roman"
                        Font-Size="12pt" Style="z-index: 100; left: 0px; position: static; top: 0px"
                        Text="Label" Width="561px"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <span style="font-size: 14pt">Giorno e Ora dell'appuntamento:</span>
                    <asp:Label ID="lblGiorno" runat="server" Font-Bold="True" Font-Names="Times New Roman"
                        Font-Size="14pt" Style="z-index: 100; left: 0px; position: static; top: 0px"
                        Text="Label" Width="395px"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal">c/o<?xml namespace="" ns="urn:schemas-microsoft-com:office:office"
                            prefix="o" ?><o:p></o:p></b></p>
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        &nbsp;<o:p></o:p></p>
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal">L’Ufficio Bandi<o:p></o:p></b></p>
                    <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                        <b style="mso-bidi-font-weight: normal">del Comune di Milano - Settore Assegnazione Alloggi di Erp<o:p></o:p></b></p>
                    <span style="font-size: 12pt; font-family: 'Times New Roman'; mso-fareast-font-family: 'Times New Roman';
                        mso-ansi-language: IT; mso-fareast-language: IT; mso-bidi-language: AR-SA">Via Prelli,
                        39 – 1° piano corpo basso</span></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: justify">
                    Si ricorda che il richiedente dovrà presentarsi munito di documento
                    di identità in corso di validità.</p>
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: justify">
                        &nbsp;<o:p></o:p></p>
                    <p class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: justify">
                        <span style="font-size: 12pt; font-family: 'Times New Roman'; mso-fareast-font-family: 'Times New Roman';
                            mso-ansi-language: IT; mso-fareast-language: IT; mso-bidi-language: AR-SA">L'appuntamento
                            può essere spostato
                    o cancellato, rivolgendosi, con alemno due
                    giorni di preavviso,<span
                                style="mso-spacerun: yes"> &nbsp; </span>presso l'ente che lo fissato oppure
                            telefonando direttamente all’Ufficio Bandi del Comune di Milano al numero 02/884.64427.</span>&nbsp;<br />
                    &nbsp;<br />
                    &nbsp;<br />
                    &nbsp;</p>
                </td>
            </tr>
            <tr>
                <td>
                    Milano, lì
                    <asp:Label ID="lblData" runat="server" Font-Names="Times New Roman" Font-Size="12pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="112px"></asp:Label></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
