<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaServizi.aspx.vb" Inherits="MANUTENZIONI_RicercaServizi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>RICERCA</title>
</head>
<body bgcolor="#ffffff">
    <form id="form1" runat="server">
    <div>
        &nbsp;&nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 670px">
                    <br />
                    <br />
                    <asp:Label ID="LblRicerca" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"
                        ForeColor="#801F1C" Style="left: 16px; position: absolute; top: 24px" Text=" Ricerca Servizi"></asp:Label>
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
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 25px; position: absolute; top: 237px"
                        Text="Label" Visible="False" Width="535px"></asp:Label>
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
        <asp:Label ID="LblUniCom" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 24px; position: absolute; top: 168px"
            Width="72px">Unità Comune</asp:Label>
        <asp:DropDownList ID="cmbUnitaComune" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 168px" TabIndex="5"
            Width="536px">
        </asp:DropDownList>
        <asp:Label ID="LblUniImmob" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 24px; position: absolute; top: 200px"
            Width="80px">Unità Immobiliare</asp:Label>
        <asp:DropDownList ID="cmbUnitaImmob" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 200px" TabIndex="5"
            Width="536px">
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 535px; position: absolute; top: 320px" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 402px; position: absolute; top: 320px" ToolTip="Avvia Ricerca" />
        &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:Label ID="LblComplesso" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; position: absolute; top: 104px">Complesso</asp:Label>
        <asp:Label ID="LblEdificio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; position: absolute; top: 136px">Edificio</asp:Label>
        <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 104px" TabIndex="5"
            Width="536px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbEdificio" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 136px" TabIndex="5"
            Width="536px">
        </asp:DropDownList>
        &nbsp;
    
    </div>
    </form>
</body>
</html>
