<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PaginaErrore.aspx.vb" Inherits="PaginaErrore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                <td style="text-align: center" valign="middle">
                    <strong><span style="font-size: 16pt; font-family: Arial">
                        <img src="AutoCompilazione/IMG/Divieto.png" alt="Errore"/><br />
                        <br />
                        Si è verificato un errore.<br />
                        
                    </span></strong></td>
            </tr>
            <tr>
                <td style="border-right: red 1px solid; width: 86px">
                </td>
                <td style="text-align: center" valign="top">
                    <strong><span style="font-size: 16pt; font-family: Arial">
                        <table width="80%">
                            <tr style="font-size: 12pt">
                                <td style="text-align: left">
                                    <strong><span style="font-family: Arial"><br />
                                </span></strong></td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td style="text-align: left">
                                    
                                    Descrizione Errore:</td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td style="text-align: left">
                    <strong><span style="font-size: 16pt; font-family: Arial">
                                    <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" 
                                        Text="Label"></asp:Label>
                    </span></strong>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td style="text-align: left">
                                    </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td style="text-align: left">
                                    </td>
                            </tr>
                        </table>
                    </span></strong>
                </td>
            </tr>
        </table>
        <br />
        <br />

    </div>
    </form>
</body>
</html>
