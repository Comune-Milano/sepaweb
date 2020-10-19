<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Grad_Cambi.aspx.vb" Inherits="Public_Grad_Cambi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Comune di Milano - Graduatoria Mobilità Abitativa</title>
</head>
<body style="font-size: 12pt; font-family: Times New Roman" background="../immagini/Sfondo.gif">
    <form id="form1" runat="server">
    <div>
        &nbsp;</div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="640">
            <tr>
                <td align="center" style="border-right: #cc0000 1px solid; border-bottom: #cc0000 1px solid;
                    height: 33px" valign="bottom" width="20%">
                    <img src="../ImmMaschere/Milano.gif" style="z-index: 103; left: 0px; position: static;
                        top: 0px" /></td>
                <td align="center" style="width: 578px; border-bottom: #cc0000 1px solid; height: 33px">
                    <span><span><span>
                        <span style="color: #cc0000; font-size: 10pt; font-family: Arial;"><strong>BANDO DI CONCORSO PER LA MOBILITA' ABITATIVA
                            <br />
                            EDILIZIA RESIDENZIALE PUBBLICA</strong></span></span></span></span></td>
            </tr>
            <tr>
                <td align="center" style="border-right: #cc0000 1px solid; height: 164px" valign="top">
                    <img src="../immagini/LogoComune.gif" style="z-index: 103; left: 0px; position: static;
                        top: 0px" /></td>
                <td align="center" style="width: 578px; height: 164px" valign="top">
                    <br />
                    <table cellpadding="0" cellspacing="0" width="80%">
                        <tr>
                            <td style="text-align: left">
                                <strong><span style="font-size: 8pt; font-family: Arial">
                    Per consultare la graduatoria alfabetica premi sul link sottostante.<br />
                        </span></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp; &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; height: 16px;">
                    <asp:HyperLink ID="HyperLink3" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="10pt" NavigateUrl="~/Public/GRAD_BANDO_CAMBI 2010 PER PROTOCOLLO.pdf" Style="z-index: 100;
                        left: 0px; position: static; top: 16px" Target="_blank" Width="316px">Visualizza la Graduatoria</asp:HyperLink>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 16px; text-align: left">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 16px; text-align: left">
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

