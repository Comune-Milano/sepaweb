<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Ut_Tab_Millesimali.aspx.vb" Inherits="MANUTENZIONI_Ut_Tab_Millesimali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pagina senza titolo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="z-index: 99; left: 0px; background-image: url(../NuoveImm/SfondoMaschere1.jpg);
            width: 674px; position: absolute; top: 0px">
            <tr>
                <td style="width: 670px; height: 423px; text-align: left;">
                    <span style="font-size: 18pt; color: maroon; font-family: Arial"><strong>&nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp;&nbsp; Utenze<br />
                    </strong></span>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
                        Style="z-index: 100; left: 8px; position: absolute; top: 27px" ToolTip="Indietro" />
                    <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                        Style="z-index: 101; left: 64px; position: absolute; top: 27px" ToolTip="Salva" />
                    <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png" Style="z-index: 102;
                        left: 528px; position: absolute; top: 27px" ToolTip="Esci" />
                    &nbsp;<br />
                    <br />
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 13px; position: absolute; top: 126px" TabIndex="-1">Tipologia*</asp:Label><asp:DropDownList ID="cmbTipoUtenze" runat="server" Style="z-index: 10; left: 74px;
                        position: absolute; top: 128px" Width="410px" Font-Names="Arial" Font-Size="10pt">
                        </asp:DropDownList>
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 13px; position: absolute; top: 163px" TabIndex="-1">Fornitore*</asp:Label>
                    <br />
                    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 12px; position: absolute; top: 96px" Width="48px">Edificio</asp:Label>
                    <asp:DropDownList ID="DrLEdificio" runat="server" AutoPostBack="True" BackColor="White"
                        Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                        border-top: black 1px solid; z-index: 111; left: 74px; border-left: black 1px solid;
                        border-bottom: black 1px solid; position: absolute; top: 96px" TabIndex="2" Width="473px" Enabled="False">
                    </asp:DropDownList>
                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 12px; position: absolute; top: 64px">Complesso</asp:Label>
                    <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
                        Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                        border-top: black 1px solid; z-index: 111; left: 74px; border-left: black 1px solid;
                        border-bottom: black 1px solid; position: absolute; top: 64px" TabIndex="1" Width="473px" Enabled="False">
                    </asp:DropDownList>
                    <br />
                    <asp:DropDownList ID="cmbFornitore" runat="server" Style="z-index: 10; left: 74px;
                        position: absolute; top: 164px" Width="410px" Font-Names="Arial" Font-Size="10pt">
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 13px; position: absolute; top: 201px" TabIndex="-1">Contatore</asp:Label>
                    <br />
                    &nbsp;&nbsp;
                    <br />
                    <asp:TextBox ID="txtContatore" runat="server" MaxLength="49" Style="z-index: 10;
                        left: 74px; position: absolute; top: 201px" Width="280px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="txtContratto" runat="server" MaxLength="49" Style="z-index: 10;
                        left: 74px; position: absolute; top: 240px" Width="280px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 13px; position: absolute; top: 240px" TabIndex="-1">Contratto*</asp:Label>
                    <br />
                    <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 1; left: 10px; position: absolute; top: 417px"
                        Text="Label" Visible="False" Width="624px"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtDescrizione" runat="server" Height="52px" MaxLength="100" Style="z-index: 10;
                        left: 74px; position: absolute; top: 277px" TextMode="MultiLine" Width="573px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 13px; position: absolute; top: 278px" TabIndex="-1">Descrizione*</asp:Label>
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
