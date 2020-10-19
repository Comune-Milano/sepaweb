<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ParamDataScadBoll.aspx.vb"
    Inherits="Contratti_ParamDataScadBoll" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .CssMaiuscolo
        {
            text-transform: uppercase;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; top: 0px">
        <tr>
            <td style="width: 800px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Parametro
                    spostamento data scadenza bollette</strong></span><br />
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
                                    Numero Mesi</legend>
                                <table width="100%">
                                    <tr>
                                        <td style="padding-left: 10px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px;">
                                            <asp:TextBox ID="txtNumMesi" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                                ControlToValidate="txtNumMesi" Display="Dynamic" ErrorMessage="!!"
                                                Font-Bold="True" Font-Names="arial" Font-Size="8pt" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Tale valore sposterà virtualm. la data di scad. delle bollette (in avanti) per la discesa dei cred. gestionali"
                                                Style="font-family: Arial; font-size: 9pt; color: Black;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px; width: 150px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
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
    </form>
</body>
</html>
