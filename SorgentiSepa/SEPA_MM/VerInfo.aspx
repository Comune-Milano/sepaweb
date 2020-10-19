<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VerInfo.aspx.vb" Inherits="VerInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Info Versione Aggiornamento</title>
    <style type="text/css">
        .style1
        {
            font-size: 14pt;
        }
        .style5
        {
            font-size: 8pt;
        }
        .style6
        {
            font-size: 6pt;
            text-align: center;
        }

    </style>
</head>
<body style="background-image: url('IMG/sfVer.jpg'); background-repeat: repeat-x;">
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
        <td width="5%"></td>
            <td class="style1" style="text-align: center; font-family: Arial" width="90%">
                <table style="width:100%;">
                    <tr>
                        <td align="center" >
                            <fieldset style="border: medium solid #009933; width: 98%; text-align :center ">
                                <legend align="right" style ="color:Black "  ><strong >INFO 
                                    AGGIORNAMENTI VERSIONE</strong></legend>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: left">
                                <div style="width: 100%; height: 400px; overflow: auto; text-align: left;">
                                    <table style="width:100%;">
                                        <tr class="style5">
                                            <td class="style6" width="5%">
                                                &nbsp;</td>
                                            <td class="style6">
                                                &nbsp;</td>
                                            <td class="style6" width="5%">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                &nbsp;</td>
                                            <td>
                                    <asp:Label ID="lblReader" runat="server" Width="97%" Font-Names="Arial" 
                                        Font-Size="8pt"></asp:Label>
                                            </td>
                                            <td width="5%">
                                                &nbsp;</td>
                                        </tr>
                                        </table>
                                </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:ImageButton ID="btnStampa" runat="server" 
                                                            ImageUrl="IMG/Devices-printer-icon.png" ToolTip="Stampa PDF" />
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <img alt="Sistemi e Soluzioni srl"
                                                src="IMG/sfVerSisol.jpg" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset></td>
                    </tr>
                </table>
            </td>
            <td width="5%"></td>
        </tr>
        </table>
    </form>
    </form>
</body>
</html>
