<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SES15.aspx.vb" Inherits="AMMSEPA_Controllo_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:TextBox ID="TextBox2" runat="server" Width="70px"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text="Conferma" />
        </asp:View>
        <asp:View ID="View2" runat="server">
            <asp:TextBox ID="TextBox1" runat="server" Width="656px"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Cerca" />
            <br />
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </asp:View>
    </asp:MultiView>
    </form>
</body>
</html>
