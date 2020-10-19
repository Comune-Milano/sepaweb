<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicSoloIndirizzo.aspx.vb" Inherits="Condomini_RicSoloIndirizzo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pagina senza titolo</title>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat :no-repeat ">
    <form id="form1" runat="server">
    <div style="width: 789px">
        <asp:Label ID="lblContratto" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="16pt"
            ForeColor="#660000" Text="Ricerca Condominio per Indirizzo" Width="759px"></asp:Label>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="Immagini/Img_Home.png" Style="z-index: 106;
                left: 666px; position: absolute; top: 304px" TabIndex="5" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="Immagini/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 534px; position: absolute; top: 304px" TabIndex="4"
            ToolTip="Avvia Ricerca" />
        &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 41px; position: absolute; top: 122px">Indirizzo</asp:Label>
        <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; right: 333px; left: 96px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 120px" TabIndex="3"
            Width="290px">
        </asp:DropDownList>
        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 401px; position: absolute; top: 124px">Civico</asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 529px; position: absolute; top: 190px" Visible="False">Interno</asp:Label>
        <asp:DropDownList ID="cmbCivico" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 437px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 121px" TabIndex="4" Width="80px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbInterno" runat="server" BackColor="White" Font-Names="ARIAL"
            Font-Size="10pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
            z-index: 111; left: 572px; border-left: black 1px solid; border-bottom: black 1px solid;
            position: absolute; top: 189px" TabIndex="5" Width="80px" Visible="False">
        </asp:DropDownList>
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 14px; position: absolute; top: 278px" Text="Label"
            Visible="False" Width="624px"></asp:Label>
    </div>
    </form>
</body>
</html>
