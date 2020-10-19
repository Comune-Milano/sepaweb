<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Autenticazione.aspx.vb" Inherits="ANAUT_Autenticazione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
		<script type="text/javascript">
		    var Uscita;
		    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Richiesta Password</title>
</head>
<body bgcolor="#f2f5f1" background="../NuoveImm/SfondoMaschere.jpg">
    <form id="form1" runat="server">
    <div>
        <strong><span style="font-size: 14pt; font-family: Arial; color: #660000;">&nbsp;<asp:Label
            ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="16pt"
            ForeColor="#660000"></asp:Label></span></strong>
        <asp:TextBox ID="txtpw" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 101;
            left: 70px; position: absolute; top: 78px" TextMode="Password" TabIndex="1"></asp:TextBox>
        &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 102; left: 533px; position: absolute; top: 202px" TabIndex="8" ToolTip="Home" />
        <asp:ImageButton ID="btnAccedi" runat="server" ImageUrl="~/NuoveImm/Img_Conferma.png"
            Style="z-index: 103; left: 428px; position: absolute; top: 203px" TabIndex="3" ToolTip="Conferma" />
        <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" ForeColor="Red"
            Style="z-index: 105; left: 70px; position: absolute; top: 106px" Visible="False"
            Width="217px"></asp:Label>
        <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" Style="left: 18px;
            position: absolute; top: 80px" Text="Password"></asp:Label>
    
    </div>
    </form>
</body>
</html>
