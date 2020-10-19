<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaOccupante.aspx.vb"
    Inherits="PED_RicercaOccupante" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ricerca U.I.</title>
    <style type="text/css">
        .style1
        {
            left: 450px;
            width: 100px;
            padding: 1px 1px 8px 15px;
        }
        .style2
        {
            left: 550px;
            width: 50px;
            padding: 1px 1px 8px 40px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="left: 0px; background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; background-repeat: no-repeat;">
        <table width="100%">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        U.I.</strong></span><br />
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
                <td class="style1">
                    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Cognome</asp:Label>
                </td>
                <td width="250px">
                    <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="arial" Font-Size="10pt" Width="245px" TabIndex="1"></asp:TextBox>
                </td>
                <td class="style2">
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Nome</asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNome" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
                        Font-Size="10pt" Width="245px" TabIndex="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Cod.Fiscale</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCF" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
                        Font-Size="10pt" Width="245px" TabIndex="3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Rag.Sociale</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRS" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
                        Font-Size="10pt" Width="245px" TabIndex="4"></asp:TextBox>
                </td>
                <td class="style2">
                    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">P.Iva</asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtIva" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
                        Font-Size="10pt" Width="245px" TabIndex="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Cod.Contratto</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCodContr" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="arial" Font-Size="10pt" Width="245px" TabIndex="6"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="left: 450px; width: 100px; padding: 1px 1px 8px 15px;">
                            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Indirizzo</asp:Label>
                        </td>
                        <td width="265px">
                            <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" BackColor="White"
                                Font-Names="arial" Font-Size="10pt" Height="20px" Style="border: 1px solid black;
                                z-index: 111;" TabIndex="7" Width="252px">
                            </asp:DropDownList>
                        </td>
                        <td style="padding: 1px 1px 8px 25px;width:50px">
                            <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">N.Civico</asp:Label>
                        </td>
                        <td width="120px">
                            <asp:DropDownList ID="cmbCivico" runat="server" AutoPostBack="True" BackColor="White"
                                Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                                border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                TabIndex="8" Width="80px">
                            </asp:DropDownList>
                        </td>
                        <td width="45px">
                            <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Interno</asp:Label>
                        </td>
                        <td width="100px">
                            <asp:DropDownList ID="cmbInterno" runat="server" BackColor="White" Font-Names="ARIAL"
                                Font-Size="10pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; border-left: black 1px solid; border-bottom: black 1px solid;"
                                TabIndex="9" Width="80px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Red" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table width="90%" style="height: 250px">
            <tr align="right">
                <td>
                    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                        TabIndex="10" ToolTip="Avvia Ricerca" />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                        TabIndex="11" ToolTip="Chiudi" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
