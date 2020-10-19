<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoRegistrazioni.aspx.vb"
    Inherits="Contratti_ElencoSimulazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Registrazioni</title>
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
        <table style="left: 0px; width: 100%; position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                        Registrazioni</strong></span><br />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td class="stileComponenti">
                    <fieldset style="width: 100%">
                        <legend style="font-family: Arial; font-size: 9pt; font-weight: bold; color: Black;">
                            Prima Stipula</legend>
                        <br />
                        <asp:Label ID="lblTbl" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 28px;
                            position: static; top: 203px" TabIndex="1"></asp:Label>
                        <div style="overflow: auto;">
                            <asp:Label ID="lblPrimaStip" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 28px;
                                position: static; top: 203px" Width="100%" TabIndex="1"></asp:Label>
                        </div>
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
                <td align="right">
                    <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                        OnClientClick="self.close();" />
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
        </table>
    </div>
    </form>
</body>
</html>
