<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SES18.aspx.vb" Inherits="AMMSEPA_Controllo_AggRendiconto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Esegui</title>
    <style type="text/css">
        #form1
        {
            height: 202px;
            width: 866px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="CONFERMA" />
        <br />
    
    </div>
    <br />
        <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="arial" Font-Size="8pt" 
                        TabIndex="4" Visible="False" Width="690px" />
        <br />
    <br />
        <asp:Button ID="Button2" runat="server" Text="AGG RENDICONTO" 
        Visible="False" />
    &nbsp;<br />
        <asp:Button ID="Button3" runat="server" Text="AGG SCAMBIODATI" 
        Visible="False" />
    </form>
</body>
</html>
