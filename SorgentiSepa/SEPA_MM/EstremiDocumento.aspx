<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EstremiDocumento.aspx.vb" Inherits="EstremiDocumento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            font-size: small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    Estremi Documenti di Riconoscimento</td>
            </tr>
        </table>
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL" 
            Font-Size="10pt" Text="Label"></asp:Label>
        <br />
        <br />
        <br />
    
    </div>
    <table style="width: 100%; font-family: Arial; font-size: 10px;">
        <tr>
            <td>
                Carta di Identità N.</td>
            <td>
                <asp:Label ID="lblCartaNumero" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                Del</td>
            <td>
                <asp:Label ID="lblCartaDel" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt"></asp:Label>
            </td>
            <td>
                Rilasciata da</td>
            <td>
                <asp:Label ID="lblCartaRilasciata" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNatoEstero" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt" Text="Italiano nato all'estero!" 
                    Visible="False"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;</td>
            <td>
                &nbsp;&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Permesso Soggiorno N.</td>
            <td>
                <asp:Label ID="lblPermessoNumero" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                Del</td>
            <td>
                <asp:Label ID="lblPermessoDel" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                Scade il</td>
            <td>
                <asp:Label ID="lblPermessoScade" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                Data Rinnovo</td>
            <td>
                <asp:Label ID="lblPermessoRinnovo" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblContinuativo" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt" Text="CONTINUATIVO!" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;</td>
            <td>
                &nbsp;&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Carta Soggiorno N.</td>
            <td>
                <asp:Label ID="lblSoggiornoNumero" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                Del</td>
            <td>
                <asp:Label ID="lblSoggiornoDel" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                Tipo Lavoro</td>
            <td>
                <asp:Label ID="lblSoggiornoLavoro" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <br />
&nbsp;
    <br />
&nbsp;
    <br />
    </form>
    <table style="width:100%;">
        <tr>
            <td style="text-align: right">
                <img onclick="self.close();" alt="" src="NuoveImm/Img_EsciCorto.png" 
                    style="cursor: pointer" /></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</body>
</html>
