<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ParametriVersamRegistraz.aspx.vb"
    Inherits="Contratti_ParametriVersamRegistraz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Parametri Versamento</title>
    <style type="text/css">
        .CssMaiuscolo
        {
            text-transform: uppercase;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; top: 0px">
        <tr>
            <td style="width: 800px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Parametri
                    per versamento - Registrazione Contratto</strong></span><br />
                <table width="800px">
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 15px" colspan="2">
                            <fieldset style="width: 96%">
                                <legend style="font-family: Arial; font-size: 9pt; font-weight: bold; color: Black;">
                                    Informazioni Conto Corrente</legend>
                                <table width="100%">
                                    <tr>
                                        <td style="padding-left: 10px; width: 210px;">
                                            <asp:Label ID="lblDenominaz" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt">Codice ABI</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtABI" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
                                                Font-Size="10pt" TabIndex="1" Width="150px" CssClass="CssMaiuscolo" MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvABI" runat="server" ControlToValidate="txtABI"
                                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px">
                                            <asp:Label ID="lblCodFisc" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Codice CAB</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCAB" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
                                                Font-Size="10pt" TabIndex="2" Width="150px" CssClass="CssMaiuscolo" MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCAB" runat="server" ControlToValidate="txtCAB"
                                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px;">
                                            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Codice CIN</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCIN" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
                                                Font-Size="10pt" TabIndex="4" Width="150px" CssClass="CssMaiuscolo" MaxLength="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCIN" runat="server" ControlToValidate="txtCIN"
                                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px; width: 150px;">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Num. conto corrente</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumCC" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
                                                Font-Size="10pt" TabIndex="3" Width="150px" CssClass="CssMaiuscolo"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNumCC" runat="server" ControlToValidate="txtNumCC"
                                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px;">
                                            <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Cod. fiscale titolare c.c.</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCFTitolare" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="arial" Font-Size="10pt" TabIndex="5" Width="150px" CssClass="CssMaiuscolo"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCFTitolare" runat="server" ControlToValidate="txtCFTitolare"
                                                Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                        <td align="right" style="padding-right: 15px;">
                            <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                                TabIndex="4" ToolTip="Salva" />
                            <asp:ImageButton ID="btnHome" runat="server" ImageUrl="~/NuoveImm/Img_Home.png" TabIndex="5"
                                ToolTip="Chiudi" CausesValidation="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="idVersamento" runat="server" Value="0" />
    </form>
</body>
</html>
