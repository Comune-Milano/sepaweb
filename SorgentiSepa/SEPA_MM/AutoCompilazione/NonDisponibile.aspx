<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NonDisponibile.aspx.vb" Inherits="AutoCompilazione_NonDisponibile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Comune di Milano - Inserimento Domanda ERP</title>
</head>
<body background="Immagini/Sfondo.gif">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 15%; height: 20px; text-align: center">
                    <img alt="Milano" src="Immagini/Milano.gif" /></td>
                <td style="border-left: #cc0000 1px solid; width: 85%; border-bottom: #cc0000 1px solid;
                    height: 20px; text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="border-right: #cc0000 1px solid; border-top: #cc0000 1px solid; width: 7%;
                    text-align: center" valign="top">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center">
                                <img alt="Logo Comune" src="Immagini/LogoComune.gif" /></td>
                        </tr>
                        <tr>
                            <td>
                                <span style="font-size: 10pt; font-family: Arial"></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="left" style="width: 85%">
                    <table width="100%">
                        <tr>
                            <td style="background-image: url(Immagini/BarraSfondo.gif); height: 38px; text-align: left"
                                valign="top">
                                <span style="font-size: 14pt; color: #393a3a">Comune di Milano - Domanda di
                                    Bando E.R.P.</span></td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 253px">
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <img src="IMG/Divieto.png" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 14pt; color: #393a3a">
                                                <br />
                                                La procedura di inserimento Domande è momentaneamente non disponibile.</span></td>
                                    </tr>
                                </table>
                                <br />
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Portale.aspx">Torna al Portale</asp:HyperLink></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
