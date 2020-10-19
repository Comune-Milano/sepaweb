<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pagina_home_ncp.aspx.vb"
    Inherits="CICLO_PASSIVO_pagina_home_ncp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="CicloPassivo.js" type="text/javascript"></script>
    <link href="CicloPassivo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style2 {
            width: 597px;
            height: 104px;
        }
    </style>
</head>
<body style="height: 600px;" class="sfondo">
    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <table align="right">
                        <tr>
                            <td>
                                <img alt="Guida per l'Utente" src="../Standard/Immagini/guida.png" title="Guida per l'Utente"
                                    style="cursor: pointer" onclick="window.open('Guida_ciclo_passivo_Milano.pdf');" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 400px; vertical-align: top;">
                <td style="text-align: center;">
                    <img alt="Logo Sepa" src="../Images/SepaWeb.png" />
                </td>
            </tr>
            <tr style="height: 100px; vertical-align: bottom;">
                <td style="text-align: center;">
                    <img alt="Logo SES" src="../IMG/sfVerSisol.jpg" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
