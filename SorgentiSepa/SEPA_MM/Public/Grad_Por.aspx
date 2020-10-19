<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Grad_Por.aspx.vb" Inherits="Public_Grad_Por" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comune di Milano - Graduatoria P.O.R.</title>
</head>
<body background="../immagini/Sfondo.gif">
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
                    <strong><span style="font-size: 10pt"><span style="font-family: Arial"><span style="color: #cc0000">
                        <span style="color: #cc0000">
                        ASSEGNAZIONE ALLOGGI DI EDILIZIA RESIDENZIALE PUBBLICA&nbsp;<br />
                    <span style="color: #cc0000">A CANONE CONCORDATO</span></span></span></span></span></strong></td>
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
                        Si ricorda che si procederà alla proposta di assegnazione di un alloggio previa verifica dei requisiti indicati nel Bando di partecipazione.<br />
                        In
                        caso di esito negativo la domanda sarà respinta.</span></strong><br />
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
                        Font-Size="10pt" NavigateUrl="~/Public/POR_2009_GRADUATORIA_IDONEI.pdf" Style="z-index: 100;
                        left: 0px; position: static; top: 16px" Target="_blank" Width="316px">Visualizza la Graduatoria Generale POR (IDONEI)</asp:HyperLink>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 16px; text-align: left">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 16px; text-align: left">
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="10pt" NavigateUrl="~/Public/POR_2009_GRADUATORIA_NON_IDONEI.pdf" Style="z-index: 100;
                                    left: 0px; position: static; top: 16px" Target="_blank" Width="365px">Visualizza la Graduatoria Generale POR (NON IDONEI)</asp:HyperLink></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
