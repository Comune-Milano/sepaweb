<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dimensioni.aspx.vb" Inherits="CENSIMENTO_Dimensioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Dimensioni</title>
</head>
<body bgcolor="#ffffff">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; position: absolute; top: 80px" Width="56px">VALORE</asp:Label>
        <asp:TextBox ID="TxtVal" runat="server" MaxLength="10" Style="left: 80px;
            position: absolute; top: 80px; right: 782px;" Width="56px" TabIndex="2"></asp:TextBox>
        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 16px; position: absolute; top: 16px" Width="160px">Tipologia di dimensione</asp:Label>
        <asp:DropDownList ID="DrLDimens" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 16px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 40px" TabIndex="1" Width="400px">
        </asp:DropDownList>
        &nbsp; &nbsp;&nbsp;
    
    </div>
        <asp:ImageButton ID="BtnADD" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            
        Style="z-index: 103; left: 311px; position: absolute; top: 110px; " 
        ToolTip="Salva" TabIndex="3" />
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="z-index: 103; left: 383px; position: absolute; top: 110px" ToolTip="Esci" TabIndex="4" />
        &nbsp;
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
        runat="server" ControlToValidate="TxtVal"
            ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 144px; position: absolute;
            top: 80px" ValidationExpression="^\d{1,7}((,|\.)\d{1,2})?$" Width="1px" 
        ToolTip="Inserire un valore con decimale a precisione doppia"></asp:RegularExpressionValidator>
    <p>
        &nbsp;</p>
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" 
        Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" 
        Style="left: 8px; position: absolute; top: 107px; width: 217px;" Text="Label"
                Visible="False"></asp:Label>
    </form>
</body>
</html>
