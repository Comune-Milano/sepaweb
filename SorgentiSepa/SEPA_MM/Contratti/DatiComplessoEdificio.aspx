<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DatiComplessoEdificio.aspx.vb" Inherits="Contratti_DatiComplessoEdificio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dati complesso-Edificio</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            font-size: small;
        }
        .style2
        {
            height: 21px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="12pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <span class="style1">COMPLESSO</span></td>
        </tr>
    </table>
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Cod. Complesso"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblcodicecomplesso" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Gestore"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblgestore" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Livello Possesso"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbllivellopossesso" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Tipo Possesso"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbltipocomplesso" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Denominazione"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDenominazione" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Lotto"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbllotto" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Cod. Ubicazione 392/78"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblubicazione" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Provenienza"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblprovenienza" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Commissariato"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblcommissariato" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Sede Territoriale"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblfiliale" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <span class="style1">EDIFICIO</span></td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Denominazione"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbldenominazioneedificio" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Codice"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblcodiceedificio" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Tipologia"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbltipologiaedificio" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Utilizzo"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblutilizzoedificio" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Tipologia Costruttiva"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbltipologiacostruttiva" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Livello Possesso"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblpossessoedificio" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Condominio"></asp:Label>
            </td>
            <td class="style2">
                <asp:Label ID="lblcondominio" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
            <td class="style2">
                <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="arial" 
                    Font-Size="8pt" Text="Imp. Riscaldamento"></asp:Label>
            </td>
            <td class="style2">
                <asp:Label ID="lblriscaldamento" runat="server" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
