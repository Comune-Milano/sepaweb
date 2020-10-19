<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Ricerca_Utenze_Fornitori.aspx.vb" Inherits="MANUTENZIONI_Ricerca_Utenze_Fornitori" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pagina senza titolo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="z-index: 1; left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg);
            width: 674px; position: absolute; top: 0px">
            <tr>
                <td style="width: 670px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        Utenze per Fornitore</strong></span><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 74px; position: absolute; top: 108px" TabIndex="-1">Fornitore*</asp:Label>
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                        Style="z-index: 2; left: 535px; position: absolute; top: 304px" TabIndex="5"
                        ToolTip="Home" />
                    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                        Style="z-index: 1; left: 407px; position: absolute; top: 304px" TabIndex="4"
                        ToolTip="Avvia Ricerca" />
                    <asp:DropDownList ID="cmbFornitore" runat="server" Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 10; left: 140px; position: absolute; top: 109px" Width="410px">
                    </asp:DropDownList>
                    <br />
                    &nbsp;&nbsp;<br />
                    <br />
                    <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="left: 10px; position: absolute; top: 282px" Text="Label"
                        Visible="False" Width="624px"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 74px; position: absolute; top: 148px">Associata a:</asp:Label>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="50px" Style="z-index: 100; left: 141px; position: absolute; top: 148px"
                        TextAlign="Left" Width="120px">
                        <asp:ListItem Selected="True" Value="0">COMPLESSI</asp:ListItem>
                        <asp:ListItem Value="1">EDIFICI</asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
