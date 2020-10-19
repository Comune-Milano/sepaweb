<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaInterventi.aspx.vb" Inherits="MANUTENZIONI_RicercaInterventi" %>

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
                        ForeColor="#801F1C" Style="left: 16px; position: absolute; top: 24px" Text=" Ricerca Interventi"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 100; left: 22px; position: absolute; top: 60px"
                        Width="69px" TabIndex="-1">Condominio</asp:Label>
                    <asp:DropDownList ID="cmbCondominio" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 60px"
            Width="68px">
                    </asp:DropDownList>
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
                        ForeColor="Red" Style="z-index: 10; left: 22px; position: absolute; top: 427px"
                        Text="Label" Visible="False" Width="535px"></asp:Label>
                    <asp:Label ID="lblImpianti" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 128; left: 23px; position: absolute; top: 218px"
                        TabIndex="-1" Width="33px">Impianti</asp:Label>
                    <asp:DropDownList ID="cmbImpianti" runat="server" AutoPostBack="True" BackColor="White"
                        Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                        border-top: black 1px solid; z-index: 129; left: 104px; border-left: black 1px solid;
                        border-bottom: black 1px solid; position: absolute; top: 219px" TabIndex="3"
                        Width="537px">
                    </asp:DropDownList>
                    <br />
                    <asp:DropDownList ID="cmbTipoServizio" runat="server" AutoPostBack="True" BackColor="White"
                        Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                        border-top: black 1px solid; z-index: 107; left: 104px; border-left: black 1px solid;
                        border-bottom: black 1px solid; position: absolute; top: 254px" TabIndex="4"
                        Width="214px">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 108; left: 22px; position: absolute; top: 254px"
                        TabIndex="-1">Tipo Servizio</asp:Label>
                    <br />
                    <br />
                    <asp:DropDownList ID="cmbTipoIntervento" runat="server" AutoPostBack="True" BackColor="White"
                        Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                        border-top: black 1px solid; z-index: 104; left: 104px; border-left: black 1px solid;
                        border-bottom: black 1px solid; position: absolute; top: 288px" TabIndex="5"
                        Width="213px">
                    </asp:DropDownList>
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 105; left: 22px; position: absolute; top: 288px"
                        Width="74px">Tipo Intervento</asp:Label>
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
            ForeColor="Black" Style="z-index: 100; left: 23px; position: absolute; top: 155px"
            Width="72px" TabIndex="-1">Unità Comune</asp:Label>
        <asp:DropDownList ID="cmbUnitaComune" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 155px" TabIndex="3"
            Width="536px">
        </asp:DropDownList>
        <asp:Label ID="LblUniImmob" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 23px; position: absolute; top: 187px"
            Width="80px" TabIndex="-1">Unità Immobiliare</asp:Label>
        <asp:DropDownList ID="cmbUnitaImmob" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 187px" TabIndex="4"
            Width="536px">
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 535px; position: absolute; top: 386px" ToolTip="Home" TabIndex="6" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 402px; position: absolute; top: 386px" ToolTip="Avvia Ricerca" TabIndex="5" />
        &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:Label ID="LblComplesso" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 23px; position: absolute; top: 91px" TabIndex="-1">Complesso</asp:Label>
        <asp:Label ID="LblEdificio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 23px; position: absolute; top: 123px" TabIndex="-1">Edificio</asp:Label>
        <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 91px" TabIndex="1"
            Width="536px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbEdificio" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 123px" TabIndex="2"
            Width="536px">
        </asp:DropDownList>
        &nbsp;
    
    </div>
    </form>
</body>
</html>
