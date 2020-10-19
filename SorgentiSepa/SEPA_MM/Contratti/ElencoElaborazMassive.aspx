<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoElaborazMassive.aspx.vb"
    Inherits="Contratti_ElencoElaborazMassive" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Elaborazioni Massive</title>
    <style type="text/css">
        .stileComponenti
        {
            font-family: Arial;
            font-size: 12pt;
            padding-left: 20px;
            padding-right: 8px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat: no-repeat;
            left: 0px; top: 0px; position: absolute;" width="800px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                        Elaborazioni Massive</strong></span><br />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td class="stileComponenti">
                    <div style="overflow: auto; height: 400px;">
                        <asp:Label ID="lblReport" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 28px;
                            position: static; top: 203px" Width="620px" TabIndex="1"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../NuoveImm/Img_Home.png" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
