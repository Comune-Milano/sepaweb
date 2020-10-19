<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaInquilini.aspx.vb" Inherits="Condomini_RicercaInquilini" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>RicInquilino</title>
</head>
<body bgColor="#f2f5f1" style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg)">
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <div>
        <asp:Label ID="lblContratto" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="16pt"
            ForeColor="#660000" Text="Ricerca Condomini per Inquilino" Width="758px" 
            TabIndex="9"></asp:Label>&nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" 
            ImageUrl="Immagini/Img_Home.png" Style="z-index: 106;
                left: 538px; position: absolute; top: 327px; height: 20px;" TabIndex="8" 
            ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="Immagini/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 406px; position: absolute; top: 327px" TabIndex="7"
            ToolTip="Avvia Ricerca" />
        &nbsp;
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 12px; position: absolute; top: 413px" Text="Label"
            Visible="False" Width="624px" TabIndex="10"></asp:Label>
        &nbsp; &nbsp;
        <asp:Label ID="lblCognome0" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 104; left: 50px; position: absolute; top: 142px" TabIndex="-1">Nome</asp:Label>
        <asp:Label ID="lblCognome" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 104; left: 50px; position: absolute; top: 118px" TabIndex="-1">Cognome</asp:Label>
        <asp:Label ID="lblCodFisc" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 104; left: 50px; position: absolute; top: 165px" TabIndex="-1">Cod. Fiscale</asp:Label>
        <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 132px; position: absolute;
            top: 115px" TabIndex="1" Width="241px"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 104; left: 50px; position: absolute; top: 238px" TabIndex="-1">Cod. Contratto</asp:Label>
        <asp:TextBox ID="txtCodContratto" runat="server" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" MaxLength="50" Style="z-index: 102; left: 132px; position: absolute;
            top: 235px" TabIndex="6" Width="241px"></asp:TextBox>
        <asp:TextBox ID="txtNome" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Style="z-index: 102; left: 132px; position: absolute; top: 138px"
            TabIndex="2" Width="241px"></asp:TextBox>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 104; left: 50px; position: absolute; top: 190px" TabIndex="-1">Rag. Sociale</asp:Label>
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 104; left: 50px; position: absolute; top: 213px" TabIndex="-1">P. IVA</asp:Label>
        <asp:TextBox ID="txtRagSociale" runat="server" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 132px; position: absolute;
            top: 186px" TabIndex="4" Width="241px"></asp:TextBox>
        <asp:TextBox ID="txtPiva" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Style="z-index: 102; left: 132px; position: absolute; top: 210px"
            TabIndex="5" Width="241px"></asp:TextBox>
        <asp:TextBox ID="txtCf" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Style="z-index: 102; left: 132px; position: absolute; top: 162px"
            TabIndex="3" Width="241px"></asp:TextBox>
        &nbsp; &nbsp; &nbsp;
    </div>
    </form>
</body>