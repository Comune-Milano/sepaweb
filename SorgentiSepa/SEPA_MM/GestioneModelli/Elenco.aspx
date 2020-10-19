<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Elenco.aspx.vb" Inherits="GestioneModelli_Elenco" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Modelli</title>
    <style type="text/css">
        .style2
        {
            width: 100px;
        }
        .style3
        {
            width: 64px;
        }
        .style4
        {
            width: 91px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: left">
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px" align="center">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Gestione
                        Modelli</strong></span><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <table style="width: 80%; height: 308px;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="style4" style="background-color: #CCCCCC;">
                                <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="177px">ERP lettera A (CANONE SOCIALE)</asp:Label>
                            </td>
                            <td class="style2" style="background-color: #CCCCCC">
                                <asp:HyperLink ID="HyperLink2" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=0" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4" style="text-align: left">
                                <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="185px">ERP lettera B (CANONE MODERATO)</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:HyperLink ID="HyperLink7" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=2" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4" style="text-align: left; background-color: #CCCCCC;">
                                <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="185px">ERP CANONE CONVENZIONATO</asp:Label>
                            </td>
                            <td class="style2" style="background-color: #CCCCCC">
                                <asp:HyperLink ID="HyperLink11" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=12" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4" style="text-align: left">
                                <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="107px">431/98</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=1" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4" style="text-align: left; background-color: #CCCCCC;">
                                <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="107px">431/98 Cooperative</asp:Label>
                            </td>
                            <td class="style2" style="background-color: #CCCCCC">
                                <asp:HyperLink ID="HyperLink4" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=4" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4" style="text-align: left">
                                <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="107px">431/98 POR</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:HyperLink ID="HyperLink8" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=7" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4" style="text-align: left; background-color: #CCCCCC;">
                                <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="107px">431/98 SPECIALI</asp:Label>
                            </td>
                            <td class="style2" style="background-color: #CCCCCC">
                                <asp:HyperLink ID="HyperLink10" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=11" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="155px">USI DIVERSI</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:HyperLink ID="HyperLink3" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=3" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4" style="background-color: #CCCCCC">
                                <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="155px">USI DIVERSI - POSTO AUTO</asp:Label>
                            </td>
                            <td class="style2" style="background-color: #CCCCCC">
                                <asp:HyperLink ID="HyperLink5" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=5" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="155px">USI DIVERSI - NEGOZI</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:HyperLink ID="HyperLink6" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=6" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4" style="background-color: #CCCCCC">
                                <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="155px">392/78 - ASSOCIAZIONI</asp:Label>
                            </td>
                            <td class="style2" style="background-color: #CCCCCC">
                                <asp:HyperLink ID="HyperLink9" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=8" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="155px">431 ART.15</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:HyperLink ID="HyperLink12" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=13" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4" style="background-color: #CCCCCC">
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="155px">431 ART.15 C.2 bis</asp:Label>
                            </td>
                            <td class="style2" style="background-color: #CCCCCC">
                                <asp:HyperLink ID="HyperLink13" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=14" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="215px">USI DIVERSI - CONCESSIONI SPAZI PUBB.</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:HyperLink ID="HyperLink14" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=15" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                         <tr bgcolor="#CCCCCC">
                            <td class="style4">
                                <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 112;" Width="215px">USI DIVERSI - COMODATO USO GRATUITO</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:HyperLink ID="HyperLink15" runat="server" Font-Names="arial" Font-Size="10pt"
                                    NavigateUrl="~/GestioneModelli/Modifica.aspx?T=16" Target="_blank">Modifica</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
