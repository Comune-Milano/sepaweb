<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreaLettere.aspx.vb" Inherits="Contratti_Morosita_CreaLettere" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crea Lettere</title>
    <style type="text/css">
        .style2
        {
            font-family: Arial;
            font-weight: bold;
            font-size: small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    
        <span class="style2">I seguenti dati saranno stampati nelle lettere di 
        ingiunzione di pagamento.</span><br class="style2" />
        <span class="style2">Dopo averne verificato l&#39;esattezza, premere il pulsante 
        &quot;Salva e Procedi&quot;</span><br />
    
        <table style="border: 2px solid #990000; width: 863px;">
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="10pt" Text="DIRETTORE DI SETTORE"></asp:Label>
                </td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td>
                    &nbsp;&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="ARIAL" 
                        Font-Size="7pt" Text="titolo"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="ARIAL" 
                        Font-Size="7pt" Text="Cognome"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="ARIAL" 
                        Font-Size="7pt" Text="Nome"></asp:Label>
                </td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbTitoloD" runat="server" Font-Names="arial" 
                        Font-Size="9pt">
                        <asp:ListItem Value="0">Dott.</asp:ListItem>
                        <asp:ListItem Value="1">Dott.ssa</asp:ListItem>
                        <asp:ListItem Value="2">Sig.</asp:ListItem>
                        <asp:ListItem Value="3">Sig.ra</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtCognomeD" runat="server" Font-Names="arial" Font-Size="9pt" 
                        MaxLength="100" TabIndex="2" Width="191px"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtNomeD" runat="server" Font-Names="arial" Font-Size="9pt" 
                        MaxLength="100" TabIndex="3" Width="191px"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    &nbsp;&nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="10pt" Text="RESP. DEL PROCEDIMENTO"></asp:Label>
                </td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="ARIAL" 
                        Font-Size="7pt" Text="titolo"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="ARIAL" 
                        Font-Size="7pt" Text="Cognome"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="ARIAL" 
                        Font-Size="7pt" Text="Nome"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="ARIAL" 
                        Font-Size="7pt" Text="Telefono"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbTitoloR" runat="server" Font-Names="arial" 
                        Font-Size="9pt" TabIndex="4">
                        <asp:ListItem Value="0">Dott.</asp:ListItem>
                        <asp:ListItem Value="1">Dott.ssa</asp:ListItem>
                        <asp:ListItem Value="2">Sig.</asp:ListItem>
                        <asp:ListItem Value="3">Sig.ra</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtCognomeR" runat="server" Font-Names="arial" Font-Size="9pt" 
                        MaxLength="100" TabIndex="5" Width="191px"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtNomeR" runat="server" Font-Names="arial" Font-Size="9pt" 
                        MaxLength="100" TabIndex="6" Width="191px"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtTelefonoR" runat="server" Font-Names="arial" 
                        Font-Size="9pt" MaxLength="100" TabIndex="7" Width="191px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    &nbsp;&nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="10pt" Text="PRATICA TRATTATA DA"></asp:Label>
                </td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="ARIAL" 
                        Font-Size="7pt" Text="titolo"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="ARIAL" 
                        Font-Size="7pt" Text="Cognome"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="ARIAL" 
                        Font-Size="7pt" Text="Nome"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="ARIAL" 
                        Font-Size="7pt" Text="Telefono"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbTitoloT" runat="server" Font-Names="arial" 
                        Font-Size="9pt" TabIndex="8">
                        <asp:ListItem Value="0">Dott.</asp:ListItem>
                        <asp:ListItem Value="1">Dott.ssa</asp:ListItem>
                        <asp:ListItem Value="2">Sig.</asp:ListItem>
                        <asp:ListItem Value="3">Sig.ra</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtCognomeT" runat="server" Font-Names="arial" Font-Size="9pt" 
                        MaxLength="100" TabIndex="9" Width="191px"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtNomeT" runat="server" Font-Names="arial" Font-Size="9pt" 
                        MaxLength="100" TabIndex="10" Width="191px"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtTelefonoT" runat="server" Font-Names="arial" 
                        Font-Size="9pt" MaxLength="100" TabIndex="10" Width="191px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    &nbsp;&nbsp;</td>
                <td style="text-align: left">
                    &nbsp;&nbsp;</td>
                <td style="text-align: left">
                    &nbsp; &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td style="text-align: left">
                    <br />
                    <asp:ImageButton ID="btnProcedi" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Salva_e_Procedi.png" TabIndex="11" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnEsci" runat="server" 
                        ImageUrl="~/NuoveImm/Img_AnnullaBolletta.png" onclientclick="self.close();" 
                        TabIndex="12" />
                </td>
            </tr>
        </table>
    
    </div>

    <asp:HiddenField ID="cond" runat="server" />
    <asp:HiddenField ID="idm" runat="server" />
    <asp:Label ID="Label15" runat="server" ForeColor="Red" TabIndex="25" 
        Visible="False"></asp:Label>
    </form>
            </body>
</html>
