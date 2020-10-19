<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Ricevuta.aspx.vb" Inherits="AutoCompilazione_Ricevuta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ricevuta</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="border-right: red 1px solid; width: 86px; border-bottom: red 1px solid">
                    <img src="Immagini/Milano.gif" /></td>
                <td style="border-bottom: red 1px solid" valign="top">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="border-right: red 1px solid; width: 86px">
                    <img src="Immagini/LogoComune.gif" /></td>
                <td style="text-align: left" valign="middle">
                    <strong><span style="font-size: 16pt; font-family: Arial">Ricevuta di registrazione
                        Domanda di Bando E.R.P.</span></strong></td>
            </tr>
            <tr>
                <td style="border-right: red 1px solid; width: 86px">
                    &nbsp;
                </td>
                <td style="text-align: left" valign="top">
                    <span style="font-size: 16pt; font-family: Arial">
                        <table width="80%" style="font-weight: bold">
                            <tr style="font-size: 12pt">
                                <td style="text-align: left">
                                    <span style="font-family: Arial"></span></td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 404px; text-align: left">
                                                <span style="font-size: 10pt">
                                                Numero di registrazione provvisorio:</span></td>
                                            <td style="text-align: left">
                                                <asp:Label
                                        ID="Label1" runat="server" Text="Label" Width="179px" style="text-align: left"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 404px; text-align: left">
                                            </td>
                                            <td style="text-align: left">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 404px; text-align: left">
                                                <span style="font-size: 10pt">
                                                Richiedente:</span></td>
                                            <td style="text-align: left">
                                        <asp:Label ID="Label2" runat="server" Text="Label" Width="355px" style="text-align: left"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 404px">
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 404px; text-align: left;">
                                                <span style="font-size: 10pt">
                                                Numero di telefono al quale sarai contattato</span></td>
                                            <td style="text-align: left">
                                        <asp:Label ID="lblTelefono" runat="server" Width="239px" style="text-align: left"></asp:Label></td>
                                        </tr>
                                    </table>
                                    </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td style="text-align: center">
                                    <span style="font-family: Arial">
                                        <br />
                                        <span style="font-size: 10pt">Info: Comune di Milano – Sezione Bandi Erp 
                                            email casa.assegnazione@comune.milano.it</span></span></td>
                            </tr>
                        </table>
                    </span>
                </td>
            </tr>
        </table>
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
