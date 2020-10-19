<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProssimaBolletta.aspx.vb" Inherits="Contratti_ProssimaBolletta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self"/>
    <title>Prossima Bolletta</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%">
    <tr>
    <td class="style1" style="text-align: center">
    Imposta Prossima Bolletta
    </td>
    </tr>
    <tr>
    <td style="text-align: center">
        &nbsp; &nbsp;</td>
    </tr>
    <tr>
    <td style="text-align: center">
        &nbsp;&nbsp; &nbsp;</td>
    </tr>
    </table>
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="12pt" 
                        Text="Mese Prossima Bolletta"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbMese" runat="server" Font-Names="arial" 
                        Font-Size="12pt" Width="150px">
                        <asp:ListItem Value="01">Gennaio</asp:ListItem>
                        <asp:ListItem Value="02">Febbraio</asp:ListItem>
                        <asp:ListItem Value="03">Marzo</asp:ListItem>
                        <asp:ListItem Value="04">Aprile</asp:ListItem>
                        <asp:ListItem Value="05">Maggio</asp:ListItem>
                        <asp:ListItem Value="06">Giugno</asp:ListItem>
                        <asp:ListItem Value="07">Luglio</asp:ListItem>
                        <asp:ListItem Value="08">Agosto</asp:ListItem>
                        <asp:ListItem Value="09">Settembre</asp:ListItem>
                        <asp:ListItem Value="10">Ottobre</asp:ListItem>
                        <asp:ListItem Value="11">Novembre</asp:ListItem>
                        <asp:ListItem Value="12">Dicembre</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="12pt" 
                        Text="Anno Prossima Bolletta"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbAnno" runat="server" Font-Names="arial" 
                        Font-Size="12pt" Width="150px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;</td>
                <td>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;</td>
                <td>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:ImageButton ID="imgSalva" runat="server" 
                        ImageUrl="~/NuoveImm/Img_SalvaGrande.png" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <img id="imgEsci" alt="" src="../NuoveImm/Img_EsciCorto.png" onclick="self.close();" style="cursor:pointer" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
