<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaDaComplesso.aspx.vb"
    Inherits="CENSIMENTO_Report_RicercaDaComplesso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ricerca Complesso</title>
    <style type="text/css">
        .style1
        {
            height: 23px;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>
                        <asp:Label ID="lbltitolo" runat="server" Text="Label"></asp:Label></strong></span>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbltitolo2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt">Indicare il Complesso Immobiliare di cui si intende visualizzare le tot. dei dati patrimoniali</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Denominazione complesso - Codice</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="cmbComplesso" runat="server" Font-Names="arial" Font-Size="10pt"
                        TabIndex="1" Width="650px" Height="22px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTipi" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo UI"
                        Visible="False"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chSelezione" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Text="seleziona/deseleziona tutti" Visible="False" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBoxList ID="chTipoUI" runat="server" CellPadding="2" CellSpacing="0" Font-Names="Arial"
                        Font-Size="7pt" RepeatColumns="4" RepeatDirection="Horizontal" BorderColor="#CCCCCC"
                        BorderStyle="Solid" BorderWidth="1px" Width="100%" Visible="False">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lbltipoUI" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipologia unità immobiliare"
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="cmbTipoUI" runat="server" Font-Names="arial" Font-Size="10pt"
                        TabIndex="2" Height="22px" Width="650px" Visible="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lbltotali" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Modalità esposizione"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="cmbTotali" runat="server" Font-Names="arial" Font-Size="10pt"
                        TabIndex="2" Height="22px" Width="300px">
                        <asp:ListItem Value="0">DETTAGLIO</asp:ListItem>
                        <asp:ListItem Value="3">TOTALI FABBRICATO</asp:ListItem>
                        <asp:ListItem Value="1">TOTALI CIVICO</asp:ListItem>
                        <asp:ListItem Value="2">TOTALI COMPLESSO</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:ImageButton ID="btnAvanti" runat="server" ImageUrl="../../NuoveImm/Img_Procedi.png"
                        TabIndex="3" ToolTip="Procedi" />&nbsp;
                    <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../../NuoveImm/Img_Home.png"
                        TabIndex="-1" ToolTip="Home" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="LBLcompl" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="LBLok" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
      
    </script>
</body>
</html>
