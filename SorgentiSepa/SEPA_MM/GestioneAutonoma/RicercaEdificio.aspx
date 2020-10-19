<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaEdificio.aspx.vb" Inherits="GestioneAutonoma_RicercaEdificio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pagina senza titolo</title>
</head>
<body bgColor="#f2f5f1" style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg)">
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <div>
        <asp:Label ID="lblContratto" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="16pt"
            ForeColor="#660000" 
            Text="Ricerca Gestione Autonoma per Complesso/Edificio" Width="759px"></asp:Label>
        <asp:ImageButton ID="btnAnnulla" runat="server" 
            ImageUrl="~/NuoveImm/Img_Home.png" Style="z-index: 106;
                left: 666px; position: absolute; top: 304px" TabIndex="5" 
            ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 534px; position: absolute; top: 304px" TabIndex="4"
            ToolTip="Avvia Ricerca" />
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 40px; position: absolute; top: 144px">Edifcio</asp:Label>
        <asp:DropDownList ID="cmbEdificio" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 96px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 144px" TabIndex="2"
            Width="488px">
        </asp:DropDownList>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 40px; position: absolute; top: 104px">Complesso</asp:Label>
        <asp:DropDownList ID="DrLComplesso" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 96px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 104px" TabIndex="1"
            Width="488px">
        </asp:DropDownList>
        &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 14px; position: absolute; top: 278px" Text="Label"
            Visible="False" Width="624px"></asp:Label>
    </div>
    </form>
</body>
</html>
