<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ParameReCaGest.aspx.vb"
    Inherits="Contratti_ParameReCaGest" %>

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
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Parametro
                    Contabilizzazione Revisione Canone</strong></span><br />
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
                                    Gestione credito/debito teorico</legend>
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
                                        <td style="padding-left: 10px; width: 210px;">
                                            <asp:RadioButtonList ID="rdbListRecaGest" runat="server" Font-Names="Arial" 
                                                Font-Size="10pt" RepeatDirection="Horizontal" Width="500px">
                                                <asp:ListItem Value="1">in Partita Gestionale</asp:ListItem>
                                                <asp:ListItem Value="0">in Partita Contabile</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            &nbsp;</td>
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
    <asp:HiddenField ID="idVersamento" runat="server" Value="0" />
    </form>
</body>
</html>
