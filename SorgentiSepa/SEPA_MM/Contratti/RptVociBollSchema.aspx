<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptVociBollSchema.aspx.vb"
    Inherits="Contratti_RptVociBollSchema" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: 8pt;
        }
        #form1
        {
            width: 783px;
        }
        .bottone
        {
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            height: 19px;
            cursor: pointer;
            padding-left: 3px;
        }
        .bottone:hover
        {
            background-color: #FFF5D3;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            height: 19px;
            cursor: pointer;
            padding-left: 3px;
        }
    </style>
</head>
<body style="background-image: url('../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <table style="width: 98%;">
        <tr>
            <td style="font-size: 14pt; color: #801f1c; font-family: Arial">
                <asp:Label ID="lbltitolo" Text="Estrazione voci bolletta" runat="server" Font-Bold="True"
                    Font-Names="Arial" />&nbsp
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td class="style1">
                                        Anno riferimento
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAnnoRiferim" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCIN" runat="server" ControlToValidate="txtAnnoRiferim"
                                            Display="Dynamic" ErrorMessage="!!!" Font-Bold="True" Font-Names="arial" Font-Size="9pt"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtAnnoRiferim"
                                            ErrorMessage="???" Font-Bold="True" ValidationExpression="\d{1,50}" ToolTip="!!"
                                            Font-Names="arial" Font-Size="9pt"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="text-align: right" width="75%">
                            &nbsp;
                        </td>
                        <td style="text-align: right" width="15%">
                            <asp:Button ID="btnRicerca" runat="server" CssClass="bottone" Text="Avvia Report"
                                ToolTip="Avvia Report" />
                        </td>
                        <td style="text-align: right" width="10%">
                            &nbsp;
                        </td>
                        <td style="text-align: right" width="10%">
                            <asp:Button ID="btnHome" runat="server" CssClass="bottone" Text="Home" ToolTip="Home"
                                CausesValidation="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <script type="text/javascript">

        if (document.getElementById('dvvvPre') != null) {

            document.getElementById('dvvvPre').style.visibility = 'hidden';
        }


    </script>
    </form>
</body>
</html>
