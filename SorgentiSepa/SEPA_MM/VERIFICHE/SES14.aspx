<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SES14.aspx.vb" Inherits="AMMSEPA_Controllo_stampe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
        &nbsp;<asp:Button ID="Button6" runat="server" style="height: 26px" Text="conferma" />
        <asp:CheckBoxList ID="CheckBoxList1" runat="server" Visible="False">
        </asp:CheckBoxList>
        <br />
    
    </div>
    <asp:Button ID="Button1" runat="server" Text="Seleziona Tutti" 
        Visible="False" />
&nbsp;<asp:Button ID="Button2" runat="server" Text="Procedi" Visible="False" />
    <br />
    <br />
    <asp:Label ID="Label1" runat="server"></asp:Label>
    </form>
</body>
</html>