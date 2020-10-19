<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VerificaPos.aspx.vb" Inherits="Public_VerificaPos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Comune di Milano - Graduatoria E.R.P.</title>
</head>
<body background="../immagini/Sfondo.gif">
    <form id="form1" runat="server">
    <div>
        &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
        <table align="center" border="0" width="640" cellpadding="0" cellspacing="0">
            <tr>
                <td style="border-right: #cc0000 1px solid; border-bottom: #cc0000 1px solid; height: 33px;" align="center" valign="bottom" width="20%">
                    <img alt="logo" src="../ImmMaschere/Milano.gif" style="z-index: 103; left: 0px; position: static;
                        top: 0px" /></td>
                <td style="border-bottom: #cc0000 1px solid; width: 578px; height: 33px;" align="center">
                    <strong><span style="font-size: 10pt"><span style="font-family: Arial"><span style="color: #cc0000">
                        ASSEGNAZIONE ALLOGGI DI EDILIZIA RESIDENZIALE PUBBLICA&nbsp;<br />
                    </span><span style="color: #cc0000">GRADUATORIA ERP</span></span></span></strong></td>
            </tr>
            <tr>
                <td style="border-right: #cc0000 1px solid; height: 164px" align="center" valign="top">
                    <img src="../immagini/LogoComune.gif" style="z-index: 103; left: 0px; position: static;
                        top: 0px" /></td>
                <td style="height: 164px; width: 578px;" align="left" valign="top">
                    <br />
                    &nbsp;<asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Text="Per consultare la propria situazione inserire protocollo e/o codice fiscale"
                        Width="477px"></asp:Label><br />
                    &nbsp;<table align="center" cellpadding="0" cellspacing="0" width="90%">
                        <tr>
                            <td style="height: 19px" width="33%">
                                <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 100;
                                    left: 0px; position: static; top: 0px" Text="Protocollo" Width="62px"></asp:Label></td>
                            <td style="width: 179px">
                                <asp:TextBox ID="txtPG" runat="server" Style="z-index: 100; left: 0px; position: static;
                                    top: 0px"></asp:TextBox></td>
                            <td style="height: 19px">
                            </td>
                        </tr>
                        <tr>
                            <td width="33%">
                                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 100;
                                    left: 0px; position: static; top: 0px" Text="Codice Fiscale" Width="87px"></asp:Label></td>
                            <td width="50%">
                                <asp:TextBox ID="txtCF" runat="server" Style="z-index: 100; left: 0px; position: static;
                                    top: 0px"></asp:TextBox>&nbsp;<asp:Button ID="btnCerca" runat="server" Style="z-index: 100;
                                        left: 0px; position: static; top: 0px" Text="Cerca" /></td>
                            <td width="22%">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td style="width: 179px">
                                &nbsp;</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="8pt" NavigateUrl="~/Public/DoveProtocollo.aspx" Style="z-index: 100;
                                    left: 0px; position: static; top: 0px" Target="_blank" Width="169px">Dove si trova il Protocollo</asp:HyperLink></td>
                            <td style="width: 179px">
                                <asp:HyperLink ID="HyperLink2" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="8pt" NavigateUrl="~/Public/DoveCF.aspx" Style="z-index: 100; left: 0px;
                                    position: static; top: 0px" Target="_blank" Width="211px">Quale Codice Fiscale bisogna indicare</asp:HyperLink></td>
                            <td width="34%">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="width: 179px">
                            </td>
                            <td width="34%">
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table align="center" cellpadding="0" cellspacing="0" width="90%">
                        <tr>
                            <td>
                                <asp:Label ID="lblNome" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                    Style="z-index: 100; left: 0px; position: static; top: 0px" Text="lblNome" Visible="False"
                                    Width="363px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblData" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                    Style="z-index: 100; left: 0px; position: static; top: 0px" Text="lblNome" Visible="False"
                                    Width="365px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBLPG" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                    Style="z-index: 100; left: 0px; position: static; top: 0px" Visible="False" Width="343px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="height: 19px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 19px">
                                <asp:Label ID="lblPosizione" runat="server" BackColor="Red" Font-Bold="True" Font-Names="arial"
                                    Font-Size="12pt" ForeColor="White" Style="z-index: 100; left: 0px; position: static;
                                    top: 0px; text-align: center" Text="Label" Visible="False" Width="370px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 19px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 19px">
                                <asp:HyperLink ID="HyperLink3" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="10pt" NavigateUrl="~/Public/GraduatoriaERP.pdf" Style="z-index: 100;
                                    left: 0px; position: static; top: 0px" Target="_blank" Width="285px">Visualizza la Graduatoria Generale di bando</asp:HyperLink></td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 19px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 19px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 19px; text-align: left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 19px">
                                <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 0px; position: static; top: 0px" Width="343px"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <br />
                    </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>



