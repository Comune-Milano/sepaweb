<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Prenotazione.aspx.vb" Inherits="Prenotazione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Prenotazione Domanda</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span style="font-size: 14pt; font-family: Arial">Prenotazione Domanda di Bando<br />
            <br />
            <asp:Label ID="Label1" runat="server" Font-Names="Times New Roman" Font-Size="12pt"
                Style="z-index: 100; left: 11px; position: absolute; top: 54px" Text="Cognome"></asp:Label>
            <asp:Label ID="Label2" runat="server" Font-Names="Times New Roman" Font-Size="12pt"
                Style="z-index: 101; left: 11px; position: absolute; top: 85px" Text="Nome"></asp:Label>
            <asp:Label ID="Label3" runat="server" Font-Names="Times New Roman" Font-Size="12pt"
                Style="z-index: 102; left: 11px; position: absolute; top: 116px" Text="Codice F."></asp:Label>
            <asp:Label ID="Label4" runat="server" Font-Names="Times New Roman" Font-Size="12pt"
                Style="z-index: 103; left: 11px; position: absolute; top: 147px" Text="Telefono"></asp:Label>
            <asp:Button ID="btnUscita" runat="server" Style="z-index: 104; left: 323px; position: absolute;
                top: 200px" TabIndex="7" Text="Esci" />
            <asp:Button ID="btnMemo" runat="server" Style="z-index: 105; left: 215px; position: absolute;
                top: 201px" TabIndex="6" Text="Memorizza" />
            <asp:Button ID="btnRicevuta" runat="server" Style="z-index: 106; left: 13px; position: absolute;
                top: 201px" TabIndex="5" Text="Ricevuta" Visible="False" />
            <asp:TextBox ID="txtCognome" runat="server" Style="z-index: 107; left: 98px; position: absolute;
                top: 52px" Width="222px"></asp:TextBox>
            <asp:TextBox ID="txtNome" runat="server" Style="z-index: 108; left: 98px; position: absolute;
                top: 83px" TabIndex="2" Width="222px"></asp:TextBox>
            <asp:TextBox ID="txtCF" runat="server" Style="z-index: 109; left: 98px; position: absolute;
                top: 113px" TabIndex="3" Width="222px"></asp:TextBox>
            <asp:TextBox ID="txtTel" runat="server" Style="z-index: 110; left: 98px; position: absolute;
                top: 145px" TabIndex="4" Width="222px"></asp:TextBox>
            &nbsp;
            <asp:Panel ID="Panel1" runat="server" BackColor="#FFFFC0" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="2px" Height="149px" Style="z-index: 111; left: 11px; position: absolute;
                top: 91px" Visible="False" Width="349px">
                <asp:LinkButton ID="CFLABEL" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                    Height="45px" Style="z-index: 100; left: 2px; position: absolute; top: 3px" TabIndex="20"
                    Visible="False" Width="340px"></asp:LinkButton>
                <asp:Label ID="Label5" runat="server" Font-Names="Times New Roman" Font-Size="12pt"
                    Style="z-index: 101; left: 58px; position: absolute; top: 67px" Text="Vuoi memorizzare questa prenotazione?"
                    Visible="False"></asp:Label>
                <asp:LinkButton ID="LSI" runat="server" Style="z-index: 102; left: 134px; position: absolute;
                    top: 100px" Visible="False">SI</asp:LinkButton>
                <asp:LinkButton ID="LNO" runat="server" Style="z-index: 104; left: 188px; position: absolute;
                    top: 100px" Visible="False" Width="16px">NO</asp:LinkButton>
            </asp:Panel>
            <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="7pt" Style="z-index: 113;
                left: 13px; position: absolute; top: 248px" Visible="False" Width="348px"></asp:Label>
        </span>
    
    </div>
    </form>
</body>
</html>
