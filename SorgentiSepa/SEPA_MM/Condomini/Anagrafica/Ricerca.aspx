<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Ricerca.aspx.vb" Inherits="Condomini_Anagrafica_Ricerca" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ricerca Amministratori</title>
    <style type="text/css">
        .style1
        {
            color: #800000;
            font-family: Arial;
            font-size: 14pt;
        }
        .style2
        {
            font-family: Arial;
            font-size: 8pt;
        }

    .CssMaiuscolo { TEXT-TRANSFORM: uppercase}

        </style>
</head>
<body style="background-attachment: fixed; background-image: url('../Immagini/SfondoMascheraContratti.jpg'); background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <table style="width: 76%;">
        <tr>
            <td class="style1">
                <strong>RICERCA ANAGRAFICA AMMINISRATORI CONDOMINIALI</strong></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td class="style2">
                            COGNOME</td>
                        <td>
                            <asp:TextBox ID="txtCognome" runat="server" Width="200px" Font-Names="Arial" 
                                Font-Size="8pt" CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            NOME</td>
                        <td>
                            <asp:TextBox ID="txtNome" runat="server" Width="200px" Font-Names="Arial" 
                                Font-Size="8pt" CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            CODICE FISCALE</td>
                        <td>
                            <asp:TextBox ID="txtCodFiscale" runat="server" Width="200px" Font-Names="Arial" 
                                Font-Size="8pt" CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            PARTITA IVA</td>
                        <td>
                            <asp:TextBox ID="txtPIVA" runat="server" Width="200px" Font-Names="Arial" 
                                Font-Size="8pt" CssClass="CssMaiuscolo"></asp:TextBox>
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
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnCerca" runat="server" 
                                ImageUrl="../Immagini/Img_AvviaRicerca.png" ToolTip="Avvia la ricerca" />
                        </td>
                        <td>
                            <img alt="Torna alla pagina HOME" src="../Immagini/Img_Home.png" 
                                style="float: right; cursor:pointer "onclick="document.location.href='../pagina_home.aspx';" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
