<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StornoMassivo.aspx.vb" Inherits="StornoMassivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Storno Massivo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="arial" 
            Font-Size="8pt" Height="20px" Width="200px" />
        <br />
        <br />
        <asp:Button ID="btnStorna" runat="server" Text="Conferma" />
    </div>
    </form>
</body>
</html>
