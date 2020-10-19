<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DatiUTMillesimale.aspx.vb" Inherits="CENSIMENTO_DatiUTMillesimale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Dati Utenza Millesimale</title>
</head>
<body bgColor="#ffffff">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 8px; position: absolute; top: 40px" Width="136px">TABELLA MILLESIMALE</asp:Label>
        <asp:DropDownList ID="cmbTabMillesimale" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 144px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 40px" TabIndex="1" Width="448px">
        </asp:DropDownList>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 8px; position: absolute; top: 72px" Width="136px">TIPOLOGIA COSTO</asp:Label>
        <asp:DropDownList ID="cmbTipolCatasto" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 144px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 72px" TabIndex="2" Width="448px">
        </asp:DropDownList>
        <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            Style="z-index: 100; left: 485px; position: absolute; top: 143px" ToolTip="Salva" TabIndex="4" />
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 8px; position: absolute; top: 104px" Width="128px">Percentuale Ripartizione</asp:Label>
        <asp:TextBox ID="txtPercRipart" runat="server" MaxLength="3" Style="left: 144px;
            position: absolute; top: 104px" Width="64px"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 224px; position: absolute; top: 112px" Width="16px">%</asp:Label>
        &nbsp;
        <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            Height="1px" Style="z-index: 100; left: 192px; position: absolute; top: 8px" Width="200px">DATI UTENZA MILLESIMALE</asp:Label>
        <asp:ImageButton ID="ImButEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="left: 556px; position: absolute; top: 143px" ToolTip="Esci" TabIndex="5" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPercRipart"
            ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 216px; position: absolute;
            top: 104px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator>
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 3; left: 8px; position: absolute; top: 134px"
            Text="Label" Visible="False" Width="436px"></asp:Label>
    
    </div>
    </form>
</body>
</html>
