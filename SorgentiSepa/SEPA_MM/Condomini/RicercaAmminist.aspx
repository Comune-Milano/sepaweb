<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaAmminist.aspx.vb" Inherits="Condomini_RicercaAmminist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pagina senza titolo</title>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg)">
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <div>
        <asp:Label ID="lblContratto" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="16pt"
            ForeColor="#660000" Text="Ricerca Condominio per Amministratore" Width="758px"></asp:Label>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 28px; position: absolute; top: 135px">Amministratore Condominiale</asp:Label>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="Immagini/Img_Home.png" Style="z-index: 106;
                left: 538px; position: absolute; top: 304px" TabIndex="5" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="Immagini/Img_AvviaRicerca.png"
            
            Style="z-index: 111; left: 406px; position: absolute; top: 304px; height: 20px;" TabIndex="4"
            ToolTip="Avvia Ricerca" />
    <asp:DropDownList ID="cmbAmministratori" runat="server" 
        
        
        style="position :absolute; top: 134px; left: 173px; right: 404px; width: 394px;" 
        Font-Names="Arial" Font-Size="9pt" TabIndex="4">
    </asp:DropDownList>
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 14px; position: absolute; top: 278px" Text="Label"
            Visible="False" Width="624px"></asp:Label>
    </div>
    </form>
</body>
</html>
